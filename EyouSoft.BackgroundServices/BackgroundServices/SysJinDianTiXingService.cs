using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Services.BackgroundServices;
using EyouSoft.BackgroundServices.IDAL;

namespace EyouSoft.BackgroundServices.BackgroundServices
{
    /// <summary>
    /// 进店提醒服务
    /// </summary>
    public class SysJinDianTiXingService:BackgroundServiceBase, IBackgroundService
    {
        #region private member
        private readonly ISysJinDianTiXingService _dal;
        private Queue<EyouSoft.Model.SmsStructure.MSmsSetting> _queue;

        /// <summary>
        /// 发送进店提醒短信
        /// </summary>
        /// <param name="info">短信配置信息实体</param>
        void SendSms(EyouSoft.Model.SmsStructure.MSmsSetting info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)) return;

            var items = _dal.GetSmsJinDianTiXings(info.CompanyId, info.BeforeDay);
            if (items == null || items.Count < 1) return;

            EyouSoft.Toolkit.Utils.WLog(string.Format("系统{0}发送{1}条进店提醒短信", info.CompanyId, items.Count), "/log/service.sms.jindian.log");

            foreach (var t in items)
            {
                if (t == null || string.IsNullOrEmpty(t.CompanyId) || t.DaoYous == null || t.DaoYous.Count < 1) continue;

                SendMessage(t, info.SendSmsUserId);
            }
        }
        
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="info"></param>
        /// <param name="sendSmsUserId"></param>
        void SendMessage(Model.SmsStructure.MSmsJinDianTiXingPlanInfo info, string sendSmsUserId)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || info.DaoYous == null || info.DaoYous.Count < 1) return;

            string s = string.Format("[导游姓名]，团号：{0}，出团日期：{1}，", info.TourCode, info.LDate.ToString("yyyy-MM-dd"));
            if (info.GouWus != null && info.GouWus.Count > 0)
            {
                s += "需进购物店如下：";
                foreach (var item in info.GouWus)
                {
                    s += item.Name+" "+item.Address + "，";
                }
            }
            s += "进店后请及时进行报账。";

            foreach (var t in info.DaoYous)
            {
                var sendMessage = new EyouSoft.BackgroundServices.SmsApi.MSendMessage
                {
                    CompanyId = info.CompanyId,
                    Mobiles = new[] { new EyouSoft.BackgroundServices.SmsApi.MSmsNumber { Code = t.Mobile } },
                    SendChannel = 0,
                    SendTime = DateTime.Now,
                    SendType = EyouSoft.BackgroundServices.SmsApi.SendType.直接发送,
                    SmsContent = s.Replace("[导游姓名]", t.Name),
                    UserFullName = string.Empty,//直接包含在内容中
                    UserId = sendSmsUserId,
                    LeiXing = EyouSoft.BackgroundServices.SmsApi.LeiXing.进店提醒
                };

                SmsUtils.GetSmsApi().SendMessage(sendMessage);
            }
        }
        #endregion

        #region constructor
        public SysJinDianTiXingService(IPluginService pluginService, ISysJinDianTiXingService sysJinDianTiXingService)
            : base(pluginService)
        {
            _dal = sysJinDianTiXingService;
            ID = new Guid("{6067A839-3FE1-4f67-AD62-F458B7C54227}");
            Name = "系统-进店提醒服务";
            Category = "Background Services";
        }
        #endregion

        #region IBackgroundService 成员
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
            EyouSoft.Toolkit.Utils.WLog("进店提醒服务开启", "/log/service.sms.jindian.log");
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
                    EyouSoft.Toolkit.Utils.WLog(string.Format("进店提醒服务读取到{0}个系统需要发送进店提醒短信", _queue.Count), "/log/service.sms.jindian.log");
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
                EyouSoft.Toolkit.Utils.WLog(e.Message + e.Source + e.StackTrace, "/log/service.sms.jindian.exception.log");
            }
        }

        #endregion
    }
}
