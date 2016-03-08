﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.HPlanStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.PrintPage
{
    public partial class JingDianPrint : BackPage
    {

        protected int listCount = 0;
        protected string JDname = "", seat = "", stardate = "";
        protected int LYrenshu = 0, LYetshu = 0;
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
            EyouSoft.BLL.HPlanStructure.BPlan bll = new EyouSoft.BLL.HPlanStructure.BPlan();
            MPlanBaseInfo mPlan = bll.GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, planid);
            this.txtsourcename.Text = "JJ Travel Service";//SiteUserInfo.CompanyName;
            this.txtname.Text = SiteUserInfo.Name;
            this.txttel.Text = SiteUserInfo.Telephone;
            this.txtfax.Text = SiteUserInfo.Fax;
            //计调实体
            if (mPlan != null)
            {
                if (mPlan.PlanAttractionsList != null)
                {
                    listCount = mPlan.PlanAttractionsList.Count;
                    this.rptlist.DataSource = mPlan.PlanAttractionsList;
                    this.rptlist.DataBind();
                }

                //string s = EyouSoft.Common.UtilsCommons.GetDateString(mPlan.StartDate, ProviderToDate);
                //if (!string.IsNullOrEmpty(mPlan.StartTime)) s += "&nbsp;" + mPlan.StartTime + "时";
                //s += " 至 " + EyouSoft.Common.UtilsCommons.GetDateString(mPlan.EndDate, ProviderToDate);
                //if (!string.IsNullOrEmpty(mPlan.EndTime)) s += "&nbsp;" + mPlan.EndTime + "时";
                //this.lbTime.Text = s;

                this.lbTotleCost.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(mPlan.Confirmation, ProviderToMoney);
                this.LbRemark.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(mPlan.Remarks);
                this.txtunitContactname.Text = mPlan.ContactName;
                this.txtunitname.Text = mPlan.SourceName;
                this.txtunittel.Text = mPlan.ContactPhone;
                this.txtunitfax.Text = mPlan.ContactFax;
                this.lblLinkNum.Text = mPlan.ContactPhone;
                //this.lbRouteName.Text = mPlan.RouteName;
                this.lbTourID.Text = mPlan.TourCode;


            }

        }
        /// <summary>
        /// 获取景点名称和人数
        /// </summary>
        /// <param name="list"></param>
        protected void GetTypeNum(IList<MPlanAttractions> list)
        {
            if (list == null || list.Count == 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                LYrenshu += list[i].AdultNumber;
                LYetshu += list[i].ChildNumber;
                seat += list[i].Seats == "" ? "" : list[i].Seats + ",";
                JDname += list[i].Attractions + ",";
                stardate += list[i].VisitTime.HasValue ? Utils.GetDateTime(list[i].VisitTime.ToString()).ToString("yyyy-MM-dd") + "," : "";
            }
            JDname = JDname.TrimEnd(',');
            seat = seat.TrimEnd(',');
            stardate = stardate.TrimEnd(',');
        }

    }
}
