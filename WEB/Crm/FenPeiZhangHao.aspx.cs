using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.CrmCenter
{
    /// <summary>
    /// 客户关系账号管理
    /// 创建人：刘树超 2013-6-5
    /// </summary>
    public partial class FenPeiZhangHao : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax request
            switch (EyouSoft.Common.Utils.GetQueryStringValue("doType"))
            {
                case "updateuserstatus":
                    UpdateUserStatus();
                    break;
                case "setcrmuser":
                    SetCrmUser();
                    break;
            }
            #endregion

            InitCrmLxrs();
        }

        #region private members
        /// <summary>
        /// 绑定客户单位联系人
        /// </summary>
        void InitCrmLxrs()
        {
            var items = new EyouSoft.BLL.CrmStructure.BCrm().GetCrmUsers(Utils.GetQueryStringValue("crmId"));

            if (items != null && items.Count > 0)
            {
                rtpLxrs.DataSource = items;
                rtpLxrs.DataBind();
            }
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        void UpdateUserStatus()
        {
            string[] userids = Utils.GetFormValue("userids").Split(',');
            string status = Utils.GetFormValue("status");
            string crmid = Utils.GetFormValue("crmid");
            EyouSoft.Model.EnumType.ComStructure.UserStatus? _status = null;

            switch (status)
            {
                case "enable": _status = EyouSoft.Model.EnumType.ComStructure.UserStatus.正常; break;
                case "stop": _status = EyouSoft.Model.EnumType.ComStructure.UserStatus.已停用; break;
                case "block": _status = EyouSoft.Model.EnumType.ComStructure.UserStatus.黑名单; break;
                default: break;
            }

            if (!_status.HasValue) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "错误的状态设置"));
            if (userids == null || userids.Length == 0) AjaxResponse(UtilsCommons.AjaxReturnJson("0", "请选择已经开启过的账号进行状态设置"));

            foreach (var s in userids)
            {
                new EyouSoft.BLL.CrmStructure.BCrm().SetCrmUserStatus(CurrentUserCompanyID, crmid, s, _status.Value);
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson("1", "状态设置成功"));
        }

        /// <summary>
        /// 设置客户单位用户信息
        /// </summary>
        void SetCrmUser()
        {
            string crmId = Utils.GetFormValue("crmid");
            string lxrId = Utils.GetFormValue("lxrid");
            string username = Utils.GetFormValue("username");
            var pwd = new EyouSoft.Model.ComStructure.MPasswordInfo()
            {
                NoEncryptPassword = Utils.GetFormValue("pwd")
            };

            int bllRetCode = new EyouSoft.BLL.CrmStructure.BCrm().SetCrmUser(CurrentUserCompanyID, SiteUserInfo.UserId, crmId, lxrId, username, pwd);
            if (bllRetCode == 1)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else if (bllRetCode == -1)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1"));
            }

            AjaxResponse(UtilsCommons.AjaxReturnJson("0"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// rptLxrs_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptLxrs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1) return;
            Literal ltrUserName = (Literal)e.Item.FindControl("ltrUserName");
            Literal ltrStatus = (Literal)e.Item.FindControl("ltrStatus");
            if (ltrUserName == null || ltrStatus == null) return;
            EyouSoft.Model.CrmStructure.MCrmUserInfo info = (EyouSoft.Model.CrmStructure.MCrmUserInfo)e.Item.DataItem;
            if (info == null) return;

            string usertnameText = string.Empty;
            string statusText = string.Empty;
            if (!string.IsNullOrEmpty(info.UserId))
            {
                usertnameText = string.Format("<span>{0}</span><input type=\"hidden\" name=\"txtUsername\" value=\"{0}\" maxlength=\"20\" />", info.Username);
            }
            else
            {
                usertnameText = "<input type=\"text\" name=\"txtUsername\" class=\"formsize80 inputtext\" style=\"width:65px;\" maxlength=\"20\" />";
            }

            switch (info.Status)
            {
                case EyouSoft.Model.EnumType.ComStructure.UserStatus.黑名单: statusText = "<b style=\"color:#ff0000\">黑名单</b>"; break;
                case EyouSoft.Model.EnumType.ComStructure.UserStatus.未启用: statusText = "<b style=\"color:#ff0000\">未启用</b>"; break;
                case EyouSoft.Model.EnumType.ComStructure.UserStatus.已停用: statusText = "<b style=\"color:#ff0000\">已停用</b>"; break;
                case EyouSoft.Model.EnumType.ComStructure.UserStatus.正常: statusText = "<b style=\"color:#000000\">正常</b>"; break;
                default: break;
            }

            ltrUserName.Text = usertnameText;
            ltrStatus.Text = statusText;
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion

    }
}
