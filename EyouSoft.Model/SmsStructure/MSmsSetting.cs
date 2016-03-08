using System;

namespace EyouSoft.Model.SmsStructure
{
    /// <summary>
    /// 短信配置实体
    /// </summary>
    /// 周文超 2011-09-14
    public class MSmsSetting
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public EnumType.SmsStructure.SettingType Type { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 前X天
        /// </summary>
        public int BeforeDay { get; set; }

        /// <summary>
        /// 点时间
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 当天是否已发送
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 发送短信用户编号-后台服务使用
        /// </summary>
        public string SendSmsUserId { get; set; }
    }
}
