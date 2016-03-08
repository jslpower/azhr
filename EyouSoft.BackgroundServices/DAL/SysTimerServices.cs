using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EyouSoft.Model.EnumType.SmsStructure;
using EyouSoft.Model.SmsStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.BackgroundServices.DAL
{
    /// <summary>
    /// 系统定时短信数据访问
    /// </summary>
    public class SysTimerServices : DALBase, EyouSoft.BackgroundServices.IDAL.ISysTimerServices
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 根据SqlXML构造定时短信接收号码集合
        /// </summary>
        /// <param name="sqlXml">sqlXml</param>
        /// <returns></returns>
        private IList<MSmsNumber> GetSmsTimerTaskNumber(string sqlXml)
        {
            IList<MSmsNumber> list = null;
            if (string.IsNullOrEmpty(sqlXml))
                return list;

            XElement xRoot = XElement.Parse(sqlXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)
                return list;

            list = new List<MSmsNumber>();
            foreach (var t in xRows)
            {
                var model = new MSmsNumber
                {
                    Code = Utils.GetXAttributeValue(t, "Code"),
                    Type = (MobileType)Utils.GetInt(Utils.GetXAttributeValue(t, "Type"))
                };

                list.Add(model);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysTimerServices()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 获取没有发送的所有的定时短信
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsTimerTask> GetSends()
        {
            var list = new Queue<MSmsTimerTask>();
            var strSql = new StringBuilder(" SELECT [TaskId],[CompanyId],[Content],[OperatorId],[SendTime],[Channel],[Status],[StatusDesc] ");
            strSql.Append(" ,(select [Code],[Type] from tbl_SmsTimerTask114 where tbl_SmsTimerTask114.TaskId = tbl_SmsTimerTask.TaskId for xml raw,root('root')) as number ");
            strSql.Append(" FROM [tbl_SmsTimerTask] ");
            strSql.Append(" where [Status] = @Status ");
            strSql.Append(" AND [SendTime] <= GETDATE() ");
            strSql.Append(" ORDER BY [SendTime] asc ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "Status", DbType.Byte, SendStatus.未发送);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                MSmsTimerTask model;
                while (dr.Read())
                {
                    model = new MSmsTimerTask();
                    if (!dr.IsDBNull(dr.GetOrdinal("TaskId")))
                        model.TaskId = dr.GetString(dr.GetOrdinal("TaskId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SendTime")))
                        model.SendTime = dr.GetDateTime(dr.GetOrdinal("SendTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Channel")))
                        model.Channel = dr.GetByte(dr.GetOrdinal("Channel"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Status")))
                        model.Status = (SendStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    if (!dr.IsDBNull(dr.GetOrdinal("StatusDesc")))
                        model.StatusDesc = dr.GetString(dr.GetOrdinal("StatusDesc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("number")))
                        model.Number = GetSmsTimerTaskNumber(dr.GetString(dr.GetOrdinal("number")));

                    list.Enqueue(model);
                }
            }

            UpdateSmsTimerTaskState(list.Select(t => new MSmsTaskState
                                                         {
                                                             TaskId = t.TaskId,
                                                             Status = SendStatus.发送成功,
                                                             StatusDesc = string.Empty,
                                                             RealTime = DateTime.Now
                                                         }).ToList());

            return list;
        }

        /// <summary>
        /// 更新定时短信的发送状态
        /// </summary>
        /// <param name="list">定时短信的发送状态实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int UpdateSmsTimerTaskState(IList<MSmsTaskState> list)
        {
            if (list == null || list.Count < 1)
                return 0;

            var strSql = new StringBuilder(" update tbl_SmsTimerTask set ");
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.TaskId))
                    continue;

                strSql.AppendFormat(" [Status] = {0},[StatusDesc] = '{1}' ", (int)t.Status, t.StatusDesc);
                if (t.RealTime.HasValue)
                    strSql.AppendFormat(" ,[RealTime] = '{0}' ", t.RealTime.Value);
                strSql.AppendFormat(" where TaskId = '{0}' ; ", t.TaskId);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, _db);
        }
    }
}
