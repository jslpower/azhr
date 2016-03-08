//短信中心-短信任务相关数据访问类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace EyouSoft.DAL.SmsStructure
{
    /// <summary>
    /// 短信中心-短信任务相关数据访问类
    /// </summary>
    public class DSmsRenWu : DALBase, EyouSoft.IDAL.SmsStructure.ISmsRenWu
    {
        #region static constants
        //static constants
        const string SQL_INSERT_InsertShangXing = "INSERT INTO [tbl_SmsShangXing]([ShangXingId],[HaoMa],[ApiSmsId],[NeiRong],[IssueTime],[CompanyId]) VALUES (@ShangXingId,@HaoMa,@ApiSmsId,@NeiRong,@IssueTime,@CompanyId)";
        const string SQL_INSERT_Insert = "INSERT INTO [tbl_SmsRenWu]([RenWuId],[CompanyId],[LeiXing],[FaQiRenId],[JieShouRenId],[JieShouTime],[JieShouStatus],[NeiRong],[HandlerStatus],[IssueTime],[ShangXingId]) VALUES (@RenWuId,@CompanyId,@LeiXing,@FaQiRenId,@JieShouRenId,@JieShouTime,@JieShouStatus,@NeiRong,@HandlerStatus,@IssueTime,@ShangXingId)";
        const string SQL_UPDATE_JieShouRenWu = "UPDATE [tbl_SmsRenWu] SET [JieShouRenId]=@JieShouRenId,[JieShouTime]=@JieShouTime,[JieShouStatus]=@JieShouStatus WHERE [RenWuId]=@RenWuId AND [CompanyId]=@CompanyId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DSmsRenWu()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region EyouSoft.IDAL.SmsStructure.ISmsRenWu 成员
        /// <summary>
        /// 写入短信上行信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShangXing(EyouSoft.Model.SmsStructure.MSmsShangXingInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertShangXing);

            _db.AddInParameter(cmd, "ShangXingId", DbType.AnsiStringFixedLength, info.ShangXingId);
            _db.AddInParameter(cmd, "HaoMa", DbType.String, info.HaoMa);
            _db.AddInParameter(cmd, "ApiSmsId", DbType.String, info.ApiSmsId);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 写入短信任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.SmsStructure.MSmsRenWuInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_Insert);
            _db.AddInParameter(cmd, "RenWuId", DbType.AnsiStringFixedLength, info.RenWuId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "FaQiRenId", DbType.AnsiStringFixedLength, info.FaQiRenId);
            _db.AddInParameter(cmd, "JieShouRenId", DbType.AnsiStringFixedLength, info.JieShouRenId);
            _db.AddInParameter(cmd, "JieShouTime", DbType.DateTime, info.JieShouTime);
            _db.AddInParameter(cmd, "JieShouStatus", DbType.Byte, info.JieShouStatus);
            _db.AddInParameter(cmd, "NeiRong", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "HandlerStatus", DbType.Byte, info.HandlerStatus);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "ShangXingId", DbType.AnsiStringFixedLength, info.ShangXingId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 接收任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JieShouRenWu(EyouSoft.Model.SmsStructure.MSmsRenWuJieShouInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_JieShouRenWu);            
            _db.AddInParameter(cmd, "JieShouRenId", DbType.AnsiStringFixedLength, info.JieShouRenId);
            _db.AddInParameter(cmd, "JieShouTime", DbType.DateTime, info.JieShouTime);
            _db.AddInParameter(cmd, "JieShouStatus", DbType.Byte, info.JieShouStatus);
            _db.AddInParameter(cmd, "RenWuId", DbType.AnsiStringFixedLength, info.RenWuId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取短信任务信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">总索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MSmsRenWuInfo> GetRenWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MSmsRenWuChaXunInfo chaXun)
        {            
            IList<EyouSoft.Model.SmsStructure.MSmsRenWuInfo> items = new List<EyouSoft.Model.SmsStructure.MSmsRenWuInfo>();
            string tableName = "tbl_SmsRenWu";
            string fields = "RenWuId,LeiXing,JieShouTime,NeiRong,JieShouStatus,HandlerStatus";
            fields += ",(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.FaQiRenId) AS FaQiRenName";
            fields += ",(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.JieShouRenId) AS JieShouRenName";
            fields += ",(SELECT A1.DeptName FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.JieShouRenId) AS JieShouRenDeptName";
            string orderByString = "IdentityId DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND LeiXing IN(1,2,3) ", companyId);

            #region sql
            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.FaQiRenId))
                {
                    sql.AppendFormat(" AND FaQiRenId='{0}' ", chaXun.FaQiRenId);
                }
                else if (!string.IsNullOrEmpty(chaXun.FaQiRenName))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.FaQiRenId AND A1.ContactName LIKE '%{0}%') ", chaXun.FaQiRenName);
                }

                if (chaXun.JieShouRenDeptId.HasValue)
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.JieShouRenId AND A1.DeptId={0}) ", chaXun.JieShouRenDeptId);
                }

                if (!string.IsNullOrEmpty(chaXun.JieShouRenId))
                {
                    sql.AppendFormat(" AND JieShouRenId='{0}' ", chaXun.JieShouRenId);
                }
                else if (!string.IsNullOrEmpty(chaXun.JieShouRenName))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_SmsRenWu.JieShouRenId AND A1.ContactName LIKE '%{0}%') ", chaXun.JieShouRenName);
                }
                if (chaXun.JieShouStatus.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing.行程变化);
                    sql.AppendFormat(" AND JieShouStatus={0} ", (int)chaXun.JieShouStatus.Value);
                }
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND LeiXing={0} ", (int)chaXun.LeiXing.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SmsStructure.MSmsRenWuInfo();

                    item.LeiXing = (EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing)rdr.GetByte(rdr.GetOrdinal("LeiXing"));
                    item.FaQiRenName = rdr["FaQiRenName"].ToString();
                    item.JieShouRenName = rdr["JieShouRenName"].ToString();
                    item.JieShouRenDeptName = rdr["JieShouRenDeptName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JieShouTime"))) item.JieShouTime = rdr.GetDateTime(rdr.GetOrdinal("JieShouTime"));
                    item.NeiRong = rdr["NeiRong"].ToString();
                    item.RenWuId = rdr.GetString(rdr.GetOrdinal("RenWuId"));
                    item.JieShouStatus = (EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus)rdr.GetByte(rdr.GetOrdinal("JieShouStatus"));
                    item.HandlerStatus = (EyouSoft.Model.EnumType.SmsStructure.RenWuHandlerStatus)rdr.GetByte(rdr.GetOrdinal("HandlerStatus"));
                        
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 行程变化，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int XCBH(EyouSoft.Model.SmsStructure.MSmsXCBHInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Sms_XCBH");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, info.TourCode);
            _db.AddInParameter(cmd, "DaoYouBH", DbType.String, info.DaoYouBH);
            _db.AddOutParameter(cmd, "FaQiRenId", DbType.String, 255);
            _db.AddOutParameter(cmd, "JieShouRenId", DbType.String, 255);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                int retCode = Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));

                if (retCode == 1)
                {
                    info.JieShouRenId = _db.GetParameterValue(cmd, "JieShouRenId").ToString();
                    info.FaQiRenId = _db.GetParameterValue(cmd, "FaQiRenId").ToString();
                }

                return retCode;
            }
        }

        /// <summary>
        /// 进店报账，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int JDBZ(EyouSoft.Model.SmsStructure.MSmsJDBZInfo info)
        {
            #region xxxml
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            if (info.XXs != null && info.XXs.Count > 0)
            {
                foreach (var item in info.XXs)
                {
                    s.AppendFormat("<info CPBH=\"{0}\" CPSL=\"{1}\" />", item.CPBH, item.CPSL);
                }
            }
            s.Append("</root>");
            #endregion

            DbCommand cmd = _db.GetStoredProcCommand("proc_Sms_JDBZ");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, info.TourCode);
            _db.AddInParameter(cmd, "DaoYouBH", DbType.String, info.DaoYouBH);
            _db.AddInParameter(cmd, "AnPaiBH", DbType.String, info.AnPaiBH);
            _db.AddInParameter(cmd, "GysBH", DbType.String, info.GysBH);
            _db.AddInParameter(cmd, "GWCR", DbType.Int32, info.GWCR);
            _db.AddInParameter(cmd, "GWET", DbType.Int32, info.GWET);
            _db.AddInParameter(cmd, "LiuShui", DbType.Decimal, info.LiuShui);
            _db.AddInParameter(cmd, "XXXml", DbType.String,s.ToString());
            _db.AddOutParameter(cmd, "FaQiRenId", DbType.String, 255);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                int retCode = Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));

                if (retCode == 1)
                {
                    info.FaQiRenId = _db.GetParameterValue(cmd, "FaQiRenId").ToString();
                }

                return retCode;
            }

        }
        #endregion
    }
}
