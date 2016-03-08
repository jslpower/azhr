using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.Model.EnumType.GovStructure;
using System.Xml;
using EyouSoft.Toolkit;
using EyouSoft.Model.EnumType.ComStructure;


namespace EyouSoft.DAL.TourStructure
{
    public class DSingleService : EyouSoft.Toolkit.DAL.DALBase, ISingleService
    {
        #region 初始化Database
        private Microsoft.Practices.EnterpriseLibrary.Data.Database _db = null;
        #endregion


        #region 构造函数
        public DSingleService()
        {
            _db = base.SystemStore;
        }
        #endregion




        #region ISingleService 成员
        //操作中——销售未派计划
        //已落实——计调配置完毕

        /// <summary>
        ///1:添加成功
        ///0:添加失败
        /// </summary>
        /// <param name="single">单项业务的实体</param>
        /// <returns></returns>
        public int AddSingleService(MSingleServiceExtend single)
        {
            string tourId = string.IsNullOrEmpty(single.TourId) ? Guid.NewGuid().ToString() : single.TourId;

            string OrderId = string.IsNullOrEmpty(single.OrderId) ? Guid.NewGuid().ToString() : single.OrderId;

            string travellerXML = string.Empty;//游客信息
            if (single.TourOrderTravellerList != null)
            {
                travellerXML = CreateTourOrderTravellerXml(single.TourOrderTravellerList, OrderId, tourId);//游客信息
            }

            string teamPriceXMl = string.Empty;//客人要求（分项报价）
            if (single.TourTeamPriceList != null)
            {
                teamPriceXMl = CreateTourTeamPriceXml(tourId, single.TourTeamPriceList);
            }

            string planXML = string.Empty;//供应商安排
            if (single.PlanBaseInfoList != null)
            {
                planXML = CreatePlanBaseInfoXml(tourId, single.CompanyId, single.OperatorDeptId, single.PlanBaseInfoList);
            }
            string planerXML = string.Empty;//计调员信息

            if (single.TourPlanersList != null)
            {
                planerXML = CreateTourPlanerXML(tourId, single.TourPlanersList);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Add_SingleService");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, single.TourCode);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, single.WeiTuoRiQi);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, single.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, single.CompanyId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, single.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, single.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.String, single.ContactDepartId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, single.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, single.ContactTel);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, single.Adults);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, single.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, single.SellerName);
            _db.AddInParameter(cmd, "DeptId", DbType.String, single.DeptId);//销售员部门编号
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, single.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, single.Operator);
            _db.AddInParameter(cmd, "TourIncome", DbType.Currency, single.TourIncome);
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, single.TravellerFile);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)single.TourStatus);//计划状态
            _db.AddInParameter(cmd, "MTourOrderTraveller", DbType.String, travellerXML);
            _db.AddInParameter(cmd, "MTourTeamPrice", DbType.String, teamPriceXMl);
            _db.AddInParameter(cmd, "MPlanBaseInfo", DbType.String, planXML);
            _db.AddInParameter(cmd, "MTourPlaner", DbType.String, planerXML);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, _db);

            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 4);

        }

        /// <summary>
        ///删除单项业务
        ///1：删除成功
        ///0: 删除失败
        ///-99:不允许删除
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public int DeleteSingleServiceByTourId(params string[] tourId)
        {
            string tourIds = string.Empty;
            foreach (string id in tourId)
            {
                tourIds += id + ",";
            }
            tourIds = tourIds.Substring(0, tourIds.Length - 1);
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Delete_SingleService");
            _db.AddInParameter(cmd, "TourIds", DbType.String, tourIds);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 1);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 0);
        }

        /// <summary>
        ///1:更新成功 
        ///2:更新失败
        /// </summary>
        /// <param name="single">单项业务的拓展实体</param>
        /// <returns></returns>
        public int UpdateSingleService(MSingleServiceExtend single)
        {
            string travellerXML = string.Empty;//订单游客信息
            if (single.TourOrderTravellerList != null)
            {
                travellerXML = CreateTourOrderTravellerXml(single.TourOrderTravellerList, single.OrderId, single.TourId);
            }

            string teamPriceXMl = string.Empty;//游客要求(分项报价)
            if (single.TourTeamPriceList != null)
            {
                teamPriceXMl = CreateTourTeamPriceXml(single.TourId, single.TourTeamPriceList);
            }
            string planXML = string.Empty;//供应商安排
            if (single.PlanBaseInfoList != null)
            {
                planXML = CreatePlanBaseInfoXml(single.TourId, single.CompanyId, single.OperatorDeptId, single.PlanBaseInfoList);
            }
            string planerXML = string.Empty;//计调员信息
            if (single.TourPlanersList != null)
            {
                planerXML = CreateTourPlanerXML(single.TourId, single.TourPlanersList);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_SingleService");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, single.TourId);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, single.WeiTuoRiQi);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, single.OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, single.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, single.CompanyId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, single.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, single.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, single.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, single.ContactTel);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.String, single.ContactDepartId);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, single.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, single.SellerName);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, single.DeptId);//销售员部门编号
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, single.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, single.Operator);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, single.OperatorDeptId);//操作员部门编号

            _db.AddInParameter(cmd, "Adults", DbType.Int32, single.Adults);
            _db.AddInParameter(cmd, "TourIncome", DbType.Currency, single.TourIncome);
            _db.AddInParameter(cmd, "TourPay", DbType.Currency, single.TourPay);//合计支出
            _db.AddInParameter(cmd, "TourProfit", DbType.Currency, single.TourProfit);
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, single.TravellerFile);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)single.TourStatus);//计划状态

            _db.AddInParameter(cmd, "MTourOrderTraveller", DbType.String, travellerXML);
            _db.AddInParameter(cmd, "MTourTeamPrice", DbType.String, teamPriceXMl);
            _db.AddInParameter(cmd, "MPlanBaseInfo", DbType.String, planXML);
            _db.AddInParameter(cmd, "MTourPlaner", DbType.String, planerXML);


            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);

            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 4);
        }

        /// <summary>
        /// 根据计划编号获取单项业务的拓展实体
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public MSingleServiceExtend GetSingleServiceExtendByTourId(string tourId)
        {
            MSingleServiceExtend single = null;
            StringBuilder query = new StringBuilder();
            query.Append(" select TourId,CompanyId,SellerName,SellerId,SellerDeptId,TourCode,");
            query.Append("TourStatus,OperatorId,Operator,TourType,IsDelete,");

            query.Append(" (SELECT TourId,Unit,Quote,ServiceStandard,ServiceType,ServiceName,ServiceId,Remark,istax,DanJia,ShuLiang");
            query.Append(" from ");
            query.Append("tbl_TourTeamPrice");
            query.Append(" where ");
            query.Append(" tbl_TourTeamPrice.TourId=tbl_Tour.TourId ");
            query.Append(" for xml raw,root('Root'))");
            query.Append(" as TourTeamPrice,");

            query.Append("(select OrderId,OrderCode,BuyCompanyName,BuyCompanyId,ContactName,ContactTel,Adults,");

            query.Append("(SELECT TravellerId,OrderId,CnName,EnName,CardId,VisitorType,CardType");
            query.Append(",CardNumber,CardValidDate,VisaStatus,IsCardTransact,Gender,Contact");
            query.Append(",LNotice,RNotice,Remark,Status,RAmount,RAmountRemark,RTime,RRemark,LiCheng,SalerIncome,CONVERT(NVARCHAR(10),QianFaDate,120) QianFaDate,CONVERT(NVARCHAR(10),Birthday,120) Birthday,QianFaDi");
            query.Append(" FROM ");
            query.Append(" tbl_TourOrderTraveller ");
            query.Append(" where tbl_TourOrder.OrderId=tbl_TourOrderTraveller.OrderId order by tbl_TourOrderTraveller.id");
            query.Append(" for xml path,elements)");
            query.Append("as TourOrderTraveller");
            query.Append(" ,ContractId,ContractCode,TravellerFile ,SumPrice");

            query.Append(" from ");
            query.Append("tbl_TourOrder");
            query.Append(" where tbl_TourOrder.TourId=tbl_Tour.TourId");
            query.Append(" for xml path,elements,root('Root'))");
            query.Append("as TourOrder,");


            query.Append("(SELECT PlanId,CompanyId,TourId,Type,SourceId,SourceName,ContactName,ContactPhone");
            query.Append(",ContactFax,Num,ReceiveJourney,PlanCost,PaymentType,Status,GuideNotes");
            query.Append(",Remarks,CostId,CostName,CostStatus,CostTime,Confirmation,CostRemarks,OperatorDeptId");
            query.Append(",OperatorId,Operator,IssueTime,Prepaid,AddStatus,IsDelete,ServiceStandard");
            query.Append(",StartDate,StartTime,EndDate,EndTime");
            query.Append(" FROM ");
            query.Append("tbl_Plan");
            query.Append(" where ");
            query.Append("tbl_Tour.TourId=tbl_Plan.TourId and tbl_Plan.IsDelete=0 ");
            query.Append("for xml raw,root('Root'))");
            query.Append("as TourPlan,");


            query.Append("(SELECT TourId,PlanerId,Planer,SellerDeptId ");
            query.Append(" FROM ");
            query.Append(" tbl_TourPlaner ");
            query.Append(" where ");
            query.Append("tbl_TourPlaner.TourId=tbl_Tour.TourId ");
            query.Append("for xml raw,root('Root') )");
            query.Append("as TourPlaner");
            query.Append(" ,LDate ");
            query.Append(" from tbl_Tour");

            query.Append(" Where ");
            query.AppendFormat(" TourType='{0}' ", 3);
            query.Append(" and ");
            query.AppendFormat(" TourId='{0}' ", tourId);

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    single = new MSingleServiceExtend();
                    if (dr.Read())
                    {
                        single.TourId = dr["TourId"].ToString();
                        single.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        single.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                        single.SellerId = dr.GetString(dr.GetOrdinal("SellerId"));
                        single.DeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));
                        single.TourStatus = (TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                        single.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                        single.Operator = dr.GetString(dr.GetOrdinal("Operator"));
                        single.TourCode = dr["TourCode"].ToString();

                        string TourOrder = !dr.IsDBNull(dr.GetOrdinal("TourOrder")) ? dr.GetString(dr.GetOrdinal("TourOrder")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourOrder))
                        {
                            single.TourOrderTravellerList = new List<MTourOrderTraveller>();
                            MSingleService order = new MSingleService();
                            single.TourOrderTravellerList = GetOrderAndTravellerByXml(TourOrder, ref order);
                            single.OrderId = order.OrderId;
                            single.OrderCode = order.OrderCode;
                            single.BuyCompanyId = order.BuyCompanyId;
                            single.BuyCompanyName = order.BuyCompanyName;
                            single.ContactName = order.ContactName;
                            single.ContactTel = order.ContactTel;
                            single.ContactDepartId = order.ContactDepartId;
                            single.TravellerFile = order.TravellerFile;
                            single.Adults = order.Adults;
                            single.HeTongId = order.HeTongId;
                            single.HeTongCode = order.HeTongCode;
                            single.TourIncome = order.TourIncome;
                            single.TravellerFile = order.TravellerFile;
                        }

                        //计调员信息
                        string TourPlaner = !dr.IsDBNull(dr.GetOrdinal("TourPlaner")) ? dr.GetString(dr.GetOrdinal("TourPlaner")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourPlaner))
                        {
                            single.TourPlanersList = new List<MTourPlaner>();
                            single.TourPlanersList = GetTourPlanerByXML(TourPlaner);
                        }

                        //客人要求(分项报价)
                        string TourTeamPrice = !dr.IsDBNull(dr.GetOrdinal("TourTeamPrice")) ? dr.GetString(dr.GetOrdinal("TourTeamPrice")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourTeamPrice))
                        {
                            single.TourTeamPriceList = new List<MTourTeamPrice>();
                            single.TourTeamPriceList = GetTourTeamPriceByXML(TourTeamPrice);
                        }
                        //供应商安排(计调中心)
                        string TourPlan = !dr.IsDBNull(dr.GetOrdinal("TourPlan")) ? dr.GetString(dr.GetOrdinal("TourPlan")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourPlan))
                        {
                            single.PlanBaseInfoList = new List<EyouSoft.Model.PlanStructure.MPlanBaseInfo>();
                            single.PlanBaseInfoList = GetPlanBaseInfoByXML(TourPlan);
                        }

                        single.WeiTuoRiQi = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    }

                }
            }

            return single;
        }



        /// <summary>
        /// 查询获取单项业务的列表
        /// </summary>
        /// <param name="search">查询的实体类</param>
        /// <param name="pagesize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="loginId">当前登录人编号</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="isOnlySeft">是否查看全部</param>
        /// <returns></returns>
        public IList<MSingleService> GetSingleServiceList(MSeachSingleService search, int pagesize, int pageindex, ref int recordCount, string loginId, int[] deptIds, bool isOnlySeft)
        {
            IList<EyouSoft.Model.TourStructure.MSingleService> list = null;

            string fields = "TourId,TourStatus,OperatorId,OrderId,OrderCode,CompanyId,BuyCompanyName,SellerName,SellerDeptId,ContactName,ContactTel,Adults,Operator,IssueTime,PlanProject,TourPlaner,TourType,IsDelete,LDate";

            StringBuilder query = new StringBuilder();
            //TourType=6（单项业务）
            query.AppendFormat("TourType='{0}' and IsDelete='{1}' ", (int)TourType.单项业务, 0);
            //团队状态搜索条件未添加

            if (!string.IsNullOrEmpty(search.CompanyId))
            {
                query.AppendFormat(" and CompanyId='{0}' ", search.CompanyId);
            }


            if (!string.IsNullOrEmpty(search.OrderCode))
            {
                query.AppendFormat(" and OrderCode like '%{0}%' ", search.OrderCode);
            }

            if (search.BeginLDate.HasValue)
            {
                query.AppendFormat(" and IssueTime>'{0}' ", search.BeginLDate.Value.AddDays(-1));
            }

            if (search.EndLDate.HasValue)
            {
                query.AppendFormat(" and  IssueTime<'{0}' ", search.EndLDate.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(search.BuyCompanyName))
            {
                query.AppendFormat(" and BuyCompanyName like  '%{0}%' ", search.BuyCompanyName);
            }

            if (!string.IsNullOrEmpty(search.BuyCompanyId))
            {

                query.AppendFormat(" and BuyCompanyId='{0}' ", search.BuyCompanyId);
            }

            if (!string.IsNullOrEmpty(search.Operator))
            {
                query.AppendFormat(" and Operator='{0}' ", search.Operator);
            }

            if (!string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" and OperatorId='{0}' ", search.OperatorId);
            }

            if (search.TourStatus.HasValue)
            {
                query.AppendFormat(" and TourStatus='{0}' ", (int)search.TourStatus.Value);
            }


            if (isOnlySeft)
            {
                query.AppendFormat(" and SellerId='{0}' ", loginId);
            }
            else
            {
                if (deptIds != null)
                {
                    query.AppendFormat(" and SellerDeptId in ({0})", GetIdsByArr(deptIds));
                }
            }

            if (search.SWeiTuoRiQi.HasValue)
            {
                query.AppendFormat(" and LDate>'{0}' ", search.SWeiTuoRiQi.Value.AddDays(-1));
            }
            if (search.EWeiTuoRiQi.HasValue)
            {
                query.AppendFormat(" and LDate<'{0}' ", search.EWeiTuoRiQi.Value.AddDays(1));
            }


            using (IDataReader dr = DbHelper.ExecuteReader(_db
                , pagesize
                , pageindex
                , ref recordCount
                , "view_SingleService"
                , "TourId"
                , fields.ToString()
                , query.ToString()
                , "IssueTime desc"))
            {
                if (dr != null)
                {
                    list = new List<MSingleService>();
                    while (dr.Read())
                    {
                        MSingleService service = new MSingleService();

                        service.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        service.TourStatus = (TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                        service.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                        service.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        service.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        service.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        service.BuyCompanyName = dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) == false ? dr["BuyCompanyName"].ToString() : string.Empty;
                        service.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) == false ? dr["ContactName"].ToString() : string.Empty;
                        service.ContactTel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) == false ? dr["ContactTel"].ToString() : string.Empty;
                        service.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        service.Operator = dr.IsDBNull(dr.GetOrdinal("Operator")) == false ? dr["Operator"].ToString() : string.Empty;
                        service.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;

                        //计调项
                        string plan = dr.IsDBNull(dr.GetOrdinal("PlanProject")) == false ? dr.GetString(dr.GetOrdinal("PlanProject")) : string.Empty;
                        if (!string.IsNullOrEmpty(plan))
                        {
                            service.PlanProjectType = GetPlanProjectByXML(plan);
                        }
                        string planers = dr.IsDBNull(dr.GetOrdinal("TourPlaner")) == false ? dr.GetString(dr.GetOrdinal("TourPlaner")) : string.Empty;
                        //计调员
                        if (!string.IsNullOrEmpty(planers))
                        {
                            service.Planers = GetPlanerByXML(planers);
                        }

                        service.WeiTuoRiQi = dr.GetDateTime(dr.GetOrdinal("LDate"));

                        list.Add(service);
                    }
                }
            }

            return list;
        }

        #endregion


        /// <summary>
        /// 创建团队分项报价XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourTeamPriceXml(string TourId, IList<EyouSoft.Model.TourStructure.MTourTeamPrice> list)
        {
            //<Root><TourTeamPrice TourId="计划编号" Unit="单位" Quote="单项报价" ServiceStandard="服务标准" ServiceType="服务类型"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.Append("<Item ");
                xmlDoc.AppendFormat("TourId=\"{0}\" ", TourId);
                xmlDoc.AppendFormat("Unit=\"{0}\" ", (int)item.Unit);
                xmlDoc.AppendFormat("Quote=\"{0}\" ", item.Quote);
                xmlDoc.AppendFormat("ServiceStandard=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.ServiceStandard));
                xmlDoc.AppendFormat("ServiceType=\"{0}\" ", (int)item.ServiceType);
                xmlDoc.AppendFormat("ServiceName=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.ServiceName));
                xmlDoc.AppendFormat("ServiceId=\"{0}\" ", item.ServiceId);
                xmlDoc.AppendFormat("Remark=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Remark));
                xmlDoc.AppendFormat("IsTax=\"{0}\" ", item.IsTax ? "1" : "0");
                xmlDoc.AppendFormat("DanJia=\"{0}\" ", item.DanJia);
                xmlDoc.AppendFormat("ShuLiang=\"{0}\" ", item.ShuLiang);
                xmlDoc.AppendFormat(" />");
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 创建游客集合的XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourOrderTravellerXml(IList<MTourOrderTraveller> list, string orderId, string tourid)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            foreach (MTourOrderTraveller traveller in list)
            {
                query.Append("<Item ");
                query.AppendFormat("TravellerId=\"{0}\" ", string.IsNullOrEmpty(traveller.TravellerId) ? Guid.NewGuid().ToString() : traveller.TravellerId);
                query.AppendFormat("OrderId=\"{0}\" ", orderId);
                query.AppendFormat("TourID=\"{0}\" ", tourid);
                query.AppendFormat("CnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.CnName));
                query.AppendFormat("EnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.EnName));
                query.AppendFormat("CardId=\"{0}\" ", traveller.CardId);
                query.AppendFormat("VisitorType=\"{0}\" ", (int?)traveller.VisitorType);
                query.AppendFormat("CardType=\"{0}\" ", (int?)traveller.CardType);
                query.AppendFormat("CardNumber=\"{0}\" ", traveller.CardNumber);
                query.AppendFormat("VisaStatus=\"{0}\" ", (int?)traveller.VisaStatus);
                query.AppendFormat("IsCardTransact=\"{0}\" ", traveller.IsCardTransact == true ? 1 : 0);
                query.AppendFormat("Gender=\"{0}\" ", (int?)traveller.Gender);
                query.AppendFormat("Contact=\"{0}\" ", traveller.Contact);
                query.AppendFormat("LNotice=\"{0}\" ", traveller.LNotice == true ? 1 : 0);
                query.AppendFormat("RNotice=\"{0}\" ", traveller.RNotice == true ? 1 : 0);
                query.AppendFormat("Remark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.Remark));
                query.AppendFormat("Status=\"{0}\" ", (int)traveller.TravellerStatus);
                query.AppendFormat("RAmount=\"{0}\" ", traveller.RAmount);
                query.AppendFormat("RAmountRemark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.RAmountRemark));
                query.AppendFormat("RTime=\"{0}\" ", traveller.RTime);
                query.AppendFormat("RRemark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.Remark));
                query.AppendFormat("IsInsurance=\"{0}\" ", traveller.IsInsurance == true ? 1 : 0);
                query.AppendFormat("LiCheng=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(traveller.LiCheng));
                query.AppendFormat("QianFaDate=\"{0}\" ", traveller.QianFaDate);
                query.AppendFormat("CardValidDate=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(traveller.CardValidDate));
                query.AppendFormat("Birthday=\"{0}\" ", traveller.Birthday);
                query.AppendFormat("QianFaDi=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(traveller.QianFaDi));
                query.Append("/>");

            }
            query.Append("</Root>");

            return query.ToString();
        }


        /// <summary>
        /// 创建供应商安排的XML
        /// </summary>
        /// <param name="tourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreatePlanBaseInfoXml(string tourId, string companyId, int DeptId, IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> list)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");

            foreach (EyouSoft.Model.PlanStructure.MPlanBaseInfo plan in list)
            {
                //2012-8-1 修改：
                plan.StartDate = null;
                plan.EndDate = null;

                query.AppendFormat("<Item PlanId=\"{0}\" ", string.IsNullOrEmpty(plan.PlanId) ? Guid.NewGuid().ToString() : plan.PlanId);
                query.AppendFormat("CompanyId=\"{0}\" ", companyId);
                query.AppendFormat("Type=\"{0}\" ", (int)plan.Type);
                query.AppendFormat("TourId=\"{0}\" ", tourId);
                query.AppendFormat("SourceId=\"{0}\" ", plan.SourceId);
                query.AppendFormat("SourceName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.SourceName));
                query.AppendFormat("ContactName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.ContactName));
                query.AppendFormat("ContactPhone =\"{0}\" ", plan.ContactPhone);
                query.AppendFormat("ContactFax=\"{0}\" ", plan.ContactFax);
                query.AppendFormat("Num=\"{0}\" ", plan.Num);
                query.AppendFormat("ReceiveJourney=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.ReceiveJourney));
                query.AppendFormat("CostDetail=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.CostDetail));
                query.AppendFormat("PlanCost=\"{0}\" ", plan.PlanCost);
                query.AppendFormat("PaymentType =\"{0}\" ", (int)plan.PaymentType);
                query.AppendFormat("Status=\"{0}\" ", (int)plan.Status);
                query.AppendFormat("GuideNotes=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.GuideNotes));
                query.AppendFormat("Remarks=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.CostDetail));
                query.AppendFormat("SueId=\"{0}\" ", plan.SueId);
                query.AppendFormat("CostId=\"{0}\" ", plan.CostId);
                query.AppendFormat("CostName=\"{0}\" ", plan.CostName);
                query.AppendFormat("CostStatus=\"{0}\" ", plan.CostStatus);
                query.AppendFormat("CostTime=\"{0}\" ", plan.CostTime);
                query.AppendFormat("Confirmation=\"{0}\" ", plan.Confirmation);
                query.AppendFormat("CostRemarks=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.CostRemarks));
                query.AppendFormat("OperatorDeptId=\"{0}\" ", DeptId);
                query.AppendFormat("OperatorId=\"{0}\" ", plan.OperatorId);
                query.AppendFormat("OperatorName=\"{0}\" ", plan.OperatorName);
                query.AppendFormat("IssueTime=\"{0}\" ", DateTime.Now);
                query.AppendFormat("Prepaid=\"{0}\" ", plan.Prepaid);
                query.AppendFormat("IsRebate=\"{0}\" ", plan.IsRebate == true ? 1 : 0);
                query.AppendFormat("AddStatus=\"{0}\" ", (int)plan.AddStatus);
                query.AppendFormat("ServiceStandard=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.ServiceStandard));
                query.AppendFormat("CustomerInfo=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.CustomerInfo));
                query.AppendFormat("StartDate=\"{0}\" ", plan.StartDate);
                query.AppendFormat("StartTime=\"{0}\" ", plan.StartTime);
                query.AppendFormat("EndDate=\"{0}\" ", plan.EndDate);
                query.AppendFormat("EndTime=\"{0}\" ", plan.EndTime);
                query.Append(" />");
            }

            query.Append("</Root>");
            return query.ToString();

        }

        /// <summary>
        /// 创建指定计调的XML
        /// </summary>
        /// <param name="tourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourPlanerXML(string tourId, IList<MTourPlaner> list)
        {
            StringBuilder query = new StringBuilder();

            query.Append("<Root>");
            foreach (MTourPlaner planer in list)
            {
                query.AppendFormat("<Item TourId=\"{0}\" PlanerId=\"{1}\" Planer=\"{2}\" DeptId=\"{3}\" />", tourId, planer.PlanerId, planer.Planer, planer.DeptId);
            }

            query.Append("</Root>");
            return query.ToString();
        }


        /// <summary>
        /// 根据xml获取分销报价的集合（客人要求）
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourTeamPrice> GetTourTeamPriceByXML(string xml)
        {
            IList<EyouSoft.Model.TourStructure.MTourTeamPrice> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root.HasChildNodes)
            {
                list = new List<MTourTeamPrice>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    MTourTeamPrice team = new MTourTeamPrice();
                    team.TourId = node.Attributes["TourId"].Value;
                    team.Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)Utils.GetInt(node.Attributes["Unit"].Value);
                    team.Quote = Utils.GetDecimal(node.Attributes["Quote"].Value);
                    team.ServiceStandard = node.Attributes["ServiceStandard"] != null ? node.Attributes["ServiceStandard"].Value : string.Empty;
                    team.ServiceType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(node.Attributes["ServiceType"].Value);
                    team.ServiceName = node.Attributes["ServiceName"] != null ? node.Attributes["ServiceName"].Value : string.Empty;
                    team.ServiceId = node.Attributes["ServiceId"] != null ? node.Attributes["ServiceId"].Value : string.Empty;
                    team.Remark = node.Attributes["Remark"] != null ? node.Attributes["Remark"].Value : string.Empty;
                    team.IsTax = node.Attributes["istax"].Value.Equals("1");
                    team.DanJia = Utils.GetDecimal(node.Attributes["DanJia"].Value);
                    team.ShuLiang = float.Parse(node.Attributes["ShuLiang"].Value);
                    list.Add(team);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据xml获取供应商安排（供应商安排）
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> GetPlanBaseInfoByXML(string xml)
        {
            IList<EyouSoft.Model.PlanStructure.MPlanBaseInfo> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root.HasChildNodes)
            {
                list = new List<EyouSoft.Model.PlanStructure.MPlanBaseInfo>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    EyouSoft.Model.PlanStructure.MPlanBaseInfo plan = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
                    plan.PlanId = node.Attributes["PlanId"].Value;
                    plan.CompanyId = node.Attributes["CompanyId"].Value;
                    plan.Type = (PlanProject)Utils.GetInt(node.Attributes["Type"].Value);
                    plan.TourId = node.Attributes["TourId"].Value;
                    plan.SourceId = node.Attributes["SourceId"] != null ? node.Attributes["SourceId"].Value : string.Empty; ;
                    plan.SourceName = node.Attributes["SourceName"] != null ? node.Attributes["SourceName"].Value : string.Empty;
                    plan.ContactName = node.Attributes["ContactName"] != null ? node.Attributes["ContactName"].Value : string.Empty;
                    plan.ContactPhone = node.Attributes["ContactPhone"] != null ? node.Attributes["ContactPhone"].Value : string.Empty;
                    plan.ContactFax = node.Attributes["ContactFax"] != null ? node.Attributes["ContactFax"].Value : string.Empty;
                    plan.Num = node.Attributes["Num"] != null ? Utils.GetInt(node.Attributes["Num"].Value) : 0;
                    plan.ReceiveJourney = node.Attributes["ReceiveJourney"] != null ? node.Attributes["ReceiveJourney"].Value : string.Empty;
                    plan.CostDetail = node.Attributes["CostDetail"] != null ? node.Attributes["CostDetail"].Value : string.Empty;
                    plan.PlanCost = Utils.GetDecimal(node.Attributes["PlanCost"].Value);

                    plan.PaymentType = (Payment)Utils.GetInt(node.Attributes["PaymentType"].Value);
                    plan.Status = (PlanState)Utils.GetInt(node.Attributes["Status"].Value);
                    plan.GuideNotes = node.Attributes["GuideNotes"] != null ? node.Attributes["GuideNotes"].Value : string.Empty;
                    plan.Remarks = node.Attributes["Remarks"] != null ? node.Attributes["Remarks"].Value : string.Empty;

                    plan.SueId = node.Attributes["SueId"] != null ? node.Attributes["SueId"].Value : string.Empty;
                    plan.CostId = node.Attributes["CostId"] != null ? node.Attributes["CostId"].Value : string.Empty;
                    plan.CostName = node.Attributes["CostName"] != null ? node.Attributes["CostName"].Value : string.Empty;

                    plan.CostTime = node.Attributes["CostTime"] != null ? Utils.GetDateTime(node.Attributes["CostTime"].Value) : (DateTime?)null;
                    plan.Confirmation = Utils.GetDecimal(node.Attributes["Confirmation"].Value);
                    plan.CostRemarks = node.Attributes["CostRemarks"] != null ? node.Attributes["CostRemarks"].Value : string.Empty;
                    plan.DeptId = Utils.GetInt(node.Attributes["OperatorDeptId"].Value);
                    plan.OperatorId = node.Attributes["OperatorId"].Value;
                    plan.OperatorName = node.Attributes["Operator"].Value;
                    if (node.Attributes["IssueTime"] != null)
                    {
                        plan.IssueTime = Utils.GetDateTime(node.Attributes["IssueTime"].Value);
                    }
                    plan.Prepaid = Utils.GetDecimal(node.Attributes["Prepaid"].Value);
                    //  plan.IsRebate = Utils.GetInt(node.Attributes["IsRebate"].Value) == 1 ? true : false;

                    plan.AddStatus = (PlanAddStatus)Utils.GetInt(node.Attributes["AddStatus"].Value);

                    //plan.ServiceStandard = node.Attributes["ServiceStandard"] != null ? node.Attributes["ServiceStandard"].Value : string.Empty;
                    //plan.CustomerInfo = node.Attributes["CustomerInfo"] != null ? node.Attributes["CustomerInfo"].Value : string.Empty;
                    //plan.StartDate = node.Attributes["StartDate"] != null ? Utils.GetDateTime(node.Attributes["StartDate"].Value) : (DateTime?)null;
                    //plan.StartTime = node.Attributes["StartTime"] != null ? node.Attributes["StartTime"].Value : string.Empty;
                    //plan.EndDate = node.Attributes["EndDate"] != null ? Utils.GetDateTime(node.Attributes["EndDate"].Value) : (DateTime?)null;

                    //plan.EndTime = node.Attributes["EndTime"] != null ? node.Attributes["EndTime"].Value : string.Empty;
                    list.Add(plan);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据xml获取计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourPlaner> GetTourPlanerByXML(string xml)
        {
            IList<MTourPlaner> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root.HasChildNodes)
            {
                list = new List<MTourPlaner>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    MTourPlaner planer = new MTourPlaner();
                    XmlNode node = root.ChildNodes[i];
                    planer.TourId = node.Attributes["TourId"].Value;
                    planer.PlanerId = node.Attributes["PlanerId"].Value;
                    planer.Planer = node.Attributes["Planer"] != null ? node.Attributes["Planer"].Value : string.Empty;
                    planer.DeptId = Utils.GetInt(node.Attributes["SellerDeptId"].Value);
                    list.Add(planer);
                }
            }

            return list;
        }



        /// <summary>
        /// 根据订单游客的xml获取订单信息、订单游客信息（单项业务订单信息、游客信息）
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="single"></param>
        /// <returns></returns>
        private IList<MTourOrderTraveller> GetOrderAndTravellerByXml(string xml, ref MSingleService single)
        {
            IList<MTourOrderTraveller> list = null;
            xml = xml.Replace("&lt;", "<").Replace("&gt;", ">");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root.HasChildNodes)
            {
                list = new List<MTourOrderTraveller>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    single.OrderId = node["OrderId"].InnerText;
                    single.OrderCode = node["OrderCode"] != null ? node["OrderCode"].InnerText : string.Empty;

                    single.BuyCompanyName = node["BuyCompanyName"] != null ? node["BuyCompanyName"].InnerText : string.Empty;
                    single.BuyCompanyId = node["BuyCompanyId"].InnerText;
                    single.ContactName = node["ContactName"] != null ? node["ContactName"].InnerText : string.Empty;
                    single.ContactTel = node["ContactTel"] != null ? node["ContactTel"].InnerText : string.Empty;
                    single.ContactDepartId = node["ContactDepartId"] != null ? node["ContactDepartId"].InnerText : string.Empty;
                    single.Adults = node["Adults"] != null ? Utils.GetInt(node["Adults"].InnerText) : 0;
                    single.TravellerFile = node["TravellerFile"] != null ? node["TravellerFile"].InnerText : string.Empty;
                    single.TourIncome = node["SumPrice"] != null ? Utils.GetDecimal(node["SumPrice"].InnerText, 0) : 0;
                    if (node["TourOrderTraveller"] != null)
                    {
                        if (node["TourOrderTraveller"].HasChildNodes)
                        {
                            for (int j = 0; j < node["TourOrderTraveller"].ChildNodes.Count; j++)
                            {
                                XmlNode child = node["TourOrderTraveller"].ChildNodes[j];
                                MTourOrderTraveller traveller = new MTourOrderTraveller();
                                traveller.TravellerId = child["TravellerId"].InnerText;
                                traveller.OrderId = child["OrderId"].InnerText;
                                traveller.CnName = child["CnName"] != null ? child["CnName"].InnerText : string.Empty;
                                traveller.VisitorType = child["VisitorType"] != null ? (VisitorType?)Utils.GetInt(child["VisitorType"].InnerText) : null;
                                traveller.CardType = child["CardType"] != null ? (CardType?)Utils.GetInt(child["CardType"].InnerText) : null;
                                traveller.CardNumber = child["CardNumber"] != null ? child["CardNumber"].InnerText : string.Empty;
                                traveller.Gender = child["Gender"] != null ? (Gender?)Utils.GetInt(child["Gender"].InnerText) : null;
                                traveller.Contact = child["Contact"] != null ? child["Contact"].InnerText : string.Empty;
                                traveller.Remark = child["Remark"] != null ? child["Remark"].InnerText : string.Empty;
                                traveller.LiCheng = child["LiCheng"] != null ? child["LiCheng"].InnerText : string.Empty;
                                traveller.CardValidDate = child["CardValidDate"] != null ? child["CardValidDate"].InnerText : string.Empty;
                                traveller.Birthday = child["Birthday"] != null ? Utils.GetDateTimeNullable(child["Birthday"].InnerText) : null;
                                traveller.QianFaDate = child["QianFaDate"] != null ? Utils.GetDateTimeNullable(child["QianFaDate"].InnerText) : null;
                                traveller.QianFaDi = child["QianFaDi"] != null ? child["QianFaDi"].InnerText : string.Empty;
                                list.Add(traveller);
                            }
                        }
                    }

                    single.HeTongId = node["ContractId"] != null ? node["ContractId"].InnerText : string.Empty;
                    single.HeTongCode = node["ContractCode"] != null ? node["ContractCode"].InnerText : string.Empty;
                }
            }
            return list;
        }


        /// <summary>
        /// 根据视图查询的xml列获取客服服务类别
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string GetPlanProjectByXML(string xml)
        {
            string planProject = string.Empty;

            string[] type = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            type = new string[root.ChildNodes.Count];

            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlNode child = root.ChildNodes[i];
                type[i] = child.Attributes["ServiceType"].Value;
            }

            foreach (string item in type)
            {

                planProject += Enum.Parse(typeof(ContainProjectType), item) + ",";
            }

            planProject = planProject.Substring(0, planProject.Length - 1);



            return planProject;
        }

        /// <summary>
        /// 根据查询视图的xml获取计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string GetPlanerByXML(string xml)
        {
            string planers = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");

            if (root.ChildNodes.Count == 1)
            {
                planers = root.ChildNodes[0].Attributes["Planer"].Value;
            }
            else
            {

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode child = root.ChildNodes[i];
                    planers += child.Attributes["Planer"].Value + ",";

                }

                planers = planers.Substring(0, planers.Length - 1);
            }


            return planers;
        }

    }
}




