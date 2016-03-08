using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SmsStructure;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 出回团提醒短信任务业务逻辑
    /// </summary>
    /// 周文超 2011-09-23
    public class BSmsTourTimeTask : BLLBase
    {
        private readonly IDAL.SmsStructure.ISmsTourTimeTask _dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.SmsStructure.ISmsTourTimeTask>();

        /// <summary>
        /// 添加出回团提醒短信任务
        /// </summary>
        /// <param name="model">出回团提醒短信任务实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddSmsTourTimeTask(MSmsTourTimeTask model)
        {
            if (model == null || string.IsNullOrEmpty(model.TourId) || string.IsNullOrEmpty(model.OrderId) || string.IsNullOrEmpty(model.TravellerId) || string.IsNullOrEmpty(model.CompanyId))
                return 0;

            model.IsSend = false;
            return _dal.AddSmsTourTimeTask(new List<MSmsTourTimeTask> { model });
        }

        /// <summary>
        /// 添加出回团提醒短信任务
        /// </summary>
        /// <param name="list">出回团提醒短信任务实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddSmsTourTimeTask(IList<MSmsTourTimeTask> list)
        {
            if (list == null || list.Count < 1)
                return 0;

            return _dal.AddSmsTourTimeTask(list);
        }

        /// <summary>
        /// 修改出回团提醒短信任务
        /// </summary>
        /// <param name="model">出回团提醒短信任务实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int UpdateSmsTourTimeTask(MSmsTourTimeTask model)
        {
            if (model == null || string.IsNullOrEmpty(model.TourId) || string.IsNullOrEmpty(model.OrderId) || string.IsNullOrEmpty(model.TravellerId))
                return 0;

            return _dal.UpdateSmsTourTimeTask(model);
        }

        /// <summary>
        /// 删除出回团提醒短信任务
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="orderId">订单编号</param>
        /// <param name="travellerId">游客编号</param>
        /// <returns>返回1成功，其他失败</returns>
        public int DeleteSmsTourTimeTask(string tourId, string orderId, params string[] travellerId)
        {
            if (string.IsNullOrEmpty(tourId) || string.IsNullOrEmpty(orderId)
                || travellerId == null || travellerId.Length < 1)
                return 0;

            return _dal.DeleteSmsTourTimeTask(tourId, orderId, travellerId);
            ;
        }
    }
}
