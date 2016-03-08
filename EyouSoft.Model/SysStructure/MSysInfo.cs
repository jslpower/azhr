using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    #region 系统信息业务实体
    /// <summary>
    /// 系统信息业务实体
    /// </summary>
    [Serializable]
    public class MSysInfo
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName { get; set; }
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 管理员账号编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 管理员登录账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 管理员登录密码
        /// </summary>
        public EyouSoft.Model.ComStructure.MPasswordInfo Password { get; set; }
        /// <summary>
        /// 联系姓名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 短信配置信息
        /// </summary>
        public EyouSoft.Model.ComStructure.MSmsConfigInfo SmsConfig { get; set; }
        /// <summary>
        /// 管理员在线状态
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus OnlineStatus { get; set; }
    }
    #endregion

    #region 系统域名信息业务实体
    /// <summary>
    /// 系统域名信息业务实体
    /// </summary>
    [Serializable]
    public class MSysDomain
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 系统域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 目标url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
    }
    #endregion

    #region 系统查询信息业务实体
    /// <summary>
    /// 系统查询信息业务实体
    /// </summary>
    public class MSysSearchInfo
    {

    }
    #endregion
}
