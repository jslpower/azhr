using System;
using System.Collections.Generic;
using System.Linq;
using EyouSoft.Model.EnumType.SmsStructure;


namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 发送短信业务逻辑
    /// </summary>
    /// 周文超 2011-09-19
    public class BSendMessage : BLLBase
    {
        #region privte member

        /// <summary>
        /// 短信中心业务访问对象
        /// </summary>
        private readonly SmsCenter.SmsCenter _smsCenter;

        /// <summary>
        /// 系统短信明细业务逻辑访问对象
        /// </summary>
        private readonly BSmsDetail _smsBll = new BSmsDetail();

        /// <summary>
        /// 构造安全访问短信中心Api的对象
        /// </summary>
        /// <returns></returns>
        private SmsCenter.SmsCenter GetSmsCenterApi()
        {
            var api = new SmsCenter.SmsCenter();
            var apiHeader = new SmsCenter.SmsCenterApiSoapHeader
            {
                SecretKey = Toolkit.ConfigHelper.ConfigClass.GetConfigString("SmsCenter_ApiKey")
            };

            api.SmsCenterApiSoapHeaderValue = apiHeader;

            return api;
        }

        /// <summary>
        /// 构造短信中心接收号码实体集合
        /// </summary>
        /// <param name="list">系统接收号码实体集合</param>
        /// <returns></returns>
        private List<SmsCenter.MSmsNumber> GetSmsNumber(IList<Model.SmsStructure.MSmsNumber> list)
        {
            List<SmsCenter.MSmsNumber> rList = null;
            if (list == null || list.Count < 1)
                return rList;

            return (from t in list
                    where t != null
                    select
                        new SmsCenter.MSmsNumber { Code = t.Code, Type = (SmsCenter.MobileType)Convert.ToInt32(t.Type) })
                .ToList();
        }

        /// <summary>
        /// 构造短信中心的短信帐号信息实体
        /// </summary>
        /// <param name="companyId">当前系统公司编号</param>
        /// <returns></returns>
        private SmsCenter.MSmsAccountBase GetSmsAccount(string companyId)
        {            
            if (string.IsNullOrEmpty(companyId)) return null;

            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(companyId);

            if (setting == null||setting.SmsConfig==null||!setting.SmsConfig.IsEnabled) return null;

            var info = new EyouSoft.BLL.SmsCenter.MSmsAccountBase();
            info.AccountId = setting.SmsConfig.Account;
            info.AppKey = setting.SmsConfig.AppKey;
            info.AppSecret = setting.SmsConfig.AppSecret;

            return info;
        }

        /// <summary>
        /// 直接发送
        /// </summary>
        /// <param name="sendMessage">发送短信实体</param>
        /// <returns></returns>
        private Model.SmsStructure.MSendResult DirectlySend(Model.SmsStructure.MSendMessage sendMessage)
        {
            var sysModel = new Model.SmsStructure.MSendResult();
            if (sendMessage == null)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "没有构造发送短信实体";

                return sysModel;
            }
            var smsPlan = new SmsCenter.MSmsPlan
                              {
                                  Channel = (SmsCenter.Channel)Convert.ToInt32(sendMessage.SendChannel),
                                  Content = sendMessage.SmsContentSendComplete,
                                  IssueTime = DateTime.Now,
                                  Number = GetSmsNumber(sendMessage.Mobiles).ToArray(),
                                  PlanId = string.Empty,
                                  SendAmount = 0,
                                  SmsAccount = GetSmsAccount(sendMessage.CompanyId),
                                  IsSend = false,
                                  SendTime = null,
                                  UnitPrice = 0
                              };


            SmsCenter.MSendResult smsModel = _smsCenter.SendMessage(smsPlan);
            if (smsModel == null)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "未知错误";

                return sysModel;
            }

            //写系统发送短信明细
            if (smsModel.IsSucceed)
            {
                var detailModel = new Model.SmsStructure.MSmsDetail
                                      {
                                          Amount = smsModel.CountFee,
                                          Channel = sendMessage.SendChannel,
                                          CompanyId = sendMessage.CompanyId,
                                          Content = sendMessage.SmsContentSendComplete,
                                          IssueTime = DateTime.Now,
                                          Number = sendMessage.Mobiles,
                                          OperatorId = sendMessage.UserId,
                                          PlanId = string.Empty,
                                          Status = SendStatus.发送成功,
                                          StatusDesc = string.Empty,
                                          LeiXing = sendMessage.LeiXing
                                      };
                _smsBll.AddSmsDetail(detailModel);
            }

            sysModel.AccountMoney = smsModel.AccountMoney;
            sysModel.CountFee = smsModel.CountFee;
            sysModel.ErrorMessage = smsModel.ErrorMessage;
            sysModel.FactCount = smsModel.FactCount;
            sysModel.IsSucceed = smsModel.IsSucceed;
            sysModel.MobileNumberCount = smsModel.MobileNumberCount;
            sysModel.PhsFactCount = smsModel.PhsFactCount;
            sysModel.PhsNumberCount = smsModel.PhsNumberCount;
            sysModel.PhsSuccessCount = smsModel.PhsSuccessCount;
            sysModel.SmsCenterPlanId = smsModel.SmsCenterPlanId;
            sysModel.SuccessCount = smsModel.SuccessCount;

            return sysModel;
        }

        /// <summary>
        /// 定时发送
        /// </summary>
        /// <param name="sendMessage">发送短信实体</param>
        /// <returns></returns>
        private Model.SmsStructure.MSendResult TimeSend(Model.SmsStructure.MSendMessage sendMessage)
        {
            var sysModel = new Model.SmsStructure.MSendResult();
            if (sendMessage == null || sendMessage.SendTime <= DateTime.Now)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "没有构造发送短信实体";

                return sysModel;
            }
            var smsPlan = new SmsCenter.MSmsPlan
                              {
                                  Channel = (SmsCenter.Channel)Convert.ToInt32(sendMessage.SendChannel),
                                  Content = sendMessage.SmsContentSendComplete,
                                  IssueTime = DateTime.Now,
                                  Number = GetSmsNumber(sendMessage.Mobiles).ToArray(),
                                  PlanId = string.Empty,
                                  SendAmount = 0,
                                  SmsAccount = GetSmsAccount(sendMessage.CompanyId),
                                  IsSend = false,
                                  SendTime = null,
                                  UnitPrice = 0
                              };

            SmsCenter.MSendResult smsModel = _smsCenter.ValidateSend(smsPlan);

            if (smsModel == null)
            {
                sysModel.IsSucceed = false;
                sysModel.ErrorMessage = "未知错误";
                return sysModel;
            }

            if (smsModel.IsSucceed)
            {
                var taskModel = new Model.SmsStructure.MSmsTimerTask
                                    {
                                        Channel = sendMessage.SendChannel,
                                        CompanyId = sendMessage.CompanyId,
                                        Content = sendMessage.SmsContentSendComplete,
                                        IssueTime = DateTime.Now,
                                        Number = sendMessage.Mobiles,
                                        OperatorId = sendMessage.UserId,
                                        RealTime = null,
                                        SendTime = sendMessage.SendTime,
                                        Status = SendStatus.未发送,
                                        StatusDesc = string.Empty,
                                        TaskId = string.Empty
                                    };

                _smsBll.AddSmsTimerTask(taskModel);
            }

            sysModel.AccountMoney = smsModel.AccountMoney;
            sysModel.CountFee = smsModel.CountFee;
            sysModel.ErrorMessage = smsModel.ErrorMessage;
            sysModel.FactCount = smsModel.FactCount;
            sysModel.IsSucceed = smsModel.IsSucceed;
            sysModel.MobileNumberCount = smsModel.MobileNumberCount;
            sysModel.PhsFactCount = smsModel.PhsFactCount;
            sysModel.PhsNumberCount = smsModel.PhsNumberCount;
            sysModel.PhsSuccessCount = smsModel.PhsSuccessCount;
            sysModel.SmsCenterPlanId = smsModel.SmsCenterPlanId;
            sysModel.SuccessCount = smsModel.SuccessCount;

            return sysModel;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public BSendMessage()
        {
            _smsCenter = GetSmsCenterApi();
        }

        /// <summary>
        /// 发送短信/定时发送
        /// </summary>
        /// <param name="sendMessage">发送短信提交的业务实体</param>
        /// <returns></returns>
        public Model.SmsStructure.MSendResult Send(Model.SmsStructure.MSendMessage sendMessage)
        {
            var model = new Model.SmsStructure.MSendResult();
            if (sendMessage == null)
            {
                model.IsSucceed = false;
                model.ErrorMessage = "没有构造发送短信实体";

                return model;
            }
            if (sendMessage.SendType == SendType.直接发送)
                model = DirectlySend(sendMessage);
            else if (sendMessage.SendType == SendType.定时发送)
                model = TimeSend(sendMessage);

            return model;
        }
    }
}
