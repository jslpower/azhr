using System;
using System.Collections.Generic;

namespace EyouSoft.DAL.FinStructure
{
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    using EyouSoft.IDAL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.CrmStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.Toolkit;
    using EyouSoft.Toolkit.DAL;

    using Microsoft.Practices.EnterpriseLibrary.Data;

    using EyouSoft.Model.CrmStructure;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-15
    /// </summary>
    public class DFinance : DALBase, IFinance
    {
        #region dal变量
        EyouSoft.DAL.TourStructure.DTourOrder dTourOrder = new EyouSoft.DAL.TourStructure.DTourOrder();
        #endregion

        #region 构造函数
        /// <summary>
        /// database
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DFinance()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddProfitDistribute(MProfitDistribute mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" INSERT INTO [tbl_FinProfitDistribute]");
            strSql.Append("            ([CompanyId]");
            strSql.Append("            ,[TourId]");
            strSql.Append("            ,[TourCode]");
            strSql.Append("            ,[OrderId]");
            strSql.Append("            ,[OrderCode]");
            strSql.Append("            ,[DistributeType]");
            strSql.Append("            ,[Amount]");
            strSql.Append("            ,[Gross]");
            strSql.Append("            ,[Profit]");
            strSql.Append("            ,[StaffId]");
            strSql.Append("            ,[Staff]");
            strSql.Append("            ,[Remark]");
            strSql.Append("            ,[OperatorId]");
            strSql.Append("            ,[IssueTime])");
            strSql.Append("      VALUES");
            strSql.Append("            (@CompanyId");
            strSql.Append("            ,@TourId");
            strSql.Append("            ,@TourCode");
            strSql.Append("            ,@OrderId");
            strSql.Append("            ,@OrderCode");
            strSql.Append("            ,@DistributeType");
            strSql.Append("            ,@Amount");
            strSql.Append("            ,@Gross");
            strSql.Append("            ,@Profit");
            strSql.Append("            ,@StaffId");
            strSql.Append("            ,@Staff");
            strSql.Append("            ,@Remark");
            strSql.Append("            ,@OperatorId");
            strSql.Append("            ,@IssueTime)");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(dc, "@OrderCode", DbType.String, mdl.OrderCode);
            this._db.AddInParameter(dc, "@DistributeType", DbType.String, mdl.DistributeType);
            this._db.AddInParameter(dc, "@Amount", DbType.Decimal, mdl.Amount);
            this._db.AddInParameter(dc, "@Gross", DbType.Decimal, mdl.Gross);
            this._db.AddInParameter(dc, "@Profit", DbType.Decimal, mdl.Profit);
            this._db.AddInParameter(dc, "@StaffId", DbType.AnsiStringFixedLength, mdl.StaffId);
            this._db.AddInParameter(dc, "@Staff", DbType.String, mdl.Staff);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdProfitDistribute(MProfitDistribute mdl)
        {
            var sql = new StringBuilder();

            sql.Append(" UPDATE  [dbo].[tbl_FinProfitDistribute]");
            sql.Append(" SET     [TourId] = @TourId ,");
            sql.Append("         [TourCode] = @TourCode ,");
            sql.Append("         [OrderId] = @OrderId ,");
            sql.Append("         [OrderCode] = @OrderCode ,");
            sql.Append("         [DistributeType] = @DistributeType ,");
            sql.Append("         [Amount] = @Amount ,");
            sql.Append("         [Gross] = @Gross ,");
            sql.Append("         [Profit] = @Profit ,");
            sql.Append("         [StaffId] = @StaffId ,");
            sql.Append("         [Staff] = @Staff ,");
            sql.Append("         [Remark] = @Remark ");
            sql.Append(" WHERE   Id = @Id");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(dc, "@OrderCode", DbType.String, mdl.OrderCode);
            this._db.AddInParameter(dc, "@DistributeType", DbType.String, mdl.DistributeType);
            this._db.AddInParameter(dc, "@Amount", DbType.Decimal, mdl.Amount);
            this._db.AddInParameter(dc, "@Gross", DbType.Decimal, mdl.Gross);
            this._db.AddInParameter(dc, "@Profit", DbType.Decimal, mdl.Profit);
            this._db.AddInParameter(dc, "@StaffId", DbType.AnsiStringFixedLength, mdl.StaffId);
            this._db.AddInParameter(dc, "@Staff", DbType.String, mdl.Staff);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public bool DelProfitDis(int id, string companyid)
        {
            var sql = "update tbl_FinProfitDistribute set isdeleted='1' where id=@id and companyid=@companyid";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "@id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@companyid", DbType.AnsiStringFixedLength, companyid);
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        public MProfitDistribute GetProfitDistribute(int id, string companId)
        {
            var mdl = new MProfitDistribute();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [DistributeType] ,");
            sql.Append("         [Amount] ,");
            sql.Append("         [Gross] ,");
            sql.Append("         [Profit] ,");
            sql.Append("         [Staff] ,");
            sql.Append("         [Remark] ");
            sql.Append(" FROM    [dbo].[tbl_FinProfitDistribute] where id=@id and companyid=@companyid");

            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@Companyid", DbType.AnsiStringFixedLength, companId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Staff = dr["Staff"].ToString();
                    mdl.DistributeType = dr["DistributeType"].ToString();
                    mdl.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    mdl.Gross = dr.GetDecimal(dr.GetOrdinal("Gross"));
                    mdl.Profit = dr.GetDecimal(dr.GetOrdinal("Profit"));
                    mdl.Remark = dr["Remark"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MProfitDistribute> GetProfitDistribute(string tourId)
        {
            var lst = new List<MProfitDistribute>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [Id] ,");
            sql.Append("         [CompanyId] ,");
            sql.Append("         [TourCode] ,");
            sql.Append("         [OrderCode] ,");
            sql.Append("         [DistributeType] ,");
            sql.Append("         [Amount] ,");
            sql.Append("         [Gross] ,");
            sql.Append("         [Profit] ,");
            sql.Append("         [Staff] ,");
            sql.Append("         [Remark] ");
            sql.Append(" FROM    [dbo].[tbl_FinProfitDistribute]");
            sql.Append(" WHERE   TourId = @TourId and isdeleted='0'");
            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MProfitDistribute
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            CompanyId = dr["CompanyId"].ToString(),
                            TourCode = dr["TourCode"].ToString(),
                            OrderCode = dr["OrderCode"].ToString(),
                            DistributeType = dr["DistributeType"].ToString(),
                            Amount = dr.GetDecimal(dr.GetOrdinal("Amount")),
                            Gross = dr.GetDecimal(dr.GetOrdinal("Gross")),
                            Profit = dr.GetDecimal(dr.GetOrdinal("Profit")),
                            Staff = dr["Staff"].ToString(),
                            Remark = dr["Remark"].ToString(),
                        });
                }
            }
            return lst;
        }

        #endregion

        #region 应收管理

        /// <summary>
        /// 根据应收搜索实体获取应收帐款/已结清账款列表和金额汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumField">金额汇总信息</param>
        /// <param name="mSearch">应收搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应收帐款/已结清账款列表</returns>
        public IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref object[] sumField
                                                        , MReceivableBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            const string table = "tbl_TourOrder";
            var sumfield = new StringBuilder();
            var lst = new List<MReceivableInfo>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            sumfield.Append("ISNULL(SUM(ConfirmMoney),0) SumPrice ,");
            sumfield.Append("ISNULL(SUM(CheckMoney),0) CheckMoney ,");
            sumfield.Append("ISNULL(SUM(Unchecked),0) Unchecked ,");
            sumfield.Append("ISNULL(SUM(Unreceived),0) Unreceived ,");
            sumfield.Append("ISNULL(SUM(ReturnMoney),0) ReturnMoney, ");
            sumfield.Append("ISNULL(SUM(UnChkRtn),0) UnChkRtn ");

            field.Append(" TourId,OrderId,OrderCode,Adults,Childs,Leaders,BuyCompanyId,BuyCompanyName,ContactName,ContactTel,SellerId,SellerName");
            field.Append(" ,Planers=STUFF((SELECT ',' + Planer FROM (SELECT Planer FROM tbl_TourPlaner WHERE TourId=tbl_TourOrder.TourId) AS A FOR XML PATH('')),1, 1,'')");
            field.Append(" ,RouteName=(select RouteName,tourcode,CONVERT(VARCHAR(19),LDate,120) LDate from tbl_Tour where TourId=tbl_TourOrder.TourId for xml raw,root)");
            field.Append(" ,SumPrice,ConfirmMoneyStatus,UnChkRtn=(select isnull(sum(CollectionRefundAmount),0) from tbl_TourOrderSales where OrderId=tbl_tourorder.OrderId and CollectionRefundState='1' and FinStatus='0')");
            field.Append(" ,CheckMoney,ReceivedMoney-CheckMoney Unchecked,ConfirmMoney-CheckMoney+ReturnMoney Unreceived,ReturnMoney,ConfirmMoney");
            field.Append(" ,Operator,OperatorId,IssueTime,OrderStatus,OrderType ");

