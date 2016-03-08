using System;

namespace EyouSoft.WebFX
{
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using EyouSoft.Common;
    using EyouSoft.Common.Function;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.FinStructure;

    public partial class JieSuan : FrontPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
        /// <summary>
        /// 总条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPageSp = string.Empty;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
    System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                //绑定数据、分页
                BindSource();
            }
            if (UtilsCommons.IsToXls()) ToXls();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        MReceivableBase GetQuery()
        {
            var mSearch = new MReceivableBase()
            {
                CompanyId = this.SiteUserInfo.CompanyId,
                IsShowDistribution = true,
                CustomerId = this.SiteUserInfo.TourCompanyInfo.CompanyId,
                TourCode = Utils.GetQueryStringValue("th")
            };

            string jieQingStatus = Utils.GetQueryStringValue("s");
            if (jieQingStatus == "1") mSearch.IsClean = true;
            else if (jieQingStatus == "0") mSearch.IsClean = false;

            return mSearch;
        }

        /// <summary>
        /// 绑定数据、分页控件
        /// </summary>
        public void BindSource()
        {
            var sum = new object[6];

            var list = new EyouSoft.BLL.FinStructure.BFinance().GetReceivableInfoLst(
                pageSize, pageIndex, ref recordCount, ref sum, GetQuery());
            this.rptList.DataSource = list;
            this.rptList.DataBind();

            //合计
            this.LtTotalReceived.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(sum[1], this.ProviderToMoney);
            this.LtTotalSumPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(sum[0], this.ProviderToMoney);
            this.LtTotalUnReceived.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(sum[3], this.ProviderToMoney);

            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        protected string GetFinancialStatus(string select)
        {
            var option = new System.Text.StringBuilder();

            option.AppendFormat("<option value=''>{0}</option>",(string)GetGlobalResourceObject("string","请选择"));
            if (string.IsNullOrEmpty(select))
            {
                option.AppendFormat("<option value='0'>{0}</option>", (string)GetGlobalResourceObject("string", "未结清"));
                option.AppendFormat("<option value='1'>{0}</option>", (string)GetGlobalResourceObject("string", "已结清"));
            }
            else
            {
                if (select == "0")
                {
                    option.AppendFormat("<option value='0' selected='selected'>{0}</option>", (string)GetGlobalResourceObject("string", "未结清"));
                    option.AppendFormat("<option value='1'>{0}</option>", (string)GetGlobalResourceObject("string", "已结清"));
                }
                else
                {
                    option.AppendFormat("<option value='0'>{0}</option>", (string)GetGlobalResourceObject("string", "未结清"));
                    option.AppendFormat("<option value='1' selected='selected' >{0}</option>", (string)GetGlobalResourceObject("string", "已结清"));
                }
            }
            return option.ToString();
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var sum = new object[6];

            var items = new EyouSoft.BLL.FinStructure.BFinance().GetReceivableInfoLst(
                pageSize, pageIndex, ref recordCount, ref sum, GetQuery());

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            var s = new StringBuilder();
            s.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n",GetGlobalResourceObject("string","编号"),GetGlobalResourceObject("string","团号"),GetGlobalResourceObject("string","抵达日期"),GetGlobalResourceObject("string","应付金额"),GetGlobalResourceObject("string","已付金额"),GetGlobalResourceObject("string","未付金额"));
            for (var i = 0; i < items.Count; i++)
            {
                s.Append((i+1) + "\t");
                s.Append(items[i].TourCode + "\t");
                s.Append(items[i].LDate.ToShortDateString() + "\t");
                s.Append(items[i].Receivable.ToString("F2") + "\t");
                s.Append(items[i].Received.ToString("F2") + "\t");
                s.Append((items[i].Receivable - items[i].Received).ToString("F2") + "\n");
            }
            //合计
            if (sum.Any())
            {
                s.AppendFormat("\t\t{3}：\t{0}\t{1}\t{2}\n", sum[0], sum[1], sum[3],GetGlobalResourceObject("string","合计金额"));
            }

            ResponseToXls(s.ToString());
        }
    }
}
