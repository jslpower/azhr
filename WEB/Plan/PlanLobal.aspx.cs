using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace EyouSoft.Web.Plan
{
    using EyouSoft.Model.EnumType.PlanStructure;

    /// <summary>
    /// 计调安排—全局计调
    /// </summary>
    public partial class PlanLobal : EyouSoft.Common.Page.BackPage
    {
        //计调项数据列表标识
        protected string PlanItemsDataListIsEnpty = string.Empty;

        //登录人
        protected string UserId = string.Empty;
        //是否是团队计调员
        protected bool ret = false;
        //地接社确认单
        protected string dijieshePrintUrl = string.Empty;
        //导游任务单
        protected string daoyouPrintUrl = string.Empty;
        //酒店确认单
        protected string hotelPrintUrl = string.Empty;
        //车队行程单
        protected string carPrintUrl = string.Empty;
        //机票订单 //火车票订单//汽车票订单//游船确认单
        protected string querenAirUrl = string.Empty;
        protected string querenTrainUrl = string.Empty;
        protected string querenBusUrl = string.Empty;
        protected string querenchinaUrl = string.Empty;
        //景点通知单
        protected string jingdianPrintUrl = string.Empty;
        //用餐
        protected string yongcanPrintUrl = string.Empty;
        //购物
        protected string gouwuPrintUrl = string.Empty;
        //其它
        protected string qitaPrintUrl = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            UserId = this.SiteUserInfo.UserId;

            //初始化菜单控件css
            this.PlanConfigMenu1.IndexClass = "2";
            PlanConfigMenu1.CompanyId = SiteUserInfo.CompanyId;

            #region 确认单
            dijieshePrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.地接确认单);

            daoyouPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.导游任务单);

            hotelPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.酒店确认单);

            carPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.用车确认单);

            querenAirUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.机票确认单);

            querenTrainUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.火车确认单);

            querenBusUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.汽车确认单);

            jingdianPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.景点确认单);

            querenchinaUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.国内游轮确认单);

            yongcanPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订餐确认单);

            gouwuPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.购物确认单);

            qitaPrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.其它安排确认单);
            #endregion

            #region 处理AJAX请求
            //获取ajax请求 删除 修改 保存
            string doType = Utils.GetQueryStringValue("action");
            string planID = Utils.GetQueryStringValue("planID");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "config":
                        Response.Clear();
                        Response.Write(GlobalOpConfig(Utils.GetQueryStringValue("tourId")));
                        Response.End();
                        break;
                    //case "delete":
                    //    Response.Clear();
                    //    Response.Write(DeletePlanByID(planID));
                    //    Response.End();
                    //    break;
                    default: break;
                }
            }
            #endregion

            //团号
            string tourId = Utils.GetQueryStringValue("tourId");
            DataBindPlanList(tourId);
            DataInit(tourId);
        }

        #region 初始化
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit(string tourID)
        {
            //this.BtnglobalAction.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_全局配置完毕);
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.HTourStructure.MTour tourInfo = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourID);
                if (tourInfo != null)
                {
                    this.litTourCode.Text = tourInfo.TourCode;
                    if (!string.IsNullOrEmpty(tourInfo.AreaId.ToString()))
                    {
                        EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(tourInfo.AreaId, SiteUserInfo.CompanyId);
                        if (AreaModel != null)
                        {
                            this.litAreaName.Text = AreaModel.AreaName;
                        }
                        AreaModel = null;
                    }
                    this.litRouteName.Text = tourInfo.RouteName;
                    this.litDays.Text = tourInfo.TourDays.ToString();
                    this.litStartDate.Text = UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                    this.litPeoples.Text = "成人" + tourInfo.Adults.ToString() + "，儿童" + tourInfo.Childs.ToString() + "，领队" + tourInfo.Leaders.ToString();
                    this.litEndDate.Text = UtilsCommons.GetDateString(tourInfo.RDate, ProviderToDate);
                    //带团导游
                    this.litGuidNames.Text = tourInfo.Guides;
                    //销售员
                    this.litSellers.Text = tourInfo.SellerName;
                    //计调员
                    if (tourInfo.TourPlanerList != null && tourInfo.TourPlanerList.Count > 0)
                    {
                        string planerList = "";
                        foreach (var item in tourInfo.TourPlanerList)
                        {
                            planerList += item.Planer + ",";
                        }
                        this.litOperaters.Text = planerList.Trim(',');
                    }
                    //计调项
                    if (tourInfo.TourPlanItemList != null && tourInfo.TourPlanItemList.Count > 0)
                    {
                        for (int i = 0; i < tourInfo.TourPlanItemList.Count; i++)
                        {
                            if (i == tourInfo.TourPlanItemList.Count - 1)
                            {
                                this.litPlanItems.Text += "" + tourInfo.TourPlanItemList[i].PlanType.ToString() + "";
                            }
                            else
                            {
                                this.litPlanItems.Text += "" + tourInfo.TourPlanItemList[i].PlanType.ToString() + ",";
                            }
                        }
                    }

                    //团队状态
                    //if (ret)
                    //{
                    //    this.BtnglobalAction.Visible = !EyouSoft.Common.UtilsCommons.GetTourStatusByTourID(SiteUserInfo.CompanyId, tourID);
                    //}
                    //else
                    //{
                    //    this.BtnglobalAction.Visible = false;
                    //}

                }
            }
        }
        #endregion

        #region 绑定安排的计调项列表
        /// <summary>
        /// 绑定安排的计调项列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataBindPlanList(string tourID)
        {
            decimal totalsettle = 0;
            decimal t = 0;
            ListPower = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourID, SiteUserInfo.UserId);
            //地接
            IList<EyouSoft.Model.HPlanStructure.MPlan> listAyency = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, null, false, null, tourID);
            if (listAyency != null && listAyency.Count > 0)
            {
                this.tabAyencyView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接 + ",";
                this.repAyencyList.DataSource = listAyency;
                this.repAyencyList.DataBind();
                t=listAyency.Sum(m => m.Confirmation);
                totalsettle += t;
                this.TotalDiJie.Text = UtilsCommons.GetMoneyString(t,ProviderToMoney);
            }


            //导游
            IList<EyouSoft.Model.HPlanStructure.MPlan> listGuid = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, null, null, false, null, tourID);
            if (listGuid != null && listGuid.Count > 0)
            {
                this.tabGuidView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游 + ",";
                this.repGuidList.DataSource = listGuid;
                this.repGuidList.DataBind();
                t=listGuid.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalDaoYou.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }


            //酒店
            IList<EyouSoft.Model.HPlanStructure.MPlan> listHotel = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, null, false, null, tourID);
            if (listHotel != null && listHotel.Count > 0)
            {
                this.tabHotelView.Visible = true;
                this.repHotelList.DataSource = listHotel;
                this.repHotelList.DataBind();
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 + ",";
                t=listHotel.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalJiuDian.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //车队 
            IList<EyouSoft.Model.HPlanStructure.MPlan> listCars = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null, null, false, null, tourID);
            if (listCars != null && listCars.Count > 0)
            {
                this.tabCarView.Visible = true;
                this.repCarlist.DataSource = listCars;
                this.repCarlist.DataBind();
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车 + ",";
                t=listCars.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalCheDui.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //机票
            IList<EyouSoft.Model.HPlanStructure.MPlan> listAirs = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, null, null, false, null, tourID);
            if (listAirs != null && listAirs.Count > 0)
            {
                this.tabAirView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机 + ",";
                this.repAirList.DataSource = listAirs;
                this.repAirList.DataBind();
                t=listAirs.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalJiPiao.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //火车
            IList<EyouSoft.Model.HPlanStructure.MPlan> listTrains = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, null, null, false, null, tourID);
            if (listTrains != null && listTrains.Count > 0)
            {
                this.tabTrainView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车 + ",";
                this.reptrainList.DataSource = listTrains;
                this.reptrainList.DataBind();
                t=listTrains.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalHuoChe.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //汽车
            IList<EyouSoft.Model.HPlanStructure.MPlan> listBus = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, null, null, false, null, tourID);
            if (listBus != null && listBus.Count > 0)
            {
                this.tabBusView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车 + ",";
                this.repBusList.DataSource = listBus;
                this.repBusList.DataBind();
                t=listBus.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalQiChe.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //景点
            IList<EyouSoft.Model.HPlanStructure.MPlan> listAtts = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null, null, false, null, tourID);
            if (listAtts != null && listAtts.Count > 0)
            {
                this.tabAttrView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 + ",";
                this.repAttrList.DataSource = listAtts;
                this.repAttrList.DataBind();
                t=listAtts.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalJingDian.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //轮船
            IList<EyouSoft.Model.HPlanStructure.MPlan> listShipChina = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, null, null, false, null, tourID);
            if (listShipChina != null && listShipChina.Count > 0)
            {
                this.tabShipChinaView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船 + ",";
                this.repChinaShipList.DataSource = listShipChina;
                this.repChinaShipList.DataBind();
                t=listShipChina.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalLunChuan.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //用餐 
            IList<EyouSoft.Model.HPlanStructure.MPlan> listDin = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null, null, false, null, tourID);
            if (listDin != null && listDin.Count > 0)
            {
                this.tabDinView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐 + ",";
                this.repDinList.DataSource = listDin;
                this.repDinList.DataBind();
                t=listDin.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalYongCan.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //购物
            IList<EyouSoft.Model.HPlanStructure.MPlan> listShop = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, null, null, false, null, tourID);
            if (listShop != null && listShop.Count > 0)
            {
                this.tabShopView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物 + ",";
                this.repShopList.DataSource = listShop;
                this.repShopList.DataBind();
            }

            //领料
            IList<EyouSoft.Model.HPlanStructure.MPlan> listPick = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, null, false, null, tourID);
            if (listPick != null && listPick.Count > 0)
            {
                this.tabPickView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料 + ",";
                this.repPickList.DataSource = listPick;
                this.repPickList.DataBind();
                t=listPick.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalLingLiao.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            //其它 
            IList<EyouSoft.Model.HPlanStructure.MPlan> listOther = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, null, false, null, tourID);
            if (listOther != null && listOther.Count > 0)
            {
                this.tabOtherView.Visible = true;
                PlanItemsDataListIsEnpty += "" + (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它 + ",";
                this.repOtherList.DataSource = listOther;
                this.repOtherList.DataBind();
                t=listOther.Sum(m => m.Confirmation);
                totalsettle+=t;
                this.TotalQiTa.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
            }

            this.TotalSettle.Text = UtilsCommons.GetMoneyString(totalsettle, ProviderToMoney);
            var o = new BLL.TourStructure.BTourOrder().GetTourOrderSummaryByTourId(tourID);
            if (o != null && o.Count > 0)
            {
                t = o.Sum(m => m.ConfirmMoney);
                this.TotalSell.Text = UtilsCommons.GetMoneyString(t, ProviderToMoney);
                this.LiRun.Text = UtilsCommons.GetMoneyString(t - totalsettle, ProviderToMoney);
            }

            //当所有安排的计调项数据列表都为null的时候，就隐藏全部配置按钮
            //if (PlanItemsDataListIsEnpty != "")
            //{
            //    this.BtnglobalAction.Visible = true;
            //}
            //else
            //{
            //    this.BtnglobalAction.Visible = false;
            //}
        }

        /// <summary>
        /// 安排明细
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetAPMX(object list, string type)
        {
            StringBuilder str = new StringBuilder();
            if (list != null)
            {
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}间<br/>", lis[i].RoomType, lis[i].Quantity);
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}{2}&nbsp;/&nbsp;{3}<br/>", lis[i].RoomType, lis[i].Quantity, lis[i].PriceType.ToString(), lis[i].Total.ToString("C2"));
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanCar>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{2}天数:{0}&nbsp;/&nbsp;金额:{1}<br/>", lis[i].Days, lis[i].SumPrice.ToString("C2"), string.IsNullOrEmpty(lis[i].Models) ? "" : "车型:"+lis[i].Models + "&nbsp;/&nbsp;");
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机.ToString()
                        ||type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString()
                        ||type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString()
                        ||type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}&nbsp;/&nbsp;{1}&nbsp;/&nbsp;{2}<br/>", lis[i].Numbers, lis[i].SumPrice.ToString("C2"), lis[i].PepolePayNum);
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanAttractions>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}:{1}<br/>", lis[i].VisitTime.Value.ToString("yyyy-MM-dd"), lis[i].Attractions);
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐.ToString())
                {
                    var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanDining>)list;
                    for (int i = 0; i < lis.Count; i++)
                    {
                        str.AppendFormat("{0}:成人{1}人,儿童{2}人,{3}桌&nbsp;&nbsp;{4}<br/>", lis[i].MenuName, lis[i].AdultNumber,lis[i].ChildNumber,lis[i].TableNumber,lis[i].SumPrice.ToString("C2"));
                    }
                }

            }
            return str.ToString();
        }
        #endregion

        #region 全局计调配置
        /// <summary>
        /// 全局计调配置
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <returns></returns>
        protected string GlobalOpConfig(string tourID)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(tourID))
            {
                var result = new EyouSoft.BLL.HPlanStructure.BPlan().DoGlobal(tourID, PlanState.已落实,null);
                switch (result)
                {
                    case 0:
                        msg = UtilsCommons.AjaxReturnJson("0", "配置失败！"); break;
                    case 1:
                        msg = UtilsCommons.AjaxReturnJson("1", "配置成功！"); break;
                }
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "您落实的团队不存在！");
            }
            return msg;
        }
        #endregion

        #region 删除计调项
        /// <summary>
        /// 删除计调项
        /// </summary>
        /// <param name="planID">计调id</param>
        /// <returns></returns>
        protected string DeletePlanByID(string planID)
        {
            string setMsg = string.Empty;
            if (!string.IsNullOrEmpty(planID))
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planID))
                {
                    setMsg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    setMsg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return setMsg;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_栏目, false);
                return;
            }
        }
        #endregion
    }
}
