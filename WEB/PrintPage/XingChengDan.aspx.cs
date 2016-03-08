using System;
using System.Linq;

namespace EyouSoft.Web.PrintPage
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web;

    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.HTourStructure;

    public partial class XingChengDan : EyouSoft.Common.Page.BackPage
    {
        /// <summary>
        /// 语言
        /// </summary>
        private EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        protected DateTime tourDate = DateTime.Now;
        /// <summary>
        /// 团队状态
        /// </summary>
        protected EyouSoft.Model.EnumType.TourStructure.TourStatus tourstatus;

        protected void Page_Load(object sender, EventArgs e)
        {

            string tourId = EyouSoft.Common.Utils.GetQueryStringValue("tourid");
            string lngType = EyouSoft.Common.Utils.GetQueryStringValue("LngType");
            string type = Utils.GetQueryStringValue("type");

            if (string.IsNullOrEmpty(tourId))
            {
                RCWE("异常请求");
            }
            else
            {
                LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)EyouSoft.Common.Utils.GetInt(lngType, 1);
                PageInit(tourId, (EyouSoft.Model.EnumType.TourStructure.TourType)(Utils.GetInt(type)));
            }

            var isGuideConfirm = Common.Utils.GetQueryStringValue("IsGuideConfirm");
            var Print_XingChengDan = string.Empty;
            if (!string.IsNullOrEmpty(isGuideConfirm) && isGuideConfirm.Equals("1"))
            {
                var b = new EyouSoft.BLL.TourStructure.BBianGeng();
                var m = b.GetFirstBianGeng(tourId, BianType.导游确认);
                if (m == null || string.IsNullOrEmpty(m.Url))
                {
                    Print_XingChengDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
                    if (!string.IsNullOrEmpty(Print_XingChengDan))
                    {
                        //导游确认时保留确认前的行程信息
                        var bModel = new EyouSoft.Model.TourStructure.MBianGeng
                        {
                            BianId = tourId,
                            BianType = EyouSoft.Model.EnumType.TourStructure.BianType.导游确认,
                            OperatorId = SiteUserInfo.UserId,
                            Url =
                                new EyouSoft.Toolkit.request(
                                Utils.ConvertToAbsolute(Print_XingChengDan) + "?tourid=" + tourId,
                                1024,
                                768,
                                1024,
                                768,
                                SiteUserInfo.CompanyId,
                                HttpContext.Current.Request.Cookies).SavePageAsImg()
                        };
                        b.InsertBianGeng(bModel);
                    }
                }
            }

        }
        /// <summary>
        /// 获取成团后的行程日期
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        protected string GettourDate(int day)
        {
            string str = string.Empty;
            str = tourDate.AddDays(day).ToString("MMdd") + "(" + Utils.ConvertWeekDayToChinese(tourDate.AddDays(day)) + ")";
            return str;
        }

        private void PageInit(string quoteId, EyouSoft.Model.EnumType.TourStructure.TourType type)
        {
            var bll = new EyouSoft.BLL.HTourStructure.BTour();


            var model = bll.GetTourModel(quoteId);
            if (model != null)
            {
                tourDate = model.LDate;
                tourstatus = model.TourStatus;
                //行程亮点
                var s = string.Empty;

                var sb = new StringBuilder();
                var md = new EyouSoft.BLL.SysStructure.BGeography().GetCountry(SiteUserInfo.CompanyId, model.CountryId);
                if (model.TourRoomList != null && model.TourRoomList.Count > 0)
                {
                    s = model.TourRoomList.Aggregate(s, (current, m) => current + (m.Num + m.TypeName + "+")).Trim('+');
                }
                sb.AppendFormat("<span style=\"font-size: medium;text-align:center;width:100%;display:inline-block\">☆关于接待{0}团计划（<select><option>新增</option><option>修改</option><option>确认</option></select>）☆</span><br/>", model.TourCode + (md!=null?md.Name:string.Empty));
                sb.AppendFormat("<span style=\"text-align:left\">&nbsp;&nbsp;&nbsp;&nbsp;我社自组团{0}一行<span style=\"text-decoration:underline\">{1}成人+{2}儿童+{3}陪</span>将于{4}来华旅游，各用房{5}费用按团队协议价报我社。</span><br/>", model.TourCode, model.Adults, model.Childs, model.Leaders, model.LDate.ToString("yyyy年MM月dd日") + "-" + model.RDate.ToString("MM月dd日"), s);
                this.ltJourneySpot.Text = sb.ToString();

                //绑定地接社
                this.rpt.DataSource = model.TourDiJieList;
                this.rpt.DataBind();
                //if (type == TourType.自由行)
                //{
                //    List<MTourPlan> TourPlanList = new List<MTourPlan>();
                //    string[] TourIds = null;
                //    if (!string.IsNullOrEmpty(model.RouteIds))
                //        TourIds = model.RouteIds.Split(',');
                //    if (TourIds.Length > 0)
                //    {
                //        EyouSoft.Model.HTourStructure.MTour modelroute = null;
                //        for (int i = 0; i < TourIds.Length; i++)
                //        {
                //            if (TourIds[i].ToString().Trim() != "")
                //            {
                //                modelroute = new EyouSoft.BLL.HTourStructure.BTour().GetRouteInfoByTourId(TourIds[i].ToString());
                //                if (modelroute != null)
                //                {
                //                    if (modelroute.TourPlanList != null && modelroute.TourPlanList.Count > 0)
                //                        TourPlanList.AddRange(modelroute.TourPlanList);
                //                }
                //            }
                //        }
                //    }
                //    //绑定行程
                //    this.rpPlan.DataSource = TourPlanList;
                //    this.rpPlan.DataBind();

                //}
                //else
                //{
                //绑定行程
                this.rpPlan.DataSource = model.TourPlanList;
                this.rpPlan.DataBind();
                //}


                //绑定风味餐
                this.ltFoot.Text = GetQuoteFoot(model.TourFootList);

                //绑定赠送
                this.ltGive.Text = GetQuoteGive(model.TourGiveList);

                //绑定小费
                this.rpTip.DataSource = model.TourTipList;
                this.rpTip.DataBind();



                //绑定价格
                if (model.TourPriceList != null)
                {
                    this.rpPrice.DataSource = model.TourPriceList.Where(c => c.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格);
                    this.rpPrice.DataBind();
                }

                this.ltRemark.Text = model.TravelNote;

            }

        }

        /// <summary>
        /// 报价行程景点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetQuotePlanSpot(object obj)
        {
            var spot = new StringBuilder();

            if (obj != null)
            {
                var list = (IList<EyouSoft.Model.HTourStructure.MTourPlanSpot>)obj;
                if (list != null && list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item.SpotName))
                        {
                            // spot.Append(item.SpotName + " ");
                            spot.AppendFormat("{0}({1},{2})", GetLngTypeSpots(item.SpotId, item.SpotName), ToMoneyString(item.Price), new EyouSoft.BLL.HGysStructure.BGys().GetJingDianInfo
(item.SpotId).YouLanShiJian);
                        }
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
                trafficPic = "<img width='20' height='20' src='../images/jt_feiji.png'>";
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车)
            {
                trafficPic = "<img width='20' height='20' src='../images/jt_qiche.png'>";
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船)
            {
                trafficPic = "<img width='20' height='20' src='../images/jt_youlun.png'>";
            }
            else if (project == EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车)
            {
                trafficPic = "<img width='20' height='20' src='../images/jt_huoche.png'>";
            }
            else
            {
                trafficPic = "";
            }

            return trafficPic;
        }

        /// <summary>
        /// 获取城市 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="traffic"></param>
        /// <returns></returns>
        protected string GetQuotePlanCity(object city, object scenic)
        {
            StringBuilder query = new StringBuilder();
            IList<EyouSoft.Model.HTourStructure.MTourPlanCity> list = (IList<EyouSoft.Model.HTourStructure.MTourPlanCity>)city;
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    query.Append(list[i].CityName);
                    if (scenic != null)
                    {
                        IList<EyouSoft.Model.HTourStructure.MTourPlanSpot> listscenic = (IList<EyouSoft.Model.HTourStructure.MTourPlanSpot>)scenic;
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
        protected string GetQuoteShop(IList<EyouSoft.Model.HTourStructure.MTourShop> list)
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
        protected string GetQuoteFoot(IList<EyouSoft.Model.HTourStructure.MTourFoot> list)
        {
            StringBuilder query = new StringBuilder();


            if (list != null && list.Count != 0)
            {
                foreach (var item in list)
                {
                    query.AppendFormat("{0}:{1}", item.Menu, ToMoneyString(item.Price));
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
        protected string GetQuoteGive(IList<EyouSoft.Model.HTourStructure.MTourGive> list)
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
        protected string GetQuoteTip(IList<EyouSoft.Model.HTourStructure.MTourTip> list)
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
                    var model = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDanInfo(menuId, LngType);
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
                IList<EyouSoft.Model.HTourStructure.MTourPlanSpot> list = (IList<EyouSoft.Model.HTourStructure.MTourPlanSpot>)obj;
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
        /// 行程购物
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetQuotePlanShop(object obj)
        {
            var spot = new StringBuilder();

            if (obj != null)
            {
                var list = (IList<EyouSoft.Model.HTourStructure.MTourShop>)obj;
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
