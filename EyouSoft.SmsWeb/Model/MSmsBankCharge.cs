using System;

namespace EyouSoft.SmsWeb.Model
{
    #region 充值明细状态
    /// <summary>
    /// 充值明细状态
    /// </summary>
    [Serializable]
    public enum ChargeStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        未审核 = 0,
        /// <summary>
        /// 已审核(通过)
        /// </summary>
        通过 = 1,
        /// <summary>
        /// 已审核(不通过)
        /// </summary>
        不通过 = 2
    }
    #endregion

    #region 短信账户充值明细
    /// <summary>
    /// 短信账户充值明细
    /// </summary>
    /// 周文超 2011-09-14
    [Serializable]
    public class MSmsBankCharge : MSmsAccountBase
    {
        /// <summary>
        /// 明细编号
        /// </summary>
        public string ChargeId { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal ChargeAmount { get; set; }

        /// <summary>
        /// 充值状态
        /// </summary>
        public ChargeStatus Status { get; set; }

        /// <summary>
        /// 本次充值可用金额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 充值人公司
        /// </summary>
        public string ChargeComName { get; set; }

        /// <summary>
        /// 充值人
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// 充值人电话
        /// </summary>
        public string ChargeTelephone { get; set; }

        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemType SysType { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string ShenHeBeiZhu { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ShenHeRen { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ShenHeShiJian { get; set; }
    }
    #endregion

    #region 充值明细查询实体
    /// <summary>
    /// 充值明细查询实体
    /// </summary>
    /// 2011-09-14
    [Serializable]
    public class MQuerySmsBankCharge
    {
        /// <summary>
        /// 短信账户编号
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
        /// 充值状态
        /// </summary>
        public ChargeStatus? Status { get; set; }

        /// <summary>
        /// 充值人公司
        /// </summary>
        public string ChargeComName { get; set; }

        /// <summary>
        /// 充值人
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// 充值时间-始
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 充值时间-止
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
    #endregion

    #region 短信充值明细审核业务实体
    /// <summary>
    /// 短信充值明细审核业务实体
    /// </summary>
    /// 周文超 2011-09-15
    [Serializable]
    public class MCheckSmsBankCharge
    {
        /// <summary>
        /// 明细编号
        /// </summary>
        public string ChargeId { get; set; }
        /// <summary>
        /// 充值状态
        /// </summary>
        public ChargeStatus Status { get; set; }
        /// <summary>
        /// 本次充值可用金额，等于0本次充值可用金额=充值金额
        /// </summary>
        public decimal RealAmount { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ShenHeRen { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string ShenHeBeiZhu { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime ShenHeShiJian { get; set; }
    }
    #endregion
}
