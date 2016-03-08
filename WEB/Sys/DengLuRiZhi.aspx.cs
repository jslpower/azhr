using System;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Sys
{
    /// <summary>
    /// 登录日志
    /// 修改记录：
    /// 1、2013-06-04 刘树超 创建
    /// </summary>
    public partial class DengLuRiZhi : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                PageInit();
            }
        }

        protected void PageInit()
        {

            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.Model.SysStructure.MSysLogLoginSearchInfo search = new EyouSoft.Model.SysStructure.MSysLogLoginSearchInfo();
            search.Name = Utils.GetQueryStringValue("txtName");
            search.STime = string.IsNullOrEmpty(Utils.GetQueryStringValue("txtLoginTimeS")) ? null : (DateTime?)Utils.GetDateTime(Utils.GetQueryStringValue("txtLoginTimeS"));
            search.ETime = string.IsNullOrEmpty(Utils.GetQueryStringValue("txtLoginTimeE")) ? null : (DateTime?)Utils.GetDateTime(Utils.GetQueryStringValue("txtLoginTimeE"));

            var list = new EyouSoft.BLL.SysStructure.BSysLogHandle().GetLoginLogs(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, search);
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                this.repList.EmptyText = "<tr><td colspan=\"4\" align=\"center\">未找到相关记录!</td></tr>";
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

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_操作日志_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_操作日志_栏目, false);
                return;
            }
        }
    }
}
