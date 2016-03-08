using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信账户充值信息业务实体
    /// <summary>
    /// 短信账户充值信息业务实体
    /// </summary>
    public class MSmsBankChargeInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsBankChargeInfo() { }

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
        /// 充值明细编号
        /// </summary>
        public string ChargeId { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal ChargeAmount { get; set; }
        /// <summary>
        /// 充值状态，0：未审核，1：已审核。
        /// </summary>
        public int Status { get; set; }
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
        public string SysTypeName { get; set; }
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

    #region 短信账户充值查询信息业务实体
    /// <summary>
    /// 短信账户充值查询信息业务实体
    /// </summary>
    public class MSmsBankChargeSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSmsBankChargeSearchInfo() { }

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
        /// 充值状态，0：未审核，1：已审核。
        /// </summary>
        public int? Status { get; set; }
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

    #region 短信充值审核信息业务实体
    /// <summary>
    /// 短信充值审核信息业务实体
    /// </summary>
    public class MSetSmsBankChargeStatusInfo
    {
        /// <summary>
        /// 明细编号
        /// </summary>
        public string ChargeId { get; set; }
        /// <summary>
        /// 充值状态，0：未审核，1：通过，2：未通过。
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 本次充值可用金额，等于0本次充值可用金额 = 充值金额
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
    }
    #endregion
}
