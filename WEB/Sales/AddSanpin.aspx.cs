using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.ComStructure;
using System.Text;
using EyouSoft.Model.ComStructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using EyouSoft.Common.Page;
using EyouSoft.Model.HTourStructure;

namespace EyouSoft.Web.Sales
{
    /// <summary>
    /// 增加/修改散拼计划
    /// 创建人：田想兵 创建日期：2011.9.13
    /// </summary>
    public partial class AddSanpin : BackPage
    {
        #region attributes
        bool Privs_Insert = false;
        bool Privs_Update = false;
        string TourId = string.Empty;
        protected string allcitys = string.Empty;

        protected string CountryId = "0";
        protected string TuanXing = "0";
        protected string AreaId = "";
        protected string tourMode = "1";
        protected string act = "";
        protected bool IsParent = Utils.GetQueryStringValue("IsParent").ToLower()=="true";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Utils.GetQueryStringValue("act");
            #region 用户初始化
            this.PriceStand1.CompanyID = SiteUserInfo.CompanyId;
            this.PriceStand1.InitTour = false;
            #endregion
            TourId = Utils.GetQueryStringValue("id");

            if (TourId != "" && act != "add")
            {
                if (act == "update"&&!IsParent)
                {
                    this.phdSelectDate.Visible = false;
                    this.phdSelectDate1.Visible = true;
                }
            }


            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "save") BaoCun();

