using System;
using System.Linq;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    using MTourStatusChange = EyouSoft.Model.HTourStructure.MTourStatusChange;

    /// <summary>
    /// 计调：计调终审-计调终审
    /// 财务：单团核算-核算
    /// 财务：报销报账-审批
    /// 统计：单团核算-查看
    /// 公共页面
    /// </summary>
    public partial class HeSuanJieShu : BackPage
    {
        #region attributes
        /// <summary>
        /// 毛利 = 团款收入结算金额 - 团队支出结算金额
        /// </summary>
        protected string Price = "0";
        /// <summary>
        /// 团款收入结算金额
        /// </summary>
        private decimal confirmSettlementMoney = 0;
        /// <summary>
        /// 团队支出结算金额
        /// </summary>
        private decimal tourMoneyOutSumNum = 0;
        /// <summary>
        /// 团号
        /// </summary>
        protected string TourCode = string.Empty;
        /// <summary>
        /// 是否已核算标识（1：已核算）
        /// </summary>
        protected string flag = string.Empty;
        /// <summary>
        /// 团队结算单路径
        /// </summary>
        protected string PringPageJSD = string.Empty;
        /// <summary>
        /// 核算单链接
        /// </summary>
        protected string PrintPageHSD = string.Empty;
        /// <summary>
        /// 团队编号
        /// </summary>
        protected string TourId = Utils.GetQueryStringValue("tourId");
        /// <summary>
        /// 团队状态
        /// </summary>
        protected TourStatus TourStatus = TourStatus.导游报销;
        /// <summary>
        /// 操作权限
        /// </summary>
        protected bool IsGrant = false;

        protected TourType _TourType;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Utils.GetQueryStringValue("flag");
            switch (Utils.GetQueryStringValue("type"))
            {
                case "ReturnOperater":
                    if (Utils.GetQueryStringValue("tourtype")==TourType.单项业务.ToString())
                    {
                        this.UpdateTourStatus(TourStatus.销售未派计划);
                    }
                    else
                    {
                        this.UpdateTourStatus(TourStatus.导游报帐);
                    }
                    break;
                case "SubmitFinanceManage":
                    this.UpdateTourStatus(TourStatus.单团核算);
                    break;
                case "AppealEnd":
                    this.UpdateTourStatus(TourStatus.封团);
                    break;
                case "Del":
                    Del();
                    break;
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //PringPageJSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.结算单);
            //PrintPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            //PrintPageHSD += "?referertype=4&tourid=" + TourId;

            //计调BLL
            var bpBLL = new EyouSoft.BLL.PlanStructure.BPlan();

            //初始化团队基本信息
            TourModelInit();

            //团款收入
            TourOrderInit();
            
            switch ((Menu2)Utils.GetInt(this.SL))
            {
                case Menu2.财务管理_报销报账:
                    if (!CheckGrant(Privs.财务管理_报销报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.财务管理_报销报账_审批);
                    }

                    this.pan_submitFinance.Visible = this.TourStatus == TourStatus.财务待审 && IsGrant;
                    break;
                case Menu2.财务管理_单团核算:
                    if (!CheckGrant(Privs.财务管理_单团核算_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.财务管理_单团核算_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.财务管理_单团核算_核算结束);
                    }

                    this.pan_sealTour.Visible = this.TourStatus == TourStatus.单团核算 && IsGrant;
                    this.pan_returnOperater.Visible = true;
                    break;
                case Menu2.统计分析_单团核算:
                    //购物收入
                    InitGouwWuShouRu();

                    //初始化其他收入列表
                    OtherMoneyIn(bpBLL);

                    //其他支出
                    OtherMoneyOut(bpBLL);

                    //初始化利润分配
                    MoneyDistribute();

                    if (!CheckGrant(Privs.统计分析_单团核算_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.统计分析_单团核算_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.统计分析_利润统计_栏目);
                    }

                    this.phGouWuShouRu.Visible = true;
                    this.pan_AddMongyAllot.Visible = IsGrant;
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }

            //初始化团队支出列表
            TourMoneyOut(bpBLL);

            //计算毛利
            Price = Utils.FilterEndOfTheZeroDecimal(confirmSettlementMoney - tourMoneyOutSumNum);

            //报账总汇
            TourSummarizing(bpBLL);

            //团队收支总汇
            TourInOutSum();
        }

        #region 团队基本数据
        /// <summary>
        /// 初始化团队实体数据
        /// </summary>
        private void TourModelInit()
        {
            var model = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(TourId);
            if (model != null)
            {
                TourCode = model.TourCode;
                //线路名称
                lbl_routeName.Text = model.RouteName;
                //出团时间
                lbl_lDate.Text = UtilsCommons.GetDateString(model.LDate, ProviderToDate);
                //团号
                lbl_tourCode.Text = model.TourCode;
                //团队天数
                lbl_tourDays.Text = model.TourDays.ToString();
                //人数
                lbl_number.Text = string.Format("<b class=fontblue>{0}</b><sup class=fontred>+{1}</sup>", model.Adults, model.Childs);
                //销售
                lbl_saleInfoName.Text = model.SellerName;
                //计调
                lbl_tourPlaner.Text = model.Planers;
                //导游
                lbl_mGuidInfoName.Text = model.Guides;
                this.TourStatus = model.TourStatus;
                this._TourType = model.TourType;
            }
        }


        #endregion

        #region 团款收入
        /// <summary>
        /// 团款收入
        /// </summary>
        private void TourOrderInit()
        {
            var sumModel = new MOrderSum();
            var sl = new BTourOrder().GetTourOrderListById(ref sumModel, TourId);
            if (sl != null && sl.Count > 0)
            {

                rpt_tourMoneyIn.DataSource = sl;
                rpt_tourMoneyIn.DataBind();
                //合同金额
                lbl_sumPrice.Text = UtilsCommons.GetMoneyString(TourIncome.Value = sumModel.ConfirmMoney.ToString(), ProviderToMoney);
                //团队收入
                lbl_tourMoneyIn.Text = (sumModel.ConfirmMoney).ToString();
                //结算金额
                lbl_confirmSettlementMoney.Text = UtilsCommons.GetMoneyString(TourSettlement.Value = (confirmSettlementMoney = sumModel.ConfirmSettlementMoney).ToString(), ProviderToMoney);
                //导游实收
                lbl_guideRealIncome.Text = UtilsCommons.GetMoneyString(sumModel.GuideRealIncome, ProviderToMoney);
                //财务实收
                lbl_checkMoney.Text = UtilsCommons.GetMoneyString(sumModel.ConfirmMoney - sumModel.GuideRealIncome, ProviderToMoney);
                //订单利润
                //lbl_profit.Text = UtilsCommons.GetMoneyString(sumModel.Profit, ProviderToMoney);
            }
            pan_tourMoneyInMsg.Visible = !(pan_tourMoneyInSum.Visible = sl != null && sl.Count > 0);//
        }
        #endregion

        #region 购物收入
        /// <summary>
        /// 初始化购物收入列表
        /// </summary>
        private void InitGouwWuShouRu()
        {
            var l = new EyouSoft.BLL.HPlanStructure.BPlan().GetGouWuShouRuLst(TourId);
            if (l != null && l.Count > 0)
            {
                this.rptGouWuShouRu.DataSource = l;
                this.rptGouWuShouRu.DataBind();
                TourGouWu.Value = l.Sum(item => item.ToCompanyTotal).ToString();
            }
            else
            {
                this.phGouWuShouRuEmpty.Visible = true;
            }
        }
        #endregion

        #region 其他收入
        /// <summary>
        /// 其他收入(这个"其他"不是计调项的其他,是财务的杂费收入)
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        private void OtherMoneyIn(EyouSoft.BLL.PlanStructure.BPlan BLL)
        {

            var ls = BLL.GetOtherIncome(TourId);

            if (ls != null && ls.Count > 0)
            {
                rpt_restsMoneyIn.DataSource = ls;
                rpt_restsMoneyIn.DataBind();
                TourOtherIncome.Value = ls.Sum(item => item.FeeAmount).ToString();
            }
            else
            {
                this.phQiTaShouRuEmpty.Visible = true;
            }
        }
        #endregion

        #region 其他支出
        /// <summary>
        /// 其他支出(这个"其他"不是计调项的其他,是财务的杂费支出)
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        private void OtherMoneyOut(EyouSoft.BLL.PlanStructure.BPlan BLL)
        {
            var ls = BLL.GetOtherOutpay(TourId);
            if (ls != null && ls.Count > 0)
            {
                repOtherMoneyOut.DataSource = ls;
                repOtherMoneyOut.DataBind();
                TourOtherOutpay.Value = ls.Sum(item => item.FeeAmount).ToString();
            }
            else
            {
                this.phQiTaZhiChuEmpty.Visible = true;
            }
        }
        #endregion

        #region 团队支出
        /// <summary>
        /// 团队支出
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        private void TourMoneyOut(EyouSoft.BLL.PlanStructure.BPlan BLL)
        {
            var ls = BLL.GetList(TourId);
            if (ls != null && ls.Count > 0)
            {
                rpt_tourMoneyOut.DataSource = ls.Where(p => p.Type != PlanProject.购物);
                rpt_tourMoneyOut.DataBind();
                //lbl_tourMoneyOutSumNum.Text = ls.Sum(item => item.Num).ToString();
                lbl_tourMoneyOutSumConfirmation.Text = UtilsCommons.GetMoneyString((lbl_tourMoneyOut.Text = TourPay.Value = (tourMoneyOutSumNum = ls.Sum(item => item.Confirmation)).ToString()), ProviderToMoney);
            }
            pan_tourMoneyOutMsg.Visible = !(pan_tourMoneyOut.Visible = ls != null && ls.Count > 0);
        }
        #endregion

        #region 利润分配
        /// <summary>
        /// 利润分配
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void MoneyDistribute()
        {
            IList<EyouSoft.Model.FinStructure.MProfitDistribute> ls = new EyouSoft.BLL.FinStructure.BFinance().GetProfitDistribute(TourId);
            if (ls != null && ls.Count > 0)
            {
                pan_mongyAllotMsg.Visible = false;
                rpt_mongyAllot.DataSource = ls;
                rpt_mongyAllot.DataBind();
                //团队订单分配利润
                //ls = ls.Where(item => item.OrderId != null && item.OrderId.Length > 0).ToList();
                //if (ls != null && ls.Count > 0)
                //{
                //    DisOrderProfit.Value = ls.Sum(item => item.Amount).ToString();
                //}
                ls = ls.Where(item => item.OrderId == null || item.OrderId.Length <= 0).ToList();
                if (ls != null && ls.Count > 0)
                {
                    DisTourProfit.Value = ls.Sum(item => item.Amount).ToString();
                }
            }

        }
        #endregion

        #region 报账汇总
        /// <summary>
        /// 报账汇总
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        private void TourSummarizing(EyouSoft.BLL.PlanStructure.BPlan BLL)
        {
            var model = BLL.GetBZHZ(TourId, this.phGouWuShouRu.Visible);
            if (model != null)
            {
                lbl_guidesIncome.Text = UtilsCommons.GetMoneyString(model.GuideIncome, ProviderToMoney);
                lbl_guidesBorrower.Text = UtilsCommons.GetMoneyString(model.GuideBorrow, ProviderToMoney);
                lbl_guidesSpending.Text = UtilsCommons.GetMoneyString(model.GuideOutlay, ProviderToMoney);
                lbl_replacementOrReturn.Text = UtilsCommons.GetMoneyString(model.GuideMoneyRtn, ProviderToMoney);
                lbl_RCSN.Text = model.GuideRelSign.ToString();
                lbl_HUSN.Text = model.GuideUsed.ToString();
                lbl_RSN.Text = model.GuideSignRtn.ToString();
            }
        }
        #endregion

        #region 团队收支总汇
        /// <summary>
        /// 团队收支总汇
        /// </summary>
        private void TourInOutSum()
        {
            //团队利润
            var tourProfit = Utils.GetDecimal(lbl_tourMoneyIn.Text)
                             +
                             (this.phGouWuShouRu.Visible
                                  ? (Utils.GetDecimal(TourOtherIncome.Value) + Utils.GetDecimal(TourGouWu.Value))
                                  : 0) - Utils.GetDecimal(lbl_tourMoneyOut.Text)
                             -
                             (this.phGouWuShouRu.Visible
                                  ? (Utils.GetDecimal(TourOtherOutpay.Value) + Utils.GetDecimal(DisTourProfit.Value))
                                  : 0);
            //团队利润=团队收入-团队支出
            lbl_tourMoney.Text = TourProfit.Value = tourProfit.ToString();
            //利润率 = 团队利润/团队收入(不要把/100.00改成100 原因请参考.net基础之 加减乘除)
            lbl_tourMoneyRate.Text = Utils.GetDecimal(lbl_tourMoneyIn.Text) != 0 ? (((int)(Utils.GetDecimal(lbl_tourMoney.Text) / Utils.GetDecimal(lbl_tourMoneyIn.Text) * 10000)) / 100.00).ToString() + "%" : "0%";
            //格式化团队利润
            lbl_tourMoney.Text = UtilsCommons.GetMoneyString(lbl_tourMoney.Text, ProviderToMoney);
            //格式化团队收入
            lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(lbl_tourMoneyIn.Text, ProviderToMoney);
            //格式化团队支出
            lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(lbl_tourMoneyOut.Text, ProviderToMoney);
        }
        #endregion

        #region Ajax
 
        /// <summary>
        /// 改变团队状态
        /// </summary>
        /// <param name="tourStatus">计划状态</param>
        private void UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus)
        {
            var info = new EyouSoft.Model.HTourStructure.MTourStatusChange
            {
                TourId = TourId,/*团队编号*/
                CompanyId = CurrentUserCompanyID,/*系统公司编号*/
                OperatorDeptId = SiteUserInfo.DeptId,/*操作人部门Id*/
                Operator = SiteUserInfo.Name,/*操作人*/
                OperatorId = SiteUserInfo.UserId,/*操作人ID*/
                TourStatus = tourStatus,/*团队状态*/
                Remark = Utils.GetQueryStringValue("tui")//计调退回说明
            };

            var bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().UpdateTourStatus(info);

            switch (bllRetCode)
            {
                case 1:
                    this.RCWE(UtilsCommons.AjaxReturnJson("1", "提交成功"));
                    break;
                case -1:
                    this.RCWE(UtilsCommons.AjaxReturnJson("-1", "导游实收未提交"));
                    break;
                case -2:
                    this.RCWE(UtilsCommons.AjaxReturnJson("-1", "购物收入未提交"));
                    break;
                default:
                    this.RCWE(UtilsCommons.AjaxReturnJson("-1", "提交失败!"));
                    break;
            }
        }
        /// <summary>
        /// 删除利润分配
        /// </summary>
        private void Del()
        {
            AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().DelProfitDis(Utils.GetInt(Utils.GetQueryStringValue("id")), CurrentUserCompanyID) ? "1" : "-1", "删除失败!"));
        }
        #endregion
    }
}
