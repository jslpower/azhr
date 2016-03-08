using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit;
using System.Xml.Linq;

namespace EyouSoft.DAL.HGysStructure
{
    /// <summary>
    /// 供应商预存款相关数据访问类
    /// </summary>
    public class DYuCunKuan : DALBase, EyouSoft.IDAL.HGysStructure.IYuCunKuan
    {
        #region static constants
        //static constants
        const string SQL_INSERT_Insert = "INSERT INTO [tbl_SourceYuCunKuan]([YuCunId],[GysId],[Time],[JinE],[BeiZhu],[OperatorId],[IssueTime],[IsDelete]) VALUES (@YuCunId,@GysId,@Time,@JinE,@BeiZhu,@OperatorId,@IssueTime,'0')";
        const string SQL_UPDATE_Update = "UPDATE [tbl_SourceYuCunKuan] SET [Time]=@Time,[JinE]=@JinE,[BeiZhu]=@BeiZhu WHERE [YuCunId]=@YuCunId";
        const string SQL_UPDATE_Delete = "UPDATE [tbl_SourceYuCunKuan] set [IsDelete]='1' WHERE [YuCunId]=@YuCunId";
        const string SQL_SELECT_GetYuCunKuans = "SELECT * FROM [tbl_SourceYuCunKuan] WHERE [GysId]=@GysId AND [IsDelete]='0' ORDER BY [IdentityId] DESC";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DYuCunKuan()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IYuCunKuan 成员
        /// <summary>
        /// 新增预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_Insert);
            _db.AddInParameter(cmd, "YuCunId", DbType.AnsiStringFixedLength, info.YuCunId);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "Time", DbType.DateTime, info.Time);
            _db.AddInParameter(cmd, "JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MYuCunKuanInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Update);
            _db.AddInParameter(cmd, "YuCunId", DbType.AnsiStringFixedLength, info.YuCunId);
            _db.AddInParameter(cmd, "Time", DbType.DateTime, info.Time);
            _db.AddInParameter(cmd, "JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除预存款信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="yuCunId">预存款编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId, string yuCunId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Delete);
            _db.AddInParameter(cmd, "YuCunId", DbType.AnsiStringFixedLength, yuCunId);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取预存款信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> GetYuCunKuans(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MYuCunKuanInfo> items = new List<EyouSoft.Model.HGysStructure.MYuCunKuanInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYuCunKuans);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MYuCunKuanInfo();

                    item.BeiZhu = rdr["BeiZhu"].ToString();
                    item.GysId = gysId;
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Time = rdr.GetDateTime(rdr.GetOrdinal("Time"));
                    item.YuCunId = rdr["YuCunId"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 重置供应商预存款余额，返回1成功，其它失败
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public int ResetYuCunKuanYuE(string gysId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_ResetYuCunKuanYuE");
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
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
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        #endregion
    }
}
