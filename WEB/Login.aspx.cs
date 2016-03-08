using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Common;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-7
        /// 说明：网站主登录页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        private void PageInit()
        {
            EyouSoft.Model.SysStructure.MSysDomain sysDomain = EyouSoft.Security.Membership.UserProvider.GetDomain();
            
            if (sysDomain != null)
            {
                var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(sysDomain.CompanyId);
                if (setting != null)
                {
                    //litWelcome.Text = "<div class=\"login-welcome\">您好！" + setting.CompanyName + ", 现在是: " + DateTime.Now.GetDateTimeFormats('D')[3].ToString() + " 系统首页</div>";
                    this.Page.Title = "登录_旅游管理系统_" + setting.CompanyName;
                    if (!string.IsNullOrEmpty(setting.WLogo) && setting.WLogo.Trim() != "")
                    {
                        this.litLogo.Text = "<img src='" + setting.WLogo + "'>";
                    }
                    else
                    {
                        this.litLogo.Text = "<img src='/images/logo_e.gif'>";
                    }

                    setting = null;
                }

                //if (sysDomain.Domain == "xz.gocn.cn" || sysDomain.Domain == "local.xz.com")
                //{
                //    ltrTiShi.Text = "<span style=\"color:#ffffff\">2013-03-01日起峡州国旅ERP系统正式启用，请大家录入实团信息，如录入中有疑问请电询18608601188.</span>";
                //}
            }
            else {
                //litWelcome.Text = "<div class=\"login-welcome\">您好！, 现在是: " + DateTime.Now.GetDateTimeFormats('D')[3].ToString() + " 系统首页</div>";
                this.litLogo.Text = "<img src='/images/logo_e.gif'>";
            }

            //#region 根据类型跳转相应登录页面
            //string type = "";
            //type = Utils.GetQueryStringValue("type");
            //switch (type)
            //{
            //    case "1":
            //        litLeft.Text = "<a href=\"/login.aspx?type=3\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-xitong-cy.gif\"></a>";
            //        litRight.Text = "<a href=\"/login.aspx?type=2\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-fenxiao-cy.gif\"></a>";
            //        break;
            //    case "2":
            //        litLeft.Text = "<a href=\"/login.aspx?type=1\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-gongying-cy.gif\"></a>";
            //        litRight.Text = "<a href=\"/login.aspx?type=3\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-xitong-cy.gif\"></a>";
            //        break;
            //    case "3":
            //        litLeft.Text = "<a href=\"/login.aspx?type=1\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-gongying-cy.gif\"></a>";
            //        litRight.Text = "<a href=\"/login.aspx?type=2\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-fenxiao-cy.gif\"></a>";
            //        break;
            //    default:
            //        litLeft.Text = "<a href=\"/login.aspx?type=1\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-gongying-cy.gif\"></a>";
            //        litRight.Text = "<a href=\"/login.aspx?type=2\">"
            //            + "<img border=\"0\" style=\"vertical-align: middle\" src=\"/images/login-fenxiao-cy.gif\"></a>";
            //        break;
            //}
            //#endregion

        }
    }
}
