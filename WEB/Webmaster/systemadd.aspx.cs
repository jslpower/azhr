//2011-09-27 汪奇志
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
    /// 添加子系统信息
    /// </summary>
    public partial class _systemadd : WebmasterPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region btnCreate click
        /// <summary>
        /// btnCreate click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.SysStructure.MSysInfo info = new EyouSoft.Model.SysStructure.MSysInfo();
            info.Password = new EyouSoft.Model.ComStructure.MPasswordInfo(); 

            #region get form values
            info.SysName = Utils.GetFormValue("txtSysName");
            info.FullName = Utils.GetFormValue("txtFullname");
            info.Telephone = Utils.GetFormValue("txtTelephone");
            info.Mobile = Utils.GetFormValue("txtMobile");
            info.Username = Utils.GetFormValue("txtUsername");
            info.Password.NoEncryptPassword = Utils.GetFormValue("txtPassword");
            #endregion

            #region validate form
            if (string.IsNullOrEmpty(info.SysName))
            {
                RegisterAlertScript("请输入系统名称！");
                return;
            }

            if (string.IsNullOrEmpty(info.Username))
            {
                RegisterAlertScript("请输入登录账号！");
                return;
            }

            if (string.IsNullOrEmpty(info.Password.NoEncryptPassword))
            {
                RegisterAlertScript("请输入登录密码！");
                return;
            }
            #endregion

            info.SmsConfig = new EyouSoft.BLL.SmsStructure.BSmsAccount().CreateSmsAccount();

            if (info.SmsConfig == null)
            {
                RegisterAlertScript("开通短信账号时异常！");
                return;
            }

            int createResult = new EyouSoft.BLL.SysStructure.BSys().CreateSys(info);

            if (createResult == 1)
            {
                this.RegisterAlertAndRedirectScript("子系统添加成功", "systems.aspx");
            }
            else
            {
                this.RegisterAlertAndRedirectScript("子系统添加失败", "systemadd.aspx");
            }
        }
        #endregion
    }
}
