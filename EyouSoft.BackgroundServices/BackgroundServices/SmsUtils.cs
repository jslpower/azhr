using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EyouSoft.Services.BackgroundServices
{
    /// <summary>
    /// 短信服务通用方法类
    /// </summary>
    public class SmsUtils
    {
        /// <summary>
        /// 获取安全调用后的短信服务对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.BackgroundServices.SmsApi.SmsApi GetSmsApi()
        {
            var api = new EyouSoft.BackgroundServices.SmsApi.SmsApi();
            var apiHeader = new EyouSoft.BackgroundServices.SmsApi.SmsApiSoapHeader
            {
                SecretKey = Toolkit.ConfigHelper.ConfigClass.GetConfigString("SmsApi_ApiKey")
            };
            api.SmsApiSoapHeaderValue = apiHeader;

            return api;
        }

        /// <summary>
        /// 获取短信中心每次获取待发送的短信数量
        /// </summary>
        /// <returns></returns>
        public static int GetSmsCenterTopNum()
        {
            return Toolkit.Utils.GetInt(Toolkit.ConfigHelper.ConfigClass.GetConfigString("SmsCenter_TopNum"));
        }

        /// <summary>
        /// 根据短信通道获取对应的用户名和密码
        /// </summary>
        /// <param name="channel">短信通道</param>
        public static Model.BackgroundServices.MSmsChannel GetSmsChannelByChannel(Model.BackgroundServices.Channel channel)
        {
            return new Model.BackgroundServices.MSmsChannelList()[(int)channel];
        }
    }
}
