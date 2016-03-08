using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Plan
{
    /// <summary>
    /// 计调配置页面
    /// </summary>
    public partial class PlanConfigPage : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            if (!IsPostBack)
            {
                this.PlanConfigMenu1.IndexClass = "1";
                this.PlanConfigMenu1.CompanyId = SiteUserInfo.CompanyId;

                string tourId = Utils.GetQueryStringValue("tourId");
                if (!string.IsNullOrEmpty(tourId))
                {
                    DataInitTourInfo(tourId);
                }
            }
        }

        #region 初始化团队信息
        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="tourID"></param>
        protected void DataInitTourInfo(string tourID)
        {
            EyouSoft.Model.HTourStructure.MTour tourInfo = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourID);
            if (tourInfo != null)
            {
                this.litTourCode.Text = tourInfo.TourCode;
                EyouSoft.Model.ComStructure.MComArea AreaModel = new EyouSoft.BLL.ComStructure.BComArea().GetModel(tourInfo.AreaId, SiteUserInfo.CompanyId);
                if (AreaModel != null)
                {
                    this.litAreaName.Text = AreaModel.AreaName;
                }
                AreaModel = null;
                this.litRouteName.Text = tourInfo.RouteName;
                this.litDays.Text = tourInfo.TourDays.ToString();
                //人数
                this.litPeoples.Text = "成人" + tourInfo.Adults.ToString() + "，儿童" + tourInfo.Childs.ToString() + "，领队" + tourInfo.Leaders.ToString() + "，司陪" + tourInfo.SiPei.ToString();
                //用房数
                if (tourInfo.TourRoomList != null && tourInfo.TourRoomList.Count > 0)
                {
                    int num = 0;
                    for (int i = 0; i < tourInfo.TourRoomList.Count; i++)
                    {
                        num += tourInfo.TourRoomList[i].Num;
                    }
                    this.litHouses.Text = num.ToString();
                }
                else
                {
                    this.litHouses.Text = "0";
                }
                EyouSoft.Model.SysStructure.MGeography CountryModel = new EyouSoft.BLL.SysStructure.BGeography().GetCountry(SiteUserInfo.CompanyId, tourInfo.CountryId);
                if (CountryModel != null)
                {
                    this.litCountry.Text = CountryModel.Name;
                }
                CountryModel = null;

                this.litDDDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                this.litDDCity.Text = tourInfo.ArriveCity;
                this.litDDHBDate.Text = tourInfo.ArriveCityFlight; 
                this.litLJDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.RDate, ProviderToDate);
                this.litLKCity.Text = tourInfo.LeaveCity;
                this.litLKHBDate.Text = tourInfo.LeaveCityFlight; 

                this.litSellers.Text = tourInfo.SellerName;
                //计调员
                if (tourInfo.TourPlanerList != null && tourInfo.TourPlanerList.Count > 0)
                {
                    string planerList = "";
                    foreach (var item in tourInfo.TourPlanerList)
                    {
                        planerList += item.Planer + ",";
                    }
                    this.litOperaters.Text = planerList.Trim(',');
                }
                this.LitInterInfo.Text = string.IsNullOrEmpty(tourInfo.InsideInformation) ? "无内部信息" : tourInfo.InsideInformation;
                this.hidTourMode.Value = ((int)tourInfo.TourMode).ToString();
            }
            tourInfo = null;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            this.holerView5.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_地接浏览);
            this.holerView4.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_导游浏览);
            this.holerView1.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_酒店浏览);
            this.holerView2.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_用车浏览);
            this.holerView9.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_区间交通浏览);
            this.holerView3.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_景点浏览);
            this.holerView6.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_用餐浏览);
            this.holerView7.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_购物浏览);
            this.holerView8.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_领料浏览);
            this.holerView13.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_计调列表_其他浏览);
        }
        #endregion
    }
}
