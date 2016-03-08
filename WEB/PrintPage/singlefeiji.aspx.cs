using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Model.PlanStructure;

namespace EyouSoft.Web.PrintPage
{
    public partial class singlefeiji : BackPage
    {
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string planid = EyouSoft.Common.Utils.GetQueryStringValue("planId");
            this.Title = "机票确认单";
            if (!IsPostBack)
            {
                PageInit(planid);
            }
        }
        private void PageInit(string planid)
        {
            EyouSoft.BLL.PlanStructure.BPlan bll = new EyouSoft.BLL.PlanStructure.BPlan();
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
                //this.lbCostDesc.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.CostDetail);
                this.lbTotleCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mPlan.Confirmation, ProviderToMoney);
                this.lbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.Remarks);
                lblNum.Text = mPlan.Num.ToString();
                ltrZhiFuFangShi.Text = mPlan.PaymentType.ToString();
                //if (mPlan.PlanLargeTime != null && mPlan.PlanLargeTime.Count > 0)
                //{
                //    listCount = mPlan.PlanLargeTime.Count;
                //    this.rptlist.DataSource = mPlan.PlanLargeTime;
                //    this.rptlist.DataBind();
                //}
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
