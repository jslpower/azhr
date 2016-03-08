using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.CrmStructure
{
    /// <summary>
    /// 客户关系数据访问类
    /// </summary>
    public class DCrm : EyouSoft.Toolkit.DAL.DALBase, IDAL.CrmStructure.ICrm
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetInfo = "SELECT * FROM tbl_Crm WHERE CrmId=@CrmId;";
        const string SQL_SELECT_GetCrmBanks = "SELECT * FROM tbl_CrmBank WHERE CrmId=@CrmId ORDER BY SortId ASC";
        const string SQL_SELECT_GetCrmLxrs = "SELECT * FROM tbl_CrmLinkman WHERE TypeId=@CrmId AND [Type]=@LxrType ORDER BY SortId ASC";
        const string SQL_SELECT_GetCrmXieYiFuJian = "SELECT * FROM tbl_ComAttach WHERE ItemId=@CrmId AND ItemType=@AttachType";
        const string SQL_SELECT_GetCrmUsers = "SELECT A.*,B.UserName,B.Password,B.UserStatus FROM tbl_CrmLinkman AS A LEFT JOIN tbl_ComUser AS B ON A.UserId=B.UserId  WHERE A.[Type]=@LxrType AND A.TypeId=@CrmId ORDER BY SortId ASC";
        const string SQL_UPDATE_SetCrmUserStatus = "UPDATE tbl_ComUser SET UserStatus=@Status WHERE UserId=@UserId AND CompanyId=@CompanyId AND TourCompanyId=@CrmId AND UserType=@UserType";
        const string SQL_SELECT_YanZhengZeRenXiaoShou = "SELECT COUNT(*) FROM [tbl_Crm] WHERE [CrmId]=@CrmId AND [SellerId]=@SellerId";
        //EyouSoft.DAL.CrmStructure.DCrmBank bankDal = new DCrmBank();
        //EyouSoft.DAL.CrmStructure.DCrmLinkman linkmanDal = new DCrmLinkman();
        //EyouSoft.DAL.ComStructure.DComUser userDal = new EyouSoft.DAL.ComStructure.DComUser();
        //EyouSoft.DAL.ComStructure.DComAttach attachDal = new EyouSoft.DAL.ComStructure.DComAttach();
        //EyouSoft.Toolkit.DataProtection.HashCrypto hc = new EyouSoft.Toolkit.DataProtection.HashCrypto();
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DCrm()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// 创建协议附件信息XML
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateHeZuoXieYiXML(EyouSoft.Model.ComStructure.MComAttach info)
        {
            //<root><info filename="" filepath="" /></root>
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (info != null && !string.IsNullOrEmpty(info.FilePath))
            {
                xml.AppendFormat("<info filename=\"{0}\" filepath=\"{1}\" />", info.Name, info.FilePath);
            }

            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// 创建银行账户信息XML
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateBanksXML(IList<Model.CrmStructure.MCrmBank> items)
        {
            //<root><info bankid="GUID" bankname="开户行" bankaccount="账号" /></root>
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item.BankId)) item.BankId = Guid.NewGuid().ToString();

                    xml.AppendFormat("<info bankid=\"{0}\" bankname=\"{1}\" bankaccount=\"{2}\" />", item.BankId
                        , Utils.ReplaceXmlSpecialCharacter(item.BankName)
                        , Utils.ReplaceXmlSpecialCharacter(item.BankAccount));
                }
            }

            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// 创建联系人信息XML
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateLxrsXML(IList<EyouSoft.Model.CrmStructure.MCrmLinkman> items)
        {
            //<root><info lxrid="编号" deptname="部门"  lxrname="姓名" telephone="电话" mobile="手机" fax="传真" birthday="生日" istixing="是否生日提醒" qq="qq" msn="msn" email="email" /></root>
            StringBuilder xml = new StringBuilder();
            xml.Append("<root>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item.Id)) item.Id = Guid.NewGuid().ToString();

                    xml.AppendFormat("<info lxrid=\"{0}\" deptname=\"{1}\"  lxrname=\"{2}\" telephone=\"{3}\" mobile=\"{4}\" fax=\"{5}\" birthday=\"{6}\" istixing=\"{7}\" qq=\"{8}\" msn=\"{9}\" email=\"{10}\" />", item.Id
                        , Utils.ReplaceXmlSpecialCharacter(item.Department)
                        , Utils.ReplaceXmlSpecialCharacter(item.Name)
                        , Utils.ReplaceXmlSpecialCharacter(item.Telephone)
                        , Utils.ReplaceXmlSpecialCharacter(item.MobilePhone)
                        , Utils.ReplaceXmlSpecialCharacter(item.Fax)
                        , item.Birthday.HasValue ? item.Birthday.Value.ToString() : ""
                        , item.IsRemind ? "1" : "0"
                        , Utils.ReplaceXmlSpecialCharacter(item.QQ)
                        , Utils.ReplaceXmlSpecialCharacter(item.MSN)
                        , Utils.ReplaceXmlSpecialCharacter(item.EMail));
                }
            }

            xml.Append("</root>");
            return xml.ToString();
        }

        /// <summary>
        /// get crm banks
        /// </summary>
        /// <param name="crmId">crmid</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MCrmBank> GetCrmBanks(string crmId)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmBank> items = new List<EyouSoft.Model.CrmStructure.MCrmBank>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCrmBanks);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.CrmStructure.MCrmBank();
                    item.BankAccount = rdr["BankAccount"].ToString();
                    item.BankId = rdr.GetString(rdr.GetOrdinal("BankId"));
                    item.BankName = rdr["BankName"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get crm 协议附件
        /// </summary>
        /// <param name="crmId">crmid</param>
        /// <returns></returns>
        Model.ComStructure.MComAttach GetCrmXieYiFuJian(string crmId)
        {
            Model.ComStructure.MComAttach info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCrmXieYiFuJian);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "AttachType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.AttachItemType.客户合作协议);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.ComStructure.MComAttach();
                    info.FilePath = rdr["FilePath"].ToString();
                    info.Name = rdr["Name"].ToString();
                }
            }

            return info;
        }

        /// <summary>
        /// get crm lxr
        /// </summary>
        /// <param name="crmId">crmid</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetCrmLxrs(string crmId)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> items = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCrmLxrs);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.CrmStructure.MCrmLinkman();

                    // item.Address = rdr["Address"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday"))) item.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    item.Department = rdr["Department"].ToString();
                    item.Fax = rdr["Fax"].ToString();
                    item.Id = rdr.GetString(rdr.GetOrdinal("Id"));
                    item.IsRemind = rdr.GetString(rdr.GetOrdinal("IsRemind")) == "1";
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.MobilePhone = rdr["MobilePhone"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.QQ = rdr["QQ"].ToString();
                    item.Telephone = rdr["Telephone"].ToString();
                    item.Type = EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位;
                    item.TypeId = crmId;
                    item.UserId = rdr["UserId"].ToString().Trim();

                    item.Post = rdr["Post"].ToString().Trim();
                    item.MSN = rdr["MSNSKYPE"].ToString().Trim();
                    item.EMail = rdr["EMail"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get crm jiaoyimingxi
        /// </summary>
        /// <param name="crmId">crmid</param>
        /// <returns></returns>
        EyouSoft.Model.CrmStructure.MCrmJiaoYiMingXiInfo GetCrmJiaoYiMingXi(string crmId)
        {
            var info = new EyouSoft.Model.CrmStructure.MCrmJiaoYiMingXiInfo();

            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_GetJiaoYiMingXi");
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);

            using (IDataReader rdr = DbHelper.RunReaderProcedure(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.DingDanJinE = rdr.GetDecimal(rdr.GetOrdinal("DingDanJinE"));
                    info.DingDanRenShu = rdr.GetInt32(rdr.GetOrdinal("DingDanRenShu"));
                    info.DingDanShu = rdr.GetInt32(rdr.GetOrdinal("DingDanShu"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LatestTime"))) info.LatestTime = rdr.GetDateTime(rdr.GetOrdinal("LatestTime"));
                    info.YiShouJinE = rdr.GetDecimal(rdr.GetOrdinal("YiShouJinE"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取客户单位查询信息业务实体的SQL查询条件
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        string GetCrmsQuerySQL(EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo)
        {
            StringBuilder s = new StringBuilder();

            if (searchInfo != null)
            {
                /*if (!string.IsNullOrEmpty(searchInfo.BriefCode))
                {
                    s.AppendFormat(" AND BrevityCode LIKE '{0}%' ", searchInfo.BriefCode);
                }*/
                if (searchInfo.CountryId.HasValue)
                {
                    s.AppendFormat(" AND CountryId={0} ", searchInfo.CountryId.Value);
                }
                if (searchInfo.CityId.HasValue)
                {
                    s.AppendFormat(" AND CityId={0} ", searchInfo.CityId.Value);
                }
                if (!string.IsNullOrEmpty(searchInfo.CrmName))
                {
                    s.AppendFormat(" AND Name LIKE '%{0}%' ", searchInfo.CrmName);
                }
                if (searchInfo.DengJiBH.HasValue)
                {
                    s.AppendFormat(" AND LevId={0} ", searchInfo.DengJiBH.Value);
                }
                if (searchInfo.ProvinceId.HasValue)
                {
                    s.AppendFormat(" AND ProvinceId={0} ", searchInfo.ProvinceId.Value);
                }
                if (!string.IsNullOrEmpty(searchInfo.SellerId))
                {
                    s.AppendFormat(" AND SellerId='{0}' ", searchInfo.SellerId);
                }
                if (!string.IsNullOrEmpty(searchInfo.SellerName))
                {
                    s.AppendFormat(" AND SellerName LIKE '%{0}%' ", searchInfo.SellerName);
                }
                if (!string.IsNullOrEmpty(searchInfo.LxrName))
                {
                    s.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CrmLinkman AS A1 WHERE A1.TypeId=tbl_Crm.CrmId AND A1.Name LIKE '%{0}%' AND A1.Type={1}) ", searchInfo.LxrName, (int)EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);
                }
            }

            return s.ToString();
        }
        #endregion

        #region public members
        /// <summary>
        /// 添加客户单位,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.CrmStructure.MCrm info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_Insert");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, info.CrmId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AttachXML", DbType.String, CreateHeZuoXieYiXML(info.AttachModel));
            _db.AddInParameter(cmd, "BanksXML", DbType.String, CreateBanksXML(info.BankList));
            _db.AddInParameter(cmd, "LxrsXML", DbType.String, CreateLxrsXML(info.LinkManList));
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "DistrictId", DbType.Int32, info.CountyId);
            _db.AddInParameter(cmd, "CrmName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "JGCode", DbType.String, info.OrganizationCode);
            _db.AddInParameter(cmd, "DengJiBH", DbType.Int32, info.LevId);
            _db.AddInParameter(cmd, "FaRen", DbType.String, info.LegalRepresentative);
            _db.AddInParameter(cmd, "FaRenTelephone", DbType.String, info.LegalRepresentativePhone);
            _db.AddInParameter(cmd, "LicenseCode", DbType.String, info.License);
            _db.AddInParameter(cmd, "CaiWu", DbType.String, info.FinancialName);
            _db.AddInParameter(cmd, "CaiWuTelephone", DbType.String, info.FinancialPhone);
            _db.AddInParameter(cmd, "CaiWuMobile", DbType.String, info.FinancialMobile);
            _db.AddInParameter(cmd, "ZeRenXiaoShouBH", DbType.AnsiStringFixedLength, info.SellerId);
            _db.AddInParameter(cmd, "BriefCode", DbType.String, info.BrevityCode);
            _db.AddInParameter(cmd, "IsXieYi", DbType.AnsiStringFixedLength, info.IsSignContract ? "1" : "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "XieYiFuJianType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.AttachItemType.客户合作协议);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);
            _db.AddInParameter(cmd, "CrmType", DbType.Byte, info.Type);
            _db.AddInParameter(cmd, "FinancialFax", DbType.String, info.FinancialFax);
            _db.AddInParameter(cmd, "ForeignName", DbType.String, info.ForeignName);
            _db.AddInParameter(cmd, "PayType", DbType.String, (int)info.PayType);
            _db.AddInParameter(cmd, "Limit", DbType.Int32, info.Limit);
            _db.AddInParameter(cmd, "SaleName", DbType.String, info.SaleName);
            _db.AddInParameter(cmd, "SalePwd", DbType.String, info.SalePwd);
            _db.AddInParameter(cmd, "SaleState", DbType.AnsiStringFixedLength, info.SaleState ? "1" : "0");
            _db.AddInParameter(cmd, "PrintHeader", DbType.String, info.PrintHeader);
            _db.AddInParameter(cmd, "PrintFooter", DbType.String, info.PrintFooter);
            _db.AddInParameter(cmd, "PrintTemplates", DbType.String, info.PrintTemplates);

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
        /// 修改客户单位,返回1成功
        /// </summary>
        /// <param name="info">客户单位信息业务实体</param>
        /// <returns></returns>
        public int Update(Model.CrmStructure.MCrm info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_Update");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, info.CrmId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AttachXML", DbType.String, CreateHeZuoXieYiXML(info.AttachModel));
            _db.AddInParameter(cmd, "BanksXML", DbType.String, CreateBanksXML(info.BankList));
            _db.AddInParameter(cmd, "LxrsXML", DbType.String, CreateLxrsXML(info.LinkManList));
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "DistrictId", DbType.Int32, info.CountyId);
            _db.AddInParameter(cmd, "CrmName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "JGCode", DbType.String, info.OrganizationCode);
            _db.AddInParameter(cmd, "DengJiBH", DbType.Int32, info.LevId);
            _db.AddInParameter(cmd, "FaRen", DbType.String, info.LegalRepresentative);
            _db.AddInParameter(cmd, "FaRenTelephone", DbType.String, info.LegalRepresentativePhone);
            //_db.AddInParameter(cmd, "FaRenMobile", DbType.String, info.LegalRepresentativeMobile);
            _db.AddInParameter(cmd, "LicenseCode", DbType.String, info.License);
            _db.AddInParameter(cmd, "CaiWu", DbType.String, info.FinancialName);
            _db.AddInParameter(cmd, "CaiWuTelephone", DbType.String, info.FinancialPhone);
            _db.AddInParameter(cmd, "CaiWuMobile", DbType.String, info.FinancialMobile);
            //_db.AddInParameter(cmd, "FanLiZehngCe", DbType.String, info.RebatePolicy);
            _db.AddInParameter(cmd, "ZeRenXiaoShouBH", DbType.AnsiStringFixedLength, info.SellerId);
            _db.AddInParameter(cmd, "BriefCode", DbType.String, info.BrevityCode);
            //_db.AddInParameter(cmd, "QianKuanEDu", DbType.Decimal, info.AmountOwed);
            //_db.AddInParameter(cmd, "ZhangLingQiXian", DbType.Int32, info.Deadline);
            _db.AddInParameter(cmd, "IsXieYi", DbType.AnsiStringFixedLength, info.IsSignContract ? "1" : "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "XieYiFuJianType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.AttachItemType.客户合作协议);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);

            _db.AddInParameter(cmd, "FinancialFax", DbType.String, info.FinancialFax);
            _db.AddInParameter(cmd, "ForeignName", DbType.String, info.ForeignName);
            _db.AddInParameter(cmd, "PayType", DbType.String, (int)info.PayType);
            _db.AddInParameter(cmd, "Limit", DbType.Int32, info.Limit);
            _db.AddInParameter(cmd, "SaleName", DbType.String, info.SaleName);
            _db.AddInParameter(cmd, "SalePwd", DbType.String, info.SalePwd);
            _db.AddInParameter(cmd, "SaleState", DbType.AnsiStringFixedLength, info.SaleState ? "1" : "0");
            _db.AddInParameter(cmd, "PrintHeader", DbType.String, info.PrintHeader);
            _db.AddInParameter(cmd, "PrintFooter", DbType.String, info.PrintFooter);
            _db.AddInParameter(cmd, "PrintTemplates", DbType.String, info.PrintTemplates);

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
        /// 删除客户单位信息，返回1成功
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int Delete(string crmId, string operatorId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_Delete");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, operatorId);

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
        /// 获取客户单位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>        
        /// <param name="depts">数据级浏览权限控制-部门集合</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MLBCrmInfo> GetCrms(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType? crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo)
        {
            IList<Model.CrmStructure.MLBCrmInfo> items = new List<Model.CrmStructure.MLBCrmInfo>();

            string tableName = "tbl_Crm";
            string fields = "CrmId,CountryId,ProvinceId,CityId,CountyId,Type,Name,IsSignContract,SellerId,OperatorId,LevId,SellerName";
            string orderByString = "IssueTime DESC";
            StringBuilder query = new StringBuilder();

            #region SQL
            query.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            //query.AppendFormat(GetOrgCondition(userId, depts, "OperatorId", "DeptId"));
            if (depts != null && depts.Length == 1 && depts[0] == -1)//查看自己
            {
                query.AppendFormat(" AND SellerId='{0}' ", userId);
            }
            else
            {
                if (depts != null && depts.Length > 0)
                {
                    query.AppendFormat(" AND( EXISTS(SELECT 1 FROM tbl_ComUser AS A WHERE A.UserId=tbl_Crm.SellerId AND A.DeptId IN({0}) ) ", GetIdsByArr(depts));

                    if (!string.IsNullOrEmpty(userId))
                    {
                        query.AppendFormat(" OR SellerId='{0}' ", userId);
                    }

                    query.Append(" ) ");
                }
            }

            query.AppendFormat(" AND Type IN({0},{1}) ", (int)EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客, (int)EyouSoft.Model.EnumType.CrmStructure.CrmType.同行客户);

            if (crmType.HasValue)
            {
                query.AppendFormat(" AND Type={0} ", (int)crmType.Value);
            }

            query.Append(GetCrmsQuerySQL(searchInfo));

            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    var info = new Model.CrmStructure.MLBCrmInfo();


                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CompanyId = companyId;
                    info.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.DingDanJinE = 0;
                    info.DingDanRenShu = 0;
                    info.DingDanShu = 0;
                    info.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    info.IsXieYi = rdr.GetString(rdr.GetOrdinal("IsSignContract")) == "1";
                    info.LastTime = null;
                    info.LevId = rdr.GetInt32(rdr.GetOrdinal("LevId"));
                    info.Lxrs = null;
                    info.Name = rdr["Name"].ToString();
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.SellerId = rdr.GetString(rdr.GetOrdinal("SellerId"));
                    info.SellerName = rdr["SellerName"].ToString();
                    info.TuoQianJinE = 0;
                    info.Type = (EyouSoft.Model.EnumType.CrmStructure.CrmType)rdr.GetByte(rdr.GetOrdinal("Type"));
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));

                    items.Add(info);

                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetCrmLxrs(item.CrmId);

                    var jiaoYiMingXi = GetCrmJiaoYiMingXi(item.CrmId);

                    item.DingDanJinE = jiaoYiMingXi.DingDanJinE;
                    item.DingDanRenShu = jiaoYiMingXi.DingDanRenShu;
                    item.DingDanShu = jiaoYiMingXi.DingDanShu;
                    item.LastTime = jiaoYiMingXi.LatestTime;
                    item.TuoQianJinE = jiaoYiMingXi.TuoQianJinE;
                }
            }

            return items;
        }

        /// <summary>
        /// 获取客户单位信息集合(选用时使用)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="crmType">客户单位类型</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MLBCrmXuanYongInfo> GetCrmsXuanYong(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.CrmStructure.CrmType? crmType, EyouSoft.Model.CrmStructure.MLBCrmSearchInfo searchInfo)
        {
            IList<Model.CrmStructure.MLBCrmXuanYongInfo> items = new List<Model.CrmStructure.MLBCrmXuanYongInfo>();

            string tableName = "tbl_Crm";
            string fields = "CrmId,Name,Type,LevId,CountryId";
            string orderByString = "IssueTime DESC";
            StringBuilder query = new StringBuilder();

            #region SQL
            query.AppendFormat("CompanyId='{0}' AND IsDelete='0' ", companyId);
            query.AppendFormat(GetOrgCondition(userId, depts, "OperatorId", "DeptId"));

            if (crmType.HasValue)
            {
                query.AppendFormat(" AND Type={0} ", (int)crmType.Value);
            }

            query.Append(GetCrmsQuerySQL(searchInfo));
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    var info = new Model.CrmStructure.MLBCrmXuanYongInfo();

                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.Lxrs = null;
                    info.Name = rdr["Name"].ToString();
                    info.CrmType = (EyouSoft.Model.EnumType.CrmStructure.CrmType)rdr.GetByte(rdr.GetOrdinal("Type"));
                    info.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    info.KeHuDengJiBH = rdr.GetInt32(rdr.GetOrdinal("LevId"));

                    items.Add(info);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetCrmLxrs(item.CrmId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取客户单位信息业务实体
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrm GetInfo(string crmId)
        {
            Model.CrmStructure.MCrm info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.CrmStructure.MCrm();

                    info.Address = rdr["Address"].ToString();
                    info.AmountOwed = rdr.GetDecimal(rdr.GetOrdinal("AmountOwed"));
                    info.AttachModel = null;
                    info.BankList = null;
                    info.BrevityCode = rdr["ShortName"].ToString();
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    info.CountyId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    info.CrmId = crmId;
                    info.Deadline = rdr.GetInt32(rdr.GetOrdinal("Deadline"));
                    info.DeptId = rdr.GetInt32(rdr.GetOrdinal("OperatorDeptId"));
                    info.FinancialMobile = rdr["FinancialMobile"].ToString();
                    info.FinancialName = rdr["FinancialName"].ToString();
                    info.FinancialPhone = rdr["FinancialPhone"].ToString();
                    info.IsDelete = rdr.GetString(rdr.GetOrdinal("IsDelete")) == "1";
                    info.IsSignContract = rdr.GetString(rdr.GetOrdinal("IsSignContract")) == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.LegalRepresentative = rdr["LegalRepresentative"].ToString();
                    //info.LegalRepresentativeMobile = rdr["LegalRepresentativeMobile"].ToString();
                    info.LegalRepresentativePhone = rdr["LegalRepresentativePhone"].ToString();
                    info.LevId = rdr.GetInt32(rdr.GetOrdinal("LevId"));
                    info.License = rdr["License"].ToString();
                    info.LinkManList = null;
                    info.Name = rdr["Name"].ToString();
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.OrganizationCode = rdr["OrganizationCode"].ToString();
                    info.PrintFooter = rdr["PrintFooter"].ToString();
                    info.PrintHeader = rdr["PrintHeader"].ToString();
                    info.PrintTemplates = rdr["PrintLogo"].ToString();
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    //info.RebatePolicy = rdr["RebatePolicy"].ToString();
                    //info.Seal = rdr["Seal"].ToString();
                    info.SellerId = rdr["SellerId"].ToString();
                    info.SellerName = rdr["SellerName"].ToString();
                    info.Type = (EyouSoft.Model.EnumType.CrmStructure.CrmType)rdr.GetByte(rdr.GetOrdinal("Type"));

                    info.ForeignName = rdr["ForeignName"].ToString();
                    info.FinancialFax = rdr["FinancialFax"].ToString();
                    info.Limit = Utils.GetInt(rdr["Deadline"].ToString());
                    info.PayType = (EyouSoft.Model.EnumType.CrmStructure.PayType)Utils.GetInt(rdr["PayType"].ToString(), 1);
                    info.SaleName = rdr["SaleName"].ToString();
                    info.SalePwd = rdr["SalePwd"].ToString();
                    info.SaleState = rdr["SaleState"].ToString() == "1" ? true : false;
                }
            }

            if (info == null) return info;

            info.BankList = GetCrmBanks(crmId);
            info.AttachModel = GetCrmXieYiFuJian(crmId);
            info.LinkManList = GetCrmLxrs(crmId);

            return info;
        }

        /// <summary>
        /// 是否存在相同的客户单位名称
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmName">客户单位名称</param>
        /// <param name="crmId">客户单位编号</param>
        /// <returns></returns>
        public bool IsExistsCrmName(string companyId, string crmName, string crmId)
        {
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            string cmdText = "SELECT COUNT(*) FROM tbl_Crm WHERE CompanyId=@CompanyId AND [Name]=@Name";
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "Name", DbType.String, crmName);
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
        /// 修改客户单位的打印配置
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <param name="printHead">打印头</param>
        /// <param name="printFoot">打印尾</param>
        /// <param name="printTemplates">打印模板</param>
        /// <param name="seal">公章</param>
        /// <returns></returns>
        public bool UpdatePrintSet(string crmId, string printHead, string printFoot, string printTemplates, string seal)
        {
            string sql = "UPDATE tbl_Crm SET PrintHeader = @PrintHeader,PrintFooter = @PrintFooter WHERE CrmId =@CrmId";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "PrintHeader", DbType.String, printHead);
            _db.AddInParameter(cmd, "PrintFooter", DbType.String, printFoot);
            //_db.AddInParameter(cmd, "PrintTemplates", DbType.String, printTemplates);
            // _db.AddInParameter(cmd, "Seal", DbType.String, seal);
            _db.AddInParameter(cmd, "CrmId", DbType.String, crmId);

            return DbHelper.ExecuteSql(cmd, _db) == 1;
        }

        /// <summary>
        /// 获得客户单位联系人列表(含账号信息)
        /// </summary>
        /// <param name="crmId">客户编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmUserInfo> GetCrmUsers(string crmId)
        {
            IList<Model.CrmStructure.MCrmUserInfo> items = new List<Model.CrmStructure.MCrmUserInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCrmUsers);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var info = new Model.CrmStructure.MCrmUserInfo();
                    info.Username = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? string.Empty : rdr["UserName"].ToString();
                    //info.Address = rdr.IsDBNull(rdr.GetOrdinal("Address")) ? string.Empty : rdr["Address"].ToString();
                    info.Birthday = rdr.IsDBNull(rdr.GetOrdinal("Birthday")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    info.Department = rdr.IsDBNull(rdr.GetOrdinal("Department")) ? string.Empty : rdr["Department"].ToString();
                    info.Fax = rdr.IsDBNull(rdr.GetOrdinal("Fax")) ? string.Empty : rdr["Fax"].ToString();
                    info.LinkManId = rdr.IsDBNull(rdr.GetOrdinal("Id")) ? string.Empty : rdr["Id"].ToString();
                    info.Mobile = rdr.IsDBNull(rdr.GetOrdinal("MobilePhone")) ? string.Empty : rdr["MobilePhone"].ToString();
                    info.Name = rdr.IsDBNull(rdr.GetOrdinal("Name")) ? string.Empty : rdr["Name"].ToString();
                    info.Pwd = rdr.IsDBNull(rdr.GetOrdinal("Password")) ? string.Empty : rdr["Password"].ToString();
                    info.QQ = rdr.IsDBNull(rdr.GetOrdinal("QQ")) ? string.Empty : rdr["QQ"].ToString();
                    info.Status = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.UserStatus>(rdr["UserStatus"].ToString(), EyouSoft.Model.EnumType.ComStructure.UserStatus.未启用);
                    info.Telephone = rdr.IsDBNull(rdr.GetOrdinal("Telephone")) ? string.Empty : rdr["Telephone"].ToString();
                    info.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? string.Empty : rdr["UserId"].ToString().Trim();



                    items.Add(info);
                }
            }

            return items;
        }

        /// <summary>
        /// 设置客户单位用户账号状态
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="status">用户状态</param>
        /// <returns></returns>
        public bool SetCrmUserStatus(string companyId, string crmId, string userId, EyouSoft.Model.EnumType.ComStructure.UserStatus status)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SetCrmUserStatus);
            _db.AddInParameter(cmd, "Status", DbType.Byte, status);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "UserType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.UserType.组团社);

            return DbHelper.ExecuteSql(cmd, _db) == 1;
        }

        /// <summary>
        /// 设置客户单位联系人账号信息，已经分配过账号的做密码修改操作，返回1成功
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="lxrId">联系人编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        public int SetCrmUser(string companyId, string operatorId, string crmId, string lxrId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Crm_SetCrmUser");
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "LxrId", DbType.AnsiStringFixedLength, lxrId);
            _db.AddInParameter(cmd, "Username", DbType.String, username);
            _db.AddInParameter(cmd, "NoEncryptPassword", DbType.String, pwd.NoEncryptPassword);
            _db.AddInParameter(cmd, "MD5Password", DbType.String, pwd.MD5Password);
            _db.AddInParameter(cmd, "UserType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.UserType.组团社);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, operatorId);

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
        /// 获取个人会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CrmStructure.MLBCrmPersonalInfo> GetCrmsPersonal(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CrmStructure.MLBCrmPersonalSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.CrmStructure.MLBCrmPersonalInfo> items = new List<EyouSoft.Model.CrmStructure.MLBCrmPersonalInfo>();
            string tableName = "view_Crm_Personal";
            string fields = "CrmId,CountryId,ProvinceId,CityId,CountyId,Type,Name,OperatorId,MemberTypeName,MemberCardNumber,Gender,ContactPhone,MobilePhone,AvailableIntegral,SellerId,CardType,IdNumber,QianFaDate,CardValidDate,Birthday,QianFaDi,Remark";
            string orderByString = "IssueTime DESC";
            StringBuilder query = new StringBuilder();

            #region SQL
            query.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            query.AppendFormat(" AND Type={0} ", (int)EyouSoft.Model.EnumType.CrmStructure.CrmType.个人会员);
            //query.AppendFormat(GetOrgCondition(userId, depts, "OperatorId", "DeptId"));

            if (depts != null && depts.Length == 1 && depts[0] == -1)//查看自己
            {
                query.AppendFormat(" AND SellerId='{0}' ", userId);
            }
            else
            {
                if (depts != null && depts.Length > 0)
                {
                    query.AppendFormat(" AND( EXISTS(SELECT 1 FROM tbl_ComUser AS A WHERE A.UserId=view_Crm_Personal.SellerId AND A.DeptId IN({0}) ) ", GetIdsByArr(depts));

                    if (!string.IsNullOrEmpty(userId))
                    {
                        query.AppendFormat(" OR SellerId='{0}' ", userId);
                    }

                    query.Append(" ) ");
                }
            }

            if (searchInfo != null)
            {

                if (searchInfo.JiFenOperator.HasValue && searchInfo.JiFenOperatorNumber.HasValue)
                {
                    string _operator = "=";
                    switch (searchInfo.JiFenOperator.Value)
                    {
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于: _operator = ">="; break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.等于: _operator = "="; break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于: _operator = "<="; break;
                        default: break;
                    }

                    query.AppendFormat(" AND AvailableIntegral{0}{1} ", _operator, searchInfo.JiFenOperatorNumber.Value);
                }

                if (!string.IsNullOrEmpty(searchInfo.MemberCardCode))
                {
                    query.AppendFormat(" AND MemberCardNumber LIKE '%{0}%' ", searchInfo.MemberCardCode);
                }
                if (searchInfo.MemberTypeId.HasValue)
                {
                    query.AppendFormat(" AND MemberType={0} ", searchInfo.MemberTypeId.Value);
                }
                if (!string.IsNullOrEmpty(searchInfo.Name))
                {
                    query.AppendFormat(" AND Name LIKE '%{0}%' ", searchInfo.Name);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    var info = new Model.CrmStructure.MLBCrmPersonalInfo();

                    info.CrmId = rdr.GetString(rdr.GetOrdinal("CrmId"));
                    info.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("Gender"));
                    info.JiFen = rdr.GetDecimal(rdr.GetOrdinal("AvailableIntegral"));
                    info.MemberCardCode = rdr["MemberCardNumber"].ToString();
                    info.MemberTypeName = rdr["MemberTypeName"].ToString();
                    info.Mobile = rdr["MobilePhone"].ToString();
                    info.Name = rdr["Name"].ToString();
                    info.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    info.Telephone = rdr["ContactPhone"].ToString();
                    info.SellerId = rdr["SellerId"].ToString();
                    info.IdNumber = rdr["IdNumber"].ToString();
                    info.QianFaDate = Utils.GetDateTimeNullable(rdr["QianFaDate"].ToString());
                    info.CardValidDate = Utils.GetDateTimeNullable(rdr["CardValidDate"].ToString());
                    info.Birthday = Utils.GetDateTimeNullable(rdr["Birthday"].ToString());
                    info.QianFaDi = rdr["QianFaDi"].ToString();
                    info.Remark = rdr["Remark"].ToString();
                    items.Add(info);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    var jiaoYiMingXi = GetCrmJiaoYiMingXi(item.CrmId);

                    item.DingDanJinE = jiaoYiMingXi.DingDanJinE;
                    item.DingDanRenShu = jiaoYiMingXi.DingDanRenShu;
                    item.DingDanShu = jiaoYiMingXi.DingDanShu;
                    item.LatestTime = jiaoYiMingXi.LatestTime;
                    item.TuoQianJinE = jiaoYiMingXi.TuoQianJinE;
                }
            }

            return items;
        }

        /// <summary>
        /// 验证客户单位责任销售，返回真匹配，返回假不匹配
        /// </summary>
        /// <param name="crmId">客户单位编号</param>
        /// <param name="sellerId">销售员编号</param>
        /// <returns></returns>
        public bool YanZhengZeRenXiaoShou(string crmId, string sellerId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_YanZhengZeRenXiaoShou);
            _db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, crmId);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, sellerId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    if (rdr.GetInt32(0) == 1) return true;
                }
            }

            return false;
        }
        #endregion
    }
}
