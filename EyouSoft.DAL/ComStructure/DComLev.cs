using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 客户等级数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/20
    /// </summary>
    public class DComLev : DALBase, EyouSoft.IDAL.ComStructure.IComLev
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComLev()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComLev 成员
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComLev item)
        {
            string sql = "INSERT INTO tbl_ComLev([CompanyId],[Name],[BackMark],[FloatMoney],[OperatorDeptId],[OperatorId],[Operator],[IssueTime]) VALUES(@CompanyId,@Name,@BackMark,@FloatMoney,@OperatorDeptId,@OperatorId,@Operator,@IssueTime)";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@BackMark", DbType.String, item.BackMark);
            this._db.AddInParameter(comm, "@FloatMoney", DbType.Decimal, item.FloatMoney);
            this._db.AddInParameter(comm, "@OperatorDeptId", DbType.Int32, item.OperatorDeptId);
            this._db.AddInParameter(comm, "@OperatorId", DbType.String, item.OperatorId);
            this._db.AddInParameter(comm, "@Operator", DbType.String, item.Operator);
            this._db.AddInParameter(comm, "@IssueTime", DbType.DateTime, item.IssueTime);



            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComLev item)
        {
            string sql = "UPDATE tbl_ComLev SET   [BackMark]=@BackMark,[FloatMoney]=@FloatMoney,[Name] = @Name,OperatorId = @OperatorId WHERE LevId = @Id AND CompanyId = @CompanyId  ";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@BackMark", DbType.String, item.BackMark);
            this._db.AddInParameter(comm, "@FloatMoney", DbType.Decimal, item.FloatMoney);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 删除客户等级
        /// </summary>
        /// <param name="ids">客户等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(int[] ids, string companyId)
        {

            string sql = string.Format("UPDATE tbl_ComLev SET IsDelete = '1' WHERE LevId IN({0}) AND CompanyId = @CompanyId", Utils.GetSqlIdStrByArray(ids));


           // string sql = "UPDATE tbl_ComLev SET IsDelete = 1 WHERE LevId=@ids  AND CompanyId = @CompanyId";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
           // this._db.AddInParameter(comm, "@ids", DbType.String, ids);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取所有客户等级
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>客户等级集合</returns>
        public IList<MComLev> GetList(string companyId)
        {
            string sql = "SELECT LevId,CompanyId,[Name],OperatorId,BackMark,FloatMoney,IsSystem FROM tbl_ComLev WHERE IsDelete = '0' AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComLev> list = new List<MComLev>();
            MComLev item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComLev()
                    {
                        Id = (int)reader["LevId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        Name = reader["Name"].ToString(),
                        OperatorId = reader["OperatorId"].ToString(),
                        BackMark = reader["BackMark"].ToString(),
                        FloatMoney = EyouSoft.Toolkit.Utils.GetDecimal(reader["FloatMoney"].ToString()),
                        IsSystem = reader["IsSystem"].ToString()=="1"
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
