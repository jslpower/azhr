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
    /// 通知公告数据访问层
    /// 2011-09-21 邵权江 创建
    /// </summary>
    public class DNotice : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.INotice
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DNotice()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名

        #endregion

        #region 成员方法
        /// <summary>
        /// 增加一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        public bool AddGovNotice(Model.GovStructure.MGovNotice model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovNotice_Add");
            this._db.AddInParameter(dc, "NoticeId", DbType.AnsiStringFixedLength, model.NoticeId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "IsRemind", DbType.AnsiStringFixedLength, model.IsRemind == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "IsMsg", DbType.AnsiStringFixedLength, model.IsMsg == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "MsgContent", DbType.AnsiString, model.MsgContent);
            this._db.AddInParameter(dc, "Views", DbType.Int32, model.Views);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "Operator", DbType.String, model.Operator);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ReceiverListXML", DbType.Xml, CreateReceiverListXML(model.MGovNoticeReceiverList));
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
        /// 更新一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateGovNotice(Model.GovStructure.MGovNotice model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovNotice_Update");
            this._db.AddInParameter(dc, "NoticeId", DbType.AnsiStringFixedLength, model.NoticeId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "IsRemind", DbType.AnsiStringFixedLength, model.IsRemind == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "IsMsg", DbType.AnsiStringFixedLength, model.IsMsg == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "MsgContent", DbType.AnsiString, model.MsgContent);
            this._db.AddInParameter(dc, "Views", DbType.Int32, model.Views);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "Operator", DbType.String, model.Operator);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "ReceiverListXML", DbType.Xml, CreateReceiverListXML(model.MGovNoticeReceiverList));
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
        /// 获得公告通知实体
        /// </summary>
        /// <param name="NoticeId">公告通知ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovNotice GetGovNoticeModel(string NoticeId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovNotice model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT NoticeId,CompanyId,Title,[Content],IsRemind,IsMsg,MsgContent,DepartId,[Views],Operator,OperatorId,IssueTime, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.NoticeId FOR XML RAW,ROOT('ROOT'))AS ComAttachXML,", (int)ItemType);
            StrSql.Append(" (SELECT ItemType,ItemId FROM tbl_GovNoticeReceiver WHERE NoticeId=a.NoticeId FOR XML RAW,ROOT('ROOT'))AS NoticeReceiverXML ");
            StrSql.AppendFormat(" FROM tbl_GovNotice a WHERE NoticeId='{0}' ", NoticeId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovNotice()
                    {
                        NoticeId = dr.GetString(dr.GetOrdinal("NoticeId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title")),
                        Content = dr.IsDBNull(dr.GetOrdinal("Content")) ? "" : dr.GetString(dr.GetOrdinal("Content")),
                        IsRemind = dr.GetString(dr.GetOrdinal("IsRemind")) == "1" ? true : false,
                        IsMsg = dr.GetString(dr.GetOrdinal("IsMsg")) == "1" ? true : false,
                        MsgContent = dr.IsDBNull(dr.GetOrdinal("MsgContent")) ? "" : dr.GetString(dr.GetOrdinal("MsgContent")),
                        Views = dr.GetInt32(dr.GetOrdinal("Views")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = dr.GetString(dr.GetOrdinal("Operator")),
                        ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), NoticeId, ItemType),
                        MGovNoticeReceiverList = this.GetNoticeReceiveList(dr["NoticeReceiverXML"].ToString(), NoticeId)
                    };
                }
            };
            return model;
        }

        /// <summary>
        /// 获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Title">标题</param>
        /// <param name="OperatorId">发布人ID</param>
        /// <param name="Operator">发布人</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId, string Title, string OperatorId, string Operator, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovNotice> ResultList = null;
            string tableName = "view_GovNotice";
            string identityColumnName = "NoticeId";
            string fields = "NoticeId,CompanyId,Title,ComAttachXML,IsRemind,[Views],DepartId,Operator,OperatorId,IssueTime ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (!string.IsNullOrEmpty(Title))
            {
                query = query + string.Format(" and Title like '%{0}%'", Title);
            }
            if (!string.IsNullOrEmpty(OperatorId))
            {
                query = query + string.Format(" and OperatorId = '{0}'", OperatorId);
            }
            if (!string.IsNullOrEmpty(Operator))
            {
                query = query + string.Format(" and Operator  like  '%{0}%'", Operator);
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovNotice>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovNotice model = new EyouSoft.Model.GovStructure.MGovNotice()
                    {
                        NoticeId = dr.GetString(dr.GetOrdinal("NoticeId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title")),
                        IsRemind = dr.GetString(dr.GetOrdinal("IsRemind")) == "1" ? true : false,
                        Views = dr.IsDBNull(dr.GetOrdinal("Views")) ? 0 : dr.GetInt32(dr.GetOrdinal("Views")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = dr.IsDBNull(dr.GetOrdinal("Operator")) ? "" : dr.GetString(dr.GetOrdinal("Operator")),
                        //MGovNoticeReceiverList = this.GetNoticeReceiveList(dr["NoticeReceiverXML"].ToString()),
                        ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("NoticeId")), ItemType)
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据接收类型获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">接收类型</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId, EyouSoft.Model.EnumType.GovStructure.ItemType Type, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovNotice> ResultList = null;
            string tableName = "view_GovNotice";
            string identityColumnName = "NoticeId";
            string fields = "NoticeId,CompanyId,Title,IsRemind,[Views],DepartId,Operator,OperatorId,IssueTime ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            query = query + string.Format(" AND CAST(NoticeReceiverXML AS XML).exist('/ROOT/row/@ItemType[.=\"{0}\"]') = 1", (int)Type);
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovNotice>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovNotice model = new EyouSoft.Model.GovStructure.MGovNotice()
                    {
                        NoticeId = dr.GetString(dr.GetOrdinal("NoticeId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title")),
                        //Content = dr.IsDBNull(dr.GetOrdinal("Content")) ? "" : dr.GetString(dr.GetOrdinal("Content")),
                        IsRemind = dr.GetString(dr.GetOrdinal("IsRemind")) == "1" ? true : false,
                        Views = dr.IsDBNull(dr.GetOrdinal("Views")) ? 0 : dr.GetInt32(dr.GetOrdinal("Views")),
                        OperatorId = dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = dr.IsDBNull(dr.GetOrdinal("Operator")) ? "" : dr.GetString(dr.GetOrdinal("Operator")),
                        //MGovNoticeReceiverList = this.GetNoticeReceiveList(dr["NoticeReceiverXML"].ToString()),
                        //ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("NoticeId")), ItemType)
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 增加一条浏览人信息
        /// </summary>
        /// <param name="model">浏览人model</param>
        public bool AddGovNoticeBrowse(Model.GovStructure.MGovNoticeBrowse model)
        {
            string StrSql = "INSERT INTO tbl_GovNoticeBrowse([NoticeId],[OperatorId],[IssueTime]) VALUES(@NoticeId,@OperatorId,@IssueTime)";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "NoticeId", DbType.AnsiStringFixedLength, model.NoticeId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获得公告通知浏览人信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="NoticeId">公告通知编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNoticeBrowse> GetGovNoticeBrowseList(string CompanyId, string NoticeId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovNoticeBrowse> ResultList = null;
            string tableName = "view_GovNoticeBrowse";
            string identityColumnName = "NoticeId";
            string fields = "NoticeId,OperatorId,IssueTime,Name,DepartName ";
            string query = string.Format(" NoticeId='{0}'", NoticeId);
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovNoticeBrowse>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovNoticeBrowse model = new EyouSoft.Model.GovStructure.MGovNoticeBrowse()
                    {
                        NoticeId = dr.GetString(dr.GetOrdinal("NoticeId")),
                        OperatorId = dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = dr.IsDBNull(dr.GetOrdinal("Name")) ? "" : dr.GetString(dr.GetOrdinal("Name")),
                        DepartName = dr.IsDBNull(dr.GetOrdinal("DepartName")) ? "" : dr.GetString(dr.GetOrdinal("DepartName")),
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据公告通知编号删除
        /// </summary>
        /// <param name="NoticeIds">公告通知ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool DeleteGovNotice(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] NoticeIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < NoticeIds.Length; i++)
            {
                sId.AppendFormat("{0},", NoticeIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovNotice_Delete");
            this._db.AddInParameter(dc, "NoticeIds", DbType.AnsiString, sId.ToString());
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString()) == 1 ? true : false;
            }
            return false;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 创建接收人员XML
        /// </summary>
        /// <param name="list">接收人员集合</param>
        /// <returns></returns>
        private string CreateReceiverListXML(IList<EyouSoft.Model.GovStructure.MGovNoticeReceiver> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovNoticeReceiver model in list)
            {
                StrBuild.AppendFormat("<GovNoticeReceiver NoticeId=\"{0}\"", model.NoticeId);
                StrBuild.AppendFormat(" ItemType=\"{0}\" ", (int)model.ItemType);
                StrBuild.AppendFormat(" ItemId=\"{0}\" />", model.ItemId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建规章制度附件XML
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
        /// 接收集合List
        /// </summary>
        /// <param name="NoticeReceiverXML">接收信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovNoticeReceiver> GetNoticeReceiveList(string NoticeReceiverXML, string NoticeId)
        {
            if (string.IsNullOrEmpty(NoticeReceiverXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovNoticeReceiver> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovNoticeReceiver>();
            XElement root = XElement.Parse(NoticeReceiverXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovNoticeReceiver model = new EyouSoft.Model.GovStructure.MGovNoticeReceiver()
                {
                    NoticeId = NoticeId,
                    ItemType = (EyouSoft.Model.EnumType.GovStructure.ItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.ItemType), tmp1.Attribute("ItemType").Value),
                    ItemId = tmp1.Attribute("ItemId").Value
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
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="NoticeId">通知编号</param>
        /// <param name="ItemType">附件类型</param>
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
