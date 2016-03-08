using System;
using System.Collections;
using System.Linq;

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.PlanStructure;
    using EyouSoft.Model.PlanStructure;
    using EyouSoft.BLL.ComStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.BLL.TourStructure;
    using System.Collections;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    /// <summary>
    /// 报销报账
    /// 导游报账
    /// 计调报账
    /// 公用
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    /// 必传参数：
    /// sl:你懂得
    /// tourId:团队编号
    public partial class BaoZhangCaoZuo : BackPage
    {
        #region attributes

        /// <summary>
        /// 团队状态
        /// </summary>
        protected TourStatus status;

        /// <summary>
        /// 核算单链接
        /// </summary>
        protected string PrintPageHSD = string.Empty;

        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        protected Menu2 _sl = Menu2.导游中心_导游报账;

        /// <summary>
        /// 操作权限
        /// </summary>
        /// 
        protected bool IsGrant = false;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            switch (Utils.GetQueryStringValue("dotype"))
            {
                default: break;
            }

            DataInit();

            #region ajax请求
            var type = Utils.GetQueryStringValue("type");
            //其它收入id
            var Id = Utils.GetQueryStringValue("ID");
            var orderID = Utils.GetQueryStringValue("OrderId");
            switch (type)
            {
                case "saveFreeItem":/*其他收入添加修改*/
                    AddAndUpdateOtherMoneyIn(Id);
                    break;
                case "DeleteFreeItem":/*其他收入删除*/
                    deleteOtherMoneyIn(Id);
                    break;
                case "QiTaZhiChuEdit":/*其他支出添加修改*/
                    AddAndUpdateOtherMoneyOut(Id);
                    break;
                case "DelQiTaZhiChu":/*其他支出删除*/
                    deleteOtherMoneyOut(Id);
                    break;
                case "saveGuidMoneyIn":/*导游收入添加修改*/
                    AddAndUpdateGuidRealIncome(orderID);
                    break;
                case "OperaterExamineV"://提交财务审核
                    OperaterExamineV();
                    break;
                case "ApplyOver":/*报销完成*/
                    ApplyOver();
                    break;
                default: break;
            }

            #endregion
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            var bll = new BPlan();
            var tourId = Utils.GetQueryStringValue("tourId");

            //二级栏目枚举
            this._sl = (Menu2)Utils.GetInt(SL);

            //团队基本数据
            TourModelInit(tourId);

            //权限判断
            PowerControl();
            
            //配置团队支出
            TourMoneyOut.TourID = tourId;

            //导游收入
            TourGuideMoneyIn(tourId);

            //购物收入列表初始化
            InitGouwWuShouRu(tourId);

            //其他收入
            OtherMoneyIn(bll, tourId);

            //其他支出
            OtherMoneyOut(bll, tourId);

            //导游借款
            Debit(tourId);

            //报账汇总
            TourSummarizing(bll, tourId);

            //团队收支总汇
            TourInOutSum(bll, tourId);

            PrintPageHSD = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.核算单);
            PrintPageHSD += "?referertype=1&tourid=" + tourId;
        }

        #region 团队基本数据
        /// <summary>
        /// 初始化团队实体数据
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void TourModelInit(string tourId)
        {
            var model = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourId);
            if (model != null)
            {
                //团号
                lbl_TourCode.Text = model.TourCode;
                //线路区域               
                this.lbl_AreaName.Text = model.AreaName;
                //线路名称
                lbl_RouteName.Text = model.RouteName;
                //天数
                lbl_TourDays.Text = model.TourDays.ToString();
                //出发时间
                lbl_LDate.Text = UtilsCommons.GetDateString(model.LDate, ProviderToDate);
                //返回时间
                lbl_RDate.Text = UtilsCommons.GetDateString(model.RDate, ProviderToDate);
                //导游
                lbl_TourGride.Text = model.Guides;
                //出发交通
                lbl_LTraffic.Text = model.ArriveCityFlight;
                //销售员
                this.hideSaleId.Value = model.SellerId;
                lbl_SaleInfo.Text = model.SellerName;
                //返回交通
                lbl_RTraffic.Text = model.LeaveCityFlight;
                //计调
                lbl_TourPlaner.Text = model.Planers;
                //集合方式
                //lbl_Gather.Text = model.Gather;
                status = model.TourStatus;
            }
        }

        #endregion

        #region 导游收入
        /// <summary>
        /// 初始化导游收入
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void TourGuideMoneyIn(string tourId)
        {
            var sum = new EyouSoft.Model.TourStructure.MOrderSum();
            var orders = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref  sum, tourId);

            //导游报账 导游收入列表绑定
            if (orders != null && orders.Count > 0)
            {
                repGuidInMoney.DataSource = orders;
                repGuidInMoney.DataBind();
            }
            else
            {
                this.phDaoYouShouRuEmpty.Visible = true;
            }
        }

        /// <summary>
        /// 导游收入 添加 修改
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns></returns>
        protected void AddAndUpdateGuidRealIncome(string orderID)
        {

            //导游现收
            decimal GuideIncome = Utils.GetDecimal(Utils.GetFormValue("txtGuideIncome"));
            //导游实收
            decimal realIncom = Utils.GetDecimal(Utils.GetFormValue("txtRealIncome"));
            //备注
            string remarks = Utils.GetFormValue("txtConfirmRemark");
            MTourOrderSales model = new MTourOrderSales();
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(CurrentUserCompanyID) ?? new List<EyouSoft.Model.ComStructure.MComPayment>();
            ls = ls.Where(item => item.Name == "导游现收" && item.IsSystem).ToList();
            if (ls != null && ls.Count > 0)
            {
                model.CollectionRefundMode = ls[0].PaymentId;
                model.CollectionRefundModeName = ls[0].Name;
            }
            model.OrderId = orderID;
            model.Operator = this.SiteUserInfo.Name;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.CollectionRefundAmount = realIncom;
            model.CollectionRefundState = CollectionRefundState.收款;
            model.CollectionRefundDate = System.DateTime.Now;
            model.CollectionRefundOperator = this.SiteUserInfo.Name;
            model.CollectionRefundOperatorID = this.SiteUserInfo.UserId;
            model.IsGuideRealIncome = true;
            model.ShouKuanType = EyouSoft.Model.EnumType.FinStructure.ShouKuanType.导游实收;

            //修改导游收入
            if (Utils.GetQueryStringValue("actionType") == "update")
            {
                if (new EyouSoft.BLL.TourStructure.BTourOrder().UpdateGuideRealIncome(orderID, GuideIncome, realIncom, remarks, model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功！"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "修改失败！"));
                }
            }
        }

        #endregion

        #region 购物收入
        /// <summary>
        /// 初始化购物收入列表
        /// </summary>
        /// <param name="tourId"></param>
        private void InitGouwWuShouRu(string tourId)
        {
            var l = new EyouSoft.BLL.HPlanStructure.BPlan().GetGouWuShouRuLst(tourId);
            if(l!=null&&l.Count>0)
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
        /// <param name="tourId">团队编号</param>
        private void OtherMoneyIn(BPlan BLL, string tourId)
        {
            IList<MOtherFeeInOut> ls = BLL.GetOtherIncome(tourId);
            if (ls != null && ls.Count > 0)
            {
                repOtherMoneyIn.DataSource = ls;
                repOtherMoneyIn.DataBind();
                TourOtherIncome.Value = ls.Sum(item => item.FeeAmount).ToString();
            }
        }

        /// <summary>
        /// 其他收入  添加 其他收入 修改
        /// <param name="Id">收入id</param>
        /// </summary>
        private void AddAndUpdateOtherMoneyIn(string ID)
        {
            string result = string.Empty;
            var outFree = new MOtherFeeInOut();

            outFree.PayType = Utils.GetInt(Utils.GetFormValue("other_payment"));
            outFree.PayTypeName = Utils.GetFormValue("paymentText");
            outFree.TourId = Utils.GetQueryStringValue("tourId");
            outFree.FeeItem = Utils.GetFormValue("txtFreeItem");
            outFree.DeptId = this.SiteUserInfo.DeptId;
            outFree.Operator = this.SiteUserInfo.Name;
            outFree.OperatorId = this.SiteUserInfo.UserId;
            outFree.IssueTime = System.DateTime.Now;
            outFree.FeeAmount = Utils.GetDecimal(Utils.GetFormValue("txtFeeAmount"));
            outFree.Remark = Utils.GetFormValue("txtRemark");
            outFree.Crm = Utils.GetFormValue("crmName");
            outFree.CrmId = Utils.GetFormValue("crmId");
            outFree.CompanyId = this.SiteUserInfo.CompanyId;
            outFree.DealTime = System.DateTime.Now;
            outFree.IsGuide = true;
            outFree.TourCode = Utils.GetQueryStringValue("tourCode");
            outFree.DealerId = Utils.GetQueryStringValue("sellerId");
            outFree.Dealer = Server.UrlDecode(Utils.GetQueryStringValue("sellerName"));
            if (!string.IsNullOrEmpty(ID))
            {
                outFree.Id = Utils.GetInt(ID);
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().UpdOtherFeeInOut(ItemType.收入, outFree) ? "1" : "-1", "提交失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().AddOtherFeeInOut(ItemType.收入, outFree) ? "1" : "-1", "提交失败!"));
            }
        }

        /// <summary>
        /// 其他收入 删除
        /// </summary>
        /// <param name="Id">收入id</param>
        private void deleteOtherMoneyIn(string Ids)
        {
            AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().DelOtherFeeInOut(this.SiteUserInfo.CompanyId, ItemType.收入, Utils.ConvertToIntArray(Ids.Split(','))) > 0 ? "1" : "-1", "删除成功!"));

        }
        #endregion

        #region 其他支出
        /// <summary>
        /// 其他支出(这个"其他"不是计调项的其他,是财务的杂费支出)
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        /// <param name="tourId">团队编号</param>
        private void OtherMoneyOut(BPlan BLL, string tourId)
        {
            IList<MOtherFeeInOut> ls = BLL.GetOtherOutpay(tourId);
            if (ls != null && ls.Count > 0)
            {
                repOtherMoneyOut.DataSource = ls;
                repOtherMoneyOut.DataBind();
                TourOtherOutpay.Value = ls.Sum(item => item.FeeAmount).ToString();
            }
        }

        /// <summary>
        /// 其他支出  添加 其他支出 修改
        /// <param name="Id">支出id</param>
        /// </summary>
        private void AddAndUpdateOtherMoneyOut(string ID)
        {
            string result = string.Empty;
            var outFree = new MOtherFeeInOut();

            outFree.PayType = Utils.GetInt(Utils.GetFormValue("other_payment"));
            outFree.PayTypeName = Utils.GetFormValue("paymentText");
            outFree.TourId = Utils.GetQueryStringValue("tourId");
            outFree.FeeItem = Utils.GetFormValue("txtFreeItem");
            outFree.DeptId = this.SiteUserInfo.DeptId;
            outFree.Operator = this.SiteUserInfo.Name;
            outFree.OperatorId = this.SiteUserInfo.UserId;
            outFree.IssueTime = System.DateTime.Now;
            outFree.FeeAmount = Utils.GetDecimal(Utils.GetFormValue("txtFeeAmount"));
            outFree.Remark = Utils.GetFormValue("txtRemark");
            outFree.Crm = Utils.GetFormValue("crmName");
            outFree.CrmId = Utils.GetFormValue("crmId");
            outFree.CompanyId = this.SiteUserInfo.CompanyId;
            outFree.DealTime = System.DateTime.Now;
            outFree.IsGuide = true;
            outFree.TourCode = Utils.GetQueryStringValue("tourCode");
            outFree.DealerId = Utils.GetQueryStringValue("sellerId");
            outFree.Dealer = Server.UrlDecode(Utils.GetQueryStringValue("sellerName"));
            if (!string.IsNullOrEmpty(ID))
            {
                outFree.Id = Utils.GetInt(ID);
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().UpdOtherFeeInOut(ItemType.支出, outFree) ? "1" : "-1", "提交失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().AddOtherFeeInOut(ItemType.支出, outFree) ? "1" : "-1", "提交失败!"));
            }
        }

        /// <summary>
        /// 其他支出 删除
        /// </summary>
        /// <param name="Ids">支出id</param>
        private void deleteOtherMoneyOut(string Ids)
        {
            AjaxResponse(UtilsCommons.AjaxReturnJson(new EyouSoft.BLL.FinStructure.BFinance().DelOtherFeeInOut(this.SiteUserInfo.CompanyId, ItemType.支出, Utils.ConvertToIntArray(Ids.Split(','))) > 0 ? "1" : "-1", "删除成功!"));

        }
        #endregion

        #region 导游借款
        /// <summary>
        /// 导游借款
        /// </summary>
        /// <param name="tourId">团队编号</param>
        private void Debit(string tourId)
        {
            IList<MDebit> ls = new EyouSoft.BLL.FinStructure.BFinance().GetDebitLstByTourId(tourId, true);
            if (ls != null && ls.Count > 0)
            {
                pan_DebitMsg.Visible = false;
                this.rpt_Debit.DataSource = ls;
                this.rpt_Debit.DataBind();
            }
        }
        #endregion

        #region 报账汇总
        /// <summary>
        /// 报账汇总
        /// </summary>
        /// <param name="BLL">计调BLL</param>
        /// <param name="tourId">团号</param>
        private void TourSummarizing(BPlan BLL, string tourId)
        {
            MBZHZ model = BLL.GetBZHZ(tourId, this.phGouWuShouRu.Visible);
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
        private void TourInOutSum(BPlan BLL, string tourID)
        {
            var orders = new EyouSoft.Model.TourStructure.MOrderSum();
            new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref orders, tourID);
            //团队收入
            decimal tourMoneyIn = 0;
            if (orders != null)
            {
                tourMoneyIn = orders.ConfirmMoney;
            }
            //团队支出
            decimal tourMoneyOut = 0;
            var ls = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(tourID);
            if (ls != null && ls.Count > 0)
            {
                tourMoneyOut = ls.Sum(item => item.Confirmation);
            }
            //团队利润
            var tourProfit = tourMoneyIn
                             +
                             (this.phGouWuShouRu.Visible
                                  ? (Utils.GetDecimal(TourOtherIncome.Value) + Utils.GetDecimal(TourGouWu.Value))
                                  : 0) - tourMoneyOut
                             -
                             (this.phGouWuShouRu.Visible
                                  ? Utils.GetDecimal(TourOtherOutpay.Value)
                                  : 0);
            //团队利润=团队收入-团队支出
            lbl_tourMoney.Text = tourProfit.ToString();
            //利润率 = 团队利润/团队收入(不要把/100.00改成100 原因请参考.net基础之 加减乘除)
            lbl_tourMoneyRate.Text = tourMoneyIn != 0 ? (((double)(Utils.GetDecimal(lbl_tourMoney.Text) / tourMoneyIn * 10000)) / 100.00).ToString("f2") + "%" : "0%";
            //格式化团队利润
            lbl_tourMoney.Text = UtilsCommons.GetMoneyString(lbl_tourMoney.Text, ProviderToMoney);
            //格式化团队收入
            lbl_tourMoneyIn.Text = UtilsCommons.GetMoneyString(tourMoneyIn, ProviderToMoney);
            //格式化团队支出
            lbl_tourMoneyOut.Text = UtilsCommons.GetMoneyString(tourMoneyOut, ProviderToMoney);
        }
        #endregion

        #region 提交财务

        /// <summary>
        /// 提交财务审核
        /// </summary>
        private void OperaterExamineV()
        {
            UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus.导游报销, Utils.GetQueryStringValue("tourId"));
        }

        #endregion

        #region 报销完成

        /// <summary>
        /// 报销完成
        /// </summary>
        private void ApplyOver()
        {
            this.AjaxResponse(UtilsCommons.AjaxReturnJson(new BTour().Apply(
                this.SiteUserInfo.DeptId,
               this.SiteUserInfo.UserId,
               this.SiteUserInfo.Name,
               Utils.GetQueryStringValue("tourId"),
               this.CurrentUserCompanyID) ? "1" : "-1", "报销成功!"));
        }

        #endregion

        #region 更新团队状态

        /// <summary>
        /// 改变团队状态
        /// </summary>
        /// <param name="tourStatus">计划状态</param>
        /// <param name="tourId">团队编号</param>
        private void UpdateTourStatus(EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus, string tourId)
        {
            var info = new EyouSoft.Model.HTourStructure.MTourStatusChange
            {
                TourId = tourId,/*团队编号*/
                CompanyId = CurrentUserCompanyID,/*系统公司编号*/
                OperatorDeptId = SiteUserInfo.DeptId,/*操作人部门Id*/
                Operator = SiteUserInfo.Name,/*操作人*/
                OperatorId = SiteUserInfo.UserId,/*操作人ID*/
                TourStatus = tourStatus/*团队状态*/
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

        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            TourMoneyOut.ParentType = PlanChangeChangeClass.计调报账;
            switch (this._sl)
            {
                case Menu2.导游中心_导游报账:
                    if (!CheckGrant(Privs.导游中心_导游报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.导游中心_导游报账_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.导游中心_导游报账_导游报账操作);
                    }

                    this.panMoneyViewGuid.Visible = true;
                    this.panMoneyInView.Visible = this.phQiTaZhiChuAdd.Visible = this.pan_OperaterExamineV.Visible = this.status < TourStatus.导游报销 && IsGrant;
                    this.pan_DaoYouBianGeng.Visible = this.phGouWuShouRu.Visible = true;
                    TourMoneyOut.IsPlanChangeChange = false;
                    break;
                case Menu2.计调中心_计调报账:
                    if (!CheckGrant(Privs.计调中心_计调报账_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.计调中心_计调报账_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.计调中心_计调报账_计调报账操作);
                    }

                    TourMoneyOut.IsPlanChangeChange = this.status > TourStatus.计调配置 && this.status < TourStatus.财务待审 && IsGrant;
                    break;
                case Menu2.财务管理_报销报账:
                    if (!CheckGrant(Privs.财务管理_报销报账_栏目))
                    { 
                        Utils.ResponseNoPermit(Privs.财务管理_报销报账_栏目, true);
                        return;
                    }
                    else
                    {
                        IsGrant = CheckGrant(Privs.财务管理_报销报账_报销完成);
                    }

                    this.panMoneyViewGuid.Visible = this.phTuanDuiShouZhi.Visible = this.phGouWuShouRu.Visible = true;
                    this.pan_ApplyOver.Visible = this.status < TourStatus.财务待审 && IsGrant;
                    TourMoneyOut.IsPlanChangeChange = false;
                    break;
                //case Menu2.财务管理_单团核算:
                //    break;
                //case Menu2.统计分析_单团核算:
                //    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }

        #endregion

        #region 获取支付方式

        /// <summary>
        /// 获得其它收入/支出的支付方式
        /// </summary>
        /// <param name="selectValue"></param>
        /// <param name="typ"></param>
        /// <returns></returns>
        protected string GetPayMentStr(string selectValue,ItemType typ)
        {
            var PaymentStr = " <select class='inputselect' name='other_payment'>";
            var payMentBll = new BComPayment();
            var list = payMentBll.GetList(this.SiteUserInfo.CompanyId, null, typ);
            if (list == null || list.Count == 0) return string.Empty;

            for (int i = 0; i < list.Count; i++)
            {
                PaymentStr += "<option " + (selectValue == list[i].PaymentId.ToString() ? "selected='selected'" : "") + " value='" + list[i].PaymentId.ToString() + "'>" + list[i].Name + "</option>";
            }
            PaymentStr += "</select>";
            return PaymentStr;
        }

        #endregion

        #endregion
    }
}
