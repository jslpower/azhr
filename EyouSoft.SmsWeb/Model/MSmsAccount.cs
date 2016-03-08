using System;
using System.Collections.Generic;

namespace EyouSoft.SmsWeb.Model
{
    #region 系统类型
    /// <summary>
    /// 系统类型
    /// </summary>
    [Serializable]
    public enum SystemType
    {
        /// <summary>
        /// NONE
        /// </summary>
        None = 0,
        /// <summary>
        /// 峡州项目
        /// </summary>
        峡州 = 1
    }
    #endregion

    #region 账户信息基础信息
    /// <summary>
    /// 账户信息基础信息
    /// </summary>
    [Serializable]
    public class MSmsAccountBase
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 账户可用金额
        /// </summary>
        public decimal Amount { get; set; }
    }
    #endregion

    #region 短信中心账户信息
    /// <summary>
    /// 短信中心账户信息
    /// </summary>
    /// 周文超 2011-09-14
    [Serializable]
    public class MSmsAccount : MSmsAccountBase
    {

        /// <summary>
        /// Pwd
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemType SysType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 短信发送价格
        /// </summary>
        public List<MSmsChannelInfo> SmsUnitPrice { get; set; }
    }
    #endregion

    #region 短信中心创建账户返回信息业务实体
    /// <summary>
    /// 短信中心创建账户返回信息业务实体
    /// </summary>
    [Serializable]
    public class MRetCreateAccount
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRetCreateAccount() { }

        /// <summary>
        /// 短信账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 返回代码，1成功，其它失败 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string Desc { get; set; }
    }
    #endregion
}
