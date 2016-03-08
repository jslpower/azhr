using System;

namespace EyouSoft.Web.CommonPage
{
    using System.Collections.Generic;
    using System.Text;

    using EyouSoft.BLL.HTourStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.HTourStructure;
    using EyouSoft.Model.TourStructure;

    public partial class TuanHaoXuanYong : BackPage
    {
        protected int PageSize = 20;
        protected int PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int RecordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("doType") == "GetTourCode") GetTourCode();
            else PageInit();
        }

        protected void PageInit()
        {
            var q = new MTourSearch();
            q.CompanyId = CurrentUserCompanyID;
            q.TourCode = Utils.GetQueryStringValue("th");
            q.BeginLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd"));
            q.EndLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed"));
            q.TourType = TourType.团队产品;
            var ls = new BTour().GetTourInfoList(
    this.PageSize,
    this.PageIndex,
    ref  this.RecordCount,
    q);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt.DataSource = ls;
                rpt.DataBind();
                BindPage();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && this.RecordCount > this.PageSize;

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = this.PageSize;
            ExporPageInfoSelect1.CurrencyPage = this.PageIndex;
            ExporPageInfoSelect1.intRecordCount = this.RecordCount;
        }

        #region private members
        void GetTourCode()
        {
            var queryString = new MTourSearch();
            queryString.CompanyId = CurrentUserCompanyID;
            queryString.TourCode = Utils.GetQueryStringValue("q");
            var sb = new StringBuilder();
            var ls = new BTour().GetTourInfoList(35, 1,ref RecordCount,queryString);
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    sb.Append(item.TourCode + "|" + item.TourId + "\n");
                }
            }
            else
            {
                sb.Append("无匹配项|-1");
            }
            AjaxResponse(sb.ToString());
        }
        #endregion
    }
}
