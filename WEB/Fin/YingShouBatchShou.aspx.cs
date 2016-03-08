using System;

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.BLL.TourStructure;
    using EyouSoft.Common;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.BLL.ComStructure;

    public partial class YingShouBatchShou : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string[] orderIds = Utils.GetQueryStringValue("OrderId").Split(',');
            if (orderIds.Length > 0)
            {
                IList<MTourOrderCollectionSales> ls = new BTourOrder().GetTourOrderCollectionSalesListByOrderId(orderIds);
                if (ls != null && ls.Count > 0)
                {
                    rpt_list.DataSource = ls;
                    rpt_list.DataBind();
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string[] list = Utils.GetFormValue("list").Split(',');
            IList<MTourOrderSales> ls = new List<MTourOrderSales>();
            foreach (string item in list)
            {
                string[] cou = item.Split('|');
                MTourOrderSales model = new MTourOrderSales
                {
                    //订单编号
                    OrderId = cou[0],
                    //收退款方式
                    CollectionRefundMode = Utils.GetInt(cou[1]),
                    //收退款金额
                    CollectionRefundAmount = Utils.GetDecimal(cou[2]),
                    //收退款类型
                    CollectionRefundState = EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款,
                    //登记人
                    Operator = SiteUserInfo.Name,
                    //收退款人
                    CollectionRefundOperator = SiteUserInfo.Name,
                    //登记人Id
                    OperatorId = SiteUserInfo.UserId,
                    //收退款人Id
                    CollectionRefundOperatorID = SiteUserInfo.UserId,
                    //收退款时间
                    CollectionRefundDate = DateTime.Now,
                    IsCheck = Utils.GetInt(Utils.GetFormValue("ParentType")) == 1 ?
                        !(new BComSetting().GetModel(CurrentUserCompanyID).FinancialIncomeReview)
                        :
                        false,

                };
                if (model.IsCheck)
                {
                    //审核人
                    model.Approver = SiteUserInfo.Name;
                    //审核人id
                    model.ApproverId = SiteUserInfo.UserId;
                    //审核人部门
                    model.ApproverDeptId = SiteUserInfo.DeptId;
                    //审核时间
                    model.ApproveTime = DateTime.Now;
                }
                ls.Add(model);
            }
            if (ls.Count > 0)
            {
                if (new BTourOrder().AddTourOrderSales(ls))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
                }

            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