            if (!string.IsNullOrEmpty(mSearch.OrderCode))
            {
                query.AppendFormat(" OrderCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.OrderCode));
            }
            if (!string.IsNullOrEmpty(mSearch.CustomerId))
            {
                query.AppendFormat(" BuyCompanyId = '{0}' AND", mSearch.CustomerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Customer))
            {
                query.AppendFormat(" BuyCompanyName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Customer));
            }
            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
            {
                query.AppendFormat(" SellerId = '{0}' AND", mSearch.SalesmanId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Salesman))
            {
                query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Salesman));
            }
            if (mSearch.UnChecked.HasValue && mSearch.SignUnChecked.HasValue)
            {
                query.AppendFormat(" (ReceivedMoney-CheckMoney) {0} {1} AND", GetEqualSign(mSearch.SignUnChecked), mSearch.UnChecked);
            }
            if (mSearch.UnReceived.HasValue && mSearch.SignUnReceived.HasValue)
            {
                query.AppendFormat(" (ConfirmMoney-CheckMoney+ReturnMoney) {0} {1} AND", GetEqualSign(mSearch.SignUnReceived), mSearch.UnReceived);
            }
            // 订单是否已结清
            if (mSearch.IsClean.HasValue)
            {
                query.AppendFormat(" IsClean = '{0}' AND", mSearch.IsClean.Value ? "1" : "0");
            }
            query.Append(" EXISTS(SELECT 1 FROM tbl_Tour WHERE ");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.RouteName))
            {
                query.AppendFormat(" RouteName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.RouteName));
            }
            query.AppendFormat(" TourId=tbl_TourOrder.TourId AND IsDelete = '0' AND TourStatus < {0} AND TourType NOT IN ({1},{2})", (int)TourStatus.已取消, (int)TourType.线路产品, (int)TourType.散拼模版团);


            //时间
            if (!string.IsNullOrEmpty(mSearch.LDateStart))
            {
                query.AppendFormat(" 	AND	LDate >= '{0}' ", mSearch.LDateStart);
            }
            if (!string.IsNullOrEmpty(mSearch.LDateEnd))
            {
                query.AppendFormat(" 	AND LDate <= '{0}' ", mSearch.LDateEnd);
            }

            query.AppendFormat(" ) AND ");
            query.AppendFormat(" CompanyId = '{0}' AND OrderStatus = {1} AND IsDelete = '0'", mSearch.CompanyId, (int)OrderStatus.已成交);
            

            // 是否同业分销
            if (!mSearch.IsShowDistribution.HasValue)
            {
                query.Append(GetReceivedOrg(operatorId, deptIds));
            }
            if (!string.IsNullOrEmpty(mSearch.OperatorId))
            {
                query.AppendFormat(" AND OperatorId='{0}' ", mSearch.OperatorId);
            }
            else if (!string.IsNullOrEmpty(mSearch.OperatorName))
            {
                query.AppendFormat(" AND Operator LIKE '%{0}%' ", mSearch.OperatorName);
            }


            using (var dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, table, field.ToString(), query.ToString(), "IssueTime DESC", sumfield.ToString()))
            {
                while (dr.Read())
                {
                    var mdl = new MReceivableInfo
                    {
                        //TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType")),
                        TourId = dr["TourId"].ToString(),
                        OrderId = dr["OrderId"].ToString(),
                        OrderCode = dr["OrderCode"].ToString(),
                        Adults = dr.GetInt32(dr.GetOrdinal("Adults")),
                        Childs = dr.GetInt32(dr.GetOrdinal("Childs")),
                        Others = dr.GetInt32(dr.GetOrdinal("Leaders")),
                        CustomerId = dr["BuyCompanyId"].ToString(),
                        Customer = dr["BuyCompanyName"].ToString(),
                        Contact = dr["ContactName"].ToString(),
                        Phone = dr["ContactTel"].ToString(),
                        SalesmanId = dr["SellerId"].ToString(),
                        Salesman = dr["SellerName"].ToString(),
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["RouteName"].ToString(), "RouteName"),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr["RouteName"].ToString(), "tourcode"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["RouteName"].ToString(), "LDate")),
                        TotalAmount = dr.GetDecimal(dr.GetOrdinal("SumPrice")),
                        IsConfirmed = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1",
                        Received = dr.GetDecimal(dr.GetOrdinal("CheckMoney")),
                        Receivable = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")),
                        UnChkRtn = dr.GetDecimal(dr.GetOrdinal("UnChkRtn")),
                        UnChecked = dr.GetDecimal(dr.GetOrdinal("Unchecked")),
                        Planers = dr["Planers"].ToString(),
                        UnReceived = dr.GetDecimal(dr.GetOrdinal("Unreceived")),
                        Returned = dr.GetDecimal(dr.GetOrdinal("ReturnMoney")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Status = (OrderStatus)dr.GetByte(dr.GetOrdinal("OrderStatus")),
                        OrderType=(OrderType)dr.GetByte(dr.GetOrdinal("OrderType"))
                    };
                    mdl.OperatorName = dr["Operator"].ToString();

                    lst.Add(mdl);
                }

                dr.NextResult();

                while (dr.Read())
                {
                    sumField[0] = dr.GetDecimal(dr.GetOrdinal("SumPrice"));//合计
                    sumField[1] = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));//已收金
                    sumField[2] = dr.GetDecimal(dr.GetOrdinal("Unchecked")); //已收待审核金
                    sumField[3] = dr.GetDecimal(dr.GetOrdinal("Unreceived"));//未收金额
                    sumField[4] = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));//已退金
                    sumField[5] = dr.GetDecimal(dr.GetOrdinal("UnChkRtn"));//已退待审
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据系统公司编号获取当日收款对账列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="tourTypes">团队类型集合</param>
        /// <param name="isShowDistribution">是否同业分销</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="search">搜索实体</param>
        /// <returns>当日收款对账列表</returns>
        public IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref string xmlSum
                                                                , string companyId
                                                                , IList<TourType> tourTypes
                                                                , bool isShowDistribution
                                                                , MDayReceivablesChkBase search
                                                                , string operatorId
                                                                , params int[] deptIds)
        {
            const string table = "view_TodayReceivedIncome";
            var sumfield = new[] { "CollectionRefundAmount" };
            var lst = new List<MDayReceivablesChk>();
            var query = new StringBuilder();

            if (tourTypes != null && tourTypes.Count > 0)
            {
                query.Append(tourTypes.Aggregate(" TourType IN (", (current, tourType) => current + string.Format("{0},", tourTypes)).TrimEnd(',') + ") AND ");
            }
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.OrderCode))
                {
                    query.AppendFormat(" OrderCode LIKE '%{0}%' AND", Utils.ToSqlLike(search.OrderCode));
                }
                if (!string.IsNullOrEmpty(search.SalesmanId))
                {
                    query.AppendFormat(" SellerId = '{0}' AND", search.SalesmanId);
                }
                else if (!string.IsNullOrEmpty(search.Salesman))
                {
                    query.AppendFormat(" SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(search.Salesman));
                }
                if (!string.IsNullOrEmpty(search.CustomerId))
                {
                    query.AppendFormat(" BuyCompanyId = '{0}' AND", search.CustomerId);
                }
                else if (!string.IsNullOrEmpty(search.Customer))
                {
                    query.AppendFormat(" BuyCompanyName LIKE '%{0}%' AND", Utils.ToSqlLike(search.Customer));
                }
                if (search.IsShowInFin)
                {
                    query.Append(" IsCheck = '1' AND");
                }
            }
            if (isShowDistribution)
            {
                query.Append(" IsShowDistribution = '1' AND");
            }
            query.AppendFormat(" CompanyId = '{0}'", companyId);
            query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
                , table, this.CreateXmlSumByField(sumfield), "TourCode,RouteName,OrderCode,BuyCompanyName,Adults,Childs,SellerName,CollectionRefundAmount,Status"
                , query.ToString()
                , "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MDayReceivablesChk
                        {
                            TourCode = dr["TourCode"].ToString(),
                            RouteName =
                                dr.IsDBNull(dr.GetOrdinal("RouteName")) ? "" : dr.GetString(dr.GetOrdinal("RouteName")),
                            OrderCode =
                                dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? "" : dr.GetString(dr.GetOrdinal("OrderCode")),
                            Customer = dr.GetString(dr.GetOrdinal("BuyCompanyName")),
                            Adults = dr.GetInt32(dr.GetOrdinal("Adults")),
                            Childs = dr.GetInt32(dr.GetOrdinal("Childs")),
                            Salesman =
                                dr.IsDBNull(dr.GetOrdinal("SellerName"))
                                    ? ""
                                    : dr.GetString(dr.GetOrdinal("SellerName")),
                            ReceivableAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount")),
                            Status = dr.GetString(dr.GetOrdinal("Status"))
                        };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="mdl">订单销售收款/退款的实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetTourOrderSalesCheck(MTourOrderSales mdl)
        {
            var dc = _db.GetStoredProcCommand("proc_SetTourOrderSalesCheck");
            this._db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this._db.AddInParameter(dc, "@Id", DbType.AnsiStringFixedLength, mdl.Id);
            this._db.AddInParameter(dc, "@ApproverDeptId", DbType.Int32, mdl.ApproverDeptId);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, mdl.ApproveTime);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);
            return Convert.ToInt32(_db.GetParameterValue(dc, "Result")) > 0;
        }

        /// <summary>
        /// 根据团队编号获取导游实收列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MTourOrderSales> GetTourOrderSalesLstByTourId(string tourId)
        {
            var lst = new List<MTourOrderSales>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  Id ,");
            sql.Append("         OrderId");
            sql.Append(" FROM    dbo.tbl_TourOrderSales");
            sql.Append(" WHERE   IsGuideRealIncome = 1");
            sql.Append("         AND CollectionRefundState = 0");
            sql.Append("         AND IsCheck = 0");
            sql.Append("         AND OrderId IN ( SELECT OrderId");
            sql.Append("                          FROM   dbo.tbl_TourOrder");
            sql.Append("                          WHERE  TourId = @TourId");
            sql.Append("                                 AND IsDelete = 0");
            sql.Append("                                 AND Status = @OrderStatus )");

            var dc = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@OrderStatus", DbType.Byte, (int)OrderStatus.已成交);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MTourOrderSales
                    {
                        Id = dr["Id"].ToString(),
                        OrderId = dr["OrderId"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        public IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds)
        {
            var lst = new List<MTourOrderSales>();

            string cmdText = string.Format("SELECT A.Id,A.CollectionRefundDate,A.CollectionRefundOperator,A.CollectionRefundAmount,A.CollectionRefundMode,A.OrderId,A.Memo,(SELECT [Name] FROM tbl_ComPayment AS B WHERE A.CollectionRefundMode=B.PaymentId) AS ZhiFuFangShiMingCheng,(SELECT A1.OrderCode FROM tbl_TourOrder AS A1 WHERE A1.OrderId=A.OrderId) AS OrderCode FROM tbl_TourOrderSales AS A WHERE A.OrderId IN ({0}) AND A.CollectionRefundState = @ShouTuiType AND A.FinStatus = 0 AND A.IsGuideRealIncome='0' AND A.T=0 ", Utils.GetSqlInExpression(orderIds));
            var cmd = _db.GetSqlStringCommand(cmdText);
            _db.AddInParameter(cmd, "ShouTuiType", DbType.AnsiStringFixedLength, (int)EyouSoft.Model.EnumType.TourStructure.CollectionRefundState.收款);

            using (var dr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (dr.Read())
                {
                    lst.Add(new MTourOrderSales
                        {
                            Id = dr["Id"].ToString(),
                            CollectionRefundDate = dr.GetDateTime(dr.GetOrdinal("CollectionRefundDate")),
                            CollectionRefundOperator = dr["CollectionRefundOperator"].ToString(),
                            CollectionRefundAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount")),
                            CollectionRefundMode = dr.GetInt32(dr.GetOrdinal("CollectionRefundMode")),
                            Memo = dr["Memo"].ToString(),
                            OrderId = dr["OrderId"].ToString(),
                            CollectionRefundModeName = dr["ZhiFuFangShiMingCheng"].ToString(),
                            OrderCode = dr["OrderCode"].ToString()
                        });
                }
            }
            return lst;
        }

        #endregion

        #region 杂费收支

        /// <summary>
        /// 添加其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" INSERT  INTO [{0}]", table);
            strSql.Append("         ( [CompanyId] ,");
            strSql.Append("           [TourId] ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("           [AuditDeptId] ,");
            }
            strSql.Append("           [TourCode] ,");
            strSql.Append("           [IsGuide] ,");
            strSql.Append("           [DealTime] ,");
            strSql.Append("           [FeeItem] ,");
            strSql.Append("           [PlanId] ,");
            strSql.Append("           [CrmId] ,");
            strSql.Append("           [Crm] ,");
            strSql.Append("           [ContactName] ,");
            strSql.Append("           [ContactPhone] ,");
            strSql.Append("           [DealerId] ,");
            strSql.Append("           [Dealer] ,");
            strSql.Append("           [FeeAmount] ,");
            strSql.Append("           [Remark] ,");
            strSql.Append("           [PayType] ,");
            strSql.Append("           [PayTypeName] ,");
            if (typ == ItemType.支出)
            {
                //strSql.Append("           [AccountantDeptId] ,");
                //strSql.Append("           [AccountantId] ,");
                //strSql.Append("           [Accountant] ,");
                //strSql.Append("           [PayTime] ,");
                strSql.Append("           [SellerId] ,");
                strSql.Append("           [Seller] ,");
            }
            strSql.Append("           [IsTax] ,");
            strSql.Append("           [Status] ,");
            strSql.Append("           [AuditId] ,");
            strSql.Append("           [Audit] ,");
            strSql.Append("           [AuditRemark] ,");
            strSql.Append("           [AuditTime] ,");
            strSql.Append("           [OperatorDeptId] ,");
            strSql.Append("           [OperatorId] ,");
            strSql.Append("           [Operator] ,");
            strSql.Append("           [IssueTime]");
            strSql.Append("         )");
            strSql.Append(" VALUES  ( @CompanyId ,");
            strSql.Append("           @TourId ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("           @AuditDeptId ,");
            }
            strSql.Append("           @TourCode ,");
            strSql.Append("           @IsGuide ,");
            strSql.Append("           @DealTime ,");
            strSql.Append("           @FeeItem ,");
            strSql.Append("           @PlanId ,");
            strSql.Append("           @CrmId ,");
            strSql.Append("           @Crm ,");
            strSql.Append("           @ContactName ,");
            strSql.Append("           @ContactPhone ,");
            strSql.Append("           @DealerId ,");
            strSql.Append("           @Dealer ,");
            strSql.Append("           @FeeAmount ,");
            strSql.Append("           @Remark ,");
            strSql.Append("           @PayType ,");
            strSql.Append("           @PayTypeName ,");
            if (typ == ItemType.支出)
            {
                //strSql.Append("           @AccountantDeptId ,");
                //strSql.Append("           @AccountantId ,");
                //strSql.Append("           @Accountant ,");
                //strSql.Append("           @PayTime ,");
                strSql.Append("           @SellerId ,");
                strSql.Append("           @Seller ,");
            }
            strSql.Append("           @IsTax ,");
            strSql.Append("           @Status ,");
            strSql.Append("           @AuditId ,");
            strSql.Append("           @Audit ,");
            strSql.Append("           @AuditRemark ,");
            strSql.Append("           @AuditTime ,");
            strSql.Append("           @DeptId ,");
            strSql.Append("           @OperatorId ,");
            strSql.Append("           @Operator ,");
            strSql.Append("           @IssueTime");
            strSql.Append("         )");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@DealTime", DbType.DateTime, mdl.DealTime);
            this._db.AddInParameter(dc, "@FeeItem", DbType.String, mdl.FeeItem);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@CrmId", DbType.AnsiStringFixedLength, mdl.CrmId);
            this._db.AddInParameter(dc, "@Crm", DbType.String, mdl.Crm);
            this._db.AddInParameter(dc, "@ContactName", DbType.String, mdl.ContactName);
            this._db.AddInParameter(dc, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@FeeAmount", DbType.Decimal, mdl.FeeAmount);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@PayType", DbType.Byte, mdl.PayType);
            this._db.AddInParameter(dc, "@PayTypeName", DbType.String, mdl.PayTypeName);
            //this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, mdl.AccountantDeptId);
            //this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, mdl.AccountantId);
            //this._db.AddInParameter(dc, "@Accountant", DbType.String, mdl.Accountant);
            //this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, mdl.PayTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@AuditDeptId", DbType.Int32, mdl.AuditDeptId);
            this._db.AddInParameter(dc, "@AuditId", DbType.AnsiStringFixedLength, mdl.AuditId);
            this._db.AddInParameter(dc, "@Audit", DbType.String, mdl.Audit);
            this._db.AddInParameter(dc, "@AuditRemark", DbType.String, mdl.AuditRemark);
            this._db.AddInParameter(dc, "@AuditTime", DbType.DateTime, mdl.AuditTime);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@SellerId", DbType.AnsiStringFixedLength, mdl.SellerId);
            this._db.AddInParameter(dc, "@Seller", DbType.String, mdl.Seller);
            this._db.AddInParameter(dc, "@IsGuide", DbType.AnsiStringFixedLength, mdl.IsGuide ? "1" : "0");
            this._db.AddInParameter(dc, "@IsTax", DbType.AnsiStringFixedLength, mdl.IsTax ? "1" : "0");

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE  [{0}]", table);
            strSql.Append(" SET");
            strSql.Append("         [DealTime] = @DealTime ,");
            strSql.Append("         [FeeItem] = @FeeItem ,");
            strSql.Append("         [PlanId] = @PlanId ,");
            strSql.Append("         [CrmId] = @CrmId ,");
            strSql.Append("         [Crm] = @Crm ,");
            strSql.Append("         [ContactName] = @ContactName ,");
            strSql.Append("         [ContactPhone] = @ContactPhone ,");
            strSql.Append("         [DealerId] = @DealerId ,");
            strSql.Append("         [Dealer] = @Dealer ,");
            strSql.Append("         [FeeAmount] = @FeeAmount ,");
            strSql.Append("         [Remark] = @Remark ,");
            strSql.Append("         [PayType] = @PayType ,");
            strSql.Append("         [PayTypeName] = @PayTypeName ");
            if (typ == ItemType.支出)
            {
                strSql.Append("         ,[SellerId] = @SellerId");
                strSql.Append("         ,[Seller] = @Seller");
            }
            strSql.Append("         ,[IsTax] = @IsTax");
            strSql.Append(" WHERE   [Id] = @Id AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.AnsiStringFixedLength, mdl.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@DealTime", DbType.DateTime, mdl.DealTime);
            this._db.AddInParameter(dc, "@FeeItem", DbType.String, mdl.FeeItem);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@CrmId", DbType.AnsiStringFixedLength, mdl.CrmId);
            this._db.AddInParameter(dc, "@Crm", DbType.String, mdl.Crm);
            this._db.AddInParameter(dc, "@ContactName", DbType.String, mdl.ContactName);
            this._db.AddInParameter(dc, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@FeeAmount", DbType.Decimal, mdl.FeeAmount);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@PayType", DbType.Byte, mdl.PayType);
            this._db.AddInParameter(dc, "@PayTypeName", DbType.String, mdl.PayTypeName);
            this._db.AddInParameter(dc, "@SellerId", DbType.AnsiStringFixedLength, mdl.SellerId);
            this._db.AddInParameter(dc, "@Seller", DbType.String, mdl.Seller);
            this._db.AddInParameter(dc, "@IsTax", DbType.AnsiStringFixedLength, mdl.IsTax?"1":"0");

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int DelOtherFeeInOut(string companyId, ItemType typ, params int[] ids)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE {0}", table);
            strSql.Append(" SET IsDeleted='1'");
            strSql.Append(" WHERE");
            strSql.Append("     Id IN (" + Utils.GetSqlIdStrByArray(ids) + ")");
            strSql.AppendFormat("     AND CompanyId = '{0}'", companyId);

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        public MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id, string companyId)
        {
            var strSql = new StringBuilder();
            var mdl = new MOtherFeeInOut();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            if (typ == ItemType.收入)
            {
                strSql.Append("         [AuditDeptId] ,");
                strSql.Append("         [TourCode] ,");
            }
            strSql.Append("         [DealTime] ,");
            strSql.Append("         [FeeItem] ,");
            strSql.Append("         [PlanId] ,");
            strSql.Append("         [CrmId] ,");
            strSql.Append("         [Crm] ,");
            strSql.Append("         [ContactName] ,");
            strSql.Append("         [ContactPhone] ,");
            strSql.Append("         [DealerId] ,");
            strSql.Append("         [Dealer] ,");
            strSql.Append("         [FeeAmount] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [PayType] ,");
            strSql.Append("         [PayTypeName] ,");
            if (typ == ItemType.支出)
            {
                strSql.Append("         [AccountantDeptId] ,");
                strSql.Append("         [AccountantId] ,");
                strSql.Append("         [Accountant] ,");
                strSql.Append("         [PayTime] ,");
                strSql.Append("         [SellerId] ,");
                strSql.Append("         [Seller] ,");
            }
            strSql.Append("         [IsTax] ,");
            strSql.Append("         [Status] ,");
            strSql.Append("         [AuditId] ,");
            strSql.Append("         [Audit] ,");
            strSql.Append("         [AuditRemark] ,");
            strSql.Append("         [AuditTime] ,");
            strSql.Append("         [OperatorDeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime]");
            strSql.AppendFormat(" FROM    [{0}]", table);
            strSql.Append(" WHERE   Id = @Id AND CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.TourId = dr.IsDBNull(dr.GetOrdinal("TourId")) ? "" : dr.GetString(dr.GetOrdinal("TourId"));
                    if (typ == ItemType.收入)
                    {
                        mdl.AuditDeptId = dr.GetInt32(dr.GetOrdinal("AuditDeptId"));
                        mdl.TourCode = dr.IsDBNull(dr.GetOrdinal("TourCode")) ? "" : dr.GetString(dr.GetOrdinal("TourCode"));
                    }
                    mdl.DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime"));
                    mdl.FeeItem = dr.IsDBNull(dr.GetOrdinal("FeeItem")) ? "" : dr.GetString(dr.GetOrdinal("FeeItem"));
                    mdl.PlanId = dr.IsDBNull(dr.GetOrdinal("PlanId")) ? "" : dr.GetString(dr.GetOrdinal("PlanId"));
                    mdl.CrmId = dr.IsDBNull(dr.GetOrdinal("CrmId")) ? "" : dr.GetString(dr.GetOrdinal("CrmId"));
                    mdl.Crm = dr.IsDBNull(dr.GetOrdinal("Crm")) ? "" : dr.GetString(dr.GetOrdinal("Crm"));
                    mdl.ContactName = dr["ContactName"].ToString();
                    mdl.ContactPhone = dr["ContactPhone"].ToString();
                    mdl.DealerId = dr["DealerId"].ToString();
                    mdl.Dealer = dr.IsDBNull(dr.GetOrdinal("Dealer")) ? "" : dr.GetString(dr.GetOrdinal("Dealer"));
                    mdl.FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount"));
                    mdl.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    mdl.PayType = dr.GetInt32(dr.GetOrdinal("PayType"));
                    mdl.PayTypeName = dr["PayTypeName"].ToString();
                    if (typ == ItemType.支出)
                    {
                        mdl.AccountantDeptId = dr.GetInt32(dr.GetOrdinal("AccountantDeptId"));
                        mdl.AccountantId = dr["AccountantId"].ToString();
                        mdl.Accountant = dr["Accountant"].ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("PayTime")))
                        {
                            mdl.PayTime = dr.GetDateTime(dr.GetOrdinal("PayTime"));
                        }
                        mdl.SellerId = dr["SellerId"].ToString();
                        mdl.Seller = dr["Seller"].ToString();
                    }
                    mdl.IsTax = dr["IsTax"].ToString() == "1";
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.AuditId = dr["AuditId"].ToString();
                    mdl.Audit = dr["Audit"].ToString();
                    mdl.AuditRemark = dr["AuditRemark"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("AuditTime")))
                    {
                        mdl.AuditTime = dr.GetDateTime(dr.GetOrdinal("AuditTime"));
                    }
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.Operator = dr.IsDBNull(dr.GetOrdinal("Operator")) ? "" : dr.GetString(dr.GetOrdinal("Operator"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量审核
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="auditDeptId">审核人部门编号</param>
        /// <param name="auditId">审核人编号</param>
        /// <param name="audit">审核人</param>
        /// <param name="auditRemark">审核意见</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="status">状态</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeInOutAudit(ItemType typ, int auditDeptId, string auditId, string audit, string auditRemark, DateTime auditTime, FinStatus status, params int[] ids)
        {
            var strSql = new StringBuilder();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";

            strSql.AppendFormat(" UPDATE {0}", table);
            strSql.Append(" SET Status=@Status");
            if (typ == ItemType.收入)
            {
                strSql.Append("     ,AuditDeptId=@AuditDeptId");
            }
            strSql.Append("     ,AuditId=@AuditId");
            strSql.Append("     ,Audit=@Audit");
            strSql.Append("     ,AuditRemark=@AuditRemark");
            strSql.Append("     ,AuditTime=@AuditTime");
            strSql.Append(" WHERE");
            strSql.Append("     Id IN (" + Utils.GetSqlIdStrByArray(ids) + ")");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@AuditDeptId", DbType.Int32, auditDeptId);
            this._db.AddInParameter(dc, "@AuditId", DbType.AnsiStringFixedLength, auditId);
            this._db.AddInParameter(dc, "@Audit", DbType.String, audit);
            this._db.AddInParameter(dc, "@AuditRemark", DbType.String, auditRemark);
            this._db.AddInParameter(dc, "@AuditTime", DbType.DateTime, auditTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, status);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据团队编号获取导游报账时添加的其他收入列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MOtherFeeInOut> GetOtherFeeInLst(string tourId)
        {
            var lst = new List<MOtherFeeInOut>();
            var sql = new StringBuilder();

            sql.Append("select Id from tbl_FinOtherInFee where TourId=@TourId and IsGuide<>0 and Status=@Status and IsDeleted=0");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(cmd, "@Status", DbType.Byte, (int)FinStatus.财务待审批);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MOtherFeeInOut
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                    }
                    );
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据其他（杂费）支出费用编号集合可以批量支付
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="status">状态</param>
        /// <param name="lst">其他（杂费）支出费用编号和支付方式集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeOutPay(int accountantDeptId, string accountantId, string accountant, DateTime payTime, FinStatus status, IList<MBatchPay> lst)
        {
            var strSql = new StringBuilder();

            foreach (var m in lst)
            {
                strSql.Append(" UPDATE tbl_FinOtherOutFee");
                strSql.AppendFormat(" SET Status=@Status");
                strSql.Append("     ,AccountantDeptId=@AccountantDeptId");
                strSql.Append("     ,AccountantId=@AccountantId");
                strSql.Append("     ,Accountant=@Accountant");
                strSql.Append("     ,PayTime=@PayTime");
                strSql.AppendFormat("     ,PayType={0}", m.PaymentType);
                strSql.AppendFormat("     ,PayTypeName='{0}'", m.PaymentTypeName);
                strSql.Append(" WHERE");
                strSql.AppendFormat("     Id = {0}", m.RegisterId);
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, accountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, accountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, payTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, status);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用搜索实体获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mSearch">其他（杂费）收入/支出费用搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ItemType typ
                                                        , MOtherFeeInOutBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            var lst = new List<MOtherFeeInOut>();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append("Id,TourId,DealTime,FeeItem,CrmId,Crm,DealerId,Dealer,OperatorId,Operator,FeeAmount,PayType,PayTypeName,Remark,Status,ContactName,ContactPhone,IsTax");
            field.Append(typ == ItemType.收入 ? ",TourCode" : ",Seller");

            if (!string.IsNullOrEmpty(mSearch.FeeItem))
            {
                query.Append("FeeItem LIKE '%" + Utils.ToSqlLike(mSearch.FeeItem) + "%' AND ");
            }
            if (!string.IsNullOrEmpty(mSearch.CrmId))
            {
                query.AppendFormat("CrmId = '{0}' AND ", mSearch.CrmId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Crm))
            {
                query.AppendFormat("Crm LIKE '%{0}%' AND ", Utils.ToSqlLike(mSearch.Crm));
            }
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat("DealerId = '{0}' AND ", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat("Dealer LIKE '%{0}%' AND ", Utils.ToSqlLike(mSearch.Dealer));
            }
            if (mSearch.DealTimeS.HasValue)
            {
                query.AppendFormat("DealTime >= '{0}' AND ", mSearch.DealTimeS.Value);
            }
            if (mSearch.DealTimeE.HasValue)
            {
                query.AppendFormat("DealTime < '{0}' AND ", mSearch.DealTimeE.Value.AddDays(1));
            }
            if ((int)mSearch.Status != -1)
            {
                query.AppendFormat("Status = {0} AND ", (int)mSearch.Status);
            }
            //if (typ == ItemType.收入)
            //{
            query.AppendFormat("(IsGuide = 0 OR EXISTS(SELECT 1 FROM tbl_Tour WHERE TourId = {1}.TourId AND IsDelete=0 AND TourStatus>{2} AND TourStatus<{0})) AND ", (int)TourStatus.已取消, table,(int)TourStatus.导游报销);
            //}
            query.AppendFormat(" IsDeleted = '0' AND CompanyId = '{0}'", mSearch.CompanyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, table, "Id", field.ToString(), query.ToString(), "DealTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MOtherFeeInOut
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            TourId = dr["TourId"].ToString(),
                            TourCode = typ == ItemType.收入 ? dr["TourCode"].ToString() : "",
                            DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime")),
                            FeeItem = dr["FeeItem"].ToString(),
                            CrmId = dr["CrmId"].ToString(),
                            Crm = dr["Crm"].ToString(),
                            ContactName = dr["ContactName"].ToString(),
                            ContactPhone = dr["ContactPhone"].ToString(),
                            DealerId = dr["DealerId"].ToString(),
                            Dealer = dr["Dealer"].ToString(),
                            FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                            Remark = dr["Remark"].ToString(),
                            PayType = dr.GetInt32(dr.GetOrdinal("PayType")),
                            PayTypeName = dr["PayTypeName"].ToString(),
                            Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                            OperatorId = dr["OperatorId"].ToString(),
                            Operator = dr.GetString(dr.GetOrdinal("Operator")),
                            Seller = typ == ItemType.支出 ? dr["Seller"].ToString() : "",
                            IsTax = dr["IsTax"].ToString()=="1"
                        };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids)
        {
            var lst = new List<MOtherFeeInOut>();
            var table = typ == ItemType.收入 ? "tbl_FinOtherInFee" : "tbl_FinOtherOutFee";
            var sql = new StringBuilder();

            sql.AppendFormat("select Id,DealTime,FeeItem,CrmId,Crm,Dealer,FeeAmount,PayType,PayTypeName,Remark,Status,AuditTime,Audit,AuditRemark from {0}", table);
            sql.AppendFormat(" where Id IN ({0})", Utils.GetSqlIdStrByList(ids));

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MOtherFeeInOut
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        DealTime = dr.GetDateTime(dr.GetOrdinal("DealTime")),
                        FeeItem = dr["FeeItem"].ToString(),
                        CrmId = dr["CrmId"].ToString(),
                        Crm = dr["Crm"].ToString(),
                        Dealer = dr["Dealer"].ToString(),
                        FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                        Remark = dr["Remark"].ToString(),
                        PayType = dr.GetInt32(dr.GetOrdinal("PayType")),
                        PayTypeName = dr["PayTypeName"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        Audit = dr["Audit"].ToString(),
                        AuditRemark = dr["AuditRemark"].ToString()
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("AuditTime")))
                    {
                        mdl.AuditTime = dr.GetDateTime(dr.GetOrdinal("AuditTime"));
                    }
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        #endregion

        #region 应付管理

        /// <summary>
        /// 根据应付帐款搜索实体获取应付帐款/已结清账款列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应付帐款/已结清账款列表</returns>
        public IList<MPayable> GetPayableLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayableBase mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            const string table = "tbl_Plan";
            var unChecked = string.Format("UnChecked=(select isnull(sum(PaymentAmount),0) from tbl_finregister where planid=tbl_plan.planid and isdeleted='0' and status!={0})", (int)FinStatus.账务已支付);
            var sumfield = new StringBuilder();
            var lst = new List<MPayable>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            sumfield.Append(" SELECT  ISNULL(SUM(Confirmation), 0) Confirmation ,");
            sumfield.Append("         ISNULL(SUM(Prepaid), 0) Prepaid ,");
            sumfield.Append("         ISNULL(SUM(UnChecked), 0) UnChecked");
            sumfield.Append(" FROM    ( SELECT    Confirmation ,");
            sumfield.Append("                     TourId ,");
            sumfield.Append("                     PlanId ,IssueTime,");
            sumfield.Append("                     SourceId ,");
            sumfield.Append("                     SourceName ,");
            sumfield.Append("                     OperatorId ,");
            sumfield.Append("                     Operator ,");
            sumfield.Append("                     CostStatus ,IsDelete,");
            sumfield.Append("                     [Type] ,");
            sumfield.Append("                     Status ,");
            sumfield.Append("                     CompanyId ,");
            sumfield.Append("                     Prepaid ,");
            sumfield.AppendFormat("                     {0}", unChecked);
            sumfield.Append("           FROM      dbo.tbl_Plan");
            sumfield.Append("         ) tbl_plan");

            field.Append(" 	[Type]");
            field.Append(" 	,XMLTour=(SELECT T.TourId,T.TourCode,T.RouteName,CONVERT(VARCHAR(19),T.LDate,120) LDate,T.SellerName,U.ContactTel,U.ContactFax,U.ContactMobile,U.QQ,T.TourStatus");
            field.Append(" 			  FROM tbl_Tour T LEFT JOIN tbl_ComUser U ON T.SellerId=U.UserId");
            field.Append(" 			  WHERE T.TourId=tbl_Plan.TourId FOR XML RAW,ROOT)");
            field.Append(" 	,SourceName");
            field.Append(" 	,Num");
            field.Append(" 	,Operator");
            field.Append(" 	,CostStatus,CostRemarks");
            field.Append(" 	,PaymentType");
            field.Append(" 	,Confirmation");
            field.Append(" 	,Prepaid");
            field.Append(" 	,PlanId,IssueTime");
            field.AppendFormat("  ,{0}", unChecked);

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour ");
            query.Append(" 		   WHERE");
            //线路区域编号
            if (mSearch.AreaId > 0)
            {
                query.AppendFormat(" 				AreaId = {0} AND", mSearch.AreaId);
            }
            ////供应商编号
            //if (!string.IsNullOrEmpty(mSearch.SupplierId))
            //{
            //    query.AppendFormat(" 				SourceId = '{0}' AND", mSearch.SupplierId);
            //}
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.SalesmanId))
            {
                query.AppendFormat(" 				SellerId = '{0}' AND", mSearch.SalesmanId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Salesman))
            {
                query.AppendFormat(" 				SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Salesman));
            }
            if (!string.IsNullOrEmpty(mSearch.LDateStart))
            {
                query.AppendFormat(" 				LDate >= '{0}' AND", mSearch.LDateStart);
            }
            if (!string.IsNullOrEmpty(mSearch.LDateEnd))
            {
                query.AppendFormat(" 				LDate <= '{0}' AND", mSearch.LDateEnd);
            }
            query.Append(" 				IsDelete='0' AND");
            query.AppendFormat(" 				TourId=tbl_Plan.TourId AND TourStatus <> {0}) AND", (int)TourStatus.已取消);
            if (!string.IsNullOrEmpty(mSearch.PaymentDateS) || !string.IsNullOrEmpty(mSearch.PaymentDateE))
            {
                query.Append(" 	EXISTS(SELECT 1 FROM tbl_FinRegister");
                query.Append(" 		   WHERE	");
                if (!string.IsNullOrEmpty(mSearch.PaymentDateS))
                {
                    query.AppendFormat(" 				PaymentDate >= '{0}' AND", mSearch.PaymentDateS);
                }
                if (!string.IsNullOrEmpty(mSearch.PaymentDateE))
                {
                    query.AppendFormat(" 				PaymentDate < '{0}' AND", Utils.GetDateTime(mSearch.PaymentDateE).AddDays(1));
                }
                query.Append(" 				PlanId=tbl_Plan.PlanId) AND");
            }
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 	SourceId = '{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 	SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            if (!string.IsNullOrEmpty(mSearch.PlanerId))
            {
                query.AppendFormat(" 	OperatorId = '{0}' AND", mSearch.PlanerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Planer))
            {
                query.AppendFormat(" 	Operator LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Planer));
            }
            if (mSearch.Paid.HasValue)
            {
                query.AppendFormat(" 	Prepaid {0} {1} AND", GetEqualSign(mSearch.SignPaid), mSearch.Paid.Value);
            }
            if (mSearch.Unpaid.HasValue)
            {
                query.AppendFormat(" 	Confirmation-Prepaid {0} {1} AND", GetEqualSign(mSearch.SignUnpaid), mSearch.Unpaid.Value);
            }
            if (mSearch.IsClean)
            {
                query.Append(" 	Confirmation = Prepaid AND");
            }
            else
            {
                query.Append(" 	Confirmation <> Prepaid AND");
            }
            if (mSearch.IsConfirmed.HasValue)
            {
                query.AppendFormat(" 	CostStatus = '{0}' AND", mSearch.IsConfirmed.Value ? "1" : "0");
            }
            if (mSearch.PlanItem.HasValue)
            {
                query.AppendFormat(" 	[Type]={0} AND", (int)mSearch.PlanItem.Value);
            }
            query.AppendFormat(" IsDelete = 0 AND Status={0} AND CompanyId='{1}' AND [Type]<>{2}", (int)PlanState.已落实, mSearch.CompanyId, (int)PlanProject.购物);
            //供应商平台判断
            if (!mSearch.IsDj)
            {
                query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_Plan.TourId");
                query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
                query.Append(")");
            }

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, sumfield.ToString(), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MPayable
                    {
                        TourId = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourId"),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode"),
                        RouteName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "RouteName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "LDate")),
                        PlanItem = (PlanProject?)dr.GetByte(dr.GetOrdinal("Type")),
                        Supplier = dr["SourceName"].ToString(),
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        Salesman = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SellerName"),
                        Tel = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactTel"),
                        Mobile = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactMobile"),
                        Fax = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "ContactFax"),
                        QQ = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "QQ"),
                        TourStatus = (TourStatus)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourStatus")),
                        Planer = dr["Operator"].ToString(),
                        IsConfirmed = dr.GetString(dr.GetOrdinal("CostStatus")) == "1",
                        CostRemarks = dr["CostRemarks"].ToString(),
                        Payable = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                        Paid = dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        Unpaid = dr.GetDecimal(dr.GetOrdinal("Confirmation")) - dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        PlanId = dr["PlanId"].ToString(),
                        UnChecked = dr.GetDecimal(dr.GetOrdinal("UnChecked")),
                        PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType")),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据搜索实体获取当天付款对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>当天付款对账列表</returns>
        public IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayRegister mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            const string table = "tbl_FinRegister";
            var lst = new List<MRegister>();
            var filed = new StringBuilder();
            var query = new StringBuilder();
            var sumfield = new[] { "PaymentAmount" };

            filed.Append(" 	XMLTour=(SELECT TourCode,SellerName FROM tbl_Tour WHERE TourId=tbl_FinRegister.TourId FOR XML RAW,ROOT)");
            filed.Append(" 	,XMLPlan=(SELECT [Type],SourceName,Operator FROM tbl_Plan WHERE PlanId=tbl_FinRegister.PlanId FOR XML RAW,ROOT)");
            filed.Append(" 	,Dealer");
            filed.Append(" 	,PaymentAmount");
            filed.Append(" 	,PaymentType");
            filed.Append(" 	,PaymentName=(SELECT Name FROM tbl_ComPayment WHERE PaymentId=PaymentType)");

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour ");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            query.Append(" 				TourId=tbl_FinRegister.TourId) AND");
            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Plan ");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 		   SourceId='{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 		   SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            query.Append(" 		   PlanId=tbl_FinRegister.PlanId) AND");
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat(" 	DealerId='{0}' AND", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat(" 	Dealer LIKE '%{0}' AND", Utils.ToSqlLike(mSearch.Dealer));
            }
            query.AppendFormat(" 	Status={0} AND CONVERT(VARCHAR(10),PayTime,120)=CONVERT(VARCHAR(10),GETDATE(),120) AND", (int)FinStatus.账务已支付);
            query.AppendFormat(" 	CompanyId='{0}'", mSearch.CompanyId);
            query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_FinRegister.TourId");
            query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
            query.Append(")");

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, CreateXmlSumByField(sumfield), filed.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MRegister
                        {
                            PlanTyp = (PlanProject)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "Type")),
                            TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode"),
                            Supplier = Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "SourceName"),
                            Salesman = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SellerName"),
                            Planer = Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "Operator"),
                            Dealer = dr["Dealer"].ToString(),
                            PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                            PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType")),
                            PaymentName = dr["PaymentName"].ToString()
                        });
                }
            }

            return lst;
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目登记基本信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>某一个支出项目登记基本信息</returns>
        public MPayRegister GetPayRegisterBaseByPlanId(string planId)
        {
            var mdl = new MPayRegister();
            var sql = new StringBuilder();

            sql.Append("SELECT TourId,PlanId,[Type],SourceName,Confirmation,Prepaid,PaymentType,IssueTime,(select t.tourcode from tbl_tour t where t.tourid=tbl_plan.tourid) as tourcode,Register=(SELECT ISNULL(SUM(PaymentAmount),0) FROM tbl_FinRegister WHERE IsDeleted='0' AND PlanId=tbl_Plan.PlanId) FROM tbl_Plan WHERE PlanId = @PlanId");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@PlanId", DbType.AnsiStringFixedLength, planId);

            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.PlanTyp = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.Supplier = dr["SourceName"].ToString();
                    mdl.Payable = dr.GetDecimal(dr.GetOrdinal("Confirmation"));
                    mdl.Paid = dr.GetDecimal(dr.GetOrdinal("Prepaid"));
                    mdl.Register = dr.GetDecimal(dr.GetOrdinal("Register"));
                    mdl.PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.TourCode = dr["TourCode"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据计调编号获取某一个计调项目的出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(string planId, bool? isPrepaid)
        {
            var lst = new List<MRegister>();
            var strSql = new StringBuilder();

            strSql.Append(" SELECT");
            strSql.Append(" 	RegisterId");
            strSql.Append(" 	,PaymentDate");
            strSql.Append(" 	,DealerId");
            strSql.Append(" 	,Dealer");
            strSql.Append(" 	,PaymentAmount");
            strSql.Append(" 	,PaymentType,(select p.Name from tbl_ComPayment p where p.PaymentId=tbl_FinRegister.PaymentType) as PaymentName");
            strSql.Append(" 	,Deadline");
            strSql.Append(" 	,Remark");
            strSql.Append(" 	,Operator");
            strSql.Append(" 	,IssueTime");
            strSql.Append(" 	,SourceName");
            strSql.Append(" 	,Status,PayTime");
            strSql.Append(" FROM");
            strSql.Append(" 	tbl_FinRegister");
            strSql.Append(" WHERE");
            strSql.Append(" 	PlanId=@PlanId AND IsDeleted='0'");
            if (isPrepaid.HasValue)
            {
                strSql.Append(" 	AND IsPrepaid=@IsPrepaid");
            }
            strSql.Append(" ORDER BY");
            strSql.Append("     IssueTime DESC");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            if (isPrepaid.HasValue)
            {
                this._db.AddInParameter(dc, "@IsPrepaid", DbType.AnsiStringFixedLength, isPrepaid.Value ? "1" : "0");
            }

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MRegister
                    {
                        RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId")),
                        PaymentDate = dr.IsDBNull(dr.GetOrdinal("PaymentDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PaymentDate")),
                        DealerId = dr["DealerId"].ToString(),
                        Dealer = dr["Dealer"].ToString(),
                        PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                        PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType")),
                        Deadline = dr.IsDBNull(dr.GetOrdinal("Deadline")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline")),
                        Remark = dr["Remark"].ToString(),
                        Supplier = dr["SourceName"].ToString(),
                        Operator = dr["Operator"].ToString(),
                        IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        PayTime = Utils.GetDateTimeNullable(dr["PayTime"].ToString()),
                        PaymentName = dr["PaymentName"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款 2：预存款余额不足</returns>
        public int AddRegister(MRegister mdl)
        {
            var dc = this._db.GetStoredProcCommand("proc_FinRegister_Add");

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this._db.AddInParameter(dc, "@PaymentDate", DbType.DateTime, mdl.PaymentDate);
            this._db.AddInParameter(dc, "@Deadline", DbType.DateTime, mdl.Deadline);
            this._db.AddInParameter(dc, "@DealerDeptId", DbType.Int32, mdl.DealerDeptId);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@PaymentAmount", DbType.Decimal, mdl.PaymentAmount);
            this._db.AddInParameter(dc, "@PaymentType", DbType.Int32, mdl.PaymentType);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, mdl.ApproverTime);
            this._db.AddInParameter(dc, "@ApproveRemark", DbType.String, mdl.ApproveRemark);
            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, mdl.AccountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, mdl.AccountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, mdl.Accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, mdl.PayTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IsDeleted", DbType.AnsiStringFixedLength, mdl.IsDeleted ? "1" : "0");
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@IsPrepaid", DbType.AnsiStringFixedLength, mdl.IsPrepaid ? "1" : "0");
            this._db.AddInParameter(dc, "@IsAuto", DbType.AnsiStringFixedLength, "0");
            this._db.AddInParameter(dc, "@IsGuide", DbType.AnsiStringFixedLength, "0");

            return DbHelper.RunProcedureWithResult(dc, this._db);
        }

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int UpdRegister(MRegister mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" DECLARE @PlanId CHAR(36)");
            strSql.Append(" SELECT  @PlanId = PlanId");
            strSql.Append(" FROM    dbo.tbl_FinRegister");
            strSql.Append(" WHERE   CompanyId = @CompanyId");
            strSql.Append("         AND RegisterId = @RegisterId");
            strSql.Append(" IF ( SELECT ISNULL(SUM(PaymentAmount), 0)");
            strSql.Append("      FROM   tbl_FinRegister");
            strSql.Append("      WHERE  IsDeleted = '0'");
            strSql.Append("             AND PlanId = @PlanId AND RegisterId <> @RegisterId");
            strSql.Append("    ) + @PaymentAmount > ( SELECT    Confirmation");
            strSql.Append("                           FROM      dbo.tbl_Plan");
            strSql.Append("                           WHERE     PlanId = @PlanId");
            strSql.Append("                         ) ");
            strSql.Append("     BEGIN");
            strSql.Append("         SELECT -1");
            strSql.Append("     END");
            strSql.Append(" ELSE ");
            strSql.Append("     BEGIN");
            strSql.Append("         UPDATE  [tbl_FinRegister]");
            strSql.Append("         SET     [PaymentDate] = @PaymentDate ,");
            strSql.Append("                 [Deadline] = @Deadline ,");
            strSql.Append("                 [DealerDeptId] = @DealerDeptId ,");
            strSql.Append("                 [DealerId] = @DealerId ,");
            strSql.Append("                 [Dealer] = @Dealer ,");
            strSql.Append("                 [PaymentAmount] = @PaymentAmount ,");
            strSql.Append("                 [PaymentType] = @PaymentType ,");
            strSql.Append("                 [Remark] = @Remark");
            strSql.Append("         WHERE   CompanyId = @CompanyId");
            strSql.Append("                 AND RegisterId = @RegisterId");
            strSql.Append("     END");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.Int32, mdl.RegisterId);
            this._db.AddInParameter(dc, "@PaymentDate", DbType.DateTime, mdl.PaymentDate);
            this._db.AddInParameter(dc, "@Deadline", DbType.DateTime, mdl.Deadline);
            this._db.AddInParameter(dc, "@DealerDeptId", DbType.Int32, mdl.DealerDeptId);
            this._db.AddInParameter(dc, "@DealerId", DbType.AnsiStringFixedLength, mdl.DealerId);
            this._db.AddInParameter(dc, "@Dealer", DbType.String, mdl.Dealer);
            this._db.AddInParameter(dc, "@PaymentAmount", DbType.Decimal, mdl.PaymentAmount);
            this._db.AddInParameter(dc, "@PaymentType", DbType.Int32, mdl.PaymentType);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        public bool DelRegister(string companyId, int registerId)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE [tbl_FinRegister] SET IsDeleted='1'");
            strSql.Append(" WHERE CompanyId = @CompanyId AND RegisterId = @RegisterId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.Int32, registerId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>付款审批列表</returns>
        public IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , MPayableApproveBase mSearch
                                                        , string operatorId
                                                        , params int[] deptIds)
        {
            const string table = "tbl_FinRegister";
            var sumfield = new[] { "PaymentAmount" };
            var lst = new List<MPayableApprove>();
            var field = new StringBuilder();
            var query = new StringBuilder();

            field.Append(" 	XMLPlan=(SELECT [Type],SourceId,SourceName,Operator");
            field.Append(" 			 FROM tbl_Plan WHERE PlanId=tbl_FinRegister.PlanId FOR XML RAW,ROOT)");
            field.Append(" 	,XMLTour=(SELECT TourCode,CONVERT(VARCHAR(19),LDate,120) LDate,SellerName");
            field.Append(" 			 FROM tbl_Tour WHERE TourId=tbl_FinRegister.TourId FOR XML RAW,ROOT)");
            field.Append(" 	,Dealer");
            field.Append(" 	,PaymentAmount");
            field.Append(" 	,Deadline");
            field.Append(" 	,Remark");
            field.Append(" 	,RegisterId");
            field.Append(" 	,Status");
            field.Append(" 	,TourId");
            field.Append(" 	,IsGuide ");

            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Tour");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" 				TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.SellerName))
            {
                query.AppendFormat(" 				SellerName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.SellerName));
            }
            if (!string.IsNullOrEmpty(mSearch.AreaName))
            {
                query.AppendFormat(" 				EXISTS(SELECT 1 FROM tbl_ComArea WHERE AreaId=tbl_Tour.AreaId AND AreaName LIKE '%{0}%') AND", Utils.ToSqlLike(mSearch.AreaName));
            }
            query.AppendFormat(" 				TourId=tbl_FinRegister.TourId AND IsDelete = '0' AND TourStatus < {0}", (int)TourStatus.已取消);
            if (mSearch.IsPrepaidConfirm)
            {
                query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));
            }
            query.Append(" 				) AND");
            query.Append(" 	EXISTS(SELECT 1 FROM tbl_Plan");
            query.Append(" 		   WHERE");
            if (!string.IsNullOrEmpty(mSearch.SupplierId))
            {
                query.AppendFormat(" 				SourceId='{0}' AND", mSearch.SupplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Supplier))
            {
                query.AppendFormat(" 				SourceName LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Supplier));
            }
            if (mSearch.PlanTyp.HasValue)
            {
                query.AppendFormat(" 				[Type]={0} AND", (int)mSearch.PlanTyp.Value);
            }
            query.AppendFormat(" 				PlanId=tbl_FinRegister.PlanId AND [Type]<>{0}) AND", (int)PlanProject.购物);
            if (!string.IsNullOrEmpty(mSearch.DealerId))
            {
                query.AppendFormat(" 	DealerId='{0}' AND", mSearch.DealerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Dealer))
            {
                query.AppendFormat(" 	Dealer LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Dealer));
            }
            if (!string.IsNullOrEmpty(mSearch.PaymentDateS))
            {
                query.AppendFormat(" 	PaymentDate>='{0}' AND", mSearch.PaymentDateS);
            }
            if (!string.IsNullOrEmpty(mSearch.PaymentDateE))
            {
                query.AppendFormat(" 	PaymentDate<'{0}' AND", Utils.GetDateTime(mSearch.PaymentDateE).AddDays(1));
            }
            if (!string.IsNullOrEmpty(mSearch.DeadlineS))
            {
                query.AppendFormat(" 	Deadline>='{0}' AND", mSearch.DeadlineS);
            }
            if (!string.IsNullOrEmpty(mSearch.DeadlineE))
            {
                query.AppendFormat(" 	Deadline<'{0}' AND", Utils.GetDateTime(mSearch.DeadlineE).AddDays(1));
            }
            if (mSearch.Status.HasValue)
            {
                query.AppendFormat(" 	Status={0} AND", (int)mSearch.Status.Value);
            }
            else
            {
                query.AppendFormat(" 	Status<>{0} AND", (int)FinStatus.销售待确认);
            }
            if (mSearch.IsPrepaidConfirm)
            {
                query.Append(" 	IsPrepaid='1' AND");
            }
            query.AppendFormat(" 	IsDeleted='0' AND CompanyId='{0}'", mSearch.CompanyId);
            if (!mSearch.IsPrepaidConfirm)
            {
                query.Append(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner WHERE TourId=tbl_FinRegister.TourId");
                query.Append(GetOrgCondition(operatorId, deptIds, "PlanerId", "DeptId"));
                query.Append(")");
            }
            //else
            //{
            //    query.Append(GetOrgCondition(operatorId, deptIds, "SellerId", "DeptId"));
            //}

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum, table, this.CreateXmlSumByField(sumfield), field.ToString(), query.ToString(), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MPayableApprove
                    {
                        TourId = dr["TourId"].ToString(),
                        RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId")),
                        PlanTyp = (PlanProject)Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLPlan"].ToString(), "Type")),
                        TourCode = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "TourCode"),
                        SellerName = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "SellerName"),
                        LDate = Utils.GetDateTime(Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLTour")), "LDate")),
                        SupplierId = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "SourceId"),
                        Supplier = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "SourceName"),
                        Planer = Utils.GetValueFromXmlByAttribute(dr.GetString(dr.GetOrdinal("XMLPlan")), "Operator"),
                        Dealer = dr["Dealer"].ToString(),
                        PayAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount")),
                        PayExpire = dr.IsDBNull(dr.GetOrdinal("Deadline")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline")),
                        Remark = dr.GetString(dr.GetOrdinal("Remark")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        IsDaoYouXianFu = dr["IsGuide"].ToString() == "1"
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据登记编号获取登记实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>登记实体</returns>
        public MRegister GetRegisterById(string companyId, int registerId)
        {
            var strSql = new StringBuilder();
            var mdl = new MRegister();

            strSql.Append(" SELECT");
            strSql.Append(" 	R.RegisterId");
            strSql.Append(" 	,R.TourId");
            strSql.Append(" 	,R.PaymentDate");
            strSql.Append(" 	,R.Dealer");
            strSql.Append(" 	,R.PaymentAmount");
            strSql.Append(" 	,R.PaymentType");
            strSql.Append(" 	,C.[Name] PaymentName");
            strSql.Append(" 	,R.Deadline");
            strSql.Append(" 	,R.Remark");
            strSql.Append(" 	,R.Status");
            strSql.Append(" 	,R.ApproveTime");
            strSql.Append(" 	,R.Approver");
            strSql.Append(" 	,R.ApproveRemark");
            strSql.Append(" 	,R.Operator");
            strSql.Append(" 	,R.IssueTime");
            strSql.Append(" 	,R.Accountant");
            strSql.Append(" 	,R.PayTime");
            strSql.Append(" 	,R.IsPrepaid");
            strSql.Append(" 	,P.[Type]");
            strSql.Append(" 	,P.SourceName");
            strSql.Append(" FROM");
            strSql.Append(" 	tbl_FinRegister R");
            strSql.Append(" LEFT OUTER JOIN");
            strSql.Append("     tbl_Plan P");
            strSql.Append(" ON");
            strSql.Append("     R.PlanId=P.PlanId");
            strSql.Append(" LEFT OUTER JOIN");
            strSql.Append("     tbl_ComPayment C");
            strSql.Append(" ON");
            strSql.Append("     C.PaymentId=R.PaymentType");
            strSql.Append(" WHERE");
            strSql.Append(" 	R.CompanyId=@CompanyId");
            strSql.Append(" 	AND R.RegisterId=@RegisterId");
            strSql.Append(" ORDER BY");
            strSql.Append("     R.IssueTime DESC");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@RegisterId", DbType.AnsiStringFixedLength, registerId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.Accountant = dr["Accountant"].ToString();
                    mdl.PayTime = dr.IsDBNull(dr.GetOrdinal("PayTime")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PayTime"));
                    mdl.Approver = dr["Approver"].ToString();
                    mdl.ApproveRemark = dr["ApproveRemark"].ToString();
                    mdl.IsPrepaid = dr.GetString(dr.GetOrdinal("IsPrepaid")) == "1";
                    mdl.RegisterId = dr.GetInt32(dr.GetOrdinal("RegisterId"));
                    mdl.PaymentDate = dr.IsDBNull(dr.GetOrdinal("PaymentDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("PaymentDate"));
                    mdl.Dealer = dr["Dealer"].ToString();
                    mdl.PaymentAmount = dr.GetDecimal(dr.GetOrdinal("PaymentAmount"));
                    mdl.PaymentType = dr.GetInt32(dr.GetOrdinal("PaymentType"));
                    mdl.PaymentName = dr["PaymentName"].ToString();
                    mdl.Deadline = dr.IsDBNull(dr.GetOrdinal("Deadline"))
                                       ? null
                                       : (DateTime?)dr.GetDateTime(dr.GetOrdinal("Deadline"));
                    mdl.Remark = dr["Remark"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.ApproverTime = dr.IsDBNull(dr.GetOrdinal("ApproveTime")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("ApproveTime"));
                    mdl.PlanTyp = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.Supplier = dr["SourceName"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据登记编号集合设置登记审核状态
        /// </summary>
        /// <param name="approverId">审核人编号</param>
        /// <param name="approver">审核人</param>
        /// <param name="approveTime">审核时间</param>
        /// <param name="approveRemark">审核意见</param>
        /// <param name="status">状态</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerIds">登记编号集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        public int SetRegisterApprove(string approverId
                                    , string approver
                                    , DateTime approveTime
                                    , string approveRemark
                                    , FinStatus status
                                    , string companyId
                                    , params int[] registerIds)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinRegister");
            strSql.Append(" SET Status=@Status");
            strSql.Append("  ,ApproverId=@ApproverId");
            strSql.Append("  ,Approver=@Approver");
            strSql.Append("  ,ApproveTime=@ApproveTime");
            strSql.Append("  ,ApproveRemark=@ApproveRemark");
            strSql.Append(" WHERE");
            strSql.Append("     CompanyId = @CompanyId AND");
            strSql.Append("     RegisterId IN (" + Utils.GetSqlIdStrByArray(registerIds) + ")");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)status);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, approverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, approver);
            this._db.AddInParameter(dc, "@ApproveTime", DbType.DateTime, approveTime);
            this._db.AddInParameter(dc, "@ApproveRemark", DbType.String, approveRemark);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db);
        }

        /// <summary>
        /// 根据登记编号集合设置支付状态
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="lst">批量支付列表</param>
        /// <returns>1：成功 0：失败 2：预存款余额不足</returns>
        public int SetRegisterPay(int accountantDeptId
                                , string accountantId
                                , string accountant
                                , DateTime payTime
                                , string companyId
                                , IList<MBatchPay> lst)
        {
            var dc = this._db.GetStoredProcCommand("proc_SetRegisterPay");

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@AccountantDeptId", DbType.Int32, accountantDeptId);
            this._db.AddInParameter(dc, "@AccountantId", DbType.AnsiStringFixedLength, accountantId);
            this._db.AddInParameter(dc, "@Accountant", DbType.String, accountant);
            this._db.AddInParameter(dc, "@PayTime", DbType.DateTime, payTime);
            this._db.AddInParameter(dc, "@XMLBatchpay", DbType.Xml, GetBatchPayXml(lst));
            this._db.AddOutParameter(dc,"@Result",DbType.Int32,1);

            return DbHelper.RunProcedureWithResult(dc, this._db);
        }

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        public MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ)
        {
            var strSql = new StringBuilder();
            var mdl = new MPlanCostPay();

            strSql.Append(" SELECT");
            strSql.Append("         ISNULL(SUM(Prepaid),0) Paid,");
            strSql.Append("         ISNULL(SUM(Confirmation)-SUM(Prepaid),0) Unpaid");
            strSql.Append(" FROM    tbl_Plan");
            strSql.Append(" WHERE   TourId = @TourId");
            strSql.Append("         AND Type = @Type");
            strSql.AppendFormat("   AND Status={0}", (int)PlanState.已落实);


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@Type", DbType.Byte, (int)typ);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Paid = dr.GetDecimal(dr.GetOrdinal("Paid"));
                    mdl.Unpaid = dr.GetDecimal(dr.GetOrdinal("Unpaid"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号、计调类型、是否确认获取计调成本确认列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <param name="isConfirmed">是否确认</param>
        /// <returns>计调成本确认列表</returns>
        public IList<MPlanCostConfirm> GetPlanCostConfirmLst(string tourId, PlanProject typ, bool isConfirmed)
        {
            var strSql = new StringBuilder();
            var lst = new List<MPlanCostConfirm>();

            strSql.Append(" SELECT");
            strSql.Append("         ROW_NUMBER() OVER(ORDER BY PlanId) AS Num,");
            strSql.Append("         SourceName,");
            strSql.Append("         Confirmation");
            strSql.Append(" FROM    tbl_Plan");
            strSql.Append(" WHERE   TourId = @TourId");
            strSql.Append("         AND Type = @Type");
            strSql.Append("         AND CostStatus=@CostStatus");
            strSql.AppendFormat("   AND Status={0}", (int)PlanState.已落实);


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this._db.AddInParameter(dc, "@Type", DbType.Byte, (int)typ);
            this._db.AddInParameter(dc, "@CostStatus", DbType.AnsiStringFixedLength, isConfirmed ? "1" : "0");

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanCostConfirm
                    {
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        Supplier = dr.IsDBNull(dr.GetOrdinal("SourceName")) ? "" : dr.GetString(dr.GetOrdinal("SourceName")),
                        Cost = dr.GetDecimal(dr.GetOrdinal("Confirmation"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据计调编号设置计调成本确认
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="costId">成本确认人ID</param>
        /// <param name="costName">成本确认人</param>
        /// <param name="costRemark">成本确认备注</param>
        /// <param name="confirmation">确认金额/结算金额</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetPlanCostConfirmed(string planId, string costId, string costName, string costRemark, decimal confirmation)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_Plan");
            strSql.Append(" SET");
            strSql.Append("         CostId=@CostId,");
            strSql.Append("         CostName=@CostName,");
            strSql.Append("         CostStatus='1',");
            //strSql.Append("         Confirmation=@Confirmation,");
            strSql.Append("         CostRemarks=@CostRemark,");
            strSql.Append("         CostTime=GETDATE()");
            strSql.Append(" WHERE   PlanId = @PlanId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            this._db.AddInParameter(dc, "@CostId", DbType.AnsiStringFixedLength, costId);
            this._db.AddInParameter(dc, "@CostName", DbType.AnsiStringFixedLength, costName);
            this._db.AddInParameter(dc, "@CostRemark", DbType.String, costRemark);
            this._db.AddInParameter(dc, "@Confirmation", DbType.Decimal, confirmation);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdDebit(MDebit mdl)
        {
            var strSql = new StringBuilder();

            if (mdl.Id > 0)
            {
                strSql.Append(" UPDATE [tbl_FinDebit]");
                strSql.Append("    SET [BorrowerId] = @BorrowerId");
                strSql.Append("       ,[Borrower] = @Borrower");
                strSql.Append("       ,[BorrowTime] = @BorrowTime");
                strSql.Append("       ,[BorrowAmount] = @BorrowAmount");
                strSql.Append("       ,[UseFor] = @UseFor");
                strSql.Append("       ,[PreSignNum] = @PreSignNum");
                strSql.Append("  WHERE Id=@Id AND CompanyId=@CompanyId");
            }
            else
            {
                strSql.Append(" INSERT  INTO [tbl_FinDebit]");
                strSql.Append("         ( [CompanyId] ,");
                strSql.Append("           [TourId] ,");
                strSql.Append("           [TourCode] ,");
                strSql.Append("           [BorrowerId] ,");
                strSql.Append("           [Borrower] ,");
                strSql.Append("           [BorrowTime] ,");
                strSql.Append("           [BorrowAmount] ,");
                strSql.Append("           [UseFor] ,");
                strSql.Append("           [PreSignNum],");
                strSql.Append("           [OperatorId],");
                strSql.Append("           [Operator],");
                strSql.Append("           [OperatorDeptId],");
                strSql.Append("           [IssueTime]");
                strSql.Append("         )");
                strSql.Append(" VALUES  ( @CompanyId ,");
                strSql.Append("           @TourId ,");
                strSql.Append("           @TourCode ,");
                strSql.Append("           @BorrowerId ,");
                strSql.Append("           @Borrower ,");
                strSql.Append("           @BorrowTime ,");
                strSql.Append("           @BorrowAmount ,");
                strSql.Append("           @UseFor ,");
                strSql.Append("           @PreSignNum,");
                strSql.Append("           @OperatorId,");
                strSql.Append("           @Operator,");
                strSql.Append("           @OperatorDeptId,");
                strSql.Append("           @IssueTime");
                strSql.Append("         )");
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, mdl.TourCode);
            this._db.AddInParameter(dc, "@BorrowerId", DbType.AnsiStringFixedLength, mdl.BorrowerId);
            this._db.AddInParameter(dc, "@Borrower", DbType.String, mdl.Borrower);
            this._db.AddInParameter(dc, "@BorrowTime", DbType.DateTime, mdl.BorrowTime);
            this._db.AddInParameter(dc, "@BorrowAmount", DbType.Decimal, mdl.BorrowAmount);
            this._db.AddInParameter(dc, "@UseFor", DbType.String, mdl.UseFor);
            this._db.AddInParameter(dc, "@PreSignNum", DbType.Int32, mdl.PreSignNum);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this._db.AddInParameter(dc, "@OperatorDeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetDebitApprove(MDebit mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE  [tbl_FinDebit]");
            strSql.Append(" SET     ");
            strSql.Append("         [Status] = @Status ,");
            strSql.Append("         [ApproverId] = @ApproverId ,");
            strSql.Append("         [Approver] = @Approver ,");
            strSql.Append("         [ApproveDate] = @ApproveDate ,");
            strSql.Append("         [Approval] = @Approval ,");
            strSql.Append("         [RealAmount] = @RealAmount, ");
            strSql.Append("         [RelSignNum] = @RelSignNum");
            strSql.Append(" WHERE   Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@ApproverId", DbType.AnsiStringFixedLength, mdl.ApproverId);
            this._db.AddInParameter(dc, "@Approver", DbType.String, mdl.Approver);
            this._db.AddInParameter(dc, "@ApproveDate", DbType.DateTime, mdl.ApproveDate);
            this._db.AddInParameter(dc, "@Approval", DbType.String, mdl.Approval);
            this._db.AddInParameter(dc, "@RealAmount", DbType.Decimal, mdl.RealAmount);
            this._db.AddInParameter(dc, "@RelSignNum", DbType.Decimal, mdl.RelSignNum);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        public MDebit GetDebit(int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MDebit();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            strSql.Append("         [TourCode] ,");
            strSql.Append("         [BorrowerId] ,");
            strSql.Append("         [Borrower] ,");
            strSql.Append("         [BorrowTime] ,");
            strSql.Append("         [BorrowAmount] ,");
            strSql.Append("         [UseFor] ,");
            strSql.Append("         [ApproverId] ,");
            strSql.Append("         [Approver] ,");
            strSql.Append("         [ApproveDate] ,");
            strSql.Append("         [Approval] ,");
            strSql.Append("         [RealAmount] ,");
            strSql.Append("         [Status] ,");
            strSql.Append("         [LenderId] ,");
            strSql.Append("         [Lender] ,");
            strSql.Append("         [LendDate] ,");
            strSql.Append("         [LendRemark],");
            strSql.Append("         [PreSignNum],");
            strSql.Append("         [RelSignNum],");
            strSql.Append("         [OperatorId],");
            strSql.Append("         [Operator],");
            strSql.Append("         [OperatorDeptId],");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [tbl_FinDebit]");
            strSql.Append(" WHERE   Id = @Id");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    mdl.TourCode = dr.IsDBNull(dr.GetOrdinal("TourCode")) ? "" : dr.GetString(dr.GetOrdinal("TourCode"));
                    mdl.BorrowerId = dr.IsDBNull(dr.GetOrdinal("BorrowerId")) ? "" : dr.GetString(dr.GetOrdinal("BorrowerId"));
                    mdl.Borrower = dr.GetString(dr.GetOrdinal("Borrower"));
                    mdl.BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime"));
                    mdl.BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount"));
                    mdl.UseFor = dr.IsDBNull(dr.GetOrdinal("UseFor")) ? "" : dr.GetString(dr.GetOrdinal("UseFor"));
                    mdl.ApproverId = dr.IsDBNull(dr.GetOrdinal("ApproverId")) ? "" : dr.GetString(dr.GetOrdinal("ApproverId"));
                    mdl.Approver = dr.IsDBNull(dr.GetOrdinal("Approver")) ? "" : dr.GetString(dr.GetOrdinal("Approver"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ApproveDate")))
                    {
                        mdl.ApproveDate = dr.GetDateTime(dr.GetOrdinal("ApproveDate"));
                    }
                    mdl.Approval = dr.IsDBNull(dr.GetOrdinal("Approval")) ? "" : dr.GetString(dr.GetOrdinal("Approval"));
                    mdl.RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount"));
                    mdl.Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.LenderId = dr["LenderId"].ToString();
                    mdl.Lender = dr["Lender"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("LendDate")))
                    {
                        mdl.LendDate = dr.GetDateTime(dr.GetOrdinal("LendDate"));
                    }
                    mdl.LendRemark = dr.IsDBNull(dr.GetOrdinal("LendRemark")) ? "" : dr.GetString(dr.GetOrdinal("LendRemark"));
                    mdl.PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum"));
                    mdl.RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据借款搜索实体获取借款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">借款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MDebitBase mSearch
                                        , string operatorId
                                        , params int[] deptIds)
        {
            var lst = new List<MDebit>();
            var query = new StringBuilder();

            if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                query.AppendFormat(" TourCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (!string.IsNullOrEmpty(mSearch.BorrowerId))
            {
                query.AppendFormat(" BorrowerId = '{0}' AND", mSearch.BorrowerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Borrower))
            {
                query.AppendFormat(" Borrower LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Borrower));
            }
            query.AppendFormat(" EXISTS(select 1 from tbl_tour where tourid=tbl_FinDebit.tourid and TourStatus<{1}) AND", mSearch.IsVerificated ? "1" : "0", (int)TourStatus.已取消);
            query.AppendFormat(" IsDeleted = '0' AND CompanyId = '{1}'", (int)mSearch.Status, mSearch.CompanyId);
            query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount
                , "tbl_FinDebit", "Id", "[Id],[TourId],[TourCode],[Borrower],[BorrowTime],[BorrowAmount],[Status],[RealAmount],[PreSignNum],[RelSignNum],UseFor"
                , query.ToString()
                , "BorrowTime DESC"))
            {
                while (dr.Read())
                {
                    var mdl = new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        TourId = dr["TourId"].ToString(),
                        TourCode = dr["TourCode"].ToString(),
                        Borrower = dr["Borrower"].ToString(),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                        UseFor = dr["UseFor"].ToString()
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByTourId(string tourId, bool isBz)
        {
            var lst = new List<MDebit>();
            var sql =
                string.Format(
                    "select Id,BorrowerId,Borrower,BorrowTime,BorrowAmount,RealAmount,PreSignNum,RelSignNum,UseFor,Status from tbl_FinDebit where IsDeleted='0' and TourId='{0}' and Status {2} {1}",
                    tourId,
                    (int)FinStatus.账务已支付,
                    isBz ? "=" : "<=");
            var cmd = this._db.GetSqlStringCommand(sql);
            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    lst.Add(new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        BorrowerId = dr["BorrowerId"].ToString(),
                        Borrower = dr.GetString(dr.GetOrdinal("Borrower")),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                        UseFor = dr["UseFor"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                    });
                    //lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByPlanId(string planId)
        {
            var lst = new List<MDebit>();
            var sql =
                string.Format(
                    "select Id,Borrower,BorrowTime,BorrowAmount,RealAmount,PreSignNum,RelSignNum,UseFor,Status from tbl_FinDebit where IsDeleted='0' and PlanId='{0}'", planId);
            var cmd = this._db.GetSqlStringCommand(sql);
            using (var dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    var mdl = new MDebit
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Borrower = dr.GetString(dr.GetOrdinal("Borrower")),
                        BorrowTime = dr.GetDateTime(dr.GetOrdinal("BorrowTime")),
                        BorrowAmount = dr.GetDecimal(dr.GetOrdinal("BorrowAmount")),
                        RealAmount = dr.GetDecimal(dr.GetOrdinal("RealAmount")),
                        PreSignNum = dr.GetInt32(dr.GetOrdinal("PreSignNum")),
                        RelSignNum = dr.GetInt32(dr.GetOrdinal("RelSignNum")),
                        UseFor = dr["UseFor"].ToString(),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Pay(MDebit mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinDebit");
            strSql.Append(" SET Status=@Status,");
            strSql.Append("     LenderId=@LenderId,");
            strSql.Append("     Lender=@Lender,");
            strSql.Append("     LendDate=getdate(),");
            strSql.Append("     LendRemark=@LendRemark");
            strSql.Append(" WHERE");
            strSql.Append("     Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@LenderId", DbType.AnsiStringFixedLength, mdl.LenderId);
            this._db.AddInParameter(dc, "@Lender", DbType.String, mdl.Lender);
            this._db.AddInParameter(dc, "@LendRemark", DbType.String, mdl.LendRemark);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, mdl.Status);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DeleteDebit(string companyId, int id)
        {
            var strSql = new StringBuilder();

            strSql.Append(" UPDATE tbl_FinDebit");
            strSql.Append(" SET IsDeleted='1'");
            strSql.Append(" WHERE");
            strSql.Append("     Id = @Id and CompanyId = @CompanyId");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        #endregion

        #region 财务情况登记
        /// <summary>
        /// 财务情况登记新增/修改
        /// </summary>
        /// <param name="m">财务情况登记实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdDengJi(MCaiWuDengJi m)
        {
            var strSql = new StringBuilder();

            if (m.Id > 0)
            {
                strSql.Append(" UPDATE  [dbo].[tbl_FinDengJi]");
                strSql.Append(" SET     [Typ] = @Typ ,");
                strSql.Append("         [ApplyDate] = @ApplyDate ,");
                strSql.Append("         [Title] = @Title ,");
                strSql.Append("         [DanWeiNm] = @DanWeiNm ,");
                strSql.Append("         [FeeAmount] = @FeeAmount ,");
                strSql.Append("         [Remark] = @Remark");
                strSql.Append(" WHERE   [Id] = @Id");
                strSql.Append("         AND [CompanyId] = @CompanyId");
            }
            else
            {
                strSql.Append(" INSERT  INTO [dbo].[tbl_FinDengJi]");
                strSql.Append("         ( [CompanyId] ,");
                strSql.Append("           [Typ] ,");
                strSql.Append("           [ApplyDate] ,");
                strSql.Append("           [Title] ,");
                strSql.Append("           [DanWeiNm] ,");
                strSql.Append("           [FeeAmount] ,");
                strSql.Append("           [Remark] ,");
                strSql.Append("           [OperatorDeptId] ,");
                strSql.Append("           [OperatorId] ,");
                strSql.Append("           [Operator] ,");
                strSql.Append("           [IssueTime]");
                strSql.Append("         )");
                strSql.Append(" VALUES  ( @CompanyId ,");
                strSql.Append("           @Typ ,");
                strSql.Append("           @ApplyDate ,");
                strSql.Append("           @Title ,");
                strSql.Append("           @DanWeiNm ,");
                strSql.Append("           @FeeAmount ,");
                strSql.Append("           @Remark ,");
                strSql.Append("           @OperatorDeptId ,");
                strSql.Append("           @OperatorId ,");
                strSql.Append("           @Operator ,");
                strSql.Append("           @IssueTime");
                strSql.Append("         )");
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, m.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, m.CompanyId);
            this._db.AddInParameter(dc, "@Typ", DbType.Byte, (int)m.Typ);
            this._db.AddInParameter(dc, "@ApplyDate", DbType.DateTime, m.ApplyDate);
            this._db.AddInParameter(dc, "@Title", DbType.String, m.Title);
            this._db.AddInParameter(dc, "@DanWeiNm", DbType.String, m.DanWeiNm);
            this._db.AddInParameter(dc, "@FeeAmount", DbType.Decimal, m.FeeAmount);
            this._db.AddInParameter(dc, "@Remark", DbType.String, m.Remark);
            this._db.AddInParameter(dc, "@OperatorDeptId", DbType.Int32, m.OperatorDeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, m.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, m.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, m.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 财务情况登记删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelDengJi(string companyId, int[] ids)
        {
            var strSql = new StringBuilder();

            strSql.Append(" DELETE  FROM [dbo].[tbl_FinDengJi]");
            strSql.AppendFormat(" WHERE   [CompanyId] = @CompanyId AND [Id] IN ( {0} )", Utils.GetSqlIdStrByArray(ids));

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据主键ID获取财务登记实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>财务登记实体</returns>
        public MCaiWuDengJi GetDengJiMdl(string companyId, int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MCaiWuDengJi();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [Typ] ,");
            strSql.Append("         [ApplyDate] ,");
            strSql.Append("         [Title] ,");
            strSql.Append("         [DanWeiId] ,");
            strSql.Append("         [DanWeiNm] ,");
            strSql.Append("         [FeeAmount] ,");
            strSql.Append("         [Remark] ,");
            strSql.Append("         [OperatorDeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [dbo].[tbl_FinDengJi]");
            strSql.Append(" WHERE   Id = @Id");
            strSql.Append("         AND CompanyId = @CompanyId");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.Typ = (CaiWuDengJi)dr.GetByte(dr.GetOrdinal("Typ"));
                    mdl.ApplyDate = dr.GetDateTime(dr.GetOrdinal("ApplyDate"));
                    mdl.Title = dr["Title"].ToString();
                    mdl.DanWeiNm = dr["DanWeiNm"].ToString();
                    mdl.FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount"));
                    mdl.Remark = dr["Remark"].ToString();
                    mdl.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取财务登记列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>财务登记列表</returns>
        public IList<MCaiWuDengJi> GetDengJiList(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MCaiWuDengJiBase mSearch
                                                , string operatorId
                                                , params int[] deptIds)
        {
            var lst = new List<MCaiWuDengJi>();
            var query = new StringBuilder();

            if (!string.IsNullOrEmpty(mSearch.Title))
            {
                query.AppendFormat(" Title LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Title));
            }
            if (mSearch.Typ != CaiWuDengJi.请选择)
            {
                query.AppendFormat(" Typ = {0} AND", (int)mSearch.Typ);
            }
            if (mSearch.ApplyDateS.HasValue)
            {
                query.AppendFormat(" ApplyDate >= '{0}' AND", mSearch.ApplyDateS.Value);
            }
            if (mSearch.ApplyDateE.HasValue)
            {
                query.AppendFormat(" ApplyDate < '{0}' AND", mSearch.ApplyDateE.Value.AddDays(1));
            }
            query.AppendFormat(" CompanyId = '{0}'", mSearch.CompanyId);
            //query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, "tbl_FinDengJi", "Id,Typ,ApplyDate,Title,FeeAmount,Remark", query.ToString(), "ApplyDate DESC", ""))
            {
                while (dr.Read())
                {
                    var mdl = new MCaiWuDengJi
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        Typ = (CaiWuDengJi)dr.GetByte(dr.GetOrdinal("Typ")),
                        ApplyDate = dr.GetDateTime(dr.GetOrdinal("ApplyDate")),
                        Title = dr["Title"].ToString(),
                        FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                        Remark = dr["Remark"].ToString(),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }
        #endregion

        #region 购物统计表
        /// <summary>
        /// 获取购物统计列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns></returns>
        public IList<MGouWuTongJi> GetGouWuTongJi(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MGouWuTongJiBase mSearch)
        {
            var l = new List<MGouWuTongJi>();

            var query = new StringBuilder();

            query.AppendFormat(" CompanyId = '{0}' AND Type = {1} AND IsDelete = 0", mSearch.CompanyId, (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物);

            if (!string.IsNullOrEmpty(mSearch.GysName))
            {
                query.AppendFormat(" AND Name LIKE '%{0}%'", Utils.ToSqlLike(mSearch.GysName));
            }
            if (!string.IsNullOrEmpty(mSearch.GuoJi))
            {
                query.AppendFormat(" AND EXISTS ( SELECT TOP 1");
                query.AppendFormat("                     1");
                query.AppendFormat("              FROM   tbl_SourceShopHeTong");
                query.AppendFormat("              WHERE  SourceId = tbl_Source.SourceId");
                query.AppendFormat("                     AND IsDisable = 1");
                query.AppendFormat("                     AND Country LIKE '%{0}%'", Utils.ToSqlLike(mSearch.GuoJi));
                query.AppendFormat("             )");
            }
            if (mSearch.JinDianRiQiS.HasValue || mSearch.JinDianRiQiE.HasValue)
            {
                query.AppendFormat(" AND EXISTS ( SELECT TOP 1");
                query.AppendFormat("                     1");
                query.AppendFormat("              FROM   tbl_Plan");
                query.AppendFormat("              WHERE  SourceId = tbl_Source.SourceId");
                query.AppendFormat("                     AND IsDelete = 0");
                if (mSearch.JinDianRiQiS.HasValue)
                    query.AppendFormat("                     AND StartDate >= '{0}'", mSearch.JinDianRiQiS.Value);
                if (mSearch.JinDianRiQiE.HasValue)
                    query.AppendFormat("                     AND StartDate < '{0}'", mSearch.JinDianRiQiE.Value);
                query.AppendFormat("             )");
            }

            using (var dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, "tbl_Source", "SourceId,Name,XMLHeTong=(SELECT TOP 1 BaoDi,LiuShui FROM tbl_SourceShopHeTong WHERE SourceId=tbl_Source.SourceId AND IsDisable=1 ORDER BY ContactTime DESC FOR XML RAW,ROOT),HeJi=(SELECT COUNT(*) FROM tbl_PlanShop A INNER JOIN tbl_Plan B ON A.PlanId=B.PlanId AND B.SourceId=tbl_Source.SourceId AND B.IsDelete=0)", query.ToString(), "IdentityId DESC", ""))
            {
                while (dr.Read())
                {
                    var mdl = new MGouWuTongJi
                    {
                        GysId = dr["SourceId"].ToString(),
                        GysName = dr["Name"].ToString(),
                        BaoDiJinE = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLHeTong"].ToString(), "BaoDi")),
                        LiuShui = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLHeTong"].ToString(), "LiuShui")),
                        BaoDiHeJiShu = dr.GetInt32(dr.GetOrdinal("HeJi")),
                        LiuShuiHeJiShu = dr.GetInt32(dr.GetOrdinal("HeJi")),
                    };
                    l.Add(mdl);
                }
            }
            return l;
        }

        /// <summary>
        /// 获取购物统计明细列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sourceId">供应商ID</param>
        /// <returns></returns>
        public IList<MGouWuTongJiDetail> GetGouWuTongJi(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , string sourceId)
        {
            var l = new List<MGouWuTongJiDetail>();

            var query = new StringBuilder();

            query.AppendFormat(" SourceId = '{0}'", sourceId);

            using (var dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, "view_GouWuTongJiDetail", "TourId,TourCode,LDate,StartDate,SellerName,Adults,Childs,YingYe,LiuShui,BaoDiE,LiuShuiE", query.ToString(), "LDate DESC", ""))
            {
                while (dr.Read())
                {
                    var mdl = new MGouWuTongJiDetail
                    {
                        TourId = dr["TourId"].ToString(),
                        TourCode = dr["TourCode"].ToString(),
                        LDate = Utils.GetDateTime(dr["LDate"].ToString()),
                        JinDianRiQi = Utils.GetDateTime(dr["StartDate"].ToString()),
                        SellerName = dr["SellerName"].ToString(),
                        Adult = Utils.GetInt(dr["Adults"].ToString()),
                        Child = Utils.GetInt(dr["Childs"].ToString()),
                        YingYeE = Utils.GetDecimal(dr["YingYe"].ToString()),
                        LiuShui = Utils.GetDecimal(dr["LiuShui"].ToString()),
                        BaoDiE = Utils.GetDecimal(dr["BaoDiE"].ToString()),
                        LiuShuiE = Utils.GetDecimal(dr["LiuShuiE"].ToString())
                    };
                    l.Add(mdl);
                }
            }
            return l;
        }
        #endregion

        #region 签单挂失
        /// <summary>
        /// 签单挂失新增/修改
        /// </summary>
        /// <param name="m">签单挂失实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdGuaShi(MQianDanGuaShi m)
        {
            var strSql = new StringBuilder();

            if (m.Id > 0)
            {
                strSql.Append(" UPDATE  [dbo].[tbl_FinQianDan]");
                strSql.Append(" SET     [TourId] = @TourId ,");
                strSql.Append("         [TourCode] = @TourCode ,");
                strSql.Append("         [Typ] = @Typ ,");
                strSql.Append("         [SignCode] = @SignCode ,");
                strSql.Append("         [ApplierId] = @ApplierId ,");
                strSql.Append("         [Applier] = @Applier ,");
                strSql.Append("         [ApplyTime] = @ApplyTime");
                strSql.Append(" WHERE   [Id] = @Id");
                strSql.Append("         AND [CompanyId] = @CompanyId");
            }
            else
            {
                strSql.Append(" INSERT  INTO [dbo].[tbl_FinQianDan]");
                strSql.Append("         ( [CompanyId] ,");
                strSql.Append("           [TourId] ,");
                strSql.Append("           [TourCode] ,");
                strSql.Append("           [Typ] ,");
                strSql.Append("           [SignCode] ,");
                strSql.Append("           [ApplierId] ,");
                strSql.Append("           [Applier] ,");
                strSql.Append("           [ApplyTime] ,");
                strSql.Append("           [OperatorDeptId] ,");
                strSql.Append("           [OperatorId] ,");
                strSql.Append("           [Operator] ,");
                strSql.Append("           [IssueTime]");
                strSql.Append("         )");
                strSql.Append(" VALUES  ( @CompanyId ,");
                strSql.Append("           @TourId ,");
                strSql.Append("           @TourCode ,");
                strSql.Append("           @Typ ,");
                strSql.Append("           @SignCode ,");
                strSql.Append("           @ApplierId ,");
                strSql.Append("           @Applier ,");
                strSql.Append("           @ApplyTime ,");
                strSql.Append("           @OperatorDeptId ,");
                strSql.Append("           @OperatorId ,");
                strSql.Append("           @Operator ,");
                strSql.Append("           @IssueTime");
                strSql.Append("         )");
            }

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, m.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, m.CompanyId);
            this._db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, m.TourId);
            this._db.AddInParameter(dc, "@TourCode", DbType.String, m.TourCode);
            this._db.AddInParameter(dc, "@Typ", DbType.Byte, (int)m.Typ);
            this._db.AddInParameter(dc, "@SignCode", DbType.String, m.SignCode);
            this._db.AddInParameter(dc, "@ApplierId", DbType.AnsiStringFixedLength, m.ApplierId);
            this._db.AddInParameter(dc, "@Applier", DbType.String, m.Applier);
            this._db.AddInParameter(dc, "@ApplyTime", DbType.DateTime, m.ApplyTime);
            this._db.AddInParameter(dc, "@OperatorDeptId", DbType.Int32, m.OperatorDeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, m.OperatorId);
            this._db.AddInParameter(dc, "@Operator", DbType.String, m.Operator);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, m.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 签单挂失删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelGuaShi(string companyId, int[] ids)
        {
            var strSql = new StringBuilder();

            strSql.Append(" DELETE  FROM [dbo].[tbl_FinQianDan]");
            strSql.AppendFormat(" WHERE   [CompanyId] = @CompanyId AND [Id] IN ( {0} )", Utils.GetSqlIdStrByArray(ids));

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据主键ID获取签单挂失实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>签单挂失实体</returns>
        public MQianDanGuaShi GetGuaShiMdl(string companyId, int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MQianDanGuaShi();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [TourId] ,");
            strSql.Append("         [TourCode] ,");
            strSql.Append("         [Typ] ,");
            strSql.Append("         [SignCode] ,");
            strSql.Append("         [ApplierId] ,");
            strSql.Append("         [Applier] ,");
            strSql.Append("         [ApplyTime] ,");
            strSql.Append("         [OperatorDeptId] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [Operator] ,");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [dbo].[tbl_FinQianDan]");
            strSql.Append(" WHERE   Id = @Id");
            strSql.Append("         AND CompanyId = @CompanyId");


            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.TourCode = dr["TourCode"].ToString();
                    mdl.Typ = (PlanProject)dr.GetByte(dr.GetOrdinal("Typ"));
                    mdl.SignCode = dr["SignCode"].ToString();
                    mdl.ApplierId = dr["ApplierId"].ToString();
                    mdl.Applier = dr["Applier"].ToString();
                    mdl.ApplyTime = dr.GetDateTime(dr.GetOrdinal("ApplyTime"));
                    mdl.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取签单挂失列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>签单挂失列表</returns>
        public IList<MQianDanGuaShi> GetGuaShiList(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MQianDanGuaShiBase mSearch
                                                , string operatorId
                                                , params int[] deptIds)
        {
            var lst = new List<MQianDanGuaShi>();
            var query = new StringBuilder();

            if (!string.IsNullOrEmpty(mSearch.SignCode))
            {
                query.AppendFormat(" SignCode LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.SignCode));
            }
            if (!string.IsNullOrEmpty(mSearch.ApplierId))
            {
                query.AppendFormat(" ApplierId = '{0}' AND", mSearch.ApplierId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Applier))
            {
                query.AppendFormat(" Applier LIKE '%{0}%' AND", Utils.ToSqlLike(mSearch.Applier));
            }
            query.AppendFormat(" CompanyId = '{0}'", mSearch.CompanyId);
            //query.Append(GetOrgCondition(operatorId, deptIds));

            using (var dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, "tbl_FinQianDan", "Id,TourCode,Typ,SignCode,Applier,ApplyTime", query.ToString(), "ApplyTime DESC", ""))
            {
                while (dr.Read())
                {
                    var mdl = new MQianDanGuaShi
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        TourCode = dr["TourCode"].ToString(),
                        Typ = (PlanProject)dr.GetByte(dr.GetOrdinal("Typ")),
                        SignCode = dr["SignCode"].ToString(),
                        Applier = dr["Applier"].ToString(),
                        ApplyTime = dr.GetDateTime(dr.GetOrdinal("ApplyTime")),
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 根据当前销售员编号和部门编号集合获取销售收款和应收管理的浏览权限
        /// </summary>
        /// <param name="sellerId">销售员编号</param>
        /// <param name="deptIds">部门集合</param>
        /// <returns>组织机构浏览条件</returns>
        protected string GetReceivedOrg(string sellerId, ICollection<int> deptIds)
        {
            var str = string.Empty;

            if (!string.IsNullOrEmpty(sellerId) && deptIds != null)
            {
                str = string.Format(" AND (SellerId = '{0}'", sellerId);

                if (deptIds.Count > 0 && !deptIds.Contains(-1))
                {
                    str = str + deptIds.Aggregate(" OR SellerDeptId IN (", (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")";
                    str = str + string.Format(" OR EXISTS(SELECT 1 FROM tbl_Tour T WHERE T.TourId=tbl_TourOrder.TourId AND (T.SellerId = '{0}' {1}))", sellerId, deptIds.Aggregate(" OR T.SellerDeptId IN (", (current, deptId) => current + string.Format("{0},", deptId)).TrimEnd(',') + ")");
                }
                str = str + ")";
            }

            return str;
        }

        /// <summary>
        /// 根据大于等于、等于、小于等于转换
        /// </summary>
        /// <param name="sign">大于等于、等于、小于等于</param>
        /// <returns>转换结果</returns>
        private static string GetEqualSign(EqualSign? sign)
        {
            var mark = "=";
            switch (sign)
            {
                case EqualSign.大于等于:
                    mark = ">=";
                    break;
                case EqualSign.等于:
                    mark = "=";
                    break;
                case EqualSign.小于等于:
                    mark = "<=";
                    break;
            }
            return mark;
        }

        /// <summary>
        /// 根据xml获取联系人列表
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>联系人列表</returns>
        private static IList<MCrmLinkman> GetCrmLinkmanLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MCrmLinkman
            {
                Name = Utils.GetXAttributeValue(i, "Name"),
                MobilePhone = Utils.GetXAttributeValue(i, "MobilePhone"),
                Telephone = Utils.GetXAttributeValue(i, "Telephone"),
                Fax = Utils.GetXAttributeValue(i, "Fax"),
                Type = (LxrType)Utils.GetInt(Utils.GetXAttributeValue(i, "Type")),
            }).ToList();
        }

        /// <summary>
        /// 将批量支付列表转XML数据
        /// </summary>
        /// <param name="lst">批量支付列表</param>
        /// <returns></returns>
        private string GetBatchPayXml(IList<MBatchPay> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row RegisterId='{0}'", i.RegisterId);
                    sb.AppendFormat(" PaymentType='{0}'", i.PaymentType);
                    sb.AppendFormat(" PaymentTypeName='{0}'", Utils.ReplaceXmlSpecialCharacter(i.PaymentTypeName));
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion
    }
}
