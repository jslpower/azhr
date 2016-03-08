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

namespace EyouSoft.Web.Fin
{
    using System.Collections.Generic;

    using EyouSoft.Common.Page;
    using EyouSoft.Common;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.BLL.ComStructure;

    public partial class YingFuDeng : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType").Length > 0)
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {

            BFinance bll = new BFinance();
            string planId = Utils.GetQueryStringValue("planID");

            if (planId != "")
            {
                //支出项目基本信息
                MPayRegister model = bll.GetPayRegisterBaseByPlanId(planId);
                if (model != null)
                {
                    lbl_listTitle.Text = "计调项:" + model.PlanTyp + "   单位名称:" + model.Supplier + "   结算金额:" + UtilsCommons.GetMoneyString(model.Payable, ProviderToMoney);
                    lbl_listTitle.Text += "   已付金额" + UtilsCommons.GetMoneyString(model.Paid, ProviderToMoney) + "   未付金额:" + UtilsCommons.GetMoneyString(model.Unpaid, ProviderToMoney);

                    if (model.PaymentType == EyouSoft.Model.EnumType.PlanStructure.Payment.现付)
                    {
                        this.phdAdd.Visible = false;
                    }
                }
                //支出项目登记列表
                IList<MRegister> ls = bll.GetPayRegisterLstByPlanId(planId);
                if (ls != null && ls.Count > 0)
                {
                    rpt_list.DataSource = ls;
                    rpt_list.DataBind();
                }
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string msg = string.Empty;
            MRegister model = new MRegister();
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "Add":
                    if (GetVal(model, ref msg))
                    {
                        var intRtn = new BFinance().AddRegister(model);
                        switch (intRtn)
                        {
                            case 0:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                                break;
                            case -1:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "超额付款!"));
                                break;
                            default:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                                break;
                        }
                        //AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().AddRegister(model) > 0 ? "1" : "-1", "添加失败!"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
                    }

                    break;
                case "Updata":
                    if (GetVal(model, ref msg))
                    {
                        var intRtn = new BFinance().UpdRegister(model);
                        switch (intRtn)
                        {
                            case 0:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                                break;
                            case -1:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "超额付款!"));
                                break;
                            default:
                                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                                break;
                        }
                        //AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().UpdRegister(model) ? "1" : "-1", "修改失败!"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
                    }
                    break;
                case "Delete":
                    AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().DelRegister(CurrentUserCompanyID, Utils.GetInt(Utils.GetFormValue("registerId"))) ? "1" : "-1", "删除失败!"));
                    break;
            }


            AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 获取参数并验证
        /// </summary>
        /// <param name="model">赋值实体</param>
        /// <param name="msg">验证提示语</param>
        /// <returns></returns>
        private bool GetVal(MRegister model, ref string msg)
        {
            //系统配置实体
            MComSetting comModel = new BComSetting().GetModel(CurrentUserCompanyID) ?? new MComSetting();
            model = model ?? new MRegister();
            model.Status = comModel.FinancialExpensesReview ? FinStatus.财务待审批 : FinStatus.账务已支付;
            model.RegisterId = Utils.GetInt(Utils.GetFormValue("registerId"));
            model.PlanId = Utils.GetFormValue("planID");
            model.TourId = Utils.GetFormValue("tourID");
            model.CompanyId = CurrentUserCompanyID;
            model.Operator = model.Planer = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            model.IssueTime = DateTime.Now;
            model.PaymentDate = Utils.GetDateTimeNullable(Utils.GetFormValue("txt_paymentDate"));
            model.Deadline = Utils.GetDateTime(Utils.GetFormValue("txt_deadline"));
            model.PaymentType = Utils.GetInt(Utils.GetFormValue("sel_paymentType"));
            model.Remark = Utils.GetFormValue("txt_remark");
            model.PaymentAmount = Utils.GetDecimal(Utils.GetFormValue("txt_paymentAmount"));
            //收退款人
            model.Dealer = Request.Form[txt_Sells.SellsNameClient] ?? Utils.GetFormValue(Utils.GetFormValue("sellsFormKey") + txt_Sells.SellsNameClient);
            //收退款人ID
            model.DealerId = Request.Form[txt_Sells.SellsIDClient] ?? Utils.GetFormValue(Utils.GetFormValue("sellsFormKey") + txt_Sells.SellsIDClient);
            msg += model.PaymentDate != DateTime.MinValue ? string.Empty : "付款时间错误!<br/>";
            msg += model.Dealer.Length > 0 && model.DealerId.Length > 0 ? string.Empty : "收退款人异常!<br/>";
            msg += model.Deadline != DateTime.MinValue ? string.Empty : "最晚付款时间错误!<br/>";
            msg += model.PaymentType > 0 ? string.Empty : "请选择付款方式错误!<br/>";
            msg += model.PaymentAmount > 0 ? string.Empty : "付款金额错误!<br/>";

            return msg.Length <= 0;
        }
    }
}
