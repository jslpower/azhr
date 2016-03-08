using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.ComStructure;
using System.Xml;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 公司(系统)信息数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/19
    /// </summary>
    public class DCompany : DALBase, EyouSoft.IDAL.ComStructure.ICompany
    {
        private readonly Database _db = null;

        #region 构造函数
        /// <summary>
        /// default constructor
        /// </summary>
        public DCompany()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region ICompany 成员
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="info">公司信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MCompany info)
        {
            //<root><info bankid="编号" bankname="银行名称" accountname="开户名" accountcode="账户编号" /></root>
            StringBuilder banksxml = new StringBuilder();
            banksxml.Append("<root>");
            if (info.ComAccount != null && info.ComAccount.Count > 0)
            {
                foreach (var bank in info.ComAccount)
                {
                    banksxml.AppendFormat("<info bankid=\"{0}\" bankname=\"{1}\" accountname=\"{2}\" accountcode=\"{3}\" />", bank.AccountId
                        , Utils.ReplaceXmlSpecialCharacter(bank.BankName)
                        , Utils.ReplaceXmlSpecialCharacter(bank.AccountName)
                        , Utils.ReplaceXmlSpecialCharacter(bank.BankNo));
                }
            }
            banksxml.Append("</root>");

            DbCommand cmd = _db.GetStoredProcCommand("proc_Company_Update");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.Id);
            _db.AddInParameter(cmd, "WLogo", DbType.String, info.WLogo);
            _db.AddInParameter(cmd, "NLogo", DbType.String, info.NLogo);
            _db.AddInParameter(cmd, "CompanyName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "CompanyEnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "License", DbType.String, info.License);
            _db.AddInParameter(cmd, "FuZeRen", DbType.String, info.ContactName);
            _db.AddInParameter(cmd, "Type", DbType.String, info.Type);
            _db.AddInParameter(cmd, "Telephone", DbType.String, info.Tel);
            _db.AddInParameter(cmd, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(cmd, "Fax", DbType.String, info.Fax);
            _db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "ZipCode", DbType.String, info.Zip);
            _db.AddInParameter(cmd, "WebSite", DbType.String, info.Domain);
            _db.AddInParameter(cmd, "BanksXML", DbType.String, banksxml.ToString());
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "MLogo", DbType.String, info.MLogo);
            _db.AddInParameter(cmd, "GYSLogo", DbType.String, info.GYSLogo);
            _db.AddInParameter(cmd, "FXSLogo", DbType.String, info.FXSLogo);

            int sqlExceptionCode = 0;

            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return false;

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "RetCode")) == 1;
        }
        /// <summary>
        /// 根据公司编号获取公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns>公司信息实体</returns>
        public MCompany GetModel(string companyId, string sysId)
        {
            string cmdText = "SELECT * FROM tbl_Company WHERE Id=@CompanyId ";

            DbCommand cmd = this._db.GetSqlStringCommand(cmdText);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            MCompany info = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new MCompany();
                    info.Id = rdr["Id"].ToString();
                    info.SysId = rdr["SysId"].ToString();
                    info.WLogo = rdr["WLogo"].ToString();
                    info.NLogo = rdr["NLogo"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.Type = rdr["Type"].ToString();
                    info.EnName = rdr["EnName"].ToString();
                    info.License = rdr["License"].ToString();
                    info.ContactName = rdr["ContactName"].ToString();
                    info.Tel = rdr["Tel"].ToString();
                    info.Mobile = rdr["Mobile"].ToString();
                    info.Fax = rdr["Fax"].ToString();
                    info.Address = rdr["Address"].ToString();
                    info.Zip = rdr["Zip"].ToString();
                    info.Domain = rdr["Domain"].ToString();
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.Province = rdr["Province"].ToString();
                    info.City = rdr["City"].ToString();
                    info.Notes = rdr["Notes"].ToString();
                    info.MLogo = rdr["MLogo"].ToString();
                    info.GYSLogo = rdr["GYSLogo"].ToString();
                    info.FXSLogo = rdr["FXSLogo"].ToString();
                };
            }

            return info;
        }

        ///// <summary>
        ///// 获取公司所有账号
        ///// </summary>
        ///// <param name="companyId">公司编号</param>
        ///// <returns></returns>
        //public IList<MComAccount> GetList(string companyId)
        //{
        //    StringBuilder sql = new StringBuilder("select * from tbl_ComAccount where CompanyId=@CompanyId AND IsCash='0' ");
        //    DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
        //    this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
        //    IList<MComAccount> list = new List<MComAccount>();
        //    MComAccount item = null;
        //    using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
        //    {
        //        while (reader.Read())
        //        {
        //            list.Add(item = new MComAccount()
        //            {
        //                AccountId = reader.IsDBNull(reader.GetOrdinal("AccountId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AccountId")),
        //                AccountName = reader["AccountName"].ToString(),
        //                BankName = reader["BankName"].ToString(),
        //                BankNo = reader["BankNo"].ToString(),
        //                CompanyId = reader["CompanyId"].ToString()
        //            });
        //        }
        //    }
        //    return list;
        //}

        #endregion
    }
}
