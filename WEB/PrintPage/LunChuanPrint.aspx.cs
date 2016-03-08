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
    public partial class LunChuanPrint : BackPage
    {

        protected MUserInfo SiteUserInfo = null;
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string planId = Utils.GetQueryStringValue("planId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(planId) && SiteUserInfo != null)
            {
                InitPage(planId);
            }
            this.Title = PrintTemplateType.涉外游轮确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.HPlanStructure.BPlan BLL = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船, planId);

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
                //线路名称
                //this.lbRouteName.Text = model.RouteName;
                //团号
                this.lbTourCode.Text = model.TourCode;
                this.lbCostDesc.Text = EyouSoft.Common.UtilsCommons.GetAPMX(model.PlanLargeFrequencyList, EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString());
                this.lbTotleCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                this.lblPayType.Text = (int)model.PaymentType == 0 ? "" : model.PaymentType.ToString();
                this.lbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                if (model.PlanLargeFrequencyList != null && model.PlanLargeFrequencyList.Count > 0)
                {
                    listCount = model.PlanLargeFrequencyList.Count;
                    this.rptlist.DataSource = model.PlanLargeFrequencyList;
                    this.rptlist.DataBind();
                }


                ////船名
                //this.lbShipName.Text = model.PlanShip.ShipName;
                ////登船日期
                //this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                ////登船码头
                //this.lbLoadDock.Text = model.PlanShip.LoadDock;
                ////航行
                //this.lbLine.Text = model.PlanShip.Line;
                ////停靠景点
                //this.lbSight.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.PlanShip.Sight);
                ////费用明细
                //this.lbCostDetail.Text = UtilsCommons.GetAPMX(model.PlanCarList, EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船.ToString());
                ////费用总额
                //this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                ////备注
                //this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                ////游客信息
                //this.lbCustomerInfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CustomerInfo);
                ////登船号
                //this.lbLoadCode.Text = model.PlanShip.LoadCode;
                //签发日期
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
