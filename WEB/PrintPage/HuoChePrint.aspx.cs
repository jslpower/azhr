using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.PrintPage
{
    public partial class QuJianPrint : BackPage
    {
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string planid = EyouSoft.Common.Utils.GetQueryStringValue("planId");
            this.Title = "火车确认单";
            if (!IsPostBack)
            {
                PageInit(planid);
            }
        }
        private void PageInit(string planid)
        {
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo mPlan = bll.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, planid);
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;
            //计调实体
            if (mPlan != null)
            {
                this.txtunitContactname.Text = mPlan.ContactName;
                this.txtunitname.Text = mPlan.SourceName;
                this.txtunittel.Text = mPlan.ContactPhone;
                this.txtunitfax.Text = mPlan.ContactFax;
                this.lbCostDesc.Text = EyouSoft.Common.UtilsCommons.GetAPMX(mPlan.PlanLargeFrequencyList, EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车.ToString());
                this.lbTotleCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mPlan.Confirmation, ProviderToMoney);
                this.lblPayType.Text = (int)mPlan.PaymentType == 0 ? "" : mPlan.PaymentType.ToString();
                this.lbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.Remarks);
                if (mPlan.PlanLargeFrequencyList != null && mPlan.PlanLargeFrequencyList.Count > 0)
                {
                    listCount = mPlan.PlanLargeFrequencyList.Count;
                    this.rptlist.DataSource = mPlan.PlanLargeFrequencyList;
                    this.rptlist.DataBind();
                }
                //this.lbRouteName.Text = mPlan.RouteName;
                this.lbTourID.Text = mPlan.TourCode;
                //this.txtplanName.Text = mPlan.OperatorName;
                //this.txtcaiwu.Text = mPlan.Accountant;
                //this.txtshenpi.Text = mPlan.Approver;
                //this.txtsellname.Text = mPlan.SellerName;

            }
        }
    }
}
