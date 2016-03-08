using System;
using System.Collections.Generic;

namespace EyouSoft.Model.SmsStructure
{
    #region 发送短信实体

    /// <summary>
    /// 发送短信实体
    /// </summary>
    /// 周文超 2011-09-19
    [Serializable]
    public class MSendMessage
    {
        private EnumType.SmsStructure.SendType _sendType = EnumType.SmsStructure.SendType.直接发送;
        private DateTime _sendTime = DateTime.Now;

        /// <summary>
        /// 发送短信的公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 发送短信的用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 发信人
        /// </summary>
        public string UserFullName { get; set; }
                
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SmsContent { get; set; }

        /// <summary>
        /// 获得短信发送的完整内容(有判断了是否存在发信人，若存在发信人，则会自动加上发信人)
        /// 格式：内容 ＋ ["发信人：" + 公司名称]  其中[ ]表示可选
        /// </summary>
        public string SmsContentSendComplete
        {
            get
            {
                if (!string.IsNullOrEmpty(UserFullName))
                    return SmsContent + "发信人：" + UserFullName;

                return SmsContent;
            }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        /// <summary>
        /// 接收手机号码集合
        /// </summary>
        public List<MSmsNumber> Mobiles { get; set; }

        /// <summary>
        /// 发送方式
        /// </summary>
        public EnumType.SmsStructure.SendType SendType
        {
            get { return _sendType; }
            set { _sendType = value; }
        }

        /// <summary>
        /// 发送通道
        /// </summary>
        public int SendChannel
        {
            get;
            set;
        }
        /// <summary>
        /// 短信类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.LeiXing LeiXing { get; set; }
    }

    #endregion

    #region 验证短信/发送短信返回结果实体

    /// <summary>
    /// 发送短信/验证发送结果业务实体
    /// </summary>
    [Serializable]
    public class MSendResult
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSendResult() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="isSucceed">是否发送/验证成功</param>
        /// <param name="errorMessage">错误信息</param>
        public MSendResult(bool isSucceed, string errorMessage)
        {
            IsSucceed = isSucceed;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 是否发送/验证成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 应扣除账户金额(为预扣除金额)
        /// </summary>
        public decimal CountFee { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountMoney { get; set; }

        /// <summary>
        /// [移动、联通]待发送的总的号码个数
        /// </summary>
        public int MobileNumberCount { get; set; }

        /// <summary>
        /// [小灵通]待发送的总的号码个数
        /// </summary>
        public int PhsNumberCount { get; set; }

        /// <summary>
        /// [移动、联通]实际发送成功的总的号码个数
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// [小灵通]实际发送成功的总的号码个数
        /// </summary>
        public int PhsSuccessCount { get; set; }

        /// <summary>
        /// [移动、联通]1条短信内容实际产生的短信条数
        /// </summary>
        public int FactCount { get; set; }

        /// <summary>
        /// [小灵通]1条短信内容实际产生的短信条数
        /// </summary>
        public int PhsFactCount { get; set; }

        /// <summary>
        /// [移动，联通]待发送的总的短信条数，[移动，联通]待发送的总的号码个数*[移动、联通]1条短信内容实际产生的短信条数
        /// </summary>
        public int WaitSendMobileCount { get { return MobileNumberCount * FactCount; } }

        /// <summary>
        /// [小灵通]待发送的总的短信条数，[小灵通]待发送的总的号码个数*[小灵通]1条短信内容实际产生的短信条数
        /// </summary>
        public int WaitSendPhsCount { get { return PhsNumberCount * PhsFactCount; } }

        /// <summary>
        /// 短信中心待发送短信编号
        /// </summary>
        public string SmsCenterPlanId { get; set; }
    }

    #endregion
}
