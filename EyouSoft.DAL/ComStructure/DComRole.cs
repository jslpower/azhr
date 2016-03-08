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
    /// 角色数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/21
    /// </summary>
    public class DComRole : DALBase, EyouSoft.IDAL.ComStructure.IComRole
    {
        private readonly Database _db = null;
        #region 构造函数
        public DComRole()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComRole 成员
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        public int Add(MComRole item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComRole_AddOrUpdate");
            this._db.AddInParameter(comm, "@RoleName", DbType.String, item.RoleName);
            this._db.AddInParameter(comm, "@RoleChilds", DbType.AnsiString, item.RoleChilds);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, 0);
            this._db.AddOutParameter(comm, "@Result", DbType.Int32, 2);
            DbHelper.RunProcedure(comm, _db);
            return Convert.ToInt32(_db.GetParameterValue(comm, "Result"));
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        public int Update(MComRole item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComRole_AddOrUpdate");
            this._db.AddInParameter(comm, "@RoleName", DbType.String, item.RoleName);
            this._db.AddInParameter(comm, "@RoleChilds", DbType.AnsiString, item.RoleChilds);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddOutParameter(comm, "@Result", DbType.Int32, 2);
            DbHelper.RunProcedure(comm, _db);
            return Convert.ToInt32(_db.GetParameterValue(comm, "Result"));
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string ids)
        {
            string sql = "UPDATE tbl_ComRole SET IsDelete = '1' WHERE CHARINDEX(','+cast(Id as varchar(15))+',',','+@ids+',',0) > 0;UPDATE tbl_ComUser SET RoleId = 0,Privs='' WHERE CHARINDEX(','+cast(RoleId as varchar(15))+',',','+@ids+',',0) > 0";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@ids", DbType.String, ids);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>角色实体</returns>
        public MComRole GetModel(int id, string companyId)
        {
            string sql = "SELECT Id,RoleName,RoleChilds,CompanyId FROM tbl_ComRole WHERE Id = @Id AND IsDelete = '0' AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return new MComRole()
                    {
                        Id = (int)reader["Id"],
                        RoleName = reader["RoleName"].ToString(),
                        RoleChilds = reader["RoleChilds"].ToString(),
                        CompanyId = reader["CompanyId"].ToString()
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色集合</returns>
        public IList<MComRole> GetList(string companyId)
        {
            string sql = "SELECT Id,RoleName,RoleChilds,CompanyId,IsSystem FROM tbl_ComRole WHERE IsDelete = '0' AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComRole> list = new List<MComRole>();
            MComRole item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComRole()
                    {
                        Id = (int)reader["Id"],
                        RoleName = reader["RoleName"].ToString(),
                        RoleChilds = reader["RoleChilds"].ToString(),
                        CompanyId = reader["CompanyId"].ToString(),
                        IsSystem = reader["IsSystem"].ToString() == "1" ? true : false
                    });
                }
            }
            return list;
        }

        #endregion
    }
}
