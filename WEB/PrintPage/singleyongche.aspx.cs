using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.PlanStructure;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.PrintPage
{
    public partial class singleyongche : BackPage
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            string planId = Utils.GetQueryStringValue("planId");
             if (!string.IsNullOrEmpty(planId) && SiteUserInfo != null)
            {
                InitPage(planId);
            }
            this.Title = PrintTemplateType.用车确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.PlanStructure.BPlan BLL = new EyouSoft.BLL.PlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, planId);
            if (model != null)
            {
                //地接社名称/联系人
                this.txtCompanyName.Text = model.SourceName;
                this.txtCompanyContactName.Text = model.ContactName;
                this.txtContact.Text = model.ContactPhone;
                this.txtFax.Text = model.ContactFax;
                //公司名、联系人
                this.txtSelfName.Text = this.SiteUserInfo.CompanyName;
                this.txtSelfContactName.Text = this.SiteUserInfo.Name;
                this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
                this.txtSelfFax.Text = this.SiteUserInfo.Fax;
                //线路名称
                //this.lbRouteName.Text = model.RouteName;
                //团号
                this.lbTourCode.Text = model.TourCode;
                //用车时间
                //this.lbStartDate.Text = UtilsCommons.SetDateTimeFormart(model.StartDate) + ":" + model.StartTime + "      -     " + UtilsCommons.SetDateTimeFormart(model.EndDate) + ":" + model.EndTime;
                //用车类型
                //this.lbModels.Text = model.PlanCar.VehicleType.ToString();
                //车型+人数
                //this.lblCarType.Text = model.PlanCar.Models + "(" + model.PlanCar.SeatNumber.ToString() + ")";
                //用车数量
                this.lbNum.Text = model.Num.ToString();
                //付款方式
                this.lbPaymentType.Text = model.PaymentType.ToString();
                //行程
                //this.lbReceiveJourney.Text =
                    EyouSoft.Common.Function.StringValidate.TextToHtml(model.ReceiveJourney);
                //费用明细
                //this.lbCostDetail.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CostDetail);
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //签发日期
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
