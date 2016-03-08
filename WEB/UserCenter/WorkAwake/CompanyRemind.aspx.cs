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
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.BLL.IndStructure;
using System.Collections.Generic;
using EyouSoft.Model.IndStructure;

namespace Web.UserCenter.WorkAwake
{
    public partial class CompanyRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：事物中心：公司提醒

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
            //设置选择
            this.UserCenterNavi1.NavIndex = 9;
            this.UserCenterNavi1.PrivsList = SiteUserInfo.Privs;
            if (!IsPostBack)
            {
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
            BIndividual bllBIndividual = new BIndividual();
            IList<MCompanyContractExpireRemind> list = bllBIndividual.GetCompanyContractExpireRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
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
        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            string argument = Utils.GetQueryStringValue("argument");
            //对应执行操作
            switch (doType)
            {
                case "SetLastTime":
                    //执行并获取结果
                    msg = SetLastTime(argument);
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
        /// 设置几天前提醒
        /// </summary>
        /// <param name="id">天数</param>
        /// <returns></returns>
        private string SetLastTime(string day)
        {
            string msg = string.Empty;
            //设置操作
            if (new EyouSoft.BLL.ComStructure.BComSetting().SetGongSiHeTongTiXing(SiteUserInfo.CompanyId,Utils.GetInt(day)))
            {
                msg = UtilsCommons.AjaxReturnJson("true", "您设置公司合同到期前" + day + "提醒成功");
            }
            else
                msg = UtilsCommons.AjaxReturnJson("false", "您设置公司合同到期前" + day + "提醒失败");

            return msg;
        }
        #endregion

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

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_合同到期提醒栏目, true);
                return;
            }
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 获取公司合同到期提醒
        /// </summary>
        /// <returns></returns>
        protected string GetLastTime()
        {
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (setting != null) return setting.ComPanyContractRemind.ToString();

            return string.Empty;
        }
        #endregion
    }
}