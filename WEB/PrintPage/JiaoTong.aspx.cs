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

namespace EyouSoft.Web.PrintPage
{
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PlanStructure;

    public partial class JiaoTong : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string planid = EyouSoft.Common.Utils.GetQueryStringValue("planId");
            this.Title = "交通确认单";
            if (!IsPostBack)
            {
                PageInit(planid);
            }
        }
        private void PageInit(string planid)
        {
            var t = (PlanProject)Common.Utils.GetInt(Common.Utils.GetQueryStringValue("t"));
            var bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            var mPlan = bll.GetModel(t, planid);
            this.txtsourcename.Text = SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;
            this.ltlJiao.Text = t.ToString();
            //计调实体
            if (mPlan != null)
            {
                if (mPlan.PlanLargeFrequencyList != null)
                {
                    this.ltlPrice.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + (m.FarePrice.ToString("C2") + ",")).Trim(',');
                    this.ltlShou.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + (m.InsuranceHandlFee.ToString("C2") + ",")).Trim(',');
                    this.ltlChuDate.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + ((m.DepartureTime.HasValue?m.DepartureTime.Value.ToShortDateString():"") + ",")).Trim(',');
                    this.ltlDiCheng.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + (m.Destination + ",")).Trim(',');
                    this.ltlChu.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + (m.Departure + ",")).Trim(',');
                    this.ltlXi.Text = mPlan.PlanLargeFrequencyList.Aggregate(string.Empty, (current, m) => current + ((string.IsNullOrEmpty(m.SeatStandard)?m.SeatType.ToString():m.SeatStandard) + ",")).Trim(',');
                }

                this.LbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.Remarks);
                this.txtunitContactname.Text = mPlan.ContactName;
                this.txtunitname.Text = mPlan.SourceName;
                this.txtunittel.Text = mPlan.ContactPhone;
                this.txtunitfax.Text = mPlan.ContactFax;
                this.lbTourID.Text = mPlan.TourCode;
                this.lblGuoJi.Text = mPlan.GuoJi;
                this.txtPeople.Text = (mPlan.Adults + mPlan.Childs + mPlan.Leaders + mPlan.SiPei).ToString();
                this.lbPaymentType.Text = (int)mPlan.PaymentType == 0 ? "" : mPlan.PaymentType.ToString();
                //导游信息
                var g = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(PlanProject.导游, null, null, false, null, mPlan.TourId);
                if (g != null && g.Count > 0)
                {
                    //导游姓名
                    this.lblDaoYou.Text = g[0].SourceName;
                    //导游手机
                    this.lblMobile.Text = g[0].ContactPhone;
                }
            }

        }
    }
}
