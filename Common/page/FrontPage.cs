using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SSOStructure;
using System.Web.UI.HtmlControls;
using System.Web;

namespace EyouSoft.Common.Page
{
    using System.Globalization;
    using System.Resources;
    using System.Threading;

    /// <summary>
    /// 组团社页面继承类
    /// </summary>
    public class FrontPage : System.Web.UI.Page
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
        /// 获取当前用户所在的组团公司ID
        /// </summary>
        public string CurrentUserCompanyID
        {
            get
            {
                return _userInfo.TourCompanyInfo.CompanyId;
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
        /// 语言类型（1为中文,2为英文,3为泰文）
        /// </summary>
        private string _lgtype;
        public string LgType
        {
            get
            {
                switch ((EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(Utils.GetQueryStringValue("LgType")))
                {
                    case EyouSoft.Model.EnumType.SysStructure.LngType.英文:
                        this._lgtype = "EN-US";
                        break;
                    //case EyouSoft.Model.EnumType.SysStructure.LngType.泰文:
                    //    this._lgtype = "TH-TH";
                    //    break;
                    default:
                        this._lgtype = "ZH-CN";
                        break;
                }
                return _lgtype;
            }
            set
            {
                this._lgtype = value;
            }
        }

        protected override void InitializeCulture()
        {
            //UICulture - 决定了采用哪一种本地化资源，也就是使用哪种语言
            //Culture - 决定各种数据类型是如何组织，如数字与日期
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.LgType);
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.LgType);
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
                if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商)
                {
                    RedirectToUrl("/GroupEnd/Suppliers/ProductList.aspx");
                }

                if (_userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.导游 || _userInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.内部员工)
                {
                    RedirectToUrl(Utils.AbsoluteWebRoot.ToString());
                }
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //给页面Title加上 当前用户的公司名称结尾
            if (this.Header != null)
            {
                AddMetaContentType();
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
            meta.Content = "IE=EmulateIE7";
            meta.HttpEquiv = "X-UA-Compatible";
            Page.Header.Controls.Add(meta);
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
        /// 转换成货币字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ToMoneyString(object obj)
        {
            return UtilsCommons.GetMoneyString(obj, this.ProviderToMoney);
        }

        /// <summary>
        /// response to xls
        /// </summary>
        /// <param name="s">要写入 HTTP 输出流的字符串。</param>
        protected void ResponseToXls(string s)
        {
            ResponseToXls(s, System.Text.Encoding.UTF8);
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
    }
}
