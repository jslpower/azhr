using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EyouSoft.SmsWeb.Model;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace EyouSoft.SmsWeb.Dal
{
    /// <summary>
    /// 短信充值明细业务逻辑
    /// </summary>
    /// 周文超 2011-09-14
    public class DSmsBankCharge : DALBase
    {
        #region private member

        private readonly Database _db;

        /// <summary>
        /// 充值明细添加Sql
        /// </summary>
        private const string SqlSmsBankChargeAdd = @" INSERT INTO [tbl_SmsBankCharge]
           ([ChargeId]
           ,[AccountId]
           ,[ChargeAmount]
           ,[Status]
           ,[RealAmount]
           ,[ChargeComName]
           ,[ChargeName]
           ,[ChargeTelephone]
           ,[IssueTime])
     VALUES
           (@ChargeId
           ,@AccountId
           ,@ChargeAmount
           ,@Status
           ,@RealAmount
           ,@ChargeComName
           ,@ChargeName
           ,@ChargeTelephone
           ,@IssueTime); ";

        /// <summary>
        /// 根据查询实体生成SqlWhere子句
        /// </summary>
        /// <param name="queryModel">查询实体</param>
        /// <returns>SqlWhere子句</returns>
        private string GetSqlWhere(MQuerySmsBankCharge queryModel)
        {
            if (queryModel == null)
                return string.Empty;

            var strWhere = new StringBuilder(" 1 = 1 ");
            if (!string.IsNullOrEmpty(queryModel.AccountId))
                strWhere.AppendFormat(" and AccountId = '{0}' ", queryModel.AccountId);
            if (!string.IsNullOrEmpty(queryModel.AppKey))
                strWhere.AppendFormat(" and AppKey = '{0}' ", queryModel.AppKey);
            if (!string.IsNullOrEmpty(queryModel.AppSecret))
                strWhere.AppendFormat(" and AppSecret = '{0}' ", queryModel.AppSecret);
            if (queryModel.Status.HasValue)
                strWhere.AppendFormat(" and [Status] = {0} ", (int)queryModel.Status.Value);
            if (!string.IsNullOrEmpty(queryModel.ChargeComName))
                strWhere.AppendFormat(" and ChargeComName like '%{0}%' ", queryModel.ChargeComName);
            if (!string.IsNullOrEmpty(queryModel.ChargeName))
                strWhere.AppendFormat(" and ChargeName like '%{0}%' ", queryModel.ChargeName);
            if (queryModel.StartTime.HasValue)
                strWhere.AppendFormat(" and IssueTime >= '{0}' ", queryModel.StartTime.Value);
            if (queryModel.EndTime.HasValue)
                strWhere.AppendFormat(" and IssueTime <= '{0}' ", queryModel.EndTime.Value);

            return strWhere.ToString();
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsBankCharge()
        {
            _db = SmsStore;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="model">充值明细实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int Recharge(MSmsBankCharge model)
        {
            if (string.IsNullOrEmpty(model.AccountId) || model.ChargeAmount <= 0)
                return 0;

            model.ChargeId = Guid.NewGuid().ToString();
            var strSql = new StringBuilder(SqlSmsBankChargeAdd);            
            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "ChargeId", DbType.AnsiStringFixedLength, model.ChargeId);
            _db.AddInParameter(dc, "AccountId", DbType.AnsiStringFixedLength, model.AccountId);
            _db.AddInParameter(dc, "ChargeAmount", DbType.Decimal, model.ChargeAmount);
            _db.AddInParameter(dc, "Status", DbType.Byte, (int)model.Status);
            _db.AddInParameter(dc, "RealAmount", DbType.Decimal, model.RealAmount);
            _db.AddInParameter(dc, "ChargeComName", DbType.String, model.ChargeComName);
            _db.AddInParameter(dc, "ChargeName", DbType.String, model.ChargeName);
            _db.AddInParameter(dc, "ChargeTelephone", DbType.String, model.ChargeTelephone);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 审核充值明细
        /// </summary>
        /// <param name="list">短信充值明细审核业务实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public int CheckRechargeState(IList<MCheckSmsBankCharge> list)
        {
            if (list == null || list.Count < 1)
                return 0;

            var strXml = new StringBuilder("<ROOT>");
            foreach (var t in list)
            {
                if (string.IsNullOrEmpty(t.ChargeId))
                    continue;

                strXml.AppendFormat("<SmsBankChargeXML ChargeId = \"{0}\" Status = \"{1}\" RealAmount = \"{2}\" ShenHeRen=\"{3}\" ShenHeBeiZhu=\"{4}\" />"
                                    , Toolkit.Utils.ReplaceXmlSpecialCharacter(t.ChargeId), (int)t.Status, t.RealAmount, t.ShenHeRen, t.ShenHeBeiZhu);
            }
            strXml.Append("</ROOT>");

            DbCommand dc = _db.GetStoredProcCommand("proc_SmsBankCharge_Check");
            _db.AddInParameter(dc, "CheckSmsBankChargeXML", DbType.String, strXml.ToString());
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                return int.Parse(obj.ToString());

            return 0;
        }

        /// <summary>
        /// 查询充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回充值明细集合</returns>
        public List<MSmsBankCharge> GetSmsBankCharge(int pageSize, int pageIndex, ref int recordCount
            , MQuerySmsBankCharge queryModel)
        {
            List<MSmsBankCharge> list = null;
            MSmsBankCharge model;
            string fields = "*";
            string tableName = "view_SmsBankChargeAccount";
            string orderByString = " [IssueTime] desc ";
            string identityColumnName = " ChargeId ";

            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName
                , identityColumnName, fields, GetSqlWhere(queryModel), orderByString))
            {
                list = new List<MSmsBankCharge>();
                while (dr.Read())
                {
                    model = new MSmsBankCharge();
                    if (!dr.IsDBNull(dr.GetOrdinal("ChargeId")))
                        model.ChargeId = dr.GetString(dr.GetOrdinal("ChargeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AccountId")))
                        model.AccountId = dr.GetString(dr.GetOrdinal("AccountId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ChargeAmount")))
                        model.ChargeAmount = dr.GetDecimal(dr.GetOrdinal("ChargeAmount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Status")))
                        model.Status = (ChargeStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RealAmount")))
                        model.RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ChargeComName")))
                        model.ChargeComName = dr.GetString(dr.GetOrdinal("ChargeComName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ChargeName")))
                        model.ChargeName = dr.GetString(dr.GetOrdinal("ChargeName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ChargeTelephone")))
                        model.ChargeTelephone = dr.GetString(dr.GetOrdinal("ChargeTelephone"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppKey")))
                        model.AppKey = dr.GetString(dr.GetOrdinal("AppKey"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppSecret")))
                        model.AppSecret = dr.GetString(dr.GetOrdinal("AppSecret"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        model.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    model.SysType = (SystemType)dr.GetByte(dr.GetOrdinal("SysType"));
                    model.ShenHeBeiZhu = dr["ShenHeBeiZhu"].ToString();
                    model.ShenHeRen = dr["ShenHeRen"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ShenHeShiJian"))) model.ShenHeShiJian = dr.GetDateTime(dr.GetOrdinal("ShenHeShiJian"));
                    list.Add(model);
                }
            }

            return list;
        }
    }
}
