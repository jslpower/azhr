using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.Web.Sys
{

    public partial class XingChengBeiZhu : BackPage
    {
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (!string.IsNullOrEmpty(dotype) && dotype == "del")
            {
                int result = new BSysOptionConfig().DeleteMSysJourneyMark(id);
                if (result == 1)
                {

                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "删除成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "删除失败"));
                }
            }
            initPage();
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void initPage()
        {
            pageIndex = EyouSoft.Common.UtilsCommons.GetPadingIndex();
            IList<MSysJourneyMark> list = new BSysOptionConfig().GetMSysJourneyMarkList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, null);
            if (list.Count > 0)
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
