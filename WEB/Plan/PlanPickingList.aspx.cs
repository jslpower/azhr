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
    /// 计调中心-领料安排
    /// </summary>
    public partial class PlanPickingList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panyMent = string.Empty;
        protected string status = string.Empty;
        //登录人
        protected string UserId = string.Empty;
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
            UserId = this.SiteUserInfo.UserId;

            DataInit();
           
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {

                //存在ajax请求
                switch (doType)
                {
                    case "delete":
                        Response.Clear();
                        Response.Write(DeletePick());
                        Response.End();
                        break;
                    case "update":
                        GetPickModel();
                        break;
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    default:
                        break;
                }
            }
            #endregion

            
        }

        /// <summary>
        /// 计调支付方式
        /// </summary>
        /// <param name="seleIndex">支付方式id</param>
        /// <returns></returns>
        public string GetOperaterPanyMentList(string seleIndex)
        {
            return UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), seleIndex);
        }

        /// <summary>
        /// 计调状态
        /// </summary>
        /// <param name="selindex">状态id</param>
        /// <returns></returns>
        public string GetOperaterStatusList(string selindex)
        {
            return UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState)).Where(item => (Utils.GetInt(item.Value)) > 2).ToList(), selindex);
        }


        #region 绑定安排的计调领料项
        /// <summary>
        /// 页面初始化
        /// 团号 
        /// </summary>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.HPlanStructure.MPlan> list = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, null, false, null, tourId);
                if (list != null && list.Count > 0)
                {
                    this.repPickList.DataSource = list;
                    this.repPickList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 删除领料计调项
        /// <summary>
        /// 删除领料计调项
        /// </summary>
        /// <param name="planID">计调项id</param>
        /// <returns></returns>
        protected string DeletePick()
        {
            string planId = Utils.GetQueryStringValue("planId");
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

        #region 获取领料实体
        /// <summary>
        /// 获取领料实体
        /// </summary>
        /// <param name="planID"></param>
        protected void GetPickModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.HPlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, planId);
                if (baseinfo != null)
                {
                    this.txtNums.Text = baseinfo.Num.ToString();
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemarks.Text = baseinfo.Remarks;
                    panyMent = ((int)baseinfo.PaymentType).ToString();
                    if (((int)baseinfo.PaymentType) == 3)
                    {
                        this.txtSigningCount.Text = baseinfo.SigningCount.ToString();
                    }
                    status = ((int)baseinfo.Status).ToString();
                    //领料内容 id
                    this.SelectObject1.HideID = baseinfo.SourceId;
                    this.SelectObject1.Name = baseinfo.SourceName;
                    //领料人name                    
                    this.hrSelect.SellsName = baseinfo.ContactName;
                    //领料人id
                    this.hrSelect.SellsID = baseinfo.ContactMobile;//领料人编号
                    this.txtUnitPrices.Text = Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(baseinfo.ContactFax));//领料价格
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存领料计调项
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region  表单取值
            string msg = string.Empty;
            string setErrorMsg = string.Empty;
            //领料id name
            string pickid = Utils.GetFormValue(this.SelectObject1.ClientValue);
            string pickName = Utils.GetFormValue(this.SelectObject1.ClientText);
            //数量
            int pickNums = Utils.GetInt(Utils.GetFormValue(this.txtNums.UniqueID));
            //单价 结算费用
            decimal prices = Utils.GetDecimal(Utils.GetFormValue(this.txtUnitPrices.UniqueID));
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            //领料人
            string pickNames = Utils.GetFormValue(this.hrSelect.SellsNameClient);
            string pickIds = Utils.GetFormValue(this.hrSelect.SellsIDClient);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtOtherRemarks.UniqueID);
            //签单数
            string signingCount = Utils.GetFormValue(this.txtSigningCount.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(pickid) && string.IsNullOrEmpty(pickName))
            {
                msg += "请选择领料内容!<br/>";
            }
            if (pickNums <= 0)
            {
                msg += "请输入领料数量！<br/>";
            }
            if (prices <= 0)
            {
                msg += "请输入单价！<br/>";
            }
            if (totalPrices <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selStatus")))
            {
                msg += "请选择状态！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setErrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.HPlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.HPlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.IsDuePlan = false;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalPrices;
            baseinfo.PlanCost = totalPrices;
            baseinfo.ContactMobile = pickIds;//领料人编号
            baseinfo.ContactFax = prices.ToString();//领料价格
            baseinfo.Remarks = otherMarks;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.SourceId = pickid;
            baseinfo.SourceName = pickName;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("selStatus"));
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料;
            baseinfo.ContactName = pickNames;
            baseinfo.OperatorDeptId = this.SiteUserInfo.DeptId;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.Operator = this.SiteUserInfo.Name;
            baseinfo.Num = pickNums;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPanyMent"));
            baseinfo.SigningCount = Utils.GetInt(Utils.GetFormValue("selPanyMent")) == 3 ? Utils.GetInt(signingCount) : 0;
            #endregion

            #region 提交操作
            string planID = Utils.GetQueryStringValue("planId");
            int result = 0;
            if (!string.IsNullOrEmpty(planID))
            {
                baseinfo.PlanId = planID;
                result = new EyouSoft.BLL.HPlanStructure.BPlan().UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -1)
                {
                    msg += "领料不足，修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = new EyouSoft.BLL.HPlanStructure.BPlan().AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -1)
                {
                    msg += "领料不足，添加失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return setErrorMsg;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_安排领料);
        }
        #endregion
    }
}
