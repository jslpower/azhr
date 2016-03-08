using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.Guide
{
    public partial class DaoYouDaiTuanXX : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string DaoYouId = string.Empty;
        protected int pageSize = 20;
        protected int pageIndex = 1;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            DaoYouId = Utils.GetQueryStringValue("daoyouid");
            if (string.IsNullOrEmpty(DaoYouId)) RCWE("异常请求");

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            pageSize = 20;
            pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var items = new EyouSoft.BLL.ComStructure.BDaoYou().GetDaoYouDaiTuanXXs(DaoYouId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXChaXunInfo();

            info.EDTTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEDTTime"));
            info.ELDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtELDate"));
            info.SDTTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSDTTime"));
            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSLDate"));
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.RouteName = Utils.GetQueryStringValue("txtRouteName");

            return info;
        }
        #endregion
    }
}
