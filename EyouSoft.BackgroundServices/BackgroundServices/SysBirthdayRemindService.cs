using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BackgroundServices.IDAL;
using EyouSoft.Model.SmsStructure;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 生日提醒服务
    /// </summary>
    /// 周文超 2011-09-26
    public class SysBirthdayRemindService : BackgroundServiceBase, IBackgroundService
    {
        #region private member

        private readonly ISysBirthdayRemindService _dal;
        private Queue<MSmsSetting> _queue;

        /// <summary>
        /// 发送生日提醒短信
        /// </summary>
        /// <param name="model">生日提醒短信配置实体</param>
        private void SendSms(MSmsSetting model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return;

            var list = _dal.GetSmsBirthdayRemindPlan(model.CompanyId, model.BeforeDay);
            if (list == null || list.Count < 1)
                return;

            EyouSoft.Toolkit.Utils.WLog(string.Format("系统{0}发送{1}条生日提醒短信", model.CompanyId, list.Count), "/log/service.sms.birthday.log");

            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.CompanyId) || string.IsNullOrEmpty(t.MobilePhone))
                    continue;

                var sendMessage = new EyouSoft.BackgroundServices.SmsApi.MSendMessage
                {
                    CompanyId = t.CompanyId,
                    Mobiles =
                        new[] { new EyouSoft.BackgroundServices.SmsApi.MSmsNumber { Code = t.MobilePhone } },
                    SendChannel = 0,
                    SendTime = DateTime.Now,
                    SendType = EyouSoft.BackgroundServices.SmsApi.SendType.直接发送,
                    SmsContent = model.Message.Replace("[姓名]", t.Name).Replace("[生日]", t.Birthday.ToString("MM-dd")),
                    UserFullName = string.Empty,//直接包含在内容中
                    UserId = model.SendSmsUserId,
                    LeiXing= EyouSoft.BackgroundServices.SmsApi.LeiXing.生日提醒
                };

                SmsUtils.GetSmsApi().SendMessage(sendMessage);
            }
        }

        #endregion

        #region IBackgroundService 成员

        #region BackgroundServiceBase  成员

        public SysBirthdayRemindService(IPluginService pluginService, ISysBirthdayRemindService sysBirthdayRemindService)
            : base(pluginService)
        {
            _dal = sysBirthdayRemindService;
            ID = new Guid("{d12449e7-f4a6-465b-b56c-8ee9ff9e8f03}");
            Name = "系统-生日提醒服务";
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
            EyouSoft.Toolkit.Utils.WLog("生日提醒服务开启", "/log/service.sms.birthday.log");
            try
            {
                //获取要发送的短信集合
                if (_queue == null || _queue.Count == 0)
                {
                    _queue = _dal.GetSmsSetting();
                }

                //发送短信
                if (_queue != null && _queue.Count > 0)
                {
                    EyouSoft.Toolkit.Utils.WLog(string.Format("生日提醒服务读取到{0}个系统需要发送生日提醒短信", _queue.Count), "/log/service.sms.birthday.log");
                    while (true)
                    {
                        if (_queue.Count == 0) break;
                        var info = _queue.Dequeue();

                        if (info != null) SendSms(info);
                    }
                }
            }
            catch (Exception e)
            {
                EyouSoft.Toolkit.Utils.WLog(e.Message + e.Source + e.StackTrace, "/log/service.sms.birthday.exception.log");
            }
        }

        #endregion
    }
}
