using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit;
using System.Xml.Linq;
namespace EyouSoft.DAL.GovStructure
{
    /// <summary>
    /// 规章制度管理数据访问层
    /// 2011-09-19 邵权江 创建
    /// </summary>
    public class DRegulation : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IRegulation
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DRegulation()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        #endregion

        #region 成员方法
        /// <summary>
        /// 判断规章制度编号是否存在
        /// </summary>
        /// <param name="Code">规章制度编码</param>
        /// <param name="RegId">规章制度RegId,新增RegId=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsCode(string Code, string RegId, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovRegulation WHERE CompanyId=@CompanyId AND Code=@Code ";
            if (RegId != "")
            {
                StrSql += " AND RegId<>'@RegId'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (RegId != "")
            {
                this._db.AddInParameter(dc, "RegId", DbType.AnsiStringFixedLength, RegId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "Code", DbType.String, Code);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 增加一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <returns></returns>
        public bool AddGovRegulation(EyouSoft.Model.GovStructure.MGovRegulation model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovRegulation_Add");
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
            this._db.AddInParameter(dc, "RegId", DbType.AnsiStringFixedLength, model.RegId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Code", DbType.String, model.Code);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "IssuedId", DbType.AnsiStringFixedLength, model.IssuedId);
            this._db.AddInParameter(dc, "IssuedDeptId", DbType.AnsiStringFixedLength, model.IssuedDeptId);
            this._db.AddInParameter(dc, "GovRegApplyDeptXML", DbType.Xml, CreateGovRegApplyDeptListXML(model.ApplyDeptList));//适用部门实体集合
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
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
        /// 更新一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateRegulation(EyouSoft.Model.GovStructure.MGovRegulation model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovRegulation_Update");
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
            this._db.AddInParameter(dc, "RegId", DbType.AnsiStringFixedLength, model.RegId);
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Code", DbType.String, model.Code);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._db.AddInParameter(dc, "IssuedId", DbType.AnsiStringFixedLength, model.IssuedId);
            this._db.AddInParameter(dc, "IssuedDeptId", DbType.AnsiStringFixedLength, model.IssuedDeptId);
            this._db.AddInParameter(dc, "GovRegApplyDeptXML", DbType.Xml, CreateGovRegApplyDeptListXML(model.ApplyDeptList));//适用部门实体集合
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
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
        /// 获得规章制度实体
        /// </summary>
        /// <param name="RegId">规章制度ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovRegulation GetGovRegulationModel(string RegId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovRegulation model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT RegId,CompanyId,Code,Title,Content,OperatorId,IssueTime,IssuedId,IssuedDeptId,");
            StrSql.Append(" (SELECT top 1 Name FROM tbl_GovFile WHERE ID=a.IssuedId)AS IssuedName, ");
            StrSql.Append(" (SELECT top 1 DepartName FROM tbl_ComDepartment WHERE DepartId=a.IssuedDeptId)AS IssuedDepartName, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.RegId FOR XML RAW,ROOT('ROOT'))AS ComAttachXML, ", (int)ItemType);
            StrSql.Append(" (SELECT DepartId,DepartName FROM tbl_ComDepartment WHERE DepartId IN(SELECT DepartId from tbl_GovRegApplyDept WHERE RegId=a.RegId) FOR XML RAW,ROOT('ROOT'))AS DepartmentXML ");
            StrSql.AppendFormat(" FROM tbl_GovRegulation AS a WHERE a.RegId = '{0}' ", RegId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            this._db.AddInParameter(dc, "RegId", DbType.AnsiStringFixedLength, RegId);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovRegulation()
                    {
                        RegId = dr.IsDBNull(dr.GetOrdinal("RegId")) ? "" : dr.GetString(dr.GetOrdinal("RegId")),
                        CompanyId = dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? "" : dr.GetString(dr.GetOrdinal("CompanyId")),
                        Code = dr.IsDBNull(dr.GetOrdinal("Code")) ? "" : dr.GetString(dr.GetOrdinal("Code")),
                        Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title")),
                        Content = dr.IsDBNull(dr.GetOrdinal("Content")) ? "" : dr.GetString(dr.GetOrdinal("Content")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssuedId = dr.IsDBNull(dr.GetOrdinal("IssuedId")) ? "" : dr.GetString(dr.GetOrdinal("IssuedId")),
                        IssuedName = dr.IsDBNull(dr.GetOrdinal("IssuedName")) ? "" : dr.GetString(dr.GetOrdinal("IssuedName")),
                        IssuedDeptId = dr.GetInt32(dr.GetOrdinal("IssuedDeptId")),
                        IssuedDepartName = dr.IsDBNull(dr.GetOrdinal("IssuedDepartName")) ? "" : dr.GetString(dr.GetOrdinal("IssuedDepartName")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), RegId, ItemType),
                        ApplyDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString()),
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// 获得规章制度信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchModel">查询实体</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovRegulation> GetGovRegulationList(string CompanyId, EyouSoft.Model.GovStructure.MGovRegSearch SearchModel, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            if (CompanyId.Trim() == "")
                return null;
            IList<Model.GovStructure.MGovRegulation> ResultList = null;
            string tableName = "view_GovRegulation";
            string identityColumnName = "RegId";
            string fields = " RegId,CompanyId,Code,Title,OperatorId,IssueTime,IssuedId,IssuedName,IssuedDeptId,IssuedDepartName,DepartmentXML,ComAttachXML ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);

            if (SearchModel != null)
            {
                if (!string.IsNullOrEmpty(SearchModel.Code))
                {
                    query = query + string.Format(" AND [Code] LIKE '%{0}%' ", SearchModel.Code);
                }
                if (!string.IsNullOrEmpty(SearchModel.Title))
                {
                    query = query + string.Format(" AND [Title] LIKE '%{0}%' ", SearchModel.Title);
                }
                if (SearchModel.DepIds != null && SearchModel.DepIds.Length > 0)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < SearchModel.DepIds.Length; i++)
                    {
                        sId.AppendFormat("{0},", SearchModel.DepIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    if (!string.IsNullOrEmpty(sId.ToString()))
                    {
                        query = query + string.Format(" AND EXISTS(SELECT RegId FROM tbl_GovRegApplyDept WHERE RegId=view_GovRegulation.RegId AND DepartId IN ({0})) ", sId);
                    }
                }
                if (!string.IsNullOrEmpty(SearchModel.DepName) && (SearchModel.DepIds == null || SearchModel.DepIds.Length < 1))
                {
                    query = query + string.Format(" and DepartmentXML like '%{0}%'", SearchModel.DepName);
                }
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<Model.GovStructure.MGovRegulation>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovRegulation model = new EyouSoft.Model.GovStructure.MGovRegulation()
                    {
                        RegId = dr.GetString(dr.GetOrdinal("RegId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Code = dr.GetString(dr.GetOrdinal("Code")),
                        Title = dr.GetString(dr.GetOrdinal("Title")),
                        OperatorId = dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? "" : dr.GetString(dr.GetOrdinal("OperatorId")),
                        IssuedId = dr.IsDBNull(dr.GetOrdinal("IssuedId")) ? "" : dr.GetString(dr.GetOrdinal("IssuedId")),
                        IssuedName = dr.IsDBNull(dr.GetOrdinal("IssuedName")) ? "" : dr.GetString(dr.GetOrdinal("IssuedName")),
                        IssuedDeptId = dr.IsDBNull(dr.GetOrdinal("IssuedDeptId")) ? 0 : dr.GetInt32(dr.GetOrdinal("IssuedDeptId")),
                        IssuedDepartName = dr.IsDBNull(dr.GetOrdinal("IssuedDepartName")) ? "" : dr.GetString(dr.GetOrdinal("IssuedDepartName")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("RegId")), ItemType),
                        ApplyDeptList = this.GetDepartmentList(dr["DepartmentXML"].ToString()),
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool DeleteGovRegulation(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovRegulation_Delete");
            this._db.AddInParameter(dc, "RegIds", DbType.AnsiString, sId.ToString());
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
        /// 创建规章制度附件XML
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
        /// 创建部门XML
        /// </summary>
        /// <param name="Lists">部门集合</param>
        /// <returns></returns>
        private string CreateGovRegApplyDeptListXML(IList<EyouSoft.Model.GovStructure.MGovRegApplyDept> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovRegApplyDept model in list)
            {
                StrBuild.AppendFormat("<GovRegApplyDept RegId=\"{0}\"", model.RegId);
                StrBuild.AppendFormat(" DepartId=\"{0}\" />", model.DepartId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="RegId">编号</param>
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
        /// 生成部门集合List
        /// </summary>
        /// <param name="DepartMentXml">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovRegApplyDept> GetDepartmentList(string DepartMentXml)
        {
            if (string.IsNullOrEmpty(DepartMentXml)) return null;
            IList<EyouSoft.Model.GovStructure.MGovRegApplyDept> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovRegApplyDept>();
            XElement root = XElement.Parse(DepartMentXml);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovRegApplyDept model = new EyouSoft.Model.GovStructure.MGovRegApplyDept()
                {
                    DepartId = int.Parse(tmp1.Attribute("DepartId").Value),
                    DepartName = tmp1.Attribute("DepartName").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        #endregion
    }
}
