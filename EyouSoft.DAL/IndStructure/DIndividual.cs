using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;

using EyouSoft.IDAL.IndStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Model.EnumType.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.DAL.IndStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;

    /// <summary>
    /// 个人中心
    /// 创建者：郑知远
    /// 创建时间：2011-09-15
    /// </summary>
    public class DIndividual : DALBase,IIndividual
    {
        #region 构造函数
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DIndividual()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 备忘录

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="mdl">备忘录实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddMemo(MMemo mdl)
        {
            var strSql = new StringBuilder();

            strSql.Append(" INSERT dbo.tbl_IndMemo");
            strSql.Append("         ( CompanyId ,");
            strSql.Append("           MemoTitle ,");
            strSql.Append("           MemoText ,");
            strSql.Append("           MemoTime ,");
            strSql.Append("           UrgentType ,");
            strSql.Append("           MemoState ,");
            strSql.Append("           OperatorId ,");
            strSql.Append("           IssueTime");
            strSql.Append("         )");
            strSql.Append(" VALUES  ( @CompanyId ,");
            strSql.Append("           @MemoTitle ,");
            strSql.Append("           @MemoText ,");
            strSql.Append("           @MemoTime ,");
            strSql.Append("           @UrgentType ,");
            strSql.Append("           @MemoState ,");
            strSql.Append("           @OperatorId ,");
            strSql.Append("           @IssueTime");
            strSql.Append("         )");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@MemoTitle", DbType.String, mdl.MemoTitle);
            this._db.AddInParameter(dc, "@MemoText", DbType.String, mdl.MemoText);
            this._db.AddInParameter(dc, "@MemoTime", DbType.DateTime, mdl.MemoTime);
            this._db.AddInParameter(dc, "@UrgentType", DbType.Byte, (int)mdl.UrgentType);
            this._db.AddInParameter(dc, "@MemoState", DbType.Byte, (int)mdl.MemoState);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 修改备忘录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMemo(MMemo model)
        {
            string sql = "update tbl_IndMemo set MemoTime=@MemoTime,UrgentType=@UrgentType,MemoState=@MemoState,MemoTitle=@MemoTitle,MemoText=@MemoText where Id=@Id and CompanyId=@CompanyId";
            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@Id", DbType.Int32, model.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "@MemoTitle", DbType.String, model.MemoTitle);
            this._db.AddInParameter(dc, "@MemoText", DbType.String, model.MemoText);
            this._db.AddInParameter(dc, "@MemoTime", DbType.DateTime, model.MemoTime);
            this._db.AddInParameter(dc, "@UrgentType", DbType.Byte, (int)model.UrgentType);
            this._db.AddInParameter(dc, "@MemoState", DbType.Byte, (int)model.MemoState);
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public bool DelMemo(int Id, string CompanyId)
        {
            string sql = "delete tbl_IndMemo where Id=@Id and CompanyId=@CompanyId";
            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@Id", DbType.Int32, Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }


        /// <summary>
        /// 根据备忘录编号获取备忘录详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns>备忘录详细信息</returns>
        public MMemo GetMemo(int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MMemo();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [MemoTitle] ,");
            strSql.Append("         [MemoText] ,");
            strSql.Append("         [MemoTime] ,");
            strSql.Append("         [UrgentType] ,");
            strSql.Append("         [MemoState] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [IssueTime]");
            strSql.Append(" FROM    [dbo].[tbl_IndMemo]");
            strSql.Append(" WHERE   Id = @Id");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.MemoTitle = dr["MemoTitle"].ToString();
                    mdl.MemoText = dr["MemoText"].ToString();
                    mdl.MemoTime = dr.GetDateTime(dr.GetOrdinal("MemoTime"));
                    mdl.UrgentType = (MemoUrgent)dr.GetByte(dr.GetOrdinal("UrgentType"));
                    mdl.MemoState = (MemoState)dr.GetByte(dr.GetOrdinal("MemoState"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据操作者编号获取指定数备忘录
        /// </summary>
        /// <param name="top">前几条记录,0表示全部</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="StartDate">备忘时间开始</param>
        /// <param name="EndDate">备忘时间结束</param>
        /// <returns>备忘录列表</returns>
        public IList<MMemo> GetMemoLst(int top, string operatorId, DateTime? StartDate, DateTime? EndDate)
        {
            var strSql = new StringBuilder();
            var lst = new List<MMemo>();

            strSql.Append(top == 0 ? " SELECT  [Id] ," : " SELECT TOP(@top)  [Id] ,");
            strSql.Append("[CompanyId] ,[MemoTitle], [MemoText], [MemoTime] ,[UrgentType],[MemoState], [OperatorId],[IssueTime] ");
            strSql.Append(" FROM    [dbo].[tbl_IndMemo]");
            strSql.Append(" WHERE   OperatorId = @OperatorId");
            if (StartDate.HasValue)
            {
                strSql.AppendFormat(" and datediff(day,'{0}',MemoTime)>=0", StartDate);
            }
            if (EndDate.HasValue)
            {
                strSql.AppendFormat(" and datediff(day,'{0}',MemoTime)<=0", EndDate);
            }
            strSql.AppendFormat(" order by MemoTime desc");
            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@top", DbType.Int32, top);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, operatorId);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                MMemo mdl = null;
                while (dr.Read())
                {
                    mdl = new MMemo();
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.MemoTitle = dr["MemoTitle"].ToString();
                    mdl.MemoText = dr["MemoText"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("MemoTime")))
                    {
                        mdl.MemoTime = dr.GetDateTime(dr.GetOrdinal("MemoTime"));
                    }
                    mdl.UrgentType = (MemoUrgent)dr.GetByte(dr.GetOrdinal("UrgentType"));
                    mdl.MemoState = (MemoState)dr.GetByte(dr.GetOrdinal("MemoState"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    lst.Add(mdl);
                }
            }
            return lst;
        }

        #endregion

        #region 事务提醒

        /// <summary>
        /// 订单提醒
        /// 说明：提醒订单销售员确认订单金额
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>订单列表</returns>
        public IList<MOrderRemind> GetOrderRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId)
        {
            IList<MOrderRemind> list = new List<MOrderRemind>();
            MOrderRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_TourOrder";
            string PrimaryKey = "OrderId";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" OrderId,OrderCode,BuyCompanyName,Adults,Childs,SumPrice ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" SellerId='{0}' and CompanyId='{1}' and ConfirmMoneyStatus=0 AND IsDelete = '0' AND OrderStatus={2}", operatorId, companyId,(int)OrderStatus.已成交);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MOrderRemind
                    {
                        OrderId = rdr["OrderId"].ToString(),
                        OrderCode = rdr["OrderCode"].ToString(),
                        Customer = rdr["BuyCompanyName"].ToString(),
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        SumPrice = rdr.IsDBNull(rdr.GetOrdinal("SumPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SumPrice")),
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 计调提醒
        /// 说明:提醒派团给计调时指定的计调员，未接收的均做提醒
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns></returns>
        public IList<MPlanRemind> GetPlanRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        {
            IList<MPlanRemind> list = new List<MPlanRemind>();
            MPlanRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourCode,RouteName,SellerName,LDate,Adults+Childs+Leaders PlanPeopleNumber,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus,TourType ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and TourStatus>={0} and CompanyId='{1}' and tourstatus<>{2}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收, companyId, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消);
            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId and isjieshou=0)", operatorId);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MPlanRemind
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        LDate = Utils.GetDateTime(rdr["LDate"].ToString()),
                        PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                        TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString()),
                        TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"))
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 收款提醒
        /// 说明:提醒客户单位责任销售员
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>欠款客户单位列表</returns>
        public IList<MReceivablesRemind> GetReceivablesRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        {
            IList<MReceivablesRemind> list = new List<MReceivablesRemind>();
            MReceivablesRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_tourorder";
            string PrimaryKey = "orderid";
            string OrderByString = "UpdateTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("tourid,OrderId,ordercode,ordertype,BuyCompanyId,BuyCompanyName,(select SellerName from tbl_crm where crmid=tbl_tourorder.BuyCompanyId) SellerName,ConfirmMoney-CheckMoney+ReturnMoney Arrear,ContactTel,ContactName,ConfirmMoney");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" isclean=0 and SellerId='{0}' and CompanyId='{1}' AND IsDelete = '0' AND OrderStatus={2}", operatorId, companyId,(int)OrderStatus.已成交);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MReceivablesRemind
                    {
                        TourId=rdr["TourId"].ToString(),
                        OrderId=rdr["OrderId"].ToString(),
                        OrderType=(OrderType)rdr.GetByte(rdr.GetOrdinal("ordertype")),
                        OrderCode=rdr["OrderCode"].ToString(),
                        CrmId = rdr["BuyCompanyId"].ToString(),
                        Customer = rdr["BuyCompanyName"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        Arrear = rdr.GetDecimal(rdr.GetOrdinal("Arrear")),
                        Phone = rdr["ContactTel"].ToString(),
                        Contact = rdr["ContactName"].ToString(),
                        ConfirmMoney = rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney"))
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 计划变更
        /// 说明:提醒相应的团队计调
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>变更信息列表</returns>
        public IList<MTourChangeRemind> GetTourChangeRemindLst(int pageSize, int pageIndex, ref int recordCount
 , string companyId, string operatorId)
        {
            IList<MTourChangeRemind> list = new List<MTourChangeRemind>();
            MTourChangeRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_TourPlanChange";
            string PrimaryKey = "Id";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.AppendFormat("*,(select DISTINCT SourceId,SourceName from tbl_Plan where TourId=tbl_TourPlanChange.TourId and Type={0} for xml raw,root) as Guid,(select * from tbl_TourPlaner where TourId=tbl_TourPlanChange.TourId for xml raw,root) as Planer",(int)PlanProject.导游);
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat("((Status < {2} and SaleId='{1}') or (Status ={2} and exists(select 1 from tbl_TourPlaner where PlanerId='{1}' and TourId=tbl_TourPlanChange.TourId and PlanerId not in (select ConfirmerId from tbl_TourPlanChangeconfirm where ConfirmerType=1 and tourid=tbl_TourPlaner.tourid)))) and CompanyId='{0}'", companyId, operatorId,(int)ChangeStatus.计调未确认);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MTourChangeRemind
                    {
                        Id=rdr.GetInt32(rdr.GetOrdinal("Id")),
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["TourCode"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        TourGuide = GetGuidByXml(rdr["Guid"].ToString()),
                        TourPlaner = GetTourPlanerByXml(rdr["Planer"].ToString()),
                        IssueTime =  rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Operator = rdr["Operator"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Content = rdr["Content"].ToString(),
                        ChangeType = (ChangeType)rdr.GetByte(rdr.GetOrdinal("ChangeType")),
                        State = (ChangeStatus)rdr.GetByte(rdr.GetOrdinal("status")),
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取计划变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MTourChangeRemind GetTourChangeModel(string CompanyId, int Id)
        {
            MTourChangeRemind info = null;
            DbCommand cmd = this._db.GetSqlStringCommand("select IssueTime,Operator,Title,Content from tbl_TourPlanChange where Id=@Id and CompanyId=@CompanyId");
            this._db.AddInParameter(cmd, "Id", DbType.Int32, Id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info = new MTourChangeRemind()
                    {
                        IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Operator = rdr["Operator"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Content = rdr["Content"].ToString(),
                    };
                }
            }
            return info;
        }

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        public bool TourChangeSure(MTourChangeRemind model)
        {
            string sql = "update tbl_TourPlanChange set Confirmer=@Confirmer,ConfirmerId=@ConfirmerId,ConfirmTime=@ConfirmTime where Id=@Id and CompanyId=@CompanyId; if not exists(select 1 from tbl_TourPlanChange where TourId=@TourId and State='0') update tbl_Tour set IsSure='1' where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            //this._db.AddInParameter(cmd, "Confirmer", DbType.String, model.Confirmer);
            //this._db.AddInParameter(cmd, "ConfirmerId", DbType.AnsiStringFixedLength, model.ConfirmerId);
            //this._db.AddInParameter(cmd, "ConfirmTime", DbType.DateTime, DateTime.Now);
            //this._db.AddInParameter(cmd, "Id", DbType.Int32, model.Id);
            //this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            //this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 订单变更
        /// 说明:提醒相应的团队计调
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="operatorId">用户编号</param>
        /// <returns>变更信息列表</returns>
        public IList<MOrderChangeRemind> GetOrderChangeRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, string operatorId)
        {
            IList<MOrderChangeRemind> list = new List<MOrderChangeRemind>();
            //MOrderChangeRemind model = null;
            //StringBuilder cmdQuery = new StringBuilder();
            //string TableName = "tbl_TourOrderChange";
            //string PrimaryKey = "Id";
            //string OrderByString = "IssueTime DESC";
            //StringBuilder fields = new StringBuilder();
            //#region 要查询的字段
            //fields.Append(" TourId,Id,OrderId,OrderCode,OrderSale,IssueTime,Operator,Content,IsSure ");
            //#endregion
            //#region 拼接查询条件
            //cmdQuery.AppendFormat(" IsSure='0' and CompanyId='{1}'", operatorId, companyId);
            //cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_TourOrderChange.TourId and PlanerId='{0}')", operatorId);
            //cmdQuery.AppendFormat(" AND ChangeType={0} ", (int)EyouSoft.Model.EnumType.TourStructure.ChangeType.变更);
            //#endregion
            //using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            //{
            //    while (rdr.Read())
            //    {
            //        model = new MOrderChangeRemind
            //        {
            //            Id = rdr["Id"].ToString(),
            //            TourId = rdr["TourId"].ToString(),
            //            OrderCode = rdr["OrderCode"].ToString(),
            //            OrderSale = rdr["OrderSale"].ToString(),
            //            IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
            //            Operator = rdr["Operator"].ToString(),
            //            Content = rdr["Content"].ToString(),
            //            IsSure = rdr["IsSure"].ToString() == "1" ? true : false
            //        };
            //        list.Add(model);
            //    }
            //}
            return list;
        }

        /// <summary>
        /// 获取订单变更实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public MOrderChangeRemind GetOrderChangeModel(string CompanyId, string Id)
        {
            MOrderChangeRemind info = null;
            DbCommand cmd = this._db.GetSqlStringCommand("select Id,IssueTime,Operator,Content from tbl_TourOrderChange where Id=@Id and CompanyId=@CompanyId");
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, Id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info = new MOrderChangeRemind()
                    {
                        Id = rdr["Id"].ToString(),
                        IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Operator = rdr["Operator"].ToString(),
                        Content = rdr["Content"].ToString(),
                    };
                }
            }
            return info;
        }

        /// <summary>
        /// 订单确认变更
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <returns></returns>
        public bool OrderChangeSure(MOrderChangeRemind model)
        {
            string sql = "update tbl_TourOrderChange set SurePerson=@SurePerson,SurePersonId=@SurePersonId,SureTime=@SureTime,IsSure='1' where Id=@Id and CompanyId=@CompanyId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "SurePerson", DbType.String, model.SurePerson);
            this._db.AddInParameter(cmd, "SurePersonId", DbType.AnsiStringFixedLength, model.SurePersonId);
            this._db.AddInParameter(cmd, "SureTime", DbType.DateTime, System.DateTime.Now);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, model.Id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 劳动合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param>
        /// <returns>劳动合同信息列表</returns>
        public IList<MLaborContractExpireRemind> GetLaborContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays)
        {
            IList<MLaborContractExpireRemind> list = new List<MLaborContractExpireRemind>();
            MLaborContractExpireRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_GovFileContract";
            string PrimaryKey = "";
            string OrderByString = "MaturityTime asc";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" (select top 1 FileNumber from tbl_GovFile where ID=tbl_GovFileContract.FileId) as FileNumber,[Name],MaturityTime,ContractNumber ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" exists(select 1 from tbl_GovFile where ID=tbl_GovFileContract.FileId and IsDelete=0 and CompanyId='{0}')", companyId);
            cmdQuery.AppendFormat(" and datediff(day,getdate(),MaturityTime)<={0}", EarlyDays);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MLaborContractExpireRemind
                    {
                        ContractNumber = rdr.IsDBNull(rdr.GetOrdinal("ContractNumber")) ? "" : rdr["ContractNumber"].ToString(),
                        FileNo = rdr.IsDBNull(rdr.GetOrdinal("FileNumber")) ? "" : rdr["FileNumber"].ToString(),
                        MaturityTime = rdr.IsDBNull(rdr.GetOrdinal("MaturityTime")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("MaturityTime")),
                        Name = rdr.IsDBNull(rdr.GetOrdinal("Name")) ? "" : rdr["Name"].ToString()
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 供应商合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param> 
        /// <returns>劳动合同信息列表</returns>
        public IList<MSourceContractExpireRemind> GetSourceContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays)
        {
            IList<MSourceContractExpireRemind> list = new List<MSourceContractExpireRemind>();
            MSourceContractExpireRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Source";
            string PrimaryKey = "SourceId";
            string OrderByString = "ContractPeriodEnd asc";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" SourceId,[Name],ContractPeriodEnd,ContractCode ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 AND IsSignContract=1 and CompanyId='{0}'", companyId);
            cmdQuery.AppendFormat(" and datediff(day,getdate(),ContractPeriodEnd)<={0}", EarlyDays);

            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MSourceContractExpireRemind
                    {
                        Source = rdr["Name"].ToString(),
                        MaturityTime = rdr.IsDBNull(rdr.GetOrdinal("ContractPeriodEnd")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("ContractPeriodEnd")),
                        ContractNumber = rdr["ContractCode"].ToString()
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 公司合同到期提醒
        /// 说明：提醒权限人
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="EarlyDays">提前天数提醒</param> 
        /// <returns>劳动合同信息列表</returns>
        public IList<MCompanyContractExpireRemind> GetCompanyContractExpireRemindLst(int pageSize, int pageIndex, ref int recordCount, string companyId, int EarlyDays)
        {
            IList<MCompanyContractExpireRemind> list = new List<MCompanyContractExpireRemind>();
            MCompanyContractExpireRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_GovContract";
            string PrimaryKey = "ID";
            string OrderByString = "MaturityTime asc";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" ID,Company,[Type],MaturityTime,[Number] ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsRemind=1 and CompanyId='{0}'", companyId);
            cmdQuery.AppendFormat(" and datediff(day,getdate(),MaturityTime)<={0}", EarlyDays);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MCompanyContractExpireRemind
                    {
                        ContractNumber = rdr.IsDBNull(rdr.GetOrdinal("Number")) ? "" : rdr["Number"].ToString(),
                        CompanyName = rdr.IsDBNull(rdr.GetOrdinal("Company")) ? "" : rdr["Company"].ToString(),
                        MaturityTime = rdr.IsDBNull(rdr.GetOrdinal("MaturityTime")) ? System.DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("MaturityTime")),
                        Type = rdr.IsDBNull(rdr.GetOrdinal("Type")) ? "" : rdr["Type"].ToString()
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 询价提醒
        /// 提醒询价时指定的计调员
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns></returns>
        public IList<MInquiryRemind> GetInquiryRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, string companyId)
        {
            IList<MInquiryRemind> list = new List<MInquiryRemind>();
            MInquiryRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Quote";
            string PrimaryKey = "QuoteId";
            string OrderByString = "IssueTime desc";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" QuoteId,RouteName,BuyCompanyName,Days,(Adults+Childs) as PersonNum,SellerName,Operator,QuoteType,CompanyId ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and IsPlanerQuote='0' and PlanerId='{1}' AND IsDelete='0'", companyId, operatorId);
            //cmdQuery.AppendFormat("AND  QuoteStatus={0} ", (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MInquiryRemind
                    {
                        QuoteId = rdr["QuoteId"].ToString(),
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        CompanyId = rdr["CompanyId"].ToString(),
                        Days = rdr.IsDBNull(rdr.GetOrdinal("Days")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Days")),
                        Operator = rdr["Operator"].ToString(),
                        PersonNum = rdr.IsDBNull(rdr.GetOrdinal("PersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PersonNum")),
                        //QuoteType = (EyouSoft.Model.EnumType.TourStructure.ModuleType)rdr.GetByte(rdr.GetOrdinal("QuoteType")),
                        RouteName = rdr["RouteName"].ToString(),
                        SellerName = rdr["SellerName"].ToString()
                    };
                    list.Add(model);
                }
            }
            return list;
        }

        ///// <summary>
        ///// 根据键名获取值
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="SettingType"></param>
        ///// <returns></returns>
        //public string GetSettingByType(string CompanyId, SysConfiguration SettingType)
        //{
        //    string sql = "SELECT CompanyId,[Key],[Value] FROM tbl_ComSetting WHERE CompanyId = @CompanyId and [Key]=@Key";
        //    DbCommand comm = this._db.GetSqlStringCommand(sql);
        //    this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
        //    this._db.AddInParameter(comm, "Key", DbType.String, (int)SettingType);
        //    string value = string.Empty;
        //    using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
        //    {
        //        if (reader.Read())
        //        {
        //            value = reader.IsDBNull(reader.GetOrdinal("Value")) ? "0" : reader.GetString(reader.GetOrdinal("Value"));
        //        }
        //    }
        //    return value;
        //}

        ///// <summary>
        ///// 修改公司配置项
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <param name="Value"></param>
        ///// <param name="CompanyId"></param>
        ///// <returns></returns>
        //public bool UpdateComSetting(SysConfiguration Key, string Value, string CompanyId)
        //{
        //    string sql = "if not exists(select 1 from tbl_ComSetting where CompanyId = @CompanyId and [Key]=@Key)  insert into tbl_ComSetting(CompanyId,[Key],[Value]) values(@CompanyId,@Key,@Value) else update tbl_ComSetting set [Value]=@Value  WHERE CompanyId = @CompanyId and [Key]=@Key";
        //    DbCommand comm = this._db.GetSqlStringCommand(sql);
        //    this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
        //    this._db.AddInParameter(comm, "@Key", DbType.String, ((int)Key).ToString());
        //    this._db.AddInParameter(comm, "@Value", DbType.String, Value);
        //    int result = DbHelper.ExecuteSql(comm, this._db);
        //    return result > 0 ? true : false;
        //}

        /// <summary>
        /// 根据类型得到提醒数
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OperatorId"></param>
        /// <param name="type"></param>
        /// <param name="setting">系统配置信息业务实体</param>
        /// <returns></returns>
        public int GetRemindCountByType(string CompanyId, string OperatorId, RemindType type,EyouSoft.Model.ComStructure.MComSetting setting)
        {
            int TipCount = 0;
            StringBuilder cmdQuery = new StringBuilder();
            switch (type)
            {
                case RemindType.变更提醒:
                    {
                        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_TourPlanChange where ((Status <{2} and SaleId='{1}') or (Status ={2} and exists(select 1 from tbl_TourPlaner where PlanerId='{1}' and TourId=tbl_TourPlanChange.TourId and PlanerId not in (select ConfirmerId from tbl_TourPlanChangeconfirm where ConfirmerType=1 and tourid=tbl_TourPlaner.tourid)))) and CompanyId='{0}'", CompanyId, OperatorId,(int)ChangeStatus.计调未确认);
                        break;
                    }
                //case RemindType.订单提醒:
                //    {
                //        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_TourOrder where CompanyId='{0}' and SellerId='{1}' and ConfirmMoneyStatus=0",CompanyId,OperatorId);
                //        break;
                //    }
                //case RemindType.合同到期提醒:
                //    {
                //        //cmdQuery.Append("select sum(TipCount) from (");
                //        //cmdQuery.AppendFormat(" select count(*) as TipCount from tbl_GovFileContract where exists(select 1 from tbl_GovFile where ID=tbl_GovFileContract.FileId and IsDelete=0 and CompanyId='{0}') and datediff(day,getdate(),MaturityTime)<={1}", CompanyId, setting.ContractRemind);
                //        //cmdQuery.Append(" union all ");
                //        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_Source where IsDelete='0' AND IsHeTong=1 and CompanyId='{0}' and exists(select 1 from tbl_SourceHeTong where SourceId=tbl_Source.sourceid and datediff(day,getdate(),Etime)<={1})", CompanyId, setting.SContractRemind);
                //        //cmdQuery.Append(" union all ");
                //        //cmdQuery.AppendFormat("select count(*) as TipCount from tbl_GovContract where IsRemind=1 and CompanyId='{0}' and datediff(day,getdate(),MaturityTime)<={1}) AS a", CompanyId, setting.ComPanyContractRemind);
                //        break;
                //    }
                case RemindType.计调提醒:
                    {
                        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_Tour where IsDelete='0' and TourStatus>={0} and tourstatus<>{3} and CompanyId='{1}' and exists(select 1 from tbl_TourPlaner where PlanerId='{2}' and TourId=tbl_Tour.TourId and isjieshou=0)", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收, CompanyId, OperatorId, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消);

                        break;
                    }
                case RemindType.收款提醒:
                    {
                        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_TourOrder where IsClean=0 and SellerId='{0}' and CompanyId='{1}' AND IsDelete = '0' AND OrderStatus={2}", OperatorId, CompanyId,(int)OrderStatus.已成交);
                        break;
                    }
                //case RemindType.询价提醒:
                //    {
                //        cmdQuery.AppendFormat("select count(*) as TipCount from tbl_Quote where CompanyId='{0}' and QuoteStatus='0' and OperatorId='{1}' and IsLatest=1 AND IsDelete='0'", CompanyId, OperatorId);
                //        break;
                //    }
                ////case RemindType.预控到期提醒:
                ////    {
                ////        cmdQuery.Append("select sum(TipCount) from (");
                ////        cmdQuery.AppendFormat("select count(*) as TipCount from view_SourceSueHotel where CompanyId='{0}' AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0  AND datediff(day,getdate(),LastTime)<={1}", CompanyId, setting.HotelControlRemind);
                ////        cmdQuery.Append(" union all ");
                ////        cmdQuery.AppendFormat("select count(*) as TipCount from view_SourceSueCar where CompanyId='{0}' AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0  AND datediff(day,getdate(),LastTime)<={1}", CompanyId, setting.CarControlRemind);
                ////        cmdQuery.Append(" union all ");
                ////        cmdQuery.AppendFormat("select count(*) as TipCount from view_SourceSueShip where CompanyId='{0}' AND (isnull(ControlNum,0)-isnull(AlreadyNum,0))>0  AND datediff(day,getdate(),LastTime)<={1}) AS a", CompanyId, setting.ShipControlRemind);
                ////        break;
                ////    }
            }
            DbCommand cmd = this._db.GetSqlStringCommand(cmdQuery.ToString());
            TipCount = (int)(DbHelper.GetSingle(cmd, this._db));
            return TipCount;
        }

        #endregion

        #region 工作汇报

        /// <summary>
        /// 添加工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddWorkReport(MWorkReport mdl)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_WorkReport_AddOrUpdate");
            this._db.AddInParameter(dc, "@Id", DbType.Int32, 0);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Title", DbType.String, mdl.Title);
            this._db.AddInParameter(dc, "@Content", DbType.String, mdl.Content);
            this._db.AddInParameter(dc, "@UploadUrl", DbType.String, mdl.UploadUrl);
            this._db.AddInParameter(dc, "@DepartmentId", DbType.Int32, mdl.DepartmentId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@OperatorName", DbType.String, mdl.OperatorName);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@CheckList", DbType.Xml, CreateReportCheckerXml(mdl.list));
            _db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);
            return Convert.ToInt32(_db.GetParameterValue(dc, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 修改工作汇报
        /// </summary>
        /// <param name="mdl">汇报实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdWorkReport(MWorkReport mdl)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_WorkReport_AddOrUpdate");
            this._db.AddInParameter(dc, "@Id", DbType.Int32, mdl.Id);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Title", DbType.String, mdl.Title);
            this._db.AddInParameter(dc, "@Content", DbType.String, mdl.Content);
            this._db.AddInParameter(dc, "@UploadUrl", DbType.String, mdl.UploadUrl);
            this._db.AddInParameter(dc, "@DepartmentId", DbType.Int32, mdl.DepartmentId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@OperatorName", DbType.String, mdl.OperatorName);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@CheckList", DbType.Xml, CreateReportCheckerXml(mdl.list));
            _db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);
            return Convert.ToInt32(_db.GetParameterValue(dc, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="ids">汇报编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelWorkReport(params int[] ids)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat("delete from tbl_IndWorkReportCheck where WorkId in({0}) DELETE FROM [tbl_IndWorkReport] WHERE Id IN ({0})", Utils.GetSqlIdStrByArray(ids));
            var dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据汇报编号获取工作汇报实体
        /// </summary>
        /// <param name="id">汇报编号</param>
        /// <returns>工作汇报实体</returns>
        public MWorkReport GetWorkReport(int id)
        {
            var strSql = new StringBuilder();
            var mdl = new MWorkReport();

            strSql.Append(" SELECT  [Id] ,");
            strSql.Append("         [CompanyId] ,");
            strSql.Append("         [Title] ,");
            strSql.Append("         [Content] ,");
            strSql.Append("         [UploadUrl] ,");
            strSql.Append("         [DepartmentId] ,");
            strSql.Append("         [Status] ,");
            strSql.Append("         [OperatorId] ,");
            strSql.Append("         [OperatorName] ,");
            strSql.Append("         [IssueTime],(select * from tbl_IndWorkReportCheck where WorkId=tbl_IndWorkReport.Id for xml raw,root) as CheckList,(select DepartName from tbl_ComDepartment where DepartId=tbl_IndWorkReport.DepartmentId) as Department");
            strSql.Append(" FROM    tbl_IndWorkReport");
            strSql.Append(" WHERE   Id = @Id");

            var dc = this._db.GetSqlStringCommand(strSql.ToString());

            this._db.AddInParameter(dc, "@Id", DbType.Int32, id);

            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.Title = dr["Title"].ToString();
                    mdl.Content = dr["Content"].ToString();
                    mdl.UploadUrl = dr["UploadUrl"].ToString();
                    mdl.DepartmentId = dr.IsDBNull(dr.GetOrdinal("DepartmentId")) ? 0 : dr.GetInt32(dr.GetOrdinal("DepartmentId"));
                    mdl.Status = (EyouSoft.Model.EnumType.IndStructure.Status)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.OperatorName = dr["OperatorName"].ToString();
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.list = GetWorkReportCheckByXml(dr["CheckList"].ToString());
                    mdl.Department = dr["Department"].ToString();
                }
            }
            return mdl;

        }

        /// <summary>
        /// 根据工作汇报搜索实体获取工作汇报列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">工作汇报搜索实体</param>
        /// <returns>工作汇报列表</returns>
        public IList<MWorkReport> GetWorkReportLst(string CompanyId, int pageSize, int pageIndex, ref int recordCount, MWorkReportSearch mSearch)
        {
            IList<MWorkReport> list = new List<MWorkReport>();
            MWorkReport mdl = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_IndWorkReport";
            string PrimaryKey = "Id";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" *,(select * from tbl_IndWorkReportCheck where WorkId=tbl_IndWorkReport.Id for xml raw,root) as CheckList,(select DepartName from tbl_ComDepartment where DepartId=tbl_IndWorkReport.DepartmentId) as Department");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", CompanyId);
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.Title))
                {
                    cmdQuery.AppendFormat(" and Title like '%{0}%'", Utils.ToSqlLike(mSearch.Title));
                }
                if (mSearch.IssueTimeS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", mSearch.IssueTimeS);
                }
                if (mSearch.IssueTimeE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", mSearch.IssueTimeE);
                }
                if (!string.IsNullOrEmpty(mSearch.OperatorName))
                {
                    cmdQuery.AppendFormat(" and OperatorName like '%{0}%'", Utils.ToSqlLike(mSearch.OperatorName));
                }
            }
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (dr.Read())
                {
                    mdl = new MWorkReport();
                    mdl.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.Title = dr["Title"].ToString();
                    mdl.Content = dr["Content"].ToString();
                    mdl.UploadUrl = dr.IsDBNull(dr.GetOrdinal("UploadUrl")) ? "" : dr.GetString(dr.GetOrdinal("UploadUrl"));
                    mdl.DepartmentId = dr.GetInt32(dr.GetOrdinal("DepartmentId"));

                    mdl.Status = (Status)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.list = GetWorkReportCheckByXml(dr["CheckList"].ToString());
                    mdl.Department = dr["Department"].ToString();
                    list.Add(mdl);
                }
                return list;
            }
        }

        /// <summary>
        /// 审批工作汇报
        /// </summary>
        /// <param name="model">工作汇报实体</param>
        /// <returns></returns>
        public bool SetWorkReportStatus(MWorkReportCheck model)
        {
            string sql = "update [tbl_IndWorkReportCheck] set ApproverId=@ApproverId,Approver=@Approver,ApproveTime=@ApproveTime,Comment=@Comment,[Status]=1 where Id=@Id and WorkId=@WorkId; if(not exists(select 1 from tbl_IndWorkReportCheck where WorkId=@WorkId and Status=0))  update tbl_IndWorkReport set Status=1 where Id=@WorkId";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "ApproverId", DbType.AnsiStringFixedLength, model.ApproverId);
            this._db.AddInParameter(dc, "Approver", DbType.String, model.Approver);
            this._db.AddInParameter(dc, "ApproveTime", DbType.DateTime, model.ApproveTime);
            this._db.AddInParameter(dc, "Comment", DbType.String, model.Comment);
            this._db.AddInParameter(dc, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(dc, "WorkId", DbType.Int32, model.WorkId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        #endregion

        #region 工作计划

        /// <summary>
        /// 添加工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddWorkPlan(MWorkPlan mdl)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_WorkPlan_AddOrUpdate");
            this._db.AddInParameter(dc, "@WorkPlanId", DbType.Int32, 0);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Title", DbType.String, mdl.Title);
            this._db.AddInParameter(dc, "@UploadUrl", DbType.String, mdl.UploadUrl);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@OperatorName", DbType.String, mdl.OperatorName);
            this._db.AddInParameter(dc, "@ScheduledTime", DbType.DateTime, mdl.ScheduledTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@CheckList", DbType.Xml, CreatePlanCheckerXml(mdl.list));
            _db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);
            return Convert.ToInt32(_db.GetParameterValue(dc, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="mdl">计划实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdWorkPlan(MWorkPlan mdl)
        {
            DbCommand dc = _db.GetStoredProcCommand("proc_WorkPlan_AddOrUpdate");
            this._db.AddInParameter(dc, "@WorkPlanId", DbType.Int32, mdl.WorkPlanId);
            this._db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this._db.AddInParameter(dc, "@Title", DbType.String, mdl.Title);
            this._db.AddInParameter(dc, "@UploadUrl", DbType.String, mdl.UploadUrl);
            this._db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this._db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this._db.AddInParameter(dc, "@OperatorName", DbType.String, mdl.OperatorName);
            this._db.AddInParameter(dc, "@ScheduledTime", DbType.DateTime, mdl.ScheduledTime);
            this._db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this._db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this._db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this._db.AddInParameter(dc, "@CheckList", DbType.Xml, CreatePlanCheckerXml(mdl.list));
            _db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(dc, _db);
            return Convert.ToInt32(_db.GetParameterValue(dc, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 删除工作汇报
        /// </summary>
        /// <param name="workPlanIds">工作计划编号集合</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelWorkPlan(params string[] workPlanIds)
        {
            var xml = new XmlDocument();
            var sbxml = new StringBuilder();

            var dc = this._db.GetStoredProcCommand("proc_IndWorkPlan_Del");

            sbxml.Append("<Values>");
            foreach (var i in workPlanIds)
            {
                sbxml.Append("<Value Id=\"" + i + "\"/>");
            }
            sbxml.Append("</Values>");
            xml.LoadXml(sbxml.ToString());

            this._db.AddInParameter(dc, "@Values", DbType.Xml, xml.InnerXml);

            return DbHelper.ExecuteSql(dc, this._db) > 0;
        }

        /// <summary>
        /// 根据工作计划编号获取工作计划实体
        /// </summary>
        /// <param name="workPlanId">工作计划编号</param>
        /// <returns>工作计划实体</returns>
        public MWorkPlan GetWorkPlan(string workPlanId)
        {
            var strSql = new StringBuilder();
            var mdl = new MWorkPlan();

            strSql.Append(" SELECT  *,(select * from tbl_IndWorkPlanCC where WorkPlanId=tbl_IndWorkPlan.WorkPlanId for xml raw,root) as CheckList,(select DepartName from tbl_ComDepartment where DepartId=tbl_IndWorkPlan.DeptId) as Department");
            strSql.Append(" FROM    tbl_IndWorkPlan");
            strSql.Append(" WHERE   WorkPlanId = @workPlanId");
            var dc = this._db.GetSqlStringCommand(strSql.ToString());
            this._db.AddInParameter(dc, "@workPlanId", DbType.Int32, workPlanId);
            using (var dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    mdl.WorkPlanId = dr.GetInt32(dr.GetOrdinal("WorkPlanId"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.Title = dr.GetString(dr.GetOrdinal("Title"));
                    mdl.UploadUrl = dr.IsDBNull(dr.GetOrdinal("UploadUrl")) ? "" : dr.GetString(dr.GetOrdinal("UploadUrl"));
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));

                    mdl.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    mdl.Status = (EyouSoft.Model.EnumType.IndStructure.Status)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.ActualTime = dr.IsDBNull(dr.GetOrdinal("ActualTime")) ? System.DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("ActualTime"));
                    mdl.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.ScheduledTime = dr.GetDateTime(dr.GetOrdinal("ScheduledTime"));
                    mdl.list = GetWorkPlanCheckByXml(dr["CheckList"].ToString());
                    mdl.Department = dr["Department"].ToString();
                }
            }
            return mdl;

        }

        /// <summary>
        /// 根据工作计划搜索实体获取工作计划列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="mSearch"></param>
        /// <returns></returns>
        public IList<MWorkPlan> GetWorkPlanLst(string CompanyId, int pageSize, int pageIndex, ref int recordCount, MWorkPlanSearch mSearch)
        {
            IList<MWorkPlan> list = new List<MWorkPlan>();
            MWorkPlan mdl = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_IndWorkPlan";
            string PrimaryKey = "WorkPlanId";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" *,(select * from tbl_IndWorkPlanCC where WorkPlanId=tbl_IndWorkPlan.WorkPlanId for xml raw,root) as CheckList");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", CompanyId);
            if (mSearch != null)
            {
                if (!string.IsNullOrEmpty(mSearch.Title))
                {
                    cmdQuery.AppendFormat(" and Title like '%{0}%'", Utils.ToSqlLike(mSearch.Title));
                }
                if (mSearch.IssueTimeS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0", mSearch.IssueTimeS);
                }
                if (mSearch.IssueTimeE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0", mSearch.IssueTimeE);
                }
                if (!string.IsNullOrEmpty(mSearch.OperatorName))
                {
                    cmdQuery.AppendFormat(" and OperatorName like '%{0}%'", Utils.ToSqlLike(mSearch.OperatorName));
                }
            }
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (dr.Read())
                {
                    mdl = new MWorkPlan();
                    mdl.WorkPlanId = dr.GetInt32(dr.GetOrdinal("WorkPlanId"));
                    mdl.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    mdl.Title = dr.GetString(dr.GetOrdinal("Title"));

                    mdl.UploadUrl = dr.IsDBNull(dr.GetOrdinal("UploadUrl")) ? "" : dr.GetString(dr.GetOrdinal("UploadUrl"));
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));

                    mdl.Status = (Status)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.OperatorName = dr.GetString(dr.GetOrdinal("OperatorName"));
                    mdl.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.ScheduledTime = dr.GetDateTime(dr.GetOrdinal("ScheduledTime"));
                    mdl.ActualTime = dr.IsDBNull(dr.GetOrdinal("ActualTime")) ? System.DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("ActualTime"));
                    mdl.list = GetWorkPlanCheckByXml(dr["CheckList"].ToString());
                    list.Add(mdl);
                }
                return list;
            }
        }

        /// <summary>
        /// 审核工作计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetWorkPlanStatus(MWorkPlanCheck model)
        {
            string sql = "update [tbl_IndWorkPlanCC] set ApproverId=@ApproverId,Approver=@Approver,ApproveTime=@ApproveTime,Comment=@Comment,[Status]=1 where Id=@Id and WorkPlanId=@WorkPlanId; if(not exists(select 1 from tbl_IndWorkPlanCC where WorkPlanId=@WorkPlanId and Status=0))  update tbl_IndWorkPlan set Status=1 where WorkPlanId=@WorkPlanId";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "ApproverId", DbType.AnsiStringFixedLength, model.ApproverId);
            this._db.AddInParameter(dc, "Approver", DbType.String, model.Approver);
            this._db.AddInParameter(dc, "ApproveTime", DbType.DateTime, model.ApproveTime);
            this._db.AddInParameter(dc, "Comment", DbType.String, model.Comment);
            this._db.AddInParameter(dc, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(dc, "WorkPlanId", DbType.Int32, model.WorkPlanId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 工作计划结束
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetWorkPlanEnd(MWorkPlan model)
        {
            string sql = "update tbl_IndWorkPlan set ActualTime=@ActualTime,Result=@Result,Status=@Status where WorkPlanId=@WorkPlanId and CompanyId=@CompanyId";
            var dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "ActualTime", DbType.DateTime, model.ActualTime);
            this._db.AddInParameter(dc, "Result", DbType.String, model.Result);
            this._db.AddInParameter(dc, "Status", DbType.Byte, (int)model.Status);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "WorkPlanId", DbType.Int32, model.WorkPlanId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        #endregion

        #region 公告通知
        /// <summary>
        /// 公告通知
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="operatorId">用户编号</param>
        /// <param name="deptid">用户部门编号</param>  
        /// <param name="companyId">系统公司编号</param>
        /// <returns>公告列表</returns>
        public IList<MNoticeRemind> GetNoticeRemindLst(int pageSize, int pageIndex, ref int recordCount, string operatorId, int deptid, string companyId)
        {
            IList<MNoticeRemind> list = new List<MNoticeRemind>();
            MNoticeRemind model = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_GovNotice";
            string PrimaryKey = "NoticeId";
            string OrderByString = "IssueTime desc";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" NoticeId,[Title],Operator,IssueTime,Views,(select DepartName from tbl_ComDepartment where DepartId=tbl_GovNotice.DepartId) as DepartName ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and IsRemind=1", companyId);
            cmdQuery.AppendFormat(" and exists(select 1 from tbl_GovNoticeReceiver where NoticeId=tbl_GovNotice.NoticeId and (ItemType={0} or (ItemType={1} and ItemId='{2}')))", (int)EyouSoft.Model.EnumType.GovStructure.ItemType.公司内部, (int)EyouSoft.Model.EnumType.GovStructure.ItemType.指定部门, deptid);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    model = new MNoticeRemind
                    {
                        NoticeId = rdr["NoticeId"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Operator = rdr["Operator"].ToString(),
                        Views = rdr.IsDBNull(rdr.GetOrdinal("Views")) ? 0 : rdr.GetInt32(rdr.GetOrdinal(("Views"))),
                        DepartName = rdr["DepartName"].ToString(),
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))
                    };
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 个人密码修改
        /// <summary>
        /// 个人密码修改
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <param name="MD5Pwd">MD5密码</param>
        /// <returns></returns>
        public bool PwdModify(string UserId, string OldPwd, string NewPwd, string MD5Pwd)
        {
            string sql = "update tbl_ComUser set Password=@NewPwd,MD5Password=@MD5Pwd where UserId=@UserId and Password=@OldPwd";
            DbCommand dc = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(dc, "NewPwd", DbType.String, NewPwd);
            this._db.AddInParameter(dc, "OldPwd", DbType.String, OldPwd);
            this._db.AddInParameter(dc, "MD5Pwd", DbType.String, MD5Pwd);
            this._db.AddInParameter(dc, "UserId", DbType.String, UserId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 创建工作汇报审核人XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateReportCheckerXml(IList<EyouSoft.Model.IndStructure.MWorkReportCheck> list)
        {
            //<Root><CheckList WorkId="工作汇报编号" ApproverId="审核人ID" Approver="审核人" ApproveTime="审核时间" Comment="审批意见" Status="审核状态"/></Root>

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<CheckList WorkId=\"{0}\" ApproverId=\"{1}\" Approver=\"{2}\" ApproveTime=\"{3}\" Comment=\"{4}\" Status=\"{5}\"/>", item.WorkId, item.ApproverId, item.Approver, item.ApproveTime, item.Comment, (int)item.Status);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 创建工作计划审核人XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreatePlanCheckerXml(IList<EyouSoft.Model.IndStructure.MWorkPlanCheck> list)
        {
            //<Root><CheckList WorkPlanId="工作汇报编号" ApproverId="审核人ID" Approver="审核人" ApproveTime="审核时间" Comment="审批意见" Status="审核状态"/></Root>

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<CheckList WorkPlanId=\"{0}\" ApproverId=\"{1}\" Approver=\"{2}\" ApproveTime=\"{3}\" Comment=\"{4}\" Status=\"{5}\"/>", item.WorkPlanId, item.ApproverId, item.Approver, item.ApproveTime, item.Comment, (int)item.Status);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 得到工作汇报审核人
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.IndStructure.MWorkReportCheck> GetWorkReportCheckByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.IndStructure.MWorkReportCheck> list = new List<EyouSoft.Model.IndStructure.MWorkReportCheck>();
            EyouSoft.Model.IndStructure.MWorkReportCheck item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.IndStructure.MWorkReportCheck()
                {
                    Approver = Utils.GetXAttributeValue(xRow, "Approver"),
                    ApproverId = Utils.GetXAttributeValue(xRow, "ApproverId"),
                    ApproveTime = Utils.GetDateTime(Utils.GetXAttributeValue(xRow, "ApproveTime")),
                    Comment = Utils.GetXAttributeValue(xRow, "Comment"),
                    Id = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Id")),
                    WorkId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "WorkId")),
                    Status = (Status)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Status")),
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到工作计划审核人
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.IndStructure.MWorkPlanCheck> GetWorkPlanCheckByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.IndStructure.MWorkPlanCheck> list = new List<EyouSoft.Model.IndStructure.MWorkPlanCheck>();
            EyouSoft.Model.IndStructure.MWorkPlanCheck item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.IndStructure.MWorkPlanCheck()
                {
                    Approver = Utils.GetXAttributeValue(xRow, "Approver"),
                    ApproverId = Utils.GetXAttributeValue(xRow, "ApproverId"),
                    ApproveTime = Utils.GetDateTime(Utils.GetXAttributeValue(xRow, "ApproveTime")),
                    Comment = Utils.GetXAttributeValue(xRow, "Comment"),
                    Id = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Id")),
                    WorkPlanId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "WorkPlanId")),
                    Status = (Status)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Status")),
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 获得计划项目安排落实实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private EyouSoft.Model.HTourStructure.MTourPlanStatus GetTourPlanStatus(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            EyouSoft.Model.HTourStructure.MTourPlanStatus item = new EyouSoft.Model.HTourStructure.MTourPlanStatus();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item.TourId = Utils.GetXAttributeValue(xRow, "TourId");
                item.Car = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Car")));
                item.Dining = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Dining")));
                item.DJ = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "DJ")));
                item.Guide = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Guide")));
                item.Hotel = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Hotel")));
                item.LL = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "LL")));
                item.CarTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CarTicket")));
                item.TrainTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "TrainTicket")));
                item.PlaneTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlaneTicket")));
                item.Other = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Other")));
                item.CShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CShip")));
                item.FShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "FShip")));
                item.Shopping = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Shopping")));
                item.Spot = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Spot")));

            }
            return item;
        }

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
        /// 根据ＸＭＬ获到导游信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MGuidInfo> GetGuidByXml(string xml)
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
                    GuidId = Utils.GetXAttributeValue(xRow, "SourceId"),
                    Name = Utils.GetXAttributeValue(xRow, "SourceName")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
