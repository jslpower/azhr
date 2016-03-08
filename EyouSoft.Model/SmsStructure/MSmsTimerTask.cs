using System;
using System.Collections.Generic;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信定时短信任务实体

    /// <summary>
    /// 短信定时短信任务实体
    /// </summary>
    /// 周文超 2011-09-13
    public class MSmsTimerTask
    {
        /// <summary>
        /// 定时短信任务编号
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 要求发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public EnumType.SmsStructure.SendStatus Status { get; set; }

        /// <summary>
        /// 发送状态描述
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// 发送通道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        public DateTime? RealTime { get; set; }

        /// <summary>
        /// 接收号码
        /// </summary>
        public IList<MSmsNumber> Number { get; set; }
    }

    #endregion

    #region 定时短信修改状态实体

    /// <summary>
    /// 定时短信修改状态实体
    /// </summary>
    public class MSmsTaskState
    {
        /// <summary>
        /// 定时短信任务编号
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public EnumType.SmsStructure.SendStatus Status { get; set; }

        /// <summary>
        /// 发送状态描述
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        public DateTime? RealTime { get; set; }
    }

    #endregion
}
