using System;

namespace EyouSoft.WebFX
{
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.HTourStructure;

    public partial class XunJia : FrontPage
    {
        #region 分页参数

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
        /// <summary>
        /// 总条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPageSp = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.BLL.ComStructure.BComSetting comSettingBll = new EyouSoft.BLL.ComStructure.BComSetting();
            PrintPageSp = comSettingBll.GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台散拼线路行程单);
            string dotye = Utils.GetQueryStringValue("dotype");
            string quoteid = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrEmpty(quoteid))
            {
                AJAX(dotye);
            }

            if (!IsPostBack)
            {
                //公告
                this.HeadDistributorControl1.CompanyId = SiteUserInfo.CompanyId;
                this.HeadDistributorControl1.IsPubLogin =
     System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == this.SiteUserInfo.Username;
                //绑定数据、分页
                BindSource();
            }

        }

        #region 处理AJAX请求
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "del":
                    //判断权限
                    string[] ids = Utils.GetQueryStringValue("id").Split(',');
                    //执行并获取结果
                    msg = DeleteData(ids);
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        #endregion

        #region 列表删除
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string[] ids)
        {
            string msg = string.Empty;
            //删除操作
            if (ids.Length > 0)
            {
                EyouSoft.BLL.HTourStructure.BQuote bll = new EyouSoft.BLL.HTourStructure.BQuote();
                foreach (string id in ids)
                {
                    bll.DeleteQuote(id);
                }

                msg = UtilsCommons.AjaxReturnJson("1", (String)GetGlobalResourceObject("string", "删除成功"));

            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", (String)GetGlobalResourceObject("string", "请选择一行数据"));
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// 绑定数据、分页控件
        /// </summary>
        public void BindSource()
        {
            MQuoteSearch searchmodel = new MQuoteSearch();
            searchmodel.CompanyId = SiteUserInfo.CompanyId;
            if (!this.HeadDistributorControl1.IsPubLogin)
            {
                searchmodel.BuyCompanyID = CurrentUserCompanyID;
            }
            searchmodel.RouteName = Utils.GetQueryStringValue("routename");
            if (Utils.GetQueryStringValue("a") != "0" && Utils.GetQueryStringValue("a") != "")
            {
                searchmodel.AreaId = Utils.GetIntNull(Utils.GetQueryStringValue("a"));
            }
            searchmodel.BeginBuyTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd"));
            searchmodel.EndBuyTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed"));
            var list = new EyouSoft.BLL.HTourStructure.BQuote().GetQuoteList(
                pageSize, pageIndex, ref recordCount, searchmodel);
            this.RtPlan.DataSource = list;
            this.RtPlan.DataBind();

            BindPage();

        }

        protected string GetDateTime(object obj)
        {
            string str = string.Empty;
            DateTime dt = (DateTime)obj;
            if (dt != null)
            {
                str = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
            }
            return str;
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// 订单信息汇总表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintPage(string tourId, string type)
        {
            string url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.分销商平台订单信息汇总单);
            url = url + "?tourId=" + tourId + "&type=" + type;
            return url;

        }

        #region 根据报价状态和次数显示信息
        /// <summary>
        /// 根据报价状态和次数显示信息
        /// </summary>
        /// <param name="count">报价次数</param>
        /// <param name="quoteState">报价状态</param>
        /// <returns></returns>
        protected string GetHtmlByState(string count, EyouSoft.Model.EnumType.TourStructure.QuoteState quoteState, string cancelReason)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (quoteState)
            {
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功:
                    sb.Append("<span class='fontgreen' data-class='QuoteState' data-state='" + (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功 + "'>" + (String)GetGlobalResourceObject("string", "报价成功") + "</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价:
                    sb.Append("<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>" + (String)GetGlobalResourceObject("string", "取消报价") + "</span></a><div style='display:none'><b>" + (String)GetGlobalResourceObject("string", "取消原因") + "</b>:" + EyouSoft.Common.Function.StringValidate.TextToHtml(cancelReason) + "</div>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理:
                    sb.Append("<span data-class='QuoteState' data-state='0'>" + (String)GetGlobalResourceObject("string", "未报价成功") + "</span>"); break;
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 截取AreaName
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        protected string GetAreaName(object obj)
        {
            int areaid = Utils.GetInt(obj.ToString());
            string areaName = string.Empty;
            if (areaid > 0)
            {
                var _bllComArea = new EyouSoft.BLL.ComStructure.BComArea();
                var model = _bllComArea.GetModel(areaid, SiteUserInfo.CompanyId, (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetQueryStringValue("LgType")));
                if (model != null)
                {
                    areaName = model.AreaName;
                }
            }

            if (!string.IsNullOrEmpty(areaName))
            {
                if (areaName.Length > 10)
                {
                    areaName = areaName.Substring(0, 10) + "...";
                }
            }
            return areaName;
        }
    }
}
