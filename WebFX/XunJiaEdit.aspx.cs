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
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Collections.Generic;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.WebFX
{
    using EyouSoft.Model.EnumType.GysStructure;
    using EyouSoft.Model.ComStructure;

    public partial class XunJiaEdit : FrontPage
    {
        protected string quoteId = string.Empty;
        //购物点
        protected string shopStr = string.Empty;
        protected string allcitys = string.Empty;
        protected int lgType = 0;
        protected string CountryId = "0";
        protected string Province = "0";

        /// <summary>
        /// 成本的价格信息
        /// </summary>
        protected IList<EyouSoft.Model.HTourStructure.MQuotePrice> Quote_Item_Price_List { get; set; }

        /// <summary>
        /// 销售的价格信息
        /// </summary>
        protected IList<EyouSoft.Model.HTourStructure.MQuotePrice> Quote_Sell_Price_List { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.HeadDistributorControl1.IsPubLogin =
    System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
            quoteId = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            lgType = Utils.GetInt(Utils.GetQueryStringValue("LgType")) == 0 ? 1 : Utils.GetInt(Utils.GetQueryStringValue("LgType"));
            if (doType != "")
            {
                Response.Clear();
                switch (doType)
                {
                    case "save":
                        Response.Write(PageSave());
                        break;
                }
                Response.End();
            }
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(quoteId))
                {
                    //新增初始化
                    this.txtxunjiatime.Value = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    BindDpt(0, lgType);
                    BindJiuDianXingJi(Model.EnumType.GysStructure.JiuDianXingJi.None);
                    shopStr = getshop(null, "");
                }
                else
                {
                    //修改初始化
                    PageInit();
                }
            }
        }

        private void PageInit()
        {
            this.Journey1.CompanyID = SiteUserInfo.CompanyId;
            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = new EyouSoft.Model.HTourStructure.MQuote();
            model = bll.GetQuoteModel(quoteId);
            if (model != null)
            {
                this.txt_Days.Value = model.Days.ToString();
                this.txtDiDaChengShi.Value = model.ArriveCity;
                this.txtDiDaHangBan.Value = model.ArriveCityFlight;
                this.txtPriceRemark.Text = model.QuoteRemark;
                this.txtLDate.Value = model.StartEffectTime.Year.ToString() + "-" + model.StartEffectTime.Month.ToString() + "-" + model.StartEffectTime.Day.ToString();
                this.txtLiKaiChengShi.Value = model.LeaveCity;
                this.txtLiKaiHangBan.Value = model.LeaveCityFlight;
                this.txtmaxadultcount.Value = model.MaxAdults.ToString();
                this.txtminadultcount.Value = model.MinAdults.ToString();
                this.txtPlanContent.Text = model.JourneySpot;
                this.txtRDate.Value = model.EndEffectTime.Year.ToString() + "-" + model.EndEffectTime.Month.ToString() + "-" + model.EndEffectTime.Day.ToString();
                this.txtroutename.Value = model.RouteName;
                this.txtSpecificRequire.Text = model.SpecificRequire;
                this.txtxunjiatime.Value = model.BuyTime.Year.ToString() + "-" + model.BuyTime.Month.ToString() + "-" + model.BuyTime.Day.ToString();
                if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
                {
                    this.PHBtnSave.Visible = false;
                }
                BindDpt(model.AreaId, lgType);
                BindJiuDianXingJi(model.JiuDianXingJi);
                CountryId = model.CountryId.ToString();
                if (model.QuoteFootList != null)
                {
                    this.selectFWeiCan1.SetFengWeiList = model.QuoteFootList;
                }
                if (model.QuoteGiveList != null)
                {
                    this.Give1.SetQuoteGiveList = model.QuoteGiveList;
                }
                if (model.QuoteJourneyList != null)
                {
                    this.SelectPriceRemark1.SetQuoteJourneyList = model.QuoteJourneyList;
                    this.SelectJourneySpot1.SetQuoteJourneyList = model.QuoteJourneyList;
                }
                //价格项目信息
                if (model.QuoteCostList != null && model.QuoteCostList.Count > 0)
                {
                    #region  项目价格信息
                    foreach (var item in model.QuoteCostList)
                    {
                        if (item.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格)
                        {
                            switch (item.Pricetype)
                            {

                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.餐:
                                    hidcancost.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机:
                                    hidpricetraffic.Value = item.Price.ToString("F2");
                                    break;

                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房1:
                                    hidpricehotel1.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房2:
                                    this.hidpricehotel2.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.景点:
                                    hidsceniccost.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.其他:
                                    hidpriceother.Value = item.Price.ToString("F2");
                                    break;

                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.综费:
                                    hidpricezongfei.Value = item.Price.ToString("F2");
                                    break;
                            }
                        }

                        if (item.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格)
                        {
                            switch (item.Pricetype)
                            {

                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.餐:
                                    hidcanprice.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机:
                                    hidpricetraffic.Value = item.Price.ToString("F2");
                                    break;

                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房1:
                                    hidpricehotel1.Value = item.Price.ToString("F2");

                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房2:
                                    this.hidpricehotel2.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.景点:
                                    hidscenicprice.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.其他:
                                    hidpriceother.Value = item.Price.ToString("F2");
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.综费:
                                    hidpricezongfei.Value = item.Price.ToString("F2");
                                    break;
                            }
                        }
                    }
                    #endregion
                }
                //自费
                if (model.QuoteSelfPayList != null && model.QuoteSelfPayList.Count > 0)
                {
                    this.SelfPay1.SetSelfPayList = model.QuoteSelfPayList;
                }

                //消费
                if (model.QuoteTipList != null && model.QuoteTipList.Count > 0)
                {
                    this.Tip1.SetQuoteTipList = model.QuoteTipList;
                }
                //行程
                if (model.QuotePlanList != null && model.QuotePlanList.Count > 0)
                {
                    this.Journey1.SetPlanList = model.QuotePlanList;
                    //获取行程中所以城市集合
                    for (int i = 0; i < model.QuotePlanList.Count; i++)
                    {
                        if (model.QuotePlanList[i].QuotePlanCityList != null && model.QuotePlanList[i].QuotePlanCityList.Count > 0)
                        {
                            for (int j = 0; j < model.QuotePlanList[i].QuotePlanCityList.Count; j++)
                            {
                                if (model.QuotePlanList[i].QuotePlanCityList[j].CityId.ToString() != "0")
                                    allcitys += model.QuotePlanList[i].QuotePlanCityList[j].CityId.ToString() + ",";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(allcitys)) allcitys = allcitys.Trim(',');
                }
                //购物
                shopStr = getshop(model.QuoteShopList, allcitys);
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        private string PageSave()
        {
            string msg = string.Empty;
            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = GetFormInfo();

            if (model.AreaId <= 0)
            {
                msg += (String)GetGlobalResourceObject("string", "请选择线路区域") + "<br />";
            }
            if (model.BuyTime.ToString("yyyy-MM-dd") == "")
            {
                msg += (String)GetGlobalResourceObject("string", "询价日期不能为空") + "<br />";
            }
            if (model.Days < 1)
            {
                msg += (String)GetGlobalResourceObject("string", "请输入正确的天数") + "<br />";
            }
            if (model.StartEffectTime.ToString("yyyy-MM-dd") == "")
            {
                msg += (String)GetGlobalResourceObject("string", "请输入抵达日期") + "<br />";
            }
            if (model.EndEffectTime.ToString("yyyy-MM-dd") == "")
            {
                msg += (String)GetGlobalResourceObject("string", "请输入离开日期") + "<br />";
            }
            if (model.MinAdults < 1)
            {
                msg += (String)GetGlobalResourceObject("string", "请输入最小成人数") + "<br />";
            }
            if (model.MaxAdults < 1)
            {
                msg += (String)GetGlobalResourceObject("string", "请输入最大成人数") + "<br />";
            }
            if (model.RouteName == "")
            {
                msg += (String)GetGlobalResourceObject("string", "请输入线路名称") + "<br />";
            }
            if (model.CountryId < 1)
            {
                msg += (String)GetGlobalResourceObject("string", "请选择国家") + "<br />";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                return UtilsCommons.AjaxReturnJson("0", msg);
            }
            int result = 0;
            if (model != null)
            {
                if (string.IsNullOrEmpty(quoteId))
                {
                    model.ParentId = "0";
                    result = bll.AddQuote(model);
                }
                else
                {
                    result = bll.UpdateQuote(model);
                }
            }
            switch (result)
            {
                case 1:
                    if (string.IsNullOrEmpty(quoteId))
                    {
                        msg = UtilsCommons.AjaxReturnJson("1", (String)GetGlobalResourceObject("string", "新增成功"));
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("1", (String)GetGlobalResourceObject("string", "修改成功"));
                    }
                    break;
                default:
                    msg = UtilsCommons.AjaxReturnJson("0", (String)GetGlobalResourceObject("string", "操作失败"));
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 获取表单
        /// </summary>
        /// <returns></returns>
        private EyouSoft.Model.HTourStructure.MQuote GetFormInfo()
        {
            EyouSoft.Model.HTourStructure.MQuote model = new EyouSoft.Model.HTourStructure.MQuote();
            model.AreaId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            model.AreaName = this.ddlArea.SelectedItem.Text;
            model.ArriveCity = Utils.GetFormValue(txtDiDaChengShi.UniqueID);
            model.ArriveCityFlight = Utils.GetFormValue(txtDiDaHangBan.UniqueID);
            model.LeaveCity = Utils.GetFormValue(txtLiKaiChengShi.UniqueID);
            model.LeaveCityFlight = Utils.GetFormValue(txtLiKaiHangBan.UniqueID);
            model.BuyCompanyID = SiteUserInfo.TourCompanyInfo.CompanyId;
            model.BuyCompanyName = SiteUserInfo.TourCompanyInfo.CompanyName;
            model.BuyId = "";
            //string[] timearr = Utils.GetFormValue(this.txtxunjiatime.UniqueID).Split('-');
            //string year = string.Empty;
            //string month = string.Empty;
            //string day = string.Empty;
            //if (timearr.Length > 2)
            //{
            //    year = timearr[0];
            //    month = timearr[1];
            //    day = timearr[2];
            //}
            //model.BuyTime = Convert.ToDateTime(Convert.ToDateTime(year + "-" + month + "-" + day).ToString("yyyy-MM-dd"));
            model.BuyTime = Utils.GetDateTime(Utils.GetFormValue(this.txtxunjiatime.UniqueID));
            model.CompanyId = SiteUserInfo.CompanyId;
            model.CountryId = Utils.GetInt(Utils.GetFormValue("ddlCountry"));
            model.Days = Utils.GetInt(Utils.GetFormValue(this.txt_Days.UniqueID));
            model.EndEffectTime = Utils.GetDateTime(Utils.GetFormValue(this.txtRDate.UniqueID));
            model.StartEffectTime = Utils.GetDateTime(Utils.GetFormValue(this.txtLDate.UniqueID));
            model.UpdateTime = DateTime.Now;
            model.JourneySpot = Utils.GetFormEditorValue(this.txtPlanContent.UniqueID);
            model.QuoteRemark = Utils.GetFormEditorValue(this.txtPriceRemark.UniqueID);
            model.MinAdults = Utils.GetInt(Utils.GetFormValue(this.txtminadultcount.UniqueID));
            model.MaxAdults = Utils.GetInt(Utils.GetFormValue(this.txtmaxadultcount.UniqueID));
            model.Operator = SiteUserInfo.Name;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;
            EyouSoft.Model.CrmStructure.MCrm modelCrm = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
            if (modelCrm != null)
            {
                model.SellerId = modelCrm.SellerId;
                model.SellerName = modelCrm.SellerName;
                if (modelCrm.LinkManList != null && modelCrm.LinkManList.Count > 0)
                {
                    model.Phone = modelCrm.LinkManList[0].MobilePhone;
                    model.Contact = modelCrm.LinkManList[0].Name;
                    model.Fax = modelCrm.LinkManList[0].Fax;
                }
            }
            model.OutQuoteType = EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项;
            model.QuoteCostList = GetQuoteCost(EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项);
            model.QuoteFileList = null;
            model.QuoteId = quoteId;
            model.QuoteFootList = selectFWeiCan1.GetFengWeiList();
            model.QuoteGiveList = Give1.GetDataList();
            model.QuotePlanList = GetPlanList();
            model.QuotePriceList = GetQuotePrice();
            model.QuoteSelfPayList = SelfPay1.GetDataList();
            model.QuoteShopList = GetShoplist();
            model.QuoteStatus = EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理;
            model.QuoteTipList = Tip1.GetDataList();
            model.QuoteTour = null;
            model.RouteName = Utils.GetFormValue(this.txtroutename.UniqueID);
            model.SpecificRequire = Utils.GetFormValue(txtSpecificRequire.UniqueID);

            //酒店星级要求
            model.JiuDianXingJi = (JiuDianXingJi)Utils.GetInt(this.ddlJiuDianXingJi.SelectedValue);

            model.QuoteJourneyList = new List<MQuoteJourney>();
            var i2 = this.SelectJourneySpot1.GetQuoteJourneyList();
            foreach (var item in i2)
            {
                model.QuoteJourneyList.Add(item);
            }
            var i3 = this.SelectPriceRemark1.GetQuoteJourneyList();

            foreach (var item in i3)
            {
                model.QuoteJourneyList.Add(item);
            }
            model.TravelNote = "";

            return model;
        }

        /// <summary>
        /// 获取合计价格信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected IList<MQuotePrice> GetQuotePrice()
        {

            IList<MQuotePrice> list = new List<MQuotePrice>();
            MQuotePrice modelcost = null;
            MQuotePrice modelsell = null;

            modelcost = new MQuotePrice();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            decimal trafficprice = Utils.GetDecimal(Utils.GetFormValue(hidpricetraffic.UniqueID));
            decimal pricehotel1 = Utils.GetDecimal(Utils.GetFormValue(hidpricehotel1.UniqueID));
            decimal pricehotel2 = Utils.GetDecimal(Utils.GetFormValue(hidpricehotel2.UniqueID));
            decimal pricecancost = Utils.GetDecimal(Utils.GetFormValue(hidcancost.UniqueID));
            decimal pricecanprice = Utils.GetDecimal(Utils.GetFormValue(hidcanprice.UniqueID));
            decimal sceniccost = Utils.GetDecimal(Utils.GetFormValue(hidsceniccost.UniqueID));
            decimal scenicprice = Utils.GetDecimal(Utils.GetFormValue(hidscenicprice.UniqueID));
            decimal pricezongfei = Utils.GetDecimal(Utils.GetFormValue(hidpricezongfei.UniqueID));
            decimal priceother = Utils.GetDecimal(Utils.GetFormValue(hidpriceother.UniqueID));


            EyouSoft.Model.CrmStructure.MCrm model = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(SiteUserInfo.TourCompanyInfo.CompanyId);
            if (model == null) return null;

            MComLev modelprice = new EyouSoft.BLL.ComStructure.BComLev().GetInfo(model.LevId, SiteUserInfo.CompanyId);
            decimal CustomerLvPrice = 0;
            if (modelprice != null)
                CustomerLvPrice = modelprice.FloatMoney;

            modelcost.AdultPrice = trafficprice + pricehotel1 + pricecancost + sceniccost + pricezongfei;
            modelcost.ChildPrice = 0;
            modelcost.LeadPrice = 0;
            modelcost.OtherPrice = priceother;
            modelcost.SingleRoomPrice = 0;
            list.Add(modelcost);
            list.Add(new MQuotePrice
            {
                AdultPrice = trafficprice + pricehotel2 + pricecancost + sceniccost + pricezongfei,
                ChildPrice = 0,
                CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格,
                LeadPrice = 0,
                OtherPrice = priceother,
                SingleRoomPrice = 0
            });

            modelsell = new MQuotePrice();
            modelsell.AdultPrice = trafficprice + pricehotel1 + pricecanprice + scenicprice + pricezongfei + CustomerLvPrice;
            modelsell.ChildPrice = 0;
            modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
            modelsell.LeadPrice = 0;
            modelsell.OtherPrice = 0;
            modelsell.SingleRoomPrice = 0;
            list.Add(modelsell);

            list.Add(new MQuotePrice
            {
                AdultPrice = trafficprice + pricehotel2 + pricecanprice + scenicprice + pricezongfei + CustomerLvPrice,
                ChildPrice = 0,
                CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格,
                LeadPrice = 0,
                OtherPrice = priceother,
                SingleRoomPrice = 0
            });

            return list;
        }

        /// <summary>
        /// 获取成本信息和价格信息
        /// </summary>
        /// <returns></returns>
        private IList<MQuoteCost> GetQuoteCost(EyouSoft.Model.EnumType.TourStructure.TourQuoteType type)
        {
            IList<MQuoteCost> list = new List<MQuoteCost>();
            MQuoteCost modelcost = null;
            MQuoteCost modelsell = null;

            #region  成本价格实体赋值
            modelcost = new MQuoteCost();
            decimal trafficCost = Utils.GetDecimal(Utils.GetFormValue(this.hidpricetraffic.UniqueID));
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = trafficCost;

            list.Add(modelcost);

            modelcost = new MQuoteCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = 0;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            list.Add(modelcost);

            decimal hotelCost1 = Utils.GetDecimal(Utils.GetFormValue(this.hidpricehotel1.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = hotelCost1;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            list.Add(modelcost);

            decimal hotelCost2 = Utils.GetDecimal(Utils.GetFormValue(this.hidpricehotel2.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = hotelCost2;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;

            list.Add(modelcost);

            decimal canCost = Utils.GetDecimal(Utils.GetFormValue(this.hidcancost.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = canCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            modelcost = new MQuoteCost();
            modelcost.Price = 0; ;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);


            decimal scenicCost = Utils.GetDecimal(Utils.GetFormValue(this.hidsceniccost.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = scenicCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            modelcost = new MQuoteCost();
            modelcost.Price = 0;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            modelcost = new MQuoteCost();
            modelcost.Price = 0;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;

            list.Add(modelcost);

            decimal zongfeiCost = Utils.GetDecimal(Utils.GetFormValue(this.hidpricezongfei.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = zongfeiCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            decimal otherCost = Utils.GetDecimal(Utils.GetFormValue(this.hidpriceother.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = otherCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            modelcost = new MQuoteCost();
            modelcost.Price = 0;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            list.Add(modelcost);

            #endregion
            if (type == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
            {
                #region 销售价格实体赋值
                modelsell = new MQuoteCost();
                decimal trafficPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidpricetraffic.UniqueID));
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = trafficPrice;

                list.Add(modelsell);

                modelsell = new MQuoteCost();
                modelsell.Price = 0;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);

                decimal hotelPrice1 = Utils.GetDecimal(Utils.GetFormValue(this.hidpricehotel1.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = hotelPrice1;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);

                decimal hotelPrice2 = Utils.GetDecimal(Utils.GetFormValue(this.hidpricehotel2.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = hotelPrice2;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);

                decimal canPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidcanprice.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = canPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);



                modelsell = new MQuoteCost();
                modelsell.Price = 0;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);


                decimal scenicPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidscenicprice.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = scenicPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);


                modelsell = new MQuoteCost();
                modelsell.Price = 0;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);


                modelsell = new MQuoteCost();
                modelsell.Price = 0;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);

                decimal zongfeiPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidpricezongfei.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = zongfeiPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);

                decimal otherPrice = Utils.GetDecimal(Utils.GetFormValue(this.hidpriceother.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = otherPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);


                modelsell = new MQuoteCost();
                modelsell.Price = 0;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                list.Add(modelsell);
                #endregion
            }

            return list;
        }

        /// <summary>
        /// 绑定线路区域
        /// </summary>
        /// <param name="areaid"></param>
        /// <param name="lgtype"></param>
        private void BindDpt(int areaid, int lgtype)
        {
            //绑定线路区域
            EyouSoft.BLL.ComStructure.BComArea _bllComArea = new EyouSoft.BLL.ComStructure.BComArea();
            IList<EyouSoft.Model.ComStructure.MComArea> listArea = _bllComArea.GetAreaByCID(SiteUserInfo.CompanyId).Where(m => m.LngType == (EyouSoft.Model.EnumType.SysStructure.LngType)lgtype).ToList();
            this.ddlArea.DataSource = listArea;
            this.ddlArea.DataValueField = "MasterId";
            this.ddlArea.DataTextField = "AreaName";
            this.ddlArea.DataBind();
            this.ddlArea.Items.Insert(0, new ListItem((String)GetGlobalResourceObject("string", "请选择"), ""));
            if (areaid > 0)
            {
                ddlArea.SelectedIndex = this.ddlArea.Items.IndexOf(this.ddlArea.Items.FindByValue(areaid.ToString()));
            }
        }

        /// <summary>
        /// 绑定酒店星级要求
        /// </summary>
        /// <param name="jiudianxingji">默认星级</param>
        private void BindJiuDianXingJi(Model.EnumType.GysStructure.JiuDianXingJi jiudianxingji)
        {
            //绑定酒店星级要求
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.None).ToString(), Text = Model.EnumType.GysStructure.JiuDianXingJi.None.ToString() });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.MOTEL或两星级).ToString(), Text = @"★★" });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.三星级).ToString(), Text = @"★★★" });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.四星级).ToString(), Text = @"★★★★" });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.四星半).ToString(), Text = @"★★★★☆" });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.五星级).ToString(), Text = @"★★★★★" });
            this.ddlJiuDianXingJi.Items.Add(new ListItem() { Value = ((int)Model.EnumType.GysStructure.JiuDianXingJi.超五星级或六星级).ToString(), Text = @"★★★★★★" });

            //默认星级
            this.ddlJiuDianXingJi.SelectedValue = ((int)jiudianxingji).ToString();
        }

        /// <summary>
        /// 获取所有购物点
        /// </summary>
        /// <param name="objshop">已选择购物点</param>
        /// <param name="citysid">所有城市</param>
        /// <returns></returns>
        private string getshop(IList<MQuoteShop> shop, string citysid)
        {
            IList<string> listshop = new List<string>();

            EyouSoft.BLL.HGysStructure.BGys bll = new EyouSoft.BLL.HGysStructure.BGys();
            //行程中包含城市下所以的购物点
            IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> allshop = null;
            if (citysid != "0" && citysid.Length > 0)
            {
                allshop = bll.GetXuanYongGouWuDians(SiteUserInfo.CompanyId, Utils.GetIntArray(citysid, ","));
            }
            if (shop != null)
            {
                foreach (var model in shop)
                {
                    if (model.ShopId != "")
                        listshop.Add(model.ShopId.ToString());
                }
            }
            int count = 0;
            if (allshop != null)
            {
                string html = string.Empty;
                string val = string.Empty;
                string text = string.Empty;
                foreach (var item in allshop)
                {
                    if (listshop != null)
                    {
                        if (listshop.Contains(item.GysId.ToString()))
                        {
                            val += item.GysId + ",";
                            text += item.GysName + ",";
                            html += "<span id='" + item.GysId + "' class='planshopspan'><input type='checkbox' checked='checked' onclick='Journey.SetShopValue(this)' name='ckgouwuid' data-name='" + item.GysName + "' value='" + item.GysId + "' />" + item.GysName + "</span>";

                        }
                        else
                            html += "<span id='" + item.GysId + "' class='planshopspan'><input type='checkbox' onclick='Journey.SetShopValue(this)' name='ckgouwuid' data-name='" + item.GysName + "' value='" + item.GysId + "' />" + item.GysName + "</span>";
                    }
                    if (count % 6 == 0 && count > 0)
                    {
                        html += ("<br />");
                    }
                    count++;
                }
                if (val.Length >= 1 && text.Length >= 1)
                {
                    val = val.Substring(0, val.Length - 1);
                    text = text.Substring(0, text.Length - 1);
                }
                return "<input type='hidden' name='ckshop' class='shopid' value='" + val + "' /><input type='hidden' class='shopname' name='shopname' value='" + text + "' />" + html;
            }
            return "<input type='hidden' class='shopid' name='ckshop' value='' /><input type='hidden' class='shopname' name='shopname' value='' />";
        }


        /// <summary>
        /// 获取购物店信息
        /// </summary>
        /// <returns></returns>
        private IList<MQuoteShop> GetShoplist()
        {
            IList<MQuoteShop> list = new List<MQuoteShop>();
            string[] shopid = Utils.GetFormValue("ckshop").Split(',');
            string[] shopname = Utils.GetFormValue("shopname").Split(',');

            if (shopid.Length > 0 && shopname.Length == shopid.Length)
            {
                for (int i = 0; i < shopid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(shopid[i]))
                    {
                        list.Add(new MQuoteShop { ShopId = shopid[i], ShopName = shopname[i] });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得计划行程集合
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuotePlan> GetPlanList()
        {
            IList<EyouSoft.Model.HTourStructure.MQuotePlan> list = new List<EyouSoft.Model.HTourStructure.MQuotePlan>();
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

            //购物供应商编号
            string[] shopids = Utils.GetFormValues("hidshopid");
            //购物供应商名称
            string[] shopnames = Utils.GetFormValues("hidshopname");

            if (cityid.Length > 0)
            {
                for (int i = 0; i < cityid.Length; i++)
                {
                    if (cityid[i] != "" && cityid[i] != "0")
                    {
                        EyouSoft.Model.HTourStructure.MQuotePlan model = new EyouSoft.Model.HTourStructure.MQuotePlan();
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
                        if (hotelid2[i].ToString() != "" && hotelname2[i].ToString() != "")
                        {
                            model.HotelId2 = hotelid2[i];
                            model.HotelName2 = hotelname2[i];
                            model.HotelPrice2 = Utils.GetDecimal(hotelprice2[i].ToString());
                        }
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
                        model.Traffic = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictype[i], PlanProject.火车);
                        model.TrafficPrice = Utils.GetDecimal(trafficprice[i].ToString());


                        IList<MQuoteShop> shopList = new List<MQuoteShop>();
                        if (shopids.Length > 0)
                        {
                            string[] shopid = shopids[i].Split(',');
                            string[] shopname = shopnames[i].Split(',');
                            for (int k = 0; k < shopid.Length; k++)
                            {
                                if (!string.IsNullOrEmpty(shopid[k]))
                                {
                                    MQuoteShop shopmodel = new MQuoteShop();
                                    shopmodel.ShopId = shopid[k];
                                    shopmodel.ShopName = shopname[k];
                                    shopList.Add(shopmodel);
                                }
                            }
                        }

                        model.QuotePlanShopList = shopList;



                        IList<MQuotePlanCity> citylist = new List<MQuotePlanCity>();

                        if (cityid.Length > 0)
                        {
                            string[] cityids = cityid[i].Split(',');
                            string[] citynames = cityname[i].Split(',');
                            string[] traffictypes = traffictype[i].Split(',');
                            string[] trafficprices = trafficprice[i].Split(',');
                            if (cityids.Length > 0)
                            {
                                for (int o = 0; o < cityids.Length; o++)
                                {
                                    MQuotePlanCity citymodel = new MQuotePlanCity();
                                    citymodel.CityId = Utils.GetInt(cityids[o]);
                                    citymodel.CityName = citynames[o];
                                    citymodel.JiaoTong = Utils.GetEnumValue<EyouSoft.Model.EnumType.PlanStructure.PlanProject>(traffictypes[o], PlanProject.火车);
                                    citymodel.JiaoTongJiaGe = Utils.GetDecimal(trafficprices[o].ToString());
                                    citylist.Add(citymodel);
                                }
                            }
                            else
                            {
                                MQuotePlanCity citymodel = new MQuotePlanCity();
                                citymodel.CityId = Utils.GetInt(cityid[i]);
                                citymodel.CityName = cityname[i];
                                citylist.Add(citymodel);
                            }
                        }

                        model.QuotePlanCityList = citylist;



                        IList<EyouSoft.Model.HTourStructure.MQuotePlanSpot> spotlist = new List<EyouSoft.Model.HTourStructure.MQuotePlanSpot>();
                        if (spotId.Length > 0)
                        {
                            string[] spotIdArray = spotId[i].Split(',');
                            string[] spotNameArray = sportName[i].Split(',');
                            string[] spotpricejs = spotpricejss[i].Split(',');
                            string[] spotpriceth = spotpriceths[i].Split(',');
                            if (spotIdArray.Length > 0 && spotId[i] != "")
                            {
                                for (int j = 0; j < spotIdArray.Length; j++)
                                {
                                    EyouSoft.Model.HTourStructure.MQuotePlanSpot spotModel = new EyouSoft.Model.HTourStructure.MQuotePlanSpot();
                                    spotModel.SpotId = spotIdArray[j];
                                    spotModel.SpotName = System.Web.HttpContext.Current.Server.UrlDecode(spotNameArray[j]);
                                    spotModel.Price = Utils.GetDecimal(spotpriceth[j]);
                                    spotModel.SettlementPrice = Utils.GetDecimal(spotpricejs[j]);
                                    spotlist.Add(spotModel);
                                }
                            }
                            else
                            {
                                EyouSoft.Model.HTourStructure.MQuotePlanSpot spotModel = new EyouSoft.Model.HTourStructure.MQuotePlanSpot();
                                spotModel.SpotId = spotId[i];
                                spotModel.SpotName = sportName[i];
                                spotModel.Price = Utils.GetDecimal(spotpriceths[i]);
                                spotModel.SettlementPrice = Utils.GetDecimal(spotpricejss[i]);
                                spotlist.Add(spotModel);
                            }
                        }
                        model.QuotePlanSpotList = spotlist;

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
