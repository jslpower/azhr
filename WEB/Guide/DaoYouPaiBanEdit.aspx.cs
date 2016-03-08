using System;
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

namespace EyouSoft.Web.Guide
{
    using System.Collections.Generic;

    using EyouSoft.Common;
    using EyouSoft.Model.ComStructure;

    public partial class DaoYouPaiBanEdit : EyouSoft.Common.Page.BackPage
    {
        protected string JobType = "";
        protected string Status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            string name = Utils.GetQueryStringValue("name");
            string dotype = Utils.GetQueryStringValue("dotype");
            string save = Utils.GetQueryStringValue("save");
            string time = Utils.GetQueryStringValue("time");
            string planid = Utils.GetQueryStringValue("planid");
            string type = Utils.GetQueryStringValue("type");
            string guideid = Utils.GetQueryStringValue("guideid");
            if (dotype != "")
            {
                switch (dotype)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave(guideid, planid));
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(deleteGuid(planid));
                        Response.End();
                        break;
                    case "update":
                        GetGuidModel(planid);
                        break;
                }
            }
            if (!IsPostBack)
            {
                DataPageInit(name, time, type, guideid);
                this.GuidControl1.SellsID = guideid;
                this.GuidControl1.SellsName = name;
                var daoYouInfo = new EyouSoft.BLL.ComStructure.BDaoYou().GetInfo(guideid);
                if (daoYouInfo != null) txttel.Text = daoYouInfo.ShouJiHao;
            }
        }

        #region 权限判断
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游排班_导游安排))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游排班_导游安排, false);
                return;
            }



        }

        /// <summary>
        /// 初始化导游信息
        /// </summary>
        /// <param name="name">导游名称</param>
        /// <param name="time">安排时间</param>
        /// <param name="tourid">团号</param>
        /// <param name="type">类型（已安排，假期，停职）</param>
        protected void DataPageInit(string name, string time, string type, string guideid)
        {
            #region 获取导游信息
            //导游姓名
            this.labinfo.Text = "[" + name + time + "]";
            this.lbguidename.Text = name;
            this.hdplantype.Value = type;
            this.hdguideid.Value = guideid;
            #endregion
            var bll = new EyouSoft.BLL.ComStructure.BDaoYou();

            IList<MGuidePlanWork> list = bll.GetGuidePlanWorkInfo(this.SiteUserInfo.CompanyId, guideid, Utils.GetDateTime(time));
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
            else
            {
                phdFrist.Visible = false;
            }
            BindJobType(JobType);
        }
        #endregion

        #region 绑定任务类型
        private void BindJobType(string selecttext)
        {
            List<EnumObj> Guidjob = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType));
            this.ddltype.ClearSelection();
            if (Guidjob != null && Guidjob.Count > 0)
            {
                for (int i = 0; i < Guidjob.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = Guidjob[i].Text;
                    item.Value = Guidjob[i].Value;
                    if (Guidjob[i].Value == selecttext)
                    {
                        item.Selected = true;
                    }
                    this.ddltype.Items.Add(item);
                }
            }
        }
        #endregion

        #region 获取实体
        /// <summary>
        /// 获取导游实体
        /// </summary>
        /// <param name="ID">计调项id</param>
        protected void GetGuidModel(string planId)
        {
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, planId);
                var bll = new EyouSoft.BLL.HTourStructure.BTour();
                if (baseinfo != null)
                {
                    this.LineSelect1.HideSelect = true;
                    var model = bll.GetTourModel(baseinfo.TourId);
                    this.txttel.Text = baseinfo.ContactPhone;
                    if (baseinfo.PlanGuide != null)
                    {
                        this.txtStartAddress.Text = baseinfo.PlanGuide.OnLocation;
                        this.txtEndAddress.Text = baseinfo.PlanGuide.NextLocation;
                        JobType = baseinfo.PlanGuide.TaskType.ToString();
                    }
                    this.GuidControl1.SellsID = baseinfo.SourceId;
                    this.GuidControl1.SellsName = baseinfo.SourceName;
                    if (model != null)
                    {
                        this.txtTourCode.Text = model.TourCode;
                        this.LineSelect1.LineName = model.RouteName;
                    }
                    this.txtDate_Start.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    this.txtDate_End.Text = UtilsCommons.GetDateString(baseinfo.EndDate, ProviderToDate);
                    this.txttravel.Text = baseinfo.ReceiveJourney;
                    this.txtService.Text = baseinfo.ServiceStandard;
                    this.txtrmark.Text = baseinfo.Remarks;
                    //this.txtCostDesc.Text = baseinfo.CostDetail.ToString();
                    this.txtPaidCost.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.ddlState.Items.FindByValue(((int)baseinfo.Status).ToString()).Selected = true;
                    this.txtTourMark.Text = baseinfo.TourMark;
                    this.selSaleMark.Value = baseinfo.SaleMark;
                    this.tourID.Value = baseinfo.TourId;
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="planid">计调编号</param>
        /// <returns></returns>
        private string deleteGuid(string planid)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planid))
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planid))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功!");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败!");
                }
            }
            return msg;
        }
        #endregion

        #region 保存
        private string PageSave(string guideid, string planid)
        {
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            #region 表单赋值
            //导游姓名
            string guidName = Utils.GetFormValue(this.GuidControl1.SellsNameClient);
            string guidID = guideid;
            //电话
            string tel = Utils.GetFormValue(this.txttel.UniqueID);
            //出团时间
            string StartTime = Utils.GetFormValue(this.txtDate_Start.UniqueID);
            //接待行程
            string treval = Utils.GetFormValue(this.txttravel.UniqueID);
            //返回时间
            string endTime = Utils.GetFormValue(this.txtDate_End.UniqueID);
            //上团地点
            string StartAddress = Utils.GetFormValue(this.txtStartAddress.UniqueID);
            //下团地点
            string EndAddress = Utils.GetFormValue(this.txtEndAddress.UniqueID);
            //费用明细
            //string CostDesc = Utils.GetFormValue(this.txtCostDesc.UniqueID);
            //备注
            string remark = Utils.GetFormValue(this.txtrmark.UniqueID);
            //服务标准
            string Service = Utils.GetFormValue(this.txtService.UniqueID);
            //结算费用
            string PaidCost = Utils.GetFormValue(this.txtPaidCost.UniqueID);
            //任务类型
            string guidJobType = Utils.GetFormValue(this.ddltype.UniqueID);
            //计调状态
            string planstatus = Utils.GetFormValue(this.ddlState.UniqueID);
            //团号
            string tourCode = Utils.GetFormValue(this.txtTourCode.UniqueID);
            #endregion
            #region 后台验证
            if (string.IsNullOrEmpty(guidName) && string.IsNullOrEmpty(guideid))
            {
                msg += "请选择导游!<br/>";
            }
            if (string.IsNullOrEmpty(StartAddress))
            {
                msg += "请输入上团地点!<br/>";
            }
            if (string.IsNullOrEmpty(StartTime.ToString()))
            {
                msg += "请选择上团时间!<br/>";
            }
            if (string.IsNullOrEmpty(EndAddress))
            {
                msg += "请输入下团地点!<br/>";
            }
            if (string.IsNullOrEmpty(endTime.ToString()))
            {
                msg += "请选择下团时间!<br/>";
            }
            if (string.IsNullOrEmpty(guidJobType))
            {
                msg += "请选择导游任务类型!<br/>";
            }
            if (Utils.GetDecimal(PaidCost) <= 0)
            {
                msg += "请填写结算费用！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseInfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseInfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseInfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseInfo.Confirmation = Utils.GetDecimal(PaidCost);
            baseInfo.PlanCost = Utils.GetDecimal(PaidCost);
            baseInfo.ContactPhone = tel;
            //baseInfo.CostDetail = CostDesc;
            baseInfo.IssueTime = System.DateTime.Now;
            TimeSpan ts1 = new TimeSpan(Utils.GetDateTime(endTime).Ticks);
            TimeSpan ts2 = new TimeSpan(Utils.GetDateTime(StartTime).Ticks);
            baseInfo.Num = ts1.Subtract(ts2).Duration().Days;
            baseInfo.PlanGuide = new EyouSoft.Model.HPlanStructure.MPlanGuide();
            baseInfo.PlanGuide.NextLocation = EndAddress;
            baseInfo.PlanGuide.OnLocation = StartAddress;
            baseInfo.PlanGuide.TaskType = (EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType), guidJobType);
            baseInfo.SourceName = guidName;
            baseInfo.SourceId = guidID;
            baseInfo.TourId = Utils.GetFormValue("tourID");
            baseInfo.ReceiveJourney = treval;
            baseInfo.Remarks = remark;
            baseInfo.ServiceStandard = Service;
            baseInfo.StartDate = Utils.GetDateTimeNullable(StartTime);
            baseInfo.EndDate = Utils.GetDateTimeNullable(endTime);
            baseInfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游;
            baseInfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), planstatus);
            baseInfo.OperatorId = this.SiteUserInfo.UserId;
            baseInfo.Operator = this.SiteUserInfo.Name;
            baseInfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            #endregion

            #region 提交操作
            //修改
            if (!string.IsNullOrEmpty(planid))
            {
                baseInfo.PlanId = planid;
                baseInfo.PlanGuide.PlanId = planid;
                if (new EyouSoft.BLL.HPlanStructure.BPlan().UpdPlan(baseInfo) > 0)
                {
                    new EyouSoft.BLL.HTourStructure.BTour().UpdateTourMark(new MGuidePlanWork{TourId = baseInfo.TourId,TourMark = Utils.GetFormValue(this.txtTourMark.ClientID),SaleMark = Utils.GetFormValue(this.selSaleMark.ClientID)});
                    msg += "保存成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "保存失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else //添加
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().AddPlan(baseInfo) > 0)
                {
                    new EyouSoft.BLL.HTourStructure.BTour().UpdateTourMark(new MGuidePlanWork { TourId = baseInfo.TourId, TourMark = Utils.GetFormValue(this.txtTourMark.ClientID), SaleMark = Utils.GetFormValue(this.selSaleMark.ClientID) });
                    msg += "保存成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "保存失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            #endregion

            return seterrorMsg;
        }
        #endregion

        #region 弹窗页面设置
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion
    }
}
