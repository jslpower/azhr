using System;

namespace EyouSoft.Web.Guide
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    public partial class DaoYouBianGeng : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int recordCount = 100;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
        }

         void PageInit()
         {
             var ls = new EyouSoft.BLL.TourStructure.BTour().GetTourPlanChange(pageSize, pageIndex, ref recordCount, GetQuery());
             if (ls != null && ls.Count > 0)
             {
                 pan_Msg.Visible = false;
                 rpt_list.DataSource = ls;
                 rpt_list.DataBind();
                 BindPage();
             }
             ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && this.recordCount > this.pageSize;
         }

         /// <summary>
         /// 绑定分页
         /// </summary>
         private void BindPage()
         {
             ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
             ExporPageInfoSelect1.UrlParams = Request.QueryString;
             ExporPageInfoSelect1.intPageSize = this.pageSize;
             ExporPageInfoSelect1.CurrencyPage = this.pageIndex;
             ExporPageInfoSelect1.intRecordCount = this.recordCount;
         }

        MTourPlanChangeBase GetQuery()
        {
            return new MTourPlanChangeBase()
                {
                    CompanyId = CurrentUserCompanyID,
                    TourId = Utils.GetQueryStringValue("tourid"),
                    ChangeType = ChangeType.导游变更,
                    SL = (Menu2)Utils.GetInt(SL)
                };
        }
    }
}
