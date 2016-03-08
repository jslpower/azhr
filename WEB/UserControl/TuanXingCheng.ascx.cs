using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 刘飞
    /// 2013-6-13
    /// 行程控件
    /// </summary>
    public partial class TuanXingCheng : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.HTourStructure.MTourPlan> _setPlanList;
        public IList<EyouSoft.Model.HTourStructure.MTourPlan> SetPlanList
        {
            get { return _setPlanList; }
            set { _setPlanList = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SetPlanList != null && SetPlanList.Count > 0)
            {
                this.rptJourney.DataSource = SetPlanList.OrderBy(p => p.Days);
                this.rptJourney.DataBind();
            }
        }

        private string _companyId;
        public string CompanyID
        {
            get { return _companyId; }
            set { _companyId = value; }
        }




        /// <summary>
        /// 初始化交通方式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetTraffic(object obj)
        {
            string trafficstr = string.Empty;
            PlanProject type = (PlanProject)obj;
            switch (type)
            {
                case PlanProject.飞机:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_feiji.png' alt=''></span></a>";
                    break;
                case PlanProject.火车:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_huoche.png' alt=''></span></a>";
                    break;
                case PlanProject.汽车:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_qiche.png' alt=''></span></a>";
                    break;
                case PlanProject.轮船:
                    trafficstr = "<a data-value='" + ((int)type).ToString() + "' href='javascript:;'><span><img class='lag flagvisibility' src='/images/jt_youlun.png' alt=''></span></a>";
                    break;
                default:
                    trafficstr = "<a href='javascript:;' data-value='-1'><span>请选择</span></a>";
                    break;
            }
            return trafficstr;
        }

        /// <summary>
        /// 通过景区的数据返回html
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetSinceByList(object o)
        {
            if (o != null)
            {
                //获得景点集合
                IList<MTourPlanSpot> list = (IList<MTourPlanSpot>)o;
                if (list.Count > 0 && list[0].SpotId.ToString().Trim() != "")
                {
                    string val = string.Empty;
                    string text = string.Empty;
                    string html = string.Empty;
                    string pricejs = string.Empty;
                    string priceth = string.Empty;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == 0)
                        {
                            pricejs += list[i].SettlementPrice.ToString("f2");
                            priceth += list[i].Price.ToString("f2");
                        }
                        else
                        {
                            pricejs += "," + list[i].SettlementPrice.ToString("f2");
                            priceth += "," + list[i].Price.ToString("f2");
                        }
                        val += list[i].SpotId + ",";
                        text += Server.UrlEncode(list[i].SpotName) + ",";
                        html += "<span id='" + list[i].SpotId + "' data-istuijian='" + GetScenicInfo(list[i].SpotId, "tuijian") + "' data-fujian='" + GetScenicInfo(list[i].SpotId, "path") + "' class='upload_filename'><a data-class='a_Journey_Since' data-pricejs='" + list[i].SettlementPrice.ToString("f2") + "' data-priceth='" + list[i].Price.ToString("f2") + "' data-for='" + list[i].SpotId + "'> " + list[i].SpotName + " </a> <a data-for='" + list[i].SpotId + "' href='javascript:void(0);' onclick='Journey.RemoveSince(this)'><img src='/images/cha.gif'></a></span>";
                    }
                    val = val.Substring(0, val.Length - 1);
                    text = text.Substring(0, text.Length - 1);
                    return "<input type='hidden' name='hd_scenery_spot' value='" + val + "' /><input type='hidden' name='show_scenery_spot' value='" + text + "' /> <input type='hidden' name='hidpriceth' value='" + priceth + "' /><input type='hidden' name='hidpricejs' value='" + pricejs + "' /><span data-class='fontblue' class='fontblue'>" + html + "</span>";
                }
            }
            return "<input type='hidden' name='hd_scenery_spot' value='' /><input type='hidden' name='show_scenery_spot' value='' /><input type='hidden' name='hidpriceth' value='' /><input type='hidden' name='hidpricejs' value='' /><span data-class='fontblue' class='fontblue'></span>";
        }

        /// <summary>
        /// 获取景点信息(是否推荐、景点图片)
        /// </summary>
        /// <param name="scenicid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetScenicInfo(string scenicid, string type)
        {
            string scenicinfo = string.Empty;
            if (!string.IsNullOrEmpty(scenicid))
            {
                EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();
                EyouSoft.Model.HGysStructure.MJingDianJingDianInfo model = bll.GetJingDianInfo(scenicid);
                if (model != null)
                {
                    if (type == "path")
                    {
                        if (model.FuJian != null)
                        {
                            scenicinfo = model.FuJian.FilePath;
                        }
                    }
                    if (type == "tuijian")
                    {
                        scenicinfo = Convert.ToInt32(model.IsTuiJian).ToString();
                    }
                }
            }
            return scenicinfo;
        }


        public IList<EyouSoft.Model.HTourStructure.MTourPlan> GetXingChengs()
        {
            var items = new List<EyouSoft.Model.HTourStructure.MTourPlan>();

            //城市
            string[] cityid = Utils.GetFormValues("hidcityids");
            string[] cityname = Utils.GetFormValues("hidcitys");
            //交通
            string[] traffictype = Utils.GetFormValues("hidtraffics");
            string[] trafficprice = Utils.GetFormValues("hidtrafficprices");
            //酒店1
            string[] hotelid1 = Utils.GetFormValues("hidhotel1id");
            string[] hotelid2 = Utils.GetFormValues("hidhotel2id");
            string[] hotelname1 = Utils.GetFormValues("txthotel1");
            //酒店2
            string[] hotelname2 = Utils.GetFormValues("txthotel2");
            string[] hotelprice1 = Utils.GetFormValues("txthotel1price");
            string[] hotelprice2 = Utils.GetFormValues("txthotel2price");
            //早餐
            string[] eatFrist = Utils.GetFormValues("eatFrist");
            string[] breakprice = Utils.GetFormValues("txtbreakprice");
            string[] breakid = Utils.GetFormValues("hidbreak");
            string[] breakname = Utils.GetFormValues("txtbreakname");
            string[] breakmenuid = Utils.GetFormValues("hidbreakmenuid");
            string[] breakfootid = Utils.GetFormValues("hidfastfootid");
            string[] breakpricejs = Utils.GetFormValues("hidfastprice");
            //午餐
            string[] eatSecond = Utils.GetFormValues("eatSecond");
            string[] secondprice = Utils.GetFormValues("txtsecondprice");
            string[] secondid = Utils.GetFormValues("hidsecond");
            string[] secondname = Utils.GetFormValues("txtsecondname");
            string[] secondmenuid = Utils.GetFormValues("hidsecondmenuid");
            string[] secondfootid = Utils.GetFormValues("hidsecondfootid");
            string[] secondpricejs = Utils.GetFormValues("hidsecondprice");
            //晚餐
            string[] eatThird = Utils.GetFormValues("eatThird");
            string[] thirdprice = Utils.GetFormValues("txtthirdprice");
            string[] thirdid = Utils.GetFormValues("hidthird");
            string[] thirdname = Utils.GetFormValues("txtthirdname");
            string[] thirdmenuid = Utils.GetFormValues("hidthirdmenuid");
            string[] thirdfootid = Utils.GetFormValues("hidthirdfootid");
            string[] thirdpricejs = Utils.GetFormValues("hidthirdprice");
            //景区
            string[] spotId = Utils.GetFormValues("hd_scenery_spot");
            string[] sportName = Utils.GetFormValues("show_scenery_spot");

            //行程内容
            string[] Content = Utils.GetFormEditorValues("txtContent");

            //景点图片
            string[] filepath = Utils.GetFormValues("filepath");
            string[] spotpricejss = Utils.GetFormValues("hidpricejs");//结算价
            string[] spotpriceths = Utils.GetFormValues("hidpriceth");//同行价


            if (cityid.Length > 0)
            {
                for (int i = 0; i < cityid.Length; i++)
                {
                    EyouSoft.Model.HTourStructure.MTourPlan model = new EyouSoft.Model.HTourStructure.MTourPlan();
                    model.FilePath = filepath[i];
                    model.BreakfastMenu = breakname[i];
                    model.BreakfastMenuId = breakmenuid[i];
                    model.BreakfastPrice = Utils.GetDecimal(breakprice[i].ToString());
                    model.BreakfastRestaurantId = breakid[i];
                    model.BreakfastId = breakfootid[i];
                    model.BreakfastSettlementPrice = Utils.GetDecimal(breakpricejs[i]);

                    model.Content = Content[i];
                    model.Days = (i + 1);
                    model.FilePath = filepath[i];

                    if (hotelname1[i].ToString() != "" && hotelid1[i].ToString() != "")
                    {
                        model.HotelId1 = hotelid1[i];
                        model.HotelName1 = hotelname1[i];
                        model.HotelPrice1 = Utils.GetDecimal(hotelprice1[i].ToString());
                    }
                    //if (hotelid2[i].ToString() != "" && hotelname2[i].ToString() != "")
                    //{
                    //    model.HotelId2 = hotelid2[i];
                    //    model.HotelName2 = hotelname2[i];
                    //    model.HotelPrice2 = Utils.GetDecimal(hotelprice2[i].ToString());
                    //}
                    model.IsBreakfast = Convert.ToBoolean(Utils.GetInt(eatFrist[i].ToString()));
                    model.IsLunch = Convert.ToBoolean(Utils.GetInt(eatSecond[i].ToString()));
                    model.IsSupper = Convert.ToBoolean(Utils.GetInt(eatThird[i].ToString()));
                    model.LunchMenu = secondname[i];
                    model.LunchMenuId = secondmenuid[i];
                    model.LunchPrice = Utils.GetDecimal(secondprice[i].ToString());
                    model.LunchSettlementPrice = Utils.GetDecimal(secondpricejs[i]);
                    model.LunchRestaurantId = secondid[i];
                    model.LunchId = secondfootid[i];
                    model.SupperMenu = thirdname[i];
                    model.SupperMenuId = thirdmenuid[i];
                    model.SupperPrice = Utils.GetDecimal(thirdprice[i].ToString());
                    model.SupperSettlementPrice = Utils.GetDecimal(thirdpricejs[i]);
                    model.SupperRestaurantId = thirdid[i];
                    model.SupperId = thirdfootid[i];
                    //model.Traffic = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictype[i], PlanProject.火车);
                    //model.TrafficPrice = Utils.GetDecimal(trafficprice[i].ToString());



                    model.TourPlanShopList = null;

                    IList<MTourPlanCity> citylist = new List<MTourPlanCity>();

                    if (cityid.Length > 0 && cityid[i].Length > 0)
                    {
                        string[] cityids = cityid[i].Split(',');
                        string[] citynames = cityname[i].Split(',');
                        string[] traffictypes = traffictype[i].Split(',');
                        string[] trafficprices = trafficprice[i].Split(',');
                        if (cityids.Length > 0)
                        {
                            for (int o = 0; o < cityids.Length; o++)
                            {
                                MTourPlanCity citymodel = new MTourPlanCity();
                                citymodel.CityId = Utils.GetInt(cityids[o]);
                                citymodel.CityName = citynames[o];
                                citymodel.JiaoTong = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictypes[o], PlanProject.火车);
                                citymodel.JiaoTongJiaGe = Utils.GetDecimal(trafficprices[o].ToString());
                                citylist.Add(citymodel);
                            }
                        }
                        else
                        {
                            MTourPlanCity citymodel = new MTourPlanCity();
                            citymodel.CityId = Utils.GetInt(cityid[i]);
                            citymodel.CityName = cityname[i];
                            citylist.Add(citymodel);
                        }
                    }

                    model.TourPlanCityList = citylist;

                    IList<EyouSoft.Model.HTourStructure.MTourPlanSpot> spotlist = new List<EyouSoft.Model.HTourStructure.MTourPlanSpot>();
                    if (spotId.Length > 0 && spotId[i].Length > 0)
                    {
                        string[] spotIdArray = spotId[i].Split(',');
                        string[] spotNameArray = sportName[i].Split(',');
                        string[] spotpricejs = spotpricejss[i].Split(',');
                        string[] spotpriceth = spotpriceths[i].Split(',');
                        if (spotIdArray.Length > 0)
                        {
                            for (int j = 0; j < spotIdArray.Length; j++)
                            {
                                EyouSoft.Model.HTourStructure.MTourPlanSpot spotModel = new EyouSoft.Model.HTourStructure.MTourPlanSpot();
                                spotModel.SpotId = spotIdArray[j];
                                spotModel.SpotName = System.Web.HttpContext.Current.Server.UrlDecode(spotNameArray[j]);
                                spotModel.Price = Utils.GetDecimal(spotpriceth[j]);
                                spotModel.SettlementPrice = Utils.GetDecimal(spotpricejs[j]);
                                spotlist.Add(spotModel);
                            }
                        }
                        else
                        {
                            EyouSoft.Model.HTourStructure.MTourPlanSpot spotModel = new EyouSoft.Model.HTourStructure.MTourPlanSpot();
                            spotModel.SpotId = spotId[i];
                            spotModel.SpotName = sportName[i];
                            spotModel.Price = Utils.GetDecimal(spotpriceths[i]);
                            spotModel.SettlementPrice = Utils.GetDecimal(spotpricejss[i]);
                            spotlist.Add(spotModel);
                        }
                    }
                    model.TourPlanSpotList = spotlist;

                    items.Add(model);
                }
            }

            return items;
        }

        protected void rptXingCheng_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1) return;

            var rpt = (Repeater)e.Item.FindControl("rptCityAndTraffic");

            if (rpt == null) return;

            var data = (EyouSoft.Model.HTourStructure.MTourPlan)e.Item.DataItem;

            if (data != null && data.TourPlanCityList != null)
            {
                rpt.DataSource = data.TourPlanCityList;
                rpt.DataBind();
            }

        }
    }
}