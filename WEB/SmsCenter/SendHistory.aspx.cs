using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
namespace Web.SmsCenter
{
    /// <summary>
    ///短信发送历史
    ///修改记录：
    ///1、2012-04-16 曹胡生 创建
    /// </summary>
    public partial class SendHistory : EyouSoft.Common.Page.BackPage
    {
        #region
        protected int PageSize = 20;
        protected int PageIndex = 20;
        protected int RecordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            PageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            PageInit();
            BindPage();
        }

        private void PageInit()
        {
            EyouSoft.Model.SmsStructure.MQuerySmsDetail model = new EyouSoft.Model.SmsStructure.MQuerySmsDetail()
            {
                StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("iptStartTime")),
                EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("iptEndTime")),
                Status = (EyouSoft.Model.EnumType.SmsStructure.SendStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.SendStatus), Utils.GetQueryStringValue("ddlStatus")),
                KeyWord = Utils.GetQueryStringValue("iptKeyWord")
            };
            var List = new EyouSoft.BLL.SmsStructure.BSmsDetail().GetSmsDetailList(PageSize, PageIndex, ref RecordCount, SiteUserInfo.CompanyId, model);
            if (List == null || List.Count == 0)
            {
                this.repList.EmptyText = "<tr align=\"center\"><td colspan=\"5\">暂无历史发送记录!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
            }
            else
            {
                this.repList.DataSource = List;
                this.repList.DataBind();
            }
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

        public string GetMobileStr(object o)
        {
            string str = "";
            IList<EyouSoft.Model.SmsStructure.MSmsNumber> list = (IList<EyouSoft.Model.SmsStructure.MSmsNumber>)o;
            foreach (var item in list)
            {
                str += item.Code + " ";
            }
            if (str.Length > 0)
            {
                str = str.Remove(str.Length - 1);
            }
            return str;
        }

        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int RecordCount = 0;
            EyouSoft.Model.SmsStructure.MQuerySmsDetail model = new EyouSoft.Model.SmsStructure.MQuerySmsDetail()
            {
                StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("iptStartTime")),
                EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("iptEndTime")),
                Status = (EyouSoft.Model.EnumType.SmsStructure.SendStatus?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.SmsStructure.SendStatus), Utils.GetQueryStringValue("ddlStatus")),
                KeyWord = Utils.GetQueryStringValue("iptKeyWord")
            };
            var List = new EyouSoft.BLL.SmsStructure.BSmsDetail().GetSmsDetailList(UtilsCommons.GetToXlsRecordCount(), 1, ref RecordCount, SiteUserInfo.CompanyId, model);
            if (List != null && List.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\n",
                    "发送时间",
                    "号码",
                    "发送内容",
                    "价格",
                    "状态");
                foreach (EyouSoft.Model.SmsStructure.MSmsDetail item in List)
                {
                    sb.Append(item.IssueTime + "\t");
                    sb.Append("'"+GetMobileStr(item.Number) + "\t");
                    sb.Append(item.Content + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Amount, ProviderToMoney) + "\t");
                    sb.Append(item.Status.ToString() + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);
        }
    }
}
