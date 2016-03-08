using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.UserCenter.WorkAwake
{
    public partial class BackRemind : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：事物中心：回团提醒

        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
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
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != "")
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //设置选择
                this.UserCenterNavi1.NavIndex = 4;
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


            //IList<EyouSoft.Model.IndStructure.MTourRemind> list = new EyouSoft.BLL.IndStructure.BIndividual().GetTourRemindLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.UserId, SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.SysConfiguration.回团提醒);

            //if (list != null && list.Count > 0)
            //{
            //    this.rptList.DataSource = list;
            //    this.rptList.DataBind();
            //    //绑定分页
            //    BindPage();
            //}
            //else
            //{
            //    this.lblMsg.Text = "没有相关数据!";
            //    this.ExporPageInfoSelect1.Visible = false;
            //    this.ExporPageInfoSelect2.Visible = false;
            //}
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
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "update":
                    int day = Utils.GetInt(Utils.GetQueryStringValue("day"));
                    //bool result = new EyouSoft.BLL.IndStructure.BIndividual().SetAdvanceRemind(day, SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.SysConfiguration.回团提醒);
                    ////执行并获取结果
                    //if (result)
                    //{
                    //    msg = "OK";
                    //}
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
        private string DeleteData(string id)
        {
            string msg = string.Empty;
            //删除操作
            return msg;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.个人中心_事务提醒_回团提醒))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.个人中心_事务提醒_回团提醒, false);
            //    return;
            //}
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 某某方法
        /// </summary>
        /// <param name="i">参数1</param>
        /// <param name="s">参数2</param>
        protected void XXX(int i, string s)
        {

        }
        #endregion
    }
}
