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
    /// 报价等级数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/20
    /// </summary>
    public class DComStand:DALBase,EyouSoft.IDAL.ComStructure.IComStand
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComStand()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComStand 成员
        /// <summary>
        /// 添加报价等级
        /// </summary>
        /// <param name="item">报价等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComStand item)
        {
            string sql = "INSERT INTO tbl_ComStand(CompanyId,[Name],OperatorId) VALUES(@CompanyId,@Name,@OperatorId)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改报价等级
        /// </summary>
        /// <param name="item">报价等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComStand item)
        {
            string sql = "Update tbl_ComStand SET [Name] = @Name,OperatorId = @OperatorId WHERE Id = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 删除报价等级
        /// </summary>
        /// <param name="ids">报价等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string ids, string companyId)
        {
            string sql = "UPDATE tbl_ComStand SET IsDelete = 1 WHERE CHARINDEX(','+cast(Id as varchar(15))+',',','+@ids+',',0) > 0 AND IsSystem = '0' AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ids", DbType.String, ids);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取所有报价等级
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>报价等级集合</returns>
        public IList<MComStand> GetList(string companyId)
        {
            string sql = "SELECT Id,CompanyId,[Name],OperatorId,IssueTime,IsSystem FROM tbl_ComStand WHERE IsDelete = '0' AND CompanyId = @CompanyId";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            IList<MComStand> list = new List<MComStand>();
            MComStand item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComStand()
                    {
                        Id = (int)reader["Id"],
                        CompanyId = reader["CompanyId"].ToString(),
                        Name = reader["Name"].ToString(),
                        OperatorId = reader["OperatorId"].ToString(),
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        IsSystem = GetBoolean(reader["IsSystem"].ToString())
                    });
                }
            }
            return list;
        }

        #endregion
    }
}
