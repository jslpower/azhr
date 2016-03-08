using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.CommonPage
{
    public partial class SelectRoute : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private int pageSize = 40;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            AreaInit();

            PageInit();
        }

        /// <summary>
        /// 初始化线路列表
        /// </summary>
        private void PageInit()
        {
            var bll = new EyouSoft.BLL.HTourStructure.BTour();
            int itemType = Utils.GetInt(Utils.GetQueryStringValue("type"));////整团还是分项(0:整团;1:分项)
            var searchModel = new MTourSearch
            {
                CompanyId = SiteUserInfo.CompanyId,
                RouteName = Utils.GetQueryStringValue("txtRouteName"),
                AreaId = Utils.GetIntNull(Utils.GetQueryStringValue("areaId")),
                TourType = TourType.线路产品
            };
            var list = bll.GetTourInfoList(pageSize, pageIndex, ref recordCount, searchModel).Where(m => m.OutQuoteType == (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)itemType).ToList();
            if (list != null && list.Count > 0)
            {
                this.rpt_List.DataSource = list;
                this.rpt_List.DataBind();
                BindPage();
            }
        }

        /// <summary>
        /// 线路区域初始化
        /// </summary>
        private void AreaInit()
        {
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(this.SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptAreaList.DataSource = list;
                this.rptAreaList.DataBind();
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

    }
}
