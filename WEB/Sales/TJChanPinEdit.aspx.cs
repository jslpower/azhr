﻿using System;
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
using System.Text;
using System.Collections.Generic;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.Web.Sales
{
    public partial class TJChanPinEdit : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string TourId = string.Empty;
        //购物点
        protected string shopStr = string.Empty;
        protected string allcitys = string.Empty;

        protected string CountryId = "0";
        protected string TuanXing = "0";
        protected string tourMode = "1";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("id");
            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.团队中心_自由行_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.团队中心_自由行_修改);
        }


        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            //TuanXingCheng1.CompanyID = SiteUserInfo.CompanyId;
            RegisterScript("var fangXing=[];");

            var deptInfo = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(SiteUserInfo.DeptId, SiteUserInfo.CompanyId);
            if (deptInfo != null)
            {
                txtJiDiaoYuan.SellsID = deptInfo.DepartPlanId;
                var jdinfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(deptInfo.DepartPlanId, SiteUserInfo.CompanyId);
                if (jdinfo != null)
                {
                    txtJiDiaoYuan.SellsName = jdinfo.ContactName;
                }
            }

            txtXiaoShouYuan.SellsID = SiteUserInfo.UserId;
            txtXiaoShouYuan.SellsName = SiteUserInfo.Name;

            if (string.IsNullOrEmpty(TourId))
            {
                return;
            }

            var info = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(TourId);
            if (info == null) RCWE("异常请求");

            txtJiDiaoYuan.IsShowSelect = info.TourStatus < EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置;
            TuanXing = ((int)info.TourMode).ToString();
            hidTourModeValue.Value = ((int)info.TourMode).ToString();
            CountryId = info.CountryId.ToString();
            tourMode = ((int)info.OutQuoteType).ToString();
            this.txtTuanXing.Attributes.Add("disabled", "true");
            this.txtTuanXing.Attributes.Add("background-color", "#dadada");
            txtTourCode.Value = info.TourCode;
            txtKeHuTourCode.Value = info.TourCustomerCode;
            txtRouteName.Value = info.RouteName;
            txtTourDays.Value = info.TourDays.ToString();
            txtKeHu.CustomerUnitId = info.BuyCompanyID;
            txtKeHu.CustomerUnitName = info.BuyCompanyName;
            txtLDate.Value = info.LDate.ToString("yyyy-MM-dd");
            txtDiDaChengShi.Value = info.ArriveCity;
            txtDiDaHangBan.Value = info.ArriveCityFlight;
            txtRDate.Value = info.RDate.ToString("yyyy-MM-dd");
            txtLiKaiChengShi.Value = info.LeaveCity;
            txtLiKaiHangBan.Value = info.LeaveCityFlight;
            txtXiaoShouYuan.SellsID = info.SellerId;
            txtXiaoShouYuan.SellsName = info.SellerName;

            hidRouteIds.Value = info.RouteIds;
            string routeids = info.RouteIds;
            System.Text.StringBuilder routeStr = new StringBuilder();
            if (!string.IsNullOrEmpty(routeids))
            {
                string[] ids = info.RouteIds.Split(',');
                if (ids.Length > 0)
                {
                    EyouSoft.Model.HTourStructure.MTour modelroute = null;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (ids[i].ToString().Trim() != "")
                        {
                            modelroute = new EyouSoft.BLL.HTourStructure.BTour().GetRouteInfoByTourId(ids[i].ToString());
                            routeStr.AppendFormat("<span class='upload_filename' data-id='" + modelroute.TourId + "'><a href='javascript:;' data-id='{0}' >{1}<img src='/images/cha.gif' class='delRoute' data-id='{0}' /></a></span>", modelroute.TourId, modelroute.RouteName);
                        }
                    }
                }
            }

            this.lbrouteName.InnerHtml = routeStr.ToString();

            if (info.TourPlanerList != null && info.TourPlanerList.Count > 0)
            {
                string s = string.Empty;
                string s1 = string.Empty;
                foreach (var item in info.TourPlanerList)
                {
                    s += "," + item.PlanerId;
                    s1 += "," + item.Planer;
                }

                if (!string.IsNullOrEmpty(s)) s = s.Trim(',');
                if (!string.IsNullOrEmpty(s1)) s1 = s1.Trim(',');

                txtJiDiaoYuan.SellsID = s;
                txtJiDiaoYuan.SellsName = s1;
            }

            txtCR.Value = info.Adults.ToString();
            txtET.Value = info.Childs.ToString();
            txtLD.Value = info.Leaders.ToString();

            RegisterScript(string.Format("var fangXing={0};", Newtonsoft.Json.JsonConvert.SerializeObject(info.TourRoomList)));

            if (info.TourFileList != null && info.TourFileList.Count > 0)
            {
                var files = new List<global::Web.UserControl.MFileInfo>();
                foreach (var item in info.TourFileList)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }
                txtFuJian1.YuanFiles = files;
            }

            if (!string.IsNullOrEmpty(info.TourFile))
            {
                var files = new List<global::Web.UserControl.MFileInfo>();
                files.Add(new global::Web.UserControl.MFileInfo() { FilePath = info.TourFile });
                txtFuJian2.YuanFiles = files;
            }

            txtLiangDian.Text = info.JourneySpot;


            TuanFengWeiCan1.SetFengWeiCan = info.TourFootList;
            //TuanZiFei1.SetZiFei = info.TourSelfPayList;
            TuanZengSong1.SetZengSong = info.TourGiveList;
            TuanXiaoFei1.SetXiaoFei = info.TourTipList;
            TuanDiJie1.DiJies = info.TourDiJieList;

            var youKes = new EyouSoft.BLL.HTourStructure.BTourOrder().GetTourOrderTraveller(TourId);
            YouKe.YouKes = youKes;

            txtNeiBuXinXi.Value = info.InsideInformation;
            txtSpecificRequire.Text = info.SpecificRequire;
            txtJourney.Text = info.TravelNote;
            this.SelectJourney1.SetTourJourneyList = info.TourJourneyList;
            this.SelectJourneySpot1.SetTourJourneyList = info.TourJourneyList;
            this.hidItemType.Value = ((int)info.OutQuoteType).ToString();

            this.LvPrice.Value = GetCustomerFloatPrice(info.BuyCompanyID);


            #region 合计价格
            //合计价格
            if (info.TourPriceList != null && info.TourPriceList.Count > 0)
            {
                var xs = info.TourPriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格).ToList();

                var cb = info.TourPriceList.Where(m => m.CostMode == EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格).ToList();
                if (cb.Count > 0)
                {
                    this.rptItem.DataSource = cb;
                    this.rptItem.DataBind();
                }

                if (info.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
                {
                    if (xs.Count > 0)
                    {
                        this.PhPrice.Visible = false;
                        this.rptPrice.DataSource = xs;
                        this.rptPrice.DataBind();
                    }
                }

                if (info.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.整团)
                {
                    if (xs.Count > 0)
                    {
                        this.Phzengtuan.Visible = false;
                        this.rptzengtuan.DataSource = xs;
                        this.rptzengtuan.DataBind();
                    }
                }
            }
            #endregion

            #region 价格项目信息
            //价格项目信息
            if (info.TourCostList != null && info.TourCostList.Count > 0)
            {
                foreach (var item in info.TourCostList)
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
                                //this.txt_Item_Room2.Text = item.Price.ToString("F2");
                                //this.txt_Item_RoomRemark2.Text =  item.Remark;
                                //ddlItemRoomUnit2.SelectedValue = ((int)item.PriceUnit).ToString();
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
                                txt_Price_Dinner1.Text = item.Price.ToString("F2");
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
                                //this.txt_Price_Room2.Text = item.Price.ToString("F2");
                                //this.txt_Price_RoomRemark2.Text =  item.Remark;
                                //ddlPriceRoom2.SelectedValue=((int)item.PriceUnit).ToString();
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

            }
            #endregion

            #region 文件信息
            if (info.TourFileList != null && info.TourFileList.Count > 0)
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();


                foreach (var item in info.TourFileList)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }

                txtFuJian1.YuanFiles = files;
            }

            if (!string.IsNullOrEmpty(info.TourFile))
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();

                files.Add(new global::Web.UserControl.MFileInfo() { FilePath = info.TourFile });

                txtFuJian2.YuanFiles = files;
            }
            #endregion

            txtDaoYouShouKuanJinE.Value = info.GuideIncome.ToString("F2");
            //txtXiaoShouShouKuanJinE.Value = info.SalerIncome.ToString("F2");
            txtHeJiJinE.Value = info.SumPrice.ToString("F2");

            TxtQuoteRemark.Text = info.QuoteRemark;

            //如果本团计调员不需要写变更记录
            if (info.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划
                && info.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 && info.TourPlanerList.Where(m => m.PlanerId == this.SiteUserInfo.UserId).ToList().Count == 0) txtIsBianGeng.Value = "1";
        }

        /// <summary>
        /// 获取公司等级对应的浮动价格
        /// </summary>
        /// <param name="BuyCompanyId"></param>
        /// <returns></returns>
        private string GetCustomerFloatPrice(string BuyCompanyId)
        {
            EyouSoft.Model.CrmStructure.MCrm Cutomermodel = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(BuyCompanyId);
            if (Cutomermodel == null) return "0";
            string price = "0";
            MComLev model = new EyouSoft.BLL.ComStructure.BComLev().GetInfo(Cutomermodel.LevId, SiteUserInfo.CompanyId);
            if (model != null)
            {
                price = model.FloatMoney.ToString("f2");
            }
            return price;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            if (string.IsNullOrEmpty(TourId))
            {
                if (!Privs_Insert) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }
            else
            {
                if (!Privs_Update) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));
            }

            var info = GetFormInfo();
            int bllRetCode = 0;

            if (string.IsNullOrEmpty(info.TourId))
            {
                bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().AddTour(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().UpdateTour(info);
            }

            if (bllRetCode == 1)
            {
                var youKes = YouKe.YouKes;
                new EyouSoft.BLL.HTourStructure.BTourOrder().AddOrUpdateTourTraveller(youKes, info.TourId);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }

        /// <summary>
        /// get form info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HTourStructure.MTour GetFormInfo()
        {
            var info = new EyouSoft.Model.HTourStructure.MTour();

            info.Adults = Utils.GetInt(Utils.GetFormValue(txtCR.UniqueID));
            info.AreaName = string.Empty;
            info.ArriveCity = Utils.GetFormValue(txtDiDaChengShi.UniqueID);
            info.ArriveCityFlight = Utils.GetFormValue(txtDiDaHangBan.UniqueID);
            info.BuyCompanyID = Utils.GetFormValue(txtKeHu.ClientNameKHBH);
            info.BuyCompanyName = Utils.GetFormValue(txtKeHu.ClientNameKHMC);
            info.Childs = Utils.GetInt(Utils.GetFormValue(txtET.UniqueID));
            info.CompanyId = SiteUserInfo.CompanyId;
            info.CountryId = Utils.GetInt(Utils.GetFormValue("txtCountryId"));
            info.GuideIncome = Utils.GetDecimal(Utils.GetFormValue(txtDaoYouShouKuanJinE.UniqueID));
            info.Guides = null;
            info.InsideInformation = Utils.GetFormValue(txtNeiBuXinXi.UniqueID);
            info.JourneySpot = Utils.GetFormEditorValue(txtLiangDian.UniqueID);
            info.LDate = Utils.GetDateTime(Utils.GetFormValue(txtLDate.UniqueID));
            info.Leaders = Utils.GetInt(Utils.GetFormValue(txtLD.UniqueID));
            info.LeaveCity = Utils.GetFormValue(txtLiKaiChengShi.UniqueID);
            info.LeaveCityFlight = Utils.GetFormValue(txtLiKaiHangBan.UniqueID);
            info.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
            info.Operator = SiteUserInfo.Name; ;
            info.OperatorDeptId = SiteUserInfo.DeptId;
            info.OperatorId = SiteUserInfo.UserId;
            //info.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Utils.GetInt(Utils.GetFormValue("rdTourQuoteType"));
            info.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Utils.GetInt(Utils.GetFormValue(this.hidItemType.UniqueID));
            info.Planers = string.Empty;
            info.QuoteRemark = Utils.GetFormValue(TxtQuoteRemark.UniqueID);
            info.RDate = Utils.GetDateTime(Utils.GetFormValue(txtRDate.UniqueID));
            info.RouteName = Utils.GetFormValue(txtRouteName.UniqueID);
            
            if (info.OutQuoteType == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
            {

                info.ConfirmMoney = info.SalerIncome = info.SumPrice = Utils.GetDecimal(Utils.GetFormValue("txtfenxiang_HeJiPrice"));
            }
            else
            {
                info.ConfirmMoney = info.SalerIncome = info.SumPrice = Utils.GetDecimal(Utils.GetFormValue("txtzengtuan_HeJiPrice"));
            }

            info.SellerDeptId = 0;
            info.SellerId = Utils.GetFormValue(txtXiaoShouYuan.SellsIDClient);
            info.SellerDeptId = new EyouSoft.BLL.ComStructure.BComUser().GetModel(info.SellerId, SiteUserInfo.CompanyId).DeptId;
            info.SellerName = Utils.GetFormValue(txtXiaoShouYuan.SellsNameClient);
            info.SpecificRequire = Utils.GetFormValue(txtSpecificRequire.UniqueID);


           
            info.TourCostList = GetTourCost(info.OutQuoteType);
            info.TourCustomerCode = Utils.GetFormValue(txtKeHuTourCode.UniqueID);
            info.TourDays = Utils.GetInt(Utils.GetFormValue(txtTourDays.UniqueID));
            info.TourDiJieList = TuanDiJie1.GetFormInfo();
            info.TourFile = GetFuJian2();
            info.TourFileList = GetFuJian1();
            info.TourFootList = TuanFengWeiCan1.GetFengWeiList();
            info.TourGiveList = TuanZengSong1.GetDataList();
            info.TourId = TourId;
            info.TourMode = (EyouSoft.Model.EnumType.TourStructure.TourMode)Utils.GetInt(Utils.GetFormValue(this.hidTourModeValue.UniqueID));
            info.TourPlanerList = GetJiDiao();
            info.TourPlanItemList = null;

            info.TourPlanRemark = null;
            info.TourPriceList = GetTourPrice(info.OutQuoteType);
            info.TourRoomList = GetFangXing();
            //info.TourSelfPayList = TuanZiFei1.GetDataList();
            //info.TourShopList = GetShoplist();
            info.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;
            info.TourSureStatus = EyouSoft.Model.EnumType.TourStructure.TourSureStatus.未确认;
            info.TourTipList = TuanXiaoFei1.GetDataList();
            info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.自由行;
            info.TravelNote = Utils.GetFormEditorValue(txtJourney.UniqueID);
            info.UpdateTime = DateTime.Now;
            info.SalerIncome = info.SumPrice - info.GuideIncome;
            info.TourJourneyList = this.SelectJourney1.GetTourJourneyList();
            info.RouteIds = Utils.GetFormValue(hidRouteIds.UniqueID);
            info.RouteIds = info.RouteIds.Replace(",,", ",");
            info.TourCode = Utils.GetFormValue(this.txtTourCode.UniqueID);
            if (info.RouteIds.EndsWith(","))
                info.RouteIds = info.RouteIds.Substring(info.RouteIds.Length - 1, 1);
            if (info.RouteIds.StartsWith(","))
                info.RouteIds = info.RouteIds.Substring(1, info.RouteIds.Length - 1);
            info.TourPlanList = GetTourPlanList(info.RouteIds);

            var i2 = this.SelectJourneySpot1.GetTourJourneyList();
            foreach (var item in i2)
            {
                info.TourJourneyList.Add(item);
            }


            if (Utils.GetFormValue(txtIsBianGeng.UniqueID) == "1")
            {
                info.TourChangeTitle = Utils.GetFormValue("txt_ChangeTitle");
                info.TourChangeContent = Utils.GetFormValue("txt_ChangeRemark");
            }

            return info;
        }
        /// <summary>
        /// 根据线路编号获取行程内容
        /// </summary>
        /// <param name="routeids"></param>
        /// <returns></returns>
        private IList<MTourPlan> GetTourPlanList(string routeids)
        {
            List<MTourPlan> TourPlanList = new List<MTourPlan>();
            string[] TourIds = null;
            if (!string.IsNullOrEmpty(routeids))
                TourIds = routeids.Split(',');
            if (TourIds.Length > 0)
            {
                EyouSoft.Model.HTourStructure.MTour modelroute = null;
                for (int i = 0; i < TourIds.Length; i++)
                {
                    if (TourIds[i].ToString().Trim() != "")
                    {
                        modelroute = new EyouSoft.BLL.HTourStructure.BTour().GetRouteInfoByTourId(TourIds[i].ToString());
                        if (modelroute != null)
                        {
                            if (modelroute.TourPlanList != null && modelroute.TourPlanList.Count > 0)
                                TourPlanList.AddRange(modelroute.TourPlanList);
                        }
                    }
                }
            }
            return TourPlanList;
        }

        /// <summary>
        /// 获取成本和价格
        /// </summary>
        /// <returns></returns>
        IList<MTourCost> GetTourCost(EyouSoft.Model.EnumType.TourStructure.TourQuoteType type)
        {
            IList<MTourCost> list = new List<MTourCost>();
            MTourCost modelcost = null;
            MTourCost modelsell = null;

            #region  成本价格实体赋值
            modelcost = new MTourCost();
            decimal trafficCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BTraffic.UniqueID));
            string trafficCostremark = Utils.GetFormValue(this.txt_Item_BTrafficRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = trafficCost;
            modelcost.Remark = trafficCostremark;

            list.Add(modelcost);

            modelcost = new MTourCost();
            decimal trainCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BTrain.UniqueID));
            string trainCostremark = Utils.GetFormValue(this.txt_Item_BTrainRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.火车;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = trainCost;
            modelcost.Remark = trainCostremark;

            list.Add(modelcost);

            modelcost = new MTourCost();
            decimal busCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_BBus.UniqueID));
            string busCostremark = Utils.GetFormValue(this.txt_Item_BBusRemark.UniqueID);
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlItemCarTicketUnit.UniqueID));
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = busCost;
            modelcost.Remark = busCostremark;

            list.Add(modelcost);

            modelcost = new MTourCost();
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
            modelcost = new MTourCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = carCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)priceunit;
            modelcost.Remark = carcostremark;
            list.Add(modelcost);

            decimal hotelCost1 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Room1.UniqueID));
            string hotelCost1remark = Utils.GetFormValue(this.txt_Item_RoomRemark1.UniqueID);
            int hotel1unit = Utils.GetInt(Utils.GetFormValue(this.ddlItemRoomUnit1.UniqueID));
            modelcost = new MTourCost();
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Price = hotelCost1;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel1unit;
            modelcost.Remark = hotelCost1remark;
            list.Add(modelcost);

            decimal hotelCost2 = 0;
            string hotelCost2remark = "";
            int hotel2unit = 0;
            modelcost = new MTourCost();
            modelcost.Price = hotelCost2;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel2unit;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = hotelCost2remark;
            list.Add(modelcost);

            decimal canCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Dinner.UniqueID));
            string canCostremark = Utils.GetFormValue(this.txt_Item_DinnerRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = canCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = canCostremark;
            list.Add(modelcost);


            decimal daofuCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Guide.UniqueID));
            string daofuCostremark = Utils.GetFormValue(this.txt_Item_GuideRemark.UniqueID);
            int daofuunit = Utils.GetInt(Utils.GetFormValue(this.ddlItemGuide.UniqueID));
            modelcost = new MTourCost();
            modelcost.Price = daofuCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)daofuunit;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = daofuCostremark;
            list.Add(modelcost);


            decimal scenicCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Scenic.UniqueID));
            string scenicCostremark = Utils.GetFormValue(this.txt_Item_ScenicRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = scenicCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = scenicCostremark;
            list.Add(modelcost);

            decimal boxianCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Insure.UniqueID));
            string boxianCostremark = Utils.GetFormValue(this.txt_Item_InsureRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = boxianCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = boxianCostremark;
            list.Add(modelcost);

            decimal strafficCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_STraffic.UniqueID));
            string strafficCostremark = Utils.GetFormValue(this.txt_Item_STrafficRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = strafficCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = strafficCostremark;
            list.Add(modelcost);

            decimal zongfeiCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Sum.UniqueID));
            string zongfeiCostremark = Utils.GetFormValue(this.txt_Item_SumRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = zongfeiCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
            modelcost.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = zongfeiCostremark;
            list.Add(modelcost);

            decimal otherCost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_Other.UniqueID));
            string otherCostremark = Utils.GetFormValue(this.txt_Item_OtherRemark.UniqueID);
            modelcost = new MTourCost();
            modelcost.Price = otherCost;
            modelcost.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
            modelcost.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlItemQiTaUnit.UniqueID));
            modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
            modelcost.Remark = otherCostremark;
            list.Add(modelcost);

            decimal freeonecost = Utils.GetDecimal(Utils.GetFormValue(this.txt_Item_FreeOne.UniqueID));
            string freeoneremark = Utils.GetFormValue(this.txt_Item_FreeOneRemark.UniqueID);
            modelcost = new MTourCost();
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
                modelsell = new MTourCost();
                decimal trafficPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BTraffic.UniqueID));
                string trafficPriceremark = Utils.GetFormValue(this.txt_Price_BTrafficRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = trafficPrice;
                modelsell.Remark = trafficPriceremark;

                list.Add(modelsell);

                modelsell = new MTourCost();
                decimal trainPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BTrain.UniqueID));
                string trainPriceremark = Utils.GetFormValue(this.txt_Price_BTrainRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.火车;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = trainPrice;
                modelsell.Remark = trainPriceremark;

                list.Add(modelsell);

                modelsell = new MTourCost();
                decimal busPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_BBus.UniqueID));
                string busPriceremark = Utils.GetFormValue(this.txt_Price_BBusRemark.UniqueID);
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlPriceCarTicket.UniqueID));
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Price = busPrice;
                modelsell.Remark = busPriceremark;

                list.Add(modelsell);

                modelsell = new MTourCost();
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
                modelsell = new MTourCost();
                modelsell.Price = carPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.用车;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)carunit;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = carpriceremark;
                list.Add(modelsell);

                decimal hotelPrice1 = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Room1.UniqueID));
                string hotelPrice1remark = Utils.GetFormValue(this.txt_Price_RoomRemark1.UniqueID);
                int hotel1unit2 = Utils.GetInt(Utils.GetFormValue(this.ddlPriceRoom1.UniqueID));
                modelsell = new MTourCost();
                modelsell.Price = hotelPrice1;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房1;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel1unit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = hotelPrice1remark;
                list.Add(modelsell);

                decimal hotelPrice2 = 0;
                string hotelPrice2remark = "";
                int hotel2unit2 = 0;
                modelsell = new MTourCost();
                modelsell.Price = hotelPrice2;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.房2;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)hotel2unit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = hotelPrice2remark;
                list.Add(modelsell);

                decimal canPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Dinner1.UniqueID));
                string canPriceremark = Utils.GetFormValue(this.txt_Price_DinnerRemark.UniqueID);
                modelsell = new MTourCost();
                modelsell.Price = canPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.餐;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = canPriceremark;
                list.Add(modelsell);


                decimal daofuPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Guide.UniqueID));
                string daofuPriceremark = Utils.GetFormValue(this.txt_Price_GuideRemark.UniqueID);
                int daofuunit2 = Utils.GetInt(Utils.GetFormValue(this.ddlPriceGuide.UniqueID));
                modelsell = new MTourCost();
                modelsell.Price = daofuPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.导服;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)daofuunit2;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = daofuPriceremark;
                list.Add(modelsell);


                decimal scenicPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Scenic.UniqueID));
                string scenicPriceremark = Utils.GetFormValue(this.txt_Price_ScenicRemark.UniqueID);
                modelsell = new MTourCost();
                modelsell.Price = scenicPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.景点;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = scenicPriceremark;
                list.Add(modelsell);

                decimal boxianPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Insure.UniqueID));
                string boxianPriceremark = Utils.GetFormValue(this.txt_Price_InsureRemark.UniqueID);
                modelsell = new MTourCost();
                modelsell.Price = boxianPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.保险;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = boxianPriceremark;
                list.Add(modelsell);

                decimal strafficPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_STraffic.UniqueID));
                string strafficPriceremark = Utils.GetFormValue(this.txt_Price_STrafficRemark.UniqueID);
                modelsell = new MTourCost();
                modelsell.Price = strafficPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = strafficPriceremark;
                list.Add(modelsell);

                decimal zongfeiPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Sum.UniqueID));
                string zongfeiPriceremark = Utils.GetFormValue(this.txt_Price_SumRemark.UniqueID);
                modelsell = new MTourCost();
                modelsell.Price = zongfeiPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.综费;
                modelsell.PriceUnit = EyouSoft.Model.EnumType.TourStructure.PriceUnit.元_人;
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = zongfeiPriceremark;
                list.Add(modelsell);

                decimal otherPrice = Utils.GetDecimal(Utils.GetFormValue(this.txt_Price_Other.UniqueID));
                string otherPriceremark = Utils.GetFormValue(this.txt_Price_OtherRemark.UniqueID);
                modelsell = new MTourCost();

                modelsell.Price = otherPrice;
                modelsell.Pricetype = EyouSoft.Model.EnumType.TourStructure.Pricetype.其他;
                modelsell.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetFormValue(this.ddlPriceQiTa.UniqueID));
                modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                modelsell.Remark = otherPriceremark;
                list.Add(modelsell);

                #endregion
            }

            return list;
        }



        /// <summary>
        /// 获取合计价格
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<MTourPrice> GetTourPrice(EyouSoft.Model.EnumType.TourStructure.TourQuoteType type)
        {
            string[] itemadultprice = Utils.GetFormValues("txt_Item_AdultPrice");
            string[] itemchildprice = Utils.GetFormValues("txt_Item_ChildPrice");
            string[] itemLeadPrice = Utils.GetFormValues("txt_Item_LeadPrice");
            string[] itemsigleroomprice = Utils.GetFormValues("txt_Item_SingleRoomPrice");
            string[] itemotherprice = Utils.GetFormValues("txt_Item_OtherPrice");

            string[] priceadultprice = Utils.GetFormValues("txt_Price_AdultPrice");
            string[] pricehildprice = Utils.GetFormValues("txt_Price_ChildPrice");
            string[] priceLeadPrice = Utils.GetFormValues("txt_Price_LeadPrice");
            string[] pricesigleroomprice = Utils.GetFormValues("txt_Price_SingleRoomPrice");
            string[] priceotherprice = Utils.GetFormValues("txt_Price_OtherPrice");
            string[] pricetotalprice = Utils.GetFormValues("txtfenxiang_HeJiPrice");


            string[] zengtuanadultprice = Utils.GetFormValues("txt_zengtuan_AdultPrice");
            string[] zengtuanhildprice = Utils.GetFormValues("txt_zengtuan_ChildPrice");
            string[] zengtuanLeadPrice = Utils.GetFormValues("txt_zengtuan_LeadPrice");
            string[] zengtuansigleroomprice = Utils.GetFormValues("txt_zengtuan_SingleRoomPrice");
            string[] zengtuanotherprice = Utils.GetFormValues("txt_zengtuan_OtherPrice");
            string[] zengtuantotalprice = Utils.GetFormValues("txtzengtuan_HeJiPrice");

            IList<MTourPrice> list = new List<MTourPrice>();
            MTourPrice modelcost = null;
            MTourPrice modelsell = null;
            if (itemadultprice.Length > 0)
            {
                for (int i = 0; i < itemadultprice.Length; i++)
                {
                    modelcost = new MTourPrice();
                    modelcost.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.成本价格;
                    modelcost.AdultPrice = Utils.GetDecimal(itemadultprice[i].ToString());
                    modelcost.ChildPrice = Utils.GetDecimal(itemchildprice[i].ToString());
                    modelcost.LeadPrice = Utils.GetDecimal(itemLeadPrice[i].ToString());
                    modelcost.OtherPrice = Utils.GetDecimal(itemotherprice[i]);
                    modelcost.SingleRoomPrice = Utils.GetDecimal(itemsigleroomprice[i]);
                    list.Add(modelcost);

                }
            }
            if (type == EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项)
            {
                if (priceadultprice.Length > 0)
                {
                    for (int j = 0; j < priceadultprice.Length; j++)
                    {
                        modelsell = new MTourPrice();
                        modelsell.AdultPrice = Utils.GetDecimal(priceadultprice[j].ToString());
                        modelsell.ChildPrice = Utils.GetDecimal(pricehildprice[j].ToString());
                        modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                        modelsell.LeadPrice = Utils.GetDecimal(priceLeadPrice[j].ToString());
                        modelsell.OtherPrice = Utils.GetDecimal(priceotherprice[j].ToString());
                        modelsell.SingleRoomPrice = Utils.GetDecimal(pricesigleroomprice[j].ToString());
                        modelsell.TotalPrice = Utils.GetDecimal(pricetotalprice[j].ToString());
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
                            modelsell = new MTourPrice();
                            modelsell.AdultPrice = Utils.GetDecimal(zengtuanadultprice[k].ToString());
                            modelsell.ChildPrice = Utils.GetDecimal(zengtuanhildprice[k].ToString());
                            modelsell.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                            modelsell.LeadPrice = Utils.GetDecimal(zengtuanLeadPrice[k].ToString());
                            modelsell.OtherPrice = Utils.GetDecimal(zengtuanotherprice[k].ToString());
                            modelsell.SingleRoomPrice = Utils.GetDecimal(zengtuansigleroomprice[k].ToString());
                            modelsell.TotalPrice = Utils.GetDecimal(zengtuantotalprice[k].ToString());
                            list.Add(modelsell);
                        }
                    }
                }

            }

            return list;
        }

        /// <summary>
        /// 获取计调员
        /// </summary>
        /// <returns></returns>
        IList<MTourPlaner> GetJiDiao()
        {
            IList<MTourPlaner> items = new List<MTourPlaner>();
            string s = Utils.GetFormValue(txtJiDiaoYuan.SellsIDClient);

            if (!string.IsNullOrEmpty(s))
            {
                var bll = new EyouSoft.BLL.ComStructure.BComUser();

                var arr = s.Split(',');
                foreach (var item in arr)
                {
                    if (string.IsNullOrEmpty(item)) continue;

                    var uinfo = bll.GetModel(item, SiteUserInfo.CompanyId);

                    if (uinfo == null) continue;

                    var _item = new MTourPlaner();
                    _item.PlanerId = item;
                    _item.Planer = uinfo.ContactName;
                    _item.PlanerDeptId = uinfo.DeptId;

                    items.Add(_item);
                }

            }

            return items;
        }

        /// <summary>
        /// 获取房型
        /// </summary>
        /// <returns></returns>
        IList<MTourRoom> GetFangXing()
        {
            IList<MTourRoom> items = new List<MTourRoom>();
            string[] s1 = Utils.GetFormValues("txtFangXing");
            string[] s2 = Utils.GetFormValues("txtFangXingShuLiang");

            if (s1 != null && s1.Length > 0)
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    if (string.IsNullOrEmpty(s1[i]) || Utils.GetInt(s2[i]) <= 0) continue;

                    var item = new MTourRoom();
                    item.Num = Utils.GetInt(s2[i]);
                    item.RoomId = s1[i];
                    items.Add(item);
                }
            }


            return items;
        }

        /// <summary>
        /// get fujian1
        /// </summary>
        /// <returns></returns>
        IList<MTourFile> GetFuJian1()
        {
            IList<MTourFile> items = new List<MTourFile>();

            var files1 = txtFuJian1.Files;
            var files2 = txtFuJian1.YuanFiles;

            IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();

            if (files1 != null && files1.Count > 0)
            {
                foreach (var item in files1)
                {
                    files.Add(item);
                }
            }
            if (files2 != null && files2.Count > 0)
            {
                foreach (var item in files2)
                {
                    files.Add(item);
                }
            }

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    items.Add(new MTourFile() { FilePath = file.FilePath });
                }
            }

            return items;
        }

        /// <summary>
        /// get fujian2
        /// </summary>
        /// <returns></returns>
        string GetFuJian2()
        {
            var files1 = txtFuJian2.Files;
            var files2 = txtFuJian2.YuanFiles;

            if (files1 != null && files1.Count > 0)
            {
                return files1[0].FilePath;
            }

            if (files2 != null && files2.Count > 0)
            {
                return files2[0].FilePath;
            }

            return string.Empty;
        }

        #endregion
    }
}
