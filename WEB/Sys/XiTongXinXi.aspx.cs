using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.Sys
{
    /// <summary>
    /// 系统公司配置页
    /// </summary>
    /// 修改记录：
    /// 1、2013-6-5 刘树超 创建
    public partial class XiTongXinXi : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            UploadOutLogo.CompanyID = UploadInnerLogo.CompanyID = ucFXSLogo.CompanyID = SiteUserInfo.CompanyId;
            UploadOutLogo.IsUploadSelf = UploadInnerLogo.IsUploadSelf = ucFXSLogo.IsUploadSelf = true;
            UploadOutLogo.FileTypes = UploadInnerLogo.FileTypes = ucFXSLogo.FileTypes = "*.jpg;*.gif;*.jpeg;*.png";



            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }

            PageInit();

        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            EyouSoft.BLL.ComStructure.BCompany BCompanyBll = new EyouSoft.BLL.ComStructure.BCompany();
            EyouSoft.Model.ComStructure.MCompany model = BCompanyBll.GetModel(SiteUserInfo.CompanyId, SiteUserInfo.SysId);
            if (model != null)
            {
                txtCompanyName.Value = model.Name;
                if (!string.IsNullOrEmpty(model.WLogo))
                {
                    lblUploadOutLogo.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='SystemConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidUploadOutLogo' value='{0}'/></span>", model.WLogo);
                }
                if (!string.IsNullOrEmpty(model.NLogo))
                {
                    lblUploadInnerLogo.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='SystemConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hidUploadInnerLogo' value='{0}'/></span>", model.NLogo);
                }
                if (!string.IsNullOrEmpty(model.FXSLogo))
                {
                    lblFXSLogo.Text = string.Format("<span class='upload_filename'>&nbsp;<a href='{0}' target='_blank'>查看</a><a href='javascript:void(0);' onclick='SystemConfig.RemoveFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='txtFXSLogo' value='{0}'/></span>", model.FXSLogo);
                }
                txtHotelType.Value = model.Type;
                //txtEnCompanyName.Value = model.EnName;
                txtXuKeCard.Value = model.License;
                txtCompanyManager.Value = model.ContactName;
                txtPhone.Value = model.Tel;
                txtMobile.Value = model.Mobile;
                txtFax.Value = model.Fax;
                txtAddress.Value = model.Address;
                txtZip.Value = model.Zip;
                txtSite.Value = model.Domain;
                if (model.ComAccount != null && model.ComAccount.Count > 0)
                {
                    phrPanel.Visible = false;
                    this.repList.DataSource = model.ComAccount;
                    this.repList.DataBind();
                }
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

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统信息_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统信息_栏目, false);
                return;
            }
        }

        //保存
        protected void Save()
        {
            string msg = string.Empty;
            EyouSoft.Model.ComStructure.MCompany model = new EyouSoft.Model.ComStructure.MCompany();
            IList<EyouSoft.Model.ComStructure.MComAccount> MComAccount = new List<EyouSoft.Model.ComStructure.MComAccount>();
            EyouSoft.Model.ComStructure.MComAccount item = null;
            model.Address = txtAddress.Value;
            model.ContactName = txtCompanyManager.Value;
            model.Domain = txtSite.Value;
            //model.EnName = txtEnCompanyName.Value;
            model.Fax = txtFax.Value;
            model.License = txtXuKeCard.Value;
            model.Mobile = txtMobile.Value;
            model.Name = txtCompanyName.Value;
            model.Id = SiteUserInfo.CompanyId;
            model.SysId = SiteUserInfo.SysId;
            model.Tel = txtPhone.Value;
            model.Type = txtHotelType.Value;
            model.Zip = txtZip.Value;

            string WLogo = Utils.GetFormValue(UploadOutLogo.ClientHideID);
            string NLogo = Utils.GetFormValue(UploadInnerLogo.ClientHideID);
            string fxsLogo = Utils.GetFormValue(ucFXSLogo.ClientHideID);
            if (string.IsNullOrEmpty(WLogo))
            {
                model.WLogo = Utils.GetFormValue("hidUploadOutLogo");
            }
            else
            {
                model.WLogo = WLogo.Split('|')[1];
            }
            if (string.IsNullOrEmpty(NLogo))
            {
                model.NLogo = Utils.GetFormValue("hidUploadInnerLogo");
            }
            else
            {
                model.NLogo = NLogo.Split('|')[1];
            }
            if (string.IsNullOrEmpty(fxsLogo))
            {
                model.FXSLogo = Utils.GetFormValue("txtFXSLogo");
            }
            else
            {
                model.FXSLogo = fxsLogo.Split('|')[1];
            }

            //公司账号
            int length = EyouSoft.Common.Utils.GetFormValues("hidAccountId").Length;
            for (int i = 0; i < length; i++)
            {
                item = new EyouSoft.Model.ComStructure.MComAccount();
                item.AccountId = Utils.GetInt(Utils.GetFormValues("hidAccountId")[i]);
                item.AccountName = EyouSoft.Common.Utils.GetFormValues("AccountName")[i];
                item.BankName = EyouSoft.Common.Utils.GetFormValues("BankName")[i];
                item.BankNo = EyouSoft.Common.Utils.GetFormValues("BankNo")[i];
                if (string.IsNullOrEmpty(item.AccountName) || string.IsNullOrEmpty(item.BankName) || string.IsNullOrEmpty(item.BankNo))
                {
                    msg += "公司账号不能为空!<br/>";
                    break;
                }
                else
                {
                    MComAccount.Add(item);
                }
            }
            model.ComAccount = MComAccount;
            if (string.IsNullOrEmpty(model.Name))
            {
                msg += "请填写公司名称!<br/>";
            }
            if (string.IsNullOrEmpty(model.Type))
            {
                msg += "请填写旅行社类别!<br/>";
            }
            if (string.IsNullOrEmpty(model.ContactName))
            {
                msg += "请填写公司负责人!<br/>";
            }
            if (string.IsNullOrEmpty(model.Tel))
            {
                msg += "请填写电话!<br/>";
            }
            if (!Utils.IsPhone(model.Tel))
            {
                msg += "请填写有效的电话格式!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", msg));
            }
            bool result = new EyouSoft.BLL.ComStructure.BCompany().Update(model);
            Response.Clear();
            if (result)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败!"));
            }
            Response.End();
        }
    }
}
