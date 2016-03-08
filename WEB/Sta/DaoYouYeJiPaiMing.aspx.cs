using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Sta
{
    public partial class DaoYouYeJiPaiMing : EyouSoft.Common.Page.BackPage
    {
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_导游业绩统计_栏目))
            {
                RCWE("没有权限");
            }
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            var chaXun = GetChaXunInfo();
            int pageSize = 20;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;
            var items = new EyouSoft.BLL.TongJiStructure.BDaoYouYeJi().GetDaoYouYeJiPaiMings(CurrentUserCompanyID, chaXun);
            var items1 = Utils.FenYe<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo>(pageSize, pageIndex, out recordCount, items);

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
        EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingChaXunInfo();

            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.PaiXu = Utils.GetInt(Utils.GetQueryStringValue("txtPaiXu"));

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.TongJiStructure.BDaoYouYeJi().GetDaoYouYeJiPaiMings(CurrentUserCompanyID, chaXun);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            s.Append("序号\t导游\t人数\n");
            int i = 1;
            foreach (var item in items)
            {
                s.AppendFormat(i + "\t");
                s.AppendFormat(item.DaoYouName + "\t");
                s.AppendFormat(item.RS + "\n");
                i++;
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
