using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EyouSoft.Model.SmsStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace EyouSoft.DAL.SmsStructure
{
    /// <summary>
    /// 短信发送明细数据访问
    /// </summary>
    /// 周文超 2011-09-14
    public class DSmsDetail : DALBase, IDAL.SmsStructure.ISmsDetail
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 短信发送明细添加Sql
        /// </summary>
        private const string SqlSmsDetailAdd = " INSERT INTO [tbl_SmsDetail]([PlanId],[CompanyId],[Channel],[Content],[Amount],[UnitPrice],[IssueTime],[Status],[StatusDesc],[OperatorId],[LeiXing]) VALUES (@PlanId,@CompanyId,@Channel,@Content,@Amount,@UnitPrice,@IssueTime,@Status,@StatusDesc,@OperatorId,@LeiXing); ";

        /// <summary>
        /// 添加短息发送明细号码Sql
        /// </summary>
        private const string SqlSmsDetail114Add = " INSERT INTO [tbl_SmsDetail114]([PlanId],[Code],[Type]) VALUES ('{0}','{1}',{2}); ";

        /// <summary>
        /// 定时短信添加Sql
        /// </summary>
        private const string SqlSmsTimerTaskAdd = " INSERT INTO [tbl_SmsTimerTask]([TaskId],[CompanyId],[Content],[OperatorId],[IssueTime],[SendTime],[Channel],[Status],[StatusDesc],[RealTime]) VALUES (@TaskId,@CompanyId,@Content,@OperatorId,@IssueTime,@SendTime,@Channel,@Status,@StatusDesc,@RealTime); ";

        private const string SqlSmsTimerTask114Add = " INSERT INTO [tbl_SmsTimerTask114]([TaskId],[Code],[Type]) VALUES ('{0}','{1}',{2}); ";

        /// <summary>
        /// 生成添加短信发送号码的Sql语句
        /// </summary>
        /// <param name="planId">短信明细Id</param>
        /// <param name="list">短信明细号码集合</param>
        /// <returns></returns>
        private string GetSmsDetailNumberSql(string planId, IList<MSmsNumber> list)
        {
            if (string.IsNullOrEmpty(planId) || list == null || list.Count < 1)
                return string.Empty;

            var strSql = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.Code))
                    continue;

                strSql.AppendFormat(SqlSmsDetail114Add, planId, t.Code.Trim(), (int)t.Type);
            }

            return strSql.ToString();
        }

        /// <summary>
        /// 生成添加定时短信发送号码的Sql语句
        /// </summary>
        /// <param name="taskId">短信明细Id</param>
        /// <param name="list">短信明细号码集合</param>
        /// <returns></returns>
        private string GetSmsTimerTaskNumberSql(string taskId, IList<MSmsNumber> list)
        {
            if (string.IsNullOrEmpty(taskId) || list == null || list.Count < 1)
                return string.Empty;

            var strSql = new StringBuilder();
            foreach (var t in list)
            {
                if (t == null || string.IsNullOrEmpty(t.Code))
                    continue;

                strSql.AppendFormat(SqlSmsTimerTask114Add, taskId, t.Code.Trim(), (int)t.Type);
            }

            return strSql.ToString();
        }

        /// <summary>
        /// 根据SqlXML获取号码集合
        /// </summary>
        /// <param name="smsNumberSqlXml">SqlXML</param>
        /// <returns></returns>
        private IList<MSmsNumber> GetSmsNumber(string smsNumberSqlXml)
        {
            IList<MSmsNumber> list = null;
            if (string.IsNullOrEmpty(smsNumberSqlXml))
                return list;

            XElement xRoot = XElement.Parse(smsNumberSqlXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)
                return list;

            list = new List<MSmsNumber>();
            foreach (var t in xRows)
            {
                var model = new MSmsNumber
                                       {
                                           Code = Utils.GetXAttributeValue(t, "Code"),
                                           Type =
                                               (Model.EnumType.SmsStructure.MobileType)
                                               Utils.GetInt(Utils.GetXAttributeValue(t, "Type"))
                                       };

                list.Add(model);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsDetail()
        {
            _db = SystemStore;
        }

        #region ISmsDetail成员

        /// <summary>
        /// 添加短信发送明细
        /// </summary>
        /// <param name="model">短信发送明细实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsDetail(MSmsDetail model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || model.Amount <= 0
                || model.Number == null || string.IsNullOrEmpty(model.Content))
                return 0;

            model.PlanId = Guid.NewGuid().ToString();
            DbCommand dc = _db.GetSqlStringCommand(SqlSmsDetailAdd + GetSmsDetailNumberSql(model.PlanId, model.Number));
            _db.AddInParameter(dc, "PlanId", DbType.AnsiStringFixedLength, model.PlanId);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "Channel", DbType.Byte, (byte)model.Channel);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "Amount", DbType.Decimal, model.Amount);
            _db.AddInParameter(dc, "UnitPrice", DbType.Decimal, model.UnitPrice);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(dc, "Status", DbType.Byte, (byte)model.Status);
            _db.AddInParameter(dc, "StatusDesc", DbType.String, model.StatusDesc);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "LeiXing", DbType.Byte, model.LeiXing);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 获取短信发送明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        public IList<MSmsDetail> GetSmsDetailList(int pageSize, int pageIndex, ref int recordCount, string companyId, MQuerySmsDetail model)
        {
            IList<MSmsDetail> list = null;
            if (string.IsNullOrEmpty(companyId))
                return list;

            MSmsDetail tmpModel;

            #region Sql拼接

            string fields = " [PlanId],[CompanyId],[Channel],[Content],[Amount],[UnitPrice],[IssueTime],[Status],[StatusDesc],[OperatorId],(select Code,Type from tbl_SmsDetail114 where tbl_SmsDetail114.PlanId = tbl_SmsDetail.PlanId for xml raw, root('root')) as Number ";
            string tableName = "tbl_SmsDetail";
            string orderByString = " [IssueTime] desc ";
            string identityColumnName = " PlanId ";
            var strWhere = new StringBuilder();
            strWhere.AppendFormat(" CompanyId = '{0}' ", companyId);
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.KeyWord.Trim()))
                    strWhere.AppendFormat(" and Content like '%{0}%' ", model.KeyWord.Trim());
                if (model.Status.HasValue)
                    strWhere.AppendFormat(" and Status = {0} ", (int)model.Status.Value);
                if (model.StartTime.HasValue)
                    strWhere.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", model.StartTime.Value);
                if (model.EndTime.HasValue)
                    strWhere.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", model.EndTime.Value);
            }

            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName
                , identityColumnName, fields, strWhere.ToString(), orderByString))
            {
                list = new List<MSmsDetail>();
                while (dr.Read())
                {
                    tmpModel = new MSmsDetail();
                    if (!dr.IsDBNull(dr.GetOrdinal("PlanId")))
                        tmpModel.PlanId = dr.GetString(dr.GetOrdinal("PlanId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        tmpModel.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Channel")))
                        tmpModel.Channel = dr.GetByte(dr.GetOrdinal("Channel"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        tmpModel.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        tmpModel.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UnitPrice")))
                        tmpModel.UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        tmpModel.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Status")))
                        tmpModel.Status = (Model.EnumType.SmsStructure.SendStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    if (!dr.IsDBNull(dr.GetOrdinal("StatusDesc")))
                        tmpModel.StatusDesc = dr.GetString(dr.GetOrdinal("StatusDesc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        tmpModel.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Number")))
                        tmpModel.Number = GetSmsNumber(dr.GetString(dr.GetOrdinal("Number")));

                    list.Add(tmpModel);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取短信发送实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="PlanId">发送任务编号</param>
        /// <returns></returns>
        public MSmsDetail GetSmsDetaiInfo(string CompanyId, string PlanId)
        {
            MSmsDetail model = null;
            string sql = "select [PlanId],[CompanyId],[Channel],[Content],[Amount],[UnitPrice],[IssueTime],[Status],[StatusDesc],[OperatorId],(select Code,Type from tbl_SmsDetail114 where tbl_SmsDetail114.PlanId = tbl_SmsDetail.PlanId for xml raw, root('root')) as Number from tbl_SmsDetail where CompanyId=@CompanyId and PlanId=@PlanId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(cmd, "PlanId", DbType.AnsiStringFixedLength, PlanId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr.Read())
                {
                    model = new MSmsDetail();
                    if (!dr.IsDBNull(dr.GetOrdinal("PlanId")))
                        model.PlanId = dr.GetString(dr.GetOrdinal("PlanId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Channel")))
                        model.Channel = dr.GetByte(dr.GetOrdinal("Channel"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        model.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UnitPrice")))
                        model.UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Status")))
                        model.Status = (Model.EnumType.SmsStructure.SendStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    if (!dr.IsDBNull(dr.GetOrdinal("StatusDesc")))
                        model.StatusDesc = dr.GetString(dr.GetOrdinal("StatusDesc"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Number")))
                        model.Number = GetSmsNumber(dr.GetString(dr.GetOrdinal("Number")));
                }
            }
            return model;
        }

        #endregion

        #region 定时短信方法

        /// <summary>
        /// 添加定时发送的短信
        /// </summary>
        /// <param name="model">定时发送的短信实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsTimerTask(MSmsTimerTask model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId)
                || model.Number == null || string.IsNullOrEmpty(model.Content))
                return 0;

            model.TaskId = Guid.NewGuid().ToString();
            DbCommand dc =
                _db.GetSqlStringCommand(SqlSmsTimerTaskAdd + GetSmsTimerTaskNumberSql(model.TaskId, model.Number));
            _db.AddInParameter(dc, "TaskId", DbType.AnsiStringFixedLength, model.TaskId);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "Channel", DbType.Byte, (byte)model.Channel);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(dc, "SendTime", DbType.DateTime, model.SendTime);
            _db.AddInParameter(dc, "Status", DbType.Byte, (byte)model.Status);
            _db.AddInParameter(dc, "StatusDesc", DbType.String, model.StatusDesc);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "RealTime", DbType.DateTime, DBNull.Value);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        #endregion
    }
}
