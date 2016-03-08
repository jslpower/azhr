using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SmsCenter
{
    public partial class AccountRecharge : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        protected void PageInit()
        {
            txtCompanyName.Text = SiteUserInfo.CompanyName;
            txtContact.Text = SiteUserInfo.Name;
            txtIssueTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_短信账户信息栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_短信账户信息栏目, false);
                return;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            
            string Msg = string.Empty;
            if (Utils.GetDecimal(Utils.GetFormValue("txtMoney")) == 0)
            {
                Msg += "请输入有效的充值金额！";
            }
            if (!string.IsNullOrEmpty(Msg))
            {
                Msg = UtilsCommons.AjaxReturnJson("0", Msg);
            }
            else
            {
                var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CurrentUserCompanyID);
                if (setting != null && setting.SmsConfig != null && setting.SmsConfig.IsEnabled)
                {
                    EyouSoft.Model.SmsStructure.MSmsBankChargeInfo model = new EyouSoft.Model.SmsStructure.MSmsBankChargeInfo();
                    model.AccountId = setting.SmsConfig.Account;
                    model.AppKey = setting.SmsConfig.AppKey;
                    model.AppSecret = setting.SmsConfig.AppSecret;
                    model.ChargeAmount = Utils.GetDecimal(Utils.GetFormValue("txtMoney"));
                    model.ChargeComName = SiteUserInfo.CompanyName;
                    model.ChargeName = SiteUserInfo.Name;
                    model.ChargeTelephone = SiteUserInfo.Telephone;
                    model.IssueTime = DateTime.Now;
                    model.Status = 0;
                    bool Result = new EyouSoft.BLL.SmsStructure.BSmsAccount().SmsBankRecharge(model);
                    Msg = Result ? UtilsCommons.AjaxReturnJson("1", "充值成功，正在审核中...") : UtilsCommons.AjaxReturnJson("0", "充值失败！");

                }
                else
                {
                    Msg = UtilsCommons.AjaxReturnJson("0", "操作失败，请联系客服！");
                }
            }
            Response.Clear();
            Response.Write(Msg);
            Response.End();
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

    }
}
