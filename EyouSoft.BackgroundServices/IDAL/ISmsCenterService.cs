using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BackgroundServices.IDAL
{
    /// <summary>
    /// 短信中心发送短信服务
    /// </summary>
    /// 周文超 2011-09-22
    public interface ISmsCenterService
    {
        /// <summary>
        /// 获得要发送的短信
        /// </summary>
        /// <param name="topNum">每次获取待发送短信的条数</param>
        /// <returns></returns>
        Queue<Model.BackgroundServices.MSmsCenterService> GetSends(int topNum);

        /// <summary>
        /// 更新待发送短信的状态
        /// </summary>
        /// <param name="isSend">是否已发送（T为已发送，F为未发送）</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="smsPlanId">待发送短信的编号</param>
        /// <returns>返回1成功，其他失败</returns>
        int UpdateSmsPlanState(bool isSend, DateTime? sendTime, params string[] smsPlanId);

        /// <summary>
        /// 写发送失败的号码组
        /// </summary>
        /// <param name="smsPlanId">发送短信编号</param>
        /// <param name="errorCode">接口返回的错误代码</param>
        /// <param name="code">本次发送失败的号码组</param>
        /// <returns></returns>
        int AddSmsPlanLose(string smsPlanId, int errorCode, string code);
    }
}
