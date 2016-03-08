using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common.Page;
using System.Text;

namespace EyouSoft.Web.PrintPage
{
    using EyouSoft.Model.EnumType.PlanStructure;

    public partial class YongCanPrint : BackPage
    {
        protected int listCount = 0;
        protected PlanDiningPriceType PriceType;
        protected MUserInfo SiteUserInfo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string planId = Utils.GetQueryStringValue("planId");
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!string.IsNullOrEmpty(planId) && SiteUserInfo != null)
            {
                InitPage(planId);
            }
            this.Title = PrintTemplateType.订餐确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.HPlanStructure.BPlan BLL = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, planId);
            if (model != null)
            {
                //地接社名称/联系人
                this.txtCompanyName.Text = model.SourceName;
                this.txtCompanyContactName.Text = model.ContactName;
                this.txtContact.Text = model.ContactPhone;
                this.txtFax.Text = model.ContactFax;
                //公司名、联系人
                this.txtSelfName.Text = "JJ Travel Service";//this.SiteUserInfo.CompanyName;
                this.txtSelfContactName.Text = this.SiteUserInfo.Name;
                this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
                this.txtSelfFax.Text = this.SiteUserInfo.Fax;
                //线路名称
                //this.lbRouteName.Text = model.RouteName;
                //团号
                this.lbTourCode.Text = model.TourCode;
                //this.lblGuoJi.Text = model.GuoJi;
                //用餐时间
                this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                //付款方式
                switch (model.PaymentType)
                {
                    case Payment.财务对冲:
                        this.lbPaymentType.Text = "Bank Tranfer";
                        break;
                    case Payment.现付:
                        this.lbPaymentType.Text = "Cash Pay";
                        break;
                    case Payment.信用卡支付:
                        this.lbPaymentType.Text = "Credit Card";
                        break;
                    default:
                        this.lbPaymentType.Text = Utils.GetEnumText(typeof(Payment), model.PaymentType);
                        break;
                }
                //费用明细
                if (model.PlanDiningList != null && model.PlanDiningList.Count>0)
                {
                    listCount = model.PlanDiningList.Count;
                    PriceType = model.PlanDiningList[0].PriceType;
                    this.rptlist.DataSource = model.PlanDiningList;
                    this.rptlist.DataBind();
                }
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //签发日期
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //导游信息
                var g = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(PlanProject.导游, null, null, false, null, model.TourId);
                if (g != null && g.Count > 0)
                {
                    //导游姓名
                    this.lblDaoYou.Text = g[0].SourceName;
                    //导游手机
                    this.lblMobile.Text = g[0].ContactPhone;
                }
                var s = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(model.SourceId);
                if (s!=null)
                {
                    this.lblMian.Text = s.IsMianYi ? "Yes" : "No";
                }
            }
        }

        protected string GetMenu(object menuId)
        {
            var m = new EyouSoft.BLL.HGysStructure.BJiaGe().GetCanGuanCaiDanInfo(menuId.ToString());
            return m != null && !string.IsNullOrEmpty(m.NeiRong) ? ":"+m.NeiRong : string.Empty;
        }
    }
}
