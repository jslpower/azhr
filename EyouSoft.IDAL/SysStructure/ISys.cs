//汪奇志 2011-09-21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 系统管理数据访问类接口
    /// </summary>
    public interface ISys
    {
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
        /// </remarks>
        int CreateSys(EyouSoft.Model.SysStructure.MSysInfo info);

        /// <summary>
        /// 设置系统域名
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名信息集合</param>
        /// <returns></returns>
        int SetSysDomains(string sysId, IList<EyouSoft.Model.SysStructure.MSysDomain> domains);

        /// <summary>
        /// 创建子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.创建子系统一级栏目
        /// 2.创建子系统二级栏目
        /// </remarks>
        int CreateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info);

        /// <summary>
        /// 修改子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        int UpdateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info);

        /// <summary>
        /// 删除子系统一级栏目
        /// </summary>
        /// <param name="menu1Id">子系统一级栏目编号</param>
        /// <returns></returns>
        int DeleteSysMenu1(int menu1Id);

        /// <summary>
        /// 设置子系统权限
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <param name="privs">权限信息集合</param>
        /// <returns></returns>
        int SetSysPrivs(string sysId, IList<EyouSoft.Model.SysStructure.MComMenu2Info> privs);

        /// <summary>
        /// 设置子系统一级栏目及二级栏目为系统默认
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        int SetMenuBySys(string sysId);

        /// <summary>
        /// 设置角色权限为子系统开通的所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysId"></param>
        /// <returns></returns>
        int SetRoleBySysPrivs(int roleId, string sysId);

        /// <summary>
        /// 设置用户权限为子系统开通的所有权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        int SetUserBySysPrivs(string userId, string sysId);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        bool SetUserRole(string userId, int roleId);

        /// <summary>
        /// 获取子系统信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysInfo> GetSyss(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SysStructure.MSysSearchInfo searchInfo);

        /// <summary>
        /// 获取子系统信息
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <returns></returns>
        EyouSoft.Model.SysStructure.MSysInfo GetSysInfo(string sysId);        

        /// <summary>
        /// 获取默认栏目信息集合
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysMenu1Info> GetMenus();

        /// <summary>
        /// 获取默认权限信息集合
        /// </summary>
        /// <param name="defaultMenu2Id">默认二级栏目编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SysStructure.MSysPrivsInfo> GetPrivs(int defaultMenu2Id);

        /// <summary>
        /// set webmaster pwd
        /// </summary>
        /// <param name="webmasterId">webmaster id</param>
        /// <param name="username">webmaster username</param>
        /// <param name="pwd">webmaster pwd info</param>
        /// <returns></returns>
        bool SetWebmasterPwd(int webmasterId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd);

        /// <summary>
        /// 获取系统域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        IList<Model.SysStructure.MSysDomain> GetDomains(string sysId);

        /// <summary>
        /// 判断域名是否重复，返回重复的域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名集合</param>
        /// <returns></returns>
        IList<string> IsExistsDomains(string sysId, IList<string> domains);

        /// <summary>
        /// 获取子系统角色(管理员)编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetSysRoleId(string companyId);
        /// <summary>
        /// 基础权限管理-写入一级栏目
        /// </summary>
        /// <param name="info">一级栏目信息业务实体</param>
        /// <returns></returns>
        int InsertFirstMenu(EyouSoft.Model.SysStructure.MSysMenu1Info info);
        /// <summary>
        /// 基础权限管理-写入二级栏目
        /// </summary>
        /// <param name="info">二级栏目信息业务实体</param>
        /// <returns></returns>
        int InsertSecondMenu(EyouSoft.Model.SysStructure.MSysMenu2Info info);
        /// <summary>
        /// 基础权限管理-写入明细权限
        /// </summary>
        /// <param name="info">权限信息业务实体</param>
        /// <returns></returns>
        int InsertPrivs(EyouSoft.Model.SysStructure.MSysPrivsInfo info);
        /// <summary>
        /// 相同二级栏目下是否存在相同的权限类别
        /// </summary>
        /// <param name="secondId">二级栏目编号</param>
        /// <param name="privsType">权限类别</param>
        /// <returns></returns>
        bool IsExistsPrivsType(int secondId, EyouSoft.Model.EnumType.SysStructure.PrivsType privsType);
        /// <summary>
        ///  相同二级栏目下是否存在相同的权限名称
        /// </summary>
        /// <param name="secondId">二级栏目编号</param>
        /// <param name="privsName">权限名称</param>
        /// <returns></returns>
        bool IsExistsPrivsName(int secondId, string privsName);
        /// <summary>
        /// 相同一级栏目下是否存在相同的二级栏目名称
        /// </summary>
        /// <param name="firstId">一级栏目编号</param>
        /// <param name="menu2Name">二级栏目名称</param>
        /// <returns></returns>
        bool IsExistsMenu2Name(int firstId, string menu2Name);
        /// <summary>
        /// 是否存在相同的一级栏目名称
        /// </summary>
        /// <param name="menu1Name">一级栏目名称</param>
        /// <returns></returns>
        bool IsExistsMenu1Name(string menu1Name);
        /// <summary>
        /// 基础权限管理-更新二级栏目链接
        /// </summary>
        /// <param name="menu2Id">二级栏目编号</param>
        /// <param name="url">二级栏目链接</param>
        /// <returns></returns>
        bool UpdateMenu2Url(int menu2Id, string url);
        /// <summary>
        /// 判断子系统是否开通二级栏目
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="menu2">二级栏目</param>
        /// <returns></returns>
        bool IsExistsMenu2(string sysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2 menu2);
    }
}
