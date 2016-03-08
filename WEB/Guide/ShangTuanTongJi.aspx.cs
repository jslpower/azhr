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
using EyouSoft.Common;
using System.Text;
using System.Collections.Generic;

namespace EyouSoft.Web.Guide
{
    public partial class ShangTuanTongJi : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected int pageSize = 20;
        protected int pageIndex = 1;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (UtilsCommons.IsToXls()) ToXls();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            bool Privs_LanMu = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_上团统计_栏目);

            if (!Privs_LanMu) RCWE("无权限");
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            pageSize = 20;
            pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;

            var items = new EyouSoft.BLL.ComStructure.BDaoYou().GetDaoYouShangTuanTongJi(CurrentUserCompanyID, chaXun);

            var items1 = Utils.FenYe<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo>(pageSize, pageIndex, out recordCount, items);

            if (items1 != null && items1.Count > 0)
            {
                rpt.DataSource = items1;
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
        EyouSoft.Model.ComStructure.MDaoYouShangTuanChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.ComStructure.MDaoYouShangTuanChaXunInfo();

            info.DaoYouId = Utils.GetQueryStringValue("txtDaoYouId");
            info.DaoYouName = Utils.GetQueryStringValue("txtDaoYouName");
            info.EDTTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEDTTime"));
            info.PaiXu = Utils.GetInt(Utils.GetQueryStringValue("txtPaiXu"));
            info.SDTTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSDTTime"));
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.RouteName = Utils.GetQueryStringValue("txtRouteName");

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            //int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.ComStructure.BDaoYou().GetDaoYouShangTuanTongJi(CurrentUserCompanyID, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.AppendFormat("导游姓名\t团队数\t团天数\n");

            foreach (var item in items)
            {
                s.AppendFormat(item.DaoYouName + "\t");
                s.AppendFormat(item.TuanDuiShu + "\t");
                s.AppendFormat(item.TuanTianShu + "\n");
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
