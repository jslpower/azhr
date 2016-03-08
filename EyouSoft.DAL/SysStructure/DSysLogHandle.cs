//汪奇志 2011-09-20
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using EyouSoft.IDAL.SysStructure;
using System.Xml.Linq;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 操作日志数据访问类
    /// </summary>
    public class DSysLogHandle : DALBase, ISysLogHandle
    {
        #region static constants
        //static constants
        const string SQL_SELECT_Insert = "INSERT INTO [tbl_SysLogHandle]([LogId],[CompanyId],[DeptId],[OperatorId],[Operator],[Menu1Id],[Menu2Id],[Content],[RemoteIp],[IssueTime]) VALUES (@LogId,@CompanyId,@DeptId,@OperatorId,@Operator,@Menu1Id,@Menu2Id,@Content,@RemoteIp,@IssueTime)";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DSysLogHandle()
        {
            _db = SystemStore;
        }
        #endregion

        #region ISysLogHandle 成员
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="item">操作日志信息业务实体</param>
        /// <returns></returns>
        public bool Insert(EyouSoft.Model.SysStructure.MSysLogHandleInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_Insert);

            _db.AddInParameter(cmd, "LogId", DbType.AnsiStringFixedLength, info.LogId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, info.DeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "Menu1Id", DbType.Int32, info.Menu1Id);
            _db.AddInParameter(cmd, "Menu2Id", DbType.Int32, info.Menu2Id);
            _db.AddInParameter(cmd, "Content", DbType.String, info.Content);
            _db.AddInParameter(cmd, "RemoteIp", DbType.String, info.RemoteIp);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取操作日志信息
        /// </summary>
        /// <param name="logId">日志编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysLogHandleInfo GetInfo(string logId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取操作日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门集合</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysLogHandleInfo> GetLogs(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SysStructure.MSysLogHandleSearch searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysLogHandleInfo> list = new List<EyouSoft.Model.SysStructure.MSysLogHandleInfo>();
            EyouSoft.Model.SysStructure.MSysLogHandleInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "view_SysLogHandle";
            string OrderByString = "IssueTime DESC";
            string fields = "*";

            #region SQL
            cmdQuery.AppendFormat(" CompanyId='{0}' ", companyId);
            cmdQuery.AppendFormat(GetOrgCondition(userId, depts, "OperatorId", "DeptId"));

            if (searchInfo != null)
            {
                if (!string.IsNullOrEmpty(searchInfo.OperatorId)) cmdQuery.AppendFormat(" and OperatorId='{0}'", searchInfo.OperatorId);
                if (searchInfo.DeptId.HasValue) cmdQuery.AppendFormat(" and DeptId={0}", searchInfo.DeptId.Value);
                if (!string.IsNullOrEmpty(searchInfo.Operator)) cmdQuery.AppendFormat(" and Operator LIKE '%{0}%'", searchInfo.Operator);
                if (searchInfo.EDate.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", searchInfo.EDate.Value);
                if (searchInfo.SDate.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", searchInfo.SDate.Value);
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.SysStructure.MSysLogHandleInfo()
                    {
                        Operator = rdr["Operator"].ToString(),
                        DeptId = rdr.GetInt32(rdr.GetOrdinal("DeptId")),
                        DeptName = rdr["DepartName"].ToString(),
                        Menu2Name = rdr["Menu2Name"].ToString(),
                        RemoteIp = rdr["RemoteIp"].ToString(),
                        IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Content = rdr["Content"].ToString(),
                        Menu1Name = rdr["Menu1Name"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取登录日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysLogLoginInfo> GetLoginLogs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SysStructure.MSysLogLoginSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysLogLoginInfo> items = new List<EyouSoft.Model.SysStructure.MSysLogLoginInfo>();

            string tableName = "view_SysLogLogin";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            StringBuilder query = new StringBuilder();

            #region SQL
            query.AppendFormat(" CompanyId='{0}' AND LoginType={1} ", companyId, (int)EyouSoft.Model.EnumType.ComStructure.UserLoginType.用户登录);
            query.AppendFormat(" AND UserType IN({0},{1}) ", (int)EyouSoft.Model.EnumType.ComStructure.UserType.内部员工, (int)EyouSoft.Model.EnumType.ComStructure.UserType.导游);

            if (searchInfo != null)
            {
                if (searchInfo.STime.HasValue) query.AppendFormat(" AND datediff(day,'{0}',IssueTime)>=0 ", searchInfo.STime.Value);
                if (!string.IsNullOrEmpty(searchInfo.Name)) query.AppendFormat(" AND ContactName LIKE '%{0}%' ", searchInfo.Name);
                if (searchInfo.ETime.HasValue) query.AppendFormat(" AND datediff(day,'{0}',IssueTime)<=0 ", searchInfo.ETime.Value);
                if (!string.IsNullOrEmpty(searchInfo.UserId)) query.AppendFormat(" AND OperatorId='{0}' ", searchInfo.UserId);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysLogLoginInfo();
                    item.IP = rdr["IP"].ToString();
                    item.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.Name = rdr["ContactName"].ToString();
                    item.Username = rdr["UserName"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
