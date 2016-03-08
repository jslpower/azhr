using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Gys
{
    public partial class JiaoYiMingXi : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 单位名称
        /// </summary>
        protected string GysName = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        protected string GysId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("gysid");
            if (string.IsNullOrEmpty(GysId)) RCWE("异常请求");

            if (UtilsCommons.IsToXls()) ToXls();

            InitInfo();

            InitRpt();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.HGysStructure.BGys().GetInfo(GysId);
            if (info == null) RCWE("异常请求");

            GysName = info.GysName;
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJiaoYiMingXiChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.HGysStructure.MJiaoYiMingXiChaXunInfo();

            info.LEDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLEDate"));
            info.LSDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLSDate"));

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            object[] heJi;
            var items = new EyouSoft.BLL.HGysStructure.BGys().GetJiaoYiMingXis(SiteUserInfo.CompanyId, GysId, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();

                ltrShuLiangHeJi.Text = heJi[0].ToString() ;
                ltrJinEHeJi.Text = UtilsCommons.GetMoneyString(heJi[1], ProviderToMoney);
                ltrWeiZhiFuJinEHeJi.Text = UtilsCommons.GetMoneyString((decimal)heJi[1] - (decimal)heJi[2], ProviderToMoney);

                phEmpty.Visible = false;
                phHeJi.Visible = true;

                paging.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;
            }
            else
            {
                paging.Visible = false;
                phEmpty.Visible = true;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            var chaXun = GetChaXunInfo();
            object[] heJi;
            int _recordCount = 0;
            var items = new EyouSoft.BLL.HGysStructure.BGys().GetJiaoYiMingXis(SiteUserInfo.CompanyId, GysId, toXlsRecordCount, 1, ref _recordCount, chaXun, out heJi);
            if (items == null || items.Count == 0) ResponseToXls(string.Empty);

            var s = new StringBuilder();
            s.Append("团号\t线路名称\t销售员\t计调\t导游\t数量\t结算金额\t未付金额\n");

            foreach (var item in items)
            {
                s.Append(item.TourCode + "\t");
                s.Append(item.RouteName + "\t");
                s.Append(item.XiaoShouYuanName + "\t");
                s.Append(item.JiDiaoYuanName + "\t");
                s.Append(item.DaoYouname + "\t");
                s.Append(item.ShuLiang + "\t");               
                s.Append(item.JinE.ToString("F2") + "\t");
                s.Append(item.WeiZhiFuJinE.ToString("F2") + "\n");
            }

            //合计
            if (heJi.Any())
            {
                s.AppendFormat("\t\t\t\t合计：\t{0}\t{1}\t{2}\n", heJi[0], heJi[1], (decimal)heJi[1] - (decimal)heJi[2]);
            }

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        #endregion
    }
}
