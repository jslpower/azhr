using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Web.Webmaster;
using EyouSoft.Common.Function;

namespace EyouSoft.Web.Webmaster
{
    public partial class cartemp : WebmasterPageBase
    {


        EyouSoft.BLL.SysStructure.BSysCarType bll = new EyouSoft.BLL.SysStructure.BSysCarType();
        EyouSoft.Model.SysStructure.MSysCarType model = new EyouSoft.Model.SysStructure.MSysCarType();

        protected void Page_Load(object sender, EventArgs e)
        {
            Bind_rptsetNumList();
            Bind_rpttempList();
            string strQuery = Utils.GetQueryStringValue("dotype");
            int strId = Utils.GetInt(Utils.GetQueryStringValue("id"));
            string strtempid = Utils.GetQueryStringValue("tempid");
            int intTemlId = Utils.GetInt(Utils.GetQueryStringValue("seatId"));

            switch (strQuery)
            {
                case "delSeatNum":
                    int resultSeat = 1;
                    resultSeat = bll.DeleteSysCarTypeSeatNum(strId);
                    if (resultSeat == 1)
                    {
                        MessageBox.ResponseScript(this, "alert('删除成功!');");
                        Response.Redirect("cartemp.aspx");
                    }
                    else if (resultSeat == 0)
                    {
                        MessageBox.ResponseScript(this, "alert('删除失败!');");
                    }
                    else
                    {
                        MessageBox.ResponseScript(this, "alert('模板已经被使用，请先删除使用项!');");
                    }

                    break;
                case "delTemp":
                    int resultTemp = 1;
                    resultTemp = bll.DeleteSysCarTypeTempLate(strtempid);
                    if (resultTemp == 1)
                    {
                        MessageBox.ResponseScript(this, "alert('删除成功!');");
                        Response.Redirect("cartemp.aspx");
                    }
                    else if (resultTemp == 0)
                    {
                        MessageBox.ResponseScript(this, "alert('删除失败!');");
                    }
                    else
                    {
                        MessageBox.ResponseScript(this, "alert('模板已经被使用，请先删除使用项!');");
                    }
                    break;
                case "setDefault":
                    bll.UpdateSysCarType(intTemlId, strtempid);
                    Response.Redirect("cartemp.aspx");

                    break;

                default:
                    break;
            }

        }

        //保存座位数
        protected void btnSaveSit_Click(object sender, EventArgs e)
        {
            int sitCount = Utils.GetInt(this.txtSitCount.Text.Trim(), 0);
            if (sitCount == 0)
            {
                MessageBox.ResponseScript(this, "alert('请输入座位数!');");
                return;
            }
            else if (sitCount <= 61)
            {
                bll.AddSysCarType(sitCount);
                Response.Redirect("cartemp.aspx");
            }
            else
            {
                MessageBox.ResponseScript(this, "alert('座位数不能大于61!');");
            }

        }
        //保存模板
        protected void btnSaveCarTemp_Click(object sender, EventArgs e)
        {
            int sitCount = Utils.GetInt(Utils.GetFormValue(this.ddlSitCount.UniqueID));
            string str = "";
            fileUpload(out str);
            model.FilePath = str == "" ? "/Images/default.jpg" : str;
            model.Id = sitCount;
            if (model.Id != 0)
            {
                bll.AddSysCarType(model);
                Response.Redirect("cartemp.aspx");
            }
            else
            {
                MessageBox.ResponseScript(this, "alert('请选择座位数!');");
            }


        }
        //绑定座位数
        protected void Bind_rptsetNumList()
        {
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> list = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeSeatNumList();
            if (list != null && list.Count > 0)
            {
                ddlSitCount.Items.Clear();
                ddlSitCount.Items.Add("请选择座位");

                for (int i = 0; i < list.Count; i++)
                {
                    ddlSitCount.Items.Add(new ListItem(list[i].SeatNum.ToString(), list[i].Id.ToString()));
                }


            }
            rpt_setNumList.DataSource = list;
            rpt_setNumList.DataBind();
        }
        //绑定模板
        protected void Bind_rpttempList()
        {
            IList<EyouSoft.Model.SysStructure.MSysCarType> list = bll.GetCarTypeList();
            if (list != null)
            {
                rpt_tempList.DataSource = list;
                rpt_tempList.DataBind();
            }

        }
        //上传文件并返回路径
        protected void fileUpload(out string newFilePath)
        {
            newFilePath = String.Empty;//文件上传后的路径, 如："/UploadFilesx/201010/guid.doc" 
            string newFileName = String.Empty;//文件上传后的文件名， 如："guid.doc"
            string oldFileName = string.Empty;//上传前的文件名,如："我的文档.doc"
            int fileSize = 0;//文件大小(kb)
            if (this.fu_fileLoad.HasFile)
            {
                UploadFile up = new UploadFile();
                string[] str = { ".jpg", ".bmp", ".gif", ".jpeg" };
                string msg;
                if (UploadFile.CheckFileType(Request.Files, this.fu_fileLoad.UniqueID, str, 4, out msg))
                {
                    bool flag = UploadFile.FileUpLoad(this.fu_fileLoad.PostedFile, "UploadFiles", out newFilePath, out newFileName);
                    if (flag)
                    {
                        oldFileName = this.fu_fileLoad.FileName;
                        fileSize = this.fu_fileLoad.PostedFile.ContentLength / 1024;
                    }
                    else
                    {
                        //文件上传失败
                        MessageBox.ResponseScript(this, "alert('文件上传失败!');");
                        return;
                    }
                }
                else
                {
                    //文件上传失败
                    MessageBox.ResponseScript(this, string.Format("alert('{0}');", msg));
                    return;
                }
            }

        }
    }
}
