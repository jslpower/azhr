using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.Gys
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-09
    /// 说明: 资源管理： 路线库
    public partial class PathList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        #region 获取打印单
        protected string PrintPageXL = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion
            string PathType = string.IsNullOrEmpty(Utils.GetQueryStringValue("PathType")) ? null : Utils.GetQueryStringValue("PathType");
            PrintPageXL = string.Empty;// new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.线路资源打印单);
            //路线名称
            string PathName = Utils.GetQueryStringValue("txtPathName");
            //天数
            string DataCount = Utils.GetQueryStringValue("txtData");
            //发布人
            string Auther = Utils.GetQueryStringValue("txtAuthor");
            //开始时间
            DateTime? StartTime = String.IsNullOrEmpty(Utils.GetQueryStringValue("txtStartTime")) ? null : (DateTime?)DateTime.Parse(Utils.GetQueryStringValue("txtStartTime"));
            //结束时间
            DateTime? EndTime = String.IsNullOrEmpty(Utils.GetQueryStringValue("txtEndTime")) ? null : (DateTime?)DateTime.Parse(Utils.GetQueryStringValue("txtEndTime"));
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel();
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //路线类型

                //初始化
                DataInit(PathType, PathName, DataCount, Auther, StartTime, EndTime);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string pathtype, string pathname, string datacount, string auther, DateTime? starttime, DateTime? endtime)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page")) == 0 ? 1 : Utils.GetInt(Utils.GetQueryStringValue("page"));

            #region 获取查询条件
            //线路区域
            int areaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"));
            //线路名称
            string routeName = Utils.GetQueryStringValue("txtRouteName");
            //出发时间
            DateTime? txtBeginDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));

            DateTime? txtEndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            int? Days = Utils.GetIntNull(Utils.GetQueryStringValue("txtDays"));


            //团队状态
            string tourState = Utils.GetQueryStringValue("sltTourState");
            //操作状态
            string state = Utils.GetQueryStringValue("selState");
            #endregion

            var bll = new EyouSoft.BLL.HTourStructure.BTour();
            var searchModel = new MTourSearch
            {
                CompanyId = this.CurrentUserCompanyID,
                AreaId = areaId,
                IssueSTime = txtBeginDate,
                IssueETime = txtEndDate,
                RouteName = routeName,
                TourType = TourType.线路产品,
                Days = Days
            };

            var Pathlist = bll.GetTourInfoList(pageSize, pageIndex, ref recordCount, searchModel);
            if (Pathlist != null && Pathlist.Count > 0)
            {
                rptList.DataSource = Pathlist;
                rptList.DataBind();
            }
            else
            {
                this.rptList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
                ExporPageInfoSelect1.Visible = false;
            }
            //绑定分页
            BindPage();
        }
        /// <summary>
        /// 获取线路区域名字
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        protected string GetAreaName(string areaId)
        {
            string name = string.Empty;
            EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.BLL.ComStructure.BComArea().GetModel(Utils.GetInt(areaId), SiteUserInfo.CompanyId);
            if (model != null)
            {
                name = model.AreaName;
            }
            return name;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "delete":
                    //判断权限
                    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_删除))
                    {
                        string id = Utils.GetQueryStringValue("id");
                        msg = this.DeleteData(id);
                    }
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string ids)
        {
            string[] id = ids.Split(',');
            //删除操作
            bool result = false;
            if (id.Length > 0)
            {
                var bll = new EyouSoft.BLL.TourStructure.BTour();

                result = bll.DeleteTour(SiteUserInfo.CompanyId, id);

            }
            if (result)
            {
                return UtilsCommons.AjaxReturnJson("1", "删除成功!");
            }
            else
            {
                return UtilsCommons.AjaxReturnJson("0", "删除失败!");
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_新增))
            {
                this.path_add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_修改))
            {
                this.path_edit.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路_删除))
            {
                this.path_del.Visible = false;
            }

        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ListToExcel()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            var s = new StringBuilder();
            //线路区域 	线路名称 	天数 	发布日期 	发布人 	
            s.Append("线路区域\t线路名称\t天数\t发布日期\t发布人\n");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 获取查询条件
            //线路区域
            int areaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"));
            //线路名称
            string routeName = Utils.GetQueryStringValue("txtRouteName");
            //出发时间
            DateTime? txtBeginDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));

            DateTime? txtEndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            int? Days = Utils.GetIntNull(Utils.GetQueryStringValue("txtDays"));


            //团队状态
            string tourState = Utils.GetQueryStringValue("sltTourState");
            //操作状态
            string state = Utils.GetQueryStringValue("selState");
            #endregion

            var bll = new EyouSoft.BLL.HTourStructure.BTour();
            var searchModel = new MTourSearch
            {
                CompanyId = this.CurrentUserCompanyID,
                AreaId = areaId,
                IssueSTime = txtBeginDate,
                IssueETime = txtEndDate,
                RouteName = routeName,
                TourType = TourType.线路产品,
                Days = Days
            };

            var list = bll.GetTourInfoList(pageSize, pageIndex, ref recordCount, searchModel);
            if (list != null && list.Count > 0)
            {
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\n",
                        t.AreaName,
                        t.RouteName,
                        t.TourDays,
                        EyouSoft.Common.UtilsCommons.GetDateString(t.IssueTime, ProviderToDate),
                        t.Operator);
                }
            }

            ResponseToXls(s.ToString());
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 获取发布人信息
        /// </summary>
        /// <param name="id">SourceId</param>
        /// <returns></returns>
        protected string GetContactInfo(object List)
        {
            IList<EyouSoft.Model.ComStructure.MComUser> userlist = (IList<EyouSoft.Model.ComStructure.MComUser>)List;
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>电话</th><th align='center'>传真</th><th align='center'>手机</th><th align='center'>QQ</th><th align='center'>MSN</th><th align='center'>E-mail</th></tr>");
            if (userlist != null && userlist.Count > 0)
            {
                contactinfo.Append("<tr><td align='center'>" + userlist[0].ContactTel + "</td><td align='center'>" + userlist[0].ContactFax + "</td><td align='center' >" + userlist[0].ContactMobile + "</td><td align='center'>" + userlist[0].QQ + "</td><td align='center'>" + userlist[0].MSN + "</td><td align='center'>" + userlist[0].ContactEmail + "</td></tr>");

            }
            contactinfo.Append("</table>");
            return contactinfo.ToString();
        }
        #endregion
    }
}
