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
using System.Text;
using EyouSoft.Common;
using System.Collections.Generic;

namespace EyouSoft.Web.Sta
{
    public partial class DaoYouYeJi : EyouSoft.Common.Page.BackPage
    {
        protected string QSQDBL = string.Empty;

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
            EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo heJi;
            var items = new EyouSoft.BLL.TongJiStructure.BDaoYouYeJi().GetDaoYouYeJis(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun, out heJi);
            QSQDBL = heJi.QSQDBL;
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;

                ltrRJCRHJ.Text = heJi.RJCR.ToString();
                ltrRJETHJ.Text = heJi.RJET.ToString();
                ltrRJLDHJ.Text = heJi.RJLD.ToString();
                ltrGWCRHJ.Text = heJi.GWCR.ToString();
                ltrGWETHJ.Text = heJi.GWET.ToString();
            }
            else
            {
                phEmpty.Visible = true;
                paging.Visible = false;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.TongJiStructure.MDaoYouYeJiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiChaXunInfo();

            info.ETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));            
            info.STime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));
            info.DaoYouId = Utils.GetQueryStringValue(txtDaoYou.SellsIDClient);
            info.DaoYouName = Utils.GetQueryStringValue(txtDaoYou.SellsNameClient);
            info.GysIds = Utils.GetQueryStringValue("txtGWDID").Split(',');

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var chaXun = GetChaXunInfo();
            EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo heJi;
            var items = new EyouSoft.BLL.TongJiStructure.BDaoYouYeJi().GetDaoYouYeJis(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun, out heJi);

            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var _QSQDBL = heJi.QSQDBL;
            StringBuilder s = new StringBuilder();
            s.Append("导游\t团号\t入境成人\t入境儿童\t入境领队\t结算成人\t结算儿童\t签单比例\t全社比例\n");

            foreach (var item in items)
            {
                s.Append(item.DaoYouName + "\t");
                s.Append(item.TourCode + "\t");
                s.Append(item.RJCR + "\t");
                s.Append(item.RJET + "\t");
                s.Append(item.RJLD + "\t");
                s.Append(item.GWCR + "\t");
                s.Append(item.GWET + "\t");
                s.Append(item.QDBL + "\t");
                s.Append(_QSQDBL + "\n");
            }
           

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取购物明细浮动HTML
        /// </summary>
        /// <param name="rjrs">入境人数</param>
        /// <param name="xxs">购物明细</param>
        /// <returns></returns>
        protected string GetGWXXHtml(object rjrs, object xxs)
        {
            if (rjrs == null || xxs == null) return string.Empty;

            int _rjrs = (int)rjrs;
            IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiGWInfo> items = (IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiGWInfo>)xxs;
            if (items == null || items.Count == 0) return string.Empty;

            StringBuilder s = new StringBuilder();
            s.Append("<table cellspacing='0' cellpadding='0' border='1' bordercolor='#EAB25A' width='100%' class='pp-tableclass2'><tr class='pp-table-title'><th height='23' align='center'>编号</th><th align='center'>购物店</th><th align='center'>人数</th><th align='center'>金额</th><th align='center'>签单比例</th></tr>");
            if (items != null && items.Count > 0)
            {
                int i = 1;
                foreach (var item in items)
                {
                    decimal qdbl = 0;
                    if (_rjrs != 0)
                    {
                        qdbl = (decimal)item.GWRS / (decimal)_rjrs;
                    }
                    qdbl = qdbl * 100;

                    s.Append("<tr ><td align='center'>" + i + "</td><td align='center'>" + item.GysName + "</td><td align='center' >" + item.GWRS + "</td><td align='center'>" + item.YingYeE.ToString("F2") + "</td><td align='center'>" + qdbl.ToString("F2") + "%</td></tr>");
                    i++;
                }
            }
            s.Append("</table>");

            return s.ToString();
        }
        #endregion
    }
}
