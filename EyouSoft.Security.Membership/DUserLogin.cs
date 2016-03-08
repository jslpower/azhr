//2011-09-23 汪奇志
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SysStructure;
using System.Xml.Linq;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 系统用户登录数据访问类
    /// </summary>
    internal class DUserLogin : DALBase, IUserLogin
    {
        #region static constants
        //static constants
        const string SQL_SELECT_Login = "SELECT [UserId],[UserName],[CompanyId],[TourCompanyId],[UserType],[ContactName],[ContactTel],[ContactMobile],[LastLoginTime],[DeptId],[DeptName],[Privs],[UserStatus],[IsAdmin],[DeptIdJG],[RoleId],[ContactFax],[OnlineStatus],[OnlineSessionId] FROM [tbl_ComUser] ";
        //const string SQL_SELECT_GetTourCompanyInfo = "SELECT [Name],[LevId] FROM [tbl_Crm] WHERE [CrmId]=@CRMID";
        const string SQL_SELECT_GetTourCompanyInfo = "SELECT B.[Name],B.[LevId],A.Id FROM tbl_CrmLinkman AS A INNER JOIN [tbl_Crm] AS B ON A.TypeId=B.CrmId WHERE A.UserId=@UserId AND A.TypeId=@CRMID AND A.[Type]=@LxrType";
        const string SQL_SELECT_GetSourceCompanyInfo = "SELECT [Name] FROM [tbl_Source] WHERE [SourceId]=@SOURCEID";
        const string SQL_INSERT_LoginLogwr = "INSERT INTO [tbl_SysLogLogin]([LogId],[CompanyId],[OperatorId],[IssueTime],[Ip],[LoginType],[Client]) VALUES (@LogId,@CompanyId,@OperatorId,@IssueTime,@Ip,@LoginType,@Client);";
        const string SQL_SELECT_GetDomain = "SELECT A.[SysId],A.[Domain],A.[Url],(SELECT B.[Id] FROM [tbl_Company] AS B WHERE B.[SysId]=A.[SysId]) AS CompanyId FROM [tbl_SysDomain] AS A WHERE A.[Domain]=@Domain";
        //const string SQL_SELECT_GetDeptUsers = "WITH DeptCTE AS(SELECT [DepartId],0 AS LVL FROM [tbl_ComDepartment] WHERE [DepartId]=@DeptId UNION ALL SELECT B.[DepartId],A.LVL+1 FROM DeptCTE AS A JOIN [tbl_ComDepartment] AS B ON A.[DepartId]=B.[PrevDepartId]) SELECT [UserId] FROM [tbl_ComUser] WHERE [DeptId] IN(SELECT [DepartId] FROM DeptCTE)";
        const string SQL_SELECT_GetComMenus = "SELECT A.[Id],A.[Name],(SELECT B.[Id],B.[Name],B.[Privs2Id],B.[Url],B.[Privs] FROM [tbl_SysMenu] AS B WHERE B.[ParentId]=A.[Id] FOR XML RAW,ROOT('root')) AS Menu2,A.[ClassName],A.[IsDisplay]  FROM [tbl_SysMenu] AS A WHERE A.[Privs2Id]=0 AND A.[SysId]=@SysId ORDER BY A.SortId DESC";
        const string SQL_SELECT_GetCompany = "SELECT [Name],[SysId] FROM [tbl_Company] WHERE [Id]=@CID";
        const string SQL_SELECT_GetPrivs = "SELECT [Id],[Name],[PrivsType] FROM [tbl_SysPrivs3] WHERE [Id] IN({0})";
        const string SQL_SELECT_GetRolePrivs = "SELECT [RoleChilds] FROM [tbl_ComRole] WHERE [Id]=@RoleId";
        const string SQL_SELECT_GetSysMenu2s = "SELECT A.[Id],A.[Name],A.[Url],(SELECT B.[Id],B.[Name],B.[PrivsType] FROM [tbl_SysPrivs3] AS B WHERE B.[ParentId]=A.[Id] FOR XML RAW,ROOT('root')) AS PrivsXML FROM [tbl_SysPrivs2] AS A WHERE A.IsEnable='1'";
        const string SQL_SELECT_GetComDepts = "SELECT A.[DepartId],A.[DepartName],A.[PrevDepartId],(SELECT COUNT(*) FROM [tbl_ComDepartment] AS B WHERE B.[PrevDepartId]=A.[DepartId]) AS ChildrenCount FROM [tbl_ComDepartment] AS A WHERE A.[CompanyId]=@CompanyId";
        const string SQL_SELECT_GetSettings = "SELECT [Key],[Value] FROM tbl_ComSetting WHERE [CompanyId]=@CompanyId;SELECT [WLogo],[NLogo],[MLogo],[Name],[GYSLogo],[FXSLogo] FROM [tbl_Company] WHERE [Id]=@CompanyId";
        const string SQL_UPDATE_SetOnlineStatus = "UPDATE [tbl_ComUser] SET [OnlineStatus]=@OnlineStatus,[OnlineSessionId]=@OnlineSessionId WHERE [UserId]=@UserId";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DUserLogin()
        {
            _db = SystemStore;
        }
        #endregion        

        #region private members
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private MUserInfo ReadUserInfo(DbCommand cmd)
        {
            MUserInfo info = null;
            string privs = string.Empty;
            string otherCompanyId = string.Empty;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, SystemStore))
            {
                if (rdr.Read())
                {
                    info = new MUserInfo();

                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    //info.CompanyName = rdr["CompanyName"].ToString();
                    info.DeptId = rdr.GetInt32(rdr.GetOrdinal("DeptId"));
                    info.DeptName = rdr["DeptName"].ToString();
                    info.Fax = rdr["ContactFax"].ToString();
                    info.IsAdmin = GetBoolean(rdr.GetString(rdr.GetOrdinal("IsAdmin")));
                    info.Status = (EyouSoft.Model.EnumType.ComStructure.UserStatus)rdr.GetByte(rdr.GetOrdinal("UserStatus"));
                    info.JGDeptId = rdr.GetInt32(rdr.GetOrdinal("DeptIdJG"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastLoginTime"))) info.LastLoginTime = rdr.GetDateTime(rdr.GetOrdinal("LastLoginTime"));
                    info.Mobile = rdr["ContactMobile"].ToString();
                    info.Name = rdr["ContactName"].ToString();                    
                    info.Telephone = rdr["ContactTel"].ToString();
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserId"));
                    info.Username = rdr.GetString(rdr.GetOrdinal("UserName"));
                    info.UserType = (EyouSoft.Model.EnumType.ComStructure.UserType)rdr.GetByte(rdr.GetOrdinal("UserType"));
                    privs = rdr["Privs"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("TourCompanyId"))) otherCompanyId = rdr.GetString(rdr.GetOrdinal("TourCompanyId"));
                    info.RoleId = rdr.GetInt32(rdr.GetOrdinal("RoleId"));
                    info.OnlineSessionId = rdr["OnlineSessionId"].ToString();
                    info.OnlineStatus = (UserOnlineStatus)rdr.GetByte(rdr.GetOrdinal("OnlineStatus"));
                }
            }

            if (info != null)
            {
                //公司名称及系统编号处理
                string companyName, sysId;
                GetCompany(info.CompanyId, out sysId, out companyName);
                info.CompanyName = companyName;
                info.SysId = sysId;

                //权限处理
                /*if (!string.IsNullOrEmpty(privs))
                {
                    string[] arr = privs.Split(',');
                    int count = arr.Length;
                    info.Privs = new int[count];

                    for (int i = 0; i < count; i++)
                    {
                        info.Privs[i] = Utils.GetInt(arr[i], -1);
                    }
                }
                else
                {
                    info.Privs = new int[] { -1 };
                }*/
                info.RolePrivs = GetRolePrivs(info.RoleId);

                //供应商用户处理
                if (info.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.供应商)
                {
                    info.SourceCompanyInfo = GetSourceCompanyInfo(otherCompanyId);
                }

                //分销商用户处理
                if (info.UserType == EyouSoft.Model.EnumType.ComStructure.UserType.组团社)
                {
                    info.TourCompanyInfo = GetTourCompanyInfo(otherCompanyId, info.UserId);
                }
            }

            return info;
        }

        /// <summary>
        /// 获取组团公司信息
        /// </summary>
        /// <param name="crmid">组团公司编号</param>
        /// <returns></returns>
        private MTourCompanyInfo GetTourCompanyInfo(string crmid, string userId)
        {
            MTourCompanyInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetTourCompanyInfo);
            _db.AddInParameter(cmd, "CRMID", DbType.AnsiStringFixedLength, crmid);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.客户单位);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new MTourCompanyInfo()
                    {
                        CompanyId = crmid,
                        CompanyName = rdr["Name"].ToString(),
                        LevelId = rdr.GetInt32(rdr.GetOrdinal("LevId")),
                        LxrId = rdr.GetString(rdr.GetOrdinal("Id"))
                    };
                }
            }

            return info;
        }

        /// <summary>
        /// 获取供应商公司信息
        /// </summary>
        /// <param name="sourceid">供应商公司编号</param>
        /// <returns></returns>
        private MSourceCompanyInfo GetSourceCompanyInfo(string sourceid)
        {
            MSourceCompanyInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetSourceCompanyInfo);
            _db.AddInParameter(cmd, "SOURCEID", DbType.AnsiStringFixedLength, sourceid);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new MSourceCompanyInfo()
                    {
                        CompanyId = sourceid,
                        CompanyName = rdr["Name"].ToString()
                    };
                }
            }

            return info;
        }

        /// <summary>
        /// 获取公司名称及系统编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sysId">系统编号</param>
        /// <param name="name">公司名称</param>
        private void GetCompany(string companyId, out string sysId, out string name)
        {
            sysId = string.Empty;
            name = string.Empty;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCompany);
            _db.AddInParameter(cmd, "CID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    name = rdr[0].ToString();
                    sysId = rdr.GetString(1);                    
                }
            }
        }

        /// <summary>
        /// 获取权限信息集合
        /// </summary>
        /// <param name="privs">权限编号集合，多个权限用,间隔</param>
        /// <param name="menu2PrivId">二级栏目栏目权限编号</param>
        /// <returns></returns>
        private IList<MSysPrivsInfo> GetPrivs(string privs, out int menu2PrivId)
        {
            menu2PrivId = -2;
            IList<MSysPrivsInfo> items = new List<MSysPrivsInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(string.Format(SQL_SELECT_GetPrivs, privs));

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    items.Add(new MSysPrivsInfo()
                    {
                        Name = rdr.GetString(1),
                        PrivsId = rdr.GetInt32(0)
                    });

                    if ((EyouSoft.Model.EnumType.SysStructure.PrivsType)rdr.GetByte(2)== EyouSoft.Model.EnumType.SysStructure.PrivsType.栏目)
                    {
                        menu2PrivId = rdr.GetInt32(0);
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// 获取角色权限集合
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        private int[] GetRolePrivs(int roleId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetRolePrivs);
            _db.AddInParameter(cmd, "RoleId", DbType.Int32, roleId);

            string privs = string.Empty;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    privs = rdr["RoleChilds"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(privs))
            {
                string[] arr = privs.Split(',');
                int count = arr.Length;
                int[] retArr = new int[count];

                for (int i = 0; i < count; i++)
                {
                    retArr[i] = Utils.GetInt(arr[i], -1);
                }

                return retArr;
            }

            return new int[] { -1 };
        }
        #endregion

        #region IUserLogin 成员
        /// <summary>
        /// 用户登录，根据系统公司编号、用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        public MUserInfo Login(string companyId, string username, MPasswordInfo pwd)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Login + " WHERE [CompanyId]=@CID AND [UserName]=@UN AND [MD5Password]=@MD5PWD AND [IsDelete]='0' ");
            _db.AddInParameter(cmd, "CID", DbType.AnsiString, companyId);
            _db.AddInParameter(cmd, "UN", DbType.String, username);
            _db.AddInParameter(cmd, "MD5PWD", DbType.String, pwd.MD5Password);

            return ReadUserInfo(cmd);
        }

        /// <summary>
        /// 用户登录，根据用户编号获取用户信息
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public MUserInfo Login(string userid)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Login + " WHERE [UserId]=@UID AND [IsDelete]='0' ");
            _db.AddInParameter(cmd, "UID", DbType.AnsiString, userid);
            
            return ReadUserInfo(cmd);
        }

        /// <summary>
        /// 用户登录，根据系统公司编号、用户名获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <returns></returns>
        public MUserInfo Login(string companyId, string username)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Login+" WHERE [CompanyId]=@CID AND [UserName]=@UN AND [IsDelete]='0' ");
            _db.AddInParameter(cmd, "CID", DbType.AnsiString, companyId);
            _db.AddInParameter(cmd, "UN", DbType.String, username);

            return ReadUserInfo(cmd);
        }

        /// <summary>
        /// 写登录日志，用户登录时更新最后登录时间、在线状态、会话标识
        /// </summary>
        /// <param name="info">登录用户信息</param>
        /// <param name="loginType">登录类型</param>
        public void LoginLogwr(MUserInfo info, EyouSoft.Model.EnumType.ComStructure.UserLoginType loginType)
        {
            string cmdText = SQL_INSERT_LoginLogwr;

            if (loginType == EyouSoft.Model.EnumType.ComStructure.UserLoginType.用户登录)
            {
                cmdText = SQL_INSERT_LoginLogwr + "UPDATE [tbl_ComUser] SET [LastLoginTime]=@IssueTime,[OnlineStatus]=@OnlineStatus,[OnlineSessionId]=@OnlineSessionId WHERE [UserId]=@OperatorId;";
            }

            DbCommand cmd = _db.GetSqlStringCommand(cmdText);

            _db.AddInParameter(cmd, "LogId", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.UserId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(cmd, "Ip", DbType.String, Utils.GetRemoteIP());
            _db.AddInParameter(cmd, "LoginType", DbType.Byte, loginType);
            _db.AddInParameter(cmd, "Client", DbType.String, new EyouSoft.Toolkit.BrowserInfo().ToJsonString());
            _db.AddInParameter(cmd, "OnlineStatus", DbType.Byte, info.OnlineStatus);
            _db.AddInParameter(cmd, "OnlineSessionId", DbType.AnsiString, info.OnlineSessionId);

            DbHelper.ExecuteSql(cmd, _db);
        }

        /// <summary>
        /// 获取域名信息
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        public MSysDomain GetDomain(string domain)
        {
            MSysDomain info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDomain);
            _db.AddInParameter(cmd, "Domain", DbType.String, domain);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new MSysDomain()
                    {
                        CompanyId = rdr.GetString(3),
                        Domain = domain,
                        SysId = rdr.GetString(0),
                        Url = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2)
                    };
                }
            }

            return info;
        }

        /*/// <summary>
        /// 获取同级部门及下级部门用户编号集合
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public IList<string> GetDeptUsers(int deptId)
        {
            IList<string> items = new List<string>();

            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDeptUsers);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, deptId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    items.Add(rdr.GetString(0));
                }
            }

            return items;
        }*/

        /// <summary>
        /// 获取公司菜单信息集合
        /// </summary>
        /// <param name="sysId">系统编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MComMenu1Info> GetComMenus(string sysId)
        {
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> items = new List<EyouSoft.Model.SysStructure.MComMenu1Info>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetComMenus);
            _db.AddInParameter(cmd, "SysId", DbType.AnsiStringFixedLength, sysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SysStructure.MComMenu1Info item = new EyouSoft.Model.SysStructure.MComMenu1Info();

                    item.MenuId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                    item.Name = rdr["Name"].ToString();
                    item.SysId = sysId;
                    item.Menu2s = new List<EyouSoft.Model.SysStructure.MComMenu2Info>();
                    item.ClassName = rdr["ClassName"].ToString();
                    item.IsDisplay = rdr.GetString(rdr.GetOrdinal("IsDisplay")) == "1";

                    string xml = rdr["Menu2"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        XElement xRoot = XElement.Parse(xml);
                        var xRows = Utils.GetXElements(xRoot, "row");

                        foreach (var xRow in xRows)
                        {
                            string privs = Utils.GetXAttributeValue(xRow, "Privs");
                            EyouSoft.Model.SysStructure.MComMenu2Info menu2Info = new EyouSoft.Model.SysStructure.MComMenu2Info();

                            menu2Info.DefaultMenu2Id = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Privs2Id"));
                            menu2Info.MenuId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Id"));
                            menu2Info.Name = Utils.GetXAttributeValue(xRow, "Name");
                            menu2Info.Privs = new List<EyouSoft.Model.SysStructure.MSysPrivsInfo>();
                            menu2Info.Url = Utils.GetXAttributeValue(xRow, "Url");

                            if (!string.IsNullOrEmpty(privs))
                            {
                                int menu2PrivId = 0;
                                menu2Info.Privs = GetPrivs(privs, out menu2PrivId);
                                menu2Info.Menu2PrivId = menu2PrivId;
                            }

                            item.Menu2s.Add(menu2Info);
                        }
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取系统默认二级栏目信息集合
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysMenu2Info> GetSysMenu2s()
        {
            IList<EyouSoft.Model.SysStructure.MSysMenu2Info> items = new List<EyouSoft.Model.SysStructure.MSysMenu2Info>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetSysMenu2s);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysMenu2Info();
                    item.MenuId = rdr.GetInt32(rdr.GetOrdinal("Id"));
                    item.Name = rdr["Name"].ToString();
                    item.Url = rdr["Url"].ToString();

                    item.Privs = new List<EyouSoft.Model.SysStructure.MSysPrivsInfo>();

                    string xml = rdr["PrivsXML"].ToString();

                    if (!string.IsNullOrEmpty(xml))
                    {
                        XElement xroot = XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");

                        foreach (var xrow in xrows)
                        {
                            var privsItem = new EyouSoft.Model.SysStructure.MSysPrivsInfo();
                            privsItem.PrivsId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "Id"));
                            privsItem.Name = Utils.GetXAttributeValue(xrow, "Name");
                            privsItem.PrivsType = Utils.GetEnumValue<EyouSoft.Model.EnumType.SysStructure.PrivsType>(Utils.GetXAttributeValue(xrow, "PrivsType"), EyouSoft.Model.EnumType.SysStructure.PrivsType.其它);

                            item.Privs.Add(privsItem);
                        }                             
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取公司部门集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public List<MCacheDeptInfo> GetComDepts(string companyId)
        {
            var items = new List<MCacheDeptInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetComDepts);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MCacheDeptInfo();

                    item.DeptId = rdr.GetInt32(rdr.GetOrdinal("DepartId"));
                    item.ParentId = rdr.GetInt32(rdr.GetOrdinal("PrevDepartId"));
                    item.HasChildren = rdr.GetInt32(rdr.GetOrdinal("ChildrenCount")) > 0;
                    item.Depts = new List<int>() { item.DeptId };

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public MComSetting GetComSetting(string companyId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            MComSetting setting = new MComSetting();
            setting.PrintDocument = new List<PrintDocument>();
            setting.SmsConfig = new MSmsConfigInfo();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    string key = rdr["Key"].ToString();
                    string value = rdr["value"].ToString();

                    if (string.IsNullOrEmpty(key)) continue;

                    #region 单据配置键处理
                    //单据配置的key为枚举+'_'
                    if (key.IndexOf(string.Format("{0}_", (int)SysConfiguration.单据配置)) == 0)
                    {
                        string printTemplateType = key.Substring(string.Format("{0}_", (int)SysConfiguration.单据配置).Length);
                        setting.PrintDocument.Add(new PrintDocument()
                        {
                            PrintTemplate = value,
                            PrintTemplateType = (PrintTemplateType)Enum.Parse(typeof(PrintTemplateType), printTemplateType)
                        });
                        continue;
                    }
                    #endregion

                    #region 配置键为数字处理
                    int? _key = EyouSoft.Toolkit.Utils.GetIntNullable(key, null);
                    if (_key.HasValue)
                    {
                        EyouSoft.Model.EnumType.ComStructure.SysConfiguration sysConfiguration = (SysConfiguration)_key.Value;
                        switch (sysConfiguration)
                        {
                            case SysConfiguration.Word模版: setting.WordTemplate = value; break;
                            case SysConfiguration.公司章: setting.CompanyChapter = value; break;
                            case SysConfiguration.积分比例: setting.IntegralProportion = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收出境线: setting.ExitArea = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收国内线: setting.CountryArea = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收省内线: setting.ProvinceArea = Utils.GetInt(value); break;
                            case SysConfiguration.劳动合同到期提醒: setting.ContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.供应商合同到期提醒: setting.SContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.公司合同到期提醒: setting.ComPanyContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.洒店预控到期提醒: setting.HotelControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.车辆预控到期提醒: setting.CarControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.游船预控到期提醒: setting.ShipControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.景点预控到期提醒: setting.SightControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.其他预控到期提醒: setting.OtherControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.列表显示控制_前几个月: setting.ShowBeforeMonth = Utils.GetInt(value); break;
                            case SysConfiguration.列表显示控制_后几个月: setting.ShowAfterMonth = Utils.GetInt(value); break;
                            case SysConfiguration.留位时间控制: setting.SaveTime = Utils.GetInt(value); break;
                            case SysConfiguration.欠款额度控制: setting.ArrearsRangeControl = GetBoolean(value); break;
                            case SysConfiguration.跳过报账终审: setting.SkipFinalJudgment = GetBoolean(value); break;
                            case SysConfiguration.跳过导游报账: setting.SkipGuide = GetBoolean(value); break;
                            case SysConfiguration.跳过销售报账: setting.SkipSale = GetBoolean(value); break;
                            case SysConfiguration.团号配置: setting.TourNoSetting = value; break;
                            case SysConfiguration.页脚: setting.FooterPath = value; break;
                            case SysConfiguration.页眉: setting.PagePath = value; break;
                            case SysConfiguration.财务收入审核: setting.FinancialIncomeReview = GetBoolean(value); break;
                            case SysConfiguration.财务支出审核: setting.FinancialExpensesReview = GetBoolean(value); break;
                            case SysConfiguration.是否开启KIS整合: setting.IsEnableKis = GetBoolean(value); break;
                            default: break;
                        }

                        continue;
                    }
                    #endregion

                    #region 配置键为字符串处理
                    switch (key)
                    {
                        case "SmsAccount": setting.SmsConfig.Account = value; break;
                        case "SmsAppKey": setting.SmsConfig.AppKey = value; break;
                        case "SmsAppSecret": setting.SmsConfig.AppSecret = value; break;
                        case "MaxUserNumber": setting.MaxUserNumber = Utils.GetInt(value); break;
                        case "UserLoginLimitType": setting.UserLoginLimitType = Utils.GetEnumValue<EyouSoft.Model.EnumType.ComStructure.UserLoginLimitType>(value, UserLoginLimitType.None); break;
                        case "IsEnableDuanXian": setting.IsEnableDuanXian = GetBoolean(value); break;
                        case "ZiDongShanChuSanPinJiHua": setting.IsZiDongShanChuSanPinJiHua = GetBoolean(value); break;
                        default: break;
                    }
                    #endregion
                }

                if (rdr.NextResult())
                {
                    if (rdr.Read())
                    {
                        setting.WLogo = rdr["WLogo"].ToString();
                        setting.NLogo = rdr["NLogo"].ToString();
                        setting.MLogo = rdr["MLogo"].ToString();
                        setting.CompanyName = rdr["Name"].ToString();
                        setting.GYSLogo = rdr["GYSLogo"].ToString();
                        setting.FXSLogo = rdr["FXSLogo"].ToString();
                    }
                }

                setting.CompanyId = companyId;
            }

            return setting;
        }

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="status">在线状态</param>
        /// <param name="onlineSessionId">用户会话状态标识</param>
        /// <returns></returns>
        public bool SetOnlineStatus(string userId, EyouSoft.Model.EnumType.ComStructure.UserOnlineStatus status, string onlineSessionId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_SetOnlineStatus);
            _db.AddInParameter(cmd, "OnlineStatus", DbType.Byte, status);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            _db.AddInParameter(cmd, "OnlineSessionId", DbType.AnsiString, onlineSessionId);

            return DbHelper.ExecuteSql(cmd, _db) > 0;
        }
        #endregion
    }
}
