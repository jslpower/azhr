using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;
namespace Web.SysCenter
{
    public partial class AccountInfo : BackPage
    {
        #region 分页参数
        protected int PageSize = 20;
        protected int PageIndex = 1;
        protected int RecordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            PageInit();
            BindPage();
        }

        private void PageInit()
        {
            var chaXun = new EyouSoft.Model.SmsStructure.MSmsBankChargeSearchInfo();
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);

            chaXun.AccountId = setting.SmsConfig.Account;
            chaXun.AppKey = setting.SmsConfig.AppKey;
            chaXun.AppSecret = setting.SmsConfig.AppSecret;

            var list = new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsBankCharges(PageSize, PageIndex, out RecordCount, chaXun);
            litBalance.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(new EyouSoft.BLL.SmsStructure.BSmsAccount().GetSmsAccountYuE(CurrentUserCompanyID), ProviderToMoney);
            if (list != null && list.Count > 0)
            {
                repList.DataSource = list;
                repList.DataBind();
            }
            else
            {
                repList.EmptyText = "<tr><td colspan=\"4\">暂无相关冲值记录！</td></tr>";
            }
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
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
        }
    }
}
