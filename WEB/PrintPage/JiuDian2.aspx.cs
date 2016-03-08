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
    using EyouSoft.Common;
    using EyouSoft.Model.EnumType.PlanStructure;

    public partial class JiuDian2 : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var planid = EyouSoft.Common.Utils.GetQueryStringValue("planId");
            this.Title = "酒店委托单";
            if (!IsPostBack)
            {
                PageInit(planid);
            }
        }

        private void PageInit(string planId)
        {
            var b = new EyouSoft.BLL.HPlanStructure.BPlan();
            var model = b.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, planId);

            if (model != null)
            {
                if (model.StartDate.HasValue)
                {
                    this.txtRuZhuShiJian.Text = model.StartDate.Value.ToShortDateString();
                }
                if (model.EndDate.HasValue)
                {
                    this.txtLiDianShiJian.Text = model.EndDate.Value.ToShortDateString();
                }
                //else
                //{
                //    this.txtRuZhuShiJian.Text = DateTime.Now.ToShortDateString();
                //}
                //酒店名称/联系人
                this.txtCompanyName.Text = model.SourceName;
                this.txtCompanyContactName.Text = model.ContactName;
                this.txtContact.Text = model.ContactPhone;
                this.txtFax.Text = model.ContactFax;
                this.txtCompanyMob.Text = model.ContactMobile;
                //公司名、联系人
                this.txtSelfName.Text = this.SiteUserInfo.CompanyName;
                this.txtSelfContactName.Text = this.SiteUserInfo.Name;
                this.txtSelfContact.Text = this.SiteUserInfo.Telephone;
                this.txtSelfFax.Text = this.SiteUserInfo.Fax;
                this.txtSelfMob.Text = this.SiteUserInfo.Mobile;
                //团号
                this.txtTuanHao.Text = model.TourCode;
                //国籍
                var m = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(model.TourId);
                if (m!=null&&m.CountryId>0)
                {
                    var mc = new EyouSoft.BLL.SysStructure.BGeography().GetCountry(SiteUserInfo.CompanyId, m.CountryId);
                    this.txtGuoJi.Text = mc.Name;
                    this.txtAdult.Text = m.Adults.ToString();
                    this.txtPei.Text = m.SiPei.ToString();
                }
                //房型
                if (model.PlanHotel.PlanHotelRoomList != null && model.PlanHotel.PlanHotelRoomList.Count > 0)
                {
                    this.rpt_PlanHotelRoomList.DataSource = model.PlanHotel.PlanHotelRoomList;
                    this.rpt_PlanHotelRoomList.DataBind();
                }
                //if (model.PlanHotel!=null)
                //{
                //    //免房数量
                //    this.txtMianFangShuLiang.Text = model.PlanHotel.FreeNumber.ToString();
                //    //免房金额
                //    this.txtMianFangJinE.Text = model.PlanHotel.FreePrice.ToString("C2");
                //    //是否含早
                //    this.txtShiFouHanZao.Text = Utils.GetEnumText(typeof(PlanHotelIsMeal), model.PlanHotel.IsMeal);
                //}
                //备注
                this.txtBeiZhu.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Remarks);
                //费用总额
                this.txtJieSuanJinE.Text = UtilsCommons.GetMoneyString(model.Confirmation, ProviderToMoney);
                //付款方式
                this.txtFuKuanFangShi.Text = Utils.GetEnumText(typeof(Payment), model.PaymentType);
                //导游信息
                var g = new EyouSoft.BLL.HPlanStructure.BPlan().GetList(PlanProject.导游,null,null,false,null,model.TourId);
                if (g!=null&&g.Count>0)
                {
                    //导游姓名
                    this.txtGuideName.Text = g[0].SourceName;
                    //导游手机
                    this.txtGuideMobile.Text = g[0].ContactPhone;
                }
            }
        }
    }
}
