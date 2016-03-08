using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.Sys
{
    /// <summary>
    /// 操作日志列表
    /// </summary>
    /// 登录日志
    /// 修改记录：
    /// 1、2013-06-04 刘树超 创建
    public partial class CaoZuoRiZhi : BackPage
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
            //部门编号
            int? DepartId = Utils.GetIntNull(Utils.GetQueryStringValue("departId"));
            //操作员
            string Operator = Utils.GetQueryStringValue("operator");
            //操作开始时间
            string BeginDate = Utils.GetQueryStringValue("txtBeginDate");
            //操作结束时间
            string EndDate = Utils.GetQueryStringValue("txtEndDate");
            //当前页
            pageIndex = UtilsCommons.GetPadingIndex();
            IList<EyouSoft.Model.ComStructure.MComDepartment> list1 = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(SiteUserInfo.CompanyId);
            this.ddlDepart.DataTextField = "DepartName";
            this.ddlDepart.DataValueField = "DepartId";
            this.ddlDepart.DataSource = list1;
            this.ddlDepart.DataBind();
            this.ddlDepart.Items.Insert(0, new ListItem("请选择", "0"));
            IList<EyouSoft.Model.SysStructure.MSysLogHandleInfo> list = null;
            EyouSoft.Model.SysStructure.MSysLogHandleSearch search = new EyouSoft.Model.SysStructure.MSysLogHandleSearch();
            search.DeptId = DepartId == 0 ? null : DepartId;
            search.Operator = Operator;
            if (BeginDate != "") { search.SDate = Utils.GetDateTime(BeginDate); }
            if (EndDate != "") { search.EDate = Utils.GetDateTime(EndDate); }
            list = new EyouSoft.BLL.SysStructure.BSysLogHandle().GetLogs(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, search);
            this.ddlDepart.SelectedValue = DepartId.ToString();
            this.txtOperator.Value = Operator;
            this.txtBeginDate.Value = BeginDate;
            this.txtEndDate.Value = EndDate;
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                this.repList.EmptyText = "<tr><td colspan=\"6\" align=\"center\">未找到相关记录!</td></tr>";
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
