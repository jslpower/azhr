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
    /// 子系统一二级栏目管理
    /// </summary>
    public partial class _menu : WebmasterPageBase
    {
        protected string CompanyId = string.Empty;
        protected string SysId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyId = Utils.GetQueryStringValue("cid");
            SysId=Utils.GetQueryStringValue("sysid");

            if (Utils.GetInt(Utils.GetQueryStringValue("ot"), 0) == 1)//删除一级栏目
            {
                DeleteComMenu1();
                return;
            }

            var sysInfo = new EyouSoft.BLL.SysStructure.BSys().GetSysInfo(SysId);

            if (sysInfo == null)
            {
                RegisterAlertAndRedirectScript("请求异常。", "systems.aspx");
            }

            ltrSysName.Text = sysInfo.SysName;

            InitComMenus();
        }

        #region private members
        /// <summary>
        /// init company menus
        /// </summary>
        void InitComMenus()
        {
            EyouSoft.BLL.SysStructure.BSysMenu bll = new EyouSoft.BLL.SysStructure.BSysMenu();
            var items = bll.GetComMenus(SysId);
            bll = null;

            if (items != null && items.Count > 0)
            {
                this.phSetDefaultMenus.Visible = false;

                this.rptMenus.DataSource = items;
                this.rptMenus.DataBind();
            }
            else
            {
                this.phSetDefaultMenus.Visible = true;
            }
        }

        /// <summary>
        /// delete company menu 1
        /// </summary>
        void DeleteComMenu1()
        {
            int menu1Id = Utils.GetInt(Utils.GetQueryStringValue("mid"));
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            if (bll.DeleteSysMenu1(SysId, menu1Id) == 1)
            {
                RegisterAlertAndRedirectScript("删除成功", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
            }
            else
            {
                RegisterAlertAndRedirectScript("删除失败", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
            }
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
            EyouSoft.Model.SysStructure.MComMenu1Info menu1 = (EyouSoft.Model.SysStructure.MComMenu1Info)e.Item.DataItem;

            if (ltr != null && menu1 != null && menu1.Menu2s != null && menu1.Menu2s.Count > 0)
            {
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                s.AppendFormat("<ul class=\"m2\">");

                foreach (var menu2 in menu1.Menu2s)
                {
                    s.AppendFormat("<li>{0}</li>", menu2.Name);
                }
                s.Append("</ul>");

                ltr.Text = s.ToString();
            }
        }

        /// <summary>
        /// btnSetDefaultMenus_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetDefaultMenus_Click(object sender, EventArgs e)
        {
            EyouSoft.BLL.SysStructure.BSys bll = new EyouSoft.BLL.SysStructure.BSys();
            if (bll.SetMenuBySys(SysId) == 1)
            {
                RegisterAlertAndRedirectScript("设置成功", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
            }
            else
            {
                RegisterAlertAndRedirectScript("设置失败", string.Format("menu.aspx?sysid={0}&cid={1}", SysId, CompanyId));
            }
            bll = null;
        }
        #endregion
    }
}
