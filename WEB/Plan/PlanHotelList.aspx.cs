using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.IO;
using System.Text;

namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调中心-酒店安排
    /// </summary>
    public partial class PlanHotelList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //确认单
        protected string querenUrl = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;

        string TourId = string.Empty;
        string PlanId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            PlanId = Utils.GetQueryStringValue("planid");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");


            InitPrivs();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.酒店确认单);

            string doType = Utils.GetQueryStringValue("action");

            switch (doType)
            {
                case "delete": Delete(); break;
                case "save":
                    Response.Clear();
                    Response.Write(PageSave());
                    Response.End();
                    break;
                case "getdata":
                    Response.Clear();
                    Response.Write(GetHotelRoom(Utils.GetQueryStringValue("suppId"), Utils.GetQueryStringValue("rid"), "", ""));
                    Response.End();
                    break;
                case "quanbuluoshi":
                    Response.Clear();
                    this.QuanBuLuoShi((EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetQueryStringValue("type")), Utils.GetQueryStringValue("tourid"));
                    Response.End();
                    break;
                default: break;
            }

            InitInfo();
            InitRpt();
        }

        #region 获取房型
        /// <summary>
        /// 获取所有房型列表
        /// </summary>
        /// <returns></returns>
        protected void GetAllRoomTypeList()
        {
            var list = GetRoomTypeList().Where(c => c.Quantity > 0).ToList();
            if (list != null && list.Count > 0)
            {
                this.holderView.Visible = false;
                this.reproomtypelist.DataSource = list;
                this.reproomtypelist.DataBind();
            }
            else
            {
                this.holderView.Visible = true;
            }
        }
        /// <summary>
        /// 获取所有房型子项
        /// </summary>
        /// <returns></returns>
        protected string GetRoomType(string rid)
        {
            var sbRoomType = new StringBuilder();
            var list = GetRoomTypeList();
            if (list != null && list.Count > 0)
            {
                var optlist = new List<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>();
                optlist.Add(list[0]);
                for (int m = 1; m < list.Count; m++)
                {
                    bool Flag = false;
                    for (int n = 0; n < optlist.Count; n++)
                    {
                        if (list[m].RoomId == optlist[n].RoomId)
                        {
                            Flag = true;
                            break;
                        }
                    }
                    if (!Flag)
                    {
                        optlist.Add(list[m]);
                    }
                }
                for (int i = 0; i < optlist.Count; i++)
                {
                    sbRoomType.AppendFormat("<option value='{0},{1}' " + (optlist[i].RoomId == rid ? "selected='selected'" : "") + " >{1}</option>", optlist[i].RoomId, optlist[i].RoomType);
                }
            }
            return sbRoomType.ToString();
        }
        /// <summary>
        /// 获取所有房型列表
        /// </summary>
        /// <returns></returns>
        protected IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom> GetRoomTypeList()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            string planId = Utils.GetQueryStringValue("PlanId");
            return new BLL.HPlanStructure.BPlan().GetRoomList((string.IsNullOrEmpty(planId) ? "0" : "1"), (string.IsNullOrEmpty(planId) ? tourId : planId));
        }
        /// <summary>
        /// 异步获取房型列表
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        protected string GetHotelRoom(string suppId, string rid, string sTime, string eTime)
        {
            if (Utils.GetQueryStringValue("star") != null && Utils.GetQueryStringValue("star") == "1")
            {
                string star = "0";
                EyouSoft.Model.HGysStructure.MGysInfo model = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(suppId);
                if (model != null)
                {
                    if ((int)model.JiuDian.XingJi > 0)
                    {
                        star = ((int)model.JiuDian.XingJi).ToString();
                    }
                }
                return "{\"star\":\"" + star + "\"}";
            }
            DateTime? d1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("d1"));
            DateTime? d2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("d2"));
            IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> list = new EyouSoft.BLL.HGysStructure.BJiaGe().GetJiuDianJiaGes(suppId);
            if (list != null && list.Count > 0)
            {
                if (!string.IsNullOrEmpty(rid))
                {
                    list = list.Where(c => c.FangXingId == rid).ToList();
                }
                if (d1.HasValue && d2.HasValue)
                {
                    IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> listN = new List<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        //list[i].STime
                        //list[i].ETime
                        //list[i].XingQi
                    }
                    return "{\"tolist\":" + Newtonsoft.Json.JsonConvert.SerializeObject(listN) + "}";
                }
                return "{\"tolist\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}";
            }
            else
            {
                return "{\"tolist\":\"\"}";
            }
        }
        #endregion

        public string GetAPMX(object list)
        {
            StringBuilder str = new StringBuilder();
            if (list != null)
            {
                var lis = (IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>)list;
                for (int i = 0; i < lis.Count; i++)
                {
                    str.AppendFormat("{0}&nbsp;/&nbsp;{1}{2}&nbsp;/&nbsp;{3}<br/>", lis[i].RoomType, lis[i].Quantity, lis[i].PriceType.ToString(), lis[i].Total.ToString("C2"));
                }
            }
            return str.ToString();
        }

        #region 酒店房型计算单位
        /// <summary>
        /// 酒店房型计算单位
        /// </summary>
        /// <param name="selectindex"></param>
        /// <returns></returns>
        protected string Getcalculate(string selectindex)
        {
            System.Text.StringBuilder calculateHtml = new System.Text.StringBuilder();
            List<EnumObj> roomUnitprices = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType));
            if (roomUnitprices.Count > 0 && roomUnitprices != null)
            {
                for (int i = 0; i < roomUnitprices.Count; i++)
                {
                    if (selectindex == roomUnitprices[i].Value)
                    {
                        calculateHtml.Append("<option value='" + roomUnitprices[i].Value + "' selected='selected'>元/" + roomUnitprices[i].Text + "</option>");
                    }
                    else
                    {
                        calculateHtml.Append("<option value='" + roomUnitprices[i].Value + "'>元/" + roomUnitprices[i].Text + "</option>");
                    }
                }
            }
            return calculateHtml.ToString();
        }
        #endregion

        #region 全部落实
        void QuanBuLuoShi(EyouSoft.Model.EnumType.PlanStructure.PlanProject typ, string tourid)
        {
            switch (new EyouSoft.BLL.HPlanStructure.BPlan().DoGlobal(tourid, EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实, EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店))
            {
                case 0:
                    Context.Response.Write("酒店全部落实失败！");
                    break;
                case 1:
                    Context.Response.Write("酒店全部落实成功！");
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave()
        {
            #region 表单赋值
            string setsrrorMsg = string.Empty;
            string msg = string.Empty;
            //酒店名称
            string hotelName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //酒店id
            string hotelId = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //联系人 联系电话 联系传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //入住时间 离店时间 天数
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtStartTime.UniqueID));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtEndTime.UniqueID));
            string days = Utils.GetFormValue(this.txtroomDays.UniqueID);
            //酒店间数
            var num = 0;
            //房型 单价 计算方式 数量 小计 
            string[] roomType = Utils.GetFormValues("ddlRoomType");
            string[] unitPirces = Utils.GetFormValues("txtunitPrice");
            string[] selectType = Utils.GetFormValues("select");
            string[] numbers = Utils.GetFormValues("txtRoomNumber");
            string[] TotalMoney = Utils.GetFormValues("txtTotalMoney");

            //免房数量 免房金额
            string FreRoomNumbers = Utils.GetFormValue(this.txtFreRoomNumbers.UniqueID);
            string FreRoomMoney = Utils.GetFormValue(this.txtFreRoomMoney.UniqueID);
            //结算费用 费用明细
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));

            //导游须知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtRemark.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(hotelName) && string.IsNullOrEmpty(hotelId))
            {
                msg += "请选择酒店名称!<br/>";
            }
            if (string.IsNullOrEmpty(startTime.ToString()))
            {
                msg += "请填写入住时间!<br/>";
            }
            if (string.IsNullOrEmpty(endTime.ToString()))
            {
                msg += "请填写离店时间!<br/>";
            }
            if (string.IsNullOrEmpty(days))
            {
                msg += "请填写入住天数!<br/>";
            }

            if (roomType.Length > 0)
            {
                for (int i = 0; i < roomType.Length; i++)
                {
                    if (string.IsNullOrEmpty(roomType[i]))
                    {
                        msg += "第" + (i + 1) + "行请选择房型!<br/>";
                    }
                }
            }

            if (unitPirces.Length > 0)
            {
                for (int i = 0; i < unitPirces.Length; i++)
                {
                    if (string.IsNullOrEmpty(unitPirces[i]))
                    {
                        msg += "第" + (i + 1) + "行请输入单价！<br/>";
                    }
                }
            }
            if (numbers.Length > 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (string.IsNullOrEmpty(numbers[i]))
                    {
                        msg += "第" + (i + 1) + "行请输入数量!<br/>";
                    }
                    num += Utils.GetInt(numbers[i]);
                }
            }
            string[] totalPrices = Utils.GetFormValues("txtTotalMoney");
            if (totalPrices.Length > 0)
            {
                for (int i = 0; i < totalPrices.Length; i++)
                {
                    if (string.IsNullOrEmpty(totalPrices[i]) && Utils.GetDecimal(totalPrices[i]) <= 0)
                    {
                        msg += "第" + totalPrices[i] + "行请输入金额费用!<br/>";
                    }
                }
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("containsEarly")))
            {
                msg += "请选择是否含早餐!<br/>";
            }

            if (totalMoney <= 0)
            {
                msg += "请填写结算费用!<br/>";
            }

            if (string.IsNullOrEmpty(Utils.GetFormValue("states")))
            {
                msg += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("panyMent")))
            {
                msg += "请选择支付方式！<br/>";
            }
            if (msg != "")
            {
                setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setsrrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.IsDuePlan = false;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalMoney;
            baseinfo.PlanCost = totalMoney;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.EndDate = endTime;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = num;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            baseinfo.SigningCount = Utils.GetInt(Utils.GetFormValue("panyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            baseinfo.DueToway = (EyouSoft.Model.EnumType.PlanStructure.DueToway)Utils.GetInt(Utils.GetFormValue("dueToway"));
            //酒店
            baseinfo.PlanHotel = new EyouSoft.Model.HPlanStructure.MPlanHotel();
            baseinfo.PlanHotel.Days = Utils.GetInt(days);
            baseinfo.PlanHotel.FreeNumber = Utils.GetDecimal(FreRoomNumbers);
            baseinfo.PlanHotel.FreePrice = Utils.GetDecimal(FreRoomMoney);
            baseinfo.PlanHotel.IsMeal = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)Utils.GetInt(Utils.GetFormValue("containsEarly"));
            baseinfo.PlanHotel.Star = (EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi)Utils.GetInt(Utils.GetFormValue("hotelStart"));
            //房型
            baseinfo.PlanHotelRoomList = new List<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>();
            for (int i = 0; i < roomType.Length; i++)
            {
                var roomHotel = new EyouSoft.Model.HPlanStructure.MPlanHotelRoom();
                roomHotel.RoomType = roomType[i].Split(',')[1];
                roomHotel.RoomId = roomType[i].Split(',')[0];
                roomHotel.UnitPrice = Utils.GetDecimal(unitPirces[i]);
                roomHotel.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType), selectType[i]);
                roomHotel.Quantity = Utils.GetInt(numbers[i]);
                roomHotel.Total = Utils.GetDecimal(TotalMoney[i]);
                baseinfo.PlanHotelRoomList.Add(roomHotel);
            }
            baseinfo.Remarks = otherMarks;
            baseinfo.SourceId = hotelId;
            baseinfo.SourceName = hotelName;
            baseinfo.StartDate = startTime;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.Operator = this.SiteUserInfo.Name;
            baseinfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            #endregion

            #region 提交操作
            //酒店id
            string planID = Utils.GetQueryStringValue("planId");
            int result = 0;
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            if (planID != null && planID != "")
            {
                baseinfo.PlanId = planID;
                baseinfo.PlanHotel.PlanId = planID;
                result = bll.UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功！";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = bll.AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,添加失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            #endregion

            return setsrrorMsg;
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排酒店);
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            if (ListPower) ListPower = panView.Visible = Privs_AnPai;

            if (string.IsNullOrEmpty(PlanId))
            {
                this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), "-1", false);
                this.litHotelStart.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi), new string[] { "0" }), "-1", false);
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);
                this.litContainsEarly.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)), "-1", false);
                return;
            }

            var info = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, PlanId);
            if (info == null) RCWE("异常请求");


            this.supplierControl1.HideID = info.SourceId;
            this.supplierControl1.Name = info.SourceName;

            if (info.PlanHotel != null)
            {
                this.litHotelStart.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi), new string[] { "0" }), ((int)info.PlanHotel.Star).ToString(), false);
                this.txtFreRoomNumbers.Text = info.PlanHotel.FreeNumber.ToString();
                this.txtFreRoomMoney.Text = Utils.FilterEndOfTheZeroDecimal(info.PlanHotel.FreePrice);
                this.litContainsEarly.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)), ((int)info.PlanHotel.IsMeal).ToString(), false);
                this.txtroomDays.Text = info.PlanHotel.Days.ToString();
            }

            //this.txtroomDays.Text = info.Num.ToString();
            this.txtContectName.Text = info.ContactName;
            this.txtContectPhone.Text = info.ContactPhone;
            this.txtContectFax.Text = info.ContactFax;
            this.txtStartTime.Text = UtilsCommons.GetDateString(info.StartDate, ProviderToDate);
            this.txtEndTime.Text = UtilsCommons.GetDateString(info.EndDate, ProviderToDate);
            this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(info.Confirmation);
            this.txtGuidNotes.Text = info.GuideNotes;
            this.txtRemark.Text = info.Remarks;
            this.litDueToway.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.DueToway)), ((int)info.DueToway).ToString(), false);
            this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)info.PaymentType).ToString(), false);
            if (((int)info.PaymentType) == 3)
            {
                this.txtSigningCount.Text = info.SigningCount.ToString();
            }
            this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)info.Status).ToString(), false);

        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void InitRpt()
        {
            GetAllRoomTypeList();

            var items = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, null, false, null, TourId);
            if (items != null && items.Count > 0)
            {
                this.repHotellist.DataSource = items;
                this.repHotellist.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        void Delete()
        {
            string planId = Utils.GetQueryStringValue("planId");

            if (!Privs_AnPai) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            if (string.IsNullOrEmpty(planId)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            bool bllRetCode = new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId);

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
        }
        #endregion
    }
}
