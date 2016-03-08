using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Security.Membership;
using System.Web.UI.HtmlControls;
using EyouSoft.Model.SSOStructure;
using System.Web;

namespace EyouSoft.Common.Page
{
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// page type
    /// </summary>
    public enum PageType
    {
        /// <summary>
        /// general page
        /// </summary>
        general,
        /// <summary>
        /// boxy page
        /// </summary>
        boxyPage
    }

    /// <summary>
    /// back page base
    /// </summary>
    public class BackPage : System.Web.UI.Page
    {

        /// <summary>
        /// 设置页面类型
        /// </summary>
        public PageType PageType = PageType.general;

        /// <summary>
        /// 页面请求类型，是浏览器正常请求还是Ajax请求
        /// </summary>
        private bool isAjaxConnect = false;
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
        /// 获取当前用户所在的公司ID
        /// </summary>
        public string CurrentUserCompanyID
        {
            get
            {
                return _userInfo.CompanyId;
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
        /// <summary>
        /// 快速登录页面URL
        /// </summary>
        public static string Url_MinLogin
        {
            get
            {
                return "/slogin.aspx";
            }
        }
        /// <summary>
        /// 统一金额显示格式
        /// </summary>
        public string ProviderToMoney
        {
            get
            {
                return "en-us";
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
        /// 分销商、供应商、内部 共用页面 1.保险设置
        /// </summary>
        private string[] CommomPage = new[] { "/sellcenter/order/orderinsurance.aspx", "/importsource/importpage.aspx", "/commonpage/setseat.aspx", "/usercenter/notice/noticeinfo.aspx" };

        string _SL = string.Empty;
        public string SL { get { return _SL; } }

        protected override void InitializeCulture()
        {
            //UICulture - 决定了采用哪一种本地化资源，也就是使用哪种语言
            //Culture - 决定各种数据类型是如何组织，如数字与日期
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.LgType);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.ProviderToMoney);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _SL = Utils.GetQueryStringValue("sl");            

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

            string weiHuXiaoXi;
            if (EyouSoft.Security.Membership.UserProvider.IsSysWeiHu(out weiHuXiaoXi) && _IsLogin)
            {
                RCWE(UtilsCommons.AjaxReturnJson("-10000", weiHuXiaoXi));
            }

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
                    if (this.PageType == PageType.general)
                    {
                        RedirectLogin(Request.Url.ToString());
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("if(window.parent.Boxy==undefined||window.parent.Boxy==null){");
                        Response.Write("window.location.href='" + Url_Login + "';");
                        Response.Write("}else{");
                        Response.Write("window.location.href='" + Url_MinLogin + "?returnurl=" + Server.UrlEncode(Request.Url.ToString()) + "';");
                        Response.Write("}");
                        Response.Write("</script>");
                        Response.End();
                    }
                }
            }
            else//已登录
            {
                if (!CommomPage.Contains(Request.Url.AbsolutePath.ToLower()))
                {
                    if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商)
                    {
                        HttpContext.Current.Response.Redirect("/GroupEnd/Suppliers/ProductList.aspx", true);
                    }
                    if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社)
                    {
                        HttpContext.Current.Response.Redirect(Utils.AbsoluteWebRoot.ToString(), true);
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //给页面Title加上 当前用户的公司名称结尾
            if (this.Header != null && PageType == PageType.general)
            {
                AddMetaContentType();
            }

            if (Header != null && PageType == PageType.boxyPage)//重写弹窗页面样式
            {
                System.Web.UI.WebControls.Style style = new System.Web.UI.WebControls.Style();
                style.BackColor = System.Drawing.Color.FromArgb(0xe9f4f9);
                Header.StyleSheet.CreateStyleRule(style, null, "body,html");
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
        /// 添加Content-Type Meta标记到页面头部
        /// </summary>
        protected virtual void AddMetaContentType()
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes["charset"] = Response.ContentEncoding.HeaderName;
            meta.Content = "IE=EmulateIE7";
            meta.HttpEquiv = "X-UA-Compatible";
            Page.Header.Controls.Add(meta);
        }

        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        public static void RedirectLogin()
        {
            HttpContext.Current.Response.Redirect(Url_Login, true);
        }
        /// <summary>
        /// 跳转到登录页面，并指定登录成功后要跳转的页面地址，及在登录页面要显示的信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        public static void RedirectLogin(string url, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                HttpContext.Current.Response.Redirect(Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url) +
                    "&nlm=" + HttpContext.Current.Server.UrlEncode(message) + "&isShow=1");
            }
            else
            {
                RedirectLogin(url);
            }
        }
        /// <summary>
        /// 跳转到登录页面，并指定登录成功后要跳转的页面地址
        /// </summary>
        /// <param name="url">指定登录成功后要跳转的页面地址</param>
        public static void RedirectLogin(string url)
        {
            HttpContext.Current.Response.Redirect(Url_Login + "?returnurl=" + HttpContext.Current.Server.UrlEncode(url));
        }

        /// <summary>
        /// response to xls
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串。</param>
        protected void ResponseToXls(string s)
        {
            ResponseToXls(s, System.Text.Encoding.Default);
        }

        /// <summary>
        /// response to xls
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串。</param>
        /// <param name="encoding">encoding</param>
        protected void ResponseToXls(string s, Encoding encoding)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            ResponseToXls(s, encoding, filename);
        }

        /// <summary>
        /// response to xls
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串。</param>
        /// <param name="encoding">encoding</param>
        /// <param name="filename">filename</param>
        protected void ResponseToXls(string s, Encoding encoding, string filename)
        {
            if (string.IsNullOrEmpty(filename)) filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            if (System.IO.Path.GetExtension(filename).ToLower() != ".xls") filename += ".xls";

            Response.Clear();
            Response.Charset = "utf-8";
            Response.ContentEncoding = encoding;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = "application/ms-excel;charset=utf-8";
            Response.Write(s.ToString());
            Response.End();
        }


        /// <summary>
        /// ajax request response,Response.Clear();Response.Write(s);Response.End();
        /// </summary>
        /// <param name="s">输出字符串</param>
        protected void AjaxResponse(string s)
        {
            RCWE(s);
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

        /// <summary>
        /// register alert script
        /// </summary>
        /// <param name="s">msg</param>
        protected void RegisterAlertScript(string s)
        {
            this.RegisterScript(string.Format("alert('{0}');", s));
        }

        /// <summary>
        /// register alert and redirect script
        /// </summary>
        /// <param name="s"></param>
        /// <param name="url">IsNullOrEmpty(url)=true page reload</param>
        protected void RegisterAlertAndRedirectScript(string s, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                this.RegisterScript(string.Format("alert('{0}');window.location.href='{1}';", s, url));
            }
            else
            {
                this.RegisterScript(string.Format("alert('{0}');window.location.href=window.location.href;", s));
            }
        }

        /// <summary>
        /// register alert and reload script
        /// </summary>
        /// <param name="s"></param>
        protected void RegisterAlertAndReloadScript(string s)
        {
            RegisterAlertAndRedirectScript(s, null);
        }

        /// <summary>
        /// register scripts
        /// </summary>
        /// <param name="script"></param>
        protected void RegisterScript(string script)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// 转换成货币字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ToMoneyString(object obj)
        {
            return UtilsCommons.GetMoneyString(obj, this.ProviderToMoney);
        }

        /// <summary>
        /// 转换成日期字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ToDateTimeString(object obj)
        {
            return UtilsCommons.GetDateString(obj, "yyyy-MM-dd");
        }
    }
}

