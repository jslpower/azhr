using System;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.HTourStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.BLL.HTourStructure;

namespace EyouSoft.Web.Fin
{
    public partial class DanTuanGuaZhang : BackPage
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

            var ls = new BTour().GetTourInfoList(
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
            if (!CheckGrant(Privs.财务管理_单团挂账_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_单团挂账_栏目, true);
                return;
            }
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MTourSearch GetChaXunInfo()
        {
            var info = new MTourSearch
            {
                //系统公司编号
                CompanyId = this.CurrentUserCompanyID,
                //团号
                TourCode = Utils.GetQueryStringValue("th"),
            };
            return info;
        }
        #endregion
    }
}
