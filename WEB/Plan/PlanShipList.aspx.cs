﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调中心-区间交通-轮船
    /// </summary>
    public partial class PlanShipList : EyouSoft.Common.Page.BackPage
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.国内游轮确认单);

            DataInit();

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (!string.IsNullOrEmpty(doType))
            {
                //存在ajax请求
                switch (doType)
                {
                    case "save": PageSave(); break;
                    case "delete": DeleteLarge(); break;
                    case "update": GetLargeModel(); break;
                    default: break;
                }
            }
            #endregion
        }

        #region 初始化舱位 乘客类型
        /// <summary>
        /// 初始化舱位 乘客类型
        /// </summary>
        /// <param name="selected">选中的id</param>
        /// <returns></returns>
        protected string seleSpaceTypeHtml(string selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> spaceType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType));
            if (spaceType != null && spaceType.Count > 0)
            {
                sb.Append("<select name=\"seleSpaceType\" class=\"inputselect\" valid=\"required\" errmsg=\"*请选择舱位!\">");
                for (int i = 0; i < spaceType.Count; i++)
                {
                    if (spaceType[i].Value == selected)
                    {
                        sb.Append("<option value='" + spaceType[i].Value + "' selected='selected'>" + spaceType[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + spaceType[i].Value + "'>" + spaceType[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        protected string PassengerstypeHtml(string Selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> PassengerType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType));
            if (PassengerType != null && PassengerType.Count > 0)
            {
                sb.Append("<select name=\"Passengerstype\" class=\"inputselect\" valid=\"required\" errmsg=\"*请选择乘客类型!\">");
                for (int i = 0; i < PassengerType.Count; i++)
                {
                    if (PassengerType[i].Value == Selected)
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "' selected='selected'>" + PassengerType[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "'>" + PassengerType[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                //支付方式
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                //状态
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);

                IList<EyouSoft.Model.HPlanStructure.MPlan> LargeList = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, null, null, false, null, tourId);
                if (LargeList != null && LargeList.Count > 0)
                {
                    this.repLargeList.DataSource = LargeList;
                    this.repLargeList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }

                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;
            }
            else
            {
                RCWE("异常请求");
            }
        }
        #endregion

        #region 删除已安排区间交通
        /// <summary>
        /// 删除已安排区间交通
        /// </summary>
        /// <returns></returns>
        void DeleteLarge()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            string mesg = "";
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId))
                {
                    mesg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    mesg = UtilsCommons.AjaxReturnJson("0", "删除失败!");
                }
            }

            RCWE(mesg);
        }
        #endregion

        #region 获取区间交通实体
        /// <summary>
        /// 获取区间交通实体
        /// </summary>
        protected void GetLargeModel()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo LargeModel = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, planId);
                if (LargeModel != null)
                {
                    this.SupplierControl1.HideID = LargeModel.SourceId;
                    this.SupplierControl1.Name = LargeModel.SourceName;
                    this.txtContentFax.Text = LargeModel.ContactFax;
                    this.txtContentName.Text = LargeModel.ContactName;
                    this.txtContentPhone.Text = LargeModel.ContactPhone;
                    if (LargeModel.PlanLargeFrequencyList != null && LargeModel.PlanLargeFrequencyList.Count > 0)
                    {
                        this.tabHolderView.Visible = false;
                        this.repFilght.DataSource = LargeModel.PlanLargeFrequencyList;
                        this.repFilght.DataBind();
                    }
                    else
                    {
                        this.tabHolderView.Visible = true;
                    }
                    this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(LargeModel.Confirmation);
                    this.txtGuidNotes.Text = LargeModel.GuideNotes;
                    this.txtOtherMark.Text = LargeModel.Remarks;
                    //支付方式
                    this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)LargeModel.PaymentType).ToString(), false);
                    if (((int)LargeModel.PaymentType) == 3)
                    {
                        this.txtSigningCount.Text = LargeModel.SigningCount.ToString();
                    }
                    //状态
                    this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)LargeModel.Status).ToString(), false);
                }
            }
        }
        #endregion

        #region 保存区间交通
        void PageSave()
        {
            #region 表单赋值
            string message = string.Empty;
            string seterrorMsg = string.Empty;
            //出票点
            string votesID = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //出票点id
            string votesName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            //出发时间 出发地 目的地 航班号
            string[] startdate = Utils.GetFormValues("txtstartTime");
            string[] startTime = Utils.GetFormValues("txtStartHours");
            string[] startPlace = Utils.GetFormValues("txtstartPlace");
            string[] endplace = Utils.GetFormValues("txtendPlace");
            string[] filghtNum = Utils.GetFormValues("txtFilghtnumbers");
            //舱位 乘客类型 人数  价格 保险 机建费 附加费 折扣 小计
            string[] seleSpaceType = Utils.GetFormValues("seleSpaceType");
            string[] Passengerstype = Utils.GetFormValues("Passengerstype");
            string[] peopleNums = Utils.GetFormValues("txtpeopleNums");
            string[] prices = Utils.GetFormValues("txtprices");
            string[] baoxian = Utils.GetFormValues("txtBaoxian");
            string[] jijian = Utils.GetFormValues("txtJijian");
            string[] fujia = Utils.GetFormValues("txtFujia");
            string[] zhekou = Utils.GetFormValues("txtZhekou");
            string[] TotalPrices = Utils.GetFormValues("txtXiaoJi");
            //结算费用
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherMark.UniqueID);
            #endregion

            #region 验证
            if (string.IsNullOrEmpty(votesID) && string.IsNullOrEmpty(votesName))
            {
                message += "请选择出票点!<br/>";
            }
            if (startdate.Length > 0)
            {
                for (int i = 0; i < startdate.Length; i++)
                {
                    if (string.IsNullOrEmpty(startdate[i]))
                    {
                        message += "*请选择出发日期!<br/>";
                    }
                }
            }
            //if (startTime.Length > 0)
            //{
            //    for (int i = 0; i < startTime.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(startTime[i]))
            //        {
            //            message += "*请输入出发时间！<br/>";
            //        }
            //    }
            //}
            //if (startPlace.Length > 0)
            //{
            //    for (int i = 0; i < startPlace.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(startPlace[i]))
            //        {
            //            message += "*请输入出发地!<br/>";
            //        }
            //    }
            //}
            //if (endplace.Length > 0)
            //{
            //    for (int i = 0; i < startPlace.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(endplace[i]))
            //        {
            //            message += "*请输入目的地!<br/>";
            //        }
            //    }
            //}
            if (filghtNum.Length > 0)
            {
                for (int i = 0; i < filghtNum.Length; i++)
                {
                    if (string.IsNullOrEmpty(filghtNum[i]))
                    {
                        message += "*请输入航班号!<br/>";
                    }
                }
            }

            if (seleSpaceType.Length > 0)
            {
                for (int i = 0; i < seleSpaceType.Length; i++)
                {
                    if (string.IsNullOrEmpty(seleSpaceType[i]))
                    {
                        message += "*请选择舱位!<br/>";
                    }
                }
            }
            if (Passengerstype.Length > 0)
            {
                for (int i = 0; i < Passengerstype.Length; i++)
                {
                    if (string.IsNullOrEmpty(Passengerstype[i]))
                    {
                        message += "*请选择乘客类型!<br/>";
                    }
                }
            }
            if (peopleNums.Length > 0)
            {
                for (int i = 0; i < peopleNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(peopleNums[i]) || Utils.GetDecimal(peopleNums[i]) <= 0)
                    {
                        message += "*请输入人数!<br/>";
                    }
                }
            }
            if (prices.Length > 0)
            {
                for (int i = 0; i < prices.Length; i++)
                {
                    if (string.IsNullOrEmpty(prices[i]) || Utils.GetDecimal(prices[i]) <= 0)
                    {
                        message += "*请输入价格!<br/>";
                    }
                }
            }
            //if (baoxian.Length > 0)
            //{
            //    for (int i = 0; i < baoxian.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(baoxian[i]) || Utils.GetDecimal(baoxian[i]) <= 0)
            //        {
            //            message += "*请输入保险!<br/>";
            //        }
            //    }
            //}
            //if (jijian.Length > 0)
            //{
            //    for (int i = 0; i < jijian.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(jijian[i]) || Utils.GetDecimal(jijian[i]) <= 0)
            //        {
            //            message += "*请输入机建费!<br/>";
            //        }
            //    }
            //}
            //if (fujia.Length > 0)
            //{
            //    for (int i = 0; i < fujia.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(fujia[i]) || Utils.GetDecimal(fujia[i]) <= 0)
            //        {
            //            message += "*请输入附加费!<br/>";
            //        }
            //    }
            //}
            //if (zhekou.Length > 0)
            //{
            //    for (int i = 0; i < zhekou.Length; i++)
            //    {
            //        if (string.IsNullOrEmpty(zhekou[i]) || Utils.GetDecimal(zhekou[i]) <= 0)
            //        {
            //            message += "*请输入折扣!<br/>";
            //        }
            //    }
            //}
            if (TotalPrices.Length > 0)
            {
                for (int i = 0; i < TotalPrices.Length; i++)
                {
                    if (string.IsNullOrEmpty(TotalPrices[i]) || Utils.GetDecimal(TotalPrices[i]) <= 0)
                    {
                        message += "*请输入小计费用!<br/>";
                    }
                }
            }

            if (totalPrices <= 0)
            {
                message += "请填写结算费用!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("states")))
            {
                message += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("panyMent")))
            {
                message += "请选择支付方式！<br/>";
            }
            if (message != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo LargeBase = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            LargeBase.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            LargeBase.IsDuePlan = false;
            LargeBase.CompanyId = this.SiteUserInfo.CompanyId;
            LargeBase.Confirmation = totalPrices;
            LargeBase.ContactFax = contectFax;
            LargeBase.ContactName = contectName;
            LargeBase.ContactPhone = contectPhone;
            LargeBase.GuideNotes = guidNotes;
            LargeBase.IssueTime = System.DateTime.Now;
            LargeBase.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            LargeBase.SigningCount = Utils.GetInt(Utils.GetFormValue("panyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            LargeBase.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            LargeBase.Num = 0;
            //航班
            LargeBase.PlanLargeFrequencyList = new List<EyouSoft.Model.HPlanStructure.MPlanLargeFrequency>();
            for (int i = 0; i < startdate.Length; i++)
            {
                EyouSoft.Model.HPlanStructure.MPlanLargeFrequency large = new EyouSoft.Model.HPlanStructure.MPlanLargeFrequency();
                large.Departure = startPlace[i];
                large.DepartureTime = Utils.GetDateTimeNullable(startdate[i]);
                large.Destination = endplace[i];
                large.Numbers = filghtNum[i];
                large.Time = startTime[i];
                large.AdultsType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType), Passengerstype[i]);
                large.PepolePayNum = Utils.GetInt(peopleNums[i]);
                large.FarePrice = Utils.GetDecimal(prices[i]);
                large.SeatType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType), seleSpaceType[i]);
                large.Surcharge = Utils.GetDecimal(fujia[i]);
                large.Discount = float.Parse(zhekou[i]);
                large.Fee = Utils.GetDecimal(jijian[i]);
                large.InsuranceHandlFee = Utils.GetDecimal(baoxian[i]);
                large.SumPrice = Utils.GetDecimal(TotalPrices[i]);
                LargeBase.PlanLargeFrequencyList.Add(large);
                LargeBase.Num += EyouSoft.Common.Utils.GetInt(peopleNums[i]);
            }
            LargeBase.Remarks = otherRemark;
            LargeBase.SourceId = votesID;
            LargeBase.SourceName = votesName;
            LargeBase.TourId = Utils.GetQueryStringValue("tourId");
            LargeBase.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船;
            LargeBase.PlanCost = totalPrices;
            LargeBase.OperatorId = this.SiteUserInfo.UserId;
            LargeBase.Operator = this.SiteUserInfo.Name;
            LargeBase.OperatorDeptId = this.SiteUserInfo.DeptId;
            #endregion

            #region 提交操作
            string editid = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(editid))
            {
                LargeBase.PlanId = editid;
                for (int i = 0; i < LargeBase.PlanLargeFrequencyList.Count; i++)
                {
                    LargeBase.PlanLargeFrequencyList[i].PlanId = editid;
                }
                if (new EyouSoft.BLL.HPlanStructure.BPlan().UpdPlan(LargeBase) > 0)
                {
                    message += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + message + "");
                }
                else
                {
                    message += "修改失败！";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                }
            }
            else
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().AddPlan(LargeBase) > 0)
                {
                    message += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + message + "");
                }
                else
                {
                    message += "添加失败！";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                }
            }
            RCWE(seterrorMsg);
            #endregion
        }
        #endregion

        #region  权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排区间交通);
        }
        #endregion
    }
}
