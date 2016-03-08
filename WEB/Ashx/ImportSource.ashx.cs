using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Data;
using EyouSoft.Common;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>

    public class ImportSource : IHttpHandler
    {

        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpCookie cookie = request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            request.Cookies.Set(cookie);
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            /*-------------------------------------*/
            // Get the file data.
            HttpPostedFile image_upload = Request.Files["Filedata"];

            if (image_upload == null || image_upload.ContentLength <= 0)//是否有文件
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件为空'}");
                Response.End();
            }
            else if (image_upload.ContentLength > 1048576)//文件大小是否超过了1MB
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件超过了指定的大小'}");
                Response.End();
            }
            //判断文件类型 是否是Excel
            string fileExtension = System.IO.Path.GetExtension(image_upload.FileName).ToLower();
            string mime = Utils.GetMimeTypeByFileExtension(fileExtension);
            if (((fileExtension == ".xls" || fileExtension == ".xlsx")
                && (mime == "application/vnd.ms-excel")) || fileExtension == ".txt")//是Excel或txt
            {
                //设置文件名
                Random rnd = new Random();
                string saveFileName = DateTime.Now.ToFileTime().ToString() + rnd.Next(1000, 99999).ToString() + fileExtension;
                rnd = null;
                string resultArr = null;
                //保存文件
                string dPath = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/temp/");
                if (!Directory.Exists(dPath))//如果目录不存在就创建目录
                {
                    Directory.CreateDirectory(dPath);
                }
                string fPath = dPath + saveFileName;//保存路径
                image_upload.SaveAs(fPath);//保存文件
                if (fileExtension == ".txt")//如果是txt文件
                {
                    resultArr = GetTxtTableData(fPath);
                }
                else
                {   //excel文件
                    IList<string> tblNames = this.GetExcelTableName(fPath);
                    if (tblNames != null && tblNames.Count > 0)
                    {
                        resultArr = this.GetExcelTableData(fPath, tblNames[0]);
                    }
                }

                File.Delete(fPath);

                context.Response.Write(resultArr == null ? "{error:'文件中找不到有效内容'}" : resultArr);
            }
            else//不是Excel文件
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件不是有效的Excel文件或Txt文件'}");
                Response.End();
            }
        }

        /// <summary>  
        /// 获取EXCEL的表 表名字列   
        /// </summary>  
        /// <param name="fPath">Excel文件</param>  
        /// <returns>数据表</returns>  
        private IList<string> GetExcelTableName(string fPath)
        {
            IList<string> tblNames = new List<string>();

            try
            {
                if (System.IO.File.Exists(fPath))
                {
                    OleDbConnection excelConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + fPath);
                    excelConn.Open();
                    DataTable tb = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    excelConn.Close();
                    excelConn.Dispose();

                    if (tb != null)
                    {
                        foreach (DataRow dr in tb.Rows)
                        {
                            tblNames.Add(dr[2].ToString());
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }


            return tblNames;
        }

        #region 获取EXCEL表里的数据
        /// <summary>
        /// 获取EXCEL表里的数据
        /// </summary>
        /// <param name="fPath"></param>
        /// <param name="tableName"></param>
        /// <param name="mobiles"></param>
        /// <returns></returns>
        private string GetExcelTableData(string fPath, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + fPath + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [" + tableName + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            myCommand.Dispose();
            conn.Close();
            conn.Dispose();

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0] == null)
            {
                return string.Empty;
            }

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.WriteStartArray();

                    jsonWriter.WriteStartArray();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        jsonWriter.WriteValue(dc.ColumnName);
                    }
                    jsonWriter.WriteEndArray();

                    foreach (DataRow dr in dt.Rows)
                    {
                        jsonWriter.WriteStartArray();
                        int i = 0;
                        for (; i < dr.ItemArray.Length; i++)
                        {
                            jsonWriter.WriteValue(dr[i].ToString());
                        }
                        jsonWriter.WriteEndArray();
                    }


                    jsonWriter.WriteEnd();
                }
            }

            dt.Dispose();

            return sb.ToString();
        }
        #endregion

        #region 获取txt文件里的数据
        /// <summary>
        /// 获取Txt文件里的数据
        /// </summary>
        /// <param name="fPath"></param>
        /// <returns></returns>
        private string GetTxtTableData(string fPath)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (StreamReader reader = new StreamReader(fPath, Encoding.Default))
            {
                if (reader.Peek() > 0)
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.WriteStartArray();

                        jsonWriter.WriteStartArray();
                        jsonWriter.WriteValue(reader.ReadLine());

                        jsonWriter.WriteEndArray();

                        while (reader.Peek() > 0)
                        {
                            jsonWriter.WriteStartArray();
                            jsonWriter.WriteValue(reader.ReadLine());
                            jsonWriter.WriteEndArray();

                        }
                        jsonWriter.WriteEnd();
                    }
                }
            }

            return sb.ToString();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
