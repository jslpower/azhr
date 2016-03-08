using System;

namespace EyouSoft.Web.PrintPage
{
    using EyouSoft.Common;
    using EyouSoft.BLL.HTourStructure;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;

    public partial class HeSuanDan : BackPage
    {
        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        protected Menu2 _sl = Menu2.财务管理_单团核算;

        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (string.IsNullOrEmpty(tourId)) Utils.RCWE("异常请求");

            PowerControl();

            InitPage(tourId);

            //referertype 请求来源            
            //referertype=1 导游报账、销售报账、计调报账
            //referertype=2 单项业务修改页面
            //referertype=3 计调终审
            //referertype=4 财务报账
            //referertype=default 单团核算
            string referertype = Utils.GetQueryStringValue("referertype");

            if (referertype == "1" || referertype == "3" || referertype == "4")
            {
                ltrDanJuTitle.Text = Title = "报账单";
            }
        }

        protected void InitPage(string tourId)
        {
            //核算单
            var model = new BTour().GetTourModel(tourId);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
                this.lbLDate.Text = model.LDate.ToShortDateString();
                this.lbTourDays.Text = model.TourDays.ToString();
                this.lbPersonNum.Text = string.Format("<b class=fontblue>{0}</b><sup class=fontred>+{1}</sup>", model.Adults, model.Childs);
                this.lbSeller.Text = model.SellerName;
                this.lbGuid.Text = model.Guides;
                this.lbTourPlaner.Text = model.Planers;
            }
            //团款收入
            var orders = new EyouSoft.Model.TourStructure.MOrderSum();
            var tourOrder = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orders, tourId);
            if (tourOrder != null && tourOrder.Count > 0)
            {
                this.rpt_tuankuan.DataSource = tourOrder;
                this.rpt_tuankuan.DataBind();
                this.lbConfirmMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmMoney, ProviderToMoney);
                this.lbSettlementMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmSettlementMoney, ProviderToMoney);
                this.lbGuideIncomeCount.Text = UtilsCommons.GetMoneyString(orders.GuideRealIncome, ProviderToMoney);
                this.lbCheckMoneyCount.Text = UtilsCommons.GetMoneyString(orders.ConfirmMoney - orders.GuideRealIncome, ProviderToMoney);
                this.lbProfitCount.Text = UtilsCommons.GetMoneyString(orders.Profit, ProviderToMoney);
            }
            else
            {
                this.ph_tuankuan.Visible = false;
            }
            //其他收入
            var otherList = new EyouSoft.BLL.PlanStructure.BPlan().GetOtherIncome(tourId);
            if (otherList != null && otherList.Count > 0)
            {
                this.rpt_qita.DataSource = otherList;
                this.rpt_qita.DataBind();
            }
            else
            {
                this.ph_qita.Visible = false;
            }
            //其他支出
            var l = new EyouSoft.BLL.PlanStructure.BPlan().GetOtherOutpay(tourId);
            if (l != null && l.Count > 0)
            {
                this.rpt_qitazhichu.DataSource = l;
                this.rpt_qitazhichu.DataBind();
            }
            else
            {
                this.ph_qitazhichu.Visible = false;
            }
            //团队支出
            var payList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(tourId);
            if (payList != null && payList.Count > 0)
            {
                this.rpt_zhichu.DataSource = payList;
                this.rpt_zhichu.DataBind();
                int Count = 0;
                decimal TotalMoney = 0;
                foreach (var item in payList)
                {
                    Count += item.Num;
                    TotalMoney += item.Confirmation;
                }
                //this.lbNumCount.Text = Count.ToString();
                this.lbSettlementMoney.Text = UtilsCommons.GetMoneyString(TotalMoney, ProviderToMoney);
            }
            else
            {
                this.ph_zhichu.Visible = false;
            }
            //利润分配
            var profitList = new EyouSoft.BLL.FinStructure.BFinance().GetProfitDistribute(tourId);
            if (profitList != null && profitList.Count > 0)
            {
                this.rpt_lirun.DataSource = profitList;
                this.rpt_lirun.DataBind();
            }
            else
            {
                this.ph_lirun.Visible = false;
            }
            //报帐汇总
            var BZmodel = new EyouSoft.BLL.PlanStructure.BPlan().GetBZHZ(tourId, this.phGouWuShouRu.Visible);
            if (model != null)
            {
                this.lb_guidesIncome.Text = UtilsCommons.GetMoneyString(BZmodel.GuideIncome, ProviderToMoney);
                this.lb_guidesBorrower.Text = UtilsCommons.GetMoneyString(BZmodel.GuideBorrow, ProviderToMoney);
                this.lb_guidesSpending.Text = UtilsCommons.GetMoneyString(BZmodel.GuideOutlay, ProviderToMoney);
                this.lb_replacementOrReturn.Text = UtilsCommons.GetMoneyString(BZmodel.GuideMoneyRtn, ProviderToMoney);
                this.lb_RCSN.Text = BZmodel.GuideRelSign.ToString();
                this.lb_HUSN.Text = BZmodel.GuideUsed.ToString();
                this.lb_RSN.Text = BZmodel.GuideSignRtn.ToString();
            }
            //团队汇总信息
            var tourModel = new EyouSoft.BLL.PlanStructure.BPlan().GetTourTotalInOut(tourId);
            /*团队收入*/
            this.lb_tourMoneyIn.Text = UtilsCommons.GetMoneyString(tourModel.TourIncome, ProviderToMoney);
            /*团队支出*/
            this.lb_tourMoneyOut.Text = UtilsCommons.GetMoneyString(tourModel.TourOutlay, ProviderToMoney);
            /*团队利润*/
            this.lb_tourMoney.Text = UtilsCommons.GetMoneyString(tourModel.TourProfit, ProviderToMoney);
            /*团队利润率*/
            this.lb_tourMoneyRate.Text = ((int)(tourModel.TourProRate * 10000) / 100.00).ToString() + "%";

        }

        #region 权限判断

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //二级栏目枚举
            this._sl = (Menu2)Utils.GetInt(SL);

            switch (this._sl)
            {
                //case Menu2.导游中心_导游报账:
                //    if (!CheckGrant(Privs.导游中心_导游报账_栏目))
                //    {
                //        Utils.ResponseNoPermit(Privs.导游中心_导游报账_栏目, true);
                //        return;
                //    }
                //    break;
                //case Menu2.计调中心_计调报账:
                //    if (!CheckGrant(Privs.计调中心_计调报账_栏目))
                //    {
                //        Utils.ResponseNoPermit(Privs.计调中心_计调报账_栏目, true);
                //        return;
                //    }
                //    break;
                //case Menu2.财务管理_报销报账:
                //    if (!CheckGrant(Privs.财务管理_报销报账_栏目))
                //    {
                //        Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, true);
                //        return;
                //    }
                //    break;
                case Menu2.财务管理_单团核算:
                    break;
                case Menu2.统计分析_单团核算:
                    this.phGouWuShouRu.Visible = CheckGrant(Privs.统计分析_单团核算_栏目);
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }

        #endregion
    }
}
