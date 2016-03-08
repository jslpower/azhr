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
using EyouSoft.Security.Membership;
using EyouSoft.Common;

namespace Web
{
    /// <summary>
    /// 戴银柱，2011-09-26
    /// 用于用户的 安全退出
    /// </summary>
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string loginUrl = string.Empty;

            //初始化用户信息,根据用户信息获取登录URL
            EyouSoft.Model.SSOStructure.MUserInfo userInfo = EyouSoft.Security.Membership.UserProvider.GetUserInfo();
            string goUrl = "/Login.aspx";
            //if (userInfo != null)
            //{
            //    if (userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.导游 || userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.内部员工)
            //    {
            //        goUrl = "/Login.aspx";
            //    }
            //    if (userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商)
            //    {
            //        goUrl = "/GroupEnd/Suppliers/Login.aspx";
            //    }

            //    if (userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社)
            //    {
            //        goUrl = "/GroupEnd/Distribution/Login.aspx";
            //    }
            //}

            EyouSoft.Security.Membership.UserProvider.Logout();
            Response.Redirect(goUrl);
        }
    }
}
