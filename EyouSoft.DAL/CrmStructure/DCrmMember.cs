using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.CrmStructure
{
    /// <summary>
    /// 个人会员数据访问类
    /// </summary>
    public class DCrmMember : EyouSoft.Toolkit.DAL.DALBase,IDAL.CrmStructure.ICrmMember
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetInfo = "SELECT A.CompanyId,A.Type,A.CountryId,A.ProvinceId,A.CityId,A.CountyId,A.OperatorId,A.ShortName BrevityCode,A.SellerId,A.SellerName,B.* FROM tbl_Crm AS A INNER JOIN tbl_CrmMember AS B ON A.CrmId=B.CrmId WHERE A.CrmId=@CrmId;";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DCrmMember() { _db = base.SystemStore; }
        #endregion

        #region public members
        /// <summary>
        /// 写入个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_InsertPersonal");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, info.CrmId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "DistrictId", DbType.Int32, info.DistrictId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "BriefCode", DbType.String, info.BriefCode);
            _db.AddInParameter(cmd, "Gender", DbType.Byte, info.Gender);
            _db.AddInParameter(cmd, "IdCardCode", DbType.String, info.IdCardCode);
            if (info.Birthday.HasValue)
            {
                _db.AddInParameter(cmd, "Birthday", DbType.DateTime, info.Birthday.Value);
            }
            else
            {
                _db.AddInParameter(cmd, "Birthday", DbType.DateTime, DBNull.Value);
            }
            _db.AddInParameter(cmd, "National", DbType.String, info.National);
            _db.AddInParameter(cmd, "Telephone", DbType.String, info.Telephone);
            _db.AddInParameter(cmd, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(cmd, "QQ", DbType.String, info.QQ);
            _db.AddInParameter(cmd, "Email", DbType.String, info.Email);
            _db.AddInParameter(cmd, "HomeAddress", DbType.String, info.HomeAddress);
            _db.AddInParameter(cmd, "ZipCode", DbType.String, info.ZipCode);
            _db.AddInParameter(cmd, "DanWei", DbType.String, info.DanWei);
            _db.AddInParameter(cmd, "DanWeiAddress", DbType.String, info.DanWeiAddress);
            _db.AddInParameter(cmd, "MemberTypeId", DbType.Int32, info.MemberTypeId);
            _db.AddInParameter(cmd, "MemberCardCode", DbType.String, info.MemberCardCode);
            _db.AddInParameter(cmd, "MemberStatus", DbType.Byte, info.MemberStatus);
            if (info.JoinTime.HasValue)
            {
                _db.AddInParameter(cmd, "JoinTime", DbType.DateTime, info.JoinTime.Value);
            }
            else
            {
                _db.AddInParameter(cmd, "JoinTime", DbType.DateTime, DBNull.Value);
            }
            _db.AddInParameter(cmd, "JoinType", DbType.String, info.JoinType);
            _db.AddInParameter(cmd, "HuiFei", DbType.Decimal, info.HuiFei);
            _db.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            _db.AddInParameter(cmd, "IsTiXing", DbType.AnsiStringFixedLength, info.IsTiXing ? "1" : "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "CrmType", DbType.Byte, info.CrmType);
            _db.AddInParameter(cmd, "LxrId", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SellerId);
            _db.AddInParameter(cmd, "CardType", DbType.Byte, info.CardType.HasValue ? info.CardType.Value : EyouSoft.Model.EnumType.TourStructure.CardType.未知);
            _db.AddInParameter(cmd, "CardValidDate", DbType.String, info.CardValidDate);
            _db.AddInParameter(cmd, "QianFaDate", DbType.DateTime, info.QianFaDate);
            _db.AddInParameter(cmd, "QianFaDi", DbType.String, info.QianFaDi);
            _db.AddInParameter(cmd, "JiFen", DbType.Decimal, info.JiFen);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 修改个人会员信息，返回1成功
        /// </summary>
        /// <param name="info">个人会员信息业务实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.CrmStructure.MCrmPersonalInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_UpdatePersonal");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, info.CrmId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "DistrictId", DbType.Int32, info.DistrictId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "BriefCode", DbType.String, info.BriefCode);
            _db.AddInParameter(cmd, "Gender", DbType.Byte, info.Gender);
            _db.AddInParameter(cmd, "IdCardCode", DbType.String, info.IdCardCode);
            if (info.Birthday.HasValue)
            {
                _db.AddInParameter(cmd, "Birthday", DbType.DateTime, info.Birthday.Value);
            }
            else
            {
                _db.AddInParameter(cmd, "Birthday", DbType.DateTime, DBNull.Value);
            }
            _db.AddInParameter(cmd, "National", DbType.String, info.National);
            _db.AddInParameter(cmd, "Telephone", DbType.String, info.Telephone);
            _db.AddInParameter(cmd, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(cmd, "QQ", DbType.String, info.QQ);
            _db.AddInParameter(cmd, "Email", DbType.String, info.Email);
            _db.AddInParameter(cmd, "HomeAddress", DbType.String, info.HomeAddress);
            _db.AddInParameter(cmd, "ZipCode", DbType.String, info.ZipCode);
            _db.AddInParameter(cmd, "DanWei", DbType.String, info.DanWei);
            _db.AddInParameter(cmd, "DanWeiAddress", DbType.String, info.DanWeiAddress);
            _db.AddInParameter(cmd, "MemberTypeId", DbType.Int32, info.MemberTypeId);
            _db.AddInParameter(cmd, "MemberCardCode", DbType.String, info.MemberCardCode);
            _db.AddInParameter(cmd, "MemberStatus", DbType.Byte, info.MemberStatus);
            if (info.JoinTime.HasValue)
            {
                _db.AddInParameter(cmd, "JoinTime", DbType.DateTime, info.JoinTime.Value);
            }
            else
            {
                _db.AddInParameter(cmd, "JoinTime", DbType.DateTime, DBNull.Value);
            }
            _db.AddInParameter(cmd, "JoinType", DbType.String, info.JoinType);
            _db.AddInParameter(cmd, "HuiFei", DbType.Decimal, info.HuiFei);
            _db.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            _db.AddInParameter(cmd, "IsTiXing", DbType.AnsiStringFixedLength, info.IsTiXing ? "1" : "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SellerId);
            _db.AddInParameter(cmd, "CardType", DbType.Byte, info.CardType.HasValue ? info.CardType.Value : EyouSoft.Model.EnumType.TourStructure.CardType.未知);
            _db.AddInParameter(cmd, "CardValidDate", DbType.String, info.CardValidDate);
            _db.AddInParameter(cmd, "QianFaDate", DbType.DateTime, info.QianFaDate);
            _db.AddInParameter(cmd, "QianFaDi", DbType.String, info.QianFaDi);
            _db.AddInParameter(cmd, "JiFen", DbType.Decimal, info.JiFen);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取个人会员信息业务实体
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CrmStructure.MCrmPersonalInfo GetInfo(string crmId)
        {
            EyouSoft.Model.CrmStructure.MCrmPersonalInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.CrmStructure.MCrmPersonalInfo();

                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday"))) info.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    info.BriefCode = rdr["BrevityCode"].ToString();
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    info.CrmId = crmId;
                    info.DanWei = rdr["WorkUnit"].ToString();
                    info.DanWeiAddress = rdr["UnitAddress"].ToString();
                    info.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    info.Email = rdr["Email"].ToString();
                    info.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("Gender"));
                    info.HomeAddress = rdr["HomeAddress"].ToString();
                    info.HuiFei = rdr.GetDecimal(rdr.GetOrdinal("MemberDues"));
                    info.IdCardCode = rdr["IdNumber"].ToString();
                    info.IsTiXing = rdr.GetString(rdr.GetOrdinal("IsRemind")) == "1";
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("AvailableIntegral"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JoinTime"))) info.JoinTime = rdr.GetDateTime(rdr.GetOrdinal("JoinTime"));
                    info.JoinType = rdr["ApplicationType"].ToString();
                    info.MemberCardCode = rdr["MemberCardNumber"].ToString();
                    info.MemberStatus = (EyouSoft.Model.EnumType.CrmStructure.CrmMemberState)rdr.GetByte(rdr.GetOrdinal("MemberState"));
                    info.MemberTypeId = rdr.GetInt32(rdr.GetOrdinal("MemberType"));
                    info.Mobile = rdr["MobilePhone"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.National = rdr["National"].ToString();
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.QQ = rdr["QQ"].ToString();
                    info.Remark = rdr["Remark"].ToString();
                    info.Telephone = rdr["ContactPhone"].ToString();
                    info.ZipCode = rdr["ZipCode"].ToString();
                    info.SellerId = rdr["SellerId"].ToString();
                    info.SellerName = rdr["SellerName"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// 获取个人会员积分明细集合
        /// </summary>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MJiFenInfo> GetJiFens(string crmId, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CrmStructure.MJiFenInfo> items = new List<EyouSoft.Model.CrmStructure.MJiFenInfo>();

            string tableName = "tbl_CrmJiFen";
            string fields = "JiFenShiJian,ZengJianLeiBie,JiFen,Remark,IssueTime";
            string orderByString = "Id DESC";
            string query = string.Format(" CrmId='{0}' ", crmId);

            using (IDataReader rdr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query, orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CrmStructure.MJiFenInfo item = new EyouSoft.Model.CrmStructure.MJiFenInfo();

                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiFen = rdr.GetDecimal(rdr.GetOrdinal("JiFen"));
                    item.JiFenShiJian = rdr.GetDateTime(rdr.GetOrdinal("JiFenShiJian"));
                    item.Remark = rdr["Remark"].ToString();
                    item.ZengJianLeiBie = (EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie)rdr.GetByte(rdr.GetOrdinal("ZengJianLeiBie"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置个人会员积分
        /// </summary>
        /// <param name="info">积分信息业务实体</param>
        /// <returns></returns>
        public bool SetJiFen(EyouSoft.Model.CrmStructure.MJiFenInfo info)
        {
            string cmdText = "INSERT INTO[tbl_CrmJiFen]([CrmId],[JiFenShiJian],[ZengJianLeiBie],[JiFen],[Remark],[OperatorId],[IssueTime])VALUES(@CrmId,@JiFenShiJian,@ZengJianLeiBie,@JiFen,@Remark,@OperatorId,@IssueTime)";
            if (info.ZengJianLeiBie == EyouSoft.Model.EnumType.CrmStructure.JiFenZengJianLeiBie.增加)
            {
                cmdText += ";UPDATE tbl_CrmMember SET AvailableIntegral=AvailableIntegral+@JiFen WHERE CrmId=@CrmId";
            }
            else
            {
                cmdText += ";UPDATE tbl_CrmMember SET AvailableIntegral=AvailableIntegral-@JiFen WHERE CrmId=@CrmId";
            }
            DbCommand cmd = _db.GetSqlStringCommand(cmdText);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, info.CrmId);
            _db.AddInParameter(cmd, "JiFenShiJian", DbType.DateTime, info.JiFenShiJian);
            _db.AddInParameter(cmd, "ZengJianLeiBie", DbType.Byte, info.ZengJianLeiBie);
            _db.AddInParameter(cmd, "JiFen", DbType.Decimal, info.JiFen);
            _db.AddInParameter(cmd, "Remark", DbType.String, info.Remark);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd,_db)>1;
        }

        /// <summary>
        /// 判断是否存在相同的个人会员身份证号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <param name="idCardCode">身份证号</param>
        /// <returns></returns>
        public bool IsExistsIdCardCode(string companyId, string crmId, string idCardCode)
        {
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            string cmdText = "SELECT COUNT(*) FROM view_Crm_Personal WHERE CompanyId=@CompanyId AND IdNumber=@IdCardCode ";
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "IdCardCode", DbType.String, idCardCode);

            if (!string.IsNullOrEmpty(crmId))
            {
                cmdText += " AND CrmId<>@CrmId ";
                _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            }

            cmd.CommandText = cmdText;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return false;
        }
        /// <summary>
        /// 判断是否存在相同的个人会员
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">个人会员编号</param>
        /// <returns></returns>
        public bool IsExists(string companyId, string crmId)
        {
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            string cmdText = "SELECT COUNT(*) FROM view_Crm_Personal WHERE CompanyId=@CompanyId ";
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            if (!string.IsNullOrEmpty(crmId))
            {
                cmdText += " AND CrmId=@CrmId ";
                _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            }

            cmd.CommandText = cmdText;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return false;
        }
        #endregion
    }
}
