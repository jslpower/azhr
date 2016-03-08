using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.IO;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;

namespace Web.CommonPage
{
    /// <summary>
    /// 文件下载公共页
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-10-11
    public partial class FileDownLoad : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (!string.IsNullOrEmpty(doType))
                {
                    this.initData(doType);
                }
            }
        }
        private void initData(string doType)
        {
            string filePath = Request.QueryString["filePath"];
            string name = HttpUtility.UrlDecode(Request.QueryString["name"]);
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(name))
            {
                switch (doType)
                {
                    case "downLoad"://下载
                        //下载次数加1
                        //BLL.AddDownloads(itemID, (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Utils.GetInt(type));
                        string reallyname = name;
                        String FullFileName = Utils.GetMapPath(filePath);
                        if (!System.IO.File.Exists(FullFileName))
                        {
                            this.lbTitle.Text = "抱歉，该附件不存在或已被删除！";
                            return;
                        }
                        //IE
                        var userAgent = this.Request.UserAgent;
                        if (userAgent != null && userAgent.ToUpper().IndexOf("MSIE") > 0)
                        {
                            reallyname = HttpUtility.UrlEncode(reallyname, System.Text.Encoding.UTF8);
                        }
                        FileInfo info = new FileInfo(FullFileName);
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.Buffer = false;
                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" + reallyname.Replace("+", "_").Replace(" ", ""));
                        Response.AppendHeader("Content-Length", info.Length.ToString());
                        Response.WriteFile(FullFileName);
                        Response.Flush();
                        Response.End();
                        break;
                }
            }
        }
    }
}