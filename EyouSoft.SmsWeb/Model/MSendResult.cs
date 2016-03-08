using System;

namespace EyouSoft.SmsWeb.Model
{
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
}
