using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Model.SysStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.Sys
{
    public partial class DaoYouXuZhi : BackPage
    {
        protected int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dotype = Utils.GetQueryStringValue("dotype");
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (!string.IsNullOrEmpty(dotype) && dotype == "del")
            {
                int i = new BSysOptionConfig().DeleteGuideKnow(id);
                RCWE(UtilsCommons.AjaxReturnJson(i.ToString(), i == 1 ? "删除成功" : "删除失败"));
            }
            initPage();
        }
        private void initPage()
        {
            pageIndex = EyouSoft.Common.UtilsCommons.GetPadingIndex();
            IList<MSysGuideKonw> list = new BSysOptionConfig().GetGuideKnowList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, null);
            if (list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                BindPage();
            }

        }


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

        protected string getDeptName(string id)
        {
            int Bid = Utils.GetInt(id);
            if (Bid == 0) return "";
            var model = new EyouSoft.BLL.ComStructure.BComDepartment().GetModel(Bid, SiteUserInfo.CompanyId);
            if (model == null) return "";
            return model.DepartName;
        }
    }
}
