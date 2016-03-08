using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SSOStructure;
using System.Web.UI.HtmlControls;
using System.Web;

namespace EyouSoft.Common.Page
{
    /// <summary>
    /// 组团社页面继承类
    /// </summary>
    public class MobilePage : System.Web.UI.Page
    {
        private bool _IsLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _IsLogin;
            }
        }


        private MUserInfo _userInfo = null;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public MUserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
        }

        /// <summary>
        /// 统一金额显示格式
        /// </summary>
        public string ProviderToMoney
        {
            get
            {
                return "zh-cn";
            }
        }

        /// <summary>
        /// 获得金额前缀，如￥,$;
        /// </summary>
        public string ProviderMoneyStr
        {
            get
            {
                return UtilsCommons.GetMoneyString(0, this.ProviderToMoney).Substring(0, 1);
            }
        }

        /// <summary>
        /// 统一日期显示格式(短)
        /// </summary>
        public string ProviderToDate
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }
        /// <summary>
        /// 统一日期显示格式(长)
        /// </summary>
        public string ProviderToDateLong
        {
            get
            {
                return "yyyy-MM-dd HH:mm:ss";
            }
        }

        /// <summary>
        /// 登录页面URL
        /// </summary>
        public static string Url_Login
        {
            get
            {
                return "/m/login.aspx";
            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string weiHuXiaoXi;
            if (EyouSoft.Security.Membership.UserProvider.IsSysWeiHu(out weiHuXiaoXi))
            {
                RCWE(UtilsCommons.AjaxReturnJson("-10000", weiHuXiaoXi));
            }

            //获取页面请求 类型
            string urlType = EyouSoft.Common.Utils.GetQueryStringValue("urltype");

            //初始化用户信息
            MUserInfo userInfo = null;
            _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out userInfo);
            _userInfo = userInfo;

            if (!_IsLogin)//没有登录
            {
                Response.Redirect(Url_Login);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //给页面Title加上 当前用户的公司名称结尾
            if (this.Header != null)
            {
                this.Title += "_" + _userInfo.CompanyName;
            }
        }
        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        public bool CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs permission)
        {
            if (_userInfo == null) return false;
            return _userInfo.Privs.Contains((int)permission);
        }

        /// <summary>
        /// Response.Clear();Response.Write(s);Response.End();
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串。</param>
        protected void RCWE(string s)
        {
            Response.Clear();
            Response.Write(s);
            Response.End();
        }
    }
}