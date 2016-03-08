using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;
    using System.Linq;

    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.PrivsStructure;

    public partial class YingShouQueRen : EyouSoft.Common.Page.BackPage
    {
        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        protected Menu2 _sl;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax请求处理
            string doType = Utils.GetQueryStringValue("action");

            switch (doType)
            {
                case "save":
                    Response.Clear();
                    Response.Write(PageSave());
                    Response.End();
                    break;
                case "QuXiaoQueRenHeTongJinE":
                    QuXiaoQueRenHeTongJinE();
                    break;
                default: break;
            }
            #endregion

            DataInit();
        }

        #region 页面初始化
        /// <summary>
        /// 团款详细信息
        /// </summary>
        protected void DataInit()
        {
            var orderId = Utils.GetQueryStringValue("OrderId");

            this._sl = (Menu2)Utils.GetInt(this.SL);

            var privs = (this._sl == Menu2.财务管理_应收管理) ? Privs.财务管理_应收管理_确认合同金额 : Privs.销售中心_销售收款_确认合同金额;
            pan_Save.Visible = CheckGrant(privs);

            if (!string.IsNullOrEmpty(orderId))
            {
                var salesmodel = new EyouSoft.BLL.TourStructure.BTourOrder().GetSettlementOrderByOrderId(orderId, EyouSoft.Model.EnumType.TourStructure.TourType.团队产品);
                if (salesmodel != null)
                {
                    //已经确认过合同金额的不可再确认
                    if (salesmodel.ConfirmMoneyStatus)
                    {
                        pan_Save.Visible = false;
                    }

                    //非订单销售员不可确认合同金额
                    if (pan_Save.Visible && salesmodel.SellerId != SiteUserInfo.UserId)
                    {
                        pan_Save.Visible = SiteUserInfo.IsHandleElse;
                    }

                    this.litTourCode.Text = salesmodel.TourCode;
                    this.txtComfirmMoney.Text = salesmodel.ConfirmMoney.ToString("F2");
                    this.txtchangeEsplain.Text = salesmodel.ConfirmRemark;
                    this.litRouteName.Text = salesmodel.RouteName;
                    this.litAccountPrices.Text = salesmodel.SumPrice.ToString("C2");
                    this.hidAccountPrices.Value = salesmodel.SumPrice.ToString("F2");

                    //费用增减
                    if (salesmodel.OrderSalesConfirm!=null&&salesmodel.OrderSalesConfirm.Count>0)
                    {
                        this.rptList.DataSource = salesmodel.OrderSalesConfirm;
                        this.rptList.DataBind();
                    }
                    else
                    {
                        this.phEmpty.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected string PageSave()
        {
            string setMsg = string.Empty;
            var saleBase = new EyouSoft.Model.TourStructure.MOrderConfirm();
            saleBase.ConfirmMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtComfirmMoney.UniqueID)); ;
            saleBase.ConfirmMoneyStatus = Utils.GetQueryStringValue("confirm") == "1";
            saleBase.ConfirmPeople = this.SiteUserInfo.Name;
            saleBase.ConfirmPeopleId = this.SiteUserInfo.UserId;
            saleBase.ConfirmRemark = Utils.GetFormValue(this.txtchangeEsplain.UniqueID); ;
            saleBase.OrderId = Utils.GetQueryStringValue("OrderId");
            saleBase.ConfirmPeopleDeptId = this.SiteUserInfo.DeptId;
            saleBase.ChangeInfo = this.GetList();

            bool result = new EyouSoft.BLL.TourStructure.BTourOrder().UpdateOrderSettlement(saleBase);
            if (result)
            {
                setMsg = UtilsCommons.AjaxReturnJson("1", "合同金额确认成功！");
            }
            else
            {
                setMsg = UtilsCommons.AjaxReturnJson("0", "合同金额确认失败！");
            }
            return setMsg;
        }

        /// <summary>
        /// 获取增减列表
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.MOrderSalesConfirm> GetList()
        {
            var l = new List<EyouSoft.Model.TourStructure.MOrderSalesConfirm>();
            var txtAddFee = Utils.GetFormValues("txtAdd");
            var txtAddRemark = Utils.GetFormValues("txtAddRemark");
            var txtRedFee = Utils.GetFormValues("txtReduce");
            var txtRedRemark = Utils.GetFormValues("txtReduceRemark");
            if (txtAddFee == null || txtAddFee.Length == 0) return l;

            l.AddRange(txtAddFee.Select((t, i) => new EyouSoft.Model.TourStructure.MOrderSalesConfirm {OrderId = Utils.GetQueryStringValue("OrderId"), AddFee = Utils.GetDecimal(t), AddRemark = txtAddRemark[i], RedFee = Utils.GetDecimal(txtRedFee[i]), RedRemark = txtRedRemark[i] }));
            return l;
        }
        #endregion

        #region 指定页面类型
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion

        /// <summary>
        /// 取消确认合同金额
        /// </summary>
        void QuXiaoQueRenHeTongJinE()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_取消确认合同金额))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));
            }

            string orderId = Utils.GetQueryStringValue("orderid");

            int bllRetCode = new EyouSoft.BLL.TourStructure.BTourOrder().QuXiaoQueRenHeTongJinE(CurrentUserCompanyID, SiteUserInfo.UserId, orderId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "取消确认合同金额成功！"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "未确认合同金额或订单信息不存在！"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "计划已核算结束，不能取消确认合同金额！"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }
    }
}
