//2011-09-23 汪奇志
using System;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SysStructure;
using System.Collections.Generic;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 系统用户登录数据访问类接口
    /// </summary>
    internal interface IUserLogin
    {
        /// <summary>
        /// 用户登录，根据系统公司编号、用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        MUserInfo Login(string companyId, string username, MPasswordInfo pwd);
        /// <summary>
        /// 用户登录，根据用户编号获取用户信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        MUserInfo Login(string userid);
        /// <summary>
        /// 用户登录，根据系统公司编号、用户名获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <returns></returns>
        MUserInfo Login(string companyId, string username);
        /// <summary>
        /// 写登录日志，用户登录时更新最后登录时间、在线状态、会话标识
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <param name="loginType">登录类型</param>
        void LoginLogwr(MUserInfo info, EyouSoft.Model.EnumType.ComStructure.UserLoginType loginType);
        /// <summary>
        /// 获取域名信息
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        MSysDomain GetDomain(string domain);
        /*/// <summary>
        /// 获取同级部门及下级部门用户编号集合
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        IList<string> GetDeptUsers(int deptId);*/
        /// <summary>
        /// 获取公司菜单信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        IList<MComMenu1Info> GetComMenus(string sysId);
        /// <summary>
        /// 获取系统默认二级栏目信息集合
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysMenu2Info> GetSysMenu2s();
        /// <summary>
        /// 获取公司部门集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        List<MCacheDeptInfo> GetComDepts(string companyId);
        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        MComSetting GetComSetting(string companyId);
        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="status">在线状态</param>
        /// <param name="onlineSessionId">用户会话状态标识</param>
        /// <returns></returns>
        bool SetOnlineStatus(string userId, EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus status, string onlineSessionId);
    }
}
