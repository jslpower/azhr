using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.BackgroundServices.DAL
{
    /// <summary>
    /// 短信中心发送短信服务数据访问
    /// </summary>
    /// 周文超 2011-09-22
    public class SmsCenterService : DALBase, EyouSoft.BackgroundServices.IDAL.ISmsCenterService
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 短信发送失败号码组添加Sql
        /// </summary>
        private const string SqlSmsPlanLose114Add =
            @" Insert Into [tbl_SmsPlanLose114] ([PlanId],[ErrorCode],[Code],[IssueTime]) values (@PlanId,@ErrorCode,@Code,@IssueTime); ";

        /// <summary>
        /// 根据SqlXML构造短信接收号码集合
        /// </summary>
        /// <param name="sqlXml">sqlXml</param>
        /// <returns></returns>
        private IList<Model.BackgroundServices.MSmsNumber> GetSmsCenterPlanNumber(string sqlXml)
        {
            IList<Model.BackgroundServices.MSmsNumber> list = null;
            if (string.IsNullOrEmpty(sqlXml))
                return list;

            XElement xRoot = XElement.Parse(sqlXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)
                return list;

            list = new List<Model.BackgroundServices.MSmsNumber>();
            foreach (var t in xRows)
            {
                var model = new Model.BackgroundServices.MSmsNumber
                {
                    Code = Utils.GetXAttributeValue(t, "Code"),
                    Type = (Model.BackgroundServices.MobileType)Utils.GetInt(Utils.GetXAttributeValue(t, "Type"))
                };

                list.Add(model);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsCenterService()
        {
            _db = SmsStore;
        }

        /// <summary>
        /// 获得要发送的短信
        /// </summary>
        /// <param name="topNum">每次获取待发送短信的条数</param>
        /// <returns></returns>
        public Queue<Model.BackgroundServices.MSmsCenterService> GetSends(int topNum)
        {
            if (topNum <= 0)
                topNum = 10;

            var list = new Queue<Model.BackgroundServices.MSmsCenterService>();
            var strSql = new StringBuilder(" SELECT ");
            strSql.AppendFormat(
                " top {0} [PlanId],[Channel],[Content],[Amount],[UnitPrice],[IssueTime],[IsSend],[SendTime] ", topNum);
            strSql.Append(" ,(select [Code],[Type] from tbl_SmsPlan114 where tbl_SmsPlan114.PlanId = tbl_SmsPlan.PlanId for xml raw,root('root')) as number ");
            strSql.Append(" FROM [tbl_SmsPlan] ");
            strSql.Append(" where [IsSend] = @IsSend ");
            strSql.Append(" ORDER BY [IssueTime] asc ");
            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "0");

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                Model.BackgroundServices.MSmsCenterService model;
                while (dr.Read())
                {
                    model = new Model.BackgroundServices.MSmsCenterService();
                    if (!dr.IsDBNull(dr.GetOrdinal("PlanId")))
                        model.PlanId = dr.GetString(dr.GetOrdinal("PlanId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Channel")))
                        model.Channel = (Model.BackgroundServices.Channel)dr.GetByte(dr.GetOrdinal("Channel"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        model.SendAmount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UnitPrice")))
                        model.UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSend")))
                        model.IsSend = GetBoolean(dr.GetString(dr.GetOrdinal("IsSend")));
                    if (!dr.IsDBNull(dr.GetOrdinal("SendTime")))
                        model.SendTime = dr.GetDateTime(dr.GetOrdinal("SendTime"));
                    else
                        model.SendTime = null;
                    if (!dr.IsDBNull(dr.GetOrdinal("number")))
                        model.Number = GetSmsCenterPlanNumber(dr.GetString(dr.GetOrdinal("number")));

                    list.Enqueue(model);
                }
            }

            IList<string> smsPlans = (from t in list where t != null && !string.IsNullOrEmpty(t.PlanId) select t.PlanId).ToList();
            UpdateSmsPlanState(true, DateTime.Now, smsPlans.ToArray());

            return list;
        }

        /// <summary>
        /// 更新待发送短信的状态
        /// </summary>
        /// <param name="isSend">是否已发送（T为已发送，F为未发送）</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="smsPlanId">待发送短信的编号</param>
        /// <returns>返回1成功，其他失败</returns>
        public int UpdateSmsPlanState(bool isSend, DateTime? sendTime, params string[] smsPlanId)
        {
            if (smsPlanId == null || smsPlanId.Length < 1)
                return 0;

            var strSal = new StringBuilder(" update [tbl_SmsPlan] set [IsSend] = @IsSend,[SendTime] = @SendTime ");
            if (smsPlanId.Length == 1)
                strSal.AppendFormat(" where [PlanId] = '{0}' ", smsPlanId[0]);
            else
            {
                string strIds = smsPlanId.Where(s => !string.IsNullOrEmpty(s)).Aggregate(string.Empty, (current, s) => current + "'" + s + "',");
                strSal.AppendFormat(" where [PlanId] in ({0}) ", strIds.TrimEnd(','));
            }
            DbCommand dc = _db.GetSqlStringCommand(strSal.ToString());
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, isSend ? "1" : "0");
            if (sendTime.HasValue)
                _db.AddInParameter(dc, "SendTime", DbType.DateTime, sendTime.Value);
            else
                _db.AddInParameter(dc, "SendTime", DbType.DateTime, DBNull.Value);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 写发送失败的号码组
        /// </summary>
        /// <param name="smsPlanId">发送短信编号</param>
        /// <param name="errorCode">接口返回的错误代码</param>
        /// <param name="code">本次发送失败的号码组</param>
        /// <returns></returns>
        public int AddSmsPlanLose(string smsPlanId, int errorCode, string code)
        {
            if (string.IsNullOrEmpty(smsPlanId) || string.IsNullOrEmpty(code))
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPlanLose114Add);
            _db.AddInParameter(dc, "PlanId", DbType.AnsiStringFixedLength, smsPlanId);
            _db.AddInParameter(dc, "ErrorCode", DbType.Int32, errorCode);
            _db.AddInParameter(dc, "Code", DbType.String, code);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }
    }
}
