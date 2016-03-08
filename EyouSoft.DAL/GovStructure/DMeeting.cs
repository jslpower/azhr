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
    /// <summary>
    /// 会议管理数据访问层
    /// 2011-09-22 邵权江 创建
    /// </summary>
    public class DMeeting:EyouSoft.Toolkit.DAL.DALBase,EyouSoft.IDAL.GovStructure.IMeeting
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DMeeting()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
 
        #endregion

        #region 成员方法
        /// <summary>
        /// 判断会议管理编号是否存在
        /// </summary>
        /// <param name="Number">会议管理编号</param>
        /// <param name="MeetingId">会议管理Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsNumber(string Number, string MeetingId, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovMeeting WHERE CompanyId=@CompanyId AND Number=@Number ";
            if (MeetingId != "")
            {
                StrSql += " AND MeetingId<>'@MeetingId'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (MeetingId != "")
            {
                this._db.AddInParameter(dc, "MeetingId", DbType.AnsiStringFixedLength, MeetingId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "Number", DbType.String, Number);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 增加一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        public bool AddGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovMeeting_Add");
            this._db.AddInParameter(dc, "MeetingId", DbType.AnsiStringFixedLength, model.MeetingId);
            this._db.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._db.AddInParameter(dc, "Number", DbType.String, model.Number);
            this._db.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._db.AddInParameter(dc, "Theme", DbType.String, model.Theme);
            this._db.AddInParameter(dc, "MeetingStaff", DbType.String, model.MeetingStaff);
            this._db.AddInParameter(dc, "StartTime", DbType.DateTime, model.StartTime);
            this._db.AddInParameter(dc, "EndTime", DbType.DateTime, model.EndTime);
            this._db.AddInParameter(dc, "Venue", DbType.String, model.Venue);
            this._db.AddInParameter(dc, "Minutes", DbType.String, model.Minutes);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            //this._db.AddInParameter(dc, "MeetingStaffListXML", DbType.Xml, CreateMeetingStaffListXML(model.MGovMeetingStaff));
            this._db.AddInParameter(dc, "MeetingStaffListXML", DbType.Xml, null);
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
        /// 更新一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        public bool UpdateGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model)
        {

            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovMeeting_Update");
            this._db.AddInParameter(dc, "MeetingId", DbType.AnsiStringFixedLength, model.MeetingId);
            this._db.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, model.CompanyID);
            this._db.AddInParameter(dc, "Number", DbType.String, model.Number);
            this._db.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._db.AddInParameter(dc, "Theme", DbType.String, model.Theme);
            this._db.AddInParameter(dc, "MeetingStaff", DbType.String, model.MeetingStaff);
            this._db.AddInParameter(dc, "StartTime", DbType.DateTime, model.StartTime);
            this._db.AddInParameter(dc, "EndTime", DbType.DateTime, model.EndTime);
            this._db.AddInParameter(dc, "Venue", DbType.String, model.Venue);
            this._db.AddInParameter(dc, "Minutes", DbType.String, model.Minutes);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            //this._db.AddInParameter(dc, "MeetingStaffListXML", DbType.Xml, CreateMeetingStaffListXML(model.MGovMeetingStaff));
            this._db.AddInParameter(dc, "MeetingStaffListXML", DbType.Xml, null);
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
        /// 获得会议管理实体
        /// </summary>
        /// <param name="MeetingId">会议管理ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovMeeting GetGovMeetingModel(string MeetingId)
        {
            EyouSoft.Model.GovStructure.MGovMeeting model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT MeetingId,CompanyID,Number,Category,Theme,MeetingStaff,StartTime,EndTime,Venue,Minutes,OperatorId,IssueTime ");
            //StrSql.Append(" (SELECT ID,Name FROM tbl_GovFile WHERE ID IN(SELECT AcceptTypeID from tbl_GovMeetingStaff WHERE MeetingId=a.MeetingId) FOR XML RAW,ROOT('ROOT'))AS GovMeetingStaff ");
            StrSql.AppendFormat(" FROM tbl_GovMeeting a WHERE MeetingId='{0}' ", MeetingId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovMeeting()
                    {
                        MeetingId = dr.GetString(dr.GetOrdinal("MeetingId")),
                        CompanyID = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Number = dr.IsDBNull(dr.GetOrdinal("Number")) ? "" : dr.GetString(dr.GetOrdinal("Number")),
                        //Category = (EyouSoft.Model.EnumType.GovStructure.Category)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Category), dr.GetString(dr.GetOrdinal("Category"))),
                        Category = (EyouSoft.Model.EnumType.GovStructure.Category)dr.GetByte(dr.GetOrdinal("Category")),
                        Theme = dr.IsDBNull(dr.GetOrdinal("Theme")) ? "" : dr.GetString(dr.GetOrdinal("Theme")),
                        StartTime = dr.GetDateTime(dr.GetOrdinal("StartTime")),
                        EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime")),
                        Venue = dr.IsDBNull(dr.GetOrdinal("Venue")) ? "" : dr.GetString(dr.GetOrdinal("Venue")),
                        Minutes = dr.IsDBNull(dr.GetOrdinal("Minutes")) ? "" : dr.GetString(dr.GetOrdinal("Minutes")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        //MGovMeetingStaff = this.GetMeetingStaffListXML(dr["GovMeetingStaff"].ToString(), dr.GetString(dr.GetOrdinal("MeetingId")))
                        MeetingStaff = dr.IsDBNull(dr.GetOrdinal("MeetingStaff")) ? "" : dr.GetString(dr.GetOrdinal("MeetingStaff"))
                    };
                }
            };
            return model;
        }

        /// <summary>
        /// 获得会议管理信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="MSearchMeeting">会议查询实体</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovMeeting> GetGovMeetingList(string CompanyId, EyouSoft.Model.GovStructure.MSearchMeeting MSearchMeeting, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovMeeting> ResultList = null;
            //string tableName = "view_GovMeeting";
            //string identityColumnName = "MeetingId";
            //string fields = "MeetingId,CompanyID,Number,Category,Theme,StartTime,EndTime,Venue,Minutes,OperatorId,IssueTime,GovMeetingStaff  ";
            string tableName = "tbl_GovMeeting";
            string identityColumnName = "MeetingId";
            string fields = "MeetingId,CompanyID,Number,Category,Theme,MeetingStaff,StartTime,EndTime,Venue,Minutes,OperatorId,IssueTime  ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (MSearchMeeting != null)
            {
                if (!string.IsNullOrEmpty(MSearchMeeting.Number))
                {
                    query = query + string.Format(" AND [Number] LIKE '%{0}%'", MSearchMeeting.Number);
                }
                if (!string.IsNullOrEmpty(MSearchMeeting.Theme))
                {
                    query = query + string.Format(" AND [Theme] LIKE '%{0}%'", MSearchMeeting.Theme);
                }
                if (MSearchMeeting.StartTime!=null)
                {
                    query = query + string.Format(" AND StartTime >='{0}' ", MSearchMeeting.StartTime);
                }
                if (MSearchMeeting.EndTime != null)
                {
                    query = query + string.Format(" AND EndTime <='{0}'  ", MSearchMeeting.EndTime);
                }
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovMeeting>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovMeeting model = new EyouSoft.Model.GovStructure.MGovMeeting()
                    {
                        MeetingId = dr.GetString(dr.GetOrdinal("MeetingId")),
                        CompanyID = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Number = dr.IsDBNull(dr.GetOrdinal("Number")) ? "" : dr.GetString(dr.GetOrdinal("Number")),
                        //Category = (EyouSoft.Model.EnumType.GovStructure.Category)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Category), dr.GetByte(dr.GetOrdinal("Category"))),
                        Category = (EyouSoft.Model.EnumType.GovStructure.Category)dr.GetByte(dr.GetOrdinal("Category")),
                        Theme = dr.IsDBNull(dr.GetOrdinal("Theme")) ? "" : dr.GetString(dr.GetOrdinal("Theme")),
                        StartTime = dr.GetDateTime(dr.GetOrdinal("StartTime")),
                        EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime")),
                        Venue = dr.IsDBNull(dr.GetOrdinal("Venue")) ? "" : dr.GetString(dr.GetOrdinal("Venue")),
                        Minutes = dr.IsDBNull(dr.GetOrdinal("Minutes")) ? "" : dr.GetString(dr.GetOrdinal("Minutes")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        //MGovMeetingStaff = this.GetMeetingStaffListXML(dr["GovMeetingStaff"].ToString(), dr.GetString(dr.GetOrdinal("MeetingId")))
                        MeetingStaff = dr.IsDBNull(dr.GetOrdinal("MeetingStaff")) ? "" : dr.GetString(dr.GetOrdinal("MeetingStaff"))
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据会议管理ID删除
        /// </summary>
        /// <param name="MeetingIds">会议管理ID</param>
        /// <returns></returns>
        public bool DeleteGovMeeting(params string[] MeetingIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < MeetingIds.Length; i++)
            {
                sId.AppendFormat("{0},", MeetingIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovMeeting_Delete");
            this._db.AddInParameter(dc, "MeetingIds", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString()) == 0 ? false : true;
            }
            return false;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 参与人员人员XML
        /// </summary>
        /// <param name="Lists">参与人员人员集合</param>
        /// <returns></returns>
        private string CreateMeetingStaffListXML(IList<EyouSoft.Model.GovStructure.MGovMeetingStaff> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovMeetingStaff model in list)
            {
                StrBuild.AppendFormat("<GovMeetingStaff MeetingId=\"{0}\"", model.MeetingId);
                StrBuild.AppendFormat(" AcceptTypeID=\"{0}\" />", Utils.ReplaceXmlSpecialCharacter(model.AcceptTypeID));
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        ///  参与人员人员List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <param name="MeetingId">会议ID</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovMeetingStaff> GetMeetingStaffListXML(string MeetingStaffXml, string MeetingId)
        {
            if (string.IsNullOrEmpty(MeetingStaffXml)) return null;
            IList<EyouSoft.Model.GovStructure.MGovMeetingStaff> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovMeetingStaff>();
            XElement root = XElement.Parse(MeetingStaffXml);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovMeetingStaff model = new EyouSoft.Model.GovStructure.MGovMeetingStaff()
                {
                    MeetingId=MeetingId,
                    AcceptTypeID = tmp1.Attribute("ID").Value,
                    AcceptType = tmp1.Attribute("Name").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }
        #endregion
    }
}
