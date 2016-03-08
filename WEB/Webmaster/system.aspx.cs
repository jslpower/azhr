//2011-09-30 汪奇志
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.Webmaster
{
    /// <summary>
    /// 查看子系统信息
    /// </summary>
    public partial class _system : WebmasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var info = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(Utils.GetQueryStringValue("SysId"));
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(Utils.GetQueryStringValue("cid"));

            if (info != null)
            {
                this.ltrSysId.Text = info.SysId;
                this.ltrCompanyId.Text = info.CompanyId;                
                this.ltrIssueTime.Text = info.IssueTime.ToString();
                this.ltrSysName1.Text = this.ltrSysName2.Text = info.SysName;
                this.ltrFullname.Text = info.FullName;
                this.ltrTelephone.Text = info.Telephone;
                this.ltrMobile.Text = info.Mobile;
                this.ltrUserId.Text = info.UserId;
                this.ltrUsername.Text = info.Username;
                this.ltrPassword.Text = info.Password.NoEncryptPassword;
            }

            if (setting.SmsConfig == null || !setting.SmsConfig.IsEnabled)
            {
                phSms.Visible = false;
                phSmsCreate.Visible = true;
            }
            else
            {
                ltrSmsAccount.Text = setting.SmsConfig.Account;
                ltrSmsAppKey.Text = setting.SmsConfig.AppKey;
                ltrSmsAppSecret.Text = setting.SmsConfig.AppSecret;

                phSms.Visible = true;
                phSmsCreate.Visible = false;
            }

        }

        #region protected members
        /// <summary>
        /// btnCreateSms click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateSms_Click(object sender, EventArgs e)
        {
            string cid = Utils.GetQueryStringValue("cid");

            var info = new EyouSoft.BLL.SmsStructure.BSmsAccount().CreateSmsAccount();

            if (info != null && new EyouSoft.BLL.ComStructure.BComSetting().SetComSmsConfig(cid, info))
            {
                RegisterAlertAndRedirectScript("短信账号创建成功", null);
            }
            else
            {
                RegisterAlertAndRedirectScript("短信账号创建失败", null);
            }
        }
        #endregion
    }
}
