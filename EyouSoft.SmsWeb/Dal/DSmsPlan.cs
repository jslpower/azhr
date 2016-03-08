using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace EyouSoft.SmsWeb.Dal
{
    /// <summary>
    /// 短信发送表数据访问
    /// </summary>
    /// 周文超 2011-09-19
    public class DSmsPlan : DALBase
    {
        #region private member

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 短信发送添加Sql
        /// </summary>
        private const string SqlSmsPlanAdd = @" INSERT INTO [tbl_SmsPlan]
           ([PlanId]
           ,[Channel]
           ,[Content]
           ,[Amount]
           ,[UnitPrice]
           ,[IssueTime]
           ,[IsSend]
           ,[SendTime]
           ,[AccountId])
     VALUES
           (@PlanId
           ,@Channel
           ,@Content
           ,@Amount
           ,@UnitPrice
           ,@IssueTime
           ,@IsSend
           ,@SendTime
           ,@AccountId); ";

        /// <summary>
        /// 发送短信接收号码添加Sql
        /// </summary>
        private const string SqlSmsPlan114Add = @" INSERT INTO [tbl_SmsPlan114]
           ([PlanId]
           ,[Code]
           ,[Type])
     VALUES
           ('{0}'
           ,'{1}'
           ,{2}); ";

        /// <summary>
        /// 生成短信发送号码添加Sql
        /// </summary>
        /// <param name="planId">短信发送编号</param>
        /// <param name="list">短信发送号码集合</param>
        /// <returns>短信发送号码添加Sql</returns>
        private string GetAddSmsPlanNumberSql(string planId, List<Model.MSmsNumber> list)
        {
            if (string.IsNullOrEmpty(planId) || list == null || list.Count <= 0)
                return string.Empty;

            var strSql = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.Code))
                    continue;

                strSql.AppendFormat(SqlSmsPlan114Add, planId, t.Code
                                    , (int) t.Type);
            }

            return strSql.ToString();
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsPlan()
        {
            _db = SmsStore;
        }

        /// <summary>
        /// 添加短信发送信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddSmsPlan(Model.MSmsPlan model)
        {
            if (model == null || model.SmsAccount == null || string.IsNullOrEmpty(model.SmsAccount.AccountId)
               || model.Number == null || model.Number.Count < 1)
                return 0;

            model.PlanId = Guid.NewGuid().ToString();

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPlanAdd + GetAddSmsPlanNumberSql(model.PlanId, model.Number));
            _db.AddInParameter(dc, "PlanId", DbType.AnsiStringFixedLength, model.PlanId);
            _db.AddInParameter(dc, "Channel", DbType.Byte, (int)model.Channel);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "Amount", DbType.Decimal, model.SendAmount);
            _db.AddInParameter(dc, "UnitPrice", DbType.Decimal, model.UnitPrice);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, model.IsSend ? "1" : "0");
            if (model.SendTime.HasValue)
                _db.AddInParameter(dc, "SendTime", DbType.DateTime, model.SendTime.Value);
            else
                _db.AddInParameter(dc, "SendTime", DbType.DateTime, DBNull.Value);
            _db.AddInParameter(dc, "AccountId", DbType.AnsiStringFixedLength, model.SmsAccount.AccountId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }
    }
}
