using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public interface IComUser
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComUser item);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComUser item);

        /*/// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户编号(以逗号分隔)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool Delete(string ids, string companyId);*/
        /// <summary>
        /// 删除用户，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        int Delete(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, string userId);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="ids">用户编号(以逗号分隔)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="UserStatus">用户状态</param>
        /// <returns></returns>
        bool SetUserStatus(string ids, string CompanyId, EyouSoft.Model.EnumType.ComStructure.UserStatus UserStatus);
        
        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="privs">权限集合</param>
        /// <returns>true：成功 false：失败</returns>
        bool UpdatePrivs(string userId, string companyId, int? roleId, string privs);

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns>true：存在 false：不存在</returns>
        bool IsExistsUserName(string userName, string companyId, string userId);

        /// <summary>
        /// 获取用户实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>用户实体</returns>
        MComUser GetModel(string userId, string companyId, EyouSoft.Model.EnumType.ComStructure.AttachItemType Item);

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>用户权限</returns>
        string GetPrivs(string userId, string companyId);

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>用户信息集合</returns>
        IList<MComUser> GetList(int pageCurrent, int pageSize, ref int pageCount,string companyId,MComUserSearch search);

           /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="userNm">用户名</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <param name="MD5Pwd">MD5密码</param>
        /// <returns></returns>
        bool PwdModify(string UserId, string userNm, string OldPwd, string NewPwd, string MD5Pwd);
        /// <summary>
        /// 获取系统用户数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetYongHuShuLiang(string companyId);
    }
}
