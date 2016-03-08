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
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    public partial class BianGengQueRen : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utils.GetQueryStringValue("doType") == "Save")
            {
                Save();
            }
            PageInit();

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            var model = new EyouSoft.BLL.TourStructure.BTour().GetTourChangeModel(
                CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("id")));

            if (model != null)
            {
                this.ltlTourCode.Text = model.TourCode;
                this.ltlAreaName.Text = model.AreaName;
                this.ltlRouteName.Text = model.RouteName;
                this.ltlTitle.Text = model.Title;
                this.ltlOperator.Text = model.Operator;
                this.ltlIssueTime.Text = model.IssueTime.ToShortDateString();
                this.ltlContent.Text = model.Content;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (!this.CheckGrant(Privs.计调中心_业务变更_变更确认))
            {
                this.AjaxResponse(UtilsCommons.AjaxReturnJson("-1","您没有变更确认权限！"));
                return;
            }
            var rtn =
                new EyouSoft.BLL.TourStructure.BTour().TourChangeSure(
                    new MTourPlanChangeConfirm()
                    {
                        Id = Utils.GetInt(Utils.GetQueryStringValue("id")),
                        TourId = Utils.GetQueryStringValue("tourid"),
                        ConfirmerType = ConfirmerType.计调员,
                        ConfirmerId = this.SiteUserInfo.UserId,
                        Confirmer = this.SiteUserInfo.Name,
                        ChangeStatus = ChangeStatus.计调已确认,
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
    }
}
