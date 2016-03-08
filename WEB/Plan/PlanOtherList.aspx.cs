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
    /// 计调中心-求他安排
    /// </summary>
    public partial class PlanOtherList : EyouSoft.Common.Page.BackPage
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
            //权限判断
            PowerControl();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.其它安排确认单);

            PageInit();

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(DelOperat());
                        Response.End();
                        break;
                    case "update":
                        GetModel();
                        break;
                    default: break;
                }
            }
            #endregion
        }


        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排其他);
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ///支付方式
                this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), "-1", false);
                //状态
                this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), "-1", false);
                IList<EyouSoft.Model.HPlanStructure.MPlan> List = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, null, false, null, tourId);
                if (List != null && List.Count > 0)
                {
                    this.repList.DataSource = List;
                    this.repList.DataBind();
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

        #region 删除已安排的计调项
        /// <summary>
        /// 删除已安排的计调项
        /// </summary>
        /// <returns></returns>
        protected string DelOperat()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.HPlanStructure.BPlan().DelPlan(planId))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return msg;
        }
        #endregion

        #region 获取计调项实体
        /// <summary>
        /// 获取计调项实体
        /// </summary>
        protected void GetModel()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, planId);
                if (baseinfo != null)
                {
                    this.SupplierControl1.HideID = baseinfo.SourceId;
                    this.SupplierControl1.Name = baseinfo.SourceName;
                    this.txtContentName.Text = baseinfo.ContactName;
                    this.txtContentPhone.Text = baseinfo.ContactPhone;
                    this.txtContentFax.Text = baseinfo.ContactFax;
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtZhichuxm.Text = baseinfo.ServiceStandard;
                    this.txtPepoleNumbers.Text = baseinfo.Num.ToString();
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherMark.Text = baseinfo.Remarks;
                    //支付方式
                    this.litpanyMent.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), ((int)baseinfo.PaymentType).ToString(), false);
                    if (((int)baseinfo.PaymentType) == 3)
                    {
                        this.txtSigningCount.Text = baseinfo.SigningCount.ToString();
                    }
                    //状态
                    this.litOperaterStatus.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), new string[] { "1", "2" }), ((int)baseinfo.Status).ToString(), false);
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //供应商
            string Name = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string Id = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 联系电话 联系传真 人数
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            int num = Utils.GetInt(Utils.GetFormValue(this.txtPepoleNumbers.UniqueID));
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            //导游需知 备注  支出项目 结算费用
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherMark.UniqueID);
            string zhichuxm = Utils.GetFormValue(this.txtZhichuxm.UniqueID);
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(Name))
            {
                msg += "请选择供应商名称!<br/>";
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
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.IsDuePlan = false;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Confirmation = totalMoney;
            baseinfo.PlanCost = totalMoney;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            baseinfo.SigningCount = Utils.GetInt(Utils.GetFormValue("panyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            baseinfo.SourceId = Id;
            baseinfo.SourceName = Name;
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它;
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Num = num;
            baseinfo.ServiceStandard = zhichuxm;
            baseinfo.Remarks = otherRemark;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            baseinfo.Operator = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            string editid = Utils.GetQueryStringValue("planId");
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            int result = 0;
            if (editid != "" && editid != null)
            {
                baseinfo.PlanId = editid;
                result = bll.UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = bll.AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return seterrorMsg;
        }
        #endregion
    }
}