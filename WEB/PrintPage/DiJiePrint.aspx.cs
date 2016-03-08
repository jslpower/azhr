using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.PrintPage
{
    public partial class DiJiePrint : BackPage
    {
        protected MUserInfo SiteUserInfo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string planId = Utils.GetQueryStringValue("planId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(planId) && SiteUserInfo != null)
            {
                InitPage(planId);
            }
            this.Title = PrintTemplateType.地接确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.HPlanStructure.BPlan BLL = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, planId);

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
                //人数
                this.lbPersonNum.Text = string.Format("{0}+{1}+{2}", model.AdultNumber, model.ChildNumber, model.LeaderNumber);
                //接团日期
                this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                //送团日期
                this.lbEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "";
                //接待行程
                this.lbReceiveJourney.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ReceiveJourney);
                //服务标准
                this.lbServiceStandard.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard);
                //费用明细
                this.lbCostDetail.Text = EyouSoft.Common.UtilsCommons.GetAPMX(model.PlanHotelRoomList, EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接.ToString());
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注                                 
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                ////游客信息
                //this.lbCustomerInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CustomerInfo);
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblRemark.Text = model.Remarks;
                lblPayType.Text = (int)model.PaymentType == 0 ? "" : model.PaymentType.ToString();
            }
        }
    }
}
