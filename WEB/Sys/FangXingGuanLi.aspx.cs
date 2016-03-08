using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.SysStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.Sys
{
    public partial class FangXingGuanLi : BackPage
    {
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            string id = Utils.GetQueryStringValue("id").Trim();
            if (!string.IsNullOrEmpty(dotype) && dotype == "del")
            {
                int result = new EyouSoft.BLL.SysStructure.BSysRoom().DeleteRoom(id, SiteUserInfo.CompanyId);
                if (result == 1) AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功"));
                if (result == 0) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "删除失败"));
                if (result == 2) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "房型已被试用"));

            }
            initList();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void initList()
        {
            pageIndex = EyouSoft.Common.UtilsCommons.GetPadingIndex();
            IList<MSysRoom> list = new EyouSoft.BLL.SysStructure.BSysRoom().GetRooms(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount);
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                BindPage();
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        protected void BindPage()
        {

            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;

            if (ExporPageInfoSelect1.intRecordCount == 0)
            {
                ExporPageInfoSelect1.Visible = false;
                ExporPageInfoSelect1.Visible = true;
            }
        }
    }
}
