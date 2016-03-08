using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.PlanStructure;

namespace EyouSoft.Web.PrintPage
{
    public partial class singleingdian : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string planid = EyouSoft.Common.Utils.GetQueryStringValue("planId");
            this.Title = "景点确认单";
            if (!IsPostBack)
            {
                PageInit(planid);
            }
        }
        private void PageInit(string planid)
        {
            EyouSoft.BLL.PlanStructure.BPlan bll = new EyouSoft.BLL.PlanStructure.BPlan();
            MPlanBaseInfo mPlan = bll.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, planid);
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;
            //计调实体
            if (mPlan != null)
            {
                if (mPlan.PlanAttractions != null)
                {
                    //this.lbScenicName.Text = mPlan.PlanAttractions.Attractions;
                    this.lbPeopleCount.Text = (mPlan.Num).ToString();
                }

                //string s = EyouSoft.Common.UtilsCommons.GetDateString(mPlan.StartDate, ProviderToDate);
                //if (!string.IsNullOrEmpty(mPlan.StartTime)) s += "&nbsp;" + mPlan.StartTime + "时";
                //s += " 至 " + EyouSoft.Common.UtilsCommons.GetDateString(mPlan.EndDate, ProviderToDate);
                //if (!string.IsNullOrEmpty(mPlan.EndTime)) s += "&nbsp;" + mPlan.EndTime + "时";
                //this.lbTime.Text = s;

                //this.lbCostDesc.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.CostDetail);
                this.lbTotleCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mPlan.Confirmation, ProviderToMoney);
                this.LbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.Remarks);
                this.txtunitContactname.Text = mPlan.ContactName;
                this.txtunitname.Text = mPlan.SourceName;
                this.txtunittel.Text = mPlan.ContactPhone;
                this.txtunitfax.Text = mPlan.ContactFax;
                lblPayType.Text = mPlan.PaymentType.ToString();

                //this.lbRouteName.Text = mPlan.RouteName;
                this.lbTourID.Text = mPlan.TourCode;
            }

        }
    }
}
