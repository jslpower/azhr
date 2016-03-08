using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;

namespace Web.UserCenter.WorkAwake
{
    public partial class OrderRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 修改人：蔡永辉
        /// 修改时间：2012-4-4
        /// 说明：事物中心：订单提醒 

        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 15;
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

        #region 获取页面参数
        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获取页面参数
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            #endregion
            if (!IsPostBack)
            {
                //ajax操作类型
                string ajaxtype = Utils.GetQueryStringValue("ajaxtype");
                if (!string.IsNullOrEmpty(ajaxtype) && ajaxtype == "OperateOrder")
                    DoAjax();

                //设置选择
                this.UserCenterNavi1.NavIndex = 1;
                this.UserCenterNavi1.PrivsList = SiteUserInfo.Privs;
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            IList<EyouSoft.Model.IndStructure.MOrderRemind> list = null;
            BIndividual bllBIndividual = new BIndividual();
            list = bllBIndividual.GetOrderRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.UserId, SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
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

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_订单提醒栏目))
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_计调提醒栏目))
                {
                    Utils.TopRedirect("/UserCenter/WorkAwake/OperaterRemind.aspx?sl=" + sl);
                    return;
                }
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_计调提醒栏目))
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
                {
                    Utils.TopRedirect("/UserCenter/WorkAwake/CollectionRemind.aspx?sl=" + sl);
                    return;
                }
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
                {
                    Utils.TopRedirect("/UserCenter/WorkAwake/ChangeRemind.aspx?sl=" + sl);
                    return;
                }
            }
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
            //{
            //    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
            //    {
            //        Utils.TopRedirect("/UserCenter/WorkAwake/Preview/PreviewHotel.aspx?sl=" + sl);
            //        return;
            //    }
            //}
            //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
            //{
            //    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
            //    {
            //        Utils.TopRedirect("/UserCenter/WorkAwake/LaborRemind.aspx?sl=" + sl);
            //        return;
            //    }
            //}
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_询价提醒栏目))
                {
                    Utils.TopRedirect("/UserCenter/WorkAwake/InquiryRemind.aspx?sl=" + sl);
                    return;
                }
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_询价提醒栏目))
            {
                Utils.TopRedirect("/Default.aspx?sl=0");
                return;
            }
        }


        #region ajax 操作
        /// <summary>
        /// ajax操作
        /// </summary>
        protected void DoAjax()
        {
            //ajax操作数据id
            string ajaxid = Utils.GetQueryStringValue("orderid");

            if (!string.IsNullOrEmpty(ajaxid))
            {

            }

        }

        #endregion

        #endregion

    }
}
