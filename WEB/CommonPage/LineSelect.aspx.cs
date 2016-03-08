using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.CommonPage
{
    public partial class LineSelect : EyouSoft.Common.Page.BackPage
    {
        #region 页面参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 4;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        protected void DataInit()
        {
            string lineName = Utils.GetQueryStringValue("txtlineName");
            string tourNo = Utils.GetQueryStringValue("txttourNo");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            EyouSoft.BLL.TourStructure.BTour BLL = new EyouSoft.BLL.TourStructure.BTour();
            IList<EyouSoft.Model.TourStructure.MTourBaseInfo> list = BLL.GetSendWCTTour(this.SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, lineName, tourNo);
            if (list != null && list.Count > 0)
            {
                this.rpt_list.DataSource = list;
                this.rpt_list.DataBind();
                BindPage();
            }
            else
            {
                this.rpt_list.Controls.Add(new Label() { Text = "<tr><td colspan='5' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
    }
}
