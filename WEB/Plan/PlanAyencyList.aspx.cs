using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Text;

namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调中心-地接安排
    /// </summary>
    public partial class PlanAyencyList : EyouSoft.Common.Page.BackPage
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
        /// <summary>
        /// 计划编号
        /// </summary>
        protected string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourId");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            //权限控制
            InitPrivs();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.地接确认单);

            DataInit();

            string doType = Utils.GetQueryStringValue("action");
            switch (doType)
            {
                case "save": PageSave(); break;
                case "delete": DeleteAyency(); break;
                case "update": GetAyencyModel(); break;
                default: break;
            }
        }


        #region private members
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="tourId">团号</param>
        void DataInit()
        {
            //支付方式
            this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
            //状态
            this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);
            //绑定安排的地接计调项
            IList<EyouSoft.Model.HPlanStructure.MPlan> AyencyList = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, null, false, null, TourId);
            if (AyencyList != null && AyencyList.Count > 0)
            {
                this.repAycentylist.DataSource = AyencyList;
                this.repAycentylist.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
            ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            if (ListPower) ListPower = panView.Visible = Privs_AnPai;

            //初始化人数
            var m = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(TourId);
            if (m!=null)
            {
                this.txtAdultNumber.Text = m.Adults.ToString();
                this.txtChildNumber.Text = m.Childs.ToString();
                this.txtLeaderNumber.Text = m.Leaders.ToString();
            }
        }

        /// <summary>
        /// 获取地接实体
        /// </summary>
        /// <param name="ID">计调项id</param>
        void GetAyencyModel()
        {
            string PlanId = Utils.GetQueryStringValue("PlanId");
            if (string.IsNullOrEmpty(PlanId)) RCWE("异常请求");
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo AyencyM = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, PlanId);
            if (AyencyM == null) RCWE("异常请求");
            this.supplierControl1.HideID = AyencyM.SourceId;
            this.supplierControl1.Name = AyencyM.SourceName;
            this.txtContentName.Text = AyencyM.ContactName;
            this.txtContentPhone.Text = AyencyM.ContactPhone;
            this.txtContentFax.Text = AyencyM.ContactFax;
            this.txtContactMobile.Text = AyencyM.ContactMobile;
            this.txtTravel.Text = AyencyM.ReceiveJourney;
            this.txtServerStand.Text = AyencyM.ServiceStandard;
            this.txtAdultNumber.Text = AyencyM.AdultNumber.ToString();
            this.txtChildNumber.Text = AyencyM.ChildNumber.ToString();
            this.txtLeaderNumber.Text = AyencyM.LeaderNumber.ToString();
            this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(AyencyM.Confirmation);
            //支付方式
            this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)AyencyM.PaymentType).ToString(), false);
            if (((int)AyencyM.PaymentType) == 3)
            {
                this.txtSigningCount.Text = AyencyM.SigningCount.ToString();
            }
            //状态
            this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState),new string[] { "1", "2" }), ((int)AyencyM.Status).ToString(), false);
            this.txtStartTime.Text = UtilsCommons.GetDateString(AyencyM.StartDate, ProviderToDate);
            this.txtEndTime.Text = UtilsCommons.GetDateString(AyencyM.EndDate, ProviderToDate);
            this.txtguidNotes.Value = AyencyM.GuideNotes;
            this.txtRemark.Text = AyencyM.Remarks;
        }

        /// <summary>
        /// 获取所有房型
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        public string GetRoomList(string TourId)
        {
            var strRoom = new StringBuilder();
            string planId = Utils.GetQueryStringValue("PlanId");
            IList<EyouSoft.Model.HPlanStructure.MPlanHotelRoom> list = new BLL.HPlanStructure.BPlan().GetRoomList((string.IsNullOrEmpty(planId) ? "0" : "1"), (string.IsNullOrEmpty(planId) ? TourId : planId));
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
                    if (i > 0 && i % 4 == 0) strRoom.Append("<br/>");
                    strRoom.Append("房型：<select name='ddlRoomType' class='inputselect'>");
                    strRoom.Append("<option value=','>-请选择-</option>");
                    for (int j = 0; j < optlist.Count; j++)
                    {
                        strRoom.AppendFormat("<option value='{0},{1}' " + (list[i].Quantity > 0 && list[i].RoomId == optlist[j].RoomId ? "selected='selected'" : "") + " >{1}</option>", optlist[j].RoomId, optlist[j].RoomType);
                    }
                    strRoom.Append("</select>");
                    strRoom.AppendFormat("间数：<input name='txtRoomNumber' type='text' value='{0}' class='inputtext formsize40' />", list[i].Quantity);
                }
            }
            return strRoom.ToString();
        }

        /// <summary>
        /// 删除地接
        /// </summary>
        /// <returns></returns>
        void DeleteAyency()
        {
            if (!Privs_AnPai) RCWE(UtilsCommons.AjaxReturnJson("0", "没有操作权限!"));

            string planId = Utils.GetQueryStringValue("PlanId");

            if (string.IsNullOrEmpty(planId)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求!"));

            if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId))
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排地接);
        }

        public string GetYFJE(decimal Confirmation, string planID)
        {
            decimal yfje = 0;
            IList<EyouSoft.Model.FinStructure.MRegister> paylist = new EyouSoft.BLL.FinStructure.BFinance().GetPayRegisterLstByPlanId(planID);
            if (paylist != null && paylist.Count > 0)
            {
                for(int i=0;i<paylist.Count;i++)
                {
                    yfje += paylist[i].PaymentAmount;
                }
            }
            if (Confirmation == 0)
                return yfje.ToString("C2");
            else
                return (Confirmation - yfje).ToString("C2");

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        void PageSave()
        {
            #region 表单赋值
            string msgArr = string.Empty;
            string SeterrorMsg = string.Empty;
            //地接社id
            string AyencyNameid = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //地接社名称
            string AyencyName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //联系人
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            //联系电话
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            string contactMobile = Utils.GetFormValue(this.txtContactMobile.UniqueID);
            //联系传真
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            //接待行程
            string Travel = Utils.GetFormValue(this.txtTravel.UniqueID);
            //服务标准
            string ServerStand = Utils.GetFormValue(this.txtServerStand.UniqueID);
            //人数
            string adultNumber = Utils.GetFormValue(this.txtAdultNumber.UniqueID);
            string childNumber = Utils.GetFormValue(this.txtChildNumber.UniqueID);
            string leaderNumber = Utils.GetFormValue(this.txtLeaderNumber.UniqueID);
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //结算费用
            decimal CostAccount = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            //接团日期
            DateTime? TourStarTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtStartTime.UniqueID));
            //送团日期
            DateTime? TourEndTime = Utils.GetDateTime(Utils.GetFormValue(this.txtEndTime.UniqueID));
            //导游需知
            string guidNeet = Utils.GetFormValue("txtguidNotes");
            //备注
            string remark = Utils.GetFormValue(this.txtRemark.UniqueID);
            //房型  数量
            string[] roomType = Utils.GetFormValues("ddlRoomType");
            string[] numbers = Utils.GetFormValues("txtRoomNumber");
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(AyencyName) && string.IsNullOrEmpty(AyencyNameid))
            {
                msgArr += "请选择地接社名称!<br/>";
            }
            if (CostAccount <= 0)
            {
                msgArr += "请填写结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(TourStarTime.ToString()))
            {
                msgArr += "请选择接团日期!<br/>";
            }
            if (string.IsNullOrEmpty(TourEndTime.ToString()))
            {
                msgArr += "请选择送团日期!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("states")))
            {
                msgArr += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("panyMent")))
            {
                msgArr += "请选择支付方式！<br/>";
            }
            if (msgArr != "")
            {
                SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");

                RCWE(SeterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlan Ayencymodel = new EyouSoft.Model.HPlanStructure.MPlan();
            Ayencymodel.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            Ayencymodel.IsDuePlan = false;
            Ayencymodel.CompanyId = this.SiteUserInfo.CompanyId;
            Ayencymodel.Confirmation = CostAccount;
            Ayencymodel.PlanCost = CostAccount;
            Ayencymodel.ContactFax = contectFax;
            Ayencymodel.ContactName = contectName;
            Ayencymodel.ContactPhone = contectPhone;
            Ayencymodel.ContactMobile = contactMobile;
            Ayencymodel.GuideNotes = guidNeet;
            Ayencymodel.IssueTime = System.DateTime.Now;
            Ayencymodel.Num = Utils.GetInt(adultNumber) + Utils.GetInt(childNumber);
            Ayencymodel.AdultNumber = Utils.GetInt(adultNumber);
            Ayencymodel.ChildNumber = Utils.GetInt(childNumber);
            Ayencymodel.LeaderNumber = Utils.GetInt(leaderNumber);
            Ayencymodel.PaymentType = (Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            Ayencymodel.SigningCount = Utils.GetInt(Utils.GetFormValue("panyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            Ayencymodel.ReceiveJourney = Travel;
            Ayencymodel.Remarks = remark;
            Ayencymodel.SourceId = AyencyNameid;
            Ayencymodel.SourceName = AyencyName;
            Ayencymodel.Status = (PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            Ayencymodel.TourId = Utils.GetQueryStringValue("tourId");
            Ayencymodel.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接;
            Ayencymodel.ServiceStandard = ServerStand;
            Ayencymodel.StartDate = TourStarTime;
            Ayencymodel.EndDate = TourEndTime;
            Ayencymodel.OperatorId = this.SiteUserInfo.UserId;
            Ayencymodel.Operator = this.SiteUserInfo.Name;
            Ayencymodel.OperatorDeptId = this.SiteUserInfo.DeptId;
            Ayencymodel.PlanHotelRoomList = new List<EyouSoft.Model.HPlanStructure.MPlanHotelRoom>();
            for (int i = 0; i < roomType.Length; i++)
            {
                if (roomType[i] != "," && !string.IsNullOrEmpty(roomType[i].Split(',')[0]) && !string.IsNullOrEmpty(roomType[i].Split(',')[1]) && Utils.GetInt(numbers[i]) > 0)
                {
                    var roomHotel = new EyouSoft.Model.HPlanStructure.MPlanHotelRoom();
                    roomHotel.RoomType = roomType[i].Split(',')[1];
                    roomHotel.RoomId = roomType[i].Split(',')[0];
                    roomHotel.Quantity = Utils.GetInt(numbers[i]);
                    Ayencymodel.PlanHotelRoomList.Add(roomHotel);
                    roomHotel = null;
                }
            }
            #endregion

            #region 提交操作
            //地接id
            string editid = Utils.GetQueryStringValue("PlanID");
            //修改
            if (editid != "" && editid != null)
            {
                Ayencymodel.PlanId = editid;
                for (int i = 0; i < Ayencymodel.PlanHotelRoomList.Count; i++)
                {
                    Ayencymodel.PlanHotelRoomList[i].PlanId = editid;
                }
                if (new EyouSoft.BLL.HPlanStructure.BPlan().UpdPlan(Ayencymodel) > 0)
                {
                    msgArr += "修改成功！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msgArr + "");

                }
                else
                {
                    msgArr += "修改失败！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                }
            }
            else //添加
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().AddPlan(Ayencymodel) > 0)
                {
                    msgArr += "添加成功！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msgArr + "");
                }
                else
                {
                    msgArr += "添加失败！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                }
            }
            #endregion

            RCWE(SeterrorMsg);
        }
        #endregion
    }
}
