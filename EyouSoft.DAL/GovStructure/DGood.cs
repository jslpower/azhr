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
    /// 物品管理数据访问层
    /// 2011-09-26 邵权江 创建
    /// </summary>
    public class DGood : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.GovStructure.IGood
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DGood()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
 
        #endregion

        #region 成员方法
        /// <summary>
        /// 判断物品是否存在
        /// </summary>
        /// <param name="Name">物品名称</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品Id,新增Id=""</param>
        /// <returns></returns>
        public bool ExistsNum(string Name, string GoodId, string CompanyId)
        {
            string StrSql = " SELECT Count(1) FROM tbl_GovGood WHERE CompanyId=@CompanyId AND GoodName=@Name ";
            if (GoodId != "")
            {
                StrSql += " AND GoodId<>'@GoodId'";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (GoodId != "")
            {
                this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, GoodId);
            }
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "Name", DbType.String, Name);
            return EyouSoft.Toolkit.DAL.DbHelper.Exists(dc, _db);
        }

        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="model">物品model</param>
        public bool AddGovGood(Model.GovStructure.MGovGood model)
        {
            string StrSql = "INSERT INTO tbl_GovGood([GoodId],[CompanyId],[GoodName],[Number],[Price],[Stock],[GoodUse],[InTime],[Remark],[OperatorId],[IssueTime]) VALUES(@GoodId,@CompanyId,@Name,@Number,@Price,@Stock,@Use,@Time,@Remark,@OperatorId,@IssueTime)";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, model.GoodId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Name", DbType.String, model.Name);
            this._db.AddInParameter(dc, "Number", DbType.Int32, model.Number);
            this._db.AddInParameter(dc, "Price", DbType.Decimal, model.Price);
            this._db.AddInParameter(dc, "Stock", DbType.Int32, model.Stock);
            this._db.AddInParameter(dc, "Use", DbType.String, model.Use);
            this._db.AddInParameter(dc, "Time", DbType.DateTime, model.Time);
            this._db.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 更新物品
        /// </summary>
        /// <param name="model">物品model</param>
        /// <returns></returns>
        public bool UpdateGovGood(Model.GovStructure.MGovGood model)
        {
            string StrSql = "UPDATE tbl_GovGood SET GoodName=@Name,Number=@Number,Price=@Price,Stock=@Stock,[GoodUse]=@Use,InTime=@Time,Remark=@Remark WHERE GoodId=@GoodId AND CompanyId=@CompanyId";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, model.GoodId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Name", DbType.String, model.Name);
            this._db.AddInParameter(dc, "Number", DbType.Int32, model.Number);
            this._db.AddInParameter(dc, "Price", DbType.Decimal, model.Price);
            this._db.AddInParameter(dc, "Stock", DbType.Int32, model.Stock);
            this._db.AddInParameter(dc, "Use", DbType.String, model.Use);
            this._db.AddInParameter(dc, "Time", DbType.DateTime, model.Time);
            this._db.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            //this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            //this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            return EyouSoft.Toolkit.DAL.DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获得物品实体
        /// </summary>
        /// <param name="GoodId">物品编号ID</param>
        /// <param name="CompanyId">公司编号ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovGood GetGovGoodModel(string GoodId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovGood model = null;
            DbCommand dc = this._db.GetSqlStringCommand("SELECT [GoodId],[CompanyId],[GoodName],[Number],[Price],[Stock],[GoodUse],[InTime],[Remark],[OperatorId],[IssueTime],(SELECT TOP 1 ContactName FROM tbl_ComUser WHERE UserId=tbl_GovGood.OperatorId )AS Operator FROM tbl_GovGood  WHERE GoodId=@GoodId AND CompanyId=@CompanyId");
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, GoodId);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovGood()
                    {
                        GoodId = dr.GetString(dr.GetOrdinal("GoodId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Name = dr.IsDBNull(dr.GetOrdinal("GoodName")) ? "" : dr.GetString(dr.GetOrdinal("GoodName")),
                        Number = !dr.IsDBNull(dr.GetOrdinal("Number")) ? dr.GetInt32(dr.GetOrdinal("Number")) : 0,
                        Price = !dr.IsDBNull(dr.GetOrdinal("Price")) ? dr.GetDecimal(dr.GetOrdinal("Price")) : 0,
                        Stock = !dr.IsDBNull(dr.GetOrdinal("Stock")) ? dr.GetInt32(dr.GetOrdinal("Stock")) : 0,
                        Use = dr.IsDBNull(dr.GetOrdinal("GoodUse")) ? "" : dr.GetString(dr.GetOrdinal("GoodUse")),
                        Time = !dr.IsDBNull(dr.GetOrdinal("InTime")) ? dr.GetDateTime(dr.GetOrdinal("InTime")) : DateTime.Now,
                        Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark")),
                        OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "",
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : ""
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// 根据条件获得物品信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">物品名称</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovGoodList> GetGovGoodList(string CompanyId, string Name, DateTime? TimeBegin, DateTime? TimeEnd, int PageSize, int PageIndex, ref int RecordCount)
        {
            if (CompanyId.Trim() == "")
                return null;
            IList<Model.GovStructure.MGovGoodList> ResultList = null;
            string tableName = "tbl_GovGood";
            string identityColumnName = "GoodId";
            string fields = " GoodId,CompanyId,GoodName,Number,Price,Stock,InTime,OperatorId,IssueTime,(SELECT TOP 1 ContactName FROM tbl_ComUser WHERE UserId=tbl_GovGood.OperatorId )AS Operator, ";
            fields += " (SELECT SUM(Number) FROM tbl_GovGoodUse WHERE GoodId=tbl_GovGood.GoodId and UseType=0)AS CollarNumber, ";
            fields += " (SELECT SUM(Number) FROM tbl_GovGoodUse WHERE GoodId=tbl_GovGood.GoodId and UseType=1)AS GrantNumber, ";
            fields += " (SELECT SUM(Number) FROM tbl_GovGoodUse WHERE GoodId=tbl_GovGood.GoodId and UseType=2 and ReturnTime is null)AS BorrowNumber ";
            string query = string.Format(" CompanyId='{0}'", CompanyId);
            if (!string.IsNullOrEmpty(Name))
            {
                query = query + string.Format(" AND GoodName LIKE '%{0}%'", Name);
            }
            if (TimeBegin != null)
            {
                query = query + string.Format(" AND InTime>= '{0}' ", TimeBegin.Value.ToShortDateString() + " 00:00:00");
            }
            if (TimeEnd != null)
            {
                query = query + string.Format(" AND InTime<= '{0}' ", TimeEnd.Value.ToShortDateString() + " 23:59:59");
            }
            string orderByString = " InTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<Model.GovStructure.MGovGoodList>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovGoodList model = new EyouSoft.Model.GovStructure.MGovGoodList()
                    {
                        GoodId = dr.GetString(dr.GetOrdinal("GoodId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Name = !dr.IsDBNull(dr.GetOrdinal("GoodName")) ? dr.GetString(dr.GetOrdinal("GoodName")) : "",
                        Number = !dr.IsDBNull(dr.GetOrdinal("Number")) ? dr.GetInt32(dr.GetOrdinal("Number")) : 0,
                        Time = !dr.IsDBNull(dr.GetOrdinal("InTime")) ? dr.GetDateTime(dr.GetOrdinal("InTime")) : DateTime.Now,
                        CollarNumber = !dr.IsDBNull(dr.GetOrdinal("CollarNumber")) ? dr.GetInt32(dr.GetOrdinal("CollarNumber")) : 0,
                        GrantNumber = !dr.IsDBNull(dr.GetOrdinal("GrantNumber")) ? dr.GetInt32(dr.GetOrdinal("GrantNumber")) : 0,
                        BorrowNumber = !dr.IsDBNull(dr.GetOrdinal("BorrowNumber")) ? dr.GetInt32(dr.GetOrdinal("BorrowNumber")) : 0,
                        Stock = !dr.IsDBNull(dr.GetOrdinal("Stock")) ? dr.GetInt32(dr.GetOrdinal("Stock")) : 0,
                        Price = !dr.IsDBNull(dr.GetOrdinal("Price")) ? dr.GetDecimal(dr.GetOrdinal("Price")) : 0,
                        OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : "",
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : ""
                    };
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据物品编号删除
        /// </summary>
        /// <param name="GoodIds">物品编号ID</param>
        /// <returns>0或负值：失败，1成功，2已被借阅未归还，不能删除</returns>
        public int DeleteGovGood(params string[] GoodIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < GoodIds.Length; i++)
            {
                sId.AppendFormat("{0},", GoodIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovGood_Delete");
            this._db.AddInParameter(dc, "GoodIds", DbType.AnsiString, sId.ToString());
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
        /// 根据物品编号获取库存
        /// </summary>
        /// <param name="GoodId">物品编号ID</param>
        /// <returns></returns>
        public int GetGovGoodNum(string GoodId)
        {
            string StrSql = " SELECT Stock FROM tbl_GovGood WHERE GoodId=@GoodId ";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, GoodId);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    return !dr.IsDBNull(dr.GetOrdinal("Stock")) ? dr.GetInt32(dr.GetOrdinal("Stock")) : 0;
                }
            }
            return 0;
        }

        /// <summary>
        /// 增加物品领用/发放/借阅
        /// </summary>
        /// <param name="model">物品领用/发放/借阅model</param>
        /// <returns>正值1:成功； 负值或0:失败；2:超过库存</returns>
        public int AddGovGoodUse(Model.GovStructure.MGovGoodUse model)
        {
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovGoodUse_Add");
            this._db.AddInParameter(dc, "UseId", DbType.AnsiStringFixedLength, model.UseId);
            this._db.AddInParameter(dc, "GoodId", DbType.AnsiStringFixedLength, model.GoodId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "Type", DbType.Byte, (int)model.Type);
            this._db.AddInParameter(dc, "Time", DbType.DateTime, model.Time);
            this._db.AddInParameter(dc, "DeptId", DbType.Int32, model.DeptId);
            this._db.AddInParameter(dc, "Number", DbType.Int32, model.Number);
            this._db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, model.UserId);
            this._db.AddInParameter(dc, "Price", DbType.Decimal, model.Price);
            this._db.AddInParameter(dc, "Use", DbType.String, model.Use);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "PlanId", DbType.AnsiStringFixedLength, model.PlanId);
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
        /// 批量增加物品领用/发放/借阅
        /// </summary>
        /// <param name="list">物品领用/发放/借阅model列表</param>
        /// <returns>0：成功； 正值：失败数量； -1：失败</returns>
        public int AddGovGoodUseList(IList<EyouSoft.Model.GovStructure.MGovGoodUse> list)
        {
            string GoodUseXML = CreateGovGoodUseXML(list);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovGoodUseList_Add");
            this._db.AddInParameter(dc, "GoodUseXML", DbType.Xml, GoodUseXML);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return -1;
        }

        /// <summary>
        /// 物品领用/发放/借阅信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品编号</param>
        /// <param name="GoodUseType">使用类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovGoodUseList> GetGovUseGoodList(string CompanyId, string GoodId, int GoodUseType, int PageSize, int PageIndex, ref int RecordCount)
        {
            if (CompanyId.Trim() == "")
                return null;
            IList<Model.GovStructure.MGovGoodUseList> ResultList = null;
            string tableName = "tbl_GovGoodUse";
            string identityColumnName = "UseId";
            string fields = " UseId,GoodId,CompanyId,Number,UseTime,UserId,DeptId,OperatorId,IssueTime,GoodUse,ReturnTime, ";
            fields += " (SELECT TOP 1 [Price] FROM tbl_GovGood WHERE GoodId=tbl_GovGoodUse.GoodId)AS Price, ";
            fields += " (SELECT TOP 1 [NAME] FROM tbl_GovFile WHERE ID=tbl_GovGoodUse.UserId)AS UserName, ";
            fields += " (SELECT TOP 1 DepartName FROM tbl_ComDepartment WHERE DepartId=tbl_GovGoodUse.DeptId)AS Dept, ";
            fields += " (SELECT TOP 1 TourId FROM tbl_Plan WHERE PlanId=tbl_GovGoodUse.PlanId)AS TourId, ";
            fields += " (SELECT TOP 1 TourCode FROM tbl_Tour WHERE TourId  IN (SELECT TOP 1 TourId FROM tbl_Plan WHERE PlanId=tbl_GovGoodUse.PlanId))AS TourCode ";
            string query = string.Format(" CompanyId='{0}' AND  UseType = {1}  AND GoodId = '{2}'  ", CompanyId, GoodUseType, GoodId);
            string orderByString = " UseTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<Model.GovStructure.MGovGoodUseList>();
                while (dr.Read())
                {
                    EyouSoft.Model.GovStructure.MGovGoodUseList model = new EyouSoft.Model.GovStructure.MGovGoodUseList()
                    {
                        UseId = dr.GetString(dr.GetOrdinal("UseId")),
                        GoodId = dr.GetString(dr.GetOrdinal("GoodId")),
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                        Number = !dr.IsDBNull(dr.GetOrdinal("Number")) ? dr.GetInt32(dr.GetOrdinal("Number")) : 0,
                        Time = !dr.IsDBNull(dr.GetOrdinal("UseTime")) ? dr.GetDateTime(dr.GetOrdinal("UseTime")) : DateTime.Now,
                        UserName=!dr.IsDBNull(dr.GetOrdinal("UserName")) ? dr.GetString(dr.GetOrdinal("UserName")) : "",
                        Dept = !dr.IsDBNull(dr.GetOrdinal("Dept")) ? dr.GetString(dr.GetOrdinal("Dept")) : "",
                        Price = !dr.IsDBNull(dr.GetOrdinal("Price")) ? dr.GetDecimal(dr.GetOrdinal("Price")) : 0,
                        //SumPrice = (!dr.IsDBNull(dr.GetOrdinal("Number")) ? dr.GetInt32(dr.GetOrdinal("Number")) : 0) * (!dr.IsDBNull(dr.GetOrdinal("Price")) ? dr.GetDecimal(dr.GetOrdinal("Price")) : 0),
                        Use = !dr.IsDBNull(dr.GetOrdinal("GoodUse")) ? dr.GetString(dr.GetOrdinal("GoodUse")) : "",
                        TourId = !dr.IsDBNull(dr.GetOrdinal("TourId")) ? dr.GetString(dr.GetOrdinal("TourId")) : "",
                        TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : "",
                        OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : ""
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("ReturnTime")))
                    {
                        model.ReturnTime = dr.GetDateTime(dr.GetOrdinal("ReturnTime"));
                    }
                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据借阅编号归还
        /// </summary>
        /// <param name="IDs">借阅编号ID</param>
        /// <returns>0或负值：失败，1成功</returns>
        public int ReturnGovGoodBorrow(params string[] IDs)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < IDs.Length; i++)
            {
                sId.AppendFormat("{0},", IDs[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovGoodBorrow_Return");
            this._db.AddInParameter(dc, "IDs", DbType.AnsiString, sId.ToString());
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString());
            }
            return 0;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建物品领用/发放/借阅XML
        /// </summary>
        /// <param name="Lists">物品领用/发放/借阅集合</param>
        /// <returns></returns>
        private string CreateGovGoodUseXML(IList<EyouSoft.Model.GovStructure.MGovGoodUse> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovGoodUse model in list)
            {
                StrBuild.AppendFormat("<GovGoodUse UseId=\"{0}\"", model.UseId);
                StrBuild.AppendFormat(" GoodId=\"{0}\" ", model.GoodId);
                StrBuild.AppendFormat(" CompanyId=\"{0}\" ", model.CompanyId);
                StrBuild.AppendFormat(" UseType=\"{0}\" ", (int)model.Type);
                StrBuild.AppendFormat(" UseTime=\"{0}\" ", model.Time);
                StrBuild.AppendFormat(" DeptId=\"{0}\" ", model.DeptId);
                StrBuild.AppendFormat(" Number=\"{0}\" ", model.Number);
                StrBuild.AppendFormat(" UserId=\"{0}\" ", model.UserId);
                StrBuild.AppendFormat(" Price=\"{0}\" ", model.Price);
                StrBuild.AppendFormat(" GoodUse=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Use));
                StrBuild.AppendFormat(" OperatorId=\"{0}\" ", model.OperatorId);
                StrBuild.AppendFormat(" IssueTime=\"{0}\" ", model.IssueTime);
                StrBuild.AppendFormat(" PlanId=\"{0}\" />", model.PlanId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        #endregion
    }
}
