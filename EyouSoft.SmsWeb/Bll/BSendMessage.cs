using System;
using System.Collections.Generic;
using System.Linq;
using EyouSoft.SmsWeb.Dal;
using EyouSoft.SmsWeb.Model;
using EyouSoft.SmsWeb.SMSStructure.VoSmsServices;

namespace EyouSoft.SmsWeb.Bll
{
    /// <summary>
    /// 发送短信业务逻辑
    /// </summary>
    /// 周文超 2011-09-19
    public class BSendMessage
    {
        #region private member

        /// <summary>
        /// 账户信息数据访问对象
        /// </summary>
        private readonly DSmsAccount _dalAccount = new DSmsAccount();

        /// <summary>
        /// 短信发送信息数据访问对象
        /// </summary>
        private readonly DSmsPlan _dalPlan = new DSmsPlan();

        /// <summary>
        /// 短信接口对象
        /// </summary>
        private readonly Service _sms = new Service();

        /// <summary>
        /// 获取短信内容实际产生总的短信条数
        /// </summary>
        /// <param name="smsContentSendComplete">要发送的完整的短信内容</param>
        /// <param name="smsType">短信号码类型</param>
        /// <returns></returns>
        private int GetSmsTotalCount(string smsContentSendComplete, MobileType smsType)
        {
            //1条短信所占的字符长度
            int oneSmsLength = 70;
            //总的实际短信条数
            int messageFaceCount = 1;
            switch (smsType)
            {
                case MobileType.Mobiel: oneSmsLength = 70; break;
                case MobileType.Phs: oneSmsLength = 45; break;
            }

            if (smsContentSendComplete.Length > oneSmsLength)
            {
                messageFaceCount = (smsContentSendComplete.Length + oneSmsLength - 1) / oneSmsLength;
            }

            return messageFaceCount;
        }

        /// <summary>
        /// 验证是否是小灵通号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsPhs(string s)
        {
            if (string.IsNullOrEmpty(s)) return true;
            string mobileRegexPattern = System.Configuration.ConfigurationManager.AppSettings["SMS_Mobile_Regex_Pattern"];//
            var regMobile = new System.Text.RegularExpressions.Regex(mobileRegexPattern);

            return !regMobile.IsMatch(s);
        }

        /// <summary>
        /// 获取小灵通号码个数
        /// </summary>
        /// <param name="mobiles"></param>
        /// <returns></returns>
        private int GetPhsCount(IList<MSmsNumber> mobiles)
        {
            if (mobiles == null) return 0;
            return mobiles.Sum(mobile => IsPhs(mobile.Code) ? 1 : 0);
        }

        #endregion

        #region 成员方法

        /// <summary>
        /// 是否包含关键字，包含时返回关键字内容，不包含时返回空字符串
        /// </summary>
        /// <param name="s">要验证的字符串</param>
        /// <returns></returns>
        public string IsIncludeKeyWord(string s)
        {
            string keyWord = string.Empty;

            try
            {
                _sms.Timeout = 5000;
                while (true)
                {
                    string tmp = _sms.IsIncludeKeyWord(s);

                    if (string.IsNullOrEmpty(tmp)) break;

                    keyWord += tmp + ",";

                    s = s.Replace(tmp, "");
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.Substring(0, keyWord.Length - 1);
            }

            return keyWord;
        }

        /// <summary>
        /// 验证要发送的短信
        /// </summary>
        /// <param name="smsPlan">发送短信提交的业务实体</param>
        /// <returns></returns>
        public MSendResult ValidateSend(MSmsPlan smsPlan)
        {
            var validateResultInfo = new MSendResult(true, null);

            if (smsPlan == null || smsPlan.SmsAccount == null)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "未填写发送短信账户信息";

                return validateResultInfo;
            }

            if (string.IsNullOrEmpty(smsPlan.SmsAccount.AccountId) || smsPlan.SmsAccount.AccountId.Length != 36)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "发送短信的账户编号不正确";

                return validateResultInfo;
            }

            if (string.IsNullOrEmpty(smsPlan.SmsAccount.AppKey) || smsPlan.SmsAccount.AppKey.Length != 36)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "发送短信的账户的AppKey不正确";

                return validateResultInfo;
            }

            /*
             if (string.IsNullOrEmpty(smsPlan.SmsAccount.AppSecret) || smsPlan.SmsAccount.AppSecret.Length != 36)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "发送短信的账户的AppSecret不正确";

                return validateResultInfo;
            }
             */

