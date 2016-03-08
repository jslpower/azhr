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
    /// 附件数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/20
    /// </summary>
    public class DComAttach:DALBase,EyouSoft.IDAL.ComStructure.IComAttach
    {
        private readonly Database _db = null;
        #region 构造函数
        public DComAttach()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComAttach 成员
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComAttach item)
        {
            string sql = "INSERT INTO tbl_ComAttach(ItemType,ItemId,[Name],FilePath,Size) VALUES(@ItemType,@ItemId,@Name,@FilePath,@Size)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)item.ItemType);
            this._db.AddInParameter(comm, "@ItemId", DbType.AnsiStringFixedLength, item.ItemId);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@FilePath", DbType.String, item.FilePath);
            this._db.AddInParameter(comm, "@Size", DbType.Int32, item.Size);

            int reuslt = DbHelper.ExecuteSql(comm, this._db);

            return reuslt > 0 ? true : false;
        }
        /// <summary>
        /// 增加附件下载次数
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        public void AddDownloads(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType)
        {
            string sql = "UPDATE tbl_ComAttach SET Downloads = Downloads + 1 WHERE ItemId = @ItemId AND ItemType = @ItemType";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)itemType);

            DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 删除附件并添加到待删除附件列表
        /// </summary>
        /// <param name="item">附件实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(MComAttach item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComAttach_Delete");
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)item.ItemType);
            this._db.AddInParameter(comm, "@ItemId", DbType.AnsiStringFixedLength, item.ItemId);

            int reuslt = DbHelper.ExecuteSql(comm, this._db);

            return reuslt > 0 ? true : false;
        }
        /// <summary>
        /// 批量修改已删除附件状态
        /// </summary>
        /// <param name="ids">文件编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool UpdateFileQue(string ids)
        {
            string sql = "UPDATE tbl_SysDeletedFileQue SET FileState = 1 WHERE CHARINDEX(','+cast(id as varchar(15))+',',','+@ids+',',0) > 0";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ids", DbType.String, ids);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取单个附件
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        /// <returns>附件实体</returns>
        public MComAttach GetModel(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType)
        {
            string sql = "SELECT ItemId,ItemType,[Name],FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemId = @ItemId AND ItemType = @ItemType";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)itemType);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    return new MComAttach()
                    {
                        ItemId = reader["ItemId"].ToString(),
                        ItemType = (AttachItemType)Enum.Parse(typeof(AttachItemType), reader["ItemType"].ToString()),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                        FilePath = reader.IsDBNull(reader.GetOrdinal("FilePath")) ? string.Empty : reader["FilePath"].ToString(),
                        Size = (int)reader["Size"],
                        Downloads = (int)reader["Downloads"]
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// 获取单个附件
        /// </summary>
        /// <param name="itemId">关联编号</param>
        /// <param name="itemType">关联类型</param>
        /// <returns>附件实体</returns>
        public IList<MComAttach> GetModelList(string itemId, EyouSoft.Model.EnumType.ComStructure.AttachItemType itemType)
        {
            IList<MComAttach> list = new List<MComAttach>();
            string sql = "SELECT ItemId,ItemType,[Name],FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemId = @ItemId AND ItemType = @ItemType";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)itemType);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(new MComAttach()
                    {
                        ItemId = reader["ItemId"].ToString(),
                        ItemType = (AttachItemType)Enum.Parse(typeof(AttachItemType), reader["ItemType"].ToString()),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                        FilePath = reader.IsDBNull(reader.GetOrdinal("FilePath")) ? string.Empty : reader["FilePath"].ToString(),
                        Size = (int)reader["Size"],
                        Downloads = (int)reader["Downloads"]
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取待删除附件列表
        /// </summary>
        /// <param name="num">获取数量</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>待删除附件列表</returns>
        public IList<MComDeletedFileQue> GetList(int? num, DateTime? startTime, DateTime? endTime)
        {
            StringBuilder sql = new StringBuilder();
            if (num != null)
            {
                sql.AppendFormat("SELECT TOP {0} Id,FilePath FROM tbl_SysDeletedFileQue", num);
            }
            else
            {
                sql.Append("SELECT ID,FilePath FROM tbl_SysDeletedFileQue");
            }
            sql.Append(" WHERE FileState = 0");
            if (startTime != null)
            {
                sql.AppendFormat(" AND IssueTime >= {0}", startTime);
            }
            if (endTime != null)
            {
                sql.AppendFormat(" AND IssueTime <= {0}", endTime);
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MComDeletedFileQue> list = new List<MComDeletedFileQue>();
            MComDeletedFileQue item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComDeletedFileQue()
                    {
                        FilePath = reader["FilePath"].ToString(),
                        Id = (int)reader["Id"]
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
