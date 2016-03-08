using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EyouSoft.Model.SmsStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace EyouSoft.DAL.SmsStructure
{
    /// <summary>
    /// 出回团提醒短信任务数据访问
    /// </summary>
    /// 周文超2011-09-23
    public class DSmsTourTimeTask : Toolkit.DAL.DALBase, IDAL.SmsStructure.ISmsTourTimeTask
    {
        #region private member

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 出回团提醒短信任务添加Sql
        /// </summary>
        private const string SqlSmsTourTimeTaskAdd = @" INSERT INTO [tbl_SmsTourTimeTask]
           ([TaskId]
           ,[TourId]
           ,[OrderId]
           ,[TravellerId]
           ,[Traveller]
           ,[Code]
           ,[Time]
           ,[TaskType]
           ,[IsSend]
           ,[CompanyId])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}'
           ,'{5}'
           ,'{6}'
           ,{7}
           ,'{8}'
           ,'{9}'); ";

        /// <summary>
        /// 出回团提醒短信任务删除Sql
        /// </summary>
        private const string SqlSmsTourTimeTaskDelete = @" DELETE FROM [tbl_SmsTourTimeTask] WHERE TourId = @TourId and OrderId = @OrderId ";

        /// <summary>
        /// 出回团提醒短信任务修改Sql
        /// </summary>
        public const string SqlSmsTourTimeTaskUpdate = @" update [tbl_SmsTourTimeTask] set 
            [Traveller] = @Traveller
            ,[Code] = @Code
            ,[Time] = @Time 
        where [TourId] = @TourId 
            and [OrderId] = @OrderId 
            and [TravellerId] = @TravellerId 
            and [TaskType] = @TaskType ";

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsTourTimeTask()
        {
            _db = SystemStore;
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

            var strSql = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.TourId) || string.IsNullOrEmpty(t.OrderId)
                    || string.IsNullOrEmpty(t.TravellerId))
                    continue;

                t.TaskId = Guid.NewGuid().ToString();
                strSql.AppendFormat(SqlSmsTourTimeTaskAdd, t.TaskId, t.TourId, t.OrderId, t.TravellerId, t.Traveller,
                                    t.Code, t.Time, (int)t.TaskType, t.IsSend ? "1" : "0", t.CompanyId);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            return Toolkit.DAL.DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
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

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsTourTimeTaskUpdate);
            _db.AddInParameter(dc, "Traveller", DbType.String, model.Traveller);
            _db.AddInParameter(dc, "Code", DbType.String, model.Code);
            _db.AddInParameter(dc, "Time", DbType.DateTime, model.Time);
            _db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.OrderId);
            _db.AddInParameter(dc, "TravellerId", DbType.AnsiStringFixedLength, model.TravellerId);
            _db.AddInParameter(dc, "TaskType", DbType.Byte, model.TaskType);

            return Toolkit.DAL.DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
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

            var strSql = new StringBuilder(SqlSmsTourTimeTaskDelete);
            if (travellerId.Length == 1)
            {
                strSql.AppendFormat(" and TravellerId = '{0}' ", travellerId[0]);
            }
            else
            {
                string strIds = travellerId.Where(t => !string.IsNullOrEmpty(t)).Aggregate(string.Empty, (current, t) => current + "'" + t + "',");
                strSql.AppendFormat(" and TravellerId in ({0}) ", strIds.TrimEnd(','));
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            return Toolkit.DAL.DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }
    }
}