            MSmsAccount modelAccount = _dalAccount.GetFullSmsAccount(smsPlan.SmsAccount.AccountId,
                                                                     smsPlan.SmsAccount.AppKey);
            if (modelAccount == null)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "发送短信的账户在短信中心不存在";

                return validateResultInfo;
            }

            if (modelAccount.Amount <= 0)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "发送短信的账户余额不足";

                return validateResultInfo;
            }

            MSmsChannelInfo price = null;
            if (modelAccount.SmsUnitPrice != null && modelAccount.SmsUnitPrice.Count > 0)
            {
                price = modelAccount.SmsUnitPrice.Find((MSmsChannelInfo tmp) => { return smsPlan.Channel == tmp.Cnannel; });
            }

            if (price == null)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "短信帐户中不存在与该广告通道对应的价格信息";

                return validateResultInfo;
            }

            if (string.IsNullOrEmpty(smsPlan.Content))
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "短信内容不能为空";

                return validateResultInfo;
            }

            if (smsPlan.Number == null || smsPlan.Number.Count < 1)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "未填写任何接收短信人的手机号码";

                return validateResultInfo;
            }

            string keyWord = IsIncludeKeyWord(smsPlan.Content);

            if (!string.IsNullOrEmpty(keyWord))
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = string.Format("您要发送的短信内容中包含:{0} 这些禁止发送的关键字，请重新编辑！", keyWord);

                return validateResultInfo;
            }

            //单条短信发送价格
            smsPlan.UnitPrice = price.Price;

            //短信内容针对移动联通手机实际产生的短信条数
            validateResultInfo.FactCount = GetSmsTotalCount(smsPlan.Content, MobileType.Mobiel);
            //短信内容针对小灵通实际产生的短信条数
            validateResultInfo.PhsFactCount = GetSmsTotalCount(smsPlan.Content, MobileType.Phs);

            //小灵通号码个数
            validateResultInfo.PhsNumberCount = GetPhsCount(smsPlan.Number);
            //移动联通号码个数
            validateResultInfo.MobileNumberCount = smsPlan.Number.Count - validateResultInfo.PhsNumberCount;

            //应扣除的金额
            validateResultInfo.CountFee = smsPlan.UnitPrice * (validateResultInfo.WaitSendMobileCount + validateResultInfo.WaitSendPhsCount);

            //本次的发送费用
            smsPlan.SendAmount = validateResultInfo.CountFee;

            //用户账户余额
            validateResultInfo.AccountMoney = modelAccount.Amount;

            if (validateResultInfo.AccountMoney < validateResultInfo.CountFee)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = string.Format("您的账户余额不足,当前余额为:{0}，此次消费:{1}", validateResultInfo.AccountMoney.ToString("C2"), validateResultInfo.CountFee.ToString("C2"));

                return validateResultInfo;
            }

            return validateResultInfo;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsPlan">发送短信提交的业务实体</param>
        /// <returns></returns>
        public MSendResult Send(MSmsPlan smsPlan)
        {
            //发送短信验证
            MSendResult validateResultInfo = ValidateSend(smsPlan);

            if (!validateResultInfo.IsSucceed)
            {
                return validateResultInfo;
            }

            smsPlan.IsSend = false;
            smsPlan.SendTime = null;
            smsPlan.IssueTime = DateTime.Now;
            int result = _dalPlan.AddSmsPlan(smsPlan);
            if(result == 1)
            {
                validateResultInfo.IsSucceed = true;
                validateResultInfo.ErrorMessage = "发送成功";
                validateResultInfo.SmsCenterPlanId = smsPlan.PlanId;
                validateResultInfo.AccountMoney = validateResultInfo.AccountMoney - validateResultInfo.CountFee;

                validateResultInfo.SuccessCount = validateResultInfo.MobileNumberCount;
                validateResultInfo.PhsSuccessCount = validateResultInfo.PhsNumberCount;

                //扣除费用
                _dalAccount.UpdateSmsAccountAmount(smsPlan.SmsAccount.AccountId, smsPlan.SmsAccount.AppKey, false,
                                                   validateResultInfo.CountFee);
            }
            else
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "写短信发送信息时发生错误，请联系管理员";
                return validateResultInfo;
            }

            return validateResultInfo;
        }

        #endregion
    }
}
