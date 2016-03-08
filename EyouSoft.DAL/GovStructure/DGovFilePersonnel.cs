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
    /// 离职申请信息DAL 邵权江 2011-9-26
    /// </summary>
    public class DGovFilePersonnel:EyouSoft.Toolkit.DAL.DALBase,EyouSoft.IDAL.GovStructure.IGovFilePersonnel
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DGovFilePersonnel()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
 
        #endregion

        #region IGovFilePersonnel 成员

        /// <summary>
        /// 添加离职信息/人事变动信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovFilePersonnel_Add");
            this._db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._db.AddInParameter(dc, "FileId", DbType.AnsiStringFixedLength, model.FileId);
            this._db.AddInParameter(dc, "Reason", DbType.String, model.Reason);
            this._db.AddInParameter(dc, "ApplicationTime", DbType.DateTime, model.ApplicationTime);
            this._db.AddInParameter(dc, "TransitionContent", DbType.String, model.TransitionContent);
            this._db.AddInParameter(dc, "TransitionState", DbType.Byte, (int)model.TransitionState);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorID);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "IsLeft", DbType.Byte, (int)model.StaffStatus);
            this._db.AddInParameter(dc, "ApproveState", DbType.Byte, (int)model.ApproveState);
            this._db.AddInParameter(dc, "DepartureTime", DbType.DateTime, model.DepartureTime);
            this._db.AddInParameter(dc, "GovPersonnelApproveXML", DbType.Xml, CreateGovPersonnelApproveListXML(model.GovPersonnelApproveList));//审批人业务实体集合
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
        /// 修改离职信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGovFilePersonnel(EyouSoft.Model.GovStructure.MGovFilePersonnel model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovFilePersonnel_Update");
            this._db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._db.AddInParameter(dc, "FileId", DbType.AnsiStringFixedLength, model.FileId);
            this._db.AddInParameter(dc, "Reason", DbType.String, model.Reason);
            this._db.AddInParameter(dc, "ApplicationTime", DbType.DateTime, model.ApplicationTime);
            this._db.AddInParameter(dc, "TransitionContent", DbType.String, model.TransitionContent);
            this._db.AddInParameter(dc, "TransitionState", DbType.Byte, (int)model.TransitionState);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorID);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "IsLeft", DbType.Byte, (int)model.StaffStatus);
            this._db.AddInParameter(dc, "ApproveState", DbType.Byte, (int)model.ApproveState);
            this._db.AddInParameter(dc, "DepartureTime", DbType.DateTime, model.DepartureTime);
            this._db.AddInParameter(dc, "GovPersonnelApproveXML", DbType.Xml, CreateGovPersonnelApproveListXML(model.GovPersonnelApproveList));//审批人业务实体集合
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
        /// 根据 离职ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">离职ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModel(string ID)
        {
            EyouSoft.Model.GovStructure.MGovFilePersonnel model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT Id,FileId,Reason,ApplicationTime,OperatorID,IssueTime,DepartureTime,IsLeft,ApproveState, ");
            StrSql.Append(" (SELECT TOP 1 Name FROM tbl_GovFile WHERE ID=a.FileId )AS FileName, ");
            StrSql.Append(" (SELECT Id,ApproveID,ApproveName,ApproveTime,ApprovalViews,ApproveState FROM tbl_GovPersonnelApprove WHERE Id=a.Id FOR XML RAW,ROOT('ROOT'))AS GovPersonnelApproveXML ");
            StrSql.AppendFormat(" FROM tbl_GovFilePersonnel a WHERE a.Id='{0}' ", ID);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFilePersonnel();
                    model.Id = dr.GetString(dr.GetOrdinal("Id"));
                    model.FileId = dr.GetString(dr.GetOrdinal("FileId"));
                    model.FileName = dr.IsDBNull(dr.GetOrdinal("FileName")) ? "" : dr.GetString(dr.GetOrdinal("FileName"));
                    model.Reason = dr.IsDBNull(dr.GetOrdinal("Reason")) ? "" : dr.GetString(dr.GetOrdinal("Reason"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ApplicationTime")))
                    {
                        model.ApplicationTime = dr.GetDateTime(dr.GetOrdinal("ApplicationTime"));
                    }
                    model.OperatorID = dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? "" : dr.GetString(dr.GetOrdinal("OperatorID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("DepartureTime")))
                    {
                        model.DepartureTime = dr.GetDateTime(dr.GetOrdinal("DepartureTime"));
                    }
                    model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)dr.GetByte(dr.GetOrdinal("ApproveState"));
                    model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("IsLeft")).ToString());
                    model.GovPersonnelApproveList = this.GetPersonnelApproveList(dr["GovPersonnelApproveXML"].ToString());
                }
            };
            return model;
        }

        /// <summary>
        /// 根据 档案ID 获取离职申请实体信息
        /// </summary>
        /// <param name="ID">档案ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovFilePersonnel GetGovFilePersonnelModelByFileId(string ID)
        {
            EyouSoft.Model.GovStructure.MGovFilePersonnel model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT Id,FileId,Reason,ApplicationTime,OperatorID,IssueTime,DepartureTime,IsLeft,ApproveState, ");
            StrSql.Append(" (SELECT TOP 1 Name FROM tbl_GovFile WHERE ID=a.FileId )AS FileName, ");
            StrSql.Append(" (SELECT Id,ApproveID,ApproveName,ApproveTime,ApprovalViews,ApproveState FROM tbl_GovPersonnelApprove WHERE Id=a.Id FOR XML RAW,ROOT('ROOT'))AS GovPersonnelApproveXML ");
            StrSql.AppendFormat(" FROM tbl_GovFilePersonnel a WHERE a.FileId='{0}' ", ID);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFilePersonnel();
                    model.Id = dr.GetString(dr.GetOrdinal("Id"));
                    model.FileId = dr.GetString(dr.GetOrdinal("FileId"));
                    model.FileName = dr.IsDBNull(dr.GetOrdinal("FileName")) ? "" : dr.GetString(dr.GetOrdinal("FileName"));
                    model.Reason = dr.IsDBNull(dr.GetOrdinal("Reason")) ? "" : dr.GetString(dr.GetOrdinal("Reason"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ApplicationTime")))
                    {
                        model.ApplicationTime = dr.GetDateTime(dr.GetOrdinal("ApplicationTime"));
                    }
                    model.OperatorID = dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? "" : dr.GetString(dr.GetOrdinal("OperatorID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("DepartureTime")))
                    {
                        model.DepartureTime = dr.GetDateTime(dr.GetOrdinal("DepartureTime"));
                    }
                    model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)dr.GetByte(dr.GetOrdinal("ApproveState"));
                    model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("IsLeft")).ToString());
                    model.GovPersonnelApproveList = this.GetPersonnelApproveList(dr["GovPersonnelApproveXML"].ToString());
                }
            };
            return model;
        }

        /// <summary>
        /// 修改离职审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovFilePersonnel(EyouSoft.Model.GovStructure.MGovPersonnelApprove model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovFilePersonnelApprove_Update");
            this._db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._db.AddInParameter(dc, "ApproveID", DbType.AnsiStringFixedLength, model.ApproveID);
            this._db.AddInParameter(dc, "ApproveTime", DbType.DateTime, model.ApproveTime);
            this._db.AddInParameter(dc, "ApprovalViews", DbType.String, model.ApprovalViews);
            this._db.AddInParameter(dc, "ApproveState", DbType.Byte, (int)model.ApproveState);
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
        /// 更新离职状态
        /// </summary>
        /// <param name="DepartureTime">离职时间</param>
        /// <param name="Id">人事编号</param>
        /// <returns></returns>
        public bool UpdateIsLeft(DateTime DepartureTime,string Id)
        {
            StringBuilder sId = new StringBuilder();
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovFilePersonnel_IsLeft");
            this._db.AddInParameter(dc, "DepartureTime", DbType.DateTime, DepartureTime);
            this._db.AddInParameter(dc, "Id", DbType.AnsiString, Id);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString()) == 0 ? false : true;
            }
            return false;
        }

        /// <summary>
        /// 删除离职信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <returns></returns>
        public bool DeleteGovFilePersonnel(params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovFilePersonnel_Delete");
            this._db.AddInParameter(dc, "IDs", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString()) == 0 ? false : true;
            }
            return false;
        }

        /// <summary>
        /// 获取离职信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">员工姓名</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> GetGovFilePersonnelList(string CompanyId, string Name, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovFilePersonnel> ResultList = null;
            string tableName = "view_GovFilePersonnel";
            string identityColumnName = "Id";
            string fields = "Id,FileId,IssueTime,ApproveState,OperatorID,FileName,IsLeft,GovPersonnelApproveXML  ";
            string query = string.Format(" FileId IN(select ID from tbl_GovFile where CompanyId='{0}')", CompanyId);
            if (!string.IsNullOrEmpty(Name))
            {
                query = query + string.Format(" AND FileName LIKE '%{0}%'", Name);
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovFilePersonnel>();
                EyouSoft.Model.GovStructure.MGovFilePersonnel model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovFilePersonnel();
                    model.Id = dr.GetString(dr.GetOrdinal("Id"));
                    model.FileId = dr.GetString(dr.GetOrdinal("FileId"));
                    model.FileName = dr.IsDBNull(dr.GetOrdinal("FileName")) ? "" : dr.GetString(dr.GetOrdinal("FileName"));
                    model.OperatorID = dr.GetString(dr.GetOrdinal("OperatorID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)dr.GetByte(dr.GetOrdinal("ApproveState"));
                    model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)dr.GetByte(dr.GetOrdinal("IsLeft"));
                    model.GovPersonnelApproveList = this.GetPersonnelApproveList(dr["GovPersonnelApproveXML"].ToString());
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 创建审批人XML
        /// </summary>
        /// <param name="Lists">审批人集合</param>
        /// <returns></returns>
        private string CreateGovPersonnelApproveListXML(IList<EyouSoft.Model.GovStructure.MGovPersonnelApprove> list)
        {
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovPersonnelApprove model in list)
            {
                StrBuild.AppendFormat("<GovPersonnelApprove Id=\"{0}\"", model.Id);
                StrBuild.AppendFormat(" ApproveID=\"{0}\" ", model.ApproveID);
                StrBuild.AppendFormat(" ApproveName=\"{0}\" ", model.ApproveName);
                StrBuild.AppendFormat(" ApproveTime=\"{0}\" ", model.ApproveTime);
                StrBuild.AppendFormat(" ApprovalViews=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.ApprovalViews));
                StrBuild.AppendFormat(" ApproveState=\"{0}\" />", (int)model.ApproveState);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 生成审批人集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovPersonnelApprove> GetPersonnelApproveList(string GovPersonnelApproveXML)
        {
            if (string.IsNullOrEmpty(GovPersonnelApproveXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovPersonnelApprove> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovPersonnelApprove>();
            XElement root = XElement.Parse(GovPersonnelApproveXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovPersonnelApprove model = new EyouSoft.Model.GovStructure.MGovPersonnelApprove();
                model.Id = tmp1.Attribute("Id").Value;
                model.ApproveID = tmp1.Attribute("ApproveID").Value;
                model.ApproveName = tmp1.Attribute("ApproveName").Value;
                if (tmp1.Attribute("ApproveTime") != null && tmp1.Attribute("ApproveTime").Value.Trim() != "")
                {
                    model.ApproveTime = Convert.ToDateTime(tmp1.Attribute("ApproveTime").Value);
                }
                model.ApprovalViews = tmp1.Attribute("ApprovalViews").Value;
                model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.ApprovalStatus), tmp1.Attribute("ApproveState").Value);
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        #endregion
    }
}
