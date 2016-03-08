using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.WebFX.PrintPage
{
    public partial class QuoteDetail : EyouSoft.Common.Page.FrontPage
    {
        /// <summary>
        /// 语言
        /// </summary>
        private EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

            string quoteId = EyouSoft.Common.Utils.GetQueryStringValue("quoteId");



            string lngType = EyouSoft.Common.Utils.GetQueryStringValue("LngType");
            if (string.IsNullOrEmpty(quoteId))
            {
                RCWE("异常请求");
            }
            else
            {
                LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)EyouSoft.Common.Utils.GetInt(lngType, 1);

                PageInit(quoteId, lngType);
            }



        }

        private void PageInit(string quoteId, string lngType)
        {
            //语言
            EyouSoft.Model.EnumType.SysStructure.LngType LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)EyouSoft.Common.Utils.GetInt(lngType, 1);

            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = bll.GetQuoteModel(quoteId);
            if (model != null)
            {

                //行程亮点
                this.ltJourneySpot.Text = LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文 ? model.JourneySpot : GetJourneySpot(model.QuoteJourneyList);

                //绑定行程
                this.rpPlan.DataSource = model.QuotePlanList;
                this.rpPlan.DataBind();


                //绑定购物点
                //this.ltShop.Text = GetQuoteShop(model.QuoteShopList);

                //绑定风味餐
                this.ltFoot.Text = GetQuoteFoot(model.QuoteFootList);

                //绑定赠送
                this.ltGive.Text = GetQuoteGive(model.QuoteGiveList);

                //绑定小费
                this.rpTip.DataSource = model.QuoteTipList;
                this.rpTip.DataBind();



                //绑定价格
                if (model.QuotePriceList != null)
                {
                    this.rpPrice.DataSource = model.QuotePriceList.Where(c => c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格);
                    this.rpPrice.DataBind();
                }

                //报价备注
                this.ltRemark.Text = LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文 ? model.QuoteRemark : GetTravelNote(model.QuoteJourneyList);
            }
        }

        /// <summary>
        /// 报价行程景点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetQuotePlanSpot(object obj)
        {
            StringBuilder spot = new StringBuilder();

            if (obj != null)
            {
                IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> list = (IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot>)obj;
                if (list != null && list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        //if (!string.IsNullOrEmpty(item.SpotName))
                        //{
                        //    spot.Append(item.SpotName + " ");
                        //}

                        spot.Append(GetLngTypeSpots(item.SpotId, item.SpotName) + " ");
                    }
                }
            }
            return spot.ToString();
        }

        /// <summary>
        /// 交通
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuoteTraffic(EyouSoft.Model.EnumType.PlanStructure.PlanProject project)
        {
            string trafficPic = string.Empty;

            if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机)
            {
                trafficPic = string.Format("<img src='{0}'>", Utils.ConvertToAbsolute("/images/feij.gif"));
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车)
            {
                trafficPic = string.Format("<img src='{0}'>", Utils.ConvertToAbsolute("/images/car.gif"));
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船)
            {
                trafficPic = string.Format("<img src='{0}'>", Utils.ConvertToAbsolute("/images/lunchuan.gif"));
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车)
            {
                trafficPic = string.Format("<img src='{0}'>", Utils.ConvertToAbsolute("/images/huoche.gif"));
            }
            else
            {
                trafficPic = "";
            }

            return trafficPic;
        }


        /// <summary>
        /// 获取城市和景点信息
        /// </summary>
        /// <param name="city"></param>
        /// <param name="traffic"></param>
        /// <returns></returns>
        protected string GetQuotePlanCity(object city, object scenic)
        {
            StringBuilder query = new StringBuilder();
            IList<EyouSoft.Model.HTourStructure.MQuotePlanCity> list = (IList<EyouSoft.Model.HTourStructure.MQuotePlanCity>)city;
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    query.Append(list[i].CityName);
                    if (scenic != null)
                    {
                        IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> listscenic = (IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot>)scenic;
                        if (listscenic != null && listscenic.Count != 0)
                        {
                            foreach (var item in listscenic)
                            {
                                //query.Append("(" + GetLngTypeSpots(item.SpotId, item.SpotName));
                                EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();
                                EyouSoft.Model.HGysStructure.MJingDianJingDianInfo model = bll.GetJingDianInfo(item.SpotId, LngType, list[i].CityId.ToString());
                                if (model != null)
                                {
                                    query.Append("(" + GetLngTypeSpots(model.JingDianId, model.Name) + "【" + model.YouLanShiJian + "】)");
                                }
                            }
                        }
                    }
                    if (i != list.Count - 1 || list.Count == 1)
                        query.Append(GetQuoteTraffic(list[i].JiaoTong));
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// 购物点
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuoteShop(IList<EyouSoft.Model.HTourStructure.MQuoteShop> list)
        {
            StringBuilder query = new StringBuilder();
            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    query.Append(item.ShopName + " ");
                }
            }
            return query.ToString();
        }


        /// <summary>
        /// 风味餐
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuoteFoot(IList<EyouSoft.Model.HTourStructure.MQuoteFoot> list)
        {
            StringBuilder query = new StringBuilder();


            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    query.AppendFormat("{0}:{1}", GetLngTypeMenu(item.MenuId, item.Menu), ToMoneyString(item.Price));
                    if (!string.IsNullOrEmpty(item.Remark))
                    {
                        query.AppendFormat("({0})", item.Remark);
                    }
                    query.Append("；");
                }
            }
            return query.ToString();
        }

        /// <summary>
        /// 赠送
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuoteGive(IList<EyouSoft.Model.HTourStructure.MQuoteGive> list)
        {
            StringBuilder query = new StringBuilder();
            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    query.AppendFormat("{0}:{1}", item.Item, ToMoneyString(item.Price));
                    if (!string.IsNullOrEmpty(item.Remark))
                    {
                        query.AppendFormat("({0})", item.Remark);
                    }
                    query.Append("；");
                }
            }
            return query.ToString();
        }

        /// <summary>
        /// 绑定小费
        /// </summary>
        /// <returns></returns>
        protected string GetQuoteTip(IList<EyouSoft.Model.HTourStructure.MQuoteTip> list)
        {
            StringBuilder query = new StringBuilder();
            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    query.AppendFormat("小费名称：{0}", item.Tip);
                    query.AppendFormat("单价：{0}", ToMoneyString(item.Price));
                    query.AppendFormat("天数:{0}", item.Days);
                    query.AppendFormat("合计金额:{0}", ToMoneyString(item.SumPrice));
                    if (!string.IsNullOrEmpty(item.Remark))
                    {
                        query.AppendFormat("备注：{0}", item.Remark);
                    }
                    query.Append("<br/>");
                }
            }
            return query.ToString();
        }

        /// <summary>
        /// 获取餐馆名称(中英泰)
        /// </summary>
        /// <param name="GysId"></param>
        /// <returns></returns>
        protected string GetLngTypeRestaurant(string GysId)
        {
            if (!string.IsNullOrEmpty(GysId))
            {
                var model = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId, LngType);
                if (model != null) return model.GysName;
            }

            return null;

        }

        /// <summary>
        /// 酒店语言处理(中英泰)
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="hotelName"></param>
        /// <returns></returns>
        protected string GetLngTypeHotel(string hotelId, string hotelName)
        {

            if (LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文)
            {
                if (!string.IsNullOrEmpty(hotelId))
                {
                    var model = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(hotelId, LngType);
                    if (model != null)
                        return model.GysName;
                }
            }
            return hotelName;
        }

        /// <summary>
        /// 菜单语言处理(中英泰)
        /// </summary>
        /// <param name="BreakfastMenuId"></param>
        /// <returns></returns>
        protected string GetLngTypeMenu(string menuId, string menuName)
        {

            if (LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文)
            {
                if (!string.IsNullOrEmpty(menuId))
                {
                    var model = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDanInfo(menuId,LngType);
                    if (model != null)
                        return model.Name;
                }
            }
            return menuName;
        }

        /// <summary>
        /// 景点语言处理(中英泰)
        /// </summary>
        /// <param name="spotId"></param>
        /// <param name="spotName"></param>
        /// <returns></returns>
        protected string GetLngTypeSpots(string spotId, string spotName)
        {
            if (LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文)
            {
                if (!string.IsNullOrEmpty(spotId))
                {
                    var model = new EyouSoft.BLL.HGysStructure.BGys().GetJingDianInfo(spotId, LngType);
                    if (model != null)
                        return model.Name;
                }
            }

            return spotName;


        }

        /// <summary>
        /// 景点导游词处理(中英泰)
        /// </summary>
        /// <param name="obj">QuotePlanSpotList</param>
        /// <param name="GuideWord"></param>
        /// <returns></returns>
        protected string GetLngTypeSpotsGuideWord(object obj, string GuideWord)
        {
            //  IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> list
            if (LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文)
            {
                IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> list = (IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot>)obj;
                if (list != null && list.Count != 0)
                {
                    StringBuilder query = new StringBuilder();

                    foreach (var item in list)
                    {
                        var model = new EyouSoft.BLL.HGysStructure.BGys().GetJingDianInfo(item.SpotId, LngType);
                        if (model != null)
                        {
                            query.AppendFormat("{0}:{1}<br/>", model.Name, model.JianJie);

                        }
                    }

                    return query.ToString();

                }
            }


            return GuideWord;


        }

        /// <summary>
        /// 获取行程亮点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetJourneySpot(IList<EyouSoft.Model.HTourStructure.MQuoteJourney> list)
        {
            if (list == null || list.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();

            list = list.Where(c => c.JourneyType == EyouSoft.Model.EnumType.TourStructure.JourneyType.行程亮点).ToList();
            if (list == null || list.Count == 0) return string.Empty;

            int[] ids = list.Select(c => c.SourceId).ToArray();
            if (ids == null || ids.Length == 0) return string.Empty;

            IList<EyouSoft.Model.SysStructure.MSysJourneyLight> items = new EyouSoft.BLL.SysStructure.BSysOptionConfig().GetJourneyLightList(ids, LngType);
            if (items == null || items.Count == 0) return string.Empty;

            for (var i = 0; i < items.Count; i++)
            {
                s.AppendFormat("{0}、{1} <br/>", i + 1, items[i].JourneySpot);
            }

            return s.ToString();
        }

        /// <summary>
        /// 获取报价备注
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetTravelNote(IList<EyouSoft.Model.HTourStructure.MQuoteJourney> list)
        {
            if (list == null || list.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();

            list = list.Where(c => c.JourneyType == EyouSoft.Model.EnumType.TourStructure.JourneyType.报价备注).ToList();
            if (list == null || list.Count == 0) return string.Empty;

            int[] ids = list.Select(c => c.SourceId).ToArray();
            if (ids == null || ids.Length == 0) return string.Empty;

            IList<EyouSoft.Model.SysStructure.MBackPriceMark> items = new EyouSoft.BLL.SysStructure.BSysOptionConfig().GetMBackPriceMarkList(ids, LngType);
            if (items == null || items.Count == 0) return string.Empty;

            for (var i = 0; i < items.Count;i++ )
            {
                s.AppendFormat("{0}、{1} <br/>", i + 1, items[i].BackMark);
            }

            return s.ToString();
        }

        /// <summary>
        /// 报价行程购物
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuotePlanShop(object obj)
        {
            var spot = new StringBuilder();

            if (obj != null)
            {
                var list = (IList<EyouSoft.Model.HTourStructure.MQuoteShop>)obj;
                if (list.Count != 0)
                {
                    foreach (var item in list.Where(item => !string.IsNullOrEmpty(item.ShopName)))
                    {
                        spot.Append(item.ShopName + " ");
                    }
                }
            }
            return spot.ToString();
        }

    }
}
