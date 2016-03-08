using System.Collections.Generic;
using EyouSoft.Model.SmsStructure;

namespace EyouSoft.IDAL.SmsStructure
{
    /// <summary>
    /// 出回团提醒短信任务数据访问接口
    /// </summary>
    /// 周文超2011-09-23
    public interface ISmsTourTimeTask
    {
        /// <summary>
        /// 添加出回团提醒短信任务
        /// </summary>
        /// <param name="list">出回团提醒短信任务实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddSmsTourTimeTask(IList<MSmsTourTimeTask> list);

        /// <summary>
        /// 修改出回团提醒短信任务
        /// </summary>
        /// <param name="model">出回团提醒短信任务实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int UpdateSmsTourTimeTask(MSmsTourTimeTask model);

        /// <summary>
        /// 删除出回团提醒短信任务
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="orderId">订单编号</param>
        /// <param name="travellerId">游客编号</param>
        /// <returns>返回1成功，其他失败</returns>
        int DeleteSmsTourTimeTask(string tourId, string orderId, params string[] travellerId);
    }
}
