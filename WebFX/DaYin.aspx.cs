using System;

namespace EyouSoft.WebFX
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;

    public partial class DaYin : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                Response.Clear();
                Response.Write(Save());
                Response.End();
            }

            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
                    System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                //上传控件
                this.UploadControl1.IsUploadSelf = true;
                this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl1.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";

                this.UploadControl2.IsUploadSelf = true;
                this.UploadControl2.CompanyID = SiteUserInfo.CompanyId;
                this.UploadControl2.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";

                PageInit();

            }
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        private void PageInit()
        {
            EyouSoft.BLL.CrmStructure.BCrm bCrm = new EyouSoft.BLL.CrmStructure.BCrm();
            EyouSoft.Model.CrmStructure.MCrm mCrm = bCrm.GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
            if (null != mCrm)
            {
                if (!string.IsNullOrEmpty(mCrm.PrintHeader))
                {
                    this.lblPrintHeader.Text = string.Format("<span class='upload_filename'><a href='{0}' target=\"_blank\">{1}</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='{2}'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"PrintHeader\" value='{0}'/></span>", mCrm.PrintHeader,GetGlobalResourceObject("string","查看"),GetGlobalResourceObject("string","删除附件"));
                }

                if (!string.IsNullOrEmpty(mCrm.PrintFooter))
                {
                    this.lblPrintFooter.Text = string.Format("<span class='upload_filename'><a href='{0}' target=\"_blank\">{1}</a><a href=\"javascript:void(0)\" onclick=\"ConfigSettings.DelFile(this)\" title='{2}'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"PrintFooter\" value='{0}'/></span>", mCrm.PrintFooter, GetGlobalResourceObject("string", "查看"), GetGlobalResourceObject("string", "删除附件"));
                }
            }
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        private string Save()
        {
            string printHead = Utils.GetFormValue(this.UploadControl1.ClientHideID);
            if (!string.IsNullOrEmpty(printHead))
            {
                printHead = printHead.Split('|')[1];
            }
            else
            {
                printHead = Utils.GetFormValue("PrintHeader");
            }


            string printFooter = Utils.GetFormValue(this.UploadControl2.ClientHideID);
            if (!string.IsNullOrEmpty(printFooter))
            {
                printFooter = printFooter.Split('|')[1];
            }
            else
            {
                printFooter = Utils.GetFormValue("PrintFooter");
            }

            EyouSoft.BLL.CrmStructure.BCrm bCrm = new EyouSoft.BLL.CrmStructure.BCrm();

            if (bCrm.UpdatePrintSet(SiteUserInfo.TourCompanyInfo.CompanyId, printHead, printFooter, string.Empty, string.Empty))
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("1",(string)GetGlobalResourceObject("string","打印设置成功"));
            }
            else
            {
                return EyouSoft.Common.UtilsCommons.AjaxReturnJson("0",(string)GetGlobalResourceObject("string","打印设置失败"));
            }
        }
    }
}