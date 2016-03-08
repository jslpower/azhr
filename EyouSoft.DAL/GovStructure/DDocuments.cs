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
    /// 文件管理DAL 邵权江 2012-03-06
    /// </summary>
    public class DDocuments : EyouSoft.Toolkit.DAL.DALBase , EyouSoft.IDAL.GovStructure.IDocuments
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DDocuments()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名

        #endregion

        #region 成员方法

        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovDocuments_Add");
            this._db.AddInParameter(dc, "DocumentsId", DbType.AnsiStringFixedLength, model.DocumentsId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "FontSize", DbType.String, model.FontSize);
            this._db.AddInParameter(dc, "Company", DbType.String, model.Company);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "FileType", DbType.Byte, (int)model.FileType);
            this._db.AddInParameter(dc, "AttnId", DbType.AnsiStringFixedLength, model.AttnId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorID);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ApproveState", DbType.Byte, (int)model.ApproveState);
            this._db.AddInParameter(dc, "GovDocumentsApproveXML", DbType.Xml, CreateGovDocumentsApproveListXML(model.GovDocumentsApproveList));//审批人业务实体集合
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
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
        /// 修改文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovDocuments_Update");
            this._db.AddInParameter(dc, "DocumentsId", DbType.AnsiStringFixedLength, model.DocumentsId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "FontSize", DbType.String, model.FontSize);
            this._db.AddInParameter(dc, "Company", DbType.String, model.Company);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "FileType", DbType.Byte, (int)model.FileType);
            this._db.AddInParameter(dc, "AttnId", DbType.AnsiStringFixedLength, model.AttnId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorID);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ApproveState", DbType.Byte, (int)model.ApproveState);
            this._db.AddInParameter(dc, "GovDocumentsApproveXML", DbType.Xml, CreateGovDocumentsApproveListXML(model.GovDocumentsApproveList));//审批人业务实体集合
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
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
        /// 根据 文件ID 获取文件实体信息
        /// </summary>
        /// <param name="ID">文件ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovDocuments GetGovDocumentsModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovDocuments model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT DocumentsId,CompanyId,FontSize,Company,Title,FileType,AttnId,OperatorID,IssueTime,ApproveState, ");
            StrSql.Append(" (SELECT TOP 1 Name FROM tbl_GovFile WHERE ID=a.AttnId )AS AttnName, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.DocumentsId FOR XML RAW,ROOT('ROOT'))AS ComAttachXML,", (int)ItemType);
            StrSql.Append(" (SELECT DocumentsId,ApproveID,ApproveName,ApproveTime,ApprovalViews,ApproveState FROM tbl_GovDocumentsApprove WHERE DocumentsId=a.DocumentsId FOR XML RAW,ROOT('ROOT'))AS GovDocumentsApproveXML ");
            StrSql.AppendFormat(" FROM tbl_GovDocuments a WHERE a.DocumentsId='{0}' ", ID);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovDocuments();
                    model.DocumentsId = dr.GetString(dr.GetOrdinal("DocumentsId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.FontSize = dr.IsDBNull(dr.GetOrdinal("FontSize")) ? "" : dr.GetString(dr.GetOrdinal("FontSize"));
                    model.Company = dr.IsDBNull(dr.GetOrdinal("Company")) ? "" : dr.GetString(dr.GetOrdinal("Company"));
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    model.FileType = (EyouSoft.Model.EnumType.GovStructure.FileType)dr.GetByte(dr.GetOrdinal("FileType"));
                    model.AttnId = dr.IsDBNull(dr.GetOrdinal("AttnId")) ? "" : dr.GetString(dr.GetOrdinal("AttnId"));
                    model.AttnName = dr.IsDBNull(dr.GetOrdinal("AttnName")) ? "" : dr.GetString(dr.GetOrdinal("AttnName"));
                    model.OperatorID = !dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? dr.GetString(dr.GetOrdinal("OperatorID")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)dr.GetByte(dr.GetOrdinal("ApproveState"));
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), model.DocumentsId, ItemType);
                    model.GovDocumentsApproveList = this.GetGovDocumentsApproveList(dr["GovDocumentsApproveXML"].ToString());
                }
            };
            return model;
        }

        /// <summary>
        /// 修改审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovDocumentsApprove(EyouSoft.Model.GovStructure.MGovDocumentsApprove model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovDocumentsApprove_Update");
            this._db.AddInParameter(dc, "DocumentsId", DbType.AnsiStringFixedLength, model.DocumentsId);
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
        /// 删除文件信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool DeleteGovDocuments(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovDocuments_Delete");
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
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
        /// 获取文件信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FontSize">文件字号</param>
        /// <param name="Company">发布单位</param>
        /// <param name="Title">标题</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovDocuments> GetGovDocumentsList(string CompanyId, string FontSize, string Company, string Title, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovDocuments> ResultList = null;
            string tableName = "view_GovDocuments";
            string identityColumnName = "Id";
            string fields = "DocumentsId,CompanyId,FontSize,Company,Title,FileType,AttnId,OperatorID,IssueTime,ApproveState,AttnName,ComAttachXML,GovDocumentsApproveXML  ";
            string query = string.Format(" CompanyId='{0}' ", CompanyId);
            if (!string.IsNullOrEmpty(FontSize))
            {
                query = query + string.Format(" AND FontSize LIKE '%{0}%'", FontSize);
            }
            if (!string.IsNullOrEmpty(Company))
            {
                query = query + string.Format(" AND Company LIKE '%{0}%'", Company);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                query = query + string.Format(" AND Title LIKE '%{0}%'", Title);
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovDocuments>();
                EyouSoft.Model.GovStructure.MGovDocuments model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovDocuments();
                    model.DocumentsId = dr.GetString(dr.GetOrdinal("DocumentsId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.FontSize = dr.IsDBNull(dr.GetOrdinal("FontSize")) ? "" : dr.GetString(dr.GetOrdinal("FontSize"));
                    model.Company = dr.IsDBNull(dr.GetOrdinal("Company")) ? "" : dr.GetString(dr.GetOrdinal("Company"));
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    model.FileType = (EyouSoft.Model.EnumType.GovStructure.FileType)dr.GetByte(dr.GetOrdinal("FileType"));
                    model.AttnId = dr.IsDBNull(dr.GetOrdinal("AttnId")) ? "" : dr.GetString(dr.GetOrdinal("AttnId"));
                    model.AttnName = dr.IsDBNull(dr.GetOrdinal("AttnName")) ? "" : dr.GetString(dr.GetOrdinal("AttnName"));
                    model.OperatorID = !dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? dr.GetString(dr.GetOrdinal("OperatorID")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }
                    model.ApproveState = (EyouSoft.Model.EnumType.GovStructure.ApprovalStatus)dr.GetByte(dr.GetOrdinal("ApproveState"));
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), model.DocumentsId, ItemType);
                    model.GovDocumentsApproveList = this.GetGovDocumentsApproveList(dr["GovDocumentsApproveXML"].ToString());
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 创建文件附件XML
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
                StrBuild.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Name));
                StrBuild.AppendFormat(" FilePath=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.FilePath));
                StrBuild.AppendFormat(" Size=\"{0}\" ", (int)model.Size);
                StrBuild.AppendFormat(" Downloads=\"{0}\" />", model.Downloads);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建审批人XML
        /// </summary>
        /// <param name="Lists">审批人集合</param>
        /// <returns></returns>
        private string CreateGovDocumentsApproveListXML(IList<EyouSoft.Model.GovStructure.MGovDocumentsApprove> list)
        {
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovDocumentsApprove model in list)
            {
                StrBuild.AppendFormat("<GovDocumentsApprove DocumentsId=\"{0}\"", model.DocumentsId);
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
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="NoticeId">通知编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetAttachList(string ComAttachXML, string RegId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
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
                    ItemId = RegId,
                    ItemType = ItemType
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成审批人集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovDocumentsApprove> GetGovDocumentsApproveList(string GovDocumentsApproveXML)
        {
            if (string.IsNullOrEmpty(GovDocumentsApproveXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovDocumentsApprove> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovDocumentsApprove>();
            XElement root = XElement.Parse(GovDocumentsApproveXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovDocumentsApprove model = new EyouSoft.Model.GovStructure.MGovDocumentsApprove();
                model.DocumentsId = tmp1.Attribute("DocumentsId").Value;
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
