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
    /// 意见建议数据访问层
    /// 2011-09-26 邵权江 创建
    /// </summary>
    public class DOpinion : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IOpinion
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DOpinion()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
 
        #endregion

        #region 成员方法
        /// <summary>
        /// 增加一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        public bool AddGovOpinion(Model.GovStructure.MGovOpinion model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovOpinion_Add");
            this._db.AddInParameter(dc, "OpinionId", DbType.AnsiStringFixedLength, model.OpinionId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "ProcessOpinion", DbType.String, model.ProcessOpinion);
            this._db.AddInParameter(dc, "ProcessTime", DbType.DateTime, model.ProcessTime);
            this._db.AddInParameter(dc, "IsOpen", DbType.AnsiStringFixedLength, model.IsOpen == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "Submit", DbType.AnsiStringFixedLength, model.Submit);
            this._db.AddInParameter(dc, "SubmitTime", DbType.DateTime, model.SubmitTime);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "OpinionUserListXML", DbType.Xml, CreateReceiverListXML(model.MGovOpinionUserList));
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComNoticeXML(model.ComAttachList));
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
        /// 更新一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateGovOpinion(Model.GovStructure.MGovOpinion model,EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovOpinion_Update");
            this._db.AddInParameter(dc, "OpinionId", DbType.AnsiStringFixedLength, model.OpinionId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "ProcessOpinion", DbType.String, model.ProcessOpinion);
            this._db.AddInParameter(dc, "ProcessTime", DbType.DateTime, model.ProcessTime);
            this._db.AddInParameter(dc, "IsOpen", DbType.AnsiStringFixedLength, model.IsOpen == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "Submit", DbType.AnsiStringFixedLength, model.Submit);
            this._db.AddInParameter(dc, "SubmitTime", DbType.DateTime, model.SubmitTime);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "OpinionUserListXML", DbType.Xml, CreateReceiverListXML(model.MGovOpinionUserList));
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComNoticeXML(model.ComAttachList));
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
        /// 获得意见建议实体
        /// </summary>
        /// <param name="OpinionId">意见建议ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovOpinion GetGovOpinionModel(string OpinionId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovOpinion model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT OpinionId,CompanyId,Title,Content,ProcessOpinion,ProcessTime,IsOpen,Submit,SubmitTime,OperatorId,IssueTime, ");
            StrSql.Append(" (SELECT TOP 1 Name FROM tbl_GovFile WHERE ID=a.Submit )AS Name,");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.OpinionId FOR XML RAW,ROOT('ROOT'))AS ComAttachXML,", (int)ItemType);
            StrSql.Append(" (SELECT ID,Name FROM tbl_GovFile WHERE ID IN(SELECT UserId from tbl_GovOpinionUser WHERE OpinionId=a.OpinionId) FOR XML RAW,ROOT('ROOT'))AS OpinionUserXML ");
            StrSql.AppendFormat(" FROM tbl_GovOpinion a WHERE OpinionId='{0}' ", OpinionId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovOpinion();
                    model.OpinionId = dr.GetString(dr.GetOrdinal("OpinionId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    model.Content = dr.IsDBNull(dr.GetOrdinal("Content")) ? "" : dr.GetString(dr.GetOrdinal("Content"));
                    model.ProcessOpinion = dr.IsDBNull(dr.GetOrdinal("ProcessOpinion")) ? "" : dr.GetString(dr.GetOrdinal("ProcessOpinion"));
                    if(!dr.IsDBNull(dr.GetOrdinal("ProcessTime")))
                    {
                        model.ProcessTime = dr.GetDateTime(dr.GetOrdinal("ProcessTime"));
                    }
                    model.IsOpen = dr.GetString(dr.GetOrdinal("IsOpen")) == "1" ? true : false;
                    model.Submit = dr.IsDBNull(dr.GetOrdinal("Submit")) ? "" : dr.GetString(dr.GetOrdinal("Submit"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SubmitTime")))
                    {
                        model.SubmitTime = dr.GetDateTime(dr.GetOrdinal("SubmitTime"));
                    }
                    model.OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), OpinionId, ItemType);
                    model.MGovOpinionUserList = this.GetOpinionUserList(dr["OpinionUserXML"].ToString(), OpinionId);
                }
            };
            return model;
        }

        /// <summary>
        /// 根据条件意见建议信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="MSearchMeeting">查询参数类</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovOpinion> GetGovOpinionList(string CompanyId, Model.GovStructure.MSearchOpinion MSearchOpinion, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovOpinion> ResultList = null;
            string tableName = "view_GovOpinion";
            string identityColumnName = "OpinionId";
            string fields = "OpinionId,CompanyId,Title,ProcessTime,IsOpen,Submit,SubmitTime,OperatorId,IssueTime,Name,OpinionUserXML  ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (MSearchOpinion != null)
            {
                if (!string.IsNullOrEmpty(MSearchOpinion.Title))
                {
                    query = query + string.Format(" AND Title LIKE '%{0}%'", MSearchOpinion.Title);
                }
                if (!string.IsNullOrEmpty(MSearchOpinion.Submit))
                {
                    query = query + string.Format(" AND Submit = '{0}'", MSearchOpinion.Submit);
                }
                if (MSearchOpinion.SubmitTime != null)
                {
                    query = query + string.Format(" AND datediff(dd, '{0}', SubmitTime) = 0", MSearchOpinion.SubmitTime);
                }
                if (!string.IsNullOrEmpty(MSearchOpinion.OpinionUserId))
                {
                    //query = query + string.Format(" AND CAST(OpinionUserXML AS XML).exist('/ROOT/row[@ID=sql:variable(\"{0}\")]') = 1", MSearchOpinion.OpinionUserId);
                    query = query + string.Format(" AND CAST(OpinionUserXML AS XML).exist('/ROOT/row/@ID[.=\"{0}\"]') = 1", MSearchOpinion.OpinionUserId);
                }
                if (MSearchOpinion.ProcessTime != null)
                {
                    query = query + string.Format(" AND datediff(dd, '{0}', ProcessTime) = 0", MSearchOpinion.ProcessTime);
                }
                if (!string.IsNullOrEmpty(MSearchOpinion.Status))//状态1：未处理,2：已处理
                {
                    if (MSearchOpinion.Status.Equals("1"))
                    {
                        query = query + " AND ProcessTime is null ";
                    }
                    if (MSearchOpinion.Status.Equals("2"))
                    {
                        query = query + " AND ProcessTime is not null ";
                    }
                }
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovOpinion>();
                EyouSoft.Model.GovStructure.MGovOpinion model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovOpinion();
                    model.OpinionId = dr.GetString(dr.GetOrdinal("OpinionId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProcessTime")))
                    {
                        model.ProcessTime = dr.GetDateTime(dr.GetOrdinal("ProcessTime"));
                        model.Status = "2";
                    }
                    else
                    {
                        model.Status = "1";
                    }
                    model.Operator = dr.IsDBNull(dr.GetOrdinal("Name")) ? "" : dr.GetString(dr.GetOrdinal("Name"));
                    model.Submit = dr.IsDBNull(dr.GetOrdinal("Submit")) ? "" : dr.GetString(dr.GetOrdinal("Submit"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Submit")))
                    {
                        model.SubmitTime = dr.GetDateTime(dr.GetOrdinal("SubmitTime"));
                    }
                    model.OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.MGovOpinionUserList = this.GetOpinionUserList(dr["OpinionUserXML"].ToString(), dr.GetString(dr.GetOrdinal("OpinionId")));

                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据意见建议编号删除
        /// </summary>
        /// <param name="OpinionIds">意见建议ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool DeleteGovOpinion(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] OpinionIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < OpinionIds.Length; i++)
            {
                sId.AppendFormat("{0},", OpinionIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovOpinion_Delete");
            this._db.AddInParameter(dc, "OpinionIds", DbType.AnsiString, sId.ToString());
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
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
        /// 创建接收人员XML
        /// </summary>
        /// <param name="Lists">接收人员集合</param>
        /// <returns></returns>
        private string CreateReceiverListXML(IList<EyouSoft.Model.GovStructure.MGovOpinionUser> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovOpinionUser model in list)
            {
                StrBuild.AppendFormat("<GovGovOpinionUser OpinionId=\"{0}\"", model.OpinionId);
                StrBuild.AppendFormat(" UserId=\"{0}\" />", model.UserId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建规章制度附件XML
        /// </summary>
        /// <param name="Lists">附件集合</param>
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
        /// 接收集合List
        /// </summary>
        /// <param name="AttendanceXml">接收信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovOpinionUser> GetOpinionUserList(string OpinionUserXML, string OpinionId)
        {
            if (string.IsNullOrEmpty(OpinionUserXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovOpinionUser> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovOpinionUser>();
            XElement root = XElement.Parse(OpinionUserXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovOpinionUser model = new EyouSoft.Model.GovStructure.MGovOpinionUser()
                {
                    OpinionId = OpinionId,
                    UserId = tmp1.Attribute("ID").Value,
                    User = tmp1.Attribute("Name").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

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
        #endregion
    }
}
