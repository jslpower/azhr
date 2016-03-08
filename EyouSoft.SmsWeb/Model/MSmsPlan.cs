using System;
using System.Collections.Generic;

namespace EyouSoft.SmsWeb.Model
{

    #region Channel  短信发送通道

    /// <summary>
    /// 短信发送通道
    /// </summary>
    [Serializable]
    public enum Channel
    {
        /// <summary>
        /// 通知类短信通道(6分/条)，到达率高，对应smsSettings.Index=0
        /// </summary>
        通用通道 = 0,
        /// <summary>
        /// 广告营销类短信通道(5分/条)，对应smsSettings.Index=1
        /// </summary>
        广告通道 = 1
    }

    #endregion

    #region MobileType 短信接收号码类型

    /// <summary>
    /// 短信接收号码类型
    /// </summary>
    [Serializable]
    public enum MobileType
    {
        /// <summary>
        /// 移动,联通手机号码
        /// </summary>
        Mobiel,

        /// <summary>
        /// 小灵通号码
        /// </summary>
        Phs
    }

    #endregion

    #region 短信发送实体

    /// <summary>
    /// 短信发送实体
    /// </summary>
    /// 周文超 2011-09-14
    [Serializable]
    public class MSmsPlan
    {
        /// <summary>
        /// 发送编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 发送通道
        /// </summary>
        public Channel Channel { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送费用
        /// </summary>
        public decimal SendAmount { get; set; }

        /// <summary>
        /// 单条价格(单位:元)
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 是否已发送（T为已发送，F为未发送）
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 接收号码
        /// </summary>
        public List<MSmsNumber> Number { get; set; }

        /// <summary>
        /// 账户信息
        /// </summary>
        public MSmsAccountBase SmsAccount { get; set; }
    }

    #endregion

    #region 短信发送明细接收号码实体

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
        public MobileType Type { get; set; }
    }

    #endregion
}
