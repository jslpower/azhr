using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Text;


namespace EyouSoft.Web.PrintPage
{
    public partial class DaoYouPrint : BackPage
    {
        #region attributes
        protected int dijie = 0, hotel = 0,
            chedui = 0, plane = 0, train = 0,
            bus = 0, jingdian = 0, shewaichuan = 0,
            guoneichuan = 0, yongcan = 0, gouwu = 0,
            lingliao = 0, qita = 0, guid = 0;

        /// <summary>
        /// 安排编号
        /// </summary>
        string AnPaiId = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourId");
            AnPaiId = Utils.GetQueryStringValue("anpaiid");

            //  if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitInfo();
            //InitAnPaiInfo();
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitInfo()
        {
            string orderid = string.Empty;
            EyouSoft.BLL.HTourStructure.BTour bll = new EyouSoft.BLL.HTourStructure.BTour();

            EyouSoft.Model.HTourStructure.MTour teamModel = bll.GetTourModel(TourId);

            // if (baseModel == null) RCWE("异常请求");

            //EyouSoft.Model.HTourStructure.MTour teamModel = baseModel;
            //switch (baseModel.TourType)
            //{
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
            //    case EyouSoft.Model.EnumType.TourStructure.TourType.特价产品:

            //        break;
            //    default:
            //        break;
            //}

            #region 订单信息

            EyouSoft.BLL.TourStructure.BTourOrder bllorder = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MOrderSum ordersum = new EyouSoft.Model.TourStructure.MOrderSum();
            IList<EyouSoft.Model.TourStructure.MTourOrder> MtourOrder = bllorder.GetTourOrderListById(TourId, ref ordersum);
            if (MtourOrder != null)
            {
                MtourOrder = MtourOrder.Where(c => c.OrderStatus == EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交).ToList();
            }
            if (MtourOrder != null && MtourOrder.Count > 0)
            {
                this.rpt_OrderinfoList.DataSource = MtourOrder;
                this.rpt_OrderinfoList.DataBind();
            }
            else
            {
                this.ph_rpt_OrderinfoList.Visible = false;
            }
            #endregion

            #region 团队信息
            if (teamModel != null)
            {
                this.lbRouteName.Text = teamModel.RouteName;
                this.lbTourCode.Text = teamModel.TourCode;
                this.lbdayCount.Text = teamModel.TourDays.ToString();
                this.lbstarttime.Text = UtilsCommons.GetDateString(teamModel.LDate, ProviderToDate);
                this.lbendtime.Text = UtilsCommons.GetDateString(teamModel.RDate, ProviderToDate);
                this.lbstartstand.Text = teamModel.ArriveCity;
                this.lbendstand.Text = teamModel.LeaveCity;
                //if (teamModel.Guides!= null )
                //{
                //    string guidelist = string.Empty;
                //    for (int i = 0; i < teamModel.GuideList.Count; i++)
                //    {
                //        if (i == teamModel.GuideList.Count - 1)
                //        {
                //            guidelist += teamModel.GuideList[i].Name;
                //        }
                //        else
                //        {
                //            guidelist += teamModel.GuideList[i].Name + "、";
                //        }
                //    }
                //   this.lbguidename.Text = guidelist;
                // }
                this.lbguidename.Text = teamModel.Guides;

                this.lbpeoplecount.Text = teamModel.Adults.ToString() + "+" + teamModel.Childs.ToString() + "+" + teamModel.Leaders.ToString();
                if (teamModel.TourPlanerList != null && teamModel.TourPlanerList.Count > 0)
                {
                    string planerlist = string.Empty;
                    for (int i = 0; i < teamModel.TourPlanerList.Count; i++)
                    {
                        if (i == teamModel.TourPlanerList.Count - 1)
                        {
                            planerlist += teamModel.TourPlanerList[i].Planer;
                        }
                        else
                        {
                            planerlist += teamModel.TourPlanerList[i].Planer + "、";
                        }
                    }

                    var jiDiaoInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(teamModel.TourPlanerList[0].PlanerId, CurrentUserCompanyID);
                    if (jiDiaoInfo != null)
                    {
                        planerlist += " " + jiDiaoInfo.ContactMobile + " " + jiDiaoInfo.ContactTel;
                    }
                    this.lbplander.Text = planerlist;
                }
                //if (teamModel.SaleInfo != null)
                //{
                //    var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(teamModel.SaleInfo.SellerId, CurrentUserCompanyID);
                //    if (xiaoShouYuanInfo != null)
                //    {
                //        teamModel.SaleInfo.Mobile = xiaoShouYuanInfo.ContactMobile;
                //        teamModel.SaleInfo.Phone = xiaoShouYuanInfo.ContactTel;
                //    }
                var xiaoShouYuanInfo = new EyouSoft.BLL.ComStructure.BComUser().GetModel(teamModel.SellerId, CurrentUserCompanyID);
                if (xiaoShouYuanInfo != null)
                {
                    this.lbsellerinfo.Text = xiaoShouYuanInfo.ContactName + " " + xiaoShouYuanInfo.ContactMobile + " " + xiaoShouYuanInfo.ContactTel;
                }

                //}
            }
            else
            {
                return;
            }
            #endregion

            #region 计调信息
            EyouSoft.BLL.HPlanStructure.BPlan bllPlan = new EyouSoft.BLL.HPlanStructure.BPlan();
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo planinfo = bllPlan.GetGuidePrint(TourId);


            #region 导游安排接待行程
            if (!string.IsNullOrEmpty(planinfo.ReceiveJourney))
            {
                TReceiveJourney.Visible = true;
                this.lbReceiveJourney.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.ReceiveJourney);
            }
            else
            {
                TReceiveJourney.Visible = false;
            }
            #endregion

