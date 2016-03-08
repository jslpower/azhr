using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;

namespace Web.UserCenter.WorkAwake
{
    public partial class ChangeRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：事物中心：变更提醒


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
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            BIndividual bllBIndividual = new BIndividual();
            IList<MOrderChangeRemind> listorder = bllBIndividual.GetOrderChangeRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, SiteUserInfo.UserId);
            if (listorder != null && listorder.Count > 0)
            {
                this.rptList.DataSource = listorder;
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
                case "IsSureChange":
                    //执行并获取结果
                    msg = IsSureChange(argument);
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
        /// <param name="tourid"></param>
        /// <returns></returns>
        private string IsSureChange(string tourid)
        {
            string result = "";
            if (!string.IsNullOrEmpty(tourid))
            {
                //实例化订单变更实体
                MOrderChangeRemind modelMOrderChangeRemind = new MOrderChangeRemind();
                //实例化业务
                BIndividual bllBIndividual = new BIndividual();
                modelMOrderChangeRemind = bllBIndividual.GetOrderChangeModel(SiteUserInfo.CompanyId, tourid);
                if (modelMOrderChangeRemind != null)
                {
                    //确认变更人
                    modelMOrderChangeRemind.SurePerson = SiteUserInfo.Name;
                    //确认变更人ID
                    modelMOrderChangeRemind.SurePersonId = SiteUserInfo.UserId;
                    //确认变更时间
                    modelMOrderChangeRemind.SureTime = DateTime.Now;
                    //公司编号
                    modelMOrderChangeRemind.CompanyId = SiteUserInfo.CompanyId;
                    if (bllBIndividual.OrderChangeSure(modelMOrderChangeRemind))
                        result = UtilsCommons.AjaxReturnJson("true", "确认变更成功");
                    else
                        result = UtilsCommons.AjaxReturnJson("false", "确认变更失败");
                }
                else
                    result = UtilsCommons.AjaxReturnJson("false", "数据对象为空");
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "数据参数id为空");
            return result;
        }

        #endregion
        #region 前台调用方法


        #endregion
    }
}
