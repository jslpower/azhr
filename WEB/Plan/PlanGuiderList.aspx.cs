using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调中心-导游安排
    /// </summary>
    public partial class PlanGuiderList : EyouSoft.Common.Page.BackPage
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
        string TourId = string.Empty;
        
        string PlanId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            PlanId = Utils.GetQueryStringValue("planid");

            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitPrivs();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.导游任务单);

            string doType = Utils.GetQueryStringValue("action");
            switch (doType)
            {
                case "save": PageSave(); break;
                case "delete": Delete(); break;
                default: break;
            }

            InitInfo();
            InitRpt();
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        void PageSave()
        {
            #region 表单赋值
            string seterrorMsg = string.Empty;
            string msg = string.Empty;
            //导游姓名
            string guidName = Utils.GetFormValue(this.guidSelect.SellsNameClient);
            //导游id
            string guidID = Utils.GetFormValue(this.guidSelect.SellsIDClient);
            //导游电话
            string guidPhone = Utils.GetFormValue(this.txtguidPhone.UniqueID);
            //上团地点
            string GroupPlace = Utils.GetFormValue(this.txtGroupPlace.UniqueID);
            //上团时间
            DateTime groupTime = Utils.GetDateTime(Utils.GetFormValue(this.txtGroupTime.UniqueID));
            //下团地点
            string UnderPlace = Utils.GetFormValue(this.txtUnderPlace.UniqueID);
            //下团时间
            DateTime UnderPlaceTime = Utils.GetDateTime(Utils.GetFormValue(this.txtunderTime.UniqueID));
            //服务标准
            string serverStande = Utils.GetFormValue(this.txtserverStand.UniqueID);
            //结算费用
            decimal CostAccount = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            string daoYouXuZhi = Utils.GetFormValue(txtDaoYouXuZhi.UniqueID);
            #endregion

            #region 后台验证
            //if (string.IsNullOrEmpty(guidID) && string.IsNullOrEmpty(guidName))
            //{
            //    msg += "请选择导游!<br/>";
            //}
            if (string.IsNullOrEmpty(GroupPlace))
            {
                msg += "请输入上团地点!<br/>";
            }
            if (string.IsNullOrEmpty(groupTime.ToString()))
            {
                msg += "请选择上团时间!<br/>";
            }
            if (string.IsNullOrEmpty(UnderPlace))
            {
                msg += "请输入下团地点!<br/>";
            }
            if (string.IsNullOrEmpty(UnderPlaceTime.ToString()))
            {
                msg += "请选择下团时间!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("guidType")))
            {
                msg += "请选择导游任务类型!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("states")))
            {
                msg += "请选择计调状态！<br/>";
            }
            if (CostAccount <= 0)
            {
                msg += "请填写结算费用！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseInfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseInfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseInfo.IsDuePlan = false;
            baseInfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseInfo.Confirmation = CostAccount;
            baseInfo.PlanCost = CostAccount;
            baseInfo.ContactPhone = guidPhone;
            baseInfo.IssueTime = System.DateTime.Now;
            baseInfo.Num = new TimeSpan(UnderPlaceTime.Ticks).Subtract(new TimeSpan(groupTime.Ticks)).Duration().Days;
            baseInfo.PlanGuide = new EyouSoft.Model.HPlanStructure.MPlanGuide();
            baseInfo.PlanGuide.NextLocation = UnderPlace;
            baseInfo.PlanGuide.OnLocation = GroupPlace;
            baseInfo.PlanGuide.TaskType = (EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)Utils.GetInt(Utils.GetFormValue("guidType")); ;
            baseInfo.PlanGuide.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetFormValue("selSex"));
            baseInfo.SourceName = guidName;
            baseInfo.SourceId = guidID;
            baseInfo.GuideNotes = daoYouXuZhi;
            baseInfo.ServiceStandard = serverStande;
            baseInfo.StartDate = groupTime;
            baseInfo.EndDate = UnderPlaceTime;
            baseInfo.TourId = TourId;
            baseInfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游;
            baseInfo.OperatorId = this.SiteUserInfo.UserId;
            baseInfo.Operator = this.SiteUserInfo.Name;
            baseInfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            baseInfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            baseInfo.PaymentType = EyouSoft.Model.EnumType.PlanStructure.Payment.现付;
            #endregion

            #region 提交操作
            //修改
            if (!string.IsNullOrEmpty(PlanId))
            {
                baseInfo.PlanId = PlanId;
                baseInfo.PlanGuide.PlanId = PlanId;
                if (new EyouSoft.BLL.HPlanStructure.BPlan().UpdPlan(baseInfo) > 0)
                {
                    msg += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else //添加
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().AddPlan(baseInfo) > 0)
                {
                    msg += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            #endregion

            RCWE(seterrorMsg);
        }
        #endregion

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排导游);
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
                this.litSex.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), new string[] { "2" }), "-1", false);
                this.litGuidType.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)), "-1", false);
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);

                return;
            }

            var info = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, PlanId);
            if (info == null) RCWE("异常请求");

            guidSelect.SellsID = info.SourceId;
            guidSelect.SellsName = info.SourceName;
            txtguidPhone.Text = info.ContactPhone;
            if (info.PlanGuide != null)
            {
                txtGroupPlace.Text = info.PlanGuide.OnLocation;
                txtUnderPlace.Text = info.PlanGuide.NextLocation;
                litSex.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), new string[] { "2" }), ((int)info.PlanGuide.Gender).ToString(), false);
                litGuidType.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)), ((int)info.PlanGuide.TaskType).ToString(), false);
            }
            txtGroupTime.Text = UtilsCommons.GetDateString(info.StartDate, ProviderToDate);
            txtunderTime.Text = UtilsCommons.GetDateString(info.EndDate, ProviderToDate);
            txtserverStand.Text = info.ServiceStandard;
            txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(info.Confirmation);
            litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)info.Status).ToString(), false);
            txtDaoYouXuZhi.Text = info.GuideNotes;
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, null, null, false, null, TourId);
            if (items != null && items.Count > 0)
            {
                this.repGuidList.DataSource = items;
                this.repGuidList.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
        }

        /// <summary>
        /// 删除导游
        /// </summary>
        /// <returns></returns>
        void Delete()
        {
            string planId = Utils.GetQueryStringValue("planId");

            if (!Privs_AnPai) RCWE(UtilsCommons.AjaxReturnJson("0", "无权限"));

            if (string.IsNullOrEmpty(planId)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            bool bllRetCode=new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId);

            if (bllRetCode) RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
        }
        #endregion
    }
}
