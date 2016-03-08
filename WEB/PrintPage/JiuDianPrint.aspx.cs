using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Common;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.PrintPage
{
    public partial class JiuDianPrint : BackPage
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
            this.Title = PrintTemplateType.酒店确认单.ToString();
        }

        protected void InitPage(string planId)
        {
            EyouSoft.BLL.HPlanStructure.BPlan BLL = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo model = BLL.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, planId);

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
                //入住时间
                this.lbStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
                //离店时间
                this.lbEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "";
                //天数
                this.lbDays.Text = model.PlanHotel.Days.ToString();
                //房型
                if (model.PlanHotel.PlanHotelRoomList != null && model.PlanHotel.PlanHotelRoomList.Count > 0)
                {
                    this.rpt_PlanHotelRoomList.DataSource = model.PlanHotel.PlanHotelRoomList;
                    this.rpt_PlanHotelRoomList.DataBind();
                }
                //付费房数
                this.lbNum.Text = model.Num.ToString();
                //免费房数
                this.lbFreeNumber.Text = model.PlanHotel.FreeNumber.ToString();
                //是否含早
                this.lbIsMeal.Text = model.PlanHotel.IsMeal.ToString();
                ////早餐费用
                //this.lbMealCost.Text = UtilsCommons.GetMoneyString((decimal)model.PlanHotel.MealFrequency *
                //    (decimal)model.PlanHotel.MealNumber * model.PlanHotel.MealPrice, ProviderToMoney);
                //费用明细
                this.lbCostDetail.Text = EyouSoft.Common.UtilsCommons.GetAPMX(model.PlanHotelRoomList, EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店.ToString());
                //费用总额
                this.lbConfirmation.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //备注
                //this.lbCostRemarks.Text = model.ContactName + "/" + model.ContactPhone + "<br/>" + model.Remarks;
                this.lbCostRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                this.lbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ltrZhiFuFangShi.Text = model.PaymentType.ToString();
            }
        }
    }
}
