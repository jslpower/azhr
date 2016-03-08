using System;
using System.Collections.Generic;

namespace EyouSoft.Model.SmsStructure
{
    /// <summary>
    /// 短信发送明细实体
    /// </summary>
    /// 周文超 2011-09-13
    public class MSmsDetail
    {
        /// <summary>
        /// 发送明细编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 发送通道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送费用
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 单条价格
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public EnumType.SmsStructure.SendStatus Status { get; set; }

        /// <summary>
        /// 发送状态描述
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 接收号码
        /// </summary>
        public IList<MSmsNumber> Number { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.LeiXing LeiXing { get; set; }
    }

    /// <summary>
    /// 短信发送明细接收号码实体
    /// </summary>
    /// 周文超 2011-09-13
    [Serializable]
    public class MSmsNumber
    {
        /// <summary>
        /// 号码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 号码类型
        /// </summary>
        public EnumType.SmsStructure.MobileType Type { get; set; }
    }

    /// <summary>
    /// 短信明细查询实体
    /// </summary>
    /// 周文超 2011-09-14
    public class MQuerySmsDetail
    {
        /// <summary>
        /// 发送时间-起
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 发送时间-止
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public EnumType.SmsStructure.SendStatus? Status { get; set; } 
    }
}
