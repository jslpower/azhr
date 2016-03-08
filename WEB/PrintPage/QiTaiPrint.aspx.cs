using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.PrintPage
{
    public partial class QiTaiPrint : BackPage
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
            this.Title = PrintTemplateType.其它安排确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.HPlanStructure.BPlan BLL = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, planId);
            if (model != null)
            {
                //名称/联系人
                this.txtCompanyName.Text = model.SourceName;
                this.txtCompanyContactName.Text = model.ContactName;
                this.txtContact.Text = model.ContactPhone;
                this.txtFax.Text = model.ContactFax;
                //公司名、联系人
                this.txtSelfName.Text = this.SiteUserInfo.CompanyName;
                this.txtSelfContactName.Text = this.SiteUserInfo.Name;
                this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
                this.txtSelfFax.Text = this.SiteUserInfo.Fax;
                //this.lbRouteName.Text = model.RouteName;
                this.lbTourCode.Text = model.TourCode;
                //人数
                this.lbNum.Text = model.Num.ToString();
                //支付方式
                this.lbPaymentType.Text = model.PaymentType.ToString();
                //支出项目
                this.lbCostDetail.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard);
                //结算费用
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //签发日期
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
