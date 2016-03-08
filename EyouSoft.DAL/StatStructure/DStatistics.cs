using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.StatStructure;


namespace EyouSoft.DAL.StatStructure
{
    using System.Xml.Linq;

    using EyouSoft.IDAL.StatStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.StatStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Toolkit;
    using EyouSoft.Toolkit.DAL;

    using Microsoft.Practices.EnterpriseLibrary.Data;

    /// <summary>
    /// 统计分析
    /// 创建者：郑知远
    /// 创建时间：2011-09-15
    /// 2012-03-20 曹胡生 修改
    /// </summary>
    public class DStatistics : DALBase, IStatistics
    {
        #region 构造函数
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DStatistics()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 线路流量统计

        /// <summary>
        /// 获取线路流量统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param> 
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MRouteFlow> GetRouteFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MRouteFlowSearch search)
        {
            IList<MRouteFlow> list = new List<MRouteFlow>();
            EyouSoft.Model.StatStructure.MRouteFlow item = null;
            StringBuilder cmdQuery = new StringBuilder();
            var TableName = new StringBuilder();
            string GroupString = "AreaId,AreaName";
            string OrderByString = "TourIncome,TourPay DESC";
            StringBuilder fields = new StringBuilder();

            #region 派生表
            TableName.Append(" SELECT  A.AreaId ,");
            TableName.Append("         A.AreaName ,");
            TableName.Append("         B.CompanyId ,");
            TableName.Append("         B.DeptId ,");
            TableName.Append("         B.Adults ,");
            TableName.Append("         B.Childs ,");
            TableName.Append("         B.TourIncome ,");
            TableName.Append("         B.TourPay ,");
            TableName.Append("         B.ReviewTime ,");
            TableName.Append("         B.LDate");
            TableName.Append(" FROM    dbo.tbl_ComArea AS A");
            TableName.Append("         INNER JOIN ( SELECT A.TourId ,");
            TableName.Append("                             A.AreaId ,");
            TableName.Append("                             A.CompanyId ,");
            TableName.Append("                             A.DeptId ,");
            TableName.Append("                             A.TourIncome ,");
            TableName.Append("                             A.TourPay ,");
            TableName.Append("                             B_1.Adults ,");
            TableName.Append("                             B_1.Childs ,");
            TableName.Append("                             C.IssueTime ReviewTime ,");
            TableName.Append("                             A.LDate");
            TableName.Append("                      FROM   dbo.tbl_Tour AS A");
            TableName.Append("                             INNER JOIN tbl_TourStatusChange AS C ON C.TourId = A.TourId");
            TableName.Append("                                                           AND C.TourStatus = A.TourStatus");
            TableName.Append("                             LEFT OUTER JOIN ( SELECT    TourId ,");
            TableName.Append("                                                         SUM(Adults) AS Adults ,");
            TableName.Append("                                                         SUM(Childs) AS Childs");
            TableName.Append("                                               FROM      dbo.tbl_TourOrder");
            TableName.Append("                                               WHERE     ( IsDelete = 0 )");
            TableName.Append("                                                         AND ( Status = 4 )");
            TableName.Append("                                                         AND ( ConfirmMoneyStatus = 1 )");
            TableName.Append("                                               GROUP BY  TourId");
            TableName.Append("                                             ) AS B_1 ON A.TourId = B_1.TourId");
            TableName.Append("                      WHERE  ( A.IsDelete = 0 )");
            TableName.Append("                             AND ( A.TourStatus = 11 )");
            TableName.Append("                    ) AS B ON A.AreaId = B.AreaId");

            #endregion
            #region 要查询的字段
            fields.Append(" AreaId,AreaName,sum(Adults) Adults,sum(Childs) Childs,sum(TourIncome) TourIncome,sum(TourPay) TourPay,count(AreaId) TourCount ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", search.AreaId);
                }
                if (!string.IsNullOrEmpty(search.AreaName))
                {
                    cmdQuery.AppendFormat(" and AreaName like '%{0}%'", Utils.ToSqlLike(search.AreaName));
                }
                if (search.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS);
                }
                if (search.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE);
                }
                if (search.SReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)>=0", search.SReviewTime);
                }

                if (search.EReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)<=0", search.EReviewTime);
                }
                if (!search.SReviewTime.HasValue && !search.EReviewTime.HasValue)
                {
                    cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
                }
            }
            else
            {
                cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName.ToString()
                , fields.ToString(), cmdQuery.ToString(), OrderByString, GroupString, string.Empty))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MRouteFlow()
                    {
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        AreaId = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AreaId")),
                        AreaName = rdr["AreaName"].ToString(),
                        TotalIncome = rdr.IsDBNull(rdr.GetOrdinal("TourIncome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourIncome")),
                        TotalOutlay = rdr.IsDBNull(rdr.GetOrdinal("TourPay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPay")),
                        TourCount = rdr.IsDBNull(rdr.GetOrdinal("TourCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourCount"))
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取线路流量统计团队数量列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowTourList> GetRouteFlowTourListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MRouteFlowSearch search)
        {
            IList<MRouteFlowTourList> list = new List<MRouteFlowTourList>();
            EyouSoft.Model.StatStructure.MRouteFlowTourList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourCode,RouteName,LDate,TourDays ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and IsDelete=0 and TourStatus=11", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.AreaId > 0) cmdQuery.AppendFormat(" and AreaId = {0} ", search.AreaId);
                if (search.LDateS.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS.Value);
                if (search.LDateE.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE.Value);
                cmdQuery.Append(" and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus ");
                if (search.SReviewTime.HasValue || search.EReviewTime.HasValue)
                {
                    if (search.SReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", search.SReviewTime.Value);
                    if (search.EReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", search.EReviewTime.Value);
                }
                else
                {
                    cmdQuery.Append(" and datediff(month,IssueTime,getdate())=0");
                }

                cmdQuery.Append(" ) ");
            }
            else
            {
                cmdQuery.Append(
                    " and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus and datediff(month,IssueTime,getdate())=0 )");
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MRouteFlowTourList()
                    {
                        Days = rdr.IsDBNull(rdr.GetOrdinal("TourDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        RouteName = rdr["RouteName"].ToString(),
                        TourCode = rdr["TourCode"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取线路流量统计总收入列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowOrderList> GetRouteFlowtOrderListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , ref string sumMoney, string deptIds, MRouteFlowSearch search)
        {
            IList<MRouteFlowOrderList> list = new List<MRouteFlowOrderList>();
            EyouSoft.Model.StatStructure.MRouteFlowOrderList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_TourOrder";
            string OrderByString = "IssueTime DESC";
            string XMLSumField = CreateXmlSumByField("ConfirmMoney");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" OrderId,OrderCode,BuyCompanyName,(select LDate,RouteName from tbl_Tour where TourId = tbl_TourOrder.TourId for xml raw,root('Root')) as TourInfo,ConfirmMoney,SellerName ");
            #endregion
            #region 拼接查询条件

            cmdQuery.AppendFormat(" TourId in (select TourId from tbl_Tour where CompanyId='{0}' and IsDelete=0 and TourStatus=11 ", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.AreaId > 0) cmdQuery.AppendFormat(" and AreaId = {0} ", search.AreaId);
                if (search.LDateS.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS.Value);
                if (search.LDateE.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE.Value);
                cmdQuery.Append(" and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus ");
                if (search.SReviewTime.HasValue || search.EReviewTime.HasValue)
                {
                    if (search.SReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", search.SReviewTime.Value);
                    if (search.EReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", search.EReviewTime.Value);
                }
                else
                {
                    cmdQuery.Append(" and datediff(month,IssueTime,getdate())=0");
                }

                cmdQuery.Append(" ) ");
            }
            else
            {
                cmdQuery.Append(
                    " and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus and datediff(month,IssueTime,getdate())=0 )");
            }

            cmdQuery.Append(" ) ");
            cmdQuery.AppendFormat(
                " AND [Status] = {0}  AND  ConfirmMoneyStatus = 1  AND IsDelete = '0' ", (int)OrderStatus.已成交);

            #endregion)
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref  sumMoney, TableName, XMLSumField, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MRouteFlowOrderList()
                    {
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        OrderCode = rdr["OrderCode"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        SumPrice = rdr.IsDBNull(rdr.GetOrdinal("ConfirmMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney"))
                    };
                    if (!rdr.IsDBNull(rdr.GetOrdinal("TourInfo")) && !string.IsNullOrEmpty(rdr.GetString(rdr.GetOrdinal("TourInfo"))))
                    {
                        var xRoot = XElement.Parse(rdr.GetString(rdr.GetOrdinal("TourInfo")));
                        var xRows = Utils.GetXElements(xRoot, "row");
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                if (t == null) continue;

                                item.LDate = Utils.GetDateTime(Utils.GetXAttributeValue(t, "LDate"));
                                item.RouteName = Utils.GetXAttributeValue(t, "RouteName");
                            }
                        }
                    }
                    list.Add(item);
                }
            }

            sumMoney = GetValueByXml(sumMoney, "ConfirmMoney");
            return list;
        }

        /// <summary>
        /// 获取线路流量统计总支出列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param> 
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowPayList> GetRouteFlowPayListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , ref string sumMoney, string deptIds, MRouteFlowSearch search)
        {
            IList<MRouteFlowPayList> list = new List<MRouteFlowPayList>();
            EyouSoft.Model.StatStructure.MRouteFlowPayList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string OrderByString = "RealPeopleNumber DESC";
            string XMLSumField = CreateXmlSumByField("TourPay");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourCode,RouteName,RealPeopleNumber,TourPay ");
            #endregion
            #region 拼接查询条件

            cmdQuery.AppendFormat(" CompanyId='{0}' and IsDelete=0 and TourStatus=11", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.AreaId > 0) cmdQuery.AppendFormat(" and AreaId = {0} ", search.AreaId);
                if (search.LDateS.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS.Value);
                if (search.LDateE.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE.Value);
                cmdQuery.Append(" and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus ");
                if (search.SReviewTime.HasValue || search.EReviewTime.HasValue)
                {
                    if (search.SReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", search.SReviewTime.Value);
                    if (search.EReviewTime.HasValue) cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", search.EReviewTime.Value);
                }
                else
                {
                    cmdQuery.Append(" and datediff(month,IssueTime,getdate())=0");
                }

                cmdQuery.Append(" ) ");
            }
            else
            {
                cmdQuery.Append(
                    " and exists (select 1 from tbl_TourStatusChange as a where a.TourId = tbl_Tour.TourId and a.TourStatus = tbl_Tour.TourStatus and datediff(month,IssueTime,getdate())=0 )");
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref  sumMoney, TableName, XMLSumField, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MRouteFlowPayList()
                    {
                        PayMoney = rdr.IsDBNull(rdr.GetOrdinal("TourPay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPay")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString()
                    };
                    list.Add(item);
                }
            }
            sumMoney = GetValueByXml(sumMoney, "TourPay");
            return list;
        }
        #endregion

        #region 部门业绩统计

        /// <summary>
        /// 获取部门业绩统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <param name="TongJi">汇总实体</param>
        /// <returns>部门业绩统计列表</returns>
        public IList<MDepartment> GetDepartmentLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds
            , MDepartmentSearch search, ref MDepartmentTongJi TongJi)
        {
            IList<MDepartment> list = new List<MDepartment>();
            EyouSoft.Model.StatStructure.MDepartment item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string GroupString = " DeptId ";
            string TableName = " select * from view_SellerYeJi";
            string OrderByString = "Income,Pay DESC";
            string XMLSumField = " Sum(OrderNum) as SumOrderNum,Sum(OrderPersonNum) as SumOrderPersonNum,Sum(InCome) as SumInCome,Sum(Pay) as SumPay,Sum(GrossProfit) as SumGrossProfit ";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" DeptId,sum(OrderNum) OrderNum,count(distinct (SellerId)) PeopleNum,sum(PeopleNum) OrderPersonNum,sum(InCome) InCome,sum(Pay) Pay, sum(GrossProfit) GrossProfit ");
            fields.Append(
                " ,(select DepartName from tbl_ComDepartment where tbl_ComDepartment.DepartId = DeptId) as DepartName ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.DeptId != 0)
                {
                    cmdQuery.AppendFormat(" and DeptId={0}", search.DeptId);
                }
                if (!string.IsNullOrEmpty(search.DepartName))
                {
                    cmdQuery.AppendFormat(" and DepartName like '%{0}%'", Utils.ToSqlLike(search.DepartName));
                }
                if (search.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS);
                }
                if (search.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE);
                }
                if (search.SReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)>=0", search.SReviewTime);
                }

                if (search.EReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)<=0", search.EReviewTime);
                }
                if (!search.SReviewTime.HasValue && !search.EReviewTime.HasValue)
                {
                    cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
                }
            }
            else
            {
                cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName
                , fields.ToString(), cmdQuery.ToString(), OrderByString, GroupString, XMLSumField))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MDepartment()
                    {
                        DeptId = rdr.IsDBNull(rdr.GetOrdinal("DeptId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("DeptId")),
                        DeptName = rdr["DepartName"].ToString(),
                        OrderNum = rdr.IsDBNull(rdr.GetOrdinal("OrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNum")),
                        TotalIncome = rdr.IsDBNull(rdr.GetOrdinal("InCome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("InCome")),
                        TotalOutlay = rdr.IsDBNull(rdr.GetOrdinal("Pay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("Pay")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("PeopleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PeopleNum")),
                        OrderPersonNum = rdr.IsDBNull(rdr.GetOrdinal("OrderPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderPersonNum"))
                    };
                    list.Add(item);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumOrderNum"))) TongJi.OrderNum = rdr["SumOrderNum"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumOrderPersonNum"))) TongJi.PeopleNum = rdr["SumOrderPersonNum"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumInCome"))) TongJi.InCome = rdr["SumInCome"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumPay"))) TongJi.Pay = rdr["SumPay"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumGrossProfit"))) TongJi.GrossProfit = rdr["SumGrossProfit"].ToString();
                }
            }

            return list;
        }

        /// <summary>
        /// 获取部门业绩统计员工人数列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">员工人数统计汇总实体</param>
        /// <returns></returns>
        public IList<MDepartmentPeopleList> GetDepartmentPeopleListByDeptId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MDepartmentSearch search, ref MDepartmentPeopleListTongJi tongJi)
        {
            IList<MDepartmentPeopleList> list = new List<MDepartmentPeopleList>();
            EyouSoft.Model.StatStructure.MDepartmentPeopleList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string SumMoney = "";
            string GroupString = " SellerId ";
            string TableName = " select * from view_SellerYeJi";
            string OrderByString = "Income,Pay DESC";
            string XMLSumField = " Sum(OrderNum) as SumOrderNum,Sum(OrderPersonNum) as SumOrderPersonNum,Sum(InCome) as SumInCome,Sum(Pay) as SumPay,Sum(GrossProfit) as SumGrossProfit "; //CreateXmlSumByField("OrderNum", "PeopleNum", "InCome", "Pay", "GrossProfit");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" SellerId,sum(OrderNum) OrderNum,sum(PeopleNum) OrderPersonNum,sum(InCome) InCome,sum(Pay) Pay, sum(GrossProfit) GrossProfit ");
            fields.Append(" ,(select ContactName from tbl_ComUser where tbl_ComUser.UserId = SellerId) as SellerName ");
            #endregion
            #region 拼接查询条件

            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (search.DeptId != 0)
                {
                    cmdQuery.AppendFormat(" and DeptId={0}", search.DeptId);
                }
                if (!string.IsNullOrEmpty(search.DepartName))
                {
                    cmdQuery.AppendFormat(" and DepartName like '%{0}%'", Utils.ToSqlLike(search.DepartName));
                }
                if (search.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS);
                }
                if (search.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE);
                }
                if (search.SReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)>=0", search.SReviewTime);
                }

                if (search.EReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)<=0", search.EReviewTime);
                }
                if (!search.SReviewTime.HasValue && !search.EReviewTime.HasValue)
                {
                    cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
                }
            }
            else
            {
                cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName
                , fields.ToString(), cmdQuery.ToString(), OrderByString, GroupString, XMLSumField))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MDepartmentPeopleList()
                    {
                        SellerName = rdr["SellerName"].ToString(),
                        OrderNum = rdr.IsDBNull(rdr.GetOrdinal("OrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNum")),
                        TotalIncome = rdr.IsDBNull(rdr.GetOrdinal("InCome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("InCome")),
                        TotalOutlay = rdr.IsDBNull(rdr.GetOrdinal("Pay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("Pay")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("OrderPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderPersonNum"))
                    };
                    list.Add(item);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumOrderNum"))) tongJi.OrderNum = rdr["SumOrderNum"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumOrderPersonNum"))) tongJi.PeopleNum = rdr["SumOrderPersonNum"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumInCome"))) tongJi.InCome = rdr["SumInCome"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumPay"))) tongJi.Pay = rdr["SumPay"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumGrossProfit"))) tongJi.GrossProfit = rdr["SumGrossProfit"].ToString();
                }
            }

            return list;
        }

        #endregion

        #region 个人业绩统计

        /// <summary>
        /// 个人业绩统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>个人业绩统计列表</returns>
        public IList<MPersonal> GetPersonalLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds
            , MPersonalSearch search)
        {
            IList<MPersonal> list = new List<MPersonal>();
            EyouSoft.Model.StatStructure.MPersonal item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string SumMoney = "";
            string GroupString = "SellerId,SellerName";
            string TableName = " select * from view_SellerYeJi";
            string OrderByString = "Income,Pay DESC";
            string XMLSumField = ""; //CreateXmlSumByField("OrderNum", "PeopleNum", "InCome", "Pay", "GrossProfit");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" SellerId,SellerName,sum(OrderNum) OrderNum,sum(PeopleNum) OrderPersonNum,sum(InCome) InCome,sum(Pay) Pay, sum(GrossProfit) GrossProfit ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", search.SellerId);
                }
                if (!string.IsNullOrEmpty(search.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(search.SellerName));
                }
                if (search.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS);
                }
                if (search.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE);
                }
                if (search.SReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)>=0", search.SReviewTime);
                }

                if (search.EReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)<=0", search.EReviewTime);
                }
                if (!search.SReviewTime.HasValue && !search.EReviewTime.HasValue)
                {
                    cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
                }
            }
            else
            {
                cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName
                , fields.ToString(), cmdQuery.ToString(), OrderByString, GroupString, XMLSumField))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MPersonal()
                    {
                        SellerId = rdr["SellerId"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        OrderNum = rdr.IsDBNull(rdr.GetOrdinal("OrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNum")),
                        TotalIncome = rdr.IsDBNull(rdr.GetOrdinal("InCome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("InCome")),
                        TotalOutlay = rdr.IsDBNull(rdr.GetOrdinal("Pay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("Pay")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("OrderPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderPersonNum"))
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 个人业绩统计订单列表
        /// </summary>
        ///<param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">统计信息</param>
        /// <returns></returns>
        public IList<MPersonalOrderList> GetPersonalOrderListBySellerId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MPersonalSearch search, ref MPersonalOrderListTongJi tongJi)
        {
            IList<MPersonalOrderList> list = new List<MPersonalOrderList>();
            EyouSoft.Model.StatStructure.MPersonalOrderList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string SumMoney = "";
            string TableName = "view_SellerYeJi";
            string OrderByString = "Income,Pay DESC";
            string XMLSumField = CreateXmlSumByField("OrderNum", "PeopleNum", "InCome", "Pay", "GrossProfit");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourCode,OrderCode,RouteName,BuyCompanyName,LDate,PeopleNum,InCome InCome,Pay, GrossProfit,Operator ");
            #endregion
            #region 拼接查询条件

            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", search.SellerId);
                }
                if (!string.IsNullOrEmpty(search.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(search.SellerName));
                }
                if (search.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", search.LDateS);
                }
                if (search.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", search.LDateE);
                }
                if (search.SReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)>=0", search.SReviewTime);
                }

                if (search.EReviewTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',ReviewTime)<=0", search.EReviewTime);
                }
                if (!search.SReviewTime.HasValue && !search.EReviewTime.HasValue)
                {
                    cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
                }
            }
            else
            {
                cmdQuery.Append(" and datediff(month,ReviewTime,getdate())=0");
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref  SumMoney, TableName, XMLSumField, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MPersonalOrderList()
                    {
                        TourCode = rdr["TourCode"].ToString(),
                        OrderCode = rdr["OrderCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr["LDate"].ToString(),
                        Operator = rdr["Operator"].ToString(),
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        TotalIncome = rdr.IsDBNull(rdr.GetOrdinal("InCome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("InCome")),
                        TotalOutlay = rdr.IsDBNull(rdr.GetOrdinal("Pay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("Pay")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("PeopleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PeopleNum"))
                    };
                    list.Add(item);
                }
            }
            tongJi.PeopleNum = GetValueByXml(SumMoney, "PeopleNum");
            tongJi.Pay = GetValueByXml(SumMoney, "Pay");
            tongJi.InCome = GetValueByXml(SumMoney, "InCome");
            tongJi.GrossProfit = GetValueByXml(SumMoney, "GrossProfit");
            return list;
        }

        #endregion

        #region 收入对帐单

        /// <summary>
        /// 获取收入对帐单列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param> 
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TongJi">统计实体</param>
        /// <returns></returns>
        public IList<MReconciliation> GetReconciliationLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MReconciliationSearch mSearch, ref MReconciliationTongJi TongJi)
        {
            IList<MReconciliation> list = new List<MReconciliation>();
            EyouSoft.Model.StatStructure.MReconciliation item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = " select * from view_Reconciliation";
            string OrderByString = "ConfirmMoney,CheckMoney DESC";
            string groupByString = " DeptId,SellerId ";
            string XMLSumField = " Sum(ConfirmMoney) as SumConfirmMoney,Sum(CheckMoney) as SumCheckMoney,Sum(NotIncome) as SumNotIncome "; //CreateXmlSumByField("OrderNum", "PeopleNum", "InCome", "Pay", "GrossProfit");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" DeptId,SellerId,(select DepartName from tbl_ComDepartment where tbl_ComDepartment.DepartId = DeptId) as DeptName,(select ContactName from tbl_ComUser where tbl_ComUser.UserId = SellerId) as SellerName,sum(ConfirmMoney) as ConfirmMoney,sum(CheckMoney) as CheckMoney,sum(NotIncome) as NotIncome ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (mSearch != null)
            {
                if (mSearch.DeptId != 0)
                {
                    cmdQuery.AppendFormat(" and DeptId ={0}", mSearch.DeptId);
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId ='{0}'", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (mSearch.RestAmount != 0)
                {
                    switch (mSearch.EqualSign)
                    {
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于:
                            cmdQuery.AppendFormat(" and NotIncome>={0}", mSearch.RestAmount);
                            break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.等于:
                            cmdQuery.AppendFormat(" and NotIncome={0}", mSearch.RestAmount);
                            break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于:
                            cmdQuery.AppendFormat(" and NotIncome<={0}", mSearch.RestAmount);
                            break;
                    }
                }
                if (mSearch.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", mSearch.LDateS);
                }
                if (mSearch.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", mSearch.LDateE);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName
                , fields.ToString(), cmdQuery.ToString(), OrderByString, groupByString, XMLSumField))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MReconciliation()
                    {
                        SellerId = rdr.IsDBNull(rdr.GetOrdinal("SellerId")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("SellerId")),
                        DeptName = rdr["DeptName"].ToString(),
                        InAmount = rdr.IsDBNull(rdr.GetOrdinal("CheckMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("CheckMoney")),
                        SellerName = rdr["SellerName"].ToString(),
                        TotalAmount = rdr.IsDBNull(rdr.GetOrdinal("ConfirmMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney")),
                        RestAmount = rdr.IsDBNull(rdr.GetOrdinal("NotIncome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("NotIncome"))
                    };
                    list.Add(item);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumConfirmMoney"))) TongJi.TotalAmount = rdr["SumConfirmMoney"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumCheckMoney"))) TongJi.InAmount = rdr["SumCheckMoney"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SumNotIncome"))) TongJi.RestAmount = rdr["SumNotIncome"].ToString();
                }
            }

            return list;
        }

        /// <summary>
        /// 获取收入对帐单未收款列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="mSearch">查询实体</param>
        /// <param name="tongJi">统计实体</param>
        /// <returns></returns>
        public IList<MReconciliationRestAmount> GetReconciliationRestAmountLst(string companyId, int pageSize, int pageIndex
            , ref int recordCount, string deptIds, MReconciliationSearch mSearch, ref MReconciliationTongJi tongJi)
        {
            IList<MReconciliationRestAmount> list = new List<MReconciliationRestAmount>();
            EyouSoft.Model.StatStructure.MReconciliationRestAmount item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string SumMoney = "";
            string TableName = "view_Reconciliation";
            string OrderByString = "NotIncome DESC";
            string XMLSumField = CreateXmlSumByField("ConfirmMoney", "CheckMoney", "NotIncome");
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" OrderCode,RouteName,LDate,BuyCompanyName,PeopleNum,ConfirmMoney,CheckMoney,NotIncome ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            if (!string.IsNullOrEmpty(deptIds))
            {
                cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            }
            if (mSearch != null)
            {
                if (mSearch.DeptId != 0)
                {
                    cmdQuery.AppendFormat(" and DeptId ={0}", mSearch.DeptId);
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId ='{0}'", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (mSearch.RestAmount != 0)
                {
                    switch (mSearch.EqualSign)
                    {
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于:
                            cmdQuery.AppendFormat(" and NotIncome>={0}", mSearch.RestAmount);
                            break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.等于:
                            cmdQuery.AppendFormat(" and NotIncome={0}", mSearch.RestAmount);
                            break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于:
                            cmdQuery.AppendFormat(" and NotIncome<={0}", mSearch.RestAmount);
                            break;
                    }
                }
                if (mSearch.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", mSearch.LDateS);
                }
                if (mSearch.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", mSearch.LDateE);
                }
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref  SumMoney, TableName, XMLSumField, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MReconciliationRestAmount()
                    {
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        OrderCode = rdr["OrderCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("PeopleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PeopleNum")),
                        InAmount = rdr.IsDBNull(rdr.GetOrdinal("CheckMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("CheckMoney")),
                        TotalAmount = rdr.IsDBNull(rdr.GetOrdinal("ConfirmMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney")),
                        RestAmount = rdr.IsDBNull(rdr.GetOrdinal("NotIncome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("NotIncome"))
                    };
                    list.Add(item);
                }
            }
            tongJi.InAmount = GetValueByXml(SumMoney, "CheckMoney");
            tongJi.RestAmount = GetValueByXml(SumMoney, "NotIncome");
            tongJi.TotalAmount = GetValueByXml(SumMoney, "ConfirmMoney");
            return list;
        }

        #endregion

        #region 状态查询表

        /// <summary>
        /// 状态查询表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="heJi">合计信息[0:int:人数合计]</param>
        /// <returns></returns>
        public IList<MTourStatus> GetTourStatusLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int[] deptIds, MTourStatusSearch mSearch, out object[] heJi)
        {
            heJi = new object[] { 0 };
            IList<MTourStatus> list = new List<MTourStatus>();
            EyouSoft.Model.StatStructure.MTourStatus item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string OrderByString = "LDate DESC";
            StringBuilder fields = new StringBuilder();
            string heJiString = "SUM(RealPeopleNumber) AS RenShuHeJi";

            #region 要查询的字段
            fields.Append(" TourId,TourCode,RouteName,LDate,RDate,RealPeopleNumber,SellerName,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as PlanerList,(select SourceId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and IsDelete='0' and [Type]=12 for xml raw,root) as GuideList, TourStatus ");
            fields.Append(",TourType");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and ParentId<>'' and IsDelete='0'", companyId);
            cmdQuery.Append(" AND TourStatus NOT IN(12,13,14,15) ");
            if (deptIds != null && deptIds.Any())
            {
                cmdQuery.AppendFormat(" and DeptId in ({0}) ", Utils.GetSqlIdStrByArray(deptIds));
            }

            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(mSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(mSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId = '{0}' ", mSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(mSearch.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
                }
                if (!string.IsNullOrEmpty(mSearch.GuiderId) || !string.IsNullOrEmpty(mSearch.Guide))
                {
                    cmdQuery.AppendFormat(
                        " and exists(select 1 from tbl_Plan where tbl_Plan.TourId = tbl_Tour.TourId and tbl_Plan.IsDelete = '0' and tbl_Plan.Type = {0} ",
                        (int)PlanProject.导游);
                    if (!string.IsNullOrEmpty(mSearch.GuiderId))
                    {
                        cmdQuery.AppendFormat(" and tbl_Plan.SourceId = '{0}' ", mSearch.GuiderId);
                    }
                    else if (!string.IsNullOrEmpty(mSearch.Guide))
                    {
                        cmdQuery.AppendFormat(" and tbl_Plan.SourceName like '%{0}%' ", Utils.ToSqlLike(mSearch.Guide));
                    }
                    cmdQuery.Append(" ) ");
                }
                if (mSearch.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", mSearch.LDateS);
                }
                if (mSearch.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", mSearch.LDateE);
                }
                if (mSearch.RDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)>=0", mSearch.RDateS);
                }
                if (mSearch.RDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)<=0", mSearch.RDateE);
                }
                if (mSearch.TourStatus != null && mSearch.TourStatus.Length > 0)
                {
                    cmdQuery.AppendFormat(" and TourStatus IN({0})", Utils.GetSqlIn<TourStatus>(mSearch.TourStatus));
                }
                if (!string.IsNullOrEmpty(mSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", mSearch.RouteName);
                }
                if (mSearch.TourSellerDeptIds != null && mSearch.TourSellerDeptIds.Length > 0)
                {
                    cmdQuery.AppendFormat(" AND DeptId IN({0})", Utils.GetSqlIn<int>(mSearch.TourSellerDeptIds));
                }
                if (!string.IsNullOrEmpty(mSearch.JiDiaoYuanId))
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner AS A WHERE A.TourId=tbl_Tour.TourId AND A.PlanerId='{0}') ", mSearch.JiDiaoYuanId);
                }
                else if (!string.IsNullOrEmpty(mSearch.JiDiaoYuanName))
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner AS A WHERE A.TourId=tbl_Tour.TourId AND A.Planer LIKE '%{0}%') ", mSearch.JiDiaoYuanName);
                }
            }

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, TableName, fields.ToString(), cmdQuery.ToString(), OrderByString, heJiString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MTourStatus()
                    {
                        TourId = rdr.IsDBNull(rdr.GetOrdinal("TourId")) ? string.Empty : rdr.GetString(rdr.GetOrdinal("TourId")),
                        Guide = GetTourGuidByXml(rdr["GuideList"].ToString()),
                        Planer = GetTourPlanerByXml(rdr["PlanerList"].ToString()),
                        TourCode = rdr["TourCode"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        PersonNum = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")),
                        RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                        RouteName = rdr["RouteName"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"))
                    };
                    item.TourType = (TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    list.Add(item);
                }

                rdr.NextResult();

                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RenShuHeJi"))) heJi[0] = rdr.GetInt32(rdr.GetOrdinal("RenShuHeJi"));
                }
            }
            return list;
        }

        #endregion

        #region 游客统计表

        /// <summary>
        /// 游客统计表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TravellerFlowType">游客类型</param>
        /// <returns></returns>
        public IList<MTravellerFlow> GetTravellerFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MTravellerFlowSearch mSearch, EyouSoft.Model.EnumType.IndStructure.TravellerFlowType TravellerFlowType)
        {
            IList<MTravellerFlow> list = new List<MTravellerFlow>();
            EyouSoft.Model.StatStructure.MTravellerFlow item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "select * from view_TravellerFlow";
            string GroupString = " BuyProvincesId,ProvinceName ";
            string OrderByString = " PeopleNum desc ";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" sum(PeopleNum) PeopleNum,sum(PeopleNum*TourDays) PeopleDays,ProvinceName");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", companyId);
            cmdQuery.AppendFormat(" and DeptId in ({0})", deptIds);
            switch (TravellerFlowType)
            {
                case EyouSoft.Model.EnumType.IndStructure.TravellerFlowType.出镜游客:
                    cmdQuery.AppendFormat("and TourType in(4,5)");
                    break;
                case EyouSoft.Model.EnumType.IndStructure.TravellerFlowType.地接游客:
                    cmdQuery.AppendFormat("and TourType in(2,3)");
                    break;
                case EyouSoft.Model.EnumType.IndStructure.TravellerFlowType.组团游客:
                    cmdQuery.AppendFormat("and TourType in(0,1)");
                    break;

            }
            if (mSearch != null)
            {
                if (mSearch.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", mSearch.LDateS);
                }
                if (mSearch.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", mSearch.LDateE);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, fields.ToString()
                , cmdQuery.ToString(), OrderByString, GroupString, string.Empty))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.StatStructure.MTravellerFlow()
                    {
                        PeopleDayNum = rdr.IsDBNull(rdr.GetOrdinal("PeopleDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PeopleDays")),
                        PeopleNum = rdr.IsDBNull(rdr.GetOrdinal("PeopleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PeopleNum")),
                        Place = rdr["ProvinceName"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 根据ＸＭＬ获到计划计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlaner> GetTourPlanerByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourPlaner> list = new List<EyouSoft.Model.TourStructure.MTourPlaner>();
            EyouSoft.Model.TourStructure.MTourPlaner item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourPlaner()
                {
                    Planer = Utils.GetXAttributeValue(xRow, "Planer"),
                    PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 根据ＸＭＬ获到计划导游
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MGuidInfo> GetTourGuidByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MGuidInfo> list = new List<EyouSoft.Model.TourStructure.MGuidInfo>();
            EyouSoft.Model.TourStructure.MGuidInfo item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MGuidInfo()
                {
                    Name = Utils.GetXAttributeValue(xRow, "SourceName"),
                    GuidId = Utils.GetXAttributeValue(xRow, "SourceId")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
