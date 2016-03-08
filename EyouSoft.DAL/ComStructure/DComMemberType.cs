using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 会员类型数据层
    /// 修改记录:
    /// 1、2012-03-22 曹胡生 创建
    /// </summary>
    public class DComMemberType : DALBase, EyouSoft.IDAL.ComStructure.IComMemberType
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComMemberType()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComMemberType item)
        {
            string sql = "INSERT INTO tbl_ComMemberType(CompanyId,TypeName) VALUES(@CompanyId,@TypeName)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "TypeName", DbType.String, item.TypeName);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 修改会员类型
        /// </summary>
        /// <param name="item">会员类型实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComMemberType item)
        {
            string sql = "UPDATE tbl_ComMemberType SET [TypeName] = @TypeName WHERE Id = @Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "TypeName", DbType.String, item.TypeName);
            this._db.AddInParameter(comm, "Id", DbType.Int32, item.Id);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 删除会员类型
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">会员类型编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string CompanyId, params int[] Ids)
        {
            string sql = string.Format("Delete tbl_ComMemberType  WHERE Id in({0}) AND CompanyId = @CompanyId", GetIdsByArr(Ids));
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MComMemberType GetModel(string CompanyId,int Id)
        {
            string sql = "SELECT Id,CompanyId,TypeName FROM tbl_ComMemberType WHERE CompanyId = @CompanyId and Id=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, Id);
            this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            MComMemberType item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MComMemberType()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CompanyId = reader["CompanyId"].ToString(),
                        TypeName = reader["TypeName"].ToString()
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 获取所有会员类型
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>会员类型集合</returns>
        public IList<MComMemberType> GetList(string CompanyId)
        {
            string sql = "SELECT Id,CompanyId,TypeName FROM tbl_ComMemberType WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            IList<MComMemberType> list = new List<MComMemberType>();
            MComMemberType item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComMemberType()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CompanyId = reader["CompanyId"].ToString(),
                        TypeName = reader["TypeName"].ToString()
                    });
                }
            }
            return list;
        }
        #endregion
    }
}
