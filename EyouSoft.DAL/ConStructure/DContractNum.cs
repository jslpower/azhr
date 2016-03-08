using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Xml.Linq;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.ConStructure
{
    /// <summary>
    /// 合同号管理数据访问层
    /// 2011-09-27 邵权江 创建
    /// </summary>
    public class DContractNum : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.ConStructure.IContractNum
    {
        #region 私有变量
        private Database _db = null;
        #endregion

        #region 构造函数
        public DContractNum()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        #endregion

        #region 私有方法
        /// <summary>
        /// 创建合同号XML
        /// </summary>
        /// <param name="Lists">合同号集合</param>
        /// <returns></returns>
        private string CreateContractNumXML(IList<EyouSoft.Model.ConStructure.MContractNum> list)
        {
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ConStructure.MContractNum model in list)
            {
                StrBuild.AppendFormat("<ContractNum ContractId=\"{0}\"", model.ContractId);
                StrBuild.AppendFormat(" CompanyId=\"{0}\" ", model.CompanyId);
                StrBuild.AppendFormat(" ContractType=\"{0}\" ", (int)model.ContractType);
                StrBuild.AppendFormat(" ContractCode=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.ContractCode));
                StrBuild.AppendFormat(" ContractStatus=\"{0}\" ", (int)model.ContractStatus);
                StrBuild.AppendFormat(" DepartId=\"{0}\" ", model.DepartId);
                StrBuild.AppendFormat(" OperatorId=\"{0}\" ", model.OperatorId);
                StrBuild.AppendFormat(" IssueTime=\"{0}\" />", model.IssueTime);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建领用合同号XML
        /// </summary>
        /// <param name="Lists">领用合同号集合</param>
        /// <returns></returns>
        private string CreateContractNumCollarXML(IList<EyouSoft.Model.ConStructure.MContractNumCollar> list)
        {
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ConStructure.MContractNumCollar model in list)
            {
                StrBuild.AppendFormat("<ContractNumCollar CollariId=\"{0}\"", model.CollariId);
                StrBuild.AppendFormat(" ContractId=\"{0}\" ", model.ContractId);
                StrBuild.AppendFormat(" ContractCode=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.ContractCode));
                StrBuild.AppendFormat(" DepartId=\"{0}\" ", model.DepartId);
                StrBuild.AppendFormat(" UseId=\"{0}\" ", model.UseId);
                StrBuild.AppendFormat(" OperatorId=\"{0}\" ", model.OperatorId);
                StrBuild.AppendFormat(" IssueTime=\"{0}\" />", model.IssueTime);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建合同号状态XML
        /// </summary>
        /// <param name="Lists">合同号状体集合</param>
        /// <returns></returns>
        private string CreateContractNumUseXML(IList<EyouSoft.Model.ConStructure.MContractStatus> list)
        {
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ConStructure.MContractStatus model in list)
            {
                StrBuild.AppendFormat("<ContractNumStatus ContractId=\"{0}\"", model.ContractId);
                StrBuild.AppendFormat(" ContractStatus=\"{0}\"  />", (int)model.ContractStatus);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        #endregion

        #region 成员方法
        /// <summary>
        /// 是否存在该合同号
        /// </summary>
        /// <param name="model">合同号model：CompanyId,ContractNum</param>
        /// <returns></returns>
        public bool ExistsNum(Model.ConStructure.MContractNum model)
        {
            string StrSql = " SELECT Count(1) FROM tbl_ContractNum WHERE CompanyId=@CompanyId and ContractCode=@ContractCode AND IsDelete='0' ";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "ContractCode", DbType.String, model.ContractCode);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 登记合同号
        /// </summary>
        /// <param name="ListModel">合同号model集合</param>
        /// <returns> 0：成功 负值：失败 正值大于0：重复数</returns>
        public int AddContractNum(IList<Model.ConStructure.MContractNum> ListModel)
        {
            string ContractNumXML = CreateContractNumXML(ListModel);
            DbCommand dc = this._db.GetStoredProcCommand("proc_ContractNum_Add");
            this._db.AddInParameter(dc, "ContractNumXML", DbType.Xml, ContractNumXML);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ContractType">合同类型</param>
        /// <param name="SearchModel">查询参数实体</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId,int ContractType, Model.ConStructure.MContractNumSearch SearchModel, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.ConStructure.MContractNumList> ResultList = null;
            string tableName = "view_ContractNum";
            string identityColumnName = "ContractId";
            string fields = "ContractId,CompanyId,ContractType,ContractCode,DepartId,TourId,CollarTime,UseId,UseName,OrderId,OrderCode,RouteId,RouteName,BuyCompanyId,BuyCompanyName,Adults,Childs,Others,SumPrice,SellerId,SellerName,ContractStatus,IssueTime  ";
            string query = string.Format(" CompanyId='{0}' AND  ContractType = {1} ", CompanyId, ContractType);
            if (SearchModel != null)
            {
                if (!string.IsNullOrEmpty(SearchModel.ContractCode))
                {
                    query = query + string.Format(" AND ContractCode LIKE '%{0}%' ", SearchModel.ContractCode);
                }
                if (!string.IsNullOrEmpty(SearchModel.UseId))
                {
                    query = query + string.Format(" AND UseId = '{0}' ", SearchModel.UseId);
                }
                if (!string.IsNullOrEmpty(SearchModel.UseName))
                {
                    query = query + string.Format(" AND UseName LIKE '%{0}%' ", SearchModel.UseName);
                }
                if (SearchModel.TimeStart != null)
                {
                    query = query + string.Format(" AND CollarTime >= '{0}' ", SearchModel.TimeStart.Value.ToShortDateString()+" 00:00:00");
                }
                if (SearchModel.TimeEnd != null)
                {
                    query = query + string.Format(" AND CollarTime <= '{0}' ", SearchModel.TimeEnd.Value.ToShortDateString()+" 23:59:59");
                }
                if (SearchModel.ContractStatus != null)
                {
                    query = query + string.Format(" AND ContractStatus = {0} ", (int)SearchModel.ContractStatus);
                }
            }
            string orderByString = "ContractCode ASC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.ConStructure.MContractNumList>();
                EyouSoft.Model.ConStructure.MContractNumList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ConStructure.MContractNumList();
                    model.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : "";
                    model.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("CollarTime")))
                    {
                        model.CollarTime = dr.GetDateTime(dr.GetOrdinal("CollarTime"));
                    }
                    model.UseId = !dr.IsDBNull(dr.GetOrdinal("UseId")) ? dr.GetString(dr.GetOrdinal("UseId")) : "";
                    model.UseName = !dr.IsDBNull(dr.GetOrdinal("UseName")) ? dr.GetString(dr.GetOrdinal("UseName")) : "";
                    model.TourId = !dr.IsDBNull(dr.GetOrdinal("TourId")) ? dr.GetString(dr.GetOrdinal("TourId")) : "";
                    model.OrderId = !dr.IsDBNull(dr.GetOrdinal("OrderId")) ? dr.GetString(dr.GetOrdinal("OrderId")) : "";
                    model.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : "";
                    model.RouteId = !dr.IsDBNull(dr.GetOrdinal("RouteId")) ? dr.GetString(dr.GetOrdinal("RouteId")) : "";
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : "";
                    model.BuyCompanyId = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyId")) ? dr.GetString(dr.GetOrdinal("BuyCompanyId")) : "";
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : "";
                    model.Adults = !dr.IsDBNull(dr.GetOrdinal("Adults")) ? dr.GetInt32(dr.GetOrdinal("Adults")) : -1;
                    model.Childs = !dr.IsDBNull(dr.GetOrdinal("Childs")) ? dr.GetInt32(dr.GetOrdinal("Childs")) : -1;
                    model.Others = !dr.IsDBNull(dr.GetOrdinal("Others")) ? dr.GetInt32(dr.GetOrdinal("Others")) : -1;
                    model.SumPrice = !dr.IsDBNull(dr.GetOrdinal("SumPrice")) ? dr.GetDecimal(dr.GetOrdinal("SumPrice")) : -1;
                    model.SellerId = !dr.IsDBNull(dr.GetOrdinal("SellerId")) ? dr.GetString(dr.GetOrdinal("SellerId")) : "";
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : "";
                    model.ContractStatus = (EyouSoft.Model.EnumType.ConStructure.ContractStatus)dr.GetByte(dr.GetOrdinal("ContractStatus"));
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">查询</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetAutocompleteHeTongs(string companyId, EyouSoft.Model.ConStructure.MAutocompleteChaXunInfo info)
        {
            IList<EyouSoft.Model.ConStructure.MContractNumList> ResultList = null;
            string sql = string.Empty;
            sql += string.Format(" SELECT TOP {0} [ContractId],[ContractCode] FROM [tbl_ContractNum] ", info.Length);
            sql += string.Format(" WHERE [CompanyId]='{0}' AND [IsDelete]='0' ", companyId);

            if (!string.IsNullOrEmpty(info.HeTongCode))
            {
                sql += string.Format(" AND [ContractCode] LIKE '%{0}%' ", info.HeTongCode);
            }
            if (info.LeiXing.HasValue)
            {
                sql += string.Format(" AND [ContractType]={0} ", (int)info.LeiXing.Value);
            }
            if (info.Status.HasValue||!string.IsNullOrEmpty(info.HeTongId))
            {
                sql += " AND(1=0 ";
                if (info.Status.HasValue)
                {
                    sql += string.Format(" OR [ContractStatus]={0} ", (int)info.Status.Value);
                }
                if (!string.IsNullOrEmpty(info.HeTongId))
                {
                    sql += string.Format(" OR [ContractId]='{0}' ", info.HeTongId);
                }
                sql += " ) ";
            }

            DbCommand dc = this._db.GetSqlStringCommand(sql);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.ConStructure.MContractNumList>();
                EyouSoft.Model.ConStructure.MContractNumList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ConStructure.MContractNumList();
                    model.ContractId = dr.GetString(dr.GetOrdinal("ContractId"));
                    model.ContractCode = dr["ContractCode"].ToString();
                    ResultList.Add(model);
                }
            };
            return ResultList;
        }


        /// <summary>
        /// 根据合同编号获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("'{0}',", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            IList<EyouSoft.Model.ConStructure.MContractNumList> ResultList = null;
            string sql = string.Format("SELECT ContractId,CompanyId,ContractType,ContractCode,CollarTime,UseName,OrderCode,RouteName,BuyCompanyName,Adults,Childs,Others,SumPrice,SellerName,ContractStatus from view_ContractNum where CompanyId='{0}' and ContractId in ({1})", CompanyId, sId.ToString());
            DbCommand dc = this._db.GetSqlStringCommand(sql);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                ResultList = new List<EyouSoft.Model.ConStructure.MContractNumList>();
                EyouSoft.Model.ConStructure.MContractNumList model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.ConStructure.MContractNumList();
                    model.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : "";
                    model.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("CollarTime")))
                    {
                        model.CollarTime = dr.GetDateTime(dr.GetOrdinal("CollarTime"));
                    }
                    model.UseName = !dr.IsDBNull(dr.GetOrdinal("UseName")) ? dr.GetString(dr.GetOrdinal("UseName")) : "";
                    model.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : "";
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : "";
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : "";
                    model.Adults = !dr.IsDBNull(dr.GetOrdinal("Adults")) ? dr.GetInt32(dr.GetOrdinal("Adults")) : 0;
                    model.Childs = !dr.IsDBNull(dr.GetOrdinal("Childs")) ? dr.GetInt32(dr.GetOrdinal("Childs")) : 0;
                    model.Others = !dr.IsDBNull(dr.GetOrdinal("Others")) ? dr.GetInt32(dr.GetOrdinal("Others")) : 0;
                    model.SumPrice = !dr.IsDBNull(dr.GetOrdinal("SumPrice")) ? dr.GetDecimal(dr.GetOrdinal("SumPrice")) : 0;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : "";
                    model.ContractStatus = (EyouSoft.Model.EnumType.ConStructure.ContractStatus)dr.GetByte(dr.GetOrdinal("ContractStatus"));
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 领用合同号
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ListModel">领用合同号model集合</param>
        /// <returns>操作结果 0：成功 负值：失败 正值大于0：重复数</returns>
        public int AddContractNumCollar(string CompanyId, IList<Model.ConStructure.MContractNumCollar> ListModel)
        {
            string ContractNumCollarXML = CreateContractNumCollarXML(ListModel);
            DbCommand dc = this._db.GetStoredProcCommand("proc_ContractNumCollar_Add");
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "ContractNumCollarXML", DbType.Xml, ContractNumCollarXML);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 合同号（使用，销号，作废）
        /// </summary>
        /// <param name="ContractStatus">合同号状态</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns>0或负值：失败，1成功，2合同号不在状态</returns>
        public int ChangeContractStatus(EyouSoft.Model.EnumType.ConStructure.ContractStatus ContractStatus, params string[] Ids)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < Ids.Length; i++)
            {
                sId.AppendFormat("{0},", Ids[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_ContractStatusChange");
            this._db.AddInParameter(dc, "ContractStatus", DbType.Byte, (int)ContractStatus);
            this._db.AddInParameter(dc, "Ids", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 批量合同号状态改变（使用，销号，作废）
        /// </summary>
        /// <param name="ListModel">合同号状态集合</param>
        /// <returns>1：成功 0：失败</returns>
        public int ChangeContractStatus(IList<EyouSoft.Model.ConStructure.MContractStatus> ListModel)
        {
            DbCommand dc = this._db.GetStoredProcCommand("proc_ContractNumStatus_Change");
            this._db.AddInParameter(dc, "ContractNumUseXML", DbType.Xml, CreateContractNumUseXML(ListModel));
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 删除合同信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="heTongId">合同编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string heTongId)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" DECLARE @RetCode INT; ");
            s.Append(" IF NOT EXISTS(SELECT 1 FROM [tbl_ContractNum] WHERE [ContractId]=@HeTongId AND [CompanyId]=@CompanyId AND [ContractStatus]=@Status) ");
            s.Append(" BEGIN ");
            s.Append(" SET @RetCode=-99; ");
            s.Append(" SELECT @RetCode; ");
            s.Append(" RETURN;  ");
            s.Append(" END ");
            //s.Append(" DELETE FROM [tbl_ContractNumCollar] WHERE [ContractId]=@HeTongId; ");
            //s.Append(" DELETE FROM [tbl_ContractNum] WHERE [ContractId]=@HeTongId; ");
            s.Append(" UPDATE [tbl_ContractNum] SET [IsDelete]='1' WHERE [ContractId]=@HeTongId; ");
            s.Append(" SET @RetCode=1; ");
            s.Append(" SELECT @RetCode; ");

            DbCommand cmd = _db.GetSqlStringCommand(s.ToString());
            _db.AddInParameter(cmd, "HeTongId", DbType.AnsiStringFixedLength, heTongId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "Status", DbType.Byte, EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return -100;
        }
        #endregion
    }
}
