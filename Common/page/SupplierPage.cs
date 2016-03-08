using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.Common.Page
{
    /// <summary>
    /// 供应商页面继承类
    /// </summary>
    public class SupplierPage : System.Web.UI.Page
    {
        /// <summary>
        /// 设置页面类型
        /// </summary>
        public PageType PageType = PageType.general;

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

        /// <summary>
        /// 页面请求类型，是浏览器正常请求还是Ajax请求
        /// </summary>
        private bool isAjaxConnect = false;

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
        /// 获取当前用户所在的组团公司ID
        /// </summary>
        public string CurrentUserCompanyID
        {
            get
            {
                return _userInfo.SourceCompanyInfo.CompanyId;
            }
        }
        /// <summary>
        /// 登录页面URL
        /// </summary>
        public static string Url_Login
        {
            get
            {
                return "/login.aspx";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //获取页面请求 类型
            string urlType = EyouSoft.Common.Utils.GetQueryStringValue("urltype");
            if (urlType == "pageajax")
            {
                isAjaxConnect = true;
            }

            //初始化用户信息
            MUserInfo userInfo = null;
            _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out userInfo);
            _userInfo = userInfo;

            if (!_IsLogin)//没有登录
            {
                //判断页面请求类型
                if (isAjaxConnect)//是Ajax请求
                {
                    Response.Clear();
                    Response.Write("{Islogin:false}");
                    Response.End();
                }
                else//普通浏览器请求
                {
                    RedirectToUrl(Url_Login);
                }
            }
            else//已登录
            {
                if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社)
                {
                    RedirectToUrl("/GroupEnd/Distribution/AcceptPlan.aspx");
                }

                if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.导游 || _userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.内部员工)
                {
                    RedirectToUrl("/Default.aspx");
                }
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
        /// 添加Content-Type Meta标记到页面头部
        /// </summary>
        protected virtual void AddMetaContentType()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes["charset"] = Response.ContentEncoding.HeaderName;
        }
        /// <summary>
        /// 跳转到登录页面，并指定登录成功后要跳转的页面地址
        /// </summary>
        /// <param name="url">指定登录成功后要跳转的页面地址</param>
        public static void RedirectToUrl(string url)
        {
            string script = "<script type='text/javascript'>window.top.location.href ='" + url + "';</script> ";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(script);
            HttpContext.Current.Response.End();
        }
    }
}
