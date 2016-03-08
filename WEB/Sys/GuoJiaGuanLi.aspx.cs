using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.SysStructure;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.Sys
{
    public partial class GuoJiaGuanLi : BackPage
    {
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BGeography bll = new BGeography();
            string dotype = Utils.GetQueryStringValue("dotype");
            int[] ids = Utils.GetIntArray(Utils.GetQueryStringValue("id"), ",");
            int result = 0;
            if (!string.IsNullOrEmpty(dotype) && dotype == "del")
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    result += bll.DeleteCountry(SiteUserInfo.CompanyId, ids[i]);

                }
                if (result == ids.Length)
                {

                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "删除失败,或者此国家下面有省份"));
                }
            }
            initPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void initPage()
        {
            pageIndex = EyouSoft.Common.UtilsCommons.GetPadingIndex();
            IList<MSysCountry> list = new BGeography().GetCountrys(SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                bindPage();
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        protected void bindPage()
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