            InitArea();
            //InitFangXing();
            InitInfo();

        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Insert = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.团队中心_团队产品_新增);
            Privs_Update = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.团队中心_团队产品_修改);
        }

        /// <summary>
        /// init area
        /// </summary>
        void InitArea()
        {
            var items = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(SiteUserInfo.CompanyId);
            StringBuilder s = new StringBuilder();

            s.AppendFormat("<option value='{0}'>{1}</option>", string.Empty, "请选择");
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.LngType != EyouSoft.Model.EnumType.SysStructure.LngType.中文) continue;
                    s.AppendFormat("<option value='{0}'>{1}</option>", item.AreaId, item.AreaName);
                }
            }

            ltrArea.Text = s.ToString();
        }

        ///// <summary>
        ///// init fangxing
        ///// </summary>
        //void InitFangXing()
        //{
        //    StringBuilder s = new StringBuilder();
        //    StringBuilder s1 = new StringBuilder();
        //    var items = new EyouSoft.BLL.SysStructure.BSysRoom().GetRooms(SiteUserInfo.CompanyId);

        //    if (items != null && items.Count != 0)
        //    {
        //        s1.Append("<option value=''>请选择</option>");
        //        foreach (var item in items)
        //        {
        //            s1.AppendFormat("<option value='{0}'>{1}</option>", item.RoomId, item.TypeName);
        //        }

        //        int i = 0;
        //        foreach (var item in items)
        //        {
        //            if (i > 0 && i % 4 == 0) s.Append("<br/>");
        //            s.Append("房型：<select  name='txtFangXing' class='inputselect'>");
        //            s.Append(s1.ToString());
        //            s.Append("</select>");
        //            s.Append("&nbsp;间数：<input name='txtFangXingShuLiang' type='text'  class='inputtext formsize40' />&nbsp;&nbsp;");
        //            i++;
        //        }
        //    }

        //    ltrFangXing.Text = s.ToString();
        //}

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            TuanXingCheng1.CompanyID = SiteUserInfo.CompanyId;
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

            SellsSelect1.SellsID = SiteUserInfo.UserId;
            SellsSelect1.SellsName = SiteUserInfo.Name;

            if (string.IsNullOrEmpty(TourId))
            {
                return;
            }
            // this.PhDinnerPrice.Visible = false;
            var info = new EyouSoft.BLL.HTourStructure.BTour().GetSanPinModel(TourId);
            if (info == null) RCWE("异常请求");

            txtJiDiaoYuan.IsShowSelect = info.TourStatus < EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置;
            TuanXing = ((int)info.TourMode).ToString();
            hidTourModeValue.Value = ((int)info.TourMode).ToString();
            CountryId = info.CountryId.ToString();
            AreaId = info.AreaId.ToString();
            tourMode = ((int)info.OutQuoteType).ToString();
            //if (act == "update")
            //{
            //    this.txtTuanXing.Attributes.Add("disabled", "true");
            //    this.txtTuanXing.Attributes.Add("background-color", "#dadada");
            //}
            //txtTourCode.Value = info.TourCode;
            //txtKeHuTourCode.Value = info.TourCustomerCode;
            txt_RouteName.Value = info.RouteName;
            txt_Days.Text = info.TourDays.ToString();
            //txtKeHu.CustomerUnitId = info.BuyCompanyID;
            //txtKeHu.CustomerUnitName = info.BuyCompanyName;
            //txtLDate.Value = info.LDate.ToString("yyyy-MM-dd");
            txtGetCity.Value = info.ArriveCity;
            txtGetCityTime.Value = info.ArriveCityFlight;
            //txtRDate.Value = info.RDate.ToString("yyyy-MM-dd");
            txtOutCity.Value = info.LeaveCity;
            txtOutCityTime.Value = info.LeaveCityFlight;
            SellsSelect1.SellsID = info.SellerId;
            SellsSelect1.SellsName = info.SellerName;

            //发班周期
            this.lblLeaveDate.Text = "共选择了:"+(string.IsNullOrEmpty(info.ZiTuanLDates)?0:info.ZiTuanLDates.Split(',').Length)+"个日期";
            this.hideLeaveDate.Value = info.ZiTuanLDates;

            txtLDate.Value = UtilsCommons.GetDateString(info.LDate, this.ProviderToDate);

            txtPeopleCount.Text = info.PlanPeopleNumber.ToString();
            //txtLeavePeopleNumber.Text = info.LeavePeopleNumber.ToString();

            this.PriceStand1.InitMode = false;

            this.PriceStand1.SetPriceStandard = info.MTourPriceStandard;

            cbxDistribution.Checked = info.IsShowDistribution;


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

            //txtCR.Value = info.Adults.ToString();
            //txtET.Value = info.Childs.ToString();
            //txtLD.Value = info.Leaders.ToString();
            //txtSiPei.Value = info.SiPei.ToString();

            RegisterScript(string.Format("var fangXing={0};", Newtonsoft.Json.JsonConvert.SerializeObject(info.TourRoomList)));

            if (info.TourFileList != null && info.TourFileList.Count > 0)
            {
                var files = new List<global::Web.UserControl.MFileInfo>();
                foreach (var item in info.TourFileList)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }
                UploadControl2.YuanFiles = files;
            }

            if (!string.IsNullOrEmpty(info.TourFile))
            {
                var files = new List<global::Web.UserControl.MFileInfo>();
                files.Add(new global::Web.UserControl.MFileInfo() { FilePath = info.TourFile });
                txtFuJian2.YuanFiles = files;
            }

            txtPlanContent.Text = info.JourneySpot;
            if (info.TourPlanList != null && info.TourPlanList.Count > 0)
            {
                TuanXingCheng1.SetPlanList = info.TourPlanList;
            }

            TuanFengWeiCan1.SetFengWeiCan = info.TourFootList;
            TuanZiFei1.SetZiFei = info.TourSelfPayList;
            TuanZengSong1.SetZengSong = info.TourGiveList;
            TuanXiaoFei1.SetXiaoFei = info.TourTipList;
            TuanDiJie1.DiJies = info.TourDiJieList;

            //var youKes = new EyouSoft.BLL.HTourStructure.BTourOrder().GetTourOrderTraveller(TourId);
            //YouKe.YouKes = youKes;

            //txtNeiBuXinXi.Value = info.InsideInformation;
            //txtSpecificRequire.Text = info.SpecificRequire;
            txtJourney.Text = info.TravelNote;
            this.SelectJourney1.SetTourJourneyList = info.TourJourneyList;
            this.SelectJourneySpot1.SetTourJourneyList = info.TourJourneyList;




            #region 文件信息
            if (info.TourFileList != null && info.TourFileList.Count > 0)
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();


                foreach (var item in info.TourFileList)
                {
                    files.Add(new global::Web.UserControl.MFileInfo() { FilePath = item.FilePath });
                }

                UploadControl2.YuanFiles = files;
            }

            if (!string.IsNullOrEmpty(info.TourFile))
            {
                IList<global::Web.UserControl.MFileInfo> files = new List<global::Web.UserControl.MFileInfo>();

                files.Add(new global::Web.UserControl.MFileInfo() { FilePath = info.TourFile });

                txtFuJian2.YuanFiles = files;
            }
            #endregion

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
            //出团时间
            string[] successDateBegin = Utils.GetFormValue(this.hideLeaveDate.UniqueID).Split(',');

            string act = Utils.GetQueryStringValue("act");

            #region 表单后台验证
            if (info.AreaId == 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "请选择线路区域!"));
            }
            if (info.RouteName == "")
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请输入线路名称!"));
            }
            if (info.TourDays == 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请输入天数!"));
            }
            if (info.PlanPeopleNumber == 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请输入预控人数!"));
            }

            if (info.SellerId == "")
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请输入销售员!"));
            }
            if (act != "")
            {
                if (Utils.GetDateTime(Utils.GetFormValue(txtLDate.UniqueID)) == null && act == "update")
                {
                    RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请选择出团日期!"));
                }
                else if (successDateBegin.Length == 0 && (act == "add"||act=="copy"))
                {
                    RCWE(UtilsCommons.AjaxReturnJson("0", "<br />请选择出团日期!"));
                }
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
            }
            #endregion


            int bllRetCode = 0;

            if (act == "copy" || act == "add")
            {
                //先添加模版团
                var parentid = string.Empty;
                info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼模版团;
                bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().AddSanPin(info,ref parentid);

                //再添加子团
                for (int i = 0; i < successDateBegin.Length; i++)
                {
                    info.ParentId = parentid;
                    info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品;
                    info.LDate = Utils.GetDateTime(successDateBegin[i]);
                    info.RDate = Utils.GetDateTime(successDateBegin[i]).AddDays(info.TourDays-1);
                    bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().AddSanPin(info);

                }
            }
            if (act == "update" && info.TourId != "")
            {
                if (!this.IsParent)
                {
                    info.LDate = Utils.GetDateTime(Utils.GetFormValue(txtLDate.UniqueID));
                    info.RDate = Utils.GetDateTime(Utils.GetFormValue(txtLDate.UniqueID)).AddDays(info.TourDays - 1);
                    bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().UpdateSanPin(info);
                }
                else
                {
                    var b = new EyouSoft.BLL.HTourStructure.BTour();
                    //先更新模版团
                    info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼模版团;
                    info.ZiTuanLDates = Utils.GetFormValue(this.hideLeaveDate.UniqueID);
                    bllRetCode = b.UpdateSanPin(info);
                    
                    //再更新子团
                    for (int i = 0; i < successDateBegin.Length; i++)
                    {
                        var m = b.GetSanPinModel(this.TourId, !this.IsParent, Utils.GetDateTime(successDateBegin[i]));
                        if (m != null)
                        {
                            info.TourId = m.TourId;
                            info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品;
                            info.LDate = Utils.GetDateTime(successDateBegin[i]);
                            info.RDate = Utils.GetDateTime(successDateBegin[i]).AddDays(info.TourDays - 1);
                            bllRetCode = new EyouSoft.BLL.HTourStructure.BTour().UpdateSanPin(info);
                        }
                        else
                        {
                            info.ParentId = this.TourId;
                            info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品;
                            info.LDate = Utils.GetDateTime(successDateBegin[i]);
                            info.RDate = Utils.GetDateTime(successDateBegin[i]).AddDays(info.TourDays - 1);
                            bllRetCode = b.AddSanPin(info);
                        }
                    }
                }
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

            //info.Adults = Utils.GetInt(Utils.GetFormValue(txtCR.UniqueID));
            info.AreaId = Utils.GetInt(Utils.GetFormValue("txtAreaId"));
            info.AreaName = string.Empty;
            info.ArriveCity = Utils.GetFormValue(txtGetCity.UniqueID);
            info.ArriveCityFlight = Utils.GetFormValue(txtGetCityTime.UniqueID);
            //info.BuyCompanyID = Utils.GetFormValue(txtKeHu.ClientNameKHBH);
            //info.BuyCompanyName = Utils.GetFormValue(txtKeHu.ClientNameKHMC);
            //info.Childs = Utils.GetInt(Utils.GetFormValue(txtET.UniqueID));
            info.CompanyId = SiteUserInfo.CompanyId;
            info.CountryId = Utils.GetInt(Utils.GetFormValue("txtCountryId"));
            //info.GuideIncome = Utils.GetDecimal(Utils.GetFormValue(txtDaoYouShouKuanJinE.UniqueID));
            info.Guides = null;
            //info.InsideInformation = Utils.GetFormValue(txtNeiBuXinXi.UniqueID);
            info.JourneySpot = Utils.GetFormEditorValue(txtPlanContent.UniqueID);
            //info.LDate = Utils.GetDateTime(Utils.GetFormValue(txtLDate.UniqueID));
            //info.Leaders = Utils.GetInt(Utils.GetFormValue(txtLD.UniqueID));
            //info.SiPei = Utils.GetInt(Utils.GetFormValue(txtSiPei.UniqueID));
            info.LeaveCity = Utils.GetFormValue(txtOutCity.UniqueID);
            info.LeaveCityFlight = Utils.GetFormValue(txtOutCityTime.UniqueID);
            info.LngType = EyouSoft.Model.EnumType.SysStructure.LngType.中文;
            info.Operator = SiteUserInfo.Name; ;
            info.OperatorDeptId = SiteUserInfo.DeptId;
            info.OperatorId = SiteUserInfo.UserId;
            info.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)Utils.GetInt(Utils.GetFormValue("rdTourQuoteType"));
            info.Planers = string.Empty;
            //info.QuoteRemark = Utils.GetFormValue(TxtQuoteRemark.UniqueID);
            //info.RDate = Utils.GetDateTime(Utils.GetFormValue(txtRDate.UniqueID));
            info.RouteName = Utils.GetFormValue(txt_RouteName.UniqueID);
            //info.SalerIncome = Utils.GetDecimal(Utils.GetFormValue(txtXiaoShouShouKuanJinE.UniqueID));
            info.SellerDeptId = 0;
            info.SellerId = Utils.GetFormValue(SellsSelect1.SellsIDClient);
            info.SellerDeptId = new EyouSoft.BLL.ComStructure.BComUser().GetModel(info.SellerId, SiteUserInfo.CompanyId).DeptId;
            info.SellerName = Utils.GetFormValue(SellsSelect1.SellsNameClient);
            //info.SpecificRequire = Utils.GetFormValue(txtSpecificRequire.UniqueID);
            //info.SumPrice = Utils.GetDecimal(Utils.GetFormValue(txtHeJiJinE.UniqueID));
            info.TourCode = string.Empty;
            //info.TourCostList = GetTourCost(info.OutQuoteType);
            //info.TourCustomerCode = Utils.GetFormValue(txtKeHuTourCode.UniqueID);
            info.TourDays = Utils.GetInt(Utils.GetFormValue(txt_Days.UniqueID));
            info.TourDiJieList = TuanDiJie1.GetFormInfo();
            info.TourFile = GetFuJian2();
            info.TourFileList = GetFuJian1();
            info.TourFootList = TuanFengWeiCan1.GetFengWeiList();
            info.TourGiveList = TuanZengSong1.GetDataList();
            info.TourId = TourId;
            info.TourMode = (EyouSoft.Model.EnumType.TourStructure.TourMode)Utils.GetInt(Utils.GetFormValue(this.hidTourModeValue.UniqueID));
            //info.TourPlanerList = GetJiDiao();
            info.TourPlanItemList = null;
            info.TourPlanList = TuanXingCheng1.GetXingChengs();
            info.TourPlanRemark = null;
            //info.TourPriceList = GetTourPrice(info.OutQuoteType);
            //info.TourRoomList = GetFangXing();
            info.TourSelfPayList = TuanZiFei1.GetDataList();
            //info.TourShopList = GetShoplist();
            info.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划;
            info.TourSureStatus = EyouSoft.Model.EnumType.TourStructure.TourSureStatus.未确认;
            info.TourTipList = TuanXiaoFei1.GetDataList();
            info.TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品;
            info.TravelNote = Utils.GetFormEditorValue(txtJourney.UniqueID);
            info.UpdateTime = DateTime.Now;
            info.SalerIncome = info.SumPrice - info.GuideIncome;
            info.TourJourneyList = this.SelectJourney1.GetTourJourneyList();
            //info.LeavePeopleNumber = Utils.GetInt(Utils.GetFormValue(txtLeavePeopleNumber.UniqueID));
            info.PlanPeopleNumber = Utils.GetInt(Utils.GetFormValue(txtPeopleCount.UniqueID));
            var i2 = this.SelectJourneySpot1.GetTourJourneyList();
            foreach (var item in i2)
            {
                info.TourJourneyList.Add(item);
            }
            info.MTourPriceStandard = GetPriceStandard();
            info.TourShouKeStatus = (int)EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.报名中;

            info.IsShowDistribution = Utils.GetFormValue(this.cbxDistribution.UniqueID) == "on" ? true : false;
            info.TourPlanerList = GetJiDiao();

            return info;
        }



        #region 获得报价等级
        /// <summary>
        /// 获得散拼报价等级
        /// </summary>
        /// <returns></returns>
        public static IList<EyouSoft.Model.TourStructure.MTourPriceStandard> GetPriceStandard()
        {
            IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list = null;
            //获得页面等级
            string[] standardAllList = Utils.GetFormValues("hide_PriceStand_LevelData");
            //获得选择的类型
            string[] priceStandardType = Utils.GetFormValues("sel_PriceStandard_type");
            if (priceStandardType.Length > 0)
            {
                list = new List<EyouSoft.Model.TourStructure.MTourPriceStandard>();
                for (int i = 0; i < priceStandardType.Length; i++)
                {
                    EyouSoft.Model.TourStructure.MTourPriceStandard m = new EyouSoft.Model.TourStructure.MTourPriceStandard();
                    m.Standard = Utils.GetInt(priceStandardType[i]);

                    //等级
                    m.PriceLevel = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();
                    //获得页面等级数量
                    if (standardAllList.Length > 0)
                    {
                        for (int j = 0; j < standardAllList.Length; j++)
                        {
                            EyouSoft.Model.TourStructure.MTourPriceLevel l = new EyouSoft.Model.TourStructure.MTourPriceLevel();
                            string[] priceLevelArray = standardAllList[j].Split('|');
                            l.AdultPrice = Utils.GetDecimal(Utils.GetFormValue("txt_PriceStand_Adult_" + priceLevelArray[0] + "_" + i.ToString()));
                            l.ChildPrice = Utils.GetDecimal(Utils.GetFormValue("txt_PriceStand_Child_" + priceLevelArray[0] + "_" + i.ToString()));

                            l.LevelId = Utils.GetInt(priceLevelArray[0]);
                            l.LevelName = priceLevelArray[1];
                            l.LevType = (EyouSoft.Model.EnumType.ComStructure.LevType)Utils.GetInt(priceLevelArray[2]);
                            l.CostMode = EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格;
                            m.PriceLevel.Add(l);
                        }
                    }
                    int count = list.Where(p => p.Standard == m.Standard).Count();
                    if (count == 0)
                    {
                        list.Add(m);
                    }
                }
            }
            return list;
        }
        #endregion


        /// <summary>
        /// 获取计调员
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.HTourStructure.MTourPlaner> GetJiDiao()
        {
            IList<EyouSoft.Model.HTourStructure.MTourPlaner> items = new List<EyouSoft.Model.HTourStructure.MTourPlaner>();
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

                    var _item = new EyouSoft.Model.HTourStructure.MTourPlaner();
                    _item.PlanerId = item;
                    _item.Planer = uinfo.ContactName;
                    _item.PlanerDeptId = uinfo.DeptId;

                    items.Add(_item);
                }

            }

            return items;
        }


        /// <summary>
        /// 获取购物店信息
        /// </summary>
        /// <returns></returns>
        IList<MTourShop> GetShoplist()
        {
            IList<MTourShop> list = new List<MTourShop>();
            string[] shopid = Utils.GetFormValue("ckshop").Split(',');
            string[] shopname = Utils.GetFormValue("shopname").Split(',');

            if (shopid.Length > 0 && shopname.Length == shopid.Length)
            {
                for (int i = 0; i < shopid.Length; i++)
                {
                    if (!string.IsNullOrEmpty(shopid[i]))
                        list.Add(new MTourShop { ShopId = shopid[i], ShopName = shopname[i] });
                }
            }
            return list;
        }

        /// <summary>
        /// get fujian1
        /// </summary>
        /// <returns></returns>
        IList<MTourFile> GetFuJian1()
        {
            IList<MTourFile> items = new List<MTourFile>();

            var files1 = UploadControl2.Files;
            var files2 = UploadControl2.YuanFiles;

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
