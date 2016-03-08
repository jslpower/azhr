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

namespace EyouSoft.Web.Plan
{
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.TourStructure;

    public partial class BianGeng : BackPage
    {
        protected int PageSize = 20;
        protected int PageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int RecordCount = 0;
        protected Menu2 _sl = Menu2.计调中心_业务变更;
        /// <summary>
        /// 打印页面Url
        /// </summary>
        protected string PrintUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            PrintUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);

            #region 处理AJAX请求
            //存在ajax请求
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "xiaoshouqueren":
                    this.SaleConfirm(ChangeStatus.计调未确认);
                    break;
                case "xiaoshouzanbuchuli":
                    this.SaleConfirm(ChangeStatus.销售暂不处理);
                    break;
                default:
                    break;
            }
            #endregion

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

            var ls = new EyouSoft.BLL.TourStructure.BTour().GetTourPlanChange(
                this.PageSize, this.PageIndex, ref this.RecordCount, queryModel);

            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rptList.DataSource = ls;
                rptList.DataBind();
                BindPage();
            }
            else
            {
                this.pan_Msg.Visible = true;
            }
            ExporPageInfoSelect1.Visible =
                 ls != null && ls.Count > 0 && this.RecordCount > this.PageSize;
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
            this._sl = (Menu2)Utils.GetInt(this.SL);
            switch (this._sl)
            {
                case Menu2.计调中心_业务变更:
                    if (!CheckGrant(Privs.计调中心_业务变更_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.计调中心_业务变更_栏目, true);
                        return;
                    }
                    break;
                case Menu2.销售中心_导游变更:
                    if (!CheckGrant(Privs.销售中心_导游变更_栏目))
                    {
                        Utils.ResponseNoPermit(Privs.销售中心_导游变更_栏目, true);
                        return;
                    }
                    break;
                default:
                    Utils.ResponseGoBack();
                    break;
            }
        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MTourPlanChangeBase GetChaXunInfo()
        {
            var saleId = Utils.GetQueryStringValue(this.yw.SellsIDClient);
            var sellerName = Utils.GetQueryStringValue(this.yw.SellsNameClient);
            var planerId = Utils.GetQueryStringValue(this.jd.SellsIDClient);
            var planer = Utils.GetQueryStringValue(this.jd.SellsNameClient);
            var guideId = Utils.GetQueryStringValue(this.dy.SellsIDClient);
            var guideNm = Utils.GetQueryStringValue(this.dy.SellsNameClient);

            this.yw.SellsID = saleId;
            this.yw.SellsName = sellerName;
            this.jd.SellsID = planerId;
            this.jd.SellsName = planer;
            this.dy.SellsID = guideId;
            this.dy.SellsName = guideNm;

            var info = new MTourPlanChangeBase
            {
                CompanyId = this.CurrentUserCompanyID,
                TourCode = Utils.GetQueryStringValue("th"),
                AreaName = Utils.GetQueryStringValue("qy"),
                SaleId = saleId,
                SellerName = sellerName,
                PlanerId = planerId,
                Planer = planer,
                GuideId = guideId,
                GuideNm = guideNm,
                State = this._sl==Menu2.计调中心_业务变更?null:(ChangeStatus?)Utils.GetInt(Utils.GetQueryStringValue("sel")),
                IssueTimeS = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("sd")),
                IssueTimeE = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("ed")),
                SL = this._sl
            };
            return info;
        }

        /// <summary>
        /// 销售确认
        /// </summary>
        /// <returns></returns>
        private void SaleConfirm(ChangeStatus status)
        {
            var rtn =
                new EyouSoft.BLL.TourStructure.BTour().TourChangeSure(
                    new MTourPlanChangeConfirm()
                        {
                            Id = Utils.GetInt(Utils.GetQueryStringValue("id")),
                            TourId = Utils.GetQueryStringValue("tourid"),
                            ConfirmerType = ConfirmerType.销售员,
                            ConfirmerId = this.SiteUserInfo.UserId,
                            Confirmer = this.SiteUserInfo.Name,
                            ChangeStatus = status,
                            ChangeType = ChangeType.导游变更
                        });
            switch (rtn)
            {
                case 0:
                    this.AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "操作失败!")); 
                    break;
                case 1:
                    this.AjaxResponse(UtilsCommons.AjaxReturnJson("1")); 
                    break;
                case 2:
                    this.AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "该变更已确认!"));
                    break;
                case 3:
                    this.AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "非该团销售员不可操作!"));
                    break;
                case 4:
                    this.AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "非该团OP不可操作!"));
                    break;
                default:
                    break;
            }
        }

        #region 获取操作人信息
        /// <summary>
        /// 操作人信息
        /// </summary>
        /// <param name="tourid">团号</param>
        /// <returns></returns>
        protected string GetOperaterInfo(string tourid)
        {
            var sb = new System.Text.StringBuilder();
            var info = new EyouSoft.BLL.HTourStructure.BTour().GetTourModel(tourid);
            if (info != null)
            {
                sb.Append("<b>" + info.TourCode + "</b><br />发布人：" + info.Operator + "<br />发布时间：" + EyouSoft.Common.UtilsCommons.GetDateString(info.IssueTime, ProviderToDate) + "");
            }
            info = null;
            return sb.ToString();
        }
        #endregion


        #endregion 
    }
}
