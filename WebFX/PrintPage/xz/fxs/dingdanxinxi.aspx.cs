using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.WebFX.PrintPage.xz.fxs
{
    public partial class dingdanxinxi : System.Web.UI.Page
    {
        protected string url = "";
        protected MUserInfo SiteUserInfo = null;
        protected string ProviderToMoney = "en-us";
        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (_IsLogin)
            {
                string type = Utils.GetQueryStringValue("type");
                if (!string.IsNullOrEmpty(tourId) && SiteUserInfo != null)
                {
                    InitPage(tourId, type);
                }
                this.Title = PrintTemplateType.分销商平台订单信息汇总单.ToString();
                url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, PrintTemplateType.分销商平台游客信息打印单);
                url += "?tourId=" + tourId + "&type=" + type;
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="tourId"></param>
        protected void InitPage(string tourId, string type)
        {
            EyouSoft.BLL.TourStructure.BTour BTour = new EyouSoft.BLL.TourStructure.BTour();
            EyouSoft.Model.TourStructure.MTourBaseInfo model = BTour.GetTourInfo(tourId);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
            }
            EyouSoft.BLL.TourStructure.BTourOrder BLL = new EyouSoft.BLL.TourStructure.BTourOrder();
            IList<EyouSoft.Model.TourStructure.MTourOrderSummary> list = BLL.GetTourOrderSummaryByTourId(tourId);
            if (list != null && list.Count > 0)
            {
                if (type == "1")
                {
                    rpt_OrderList.DataSource = list.Where(i => i.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位);
                }
                else
                {
                    rpt_OrderList.DataSource = list.Where(i => i.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交);
                }
                rpt_OrderList.DataBind();
            }
        }

        /// <summary>
        /// 嵌套repeater绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_OrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater BackList = (Repeater)e.Item.FindControl("rpt_BackList");
                Repeater CustomerList = (Repeater)e.Item.FindControl("rpt_CustomerList");
                MTourOrderSummary model = ((MTourOrderSummary)e.Item.DataItem) ?? new MTourOrderSummary();

                IList<MTourOrderSales> list = model.TourOrderSalesList;
                if (model.TourOrderTravellerList != null)
                {
                    IList<MTourOrderTraveller> ls = new List<MTourOrderTraveller>();
                    ls = model.TourOrderTravellerList.Where(item => item.TravellerStatus == TravellerStatus.在团).ToList();
                    if (ls != null && ls.Count > 0)
                    {
                        ls = null;
                        ls = new List<MTourOrderTraveller>();
                        ls.Add(model.TourOrderTravellerList.Where(item => item.TravellerStatus == TravellerStatus.在团).ToList().First());
                    }

                    CustomerList.DataSource = ls;
                    CustomerList.DataBind();
                }
                if (list != null && list.Count > 0)
                {
                    BackList.DataSource = list;
                    BackList.DataBind();
                }
            }
        }

    }
}
