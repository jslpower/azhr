//2011-09-23 汪奇志
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.SysStructure;
using EyouSoft.Cache.Facade;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 系统用户登录处理类
    /// </summary>
    public sealed class UserProvider
    {
        #region static constants
        //static constants
        /// <summary>
        /// 登录Cookie，用户编号
        /// </summary>
        public const string LoginCookieUserId = "SYS_UID";
        /// <summary>
        /// 登录Cookie，用户账号
        /// </summary>
        public const string LoginCookieUsername = "SYS_UN";
        /// <summary>
        /// 登录Cookie，公司编号
        /// </summary>
        public const string LoginCookieCompanyId = "SYS_CID";
        /// <summary>
        /// 登录Cookie，系统编号
        /// </summary>
        public const string LoginCookieSysId = "SYS_SID";
        /// <summary>
        /// 登录Cookie，客服登录
        /// </summary>
        public const string LoginCookieKeFu = "SYS_KF";
        /// <summary>
        /// 登录Cookie，会话标识
        /// </summary>
        public const string LoginCookieSessionId = "SYS_SESSIONID";
        #endregion

        #region private members
        /// <summary>
        /// 设置登录用户cache
        /// </summary>
        /// <param name="info">登录用户信息</param>
        private static void SetUserCache(MUserInfo info)
        {
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComUser, info.CompanyId, info.UserId);
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
            EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, info, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 移除登录用户cache
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        private static void RemoveUserCache(string companyId, string userId)
        {
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComUser, companyId, userId);

            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
        }

        /// <summary>
        /// 获取登录用户Cookie信息
        /// </summary>
        /// <param name="name">登录Cookie名称</param>
        /// <returns></returns>
        private static string GetCookie(string name)
        {
            HttpRequest request = HttpContext.Current.Request;

            if (request.Cookies[name] == null)
            {
                return string.Empty;
            }

            return request.Cookies[name].Value;
        }

        /// <summary>
        /// 移除登录用户Cookie
        /// </summary>
        private static void RemoveCookies()
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Cookies.Remove(LoginCookieCompanyId);
            response.Cookies.Remove(LoginCookieSysId);
            response.Cookies.Remove(LoginCookieUserId);
            response.Cookies.Remove(LoginCookieUsername);
            response.Cookies.Remove(LoginCookieSessionId);
            response.Cookies.Remove(LoginCookieKeFu);

            DateTime cookiesExpiresDateTime = DateTime.Now.AddDays(-1);

            response.Cookies[LoginCookieCompanyId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieSysId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieUserId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieUsername].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieSessionId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieKeFu].Expires = cookiesExpiresDateTime;
        }

        /// <summary>
        /// 设置登录Cookies
        /// </summary>
        /// <param name="info">登录用户信息</param>
        private static void SetCookies(MUserInfo info)
        {
            //Cookies生存周期为浏览器进程
            HttpResponse response = HttpContext.Current.Response;

            RemoveCookies();

            System.Web.HttpCookie cookie = new HttpCookie(LoginCookieCompanyId);
            cookie.Value = info.CompanyId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieSysId);
            cookie.Value = info.SysId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieUserId);
            cookie.Value = info.UserId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieUsername);
            cookie.Value = info.Username;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieSessionId);
            cookie.Value = info.OnlineSessionId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);
        }

        /// <summary>
        /// 设置客服登录Cookies
        /// </summary>
        private static void SetKeFuLoginCookies()
        {
            HttpResponse response = HttpContext.Current.Response;

            System.Web.HttpCookie cookie = new HttpCookie(LoginCookieKeFu);
            cookie.Value = "Y";
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

        }

        /// <summary>
        /// 自动登录处理
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="username">用户账号</param>  
        /// <param name="uInfo">登录用户信息</param>
        private static void AutoLogin(string sysId, string companyId, string userId, string username, out MUserInfo uInfo)
        {
            uInfo = null;
            IUserLogin dal = new DUserLogin();
            MSysDomain domainInfo = GetDomain();
            if (domainInfo == null || domainInfo.SysId != sysId || domainInfo.CompanyId != companyId) { uInfo = null; return; }

            uInfo = dal.Login(userId);

            if (uInfo == null) return;
            if (uInfo.Username != username) { uInfo = null; return; }
            if (uInfo.CompanyId != companyId) { uInfo = null; return; }
            if (uInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商 && uInfo.SourceCompanyInfo == null) { uInfo = null; return; }
            if (uInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社 && uInfo.TourCompanyInfo == null) { uInfo = null; return; }
            if (uInfo.Status != EyouSoft.Model.EnumType.ComStructure.UserStatus.正常) { uInfo = null; return; }

            uInfo.SysId = sysId;
            uInfo.LoginTime = uInfo.LastLoginTime.HasValue ? uInfo.LastLoginTime.Value : DateTime.Now;

            dal.LoginLogwr(uInfo, EyouSoft.Model.EnumType.ComStructure.UserLoginType.自动登录);

            SetUserCache(uInfo);
        }

        /// <summary>
        /// 递归处理部门所包含的子部门
        /// </summary>
        /// <param name="items">公司组织机构集合</param>
        /// <param name="deptId">部门编号</param>
        /// <param name="depts">下级部门集合</param>
        private static void CTEDeptChildrens(List<EyouSoft.Model.ComStructure.MCacheDeptInfo> items, int deptId, IList<int> depts)
        {
            foreach (var item in items)
            {
                if (item.ParentId == deptId)
                {
                    depts.Add(item.DeptId);

                    if (item.HasChildren)
                    {
                        CTEDeptChildrens(items, item.DeptId, depts);
                    }
                }
            }
        }

        /// <summary>
        /// 递归处理部门所在一级部门，返回部门所在的一级部门编号
        /// </summary>
        /// <param name="items">公司组织机构集合</param>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        private static int CTEDeptFirstId(List<EyouSoft.Model.ComStructure.MCacheDeptInfo> items, int deptId)
        {
            foreach (var item in items)
            {
                if (item.DeptId == deptId)
                {
                    if (item.ParentId == 0) return item.DeptId;
                    return CTEDeptFirstId(items, item.ParentId);
                }
            }

            return 0;
        }

        /// <summary>
        /// 是否客服登录
        /// </summary>
        /// <returns></returns>
        private static bool IsKeFuLogin()
        {
            return GetCookie(LoginCookieKeFu) == "Y";
        }

        /// <summary>
        /// 根据用户类型判断用户登录是否做登录限制
        /// </summary>
        /// <param name="userType">用户类型</param>
        /// <returns></returns>
        private static bool IsLoginLimit(EyouSoft.Model.EnumType.ComStructure.UserType userType)
        {
            return userType == EyouSoft.Model.EnumType.ComStructure.UserType.导游 || userType == EyouSoft.Model.EnumType.ComStructure.UserType.内部员工;
        }
        #endregion

        #region public members
        /// <summary>
        /// 用户登录，返回1登录成功
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwdInfo">登录密码</param>
        /// <param name="uInfo">登录用户信息</param>
        /// <returns></returns>
        public static int Login(string companyId, string username, MPasswordInfo pwdInfo, out MUserInfo uInfo)
        {
            IUserLogin dal = new DUserLogin();
            uInfo = null;

            if (string.IsNullOrEmpty(companyId)) return 0;
            if (string.IsNullOrEmpty(username)) return -1;
            if (pwdInfo == null || string.IsNullOrEmpty(pwdInfo.NoEncryptPassword)) return -2;
            MSysDomain domainInfo = GetDomain();
            if (domainInfo == null) return -3;

            uInfo = dal.Login(companyId, username, pwdInfo);

            //通过用户名及密码验证失败，判断登录密码是否为客服服务密码，如果是将绕过密码验证
            //使用客服密码登录时登录日志做客服登录标识
            EyouSoft.Model.EnumType.ComStructure.UserLoginType loginType = EyouSoft.Model.EnumType.ComStructure.UserLoginType.用户登录;
            if (uInfo == null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["KeFuPwd"] == pwdInfo.MD5Password)
                {
                    uInfo = dal.Login(companyId, username);
                    loginType = EyouSoft.Model.EnumType.ComStructure.UserLoginType.客服登录;
                }
                ////是否公共帐号登录
                //if (System.Configuration.ConfigurationManager.AppSettings["PublicUnm"] == username && System.Configuration.ConfigurationManager.AppSettings["PublicPwd"] == pwdInfo.MD5Password)
                //{
                //    uInfo = dal.Login(companyId, username);
                //    loginType = EyouSoft.Model.EnumType.ComStructure.UserLoginType.公共登录;
                //}

                if (uInfo == null) return -4;
            }

            if (uInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商 && uInfo.SourceCompanyInfo == null)
            {
                uInfo = null;
                return -5;
            }

            if (uInfo.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社 && uInfo.TourCompanyInfo == null)
            {
                uInfo = null;
                return -6;
            }

            if (uInfo.Status != EyouSoft.Model.EnumType.ComStructure.UserStatus.正常)
            {
                uInfo = null;
                return -7;
            }

            var setting = GetComSetting(companyId);

            if (IsLoginLimit(uInfo.UserType))
            {
                switch (setting.UserLoginLimitType)
                {
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.None: break;
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.Earliest:
                        if (loginType == EyouSoft.Model.EnumType.ComStructure.UserLoginType.用户登录
                            && uInfo.OnlineStatus == EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Online)
                        {
                            uInfo = null;
                            return -8;
                        }
                        break;
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.Latest: break;
                    default: break;
                }
            }

            uInfo.SysId = domainInfo.SysId;
            uInfo.LoginTime = DateTime.Now;

            if (loginType == EyouSoft.Model.EnumType.ComStructure.UserLoginType.用户登录)
            {
                uInfo.OnlineStatus = EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Online;
                uInfo.OnlineSessionId = Guid.NewGuid().ToString();
            }

            dal.LoginLogwr(uInfo, loginType);

            SetUserCache(uInfo);
            SetCookies(uInfo);
            if (loginType == EyouSoft.Model.EnumType.ComStructure.UserLoginType.客服登录)
            {
                SetKeFuLoginCookies();
            }

            return 1;
        }

        /// <summary>
        /// 获取当前域名信息
        /// </summary>
        /// <returns></returns>
        public static MSysDomain GetDomain()
        {
            string s = System.Web.HttpContext.Current.Request.Url.Host.ToLower();

            /*//单域名缓存
            MSysDomain info = (MSysDomain)EyouSoftCache.GetCache(string.Format(EyouSoft.Cache.Tag.TagName.SysDomain, s));            

            if (info == null)
            {
                IUserLogin dal = new DUserLogin();
                info = dal.GetDomain(s);

                if (info != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.Cache.Tag.TagName.SysDomain, s), info, DateTime.Now.AddHours(2));
                }
            }

            return info;*/

            IDictionary<string, MSysDomain> domains = (IDictionary<string, MSysDomain>)EyouSoftCache.GetCache(EyouSoft.Cache.Tag.TagName.SysDomains);
            MSysDomain info = null;
            domains = domains ?? new Dictionary<string, MSysDomain>();
            if (domains.ContainsKey(s))
            {
                info = domains[s];
            }
            else
            {
                IUserLogin dal = new DUserLogin();
                info = dal.GetDomain(s);
                if (info != null)
                {
                    domains.Add(s, info);
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.Cache.Tag.TagName.SysDomains, domains);
                }
            }

            return info;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout()
        {
            string companyId = GetCookie(LoginCookieCompanyId);
            string userId = GetCookie(LoginCookieUserId);

            if (!IsKeFuLogin() && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId))
            {
                RemoveUserCache(companyId, userId);
            }

            RemoveCookies();

            if (!IsKeFuLogin())
            {
                IUserLogin dal = new DUserLogin();
                dal.SetOnlineStatus(userId, EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Offline, "00000000-0000-0000-0000-000000000000");
            }
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public static MUserInfo GetUserInfo()
        {
            MUserInfo info = null;
            string companyId = GetCookie(LoginCookieCompanyId);
            string userId = GetCookie(LoginCookieUserId);
            string username = GetCookie(LoginCookieUsername);
            string sysId = GetCookie(LoginCookieSysId);


            if (string.IsNullOrEmpty(companyId)
                || string.IsNullOrEmpty(userId)
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(sysId))
            {
                return null;
            }

            //从缓存查询登录用户信息
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComUser, companyId, userId);
            //从缓存查询登录用户信息计数器
            int getCacheCount = 2;

            do
            {
                info = (MUserInfo)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);
                getCacheCount--;
            } while (info == null && getCacheCount > 0);

            //缓存中未找到登录用户信息，自动登录处理
            if (info == null)
            {
                AutoLogin(sysId, companyId, userId, username, out info);
            }

            if (info == null) return null;
            
            var setting = GetComSetting(companyId);

            if (!IsKeFuLogin() && IsLoginLimit(info.UserType))
            {
                switch (setting.UserLoginLimitType)
                {
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.Earliest:
                        if (info.OnlineStatus == EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Offline) return null;
                        break;
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.Latest:
                        if (info.OnlineSessionId != GetCookie(LoginCookieSessionId)) return null;
                        break;
                    case EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType.None:
                        break;
                    default: break;
                }
            }

            return info;
        }

        /// <summary>
        /// 用户是否登录
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <returns></returns>
        public static bool IsLogin(out MUserInfo info)
        {
            info = GetUserInfo();

            if (info == null) return false;

            return true;
        }

        /// <summary>
        /// 获取公司菜单信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public static IList<MComMenu1Info> GetComMenus(string sysId)
        {
            if (string.IsNullOrEmpty(sysId)) return null;

            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, sysId);
            IList<MComMenu1Info> items = (IList<MComMenu1Info>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);

            if (items == null)
            {
                IUserLogin dal = new DUserLogin();
                items = dal.GetComMenus(sysId);

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, items);
            }

            return items;
        }

        /// <summary>
        /// 获取当前位置信息
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public static MComLocationInfo GetLocation(string sysId)
        {
            //分销商、供应商端怎么处理？？？待解决。
            int? defaultMenu2Id = EyouSoft.Toolkit.Utils.GetIntNullable(HttpContext.Current.Request.QueryString["sl"],null);
            string currentExecutionFilePath = HttpContext.Current.Request.CurrentExecutionFilePath.ToLower();            
            if (currentExecutionFilePath == "/default.aspx") defaultMenu2Id = 0;
            if (currentExecutionFilePath.IndexOf("/groupend/distribution") > -1) defaultMenu2Id = -1;
            if (currentExecutionFilePath.IndexOf("/groupend/suppliers") > -1) defaultMenu2Id = -2;

            if (defaultMenu2Id == null)
            {
                throw new System.Exception("未设置系统默认二级栏目编号，请确保在URL查询参数中包含系统默认二级栏目编号参数：sl(这里是L不是1)。");
            }

            MComLocationInfo locationInfo = new MComLocationInfo()
            {
                DefaultMenu2Id = 0,
                Menu1Id = 0,
                Menu1Name = string.Empty,
                Menu2Id = 0,
                Menu2Name = string.Empty
            };

            if (string.IsNullOrEmpty(sysId))
            {
                MSysDomain domainInfo = GetDomain();

                if (domainInfo != null)
                {
                    sysId = domainInfo.SysId;
                }
            }

            if (string.IsNullOrEmpty(sysId)) return locationInfo;

            IList<MComMenu1Info> menus = GetComMenus(sysId);
            if (menus == null || menus.Count < 1) return locationInfo;

            bool isFind = false;
            foreach (var item1 in menus)
            {
                if (item1 == null || item1.Menu2s == null || item1.Menu2s.Count < 1) continue;
                foreach (var item2 in item1.Menu2s)
                {
                    if (item2 == null) continue;
                    if (item2.DefaultMenu2Id == defaultMenu2Id.Value)
                    {
                        locationInfo.DefaultMenu2Id = defaultMenu2Id.Value;
                        locationInfo.Menu1Id = item1.MenuId;
                        locationInfo.Menu1Name = item1.Name;
                        locationInfo.Menu2Id = item2.MenuId;
                        locationInfo.Menu2Name = item2.Name;

                        isFind = true;
                        break;
                    }
                }

                if (isFind) break;
            }

            return locationInfo;
        }

        /// <summary>
        /// 获取当前位置信息
        /// </summary>
        /// <returns></returns>
        public static MComLocationInfo GetLocation()
        {
            return GetLocation(null);
        }

        /*/// <summary>
        /// 获取组织机构用户(同级及下级部门用户)信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static IList<string> GetDeptUsers(string companyId, int deptId)
        {
            if (string.IsNullOrEmpty(companyId) || deptId < 1) return null;

            IList<string> users = null;
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComDept, companyId);
            //IDictionary<部门编号,IList<用户编号>> 部门缓存以公司为单位
            IDictionary<int, IList<string>> items = (IDictionary<int, IList<string>>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);

            if (items == null || items.Count < 1 || !items.ContainsKey(deptId))
            {
                IUserLogin dal = new DUserLogin();
                users = dal.GetDeptUsers(deptId);

                items = items ?? new Dictionary<int, IList<string>>();

                items.Add(deptId, users);

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, items);
            }
            else
            {
                users = items[deptId];
            }

            return users;
        }*/

        /// <summary>
        /// 获取系统默认二级栏目信息集合
        /// </summary>
        /// <returns></returns>
        public static IList<EyouSoft.Model.SysStructure.MSysMenu2Info> GetSysMenu2s()
        {
            string cacheKey = EyouSoft.Cache.Tag.TagName.SysMenu2s;
            IList<MSysMenu2Info> items = (IList<MSysMenu2Info>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);

            if (items == null)
            {
                IUserLogin dal = new DUserLogin();
                items = dal.GetSysMenu2s();

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, items);
            }

            return items;
        }

        /// <summary>
        /// 是否有指定权限（权限集范围内）
        /// </summary>
        /// <param name="userPrivs">权限集</param>
        /// <param name="privsId">要验证的权限</param>
        /// <returns></returns>
        public static bool IsGrant(int[] userPrivs, int privsId)
        {
            if (userPrivs != null && userPrivs.Length > 0 && userPrivs.Contains(privsId)) return true;

            return false;
        }

        /// <summary>
        /// 是否有指定权限（当前登录用户权限集）
        /// </summary>
        /// <param name="privsId">要验证的权限</param>
        /// <returns></returns>
        public static bool IsGrant(int privsId)
        {
            var info = GetUserInfo();

            if (info != null && info.Privs != null && info.Privs.Length > 0 && info.Privs.Contains(privsId)) return true;

            return false;
        }

        /// <summary>
        /// 获取公司部门信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public static List<MCacheDeptInfo> GetDepts(string companyId)
        {
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComDept, companyId);
            var items = (List<MCacheDeptInfo>)EyouSoftCache.GetCache(cacheKey);

            if (items != null) return items;

            IUserLogin dal = new DUserLogin();
            items = dal.GetComDepts(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                CTEDeptChildrens(items, item.DeptId, item.Depts);
                item.FirstId = CTEDeptFirstId(items, item.DeptId);
            }

            EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, items);

            return items;
        }

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public static MComSetting GetComSetting(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, companyId);
            MComSetting setting = (MComSetting)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);

            if (setting == null)
            {
                IUserLogin dal = new DUserLogin();
                setting= dal.GetComSetting(companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, setting);
            }

            return setting;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout(string companyId, string userId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(companyId))
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComUser, companyId, userId);
                var info = (MUserInfo)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);

                if (info != null)
                {
                    info.OnlineStatus = EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Offline;
                    info.OnlineSessionId = string.Empty;
                }

                IUserLogin dal = new DUserLogin();
                dal.SetOnlineStatus(userId, EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus.Offline, "00000000-0000-0000-0000-000000000000");
            }
        }

        /// <summary>
        /// 是否系统维护中
        /// </summary>
        /// <param name="weiHuXiaoXi">系统维护消息</param>
        /// <returns></returns>
        public static bool IsSysWeiHu(out string weiHuXiaoXi)
        {
            weiHuXiaoXi = string.Empty;
            bool b = false;
            string cacheName = EyouSoft.Cache.Tag.TagName.SysWeiHu;
            object _b = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);

            if (_b != null)
            {
                b = (bool)_b;
            }
            else
            {
                b = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("IsSysWeiHu") == "1";
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, b);
            }

            if (b) weiHuXiaoXi = EyouSoft.Toolkit.ConfigHelper.ConfigClass.GetConfigString("SysWeiHuXiaoXi");

            return b;
        }
        #endregion


    }
}
