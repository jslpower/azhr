using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BackgroundServices.IDAL;
using EyouSoft.Model.SmsStructure;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 回团提醒服务
    /// </summary>
    /// 周文超 2011-09-26
    public class SysBackRemindService : BackgroundServiceBase, IBackgroundService
    {
        #region private member

        private readonly ISysBackRemindService _dal;
        private Queue<MSmsSetting> _queue;

        /// <summary>
        /// 发送回团提醒短信
        /// </summary>
        /// <param name="model">出回团提醒短信任务实体</param>
        /// <param name="message">短信内容（未替换）</param>
        /// <param name="sendSmsUserId">发送短信用户编号</param>
        private void SendMessage(MSmsTourTimePlan model, string message, string sendSmsUserId)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || model.Traveller == null
                || model.Traveller.Count < 1 || string.IsNullOrEmpty(message))
                return;

            string smscontent = message.Replace("[线路名称]", model.RouteName).Replace("[回团时间]", model.BackTime.ToString("yyyy-MM-dd")).Replace("[团号]", model.TourCode);

            foreach (var t in model.Traveller)
            {
                var sendMessage = new EyouSoft.BackgroundServices.SmsApi.MSendMessage
                {
                    CompanyId = model.CompanyId,
                    Mobiles =
                        new[] { new EyouSoft.BackgroundServices.SmsApi.MSmsNumber { Code = t.Code } },
                    SendChannel = 0,
                    SendTime = DateTime.Now,
                    SendType = EyouSoft.BackgroundServices.SmsApi.SendType.直接发送,
                    SmsContent = smscontent.Replace("[游客姓名]", t.Traveller),
                    UserFullName = string.Empty,//直接包含在内容中
                    UserId = sendSmsUserId,
                    LeiXing = EyouSoft.BackgroundServices.SmsApi.LeiXing.回团通知
                };

                SmsUtils.GetSmsApi().SendMessage(sendMessage);
            }
        }

        /// <summary>
        /// 发送回团提醒短信
        /// </summary>
        /// <param name="model">回团提醒短信配置实体</param>
        private void SendSms(MSmsSetting model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return;

            var list = _dal.GetSmsTourTimePlan(model.CompanyId, model.BeforeDay);
            if (list == null || list.Count < 1)
                return;

            EyouSoft.Toolkit.Utils.WLog(string.Format("系统{0}发送{1}条回团提醒短信", model.CompanyId, list.Count), "/log/service.sms.rtour.log");

            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.CompanyId) || t.Traveller == null || t.Traveller.Count < 1)
                    continue;

                SendMessage(t, model.Message, model.SendSmsUserId);
            }
        }

        #endregion

        #region IBackgroundService 成员

        #region BackgroundServiceBase  成员

        public SysBackRemindService(IPluginService pluginService, ISysBackRemindService sysBackRemindService)
            : base(pluginService)
        {
            _dal = sysBackRemindService;
            ID = new Guid("{5986fac1-facc-4e15-a2f5-73596b2f41cc}");
            Name = "系统-回团提醒服务";
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
            EyouSoft.Toolkit.Utils.WLog("回团提醒服务开启", "/log/service.sms.rtour.log");
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
                    EyouSoft.Toolkit.Utils.WLog(string.Format("回团提醒服务读取到{0}个系统需要发送回团提醒短信", _queue.Count), "/log/service.sms.rtour.log");
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
                EyouSoft.Toolkit.Utils.WLog(e.Message + e.Source + e.StackTrace, "/log/service.sms.rtour.exception.log");
            }
        }

        #endregion
    }
}
