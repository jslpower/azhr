using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using EyouSoft.Common;
using EyouSoft.Common.Page;


namespace EyouSoft.Web.Sys
{
    public partial class XiTongPeiZhi : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            this.UploadYM.CompanyID = SiteUserInfo.CompanyId;
            this.UploadYM.IsUploadMore = false;
            this.UploadYM.IsUploadSelf = true;
            this.UploadYJ.CompanyID = SiteUserInfo.CompanyId;
            this.UploadYJ.IsUploadMore = false;
            this.UploadYJ.IsUploadSelf = true;
            this.UploadWordTemp.CompanyID = SiteUserInfo.CompanyId;
            this.UploadWordTemp.IsUploadMore = false;
            this.UploadWordTemp.IsUploadSelf = true;

            this.UploadYM.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            this.UploadYJ.FileTypes = "*.jpg;*.jpeg;*.gif;*.png;*.bmp;";
            this.UploadWordTemp.FileTypes = "*.dot;";


            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        protected void Save()
        {
            string filePageMei = Utils.GetFormValue(UploadYM.ClientHideID);
            string filePageFoot = Utils.GetFormValue(UploadYJ.ClientHideID);
            string fileWordTemplate = Utils.GetFormValue(UploadWordTemp.ClientHideID);

            EyouSoft.Model.ComStructure.MComSetting model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CurrentUserCompanyID);
            model = model ?? new EyouSoft.Model.ComStructure.MComSetting();

            if (!string.IsNullOrEmpty(filePageMei))
                model.PagePath = filePageMei.Split('|')[1];
            else
                model.PagePath = Utils.GetFormValue("hidYM");

            if (!string.IsNullOrEmpty(filePageFoot))
                model.FooterPath = filePageFoot.Split('|')[1];
            else
                model.FooterPath = Utils.GetFormValue("hidYJ");

            if (!string.IsNullOrEmpty(fileWordTemplate))
                model.WordTemplate = fileWordTemplate.Split('|')[1];
            else
                model.WordTemplate = Utils.GetFormValue("hidWordTemp");

            model.CompanyId = SiteUserInfo.CompanyId;
            Response.Clear();
            if (new EyouSoft.BLL.ComStructure.BComSetting().UpdateComSetting(model))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败!"));
            }
            Response.End();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        private string uploadFile(HttpPostedFile File)
        {
            string Name = "";
            string Path = "";
            EyouSoft.Common.Function.UploadFile.FileUpLoad(File, "CompanyFile", out Path, out Name);
            return Path;
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            EyouSoft.Model.ComStructure.MComSetting model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.PagePath))
                {
                    lblYM.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='PrintConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidYM' value='{0}'/></span>", model.PagePath);
                }
                if (!string.IsNullOrEmpty(model.FooterPath))
                {
                    lblYJ.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='PrintConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidYJ' value='{0}'/></span>", model.FooterPath);
                }
                if (!string.IsNullOrEmpty(model.WordTemplate))
                {
                    lblWordTemp.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='PrintConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidWordTemp' value='{0}'/></span>", model.WordTemplate);
                }
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
