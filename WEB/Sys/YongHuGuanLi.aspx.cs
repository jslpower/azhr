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
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Collections.Generic;

namespace EyouSoft.Web.Sys
{
    public partial class YongHuGuanLi : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl(Utils.GetQueryStringValue("dotype"));
                //所属部门非必填
                this.BelongDepart.IsNotValid = false;
                string dotype = Utils.GetQueryStringValue("dotype");
                if (!string.IsNullOrEmpty(dotype))
                {
                    Response.Clear();
                    switch (dotype)
                    {

                        //删除用户
                        case "del":
                            DelUser();
                            break;
                        //停用用户
                        case "stop":
                            StopUser();
                            break;
                        case "start":
                            StartUser();
                            break;
                        case "logout":
                            LogoutUser();
                            break;
                    }
                    Response.End();
                }
                else
                {
                    PageInit();
                }
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            IList<EyouSoft.Model.ComStructure.MComUser> list = null;
            //部门名称
            string departName = Utils.GetQueryStringValue(BelongDepart.SelectNameClient);
            //部门编号
            int departId = Utils.GetInt(Utils.GetQueryStringValue(BelongDepart.SelectIDClient));
            BelongDepart.SectionID = departId.ToString();
            BelongDepart.SectionName = departName;
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.Model.ComStructure.MComUserSearch searchModel = new EyouSoft.Model.ComStructure.MComUserSearch();
            searchModel.ContactName = Utils.GetQueryStringValue("name");
            searchModel.UserName = Utils.GetQueryStringValue("username");
            searchModel.DeptId = departId;
            searchModel.DeptName = departName;
            list = new EyouSoft.BLL.ComStructure.BComUser().GetList(pageIndex, pageSize, ref recordCount, SiteUserInfo.CompanyId, searchModel);
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                ExporPageInfoSelect1.Visible = false;
                this.repList.EmptyText = "<tr><td colspan=\"10\" align=\"center\">未找到相关记录!</td></tr>";
            }
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 用户删除
        /// </summary>
        protected void DelUser()
        {
            //string ids = Utils.GetQueryStringValue("ids").Trim(',');
            //bool result = new EyouSoft.BLL.ComStructure.BComUser().Delete(SiteUserInfo.CompanyId, ids) == 1 ? true : false;
            //if (result)
            //{

            //    Response.Write(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            //}
            //else
            //{
            //    Response.Write(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
            //}

            /*string s = Utils.GetQueryStringValue("ids");
            if (string.IsNullOrEmpty(s)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的用户信息！"));

            string[] items = s.Split(',');
            if (items == null || items.Length == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的用户信息！"));

            var bll = new EyouSoft.BLL.ComStructure.BComUser();

            f1:删除成功的用户数 f2:删除失败的用户数
            int f1 = 0;
            int f2 = 0;

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item)) continue;

                int bllRetCode = bll.Delete(SiteUserInfo.CompanyId, item);

                if (bllRetCode == 1) f1++;
                else f2++;
            }

            if (f1 + f2 == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的用户信息！"));
            if (f1 == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：选择的用户(" + (f1 + f2) + "个)信息不能删除！"));
            if (f2 == 0) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：用户(" + f1 + "个)信息删除成功！"));
            else RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：" + f1 + "个用户信息删除成功，" + f2 + "个用户信息不能删除！"));*/

            string s = Utils.GetQueryStringValue("ids");
            if (string.IsNullOrEmpty(s)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的用户信息！"));

            int bllRetCode = new EyouSoft.BLL.ComStructure.BComUser().Delete(SiteUserInfo.CompanyId, s);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：用户信息已成功删除！"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：用户信息不存在或已删除！"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：默认的管理员账号不能删除！"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：客户的责任销售不能删除！"));
            else if (bllRetCode == -96) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：团队的销售员不能删除！"));
            else if (bllRetCode == -95) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：订单的销售员不能删除！"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }

        /// <summary>
        /// 用户停用
        /// </summary>
        protected void StopUser()
        {
            string ids = Utils.GetQueryStringValue("ids").Trim(',');
            bool result = new EyouSoft.BLL.ComStructure.BComUser().SetUserStatus(ids, SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.UserStatus.已停用);
            if (result)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "停用成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "停用失败!"));
            }
        }

        /// <summary>
        /// 用户启用
        /// </summary>
        protected void StartUser()
        {
            string ids = Utils.GetQueryStringValue("ids").Trim(',');
            bool result = new EyouSoft.BLL.ComStructure.BComUser().SetUserStatus(ids, SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.UserStatus.正常);
            if (result)
            {
                Response.Write(UtilsCommons.AjaxReturnJson("1", "启用成功!"));
            }
            else
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "启用失败!"));
            }
        }

        /// <summary>
        /// 强制下线
        /// </summary>
        protected void LogoutUser()
        {
            try
            {
                string[] userids = Utils.GetQueryStringValue("ids").Split(',');

                if (userids != null && userids.Length > 0)
                {
                    foreach (var userid in userids)
                    {
                        EyouSoft.Security.Membership.UserProvider.Logout(SiteUserInfo.CompanyId, userid);
                    }
                }

                Response.Write(UtilsCommons.AjaxReturnJson("1", "下线成功!"));
            }
            catch
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", "下线失败!"));
            }
        }

        /// <summary>
        /// 获取用户登录状态
        /// </summary>
        /// <param name="onlinestatus"></param>
        /// <returns></returns>
        protected string GetOnlineStatus(string onlinestatus)
        {
            string status = "";
            switch (onlinestatus)
            {
                case "Offline":
                    status = "未登录";
                    break;
                case "Online":
                    status = "已登录";
                    break;
            }
            return status;
        }

        /// 权限控制
        /// </summary>
        private void PowerControl(string dotype)
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_栏目, false);
                return;
            }
            switch (dotype)
            {
                case "start":
                case "stop":
                    {
                        if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改, false);
                            return;
                        }
                        break;
                    }
                case "del":
                    {
                        if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_删除, false);
                            return;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
