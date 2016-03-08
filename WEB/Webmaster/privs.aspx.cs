//2011-10-13 汪奇志
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.Webmaster
{
    /// <summary>
    /// 子系统权限管理
    /// </summary>
    public partial class _privs : WebmasterPageBase
    {
        string CompanyId = string.Empty;
        string SysId = string.Empty;
        string UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Utils.GetQueryStringValue("cid");
            SysId = Utils.GetQueryStringValue("sysid");
            UserId = Utils.GetQueryStringValue("uid");

            var sysInfo = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(SysId);

            if (sysInfo == null)
            {
                RegisterAlertAndRedirectScript("请求异常。", "systems.aspx");
            }

            ltrSysName.Text = sysInfo.SysName;

            InitPrivs();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            EyouSoft.BLL.SysStructure.BSysMenu bll = new EyouSoft.BLL.SysStructure.BSysMenu();
            var items = bll.GetComMenus(SysId);
            bll = null;
            string script = "var comMenus={0};";

            if (items == null || items.Count < 1)
            {
                this.phUnsetMenuMsg.Visible = true;
                this.phPrivs.Visible = false;
                script = string.Format(script, "[]");
                RegisterScript(script);
                return;
            }
            else
            {
                this.phUnsetMenuMsg.Visible = false;
                this.phPrivs.Visible = true;
            }

            System.Text.StringBuilder html = new System.Text.StringBuilder();
            EyouSoft.BLL.SysStructure.BSys sysbll = new EyouSoft.BLL.SysStructure.BSys();

            foreach (var menu1 in items)
            {
                html.AppendFormat("<div class=\"p1\"><input type=\"checkbox\" id=\"chk_p_1_{1}\" value=\"{1}\" name=\"chk_p_1\" /><label for=\"chk_p_1_{1}\">{0}</label><!--<span class=\"pcode\">[{1}]</span>--></div>", menu1.Name, menu1.MenuId);

                if (menu1.Menu2s == null || menu1.Menu2s.Count < 1) continue;

                html.Append("<div>");

                int i = 0;
                foreach (var menu2 in menu1.Menu2s)
                {
                    html.Append("<ul class=\"p2\">");
                    html.AppendFormat("<li class=\"p2title\"><input type=\"checkbox\" id=\"chk_p_2_{1}\" value=\"{1}\" name=\"chk_p_2_p1v_{2}\" /><label for=\"chk_p_2_{1}\">{0}</label><!--<span class=\"pcode\">[{1}]</span>--></li>", menu2.Name, menu2.MenuId,menu1.MenuId);

                    var privs = sysbll.GetPrivs(menu2.DefaultMenu2Id);

                    if (privs != null && privs.Count > 0)
                    {
                        foreach (var priv in privs)
                        {
                            html.AppendFormat("<li class=\"p3\"><input type=\"checkbox\" id=\"chk_p_3_{1}\" value=\"{1}\" name=\"chk_p_3_p2v_{2}\"  /><label for=\"chk_p_3_{1}\">{0}</label><span class=\"pcode\">[{1}]</span></li>", priv.Name, priv.PrivsId, menu2.MenuId);
                        }
                    }

                    html.Append("</ul>");

                    if (i % 4 == 3)
                    {
                        html.Append("<ul class=\"p2space\"><li></li></ul>");
                    }
                    i++;
                }

                html.Append("<ul class=\"p2space\"><li></li></ul>");
                html.Append("</div>");
            }

            this.ltrPrivs.Text = html.ToString();

            script = string.Format(script, Newtonsoft.Json.JsonConvert.SerializeObject(items));

            RegisterScript(script);
        }
        #endregion

        #region protected members
        /// <summary>
        /// btnSetAdminPrivsBySys_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetSysPrivs_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.SysStructure.MComMenu2Info> privs = new List<EyouSoft.Model.SysStructure.MComMenu2Info>();
            string[] p1s = Utils.GetFormValues("chk_p_1");

            foreach (var p1 in p1s)
            {
                string[] p2s = Utils.GetFormValues("chk_p_2_p1v_" + p1);

                foreach (var p2 in p2s)
                {      
                    EyouSoft.Model.SysStructure.MComMenu2Info privs2 = new EyouSoft.Model.SysStructure.MComMenu2Info();
                    privs2.Privs = new List<EyouSoft.Model.SysStructure.MSysPrivsInfo>();
                    privs2.MenuId = Utils.GetInt(p2);
                    string[] p3s = Utils.GetFormValues("chk_P_3_p2v_" + p2);

                    foreach (var p3 in p3s)
                    {
                        privs2.Privs.Add(new EyouSoft.Model.SysStructure.MSysPrivsInfo()
                        {
                            PrivsId=Utils.GetInt(p3)
                        });
                    }

                    privs.Add(privs2);
                }
            }

            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            if (bll.SetSysPrivs(SysId, privs) == 1)
            {
                RegisterAlertAndRedirectScript("设置子系统权限成功", "systems.aspx");
            }
            else
            {
                RegisterAlertAndReloadScript("设置子系统权限失败");
            }
            bll = null;
        }

        /// <summary>
        /// btnSetAdminPrivsBySys_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetAdminRoleBySys_Click(object sender, EventArgs e)
        {
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            int roleId = bll.GetSysRoleId(CompanyId);
            if (bll.SetRoleBySysPrivs(roleId, SysId) == 1)
            {
                RegisterAlertAndReloadScript("设置管理员角色权限为子系统权限成功");
            }
            else
            {
                RegisterAlertAndReloadScript("设置管理员角色权限为子系统权限失败");
            }
            bll = null;
        }

        /*/// <summary>
        /// btnSetAdminPrivsBySys_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetAdminPrivsBySys_Click(object sender, EventArgs e)
        {
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            if (bll.SetUserBySysPrivs(UserId, SysId) == 1)
            {
                RegisterAlertAndReloadScript("设置管理员账号权限为子系统权限成功");
            }
            else
            {
                RegisterAlertAndReloadScript("设置管理员账号权限为子系统权限失败");
            }
            bll = null;
        }*/

        /// <summary>
        /// btnSetAdminRoleByAdminRole_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetAdminRoleByAdminRole_Click(object sender, EventArgs e)
        {
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            int roleId = bll.GetSysRoleId(CompanyId);

            if (bll.SetUserRole(UserId, roleId))
            {
                RegisterAlertAndReloadScript("设置管理员为管理员角色成功");
            }
            else
            {
                RegisterAlertAndReloadScript("设置管理员为管理员角色成功");
            }
        }
        #endregion
    }
}
