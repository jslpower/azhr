using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using System.Text;
using EyouSoft.Common;

namespace Web.MasterPage
{
    public partial class UserCenter : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：个人中心 模板页 
        /// <summary>
        /// 当前登录公司编号
        /// </summary>
        protected string CompanyID = string.Empty;
        /// <summary>
        /// 当前登录人编号
        /// </summary>
        protected string UserID = string.Empty;
        /// <summary>
        /// 当前系统操作模式
        /// </summary>
        protected bool IsHandleElse = true;

        protected BackPage backPage = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //初始化backPage
            backPage = this.Page as BackPage;
            this.HeadOrderControl1.SiteUserInfo = backPage.SiteUserInfo;
            this.HeadUserControl1.SiteUserInfo = backPage.SiteUserInfo;
            PowerControl();
            if (!IsPostBack)
            {
                this.CompanyID = backPage.SiteUserInfo.CompanyId;
                this.UserID = backPage.SiteUserInfo.UserId;
                this.IsHandleElse = backPage.SiteUserInfo.IsHandleElse;
                if (backPage.PageType == PageType.general)
                {
                    EyouSoft.Model.SysStructure.MComLocationInfo locationInfo = EyouSoft.Security.Membership.UserProvider.GetLocation();
                    if (locationInfo != null && locationInfo.Menu1Name.Trim() != "" && locationInfo.Menu2Name.Trim() != "")
                    {
                        Page.Title = locationInfo.Menu1Name + "_" + locationInfo.Menu2Name + "_" + backPage.SiteUserInfo.CompanyName;
                        this.litFristLevel.Text = "> " + locationInfo.Menu1Name;
                        this.litSecondLevel.Text = "> <b class=\"fontbsize12\">" + locationInfo.Menu2Name + "</b>";
                    }

                    ltrCompanyName.Text = backPage.SiteUserInfo.CompanyName;
                }
            }
        }
        /// <summary>
        /// 绑定Logo
        /// </summary>
        /// <returns></returns>
        protected string LogoBind()
        {
            string s = string.Empty;
            s += "<div class=\"logo\">";

            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(backPage.SiteUserInfo.CompanyId);

            if (setting != null && !string.IsNullOrEmpty(setting.NLogo))
            {
                s += "<img src=\"" + setting.NLogo + "\" complete=\"complete\"/>";

                setting = null;
            }
            else
            {
                s += "logo";
            }

            s += "</div>";

            return s;
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {

        }

        protected string MenuBind()
        {
            StringBuilder menuSb = new StringBuilder();
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> menuList = new EyouSoft.BLL.SysStructure.BSysMenu().GetComMenus(backPage.SiteUserInfo.SysId);
            if (menuList != null && menuList.Count > 0)
            {
                //遍历一级栏目
                for (int i = 0; i < menuList.Count; i++)
                {
                    if (!menuList[i].IsDisplay) continue;
                    //判断用户是否有一级栏目权限
                    //if (backPage.SiteUserInfo.Privs.Contains(menuList[i].MenuId))
                    //{
                    string menuFrist = "<li><s class=\"" + menuList[i].ClassName + "\"></s><a class=\"hide\" href=\"{1}\">" + menuList[i].Name + "</a>{0}</li>";
                    //string menuFrist = "<span " + (i == (menuList.Count - 1) ? "style='border-bottom:#137CBF solid 1px; display:block;'" : "") + " ><h5><s class=\"" + menuList[i].ClassName + "\"></s><a onclick=\"show_menuB(" + i.ToString() + ")\" href=\"javascript:;\" id=\"Bf0" + i.ToString() + "\">" + menuList[i].Name + "</a></h5>{0}</span>";
                    //用来验证是否拥有一级栏目权限
                    bool isGrant = false;
                    string menuSecond = "";
                    //判断是否拥有二级栏目
                    if (menuList[i].Menu2s != null)
                    {
                        menuSecond += "<dl>";
                        //menuSecond += "<div id=\"Bli0" + i.ToString() + "\" class=\"sub-menu\"><ul>";
                        for (int j = 0; j < menuList[i].Menu2s.Count; j++)
                        {
                            //判断是否拥有二级栏目权限
                            if (backPage.SiteUserInfo.Privs.Contains(menuList[i].Menu2s[j].Menu2PrivId))
                            {
                                //拥有二级栏目权限时即显示一级栏目
                                isGrant = true;
                                menuSecond += "<dd><a href=\"" + menuList[i].Menu2s[j].Url + "\">" + menuList[i].Menu2s[j].Name + "</a></dd>";
                                //menuSecond += "<li ><a href=\"" + menuList[i].Menu2s[j].Url + "\">" + menuList[i].Menu2s[j].Name + "</a></li>";
                            }
                        }
                        menuSecond += "</dl>";
                        //menuSecond += "</ul></div>";
                    }
                    if (isGrant)
                    {
                        menuFrist = string.Format(menuFrist,menuList[i].Menu2s != null && menuList[i].Menu2s.Count == 1 ? "": menuSecond,menuList[i].Menu2s != null && menuList[i].Menu2s.Count == 1 ? menuList[i].Menu2s[0].Url : "#");
                        menuSb.Append(menuFrist);
                    }

                    //  }
                }
            }
            return menuSb.ToString();
        }
    }
}
