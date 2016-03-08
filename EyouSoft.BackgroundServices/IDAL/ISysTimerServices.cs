using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BackgroundServices.IDAL
{
    /// <summary>
    /// 系统定时短信服务接口
    /// </summary>
    public interface ISysTimerServices
    {
        /// <summary>
        /// 获得要发送的短信
        /// </summary>
        /// <returns></returns>
        Queue<Model.SmsStructure.MSmsTimerTask> GetSends();

        /// <summary>
        /// 更新定时短信的发送状态
        /// </summary>
        /// <param name="list">定时短信的发送状态实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int UpdateSmsTimerTaskState(IList<Model.SmsStructure.MSmsTaskState> list);
    }
}
