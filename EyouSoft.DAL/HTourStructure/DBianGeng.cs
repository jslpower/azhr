using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.TourStructure
{
    public class DBianGeng : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.TourStructure.IBianGeng
    {
        #region 初始化db

        private Microsoft.Practices.EnterpriseLibrary.Data.Database _db = null;

        /// <summary>
        /// 初始化_db
        /// </summary>
        public DBianGeng()
        {
            _db = base.SystemStore;
        }
        #endregion


        #region IBianGeng 成员
        /// <summary>
        /// 添加变更信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertBianGeng(EyouSoft.Model.TourStructure.MBianGeng model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_BianGeng_");
            this._db.AddInParameter(cmd, "BianId", DbType.AnsiStringFixedLength, model.BianId);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(cmd, "BianType", DbType.Byte, (int)model.BianType);
            this._db.AddInParameter(cmd, "Url", DbType.String, model.Url);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }


        /// <summary>
        /// 获取变更信息
        /// </summary>
        /// <param name="bianId"></param>
        /// <param name="bianType"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBianGeng> GetBianGengList(string bianId, EyouSoft.Model.EnumType.TourStructure.BianType? bianType)
        {
            IList<EyouSoft.Model.TourStructure.MBianGeng> list = new List<EyouSoft.Model.TourStructure.MBianGeng>();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT Id,OperatorId,");
            query.Append("(SELECT B.ContactName FROM tbl_ComUser AS B WHERE B.UserId=A.OperatorId) AS OperatorName,");
            query.Append("BianId,BianType,IssueTime,Url");
            query.Append("  FROM tbl_BianGeng as A");
            query.AppendFormat(" Where BianId='{0}' ", bianId);
            if (bianType.HasValue)
            {
                query.AppendFormat("  and  BianType={0} ", (int)bianType);
            }
            query.Append(" ORDER BY IssueTime DESC ");
            DbCommand cmd = this._db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.TourStructure.MBianGeng model = new EyouSoft.Model.TourStructure.MBianGeng
                    {
                        BianId = dr.GetString(dr.GetOrdinal("BianId")),
                        BianType = (EyouSoft.Model.EnumType.TourStructure.BianType)dr.GetByte(dr.GetOrdinal("BianType")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        OperatorId = dr["OperatorId"].ToString(),
                        OperatorName = !dr.IsDBNull(dr.GetOrdinal("OperatorName")) ? dr.GetString(dr.GetOrdinal("OperatorName")) : null,
                        Url = !dr.IsDBNull(dr.GetOrdinal("Url")) ? dr.GetString(dr.GetOrdinal("Url")) : null
                    };
                    list.Add(model);
                }
            }
            return list;

        }

        /// <summary>
        /// 获取第一次变更信息
        /// </summary>
        /// <param name="bianId"></param>
        /// <param name="bianType"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MBianGeng GetFirstBianGeng(string bianId, EyouSoft.Model.EnumType.TourStructure.BianType? bianType)
        {
            var model = new EyouSoft.Model.TourStructure.MBianGeng();
            StringBuilder query = new StringBuilder();
            query.Append("SELECT top 1 Id,OperatorId,");
            query.Append("(SELECT B.ContactName FROM tbl_ComUser AS B WHERE B.UserId=A.OperatorId) AS OperatorName,");
            query.Append("BianId,BianType,IssueTime,Url");
            query.Append("  FROM tbl_BianGeng as A");
            query.AppendFormat(" Where BianId='{0}' ", bianId);
            if (bianType.HasValue)
            {
                query.AppendFormat("  and  BianType={0} ", (int)bianType);
            }
            query.Append(" ORDER BY IssueTime");
            DbCommand cmd = this._db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model.BianId = dr.GetString(dr.GetOrdinal("BianId"));
                    model.BianType = (EyouSoft.Model.EnumType.TourStructure.BianType)dr.GetByte(dr.GetOrdinal("BianType"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.OperatorName = !dr.IsDBNull(dr.GetOrdinal("OperatorName")) ? dr.GetString(dr.GetOrdinal("OperatorName")) : null;
                    model.Url = !dr.IsDBNull(dr.GetOrdinal("Url")) ? dr.GetString(dr.GetOrdinal("Url")) : null;
                }
            }
            return model;

        }

        #endregion
    }
}
