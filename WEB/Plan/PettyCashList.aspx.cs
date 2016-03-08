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
    /// <summary>
    /// 计调安排—备用金申请
    /// </summary>
    public partial class PettyCashList : EyouSoft.Common.Page.BackPage
    {
        protected string tourCode = string.Empty;
        protected bool ret = false;

        protected EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            //菜单控件 样式初始化
            this.PlanConfigMenu1.IndexClass = "3";
            this.PlanConfigMenu1.CompanyId = SiteUserInfo.CompanyId;

            string tourID = Utils.GetQueryStringValue("tourId");
            DataInitByTourId(tourID);
            GetOrderListBytourID(tourID);
            GetDebitListByTourId(tourID);

            string doType = Utils.GetQueryStringValue("action");
            string id = Utils.GetQueryStringValue("ID");
            switch (doType)
            {
                case "delete":
                    Response.Clear();
                    Response.Write(DeleteDebitByID(id));
                    Response.End();
                    break;
                case "update":
                    Response.Clear();
                    Response.Write(debitAddOrUpdate());
                    Response.End();
                    break;
            }
        }

        #region 根据团号获取团队信息
        /// <summary>
        /// 根据团号获取团队信息
        /// </summary>
        /// <param name="toutID">团号</param>
        protected void DataInitByTourId(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                //团队信息
                EyouSoft.Model.HTourStructure.MTour tourInfo = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourID);
                if (tourInfo != null)
                {
                    this.litTourCode.Text = tourInfo.TourCode;
                    tourCode = tourInfo.TourCode;
                    EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(tourInfo.AreaId, SiteUserInfo.CompanyId);
                    if (AreaModel != null)
                    {
                        this.litAreaName.Text = AreaModel.AreaName;
                    }
                    AreaModel = null;
                    this.litRouteName.Text = tourInfo.RouteName;
                    this.litDays.Text = tourInfo.TourDays.ToString();
                    //人数
                    this.litPeoples.Text = "成人" + tourInfo.Adults.ToString() + "，儿童" + tourInfo.Childs.ToString() + "，领队" + tourInfo.Leaders.ToString();
                    //用房数
                    if (tourInfo.TourRoomList != null && tourInfo.TourRoomList.Count > 0)
                    {
                        int num = 0;
                        for (int i = 0; i < tourInfo.TourRoomList.Count; i++)
                        {
                            num += tourInfo.TourRoomList[i].Num;
                        }
                        this.litHouses.Text = num.ToString();
                    }
                    else
                    {
                        this.litHouses.Text = "0";
                    }
                    EyouSoft.Model.SysStructure.MGeography CountryModel = new EyouSoft.BLL.SysStructure.BGeography().GetCountry(SiteUserInfo.CompanyId, tourInfo.CountryId);
                    if (CountryModel != null)
                    {
                        this.litCountry.Text = CountryModel.Name;
                    }
                    CountryModel = null;

                    this.litDDDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                    this.litDDCity.Text = tourInfo.ArriveCity;
                    this.litDDHBDate.Text = tourInfo.ArriveCityFlight;
                    this.litLJDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.RDate, ProviderToDate);
                    this.litLKCity.Text = tourInfo.LeaveCity;
                    this.litLKHBDate.Text = tourInfo.LeaveCityFlight;

                    this.litSellers.Text = tourInfo.SellerName;
                    //计调员
                    if (tourInfo.TourPlanerList != null && tourInfo.TourPlanerList.Count > 0)
                    {
                        string planerList = "";
                        foreach (var item in tourInfo.TourPlanerList)
                        {
                            planerList += item.Planer + ",";
                        }
                        this.litOperaters.Text = planerList;
                    }
                }
                tourInfo = null;

                //总金额统计
                decimal totalPrices = 0;
                int indexCount = 0;

                IList<EyouSoft.Model.EnumType.PlanStructure.Payment> paylist = new List<EyouSoft.Model.EnumType.PlanStructure.Payment>();
                paylist.Add(EyouSoft.Model.EnumType.PlanStructure.Payment.现付);
                paylist.Add(EyouSoft.Model.EnumType.PlanStructure.Payment.签单);
                //地接
                IList<EyouSoft.Model.HPlanStructure.MPlan> ayencylist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, paylist, null, false, null, tourID);
                if (ayencylist != null && ayencylist.Count > 0)
                {
                    this.tabAyencyView.Visible = true;
                    indexCount += ayencylist.Sum(p => p.SigningCount);
                    totalPrices += ayencylist.Sum(p => p.Confirmation);
                    this.repayencyList.DataSource = ayencylist;
                    this.repayencyList.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> guidlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, paylist, null, false, null, tourID);
                if (guidlist != null && guidlist.Count > 0)
                {
                    this.tabGuidView.Visible = true;
                    indexCount += guidlist.Sum(p => p.SigningCount);
                    totalPrices += guidlist.Sum(p => p.Confirmation);
                    this.repGuidList.DataSource = guidlist;
                    this.repGuidList.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> hotellist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, paylist, null, false, null, tourID);
                if (hotellist != null && hotellist.Count > 0)
                {
                    indexCount += hotellist.Sum(p => p.SigningCount);
                    this.tabHotelView.Visible = true;
                    totalPrices += hotellist.Sum(p => p.Confirmation);
                    this.rephotellist.DataSource = hotellist;
                    this.rephotellist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> carslist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, paylist, null, false, null, tourID);
                if (carslist != null && carslist.Count > 0)
                {
                    indexCount += carslist.Sum(p => p.SigningCount);
                    this.tabCarsView.Visible = true;
                    totalPrices += carslist.Sum(p => p.Confirmation);
                    this.repcarslist.DataSource = carslist;
                    this.repcarslist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> airlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, paylist, null, false, null, tourID);
                if (airlist != null && airlist.Count > 0)
                {
                    indexCount += airlist.Sum(p => p.SigningCount);
                    this.tabAirView.Visible = true;
                    totalPrices += airlist.Sum(p => p.Confirmation);
                    this.repairlist.DataSource = airlist;
                    this.repairlist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> trainlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, paylist, null, false, null, tourID);
                if (trainlist != null && trainlist.Count > 0)
                {
                    indexCount += trainlist.Sum(p => p.SigningCount);
                    this.tabtrainView.Visible = true;
                    totalPrices += trainlist.Sum(p => p.Confirmation);
                    this.reptrainlist.DataSource = trainlist;
                    this.reptrainlist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> buslist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, paylist, null, false, null, tourID);
                if (buslist != null && buslist.Count > 0)
                {
                    indexCount += buslist.Sum(p => p.SigningCount);
                    this.tabbusView.Visible = true;
                    totalPrices += buslist.Sum(p => p.Confirmation);
                    this.repbuslist.DataSource = buslist;
                    this.repbuslist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> attrlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, paylist, null, false, null, tourID);
                if (attrlist != null && attrlist.Count > 0)
                {
                    indexCount += attrlist.Sum(p => p.SigningCount);
                    this.tabAttrView.Visible = true;
                    totalPrices += attrlist.Sum(p => p.Confirmation);
                    this.repattrlist.DataSource = attrlist;
                    this.repattrlist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> chinashiplist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, paylist, null, false, null, tourID);
                if (chinashiplist != null && chinashiplist.Count > 0)
                {
                    indexCount += chinashiplist.Sum(p => p.SigningCount);
                    this.tabchinashipView.Visible = true;
                    totalPrices += chinashiplist.Sum(p => p.Confirmation);
                    this.repchinashiplist.DataSource = chinashiplist;
                    this.repchinashiplist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> dinlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, paylist, null, false, null, tourID);
                if (dinlist != null && dinlist.Count > 0)
                {
                    indexCount += dinlist.Sum(p => p.SigningCount);
                    this.tabDinView.Visible = true;
                    totalPrices += dinlist.Sum(p => p.Confirmation);
                    this.repDinlist.DataSource = dinlist;
                    this.repDinlist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> shoplist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, paylist, null, false, null, tourID);
                if (shoplist != null && shoplist.Count > 0)
                {
                    indexCount += shoplist.Sum(p => p.SigningCount);
                    this.tabshopView.Visible = true;
                    totalPrices += shoplist.Sum(p => p.Confirmation);
                    this.repshoplist.DataSource = shoplist;
                    this.repshoplist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> picklist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, paylist, null, false, null, tourID);
                if (picklist != null && picklist.Count > 0)
                {
                    indexCount += picklist.Sum(p => p.SigningCount);
                    this.tabpickView.Visible = true;
                    totalPrices += picklist.Sum(p => p.Confirmation);
                    this.reppicklist.DataSource = picklist;
                    this.reppicklist.DataBind();
                }

                IList<EyouSoft.Model.HPlanStructure.MPlan> otherlist = new EyouSoft.BLL.HPlanStructure.BPlan().GetListP(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, paylist, null, false, null, tourID);
                if (otherlist != null && otherlist.Count > 0)
                {
                    indexCount += otherlist.Sum(p => p.SigningCount);
                    this.tabotherView.Visible = true;
                    totalPrices += otherlist.Sum(p => p.Confirmation);
                    this.repotherlist.DataSource = otherlist;
                    this.repotherlist.DataBind();
                }
                //总金额统计
                this.littotalPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(totalPrices, ProviderToMoney);
                //签单数
                this.litSignNums.Text = indexCount.ToString();
                paylist = null;
            }
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
                        str.AppendFormat("{2}天数:{0}&nbsp;/&nbsp;金额:{1}<br/>", lis[i].Days, lis[i].SumPrice.ToString("C2"), string.IsNullOrEmpty(lis[i].Models) ? "" : "车型:" + lis[i].Models + "&nbsp;/&nbsp;");
                    }
                }
                if (type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机.ToString()
                        || type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString()
                        || type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车.ToString()
                        || type == EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString())
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
                        str.AppendFormat("{0}:成人{1}人,儿童{2}人,{3}桌&nbsp;&nbsp;{4}<br/>", lis[i].MenuName, lis[i].AdultNumber, lis[i].ChildNumber, lis[i].TableNumber, lis[i].SumPrice.ToString("C2"));
                    }
                }

            }
            return str.ToString();
        }
        #endregion

        #region 导游收款 订单列表
        /// <summary>
        /// 导游收款 订单列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetOrderListBytourID(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MOrderSum sum = new EyouSoft.Model.TourStructure.MOrderSum();
                IList<EyouSoft.Model.TourStructure.MTourOrder> orders = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref  sum, tourID);
                if (orders != null && orders.Count > 0)
                {
                    this.litsumPrices.Text = UtilsCommons.GetMoneyString(orders.Sum(p => p.GuideIncome), ProviderToMoney);
                    this.repGuidPayment.DataSource = orders;
                    this.repGuidPayment.DataBind();
                }
                else
                {
                    this.tabGuidPaymentView.Visible = false;
                }
            }
        }
        #endregion

        #region 绑定导游借款列表
        /// <summary>
        /// 绑定导游借款列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetDebitListByTourId(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                IList<EyouSoft.Model.FinStructure.MDebit> detitlist = new EyouSoft.BLL.FinStructure.BFinance().GetDebitLstByTourId(tourID, false);
                if (detitlist != null && detitlist.Count > 0)
                {
                    this.repGuidPayList.DataSource = detitlist;
                    this.repGuidPayList.DataBind();
                }
            }
        }
        #endregion

        #region 删除导游借款
        /// <summary>
        /// 删除导游借款
        /// </summary>
        /// <param name="Id">借款id</param>
        /// <returns></returns>
        protected string DeleteDebitByID(string Id)
        {
            string setMsg = string.Empty;
            if (!string.IsNullOrEmpty(Id))
            {
                switch (new EyouSoft.BLL.FinStructure.BFinance().DeleteDebit(this.SiteUserInfo.CompanyId, Convert.ToInt32(Id)))
                {
                    case 1:
                        setMsg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                        break;
                    case 0:
                        setMsg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                        break;
                    case -1:
                        setMsg = UtilsCommons.AjaxReturnJson("0", "财务已审！");
                        break;
                }
            }
            return setMsg;
        }
        #endregion

        #region 财务状态
        /// <summary>
        /// 财务状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetFinStatus(EyouSoft.Model.EnumType.FinStructure.FinStatus? status, EyouSoft.Model.EnumType.TourStructure.TourStatus tStatus, string id)
        {
            string str = "";

            if ((status != EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团) && id != "")
            {
                return str;
            }
            if (id != "")
            {
                str = "<a href='javascript:' data-class='savePreApp'><img src='/images/y-delupdateicon.gif' border='0' data-id='" + id + "' />修改</a> <a href='javascript:' data-class='deletePreApp'><img src='/images/y-delicon.gif' alt='' data-id='" + id + "' />删除</a>";
            }
            else
            {
                if ( tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消)
                {
                    ret = true;
                    str = "";
                }
                else
                {
                    str = "<a data-class=\"savePreApp\" href=\"javascript:void(0);\"><img border=\"0\"  src=\"/images/addicon.gif\">申请</a>";
                }
            }
            return str;
        }

        #endregion

        #region 添加 修改 借款
        /// <summary>
        /// 导游借款 添加 修改
        /// </summary>
        /// <returns></returns>
        protected string debitAddOrUpdate()
        {
            string setMsg = string.Empty;
            string msgArr = string.Empty;
            EyouSoft.Model.FinStructure.MDebit debit = new EyouSoft.Model.FinStructure.MDebit();
            debit.CompanyId = this.SiteUserInfo.CompanyId;
            debit.DeptId = this.SiteUserInfo.DeptId;
            debit.IssueTime = System.DateTime.Now;
            debit.Operator = this.SiteUserInfo.Name;
            debit.OperatorId = this.SiteUserInfo.UserId;
            //借款编号
            debit.Id = Utils.GetInt(Utils.GetQueryStringValue("ID"));
            //团号
            debit.TourId = Utils.GetQueryStringValue("TourId");
            debit.TourCode = tourCode;
            //借款用途
            debit.UseFor = Utils.GetQueryStringValue("txtUseFor");
            //借款人
            string Borrower = Utils.GetQueryStringValue("guidName");
            string BorrowerId = Utils.GetQueryStringValue("guid");
            if (string.IsNullOrEmpty(Borrower) && string.IsNullOrEmpty(BorrowerId))
            {
                msgArr += "请选择借款人!<br/>";
            }
            else
            {
                debit.Borrower = Borrower;
                debit.BorrowerId = BorrowerId;
            }
            //借款时间
            DateTime BorrowTime = Utils.GetDateTime(Utils.GetQueryStringValue("txtBorrowTime"));
            if (string.IsNullOrEmpty(BorrowTime.ToString()))
            {
                msgArr += "请选择借款时间！<br/>";
            }
            else
            {
                debit.BorrowTime = BorrowTime;
            }
            //实借金额 预借金额
            decimal RealAmount = Utils.GetDecimal(Utils.GetQueryStringValue("txtRealAmount"));
            decimal BorrowAmount = Utils.GetDecimal(Utils.GetQueryStringValue("txtBorrowAmount"));
            if (BorrowAmount <= 0)
            {
                msgArr += "请输入预借金额！<br/>";
            }
            else
            {
                debit.RealAmount = RealAmount;
                debit.BorrowAmount = BorrowAmount;
            }
            //欲领签单数 实领签单数
            int PreSignNum = Utils.GetInt(Utils.GetQueryStringValue("txtPreSignNum"));
            int RelSignNum = Utils.GetInt(Utils.GetQueryStringValue("txtRelSignNum"));
            if (PreSignNum <= 0 && !Utils.GetQueryStringValue("txtPreSignNum").Equals("0"))
            {
                msgArr += "请输入预领签单数！<br/>";
            }
            else
            {
                debit.PreSignNum = PreSignNum;
                debit.RelSignNum = RelSignNum;
            }
            if (!string.IsNullOrEmpty(msgArr))
            {
                setMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                return setMsg;
            }

            switch (new EyouSoft.BLL.FinStructure.BFinance().AddOrUpdDebit(debit))
            {
                case 1:
                    setMsg = UtilsCommons.AjaxReturnJson("1", "操作成功！");
                    break;
                case 0:
                    setMsg = UtilsCommons.AjaxReturnJson("0", "操作失败！");
                    break;
                case -1:
                    setMsg = UtilsCommons.AjaxReturnJson("0", "财务已审！");
                    break;
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_备用金申请))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_备用金申请, false);
                return;
            }
        }
        #endregion
    }
}
