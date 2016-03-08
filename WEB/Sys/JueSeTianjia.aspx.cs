using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.Sys
{
    /// <summary>
    /// 权限组配置
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class JueSeTianjia : BackPage
    {
        //权限html
        protected string PowerStr = "";
        protected bool IsAddOrUpdatePrivs = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            else if (Utils.GetQueryStringValue("dotype") == "copy")
            {
                Save();
            }
            else
            {
                PageInit();
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            int RoleId = Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("id"));
            //当前角色的所有权限ID
            string[] PowerIds = { };
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.Text.StringBuilder Privs = null;
            EyouSoft.BLL.SysStructure.BSysMenu BSysMenuBll = new EyouSoft.BLL.SysStructure.BSysMenu();
            EyouSoft.BLL.ComStructure.BComRole BComRoleBll = new EyouSoft.BLL.ComStructure.BComRole();
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> list = BSysMenuBll.GetComMenus(SiteUserInfo.SysId);
            if (RoleId != 0)
            {
                EyouSoft.Model.ComStructure.MComRole model = BComRoleBll.GetModel(RoleId, SiteUserInfo.CompanyId);
                if (model != null)
                {
                    txtRoleName.Value = model.RoleName;
                    PowerIds = model.RoleChilds.Split(',');
                }
            }
            else
            {
                txtRoleName.Value = "";
            }
            if (list != null)
            {
                //一级栏目
                for (int i = 0; i < list.Count; i++)
                {
                    str.Append("<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\" class=\"quanxian_add_bottom\"> <tbody><tr>");
                    str.AppendFormat(" <td width=\"92\" height=\"25\" align=\"center\" class=\"quanxian_add\">{0}</td>", list[i].Name);
                    str.AppendFormat("<td><label><input type=\"checkbox\" name=\"chkAll\" value=\"{0}\"/>&nbsp;&nbsp;全选</label></td>", list[i].MenuId);
                    str.Append(" </tr></tbody></table>");
                    str.Append("<div class=\"hr_5\"></div>");
                    str.Append(" <table width=\"100%\" cellspacing=\"1\" cellpadding=\"0\" border=\"0\"  align=\"center\" bgcolor=\"#85c1dd\"><tbody>");
                    if (list[i].Menu2s != null)
                    {
                        //每行显示4列,共循环多少次
                        for (int k = 0; k <= list[i].Menu2s.Count / 4; k++)
                        {
                            Privs = new System.Text.StringBuilder();
                            str.Append("<tr>");
                            Privs.Append("<tr>");
                            for (int j = k * 4; j < list[i].Menu2s.Count; j++)
                            {
                                //当前权限是否选中
                                bool check = false;
                                str.AppendFormat("<th width=\"25%\" height=\"26\" bgcolor=\"#BDDCF4\" align=\"left\"><label>&nbsp;&nbsp;<input type=\"checkbox\" MenuId =\"{0}\" Menu2Id=\"{1}\" name=\"chkMenu\" />&nbsp;{2}</label></th>", list[i].MenuId, list[i].Menu2s[j].MenuId, list[i].Menu2s[j].Name);
                                Privs.Append("<td align='left' style=\" vertical-align:top;\" bgcolor=\"#FFFFFF\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
                                for (int t = 0; t < list[i].Menu2s[j].Privs.Count; t++)
                                {
                                    for (int p = 0; p < PowerIds.Length; p++)
                                    {
                                        check = false;
                                        if (Utils.GetInt(PowerIds[p]) == list[i].Menu2s[j].Privs[t].PrivsId)
                                        {
                                            check = true;
                                            break;
                                        }
                                    }
                                    Privs.AppendFormat("<tr><td width=\"25%\" height=\"26\" bgcolor=\"#FFFFFF\" align=\"left\"><label>&nbsp;&nbsp;<input type=\"checkbox\"  name=\"chkPower\" Menu2Id=\"{0}\" value=\"{1}\" {2}/>&nbsp;&nbsp;{3}</label></td></tr>", list[i].Menu2s[j].MenuId, list[i].Menu2s[j].Privs[t].PrivsId, check ? "checked=\"checked\"" : "", list[i].Menu2s[j].Privs[t].Name);
                                }
                                Privs.Append("</table></td>");
                                if ((j != 0 && (j + 1) % 4 == 0) || j + 1 == list[i].Menu2s.Count)
                                {
                                    int AddThTd = 4 - list[i].Menu2s.Count % 4;
                                    if (AddThTd > 0 && list[i].Menu2s.Count > 4)
                                    {
                                        for (int s = 0; s < AddThTd; s++)
                                        {
                                            str.Append("<th width=\"25%\" height=\"26\" bgcolor=\"#BDDCF4\" align=\"left\"></th>");
                                            Privs.Append("<td width=\"25%\" height=\"26\" bgcolor=\"#FFFFFF\" align=\"left\"></td>");
                                        }
                                    }
                                    str.Append("</tr>");
                                    Privs.Append("<tr>");
                                    str.Append(Privs.ToString());
                                    break;
                                }
                            }
                        }
                    }
                    str.Append("</tbody></table><div class=\"hr_5\"></div>");
                    PowerStr = str.ToString();
                }
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_角色管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_角色管理_栏目, false);
                return;
            }
            if (Utils.GetQueryStringValue("state") == "copy" || Utils.GetQueryStringValue("id") == "")
            {
                if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_新增))
                {
                    IsAddOrUpdatePrivs = true;
                }
            }
            else
            {
                if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_用户管理_修改))
                {
                    IsAddOrUpdatePrivs = true;
                }
            }
        }

        protected void Save()
        {
            int Result = 0;
            string dotype = Utils.GetQueryStringValue("dotype");
            EyouSoft.Model.ComStructure.MComRole model = new EyouSoft.Model.ComStructure.MComRole();
            EyouSoft.BLL.ComStructure.BComRole BComRoleBll = new EyouSoft.BLL.ComStructure.BComRole();
            string RoleId = Utils.GetQueryStringValue("id");
            int length = Utils.GetFormValues("chkPower").Length;
            for (int i = 0; i < length; i++)
            {
                model.RoleChilds += Utils.GetFormValues("chkPower")[i] + ",";
            }
            if (!string.IsNullOrEmpty(model.RoleChilds))
            {
                model.RoleChilds = model.RoleChilds.Trim(',');
            }
            string msg = string.Empty;
            if (model.RoleChilds == "")
            {
                msg += "请选择权限!<br/>";
            }
            if (txtRoleName.Value == "")
            {
                msg += "权限组名称不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (RoleId == "" || dotype == "copy")
            {
                model.CompanyId = SiteUserInfo.CompanyId;
                model.RoleName = txtRoleName.Value;
                Response.Clear();
                Result = BComRoleBll.Add(model);
                switch (Result)
                {
                    case 0:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败!"));
                            break;
                        }
                    case 1:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功!"));
                            break;
                        }
                    case 2:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("0", "角色名重复!"));
                            break;
                        }
                }
            }
            else
            {
                model.Id = EyouSoft.Common.Utils.GetInt(RoleId);
                model.CompanyId = SiteUserInfo.CompanyId;
                model.RoleName = txtRoleName.Value;
                Result = BComRoleBll.Update(model);
                switch (Result)
                {
                    case 0:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败!"));
                            break;
                        }
                    case 1:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功!"));
                            break;
                        }
                    case 2:
                        {
                            Response.Write(UtilsCommons.AjaxReturnJson("0", "角色名重复!"));
                            break;
                        }
                }
            }
            Response.End();
        }
    }
}
