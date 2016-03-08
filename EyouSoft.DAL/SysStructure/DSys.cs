//汪奇志 2011-09-20
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using EyouSoft.IDAL.SysStructure;
using System.Xml.Linq;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 系统管理数据访问类
    /// </summary>
    public class DSys : DALBase, ISys
    {
        #region static constants
        //static constants
        /// <summary>
        /// 删除系统菜单一级栏目Sql
        /// </summary>
        const string SQL_DELETE_DeleteSysMenu1 = " delete from [tbl_SysMenu] where Id = @Id ; delete from [tbl_SysMenu] where ParentId = @Id; ";        
        /// <summary>
        /// 基础权限查询Sql
        /// </summary>
        const string SQL_SELECT_GetPrivs = " SELECT [Id],[ParentId],[Name],[SortId],[IsEnable] FROM [tbl_SysPrivs3] WHERE [ParentId]=@DefaultMenu2Id AND [IsEnable] = '1' ORDER BY [SortId]";
        /// <summary>
        /// 系统域名查询Sql
        /// </summary>
        const string SQL_SELECT_GetDomains = " select SysId,[Domain],Url,(select Id from tbl_Company where SysId = @SysId) as CompanyId from [tbl_SysDomain] where SysId = @SysId ";
        const string SQL_SELECT_GetSysRoleId = "SELECT [Id] FROM [tbl_ComRole] WHERE [CompanyId]=@CompanyId AND [IsSystem]='1' AND [IsDelete]='0'";
        const string SQL_INSERT_InsertFirstMenu = "INSERT INTO [tbl_SysPrivs1]([Name],[SortId],[IsEnable],[ClassName]) VALUES (@Name,@SortId,@IsEnable,@ClassName);SELECT SCOPE_IDENTITY();";
        const string SQL_INSERT_InsertSecondMenu = "INSERT INTO [tbl_SysPrivs2]([ParentId],[Name],[Url],[SortId],[IsEnable]) VALUES(@ParentId,@Name,@Url,@SortId,@IsEnable);SELECT SCOPE_IDENTITY();";
        const string SQL_INSERT_InsertPrivs = "INSERT INTO [tbl_SysPrivs3]([ParentId],[Name],[SortId],[IsEnable],[PrivsType]) VALUES(@ParentId,@Name,@SortId,@IsEnable,@PrivsType);SELECT SCOPE_IDENTITY();";
        const string SQL_SELECT_IsExistsPrivsType = "SELECT COUNT(*) FROM [tbl_SysPrivs3] WHERE [ParentId]=@ParentId AND [PrivsType]=@PrivsType";
        const string SQL_SELECT_IsExistsPrivsName = "SELECT COUNT(*) FROM [tbl_SysPrivs3] WHERE [ParentId]=@ParentId AND [Name]=@Name";
        const string SQL_SELECT_IsExistsMenu2Name = "SELECT COUNT(*) FROM [tbl_SysPrivs2] WHERE [ParentId]=@ParentId AND [Name]=@Name";
        const string SQL_SELECT_IsExistsMenu1Name = "SELECT COUNT(*) FROM [tbl_SysPrivs1] WHERE [Name]=@Name";
        const string SQL_UPDATE_UpdateMenu2Url = "UPDATE [tbl_SysPrivs2] SET [Url]=@Url WHERE [Id]=@Menu2Id;UPDATE [tbl_SysMenu] SET [Url]=@Url WHERE [Privs2Id]=@Menu2Id;";
        const string SQL_UPDATE_SetUserRole = "UPDATE [tbl_ComUser] SET [RoleId]=@RoleId WHERE [UserId]=@UserId";
        const string SQL_SELECT_IsExistsMenu2 = "SELECT COUNT(*) FROM [tbl_SysMenu] WHERE [SysId]=@SysId AND [Privs2Id]=@Privs2Id";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DSys()
        {
            _db = SystemStore;
        }
        #endregion

        #region private member

        /// <summary>
        /// 生个二级栏目的SqlXml
        /// </summary>
        /// <param name="menu2S">二级栏目实体集合</param>
        /// <returns></returns>
        private string GetMenu2SqlXml(IList<Model.SysStructure.MComMenu2Info> menu2S)
        {
            if (menu2S == null || menu2S.Count < 1)
                return string.Empty;

            var strXml = new StringBuilder("<ROOT>");
            foreach (var t in menu2S)
            {
                if (t.DefaultMenu2Id <= 0)
                    continue;

                strXml.AppendFormat(
                    "<Info Privs2Id = \"{0}\" Name = \"{1}\" Url = \"{2}\" MenuId = \"{3}\" Privs = \"{4}\" />",
                    t.DefaultMenu2Id,
                    Utils.ReplaceXmlSpecialCharacter(t.Name),
                    Utils.ReplaceXmlSpecialCharacter(t.Url),
                    t.MenuId,
                    Utils.ReplaceXmlSpecialCharacter(GetPrivsIds(t.Privs)));
            }
            strXml.Append("</ROOT>");

            return strXml.ToString();
        }

        /// <summary>
        /// 构造权限Id的半角逗号分割形式的字符串
        /// </summary>
        /// <param name="privs">权限集合</param>
        /// <returns></returns>
        private string GetPrivsIds(IList<Model.SysStructure.MSysPrivsInfo> privs)
        {
            if (privs == null || privs.Count < 1)
                return string.Empty;

            var str = new StringBuilder();
            foreach (var t in privs)
            {
                if (t == null || t.PrivsId <= 0)
                    continue;

                str.AppendFormat("{0},", t.PrivsId);
            }

            return str.ToString().TrimEnd(',');

            //return GetIdsByArr((from c in Privs where c != null && c.PrivsId > 0 select c.PrivsId).ToArray());
        }

        /// <summary>
        /// 构造二级栏目的权限值的SqlXml
        /// </summary>
        /// <param name="privs">二级栏目实体集合</param>
        /// <returns></returns>
        private string GetMenu2PrivsSqlXml(IList<Model.SysStructure.MComMenu2Info> privs)
        {
            if (privs == null || privs.Count < 1)
                return string.Empty;

            var strXml = new StringBuilder("<ROOT>");
            foreach (var t in privs)
            {
                if (t.MenuId <= 0)
                    continue;

                strXml.AppendFormat("<Info MenuId = \"{0}\" Privs = \"{1}\" />", t.MenuId,
                                    Utils.ReplaceXmlSpecialCharacter(GetPrivsIds(t.Privs)));
            }
            strXml.Append("</ROOT>");

            return strXml.ToString();
        }

        /// <summary>
        /// 构造系统信息的管理员信息
        /// </summary>
        /// <param name="adminSqlXml">管理员SqlXml</param>
        /// <param name="model">系统信息实体</param>
        private void GetSysAdminInfo(string adminSqlXml, Model.SysStructure.MSysInfo model)
        {
            if (string.IsNullOrEmpty(adminSqlXml) || model == null)
                return;

            XElement xRoot = XElement.Parse(adminSqlXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)
                return;

            foreach (var t in xRows)
            {
                model.UserId = Utils.GetXAttributeValue(t, "UserId");
                model.Username = Utils.GetXAttributeValue(t, "UserName");
                model.FullName = Utils.GetXAttributeValue(t, "ContactName");
                model.Telephone = Utils.GetXAttributeValue(t, "ContactTel");
                model.Mobile = Utils.GetXAttributeValue(t, "ContactMobile");
                model.Password = new Model.ComStructure.MPasswordInfo { NoEncryptPassword = Utils.GetXAttributeValue(t, "Password") };
                model.OnlineStatus = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus>(Utils.GetXAttributeValue(t, "OnlineStatus"), UserOnlineStatus.Offline);

                break;
            }
        }

        /// <summary>
        /// 根据SqlXML生成二级栏目集合
        /// </summary>
        /// <param name="sqlXml">二级栏目SalXml</param>
        /// <returns></returns>
        private IList<Model.SysStructure.MSysMenu2Info> GetSysMenu2ByXml(string sqlXml)
        {
            IList<Model.SysStructure.MSysMenu2Info> list = null;
            if (string.IsNullOrEmpty(sqlXml))
                return list;

            XElement xRoot = XElement.Parse(sqlXml);
            var xRow = Utils.GetXElements(xRoot, "row");
            if (xRow == null || xRow.Count() < 1)
                return list;

            list = new List<Model.SysStructure.MSysMenu2Info>();
            foreach (var t in xRow)
            {
                if (t == null)
                    continue;

                var model = new Model.SysStructure.MSysMenu2Info
                                {
                                    MenuId = Utils.GetInt(Utils.GetXAttributeValue(t, "Id")),
                                    Name = Utils.GetXAttributeValue(t, "Name"),
                                    Url = Utils.GetXAttributeValue(t, "Url"),
                                    Privs = GetSysPrivsByXml(Utils.GetXAttributeValue(t, "Privs3Xml"))
                                };

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 根据SqlXml生成权限集合
        /// </summary>
        /// <param name="sqlXml">权限Xml</param>
        /// <returns></returns>
        private IList<Model.SysStructure.MSysPrivsInfo> GetSysPrivsByXml(string sqlXml)
        {
            IList<Model.SysStructure.MSysPrivsInfo> list = null;
            if (string.IsNullOrEmpty(sqlXml))
                return list;

            XElement xRoot = XElement.Parse(sqlXml);
            var xRow = Utils.GetXElements(xRoot, "row");
            if (xRow == null || xRow.Count() < 1)
                return list;

            list = new List<Model.SysStructure.MSysPrivsInfo>();
            foreach (var t in xRow)
            {
                if(t == null)
                    continue;

                var model = new Model.SysStructure.MSysPrivsInfo
                                {
                                    PrivsId = Utils.GetInt(Utils.GetXAttributeValue(t, "Id")),
                                    Name = Utils.GetXAttributeValue(t, "Name"),
                                    Remark = Utils.GetXAttributeValue(t, "Remark")
                                };

                list.Add(model);
            }

            return list;
        }
        #endregion

        #region ISys 接口
        /// <summary>
        /// 创建子系统
        /// </summary>
        /// <param name="info">系统信息业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.创建系统信息
        /// 2.创建系统公司信息
        /// 3.创建管理员账号信息
        /// 4.创建管理员角色信息
        /// 5.创建总部信息
        /// 6.子系统基础数据
        /// </remarks>
        public int CreateSys(EyouSoft.Model.SysStructure.MSysInfo info)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_Sys_Create");
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, info.SysId);
            _db.AddInParameter(dc, "SysName", DbType.String, info.SysName);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(dc, "FullName", DbType.String, info.FullName);
            _db.AddInParameter(dc, "Telephone", DbType.String, info.Telephone);
            _db.AddInParameter(dc, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, info.UserId);
            _db.AddInParameter(dc, "Username", DbType.String, info.Username);
            _db.AddInParameter(dc, "NoEncryptPassword", DbType.String, info.Password.NoEncryptPassword);
            _db.AddInParameter(dc, "MD5Password", DbType.String, info.Password.MD5Password);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, info.IssueTime);
            //_db.AddInParameter(dc, "KingdeeKMImportPath", DbType.String, Utils.GetMapPath("/ExcelDownTemp/导入科目表.xls"));
            //_db.AddInParameter(dc, "KingdeeHSImportPath", DbType.String, Utils.GetMapPath("/ExcelDownTemp/导入核算项目.xls"));
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;

            return Utils.GetInt(obj.ToString());
        }

        /// <summary>
        /// 设置系统域名
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名信息集合</param>
        /// <returns></returns>
        public int SetSysDomains(string sysId, IList<EyouSoft.Model.SysStructure.MSysDomain> domains)
        {
            var strSql = new StringBuilder();
            strSql.Append("DELETE FROM [tbl_SysDomain] WHERE [SysId] = @SysId;");
            foreach (var t in domains)
            {
                if (string.IsNullOrEmpty(t.SysId))
                    continue;

                strSql.AppendFormat(" insert into [tbl_SysDomain] (SysId,Domain,Url) values ('{0}','{1}','{2}') ; ",
                                    sysId, t.Domain, t.Url);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 创建子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.创建子系统一级栏目
        /// 2.创建子系统二级栏目
        /// </remarks>
        public int CreateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info)
        {
            if (info == null || string.IsNullOrEmpty(info.SysId) || info.Menu2s == null || info.Menu2s.Count < 1)
                return 0;

            DbCommand dc = _db.GetStoredProcCommand("proc_SysMenu_Create");
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, info.SysId);
            _db.AddInParameter(dc, "Name", DbType.String, info.Name);
            _db.AddInParameter(dc, "Menu2Xml", DbType.String, GetMenu2SqlXml(info.Menu2s));
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);
            _db.AddInParameter(dc, "ClassName", DbType.String, info.ClassName);
            _db.AddInParameter(dc, "IsDisplay", DbType.AnsiStringFixedLength, info.IsDisplay ? "1" : "0");

            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;

            return Utils.GetInt(obj.ToString());
        }

        /// <summary>
        /// 修改子系统一级及二级栏目信息
        /// </summary>
        /// <param name="info">栏目信息业务实体</param>
        /// <returns></returns>
        public int UpdateSysMenu(EyouSoft.Model.SysStructure.MComMenu1Info info)
        {
            if (info == null || info.MenuId <= 0 || string.IsNullOrEmpty(info.SysId))
                return 0;

            DbCommand dc = _db.GetStoredProcCommand("proc_SysMenu_Update");
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, info.SysId);
            _db.AddInParameter(dc, "Menu1Id", DbType.Int32, info.MenuId);
            _db.AddInParameter(dc, "Menu1Name", DbType.String, info.Name);
            _db.AddInParameter(dc, "Menu2Xml", DbType.String, GetMenu2SqlXml(info.Menu2s));
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);
            _db.AddInParameter(dc, "ClassName", DbType.String, info.ClassName);
            _db.AddInParameter(dc, "IsDisplay", DbType.AnsiStringFixedLength, info.IsDisplay ? "1" : "0");

            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;

            return Utils.GetInt(obj.ToString());
        }

        /// <summary>
        /// 删除子系统一级栏目
        /// </summary>
        /// <param name="menu1Id">子系统一级栏目编号</param>
        /// <returns></returns>
        public int DeleteSysMenu1(int menu1Id)
        {
            if (menu1Id <= 0)
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SQL_DELETE_DeleteSysMenu1);
            _db.AddInParameter(dc, "Id", DbType.Int32, menu1Id);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置子系统权限
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <param name="privs">权限信息集合</param>
        /// <returns></returns>
        public int SetSysPrivs(string sysId, IList<EyouSoft.Model.SysStructure.MComMenu2Info> privs)
        {
            if (privs == null || privs.Count < 1)
                return 0;

            DbCommand dc = _db.GetStoredProcCommand("proc_SysMenu_SetMenu2Privs");
            _db.AddInParameter(dc, "Menu2Xml", DbType.String, GetMenu2PrivsSqlXml(privs));
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId);

            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;

            return Utils.GetInt(obj.ToString());
        }

        /// <summary>
        /// 设置子系统一级栏目及二级栏目为系统默认
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public int SetMenuBySys(string sysId)
        {
            if (string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36)
                return 0;

            DbCommand dc = _db.GetStoredProcCommand("proc_SysMenu_SetMenuSysDefault");
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId.Trim());
            _db.AddOutParameter(dc, "ReturnValue", DbType.Int32, 4);

            DbHelper.RunProcedure(dc, _db);

            object obj = _db.GetParameterValue(dc, "ReturnValue");
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;

            return Utils.GetInt(obj.ToString());
        }

        /// <summary>
        /// 设置角色权限为子系统开通的所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public int SetRoleBySysPrivs(int roleId, string sysId)
        {
            if (roleId <= 0 || string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36)
                return 0;

            var strSql = new StringBuilder();
            strSql.Append(" update tbl_ComRole set RoleChilds =  ");
            strSql.Append(" stuff((select ',' + Privs from tbl_SysMenu where SysId = @SysId and ParentId > 0 and Privs2Id > 0 and Privs>'' for xml path('')),1,1,'') ");
            strSql.Append(" where Id = @RoleId ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId.Trim());
            _db.AddInParameter(dc, "RoleId", DbType.Int32, roleId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置用户权限为子系统开通的所有权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public int SetUserBySysPrivs(string userId, string sysId)
        {
            if (string.IsNullOrEmpty(userId) || userId.Trim().Length != 36
                || string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36)
                return 0;

            var strSql = new StringBuilder();
            strSql.Append(" update tbl_ComUser set Privs =  ");
            strSql.Append(" stuff((select ',' + Privs from tbl_SysMenu where SysId = @SysId and ParentId > 0 and Privs2Id > 0 and Privs>'' for xml path('')),1,1,'') ");
            strSql.Append(" where UserId = @UserId ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId.Trim());
            _db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, userId.Trim());

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public bool SetUserRole(string userId, int roleId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SetUserRole);
            _db.AddInParameter(cmd, "RoleId", DbType.Int32, roleId);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);

            return DbHelper.ExecuteSql(cmd, _db) == 1;
        }

        /// <summary>
        /// 获取子系统信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysInfo> GetSyss(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SysStructure.MSysSearchInfo searchInfo)
        {
            IList<Model.SysStructure.MSysInfo> list = null;

            string fields = " SysId,SysName,IssueTime,CompanyId,AdminInfo ";
            string tableName = "view_SysAndCompanyAndUser";
            string orderByString = " [IssueTime] asc ";
            string identityColumnName = " SysId ";

            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identityColumnName, fields, string.Empty, orderByString))
            {
                list = new List<Model.SysStructure.MSysInfo>();
                while (dr.Read())
                {
                    var model = new Model.SysStructure.MSysInfo();
                    if (!dr.IsDBNull(dr.GetOrdinal("SysId")))
                        model.SysId = dr.GetString(dr.GetOrdinal("SysId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SysName")))
                        model.SysName = dr.GetString(dr.GetOrdinal("SysName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AdminInfo")))
                        GetSysAdminInfo(dr.GetString(dr.GetOrdinal("AdminInfo")), model);

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取子系统信息
        /// </summary>
        /// <param name="sysId">子系统编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysInfo GetSysInfo(string sysId)
        {
            Model.SysStructure.MSysInfo model = null;
            if (string.IsNullOrEmpty(sysId))
                return model;

            string strSql = " select SysId,SysName,IssueTime,CompanyId,AdminInfo from view_SysAndCompanyAndUser where SysId = @SysId ";
            DbCommand dc = _db.GetSqlStringCommand(strSql);
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new Model.SysStructure.MSysInfo();
                    if (!dr.IsDBNull(dr.GetOrdinal("SysId")))
                        model.SysId = dr.GetString(dr.GetOrdinal("SysId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SysName")))
                        model.SysName = dr.GetString(dr.GetOrdinal("SysName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AdminInfo")))
                        GetSysAdminInfo(dr.GetString(dr.GetOrdinal("AdminInfo")), model);
                }
            }

            return model;
        }        

        /// <summary>
        /// 获取默认栏目信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysMenu1Info> GetMenus()
        {
            var strSql = new StringBuilder(" SELECT a.[Id],a.[Name],a.[SortId],a.[IsEnable],a.[ClassName] ");
            strSql.Append(" ,(select b.Id,b.Name,b.Url,(select c.Id,c.Name,c.Remark from tbl_SysPrivs3 as c where c.ParentId = b.Id and c.IsEnable = '1' order by c.SortId asc for xml raw,root('Root')) as Privs3Xml from tbl_SysPrivs2 as b where b.ParentId = a.Id and b.IsEnable = '1' order by b.SortId asc for xml raw,root('Root')) as Privs2Xml ");
            strSql.Append(" from [tbl_SysPrivs1] as a ");
            strSql.Append(" where a.IsEnable = '1' order by a.SortId ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            IList<Model.SysStructure.MSysMenu1Info> list = new List<Model.SysStructure.MSysMenu1Info>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                while (dr.Read())
                {
                    var model = new Model.SysStructure.MSysMenu1Info();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.MenuId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        model.Name = dr.GetString(dr.GetOrdinal("Name"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Privs2Xml")))
                        model.Menu2s = GetSysMenu2ByXml(dr.GetString(dr.GetOrdinal("Privs2Xml")));
                    model.ClassName = dr["ClassName"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取默认权限信息集合
        /// </summary>
        /// <param name="defaultMenu2Id">默认二级栏目编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysPrivsInfo> GetPrivs(int defaultMenu2Id)
        {
            IList<Model.SysStructure.MSysPrivsInfo> list = null;
            if (defaultMenu2Id <= 0)
                return list;

            DbCommand dc = _db.GetSqlStringCommand(SQL_SELECT_GetPrivs);
            _db.AddInParameter(dc, "DefaultMenu2Id", DbType.Int32, defaultMenu2Id);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<Model.SysStructure.MSysPrivsInfo>();
                while (dr.Read())
                {
                    var model = new Model.SysStructure.MSysPrivsInfo();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.PrivsId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        model.Name = dr.GetString(dr.GetOrdinal("Name"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// set webmaster pwd
        /// </summary>
        /// <param name="webmasterId">webmaster id</param>
        /// <param name="username">webmaster username</param>
        /// <param name="pwd">webmaster pwd info</param>
        /// <returns></returns>
        public bool SetWebmasterPwd(int webmasterId, string username, EyouSoft.Model.ComStructure.MPasswordInfo pwd)
        {
            if (webmasterId <= 0 || string.IsNullOrEmpty(username) || pwd == null)
                return false;

            var strSql = new StringBuilder();
            strSql.Append(" update tbl_SysWebmaster set [Password] = @Password,MD5Password = @MD5Password where [Id] = @Id and [Username] = @Username ");
            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "Password", DbType.String, pwd.NoEncryptPassword);
            _db.AddInParameter(dc, "MD5Password", DbType.String, pwd.MD5Password);
            _db.AddInParameter(dc, "Id", DbType.Int32, webmasterId);
            _db.AddInParameter(dc, "Username", DbType.String, username);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取系统域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public IList<Model.SysStructure.MSysDomain> GetDomains(string sysId)
        {
            IList<Model.SysStructure.MSysDomain> list = null;
            if (string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36)
                return list;

            DbCommand dc = _db.GetSqlStringCommand(SQL_SELECT_GetDomains);
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<Model.SysStructure.MSysDomain>();
                while (dr.Read())
                {
                    var model = new Model.SysStructure.MSysDomain();
                    if (!dr.IsDBNull(dr.GetOrdinal("SysId")))
                        model.SysId = dr.GetString(dr.GetOrdinal("SysId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Domain")))
                        model.Domain = dr.GetString(dr.GetOrdinal("Domain"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Url")))
                        model.Url = dr.GetString(dr.GetOrdinal("Url"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 判断域名是否重复，返回重复的域名信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="domains">域名集合</param>
        /// <returns></returns>
        public IList<string> IsExistsDomains(string sysId, IList<string> domains)
        {
            IList<string> list = null;
            if (string.IsNullOrEmpty(sysId) || sysId.Trim().Length != 36 || domains == null || domains.Count < 1)
                return list;

            var strSql = new StringBuilder(" select [Domain] from [tbl_SysDomain] where SysId <> @SysId ");
            if (domains.Count == 1)
                strSql.AppendFormat(" and [Domain] = '{0}' ", domains[0]);
            else
            {
                string strIds = domains.Where(t => !string.IsNullOrEmpty(t)).Aggregate(string.Empty, (current, t) => current + "'" + t + "',");
                strSql.AppendFormat(" and [Domain] in ({0}) ", strIds.TrimEnd(','));
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "SysId", DbType.AnsiStringFixedLength, sysId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<string>();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("Domain")))
                        list.Add(dr.GetString(dr.GetOrdinal("Domain")));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取子系统角色(管理员)编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetSysRoleId(string companyId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetSysRoleId);
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
        /// 基础权限管理-写入一级栏目
        /// </summary>
        /// <param name="info">一级栏目信息业务实体</param>
        /// <returns></returns>
        public int InsertFirstMenu(EyouSoft.Model.SysStructure.MSysMenu1Info info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertFirstMenu);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "SortId", DbType.Int32, 0);
            _db.AddInParameter(cmd, "IsEnable", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(cmd, "ClassName", DbType.String, info.ClassName);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return Convert.ToInt32(rdr.GetDecimal(0));
                }
            }

            return 0;
        }

        /// <summary>
        /// 基础权限管理-写入二级栏目
        /// </summary>
        /// <param name="info">二级栏目信息业务实体</param>
        /// <returns></returns>
        public int InsertSecondMenu(EyouSoft.Model.SysStructure.MSysMenu2Info info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertSecondMenu);
            _db.AddInParameter(cmd, "ParentId", DbType.Int32, info.FirstId);
            _db.AddInParameter(cmd, "Name", DbType.String,info.Name);
            _db.AddInParameter(cmd, "Url", DbType.String, info.Url);
            _db.AddInParameter(cmd, "SortId", DbType.Int32, 0);
            _db.AddInParameter(cmd, "IsEnable", DbType.AnsiStringFixedLength, "1");

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return Convert.ToInt32(rdr.GetDecimal(0));
                }
            }

            return 0;
        }

        /// <summary>
        /// 基础权限管理-写入明细权限
        /// </summary>
        /// <param name="info">权限信息业务实体</param>
        /// <returns></returns>
        public int InsertPrivs(EyouSoft.Model.SysStructure.MSysPrivsInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertPrivs);
            _db.AddInParameter(cmd, "ParentId", DbType.Int32, info.SecondId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "SortId", DbType.Int32, 0);
            _db.AddInParameter(cmd, "IsEnable", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(cmd, "PrivsType", DbType.Byte, info.PrivsType);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return Convert.ToInt32(rdr.GetDecimal(0));
                }
            }

            return 0;
        }

        /// <summary>
        /// 相同二级栏目下是否存在相同的权限类别
        /// </summary>
        /// <param name="secondId">二级栏目编号</param>
        /// <param name="privsType">权限类别</param>
        /// <returns></returns>
        public bool IsExistsPrivsType(int secondId, EyouSoft.Model.EnumType.SysStructure.PrivsType privsType)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsPrivsType);
            _db.AddInParameter(cmd, "ParentId", DbType.Int32, secondId);
            _db.AddInParameter(cmd, "PrivsType", DbType.Byte, privsType);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return true;
        }

        /// <summary>
        ///  相同二级栏目下是否存在相同的权限名称
        /// </summary>
        /// <param name="secondId">二级栏目编号</param>
        /// <param name="privsName">权限名称</param>
        /// <returns></returns>
        public bool IsExistsPrivsName(int secondId, string privsName)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsPrivsName);
            _db.AddInParameter(cmd, "ParentId", DbType.Int32, secondId);
            _db.AddInParameter(cmd, "Name", DbType.String, privsName);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return true;
        }

        /// <summary>
        /// 相同一级栏目下是否存在相同的二级栏目名称
        /// </summary>
        /// <param name="firstId">一级栏目编号</param>
        /// <param name="menu2Name">二级栏目名称</param>
        /// <returns></returns>
        public bool IsExistsMenu2Name(int firstId, string menu2Name)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsMenu2Name);
            _db.AddInParameter(cmd, "ParentId", DbType.Int32, firstId);
            _db.AddInParameter(cmd, "Name", DbType.String, menu2Name);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否存在相同的一级栏目名称
        /// </summary>
        /// <param name="menu1Name">一级栏目名称</param>
        /// <returns></returns>
        public bool IsExistsMenu1Name(string menu1Name)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsMenu1Name);
            _db.AddInParameter(cmd, "Name", DbType.String, menu1Name);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }

            return true;
        }

        /// <summary>
        /// 基础权限管理-更新二级栏目链接
        /// </summary>
        /// <param name="menu2Id">二级栏目编号</param>
        /// <param name="url">二级栏目链接</param>
        /// <returns></returns>
        public bool UpdateMenu2Url(int menu2Id, string url)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateMenu2Url);
            _db.AddInParameter(cmd, "Menu2Id", DbType.Int32, menu2Id);
            _db.AddInParameter(cmd, "Url", DbType.String, url);

            return DbHelper.ExecuteSql(cmd, _db) > 0;
        }

        /// <summary>
        /// 判断子系统是否开通二级栏目
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <param name="menu2">二级栏目</param>
        /// <returns></returns>
        public bool IsExistsMenu2(string sysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2 menu2)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsMenu2);
            _db.AddInParameter(cmd, "SysId", DbType.AnsiStringFixedLength, sysId);
            _db.AddInParameter(cmd, "Privs2Id", DbType.Int32, menu2);

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
