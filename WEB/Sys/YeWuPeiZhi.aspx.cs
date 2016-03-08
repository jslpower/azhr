using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Sys
{
    public partial class YeWuPeiZhi : BackPage
    {

        EyouSoft.Model.ComStructure.MComSetting model = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        protected void PageInit()
        {
            model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (model != null)
            {
                showMonthBegin.Value = model.ShowBeforeMonth.ToString();
                showMonthEnd.Value = model.ShowAfterMonth.ToString();
                txtLeaveHours.Value = (model.SaveTime / 60).ToString();
                txtLeaveMinutes.Value = (model.SaveTime % 60).ToString();
                

                txtAdvanceDayforCountry.Value = model.CountryArea.ToString();
                txtAdvanceDayforProvince.Value = model.ProvinceArea.ToString();
                txtAdvanceDayforForeign.Value = model.ExitArea.ToString();

                //chkPayOutCheck.Checked = model.FinancialExpensesReview;
                //chkIncomeCheck.Checked = model.FinancialIncomeReview;
                //txtCreditsPer.Value = model.IntegralProportion.ToString();
                chkGuidBZ.Checked = model.SkipGuide;
                chkSaleBZ.Checked = model.SkipSale;
                chkEndBZ.Checked = model.SkipFinalJudgment;
            }
        }

        protected void Save()
        {
            IList<EyouSoft.Model.ComStructure.MTourNoOptionCode> TourNoOptionCodeList = new List<EyouSoft.Model.ComStructure.MTourNoOptionCode>();

            EyouSoft.Model.ComStructure.MComSetting model = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(CurrentUserCompanyID);
            model = model ?? new EyouSoft.Model.ComStructure.MComSetting();

            model.ShowBeforeMonth = Utils.GetInt(showMonthBegin.Value);
            model.ShowAfterMonth = Utils.GetInt(showMonthEnd.Value);
            model.SaveTime = Utils.GetInt(txtLeaveHours.Value) * 60 + Utils.GetInt(txtLeaveMinutes.Value);
            //model.RemindTime = Utils.GetInt(txtTipHours.Value) * 3600 + Utils.GetInt(txtTipMinutes.Value) * 60 + Utils.GetInt(txtTipSecond.Value);
            //if (chkCountry.Checked)
            //{
            //    model.CountryArea = Utils.GetInt(txtAdvanceDayforCountry.Value);
            //}
            //if (chkProvince.Checked)
            //{
            //    model.ProvinceArea = Utils.GetInt(txtAdvanceDayforProvince.Value);
            //}
            //if (chkForeign.Checked)
            //{
            //    model.ExitArea = Utils.GetInt(txtAdvanceDayforForeign.Value);
            //}
            //model.ArrearsRangeControl = chkQianKuaiControl.Checked;

            model.CountryArea = Utils.GetIntSign(txtAdvanceDayforCountry.Value);
            model.ProvinceArea = Utils.GetIntSign(txtAdvanceDayforProvince.Value);
            model.ExitArea = Utils.GetIntSign(txtAdvanceDayforForeign.Value);

            //model.FinancialExpensesReview = chkPayOutCheck.Checked;
            //model.FinancialIncomeReview = chkIncomeCheck.Checked;
            //model.IntegralProportion = Utils.GetInt(txtCreditsPer.Value);
            model.SkipGuide = chkGuidBZ.Checked;
            model.SkipSale = chkSaleBZ.Checked;
            model.SkipFinalJudgment = chkEndBZ.Checked;
            model.CompanyId = SiteUserInfo.CompanyId;
            Response.Clear();
            if (new EyouSoft.BLL.ComStructure.BComSetting().UpdateComSetting(model))
            {

                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));

            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "修改失败"));
            }
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_系统设置_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
