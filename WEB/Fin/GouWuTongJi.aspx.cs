using System;

namespace EyouSoft.Web.Fin
{
    using EyouSoft.Common.Page;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;

    public partial class GouWuTongJi : BackPage
    {
        protected int PageSize = 20;
        protected int PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int RecordCount = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            
            //初始化
            DataInit();
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            var queryModel = GetChaXunInfo();

            var ls = new BFinance().GetGouWuTongJi(
                this.PageSize,
                this.PageIndex,
                ref  this.RecordCount,
                queryModel);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && this.RecordCount > this.PageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = this.PageSize;
            ExporPageInfoSelect1.CurrencyPage = this.PageIndex;
            ExporPageInfoSelect1.intRecordCount = this.RecordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_购物统计表_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_购物统计表_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MGouWuTongJiBase GetChaXunInfo()
        {
            var info = new MGouWuTongJiBase
                {
                    //系统公司编号
                    CompanyId = this.CurrentUserCompanyID,
                    //国籍
                    GuoJi = Utils.GetQueryStringValue("gj"),
                    //进店日期_开始
                    JinDianRiQiS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd")),
                    //进店日期_结束
                    JinDianRiQiE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed")),
                    //购物店
                    GysName = Utils.GetQueryStringValue("nm")
                };
            return info;
        }
        #endregion    
    }
}
