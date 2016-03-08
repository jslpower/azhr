//2011-10-11 汪奇志
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
    /// 子系统一二级栏目修改
    /// </summary>
    public partial class _menuedit : WebmasterPageBase
    {
        string CompanyId = string.Empty;
        string SysId = string.Empty;
        int ComMenu1Id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Utils.GetQueryStringValue("cid");
            SysId = Utils.GetQueryStringValue("sysid");
            ComMenu1Id = Utils.GetInt(Utils.GetQueryStringValue("mid"));

            var sysInfo = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(SysId);

            if (sysInfo == null)
            {
                RegisterAlertAndRedirectScript("请求异常。", "systems.aspx");
            }

            ltrSysName.Text = sysInfo.SysName;

            InitSysMenus();
            InitComMenus();

            RegisterScript(string.Format("var comMenu1Id={0};", ComMenu1Id));
        }

        #region private members
        /// <summary>
        /// init sys default menus
        /// </summary>
        void InitSysMenus()
        {
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            var items = bll.GetMenus();
            bll = null;

            if (items != null && items.Count > 0)
            {
                this.rptMenus.DataSource = items;
                this.rptMenus.DataBind();
            }
        }

        /// <summary>
        /// init company menus
        /// </summary>
        void InitComMenus()
        {
            var bll = new EyouSoft.BLL.SysStructure.BSysMenu();
            var items = bll.GetComMenus(SysId);
            bll = null;

            string script = "var comMenus={0};";

            if (items != null && items.Count > 0)
            {
                script = string.Format(script, Newtonsoft.Json.JsonConvert.SerializeObject(items));
            }
            else
            {
                script = string.Format(script, "[]");
            }

            RegisterScript(script);
        }
        #endregion

        #region protected members
        /// <summary>
        /// rptMenus_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex < 0) return;

            Literal ltr = (Literal)e.Item.FindControl("ltrMenu2s");
            EyouSoft.Model.SysStructure.MSysMenu1Info menu1 = (EyouSoft.Model.SysStructure.MSysMenu1Info)e.Item.DataItem;

            if (ltr != null && menu1 != null && menu1.Menu2s != null && menu1.Menu2s.Count > 0)
            {
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                s.AppendFormat("<ul class=\"m2\">");

                foreach (var menu2 in menu1.Menu2s)
                {
                    s.Append("<li>");
                    s.AppendFormat("<input name=\"chkSysMenu2Id\" type=\"checkbox\" value=\"{0}\" id=\"chkSysMenu2Id_{0}\" />",menu2.MenuId);
                    s.AppendFormat("<input type=\"hidden\" name=\"txtComMenu2Id_{0}\" value=\"0\">",menu2.MenuId);
                    s.AppendFormat("<input type=\"text\" class=\"input_text\" value=\"{0}\" name=\"txtComMenu2Name_{1}\" style=\"width:98px;\" />", menu2.Name, menu2.MenuId);
                    s.AppendFormat("<input type=\"hidden\" value=\"{0}\" name=\"txtSysMenu2Url_{1}\" />", menu2.Url, menu2.MenuId);
                    s.Append("</li>");
                }
                s.Append("</ul>");

                ltr.Text = s.ToString();
            }
        }        

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.SysStructure.MComMenu1Info info = new EyouSoft.Model.SysStructure.MComMenu1Info();

            info.MenuId = ComMenu1Id;
            info.Name = Utils.GetFormValue("txtMenu1Name");
            info.SysId = SysId;
            info.ClassName = Utils.GetFormValue("radClassName");
            info.Menu2s = new List<EyouSoft.Model.SysStructure.MComMenu2Info>();
            info.IsDisplay = Utils.GetFormValue("chkIsDisplay") == "1";

            string[] chkSysMenu2Ids = Utils.GetFormValues("chkSysMenu2Id");

            foreach (var s in chkSysMenu2Ids )
            {
                int sysMenu2Id = Utils.GetInt(s, -1);
                if (sysMenu2Id <= 0) continue;

                EyouSoft.Model.SysStructure.MComMenu2Info item = new EyouSoft.Model.SysStructure.MComMenu2Info();
                item.DefaultMenu2Id = sysMenu2Id;
                item.MenuId = Utils.GetInt(Utils.GetFormValue("txtComMenu2Id_" + sysMenu2Id));
                item.Name = Utils.GetFormValue("txtComMenu2Name_" + sysMenu2Id);
                item.Url = Utils.GetFormValue("txtSysMenu2Url_" + sysMenu2Id);

                info.Menu2s.Add(item);
            }

            //数据未验证

            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            if (info.MenuId > 0)
            {
                if (bll.UpdateSysMenu(info) == 1)
                {
                    RegisterAlertAndRedirectScript("修改成功", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
                }
                else
                {
                    RegisterAlertAndReloadScript("修改失败");
                }
            }
            else
            {
                if (bll.CreateSysMenu(info) == 1)
                {
                    RegisterAlertAndRedirectScript("添加成功", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
                }
                else
                {
                    RegisterAlertAndReloadScript("添加失败");
                }
            }
            bll = null;
        }
        #endregion
    }
}
