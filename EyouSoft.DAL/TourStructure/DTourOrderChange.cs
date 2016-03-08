using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.TourStructure;
using System.Data.Common;
using EyouSoft.Toolkit.DAL;
using System.Data;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.DAL.TourStructure
{
    public class DTourOrderChange : EyouSoft.Toolkit.DAL.DALBase, ITourOrderChange
    {
        #region 数据库变量
        private Microsoft.Practices.EnterpriseLibrary.Data.Database _db = null;
        #endregion

        #region 构造函数
        public DTourOrderChange()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region ITourOrderChange 成员


        /// <summary>
        /// 确认订单变更
        /// </summary>
        /// <param name="id">变更编号</param>
        /// <param name="surePersonId">确认人编号</param>
        /// <param name="surePerson">确认人</param>
        /// <returns></returns>
        public int UpdateTourOrderChange(string id, string surePersonId, string surePerson)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderChange_Update");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, id);
            _db.AddInParameter(cmd, "SurePersonId", DbType.AnsiStringFixedLength, surePersonId);
            _db.AddInParameter(cmd, "SurePerson", DbType.String, surePerson);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }


        /// <summary>
        /// 根据订单的编号、（修改或变更）获取订单变更列表
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="changeType">变更或修改</param>
        /// <returns></returns>
        public IList<MTourOrderChange> GetTourOrderChangeList(string OrderId, ChangeType? changeType)
        {

            IList<MTourOrderChange> list = null;

            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("Id,CompanyId,TourId,RouteId,RouteName,TourSaleId,TourSale,OrderId,OrderCode");
            query.Append(",ChangePerson,ChangePrice,OrderSaleId,OrderSale,Content,OperatorId");
            query.Append(",Operator,IssueTime,ChangeType,IsSure,SurePersonId,SurePerson,SureTime");
            query.Append(" FROM ");
            query.Append("tbl_TourOrderChange");
            query.Append(" Where ");
            query.AppendFormat("OrderId='{0}'", OrderId);

            if (changeType.HasValue)
            {
                query.AppendFormat(" and ChangeType={0}", (int)changeType.Value);
            }

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderChange>();

                    while (dr.Read())
                    {
                        MTourOrderChange change = new MTourOrderChange();

                        change.Id = dr.GetString(dr.GetOrdinal("Id"));

                        change.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));

                        change.TourId = dr.GetString(dr.GetOrdinal("TourId"));

                        change.RouteId = !dr.IsDBNull(dr.GetOrdinal("RouteId")) ? dr.GetString(dr.GetOrdinal("RouteId")) : string.Empty;

                        change.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr["RouteName"].ToString() : string.Empty;

                        change.TourSaleId = dr.GetString(dr.GetOrdinal("TourSaleId"));

                        change.TourSale = !dr.IsDBNull(dr.GetOrdinal("TourSale")) ? dr["TourSale"].ToString() : string.Empty;

                        change.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));

                        change.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;

                        change.ChangePerson = dr.GetInt32(dr.GetOrdinal("ChangePerson"));

                        change.ChangePrice = dr.GetDecimal(dr.GetOrdinal("ChangePrice"));

                        change.OrderSaleId = dr.GetString(dr.GetOrdinal("OrderSaleId"));

                        change.OrderSale = !dr.IsDBNull(dr.GetOrdinal("OrderSale")) ? dr["OrderSale"].ToString() : string.Empty;

                        change.Content = !dr.IsDBNull(dr.GetOrdinal("Content")) ? dr["Content"].ToString() : string.Empty;

                        change.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));

                        change.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr["Operator"].ToString() : string.Empty;

                        change.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        change.ChangeType = (ChangeType)dr.GetByte(dr.GetOrdinal("ChangeType"));

                        change.IsSure = dr.GetString(dr.GetOrdinal("IsSure")) == "1" ? true : false;

                        change.SurePersonId = !dr.IsDBNull(dr.GetOrdinal("SurePersonId")) ? dr.GetString(dr.GetOrdinal("SurePersonId")) : string.Empty;

                        change.SurePerson = !dr.IsDBNull(dr.GetOrdinal("SurePerson")) ? dr.GetString(dr.GetOrdinal("SurePerson")) : string.Empty; ;

                        change.SureTime = !dr.IsDBNull(dr.GetOrdinal("SureTime")) ? dr.GetDateTime(dr.GetOrdinal("SureTime")) : (DateTime?)null;

                        list.Add(change);
                    }
                }
            }
            return list;


        }



        /// <summary>
        /// 根据编号获取订单变更的详细信息
        /// </summary>
        /// <param name="id">变更Id 主键编号</param>
        /// <returns></returns>
        public MTourOrderChange GetTourOrderChangById(string id)
        {
            MTourOrderChange change = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("Id,CompanyId,TourId,RouteId,RouteName,TourSaleId,TourSale,OrderId,OrderCode");
            query.Append(",ChangePerson,ChangePrice,OrderSaleId,OrderSale,Content,OperatorId");
            query.Append(",Operator,IssueTime,ChangeType,IsSure,SurePersonId,SurePerson,SureTime");
            query.Append(" FROM ");
            query.Append("tbl_TourOrderChange");
            query.Append(" Where ");
            query.AppendFormat("Id='{0}'", id);
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        change = new MTourOrderChange();

                        change.Id = dr.GetString(dr.GetOrdinal("Id"));

                        change.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));

                        change.TourId = dr.GetString(dr.GetOrdinal("TourId"));

                        change.RouteId = !dr.IsDBNull(dr.GetOrdinal("RouteId")) ? dr.GetString(dr.GetOrdinal("RouteId")) : string.Empty;

                        change.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr["RouteName"].ToString() : string.Empty;

                        change.TourSaleId = dr.GetString(dr.GetOrdinal("TourSaleId"));

                        change.TourSale = !dr.IsDBNull(dr.GetOrdinal("TourSale")) ? dr["TourSale"].ToString() : string.Empty;

                        change.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));

                        change.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;

                        change.ChangePerson = dr.GetInt32(dr.GetOrdinal("ChangePerson"));

                        change.ChangePrice = dr.GetDecimal(dr.GetOrdinal("ChangePrice"));

                        change.OrderSaleId = dr.GetString(dr.GetOrdinal("OrderSaleId"));

                        change.OrderSale = !dr.IsDBNull(dr.GetOrdinal("OrderSale")) ? dr["OrderSale"].ToString() : string.Empty;

                        change.Content = !dr.IsDBNull(dr.GetOrdinal("Content")) ? dr["Content"].ToString() : string.Empty;

                        change.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));

                        change.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr["Operator"].ToString() : string.Empty;

                        change.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        change.ChangeType = (ChangeType)dr.GetByte(dr.GetOrdinal("ChangeType"));

                        change.IsSure = dr.GetString(dr.GetOrdinal("IsSure")) == "1" ? true : false;

                        change.SurePersonId = !dr.IsDBNull(dr.GetOrdinal("SurePersonId")) ? dr.GetString(dr.GetOrdinal("SurePersonId")) : string.Empty;

                        change.SurePerson = !dr.IsDBNull(dr.GetOrdinal("SurePerson")) ? dr.GetString(dr.GetOrdinal("SurePerson")) : string.Empty; ;

                        change.SureTime = !dr.IsDBNull(dr.GetOrdinal("SureTime")) ? dr.GetDateTime(dr.GetOrdinal("SureTime")) : (DateTime?)null;
                    }
                }
            }
            return change;
        }



        /// <summary>
        /// 查询获取订单变更列表 分页
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="changeType">变更 OR 修改</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DetpIds">部门</param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId">当前登录员编号</param>
        /// <returns></returns>
        public IList<MTourOrderChange> GetTourOrderChangeList(string CompanyId
            , ChangeType? changeType
            , int pageSize
            , int pageindex
            , ref int recordCount
            , int[] DetpIds
            , bool isOnlySeft
            , string LoginUserId)
        {
            IList<MTourOrderChange> list = null;

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId='{0}' ", CompanyId);
            if (changeType.HasValue)
            {
                query.AppendFormat(" and ChangeType={0}", (int)changeType.Value);
            }

            StringBuilder field = new StringBuilder();
            field.Append("Id,CompanyId,TourId,RouteId,RouteName,TourSaleId");
            field.Append(",TourSale,OrderId,OrderCode,ChangePerson,ChangePrice");
            field.Append(",OrderSaleId,OrderSale,Content,OperatorId,Operator,IssueTime");
            field.Append(",ChangeType,IsSure,SurePersonId,SurePerson,SureTime");

            //权限控制  控制派团给计调时指定的计调员
            if (isOnlySeft)
            {
                query.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_TourOrderChange.TourId)", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    query.AppendFormat(" and exists(select 1 from tbl_TourPlaner where DeptId in ({0}) and TourId=tbl_TourOrderChange.TourId)", GetIdsByArr(DetpIds));
                }
            }



            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageindex, ref recordCount
                     , "tbl_TourOrderChange"
                     , "TourId"
                     , field.ToString()
                     , query.ToString()
                     , "IssueTime desc"))

                if (dr != null)
                {
                    list = new List<MTourOrderChange>();

                    while (dr.Read())
                    {
                        MTourOrderChange change = new MTourOrderChange();

                        change.Id = dr.GetString(dr.GetOrdinal("Id"));

                        change.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));

                        change.TourId = dr.GetString(dr.GetOrdinal("TourId"));

                        change.RouteId = !dr.IsDBNull(dr.GetOrdinal("RouteId")) ? dr.GetString(dr.GetOrdinal("RouteId")) : string.Empty;

                        change.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr["RouteName"].ToString() : string.Empty;

                        change.TourSaleId = dr.GetString(dr.GetOrdinal("TourSaleId"));

                        change.TourSale = !dr.IsDBNull(dr.GetOrdinal("TourSale")) ? dr["TourSale"].ToString() : string.Empty;

                        change.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));

                        change.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;

                        change.ChangePerson = dr.GetInt32(dr.GetOrdinal("ChangePerson"));

                        change.ChangePrice = dr.GetDecimal(dr.GetOrdinal("ChangePrice"));

                        change.OrderSaleId = dr.GetString(dr.GetOrdinal("OrderSaleId"));

                        change.OrderSale = !dr.IsDBNull(dr.GetOrdinal("OrderSale")) ? dr["OrderSale"].ToString() : string.Empty;

                        change.Content = !dr.IsDBNull(dr.GetOrdinal("Content")) ? dr["Content"].ToString() : string.Empty;

                        change.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));

                        change.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr["Operator"].ToString() : string.Empty;

                        change.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        change.ChangeType = (ChangeType)dr.GetByte(dr.GetOrdinal("ChangeType"));

                        change.IsSure = dr.GetString(dr.GetOrdinal("IsSure")) == "1" ? true : false;

                        change.SurePersonId = !dr.IsDBNull(dr.GetOrdinal("SurePersonId")) ? dr.GetString(dr.GetOrdinal("SurePersonId")) : string.Empty;

                        change.SurePerson = !dr.IsDBNull(dr.GetOrdinal("SurePerson")) ? dr.GetString(dr.GetOrdinal("SurePerson")) : string.Empty; ;

                        change.SureTime = !dr.IsDBNull(dr.GetOrdinal("SureTime")) ? dr.GetDateTime(dr.GetOrdinal("SureTime")) : (DateTime?)null;


                        list.Add(change);
                    }
                }

            return list;

        }


        #endregion






    }
}
