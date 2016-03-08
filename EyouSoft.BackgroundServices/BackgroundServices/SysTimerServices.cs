using System;
using System.Collections.Generic;
using EyouSoft.BackgroundServices.IDAL;
using EyouSoft.Model.EnumType.SmsStructure;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 定时短信服务
    /// </summary>
    public class SysTimerServices : BackgroundServiceBase, IBackgroundService
    {
        #region private member

        private readonly ISysTimerServices _dal;
        private Queue<Model.SmsStructure.MSmsTimerTask> _queue;

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="info">计划信息</param>
        private void SendSms(Model.SmsStructure.MSmsTimerTask info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || string.IsNullOrEmpty(info.Content)
                || info.Number == null)
                return;

            var sendMessage = new EyouSoft.BackgroundServices.SmsApi.MSendMessage();
            var number = new EyouSoft.BackgroundServices.SmsApi.MSmsNumber[info.Number.Count];
            for (int i = 0; i < info.Number.Count; i++)
            {
                if (info.Number[i] == null)
                    continue;
                number[i] = new EyouSoft.BackgroundServices.SmsApi.MSmsNumber
                                {
                                    Code = info.Number[i].Code,
                                    Type =
                                        (EyouSoft.BackgroundServices.SmsApi.MobileType)
                                        Convert.ToInt32(info.Number[i].Type)
                                };
            }
            sendMessage.CompanyId = info.CompanyId;
            sendMessage.Mobiles = number;
            sendMessage.SendChannel = info.Channel;
            sendMessage.SendTime = info.SendTime;
            sendMessage.SendType = EyouSoft.BackgroundServices.SmsApi.SendType.直接发送;
            sendMessage.SmsContent = info.Content;
            sendMessage.UserFullName = string.Empty;
            sendMessage.UserId = info.OperatorId;
            sendMessage.LeiXing = EyouSoft.BackgroundServices.SmsApi.LeiXing.定时发送;

            EyouSoft.BackgroundServices.SmsApi.MSendResult result = SmsUtils.GetSmsApi().SendMessage(sendMessage);

            if (result.IsSucceed == false)
            {
                IList<Model.SmsStructure.MSmsTaskState> list = new List<Model.SmsStructure.MSmsTaskState>
                                                                   {
                                                                       new Model.SmsStructure.MSmsTaskState
                                                                           {
                                                                               RealTime = null,
                                                                               Status = SendStatus.发送失败,
                                                                               StatusDesc = result.ErrorMessage,
                                                                               TaskId = info.TaskId
                                                                           }
                                                                   };

                _dal.UpdateSmsTimerTaskState(list);
            }
        }

        #endregion

        #region IBackgroundService 成员

        #region BackgroundServiceBase成员

        public SysTimerServices(IPluginService pluginService, ISysTimerServices smsTimerService)
            : base(pluginService)
        {
            _dal = smsTimerService;
            ID = new Guid("{3caef7f7-8737-4673-9a64-b2d3fbea5da0}");
            Name = "系统-定时短信服务";
            Category = "Background Services";
        }

        #endregion

        public bool ExecuteOnAll
        {
            get
            {
                return bool.Parse(GetSetting("ExecuteOnAll"));
            }
            set
            {
                SaveSetting("ExecuteOnAll", value.ToString());
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return new TimeSpan(long.Parse(GetSetting("Interval")));
            }
            set
            {
                SaveSetting("Interval", value.Ticks.ToString());
            }
        }

        public void Run()
        {
            EyouSoft.Toolkit.Utils.WLog("定时短信服务开启", "/log/service.sms.timer.log");

            try
            {
                //获取要发送的短信集合
                if (_queue == null || _queue.Count == 0)
                {
                    _queue = _dal.GetSends();
                }

                //发送短信
                if (_queue != null && _queue.Count > 0)
                {
                    EyouSoft.Toolkit.Utils.WLog(string.Format("定时短信服务读取到{0}条短信需要发送", _queue.Count), "/log/service.sms.timer.log");
                    while (true)
                    {
                        if (_queue.Count == 0) break;
                        var info = _queue.Dequeue();
                        SendSms(info);
                    }
                }
            }
            catch (Exception e)
            {
                EyouSoft.Toolkit.Utils.WLog(e.Message + e.Source + e.StackTrace, "/log/service.sms.timer.exception.log");
            }
        }

        #endregion
    }
}
