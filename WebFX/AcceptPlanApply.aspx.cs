using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.WebFX
{
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class AcceptPlanApply : EyouSoft.Common.Page.FrontPage
    {
        /// <summary>
        /// 系统配置的留位时间
        /// </summary>
        protected string MaxDateTime;

        /// <summary>
        /// 最小留位时间
        /// </summary>
        protected string MinDateTime = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm");



        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Save"))
                {
                    Response.Clear();
                    Response.Write(Save());
                    Response.End();
                }
            }

            if (!IsPostBack)
            {
                string tourId = EyouSoft.Common.Utils.GetQueryStringValue("TourId");
                PageInit(tourId);
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;

                //#region 获得留位时间
                //EyouSoft.BLL.ComStructure.BComSetting settingBll = new EyouSoft.BLL.ComStructure.BComSetting();
                //EyouSoft.Model.ComStructure.MComSetting settModel = settingBll.GetModel(SiteUserInfo.CompanyId);
                //if (settModel != null)
                //{
                //    if (settModel.SaveTime != 0)
                //    {
                //        this.MaxDateTime = DateTime.Now.AddMinutes(settModel.SaveTime).ToString("yyyy-MM-dd HH:mm");
                //    }
                //    else
                //    {
                //        this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
                //    }
                //}
                //else
                //{
                //    this.MaxDateTime = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm");
                //}
                //settingBll = null;
                //settModel = null;
                //#endregion
            }
        }


        private void PageInit(string tourId)
        {
            this.PriceStand1.CompanyID = SiteUserInfo.CompanyId;
            this.PriceStand1.ShowModel = true;
             // this.PriceStand1.IsDistribution = true;
            this.PriceStand1.InitMode = false;

            EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();

            EyouSoft.Model.TourStructure.MTourSanPinInfo model = (EyouSoft.Model.TourStructure.MTourSanPinInfo)bll.GetTourInfo(tourId);
            if (model != null)
            {
                IList<EyouSoft.Model.TourStructure.MTourPriceStandard> MTourPriceStandard = model.MTourPriceStandard;
                List<EyouSoft.Model.TourStructure.MTourPriceLevel> list = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();

                if (MTourPriceStandard != null && MTourPriceStandard.Count != 0)
                {
                    foreach (var standard in MTourPriceStandard)
                    {
                        list.AddRange(standard.PriceLevel);
                    }

                    var Leve = list.Where(c => c.LevelId == SiteUserInfo.TourCompanyInfo.LevelId).ToList();


                    IList<EyouSoft.Model.TourStructure.MTourPriceStandard> currentStand = new List<EyouSoft.Model.TourStructure.MTourPriceStandard>();
                    for (int i = 0; i < MTourPriceStandard.Count; i++)
                    {
                        var s = MTourPriceStandard[i];
                        EyouSoft.Model.TourStructure.MTourPriceStandard item = new EyouSoft.Model.TourStructure.MTourPriceStandard();
                        item.Id = s.Id;
                        item.Standard = s.Standard;
                        item.StandardName = s.StandardName;
                        item.TourId = s.TourId;
                        item.PriceLevel = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();


                        for (int j = 0; j < s.PriceLevel.Count; j++)
                        {
                            var p = s.PriceLevel[j];
                            for (int k = 0; k < Leve.Count; k++)
                            {
                                var l = Leve[k];
                                if (p.LevelId == Leve[k].LevelId && p.AdultPrice == l.AdultPrice && p.ChildPrice == l.ChildPrice)
                                {
                                    item.PriceLevel = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();
                                    item.PriceLevel.Add(Leve[k]);
                                    currentStand.Add(item);
                                    break;
                                }
                            }
                        }
                    }

                    this.PriceStand1.SetPriceStandard = currentStand;
                    this.hfTourType.Value = ((int)model.TourType).ToString();

                    //游客控件的显示
                    switch (model.TourType)
                    {
                        case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.单项业务:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品:
                            
                            break;
                        case EyouSoft.Model.EnumType.TourStructure.TourType.线路产品:
                            
                            break;
                        default: this.phdTravelControlS.Visible = false; 
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// 页面验证
        /// </summary>
        /// <param name="order"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool PageValidate(ref EyouSoft.Model.TourStructure.MTourOrderExpand order, ref string msg)
        {
            //验证的字段
            string DCompanyName = Utils.GetFormValue("txtDCompanyName");
            //if (string.IsNullOrEmpty(DCompanyName))
            //{
            //    msg += "客源单位 不能为空！</br>";
            //}
            string DContactName = Utils.GetFormValue("txtDContactName");
            if (string.IsNullOrEmpty(DContactName))
            {
                msg += "联系人 不能为空！</br>";
            }

            string DContactTel = Utils.GetFormValue("txtDContactTel");
            if (string.IsNullOrEmpty(DContactTel))
            {
                msg += "联系定电话 不能为空！</br>";
            }

            int Adults = Utils.GetInt(Utils.GetFormValue("txtAdults"));
            if (Adults < 0)
            {
                msg += "成人数 格式不正确！</br>";
            }

            int Childs = Utils.GetInt(Utils.GetFormValue("txtChilds"));
            if (Childs < 0)
            {
                msg += "儿童数 格式不正确！</br>";
            }
            decimal SumPrice = Utils.GetDecimal(Utils.GetFormValue("txtSumPrice"));
            if (SumPrice <= 0)
            {

                msg += "合计金额 格式不正确！</br>";
            }
            string saveDate = Utils.GetFormValue("txtSaveSeatDate");
            if (!string.IsNullOrEmpty(saveDate))
            {
                DateTime? SaveSeatDate = Utils.GetDateTimeNullable(saveDate);
                if (!SaveSeatDate.HasValue)
                {
                    msg += "留位时间 格式不正确！</br>";
                }
            }
            string tourType = Utils.GetFormValue("hfTourType");

            if (msg.Length <= 0)
            {
                order.CompanyId = SiteUserInfo.CompanyId;
                order.TourId = Utils.GetQueryStringValue("TourId");
                order.BuyCompanyName = SiteUserInfo.TourCompanyInfo.CompanyName;
                order.BuyCompanyId = SiteUserInfo.TourCompanyInfo.CompanyId;
                //客源单位联系人信息（当前分销商）
                string lxrId = SiteUserInfo.TourCompanyInfo.LxrId;
                order.ContactDepartId = lxrId;
                EyouSoft.BLL.CrmStructure.BCrmLinkMan link = new EyouSoft.BLL.CrmStructure.BCrmLinkMan();
                EyouSoft.Model.CrmStructure.MCrmLinkman linkMan = link.GetLinkManModel(lxrId);
                if (linkMan != null)
                {
                    order.ContactName = linkMan.Name;
                    order.ContactTel = linkMan.Telephone;
                }


                order.OrderStatus = EyouSoft.Model.EnumType.TourStructure.OrderStatus.未处理;

                //联系人信息
                order.DCompanyName = DCompanyName;
                order.DContactName = DContactName;
                order.DContactTel = DContactTel;



                //销售员信息
                order.SellerId = SiteUserInfo.UserId;
                order.SellerName = SiteUserInfo.Name;
                order.DeptId = SiteUserInfo.DeptId;

                //操作员信息
                order.Operator = SiteUserInfo.Name;
                order.OperatorId = SiteUserInfo.UserId;

                order.Adults = Adults;
                order.Childs = Childs;

                //价格组成
                string strPrice = Utils.GetFormValue("txt_PriceStand_radio_price");
                if (!string.IsNullOrEmpty(strPrice))
                {
                    string[] price = strPrice.Split('|');
                    order.LevId = Utils.GetInt(price[0]);
                    order.AdultPrice = Utils.GetDecimal(price[1]);
                    order.ChildPrice = Utils.GetDecimal(price[2]);
                }

                string strStandard = Utils.GetFormValue("_hstandard");
                if (!string.IsNullOrEmpty(strStandard))
                {
                    int Standard = Utils.GetInt(strStandard);
                    order.PriceStandId = Standard;
                    EyouSoft.BLL.TourStructure.BTour bll = new EyouSoft.BLL.TourStructure.BTour();
                    EyouSoft.Model.TourStructure.MTourSanPinInfo model = (EyouSoft.Model.TourStructure.MTourSanPinInfo)bll.GetTourInfo(order.TourId);
                    IList<EyouSoft.Model.TourStructure.MTourPriceStandard> standardList = model.MTourPriceStandard.Where(c => c.Standard == Standard).ToList();

                    if (standardList != null && standardList.Count != 0)
                    {
                        List<EyouSoft.Model.TourStructure.MTourPriceLevel> levelList = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();
                        foreach (var a in standardList)
                        {
                            levelList.AddRange(a.PriceLevel);
                        }
                        if (levelList != null && levelList.Count != 0)
                        {
                            EyouSoft.Model.TourStructure.MTourPriceLevel item = levelList.SingleOrDefault(c => c.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.内部结算价);
                            if (item != null)
                            {
                                order.PeerAdultPrice = item.AdultPrice;
                                order.PeerChildPrice = item.ChildPrice;
                                order.PeerLevId = item.LevelId;
                                order.SettlementMoney = order.PeerAdultPrice * Adults + order.PeerChildPrice * Childs;
                            }
                        }
                    }
                }

                order.SaleAddCost = Utils.GetDecimal(Utils.GetFormValue("txtSaleAddCost"));
                order.SaleAddCostRemark = Utils.GetFormValue("txtSaleAddCostRemark");
                order.SaleReduceCost = Utils.GetDecimal(Utils.GetFormValue("txtSaleReduceCost"));
                order.SaleReduceCostRemark = Utils.GetFormValue("txtSaleReduceCostRemark");

                order.SumPrice = Utils.GetDecimal(Utils.GetFormValue("txtSumPrice"));

                order.SaveSeatDate = Utils.GetDateTimeNullable(Utils.GetFormValue("txtSaveSeatDate"));
                order.OrderRemark = Utils.GetFormValue("textarea");



                order.OrderType = EyouSoft.Model.EnumType.TourStructure.OrderType.分销商下单;

                order.MTourOrderTravellerList = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
                order.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.TourType), tourType);
                switch (order.TourType)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                    case TourType.散拼产品:
                    case EyouSoft.Model.EnumType.TourStructure.TourType.单项业务:
                       order.MTourOrderTravellerList = UtilsCommons.GetTravelList();
                       break;
                    case EyouSoft.Model.EnumType.TourStructure.TourType.线路产品:
                       // order.MTourOrderTravellerList = UtilsCommons.GetTravelListS();
                        break;
                }


            }

            return msg.Length <= 0;
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        private string Save()
        {
            string msg = string.Empty;
            EyouSoft.Model.TourStructure.MTourOrderExpand order = new EyouSoft.Model.TourStructure.MTourOrderExpand();
            IList<EyouSoft.Model.TourStructure.MTourOrderCarTypeSeat> list = null;
            if (PageValidate(ref order, ref msg))
            {
                //判断客户单位是否欠款、单团账龄是否超限
                //EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();

                //EyouSoft.Model.FinStructure.MCustomerWarning customerWarningModel = bll.GetCustomerOverrunDetail(SiteUserInfo.TourCompanyInfo.CompanyId, order.SumPrice, SiteUserInfo.CompanyId);

                if (false)//customerWarningModel != null
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "您的欠款已经超过限额，暂不可报名。");
                 }
                else
                {
                    EyouSoft.BLL.TourStructure.BTourOrder bOrder = new EyouSoft.BLL.TourStructure.BTourOrder();
                    string IsShortRoute = Utils.GetQueryStringValue("IsShort");
                    int flg = 0;
                    if (!string.IsNullOrEmpty(IsShortRoute))
                    {
                        flg = bOrder.AddTourOrderExpand(order, ref list);
                    }
                    else
                    {
                        flg = bOrder.AddTourOrderExpand(order);
                    }
                    if (flg == 3)
                    {
                        msg = UtilsCommons.AjaxReturnJson("1", "订单报名成功！ 正在跳转...");
                    }
                    else if (flg == 5)
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "计划自动客满，不允许报名！");
                    }
                    else if (flg == 6)
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "计划自动停收，不允许报名！");
                    }
                    else if (flg == 7)
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "计划手动客满，不允许报名！");
                    }
                    else if (flg == 8)
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "计划手动停收，不允许报名！");
                    }
                    else if (flg == 1)
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "订单报名人数超过计划剩余人数！");
                    }
                    else
                    {
                        msg = UtilsCommons.AjaxReturnJson("0", "订单报名失败！");
                    }
                }

            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", msg);
            }

            return msg;
        }

    }
}
