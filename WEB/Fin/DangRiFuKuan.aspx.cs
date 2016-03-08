using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    
    public partial class DangRiFuKuan : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion

            decimal sum = 0;
            MPayRegister queryModel = new MPayRegister();
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.Dealer = txt_SellsSelect1.SellsName = Utils.GetQueryStringValue(txt_SellsSelect1.SellsNameClient);
            queryModel.DealerId = txt_SellsSelect1.SellsID = Utils.GetQueryStringValue(txt_SellsSelect1.SellsIDClient);
            queryModel.Supplier = Utils.GetQueryStringValue("Supplier");
            queryModel.TourCode = Utils.GetQueryStringValue("TourCode");
            IList<MRegister> ls = new BFinance().GetTodayPaidLst(
                pageSize,
                pageIndex,
                ref recordCount,
                ref sum,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                //绑定分页
                BindPage(pageSize, pageIndex, recordCount);
            }
            lbl_sum.Text = UtilsCommons.GetMoneyString(sum, ProviderToMoney);
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            decimal sum = 0;
            MPayRegister queryModel = new MPayRegister();
            queryModel.CompanyId = CurrentUserCompanyID;
            IList<MRegister> ls = new BFinance().GetTodayPaidLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref recordCount,
                ref sum,
                queryModel);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                //应付账款
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\n",
                    "计调项",
                    "团号",
                    "供应商",
                    "销售员",
                    "OP",
                    "请款人",
                    "请款金额",
                    "支付方式");
                foreach (MRegister item in ls)
                {
                    sb.Append(item.PlanTyp + "\t");
                    sb.Append(item.TourCode + "\t");
                    sb.Append(item.Supplier + "\t");
                    sb.Append(item.Salesman + "\t");
                    sb.Append(item.Planer + "\t");
                    sb.Append(item.Dealer + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Payable, ProviderToMoney) + "\t");
                    sb.Append(item.PaymentName + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);
        }
    }
}
