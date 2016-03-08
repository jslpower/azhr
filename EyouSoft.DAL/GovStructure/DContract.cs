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
    /// 公司合同数据访问层
    /// 2011-09-26 邵权江 创建
    /// </summary>
    public class DContract : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IContract
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DContract()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        #endregion

        #region 成员方法
        /// <summary>
        /// 判断合同编号是否存在
        /// </summary>
        /// <param name="Number">合同编码</param>
        /// <param name="ID">合同ID,新增ID=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsNumber(string Number, string ID, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovContract WHERE CompanyId=@CompanyId AND Number=@Number ";
            if (ID != "")
            {
                StrSql += " AND ID<>'@ID'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (ID != "")
            {
                this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "Number", DbType.String, Number);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <returns></returns>
        public bool AddGovContract(EyouSoft.Model.GovStructure.MGovContract model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovContract_Add");
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Number", DbType.String, model.Number);
            this._db.AddInParameter(dc, "Type", DbType.String, model.Type);
            this._db.AddInParameter(dc, "SignedTime", DbType.DateTime, model.SignedTime);
            this._db.AddInParameter(dc, "MaturityTime", DbType.DateTime, model.MaturityTime);
            this._db.AddInParameter(dc, "Company", DbType.String, model.Company);
            this._db.AddInParameter(dc, "IsRemind", DbType.AnsiStringFixedLength, model.IsRemind == true ? "1" : "0");
            this._db.AddInParameter(dc, "Description", DbType.String, model.Description);
            this._db.AddInParameter(dc, "signierId", DbType.AnsiStringFixedLength, model.signierId);
            this._db.AddInParameter(dc, "SignedDepId", DbType.Int32, model.SignedDepId);
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
        /// 更新合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <returns></returns>
        public bool UpdateGovContract(EyouSoft.Model.GovStructure.MGovContract model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovContract_Update");
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComAttachListXML(model.ComAttachList));
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Number", DbType.String, model.Number);
            this._db.AddInParameter(dc, "Type", DbType.String, model.Type);
            this._db.AddInParameter(dc, "SignedTime", DbType.DateTime, model.SignedTime);
            this._db.AddInParameter(dc, "MaturityTime", DbType.DateTime, model.MaturityTime);
            this._db.AddInParameter(dc, "Company", DbType.String, model.Company);
            this._db.AddInParameter(dc, "IsRemind", DbType.AnsiStringFixedLength, model.IsRemind == true ? "1" : "0");//1:是，0:否
            this._db.AddInParameter(dc, "Description", DbType.String, model.Description);
            this._db.AddInParameter(dc, "signierId", DbType.AnsiStringFixedLength, model.signierId);
            this._db.AddInParameter(dc, "SignedDepId", DbType.Int32, model.SignedDepId);
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
        /// 获得合同实体
        /// </summary>
        /// <param name="ID">合同ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovContract GetGovContractModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovContract model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT ID,CompanyId,Number,Type,SignedTime,MaturityTime,Company,IsRemind,Description,signierId,SignedDepId,OperatorId,IssueTime,");
            StrSql.Append(" (SELECT TOP 1 Name FROM tbl_GovFile WHERE ID=a.signierId )AS signier,");
            StrSql.Append(" (SELECT top 1 DepartName FROM tbl_ComDepartment WHERE DepartId=a.SignedDepId )AS SignedDep,");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.ID FOR XML RAW,ROOT('ROOT'))AS ComAttachXML ", (int)ItemType);
            StrSql.AppendFormat(" FROM tbl_GovContract AS a WHERE A.ID = '{0}'", ID);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            //this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovContract();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Number = dr.IsDBNull(dr.GetOrdinal("Number")) ? "" : dr.GetString(dr.GetOrdinal("Number"));
                    model.Type = dr.IsDBNull(dr.GetOrdinal("Type")) ? "" : dr.GetString(dr.GetOrdinal("Type"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SignedTime")))
                    {
                        model.SignedTime = dr.GetDateTime(dr.GetOrdinal("SignedTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("MaturityTime")))
                    {
                        model.MaturityTime = dr.GetDateTime(dr.GetOrdinal("MaturityTime"));
                    }
                    model.Company = dr.IsDBNull(dr.GetOrdinal("Company")) ? "" : dr.GetString(dr.GetOrdinal("Company"));
                    model.Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "" : dr.GetString(dr.GetOrdinal("Description"));
                    model.IsRemind = dr.GetString(dr.GetOrdinal("IsRemind")) == "1" ? true : false;
                    model.signierId = dr.IsDBNull(dr.GetOrdinal("signierId")) ? "" : dr.GetString(dr.GetOrdinal("signierId"));
                    model.SignedDepId = dr.IsDBNull(dr.GetOrdinal("SignedDepId")) ? 0 : dr.GetInt32(dr.GetOrdinal("SignedDepId"));
                    model.signier = dr.IsDBNull(dr.GetOrdinal("signier")) ? "" : dr.GetString(dr.GetOrdinal("signier"));
                    model.SignedDep = dr.IsDBNull(dr.GetOrdinal("SignedDep")) ? "" : dr.GetString(dr.GetOrdinal("SignedDep"));
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "";
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), ID, ItemType);
                }
            }
            return model;
        }

        /// <summary>
        /// 根据条件合同编号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">合同类型</param>
        /// <param name="TimeBegin">签订时间</param>
        /// <param name="TimeEnd">到期时间</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovContract> GetGovContractList(string CompanyId, string Type, DateTime? TimeBegin, DateTime? TimeEnd, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            if (CompanyId.Trim() == "")
                return null;
            IList<Model.GovStructure.MGovContract> ResultList = null;
            string tableName = "view_GovContract";
            string identityColumnName = "ID";
            string fields = " ID,CompanyId,Number,Type,MaturityTime,[Description],OperatorId,IssueTime,ComAttachXML ";
            string query = string.Format(" CompanyId='{0}' ", CompanyId, (int)ItemType);
            if (!string.IsNullOrEmpty(Type))
            {
                query = query + string.Format(" AND [Type] LIKE '%{0}%' ", Type);
            }
            if (TimeBegin != null)
            {
                query = query + string.Format(" AND [MaturityTime] >= '{0}' ", TimeBegin.Value.ToShortDateString() + " 00:00:00");
            }
            if (TimeEnd != null)
            {
                query = query + string.Format(" AND [MaturityTime] <= '{0}' ", TimeEnd.Value.ToShortDateString() + " 23:59:59");
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<Model.GovStructure.MGovContract>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovContract model = new EyouSoft.Model.GovStructure.MGovContract();
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Number = dr.GetString(dr.GetOrdinal("Number"));
                    model.Type = dr.IsDBNull(dr.GetOrdinal("Type")) ? "" : dr.GetString(dr.GetOrdinal("Type"));
                    model.Description = dr.IsDBNull(dr.GetOrdinal("Description")) ? "" : dr.GetString(dr.GetOrdinal("Description"));
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("MaturityTime")))
                    {
                        model.MaturityTime = dr.GetDateTime(dr.GetOrdinal("MaturityTime"));
                    }
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("ID")), ItemType);
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据合同ID删除
        /// </summary>
        /// <param name="IDs">合同ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool DeleteGovContract(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovContract_Delete");
            this._db.AddInParameter(dc, "IDs", DbType.AnsiString, sId.ToString());
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
        /// 创建附件XML
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
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="NoticeId">通知编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetAttachList(string ComAttachXML, string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
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
                    Size = (tmp1.Attribute("Size") != null && tmp1.Attribute("Size").Value.Trim() != "") ? int.Parse(tmp1.Attribute("Size").Value) : 0,
                    Downloads = (tmp1.Attribute("Downloads") != null && tmp1.Attribute("Downloads").Value.Trim() != "") ? int.Parse(tmp1.Attribute("Downloads").Value) : 0,
                    ItemId = ID,
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
