//代收相关信息数据访问类 汪奇志 2013-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using EyouSoft.IDAL.FinStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace EyouSoft.DAL.FinStructure
{
    /// <summary>
    /// 代收相关信息数据访问类
    /// </summary>
    public class DDaiShou : DALBase, IDaiShou
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetInfo = "SELECT * FROM [view_FinDaiShou] WHERE [DaiShouId]=@DaiShouId";
        const string SQL_SELECT_GetDaiShous = "SELECT * FROM [view_FinDaiShou] WHERE [TourId]=@TourId ORDER BY [IdentityId] ASC";
        const string SQL_SELECT_GetOrders = "SELECT [OrderId],[OrderCode],[BuyCompanyName],[ConfirmMoney],[ConfirmMoneyStatus],[SellerName],[Operator] FROM [tbl_TourOrder] WHERE [TourId]=@TourId AND [IsDelete]='0' AND [Status]=4 ORDER BY [IssueTime] ASC";
        const string SQL_SELECT_GetAnPais = "SELECT [PlanId],[Type],[SourceId],[SourceName],[Confirmation]  FROM [tbl_Plan] WHERE [TourId]=@TourId AND [IsDelete]='0' AND [Status]=4 AND [Type] NOT IN(7,12) ORDER BY [Type] ASC,[IssueTime] ASC";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DDaiShou()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members

        #endregion

        #region IDaiShou 成员
        /// <summary>
        /// 写入代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.FinStructure.MDaiShouInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_FinDaiShou_Insert");
            _db.AddInParameter(cmd, "DaiShouId", DbType.AnsiStringFixedLength, info.DaiShouId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, info.OrderId);
            _db.AddInParameter(cmd, "JinE", DbType.Decimal, info.JinE);           
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "Time", DbType.DateTime, info.Time);
            _db.AddInParameter(cmd, "AnPaiId", DbType.AnsiStringFixedLength, info.AnPaiId);
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

        /// <summary>
        /// 更新代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.FinStructure.MDaiShouInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_FinDaiShou_Update");
            _db.AddInParameter(cmd, "DaiShouId", DbType.AnsiStringFixedLength, info.DaiShouId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, info.OrderId);
            _db.AddInParameter(cmd, "JinE", DbType.Decimal, info.JinE);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "Time", DbType.DateTime, info.Time);
            _db.AddInParameter(cmd, "AnPaiId", DbType.AnsiStringFixedLength, info.AnPaiId);
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

        /// <summary>
        /// 删除代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int Delete(string daiShouId, string companyId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_FinDaiShou_Delete");
            _db.AddInParameter(cmd, "DaiShouId", DbType.AnsiStringFixedLength, daiShouId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
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

        /// <summary>
        /// 获取代收信息业务实体
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <returns></returns>
        public EyouSoft.Model.FinStructure.MDaiShouInfo GetInfo(string daiShouId)
        {
            EyouSoft.Model.FinStructure.MDaiShouInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "DaiShouId", DbType.AnsiStringFixedLength, daiShouId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.FinStructure.MDaiShouInfo();

                    info.AnPaiId = rdr.GetString(rdr.GetOrdinal("AnPaiId"));
                    info.BeiZhu = rdr["BeiZhu"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.CrmName = rdr["CrmName"].ToString();
                    info.DaiShouId = daiShouId;
                    info.GysId = rdr.GetString(rdr.GetOrdinal("GysId"));
                    info.GysName = rdr["GysName"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.OperatorName = rdr["OperatorName"].ToString();
                    info.OrderCode = rdr["OrderCode"].ToString();
                    info.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    info.Status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.Time = rdr.GetDateTime(rdr.GetOrdinal("Time"));
                    info.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));

                    info.ShenPiRenName = rdr["ShenPiRenName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShenPiTime"))) info.ShenPiTime = rdr.GetDateTime(rdr.GetOrdinal("ShenPiTime"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string tourId)
        {
            IList<EyouSoft.Model.FinStructure.MDaiShouInfo> items = new List<EyouSoft.Model.FinStructure.MDaiShouInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDaiShous);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.FinStructure.MDaiShouInfo();

                    info.AnPaiId = rdr.GetString(rdr.GetOrdinal("AnPaiId"));
                    info.BeiZhu = rdr["BeiZhu"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.CrmName = rdr["CrmName"].ToString();
                    info.DaiShouId = rdr.GetString(rdr.GetOrdinal("DaiShouId"));
                    info.GysId = rdr.GetString(rdr.GetOrdinal("GysId"));
                    info.GysName = rdr["GysName"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.OperatorName = rdr["OperatorName"].ToString();
                    info.OrderCode = rdr["OrderCode"].ToString();
                    info.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    info.Status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.Time = rdr.GetDateTime(rdr.GetOrdinal("Time"));
                    info.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取代收订单信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouOrderInfo> GetOrders(string tourId)
        {
            IList<EyouSoft.Model.FinStructure.MDaiShouOrderInfo> items = new List<EyouSoft.Model.FinStructure.MDaiShouOrderInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetOrders);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.FinStructure.MDaiShouOrderInfo();

                    item.CrmName = rdr["BuyCompanyName"].ToString();
                    item.IsQueRen = rdr["ConfirmMoneyStatus"].ToString() == "1";
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney"));
                    item.OrderCode = rdr["OrderCode"].ToString();
                    item.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    item.SellerName = rdr["SellerName"].ToString();
                    item.XiaDanRenName = rdr["Operator"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取代收计调安排信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo> GetAnPais(string tourId)
        {
            IList<EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo> items =new List<EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetAnPais);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo();
                    item.AnPaiId = rdr.GetString(rdr.GetOrdinal("PlanId"));
                    item.AnPaiLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)rdr.GetByte(rdr.GetOrdinal("Type"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["SourceName"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("Confirmation"));                    

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.FinStructure.MDaiShouChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.FinStructure.MDaiShouInfo> items = new List<EyouSoft.Model.FinStructure.MDaiShouInfo>();
            string tableName = "view_FinDaiShou";
            string fields = "*";
            string orderByString = "IdentityId DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            #region privs sql
            if (depts != null && depts.Length == 1 && depts[0] == -1)//查看自己
            {
                sql.AppendFormat(" AND TourSellerId='{0}' ", userId);
            }
            else
            {
                if (depts != null && depts.Length > 0)
                {
                    sql.AppendFormat(" AND( TourSellerDeptId IN({0}) ", GetIdsByArr(depts));

                    if (!string.IsNullOrEmpty(userId))
                    {
                        sql.AppendFormat(" OR TourSellerId='{0}' ", userId);
                    }

                    sql.Append(" ) ");
                }
            }
            #endregion

            #region sql
            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.CrmId))
                {
                    sql.AppendFormat(" AND CrmId='{0}' ", chaXun.CrmId);
                }
                else if (!string.IsNullOrEmpty(chaXun.CrmName))
                {
                    sql.AppendFormat(" AND CrmName LIKE'%{0}%' ", chaXun.CrmName);
                }
                if (!string.IsNullOrEmpty(chaXun.GysId))
                {
                    sql.AppendFormat(" AND GysId='{0}' ", chaXun.GysId);
                }
                else if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND GysName='{0}' ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.OrderCode))
                {
                    sql.AppendFormat(" AND OrderCode '%{0}%' ", chaXun.OrderCode);
                }
                if (chaXun.Status.HasValue)
                {
                    sql.AppendFormat(" AND Status={0} ", (int)chaXun.Status.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var info = new EyouSoft.Model.FinStructure.MDaiShouInfo();

                    info.AnPaiId = rdr.GetString(rdr.GetOrdinal("AnPaiId"));
                    info.BeiZhu = rdr["BeiZhu"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.CrmName = rdr["CrmName"].ToString();
                    info.DaiShouId = rdr.GetString(rdr.GetOrdinal("DaiShouId"));
                    info.GysId = rdr.GetString(rdr.GetOrdinal("GysId"));
                    info.GysName = rdr["GysName"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.OperatorName = rdr["OperatorName"].ToString();
                    info.OrderCode = rdr["OrderCode"].ToString();
                    info.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    info.Status = (EyouSoft.Model.EnumType.FinStructure.DaiShouStatus)rdr.GetByte(rdr.GetOrdinal("Status"));
                    info.Time = rdr.GetDateTime(rdr.GetOrdinal("Time"));
                    info.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    info.OrderSellerName = rdr["OrderSellerName"].ToString();
                    info.OrderXiaDanRenName = rdr["OrderXiaDanRenName"].ToString();

                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 代收审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">审批实体</param>
        /// <returns></returns>
        public int ShenPi(EyouSoft.Model.FinStructure.MDaiShouShenPiInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_FinDaiShou_ShenPi");
            _db.AddInParameter(cmd, "DaiShouId", DbType.AnsiStringFixedLength, info.DaiShouId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "Status", DbType.Byte, info.Status);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Time", DbType.DateTime, info.Time);
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
