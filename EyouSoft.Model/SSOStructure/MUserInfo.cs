using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SSOStructure
{
    #region 登录用户信息业务实体
    /// <summary>
    /// 登录用户信息业务实体
    /// </summary>
    [Serializable]
    public class MUserInfo
    {
        //EyouSoft.Model.ComStructure.MPasswordInfo _passwordinfo = new EyouSoft.Model.ComStructure.MPasswordInfo();
        bool _IsHandleElse = false;

        /// <summary>
        /// default constructor
        /// </summary>
        public MUserInfo() { }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 系统公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }      
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户权限集合
        /// </summary>
        public int[] Privs { get { return RolePrivs; } }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 兼管部门编号
        /// </summary>
        public int JGDeptId { get; set; }        
        /// <summary>
        /// 获取部门信息集合
        /// </summary>
        public int[] Depts { get { return this.DeptId == this.JGDeptId ? new int[] { this.DeptId } : new int[] { this.DeptId, this.JGDeptId }; } }
        /// <summary>
        /// 用户类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserType UserType { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 分销商公司信息业务实体
        /// </summary>
        public MTourCompanyInfo TourCompanyInfo { get; set; }
        /// <summary>
        /// 供应商公司信息业务实体
        /// </summary>
        public MSourceCompanyInfo SourceCompanyInfo { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserStatus Status { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 所属角色编号
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 所属角色权限集合
        /// </summary>
        public int[] RolePrivs { get; set; }
        /// <summary>
        /// 是否允许操作除自己外的数据
        /// </summary>
        public bool IsHandleElse { get { return this.IsAdmin; } set { _IsHandleElse = value; } }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 用户在线状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus OnlineStatus { get; set; }
        /// <summary>
        /// 用户会话状态标识
        /// </summary>
        public string OnlineSessionId { get; set; }
    }
    #endregion

    #region 分销商公司信息业务实体
    /// <summary>
    /// 分销商公司信息业务实体
    /// </summary>
    [Serializable]
    public class MTourCompanyInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTourCompanyInfo() { }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int LevelId { get; set; }
        /// <summary>
        /// 登录账号联系人编号（即登录账号部门编号）
        /// </summary>
        public string LxrId { get; set; }
    }
    #endregion

    #region 供应商公司信息业务实体
    /// <summary>
    /// 供应商公司信息业务实体
    /// </summary>
    [Serializable]
    public class MSourceCompanyInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceCompanyInfo() { }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
    #endregion
}
