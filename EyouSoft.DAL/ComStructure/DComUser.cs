using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Toolkit;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;
using System.Xml.Linq;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 用户数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/19
    /// </summary>
    public class DComUser : DALBase, EyouSoft.IDAL.ComStructure.IComUser
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetYongHuShuLiang = "SELECT COUNT(*) FROM [tbl_ComUser] WHERE CompanyId=@CompanyId AND [IsDelete]='0' ";
        #endregion

        private readonly Database _db = null;
        #region 构造函数
        public DComUser()
        {
            this._db = base.SystemStore;
        }
        #endregion
        #region IComUser 成员
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComUser item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComUser_Add");
            this._db.AddInParameter(comm, "@UserId", DbType.AnsiStringFixedLength, item.UserId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@TourCompanyId", DbType.AnsiStringFixedLength, item.TourCompanyId);
            this._db.AddInParameter(comm, "@UserName", DbType.AnsiString, item.UserName);
            this._db.AddInParameter(comm, "@Password", DbType.AnsiString, item.Password);
            this._db.AddInParameter(comm, "@md5", DbType.AnsiString, item.MD5Password);
            this._db.AddInParameter(comm, "@userType", DbType.Byte, (int)item.UserType);
            this._db.AddInParameter(comm, "@ContactName", DbType.String, item.ContactName);
            this._db.AddInParameter(comm, "@ContactSex", DbType.Byte, item.ContactSex);
            this._db.AddInParameter(comm, "@ContactTel", DbType.String, item.ContactTel);
            this._db.AddInParameter(comm, "@ContactFax", DbType.String, item.ContactFax);
            this._db.AddInParameter(comm, "@ContactMobile", DbType.String, item.ContactMobile);
            this._db.AddInParameter(comm, "@ContactEmail", DbType.String, item.ContactEmail);
            this._db.AddInParameter(comm, "@QQ", DbType.String, item.QQ);
            this._db.AddInParameter(comm, "@msn", DbType.String, item.MSN);
            this._db.AddInParameter(comm, "@JobName", DbType.String, item.JobName);
            this._db.AddInParameter(comm, "@LastLoginIP", DbType.String, item.LastLoginIP);
            this._db.AddInParameter(comm, "@LastLoginTime", DbType.DateTime, item.LastLoginTime);
            this._db.AddInParameter(comm, "@RoleId", DbType.Int32, item.RoleId);
            this._db.AddInParameter(comm, "@DeptId", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@DeptName", DbType.String, item.DeptName);
            this._db.AddInParameter(comm, "@PeopProfile", DbType.String, item.PeopProfile);
            this._db.AddInParameter(comm, "@Remark", DbType.String, item.Remark);

            this._db.AddInParameter(comm, "@DeptIdJG", DbType.Int32, item.DeptIdJG);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Operator", DbType.String, item.Operator);
            this._db.AddInParameter(comm, "@OperDeptId", DbType.Int32, item.OperDeptId);

            this._db.AddInParameter(comm, "@ContactShortTel", DbType.String, item.ContactShortTel);
            this._db.AddInParameter(comm, "@ContactShortMobile", DbType.String, item.ContactShortMobile);


            this._db.AddInParameter(comm, "@UserImg", DbType.String, item.UserImg);
            this._db.AddInParameter(comm, "@IsGuide", DbType.Byte, item.IsGuide);
            this._db.AddInParameter(comm, "@IDcardId", DbType.String, item.IDcardId);
            this._db.AddInParameter(comm, "@BirthDate", DbType.DateTime, item.BirthDate);
            this._db.AddInParameter(comm, "@ComAttachXML", DbType.Xml, CreateComNoticeXML(item.ComAttachList));
            this._db.AddOutParameter(comm, "@result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(comm, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(comm, "result")) == 1 ? true : false;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="item">用户信息实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComUser item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComUser_Update");
            this._db.AddInParameter(comm, "@UserId", DbType.AnsiStringFixedLength, item.UserId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Password", DbType.AnsiString, item.Password);
            this._db.AddInParameter(comm, "@md5", DbType.AnsiString, item.MD5Password);
            this._db.AddInParameter(comm, "@userType", DbType.Byte, (int)AttachItemType.人事档案);
            this._db.AddInParameter(comm, "@ContactName", DbType.String, item.ContactName);
            this._db.AddInParameter(comm, "@ContactSex", DbType.Byte, item.ContactSex);
            this._db.AddInParameter(comm, "@ContactTel", DbType.String, item.ContactTel);
            this._db.AddInParameter(comm, "@ContactFax", DbType.String, item.ContactFax);
            this._db.AddInParameter(comm, "@ContactMobile", DbType.String, item.ContactMobile);
            this._db.AddInParameter(comm, "@ContactEmail", DbType.String, item.ContactEmail);
            this._db.AddInParameter(comm, "@QQ", DbType.String, item.QQ);
            this._db.AddInParameter(comm, "@msn", DbType.String, item.MSN);
            this._db.AddInParameter(comm, "@JobName", DbType.String, item.JobName);
            this._db.AddInParameter(comm, "@RoleId", DbType.Int32, item.RoleId);
            this._db.AddInParameter(comm, "@DeptId", DbType.Int32, item.DeptId);
            this._db.AddInParameter(comm, "@DeptName", DbType.String, item.DeptName);
            this._db.AddInParameter(comm, "@PeopProfile", DbType.String, item.PeopProfile);
            this._db.AddInParameter(comm, "@Remark", DbType.String, item.Remark);

            this._db.AddInParameter(comm, "@DeptIdJG", DbType.Int32, item.DeptIdJG);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Operator", DbType.String, item.Operator);
            this._db.AddInParameter(comm, "@OperDeptId", DbType.String, item.OperDeptId);

            this._db.AddInParameter(comm, "@ContactShortTel", DbType.String, item.ContactShortTel);
            this._db.AddInParameter(comm, "@ContactShortMobile", DbType.String, item.ContactShortMobile);


            this._db.AddInParameter(comm, "@UserImg", DbType.String, item.UserImg);
            this._db.AddInParameter(comm, "@IsGuide", DbType.Byte, item.IsGuide);
            this._db.AddInParameter(comm, "@IDcardId", DbType.String, item.IDcardId);
            this._db.AddInParameter(comm, "@BirthDate", DbType.DateTime, item.BirthDate);
            this._db.AddInParameter(comm, "@ComAttachXML", DbType.Xml, CreateComNoticeXML(item.ComAttachList));




            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /*/// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户编号(以逗号分隔)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool Delete(string ids, string companyId)
        {
            string sql = "UPDATE tbl_ComUser SET IsDelete = '1' WHERE CHARINDEX(UserId,@ids,0) > 0 AND IsAdmin = '0' AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "ids", DbType.String, ids);
            this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0;
        }*/

        /// <summary>
        /// 删除用户，返回1成功，其它失败
        /// </summary>
        /// <param name="ItemType">附件类型</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public int Delete(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, string userId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_UserID_Delete");
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            _db.AddInParameter(cmd, "ItemType", DbType.Byte, (int)ItemType);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

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

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="ids">用户编号(以逗号分隔)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="UserStatus">用户状态</param>
        /// <returns></returns>
        public bool SetUserStatus(string ids, string CompanyId, EyouSoft.Model.EnumType.ComStructure.UserStatus UserStatus)
        {
            string sql = "UPDATE tbl_ComUser SET UserStatus = @UserStatus WHERE CHARINDEX(UserId,@ids,0) > 0 AND IsAdmin = '0' AND CompanyId=@CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "ids", DbType.String, ids);
            this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            _db.AddInParameter(comm, "UserStatus", DbType.Byte, (int)UserStatus);
            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0;
        }

        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="privs">权限集合</param>
        /// <returns>true：成功 false：失败</returns>
        public bool UpdatePrivs(string userId, string companyId, int? roleId, string privs)
        {
            StringBuilder sql = new StringBuilder();
            if (roleId == null)
            {
                sql.Append("UPDATE tbl_ComUser SET Privs = @Privs");
            }
            else
            {
                sql.Append("UPDATE tbl_ComUser SET RoleId = @RoleId");
                if (!string.IsNullOrEmpty(privs))
                {
                    sql.Append(",Privs=@Privs");
                }
            }
            sql.Append(" WHERE UserId = @UserId AND CompanyId = @CompanyId");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@Privs", DbType.AnsiString, privs);
            this._db.AddInParameter(comm, "@UserId", DbType.AnsiStringFixedLength, userId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(comm, "@RoleId", DbType.Int32, roleId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns>true：存在 false：不存在</returns>
        public bool IsExistsUserName(string userName, string companyId, string userId)
        {
            string s = "SELECT Count(1) FROM tbl_ComUser WHERE UserName = @UserName AND CompanyId = @CompanyId AND IsDelete=0";
            DbCommand cmd = this._db.GetSqlStringCommand("SELECT 1");
            if (!string.IsNullOrEmpty(userId))
            {
                s += " AND UserId<>@UserId ";
                _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            }
            this._db.AddInParameter(cmd, "@UserName", DbType.AnsiString, userName);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            cmd.CommandText = s;

            return DbHelper.Exists(cmd, this._db);
        }

        /// <summary>
        /// 获取用户实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="Item">附件类型</param>
        /// <returns>用户实体</returns>
        public MComUser GetModel(string userId, string companyId, EyouSoft.Model.EnumType.ComStructure.AttachItemType Item)
        {
            StringBuilder sql = new StringBuilder("SELECT UserId,CompanyId,Password,MD5Password,TourCompanyId,UserName,UserType,ContactName,ContactSex,ContactTel,ContactFax,ContactMobile,UserImg,IsGuide,IDcardId,BirthDate,ContactShortTel,ContactShortMobile,");
            sql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemId ='{1}'  and  ItemType={0}   FOR XML RAW,ROOT('ROOT'))AS ComAttachXML", (int)Item, userId);
            //ComAttachXML

            sql.Append(",ContactEmail,QQ,MSN,JobName,RoleId, DeptId,(select DepartName from tbl_ComDepartment where DepartId=tbl_ComUser.DeptId) as DeptName,(select DepartName from tbl_ComDepartment where DepartId=tbl_ComUser.DeptIdJG) as JGDeptName ,PeopProfile,Remark,DeptIdJG,UserStatus FROM tbl_ComUser ");
            sql.Append("WHERE UserId=  @UserId AND CompanyId = @CompanyId");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@UserId", DbType.AnsiStringFixedLength, userId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return new MComUser()
                    {
                        UserId = userId,
                        CompanyId = reader["CompanyId"].ToString(),
                        TourCompanyId = reader["TourCompanyId"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        MD5Password = reader["MD5Password"].ToString(),
                        UserType = (UserType)Enum.Parse(typeof(UserType), reader["UserType"].ToString()),
                        ContactName = reader["ContactName"].ToString(),
                        ContactSex = (EyouSoft.Model.EnumType.GovStructure.Gender)reader.GetByte(reader.GetOrdinal("ContactSex")),
                        ContactTel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                        ContactFax = reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? string.Empty : reader["ContactFax"].ToString(),
                        ContactMobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                        ContactEmail = reader.IsDBNull(reader.GetOrdinal("ContactEmail")) ? string.Empty : reader["ContactEmail"].ToString(),
                        QQ = reader.IsDBNull(reader.GetOrdinal("QQ")) ? string.Empty : reader["QQ"].ToString(),
                        MSN = reader.IsDBNull(reader.GetOrdinal("MSN")) ? string.Empty : reader["MSN"].ToString(),
                        JobName = reader.IsDBNull(reader.GetOrdinal("JobName")) ? string.Empty : reader["JobName"].ToString(),
                        RoleId = (int)reader["RoleId"],
                        DeptId = (int)reader["DeptId"],
                        DeptName = reader.IsDBNull(reader.GetOrdinal("DeptName")) ? string.Empty : reader["DeptName"].ToString(),
                        PeopProfile = reader.IsDBNull(reader.GetOrdinal("PeopProfile")) ? string.Empty : reader["PeopProfile"].ToString(),
                        Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? string.Empty : reader["Remark"].ToString(),
                        DeptIdJG = (int)reader["DeptIdJG"],
                        UserStatus = (EyouSoft.Model.EnumType.ComStructure.UserStatus)reader.GetByte(reader.GetOrdinal("UserStatus")),
                        JGDeptName = reader["JGDeptName"].ToString(),
                        ContactShortTel = reader["ContactShortTel"].ToString(),
                        ContactShortMobile = reader["ContactShortMobile"].ToString(),
                        UserImg = reader["UserImg"].ToString(),
                        IsGuide = reader["IsGuide"].ToString() == "1" ? true : false,
                        IDcardId = reader["IDcardId"].ToString(),
                        BirthDate = Utils.GetDateTimeNullable(reader.IsDBNull(reader.GetOrdinal("BirthDate")) ? string.Empty : reader["BirthDate"].ToString()),
                        ComAttachList = this.GetAttachList(reader["ComAttachXML"].ToString(), userId, Item)

                    };
                }
            }

            return null;
        }
        /// <summary>
        /// 获取用户权限(待修改,当用户权限为空时，获取角色权限)
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>用户权限</returns>
        public string GetPrivs(string userId, string companyId)
        {
            string sql = "SELECT Privs FROM tbl_ComUser WHERE UserId = @userid AND CompanyId = @companyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@userid", DbType.AnsiStringFixedLength, userId);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(reader.GetOrdinal("Privs")) ? string.Empty : reader["Privs"].ToString();
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>用户信息集合</returns>
        public IList<MComUser> GetList(int pageCurrent, int pageSize, ref int pageCount,
            string companyId, MComUserSearch search)
        {
            string tableName = "tbl_ComUser";
            string primaryKey = "UserId";
            string orderBy = "IssueTime DESC";
            string fileds = "UserId,CompanyId,RoleId,DeptId,IsAdmin,(select DepartName from tbl_ComDepartment where DepartId=tbl_ComUser.DeptId) DeptName,(SELECT RoleName  FROM  tbl_ComRole WHERE Id=tbl_ComUser.RoleId) RoleName,UserName,UserType,ContactName,ContactTel,ContactMobile,ContactFax,QQ,UserStatus,OnlineStatus,UserImg,IsGuide,IDcardId,BirthDate,ContactShortTel,ContactShortMobile,ContactSex";

            IList<MComUser> list = new List<MComUser>();
            MComUser item = null;
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId = '{0}' and IsDelete='0' and  UserType in({1},{2}) ", companyId, (int)UserType.内部员工, (int)UserType.导游);
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.UserName))
                {
                    query.AppendFormat(" AND UserName like '%{0}%'", Utils.ToSqlLike(search.UserName));
                }
                if (!string.IsNullOrEmpty(search.ContactName))
                {
                    query.AppendFormat(" AND ContactName like '%{0}%'", Utils.ToSqlLike(search.ContactName));
                }
                if (search.DeptId != 0)
                {
                    query.AppendFormat(" AND DeptId = {0}", search.DeptId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(search.DeptName))
                    {
                        query.AppendFormat(" AND exists(select 1 from tbl_ComDepartment where DepartId=tbl_ComUser.DeptId and DepartName like '%{0}%')", Utils.ToSqlLike(search.DeptName));
                    }
                }
                if (search.UserStatus.HasValue)
                {
                    query.AppendFormat(" AND UserStatus = {0}", (int)search.UserStatus);
                }
            }

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComUser()
                    {
                        UserId = reader["UserId"].ToString(),
                        CompanyId = reader["CompanyId"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        RoleId = (int)reader["RoleId"],
                        UserType = (UserType)Enum.Parse(typeof(UserType), reader["UserType"].ToString()),
                        ContactName = reader["ContactName"].ToString(),
                        ContactTel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString(),
                        ContactFax = reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? string.Empty : reader["ContactFax"].ToString(),
                        ContactMobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString(),
                        QQ = reader.IsDBNull(reader.GetOrdinal("QQ")) ? string.Empty : reader["QQ"].ToString(),
                        DeptName = reader.IsDBNull(reader.GetOrdinal("DeptName")) ? string.Empty : reader["DeptName"].ToString(),
                        RoleName = reader.IsDBNull(reader.GetOrdinal("RoleName")) ? string.Empty : reader["RoleName"].ToString(),
                        UserStatus = (EyouSoft.Model.EnumType.ComStructure.UserStatus)reader.GetByte(reader.GetOrdinal("UserStatus")),
                        DeptId = reader.IsDBNull(reader.GetOrdinal("DeptId")) ? 0 : reader.GetInt32(reader.GetOrdinal("DeptId")),
                        IsAdmin = reader["IsAdmin"].ToString() == "1" ? true : false,
                        OnlineStatus = (UserOnlineStatus)reader.GetByte(reader.GetOrdinal("OnlineStatus")),
                        ContactShortTel = reader["ContactShortTel"].ToString(),
                        ContactShortMobile = reader["ContactShortMobile"].ToString(),
                        UserImg = reader["UserImg"].ToString(),
                        IsGuide = reader["IsGuide"].ToString() == "1" ? true : false,
                        IDcardId = reader["IDcardId"].ToString(),
                        BirthDate = Utils.GetDateTimeNullable(reader.GetOrdinal("BirthDate").ToString()),
                        ContactSex = (EyouSoft.Model.EnumType.GovStructure.Gender)reader.GetByte(reader.GetOrdinal("ContactSex"))
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="userNm">用户名</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <param name="MD5Pwd">MD5密码</param>
        /// <returns></returns>
        public bool PwdModify(string UserId,string userNm, string OldPwd, string NewPwd, string MD5Pwd)
        {
            string sql = "update tbl_ComUser set Password=@NewPwd,MD5Password=@MD5Pwd,UserName=@UserName where UserId=@UserId and Password=@OldPwd";
            DbCommand dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "NewPwd", DbType.String, NewPwd);
            this._db.AddInParameter(dc, "OldPwd", DbType.String, OldPwd);
            this._db.AddInParameter(dc, "MD5Pwd", DbType.String, MD5Pwd);
            this._db.AddInParameter(dc, "UserId", DbType.String, UserId);
            this._db.AddInParameter(dc, "UserName", DbType.String, userNm);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取系统用户数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetYongHuShuLiang(string companyId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetYongHuShuLiang);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }

        /// <summary>
        /// 创建附件XML
        /// </summary>
        /// <param name="list">附件集合</param>
        /// <returns></returns>
        private string CreateComNoticeXML(IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ComStructure.MComAttach model in list)
            {
                StrBuild.AppendFormat("<ComAttach ItemType=\"{0}\"", (int)model.ItemType);
                StrBuild.AppendFormat(" ItemId=\"{0}\" ", model.ItemId);
                StrBuild.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Name));
                StrBuild.AppendFormat(" FilePath=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.FilePath));
                StrBuild.AppendFormat(" Size=\"{0}\" ", (int)model.Size);
                StrBuild.AppendFormat(" Downloads=\"{0}\" />", model.Downloads);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }


        /// <summary>
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="userId">员工编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetAttachList(string ComAttachXML, string userId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
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
                    ItemId = userId,
                    ItemType = ItemType
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }
        #endregion
    }
}
