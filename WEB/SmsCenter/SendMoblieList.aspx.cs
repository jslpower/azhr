using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.SmsCenter
{
    public partial class SendMoblieList : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            PageInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void PageInit()
        {

            var item = new EyouSoft.BLL.SmsStructure.BSmsDetail().GetSmsDetaiInfo(CurrentUserCompanyID, Request.QueryString["PlanId"]);
            if (item != null)
            {
                lblSendTime.Text = item.IssueTime.ToString();
                litSendMobile.Text = GetMobileStr(item.Number);
                litSendContent.Text = item.Content;
                lblPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(item.Amount, this.ProviderToMoney);
                lblStatus.Text = item.Status.ToString();
            }
        }

        /// <summary>
        /// 得到手机号码
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetMobileStr(IList<EyouSoft.Model.SmsStructure.MSmsNumber> list)
        {
            string str = "";
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    str += item.Code + " ";
                }
                if (str.Length > 0)
                {
                    str = str.Remove(str.Length - 1);
                }
            }
            return str;
        }


        /// <summary>
        /// 权限验证
        /// </summary>
        private void PowerControl()
        {
            //判断权限
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_栏目, true);
                return;
            }
            else if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_发送历史栏目))
            {
                EyouSoft.Common.Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_发送历史栏目, true);
                return;
            }
        }
    }
}
