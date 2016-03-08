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
    /// 预付申请页面
    /// </summary>
    public partial class PrepaidAppliaction : EyouSoft.Common.Page.BackPage
    {
        /// <summary>
        /// 是否本团计调
        /// </summary>
        protected static bool _isPlaner = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            DateInit();
            DataInitPayList();

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
                        Response.Write(DeletPayPlan());
                        Response.End();
                        break;
                    case "update":
                        GetPayResgin();
                        break;
                    case "savePlan":
                        Response.Clear();
                        Response.Write(PageSaveSellConfirm(EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批));
                        Response.End();
                        break;
                    case "saveSale":
                        Response.Clear();
                        Response.Write(PageSaveSellConfirm(EyouSoft.Model.EnumType.FinStructure.FinStatus.销售待确认));
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion

        }

        #region 权限判断
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_预付申请))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_预付申请, false);
                return;
            }
        }
        #endregion

        #region 初始化团队信息
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <param name="souceName">供应商名称</param>
        protected void DateInit()
        {
            string tourID = Utils.GetQueryStringValue("tourId");
            string planID = Utils.GetQueryStringValue("PlanId");
            string souceName = Utils.GetQueryStringValue("souceName");
            this.txtPayDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            this.litAppPeople.Text = SiteUserInfo.Name;
            this.litAppDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.HTourStructure.MTour tourInfo = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourID);
                if (tourInfo != null)
                {
                    _isPlaner = tourInfo.TourPlanerList.Where(m => m.PlanerId == this.SiteUserInfo.UserId).Count() > 0;
                    this.panView.Visible = _isPlaner;
                    this.litTourCode.Text = tourInfo.TourCode;
                    this.litSellers.Text = tourInfo.SellerName;
                }
                else
                {
                    this.panView.Visible = false;
                }
            }
            else
            {
                this.panView.Visible = false;
            }

            if (planID != "")
            {
                EyouSoft.BLL.FinStructure.BFinance bll = new EyouSoft.BLL.FinStructure.BFinance();
                //支出项目基本信息
                EyouSoft.Model.FinStructure.MPayRegister model = bll.GetPayRegisterBaseByPlanId(planID);
                if (model != null)
                {
                    if (model.PaymentType == EyouSoft.Model.EnumType.PlanStructure.Payment.现付)
                    {
                        this.panView.Visible = false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(souceName))
            {
                this.litPrepaidCompany.Text = souceName;
            }


            //this.panView.Visible = true;
        }
        /// <summary>
        /// 根据计调项目获取某一个支出项目出账等级列表
        /// </summary>
        /// <param name="planID">计调项id</param>
        protected void DataInitPayList()
        {
            string planID = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planID))
            {
                IList<EyouSoft.Model.FinStructure.MRegister> paylist = new EyouSoft.BLL.FinStructure.BFinance().GetPayRegisterLstByPlanId(planID);

                if (paylist != null && paylist.Count > 0)
                {
                    this.repPrepaidList.DataSource = paylist;
                    this.repPrepaidList.DataBind();
                }
            }
        }
        #endregion

        #region 删除计调项出账登记
        /// <summary>
        /// 删除计调项出账登记
        /// </summary>
        /// <param name="registerID">出账登记编号</param>
        /// <returns></returns>
        protected string DeletPayPlan()
        {
            string msg = string.Empty;
            string Id = Utils.GetQueryStringValue("ID");
            if (!string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.FinStructure.BFinance().DelRegister(this.SiteUserInfo.CompanyId, Convert.ToInt32(Id)))
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

        #region 获取计调项出账等级实体
        /// <summary>
        /// 获取计调项出账等级实体
        /// </summary>
        /// <param name="registerID">出账等级id</param>
        protected void GetPayResgin()
        {
            string ID = Utils.GetQueryStringValue("ID");
            if (!string.IsNullOrEmpty(ID))
            {
                EyouSoft.Model.FinStructure.MRegister registerM = new EyouSoft.BLL.FinStructure.BFinance().GetRegisterById(this.SiteUserInfo.CompanyId, Utils.GetInt(ID));
                if (registerM != null)
                {
                    this.txtPrepaidPrices.Text = Utils.FilterEndOfTheZeroDecimal(registerM.PaymentAmount);
                    this.txtPayDate.Text = UtilsCommons.GetDateString(registerM.Deadline, ProviderToDate);
                    this.txtUseInterpret.Text = registerM.Remark;
                    this.litAppPeople.Text = registerM.Operator;
                    this.litAppDate.Text = UtilsCommons.GetDateString(registerM.IssueTime, ProviderToDate);
                    if (registerM.Status == EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付 || registerM.Status == EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付)
                    {
                        this.panView.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 确认销售 计调确认
        /// <summary>
        /// 预付申请 销售确认 计调确认
        /// </summary>
        /// <param name="RegisterId">登记id</param>
        /// <param name="tourID">团号</param>
        /// <param name="planID">计调id</param>
        /// <returns></returns>
        protected string PageSaveSellConfirm(EyouSoft.Model.EnumType.FinStructure.FinStatus status)
        {
            string setErorMsg = string.Empty;
            string msg = string.Empty;
            string tourID = Utils.GetQueryStringValue("tourId");
            string planID = Utils.GetQueryStringValue("PlanId");
            decimal prepaidPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtPrepaidPrices.UniqueID));
            DateTime? dtPayDate = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtPayDate.UniqueID));
            if (prepaidPrices <= 0)
            {
                msg += "请输入预付金额！<br/>";
            }
            if (dtPayDate == null)
            {
                msg += "请选择最晚付款日期！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                setErorMsg = "{\"result\":\"0\",\"msg\":\"" + msg + "\"}";
                return setErorMsg;
            }
            EyouSoft.Model.FinStructure.MRegister registerM = new EyouSoft.Model.FinStructure.MRegister();
            registerM.CompanyId = this.SiteUserInfo.CompanyId;
            registerM.Deadline = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtPayDate.UniqueID));
            //付款人 
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.HTourStructure.MTour tourInfo = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourID);
                if (tourInfo != null)
                {
                    //this.litTourCode.Text = tourInfo.TourCode;
                    if (!string.IsNullOrEmpty(tourInfo.SellerId))
                    {
                        //this.litSellers.Text = tourInfo.SellerName;
                        registerM.Dealer = tourInfo.SellerName;
                        registerM.DealerDeptId = tourInfo.SellerDeptId;
                        registerM.DealerId = tourInfo.SellerId;
                    }
                }
            }
            registerM.IsPrepaid = true;
            registerM.PaymentDate = System.DateTime.Now;
            registerM.PaymentAmount = Utils.GetDecimal(Utils.GetFormValue(this.txtPrepaidPrices.UniqueID));
            registerM.PlanId = planID;
            registerM.Remark = Utils.GetFormValue(this.txtUseInterpret.UniqueID);
            registerM.TourId = tourID;
            registerM.OperatorId = this.SiteUserInfo.UserId;
            registerM.Operator = this.SiteUserInfo.Name;
            registerM.DeptId = this.SiteUserInfo.DeptId;
            registerM.Status = status;
            registerM.IssueTime = DateTime.Now;
            registerM.Supplier = Utils.GetQueryStringValue("souceName");
            registerM.SupplierId = Utils.GetQueryStringValue("soucesId");
            if (string.IsNullOrEmpty(Utils.GetQueryStringValue("ID")))
            {
                var intRtn = new EyouSoft.BLL.FinStructure.BFinance().AddRegister(registerM);
                switch (intRtn)
                {
                    case 0:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("0", "确认失败!"));
                        break;
                    case -1:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "超额付款!"));
                        break;
                    default:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1", "确认成功"));
                        break;
                }
            }
            else
            {
                registerM.RegisterId = Utils.GetInt(Utils.GetQueryStringValue("ID"));
                var intRtn = new EyouSoft.BLL.FinStructure.BFinance().UpdRegister(registerM);
                switch (intRtn)
                {
                    case 0:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("0", "确认失败!"));
                        break;
                    case -1:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "超额付款!"));
                        break;
                    default:
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1", "确认成功"));
                        break;
                }
            }

            return setErorMsg;
        }
        #endregion

        #region 财务状态
        /// <summary>
        /// /财务状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected bool GetFinStatus(EyouSoft.Model.EnumType.FinStructure.FinStatus status)
        {
            bool result = false;
            if (status != EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付 && status != EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付 && _isPlaner)
            {
                result = true;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
    }
}
