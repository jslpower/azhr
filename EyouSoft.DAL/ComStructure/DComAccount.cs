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
    /// 公司银行帐号
    /// 创建者：郑付杰
    /// 创建时间：2011/9/28
    /// </summary>
    public class DComAccount:DALBase,EyouSoft.IDAL.ComStructure.IComAccount
    {
        private readonly Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DComAccount()
        {
            this._db = base.SystemStore;
        }


        #region IComAccount 成员
        /// <summary>
        /// 获取公司的所有银行帐号信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号集合</returns>
        public IList<MComAccount> GetList(string companyId)
        {
            string sql = "SELECT * FROM tbl_ComAccount WHERE CompanyId = @CompanyId AND IsCash='0' "; 
            IList<MComAccount> items = new List<MComAccount>();                       
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(comm,this._db))
            {
                while (rdr.Read())
                {
                    items.Add(new MComAccount()
                    {
                        AccountId = rdr.GetInt32(rdr.GetOrdinal("AccountId")),
                        CompanyId = companyId,
                        AccountName = rdr["AccountName"].ToString(),
                        BankName = rdr["BankName"].ToString(),
                        BankNo = rdr["BankNo"].ToString(),
                        IsSet = rdr.GetString(rdr.GetOrdinal("IsSet")) == "1"
                    });
                }
            }

            return items;
        }

        /// <summary>
        /// 获取单个银行帐号
        /// </summary>
        /// <param name="id">银行账户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号实体</returns>
        public MComAccount GetModel(int id, string companyId)
        {

            string sql = "SELECT * FROM tbl_ComAccount WHERE AccountId = @Id AND CompanyId = @CompanyId";
            MComAccount info = null;
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(comm, this._db))
            {
                if (rdr.Read())
                {
                    info = new MComAccount()
                    {
                        AccountId = id,
                        CompanyId = companyId,
                        AccountName = rdr["AccountName"].ToString(),
                        BankName = rdr["BankName"].ToString(),
                        BankNo = rdr["BankNo"].ToString(),
                        IsSet = rdr.GetString(rdr.GetOrdinal("IsSet")) == "1"
                    };
                }
            }

            return info;
        }
        #endregion
    }
}
