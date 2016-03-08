using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace EyouSoft.DAL.GovStructure
{
    /// <summary>
    /// 职务管理数据访问层
    /// 2011-09-19 邵权江 创建
    /// </summary>
    public class DPosition : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IPosition
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DPosition()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名

        #endregion

        #region 成员方法

        /// <summary>
        /// 判断职务信息是否存在
        /// </summary>
        /// <param name="PositionName">职务名称</param>
        /// <param name="Id">职务Id,新增Id=0</param>
        /// <returns></returns>
        public bool ExistsNum(string PositionName, int Id, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovPosition WHERE CompanyId=@CompanyId AND Title=@Title ";
            if (Id > 0)
            {
                StrSql += " AND [PositionId]<>@Id";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (Id > 0)
            {
                this._db.AddInParameter(dc, "PositionId", DbType.Int32, Id);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, PositionName);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 增加一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        public int AddGovPosition(EyouSoft.Model.GovStructure.MGovPosition model)
        {
            string StrSql = "INSERT INTO tbl_GovPosition([CompanyId],[Title],[Description],[OperatorId],[IssueTime]) VALUES(@CompanyId,@Title,@Description,@OperatorId,@IssueTime)  select @@identity";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Description", DbType.String, model.Description);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    return EyouSoft.Toolkit.Utils.GetInt(dr[0].ToString());
                }
            }
            return 0;
            // return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 更新一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        public bool UpdateGovPosition(EyouSoft.Model.GovStructure.MGovPosition model)
        {
            string StrSql = "UPDATE tbl_GovPosition SET Title=@Title,Description=@Description,OperatorId=@OperatorId,IssueTime=@IssueTime WHERE PositionId=@PositionId AND CompanyId=@CompanyId";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "PositionId", DbType.Int32, model.PositionId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Description", DbType.String, model.Description);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获得职务实体
        /// </summary>
        /// <param name="PositionId">职务ID</param>
        /// <param name="CompanyId">公司编号ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovPosition GetGovPositionModel(int PositionId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovPosition model = null;
            DbCommand dc = this._db.GetSqlStringCommand("SELECT PositionId,CompanyId,Title,Description,OperatorId,IssueTime FROM tbl_GovPosition WHERE PositionId=@PositionId AND CompanyId=@CompanyId");
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "PositionId", DbType.Int32, PositionId);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovPosition()
                    {
                        PositionId = dr.GetInt32(dr.GetOrdinal("PositionId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Title = dr.GetString(dr.GetOrdinal("Title")),
                        Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "" : dr.GetString(dr.GetOrdinal("Description")),
                        OperatorId = dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// 获得职务信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovPosition> GetGovPositionList(string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            if (CompanyId.Trim() == "")
                return null;
            IList<Model.GovStructure.MGovPosition> ResultList = null;
            string tableName = "tbl_GovPosition";
            string identityColumnName = "PositionId";
            string fields = " PositionId,CompanyId,Title,Description,OperatorId,IssueTime ";
            string query = string.Format("CompanyId='{0}'", CompanyId);
            string orderByString = " PositionId DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<Model.GovStructure.MGovPosition>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovPosition model = new EyouSoft.Model.GovStructure.MGovPosition()
                    {
                        PositionId = dr.GetInt32(dr.GetOrdinal("PositionId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Title = dr.GetString(dr.GetOrdinal("Title")),
                        Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "" : dr.GetString(dr.GetOrdinal("Description")),
                        OperatorId = dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据职务ID删除(需判断职务有无人员担任)
        /// </summary>
        /// <param name="PositionIds">职务ID(如：1,3,4)</param>
        /// <returns>0或负值：失败，1成功，2职务正在使用</returns>
        public int DeleteGovPosition(params string[] PositionIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < PositionIds.Length; i++)
            {
                sId.AppendFormat("{0},", PositionIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovPosition_Delete");
            this._db.AddInParameter(dc, "PositionIds", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        #endregion
    }
}
