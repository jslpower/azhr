using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;


namespace EyouSoft.WebFX
{
    public partial class FinancialRegistration : EyouSoft.Common.Page.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                Response.Clear();
                Response.Write(Save(type));
                Response.End();
            }

            if (!IsPostBack)
            {
                string orderId = Utils.GetQueryStringValue("OrderId");
                if (!string.IsNullOrEmpty(orderId))
                {
                    PageInit(orderId);

                }
            }
        }


        /// <summary>
        /// 订单销售员
        /// </summary>
        /// <returns></returns>
        protected string GetUser()
        {
            return SiteUserInfo.Username;
        }

        private void PageInit(string orderId)
        {

            EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
            IList<EyouSoft.Model.TourStructure.MTourOrderSales> list = bOrder.GetTourOrderSalesListByOrderId(orderId, EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款);
            this.RpIncome.DataSource = list;
            this.RpIncome.DataBind();
            EyouSoft.Model.TourStructure.OrderMoney order = bOrder.GetOrderMoney(orderId);
            this.LtTotalReceived.Text = UtilsCommons.GetMoneyString(order.ConfirmMoney, this.ProviderToMoney);
            this.LtTotalSumPrice.Text = UtilsCommons.GetMoneyString(order.CheckMoney, this.ProviderToMoney);
            this.LtTotalUnReceived.Text = UtilsCommons.GetMoneyString(order.ConfirmMoney - order.CheckMoney + order.ReturnMoney, this.ProviderToMoney);

            
        }



        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="sales"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool ValidateForm(ref EyouSoft.Model.TourStructure.MTourOrderSales sales, ref string msg)
        {
            string id = EyouSoft.Common.Utils.GetFormValue("id");
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                sales = bOrder.GetTourOrderSalesById(id);

            }
            else
            {
                sales.OrderId = Utils.GetFormValue("OrderId");
            }

            sales.OperatorId = SiteUserInfo.UserId;
            sales.Operator = SiteUserInfo.Username;

            DateTime? collectionRefundDate = Utils.GetDateTimeNullable(Utils.GetFormValue("txt_collectionRefundDate"));
            if (!collectionRefundDate.HasValue)
            {
                msg += "付款时间格式不正确！<br/>";
            }
            decimal collectionRefundAmount = Utils.GetDecimal(Utils.GetFormValue("txt_collectionRefundAmount"));
            if (collectionRefundAmount <= 0)
            {
                msg += "付款金额格式不正确！<br/>";
            }

            int collectionRefundMode = Utils.GetInt(Utils.GetFormValue("ddl_CollectionRefundMode"));
            if (collectionRefundMode <= 0)
            {
                msg += "请选择付款方式！";
            }



            sales.CollectionRefundOperatorID = SiteUserInfo.UserId;
            sales.CollectionRefundOperator = SiteUserInfo.Username;

            sales.CollectionRefundDate = (collectionRefundDate);
            sales.CollectionRefundAmount = collectionRefundAmount;
            sales.CollectionRefundMode = collectionRefundMode;
            sales.CollectionRefundState = EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款;
            sales.Memo = Utils.GetFormValue("txt_Memo");
            sales.IsCheck = false;

            return msg.Length <= 0;
        }


        private string Save(string type)
        {
            string msg = string.Empty;
            EyouSoft.Model.TourStructure.MTourOrderSales sales = new EyouSoft.Model.TourStructure.MTourOrderSales();
            switch (type)
            {
                case "Add":
                    if (ValidateForm(ref sales, ref msg))
                    {
                        EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                        if (bOrder.AddTourOrderSales(sales))
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "添加成功！");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "添加失败！");
                        }
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", msg + "<br/>添加失败！");
                    }
                    break;
                case "Update":

                    if (ValidateForm(ref sales, ref msg))
                    {
                        EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                        if (bOrder.UpdateTourOrderSales(sales))
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "修改成功！");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "修改失败！");
                        }
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", msg + "<br/>修改失败！");
                    }

                    break;

                case "Delete":
                    string id = EyouSoft.Common.Utils.GetFormValue("id");

                    if (!string.IsNullOrEmpty(id))
                    {
                        EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                        if (bOrder.DeleteTourOrderSales(id))
                        {
                            msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                        }
                        else
                        {
                            msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                        }
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                    }

                    break;
                default:
                    msg = UtilsCommons.AjaxReturnJson("0", "操作失败，服务器忙！");
                    break;


            }
            return msg;
        }


        protected string GetStatusForShenhe(object v)
        {
            if (v != null)
            {
                bool status =  (bool)v;

                if (status)
                {
                    return "已审";
                }
            }

            return "待审";
        }


    }
}
