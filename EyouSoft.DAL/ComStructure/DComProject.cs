using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 服务项目
    /// 创建者：郑付杰
    /// 创建时间：2011/9/20
    /// </summary>
    public class DComProject:DALBase,EyouSoft.IDAL.ComStructure.IComProject
    {
        private readonly Database _db = null;
        #region 构造函数
        public DComProject()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComProject 成员
        /// <summary>
        /// 添加服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Add(MComProject item)
        {
            string sql = string.Empty;
            if (item.Type == ProjectType.包含项目)
            {
                 sql = "INSERT INTO tbl_ComProject(CompanyId,[Type],ItemType,Unit,[Content],OperatorId) VALUES(@CompanyId,@Type,@ItemType,@Unit,@Content,@OperatorId)";
            }
            else
            {
                sql = "INSERT INTO tbl_ComProject(CompanyId,[Type],[Content],OperatorId) VALUES(@CompanyId,@Type,@Content,@OperatorId)"; 
            } 
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Type", DbType.Byte, (int)item.Type);
            if (item.Type == ProjectType.包含项目)
            {
                this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)item.ItemType);
                this._db.AddInParameter(comm, "@Unit", DbType.Byte, (int)item.Unit);
            }
            this._db.AddInParameter(comm, "@Content", DbType.String, item.Content);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(MComProject item)
        {
            string sql = string.Empty;
            if (item.Type == ProjectType.包含项目)
            {
                sql = "UPDATE tbl_ComProject SET [Content]= @Content,Unit = @Unit,OperatorId = @OperatorId WHERE Id=@Id AND CompanyId = @CompanyId";
            }
            else
            {
                sql = "UPDATE tbl_ComProject SET [Content]= @Content,OperatorId = @OperatorId WHERE Id=@Id AND CompanyId = @CompanyId";
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Content", DbType.String, item.Content);
            if (item.Type == ProjectType.包含项目)
            {
                this._db.AddInParameter(comm, "@Unit", DbType.Byte, (int)item.Unit);
            }
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength,item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 删除服务标准模版
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Delete(int id,string companyId)
        {
            string sql = "DELETE FROM tbl_ComProject WHERE Id = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取服务标准模版实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版实体</returns>
        public MComProject GetModel(int id,string companyId)
        {
            string sql = "SELECT [Type],ItemType,Unit,Content FROM tbl_ComProject WHERE Id = @id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.Int32, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            MComProject item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MComProject();
                    item.Type = (ProjectType)Enum.Parse(typeof(ProjectType), reader["Type"].ToString());
                    item.Content = reader.IsDBNull(reader.GetOrdinal("Content")) ? string.Empty : reader["Content"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("ItemType")))
                        item.ItemType = (ContainProjectType)Enum.Parse(typeof(ContainProjectType), reader["ItemType"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("Unit")))
                        item.Unit = (ContainProjectUnit)Enum.Parse(typeof(ContainProjectUnit), reader["Unit"].ToString());
                }
            }
            return item;
        }
        /// <summary>
        /// 获取指定模版中某项的信息
        /// </summary>
        /// <param name="projectType">模版类型</param>
        /// <param name="cpType">包含项目类型</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版集合</returns>
        public IList<MComProject> GetList(ProjectType projectType, ContainProjectType? cpType, string companyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ID,[Content],Unit,ItemType FROM tbl_ComProject WHERE [Type] = @Type");
            if (cpType != null)
            {
                sql.Append(" AND ItemType = @ItemType");
            }
            sql.Append(" AND CompanyId = @companyId");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@Type", DbType.Byte, (int)projectType);
            if (cpType != null)
            {
                this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)cpType);
            }
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComProject> list = new List<MComProject>();
            MComProject item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MComProject();
                    if (projectType == ProjectType.包含项目)
                    {
                        item.ItemType = (ContainProjectType)Enum.Parse(typeof(ContainProjectUnit), reader["ItemType"].ToString()); 
                        item.Unit = (ContainProjectUnit)Enum.Parse(typeof(ContainProjectUnit), reader["Unit"].ToString());
                    }
                    item.Type = projectType;
                    item.Content = reader.IsDBNull(reader.GetOrdinal("Content")) ? string.Empty : reader["Content"].ToString();
                    item.Id = (int)reader["Id"];
                    item.CompanyId = companyId;
                    list.Add(item);
                }
            }

            return list;
        }
        #endregion
    }
}
