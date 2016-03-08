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
namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 保险数据层
    /// 修改记录:
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public class DComInsurance : DALBase, EyouSoft.IDAL.ComStructure.IComInsurance
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComInsurance()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region DComInsurance 成员
        /// <summary>
        /// 添加保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComInsurance item)
        {
            string sql = "INSERT INTO tbl_ComInsurance(CompanyId,InsuranceId,InsuranceName,UnitPrice) VALUES(@CompanyId,@InsuranceId,@InsuranceName,@UnitPrice)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@InsuranceId", DbType.AnsiStringFixedLength, System.Guid.NewGuid().ToString());
            this._db.AddInParameter(comm, "@InsuranceName", DbType.String, item.InsuranceName);
            this._db.AddInParameter(comm, "@UnitPrice", DbType.Decimal, item.UnitPrice);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 修改保险
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComInsurance item)
        {
            string sql = "UPDATE tbl_ComInsurance SET InsuranceName= @InsuranceName,UnitPrice = @UnitPrice WHERE InsuranceId = @InsuranceId AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@InsuranceName", DbType.String, item.InsuranceName);
            this._db.AddInParameter(comm, "@UnitPrice", DbType.Decimal, item.UnitPrice);
            this._db.AddInParameter(comm, "@InsuranceId", DbType.AnsiStringFixedLength, item.InsuranceId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 删除保险
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string ids, string CompanyId)
        {
            string sql = "delete from tbl_ComInsurance where CHARINDEX(InsuranceId,@ids,0) > 0 and CompanyId=@CompanyId";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ids", DbType.String, ids);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取保险实体
        /// </summary>
        /// <param name="InsuranceId">保险编号</param>
        /// <returns></returns>
        public MComInsurance GetModel(string InsuranceId)
        {
            string sql = "SELECT * FROM tbl_ComInsurance WHERE InsuranceId = @InsuranceId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@InsuranceId", DbType.AnsiStringFixedLength, InsuranceId);
            MComInsurance  model =null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    model=new MComInsurance()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        InsuranceId = reader["InsuranceId"].ToString(),
                        InsuranceName = reader["InsuranceName"].ToString(),
                        UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// 获取所有保险
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<MComInsurance> GetList(string CompanyId)
        {
            string sql = "SELECT * FROM tbl_ComInsurance WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            IList<MComInsurance> list = new List<MComInsurance>();
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(new MComInsurance()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        InsuranceId = reader["InsuranceId"].ToString(),
                        InsuranceName = reader["InsuranceName"].ToString(),
                        UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
