using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.HTourStructure;
using System.Collections;

namespace EyouSoft.Web.Quote
{
    using EyouSoft.Model.EnumType.GysStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class BaoJiaEdit : EyouSoft.Common.Page.BackPage
    {
        protected int type = 0;
        protected string act = "add";
        protected int sl = 0;
        //购物点
        protected string shopStr = string.Empty;
        //类型(整团/分项)
        protected string tourMode = "1";
        bool Privs_Insert = false;
        bool Privs_Update = false;
        bool Privs_Success = false;
        bool Privs_Cencer = false;


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
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            act = Utils.GetQueryStringValue("act");
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            string quoteid = Utils.GetQueryStringValue("id");
            string buyid = Utils.GetQueryStringValue("buyid");

            #region 处理AJAX请求
            string doType = Utils.GetQueryStringValue("doType");
            if (doType != "")
            {
                Response.Clear();
                switch (doType)
                {
                    case "save":
                        InitPrivs();
                        Response.Write(PageSave());
                        break;
                    case "check":
                        Response.Write(CheckXunJiaID(buyid, quoteid));
                        break;
                    case "GetRDate":
                        Response.Write(GetRDate());
                        break;
                }
                Response.End();
            }
            #endregion
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(quoteid))
                {
                    PageInit(quoteid);
                    if (act == "copy")
                    {
                        this.phdCanel.Visible = false;
                        this.phdNewAdd.Visible = false;
                        this.phJianPrint.Visible = false;
                        this.phMingPrint.Visible = false;
                        this.phdSave.Visible = true;
                        this.phdQuote.Visible = true;
                        //this.txtBuyId.Text = "";
                    }
                }
                else
                {
                    txtBuyTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.SellsSelect1.SellsID = SiteUserInfo.UserId;
                    this.SellsSelect1.SellsName = SiteUserInfo.Name;

                    this.phdCanel.Visible = false;
                    this.phdNewAdd.Visible = false;
                    this.phJianPrint.Visible = false;
                    this.phMingPrint.Visible = false;
                    BindList(EyouSoft.Model.EnumType.TourStructure.TourMode.整团, -1, -1, Model.EnumType.GysStructure.JiuDianXingJi.None, TourType.团队产品);
                }
            }

            var deptInfo = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(SiteUserInfo.DeptId, SiteUserInfo.CompanyId);
            if (deptInfo != null)
            {
                SellsSelect2.SellsID = deptInfo.DepartPlanId;
                var jdinfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(deptInfo.DepartPlanId, SiteUserInfo.CompanyId);
                if (jdinfo != null)
                {
                    SellsSelect2.SellsName = jdinfo.ContactName;
                }
            }
        }

        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_修改);
            Privs_Success = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_报价成功);
            Privs_Cencer = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.整团报价_报价中心_取消);
        }

        private void PageInit(string quoteid)
        {
            this.PhDinnerPrice.Visible = false;
            this.Journey1.CompanyID = SiteUserInfo.CompanyId;
            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = new EyouSoft.Model.HTourStructure.MQuote();
            model = bll.GetQuoteModel(quoteid);
            //绑定报价次数列表
            IList<EyouSoft.Model.HTourStructure.MTourQuoteNo> list = bll.GetTourQuoteNo(quoteid);
            if (list != null && list.Count > 0)
            {
                this.rptChildPrice.DataSource = list;
                this.rptChildPrice.DataBind();
            }

            if (model != null)
            {

                if (model.QuoteStatus == EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功)
                {
                    this.phdCanel.Visible = false;
                    this.phdNewAdd.Visible = false;
                    this.phdQuote.Visible = false;
                    this.phdSave.Visible = false;
                }
                BindList(model.TourMode, model.AreaId, model.CountryId, model.JiuDianXingJi,model.TourType);
                //if (act == "update")
                //{
                //    this.ddlTourMode.Attributes.Add("disabled", "true");
                //    this.ddlTourMode.Attributes.Add("background-color", "#dadada");
                //}
                this.txt_Days.Text = model.Days.ToString();

                this.CustomerUnitSelect1.CustomerUnitId = model.BuyCompanyID;
                this.CustomerUnitSelect1.CustomerUnitName = model.BuyCompanyName;

                EyouSoft.Model.CrmStructure.MCrm modelcrm = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(model.BuyCompanyID);
                if (modelcrm == null) return;

                MComLev modelprice = new EyouSoft.BLL.ComStructure.BComLev().GetInfo(modelcrm.LevId, SiteUserInfo.CompanyId);
                decimal CustomerLvPrice = 0;
                if (modelprice != null)
                    CustomerLvPrice = modelprice.FloatMoney;
                this.LvPrice.Value = CustomerLvPrice.ToString();

                this.txtArriveCity.Text = model.ArriveCity;
                this.txtArriveCityFlight.Text = model.ArriveCityFlight;
                //this.txtBuyId.Text = model.BuyId;
                this.txtBuyTime.Text = model.BuyTime.ToString("yyyy-MM-dd");
                this.txtContact.Text = model.Contact;
                this.txtEndEffectTime.Text = model.EndEffectTime.ToString("yyyy-MM-dd");
                this.txtFax.Text = model.Fax;
                this.txtJourney.Text = model.TravelNote;
                this.txtLeaveCity.Text = model.LeaveCity;
                this.txtLeaveCityFlight.Text = model.LeaveCityFlight;
                this.txtMaxAdults.Text = model.MaxAdults.ToString();
                this.hidTourModeValue.Value = ((int)model.TourMode).ToString();
                this.txtMinAdults.Text = model.MinAdults.ToString();
                this.txtPhone.Text = model.Phone;
                this.txtPlanContent.Text = model.JourneySpot;
                this.txtPriceRemark.Text = model.QuoteRemark;
                this.txtRouteName.Text = model.RouteName;
                this.txtSpecificRequire.Text = model.SpecificRequire;
                this.txtStartEffectTime.Text = model.StartEffectTime.ToString("yyyy-MM-dd");
                this.hidQuoteId.Value = model.QuoteId;
                this.SellsSelect1.SellsID = model.SellerId;
                this.SellsSelect1.SellsName = model.SellerName;
                this.hidparentid.Value = model.ParentId;
                this.hidItemType.Value = ((int)model.OutQuoteType).ToString();
                this.tourMode = ((int)model.OutQuoteType).ToString();
                this.SelectJourney1.SetQuoteJourneyList = model.QuoteJourneyList;
                this.SelectJourneySpot1.SetQuoteJourneyList = model.QuoteJourneyList;
                this.SelectPriceRemark1.SetQuoteJourneyList = model.QuoteJourneyList;

                //行程
                if (model.QuotePlanList != null && model.QuotePlanList.Count > 0)
                {
                    this.Journey1.SetPlanList = model.QuotePlanList;
                }

                //合计价格
                if (model.QuotePriceList != null && model.QuotePriceList.Count > 0)
                {
                    if (model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格).ToList().Count > 0)
                    {
                        this.phItem.Visible = false;
                        this.rptItem.DataSource = model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格).ToList();
                        this.rptItem.DataBind();
                    }
                    if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                    {
                        if (model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList().Count > 0)
                        {
                            this.PhPrice.Visible = false;
                            this.rptPrice.DataSource = model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList();
                            this.rptPrice.DataBind();
                        }
                    }
                    if (model.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                    {
                        if (model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList().Count > 0)
                        {
                            this.Phzengtuan.Visible = false;
                            this.rptzengtuan.DataSource = model.QuotePriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList();
                            this.rptzengtuan.DataBind();
                        }
                    }
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
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.保险:
                                    txt_Item_Insure.Text = item.Price.ToString("F2");
                                    txt_Item_InsureRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.餐:
                                    txt_Item_Dinner.Text = item.Price.ToString("F2");
                                    txt_Item_DinnerRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机:
                                    txt_Item_BTraffic.Text = item.Price.ToString("F2");
                                    txt_Item_BTrafficRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.火车:
                                    txt_Item_BTrain.Text = item.Price.ToString("F2");
                                    txt_Item_BTrainRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车:
                                    txt_Item_BBus.Text = item.Price.ToString("F2");
                                    txt_Item_BBusRemark.Text = item.Remark;
                                    ddlItemCarTicketUnit.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船:
                                    txt_Item_BShip.Text = item.Price.ToString("F2");
                                    txt_Item_BShipRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.导服:
                                    txt_Item_Guide.Text = item.Price.ToString("F2");
                                    txt_Item_GuideRemark.Text = item.Remark;
                                    ddlItemGuide.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房1:
                                    txt_Item_Room1.Text = item.Price.ToString("F2");
                                    txt_Item_RoomRemark1.Text = item.Remark;
                                    ddlItemRoomUnit1.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房2:
                                    this.txt_Item_Room2.Text = item.Price.ToString("F2");
                                    this.txt_Item_RoomRemark2.Text = item.Remark;
                                    ddlItemRoomUnit2.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.景点:
                                    txt_Item_Scenic.Text = item.Price.ToString("F2");
                                    txt_Item_ScenicRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.其他:
                                    txt_Item_Other.Text = item.Price.ToString("F2");
                                    txt_Item_OtherRemark.Text = item.Remark;
                                    ddlItemQiTaUnit.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一:
                                    txt_Item_FreeOne.Text = item.Price.ToString("F2");
                                    txt_Item_FreeOneRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通:
                                    txt_Item_STraffic.Text = item.Price.ToString("F2");
                                    txt_Item_STrafficRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.用车:
                                    txt_Item_Car.Text = item.Price.ToString("F2");
                                    txt_Item_CarRemark.Text = item.Remark;
                                    ddlItemCarUnit.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.综费:
                                    txt_Item_Sum.Text = item.Price.ToString("F2");
                                    txt_Item_SumRemark.Text = item.Remark;
                                    break;
                            }
                        }

                        if (item.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格)
                        {
                            switch (item.Pricetype)
                            {
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.保险:
                                    txt_Price_Insure.Text = item.Price.ToString("F2");
                                    txt_Price_InsureRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.餐:
                                    txt_Price_Dinner.Text = item.Price.ToString("F2");
                                    txt_Price_DinnerRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机:
                                    txt_Price_BTraffic.Text = item.Price.ToString("F2");
                                    txt_Price_BTrafficRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.火车:
                                    txt_Price_BTrain.Text = item.Price.ToString("F2");
                                    txt_Price_BTrainRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车:
                                    txt_Price_BBus.Text = item.Price.ToString("F2");
                                    txt_Price_BBusRemark.Text = item.Remark;
                                    ddlPriceCarTicket.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船:
                                    txt_Price_BShip.Text = item.Price.ToString("F2");
                                    txt_Price_BShipRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.导服:
                                    txt_Price_Guide.Text = item.Price.ToString("F2");
                                    txt_Price_GuideRemark.Text = item.Remark;
                                    ddlPriceGuide.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房1:
                                    txt_Price_Room1.Text = item.Price.ToString("F2");
                                    txt_Price_RoomRemark1.Text = item.Remark;
                                    ddlPriceRoom1.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.房2:
                                    this.txt_Price_Room2.Text = item.Price.ToString("F2");
                                    this.txt_Price_RoomRemark2.Text = item.Remark;
                                    ddlPriceRoom2.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.景点:
                                    txt_Price_Scenic.Text = item.Price.ToString("F2");
                                    txt_Price_ScenicRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.其他:
                                    txt_Price_Other.Text = item.Price.ToString("F2");
                                    txt_Price_OtherRemark.Text = item.Remark;
                                    ddlPriceQiTa.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一:
                                    txt_Price_FreeOne.Text = item.Price.ToString("F2");
                                    txt_Price_FreeOneRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通:
                                    txt_Price_STraffic.Text = item.Price.ToString("F2");
                                    txt_Price_STrafficRemark.Text = item.Remark;
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.用车:
                                    txt_Price_Car.Text = item.Price.ToString("F2");
                                    txt_Price_CarRemark.Text = item.Remark;
                                    ddlPriceCar.SelectedValue = ((int)item.PriceUnit).ToString();
                                    break;
                                case EyouSoft.Model.EnumType.TourStructure.Pricetype.综费:
                                    txt_Price_Sum.Text = item.Price.ToString("F2");
                                    txt_Price_SumRemark.Text = item.Remark;
                                    break;
                            }
                        }
                    }
                    #endregion
                }
                //风味餐
                if (model.QuoteFootList != null && model.QuoteFootList.Count > 0)
                {
                    this.selectFengWeiCan1.SetFengWeiList = model.QuoteFootList;
                }
                //赠送
                if (model.QuoteGiveList != null && model.QuoteGiveList.Count > 0)
                {
                    this.Give1.SetQuoteGiveList = model.QuoteGiveList;
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
                //附件
                if (model.QuoteFileList != null && model.QuoteFileList.Count > 0)
                {
                    this.rptfile.DataSource = model.QuoteFileList.Where(m => m.FileModel == EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.附件).ToList();
                    this.rptfile.DataBind();
                    this.rptorder.DataSource = model.QuoteFileList.Where(n => n.FileModel == EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.外语报价单).ToList();
                    this.rptorder.DataBind();
                }
            }

        }

        /// <summary>
        /// 绑定下拉菜单
        /// </summary>
        private void BindList(EyouSoft.Model.EnumType.TourStructure.TourMode tourmode, int areaid, int countryid, EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi jiudianxingji,TourType tourtype)
        {
            //绑定团型
            var tourMode = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourMode));
            this.ddlTourMode.DataSource = tourMode;
            this.ddlTourMode.DataValueField = "Value";
            this.ddlTourMode.DataTextField = "Text";
            this.ddlTourMode.DataBind();
            ddlTourMode.SelectedIndex = this.ddlTourMode.Items.IndexOf(this.ddlTourMode.Items.FindByValue(((int)tourmode).ToString()));
            this.ddlTourMode.SelectedItem.Text = tourmode.ToString();
            this.ddlTourMode.SelectedItem.Value = ((int)tourmode).ToString();

            //绑定线路区域
            //EyouSoft.BLL.ComStructure.BComArea _bllComArea = new EyouSoft.BLL.ComStructure.BComArea();
            //IList<EyouSoft.Model.ComStructure.MComArea> listArea = _bllComArea.GetAreaByCID(SiteUserInfo.CompanyId).Where(m => m.LngType == EyouSoft.Model.EnumType.SysStructure.LngType.中文).ToList();
            //this.ddlArea.DataSource = listArea;
            // this.ddlArea.DataValueField = "AreaId";
            //this.ddlArea.DataTextField = "AreaName";
            //this.ddlArea.DataBind();
            //this.ddlArea.Items.Insert(0, new ListItem("-请选择-", ""));
            //if (areaid > 0)
            //{
            //    ddlArea.SelectedIndex = this.ddlArea.Items.IndexOf(this.ddlArea.Items.FindByValue(areaid.ToString()));
            //}

            //绑定国家
            EyouSoft.BLL.SysStructure.BGeography _bllGeography = new EyouSoft.BLL.SysStructure.BGeography();
            IList<EyouSoft.Model.SysStructure.MSysCountry> listCountry = _bllGeography.GetCountrys(SiteUserInfo.CompanyId);
            this.ddlCountry.DataSource = listCountry;
            this.ddlCountry.DataValueField = "CountryId";
            this.ddlCountry.DataTextField = "Name";
            this.ddlCountry.DataBind();
            this.ddlCountry.Items.Insert(0, new ListItem("-请选择-", ""));
            if (countryid > 0)
            {
                ddlCountry.SelectedIndex = this.ddlCountry.Items.IndexOf(this.ddlCountry.Items.FindByValue(countryid.ToString()));
            }

            //绑定酒店星级
            this.ddlJiuDianXingJi.DataSource =
                EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi));
            this.ddlJiuDianXingJi.DataValueField = "value";
            this.ddlJiuDianXingJi.DataTextField = "text";
            this.ddlJiuDianXingJi.DataBind();
            this.ddlJiuDianXingJi.SelectedValue = ((int)jiudianxingji).ToString();
            //绑定报价类型
            this.ddlTourType.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourType)).Where(m => m.Value == ((int)TourType.自由行).ToString() || m.Value == ((int)TourType.团队产品).ToString()).ToList();
            this.ddlTourType.DataValueField = "value";
            this.ddlTourType.DataTextField = "text";
            this.ddlTourType.DataBind();
            this.ddlTourType.SelectedValue = ((int)tourtype).ToString();
        }


        /// <summary>
        /// 获取值
        /// </summary>
        private string PageSave()
        {
            string msg = UtilsCommons.AjaxReturnJson("0", "操作失败!");
            EyouSoft.Model.EnumType.TourStructure.TourMode mode = (EyouSoft.Model.EnumType.TourStructure.TourMode)Utils.GetInt(Utils.GetFormValue(this.hidTourModeValue.UniqueID));
            string BuyCompanyID = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHBH);
            string BuyCompanyName = Utils.GetFormValue(this.CustomerUnitSelect1.ClientNameKHMC);
            string Contact = Utils.GetFormValue(this.txtContact.UniqueID);
            string Phone = Utils.GetFormValue(this.txtPhone.UniqueID);
            string Fax = Utils.GetFormValue(this.txtFax.UniqueID);
            DateTime BuyTime = Utils.GetDateTime(Utils.GetFormValue(this.txtBuyTime.UniqueID));
            //string BuyId = Utils.GetFormValue(this.txtBuyId.UniqueID);
            //int AreaId = Utils.GetInt(Utils.GetFormValue(this.ddlArea.UniqueID));
            string RouteName = Utils.GetFormValue(this.txtRouteName.UniqueID);
            int Days = Utils.GetInt(Utils.GetFormValue(this.txt_Days.UniqueID));
            string CountryId = Utils.GetFormValue(this.ddlCountry.UniqueID);
            string StartEffectTime = Utils.GetFormValue(this.txtStartEffectTime.UniqueID);
            string EndEffectTime = Utils.GetFormValue(this.txtEndEffectTime.UniqueID);
            string ArriveCity = Utils.GetFormValue(this.txtArriveCity.UniqueID);
            string ArriveCityFlight = Utils.GetFormValue(this.txtArriveCityFlight.UniqueID);
            string LeaveCity = Utils.GetFormValue(this.txtLeaveCity.UniqueID);
            string LeaveCityFlight = Utils.GetFormValue(this.txtLeaveCityFlight.UniqueID);
            string SellerId = Utils.GetFormValue(this.SellsSelect1.SellsIDClient);
            string SellerName = Utils.GetFormValue(this.SellsSelect1.SellsNameClient);
            int MaxAdults = Utils.GetInt(Utils.GetFormValue(this.txtMaxAdults.UniqueID));
            int MinAdults = Utils.GetInt(Utils.GetFormValue(this.txtMinAdults.UniqueID));
            string JourneySpot = Utils.GetFormEditorValue(this.txtPlanContent.UniqueID);
            string parentid = Utils.GetFormValue(this.hidparentid.UniqueID);
            string quoteRemark = Utils.GetFormEditorValue(this.txtPriceRemark.UniqueID);

            //自费项目
            IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> selfPayList = this.SelfPay1.GetDataList();
            //赠送
            IList<EyouSoft.Model.HTourStructure.MQuoteGive> giveList = this.Give1.GetDataList();
            //小费
            IList<EyouSoft.Model.HTourStructure.MQuoteTip> tipList = this.Tip1.GetDataList();
            //个性服务要求
            string SpecificRequire = Utils.GetFormValue(this.txtSpecificRequire.UniqueID);
            //行程备注
            string TravelNote = Utils.GetFormEditorValue(this.txtJourney.UniqueID);

            //--取消原因--------
            string CancelReason = Utils.GetFormValue(this.txtCanelRemark.UniqueID);

            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = new EyouSoft.Model.HTourStructure.MQuote();

            model.ArriveCity = ArriveCity;
            model.ArriveCityFlight = ArriveCityFlight;
            model.BuyCompanyID = BuyCompanyID;
            model.BuyCompanyName = BuyCompanyName;

            model.BuyTime = BuyTime;
            model.CancelReason = CancelReason;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Contact = Contact;
            model.CountryId = Utils.GetInt(CountryId);
            model.Days = Days;
            model.EndEffectTime = Utils.GetDateTime(EndEffectTime);
            model.QuoteRemark = quoteRemark;
            model.Fax = Fax;
            model.JourneySpot = JourneySpot;
            model.LeaveCity = LeaveCity;
            model.LeaveCityFlight = LeaveCityFlight;
            model.MaxAdults = MaxAdults;
            model.MinAdults = MinAdults;
            model.Operator = SiteUserInfo.Name;
            model.OperatorDeptId = SiteUserInfo.DeptId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Phone = Phone;
            model.QuotePlanList = UtilsCommons.GetPlanList();
            model.QuoteGiveList = giveList;
            model.QuoteSelfPayList = selfPayList;
            model.QuoteTipList = tipList;
            model.QuoteJourneyList = this.SelectJourney1.GetQuoteJourneyList();

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



            model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Utils.GetInt(Utils.GetFormValue(this.hidItemType.UniqueID));


            //文件上传
            string[] fileUpload = Utils.GetFormValues(this.UploadControl2.ClientHideID);
            string[] oldfileUpload = Utils.GetFormValues("hidefile");
            IList<MQuoteFile> filelist = new List<MQuoteFile>();
            #region 文件上传(附件)
            if (fileUpload.Length > 0)
            {
                for (int i = 0; i < fileUpload.Length; i++)
                {
                    if (fileUpload[i].Trim() != "")
                    {
                        if (fileUpload[i].Split('|').Length > 1)
                        {
                            MQuoteFile filemodel = new MQuoteFile();
                            filemodel.FileModel = EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.附件;
                            filemodel.FileName = fileUpload[i].Split('|')[0];
                            filemodel.FilePath = fileUpload[i].Split('|')[1];
                            filelist.Add(filemodel);
                        }
                    }
                }
            }
            if (oldfileUpload.Length > 0)
            {
                for (int i = 0; i < oldfileUpload.Length; i++)
                {
                    if (oldfileUpload[i].Trim() != "")
                    {
                        MQuoteFile filemodel = new MQuoteFile();
                        filemodel.FileModel = EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.附件;
                        filemodel.FileName = oldfileUpload[i].Split('|')[0];
                        filemodel.FilePath = oldfileUpload[i].Split('|')[1];
                        filelist.Add(filemodel);
                    }
                }
            }
            #endregion

            #region 外语报价单上传
            string[] orderUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldorderUpload = Utils.GetFormValues("hideorder");

            if (orderUpload.Length > 0)
            {
                for (int i = 0; i < orderUpload.Length; i++)
                {
                    if (orderUpload[i].Trim() != "")
                    {
                        if (orderUpload[i].Split('|').Length > 1)
                        {
                            MQuoteFile filemodel = new MQuoteFile();
                            filemodel.FileModel = EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.外语报价单;
                            filemodel.FileName = orderUpload[i].Split('|')[0];
                            filemodel.FilePath = orderUpload[i].Split('|')[1];
                            filelist.Add(filemodel);
                        }
                    }
                }
            }
            if (oldorderUpload.Length > 0)
            {
                for (int i = 0; i < oldorderUpload.Length; i++)
                {
                    if (oldorderUpload[i].Trim() != "")
                    {
                        MQuoteFile filemodel = new MQuoteFile();
                        filemodel.FileModel = EyouSoft.Model.EnumType.TourStructure.QuoteFileModel.外语报价单;
                        filemodel.FileName = oldorderUpload[i].Split('|')[0];
                        filemodel.FilePath = oldorderUpload[i].Split('|')[1];
                        filelist.Add(filemodel);
                    }
                }
            }
            #endregion
            model.QuoteFileList = filelist;

            model.QuoteFootList = selectFengWeiCan1.GetFengWeiList();
            model.QuoteStatus = EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理;
            //model.QuoteShopList = GetShoplist();
            EyouSoft.Model.ComStructure.MComUser sellsModel = new EyouSoft.BLL.ComStructure.BComUser().GetModel(SellerId, SiteUserInfo.CompanyId);
            if (sellsModel != null)
            {
                model.SellerDeptId = sellsModel.DeptId;
            }

            model.TourMode = mode;
            model.RouteName = RouteName;
            model.SellerId = SellerId;
            model.SellerName = SellerName;
            model.SpecificRequire = SpecificRequire;
            model.StartEffectTime = Utils.GetDateTime(StartEffectTime);

            model.TravelNote = TravelNote;
            model.UpdateTime = DateTime.Now;
            model.QuoteCostList = GetQuoteCost(model.OutQuoteType);
            model.QuotePriceList = GetQuotePrice(model.OutQuoteType);

            //酒店星级要求
            model.JiuDianXingJi = (JiuDianXingJi)Utils.GetInt(this.ddlJiuDianXingJi.SelectedValue);
            //报价类型
            model.TourType = (TourType)Utils.GetInt(this.ddlTourType.SelectedValue);
            //1=保存,2=报价超限，3=报价未超，4=保存新报价
            string saveType = Utils.GetQueryStringValue("saveType");
            string act = Utils.GetQueryStringValue("act");
            string qid = Utils.GetQueryStringValue("id");

            int result = 0;
            //新增，修改，复制
            if (saveType == "1")
            {

                if (act == "add" || act == "copy")
                {
                    if (!Privs_Insert) return UtilsCommons.AjaxReturnJson("0", "没有新增权限!");
                    if (bll.isExist(model.BuyId, model.QuoteId))
                    {
                        return UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
                    }
                    model.ParentId = "0";
                    result = bll.AddQuote(model);
                    if (result == 1)
                        msg = UtilsCommons.AjaxReturnJson("1", "新增报价成功!");
                }
                if (act == "update")
                {
                    if (!Privs_Update) return UtilsCommons.AjaxReturnJson("0", "没有修改权限!");
                    model.QuoteId = qid;
                    if (bll.isExist(model.BuyId, model.QuoteId))
                    {
                        return UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
                    }
                    model.UpdateTime = DateTime.Now;
                    result = bll.UpdateQuote(model);
                    if (result == 1)
                        msg = UtilsCommons.AjaxReturnJson("1", "修改成功!");
                }
            }
            //报价成功
            if (saveType == "3")
            {
                if (!Privs_Success) return UtilsCommons.AjaxReturnJson("0", "没有报价成功的权限!");
                model.QuoteTour = GetTourInfo();
                model.QuoteStatus = EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功;
                //报价成功重写抵达城市/航班和离开城市/航班信息
                model.ArriveCity = Utils.GetFormValue(this.txtTourArriveCity.UniqueID);
                model.ArriveCityFlight = Utils.GetFormValue(this.txtTourArriveCityFlight.UniqueID);
                model.LeaveCity = Utils.GetFormValue(this.txtTourLeaveCity.UniqueID);
                model.LeaveCityFlight = Utils.GetFormValue(this.txTourLeaveCityFlight.UniqueID);
                if (act == "add" || act == "copy")
                {
                    if (bll.isExist(model.BuyId, model.QuoteId))
                    {
                        return UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
                    }
                    model.ParentId = "0";
                    result = bll.AddQuote(model);
                }
                if (act == "update")
                {
                    model.QuoteId = qid;
                    if (bll.isExist(model.BuyId, model.QuoteId))
                    {
                        return UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
                    }
                    result = bll.UpdateQuote(model);
                }
                if (result == 2)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "报价成功!");
                }
            }
            //另存为报价
            if (saveType == "4")
            {
                if (!Privs_Insert) return UtilsCommons.AjaxReturnJson("0", "没有新增权限!");
                if (bll.isExist(model.BuyId, model.QuoteId))
                {
                    return UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
                }
                model.ParentId = Utils.GetFormValue(this.hidQuoteId.UniqueID);
                result = bll.AddQuote(model);
                if (result == 1)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "保存新报价成功!");
                }

            }
            //取消报价
            if (saveType == "5")
            {
                if (!Privs_Cencer) return UtilsCommons.AjaxReturnJson("0", "没有取消的权限!");
                model.QuoteId = qid;
                model.QuoteStatus = EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价;
                result = bll.UpdateQuote(model);
                if (result == 1)
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "取消成功!");
                }

            }
            return msg;
        }


        private MQuoteTour GetTourInfo()
        {
            //--团队信息--------
            string LDate = Utils.GetFormValue(this.txtTourLDate.UniqueID);
            string RDate = Utils.GetFormValue(this.txtTourRDate.UniqueID);
            string TourArriveCity = Utils.GetFormValue(this.txtTourArriveCity.UniqueID);
            string TourArriveCityFlight = Utils.GetFormValue(this.txtTourArriveCityFlight.UniqueID);
            string TourLeaveCity = Utils.GetFormValue(this.txtTourLeaveCity.UniqueID);
            string TourLeaveCityFlight = Utils.GetFormValue(this.txTourLeaveCityFlight.UniqueID);
            //--计调--------

            int Adults = Utils.GetInt(Utils.GetFormValue(this.txtTourAdults.UniqueID));
            int Childs = Utils.GetInt(Utils.GetFormValue(this.txtTourChilds.UniqueID));
            int Leaders = Utils.GetInt(Utils.GetFormValue(this.txtTourLeaders.UniqueID));
            int SiPei = Utils.GetInt(Utils.GetFormValue(this.txtToursipei.UniqueID));


            decimal AdultsPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtTourAdultsPrice.UniqueID));
            decimal ChildsPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtTourChildsPrice.UniqueID));
            decimal LeadersPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtTourLeadersPrice.UniqueID));
            decimal SingleRoomPrice = Utils.GetDecimal(Utils.GetFormValue(this.txtTourSingleRoomPrice.UniqueID));

            string[] RoomTypes = Utils.GetFormValues("ddlRoomType");
            string[] Nums = Utils.GetFormValues("txtRoomNumber");

            IList<EyouSoft.Model.HTourStructure.MTourRoom> tourRommList = null;

            if (RoomTypes != null && Nums != null && RoomTypes.Length > 0 && Nums.Length > 0)
            {
                tourRommList = new List<EyouSoft.Model.HTourStructure.MTourRoom>();
                for (int i = 0; i < RoomTypes.Length; i++)
                {
                    if (!string.IsNullOrEmpty(RoomTypes[i]) && Utils.GetInt(Nums[i]) != 0)
                    {
                        EyouSoft.Model.HTourStructure.MTourRoom tourRoomModel = new EyouSoft.Model.HTourStructure.MTourRoom();
                        tourRoomModel.RoomId = RoomTypes[i];
                        tourRoomModel.Num = Utils.GetInt(Nums[i]);
                        tourRommList.Add(tourRoomModel);
                    }
                }
            }

            //--内部提醒信息--------

            string InsideInformation = Utils.GetFormValue(this.txtTourInsideInformation.UniqueID);
            MQuoteTour model = new MQuoteTour();
            model.AdultPrice = AdultsPrice;
            model.Adults = Adults;
            model.ArriveCity = TourArriveCity;
            model.ArriveCityFlight = TourArriveCityFlight;
            model.ChildPrice = ChildsPrice;
            model.Childs = Childs;
            model.SiPei = SiPei;

            model.InsideInformation = InsideInformation;
            model.LDate = Utils.GetDateTime(LDate);
            model.LeadPrice = LeadersPrice;
            model.Leads = Leaders;
            model.LeaveCity = TourLeaveCity;
            model.LeaveCityFlight = TourLeaveCityFlight;
            IList<MTourPlaner> Planlist = new List<MTourPlaner>();
            string planerid = Utils.GetFormValue(this.SellsSelect2.SellsIDClient);
            EyouSoft.Model.ComStructure.MComUser sellsModel = new EyouSoft.BLL.ComStructure.BComUser().GetModel(planerid, SiteUserInfo.CompanyId);

            Planlist.Add(new MTourPlaner { Planer = Utils.GetFormValue(this.SellsSelect2.SellsNameClient), PlanerDeptId = sellsModel.DeptId, PlanerId = planerid });
            model.PlanerList = Planlist;
            model.RDate = Utils.GetDateTime(RDate);
            model.TourRoomList = tourRommList;
            model.SingleRoomPrice = SingleRoomPrice;
            model.TourCode = "";
            model.TourId = "";
            model.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;
            model.TourType = (TourType)Utils.GetInt(this.ddlTourType.SelectedValue);
            model.TourDiJieList = TuanDiJie1.GetFormInfo();

            return model;
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
                    if (!String.IsNullOrEmpty(shopid[i]))
                    {
                        list.Add(new MQuoteShop { ShopId = shopid[i], ShopName = shopname[i] });
                    }
                }
            }
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
            decimal trafficCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BTraffic.UniqueID));
            string trafficCostremark = Utils.GetFormValue(this.txt_Item_BTrafficRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = trafficCost;
            modelcost.Remark = trafficCostremark;

            list.Add(modelcost);

            modelcost = new MQuoteCost();
            decimal trainCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BTrain.UniqueID));
            string trainCostremark = Utils.GetFormValue(this.txt_Item_BTrainRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.火车;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = trainCost;
            modelcost.Remark = trainCostremark;

            list.Add(modelcost);

            modelcost = new MQuoteCost();
            decimal busCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BBus.UniqueID));
            string busCostremark = Utils.GetFormValue(this.txt_Item_BBusRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlItemCarTicketUnit.UniqueID));
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = busCost;
            modelcost.Remark = busCostremark;

            list.Add(modelcost);

            modelcost = new MQuoteCost();
            decimal shipCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BShip.UniqueID));
            string shipCostremark = Utils.GetFormValue(this.txt_Item_BShipRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = shipCost;
            modelcost.Remark = shipCostremark;

            list.Add(modelcost);

            decimal carCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Car.UniqueID));
            string carcostremark = Utils.GetFormValue(this.txt_Item_CarRemark.UniqueID);
            int priceunit = Utils.GetInt(Utils.GetFormValue(this.ddlItemCarUnit.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = carCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)priceunit;
            modelcost.Remark = carcostremark;
            list.Add(modelcost);

            decimal hotelCost1 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Room1.UniqueID));
            string hotelCost1remark = Utils.GetFormValue(this.txt_Item_RoomRemark1.UniqueID);
            int hotel1unit = Utils.GetInt(Utils.GetFormValue(this.ddlItemRoomUnit1.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = hotelCost1;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel1unit;
            modelcost.Remark = hotelCost1remark;
            list.Add(modelcost);

            decimal hotelCost2 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Room2.UniqueID));
            string hotelCost2remark = Utils.GetFormValue(this.txt_Item_RoomRemark2.UniqueID);
            int hotel2unit = Utils.GetInt(Utils.GetFormValue(this.ddlItemRoomUnit2.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = hotelCost2;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel2unit;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = hotelCost2remark;
            list.Add(modelcost);

            decimal canCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Dinner.UniqueID));
            string canCostremark = Utils.GetFormValue(this.txt_Item_DinnerRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = canCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = canCostremark;
            list.Add(modelcost);


            decimal daofuCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Guide.UniqueID));
            string daofuCostremark = Utils.GetFormValue(this.txt_Item_GuideRemark.UniqueID);
            int daofuunit = Utils.GetInt(Utils.GetFormValue(this.ddlItemGuide.UniqueID));
            modelcost = new MQuoteCost();
            modelcost.Price = daofuCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)daofuunit;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = daofuCostremark;
            list.Add(modelcost);


            decimal scenicCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Scenic.UniqueID));
            string scenicCostremark = Utils.GetFormValue(this.txt_Item_ScenicRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = scenicCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = scenicCostremark;
            list.Add(modelcost);

            decimal boxianCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Insure.UniqueID));
            string boxianCostremark = Utils.GetFormValue(this.txt_Item_InsureRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = boxianCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = boxianCostremark;
            list.Add(modelcost);

            decimal strafficCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_STraffic.UniqueID));
            string strafficCostremark = Utils.GetFormValue(this.txt_Item_STrafficRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = strafficCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = strafficCostremark;
            list.Add(modelcost);

            decimal zongfeiCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Sum.UniqueID));
            string zongfeiCostremark = Utils.GetFormValue(this.txt_Item_SumRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = zongfeiCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = zongfeiCostremark;
            list.Add(modelcost);

            decimal otherCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Other.UniqueID));
            string otherCostremark = Utils.GetFormValue(this.txt_Item_OtherRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = otherCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlItemQiTaUnit.UniqueID));
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = otherCostremark;
            list.Add(modelcost);

            decimal freeonecost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_FreeOne.UniqueID));
            string freeoneremark = Utils.GetFormValue(this.txt_Item_FreeOneRemark.UniqueID);
            modelcost = new MQuoteCost();
            modelcost.Price = freeonecost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = freeoneremark;
            list.Add(modelcost);

            #endregion
            if (type == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
            {
                #region 销售价格实体赋值
                modelsell = new MQuoteCost();
                decimal trafficPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BTraffic.UniqueID));
                string trafficPriceremark = Utils.GetFormValue(this.txt_Price_BTrafficRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = trafficPrice;
                modelsell.Remark = trafficPriceremark;

                list.Add(modelsell);

                modelsell = new MQuoteCost();
                decimal trainPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BTrain.UniqueID));
                string trainPriceremark = Utils.GetFormValue(this.txt_Price_BTrainRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.火车;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = trainPrice;
                modelsell.Remark = trainPriceremark;

                list.Add(modelsell);

                modelsell = new MQuoteCost();
                decimal busPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BBus.UniqueID));
                string busPriceremark = Utils.GetFormValue(this.txt_Price_BBusRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlPriceCarTicket.UniqueID));
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = busPrice;
                modelsell.Remark = busPriceremark;

                list.Add(modelsell);

                modelsell = new MQuoteCost();
                decimal shipPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BShip.UniqueID));
                string shipPriceremark = Utils.GetFormValue(this.txt_Price_BShipRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = shipPrice;
                modelsell.Remark = shipPriceremark;

                list.Add(modelsell);

                decimal carPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Car.UniqueID));
                string carpriceremark = Utils.GetFormValue(this.txt_Price_CarRemark.UniqueID);
                int carunit = Utils.GetInt(Utils.GetFormValue(this.ddlPriceCar.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = carPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)carunit;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = carpriceremark;
                list.Add(modelsell);

                decimal hotelPrice1 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Room1.UniqueID));
                string hotelPrice1remark = Utils.GetFormValue(this.txt_Price_RoomRemark1.UniqueID);
                int hotel1unit2 = Utils.GetInt(Utils.GetFormValue(this.ddlPriceRoom1.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = hotelPrice1;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel1unit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = hotelPrice1remark;
                list.Add(modelsell);

                decimal hotelPrice2 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Room2.UniqueID));
                string hotelPrice2remark = Utils.GetFormValue(this.txt_Price_RoomRemark2.UniqueID);
                int hotel2unit2 = Utils.GetInt(Utils.GetFormValue(this.ddlPriceRoom2.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = hotelPrice2;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel2unit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = hotelPrice2remark;
                list.Add(modelsell);

                decimal canPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Dinner.UniqueID));
                string canPriceremark = Utils.GetFormValue(this.txt_Price_DinnerRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = canPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = canPriceremark;
                list.Add(modelsell);


                decimal daofuPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Guide.UniqueID));
                string daofuPriceremark = Utils.GetFormValue(this.txt_Price_GuideRemark.UniqueID);
                int daofuunit2 = Utils.GetInt(Utils.GetFormValue(this.ddlPriceGuide.UniqueID));
                modelsell = new MQuoteCost();
                modelsell.Price = daofuPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)daofuunit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = daofuPriceremark;
                list.Add(modelsell);


                decimal scenicPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Scenic.UniqueID));
                string scenicPriceremark = Utils.GetFormValue(this.txt_Price_ScenicRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = scenicPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = scenicPriceremark;
                list.Add(modelsell);

                decimal boxianPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Insure.UniqueID));
                string boxianPriceremark = Utils.GetFormValue(this.txt_Price_InsureRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = boxianPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = boxianPriceremark;
                list.Add(modelsell);

                decimal strafficPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_STraffic.UniqueID));
                string strafficPriceremark = Utils.GetFormValue(this.txt_Price_STrafficRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = strafficPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = strafficPriceremark;
                list.Add(modelsell);

                decimal zongfeiPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Sum.UniqueID));
                string zongfeiPriceremark = Utils.GetFormValue(this.txt_Price_SumRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = zongfeiPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = zongfeiPriceremark;
                list.Add(modelsell);

                decimal otherPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Other.UniqueID));
                string otherPriceremark = Utils.GetFormValue(this.txt_Price_OtherRemark.UniqueID);
                modelsell = new MQuoteCost();

                modelsell.Price = otherPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlPriceQiTa.UniqueID));
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = otherPriceremark;
                list.Add(modelsell);

                decimal freeoneprice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_FreeOne.UniqueID));
                string freeonepriceremark = Utils.GetFormValue(this.txt_Price_FreeOneRemark.UniqueID);
                modelsell = new MQuoteCost();
                modelsell.Price = freeoneprice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.十六免一;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_团;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = freeonepriceremark;
                list.Add(modelsell);
                #endregion
            }

            return list;
        }

        /// <summary>
        /// 获取合计价格信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected IList<MQuotePrice> GetQuotePrice(EyouSoft.Model.EnumType.TourStructure.TourQuoteType type)
        {
            string[] itemadultprice = Utils.GetFormValues("txt_Item_AdultPrice");
            string[] itemchildprice = Utils.GetFormValues("txt_Item_ChildPrice");
            string[] itemLeadPrice = Utils.GetFormValues("txt_Item_LeadPrice");
            string[] itemsigleroomprice = Utils.GetFormValues("txt_Item_SingleRoomPrice");
            string[] itemotherprice = Utils.GetFormValues("txt_Item_OtherPrice");
            string[] itemhejiprice = Utils.GetFormValues("txt_Item_HejiPrice");


            string[] priceadultprice = Utils.GetFormValues("txt_Price_AdultPrice");
            string[] pricehildprice = Utils.GetFormValues("txt_Price_ChildPrice");
            string[] priceLeadPrice = Utils.GetFormValues("txt_Price_LeadPrice");
            string[] pricesigleroomprice = Utils.GetFormValues("txt_Price_SingleRoomPrice");
            string[] priceotherprice = Utils.GetFormValues("txt_Price_OtherPrice");
            string[] pricehejiprice = Utils.GetFormValues("txt_Price_HejiPrice");


            string[] zengtuanadultprice = Utils.GetFormValues("txt_zengtuan_AdultPrice");
            string[] zengtuanhildprice = Utils.GetFormValues("txt_zengtuan_ChildPrice");
            string[] zengtuanLeadPrice = Utils.GetFormValues("txt_zengtuan_LeadPrice");
            string[] zengtuansigleroomprice = Utils.GetFormValues("txt_zengtuan_SingleRoomPrice");
            string[] zengtuanotherprice = Utils.GetFormValues("txt_zengtuan_OtherPrice");
            string[] zengtuanhejiprice = Utils.GetFormValues("txt_zengtuan_hejiPrice");

            IList<MQuotePrice> list = new List<MQuotePrice>();
            MQuotePrice modelcost = null;
            MQuotePrice modelsell = null;
            if (itemadultprice.Length > 0)
            {
                for (int i = 0; i < itemadultprice.Length; i++)
                {

                    modelcost = new MQuotePrice();
                    modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
                    modelcost.AdultPrice = Utils.GetDecimal(itemadultprice[i].ToString());
                    modelcost.ChildPrice = Utils.GetDecimal(itemchildprice[i].ToString());
                    modelcost.LeadPrice = Utils.GetDecimal(itemLeadPrice[i].ToString());
                    modelcost.OtherPrice = Utils.GetDecimal(itemotherprice[i]);
                    modelcost.SingleRoomPrice = Utils.GetDecimal(itemsigleroomprice[i]);
                    modelcost.HeJiPrice = Utils.GetDecimal(itemhejiprice[i]);
                    list.Add(modelcost);

                }
            }
            if (type == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
            {
                if (priceadultprice.Length > 0)
                {
                    for (int j = 0; j < priceadultprice.Length; j++)
                    {
                        modelsell = new MQuotePrice();
                        modelsell.AdultPrice = Utils.GetDecimal(priceadultprice[j].ToString());
                        modelsell.ChildPrice = Utils.GetDecimal(pricehildprice[j].ToString());
                        modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                        modelsell.LeadPrice = Utils.GetDecimal(priceLeadPrice[j].ToString());
                        modelsell.OtherPrice = Utils.GetDecimal(priceotherprice[j].ToString());
                        modelsell.SingleRoomPrice = Utils.GetDecimal(pricesigleroomprice[j].ToString());
                        modelsell.HeJiPrice = Utils.GetDecimal(pricehejiprice[j]);
                        list.Add(modelsell);

                    }
                }
            }
            if (type == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
            {
                if (zengtuanadultprice.Length > 0)
                {
                    for (int k = 0; k < zengtuanadultprice.Length; k++)
                    {
                        if (zengtuanadultprice[k] != "")
                        {
                            modelsell = new MQuotePrice();
                            modelsell.AdultPrice = Utils.GetDecimal(zengtuanadultprice[k].ToString());
                            modelsell.ChildPrice = Utils.GetDecimal(zengtuanhildprice[k].ToString());
                            modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                            modelsell.LeadPrice = Utils.GetDecimal(zengtuanLeadPrice[k].ToString());
                            modelsell.OtherPrice = Utils.GetDecimal(zengtuanotherprice[k].ToString());
                            modelsell.SingleRoomPrice = Utils.GetDecimal(zengtuansigleroomprice[k].ToString());
                            modelsell.HeJiPrice = Utils.GetDecimal(zengtuanhejiprice[k]);
                            list.Add(modelsell);
                        }
                    }
                }

            }

            return list;
        }

        /// <summary>
        /// 绑定房型
        /// </summary>
        protected string BindHotelRoomType()
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();

            EyouSoft.BLL.SysStructure.BSysRoom bll = new EyouSoft.BLL.SysStructure.BSysRoom();
            IList<EyouSoft.Model.SysStructure.MSysRoom> list = bll.GetRooms(SiteUserInfo.CompanyId);

            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    query.Append("房型：<select  name='ddlRoomType' class='inputselect'>");
                    for (int j = 0; j < list.Count; j++)
                    {
                        EyouSoft.Model.SysStructure.MSysRoom model = list[j];
                        query.AppendFormat("<option value='{0}'>{1}</option>", model.RoomId, model.TypeName);
                    }
                    query.Append("</select>");

                    query.Append("&nbsp;间数：<input name='txtRoomNumber' type='text'  class='inputtext formsize40' />&nbsp;&nbsp;");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// 验证是否存在相同的询价编号
        /// </summary>
        /// <param name="BuyId">询价编号</param>
        /// <returns></returns>
        private string CheckXunJiaID(string BuyId, string QuoteId)
        {
            string msg = string.Empty;
            EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
            EyouSoft.Model.HTourStructure.MQuote model = new EyouSoft.Model.HTourStructure.MQuote();
            if (bll.isExist(BuyId, QuoteId))
            {
                msg = UtilsCommons.AjaxReturnJson("0", "存在相同的询价编号!");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("1", "不重复!");
            }
            return msg;
        }

        /// <summary>
        /// 获取回团时间
        /// </summary>
        /// <returns></returns>
        private string GetRDate()
        {
            var ldate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("LDate"));
            var d = Utils.GetInt(Utils.GetQueryStringValue("Days"));
            return ldate.HasValue ? ldate.Value.AddDays(d - 1).ToString("yyyy-MM-dd") : string.Empty;
        }
    }
}
