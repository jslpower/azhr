using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using System.Collections.Generic;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;

namespace Web.UserCenter.WorkAwake
{
    using EyouSoft.Model.EnumType.TourStructure;

    public partial class PlanChangeRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：蔡永辉
        /// 创建时间：2012-3-26
        /// 说明：事物中心：变更提醒（计划变更提醒）


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
        /// <summary>
        /// 打印页面Url
        /// </summary>
        private string PrintUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //设置选择
                this.UserCenterNavi1.NavIndex = 7;
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
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            BIndividual bllBIndividual = new BIndividual();
            IList<MTourChangeRemind> listChangeRemind = bllBIndividual.GetTourChangeRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, SiteUserInfo.UserId);
            if (listChangeRemind != null && listChangeRemind.Count > 0)
            {
                this.rptList.DataSource = listChangeRemind;
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

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_变更提醒栏目, true);
                return;
            }
        }

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "IsSureChange":
                    //执行并获取结果
                    msg = IsSureChange();
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
        /// 确认变更
        /// </summary>
        /// <returns></returns>
        private string IsSureChange()
        {
            string result = "";
            if (Utils.GetInt(Utils.GetQueryStringValue("id")) > 0)
            {
                //实例化计划变更实体
                var modelMTourPlanChange = new MTourPlanChangeConfirm();
                //实例化业务
                BTour bllBTour = new BTour();
                //主键编号
                modelMTourPlanChange.Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
                //确认变更人
                modelMTourPlanChange.Confirmer = SiteUserInfo.Name;
                //确认变更人ID
                modelMTourPlanChange.ConfirmerId = SiteUserInfo.UserId;
                //确认变更时间
                modelMTourPlanChange.ConfirmTime = DateTime.Now;
                //计划编号
                modelMTourPlanChange.TourId = Utils.GetQueryStringValue("tourid");
                modelMTourPlanChange.ChangeStatus = (ChangeStatus)Utils.GetInt(Utils.GetQueryStringValue("changestatus"));
                modelMTourPlanChange.ChangeType = (ChangeType)Utils.GetInt(Utils.GetQueryStringValue("ChangeType"));
                switch (bllBTour.TourChangeSure(modelMTourPlanChange))
                {
                    case 0:
                        result = UtilsCommons.AjaxReturnJson("false", "确认变更失败");
                        break;
                    case 1:
                        result = UtilsCommons.AjaxReturnJson("true", "确认变更成功");
                        break;
                    case 2:
                        result = UtilsCommons.AjaxReturnJson("false", "已确认");
                        break;
                    case 3:
                        result = UtilsCommons.AjaxReturnJson("false", "非该团销售员不可操作");
                        break;
                    case 4:
                        result = UtilsCommons.AjaxReturnJson("false", "非该团OP不可操作");
                        break;
                }
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "数据参数id为空");
            return result;
        }
        #endregion

        #endregion
        #region 前台调用方法

        #region 计划变更 颜色处理
        protected string GetTourPlanIschange(bool State, string TourId)
        {
            System.Text.StringBuilder sbChange = new System.Text.StringBuilder();
            string url = PrintUrl + "?tourid=" + TourId;
            //确认 绿色 未确认 红色
            if (State)
            {
                sbChange.AppendFormat("<span class=\"fontgreen\"><a {0}>(变)</a></span>", string.IsNullOrEmpty(TourId) ? "href=\"javascript:;\"" : "target=\"_blank\" href=\"" + url + "\"");
            }
            else
            {
                sbChange.AppendFormat("<span class=\"fontred\"><a {0}>(变)</a></span>", string.IsNullOrEmpty(TourId) ? "href=\"javascript:;\"" : "target=\"_blank\" href=\"" + url + "\"");
            }

            return sbChange.ToString();
        }
        #endregion

        #region 获取操作人信息
        protected string GetOperaterInfo(string tourid)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            EyouSoft.Model.TourStructure.MTourBaoInfo info = new EyouSoft.BLL.TourStructure.BTour().GetTourBaoInfo(tourid);
            if (info != null)
            {
                sb.Append("<b>" + info.TourCode + "</b><br />发布人：" + info.Operator + "<br />发布时间：" + EyouSoft.Common.UtilsCommons.GetDateString(info.IssueTime, ProviderToDate) + "");
            }
            info = null;
            return sb.ToString();
        }
        #endregion

        #region Repeater嵌套绑定
        /// <summary>
        /// Repeater嵌套绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //判断里层repeater处于外层repeater的哪个位置（ AlternatingItemTemplate，FooterTemplate，
            //HeaderTemplate，，ItemTemplate，SeparatorTemplate）
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repTourPlaner = e.Item.FindControl("rptTourPlaner") as Repeater;//找到里层的repeater对象
                Repeater repTourGuide = e.Item.FindControl("rptTourGuide") as Repeater;//找到里层的repeater对象
                //Repeater数据源
                repTourPlaner.DataSource = ((MTourChangeRemind)e.Item.DataItem).TourPlaner;
                repTourPlaner.DataBind();

                //Repeater数据源
                repTourGuide.DataSource = ((MTourChangeRemind)e.Item.DataItem).TourGuide;
                repTourGuide.DataBind();

            }
        }
        #endregion


        #endregion
    }
}
