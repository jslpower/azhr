//汪奇志 2011-09-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 系统管理业务逻辑类
    /// </summary>
    public class BSys
    {
        private readonly EyouSoft.IDAL.SysStructure.ISys dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.ISys>();

        #region constructure
        /// <summary>
        /// default constructor
        /// </summary>
        public BSys() { }
        #endregion

        #region public memebers
        /// <summary>
        /// 创建子系统
        /// </summary>
        /// <param name="info">系统信息业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.创建系统信息
        /// 2.创建系统公司信息
        /// 3.创建管理员账号信息
        /// 4.创建管理员角色信息
        /// 5.创建总部信息
        /// 6.子系统基础数据
        /// 7.创建短信账号
        /// </remarks>
        public int CreateSys(EyouSoft.Model.SysStructure.MSysInfo info)
        {
            if (info == null) return 0;
            if (info.Password == null || string.IsNullOrEmpty(info.Password.NoEncryptPassword)) return -1;

            if (info.SmsConfig == null || !info.SmsConfig.IsEnabled)
            {
                info.SmsConfig = new EyouSoft.BLL.SmsStructure.BSmsAccount().CreateSmsAccount();
            }

            if (info.SmsConfig == null) return -2;

            info.SysId = Guid.NewGuid().ToString();
            info.CompanyId = Guid.NewGuid().ToString();
            info.UserId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.CreateSys(info);

            if (dalRetCode == 1)
            {
                new EyouSoft.BLL.ComStructure.BComSetting().SetComSmsConfig(info.CompanyId, info.SmsConfig);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 设置系统域名
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名信息集合</param>
        /// <returns></returns>
        public int SetSysDomains(string sysId,IList<EyouSoft.Model.SysStructure.MSysDomain> domains)
        {
            if (string.IsNullOrEmpty(sysId)) return 0;
            if (domains == null || domains.Count < 1) return -1;

            IList<string> notVerifyDomains = new List<string>();
            IList<string> existsDomains = new List<string>();

            foreach (var item in domains)
            {
                if (item != null && !string.IsNullOrEmpty(item.Domain))
                {
                    item.Domain = item.Domain.Trim().ToLower();
                    notVerifyDomains.Add(item.Domain);
                }
                else
                {
                    existsDomains.Add("null or empty!");
                }
            }

            if (existsDomains != null && existsDomains.Count > 0) return -2;

            existsDomains = IsExistsDomains(sysId, notVerifyDomains);

            if (existsDomains != null && existsDomains.Count > 0) return -2;

            if (dal.SetSysDomains(sysId, domains) == 1)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.Cache.Tag.TagName.SysDomains);
                return 1;
            }

            return -2;
        }

        /// <summary>
        /// 创建子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.创建子系统一级栏目
        /// 2.创建子系统二级栏目
        /// </remarks>
        public int CreateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info)
        {
            if (info == null) return 0;
            if (string.IsNullOrEmpty(info.SysId)) return -1;
            if (string.IsNullOrEmpty(info.Name)) return -2;
            if (info.Menu2s == null || info.Menu2s.Count() < 1) return -3;

            if (dal.CreateSysMenu(info) == 1)
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, info.SysId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return 1;
            }

            return -4;
        }

        /// <summary>
        /// 修改子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        public int UpdateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info)
        {
            if (info == null) return 0;
            if (string.IsNullOrEmpty(info.SysId)) return -1;
            if (string.IsNullOrEmpty(info.Name)) return -2;
            if (info.Menu2s == null || info.Menu2s.Count() < 1) return -3;
            if (info.MenuId == 0) return -4;

            bool isSetDefaultMenu2Id = true;
            foreach (var item in info.Menu2s)
            {
                if (item.DefaultMenu2Id < 1)
                {
                    isSetDefaultMenu2Id = false;
                    break;
                }
            }

            if (!isSetDefaultMenu2Id) return -5;

            if (dal.UpdateSysMenu(info)==1)
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, info.SysId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return 1;
            }

            return -6;
        }

        /// <summary>
        /// 删除子系统一级栏目
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <param name="menu1Id">子系统一级栏目编号</param>
        /// <returns></returns>
        public int DeleteSysMenu1(string sysId, int menu1Id)
        {
            if (string.IsNullOrEmpty(sysId)) return -1;
            if (menu1Id < 1) return -2;

            if (dal.DeleteSysMenu1(menu1Id) == 1)
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, sysId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return 1;
            }

            return -3;
        }

        /// <summary>
        /// 设置子系统权限
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <param name="privs">权限信息集合</param>
        /// <returns></returns>
        public int SetSysPrivs(string sysId,IList<EyouSoft.Model.SysStructure.MComMenu2Info> privs)
        {
            if (string.IsNullOrEmpty(sysId)) return -1;

            if (dal.SetSysPrivs(sysId, privs) == 1)
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, sysId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return 1;
            }

            return -2;
        }

        /// <summary>
        /// 设置子系统一级栏目及二级栏目为系统默认
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public int SetMenuBySys(string sysId)
        {
            if (string.IsNullOrEmpty(sysId)) return 0;

            if (dal.SetMenuBySys(sysId) == 1)
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComMenu, sysId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return 1;
            }

            return -2;
        }

        /// <summary>
        /// 设置角色权限为子系统开通的所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public int SetRoleBySysPrivs(int roleId, string sysId)
        {
            return dal.SetRoleBySysPrivs(roleId, sysId);
        }

        /// <summary>
        /// 设置用户权限为子系统开通的所有权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public int SetUserBySysPrivs(string userId, string sysId)
        {
            return dal.SetUserBySysPrivs(userId, sysId);
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public bool SetUserRole(string userId, int roleId)
        {
            if (string.IsNullOrEmpty(userId) || roleId < 1) return false;
            return dal.SetUserRole(userId, roleId);
        }

        /// <summary>
        /// 获取子系统信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysInfo> GetSyss(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SysStructure.MSysSearchInfo searchInfo)
        {
            var items = dal.GetSyss(pageSize, pageIndex, ref recordCount, searchInfo);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    var setting= new EyouSoft.BLL.ComStructure.BComSetting().GetModel(item.CompanyId);
                    if (setting != null) item.SmsConfig = setting.SmsConfig;
                }
            }

            return items;
        }

        /// <summary>
        /// 获取子系统信息
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysInfo GetSysInfo(string sysId)
        {
            return dal.GetSysInfo(sysId);
        }

        /// <summary>
        /// 获取默认栏目信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysMenu1Info> GetMenus()
        {
            return dal.GetMenus();
        }

        /// <summary>
        /// 获取默认权限信息集合
        /// </summary>
        /// <param name="defaultMenu2Id">默认二级栏目编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysPrivsInfo> GetPrivs(int defaultMenu2Id)
        {
            return dal.GetPrivs(defaultMenu2Id);
        }

        /// <summary>
        /// set webmaster pwd
        /// </summary>
        /// <param name="webmasterId">webmaster id</param>
        /// <param name="username">webmaster username</param>
        /// <param name="pwd">webmaster pwd info</param>
        /// <returns></returns>
        public bool SetWebmasterPwd(int webmasterId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd)
        {
            if (webmasterId < 1 || string.IsNullOrEmpty(username) || pwd == null || string.IsNullOrEmpty(pwd.NoEncryptPassword)) return false;

            return dal.SetWebmasterPwd(webmasterId, username, pwd);
        }

        /// <summary>
        /// 获取系统域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysDomain> GetDomains(string sysId)
        {
            if (string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36)
                return null;

            return dal.GetDomains(sysId);
        }

        /// <summary>
        /// 判断域名是否重复，返回重复的域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名集合</param>
        /// <returns></returns>
        public IList<string> IsExistsDomains(string sysId, IList<string> domains)
        {
            if (string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36 || domains == null || domains.Count < 1)
            {
                return null;
            }

            IList<string> notExistsDomains = new List<string>();
            IList<string> existsDomains = new List<string>();

            foreach (var s in domains)
            {
                string s1 = s;
                if (s1 != null) s1 = s1.Trim().ToLower();

                if (string.IsNullOrEmpty(s1))
                {
                    existsDomains.Add(null);
                    continue;
                }

                if (s1.IndexOf("http://") > -1)
                {
                    existsDomains.Add(s1);
                    continue;
                }

                if (notExistsDomains.IndexOf(s1) > -1)
                {
                    existsDomains.Add(s1);
                }
                else
                {
                    notExistsDomains.Add(s1);
                }
            }

            if (existsDomains != null && existsDomains.Count > 0) return existsDomains;

            return dal.IsExistsDomains(sysId, domains);
        }

        /// <summary>
        /// 获取子系统角色(管理员)编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetSysRoleId(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return -1;

            return dal.GetSysRoleId(companyId);
        }

        /// <summary>
        /// 基础权限管理-写入一级栏目
        /// </summary>
        /// <param name="info">一级栏目信息业务实体</param>
        /// <returns></returns>
        public int InsertFirstMenu(EyouSoft.Model.SysStructure.MSysMenu1Info info)
        {
            if (info == null || string.IsNullOrEmpty(info.Name)) return 0;
            info.Name = info.Name.Trim();
            if (string.IsNullOrEmpty(info.Name)) return 0;
            if (dal.IsExistsMenu1Name(info.Name)) return 0;

            return dal.InsertFirstMenu(info);
        }

        /// <summary>
        /// 基础权限管理-写入二级栏目
        /// </summary>
        /// <param name="info">二级栏目信息业务实体</param>
        /// <returns></returns>
        public int InsertSecondMenu(EyouSoft.Model.SysStructure.MSysMenu2Info info)
        {
            if (info == null || string.IsNullOrEmpty(info.Name) || info.FirstId < 1 || string.IsNullOrEmpty(info.Url)) return 0;
            info.Name = info.Name.Trim();
            if (string.IsNullOrEmpty(info.Name)) return 0;
            if (dal.IsExistsMenu2Name(info.FirstId, info.Name)) return 0;

            int menu2Id = dal.InsertSecondMenu(info);
            if (menu2Id>0)
            {
                string cacheKey = EyouSoft.Cache.Tag.TagName.SysMenu2s;
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return menu2Id;
            }

            return 0;
        }

        /// <summary>
        /// 基础权限管理-写入明细权限
        /// </summary>
        /// <param name="info">权限信息业务实体</param>
        /// <returns></returns>
        public int InsertPrivs(EyouSoft.Model.SysStructure.MSysPrivsInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.Name) || info.SecondId < 1) return 0;
            info.Name = info.Name.Trim();
            if (string.IsNullOrEmpty(info.Name)) return 0;
            if (dal.IsExistsPrivsName(info.SecondId, info.Name)) return 0;
            if (info.PrivsType != EyouSoft.Model.EnumType.SysStructure.PrivsType.其它 && dal.IsExistsPrivsType(info.SecondId, info.PrivsType)) return 0;

            int privsId=dal.InsertPrivs(info);

            if (privsId > 0)
            {
                string cacheKey = EyouSoft.Cache.Tag.TagName.SysMenu2s;
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return privsId;
            }

            return 0;
        }

        /// <summary>
        /// 基础权限管理-更新二级栏目链接
        /// </summary>
        /// <param name="menu2Id">二级栏目编号</param>
        /// <param name="url">二级栏目链接</param>
        /// <returns></returns>
        public bool UpdateMenu2Url(int menu2Id, string url)
        {
            if (menu2Id < 1 || string.IsNullOrEmpty(url)) return false;

            return dal.UpdateMenu2Url(menu2Id, url);
        }

        /// <summary>
        /// 判断子系统是否开通二级栏目
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="menu2">二级栏目</param>
        /// <returns></returns>
        public bool IsExistsMenu2(string sysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2 menu2)
        {
            if (string.IsNullOrEmpty(sysId)) return false;
            if (menu2 == EyouSoft.Model.EnumType.PrivsStructure.Menu2.None) return false;

            return dal.IsExistsMenu2(sysId, menu2);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="cacheName">cache name</param>
        /// <returns></returns>
        public bool RemoveCache(string cacheName)
        {
            if (string.IsNullOrEmpty(cacheName)) return true;

            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);

            return true;
        }
        #endregion
    }
}
