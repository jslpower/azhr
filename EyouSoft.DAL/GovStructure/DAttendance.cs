using System;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Toolkit;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Xml.Linq;
namespace EyouSoft.DAL.GovStructure
{
    /// <summary>
    /// 考勤管理数据访问层
    /// 2011-09-13 邵权江 创建
    /// </summary>
    public class DAttendance : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IAttendance
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DAttendance()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        private const string SQL_SELECT_ExistsAttendance = "select AttendanceId from tbl_GovAttendance where CompanyId=@CompanyId AND StaffId=@StaffId AND AttendanceTime=@AttendanceTime AND AttendanceType=@AttendanceType";
        #endregion

        #region 成员方法
        /// <summary>
        /// 是否已登记考勤(0准点 1迟到 2早退 3旷工)
        /// 如果 准点 就不能 迟到 矿工
        /// 如果 迟到 就不能 准点 矿工
        /// 如果 早退 就不能 矿工
        /// 如果 矿工 就不能 准点 迟到 早退
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">职员编号</param>
        /// <param name="AttendanceTime">考勤时间</param>
        /// <param name="AttendanceType">考情类型</param>
        /// <param name="AttendanceId">考勤id，新增时AttendanceId=""</param>
        /// <returns></returns>
        public bool ExistsNum(string CompanyId, string StaffId, DateTime AttendanceTime, EyouSoft.Model.EnumType.GovStructure.AttendanceType AttendanceType, string AttendanceId)
        {
            string StrSql = "SELECT Count(1) FROM tbl_GovAttendance WHERE CompanyId=@CompanyId AND StaffId=@StaffId AND datediff(dd, @AttendanceTime, AttendanceTime) = 0 ";
            //if (AttendanceType.ToString() == "准点" || AttendanceType.ToString() == "迟到")
            //{
            //    StrSql += " AND (AttendanceType=0 or AttendanceType=1 or AttendanceType=3) ";
            //}
            //if (AttendanceType.ToString() == "早退")
            //{
            //    StrSql += " AND (AttendanceType=2 or AttendanceType=3) ";
            //}
            //if (AttendanceType.ToString() == "旷工")
            //{
            //    StrSql += " AND (AttendanceType=0 or AttendanceType=1 or AttendanceType=2 or AttendanceType=3) ";
            //}
            if (AttendanceId.Trim() != "")
            {
                StrSql += " AND [AttendanceId]<>@AttendanceId";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (AttendanceId.Trim() != "")
            {
                this._db.AddInParameter(dc, "AttendanceId", DbType.AnsiStringFixedLength, AttendanceId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "StaffId", DbType.AnsiStringFixedLength, StaffId);
            this._db.AddInParameter(dc, "AttendanceTime", DbType.DateTime, AttendanceTime);
            this._db.AddInParameter(dc, "AttendanceType", DbType.Byte, (int)AttendanceType);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 批量登记考勤(判断请假加班数)
        /// </summary>
        /// <param name="list">考勤model列表</param>
        /// <returns></returns>
        public bool AddAllGovAttendanceList(IList<EyouSoft.Model.GovStructure.MGovAttendance> list)
        {
            bool IsTrue = false;
            string AttendanceXML = CreateGovAttendanceXML(list);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovAttendance_Add");
            this._db.AddInParameter(dc, "AttendanceXML", DbType.Xml, AttendanceXML);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                IsTrue = int.Parse(Result.ToString()) > 0 ? true : false;
            }
            return IsTrue;
        }

        /// <summary>
        /// 更新考勤
        /// </summary>
        /// <param name="StaffId">员工编号</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="list">考勤model列表</param>
        /// <returns></returns>
        public bool UpdateGovAttendance(string StaffId, string CompanyId, IList<EyouSoft.Model.GovStructure.MGovAttendance> list)
        {
            bool IsTrue = false;
            string AttendanceXML = CreateGovAttendanceXML(list);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovAttendance_Update");
            this._db.AddInParameter(dc, "StaffId", DbType.AnsiStringFixedLength, StaffId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "AttendanceXML", DbType.Xml, AttendanceXML);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                IsTrue = int.Parse(Result.ToString()) > 0 ? true : false;
            }
            return IsTrue;
        }

        /// <summary>
        /// 根据考勤编号获取单个考勤实体
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovAttendance GetGovAttendanceModel(string AttendanceId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovAttendance model = null;
            DbCommand dc = this._db.GetSqlStringCommand("SELECT AttendanceId,CompanyId,StaffId,AttendanceTime,AttendanceType,Subject,StartTime,EndTime,TimeCount,DeptId,OperatorId,IssueTime FROM tbl_GovAttendance WHERE AttendanceId=@AttendanceId AND CompanyId=@CompanyId");
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "AttendanceId", DbType.AnsiStringFixedLength, AttendanceId);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovAttendance();
                    model.AttendanceId = dr["AttendanceId"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.StaffId = dr["StaffId"].ToString();
                    model.AttendanceTime = dr.GetDateTime(dr.GetOrdinal("AttendanceTime"));
                    model.AttendanceType = (EyouSoft.Model.EnumType.GovStructure.AttendanceType)dr.GetByte(dr.GetOrdinal("AttendanceType"));
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.Subject = dr.IsDBNull(dr.GetOrdinal("Subject")) ? "" : dr["Subject"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("StartTime")))
                    {
                        model.StartTime = dr.GetDateTime(dr.GetOrdinal("StartTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        model.EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime"));
                    }
                    if (dr.IsDBNull(dr.GetOrdinal("TimeCount")) && !dr.IsDBNull(dr.GetOrdinal("StartTime")) && !dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        //if (dr.GetByte(dr.GetOrdinal("AttendanceType")) == 4 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 6 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 7 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 8)
                        //Vacation请假=4,OverTime加班=5,Travel出差=6,Rest休假=7,Suspension停职=8
                        if (model.AttendanceType.ToString() == "请假" || model.AttendanceType.ToString() == "出差" || model.AttendanceType.ToString() == "休假" || model.AttendanceType.ToString() == "停职")
                        {
                            model.TimeCount = (dr.GetDateTime(dr.GetOrdinal("EndTime")) - dr.GetDateTime(dr.GetOrdinal("StartTime"))).Days;
                        }
                        //if (dr.GetByte(dr.GetOrdinal("AttendanceType")) == 5)
                        if (model.AttendanceType.ToString() == "加班")
                        {
                            model.TimeCount = (dr.GetDateTime(dr.GetOrdinal("EndTime")) - dr.GetDateTime(dr.GetOrdinal("StartTime"))).Hours;
                        }
                    }
                    else
                    {
                        model.TimeCount = dr.IsDBNull(dr.GetOrdinal("TimeCount")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TimeCount"));
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获取某年月的考勤概况
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns>考勤概况实体</returns>
        public EyouSoft.Model.GovStructure.MAttendanceAbout GetAttendanceAbout(string CompanyId, string StaffId, int Year, int Month)
        {
            EyouSoft.Model.GovStructure.MAttendanceAbout model = null;
            DateTime AddDate = new DateTime(Year, Month, 1);
            StringBuilder StrSql = new StringBuilder();//at.[DepartmentId], 
            StrSql.Append("SELECT at.[ID] AS StaffId, at.CompanyId, at.[Name] AS [StaffName],");//Punctuality准点 = 0,Late迟到=1,LeaveEarly早退=2,Absenteeism旷工=3,Vacation请假=4,OverTime加班=5,Travel出差=6,Rest休假=7,Suspension停职=8
            StrSql.AppendFormat(" (SELECT Count(1) FROM tbl_GovAttendance d WHERE d.[StaffId] = at.[id] AND d.[AttendanceType] = 0 AND datediff(mm, '{0}', d.[AttendanceTime]) = 0) AS Punctuality,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT Count(1) FROM tbl_GovAttendance e WHERE e.[StaffId] = at.[id] AND e.[AttendanceType] = 1 AND datediff(mm, '{0}', e.[AttendanceTime]) = 0) AS Late,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT Count(1) FROM tbl_GovAttendance f WHERE f.[StaffId] = at.[id] AND f.[AttendanceType] = 2 AND datediff(mm, '{0}', f.[AttendanceTime]) = 0) AS LeaveEarly,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT Count(1) FROM tbl_GovAttendance g WHERE g.[StaffId] = at.[id] AND g.[AttendanceType] = 3 AND datediff(mm, '{0}', g.[AttendanceTime]) = 0) AS Absenteeism,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT isNull(SUM(TimeCount),0) FROM tbl_GovAttendance h WHERE h.[StaffId] = at.[id] AND h.[AttendanceType] = 4 AND datediff(mm, '{0}', h.[AttendanceTime]) = 0) AS Vacation,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT isNull(SUM(TimeCount),0) FROM tbl_GovAttendance m WHERE m.[StaffId] = at.[id] AND m.[AttendanceType] = 5 AND datediff(mm,'{0}', m.[AttendanceTime]) = 0) AS OverTime,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT isNull(SUM(TimeCount),0) FROM tbl_GovAttendance n WHERE n.[StaffId] = at.[id] AND n.[AttendanceType] = 6 AND datediff(mm,'{0}', n.[AttendanceTime]) = 0) AS Travel,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT isNull(SUM(TimeCount),0) FROM tbl_GovAttendance o WHERE o.[StaffId] = at.[id] AND o.[AttendanceType] = 7 AND datediff(mm,'{0}', o.[AttendanceTime]) = 0) AS Rest,", AddDate.ToShortDateString());
            StrSql.AppendFormat(" (SELECT isNull(SUM(TimeCount),0) FROM tbl_GovAttendance p WHERE p.[StaffId] = at.[id] AND p.[AttendanceType] = 8 AND datediff(mm,'{0}', p.[AttendanceTime]) = 0) AS Suspension", AddDate.ToShortDateString());
            StrSql.AppendFormat(" FROM tbl_GovFile AS at WHERE at.ID='{0}' AND at.CompanyId='{1}' and at.IsDelete = '0'", StaffId, CompanyId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MAttendanceAbout()
                    {
                        StaffName = dr.IsDBNull(dr.GetOrdinal("StaffName")) ? "" : dr.GetString(dr.GetOrdinal("StaffName")),
                        StaffId = dr.IsDBNull(dr.GetOrdinal("StaffId")) ? "" : dr.GetString(dr.GetOrdinal("StaffId")),
                        Absenteeism = dr.IsDBNull(dr.GetOrdinal("Absenteeism")) ? 0 : dr.GetInt32(dr.GetOrdinal("Absenteeism")),//旷工=3
                        Late = dr.IsDBNull(dr.GetOrdinal("Late")) ? 0 : dr.GetInt32(dr.GetOrdinal("Late")),//迟到=1
                        LeaveEarly = dr.IsDBNull(dr.GetOrdinal("LeaveEarly")) ? 0 : dr.GetInt32(dr.GetOrdinal("LeaveEarly")),//早退=2
                        Punctuality = dr.IsDBNull(dr.GetOrdinal("Punctuality")) ? 0 : dr.GetInt32(dr.GetOrdinal("Punctuality")),//准点 = 0
                        Vacation = dr.IsDBNull(dr.GetOrdinal("Vacation")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Vacation")),//请假=4
                        OverTime = dr.IsDBNull(dr.GetOrdinal("OverTime")) ? 0 : dr.GetDecimal(dr.GetOrdinal("OverTime")),//加班=5
                        Travel = dr.IsDBNull(dr.GetOrdinal("Travel")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Travel")),//出差=6
                        Rest = dr.IsDBNull(dr.GetOrdinal("Rest")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Rest")),//休假=7
                        Suspension = dr.IsDBNull(dr.GetOrdinal("Suspension")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Suspension"))//停职=8
                    };
                }
            };
            return model;
        }

        /// <summary>
        /// 按考勤时间获取员工考勤信息集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="AttendanceTime">考勤的时间</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendance> GetList(string CompanyId, string StaffId, DateTime AttendanceTime)
        {
            EyouSoft.Model.GovStructure.MGovAttendance model = null;
            IList<EyouSoft.Model.GovStructure.MGovAttendance> ResultList = null;
            DbCommand dc = this._db.GetSqlStringCommand("SELECT AttendanceId,CompanyId,StaffId,AttendanceTime,AttendanceType,Subject,StartTime,EndTime,TimeCount,OperatorId,IssueTime FROM tbl_GovAttendance WHERE StaffId=@StaffId AND CompanyId=@CompanyId AND datediff(dd, @AttendanceTime, AttendanceTime) = 0");
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "StaffId", DbType.AnsiStringFixedLength, StaffId);
            this._db.AddInParameter(dc, "AttendanceTime", DbType.DateTime, AttendanceTime);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovAttendance>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovAttendance();
                    model.AttendanceId = dr["AttendanceId"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.StaffId = dr["StaffId"].ToString();
                    model.AttendanceTime = dr.GetDateTime(dr.GetOrdinal("AttendanceTime"));
                    model.AttendanceType = (EyouSoft.Model.EnumType.GovStructure.AttendanceType)dr.GetByte(dr.GetOrdinal("AttendanceType"));
                    model.OperatorId = dr["OperatorId"].ToString();
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.Subject = dr.IsDBNull(dr.GetOrdinal("Subject")) ? "" : dr["Subject"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("StartTime")))
                    {
                        model.StartTime = dr.GetDateTime(dr.GetOrdinal("StartTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        model.EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime"));
                    }
                    if (dr.IsDBNull(dr.GetOrdinal("TimeCount")) && !dr.IsDBNull(dr.GetOrdinal("StartTime")) && !dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        //if (dr.GetByte(dr.GetOrdinal("AttendanceType")) == 4 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 6 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 7 || dr.GetByte(dr.GetOrdinal("AttendanceType")) == 8)
                        //Vacation请假=4,OverTime加班=5,Travel出差=6,Rest休假=7,Suspension停职=8
                        if (model.AttendanceType.ToString() == "请假" || model.AttendanceType.ToString() == "出差" || model.AttendanceType.ToString() == "休假" || model.AttendanceType.ToString() == "停职")
                        {
                            model.TimeCount = (dr.GetDateTime(dr.GetOrdinal("EndTime")) - dr.GetDateTime(dr.GetOrdinal("StartTime"))).Days;
                        }
                        //if (dr.GetByte(dr.GetOrdinal("AttendanceType")) == 5)
                        if (model.AttendanceType.ToString() == "加班")
                        {
                            model.TimeCount = (dr.GetDateTime(dr.GetOrdinal("EndTime")) - dr.GetDateTime(dr.GetOrdinal("StartTime"))).Hours;
                        }
                    }
                    else
                    {
                        model.TimeCount = dr.IsDBNull(dr.GetOrdinal("TimeCount")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TimeCount"));
                    }
                    ResultList.Add(model);
                    model = null;
                }
            }
            return ResultList;
        }

        /// <summary>
        /// 获取考勤管理集合信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FileNumber">员工号(为空时不作条件)</param>
        /// <param name="StaffName">员工名字(为空时不作条件)</param>
        /// <param name="DepartIds">部门ID集合(如：1,2,3)</param>
        /// <param name="Department">部门(为空时不作条件)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MAttendanceAbout> GetList(string FileNumber, string StaffName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MAttendanceAbout> ResultList = null;
            string tableName = "view_GovAttendance";
            string identityColumnName = "StaffId";
            //Punctuality全勤 = 0,Late迟到=1,LeaveEarly早退=2,Absenteeism旷工=3,Vacation请假=4,OverTime加班=5,Travel出差=6,Rest休假=7,Suspension停职=8
            string fields = "[StaffId],FileNumber,CompanyId,[StaffName],DepartId,DepartName,[Punctuality],[Late],[LeaveEarly],[Absenteeism],[Vacation],[OverTime],Travel,Rest,Suspension ";
            string query = string.Format(" [CompanyId]='{0}'", CompanyId);
            if (!string.IsNullOrEmpty(StaffName))
            {
                query = query + string.Format(" AND [StaffName] LIKE '%{0}%'", StaffName);
            }
            if (!string.IsNullOrEmpty(FileNumber))
            {
                query = query + string.Format(" AND [FileNumber] LIKE '%{0}%'", FileNumber);
            }
            if (!string.IsNullOrEmpty(DepartIds))
            {
                query = query + string.Format(" AND DepartId in ({0}) ", DepartIds);
            }
            if (!string.IsNullOrEmpty(Department) && string.IsNullOrEmpty(DepartIds))
            {
                query = query + string.Format(" AND [DepartName] LIKE '%{0}%'", Department);
            }
            string orderByString = " [IssueTime] DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MAttendanceAbout>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MAttendanceAbout model = new EyouSoft.Model.GovStructure.MAttendanceAbout()
                    {
                        StaffName = dr.IsDBNull(dr.GetOrdinal("StaffName")) ? "" : dr.GetString(dr.GetOrdinal("StaffName")),
                        StaffId = dr.IsDBNull(dr.GetOrdinal("StaffId")) ? "" : dr.GetString(dr.GetOrdinal("StaffId")),
                        FileNumber = dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? "" : dr.GetString(dr.GetOrdinal("FileNumber")),
                        DepartName = dr.IsDBNull(dr.GetOrdinal("DepartName")) ? "" : dr.GetString(dr.GetOrdinal("DepartName")),
                        Absenteeism = dr.IsDBNull(dr.GetOrdinal("Absenteeism")) ? 0 : dr.GetInt32(dr.GetOrdinal("Absenteeism")),
                        Late = dr.IsDBNull(dr.GetOrdinal("Late")) ? 0 : dr.GetInt32(dr.GetOrdinal("Late")),
                        LeaveEarly = dr.IsDBNull(dr.GetOrdinal("LeaveEarly")) ? 0 : dr.GetInt32(dr.GetOrdinal("LeaveEarly")),
                        Punctuality = dr.IsDBNull(dr.GetOrdinal("Punctuality")) ? 0 : dr.GetInt32(dr.GetOrdinal("Punctuality")),
                        Vacation = dr.IsDBNull(dr.GetOrdinal("Vacation")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Vacation")),
                        OverTime = dr.IsDBNull(dr.GetOrdinal("OverTime")) ? 0 : dr.GetDecimal(dr.GetOrdinal("OverTime")),
                        Travel = dr.IsDBNull(dr.GetOrdinal("Travel")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Travel")),
                        Rest = dr.IsDBNull(dr.GetOrdinal("Rest")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Rest")),
                        Suspension = dr.IsDBNull(dr.GetOrdinal("Suspension")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Suspension"))
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 按条件获取员工考勤列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendanceList> GetList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo)
        {
            IList<EyouSoft.Model.GovStructure.MGovAttendanceList> ResultList = null;
            DateTime SearchAddDate = DateTime.Now;
            if (SearchInfo != null && SearchInfo.Year != null && SearchInfo.Month != null)
            {
                SearchAddDate = new DateTime(SearchInfo.Year.Value, SearchInfo.Month.Value, 1);
            }
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT a.ID,a.Name,a.FileNumber,a.CompanyId,a.DepartId,a.IssueTime,b.DepartName,");
            StrSql.AppendFormat(" (SELECT StaffId,AttendanceType,AttendanceTime,Subject,TimeCount FROM tbl_GovAttendance WHERE StaffId=a.ID and DATEDIFF(month,AttendanceTime,'{0}')=0 FOR XML RAW,ROOT('ROOT')) AS AttendanceInfoXML", SearchAddDate.ToShortDateString());
            StrSql.AppendFormat(" FROM tbl_GovFile a LEFT JOIN  tbl_ComDepartment as b on b.DepartId=a.DepartId where a.CompanyId='{0}' and a.IsDelete = '0' ", CompanyId);
            if (SearchInfo != null)
            {
                if (!string.IsNullOrEmpty(SearchInfo.DepartIds))
                {
                    StrSql.AppendFormat(" AND a.DepartId in ({0}) ", SearchInfo.DepartIds);
                }
                if (!string.IsNullOrEmpty(SearchInfo.DepartName) && string.IsNullOrEmpty(SearchInfo.DepartIds))
                {
                    StrSql.AppendFormat(" AND b.DepartName like '%{0}%' ", SearchInfo.DepartName);
                }
                if (!string.IsNullOrEmpty(SearchInfo.ID))
                {
                    StrSql.AppendFormat(" AND a.ID='{0}' ", SearchInfo.ID);
                }
                if (!string.IsNullOrEmpty(SearchInfo.FileNumber))
                {
                    StrSql.AppendFormat(" AND a.FileNumber LIKE '%{0}%'", SearchInfo.FileNumber);
                }
                if (!string.IsNullOrEmpty(SearchInfo.StaffName))
                {
                    StrSql.AppendFormat(" AND a.Name LIKE '%{0}%'", SearchInfo.StaffName);
                }
            }
            StrSql.Append(" ORDER BY a.IssueTime DESC");
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovAttendanceList>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovAttendanceList model = new EyouSoft.Model.GovStructure.MGovAttendanceList()
                    {
                        Name = dr.IsDBNull(dr.GetOrdinal("Name")) ? "" : dr.GetString(dr.GetOrdinal("Name")),
                        DepartName = dr.IsDBNull(dr.GetOrdinal("DepartName")) ? "" : dr.GetString(dr.GetOrdinal("DepartName")),
                        ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? "" : dr.GetString(dr.GetOrdinal("ID")),
                        FileNumber = dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? "" : dr.GetString(dr.GetOrdinal("FileNumber")),
                        AttendanceList = this.GetAttendanceList(dr["AttendanceInfoXML"].ToString())
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 按部门获取考勤汇总信息集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment> GetAttendanceByDepartmentList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo)
        {
            IList<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment> ResultList = null;
            string StrSql = string.Format("SELECT DepartId,DepartName  FROM tbl_ComDepartment WHERE CompanyId='{0}' ", CompanyId);
            if (SearchInfo.DepartId != null && SearchInfo.DepartId > 0)
            {
                StrSql += string.Format(" AND DepartId={0}", SearchInfo.DepartId);
            }
            StrSql += " ORDER BY IssueTime DESC";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovAttendanceByDepartment model = new EyouSoft.Model.GovStructure.MGovAttendanceByDepartment()
                    {
                        DepartmentName = dr.IsDBNull(dr.GetOrdinal("DepartName")) ? "" : dr.GetString(dr.GetOrdinal("DepartName")),
                        DepartId = dr.IsDBNull(dr.GetOrdinal("DepartId")) ? "" : dr.GetInt32(dr.GetOrdinal("DepartId")).ToString(),
                    };
                    model.PersonList = GetPersonnelList(CompanyId, model.DepartId, SearchInfo);
                    ResultList.Add(model);
                    model = null;
                }
            };

            return ResultList;
        }

        /// <summary>
        /// 根据考勤ID删除
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <returns></returns>
        public bool DeleteGovAttendance(string AttendanceId)
        {
            DbCommand dc = this._db.GetSqlStringCommand("DELETE FROM tbl_GovAttendance WHERE AttendanceId=@AttendanceId");
            this._db.AddInParameter(dc, "AttendanceId", DbType.AnsiStringFixedLength, AttendanceId);
            return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 根据部门获取所有员工信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="DepartmentId">部门编号</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovAttendanceList> GetPersonnelList(string CompanyId, string DepartId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo)
        {
            DateTime SearchAddDate = new DateTime(SearchInfo.Year.Value, SearchInfo.Month.Value, 1);
            IList<EyouSoft.Model.GovStructure.MGovAttendanceList> ResultList = null;
            string StrSql = string.Format("SELECT ID,Name,FileNumber,(SELECT StaffId,AttendanceType,AttendanceTime FROM tbl_GovAttendance WHERE DATEDIFF(month,AttendanceTime,'{0}')=0 FOR XML RAW,ROOT('ROOT')) AS AttendanceInfoXML  FROM tbl_GovFile WHERE CompanyId='{1}' AND EXISTS(SELECT 1 FROM tbl_GovFileDept WHERE DepartId={2} AND FileId=tbl_GovAttendance.ID)", SearchAddDate.ToShortDateString(), CompanyId, DepartId);
            if (!string.IsNullOrEmpty(SearchInfo.FileNumber))
            {
                StrSql += string.Format(" AND FileNumber LIKE '%{0}%'", SearchInfo.FileNumber);
            }
            if (!string.IsNullOrEmpty(SearchInfo.StaffName))
            {
                StrSql += string.Format(" AND Name LIKE '%{0}%'", SearchInfo.StaffName);
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovAttendanceList>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovAttendanceList model = new EyouSoft.Model.GovStructure.MGovAttendanceList()
                    {
                        Name = dr.IsDBNull(dr.GetOrdinal("Name")) ? "" : dr.GetString(dr.GetOrdinal("Name")),
                        ID = dr.IsDBNull(dr.GetOrdinal("ID")) ? "" : dr.GetString(dr.GetOrdinal("ID")),
                        FileNumber = dr.IsDBNull(dr.GetOrdinal("FileNumber")) ? "" : dr.GetString(dr.GetOrdinal("FileNumber")),
                        AttendanceList = this.GetAttendanceList(dr["AttendanceInfoXML"].ToString())
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }


        /// <summary>
        /// 创建考勤信息XML
        /// </summary>
        /// <param name="Lists">考勤信息集合</param>
        /// <returns></returns>
        private string CreateGovAttendanceXML(IList<EyouSoft.Model.GovStructure.MGovAttendance> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovAttendance model in list)
            {
                StrBuild.AppendFormat("<GovAttendance AttendanceId=\"{0}\"", model.AttendanceId);
                StrBuild.AppendFormat(" CompanyId=\"{0}\" ", model.CompanyId);
                StrBuild.AppendFormat(" StaffId=\"{0}\" ", model.StaffId);
                StrBuild.AppendFormat(" AttendanceTime=\"{0}\" ", model.AttendanceTime);
                StrBuild.AppendFormat(" AttendanceType=\"{0}\" ", (int)model.AttendanceType);
                StrBuild.AppendFormat(" Subject=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Subject));
                StrBuild.AppendFormat(" StartTime=\"{0}\" ", model.StartTime);
                StrBuild.AppendFormat(" EndTime=\"{0}\" ", model.EndTime);
                StrBuild.AppendFormat(" TimeCount=\"{0}\" ", model.TimeCount);
                StrBuild.AppendFormat(" DeptId=\"{0}\" ", model.DeptId);
                StrBuild.AppendFormat(" OperatorId=\"{0}\" ", model.OperatorId);
                StrBuild.AppendFormat(" IssueTime=\"{0}\" />", model.IssueTime);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 生成部门集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComDepartment> GetDepartmentList(string DepartMentXml)
        {
            if (string.IsNullOrEmpty(DepartMentXml)) return null;
            IList<EyouSoft.Model.ComStructure.MComDepartment> ResultList = null;
            ResultList = new List<EyouSoft.Model.ComStructure.MComDepartment>();
            XElement root = XElement.Parse(DepartMentXml);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.ComStructure.MComDepartment model = new EyouSoft.Model.ComStructure.MComDepartment()
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
        /// 生成考勤信息集合List
        /// </summary>
        /// <param name="AttendanceXml">考勤信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovAttendance> GetAttendanceList(string AttendanceXml)
        {
            if (string.IsNullOrEmpty(AttendanceXml)) return null;
            IList<EyouSoft.Model.GovStructure.MGovAttendance> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovAttendance>();
            XElement root = XElement.Parse(AttendanceXml);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovAttendance model = new EyouSoft.Model.GovStructure.MGovAttendance()
                {
                    AttendanceTime = DateTime.Parse(tmp1.Attribute("AttendanceTime").Value),
                    StaffId = tmp1.Attribute("StaffId").Value,
                    Subject = tmp1.Attribute("Subject").Value,
                    TimeCount = (tmp1.Attribute("TimeCount") != null && tmp1.Attribute("TimeCount").Value.Trim() != "") ? Convert.ToDecimal(tmp1.Attribute("TimeCount").Value.Trim()) : 0,
                    AttendanceType = (EyouSoft.Model.EnumType.GovStructure.AttendanceType)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.AttendanceType), tmp1.Attribute("AttendanceType").Value)
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }
        #endregion
    }
}