            #region 导游安排服务标准
            if (!string.IsNullOrEmpty(planinfo.ServiceStandard))
            {
                TService.Visible = true;
                this.lbServiceStandard.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.ServiceStandard);
            }
            else
            {
                TService.Visible = false;
            }
            #endregion

            #region 导游须知
            if (!string.IsNullOrEmpty(planinfo.GuideNotes))
            {
                TGuideNote.Visible = true;
                this.lbGuidNote.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(planinfo.GuideNotes);
            }
            else
            {
                this.TGuideNote.Visible = false;
            }
            #endregion

            #region 导游支付订单信息

            #region 导游

            IList<EyouSoft.Model.HPlanStructure.MPlan> GuidePlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, planinfo.PaymentType, null, false, null, TourId);
            if (GuidePlanList != null && GuidePlanList.Count > 0)
            {
                this.guid = GuidePlanList.Count;
                this.rpt_guid.DataSource = GuidePlanList;
                this.rpt_guid.DataBind();
            }
            else
            {
                this.ph_guid.Visible = false;
            }

            #endregion

            #region 地接

            IList<EyouSoft.Model.HPlanStructure.MPlan> groundPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, null, false, null, TourId);
            if (groundPlanList != null && groundPlanList.Count > 0)
            {
                this.dijie = groundPlanList.Count;
                this.rpt_dijie.DataSource = groundPlanList;
                this.rpt_dijie.DataBind();
            }
            else
            {
                this.ph_dijie.Visible = false;
            }

            #endregion

            #region 飞机

            IList<EyouSoft.Model.HPlanStructure.MPlan> phanePlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, null, null, false, null, TourId);
            if (phanePlanList != null && phanePlanList.Count > 0)
            {
                this.plane = phanePlanList.Count;
                this.rpt_plane.DataSource = phanePlanList;
                this.rpt_plane.DataBind();
            }
            else
            {
                this.ph_plane.Visible = false;
            }

            #endregion

            #region 购物

            IList<EyouSoft.Model.HPlanStructure.MPlan> shopPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, null, null, false, null, TourId);
            if (shopPlanList != null && shopPlanList.Count > 0)
            {
                this.gouwu = shopPlanList.Count;
                this.rpt_gouwu.DataSource = shopPlanList;
                this.rpt_gouwu.DataBind();
            }
            else
            {
                this.ph_gouwu.Visible = false;
            }

            #endregion

            #region 国内游轮

            IList<EyouSoft.Model.HPlanStructure.MPlan> InshipPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, null, null, false, null, TourId);
            if (InshipPlanList != null && InshipPlanList.Count > 0)
            {
                this.guoneichuan = InshipPlanList.Count;
                this.rpt_guoneichuan.DataSource = InshipPlanList;
                this.rpt_guoneichuan.DataBind();
            }
            else
            {
                this.ph_guoneichuan.Visible = false;
            }

            #endregion

            #region 火车

            IList<EyouSoft.Model.HPlanStructure.MPlan> trainPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, null, null, false, null, TourId);
            if (trainPlanList != null && trainPlanList.Count > 0)
            {
                this.train = trainPlanList.Count;
                this.rpt_train.DataSource = trainPlanList;
                this.rpt_train.DataBind();
            }
            else
            {
                this.ph_train.Visible = false;
            }

            #endregion

            #region 景点

            IList<EyouSoft.Model.HPlanStructure.MPlan> scenicPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null, null, false, null, TourId);
            if (scenicPlanList != null && scenicPlanList.Count > 0)
            {
                this.jingdian = scenicPlanList.Count;
                this.rpt_jingdian.DataSource = scenicPlanList;
                this.rpt_jingdian.DataBind();
            }
            else
            {
                this.ph_jingdian.Visible = false;
            }

            #endregion

            #region 酒店

            IList<EyouSoft.Model.HPlanStructure.MPlan> hotelPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, null, false, null, TourId);
            if (hotelPlanList != null && hotelPlanList.Count > 0)
            {
                this.hotel = hotelPlanList.Count;
                this.rpt_hotellistk.DataSource = hotelPlanList;
                this.rpt_hotellistk.DataBind();
            }
            else
            {
                this.ph_hotel.Visible = false;
            }

            #endregion

            #region 领料

            IList<EyouSoft.Model.HPlanStructure.MPlan> lingliaolPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, null, false, null, TourId);
            if (lingliaolPlanList != null && lingliaolPlanList.Count > 0)
            {
                this.lingliao = lingliaolPlanList.Count;
                this.rpt_lingliao.DataSource = lingliaolPlanList;
                this.rpt_lingliao.DataBind();
            }
            else
            {
                this.ph_lingliao.Visible = false;
            }

            #endregion

            #region 其它

            IList<EyouSoft.Model.HPlanStructure.MPlan> otherlPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, null, false, null, TourId);
            if (otherlPlanList != null && otherlPlanList.Count > 0)
            {
                this.qita = otherlPlanList.Count;
                this.rpt_qita.DataSource = otherlPlanList;
                this.rpt_qita.DataBind();
            }
            else
            {
                this.ph_qita.Visible = false;
            }

            #endregion

            #region 汽车

            IList<EyouSoft.Model.HPlanStructure.MPlan> busPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, null, null, false, null, TourId);
            if (busPlanList != null && busPlanList.Count > 0)
            {
                this.bus = busPlanList.Count;
                this.rpt_bus.DataSource = busPlanList;
                this.rpt_bus.DataBind();
            }
            else
            {
                this.ph_bus.Visible = false;
            }

            #endregion

            #region 用餐

            IList<EyouSoft.Model.HPlanStructure.MPlan> yongcanPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null, null, false, null, TourId);
            if (yongcanPlanList != null && yongcanPlanList.Count > 0)
            {
                this.yongcan = yongcanPlanList.Count;
                this.rpt_yongcan.DataSource = yongcanPlanList;
                this.rpt_yongcan.DataBind();
            }
            else
            {
                this.ph_yongcan.Visible = false;
            }

            #endregion

            #region 用车

            IList<EyouSoft.Model.HPlanStructure.MPlan> carPlanList = bllPlan.GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null, null, false, null, TourId);
            if (carPlanList != null && carPlanList.Count > 0)
            {
                this.chedui = carPlanList.Count;
                this.rpt_chedui.DataSource = carPlanList;
                this.rpt_chedui.DataBind();
            }
            else
            {
                this.ph_chedui.Visible = false;
            }

            #endregion

            #endregion
            #endregion
        }

        /// <summary>
        /// 初始化计调安排信息
        /// </summary>
        void InitAnPaiInfo()
        {
            if (string.IsNullOrEmpty(AnPaiId)) return;
            var info = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, AnPaiId);
            if (info == null || string.IsNullOrEmpty(info.PlanId)) RCWE("异常请求");

            if (!string.IsNullOrEmpty(info.GuideNotes))
            {
                TGuideNote.Visible = true;
                var notes = new EyouSoft.BLL.HPlanStructure.BPlan().GetGuidePrint(info.TourId);
                if (notes != null)
                {
                    lbGuidNote.Text = Utils.TextareaToHTML(notes.GuideNotes);
                }
            }
            else
            {
                TGuideNote.Visible = false;
            }

            if (!string.IsNullOrEmpty(info.ReceiveJourney))
            {
                TReceiveJourney.Visible = true;
                lbReceiveJourney.Text = Utils.TextareaToHTML(info.ReceiveJourney);
            }
            else
            {
                TReceiveJourney.Visible = false;
            }

            if (!string.IsNullOrEmpty(info.ServiceStandard))
            {
                TService.Visible = true;
                lbServiceStandard.Text = Utils.TextareaToHTML(info.ServiceStandard);
            }
            else
            {
                TService.Visible = false;
            }
        }

        #region 前台调用方法
        //protected string GetshipNum(object obj, string type)
        //{
        //    EyouSoft.Model.PlanStructure.MPlanShip mplanship = (EyouSoft.Model.PlanStructure.MPlanShip)obj;
        //    if (mplanship != null && mplanship.PlanShipPriceList != null && mplanship.PlanShipPriceList.Count > 0)
        //    {
        //        if (type == "adult")
        //        {
        //            return mplanship.PlanShipPriceList[0].AdultNumber.ToString();
        //        }
        //        else
        //        {
        //            return mplanship.PlanShipPriceList[0].ChildNumber.ToString();
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        /// <summary>
        /// 获取大交通的出发时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetDepartureTime(object obj)
        {
            IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency> LargeTime = (IList<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>)obj;
            if (LargeTime != null && LargeTime.Count > 0)
            {
                return UtilsCommons.GetDateString(LargeTime[0].DepartureTime);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取订单第一个游客信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        protected string GetYouKeXinXi(object orderId)
        {
            string s = string.Empty;
            if (orderId == null) return string.Empty;
            string _orderId = orderId.ToString();
            if (string.IsNullOrEmpty(_orderId)) return string.Empty;

            var items = new EyouSoft.BLL.HTourStructure.BTourOrder().GetTourOrderTraveller(_orderId);

            if (items != null && items.Count > 0)
            {
                s = items[0].CnName + " " + items[0].Contact;
            }

            return s;
        }

        /// <summary>
        /// 获取客户等级
        /// </summary>
        /// <param name="orderId">客户</param>
        /// <returns></returns>
        protected string GetLevName(object comID)
        {
            string s = string.Empty;
            if (comID == null) return string.Empty;
            string _comID = comID.ToString();
            if (string.IsNullOrEmpty(_comID)) return string.Empty;

            var items = new EyouSoft.BLL.CrmStructure.BCrm().GetInfo(_comID);

            if (items != null)
            {
                var model = new EyouSoft.BLL.ComStructure.BComLev().GetInfo(items.LevId, SiteUserInfo.CompanyId);
                s = model != null ? model.Name : "";
            }

            return s;
        }

        #endregion

        #region 获取供应商信息
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="id">供应商编号</param>
        /// <param name="tp">供应商类型</param>
        /// <returns></returns>
        protected string GetGYSinfo(object id, PlanProject tp)
        {
            string GYSID = Convert.ToString(id);
            StringBuilder strCX = new StringBuilder();
            StringBuilder strLinkMan = new StringBuilder();
            StringBuilder strTELFAX = new StringBuilder();
            if (tp == PlanProject.导游)
            {
                var info = new EyouSoft.BLL.ComStructure.BDaoYou().GetInfo(GYSID);
                if (info != null)
                {
                    return info.IdentityId.ToString();
                }
            }
            else
            {
                var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GYSID);

                if (info != null)
                {
                    switch (tp)
                    {
                        #region 酒店
                        case PlanProject.酒店:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 用车
                        case PlanProject.用车:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 景点
                        case PlanProject.景点:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 地接
                        case PlanProject.地接:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 用餐
                        case PlanProject.用餐:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 购物
                        case PlanProject.购物:
                            StringBuilder strCPlist = new StringBuilder();
                            decimal liushui = 0;
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            if (info.GouWu.ChanPins != null && info.GouWu.ChanPins.Count > 0)
                            {
                                for (int i = 0; i < info.GouWu.ChanPins.Count; i++)
                                {
                                    strCPlist.AppendFormat("{0}({1})<br/>", info.GouWu.ChanPins[i].Name, info.GouWu.ChanPins[i].IdentityId);
                                }
                            }
                            if (info.GouWu.HeTongs!=null&&info.GouWu.HeTongs.Count>0)
                            {
                                var l = info.GouWu.HeTongs.Where(m => m.IsQiYong == true).ToList();
                                if (l!=null&&l.Count>0)
                                {
                                    liushui = l[0].LiuShui;
                                }
                            }
                            strCX.AppendFormat("<td  align=\"left\">{3}</td><td align=\"right\">{6}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), strCPlist.ToString(), info.GysName, info.IdentityId,liushui.ToString("F2"));
                            return strCX.ToString();
                        #endregion

                        #region 领料
                        case PlanProject.领料:
                            break;
                        #endregion

                        #region 飞机
                        case PlanProject.飞机:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 火车
                        case PlanProject.火车:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 汽车
                        case PlanProject.汽车:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 轮船
                        case PlanProject.轮船:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        #region 其它
                        case PlanProject.其它:
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strLinkMan.Append(info.Lxrs[i].Name + "<br/>");
                                }
                            }
                            if (info.Lxrs != null && info.Lxrs.Count > 0)
                            {
                                for (int i = 0; i < info.Lxrs.Count; i++)
                                {
                                    strTELFAX.AppendFormat("{0}/{1}<br/>", info.Lxrs[i].Telephone, info.Lxrs[i].Fax);
                                }
                            }
                            strCX.AppendFormat("<td align=\"left\">{3}</td><td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", info.Address, strLinkMan.ToString(), strTELFAX.ToString(), info.GysName);
                            return strCX.ToString();
                        #endregion

                        default:
                            break;
                    }
                }
            }
            return "";


        }
        #endregion

        #region 获取车队信息
        /// <summary>
        /// 返回车队相关信息
        /// </summary>
        /// <param name="list">车队列表</param>
        /// <returns></returns>
        protected string reCarInfo(object list)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder strCPH = new StringBuilder();
            StringBuilder strSJ = new StringBuilder();
            StringBuilder strSJDH = new StringBuilder();
            var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanCar>)list;
            for (int i = 0; i < lis.Count; i++)
            {
                strCPH.AppendFormat("{0}/{1}<br/>", lis[i].CarNumber, lis[i].Models);
                strSJ.AppendFormat("{0}<br/>", lis[i].Driver + "&nbsp;&nbsp;");
                strSJDH.AppendFormat("{0}<br/>", lis[i].DriverPhone + "&nbsp;&nbsp;");
            }
            str.AppendFormat("<td align=\"center\">{0}</td><td align=\"center\">{1}</td><td align=\"center\">{2}</td>", strCPH.ToString(), strSJ.ToString(), strSJDH.ToString());
            return str.ToString();
        }
        #endregion

    }
}
