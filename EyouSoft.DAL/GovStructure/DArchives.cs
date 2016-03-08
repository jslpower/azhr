using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml.Linq;
using EyouSoft.Toolkit;
namespace EyouSoft.DAL.GovStructure
{
    using EyouSoft.Model.EnumType.GovStructure;
    using EyouSoft.Toolkit.DAL;

    /// <summary>
    /// 档案信息数据访问层
    /// 2011-09-22 邵权江 创建
    /// </summary>
    public class DArchives : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IArchives
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DArchives()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名

        #endregion

        #region 成员方法
        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsIDNumber(string IDNumber, string Id, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovFile WHERE CompanyId=@CompanyId AND IDNumber=@IDNumber ";
            if (Id != "")
            {
                StrSql += " AND ID<>'@ID'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (Id != "")
            {
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Id);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "IDNumber", DbType.String, IDNumber);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 判断档案编号是否存在
        /// </summary>
        /// <param name="FileNumber">档案编号</param>
        /// <param name="Id">档案Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsFileNumber(string FileNumber, string Id, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovFile WHERE CompanyId=@CompanyId AND FileNumber=@FileNumber ";
            if (Id != "")
            {
                StrSql += " AND ID<>'@ID'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (Id != "")
            {
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Id);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "FileNumber", DbType.String, FileNumber);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 添加档案信息
        /// </summary>
        /// <param name="model">档案实体</param>
        /// <returns>-1：存在相同的身份证号  0：添加失败  1： 添加成功</returns>
        public int AddArchives(EyouSoft.Model.GovStructure.MGovFile model)
        {
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovArchives_Add");
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "FileNumber", DbType.String, model.FileNumber);
            this._db.AddInParameter(dc, "Name", DbType.String, model.Name);
            this._db.AddInParameter(dc, "Sex", DbType.Byte, (int)model.Sex);
            this._db.AddInParameter(dc, "IDNumber", DbType.String, model.IDNumber);
            this._db.AddInParameter(dc, "BirthDate", DbType.DateTime, model.BirthDate);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "StaffPhoto", DbType.String, model.StaffPhoto);
            this._db.AddInParameter(dc, "StaffType", DbType.Byte, (int)model.StaffType);
            this._db.AddInParameter(dc, "StaffStatus", DbType.Byte, (int)model.StaffStatus);
            this._db.AddInParameter(dc, "EntryTime", DbType.DateTime, model.EntryTime);
            this._db.AddInParameter(dc, "LengthService", DbType.Int32, model.LengthService);
            this._db.AddInParameter(dc, "IsMarriage", DbType.AnsiStringFixedLength, model.IsMarriage == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "Nation", DbType.String, model.Nation);
            this._db.AddInParameter(dc, "Birthplace", DbType.String, model.Birthplace);
            this._db.AddInParameter(dc, "PoliticalFace", DbType.String, model.PoliticalFace);
            this._db.AddInParameter(dc, "Contact", DbType.String, model.Contact);
            this._db.AddInParameter(dc, "ContactShort", DbType.String, model.ContactShort);
            this._db.AddInParameter(dc, "Mobile", DbType.String, model.Mobile);
            this._db.AddInParameter(dc, "MobileShort", DbType.String, model.MobileShort);
            this._db.AddInParameter(dc, "qq", DbType.String, model.qq);
            this._db.AddInParameter(dc, "Msn", DbType.String, model.Msn);
            this._db.AddInParameter(dc, "Email", DbType.String, model.Email);
            this._db.AddInParameter(dc, "Address", DbType.String, model.Address);
            this._db.AddInParameter(dc, "Remarks", DbType.String, model.Remarks);
            this._db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserId);//分配用户后关联用户ID
            this._db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, model.GuideId);//同步导游后关联导游ID
            this._db.AddInParameter(dc, "UserName", DbType.String, model.UserName);
            this._db.AddInParameter(dc, "Password", DbType.String, model.Password);
            this._db.AddInParameter(dc, "MD5Password", DbType.String, model.MD5Password);
            this._db.AddInParameter(dc, "Operator", DbType.String, model.Operator);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "OperDeptId", DbType.Int32, model.OperDeptId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));//附件
            //this._db.AddInParameter(dc, "GovFileDeptXML", DbType.Xml, CreateGovFileDeptListXML(model.GovFileDeptList));//所属部门业务实体集合
            this._db.AddInParameter(dc, "GovFilePositionXML", DbType.Xml, CreateGovFilePositionListXML(model.GovFilePositionList));//职务关系业务实体集合
            this._db.AddInParameter(dc, "GovFileEducationXML", DbType.Xml, CreateGovFileEducationListXML(model.GovFileEducationList));//学历信息业务实体集合
            this._db.AddInParameter(dc, "GovFileCurriculumXML", DbType.Xml, CreateGovFileCurriculumListXML(model.GovFileCurriculumList));//履历业务实体集合
            this._db.AddInParameter(dc, "GovFilehomeXML", DbType.Xml, CreateGovFilehomeListXML(model.GovFilehomeList));//家庭关系业务实体集合
            this._db.AddInParameter(dc, "GovFileContractXML", DbType.Xml, CreateGovFileContractListXML(model.GovFileContractList));//劳动合同表业务实体集合
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 更新档案信息
        /// </summary>
        /// <param name="model">档案实体</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="IsUser">是否用户</param>
        /// <param name="IsGuide">是否导游</param>
        /// <returns>-1：存在相同的身份证号  -2:已经存在相同的用户名称  -3:已经存在相同的身份证号的导游  0：添加失败  1： 添加成功</returns>
        public int UpdateArchives(EyouSoft.Model.GovStructure.MGovFile model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, bool IsUser, bool IsGuide)
        {
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovArchives_Update");
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "FileNumber", DbType.String, model.FileNumber);
            this._db.AddInParameter(dc, "Name", DbType.String, model.Name);
            this._db.AddInParameter(dc, "Sex", DbType.Byte, (int)model.Sex);
            this._db.AddInParameter(dc, "IDNumber", DbType.String, model.IDNumber);
            this._db.AddInParameter(dc, "BirthDate", DbType.DateTime, model.BirthDate);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "StaffPhoto", DbType.String, model.StaffPhoto);
            this._db.AddInParameter(dc, "StaffType", DbType.Byte, (int)model.StaffType);
            this._db.AddInParameter(dc, "StaffStatus", DbType.Byte, (int)model.StaffStatus);
            this._db.AddInParameter(dc, "EntryTime", DbType.DateTime, model.EntryTime);
            this._db.AddInParameter(dc, "LengthService", DbType.Int32, model.LengthService);
            this._db.AddInParameter(dc, "IsMarriage", DbType.AnsiStringFixedLength, model.IsMarriage == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "Nation", DbType.String, model.Nation);
            this._db.AddInParameter(dc, "Birthplace", DbType.String, model.Birthplace);
            this._db.AddInParameter(dc, "PoliticalFace", DbType.String, model.PoliticalFace);
            this._db.AddInParameter(dc, "Contact", DbType.String, model.Contact);
            this._db.AddInParameter(dc, "ContactShort", DbType.String, model.ContactShort);
            this._db.AddInParameter(dc, "Mobile", DbType.String, model.Mobile);
            this._db.AddInParameter(dc, "MobileShort", DbType.String, model.MobileShort);
            this._db.AddInParameter(dc, "qq", DbType.String, model.qq);
            this._db.AddInParameter(dc, "Msn", DbType.String, model.Msn);
            this._db.AddInParameter(dc, "Email", DbType.String, model.Email);
            this._db.AddInParameter(dc, "Address", DbType.String, model.Address);
            this._db.AddInParameter(dc, "Remarks", DbType.String, model.Remarks);
            this._db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserId);//分配用户后关联用户ID
            this._db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, model.GuideId);//同步导游后关联导游ID
            this._db.AddInParameter(dc, "IsUser", DbType.AnsiStringFixedLength, IsUser == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "IsGuide", DbType.AnsiStringFixedLength, IsGuide == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "UserName", DbType.String, model.UserName);
            this._db.AddInParameter(dc, "Password", DbType.String, model.Password);
            this._db.AddInParameter(dc, "MD5Password", DbType.String, model.MD5Password);
            this._db.AddInParameter(dc, "Operator", DbType.String, model.Operator);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "OperDeptId", DbType.Int32, model.OperDeptId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));//附件
            //this._db.AddInParameter(dc, "GovFileDeptXML", DbType.Xml, CreateGovFileDeptListXML(model.GovFileDeptList));//所属部门业务实体集合
            this._db.AddInParameter(dc, "GovFilePositionXML", DbType.Xml, CreateGovFilePositionListXML(model.GovFilePositionList));//职务关系业务实体集合
            this._db.AddInParameter(dc, "GovFileEducationXML", DbType.Xml, CreateGovFileEducationListXML(model.GovFileEducationList));//学历信息业务实体集合
            this._db.AddInParameter(dc, "GovFileCurriculumXML", DbType.Xml, CreateGovFileCurriculumListXML(model.GovFileCurriculumList));//履历业务实体集合
            this._db.AddInParameter(dc, "GovFilehomeXML", DbType.Xml, CreateGovFilehomeListXML(model.GovFilehomeList));//家庭关系业务实体集合
            this._db.AddInParameter(dc, "GovFileContractXML", DbType.Xml, CreateGovFileContractListXML(model.GovFileContractList));//劳动合同表业务实体集合
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 根据档案id获取档案实体
        /// </summary>
        /// <param name="ID">档案ID</param>
        /// <returns>true:成功，false:失败</returns>
        public EyouSoft.Model.GovStructure.MGovFile GetArchivesModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovFile model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT ID,CompanyId,FileNumber,[Name],Sex,IDNumber,BirthDate,DepartId,StaffPhoto,StaffType,StaffStatus,EntryTime,LengthService, ");
            StrSql.Append("IsMarriage,Nation,Birthplace,PoliticalFace,Contact,ContactShort,Mobile,MobileShort,qq,Msn,Email,[Address],Remarks,UserId,GuideId,OperatorId,IssueTime, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.ID FOR XML RAW,ROOT('ROOT'))AS ComAttachXML,", (int)ItemType);
            //StrSql.Append(" (SELECT DepartId,DepartName FROM tbl_ComDepartment WHERE DepartId IN(SELECT DepartId from tbl_GovFileDept WHERE FileId=a.ID) FOR XML RAW,ROOT('ROOT'))AS DepartmentXML,");
            StrSql.Append(" (SELECT top 1 DepartName FROM tbl_ComDepartment WHERE DepartId=a.DepartId )AS DepartName,");
            StrSql.Append(" (SELECT top 1 UserName FROM tbl_ComUser WHERE UserId=a.UserId )AS UserName,");
            StrSql.Append(" (SELECT top 1 Password FROM tbl_ComUser WHERE UserId=a.UserId )AS Password,");
            StrSql.Append(" (SELECT PositionId,Title FROM tbl_GovPosition WHERE PositionId IN(SELECT PositionId from tbl_GovFilePosition WHERE FileId=a.ID) FOR XML RAW,ROOT('ROOT'))AS PositionXML,");
            StrSql.Append(" (SELECT FileId,StartTime,EndTime,Education,Profession,Graduated,Statue,Remarks FROM tbl_GovFileEducation WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS EducationXML,");
            StrSql.Append(" (SELECT FileId,StartTime,EndTime,Location,WorkUnit,Occupation,Remarks FROM tbl_GovFileCurriculum WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS CurriculumXML,");
            StrSql.Append(" (SELECT FileId,Relationship,[Name],Phone,WorkUnit,[Address] FROM tbl_GovFilehome WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS HomeXML,");
            StrSql.Append(" (SELECT FileId,ContractNumber,[Name],SignedTime,MaturityTime,[Status],Remarks FROM tbl_GovFileContract WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS ContractXML ");
            StrSql.AppendFormat(" FROM tbl_GovFile a WHERE a.ID='{0}' ", ID);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFile();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? dr.GetString(dr.GetOrdinal("CompanyId")) : "";
                    model.FileNumber = !dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? dr.GetString(dr.GetOrdinal("FileNumber")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), dr.GetByte(dr.GetOrdinal("Sex")).ToString());
                    model.IDNumber = !dr.IsDBNull(dr.GetOrdinal("IDNumber")) ? dr.GetString(dr.GetOrdinal("IDNumber")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("BirthDate")))
                    {
                        model.BirthDate = dr.GetDateTime(dr.GetOrdinal("BirthDate"));
                    }
                    model.DepartId = dr.GetInt32(dr.GetOrdinal("DepartId")); ;
                    model.DepartName = !dr.IsDBNull(dr.GetOrdinal("DepartName")) ? dr.GetString(dr.GetOrdinal("DepartName")) : "";
                    model.UserName = !dr.IsDBNull(dr.GetOrdinal("UserName")) ? dr.GetString(dr.GetOrdinal("UserName")) : "";
                    model.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : "";
                    model.StaffPhoto = !dr.IsDBNull(dr.GetOrdinal("StaffPhoto")) ? dr.GetString(dr.GetOrdinal("StaffPhoto")) : "";
                    model.StaffType = (EyouSoft.Model.EnumType.GovStructure.StaffType)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffType), dr.GetByte(dr.GetOrdinal("StaffType")).ToString());
                    model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("StaffStatus")).ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("EntryTime")))
                    {
                        model.EntryTime = dr.GetDateTime(dr.GetOrdinal("EntryTime"));
                    }
                    model.LengthService = dr.IsDBNull(dr.GetOrdinal("LengthService")) ? 0 : dr.GetInt32(dr.GetOrdinal("LengthService"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsMarriage")))
                    {
                        model.IsMarriage = dr.GetString(dr.GetOrdinal("IsMarriage")) == "1" ? true : false;
                    }
                    model.Nation = !dr.IsDBNull(dr.GetOrdinal("Nation")) ? dr.GetString(dr.GetOrdinal("Nation")) : "";
                    model.Birthplace = !dr.IsDBNull(dr.GetOrdinal("Birthplace")) ? dr.GetString(dr.GetOrdinal("Birthplace")) : "";
                    model.PoliticalFace = !dr.IsDBNull(dr.GetOrdinal("PoliticalFace")) ? dr.GetString(dr.GetOrdinal("PoliticalFace")) : "";
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : "";
                    model.ContactShort = !dr.IsDBNull(dr.GetOrdinal("ContactShort")) ? dr.GetString(dr.GetOrdinal("ContactShort")) : "";
                    model.Mobile = !dr.IsDBNull(dr.GetOrdinal("Mobile")) ? dr.GetString(dr.GetOrdinal("Mobile")) : "";
                    model.MobileShort = !dr.IsDBNull(dr.GetOrdinal("MobileShort")) ? dr.GetString(dr.GetOrdinal("MobileShort")) : "";
                    model.qq = !dr.IsDBNull(dr.GetOrdinal("qq")) ? dr.GetString(dr.GetOrdinal("qq")) : "";
                    model.Msn = !dr.IsDBNull(dr.GetOrdinal("Msn")) ? dr.GetString(dr.GetOrdinal("Msn")) : "";
                    model.Email = !dr.IsDBNull(dr.GetOrdinal("Email")) ? dr.GetString(dr.GetOrdinal("Email")) : "";
                    model.Address = !dr.IsDBNull(dr.GetOrdinal("Address")) ? dr.GetString(dr.GetOrdinal("Address")) : "";
                    model.Remarks = !dr.IsDBNull(dr.GetOrdinal("Remarks")) ? dr.GetString(dr.GetOrdinal("Remarks")) : "";
                    model.UserId = !dr.IsDBNull(dr.GetOrdinal("UserId")) ? dr.GetString(dr.GetOrdinal("UserId")) : "";
                    model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), ID, ItemType);
                    //model.GovFileDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString());
                    model.GovFilePositionList = this.GetPositionList(dr["PositionXML"].ToString());
                    model.GovFileEducationList = this.GetEducationList(dr["EducationXML"].ToString());
                    model.GovFileCurriculumList = this.GetCurriculumList(dr["CurriculumXML"].ToString());
                    model.GovFilehomeList = this.GetHomeList(dr["HomeXML"].ToString());
                    model.GovFileContractList = this.GetContractList(dr["ContractXML"].ToString());
                }
            };
            return model;
        }

        /// <summary>
        /// 根据用户id获取档案实体
        /// </summary>
        /// <param name="UserId">用户UserId</param>
        /// <returns>true:成功，false:失败</returns>
        public EyouSoft.Model.GovStructure.MGovFile GetArchivesModelByUserId(string UserId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovFile model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT ID,CompanyId,FileNumber,[Name],Sex,IDNumber,BirthDate,DepartId,StaffPhoto,StaffType,StaffStatus,EntryTime,LengthService, ");
            StrSql.Append("IsMarriage,Nation,Birthplace,PoliticalFace,Contact,ContactShort,Mobile,MobileShort,qq,Msn,Email,[Address],Remarks,UserId,GuideId,OperatorId,IssueTime, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.ID FOR XML RAW,ROOT('ROOT'))AS ComAttachXML,", (int)ItemType);
            //StrSql.Append(" (SELECT DepartId,DepartName FROM tbl_ComDepartment WHERE DepartId IN(SELECT DepartId from tbl_GovFileDept WHERE FileId=a.ID) FOR XML RAW,ROOT('ROOT'))AS DepartmentXML,");
            StrSql.Append(" (SELECT top 1 DepartName FROM tbl_ComDepartment WHERE DepartId=a.DepartId )AS DepartName,");
            StrSql.Append(" (SELECT top 1 UserName FROM tbl_ComUser WHERE UserId=a.UserId )AS UserName,");
            StrSql.Append(" (SELECT top 1 Password FROM tbl_ComUser WHERE UserId=a.UserId )AS Password,");
            StrSql.Append(" (SELECT PositionId,Title FROM tbl_GovPosition WHERE PositionId IN(SELECT PositionId from tbl_GovFilePosition WHERE FileId=a.ID) FOR XML RAW,ROOT('ROOT'))AS PositionXML,");
            StrSql.Append(" (SELECT FileId,StartTime,EndTime,Education,Profession,Graduated,Statue,Remarks FROM tbl_GovFileEducation WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS EducationXML,");
            StrSql.Append(" (SELECT FileId,StartTime,EndTime,Location,WorkUnit,Occupation,Remarks FROM tbl_GovFileCurriculum WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS CurriculumXML,");
            StrSql.Append(" (SELECT FileId,Relationship,[Name],Phone,WorkUnit,[Address] FROM tbl_GovFilehome WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS HomeXML,");
            StrSql.Append(" (SELECT FileId,ContractNumber,[Name],SignedTime,MaturityTime,[Status],Remarks FROM tbl_GovFileContract WHERE FileId=a.ID FOR XML RAW,ROOT('ROOT'))AS ContractXML ");
            StrSql.AppendFormat(" FROM tbl_GovFile a WHERE a.UserId='{0}' ", UserId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFile();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? dr.GetString(dr.GetOrdinal("CompanyId")) : "";
                    model.FileNumber = !dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? dr.GetString(dr.GetOrdinal("FileNumber")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), dr.GetByte(dr.GetOrdinal("Sex")).ToString());
                    model.IDNumber = !dr.IsDBNull(dr.GetOrdinal("IDNumber")) ? dr.GetString(dr.GetOrdinal("IDNumber")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("BirthDate")))
                    {
                        model.BirthDate = dr.GetDateTime(dr.GetOrdinal("BirthDate"));
                    }
                    model.DepartId = model.DepartId;
                    model.DepartName = !dr.IsDBNull(dr.GetOrdinal("DepartName")) ? dr.GetString(dr.GetOrdinal("DepartName")) : "";
                    model.UserName = !dr.IsDBNull(dr.GetOrdinal("UserName")) ? dr.GetString(dr.GetOrdinal("UserName")) : "";
                    model.Password = !dr.IsDBNull(dr.GetOrdinal("Password")) ? dr.GetString(dr.GetOrdinal("Password")) : "";
                    model.StaffPhoto = !dr.IsDBNull(dr.GetOrdinal("StaffPhoto")) ? dr.GetString(dr.GetOrdinal("StaffPhoto")) : "";
                    model.StaffType = (EyouSoft.Model.EnumType.GovStructure.StaffType)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffType), dr.GetByte(dr.GetOrdinal("StaffType")).ToString());
                    model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("StaffStatus")).ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("EntryTime")))
                    {
                        model.EntryTime = dr.GetDateTime(dr.GetOrdinal("EntryTime"));
                    }
                    model.LengthService = dr.IsDBNull(dr.GetOrdinal("LengthService")) ? 0 : dr.GetInt32(dr.GetOrdinal("LengthService"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsMarriage")))
                    {
                        model.IsMarriage = dr.GetString(dr.GetOrdinal("IsMarriage")) == "1" ? true : false;
                    }
                    model.Nation = !dr.IsDBNull(dr.GetOrdinal("Nation")) ? dr.GetString(dr.GetOrdinal("Nation")) : "";
                    model.Birthplace = !dr.IsDBNull(dr.GetOrdinal("Birthplace")) ? dr.GetString(dr.GetOrdinal("Birthplace")) : "";
                    model.PoliticalFace = !dr.IsDBNull(dr.GetOrdinal("PoliticalFace")) ? dr.GetString(dr.GetOrdinal("PoliticalFace")) : "";
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : "";
                    model.ContactShort = !dr.IsDBNull(dr.GetOrdinal("ContactShort")) ? dr.GetString(dr.GetOrdinal("ContactShort")) : "";
                    model.Mobile = !dr.IsDBNull(dr.GetOrdinal("Mobile")) ? dr.GetString(dr.GetOrdinal("Mobile")) : "";
                    model.MobileShort = !dr.IsDBNull(dr.GetOrdinal("MobileShort")) ? dr.GetString(dr.GetOrdinal("MobileShort")) : "";
                    model.qq = !dr.IsDBNull(dr.GetOrdinal("qq")) ? dr.GetString(dr.GetOrdinal("qq")) : "";
                    model.Msn = !dr.IsDBNull(dr.GetOrdinal("Msn")) ? dr.GetString(dr.GetOrdinal("Msn")) : "";
                    model.Email = !dr.IsDBNull(dr.GetOrdinal("Email")) ? dr.GetString(dr.GetOrdinal("Email")) : "";
                    model.Address = !dr.IsDBNull(dr.GetOrdinal("Address")) ? dr.GetString(dr.GetOrdinal("Address")) : "";
                    model.Remarks = !dr.IsDBNull(dr.GetOrdinal("Remarks")) ? dr.GetString(dr.GetOrdinal("Remarks")) : "";
                    model.UserId = !dr.IsDBNull(dr.GetOrdinal("UserId")) ? dr.GetString(dr.GetOrdinal("UserId")) : "";
                    model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("ID")), ItemType);
                    //model.GovFileDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString());
                    model.GovFilePositionList = this.GetPositionList(dr["PositionXML"].ToString());
                    model.GovFileEducationList = this.GetEducationList(dr["EducationXML"].ToString());
                    model.GovFileCurriculumList = this.GetCurriculumList(dr["CurriculumXML"].ToString());
                    model.GovFilehomeList = this.GetHomeList(dr["HomeXML"].ToString());
                    model.GovFileContractList = this.GetContractList(dr["ContractXML"].ToString());
                }
            };
            return model;
        }

        /// <summary>
        /// 根获取档案列表
        /// </summary>
        /// <param name="SearchModel">查询实体</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(EyouSoft.Model.GovStructure.MSearchGovFile SearchModel, string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFile> ResultList = null;
            string tableName = "view_GovFile";
            string identityColumnName = "ID";
            string fields = " ID,CompanyId,FileNumber,[Name],Sex,BirthDate,DepartId,LengthService,Contact,Mobile,qq,StaffType,StaffStatus,Account,IsMarriage,OperatorId,IssueTime,DepartName,PositionXML,Education,MaturityTime  ";
            string query = string.Format(" CompanyId='{0}' ", CompanyId);
            string StaffStatus = "  AND StaffStatus <>2 ";
            if (SearchModel != null)
            {
                if (!string.IsNullOrEmpty(SearchModel.FileNumber))
                {
                    query = query + string.Format(" AND FileNumber LIKE '%{0}%'", SearchModel.FileNumber);
                }
                if (!string.IsNullOrEmpty(SearchModel.Name))
                {
                    query = query + string.Format(" AND [Name] LIKE '%{0}%' ", SearchModel.Name);
                }
                if (SearchModel.Sex != null && SearchModel.Sex >= 0)
                {
                    query = query + string.Format(" AND Sex = {0} ", (int)SearchModel.Sex);
                }
                if (SearchModel.BirthDateBegin != null)
                {
                    query = query + string.Format(" AND BirthDate>='{0}' ", SearchModel.BirthDateBegin.Value.ToShortDateString() + " 00:00:00");
                }
                if (SearchModel.BirthDateEnd != null)
                {
                    query = query + string.Format(" AND BirthDate<='{0}' ", SearchModel.BirthDateEnd.Value.ToShortDateString() + " 23:59:59");
                }
                if (SearchModel.LengthService != null && SearchModel.LengthService >= 0)
                {
                    query = query + string.Format(" AND LengthService = {0} ", SearchModel.LengthService);
                }
                if (SearchModel.PositionId != null && SearchModel.PositionId > 0)
                {
                    //query = query + string.Format(" AND CAST(PositionXML AS XML).exist('/ROOT/row[@PositionId=sql:variable(\"{0}\")]') = 1 ", SearchModel.PositionId.ToString());
                    query = query + string.Format(" AND CAST(PositionXML AS XML).exist('/ROOT/row/@PositionId[.=\"{0}\"]') = 1", SearchModel.PositionId.ToString());
                }
                if (!string.IsNullOrEmpty(SearchModel.Position))
                {
                    query = query + string.Format(" AND PositionXML LIKE '%{0}%'", SearchModel.Position);
                }
                if (SearchModel.StaffType != null && SearchModel.StaffType > 0)
                {
                    query = query + string.Format(" AND StaffType = {0} ", (int)SearchModel.StaffType);
                }
                if (SearchModel.StaffStatus != null && SearchModel.StaffStatus > 0)
                {
                    StaffStatus = string.Format(" AND StaffStatus = {0} ", (int)SearchModel.StaffStatus);
                }
                if (SearchModel.NoLeft != null && SearchModel.NoLeft.Value)
                {
                    StaffStatus = string.Format(" AND StaffStatus {0}2 ", SearchModel.NoLeft.Value ? "<>" : "=");
                }
                if (SearchModel.IsMarriage != null)
                {
                    query = query + string.Format(" AND IsMarriage = '{0}' ", SearchModel.IsMarriage == true ? "1" : "0");
                }
                if (SearchModel.IsAccount != null)
                {
                    query = query + string.Format(" AND LEN(Account){0}0 ", SearchModel.IsAccount == true ? ">" : "=");
                }
            }
            query = query + StaffStatus;
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovFile>();
                EyouSoft.Model.GovStructure.MGovFile model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFile();
                    model.ID = !dr.IsDBNull(dr.GetOrdinal("ID")) ? dr.GetString(dr.GetOrdinal("ID")) : "";
                    model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? dr.GetString(dr.GetOrdinal("CompanyId")) : "";
                    model.FileNumber = !dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? dr.GetString(dr.GetOrdinal("FileNumber")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    //model.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), dr.GetByte(dr.GetOrdinal("Sex")).ToString());
                    model.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)dr.GetByte(dr.GetOrdinal("Sex"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BirthDate")))
                    {
                        model.BirthDate = dr.GetDateTime(dr.GetOrdinal("BirthDate"));
                    }
                    model.DepartId = !dr.IsDBNull(dr.GetOrdinal("DepartId")) ? dr.GetInt32(dr.GetOrdinal("DepartId")) : 0;
                    model.DepartName = !dr.IsDBNull(dr.GetOrdinal("DepartName")) ? dr.GetString(dr.GetOrdinal("DepartName")) : "";
                    model.LengthService = dr.IsDBNull(dr.GetOrdinal("LengthService")) ? 0 : dr.GetInt32(dr.GetOrdinal("LengthService"));
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : "";
                    model.Mobile = !dr.IsDBNull(dr.GetOrdinal("Mobile")) ? dr.GetString(dr.GetOrdinal("Mobile")) : "";
                    model.qq = !dr.IsDBNull(dr.GetOrdinal("qq")) ? dr.GetString(dr.GetOrdinal("qq")) : "";
                    model.Education = !dr.IsDBNull(dr.GetOrdinal("Education")) ? dr.GetString(dr.GetOrdinal("Education")) : "";
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("MaturityTime")))
                    {
                        model.MaturityTime = dr.GetDateTime(dr.GetOrdinal("MaturityTime"));
                    }
                    model.IsSignContract = !dr.IsDBNull(dr.GetOrdinal("MaturityTime"));
                    //model.GovFileDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString());
                    model.GovFilePositionList = this.GetPositionList(dr["PositionXML"].ToString());
                    model.UserId = dr["Account"].ToString();
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /*/// <summary>
        /// 删除档案信息
        /// </summary>
        /// <param name="ID">档案ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns>0：成功     1：已经分配用户，不能删除     -1：失败</returns>
        public int DeleteArchives(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovArchives_Delete");
            this._db.AddInParameter(dc, "Ids", DbType.AnsiString, sId.ToString());
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }*/

        /// <summary>
        /// 删除档案信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="dangAnId">档案编号</param>
        /// <returns></returns>
        public int DeleteArchives(string companyId, string dangAnId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_GovArchives_Delete");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, dangAnId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;

            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0) return sqlExceptionCode;

            return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
        }

        /// <summary>
        /// 获取内部通讯录信息
        /// </summary>
        /// <param name="UserName">姓名</param>
        /// <param name="DepartIds">部门编号集合（如：1,2,3）</param>
        /// <param name="Department">部门</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFile> GetSearchArchivesList(string UserName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFile> ResultList = null;
            string tableName = "view_GovFileContacts";
            string identityColumnName = "ID";
            string fields = " ID,CompanyId,FileNumber,[Name],DepartId,Contact,Mobile,qq,DepartName,IssueTime,PositionXML  ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (!string.IsNullOrEmpty(UserName))
            {
                query = query + string.Format(" AND [Name] LIKE '%{0}%'", UserName);
            }
            if (!string.IsNullOrEmpty(DepartIds))
            {
                //query = query + string.Format(" AND CAST(DepartmentXML AS XML).exist('/ROOT/row/@DepartId[.=\"{0}\"]') = 1", DepartmentId.ToString());
                query = query + string.Format(" AND DepartId in ({0})", DepartIds);
            }
            if (!string.IsNullOrEmpty(Department) && string.IsNullOrEmpty(DepartIds))
            {
                query = query + string.Format(" AND DepartName LIKE '%{0}%'", Department);
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovFile>();
                EyouSoft.Model.GovStructure.MGovFile model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFile();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.FileNumber = !dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? dr.GetString(dr.GetOrdinal("FileNumber")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : "";
                    model.Mobile = !dr.IsDBNull(dr.GetOrdinal("Mobile")) ? dr.GetString(dr.GetOrdinal("Mobile")) : "";
                    model.qq = !dr.IsDBNull(dr.GetOrdinal("qq")) ? dr.GetString(dr.GetOrdinal("qq")) : "";
                    //model.GovFileDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString());
                    model.DepartName = !dr.IsDBNull(dr.GetOrdinal("DepartName")) ? dr.GetString(dr.GetOrdinal("DepartName")) : "";
                    model.GovFilePositionList = this.GetPositionList(dr["PositionXML"].ToString());
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成附件集合List
        /// </summary>
        /// <param name="AttendanceXml">附件信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetAttachList(string ComAttachXML, string NoticeId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            if (string.IsNullOrEmpty(ComAttachXML)) return null;
            IList<EyouSoft.Model.ComStructure.MComAttach> ResultList = null;
            ResultList = new List<EyouSoft.Model.ComStructure.MComAttach>();
            XElement root = XElement.Parse(ComAttachXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.ComStructure.MComAttach model = new EyouSoft.Model.ComStructure.MComAttach()
                {
                    Name = tmp1.Attribute("Name").Value,
                    FilePath = tmp1.Attribute("FilePath").Value,
                    Size = int.Parse(tmp1.Attribute("Size").Value),
                    Downloads = int.Parse(tmp1.Attribute("Downloads").Value),
                    ItemId = NoticeId,
                    ItemType = ItemType
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成部门集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFileDept> GetDepartmentList(string DepartMentXml)
        {
            if (string.IsNullOrEmpty(DepartMentXml)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFileDept> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFileDept>();
            XElement root = XElement.Parse(DepartMentXml);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFileDept model = new EyouSoft.Model.GovStructure.MGovFileDept()
                {
                    DepartId = int.Parse(tmp1.Attribute("DepartId").Value),
                    DepartName = tmp1.Attribute("DepartName").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成职务集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFilePosition> GetPositionList(string PositionXML)
        {
            if (string.IsNullOrEmpty(PositionXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFilePosition> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFilePosition>();
            XElement root = XElement.Parse(PositionXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFilePosition model = new EyouSoft.Model.GovStructure.MGovFilePosition()
                {
                    PositionId = int.Parse(tmp1.Attribute("PositionId").Value),
                    Title = tmp1.Attribute("Title").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成学历集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFileEducation> GetEducationList(string EducationXML)
        {
            if (string.IsNullOrEmpty(EducationXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFileEducation> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFileEducation>();
            XElement root = XElement.Parse(EducationXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFileEducation model = new EyouSoft.Model.GovStructure.MGovFileEducation();
                model.FileId = tmp1.Attribute("FileId").Value;
                if (tmp1.Attribute("StartTime") != null && tmp1.Attribute("StartTime").Value.Trim() != "")
                {
                    model.StartTime = Convert.ToDateTime(tmp1.Attribute("StartTime").Value);
                }
                if (tmp1.Attribute("EndTime") != null && tmp1.Attribute("EndTime").Value.Trim() != "")
                {
                    model.EndTime = Convert.ToDateTime(tmp1.Attribute("EndTime").Value);
                }
                model.Education = tmp1.Attribute("Education").Value;
                model.Profession = tmp1.Attribute("Profession").Value;
                model.Graduated = tmp1.Attribute("Graduated").Value;
                model.Statue = (EyouSoft.Model.EnumType.GovStructure.Statue)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Statue), tmp1.Attribute("Statue").Value);
                model.Remarks = tmp1.Attribute("Remarks").Value;
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成履历集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFileCurriculum> GetCurriculumList(string CurriculumXML)
        {
            if (string.IsNullOrEmpty(CurriculumXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFileCurriculum> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFileCurriculum>();
            XElement root = XElement.Parse(CurriculumXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFileCurriculum model = new EyouSoft.Model.GovStructure.MGovFileCurriculum();
                model.FileId = tmp1.Attribute("FileId").Value;
                if (tmp1.Attribute("StartTime") != null && tmp1.Attribute("StartTime").Value.Trim() != "")
                {
                    model.StartTime = Convert.ToDateTime(tmp1.Attribute("StartTime").Value);
                }
                if (tmp1.Attribute("EndTime") != null && tmp1.Attribute("EndTime").Value.Trim() != "")
                {
                    model.EndTime = Convert.ToDateTime(tmp1.Attribute("EndTime").Value);
                }
                model.Location = tmp1.Attribute("Location").Value;
                model.WorkUnit = tmp1.Attribute("WorkUnit").Value;
                model.Occupation = tmp1.Attribute("Occupation").Value;
                model.Remarks = tmp1.Attribute("Remarks").Value;
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成家庭关系集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFilehome> GetHomeList(string HomeXML)
        {
            if (string.IsNullOrEmpty(HomeXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFilehome> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFilehome>();
            XElement root = XElement.Parse(HomeXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFilehome model = new EyouSoft.Model.GovStructure.MGovFilehome();
                model.FileId = tmp1.Attribute("FileId").Value;
                model.Relationship = tmp1.Attribute("Relationship").Value;
                model.Name = tmp1.Attribute("Name").Value;
                model.Phone = tmp1.Attribute("Phone").Value;
                model.WorkUnit = tmp1.Attribute("WorkUnit").Value;
                model.Address = tmp1.Attribute("Address").Value;
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成劳动合同集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovFileContract> GetContractList(string ContractXML)
        {
            if (string.IsNullOrEmpty(ContractXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovFileContract> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovFileContract>();
            XElement root = XElement.Parse(ContractXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovFileContract model = new EyouSoft.Model.GovStructure.MGovFileContract();
                model.FileId = tmp1.Attribute("FileId").Value;
                model.ContractNumber = tmp1.Attribute("ContractNumber").Value;
                model.Name = tmp1.Attribute("Name").Value;
                if (tmp1.Attribute("SignedTime") != null && tmp1.Attribute("SignedTime").Value.Trim()!="")
                {
                    model.SignedTime = Convert.ToDateTime(tmp1.Attribute("SignedTime").Value);
                }
                if (tmp1.Attribute("MaturityTime") != null && tmp1.Attribute("MaturityTime").Value.Trim() != "")
                {
                    model.MaturityTime = Convert.ToDateTime(tmp1.Attribute("MaturityTime").Value);
                }
                if (tmp1.Attribute("Status") != null && tmp1.Attribute("MaturityTime") != null && tmp1.Attribute("MaturityTime").Value.Trim() != "")
                {
                    if (tmp1.Attribute("Status").Value.Trim() == "3")
                        model.Status = EyouSoft.Model.EnumType.GovStructure.FileContractStatus.到期已处理;
                    else if (Convert.ToDateTime(tmp1.Attribute("MaturityTime").Value) > DateTime.Now)
                        model.Status = EyouSoft.Model.EnumType.GovStructure.FileContractStatus.未到期;
                    else
                        model.Status = EyouSoft.Model.EnumType.GovStructure.FileContractStatus.到期未处理;
                }
                model.Remarks = tmp1.Attribute("Remarks").Value;
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 创建附件XML
        /// </summary>
        /// <param name="Lists">附件集合</param>
        /// <returns></returns>
        private string CreateComAttachListXML(IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ComStructure.MComAttach model in list)
            {
                StrBuild.AppendFormat("<ComAttach ItemType=\"{0}\"", (int)model.ItemType);
                StrBuild.AppendFormat(" ItemId=\"{0}\" ", model.ItemId);
                StrBuild.AppendFormat(" Name=\"{0}\" ", model.Name);
                StrBuild.AppendFormat(" FilePath=\"{0}\" ", model.FilePath);
                StrBuild.AppendFormat(" Size=\"{0}\" ", (int)model.Size);
                StrBuild.AppendFormat(" Downloads=\"{0}\" />", model.Downloads);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建部门XML
        /// </summary>
        /// <param name="Lists">部门集合</param>
        /// <returns></returns>
        private string CreateGovFileDeptListXML(IList<EyouSoft.Model.GovStructure.MGovFileDept> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFileDept model in list)
            {
                StrBuild.AppendFormat("<GovFileDept FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" DepartId=\"{0}\" />", model.DepartId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建职务XML
        /// </summary>
        /// <param name="Lists">职务集合</param>
        /// <returns></returns>
        private string CreateGovFilePositionListXML(IList<EyouSoft.Model.GovStructure.MGovFilePosition> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFilePosition model in list)
            {
                StrBuild.AppendFormat("<GovFilePosition FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" PositionId=\"{0}\" />", model.PositionId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建学历XML
        /// </summary>
        /// <param name="Lists">学历集合</param>
        /// <returns></returns>
        private string CreateGovFileEducationListXML(IList<EyouSoft.Model.GovStructure.MGovFileEducation> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFileEducation model in list)
            {
                StrBuild.AppendFormat("<GovFileEducation FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" StartTime=\"{0}\" ", model.StartTime);
                StrBuild.AppendFormat(" EndTime=\"{0}\" ", model.EndTime);
                StrBuild.AppendFormat(" Education=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Education));
                StrBuild.AppendFormat(" Profession=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Profession));
                StrBuild.AppendFormat(" Graduated=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Graduated));
                StrBuild.AppendFormat(" Statue=\"{0}\" ", (int)model.Statue);
                StrBuild.AppendFormat(" Remarks=\"{0}\" />", Utils.ReplaceXmlSpecialCharacter(model.Remarks));
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建履历XML
        /// </summary>
        /// <param name="Lists">履历集合</param>
        /// <returns></returns>
        private string CreateGovFileCurriculumListXML(IList<EyouSoft.Model.GovStructure.MGovFileCurriculum> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFileCurriculum model in list)
            {
                StrBuild.AppendFormat("<GovFileCurriculum FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" StartTime=\"{0}\" ", model.StartTime);
                StrBuild.AppendFormat(" EndTime=\"{0}\" ", model.EndTime);
                StrBuild.AppendFormat(" Location=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Location));
                StrBuild.AppendFormat(" WorkUnit=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.WorkUnit));
                StrBuild.AppendFormat(" Occupation=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Occupation));
                StrBuild.AppendFormat(" Remarks=\"{0}\" />", Utils.ReplaceXmlSpecialCharacter(model.Remarks));
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建家庭关系XML
        /// </summary>
        /// <param name="Lists">家庭关系集合</param>
        /// <returns></returns>
        private string CreateGovFilehomeListXML(IList<EyouSoft.Model.GovStructure.MGovFilehome> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFilehome model in list)
            {
                StrBuild.AppendFormat("<GovFilehome FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" Relationship=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Relationship));
                StrBuild.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Name));
                StrBuild.AppendFormat(" Phone=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Phone));
                StrBuild.AppendFormat(" WorkUnit=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.WorkUnit));
                StrBuild.AppendFormat(" Address=\"{0}\" />", Utils.ReplaceXmlSpecialCharacter(model.Address));
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建劳动合同XML
        /// </summary>
        /// <param name="Lists">劳动合同集合</param>
        /// <returns></returns>
        private string CreateGovFileContractListXML(IList<EyouSoft.Model.GovStructure.MGovFileContract> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovFileContract model in list)
            {
                StrBuild.AppendFormat("<GovFileContract FileId=\"{0}\"", model.FileId);
                StrBuild.AppendFormat(" ContractNumber=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.ContractNumber));
                StrBuild.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Name));
                StrBuild.AppendFormat(" SignedTime=\"{0}\" ", model.SignedTime);
                StrBuild.AppendFormat(" MaturityTime=\"{0}\" ", model.MaturityTime);
                StrBuild.AppendFormat(" Status=\"{0}\" ", (int)model.Status);
                StrBuild.AppendFormat(" Remarks=\"{0}\" />", Utils.ReplaceXmlSpecialCharacter(model.Remarks));
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        #endregion
    }
}
