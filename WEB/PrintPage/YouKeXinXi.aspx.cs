using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.Web.PrintPage
{
    using System.Collections.Generic;

    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class YouKeXinXi : EyouSoft.Common.Page.BackPage
    {
        /// <summary>
        /// 游客信息汇总单
        /// 方琪 2012-05-15
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            string orderId = Utils.GetQueryStringValue("orderId");
            string type = Utils.GetQueryStringValue("type");
            if (tourId != "")
            {
                InitPageForTour(tourId, type);
            }
            if (orderId != "")
            {
                InitPageForOrder(orderId);
            }
            this.Title = PrintTemplateType.游客名单.ToString();
        }

        /// <summary>
        /// 团队下面的游客信息汇总
        /// </summary>
        /// <param name="tourId">团队编号</param>
        protected void InitPageForTour(string tourId, string type)
        {
            var BTour = new EyouSoft.BLL.TourStructure.BTour();
            var model = BTour.GetBasicTourInfo(tourId);
            if (model != null)
            {
                this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
                this.lbName.Text = model.OperatorInfo.Name + (string.IsNullOrEmpty(model.OperatorInfo.Mobile) ? "" : "/" + model.OperatorInfo.Mobile);
                if (model.GuideList != null && model.GuideList.Count > 0)
                {
                    this.lbGuid.Text = model.GuideList.First().Name + (string.IsNullOrEmpty(model.GuideList.First().Phone) ? "" : "/" + model.GuideList.First().Phone);
                }
                if (model.TourPlaner != null && model.TourPlaner.Count > 0)
                {
                    this.lbTourPlaner.Text = model.TourPlaner.First().Planer + (string.IsNullOrEmpty(model.TourPlaner.First().Phone) ? "" : "/" + model.TourPlaner.First().Phone);
                }
            }
            var BLL = new EyouSoft.BLL.HTourStructure.BTourOrder();
            IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.HTourStructure.MTourOrderTraveller>();
            int[] arry = null;
            switch (type)
            {
                case "1":
                    arry = new int[] { (int)OrderStatus.已留位 };
                    list = BLL.GetTourOrderTraveller(tourId);
                    break;
                case "2":
                    arry = new int[] { (int)OrderStatus.已成交 };
                    list = BLL.GetTourOrderTraveller(tourId);
                    break;
                default:
                    list = BLL.GetTourOrderTraveller(tourId);
                    break;

            }
            if (list != null && list.Count > 0)
            {
                this.rpt_CustomerList.DataSource = list.Where(i => i.TravellerStatus == EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团).ToList();
                this.rpt_CustomerList.DataBind();
            }
        }

        /// <summary>
        /// 订单下面的游客信息汇总
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="tourId">团队编号</param>
        protected void InitPageForOrder(string orderId)
        {
            EyouSoft.BLL.TourStructure.BTourOrder BLL = new EyouSoft.BLL.TourStructure.BTourOrder();
            IList<EyouSoft.Model.TourStructure.BuyCompanyTraveller> list = BLL.GetTourOrderBuyCompanyTravellerByOrderId(orderId);

            if (list != null && list.Count > 0)
            {
                this.rpt_CustomerList.DataSource = list.Where(p => p.TravellerStatus == TravellerStatus.在团);
                this.rpt_CustomerList.DataBind();
            }

            EyouSoft.Model.TourStructure.MTourOrderExpand model = BLL.GetTourOrderExpandByOrderId(orderId);
            EyouSoft.BLL.TourStructure.BTour BTour = new EyouSoft.BLL.TourStructure.BTour();

            if (model != null)
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourModel = BTour.GetBasicTourInfo(model.TourId);
                if (tourModel != null)
                {
                    this.lbRouteName.Text = tourModel.RouteName;
                    this.lbTourCode.Text = tourModel.TourCode;
                    this.lbName.Text = tourModel.OperatorInfo.Name + (string.IsNullOrEmpty(tourModel.OperatorInfo.Mobile) ? "" : "/" + tourModel.OperatorInfo.Mobile);
                    if (tourModel.GuideList != null && tourModel.GuideList.Count > 0)
                    {
                        this.lbGuid.Text = tourModel.GuideList.First().Name + (string.IsNullOrEmpty(tourModel.GuideList.First().Phone) ? "" : "/" + tourModel.GuideList.First().Phone);
                    }
                    if (tourModel.TourPlaner != null && tourModel.TourPlaner.Count > 0)
                    {
                        this.lbTourPlaner.Text = tourModel.TourPlaner.First().Planer + (string.IsNullOrEmpty(tourModel.TourPlaner.First().Phone) ? "" : "/" + tourModel.TourPlaner.First().Phone);
                    }
                }
            }


        }
    }
}
