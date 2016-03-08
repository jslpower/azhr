using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.PrintPage
{
    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class YingShou : EyouSoft.Common.Page.BackPage
    {
        protected string OrderId = Utils.GetQueryStringValue("OrderId");
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "应收明细";
            if (!IsPostBack)
            {
                PageInit();
            }
        }

        private void PageInit()
        {
            var l = new BLL.TourStructure.BTourOrder().GetTourOrderSalesListByOrderId(OrderId, Utils.GetQueryStringValue("ReturnOrSet") == "1" ? CollectionRefundState.收款 : CollectionRefundState.退款);
            if (l!=null)
            {
                this.rpt.DataSource = l;
                this.rpt.DataBind();
            }
        }

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.rpt.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rpt_ItemDataBound);

		}

		private void rpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
            if (e.Item.ItemIndex != 0) return;
		    ;
            var lblIssueTime = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblIssueTime");
            var lblOrderCode = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblOrderCode");
            var lblBuyCompanyName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblBuyCompanyName");
            var lblConfirmMomey = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblConfirmMomey");
            var m = new BLL.TourStructure.BTourOrder().GetOrderMoney(OrderId);
            if (m != null)
            {
                lblIssueTime.Text = m.IssueTime.ToShortDateString();
                lblOrderCode.Text = m.OrderCode;
                lblBuyCompanyName.Text = m.BuyCompanyName;
                lblConfirmMomey.Text = m.ConfirmMoney.ToString("F2");
            }
		}
    }
}
