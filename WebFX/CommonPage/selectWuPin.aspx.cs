using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.WebFX.CommonPage
{
    public partial class selectWuPin : EyouSoft.Common.Page.FrontPage
    {
        #region 分页参数
        /// <summary>
        /// 每页数量
        /// </summary>
        protected int pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 一共多少页
        /// </summary>
        int recordCount = 0;
        protected int listCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageInit();
            }
        }
        private void pageInit()
        {
            EyouSoft.BLL.HGysStructure.BWuPin bll = new EyouSoft.BLL.HGysStructure.BWuPin();
            EyouSoft.Model.HGysStructure.MWuPinChaXunInfo searchmodel = new EyouSoft.Model.HGysStructure.MWuPinChaXunInfo();
            IList<EyouSoft.Model.HGysStructure.MWuPinInfo> list = bll.GetWuPins(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchmodel);
            if (list != null && list.Count > 0)
            {
                rpt_WuPinList.DataSource = list;
                rpt_WuPinList.DataBind();
                BindPage();
            }
            else
            {
                litMsg.Text = "<tr class='old'><td colspan='4' align='center'>" + (String)GetGlobalResourceObject("string", "暂无数据") + "</td></tr>";
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
    }
}
