using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 上车地点数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/20
    /// </summary>
    public class DComCarPlace:DALBase,EyouSoft.IDAL.ComStructure.IComCarPlace
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComCarPlace()
        {
            this._db = base.SystemStore;
        }
        #endregion



        #region IComCarPlace 成员
        /// <summary>
        /// 添加上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComCarPlace item)
        {
            string sql = "INSERT INTO tbl_ComCarPlace(CompanyId,Place,Amount,OperatorId) VALUES(@CompanyId,@Place,@Amount,@OperatorId)";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Place", DbType.String, item.Place);
            this._db.AddInParameter(comm, "@Amount", DbType.Currency, item.Amount);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="item">上传地点实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MComCarPlace item)
        {
            string sql = "UPDATE tbl_ComCarPlace SET Place = @Place,Amount = @Amount,OperatorId = @OperatorId WHERE Id = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Place", DbType.String, item.Place);
            this._db.AddInParameter(comm, "@Amount", DbType.Currency, item.Amount);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取加价金额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="place">上车地点</param>
        /// <returns>加价金额</returns>
        public decimal GetAmountByPlace(string companyId, string place)
        {
            string sql = "SELECT Amount FROM tbl_ComCarPlace WHERE Place = @Place AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Place", DbType.String, place);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return decimal.Parse(reader["Amount"].ToString());
                }
            }
            return 0;
        }
        /// <summary>
        /// 获取所有上车地点信息
        /// </summary>
        /// <returns>上车地点集合</returns>
        public IList<MComCarPlace> GetList(string companyId)
        {
            string sql = "SELECT ID,Place,Amount FROM tbl_ComCarPlace WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComCarPlace> list = new List<MComCarPlace>();
            MComCarPlace item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComCarPlace()
                    {
                        Id = (int)reader["Id"],
                        Place = reader["Place"].ToString(),
                        Amount = decimal.Parse(reader["Amount"].ToString())
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 分页获取上车地点列表
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <returns>上车地点集合</returns>
        public IList<MComCarPlace> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId)
        {
            string tableName = "tbl_ComCarPlace";
            string primaryKey = "Id";
            string orderBy = string.Empty;
            string fileds = "ID,CompanyId,Place,Amount,OperatorId,IssueTime";
            string query = string.Format(" CompanyId = '{0}'",companyId);
            IList<MComCarPlace> list = new List<MComCarPlace>();
            MComCarPlace item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db,pageSize,pageCurrent,ref pageCount,tableName
                , primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComCarPlace()
                    {
                        Id = (int)reader["Id"],
                        CompanyId = reader["CompanyId"].ToString(),
                        Place = reader["Place"].ToString(),
                        Amount = decimal.Parse(reader["Amount"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString())
                    });
                }
            }
            return list;
        }

        #endregion
    }
}
