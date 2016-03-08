using System;
using System.Collections.Generic;
using System.Text;
using EyouSoft.BackgroundServices.IDAL;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 短信中心发送短信服务
    /// </summary>
    /// 周文超 2011-09-22
    public class SmsCenterService : BackgroundServiceBase, IBackgroundService
    {
        #region private member

        private readonly ISmsCenterService _dal;
        private Queue<Model.BackgroundServices.MSmsCenterService> _queue;

        /// <summary>
        /// 发送短信接口
        /// </summary>
        private EyouSoft.BackgroundServices.SMSStructure.VoSmsServices.Service _sms = new EyouSoft.BackgroundServices.SMSStructure.VoSmsServices.Service();

        /// <summary>
        /// send message enterprise id
        /// </summary>
        private string _enterpriseId = string.Empty;

        /// <summary>
        /// 发送短信超时时异常编号
        /// </summary>
        private readonly int SendTimeOutEventCode = -2147483646;

        /// <summary>
        /// 一次提交到短信接口的最大号码数量
        /// </summary>
        private const int WaitCanMobilesMax = 100;

        /// <summary>
        /// 获得短信服务接口状态
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        private string GetServicesState(int code)
        {
            string state = "";
            switch (code)
            {
                case 0:
                    state = "成功";
                    break;
                case -1:
                    state = "登录错误";
                    break;
                case -2:
                    state = "数据库链接错误";
                    break;
                case -3:
                    state = "语句超长";
                    break;
                case -4:
                    state = "网络超时";
                    break;
                case -5:
                    state = "手机号码个数超过";
                    break;
                case -6:
                    state = "费用不足";
                    break;
                case -7:
                    state = "手机号码错误";
                    break;
                case -8:
                    state = "短信内容为空";
                    break;
                case -9:
                    state = "包含关键字";
                    break;
                case -11:
                    state = "通道错误";
                    break;
            }
            return state;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="info">计划信息</param>
        private void SendSms(Model.BackgroundServices.MSmsCenterService info)
        {
            if (info == null || string.IsNullOrEmpty(info.Content) || info.Number == null || info.Number.Count < 1)
                return;

            //每次调用发送接口时待发送的手机号码
            var waitMobiles = new StringBuilder();
            //总的要发送的手机号码个数
            int waitMobileLength = info.Number.Count;
            for (int indexC = 0; indexC < waitMobileLength; indexC++)
            {
                waitMobiles.AppendFormat("{0},", info.Number[indexC].Code);

                #region 判断是否开始发送短信

                //号码数量达到100或者循环完全部的号码时发送短信
                if ((indexC + 1) % WaitCanMobilesMax == 0 || (indexC + 1) == waitMobileLength)
                {
                    int sendResult = 0;
                    //string sendMsg = "";
                    try
                    {
                        _sms.Timeout = 1000;
                        Model.BackgroundServices.MSmsChannel smsChannel = SmsUtils.GetSmsChannelByChannel(info.Channel);
                        //发送结果返回值 返回int类型的0时成功 返回对应的负数时失败
                        sendResult = _sms.SendSms(_enterpriseId, waitMobiles.ToString().TrimEnd(','), info.Content,
                                                  smsChannel.UserName, smsChannel.Pw);
                        //sendMsg = GetServicesState(sendResult);
                    }
                    catch
                    {
                        //sendMsg = "超时";
                        sendResult = SendTimeOutEventCode;
                    }

                    //发送失败记录发送失败的号码
                    if (sendResult != 0)
                    {
                        _dal.AddSmsPlanLose(info.PlanId, sendResult, waitMobiles.ToString().TrimEnd(','));
                    }

                    //要发送的号码清空
                    waitMobiles.Remove(0, waitMobiles.Length);
                }

                #endregion 判断是否开始发送短信
            }
        }

        #endregion

        #region IBackgroundService 成员

        #region BackgroundServiceBase成员

        public SmsCenterService(IPluginService pluginService, ISmsCenterService smsCenterService)
            : base(pluginService)
        {
            _dal = smsCenterService;
            ID = new Guid("{a901264c-1d63-4463-b145-5ca8e25a2ab5}");
            Name = "短信中心-发送短信服务";
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
            EyouSoft.Toolkit.Utils.WLog("短信中心发送短信后台服务开启", "/log/service.sms.smscenter.log");
            try
            {
                //获取要发送的短信集合
                if (_queue == null || _queue.Count == 0)
                {
                    _queue = _dal.GetSends(SmsUtils.GetSmsCenterTopNum());
                }

                //发送短信
                if (_queue != null && _queue.Count > 0)
                {
                    EyouSoft.Toolkit.Utils.WLog(string.Format("短信中心后台服务读取到{0}条短信需要发送", _queue.Count), "/log/service.sms.smscenter.log");
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
                EyouSoft.Toolkit.Utils.WLog(e.Message + e.Source + e.StackTrace, "/log/service.smscenter.exception.log");
            }
        }

        #endregion
    }
}
