using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Quote
{
    using System.Data;

    public partial class BiJiao : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FristQuoteId = Utils.GetQueryStringValue("FristQuoteId");
            string SecondQuoteId = Utils.GetQueryStringValue("SecondQuoteId");

            string FristCount = Utils.GetQueryStringValue("FristCount");
            string SecondCount = Utils.GetQueryStringValue("SecondCount");

            if (!string.IsNullOrEmpty(FristQuoteId) && !string.IsNullOrEmpty(SecondQuoteId) && !string.IsNullOrEmpty(FristCount) && !string.IsNullOrEmpty(SecondCount))
            {
                PageInit(FristQuoteId, SecondQuoteId, FristCount, SecondCount);
            }


        }


        /// <summary>
        /// 初始化页面
        /// </summary>
        private void PageInit(string FristQuoteId, string SecondQuoteId, string FristCount, string SecondCount)
        {
            string[] QuoteIds = new string[2];
            QuoteIds[0] = FristQuoteId;
            QuoteIds[1] = SecondQuoteId;

            FristCount = Server.UrlDecode(FristCount);
            SecondCount = Server.UrlDecode(SecondCount);

            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            IList<EyouSoft.Model.HTourStructure.MQuoteCost> list = bll.GetQuoteCostList(QuoteIds);

            if (list != null && list.Count != 0)
            {
                this.ltCostTitle.Text = string.Format("<a href='BaoJiaEdit.aspx?sl={4}&id={2}&act=update'>{0}</a> 与 <a href='BaoJiaEdit.aspx?sl={4}&id={3}&act=update'>{1}</a> 的成本比较", FristCount, SecondCount, FristQuoteId, SecondQuoteId, this.SL);
                this.ltPriceTitle.Text = string.Format("<a href='BaoJiaEdit.aspx?sl={4}&id={2}&act=update'>{0}</a> 与 <a href='BaoJiaEdit.aspx?sl={4}&id={3}&act=update'>{1}</a> 的价格比较", FristCount, SecondCount, FristQuoteId, SecondQuoteId, this.SL);

                //frist 成本
                var firstCost = list.Where(c => c.QuoteId == FristQuoteId && c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格).OrderBy(m=>m.Pricetype).ToList();
                //second
                var secondCost = list.Where(c => c.QuoteId == SecondQuoteId && c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格).OrderBy(m => m.Pricetype).ToList();
                
                this.ltFristCost.Text = GetTrInfo(firstCost, secondCost);
                this.ltSecondCost.Text = GetTrInfo(secondCost, firstCost);


                //frist 价格
                var firstPrice = list.Where(c => c.QuoteId == FristQuoteId && c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).OrderBy(m => m.Pricetype).ToList();
                //second
                var secondPrice = list.Where(c => c.QuoteId == SecondQuoteId && c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).OrderBy(m => m.Pricetype).ToList();
                
                this.ltFristPrice.Text = GetTrInfo(firstPrice, secondPrice);
                this.ltSecondPrice.Text = GetTrInfo(secondPrice, firstPrice);
            }
        }


        /// <summary>
        /// 获取tr信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="compare">比较列表</param>
        /// <returns></returns>
        private string GetTrInfo(IList<EyouSoft.Model.HTourStructure.MQuoteCost> list, IList<EyouSoft.Model.HTourStructure.MQuoteCost> compare)
        {
            var bll = new EyouSoft.BLL.HTourStructure.BQuote();
            var ds = new DataSet();
            var dc = new DataSet();
            System.Text.StringBuilder query = new System.Text.StringBuilder();
            if (list == null || list.Count == 0)
            {
                query.Append("<tr>");
                query.Append("<td align='center'>");
                query.Append("该次报价为整团报价！");
                query.Append("</td>");
                query.Append("</tr>");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    EyouSoft.Model.HTourStructure.MQuoteCost model = list[i];
                    query.AppendFormat("<tr {0}>", compare != null && compare.Count > i && model.Price != compare[i].Price ? "bgcolor=\"gray\"" : "");
                    query.Append("<td align='left'>");
                    query.Append("<span style='width: 10%;display:inline-block'>"+model.Pricetype+"</span>");
                    query.AppendFormat("<input type='text' class='inputtext formsize50' value='{0}' {1}/>", model.Price.ToString("f2"), compare != null && compare.Count>i&&model.Price != compare[i].Price ? "style=\"color: red\"" : "");
                    query.Append(model.PriceUnit == EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人 ? "元/人" : "元/团");
                    query.Append("<img width='19' height='18' src='../images/bei.jpg' />");
                    query.Append(model.Remark);
                    //if (!string.IsNullOrEmpty(model.Remark))
                    //{
                    //    query.AppendFormat("<input type='text' class='formsize180 input-txt' value='{0}' />", model.Remark);
                    //}
                    if (compare != null && compare.Count > i && model.Price != compare[i].Price)
                    {
                        ds = bll.GetQuoteCompare(model.QuoteId, model.CostMode, model.Pricetype);
                        dc = bll.GetQuoteCompare(compare[i].QuoteId, compare[i].CostMode, compare[i].Pricetype);
                        var dd = ds.Tables[0].AsEnumerable().Except(dc.Tables[0].AsEnumerable(), DataRowComparer.Default);
                        foreach (var r in dd)
                        {
                            query.Append("<br>" + r["项目"]);
                        }
                    }
                    query.Append("</td>");
                    query.Append("</tr>");
                }
            }
            return query.ToString();
        }
    }
}
