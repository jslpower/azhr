using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WebFX
{
    public partial class OrderSee : EyouSoft.Common.Page.FrontPage
    {
        //获取tourid
        protected string tourID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = EyouSoft.Common.Utils.GetQueryStringValue("Type");
            tourID = EyouSoft.Common.Utils.GetQueryStringValue("tourid");

            if (!string.IsNullOrEmpty(type))
            {
                string id = EyouSoft.Common.Utils.GetQueryStringValue("OrderId");
                Response.Clear();
                Response.Write(DoUpdate(id));
                Response.End();
            }

            if (!IsPostBack)
            {
                string orderId = EyouSoft.Common.Utils.GetQueryStringValue("OrderId");
                string dotype = EyouSoft.Common.Utils.GetQueryStringValue("Type");
                if (!string.IsNullOrEmpty(orderId))
                {
                    PageInit(orderId, dotype);
                }
            }
        }


        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="orderId"></param>
        private void PageInit(string orderId, string type)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand order = bOrder.GetTourOrderExpandByOrderId(orderId);
            if (order != null)
            {
                this.LtOrderCode.Text = order.OrderCode;
                this.LtDCompanyName.Text = order.DCompanyName;
                this.LtDContactName.Text = order.DContactName;
                this.LtDContactTel.Text = order.DContactTel;

                this.LtSellerName.Text = order.SellerName;
                this.LtOperator.Text = order.Operator;
                this.LtAdults.Text = order.Adults.ToString();
                this.LtChilds.Text = order.Childs.ToString();
                this.LtAdultPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.AdultPrice, this.ProviderToMoney);
                this.LtChildPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.ChildPrice, this.ProviderToMoney);
                this.LtSaleAddCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SaleAddCost, this.ProviderToMoney);
                this.LtSaleAddCostRemark.Text = order.SaleAddCostRemark;
                this.LtSaleReduceCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SaleReduceCost, this.ProviderToMoney);
                this.LtSaleReduceCostRemark.Text = order.SaleReduceCostRemark;
                this.LtSumPrice.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(order.SumPrice, this.ProviderToMoney);

                this.LtSaveSeatDate.Text = order.SaveSeatDate.HasValue ? order.SaveSeatDate.Value.ToString() : string.Empty;
                this.LtOrderRemark.Text = order.OrderRemark;


                if (order.MTourOrderTravellerList != null && order.MTourOrderTravellerList.Count != 0)
                {
                    this.RpTravller.DataSource = order.MTourOrderTravellerList;
                    this.RpTravller.DataBind();
                }
                else
                {
                    this.phTraveller.Visible = false;
                }



            }
        }


        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private string DoUpdate(string orderId)
        {
            string msg = string.Empty;
            string strStatus = EyouSoft.Common.Utils.GetQueryStringValue("Status");
            if (string.IsNullOrEmpty(strStatus))
            {
                msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "订单修改 失败！");
            }
            else
            {

                EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                if (bOrder.UpdateTourOrderExpand(orderId, (EyouSoft.Model.EnumType.TourStructure.OrderStatus)int.Parse(strStatus), null))
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "订单修改 成功！");
                }
                else
                {
                    msg = EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "订单修改 失败！");

                }
            }

            return msg;
        }
    }
}
