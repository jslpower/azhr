using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.GovStructure;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.Model.EnumType.IndStructure;
using EyouSoft.Common;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;

namespace Web
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-7
    /// 说明：个人中心：首页
    public partial class _Default : BackPage
    {

        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 7;
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
        protected void Page_Load(object sender, EventArgs e)
        {

            PowerControl();
            if (!IsPostBack)
            {
                this.lblDate.Text = DateTime.Now.ToString("yyyy年MM月");
                IntoDate();
            }
        }


        private void IntoDate()
        {

            #region 待处理事项
            BIndividual bllBIndividual = new BIndividual();
            IList<MMemo> listMMemo = bllBIndividual.GetMemoLst(6, SiteUserInfo.UserId, null, null);

            if (listMMemo != null && listMMemo.Count > 0)
            {
                rptFirst.DataSource = listMMemo;
                rptFirst.DataBind();
            }
            else
                lblMsgFirst.Text = "暂无数据";
            #endregion

            #region 订单提醒
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            var listMOrderRemind = bllBIndividual.GetReceivablesRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, SiteUserInfo.UserId);
            if (listMOrderRemind != null && listMOrderRemind.Count > 0)
            {
                this.rptSecond.DataSource = listMOrderRemind;
                this.rptSecond.DataBind();
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.lblMsgSecond.Text = "暂无数据";
            }
            #endregion

            #region 公告列表
            IList<MNoticeRemind> listMGovNotice = bllBIndividual.GetNoticeRemindLst(5, 1, ref recordCount, SiteUserInfo.UserId, SiteUserInfo.DeptId, SiteUserInfo.CompanyId);
            if (listMGovNotice != null && listMGovNotice.Count > 0)
            {
                rptThird.DataSource = listMGovNotice;
                rptThird.DataBind();
            }
            else
                lblMsgThird.Text = "暂无数据";
            #endregion

            #region 获取今后两天备忘录
            //今天
            IList<MMemo> listMMemoToday = bllBIndividual.GetMemoLst(
                5,
                SiteUserInfo.UserId,
                Utils.GetDateTimeNullable(DateTime.Now.ToShortDateString()),
                Utils.GetDateTimeNullable(DateTime.Now.ToShortDateString()));
            IList<MMemo> listMMemoTodayCount = bllBIndividual.GetMemoLst(
                0,
                SiteUserInfo.UserId,
                Utils.GetDateTimeNullable(DateTime.Now.ToShortDateString()),
                Utils.GetDateTimeNullable(DateTime.Now.ToShortDateString()));
            if (listMMemoToday != null && listMMemoToday.Count > 0)
            {
                if (listMMemoTodayCount.Count > 5)
                    litTodayAll.Text = "<span class=\"jsh_text_li\"><a href=\"javascript:void(0)\" onclick=\"AllBoxy('" + DateTime.Now.ToString("M") + "')\">更多</a></span>";
                rptFour.DataSource = listMMemoToday;
                rptFour.DataBind();
            }
            //明天
            IList<MMemo> listMMemoTom = bllBIndividual.GetMemoLst(
                1,
                SiteUserInfo.UserId,
                Utils.GetDateTimeNullable(DateTime.Now.AddDays(1).ToShortDateString()),
                Utils.GetDateTimeNullable(DateTime.Now.AddDays(1).ToShortDateString()));
            IList<MMemo> listMMemoTomCount = bllBIndividual.GetMemoLst(
                0,
                SiteUserInfo.UserId,
                Utils.GetDateTimeNullable(DateTime.Now.AddDays(1).ToShortDateString()),
                Utils.GetDateTimeNullable(DateTime.Now.AddDays(1).ToShortDateString()));
            if (listMMemoTom != null && listMMemoTom.Count > 0)
            {
                if (listMMemoTomCount.Count > 1)
                    litTomorrowAll.Text = "<span class=\"jsh_text_li\"><a href=\"javascript:void(0)\" onclick=\"AllBoxy('" + DateTime.Now.AddDays(1).ToString("M") + "')\">更多</a></span>";
                rptFive.DataSource = listMMemoTom;
                rptFive.DataBind();
            }
            #endregion
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_订单提醒栏目))
            //    this.phOrderremind.Visible = false;
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_计调提醒栏目))
                this.phjdremind.Visible = false;
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
                this.phskremin.Visible = false;
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
                this.phbgRemind.Visible = false;
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
            //    this.phykremind.Visible = false;
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
            //    this.phhtremind.Visible = false;
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_询价提醒栏目))
            //    this.phxjremind.Visible = false;
        }

        #region 前台调用方法
        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="DepartmentID">部门ID</param>
        protected string GetDepartmentById(int DepartmentID, string companyid)
        {
            //返回信息
            string result = "";
            //实例化部门业务层
            BComDepartment BLL = new BComDepartment();
            MComDepartment model = BLL.GetModel(DepartmentID, companyid);
            if (model != null)
            {
                result = model.DepartName;
            }
            return result;
        }



        /// <summary>
        /// 获得事物提醒中的数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetRemindCount(int type)
        {
            int remindCount = 0;
            BIndividual bllBIndividual = new BIndividual();
            remindCount = bllBIndividual.GetRemindCountByType(SiteUserInfo.CompanyId, SiteUserInfo.UserId, (RemindType)type);
            return remindCount.ToString();
        }
        #endregion
    }
}
