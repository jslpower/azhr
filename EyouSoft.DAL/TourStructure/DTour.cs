using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.EnumType.TourStructure;


namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 描述：计划数据访问层
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public class DTour : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.TourStructure.ITour
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DTour()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        //获取计划
        //private const string SQL_SELECT_GetTourInfo = "select *,(select * from tbl_TourPlanItem where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanItem,(select GuideUserId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and type=12 and IsDelete='0' for xml raw,root) as GuideList,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList,(select * from tbl_ComAttach where ItemType=18 and ItemId=@TourId for xml raw,root) as VisaFile,(select [Key] from tbl_ComTourKey where KeyId=tbl_Tour.KeyId) as KeyName from tbl_Tour where TourId=@TourId";
        // private const string SQL_SELECT_GetTourInfo = "select *,(select * from tbl_TourPlanItem where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanItem,(select GuideUserId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and type=12 and IsDelete='0' for xml raw,root) as GuideList,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList,(select * from tbl_ComAttach where ItemType=18 and ItemId=@TourId for xml raw,root) as VisaFile,(select * from tbl_TourCarLocation where TourId=tbl_Tour.TourId and IsDelete='0' for xml raw,root) as CarLocationList,(select * from tbl_TourCarType where TourId=tbl_Tour.TourId and IsDelete='0' for xml raw,root) as CarTypeList from tbl_Tour where TourId=@TourId";
        //private const string SQL_SELECT_GetTourInfo = "select *,(select * from tbl_TourPlanItem where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanItem,(select GuideUserId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and type=12 and IsDelete='0' for xml raw,root) as GuideList,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList,(select * from tbl_ComAttach where ItemType=18 and ItemId=@TourId for xml raw,root) as VisaFile from tbl_Tour where TourId=@TourId";

        private const string SQL_SELECT_GetTourInfo = "select *,(select * from tbl_TourPlanItem where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanItem,(select GuideUserId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and type=12 and IsDelete='0' for xml raw,root) as GuideList,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList,(select * from tbl_ComAttach where ItemType=18 and ItemId=@TourId for xml raw,root) as VisaFile from tbl_Tour where TourId=@TourId";
        //获取计划基础信息
        private const string SQL_SELECT_GetTourBasicInfo = "select *,(select ContactMobile from tbl_ComUser where UserId=tbl_Tour.OperatorId) as ContactMobile,(select SourceName,ContactPhone from tbl_Plan where TourId=tbl_Tour.TourId and type=4 and IsDelete='0' for xml raw,root) as GuideList,(select *,(select ContactMobile from tbl_ComUser where UserId=tbl_TourPlaner.PlanerId) as ContactMobile from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList from tbl_Tour where TourId=@TourId";
        //获得派团计划订单信息
        private const string SQL_SELECT_GetTourOrder = "select Adults,Childs,AdultPrice,ChildPrice,0 OtherCost,SumPrice,BuyCompanyName,BuyCompanyId,ContactName,ContactTel,SaleAddCost,SaleReduceCost,SaleAddCostRemark,SaleReduceCostRemark,GuideIncome,SalerIncome,OrderRemark,BuyCountryId,BuyProvincesId,ContactId ContactDepartId,(select *,null as InsuranceList from tbl_TourOrderTraveller where OrderId=tbl_TourOrder.OrderId for xml raw,root) as TravellerList,ContractCode,ContractId from tbl_TourOrder where TourId=@TourId";
        //获取计划服务
        private const string SQL_SELECT_GetTourService = "if exists(select 1 from tbl_TourService where TourId=@TourId) select * from tbl_TourService where TourId=@TourId else select * from tbl_TourService where TourId=(select ParentId from tbl_Tour where TourId=@TourId)";
        //获取计划行程
        private const string SQL_SELECT_GetTourPlan = "if exists(select 1 from tbl_TourPlan where TourId=@TourId) select tbl_TourPlan.*,(select * from tbl_TourPlanSpot where PlanId=tbl_TourPlan.PlanId for xml raw,root) as Spot from tbl_TourPlan where TourId=@TourId order by Days else select tbl_TourPlan.*,(select * from tbl_TourPlanSpot where PlanId=tbl_TourPlan.PlanId for xml raw,root) as Spot from tbl_TourPlan where TourId=(select TourId from tbl_Tour where TourId=@TourId) order by Days";
        //获取散拼计划报价
        private const string SQL_SELECT_GetSanPinPrice = "if exists(select 1 from tbl_TourPrice where TourId=@TourId) select A.*,(select Name from tbl_ComStand where Id=A.Standard) as StandardName,(select Name from tbl_ComLev where LevId=A.LevelId) as LevelName from tbl_TourPrice as A where TourId=@TourId else select A.*,(select Name from tbl_ComStand where Id=A.Standard) as StandardName,(select Name from tbl_ComLev where LevId=A.LevelId) as LevelName from tbl_TourPrice as A where TourId=(select TourId from tbl_Tour where TourId=@TourId)";
        //获取团队计划分项报价
        private const string SQL_SELECT_GetTourTeamPrice = "select * from tbl_TourTeamPrice where TourId=@TourId";
        //获取无计划散客信息
        private const string SQL_SELECT_GetWuPlan = "select A.*,B.OrderId,B.OrderCode,(select * from tbl_TourOrderTraveller where OrderId=B.OrderId for xml raw,root) as Traveller,(select [Type],SourceId,SourceName,GuideNotes,PlanCost,Remarks from tbl_Plan where TourId=A.TourId and IsDelete='0' for xml raw,root) as PlanItem from tbl_Tour A inner join tbl_TourOrder B on B.TourId=A.TourId ";
        //设置收客状态
        private const string SQL_UPDATE_SetSouKeStatus = "update tbl_Tour set TourShouKeStatus=@TourShouKeStatus where TourId=@TourId and TourShouKeStatus in(0,3,4)";
        //获得线路区域
        private const string SQL_SELECT_GetArea = "select AreaId,AreaName,(select count(*) from tbl_Tour where AreaId=tbl_ComArea.AreaId and IsDelete=0 and TourStatus<11 and ParentId<>'' and TourShouKeStatus=0 and IsCheck=1 and TourType in(1,3,5,7) and datediff(day,getdate(),LDate)>=0) as TourNum from tbl_ComArea where CompanyId=@CompanyId";
        //获得计划变更实体
        private const string SQL_SELECT_GetTourPlanChangeModel = "select * from tbl_TourPlanChange where Id=@Id and CompanyId=@CompanyId";
        //设置计划状态
        private const string SQL_UPDATE_SetTourType = "update tbl_Tour set TourStatus=@TourStatus where TourId=@TourId";
        //计划弹出信息
        private const string SQL_SELECT_GetTourBaoInfo = "select TourCode,Operator,IssueTime from tbl_Tour where TourId=@TourId";
        //关键字计划数
        private const string SQL_SELECT_GetTourKeyInfo = "select KeyId,[Key],TourNum from tbl_ComTourKey where CompanyId=@CompanyId";
        //计划编号获得游客列表
        private const string SQL_SELECT_GetTourTraveller = "select * from tbl_TourOrderTraveller where OrderId in(select OrderId from tbl_TourOrder where TourId=@TourId)";
        //要安排合同号的订单列表
        private const string SQL_SELECT_GetSendTourOrderList = "select OrderId,OrderCode,BuyCompanyName,SellerName,TheNum from tbl_TourOrder where TourId=@TourId and IsDelete=0";
        //获得供应商发布的价格
        private const string SQL_SELECT_GetSupplyPrice = "if(exists(select 1 from tbl_TourSupplyPrice where TourId=@TourId)) select * from tbl_TourSupplyPrice where TourId=@TourId else  select * from tbl_TourSupplyPrice where TourId=(select ParentId from tbl_Tour where TourId=@TourId)";
        //得到订单编号
        private const string SQL_SELECT_GetOrderCode = "select OrderId,OrderCode from tbl_TourOrder where TourId=@TourId";
        //得到计划编号
        private const string SQL_SELECT_GetTourId = "select TourId from tbl_Tour where TourCode=@TourCode";
        //报销报账完成
        private const string SQL_SELECT_Apply = "update tbl_Tour set IsSubmit='1' where TourId=@TourId and CompanyId=@CompanyId and TourStatus>4 and TourStatus<11";
        //获得计划发布人信息
        private const string SQL_SELECT_GetPublisher = "if @UserType=1 select ContactName,ContactTel,ContactMobile,(select [Name] from tbl_Company where CompanyId=tbl_ComUser.CompanyId) as CompanyName from tbl_ComUser where UserId=@UserId else if @UserType=2 select [Name] as ContactName,Telephone as ContactTel,MobilePhone as ContactMobile,(select [Name] from tbl_Source where SourceId=tbl_CrmLinkman.TypeId) as CompanyName from tbl_CrmLinkman where UserId=@UserId";
        //添加计划原始信息
        private const string SQL_INSERT_AddOriginalTourInfo = "if exists(select 1 from tbl_Tour where TourId=@TourId and TourStatus=1 and CompanyId=@CompanyId) insert into tbl_TourOriginalInfo(TourId,CompanyId,TourType,TourContent,IssueTime) values(@TourId,@CompanyId,@TourType,@TourContent,getdate())";
        //获得计划原始信息
        private const string SQL_SELECT_GetOriginalTourInfo = "select * from tbl_TourOriginalInfo where TourId=@TourId and CompanyId=@CompanyId";
        //获取订单利润分配订单列表
        private const string SQL_SELECT_GetTourOrderDis = "select TourId,OrderId,OrderCode,BuyCompanyName,(Adults+Childs) as PersonNum,ConfirmMoney,SellerName from tbl_TourOrder where TourId=@TourId and not exists(select 1 from tbl_FinProfitDistribute where OrderId=tbl_TourOrder.OrderId and IsDeleted=0) ";
        const string SQL_SELECT_GetTourJiaGeBeiZhu = "SELECT QuoteRemark FROM tbl_Tour WHERE TourId=@TourId";

        //获取计划的预设车型
        private const string SQL_SELECT_GetTourCarType = "SELECT TourCarTypeId,TourId,CarTypeId,CarTypeName,SeatNum,[Desc] FROM tbl_TourCarType WHERE TourId=@TourId";

        //获取计划的上车地点
        private const string SQL_SELECT_GetTourCarLocation = "SELECT TourLocationId,TourId,CarLocationId,Location,OffPrice,OnPrice,[Desc],(case when exists(select 1 from tbl_TourOrderCarLocation where TourLocationId=tbl_TourCarLocation.TourLocationId) then '1' else '0' end) as isTourOrderExists FROM tbl_TourCarLocation WHERE TourId=@TourId";
        #endregion

        #region 成员方法

        /// <summary>
        /// 获得团队计划列表
        /// </summary>
        /// <param name="CompanyId">计划所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType">模块类型</param>  
        /// <param name="TourTeamSearch">搜索实体</param>
        /// <param name="DetpIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param>  
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourTeamInfo> GetTourTeamList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourTeamSearch TourTeamSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MTourTeamInfo> list = new List<EyouSoft.Model.TourStructure.MTourTeamInfo>();
            EyouSoft.Model.TourStructure.MTourTeamInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "IsChuTuan ASC , LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourCode,RouteName,LDate,TourDays,SellerName,SellerId,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlaner,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus,(select Adults,Childs,AdultPrice,BuyCompanyName,ContactName,ContactTel,OrderId from tbl_TourOrder where TourId=tbl_Tour.TourId  for xml raw,root) as OrderInfo,TourStatus,IsChange,IsSure,OperatorId,Operator,CancelReson");
            fields.Append(",case when exists(select 1 from tbl_Plan where TourId=tbl_Tour.TourId and IsDelete='0') then 1 else 0 end as IsPayMoney");
            #endregion
            #region 拼接查询条件
            //cmdQuery.AppendFormat(" IsDelete=0  and TourStatus<=12 and CompanyId='{0}'", CompanyId);
            cmdQuery.AppendFormat(" IsDelete=0  and CompanyId='{0}'", CompanyId);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and SellerId='{0}'", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(GetOrgCondition(LoginUserId, DetpIds, "SellerId", "DeptId"));
                }
            }
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.组团)
            //{
            //    cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.组团团队);
            //}
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.地接)
            //{
            //    cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.地接团队);
            //}
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.出境)
            //{
            //    cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.出境团队);
            //}
            if (TourTeamSearch != null)
            {
                if (TourTeamSearch.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", TourTeamSearch.AreaId);
                }
                if (TourTeamSearch.LDateStart.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", TourTeamSearch.LDateStart);
                }
                if (TourTeamSearch.LDateEnd.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", TourTeamSearch.LDateEnd);
                }
                if (TourTeamSearch.RDateStart.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)>=0", TourTeamSearch.RDateStart);
                }
                if (TourTeamSearch.RDateEnd.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)<=0", TourTeamSearch.RDateEnd);
                }
                if (TourTeamSearch.TourStatus.HasValue)
                {
                    cmdQuery.AppendFormat(" and TourStatus={0}", (int)TourTeamSearch.TourStatus);
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(TourTeamSearch.RouteName));
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", TourTeamSearch.SellerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(TourTeamSearch.SellerName))
                    {
                        cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(TourTeamSearch.SellerName));
                    }
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.PlanerId))
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourPlaner AS A WHERE A.TourId=tbl_Tour.TourId AND A.PlanerId='{0}') ", TourTeamSearch.PlanerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(TourTeamSearch.Planer))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_Tour.TourId and Planer like '%{0}%')", Utils.ToSqlLike(TourTeamSearch.Planer));
                    }
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.BuyCompanyId))
                {
                    cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourOrder AS A WHERE A.TourId=tbl_Tour.TourId AND A.BuyCompanyId='{0}' AND A.IsDelete='0') ", TourTeamSearch.BuyCompanyId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(TourTeamSearch.BuyCompanyName))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Tour.TourId and BuyCompanyName like '%{0}%' AND IsDelete='0') ", Utils.ToSqlLike(TourTeamSearch.BuyCompanyName));
                    }
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.OperatorId))
                {
                    cmdQuery.AppendFormat(" AND OperatorId='{0}' ", TourTeamSearch.OperatorId);
                }
                else if (!string.IsNullOrEmpty(TourTeamSearch.OperatorName))
                {
                    cmdQuery.AppendFormat(" AND Operator LIKE '%{0}%' ", TourTeamSearch.OperatorName);
                }
                if (!string.IsNullOrEmpty(TourTeamSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" AND TourCode LIKE '%{0}%' ", TourTeamSearch.TourCode);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourTeamInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        TourDays = rdr.IsDBNull(rdr.GetOrdinal("TourDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        CompanyInfo = new EyouSoft.Model.TourStructure.MCompanyInfo() { CompanyName = GetValueByXml(rdr["OrderInfo"].ToString(), "BuyCompanyName"), Contact = GetValueByXml(rdr["OrderInfo"].ToString(), "ContactName"), Phone = GetValueByXml(rdr["OrderInfo"].ToString(), "ContactTel") },
                        OrderId = GetValueByXml(rdr["OrderInfo"].ToString(), "OrderId"),
                        TourPlaner = GetTourPlanerByXml(rdr["TourPlaner"].ToString()),
                        SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { Name = rdr["SellerName"].ToString(), SellerId = rdr["SellerId"].ToString() },
                        Adults = Utils.GetInt(GetValueByXml(rdr["OrderInfo"].ToString(), "Adults")),
                        Childs = Utils.GetInt(GetValueByXml(rdr["OrderInfo"].ToString(), "Childs")),
                        AdultPrice = Utils.GetDecimal(GetValueByXml(rdr["OrderInfo"].ToString(), "AdultPrice")),
                        //TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString()),
                        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                        IsChange = rdr.IsDBNull(rdr.GetOrdinal("IsChange")) ? false : rdr.GetString(rdr.GetOrdinal("IsChange")) == "1" ? true : false,
                        IsSure = rdr.IsDBNull(rdr.GetOrdinal("IsSure")) ? false : rdr.GetString(rdr.GetOrdinal("IsSure")) == "1" ? true : false,
                        OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString() },
                        IsPayMoney = rdr.GetInt32(rdr.GetOrdinal("IsPayMoney")) == 1 ? true : false,
                        CancelReson = rdr["CancelReson"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得散拼计划列表
        /// </summary>
        /// <param name="CompanyId">计划所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType">模块类型</param>  
        /// <param name="TourSanPinSearch">搜索实体</param>
        /// <param name="DetpIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param>   
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = new List<EyouSoft.Model.TourStructure.MTourSanPinInfo>();
            EyouSoft.Model.TourStructure.MTourSanPinInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "IsChuTuan ASC , LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            //Old fields.Append(" SourceId,SourceCompanyName,TourId,TourCode,RouteName,LDate,TourDays,OperatorId,Operator,TourType,(select top 1 AdultPrice from tbl_TourSanPinPrice where (TourId=tbl_Tour.TourId or  TourId=tbl_Tour.ParentId) and LevType=1 order by Id desc for xml raw,root) as Price,PlanPeopleNumber,RealPeopleNumber,LeavePeopleNumber,(select sum(Adults+Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId and Status in(0,6,7,9)) as WeiChuLiPersonNum,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlaner,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanStatus ,TourStatus,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0 and Status<=5) as OrderCount,Adults,Childs,IsChange,IsSure,(select AreaName from tbl_ComArea where AreaId=tbl_Tour.AreaId) as AreaName,TourShouKeStatus,IsCheck,SellerId,CancelReson");
            fields.Append("TourId,RealPeopleNumber,TourCode,RouteName,LDate,TourDays,OperatorId,Operator,TourType,PlanPeopleNumber,(select top 1 AdultPrice from tbl_TourPrice where (TourId=tbl_Tour.TourId ) order by Id for xml raw,root) as Price,LeavePeopleNumber,(select sum(Adults+Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId) as WeiChuLiPersonNum,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlaner,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanStatus ,TourStatus,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0) as OrderCount,Adults,Childs,(select AreaName from tbl_ComArea where AreaId=tbl_Tour.AreaId) as AreaName,TourShouKeStatus,SellerId,CancelReson");
            fields.Append(",case when exists(select 1 from tbl_Plan where TourId=tbl_Tour.TourId and IsDelete='0') then 1 else 0 end as IsPayMoney");
            fields.Append(" ,SellerName, ");
            fields.Append(" (select sum(Adults) from tbl_TourOrder where TourId=tbl_Tour.TourId and OrderStatus=4)as AdultsOrderNum");
            fields.Append(" ,(select sum(Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId and OrderStatus=4)as ChildsOrderNum");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0  and CompanyId='{0}'", CompanyId);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and SellerId='{0}'", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(GetOrgCondition(LoginUserId, DetpIds, "SellerId", "DeptId"));
                }
            }
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.组团)
            //{
            //cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼);
            //2012-08-27 散拼添加 散拼短线查询的条件
            if (!string.IsNullOrEmpty(TourSanPinSearch.ParentId))
            {
                cmdQuery.AppendFormat(" and TourType={0} and parentid='{1}'", (int)EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品,TourSanPinSearch.ParentId);
            }
            else
            {
                cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.散拼模版团);
            }
            //}
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.地接)
            //{
            //    cmdQuery.Append(" and SourceId=''");
            //    cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼);
            //}
            //if (ModuleType == EyouSoft.Model.EnumType.TourStructure.ModuleType.出境)
            //{
            //    cmdQuery.Append(" and SourceId=''");
            //    cmdQuery.AppendFormat(" and TourType={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼);
            //}
            if (TourSanPinSearch != null)
            {
                if (TourSanPinSearch.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", TourSanPinSearch.AreaId);
                }
                if (TourSanPinSearch.TourSureStatus.HasValue)
                {
                    cmdQuery.AppendFormat(" and TourSureStatus={0} ", (int)TourSanPinSearch.TourSureStatus.Value);
                }
                if (TourSanPinSearch.TourStatus.HasValue)
                {
                    cmdQuery.AppendFormat(" and TourStatus={0}", (int)TourSanPinSearch.TourStatus);
                }
                if (TourSanPinSearch.TourDays != 0)
                {
                    cmdQuery.AppendFormat(" and TourDays='{0}'", TourSanPinSearch.TourDays);
                }
                if (TourSanPinSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", TourSanPinSearch.SLDate);
                }
                if (TourSanPinSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", TourSanPinSearch.LLDate);
                }
                if (TourSanPinSearch.SRDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)>=0", TourSanPinSearch.SRDate);
                }
                if (TourSanPinSearch.LRDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',RDate)<=0", TourSanPinSearch.LRDate);
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.RouteName));
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", TourSanPinSearch.SellerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(TourSanPinSearch.SellerName))
                    {
                        cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.SellerName));
                    }
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    var itemInfo = new EyouSoft.Model.TourStructure.MTourSanPinInfo();

                    itemInfo.TourId = rdr["TourId"].ToString();
                    itemInfo.TourCode = rdr["TourCode"].ToString();
                    itemInfo.RouteName = rdr["RouteName"].ToString();
                    itemInfo.LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    itemInfo.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    itemInfo.AdultPrice = Utils.GetDecimal(GetValueByXml(rdr["Price"].ToString(), "AdultPrice").ToString());
                    //PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                    itemInfo.RealPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")); //(rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults"))) + (rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")));
                    itemInfo.PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber"));
                    itemInfo.LeavePeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("LeavePeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LeavePeopleNumber"));
                    itemInfo.TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString());
                    itemInfo.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    itemInfo.TourPlaner = GetTourPlanerByXml(rdr["TourPlaner"].ToString());
                    itemInfo.OrderCount = rdr.IsDBNull(rdr.GetOrdinal("OrderCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderCount"));
                    itemInfo.Adults = rdr.IsDBNull(rdr.GetOrdinal("AdultsOrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AdultsOrderNum"));
                    itemInfo.Childs = rdr.IsDBNull(rdr.GetOrdinal("ChildsOrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ChildsOrderNum"));
                    //IsChange = rdr.IsDBNull(rdr.GetOrdinal("IsChange")) ? false : rdr.GetString(rdr.GetOrdinal("IsChange")) == "1" ? true : false;
                    //IsSure = rdr.IsDBNull(rdr.GetOrdinal("IsSure")) ? false : rdr.GetString(rdr.GetOrdinal("IsSure")) == "1" ? true : false;
                    itemInfo.AreaName = rdr["AreaName"].ToString();
                    itemInfo.TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)rdr.GetByte(rdr.GetOrdinal("TourShouKeStatus"));
                    itemInfo.OperatorInfo = new MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString() };
                    itemInfo.IsPayMoney = rdr.GetInt32(rdr.GetOrdinal("IsPayMoney")) == 1 ? true : false;
                    //SourceId = rdr["SourceId"].ToString().Trim();
                    //SourceCompanyName = rdr["SourceCompanyName"].ToString().Trim();
                    //IsCheck = rdr.IsDBNull(rdr.GetOrdinal("IsCheck")) ? false : rdr.GetString(rdr.GetOrdinal("IsCheck")) == "1" ? true : false;
                    //WeiChuLiPersonNum = rdr.IsDBNull(rdr.GetOrdinal("WeiChuLiPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("WeiChuLiPersonNum"));
                    itemInfo.SaleInfo = new MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() };
                    itemInfo.CancelReson = rdr["CancelReson"].ToString();
                    itemInfo.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));

                    list.Add(itemInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得分销商平台计划列表
        /// </summary>
        /// <param name="CompanyId">分销商所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MTourSaleSearch"></param>
        /// <param name="LevelId">客户等级编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSaleList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSaleSearch MTourSaleSearch, string LevelId)
        {
            IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = new List<EyouSoft.Model.TourStructure.MTourSanPinInfo>();
            EyouSoft.Model.TourStructure.MTourSanPinInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourDays,TourCode,RouteName,LDate,SellerId,SellerName,TourType,(select top 1 AdultPrice from tbl_TourPrice where TourId=tbl_Tour.TourId) as PeerAdultPrice,(select top 1 AdultPrice from tbl_TourPrice where TourId=tbl_Tour.TourId ) as SaleAdultPrice,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlaner,(select sum(Adults+Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId) as WeiChuLiPersonNum,Adults,Childs,PlanPeopleNumber,LeavePeopleNumber,RealPeopleNumber,TourShouKeStatus,ParentId");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and IsShowDistribution='1' and CompanyId='{1}'", DateTime.Now, CompanyId);
            if (MTourSaleSearch != null)
            {
                if (MTourSaleSearch.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", MTourSaleSearch.AreaId);
                }
                if (!string.IsNullOrEmpty(MTourSaleSearch.Keyword))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_ComArea a where a.areaid=tbl_tour.areaid and a.keyword='{0}')", Utils.ToSqlLike(MTourSaleSearch.Keyword));
                }
                if (!string.IsNullOrEmpty(MTourSaleSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(MTourSaleSearch.RouteName));
                }
                if (MTourSaleSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", MTourSaleSearch.SLDate);
                }
                if (MTourSaleSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", MTourSaleSearch.LLDate);
                }
                if (MTourSaleSearch.IsChuTuan.HasValue)
                {
                    cmdQuery.AppendFormat(" and ischutuan='{0}'", MTourSaleSearch.IsChuTuan.Value ? "1" : "0");
                }
                if (MTourSaleSearch.TourType.HasValue)
                {
                    cmdQuery.AppendFormat(" and TourType='{0}'", (int)MTourSaleSearch.TourType.Value);
                }
                if (!string.IsNullOrEmpty(MTourSaleSearch.ParentId))
                {
                    cmdQuery.AppendFormat(" and ParentId='{0}'", MTourSaleSearch.ParentId);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourSanPinInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        SaleAdultPrice = rdr.IsDBNull(rdr.GetOrdinal("SaleAdultPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SaleAdultPrice")),
                        PeerAdultPrice = rdr.IsDBNull(rdr.GetOrdinal("PeerAdultPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("PeerAdultPrice")),
                        PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                        RealPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")),
                        LeavePeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("LeavePeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LeavePeopleNumber")),
                        TourPlaner = GetTourPlanerByXml(rdr["TourPlaner"].ToString()),
                        // ShowPublisher = (EyouSoft.Model.EnumType.TourStructure.ShowPublisher)rdr.GetByte(rdr.GetOrdinal("ShowPublisher")),
                        // SourceCompanyName = rdr.IsDBNull(rdr.GetOrdinal("SourceCompanyName")) ? "" : rdr.GetString(rdr.GetOrdinal("SourceCompanyName")),
                        SaleInfo = new MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() },
                        TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)rdr.GetByte(rdr.GetOrdinal("TourShouKeStatus")),
                        WeiChuLiPersonNum = rdr.IsDBNull(rdr.GetOrdinal("WeiChuLiPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("WeiChuLiPersonNum")),
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType")),
                        ParentId=rdr["ParentId"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得供应商平台计划列表
        /// </summary>
        /// <param name="SourceId">供应商编号</param> 
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MTourSupplierSearch">搜索实体</param>         
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> GetTourSupplierList(string SourceId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSupplierSearch MTourSupplierSearch)
        {
            IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = new List<EyouSoft.Model.TourStructure.MTourSanPinInfo>();
            EyouSoft.Model.TourStructure.MTourSanPinInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourCode,RouteName,LDate,TourDays,(select top 1 AdultJSPrice,ChildJSPrice from tbl_TourSupplyPrice where TourId=tbl_Tour.TourId for xml raw,root) as Price,(select top 1 AdultJSPrice,ChildJSPrice from tbl_TourSupplyPrice where TourId=tbl_Tour.ParentId for xml raw,root) as ParentPrice,PlanPeopleNumber,RealPeopleNumber,LeavePeopleNumber,TourStatus,IsCheck,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0 and Status<=5) as OrderCount,Adults,Childs,(select sum(Adults+Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId and Status in(0,6,7,9)) as WeiChuLiPersonNum");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and ParentId<>'' and SourceId='{0}'", SourceId);

            if (MTourSupplierSearch != null)
            {
                if (MTourSupplierSearch.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", MTourSupplierSearch.AreaId);
                }
                if (MTourSupplierSearch.RealPeopleNumberManipulate.HasValue)
                {
                    switch (MTourSupplierSearch.RealPeopleNumberManipulate)
                    {
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于:
                            {
                                cmdQuery.AppendFormat(" and RealPeopleNumber>={0}", MTourSupplierSearch.RealPeopleNumber);
                                break;
                            }
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.等于:
                            {
                                cmdQuery.AppendFormat(" and RealPeopleNumber={0}", MTourSupplierSearch.RealPeopleNumber);
                                break;
                            }
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于:
                            {
                                cmdQuery.AppendFormat(" and RealPeopleNumber<={0}", MTourSupplierSearch.RealPeopleNumber);
                                break;
                            }
                    }
                }
                if (!string.IsNullOrEmpty(MTourSupplierSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(MTourSupplierSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(MTourSupplierSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(MTourSupplierSearch.RouteName));
                }
                if (MTourSupplierSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", MTourSupplierSearch.SLDate);
                }
                if (MTourSupplierSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", MTourSupplierSearch.LLDate);
                }

            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourSanPinInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        AdultPrice = Utils.GetDecimal(GetValueByXml(string.IsNullOrEmpty(rdr["Price"].ToString()) ? rdr["ParentPrice"].ToString() : rdr["Price"].ToString(), "AdultJSPrice").ToString()),
                        ChildPrice = Utils.GetDecimal(GetValueByXml(string.IsNullOrEmpty(rdr["Price"].ToString()) ? rdr["ParentPrice"].ToString() : rdr["Price"].ToString(), "ChildJSPrice").ToString()),
                        PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                        RealPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")),
                        LeavePeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("LeavePeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LeavePeopleNumber")),
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        WeiChuLiPersonNum = rdr.IsDBNull(rdr.GetOrdinal("WeiChuLiPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("WeiChuLiPersonNum")),
                        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                        IsCheck = rdr["IsCheck"].ToString() == "1" ? true : false,
                        OrderCount = rdr.IsDBNull(rdr.GetOrdinal("OrderCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderCount"))
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得计调列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="ModuleType"></param>
        /// <param name="MPlanListSearch"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MPlanList> GetPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MPlanListSearch MPlanListSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MPlanList> list = new List<EyouSoft.Model.TourStructure.MPlanList>();
            EyouSoft.Model.TourStructure.MPlanList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" CompanyId,TourId,TourCode,RouteName,LDate,TourStatus,TourType,OutQuoteType,SellerName,(select top 1 BuyCompanyId,BuyCompanyName,ContactName,ContactTel from tbl_TourOrder where TourId=tbl_Tour.TourId for xml raw,root) as CompanyList,TourDays,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as PlanerList,Adults,Childs,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0 and Status=4) as OrderNum,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus,IsChange,IsSure");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and TourStatus>0 and TourStatus<=11 and ParentId<>'' and CompanyId='{0}'", CompanyId);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(" and (exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
                    cmdQuery.AppendFormat(" or exists(select 1 from tbl_TourPlaner where DeptId in ({0}) and TourId=tbl_Tour.TourId))", GetIdsByArr(DetpIds));
                }
            }
            //switch (ModuleType)
            //{
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.组团:
            //        cmdQuery.Append(" and TourType in(0,1,7)");
            //        break;
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.地接:
            //        cmdQuery.Append(" and TourType in(2,3)");
            //        break;
            //    case EyouSoft.Model.EnumType.TourStructure.ModuleType.出境:
            //        cmdQuery.Append(" and  TourType in(4,5)");
            //        break;
            //}
            if (MPlanListSearch != null)
            {
                if (!string.IsNullOrEmpty(MPlanListSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(MPlanListSearch.TourCode));
                }
                if (MPlanListSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", MPlanListSearch.SLDate);
                }
                if (MPlanListSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", MPlanListSearch.LLDate);
                }
                if (!string.IsNullOrEmpty(MPlanListSearch.PlanerId))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_Tour.TourId  and PlanerId='{0}')", MPlanListSearch.PlanerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.Planer))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_Tour.TourId  and Planer like '%{0}%')", Utils.ToSqlLike(MPlanListSearch.Planer));
                    }
                }
                if (MPlanListSearch.TourStatus != null)
                {
                    cmdQuery.AppendFormat(" and TourStatus={0}", (int)MPlanListSearch.TourStatus);
                }
                if (MPlanListSearch.SaleInfo != null)
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.SaleInfo.SellerId))
                    {
                        cmdQuery.AppendFormat(" and SellerId='{0}'", MPlanListSearch.SaleInfo.SellerId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(MPlanListSearch.SaleInfo.Name))
                        {
                            cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(MPlanListSearch.SaleInfo.Name));
                        }
                    }
                }
                if (MPlanListSearch.CompanyInfo != null)
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.CompanyInfo.CompanyId))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Tour.TourId and BuyCompanyId='{0}')", MPlanListSearch.CompanyInfo.CompanyId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(MPlanListSearch.CompanyInfo.CompanyName))
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Tour.TourId and BuyCompanyName like '%{0}%')", Utils.ToSqlLike(MPlanListSearch.CompanyInfo.CompanyName));
                        }
                    }
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MPlanList();
                    item.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    item.TourCode = rdr["TourCode"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LDate")))
                    {
                        item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    }
                    item.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.OrderNum = rdr.IsDBNull(rdr.GetOrdinal("OrderNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNum"));
                    if (item.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.团队产品)
                    {
                        item.CompanyInfo = GetCompanyInfoByXml(rdr["CompanyList"].ToString());
                    }
                    item.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { Name = rdr.GetString(rdr.GetOrdinal("SellerName")) };
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    item.TourPlaner = GetTourPlanerByXml(rdr["PlanerList"].ToString());
                    item.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)rdr.GetByte(rdr.GetOrdinal("OutQuoteType"));
                    //item.TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString());
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    item.IsChange = rdr.IsDBNull(rdr.GetOrdinal("IsChange")) ? false : rdr.GetString(rdr.GetOrdinal("IsChange")) == "1" ? true : false;
                    item.IsSure = rdr.IsDBNull(rdr.GetOrdinal("IsSure")) ? false : rdr.GetString(rdr.GetOrdinal("IsSure")) == "1" ? true : false;
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得计划报账列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="BZList"></param>
        /// <param name="DeptIds">部门编号</param>  
        /// <param name="isOnlySelf">是否仅查看自己的数据</param> 
        /// <param name="LoginUserId">当前登录用户编号</param> 
        /// <param name="MBZSearch"></param>
        /// <param name="setting">系统配置信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MBZInfo> GetBZList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.EnumType.TourStructure.BZList BZList, int[] DeptIds, bool isOnlySelf, string LoginUserId, EyouSoft.Model.TourStructure.MBZSearch MBZSearch, EyouSoft.Model.ComStructure.MComSetting setting)
        {
            IList<EyouSoft.Model.TourStructure.MBZInfo> list = new List<EyouSoft.Model.TourStructure.MBZInfo>();
            EyouSoft.Model.TourStructure.MBZInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.AppendFormat(" TourId,TourCode,RouteName,LDate,SellerName,SellerId,TourType,Guides=STUFF((SELECT ',' + SourceName FROM (SELECT SourceName FROM dbo.tbl_Plan WHERE TourId=tbl_Tour.TourId AND IsDelete=0 AND [Type]={0}) AS T FOR XML PATH('')),1, 1,''),Planers=STUFF((SELECT ',' + Planer FROM (SELECT Planer FROM tbl_TourPlaner WHERE TourId=tbl_Tour.TourId) AS T FOR XML PATH('')),1, 1,''),Adults,Childs,(select sum(ConfirmMoney) TourSettlement from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0) as ConfirmSettlementMoney,(select sum(Confirmation) from tbl_Plan where Status={2} and TourId=tbl_Tour.TourId and IsDelete=0) as TourPay,(select sum(Amount) from tbl_FinProfitDistribute where  TourId=tbl_Tour.TourId and IsDeleted='0') as DisProfit,TourStatus,ISNULL((SELECT SUM(FeeAmount) FROM dbo.tbl_FinOtherInFee WHERE TourId=tbl_tour.tourid),0) as OtherIncome,ISNULL((SELECT SUM(FeeAmount) FROM dbo.tbl_FinOtherOutFee WHERE TourId=tbl_tour.tourid),0) as OtherOutpay", (int)PlanProject.导游, (int)OrderStatus.已成交, (int)PlanState.已落实);
            //订单结算金额合计，所有订单报账后用黑色，其它红色
            //fields.AppendFormat(",case when not exists(select 1 from tbl_tourorder where TourId=tbl_Tour.TourId and IsDelete=0) then 1 else 0 end as isblack", (int)OrderStatus.已成交);
            fields.Append(",Operator");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and CompanyId='{0}' and TourStatus<={1} AND TourType not in ({2}) ", CompanyId, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.封团, (int)TourType.自由行);
            if (MBZSearch != null && !string.IsNullOrEmpty(MBZSearch.TourId)) cmdQuery.AppendFormat(" AND TourId='{0}' ", MBZSearch.TourId);
            switch (BZList)
            {
                #region 导游报账
                case EyouSoft.Model.EnumType.TourStructure.BZList.导游报账:
                    {
                        if (isOnlySelf)
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_Plan where TourId=tbl_Tour.TourId and [Type]={1} and GuideUserId='{0}' and IsDelete=0)", LoginUserId, (int)PlanProject.导游);
                        }
                        else
                        {
                            if (DeptIds != null)
                            {
                                cmdQuery.AppendFormat(" and (exists(select 1 from tbl_Plan where TourId=tbl_Tour.TourId and [Type]={1} and GuideUserId='{0}' and IsDelete=0)", LoginUserId, (int)PlanProject.导游);
                                cmdQuery.AppendFormat(" or exists(select 1 from tbl_Plan where TourId=tbl_Tour.TourId and [Type]={1} and IsDelete=0 and GuideDeptId in ({0}))", GetIdsByArr(DeptIds), (int)PlanProject.导游);
                                cmdQuery.AppendFormat(" OR NOT EXISTS(SELECT 1 FROM tbl_Plan WHERE TourId=tbl_Tour.TourId AND [Type]={0} AND IsDelete='0')) ", (int)PlanProject.导游);
                            }
                        }
                        if (MBZSearch.IsDealt)
                        {
                            cmdQuery.AppendFormat(" and TourStatus>{0} ", (int)TourStatus.导游报帐);
                        }
                        else
                        {
                            cmdQuery.AppendFormat(" and TourStatus={0}", (int)TourStatus.导游报帐);
                        }
                        break;
                    }
                #endregion

                #region 计调报账
                case EyouSoft.Model.EnumType.TourStructure.BZList.计调报账:
                    {
                        if (isOnlySelf)
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
                        }
                        else
                        {
                            if (DeptIds != null)
                            {
                                cmdQuery.AppendFormat(" and (exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
                                cmdQuery.AppendFormat(" or exists(select 1 from tbl_TourPlaner where PlanerDeptId in({0}) and TourId=tbl_Tour.TourId))", GetIdsByArr(DeptIds));
                            }
                        }
                        cmdQuery.AppendFormat(" and TourStatus<={0} and TourStatus>={1}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.导游报销, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.导游带团);
                        break;
                    }
                #endregion

                #region 报销
                case BZList.报销:
                    if (MBZSearch.IsDealt)//已报销
                    {
                        cmdQuery.AppendFormat(" AND TourStatus>{0} ", (int)TourStatus.导游报销);
                    }
                    else//未报销
                    {
                        cmdQuery.AppendFormat(" AND TourStatus={0} ", (int)TourStatus.导游报销);
                    }

                    if (isOnlySelf)
                    {
                        cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourStatusChange AS A1 WHERE A1.TourId=tbl_Tour.TourId AND A1.TourStatus IN({1},{2}) AND A1.OperatorId='{0}') ", LoginUserId, (int)TourStatus.导游报销, (int)TourStatus.财务待审);
                    }
                    else
                    {
                        if (DeptIds != null && DeptIds.Length > 0)
                        {
                            cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourStatusChange AS A1 WHERE A1.TourId=tbl_Tour.TourId AND A1.TourStatus IN({2},{3}) AND (A1.OperatorId='{0}' OR A1.OperatorDeptId IN({1}))) ", LoginUserId, GetIdsByArr(DeptIds), (int)TourStatus.导游报销, (int)TourStatus.财务待审);
                        }
                    }
                    break;
                #endregion

                #region 报账
                case EyouSoft.Model.EnumType.TourStructure.BZList.报账:
                    if (MBZSearch.IsDealt)//已报账
                    {
                        cmdQuery.AppendFormat(" AND TourStatus>{0} ", (int)TourStatus.财务待审);
                    }
                    else//未报账
                    {
                        cmdQuery.AppendFormat(" AND TourStatus ={0} ", (int)TourStatus.财务待审);
                    }

                    if (isOnlySelf)
                    {
                        cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourStatusChange AS A1 WHERE A1.TourId=tbl_Tour.TourId AND A1.TourStatus in ({1},{2}) AND A1.OperatorId='{0}') ", LoginUserId, (int)TourStatus.财务待审, (int)TourStatus.单团核算);
                    }
                    else
                    {
                        if (DeptIds != null && DeptIds.Length > 0)
                        {
                            cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourStatusChange AS A1 WHERE A1.TourId=tbl_Tour.TourId AND A1.TourStatus in ({2},{3}) AND (A1.OperatorId='{0}' OR A1.OperatorDeptId IN({1}))) ", LoginUserId, GetIdsByArr(DeptIds), (int)TourStatus.财务待审, (int)TourStatus.单团核算);
                        }
                    }
                    break;
                #endregion

                #region 单团核算
                case EyouSoft.Model.EnumType.TourStructure.BZList.单团核算:
                    if (isOnlySelf)
                    {
                        cmdQuery.AppendFormat(" AND SellerId='{0}' ", LoginUserId);
                    }
                    else if (DeptIds != null)
                    {
                        cmdQuery.AppendFormat(" AND(SellerId='{0}' OR SellerDeptId IN({1})) ", LoginUserId, Utils.GetSqlIn<int>(DeptIds));
                    }

                    if (MBZSearch.IsDealt)
                    {
                        cmdQuery.AppendFormat(" and TourStatus={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.封团);
                    }
                    else
                    {
                        cmdQuery.AppendFormat(" and TourStatus={0}", (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算);
                    }
                    break;
                #endregion
            }
            if (MBZSearch != null)
            {
                if (!string.IsNullOrEmpty(MBZSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(MBZSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(MBZSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(MBZSearch.RouteName));
                }
                if (MBZSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and LDate>='{0}'", MBZSearch.SLDate.Value);
                }
                if (MBZSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and LDate<'{0}'", MBZSearch.LLDate.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(MBZSearch.PlanerId))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", MBZSearch.PlanerId);
                }
                else if (!string.IsNullOrEmpty(MBZSearch.Planer))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where Planer like '%{0}%' and TourId=tbl_Tour.TourId)", Utils.ToSqlLike(MBZSearch.Planer));
                }
                if (!string.IsNullOrEmpty(MBZSearch.GuideId))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Plan where SourceId='{0}' and [Type]={1} and IsDelete=0 and TourId=tbl_Tour.TourId)", MBZSearch.GuideId, (int)PlanProject.导游);
                }
                else if (!string.IsNullOrEmpty(MBZSearch.Guide))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Plan where SourceName like '%{0}%' and [Type]={1} and IsDelete=0 and TourId=tbl_Tour.TourId)", Utils.ToSqlLike(MBZSearch.Guide), (int)PlanProject.导游);
                }
                if (!string.IsNullOrEmpty(MBZSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", MBZSearch.SellerId);
                }
                else if (!string.IsNullOrEmpty(MBZSearch.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(MBZSearch.SellerName));
                }
                if (MBZSearch.TourStatus != null)
                {
                    cmdQuery.AppendFormat(" and TourStatus={0}", (int)MBZSearch.TourStatus);
                }
                if (!string.IsNullOrEmpty(MBZSearch.FaBuRenId))
                {
                    cmdQuery.AppendFormat(" AND OperatorId='{0}' ", MBZSearch.FaBuRenId);
                }
                else if (!string.IsNullOrEmpty(MBZSearch.FaBuRenName))
                {
                    cmdQuery.AppendFormat(" AND Operator LIKE '%{0}%' ", MBZSearch.FaBuRenName);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MBZInfo();
                    item.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.RouteName = rdr["RouteName"].ToString();
                    item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    item.Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.TourSettlement = rdr.IsDBNull(rdr.GetOrdinal("ConfirmSettlementMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmSettlementMoney"));
                    item.TourPay = rdr.IsDBNull(rdr.GetOrdinal("TourPay")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPay"));
                    item.SellerId = rdr["SellerId"].ToString();
                    item.SellerName = rdr["SellerName"].ToString();
                    //item.MPlanerInfo = GetTourPlanerByXml(rdr["Planer"].ToString());
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    //item.MGuidInfo = GetGuidByXml(rdr["Guid"].ToString());
                    item.DisProfit = rdr.IsDBNull(rdr.GetOrdinal("DisProfit")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("DisProfit"));
                    //item.IsBlack = rdr.GetInt32(rdr.GetOrdinal("IsBlack")) == 1 ? true : false;
                    item.TourType = (TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.FaBuRenName = rdr["Operator"].ToString();
                    item.Planers = rdr["Planers"].ToString();
                    item.Guides = rdr["Guides"].ToString();
                    item.OtherIncome = rdr.GetDecimal(rdr.GetOrdinal("OtherIncome"));
                    item.OtherOutpay = rdr.GetDecimal(rdr.GetOrdinal("OtherOutpay"));
                    item.IsShowGouWu = MBZSearch.IsShowGouWu;
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得同行分销散拼计划
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="TourSanPinSearch"></param>
        /// <param name="DetpIds">部门集合</param>
        /// <param name="isOnlySeft">是否仅查看自己的数据</param>
        /// <param name="LoginUserId">当前登录的用户编号</param>  
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourTongHanInfo> GetTYFXTourSanPinList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourSanPinSearch TourSanPinSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MTourTongHanInfo> list = new List<EyouSoft.Model.TourStructure.MTourTongHanInfo>();
            EyouSoft.Model.TourStructure.MTourTongHanInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("SourceId,TourId,TourCode,RouteName,LDate,TourDays,OperatorId,Operator,IsCheck,SourceCompanyName,(select top 1 AdultPrice from tbl_TourSanPinPrice where (TourId=tbl_Tour.TourId or  TourId=tbl_Tour.ParentId) and LevType=1 order by Id desc for xml raw,root) as Price,PlanPeopleNumber,RealPeopleNumber,LeavePeopleNumber,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlaner,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanStatus ,TourStatus,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0 and Status<=5) as OrderCount,Adults,Childs,IsChange,IsSure,(select AreaName from tbl_ComArea where AreaId=tbl_Tour.AreaId) as AreaName,TourShouKeStatus,(select sum(Adults+Childs) from tbl_TourOrder where TourId=tbl_Tour.TourId and Status in(0,6,7,9)) as WeiChuLiPersonNum");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and ParentId<>'' and (TourStatus<11 or TourStatus=13 or TourStatus=15) and IsCheck=1 and TourShouKeStatus=0 and datediff(day,getdate(),LDate)>=0 and TourType in(1,3,5,7) and CompanyId='{0}'", CompanyId);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and SellerId='{0}'", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(GetOrgCondition(LoginUserId, DetpIds, "SellerId", "DeptId"));
                }
            }
            if (TourSanPinSearch != null)
            {
                if (TourSanPinSearch.KeyId != 0)
                {
                    cmdQuery.AppendFormat(" and KeyId={0}", TourSanPinSearch.KeyId);
                }
                if (TourSanPinSearch.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", TourSanPinSearch.AreaId);
                }
                if (TourSanPinSearch.TourDays != 0)
                {
                    cmdQuery.AppendFormat(" and TourDays={0}", TourSanPinSearch.TourDays);
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.TourCode));
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.RouteName));
                }
                if (TourSanPinSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", TourSanPinSearch.SLDate);
                }
                if (TourSanPinSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", TourSanPinSearch.LLDate);
                }
                if (!string.IsNullOrEmpty(TourSanPinSearch.SellerName))
                {
                    cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(TourSanPinSearch.SellerName));
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourTongHanInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        AdultPrice = Utils.GetDecimal(GetValueByXml(rdr["Price"].ToString(), "AdultPrice").ToString()),
                        PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                        RealPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("RealPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("RealPeopleNumber")),
                        LeavePeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("LeavePeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LeavePeopleNumber")),
                        //TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString()),
                        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                        TourPlaner = GetTourPlanerByXml(rdr["TourPlaner"].ToString()),
                        OrderCount = rdr.IsDBNull(rdr.GetOrdinal("OrderCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderCount")),
                        IsChange = rdr.IsDBNull(rdr.GetOrdinal("IsChange")) ? false : rdr.GetString(rdr.GetOrdinal("IsChange")) == "1" ? true : false,
                        IsSure = rdr.IsDBNull(rdr.GetOrdinal("IsSure")) ? false : rdr.GetString(rdr.GetOrdinal("IsSure")) == "1" ? true : false,
                        AreaName = rdr["AreaName"].ToString(),
                        TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)rdr.GetByte(rdr.GetOrdinal("TourShouKeStatus")),
                        OperatorInfo = new MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString() },
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        WeiChuLiPersonNum = rdr.IsDBNull(rdr.GetOrdinal("WeiChuLiPersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("WeiChuLiPersonNum")),
                        SourceId = rdr["SourceId"].ToString(),
                        IsCheck = rdr.IsDBNull(rdr.GetOrdinal("IsCheck")) ? false : rdr.GetString(rdr.GetOrdinal("IsCheck")) == "1" ? true : false,
                        SourceCompanyName = rdr["SourceCompanyName"].ToString().Trim()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得计划签证文件
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string TourId, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.ComStructure.MComAttach> list = new List<EyouSoft.Model.ComStructure.MComAttach>();
            EyouSoft.Model.ComStructure.MComAttach item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_ComAttach";
            string PrimaryKey = "";
            string OrderByString = "Downloads";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("[Name],FilePath");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" ItemId='{0}' and ItemType=18", TourId);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.ComStructure.MComAttach()
                    {
                        Name = rdr["Name"].ToString(),
                        FilePath = rdr["FilePath"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 派团给计调的未出团计划列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="RouteName"></param>
        /// <param name="TourCode"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourBaseInfo> GetSendWCTTour(string CompanyId, int pageSize, int pageIndex, ref int recordCount, string RouteName, string TourCode, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MTourBaseInfo> list = new List<EyouSoft.Model.TourStructure.MTourBaseInfo>();
            EyouSoft.Model.TourStructure.MTourBaseInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("TourId,TourCode,RouteName,LDate,RDate,TourDays");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and IsDelete='0' and TourStatus={1} and datediff(day,getdate(),LDate)>=0", CompanyId, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置);
            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
            if (!string.IsNullOrEmpty(RouteName))
            {
                cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(RouteName));
            }
            if (!string.IsNullOrEmpty(TourCode))
            {
                cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(TourCode));
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourBaseInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                        TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取计划行程变更列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourPlanChange> GetTourPlanChange(MTourPlanChangeBase mSearch, int pageSize, int pageIndex, ref int recordCount, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.TourStructure.MTourPlanChange> list = new List<EyouSoft.Model.TourStructure.MTourPlanChange>();
            EyouSoft.Model.TourStructure.MTourPlanChange item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_TourPlanChange";
            string PrimaryKey = "Id";
            string OrderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.AppendFormat("*,Planers=STUFF((SELECT ',' + Planer FROM (SELECT Planer FROM tbl_TourPlaner WHERE TourId=tbl_TourPlanChange.TourId) AS T FOR XML PATH('')),1, 1,''),(select DISTINCT SourceId,SourceName from tbl_Plan where TourId=tbl_TourPlanChange.TourId and Type={0} and IsDelete=0 for xml raw,root) as Guid,(select * from tbl_TourPlaner where TourId=tbl_TourPlanChange.TourId for xml raw,root) as Planer,(select * from tbl_TourPlaner where TourId=tbl_TourPlanChange.TourId for xml raw,root) as PlanerList,(select * from tbl_TourPlanStatus where TourId=tbl_TourPlanChange.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus", (int)PlanProject.导游);
            #endregion
            #region 要查询的条件
            cmdQuery.AppendFormat(" CompanyId='{0}'", mSearch.CompanyId);
            if (mSearch.ChangeType.HasValue)
            {
                cmdQuery.AppendFormat(" AND ChangeType={0}", (int)mSearch.ChangeType.Value);
            }
            if (mSearch.State.HasValue)
            {
                cmdQuery.AppendFormat(" AND Status={0}", (int)mSearch.State.Value);
            }
            if (!string.IsNullOrEmpty(mSearch.TourId))
            {
                cmdQuery.AppendFormat(" AND TourId='{0}'", mSearch.TourId);
            }
            else if (!string.IsNullOrEmpty(mSearch.TourCode))
            {
                cmdQuery.AppendFormat(" AND tourcode like '%{0}%'", Utils.ToSqlLike(mSearch.TourCode));
            }
            if (mSearch.AreaId != 0)
            {
                cmdQuery.AppendFormat(" AND areaid={0}", mSearch.AreaId);
            }
            else if (!string.IsNullOrEmpty(mSearch.AreaName))
            {
                cmdQuery.AppendFormat(" AND areaname like '%{0}%'", Utils.ToSqlLike(mSearch.AreaName));
            }
            if (!string.IsNullOrEmpty(mSearch.SaleId))
            {
                cmdQuery.AppendFormat(" AND SaleId='{0}'", mSearch.SaleId);
            }
            else if (!string.IsNullOrEmpty(mSearch.SellerName))
            {
                cmdQuery.AppendFormat(" AND SellerName like '%{0}%'", Utils.ToSqlLike(mSearch.SellerName));
            }
            if (!string.IsNullOrEmpty(mSearch.GuideId))
            {
                cmdQuery.AppendFormat(" AND GuideId='{0}'", mSearch.GuideId);
            }
            else if (!string.IsNullOrEmpty(mSearch.GuideNm))
            {
                cmdQuery.AppendFormat(" AND GuideNm like '%{0}%'", Utils.ToSqlLike(mSearch.GuideNm));
            }
            if (mSearch.IssueTimeS.HasValue)
            {
                cmdQuery.AppendFormat(" AND IssueTime>='{0}'", mSearch.IssueTimeS.Value);
            }
            if (mSearch.IssueTimeE.HasValue)
            {
                cmdQuery.AppendFormat(" AND IssueTime<'{0}'", mSearch.IssueTimeE.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(mSearch.PlanerId))
            {
                cmdQuery.AppendFormat(" AND exists(select 1 from tbl_TourPlaner where tourid=tbl_TourPlanChange.tourid and PlanerId='{0}')", mSearch.PlanerId);
            }
            else if (!string.IsNullOrEmpty(mSearch.Planer))
            {
                cmdQuery.AppendFormat(" AND exists(select 1 from tbl_TourPlaner where tourid=tbl_TourPlanChange.tourid and Planer like '%{0}%')", Utils.ToSqlLike(mSearch.Planer));
            }
            switch (mSearch.SL)
            {
                case EyouSoft.Model.EnumType.PrivsStructure.Menu2.销售中心_导游变更:
                    cmdQuery.AppendFormat(" AND Status <= {0}", (int)ChangeStatus.计调未确认);
                    if (isOnlySeft)
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_plan where GuideUserId='{0}' and type={1} and TourId=tbl_TourPlanChange.TourId and isdelete=0)", LoginUserId, (int)PlanProject.导游);
                    }
                    else
                    {
                        if (DetpIds != null)
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_plan where GuideDeptId in ({0}) and type={1} and TourId=tbl_TourPlanChange.TourId and isdelete=0)", GetIdsByArr(DetpIds), (int)PlanProject.导游);
                        }
                    }
                    break;
                case EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_业务变更:
                    cmdQuery.AppendFormat(" AND Status >= {0}", (int)ChangeStatus.计调未确认);
                    if (isOnlySeft)
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_TourPlanChange.TourId)", LoginUserId);
                    }
                    else
                    {
                        if (DetpIds != null)
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerDeptId in ({0}) and TourId=tbl_TourPlanChange.TourId)", GetIdsByArr(DetpIds));
                        }
                    }
                    break;
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourPlanChange()
                    {
                        Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                        TourId = rdr["TourId"].ToString(),
                        AreaName = rdr["areaname"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        SellerName = rdr["SellerName"].ToString(),
                        GuideId = rdr["guideid"].ToString(),
                        GuideNm = rdr["guidenm"].ToString(),
                        TourGuide = GetGuidByXml(rdr["Guid"].ToString()),
                        TourPlaner = GetTourPlanerByXml(rdr["Planer"].ToString()),
                        TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString()),
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Operator = rdr["Operator"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Content = rdr["Content"].ToString(),
                        State = (ChangeStatus)rdr.GetByte(rdr.GetOrdinal("status")),
                        Planer = rdr["Planers"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取计划变更实体
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourPlanChange GetTourChangeModel(string CompanyId, int Id)
        {
            EyouSoft.Model.TourStructure.MTourPlanChange info = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourPlanChangeModel);
            this._db.AddInParameter(cmd, "Id", DbType.Int32, Id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.TourStructure.MTourPlanChange()
                    {
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),
                        Operator = rdr["Operator"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Content = rdr["Content"].ToString(),
                        State = (ChangeStatus)rdr.GetByte(rdr.GetOrdinal("status")),
                        AreaName = rdr["areaname"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                    };
                }
            }
            return info;
        }

        /// <summary>
        /// 计划确认变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int TourChangeSure(EyouSoft.Model.TourStructure.MTourPlanChangeConfirm model)
        {
            var cmd = this._db.GetStoredProcCommand("proc_TourChangeConfirm");
            this._db.AddInParameter(cmd, "Confirmer", DbType.String, model.Confirmer);
            this._db.AddInParameter(cmd, "ConfirmerId", DbType.AnsiStringFixedLength, model.ConfirmerId);
            this._db.AddInParameter(cmd, "ConfirmTime", DbType.DateTime, DateTime.Now);
            this._db.AddInParameter(cmd, "ConfirmerType", DbType.Byte, (int)model.ConfirmerType);
            this._db.AddInParameter(cmd, "ChangeStatus", DbType.Byte, (int)model.ChangeStatus);
            this._db.AddInParameter(cmd, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            this._db.AddOutParameter(cmd, "@Return", DbType.Int32, 1);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Return"));
        }

        /// <summary>
        /// 业务变更新增/修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int TourChangeAddOrUpd(EyouSoft.Model.TourStructure.MTourPlanChange model)
        {
            var cmd = this._db.GetStoredProcCommand("proc_TourPlanChangeAddOrUpd");
            this._db.AddInParameter(cmd, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(cmd, "@TourId", DbType.AnsiStringFixedLength, model.TourId);
            this._db.AddInParameter(cmd, "@TourCode", DbType.String, model.TourCode);
            this._db.AddInParameter(cmd, "@RouteName", DbType.String, model.RouteName);
            this._db.AddInParameter(cmd, "@AreaName", DbType.String, model.AreaName);
            this._db.AddInParameter(cmd, "@Title", DbType.String, model.Title);
            this._db.AddInParameter(cmd, "@Content", DbType.String, model.Content);
            this._db.AddInParameter(cmd, "@SaleId", DbType.AnsiStringFixedLength, model.SaleId);
            this._db.AddInParameter(cmd, "@SellerName", DbType.String, model.SellerName);
            this._db.AddInParameter(cmd, "@GuideId", DbType.AnsiStringFixedLength, model.GuideId);
            this._db.AddInParameter(cmd, "@GuideNm", DbType.String, model.GuideNm);
            this._db.AddInParameter(cmd, "@ChangeType", DbType.Byte, (int)model.ChangeType.Value);
            this._db.AddInParameter(cmd, "@OperatorDeptId", DbType.Int32, model.OperatorDeptId);
            this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(cmd, "@Operator", DbType.String, model.Operator);
            this._db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddOutParameter(cmd, "@Return", DbType.Int32, 1);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Return"));
        }

        /// <summary>
        /// 资源预控团号选择
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        ///  <param name="serach"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MControlTour> GetControlTourList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MControlTourSearch serach)
        {
            IList<EyouSoft.Model.TourStructure.MControlTour> list = new List<EyouSoft.Model.TourStructure.MControlTour>();
            EyouSoft.Model.TourStructure.MControlTour item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" TourId,TourCode ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' and ParentId<>'' and TourCode is not null and TourCode<>''", CompanyId);
            #endregion
            if (serach != null)
            {
                if (!string.IsNullOrEmpty(serach.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(serach.TourCode));
                }
                if (serach.LDateS.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", serach.LDateS);
                }
                if (serach.LDateE.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", serach.LDateE);
                }
                if (serach.TourType != null)
                {
                    cmdQuery.AppendFormat(" and TourType={0}", (int)serach.TourType);
                }
            }
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MControlTour()
                    {
                        TourId = rdr.GetString(rdr.GetOrdinal("TourId")),
                        TourCode = rdr["TourCode"].ToString(),
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据计划编号获得所有游客
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTourTravellerList(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourTraveller);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.TourStructure.MTourOrderTraveller()
                    {
                        TravellerId = sdr["TravellerId"].ToString(),
                        OrderId = sdr["OrderId"].ToString(),
                        CnName = sdr["CnName"].ToString(),
                        EnName = sdr["EnName"].ToString(),
                        VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)sdr.GetByte(sdr.GetOrdinal("VisitorType")),
                        CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)sdr.GetByte(sdr.GetOrdinal("CardType")),
                        CardNumber = sdr["CardNumber"].ToString(),
                        CardValidDate = sdr["CardValidDate"].ToString(),
                        VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)sdr.GetByte(sdr.GetOrdinal("VisaStatus")),
                        IsCardTransact = sdr["IsCardTransact"].ToString().Equals("1") ? true : false,
                        Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)sdr.GetByte(sdr.GetOrdinal("Gender")),
                        Contact = sdr["Contact"].ToString(),
                        LNotice = sdr["LNotice"].ToString().Equals("1") ? true : false,
                        RNotice = sdr["RNotice"].ToString().Equals("1") ? true : false,
                        Remark = sdr["Remark"].ToString(),
                        TravellerStatus = (EyouSoft.Model.EnumType.TourStructure.TravellerStatus)sdr.GetByte(sdr.GetOrdinal("Status")),
                        RAmount = sdr.GetDecimal(sdr.GetOrdinal("RAmount")),
                        RTime = !sdr.IsDBNull(sdr.GetOrdinal("RTime")) ? (DateTime?)sdr.GetDateTime(sdr.GetOrdinal("RTime")) : null,
                        RRemark = sdr["RRemark"].ToString(),
                        IsInsurance = sdr["IsInsurance"].ToString().Equals("1") ? true : false
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 派团时的订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MSendTourOrderList> GetSendTourOrderList(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MSendTourOrderList> list = new List<EyouSoft.Model.TourStructure.MSendTourOrderList>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSendTourOrderList);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.TourStructure.MSendTourOrderList()
                    {
                        OrderId = sdr["OrderId"].ToString(),
                        BuyCompanyName = sdr["BuyCompanyName"].ToString(),
                        SellerName = sdr["SellerName"].ToString(),
                        TheNum = sdr.GetInt32(sdr.GetOrdinal("TheNum"))
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="SendTour">派团给计调实体</param>
        /// <returns></returns>
        public bool SendTour(EyouSoft.Model.TourStructure.MSendTour SendTour)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_SendTour");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, SendTour.TourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, SendTour.TourCode);
            _db.AddInParameter(cmd, "InsiderInfor", DbType.String, SendTour.InsiderInfor);
            _db.AddInParameter(cmd, "TourPlaner", DbType.String, CreateMTourPlanerXml(SendTour.TourId, SendTour.Planer));
            _db.AddInParameter(cmd, "TourPlanItem", DbType.String, CreateMTourPlanItemXml(SendTour.TourId, SendTour.PlanItem));
            _db.AddInParameter(cmd, "Operator", DbType.String, SendTour.Operator);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, SendTour.CompanyId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, SendTour.OperatorId);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, SendTour.DeptId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获得计划实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaseInfo GetTourInfo(string TourId)
        {
            EyouSoft.Model.EnumType.TourStructure.TourType TourType = EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            EyouSoft.Model.TourStructure.MTourTeamInfo MTourTeamInfo = null;
            EyouSoft.Model.TourStructure.MTourSanPinInfo MTourSanPinInfo = null;
            #region 基本信息
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    switch (TourType)
                    {
                        #region 团队计划基本信息
                        //case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                        //case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                            MTourTeamInfo = new EyouSoft.Model.TourStructure.MTourTeamInfo()
                            {
                                TourId = rdr["TourId"].ToString(),
                                TourCode = rdr["TourCode"].ToString(),

                                AreaId = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AreaId")),
                                RouteName = rdr["RouteName"].ToString(),
                                //RouteId = rdr["RouteId"].ToString(),
                                CompanyId = rdr["CompanyId"].ToString(),
                                //CostCalculation = rdr["CostCalculation"].ToString(),
                                // Gather = rdr["Gather"].ToString(),
                                //IsSubmit = rdr.IsDBNull(rdr.GetOrdinal("IsSubmit")) ? false : rdr.GetString(rdr.GetOrdinal("IsSubmit")) == "1" ? true : false,
                                LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                                
                                RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                                
                                    //LTraffic = rdr["LTraffic"].ToString(),
                                //RTraffic = rdr["RTraffic"].ToString(),
                                OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString() },
                                OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)rdr.GetByte(rdr.GetOrdinal("OutQuoteType")),
                                SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString(), DeptId = rdr.IsDBNull(rdr.GetOrdinal("SellerDeptId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SellerDeptId")) },
                                TourDays = rdr.IsDBNull(rdr.GetOrdinal("TourDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                                // TourService = this.GetTourService(TourId),
                                //TourPlan = this.GetTourPlan(TourId),
                                TourTeamPrice = this.GetTourTeamPrice(TourId),
                                TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                                TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType")),
                                PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                                GuideList = GetGuidByXml(rdr["GuideList"].ToString()),
                                TourPlanItem = GetTourPlanItemByXml(rdr["TourPlanItem"].ToString()),
                                TourPlaner = GetTourPlanerByXml(rdr["TourPlanerList"].ToString()),
                                //VisaFileList = GetVisaFileByXml(rdr["VisaFile"].ToString()),
                                //PlanFeature = rdr["PlanFeature"].ToString(),
                                QuoteRemark = rdr["QuoteRemark"].ToString(),
                            };
                            GetTourOrder(MTourTeamInfo);
                            break;
                        #endregion

                        #region 散拼计划基本信息
                        //case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.散拼模版团:
                        case EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品:
                            ////2012-09-07合并散拼短线
                            //case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                            MTourSanPinInfo = new EyouSoft.Model.TourStructure.MTourSanPinInfo()
                             {
                                 TourId = rdr["TourId"].ToString(),
                                 TourCode = rdr["TourCode"].ToString(),
                                 AreaId = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AreaId")),
                                 RouteName = rdr["RouteName"].ToString(),
                                 CompanyId = rdr["CompanyId"].ToString(),
                                
                                 LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                                 RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                                 OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString() },
                                 SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() },
                                 
                                 IsShowDistribution = rdr.IsDBNull(rdr.GetOrdinal("IsShowDistribution")) ? false : rdr.GetString(rdr.GetOrdinal("IsShowDistribution")) == "1" ? true : false,
                                 
                                 PlanPeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("PlanPeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PlanPeopleNumber")),
                                 LeavePeopleNumber = rdr.IsDBNull(rdr.GetOrdinal("LeavePeopleNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("LeavePeopleNumber")),
                                 
                                 TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)rdr.GetByte(rdr.GetOrdinal("TourShouKeStatus")),
                                 
                                 TourDays = rdr.IsDBNull(rdr.GetOrdinal("TourDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                                 
                                 TourPlan = this.GetTourPlan(TourId),
                                 MTourPriceStandard = this.GetTourSanPinPrice(TourId),
                                 
                                 TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                                 TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType")),
                                 GuideList = GetGuidByXml(rdr["GuideList"].ToString()),
                                 TourPlanItem = GetTourPlanItemByXml(rdr["TourPlanItem"].ToString()),
                                 TourPlaner = GetTourPlanerByXml(rdr["TourPlanerList"].ToString()),
                                
                                 Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                                 Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                                 RouteIds = !rdr.IsDBNull(rdr.GetOrdinal("RouteIds")) ? rdr.GetString(rdr.GetOrdinal("RouteIds")) : string.Empty

                             };
                            break;
                        #endregion

                        //#region 单项服务计划基本信息
                        //case EyouSoft.Model.EnumType.TourStructure.TourType.单项服务:
                        //    MTourSingleInfo = new EyouSoft.Model.TourStructure.MTourSingleInfo()
                        //    {
                        //        TourId = rdr["TourId"].ToString(),
                        //        TourCode = rdr["TourCode"].ToString(),
                        //        AreaId = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AreaId")),
                        //        RouteName = rdr["RouteName"].ToString(),
                        //        RouteId = rdr["RouteId"].ToString(),
                        //        CompanyId = rdr["CompanyId"].ToString(),
                        //        CostCalculation = rdr["CostCalculation"].ToString(),
                        //        Gather = rdr["Gather"].ToString(),
                        //        IsSubmit = rdr["IsSubmit"].ToString() == "1" ? true : false,
                        //        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        //        RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                        //        LTraffic = rdr["LTraffic"].ToString(),
                        //        RTraffic = rdr["RTraffic"].ToString(),
                        //        OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId")), Name = rdr["Operator"].ToString() },
                        //        SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() },
                        //        TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays")),
                        //        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                        //        TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType")),
                        //        GuideList = GetGuidByXml(rdr["GuideList"].ToString()),
                        //        TourPlaner = GetTourPlanerByXml(rdr["TourPlanerList"].ToString()),
                        //        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        //        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs"))
                        //    };
                        //    break;
                        //#endregion
                    }
                }
            }
            #endregion

            switch (TourType)
            {
                #region 团队计划基本信息
                case EyouSoft.Model.EnumType.TourStructure.TourType.团队产品:
                    return MTourTeamInfo;
                #endregion

                //#region 散拼计划基本信息
                //case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                //case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.散拼模版团:
                case EyouSoft.Model.EnumType.TourStructure.TourType.散拼产品:
                    return MTourSanPinInfo;
                //#endregion

                //#region 单项服务计划基本信息
                //case EyouSoft.Model.EnumType.TourStructure.TourType.单项服务:
                //    return MTourSingleInfo;
                //#endregion
            }
            return null;
        }

        /// <summary>
        /// 获得计划基础信息实体
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaseInfo GetBasicTourInfo(string TourId)
        {
            EyouSoft.Model.TourStructure.MTourBaseInfo BaseInfo = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourBasicInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    BaseInfo = new EyouSoft.Model.TourStructure.MTourBaseInfo()
                    {
                        TourId = rdr["TourId"].ToString(),
                        TourCode = rdr["TourCode"].ToString(),

                        AreaId = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AreaId")),
                        RouteName = rdr["RouteName"].ToString(),
                        CompanyId = rdr["CompanyId"].ToString(),
                        LDate = rdr.IsDBNull(rdr.GetOrdinal("LDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("LDate")),
                        RDate = rdr.IsDBNull(rdr.GetOrdinal("RDate")) ? null : (DateTime?)rdr.GetDateTime(rdr.GetOrdinal("RDate")),
                        OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = rdr["OperatorId"].ToString(), Name = rdr["Operator"].ToString(), Phone = rdr["ContactMobile"].ToString() },

                        SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() },
                        TourDays = rdr.IsDBNull(rdr.GetOrdinal("TourDays")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TourDays")),

                        TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus")),
                        TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType")),
                        GuideList = GetGuidByXml(rdr["GuideList"].ToString()),
                        TourPlaner = GetTourPlanerByXml(rdr["TourPlanerList"].ToString()),
                    };
                }
            }
            return BaseInfo;
        }

        /// <summary>
        /// 新增团队计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns>(0:添加失败，1:添加成功,[2:垫付审请中(去掉)]，3:销售员超限，4: 客户超限，5:销售员客户均超限)</returns>
        public int AddTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Add");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, info.TourId);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, info.OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, info.OrderCode);
            _db.AddInParameter(cmd, "TourCode", DbType.String, info.TourCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, info.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, info.RouteName);
            _db.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, info.RouteId);
            _db.AddInParameter(cmd, "TourDays", DbType.Int32, info.TourDays);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, info.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, info.RDate);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, info.CompanyInfo.CompanyId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, info.CompanyInfo.CompanyName);
            _db.AddInParameter(cmd, "ContactName", DbType.String, info.CompanyInfo.Contact);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, info.CompanyInfo.Phone);
            _db.AddInParameter(cmd, "LTraffic", DbType.String, info.LTraffic);
            _db.AddInParameter(cmd, "RTraffic", DbType.String, info.RTraffic);
            _db.AddInParameter(cmd, "Gather", DbType.String, info.Gather);
            _db.AddInParameter(cmd, "SellerName", DbType.String, info.SaleInfo.Name);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SaleInfo.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, info.SaleInfo.DeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, info.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, info.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, info.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, info.ChildPrice);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)info.OutQuoteType);
            _db.AddInParameter(cmd, "OtherCost", DbType.Decimal, info.OtherCost);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, info.SumPrice);
            _db.AddInParameter(cmd, "CostCalculation", DbType.String, info.CostCalculation);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)info.TourType);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划);
            _db.AddInParameter(cmd, "IsShowDistribution", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "KeyName", DbType.String, "");
            _db.AddInParameter(cmd, "PlanPeopleNumber", DbType.Int32, info.Adults + info.Childs);
            _db.AddInParameter(cmd, "RealPeopleNumber", DbType.Int32, info.Adults + info.Childs);
            _db.AddInParameter(cmd, "LeavePeopleNumber", DbType.Int32, 0);
            _db.AddInParameter(cmd, "TourShouKeStatus", DbType.Byte, (int)EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.自动停收);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorInfo.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.OperatorInfo.Name);
            _db.AddInParameter(cmd, "SourceId", DbType.AnsiStringFixedLength, "");
            _db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "IsRecentLeave", DbType.AnsiStringFixedLength, "1");//是否最近发团
            _db.AddInParameter(cmd, "FilePath", DbType.String, "");
            _db.AddInParameter(cmd, "TourService", DbType.String, CreateTourServiceXml(info.TourId, info.TourService));
            _db.AddInParameter(cmd, "TourPlan", DbType.Xml, CreatePlanXml(info.TourId, info.TourPlan));
            _db.AddInParameter(cmd, "TourPlanSpot", DbType.Xml, CreatePlanSpotXml(info.TourPlan));
            _db.AddInParameter(cmd, "TourTeamPrice", DbType.String, CreateTourTeamPriceXml(info.TourId, info.TourTeamPrice));
            _db.AddInParameter(cmd, "Traveller", DbType.Xml, CreateTravellerXml(info.OrderId, info.Traveller));
            _db.AddInParameter(cmd, "TravellerInsur", DbType.Xml, CreateInsurXml(info.Traveller));
            _db.AddInParameter(cmd, "Supplier", DbType.Xml, null);

            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, null);
            _db.AddInParameter(cmd, "TourChildren", DbType.Xml, null);

            _db.AddInParameter(cmd, "SupplierPublishPrice", DbType.Xml, null);

            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "SaleAddCost", DbType.Decimal, info.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Decimal, info.SaleReduceCost);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Decimal, info.GuideIncome);
            _db.AddInParameter(cmd, "AddCostRemark", DbType.String, info.AddCostRemark);
            _db.AddInParameter(cmd, "ReduceCostRemark", DbType.String, info.ReduceCostRemark);
            _db.AddInParameter(cmd, "OrderRemark", DbType.String, info.OrderRemark);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, info.QuoteRemark);
            _db.AddInParameter(cmd, "AdvanceApp", DbType.Xml, CreateOverrunXml(info.AdvanceApp));
            _db.AddInParameter(cmd, "VisaFile", DbType.String, CreateVisaFileXml(info.TourId, info.VisaFileList, null));
            _db.AddInParameter(cmd, "PlanFeature", DbType.String, info.PlanFeature);
            _db.AddInParameter(cmd, "StopDays", DbType.Int32, 0);
            _db.AddInParameter(cmd, "IsCheck", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(cmd, "AddTourNum", DbType.Int32, 1);
            _db.AddInParameter(cmd, "SourceCompanyName", DbType.String, "");
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, info.ContactDepartId);
            //2012-09-07散拼短线合并
            _db.AddInParameter(cmd, "TourCarLocation", DbType.Xml, null);
            _db.AddInParameter(cmd, "TourCarType", DbType.Xml, null);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            _db.AddInParameter(cmd, "HeTongId", DbType.AnsiStringFixedLength, info.HeTongId);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 新增散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Add");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)model.LngType);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourMode", DbType.Byte, (int)model.TourMode);
            _db.AddInParameter(cmd, "TourFile", DbType.String, model.TourFile);
            _db.AddInParameter(cmd, "TourCustomerCode", DbType.String, model.TourCustomerCode);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            _db.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, model.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, model.CountryId);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, model.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, model.RDate);
            _db.AddInParameter(cmd, "ArriveCity", DbType.String, model.ArriveCity);
            _db.AddInParameter(cmd, "ArriveCityFlight", DbType.String, model.ArriveCityFlight);
            _db.AddInParameter(cmd, "LeaveCity", DbType.String, model.LeaveCity);
            _db.AddInParameter(cmd, "LeaveCityFlight", DbType.String, model.LeaveCityFlight);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, model.SellerId == null ? "" : model.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, model.SellerName == null ? "" : model.SellerName);
            _db.AddInParameter(cmd, "SellerDeptId", DbType.Int32, model.SellerDeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, model.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, model.Childs);
            _db.AddInParameter(cmd, "Leaders", DbType.Int32, model.Leaders);
            _db.AddInParameter(cmd, "sipei", DbType.Int32, model.SiPei);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.PlanFeature);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)model.OutQuoteType);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, model.QuoteRemark);
            _db.AddInParameter(cmd, "SpecificRequire", DbType.String, model.SpecificRequire);
            _db.AddInParameter(cmd, "TravelNote", DbType.String, model.TravelNote);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)model.TourType);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)model.TourStatus);
            _db.AddInParameter(cmd, "TourSureStatus", DbType.Byte, (int)model.TourSureStatus);
            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, model.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, model.GuideIncome);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, model.SumPrice);
            _db.AddInParameter(cmd, "InsideInformation", DbType.String, model.InsideInformation);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);

            _db.AddInParameter(cmd, "TourJourney", DbType.Xml, CreateTourJourneyXml(model.TourJourneyList));

            //计划计调信息
            _db.AddInParameter(cmd, "TourPlaner", DbType.Xml, CreateTourPlaner(model.TourPlanerList, model.TourId));
            //计划用房信息
            _db.AddInParameter(cmd, "TourRoom", DbType.Xml, CreateTourRoom(model.TourRoomList, model.TourId));
            //计划地接信息
            _db.AddInParameter(cmd, "TourDiJie", DbType.Xml, CreateTourDiJie(model.TourDiJieList, model.TourId));
            //计划文件
            _db.AddInParameter(cmd, "TourFiles", DbType.Xml, CreateTourFileXml(model.TourFileList, model.TourId));
            //计划行程安排
            _db.AddInParameter(cmd, "TourPlan", DbType.Xml, CreateTourPlanXml(model.TourPlanList, model.TourId));
            //计划行程城市
            _db.AddInParameter(cmd, "TourPlanCity", DbType.Xml, CreateTourPlanCityXml(model.TourPlanList));
            //计划行程购物点
            _db.AddInParameter(cmd, "TourPlanShop", DbType.Xml, CreateTourPlanShopXml(model.TourPlanList));
            //计划行程景点
            _db.AddInParameter(cmd, "TourPlanSpot", DbType.Xml, CreateTourPlanSpotXml(model.TourPlanList));
            //计划购物点
            _db.AddInParameter(cmd, "TourShop", DbType.Xml, CreateTourShopXml(model.TourShopList, model.TourId));
            //计划风味餐
            _db.AddInParameter(cmd, "TourFoot", DbType.Xml, CreateTourFootXml(model.TourFootList, model.TourId));
            //计划自费项目
            _db.AddInParameter(cmd, "TourSelfPay", DbType.Xml, CreateTourSelfPayXml(model.TourSelfPayList, model.TourId));
            //计划赠送
            _db.AddInParameter(cmd, "TourGive", DbType.Xml, CreateTourGiveXml(model.TourGiveList, model.TourId));
            //计划小费
            _db.AddInParameter(cmd, "TourTip", DbType.Xml, CreateTourTipXml(model.TourTipList, model.TourId));
            //计划价格项目
            _db.AddInParameter(cmd, "TourCost", DbType.Xml, CreateTourCostXml(model.TourCostList, model.TourId));
            //计划项目
            _db.AddInParameter(cmd, "TourPrice", DbType.Xml, CreateTourPriceXml(model.TourPriceList, model.TourId));
            //线路编号集合(以,分隔)
            if (model.RouteIds == null)
                model.RouteIds = "";
            _db.AddInParameter(cmd, "RouteIds", DbType.String, model.RouteIds);
            //散拼报价标准
            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, CreateTourSanPinPriceXml(model.MTourPriceStandard));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateTourTeam(EyouSoft.Model.TourStructure.MTourTeamInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Update");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, info.TourId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, info.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, info.RouteName);
            _db.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, info.RouteId);
            _db.AddInParameter(cmd, "TourDays", DbType.Int32, info.TourDays);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, info.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, info.RDate);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, info.CompanyInfo.CompanyId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, info.CompanyInfo.CompanyName);
            _db.AddInParameter(cmd, "ContactName", DbType.String, info.CompanyInfo.Contact);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, info.CompanyInfo.Phone);
            _db.AddInParameter(cmd, "LTraffic", DbType.String, info.LTraffic);
            _db.AddInParameter(cmd, "RTraffic", DbType.String, info.RTraffic);
            _db.AddInParameter(cmd, "Gather", DbType.String, info.Gather);
            _db.AddInParameter(cmd, "SellerName", DbType.String, info.SaleInfo.Name);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SaleInfo.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, info.SaleInfo.DeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, info.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, info.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, info.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, info.ChildPrice);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)info.OutQuoteType);
            _db.AddInParameter(cmd, "OtherCost", DbType.Decimal, info.OtherCost);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, info.SumPrice);
            _db.AddInParameter(cmd, "CostCalculation", DbType.String, info.CostCalculation);
            _db.AddInParameter(cmd, "IsShowDistribution", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "KeyName", DbType.String, "");
            _db.AddInParameter(cmd, "PlanPeopleNumber", DbType.Int32, info.Adults + info.Childs);
            //_db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorInfo.OperatorId);
            //_db.AddInParameter(cmd, "Operator", DbType.String, info.OperatorInfo.Name);
            _db.AddInParameter(cmd, "SourceId", DbType.AnsiStringFixedLength, "");

            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)info.TourType);
            _db.AddInParameter(cmd, "FilePath", DbType.String, "");
            _db.AddInParameter(cmd, "TourService", DbType.String, CreateTourServiceXml(info.TourId, info.TourService));
            _db.AddInParameter(cmd, "TourPlan", DbType.Xml, CreatePlanXml(info.TourId, info.TourPlan));
            _db.AddInParameter(cmd, "TourPlanSpot", DbType.Xml, CreatePlanSpotXml(info.TourPlan));
            _db.AddInParameter(cmd, "TourTeamPrice", DbType.String, CreateTourTeamPriceXml(info.TourId, info.TourTeamPrice));
            _db.AddInParameter(cmd, "Traveller", DbType.Xml, CreateTravellerXml(info.OrderId, info.Traveller));
            _db.AddInParameter(cmd, "TravellerInsur", DbType.Xml, CreateInsurXml(info.Traveller));
            _db.AddInParameter(cmd, "Supplier", DbType.Xml, null);
            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, null);
            _db.AddInParameter(cmd, "SupplierPublishPrice", DbType.Xml, null);

            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "SaleAddCost", DbType.Decimal, info.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Decimal, info.SaleReduceCost);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Decimal, info.GuideIncome);
            _db.AddInParameter(cmd, "AddCostRemark", DbType.String, info.AddCostRemark);
            _db.AddInParameter(cmd, "ReduceCostRemark", DbType.String, info.ReduceCostRemark);
            _db.AddInParameter(cmd, "OrderRemark", DbType.String, info.OrderRemark);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, info.QuoteRemark);
            _db.AddInParameter(cmd, "VisaFile", DbType.String, CreateVisaFileXml(info.TourId, info.VisaFileList, null));
            _db.AddInParameter(cmd, "PlanFeature", DbType.String, info.PlanFeature);
            _db.AddInParameter(cmd, "TourChangeTitle", DbType.String, info.TourChangeTitle);
            _db.AddInParameter(cmd, "TourChangeContent", DbType.String, info.TourChangeContent);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, info.ContactDepartId);
            _db.AddInParameter(cmd, "StopDays", DbType.Int32, 0);
            //2012-09-07修改散拼计划界面已改为不需要修改车型
            _db.AddInParameter(cmd, "TourCarLocation", DbType.Xml, null);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            _db.AddInParameter(cmd, "HeTongId", DbType.AnsiStringFixedLength, info.HeTongId);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTourSanPin(EyouSoft.Model.TourStructure.MTourSanPinInfo model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Update");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)model.LngType);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourMode", DbType.Byte, (int)model.TourMode);
            _db.AddInParameter(cmd, "TourFile", DbType.String, model.TourFile);
            _db.AddInParameter(cmd, "TourCustomerCode", DbType.String, model.TourCustomerCode);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            _db.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, model.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, model.CountryId);
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, model.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, model.RDate);
            _db.AddInParameter(cmd, "ArriveCity", DbType.String, model.ArriveCity);
            _db.AddInParameter(cmd, "ArriveCityFlight", DbType.String, model.ArriveCityFlight);
            _db.AddInParameter(cmd, "LeaveCity", DbType.String, model.LeaveCity);
            _db.AddInParameter(cmd, "LeaveCityFlight", DbType.String, model.LeaveCityFlight);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, model.SellerId == null ? "" : model.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, model.SellerName == null ? "" : model.SellerName);
            _db.AddInParameter(cmd, "SellerDeptId", DbType.Int32, model.SellerDeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, model.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, model.Childs);
            _db.AddInParameter(cmd, "Leaders", DbType.Int32, model.Leaders);
            _db.AddInParameter(cmd, "sipei", DbType.Int32, model.SiPei);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.PlanFeature);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)model.OutQuoteType);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, model.QuoteRemark);
            _db.AddInParameter(cmd, "SpecificRequire", DbType.String, model.SpecificRequire);
            _db.AddInParameter(cmd, "TravelNote", DbType.String, model.TravelNote);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)model.TourType);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)model.TourStatus);
            _db.AddInParameter(cmd, "TourSureStatus", DbType.Byte, (int)model.TourSureStatus);
            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, model.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, model.GuideIncome);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, model.SumPrice);
            _db.AddInParameter(cmd, "InsideInformation", DbType.String, model.InsideInformation);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);


            _db.AddInParameter(cmd, "TourJourney", DbType.Xml, CreateTourJourneyXml(model.TourJourneyList));


            //计划计调信息
            _db.AddInParameter(cmd, "TourPlaner", DbType.Xml, CreateTourPlaner(model.TourPlanerList, model.TourId));
            //计划用房信息
            _db.AddInParameter(cmd, "TourRoom", DbType.Xml, CreateTourRoom(model.TourRoomList, model.TourId));
            //计划地接信息
            _db.AddInParameter(cmd, "TourDiJie", DbType.Xml, CreateTourDiJie(model.TourDiJieList, model.TourId));
            //计划文件
            _db.AddInParameter(cmd, "TourFiles", DbType.Xml, CreateTourFileXml(model.TourFileList, model.TourId));
            //计划行程安排
            _db.AddInParameter(cmd, "TourPlan", DbType.Xml, CreateTourPlanXml(model.TourPlanList, model.TourId));
            //计划行程城市
            _db.AddInParameter(cmd, "TourPlanCity", DbType.Xml, CreateTourPlanCityXml(model.TourPlanList));
            //计划行程购物点
            _db.AddInParameter(cmd, "TourPlanShop", DbType.Xml, CreateTourPlanShopXml(model.TourPlanList));
            //计划行程景点
            _db.AddInParameter(cmd, "TourPlanSpot", DbType.Xml, CreateTourPlanSpotXml(model.TourPlanList));
            //计划购物点
            _db.AddInParameter(cmd, "TourShop", DbType.Xml, CreateTourShopXml(model.TourShopList, model.TourId));
            //计划风味餐
            _db.AddInParameter(cmd, "TourFoot", DbType.Xml, CreateTourFootXml(model.TourFootList, model.TourId));
            //计划自费项目
            _db.AddInParameter(cmd, "TourSelfPay", DbType.Xml, CreateTourSelfPayXml(model.TourSelfPayList, model.TourId));
            //计划赠送
            _db.AddInParameter(cmd, "TourGive", DbType.Xml, CreateTourGiveXml(model.TourGiveList, model.TourId));
            //计划小费
            _db.AddInParameter(cmd, "TourTip", DbType.Xml, CreateTourTipXml(model.TourTipList, model.TourId));
            //计划价格项目
            _db.AddInParameter(cmd, "TourCost", DbType.Xml, CreateTourCostXml(model.TourCostList, model.TourId));
            //计划项目
            _db.AddInParameter(cmd, "TourPrice", DbType.Xml, CreateTourPriceXml(model.TourPriceList, model.TourId));
            //变更标题
            _db.AddInParameter(cmd, "TourChangeTitle", DbType.String, model.TourChangeTitle);
            //变更内容
            _db.AddInParameter(cmd, "TourChangeContent", DbType.String, model.TourChangeContent);
            //线路编号集合(以,分隔)
            if (model.RouteIds == null)
                model.RouteIds = "";
            _db.AddInParameter(cmd, "RouteIds", DbType.String, model.RouteIds);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 获取线路区域计划数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MAreaTour> GetAreaTour(string CompanyId)
        {
            IList<EyouSoft.Model.TourStructure.MAreaTour> list = new List<EyouSoft.Model.TourStructure.MAreaTour>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetArea);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.TourStructure.MAreaTour()
                    {
                        AreaId = sdr.GetInt32(sdr.GetOrdinal("AreaId")),
                        AreaName = sdr["AreaName"].ToString(),
                        TourNum = sdr.GetInt32(sdr.GetOrdinal("TourNum"))
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 获取关键字计划数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MKeyTour> GetKeyTour(string CompanyId)
        {
            IList<EyouSoft.Model.TourStructure.MKeyTour> list = new List<EyouSoft.Model.TourStructure.MKeyTour>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourKeyInfo);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.TourStructure.MKeyTour()
                    {
                        Key = sdr["Key"].ToString(),
                        KeyId = sdr.GetInt32(sdr.GetOrdinal("KeyId")),
                        TourNum = sdr.GetInt32(sdr.GetOrdinal("TourNum"))
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 取消计划
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="CancelReson">取消原因</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="OperatorId">当前操作人编号</param>
        /// <returns></returns>
        public bool CancelTour(string TourId, string CancelReson, string CompanyId, string OperatorId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Cancel");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(cmd, "CancelReson", DbType.String, CancelReson);
            this._db.AddInParameter(cmd, "OperatorId", DbType.String, OperatorId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SuccessDelTourIds">成功删除的计划编号列表</param>
        /// <param name="TourIds">计划编号列表</param>
        /// <returns></returns>
        public void DeleteTour(string CompanyId, ref List<string> SuccessDelTourIds, string[] TourIds)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Del");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(cmd, "TourIdXml", DbType.Xml, CreateTourIdXml(TourIds));
            using (IDataReader rdr = DbHelper.RunReaderProcedure(cmd, this._db))
            {
                while (rdr.Read())
                {
                    SuccessDelTourIds.Add(rdr["TourId"].ToString());
                }
            }
        }

        /// <summary>
        /// 根据计划编号得到行程
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> GetTourPlan(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list = new List<EyouSoft.Model.TourStructure.MPlanBaseInfo>();
            EyouSoft.Model.TourStructure.MPlanBaseInfo item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourPlan);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MPlanBaseInfo()
                    {
                        Breakfast = rdr.GetString(rdr.GetOrdinal("IsBreakfast")) == "1" ? true : false,
                        Lunch = rdr.GetString(rdr.GetOrdinal("IsLunch")) == "1" ? true : false,
                        Supper = rdr.GetString(rdr.GetOrdinal("IsSupper")) == "1" ? true : false,
                        Content = rdr["Content"].ToString(),
                        Days = rdr.GetInt32(rdr.GetOrdinal("Days")),
                        Hotel = rdr["HotelName1"].ToString(),
                        PlanId = rdr["PlanId"].ToString(),
                        ItemId = TourId,
                        //Section = rdr["Section"].ToString(),
                        TourPlanSpot = GetPlanSpotByXml(rdr["Spot"].ToString()),
                        FilePath = rdr["FilePath"].ToString(),
                        HotelId = rdr["HotelId1"].ToString(),
                        Traffic = rdr["Traffic"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 供应商发布的计划审核(成功进同行分销与分销商平台)
        /// </summary>
        /// <param name="TourId">计划编号</param>
        /// <param name="ShowPublisher">供应商计划在分销商显示的发布人</param> 
        /// <param name="SaleInfo">审核人信息</param> 
        ///<param name="list">价格标准</param> 
        /// <returns></returns>
        public bool Review(string TourId, EyouSoft.Model.EnumType.TourStructure.ShowPublisher ShowPublisher, MSaleInfo SaleInfo, IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Check");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "ShowPublisher", DbType.Byte, (int)ShowPublisher);
            this._db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, SaleInfo.SellerId);
            this._db.AddInParameter(cmd, "SellerName", DbType.String, SaleInfo.Name);
            this._db.AddInParameter(cmd, "DeptId", DbType.Int32, SaleInfo.DeptId);
            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, CreateTourSanPinPriceXml(list));
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 改变计划状态
        /// </summary>
        /// <param name="TourStatusChange">状态实体</param>
        /// <returns></returns>
        public bool UpdateTourStatus(MTourStatusChange TourStatusChange)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Status");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourStatusChange.TourId);
            this._db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)TourStatusChange.TourStatus);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, TourStatusChange.CompanyId);
            this._db.AddInParameter(cmd, "Operator", DbType.String, TourStatusChange.Operator);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, TourStatusChange.OperatorId);
            this._db.AddInParameter(cmd, "DeptId", DbType.Int32, TourStatusChange.DeptId);
            this._db.AddInParameter(cmd, "TourIncome", DbType.Decimal, TourStatusChange.TourIncome);
            this._db.AddInParameter(cmd, "TourPay", DbType.Decimal, TourStatusChange.TourPay);
            this._db.AddInParameter(cmd, "TourSettlement", DbType.Decimal, TourStatusChange.TourSettlement);
            this._db.AddInParameter(cmd, "TourOtherIncome", DbType.Decimal, TourStatusChange.TourOtherIncome);
            this._db.AddInParameter(cmd, "DisOrderProfit", DbType.Decimal, TourStatusChange.DisOrderProfit);
            this._db.AddInParameter(cmd, "DisTourProfit", DbType.Decimal, TourStatusChange.DisTourProfit);
            this._db.AddInParameter(cmd, "TourProfit", DbType.Decimal, TourStatusChange.TourProfit);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);

            TourStatusChange.OutputCode = Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

            return TourStatusChange.OutputCode == 1 ? true : false;
        }

        /// <summary>
        /// 手工设置收客状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool SetHandStatus(string TourId, EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus Status)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_SetSouKeStatus);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "TourShouKeStatus", DbType.Byte, (int)Status);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获得计划服务
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourService GetTourService(string TourId)
        {
            EyouSoft.Model.TourStructure.MTourService info = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourService);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.TourStructure.MTourService()
                    {
                        ServiceStandard = rdr["ServiceStandard"].ToString(),
                        ChildServiceItem = rdr["ChildServiceItem"].ToString(),
                        InsiderInfor = rdr["InsiderInfor"].ToString(),
                        NeedAttention = rdr["NeedAttention"].ToString(),
                        NoNeedItem = rdr["NoNeedItem"].ToString(),
                        OwnExpense = rdr["OwnExpense"].ToString(),
                        ShoppingItem = rdr["ShoppingItem"].ToString(),
                        WarmRemind = rdr["WarmRemind"].ToString()
                    };
                }
            }
            return info;
        }

        /// <summary>
        /// 获取团队分项报价
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourTeamPrice> GetTourTeamPrice(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MTourTeamPrice> list = new List<EyouSoft.Model.TourStructure.MTourTeamPrice>();
            EyouSoft.Model.TourStructure.MTourTeamPrice item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourTeamPrice);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourTeamPrice()
                    {
                        Quote = rdr.GetDecimal(rdr.GetOrdinal("Quote")),
                        ServiceStandard = rdr["ServiceStandard"].ToString(),
                        ServiceType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)rdr.GetByte(rdr.GetOrdinal("ServiceType")),
                        ServiceId = rdr["ServiceId"].ToString(),
                        ServiceName = rdr["ServiceName"].ToString(),
                        TourId = TourId,
                        Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)rdr.GetByte(rdr.GetOrdinal("Unit")),
                        Remark = rdr["Remark"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得散拼价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourPriceStandard> GetTourSanPinPrice(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list = new List<EyouSoft.Model.TourStructure.MTourPriceStandard>();
            IList<EyouSoft.Model.TourStructure.MTourPriceLevel> PriceLevel = null;
            EyouSoft.Model.TourStructure.MTourPriceStandard item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSanPinPrice);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    int index = -1;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (rdr.GetInt32(rdr.GetOrdinal("Standard")) == list[i].Standard)
                        {
                            index = 1;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        item = new EyouSoft.Model.TourStructure.MTourPriceStandard()
                        {
                            Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                            Standard = rdr.GetInt32(rdr.GetOrdinal("Standard")),
                            StandardName = rdr["StandardName"].ToString(),
                            TourId = rdr.GetString(rdr.GetOrdinal("TourId"))
                        };
                        PriceLevel = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();
                        PriceLevel.Add(new EyouSoft.Model.TourStructure.MTourPriceLevel()
                        {
                            LevelId = rdr.GetInt32(rdr.GetOrdinal("LevelId")),
                            LevelName = rdr["LevelName"].ToString(),
                            AdultPrice = rdr.GetDecimal(rdr.GetOrdinal("AdultPrice")),
                            ChildPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice")),
                            // LevType = (EyouSoft.Model.EnumType.ComStructure.LevType)rdr.GetByte(rdr.GetOrdinal("LevType"))
                        });
                        item.PriceLevel = PriceLevel;
                        list.Add(item);
                    }
                    else
                    {
                        PriceLevel.Add(new EyouSoft.Model.TourStructure.MTourPriceLevel()
                        {
                            LevelId = rdr.GetInt32(rdr.GetOrdinal("LevelId")),
                            LevelName = rdr["LevelName"].ToString(),
                            AdultPrice = rdr.GetDecimal(rdr.GetOrdinal("AdultPrice")),
                            ChildPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice")),
                            // LevType = (EyouSoft.Model.EnumType.ComStructure.LevType)rdr.GetByte(rdr.GetOrdinal("LevType"))
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得供应商发布的价格
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MSupplierPublishPrice GetSupplyPrice(string TourId)
        {
            EyouSoft.Model.TourStructure.MSupplierPublishPrice item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSupplyPrice);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MSupplierPublishPrice()
                    {
                        SettleAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("AdultJSPrice")),
                        SettleChildPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildJSPrice"))
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 根据团号得到计划编号
        /// </summary>
        /// <param name="TourCode"></param>
        /// <returns></returns>
        public string GetTourId(string TourCode)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourId);
            this._db.AddInParameter(cmd, "TourCode", DbType.String, TourCode);
            return DbHelper.GetSingle(cmd, this._db).ToString();
        }

        /// <summary>
        /// 报销报账报销
        /// </summary>
        /// <param name="approverDeptId">报销完成人员部门编号</param>
        /// <param name="approverId">报销完成人员编号</param>
        /// <param name="approver">报销完成人员名</param>
        /// <param name="TourId">计划编号</param>
        /// <param name="CompanyId">系统公司编号</param>
        /// <returns></returns>
        public bool Apply(int approverDeptId, string approverId, string approver, string TourId, string CompanyId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_AutoSetApply");
            this._db.AddInParameter(cmd, "DeptId", DbType.Int32, approverDeptId);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, approverId);
            this._db.AddInParameter(cmd, "UserNm", DbType.String, approver);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }

        /// <summary>
        /// 获得计划弹出信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourBaoInfo GetTourBaoInfo(string TourId)
        {
            EyouSoft.Model.TourStructure.MTourBaoInfo item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourBaoInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourBaoInfo()
                    {
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        Operator = rdr["Operator"].ToString(),
                        TourCode = rdr["TourCode"].ToString()
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 得到派团计划订单信息
        /// </summary>
        /// <param name="info"></param>
        public void GetTourOrder(EyouSoft.Model.TourStructure.MTourTeamInfo info)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourOrder);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, info.TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    info.CompanyInfo = new MCompanyInfo()
                    {
                        CompanyId = rdr["BuyCompanyId"].ToString(),
                        CompanyName = rdr["BuyCompanyName"].ToString(),
                        Contact = rdr["ContactName"].ToString(),
                        Phone = rdr["ContactTel"].ToString()
                    };
                    info.AdultPrice = rdr.IsDBNull(rdr.GetOrdinal("AdultPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("AdultPrice"));
                    info.ChildPrice = rdr.IsDBNull(rdr.GetOrdinal("ChildPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    info.Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    info.Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    //info.OtherCost = rdr.IsDBNull(rdr.GetOrdinal("OtherCost")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("OtherCost"));
                    info.SumPrice = rdr.IsDBNull(rdr.GetOrdinal("SumPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SumPrice"));
                    info.SaleAddCost = rdr.IsDBNull(rdr.GetOrdinal("SaleAddCost")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SaleAddCost"));
                    info.SaleReduceCost = rdr.IsDBNull(rdr.GetOrdinal("SaleReduceCost")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SaleReduceCost"));
                    info.AddCostRemark = rdr["SaleAddCostRemark"].ToString();
                    info.ReduceCostRemark = rdr["SaleReduceCostRemark"].ToString();
                    info.GuideIncome = rdr.IsDBNull(rdr.GetOrdinal("GuideIncome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("GuideIncome"));
                    info.SalerIncome = rdr.IsDBNull(rdr.GetOrdinal("SalerIncome")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("SalerIncome"));
                    info.OrderRemark = rdr["OrderRemark"].ToString();
                    info.CountryId = rdr.IsDBNull(rdr.GetOrdinal("BuyCountryId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuyCountryId"));
                    info.ProvinceId = rdr.IsDBNull(rdr.GetOrdinal("BuyProvincesId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuyProvincesId"));
                    info.Traveller = GetTravellerByXml(rdr["TravellerList"].ToString());
                    info.ContactDepartId = rdr["ContactDepartId"].ToString();
                    info.HeTongCode = rdr["ContractCode"].ToString();
                    info.HeTongId = rdr["ContractId"].ToString();
                }
            }
        }

        /// <summary>
        /// 获取计划数量
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetTourNum(string CompanyId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("select dbo.fn_PadLeft(cast(count(TourId)+1 as nvarchar(50)),'0',5) from tbl_Tour where CompanyId=@CompanyId and  TourCode<>''");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.GetSingle(cmd, this._db).ToString();
        }

        /// <summary>
        /// 得到计划发布人信息
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="OperatorId"></param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MPublisherInfo GetPublisherInfo(EyouSoft.Model.EnumType.ComStructure.UserType UserType, string OperatorId)
        {
            EyouSoft.Model.TourStructure.MPublisherInfo item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetPublisher);
            this._db.AddInParameter(cmd, "UserType", DbType.Byte, UserType);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, OperatorId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MPublisherInfo()
                    {
                        CompanyName = rdr["CompanyName"].ToString(),
                        Mobile = rdr["ContactMobile"].ToString(),
                        Phone = rdr["ContactTel"].ToString(),
                        Name = rdr["ContactName"].ToString()
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus GetTourStatus(string CompanyId, string TourId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("select TourStatus from tbl_Tour where TourId=@TourId and CompanyId=@CompanyId");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            return (EyouSoft.Model.EnumType.TourStructure.TourStatus)Utils.GetInt(DbHelper.GetSingle(cmd, this._db) == null ? "0" : DbHelper.GetSingle(cmd, this._db).ToString());
        }

        /// <summary>
        /// 添加计划原始信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddOriginalTourInfo(EyouSoft.Model.TourStructure.MTourOriginalInfo model)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_INSERT_AddOriginalTourInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            this._db.AddInParameter(cmd, "TourContent", DbType.String, model.TourContent);
            return DbHelper.ExecuteSql(cmd, this._db) == 0 ? false : true;
        }

        /// <summary>
        /// 获取计划原始信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public MTourOriginalInfo GetOriginalTourInfo(string TourId, string CompanyId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetOriginalTourInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            MTourOriginalInfo item = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    item = new MTourOriginalInfo()
                    {
                        CompanyId = rdr["CompanyId"].ToString(),
                        TourId = rdr["TourId"].ToString(),
                        TourContent = rdr["TourContent"].ToString(),
                        TourType = (TourType)rdr.GetByte(rdr.GetOrdinal("TourType"))
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 获得订单利润分配订单列表
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourOrderDisInfo> GetTourOrderDisList(string TourId)
        {
            IList<EyouSoft.Model.TourStructure.MTourOrderDisInfo> list = new List<EyouSoft.Model.TourStructure.MTourOrderDisInfo>();
            EyouSoft.Model.TourStructure.MTourOrderDisInfo item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourOrderDis);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourOrderDisInfo()
                    {
                        OrderId = rdr["OrderId"].ToString(),
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        //ConfirmSettlementMoney = rdr.IsDBNull(rdr.GetOrdinal("ConfirmSettlementMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmSettlementMoney")),
                        OrderCode = rdr["OrderCode"].ToString(),
                        PersonNum = rdr.IsDBNull(rdr.GetOrdinal("PersonNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("PersonNum")),
                        SellerName = rdr["SellerName"].ToString(),
                        ConfirmMoney = rdr.IsDBNull(rdr.GetOrdinal("ConfirmMoney")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("ConfirmMoney"))

                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 得到发布人信息
        /// </summary>
        /// <param name="SourceId"></param>
        /// <param name="OperatorId"></param>
        /// <returns></returns>
        public MPersonInfo GetPersonInfo(string SourceId, string OperatorId)
        {
            string SQL_SELECT_GetPersonInfo = string.IsNullOrEmpty(SourceId) ? "select ContactTel,ContactMobile from tbl_ComUser where UserId=@Id" : "select Telephone as ContactTel,MobilePhone as ContactMobile from tbl_CrmLinkman where TypeId=@Id";
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetPersonInfo);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, string.IsNullOrEmpty(SourceId) ? OperatorId : SourceId);
            MPersonInfo item = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    item = new MPersonInfo()
                    {
                        Mobile = rdr["ContactMobile"].ToString(),
                        Phone = rdr["ContactTel"].ToString()
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 判断供应商发布的计划是否审核，审核后，供应商平台不能修改计划
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="SourceId"></param>
        /// <returns></returns>
        public bool GetSupplierTourCheckStatus(string TourId, string SourceId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("select IsCheck from tbl_Tour where SourceId=@SourceId and TourId=@TourId");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "SourceId", DbType.AnsiStringFixedLength, SourceId);
            return DbHelper.GetSingle(cmd, this._db).ToString() == "1" ? true : false;
        }

        /// <summary>
        /// 得到计划计调项成本确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool GetCostStatus(string TourId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("SELECT count(*) FROM tbl_Plan WHERE TourId=@TourId AND CostStatus='0' AND IsDelete='0'  and Type<>7 and Status=4");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            return (int)DbHelper.GetSingle(cmd, this._db) > 0 ? false : true;
        }

        /// <summary>
        /// 得到计划合同金额确认状态
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool GetConfirmMoneyStatus(string TourId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("SELECT COUNT(*) FROM tbl_TourOrder WHERE TourId=@TourId AND [OrderStatus]=4 AND ConfirmMoneyStatus='0'");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            return (int)DbHelper.GetSingle(cmd, this._db) > 0 ? false : true;
        }

        /// <summary>
        /// 根据计划编号获得计划类型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public TourType GetTourType(string TourId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("select TourType from tbl_Tour where TourId=@TourId");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return (TourType)rdr.GetByte(0);
                }
            }

            return TourType.团队产品;
        }

        /// <summary>
        /// 获取计划价格备注信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public string GetTourJiaGeBeiZhu(string tourId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetTourJiaGeBeiZhu);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr[0].ToString();
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// 添加垫付申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns>0:失败，1：成功，2：销售员或客户单位不存在欠款</returns>
        public int AddAdvanceApp(EyouSoft.Model.TourStructure.MAdvanceApp model)
        {
            System.Data.Common.DbCommand cmd = this._db.GetStoredProcCommand("proc_FinDisburseApply_Add");
            this._db.AddInParameter(cmd, "DisburseId", DbType.AnsiStringFixedLength, model.DisburseId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(cmd, "ItemId", DbType.AnsiStringFixedLength, model.ItemId);
            this._db.AddInParameter(cmd, "ItemType", DbType.Int32, (int)model.ItemType);
            this._db.AddInParameter(cmd, "ApplierId", DbType.AnsiStringFixedLength, model.ApplierId);
            this._db.AddInParameter(cmd, "Applier", DbType.String, model.Applier);
            this._db.AddInParameter(cmd, "ApplyTime", DbType.DateTime, model.ApplyTime);
            this._db.AddInParameter(cmd, "DisburseAmount", DbType.Currency, model.DisburseAmount);
            this._db.AddInParameter(cmd, "Remark", DbType.String, model.Remark);
            this._db.AddInParameter(cmd, "DeptId", DbType.AnsiStringFixedLength, model.DeptId);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }


        #endregion

        #region 私有方法
        /// <summary>
        /// 创建行程XML
        /// </summary>
        /// <param name="TourId"></param> 
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreatePlanXml(string TourId, IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list)
        {
            //<Root><TourPlan PlanId="行程编号" TourId="团队编号" Days="第几天" Section="区间" Hotel="住宿" HotelId="住宿编号" Breakfast="用餐早" Lunch="用餐中" Supper="用餐晚" Content="行程内容" FilePath="行程图片" Traffic="交通"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.PlanId = System.Guid.NewGuid().ToString();
                xmlDoc.AppendFormat("<TourPlan PlanId=\"{0}\" TourId=\"{1}\" Days=\"{2}\" Section=\"{3}\" Hotel=\"{4}\" HotelId=\"{5}\" Breakfast=\"{6}\" Lunch=\"{7}\" Supper=\"{8}\" Content=\"{9}\" FilePath=\"{10}\" Traffic=\"{11}\"/>", item.PlanId, TourId, item.Days, Utils.ReplaceXmlSpecialCharacter(item.Section), Utils.ReplaceXmlSpecialCharacter(item.Hotel), item.HotelId, item.Breakfast ? "1" : "0", item.Lunch ? "1" : "0", item.Supper ? "1" : "0", Utils.ReplaceXmlSpecialCharacter(item.Content), item.FilePath, Utils.ReplaceXmlSpecialCharacter(item.Traffic));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建行程景点XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreatePlanSpotXml(IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list)
        {
            //<Root><TourPlanSpot PlanId="行程编号" SpotID="景点编号" SpotName="景点名称"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var spots = item.TourPlanSpot;

                string PlanId = item.PlanId;
                if (spots != null)
                {
                    foreach (var spot in spots)
                    {
                        xmlDoc.AppendFormat("<TourPlanSpot PlanId=\"{0}\" SpotID=\"{1}\" SpotName=\"{2}\"/>", PlanId, spot.SpotId, Utils.ReplaceXmlSpecialCharacter(spot.SpotName));
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

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
                xmlDoc.AppendFormat("<TourTeamPrice TourId=\"{0}\" Unit=\"{1}\" Quote=\"{2}\" ServiceStandard=\"{3}\" ServiceType=\"{4}\"/>", TourId, (int)item.Unit, item.Quote, Utils.ReplaceXmlSpecialCharacter(item.ServiceStandard), (int)item.ServiceType);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建计划服务XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string CreateTourServiceXml(string TourId, EyouSoft.Model.TourStructure.MTourService item)
        {
            //<Root><TourService TourId="团队编号" NoNeedItem="不含项目" ShoppingItem="购物安排" ChildServiceItem="儿童安排" OwnExpense="自费项目" NeedAttention="注意事项" WarmRemind="温馨提醒" InsiderInfor="内部信息" ServiceStandard="服务标准" /></Root>
            if (item == null) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<TourService TourId=\"{0}\" NoNeedItem=\"{1}\" ShoppingItem=\"{2}\" ChildServiceItem=\"{3}\" OwnExpense=\"{4}\" NeedAttention=\"{5}\" WarmRemind=\"{6}\" InsiderInfor=\"{7}\" ServiceStandard=\"{8}\"/>", TourId, Utils.ReplaceXmlSpecialCharacter(item.NoNeedItem), Utils.ReplaceXmlSpecialCharacter(item.ShoppingItem), Utils.ReplaceXmlSpecialCharacter(item.ChildServiceItem), Utils.ReplaceXmlSpecialCharacter(item.OwnExpense), Utils.ReplaceXmlSpecialCharacter(item.NeedAttention), Utils.ReplaceXmlSpecialCharacter(item.WarmRemind), Utils.ReplaceXmlSpecialCharacter(item.InsiderInfor), Utils.ReplaceXmlSpecialCharacter(item.ServiceStandard));
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建散拼计划子团XML
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourDays"></param>
        /// <returns></returns>
        private string CreateTourChildrenXml(IList<EyouSoft.Model.TourStructure.MTourChildrenInfo> list, int TourDays)
        {
            //<Root><TourChildren ChildrenId="子团编号" LDate="出团日期" RDate="出团日期" TourCode="团号" IsRecentLeave="是否最近发团"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.ChildrenId = System.Guid.NewGuid().ToString();
                xmlDoc.AppendFormat("<TourChildren ChildrenId=\"{0}\" LDate=\"{1}\" RDate=\"{2}\" TourCode=\"{3}\"  IsRecentLeave=\"{4}\"/>", item.ChildrenId, item.LDate, item.LDate.AddDays(TourDays - 1), item.TourCode, item.IsRecentLeave ? "1" : "0");
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建散拼计划价格标准XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourSanPinPriceXml(IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list)
        {
            //<Root><TourPriceStandard Standard="报价标准编号" LevelId="客户等级编号" AdultPrice="成人价" ChildPrice="儿童价" LevType="客户等级类型" /></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                if (item != null)
                {
                    foreach (var items in item.PriceLevel)
                    {
                        xmlDoc.AppendFormat("<TourPriceStandard Standard=\"{0}\" LevelId=\"{1}\" AdultPrice=\"{2}\" ChildPrice=\"{3}\" LevType=\"{4}\"/>", item.Standard, items.LevelId, items.AdultPrice, items.ChildPrice, (int)items.LevType);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建供应商发布的价格XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string CreateSupplierPublishPriceXml(string TourId, EyouSoft.Model.TourStructure.MSupplierPublishPrice item)
        {
            //<Root><SupplierPublishPrice TourId="计划编号" SettleAdultPrice="结算成人价" SettleChildPrice="结算儿童价"/></Root>
            if (item == null) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<SupplierPublishPrice TourId=\"{0}\" SettleAdultPrice=\"{1}\" SettleChildPrice=\"{2}\"/>", TourId, item.SettleAdultPrice, item.SettleChildPrice);
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建单项服务,无计划供应商安排XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateSingleSupplierXml(string TourId, IList<EyouSoft.Model.TourStructure.MSingleSupplier> list)
        {
            //<Root><Supplier TourId="计划编号" ItemType="项目类型" SourceId="供应商编号" SourceName="供应商名称" GuideNotes="具体安排"　TotalCosts="结算价" Remarks="备注"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Supplier TourId=\"{0}\" ItemType=\"{1}\" SourceId=\"{2}\" SourceName=\"{3}\" GuideNotes=\"{4}\"　TotalCosts=\"{5}\" Remarks=\"{6}\"/>", TourId, item.Type, item.SourceId, Utils.ReplaceXmlSpecialCharacter(item.SourceName), item.GuideNotes, item.PlanCost, item.Remarks);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 生成计划计调人员XML
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId">计划编号</param> 
        /// <returns></returns>
        private string CreateMTourPlanerXml(string TourId, IList<EyouSoft.Model.TourStructure.MTourPlaner> list)
        {
            //<Root><TourPlaner TourId="计划编号" PlanerId="计调员ID" Planer="计调员姓名" DeptId="计调员部门编号" /></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<TourPlaner TourId=\"{0}\" PlanerId=\"{1}\" Planer=\"{2}\" DeptId=\"{3}\"/>", TourId, item.PlanerId, Utils.ReplaceXmlSpecialCharacter(item.Planer), item.DeptId);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建计划安排计调项XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateMTourPlanItemXml(string TourId, IList<EyouSoft.Model.TourStructure.MTourPlanItem> list)
        {
            //<Root><TourPlanItem TourId="计划编号" PlanType="计调项目类型"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<TourPlanItem TourId=\"{0}\" PlanType=\"{1}\" />", TourId, (int)item.PlanType);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建游客XML
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTravellerXml(string OrderId, IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list)
        {
            //<Root><Traveller TravellerId="游客编号" OrderId="订单编号" CnName="游客中文姓名" EnName="游客英文姓名" VisitorType="游客类型" CardType="证件类型" CardNumber="证件号码" CardValidDate="证件有效期" VisaStatus="签证状态" IsCardTransact="证件是否已办理" Gender="性别" Contact="联系方式" LNotice="出团通知" RNotice="回团通知" Remark="备注" Status="游客状态" CareId="身份证号" LiCheng="里程积分"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.TravellerId = System.Guid.NewGuid().ToString();
                if (item.OrderTravellerInsuranceList != null && item.OrderTravellerInsuranceList.Count > 0)
                {
                    item.IsInsurance = true;
                }
                xmlDoc.AppendFormat("<Traveller TravellerId=\"{0}\" OrderId=\"{1}\" CnName=\"{2}\" EnName=\"{3}\" VisitorType=\"{4}\" CardType=\"{5}\" CardNumber=\"{6}\" CardValidDate=\"{7}\" VisaStatus=\"{8}\" IsCardTransact=\"{9}\" Gender=\"{10}\" Contact=\"{11}\" LNotice=\"{12}\" RNotice=\"{13}\" Remark=\"{14}\" Status=\"{15}\" CardId=\"{16}\" IsInsurance=\"{17}\" LiCheng=\"{18}\"/>", item.TravellerId, OrderId, Utils.ReplaceXmlSpecialCharacter(item.CnName), Utils.ReplaceXmlSpecialCharacter(item.EnName), (int)item.VisitorType, (int)item.CardType, item.CardNumber, item.CardValidDate, (int?)(item.VisaStatus.HasValue ? item.VisaStatus : null), item.IsCardTransact ? "1" : "0", (int)item.Gender, Utils.ReplaceXmlSpecialCharacter(item.Contact), item.LNotice ? "1" : "0", item.RNotice ? "1" : "0", Utils.ReplaceXmlSpecialCharacter(item.Remark), (int)EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团, string.IsNullOrEmpty(item.CardId) ? "" : item.CardId, item.IsInsurance ? "1" : "0",Utils.ReplaceXmlSpecialCharacter(item.LiCheng));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建游客保险XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateInsurXml(IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list)
        {
            //<Root><TravellerInsur TravellerId="游客编号" InsuranceId="保险编号" BuyNum="购买份数" UnitPrice="单价" SumPrice="合计金额"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                if (item.OrderTravellerInsuranceList != null)
                {
                    foreach (var items in item.OrderTravellerInsuranceList)
                    {
                        xmlDoc.AppendFormat("<TravellerInsur TravellerId=\"{0}\" InsuranceId=\"{1}\" BuyNum=\"{2}\" UnitPrice=\"{3}\" SumPrice=\"{4}\"/>", item.TravellerId, items.InsuranceId, items.BuyNum, items.UnitPrice, items.SumPrice);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 创建签证文件XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <param name="TourChildrenInfo"></param>
        /// <returns></returns>
        private string CreateVisaFileXml(string TourId, IList<EyouSoft.Model.ComStructure.MComAttach> list, IList<MTourChildrenInfo> TourChildrenInfo)
        {
            //<Root><VisaFile ItemType="关联类型" ItemId="关联编号" Name="附件名称" FilePath="附件路径" Size="附件大小" Downloads="下载次数"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<VisaFile ItemType=\"{0}\" ItemId=\"{1}\" Name=\"{2}\" FilePath=\"{3}\" Size=\"{4}\" Downloads=\"{5}\"/>", (int)EyouSoft.Model.EnumType.ComStructure.AttachItemType.计划签证资料, TourId, item.Name, item.FilePath, item.Size, item.Downloads);

            }
            if (TourChildrenInfo != null && TourChildrenInfo.Count > 0)
            {
                foreach (var childTour in TourChildrenInfo)
                {
                    foreach (var item in list)
                    {
                        xmlDoc.AppendFormat("<VisaFile ItemType=\"{0}\" ItemId=\"{1}\" Name=\"{2}\" FilePath=\"{3}\" Size=\"{4}\" Downloads=\"{5}\"/>", (int)EyouSoft.Model.EnumType.ComStructure.AttachItemType.计划签证资料, childTour.ChildrenId, item.Name, item.FilePath, item.Size, item.Downloads);

                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 创建垫付申请XML
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string CreateOverrunXml(EyouSoft.Model.TourStructure.MAdvanceApp info)
        {
            if (info == null) return null;
            //<Root><AdvanceApp ApplierId="申请人编号" Applier="申请人" ApplyTime="申请时间" DisburseAmount="垫付金额" DeptId="操作人部门编号" Remark="备注"/></Root>
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<AdvanceApp ApplierId=\"{0}\" Applier=\"{1}\" ApplyTime=\"{2}\" DisburseAmount=\"{3}\" DeptId=\"{4}\" Remark=\"{5}\" />", info.ApplierId, info.Applier, info.ApplyTime, info.DisburseAmount, info.DeptId, info.Remark);
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建计划编号XML
        /// </summary>
        /// <param name="TourIds"></param>
        /// <returns></returns>
        private string CreateTourIdXml(string[] TourIds)
        {
            if (TourIds == null || TourIds.Length == 0) return null;
            //<Root><item TourId="计划编号"/></Root>
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var TourId in TourIds)
            {
                xmlDoc.AppendFormat("<item TourId=\"{0}\" />", TourId);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        ///// <summary>
        ///// 获得计划项目安排落实实体
        ///// </summary>
        ///// <param name="xml"></param>
        ///// <returns></returns>
        //private EyouSoft.Model.HTourStructure.MTourPlanStatus GetTourPlanStatus(string xml)
        //{
        //    if (string.IsNullOrEmpty(xml)) return null;
        //    EyouSoft.Model.HTourStructure.MTourPlanStatus item = new EyouSoft.Model.HTourStructure.MTourPlanStatus();
        //    System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
        //    var xRows = Utils.GetXElements(xRoot, "row");
        //    foreach (var xRow in xRows)
        //    {
        //        item.TourId = Utils.GetXAttributeValue(xRow, "TourId");
        //        item.Car = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Car")));
        //        item.Dining = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Dining")));
        //        item.DJ = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "DJ")));
        //        item.Guide = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Guide")));
        //        item.Hotel = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Hotel")));
        //        item.LL = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "LL")));
        //        item.CarTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CarTicket")));
        //        item.TrainTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "TrainTicket")));
        //        item.PlaneTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlaneTicket")));
        //        item.Other = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Other")));
        //        item.CShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CShip")));
        //        item.FShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "FShip")));
        //        item.Shopping = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Shopping")));
        //        item.Spot = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Spot")));
        //    }
        //    return item;
        //}

        /// <summary>
        /// 得到行程景点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlanSpot> GetPlanSpotByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourPlanSpot> list = new List<EyouSoft.Model.TourStructure.MTourPlanSpot>();
            EyouSoft.Model.TourStructure.MTourPlanSpot item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourPlanSpot()
                {
                    PlanId = Utils.GetXAttributeValue(xRow, "PlanId"),
                    SpotId = Utils.GetXAttributeValue(xRow, "SpotID"),
                    SpotName = Utils.GetXAttributeValue(xRow, "SpotName")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到无计划，单项业务供应商安排
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MSingleSupplier> GetSingleSupplierByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MSingleSupplier> list = new List<EyouSoft.Model.TourStructure.MSingleSupplier>();
            EyouSoft.Model.TourStructure.MSingleSupplier item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MSingleSupplier()
                {
                    GuideNotes = Utils.GetXAttributeValue(xRow, "GuideNotes"),
                    PlanCost = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PlanCost")),
                    SourceId = Utils.GetXAttributeValue(xRow, "SourceId"),
                    SourceName = Utils.GetXAttributeValue(xRow, "SourceName"),
                    Type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Type")),
                    Remarks = Utils.GetXAttributeValue(xRow, "Remarks")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到游客
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravellerByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            EyouSoft.Model.TourStructure.MTourOrderTraveller item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourOrderTraveller()
                {
                    CardNumber = Utils.GetXAttributeValue(xRow, "CardNumber"),
                    CardId = Utils.GetXAttributeValue(xRow, "CardId"),
                    CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "CardType")),
                    CardValidDate = Utils.GetXAttributeValue(xRow, "CardValidDate"),
                    CnName = Utils.GetXAttributeValue(xRow, "CnName"),
                    Contact = Utils.GetXAttributeValue(xRow, "Contact"),
                    EnName = Utils.GetXAttributeValue(xRow, "EnName"),
                    Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Gender")),
                    IsCardTransact = Utils.GetXAttributeValue(xRow, "IsCardTransact") == "1" ? true : false,
                    IsInsurance = Utils.GetXAttributeValue(xRow, "IsInsurance") == "1" ? true : false,
                    LNotice = Utils.GetXAttributeValue(xRow, "LNotice") == "1" ? true : false,
                    OrderId = Utils.GetXAttributeValue(xRow, "OrderId"),
                    RAmount = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "RAmount")),
                    RNotice = Utils.GetXAttributeValue(xRow, "RNotice") == "1" ? true : false,
                    Remark = Utils.GetXAttributeValue(xRow, "Remark"),
                    TravellerId = Utils.GetXAttributeValue(xRow, "TravellerId"),
                    TravellerStatus = (EyouSoft.Model.EnumType.TourStructure.TravellerStatus)Utils.GetInt(Utils.GetXAttributeValue(xRow, "TravellerStatus")),
                    VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)Utils.GetInt(Utils.GetXAttributeValue(xRow, "VisaStatus")),
                    VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "VisitorType")),
                    OrderTravellerInsuranceList = GetInsuranceByXml(Utils.GetXAttributeValue(xRow, "InsuranceList"))
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到游客保险信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> GetInsuranceByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> list = new List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>();
            EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance()
                {
                    InsuranceId = Utils.GetXAttributeValue(xRow, "InsuranceId"),
                    SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SumPrice")),
                    UnitPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "UnitPrice")),
                    TravellerId = Utils.GetXAttributeValue(xRow, "TravellerId"),
                    BuyNum = Utils.GetInt(Utils.GetXAttributeValue(xRow, "BuyNum"))
                };
                list.Add(item);
            }
            return list;
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
                    PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId"),
                    Phone = Utils.GetXAttributeValue(xRow, "ContactMobile")
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
                    GuidId = Utils.GetXAttributeValue(xRow, "GuideUserId"),
                    Name = Utils.GetXAttributeValue(xRow, "SourceName"),
                    Phone = Utils.GetXAttributeValue(xRow, "ContactPhone")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 根据ＸＭＬ获到客户单位
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MCompanyInfo> GetCompanyInfoByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MCompanyInfo> list = new List<EyouSoft.Model.TourStructure.MCompanyInfo>();
            EyouSoft.Model.TourStructure.MCompanyInfo item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MCompanyInfo()
                {
                    CompanyId = Utils.GetXAttributeValue(xRow, "BuyCompanyId"),
                    CompanyName = Utils.GetXAttributeValue(xRow, "BuyCompanyName"),
                    Contact = Utils.GetXAttributeValue(xRow, "ContactName"),
                    Phone = Utils.GetXAttributeValue(xRow, "ContactTel")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 根据ＸＭＬ获到计划需安排的计调项
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlanItem> GetTourPlanItemByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourPlanItem> list = new List<EyouSoft.Model.TourStructure.MTourPlanItem>();
            EyouSoft.Model.TourStructure.MTourPlanItem item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourPlanItem()
                {
                    PlanType = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlanType"))
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到签证文件实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.ComStructure.MComAttach> list = new List<EyouSoft.Model.ComStructure.MComAttach>();
            EyouSoft.Model.ComStructure.MComAttach item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.ComStructure.MComAttach()
                {
                    Downloads = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Downloads")),
                    FilePath = Utils.GetXAttributeValue(xRow, "FilePath"),
                    ItemId = Utils.GetXAttributeValue(xRow, "ItemId"),
                    ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "ItemType")),
                    Name = Utils.GetXAttributeValue(xRow, "Name"),
                    Size = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Size"))
                };
                list.Add(item);
            }
            return list;
        }
        #endregion


        #region --2012-08-20 短线功能添加的方法-----------------------------------------

        /// <summary>
        /// 获取计划预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<MTourCarType> GetTourCarType(string TourId)
        {
            IList<MTourCarType> list = null;
            MTourCarType item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourCarType);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (null != dr)
                {
                    list = new List<MTourCarType>();
                    while (dr.Read())
                    {
                        item = new MTourCarType()
                        {

                            TourCarTypeId = dr.GetString(dr.GetOrdinal("TourCarTypeId")),
                            CarTypeId = dr.GetString(dr.GetOrdinal("CarTypeId")),
                            CarTypeName = !dr.IsDBNull(dr.GetOrdinal("CarTypeName")) ? dr.GetString(dr.GetOrdinal("CarTypeName")) : null,
                            Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                            SeatNum = dr.GetInt32(dr.GetOrdinal("SeatNum"))
                        };
                        list.Add(item);
                    }
                }
            }

            return list;

        }

        /// <summary>
        /// 获取计划上车地点
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<MTourCarLocation> GetTourCarLocation(string TourId)
        {
            IList<MTourCarLocation> list = null;
            MTourCarLocation item = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourCarLocation);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (null != dr)
                {
                    list = new List<MTourCarLocation>();
                    while (dr.Read())
                    {
                        item = new MTourCarLocation()
                        {
                            TourLocationId = dr.GetString(dr.GetOrdinal("TourLocationId")),
                            CarLocationId = dr.GetString(dr.GetOrdinal("CarLocationId")),
                            Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                            Location = !dr.IsDBNull(dr.GetOrdinal("Location")) ? dr.GetString(dr.GetOrdinal("Location")) : null,
                            OffPrice = dr.GetDecimal(dr.GetOrdinal("OffPrice")),
                            OnPrice = dr.GetDecimal(dr.GetOrdinal("OnPrice")),
                            isTourOrderExists = dr["isTourOrderExists"].ToString() == "1" ? true : false
                        };
                        list.Add(item);
                    }

                }
            }

            return list;
        }



        /// <summary>
        /// 修改散拼短线的预设车型
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="Operator"></param>
        /// <param name="OperatorId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdateTourCarType(string TourId, string Operator, string OperatorId, IList<EyouSoft.Model.TourStructure.MTourCarType> list)
        {
            string TourCarType = null;
            if (list != null && list.Count != 0)
            {
                TourCarType = CreateTourCarTypeXML(TourId, list);
            }
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Update_CarType");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "Operator", DbType.String, Operator);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, OperatorId);
            this._db.AddInParameter(cmd, "TourCarType", DbType.Xml, TourCarType);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;

        }

        /// <summary>
        /// 获取分销商订单座次变更的提示消息
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="SourceId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<MCarTypeSeatChange> GetCarTypeSeatChangeList(string CompanyId, string SourceId, int top, CarChangeType? CarChangeType)
        {
            IList<MCarTypeSeatChange> list = null;
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" select top {0} *,(select RouteName from tbl_Tour where TourId=tbl_CarTypeSeatChange.TourId)as RouteName ", top);
            query.Append(" from tbl_CarTypeSeatChange ");
            query.AppendFormat(" where IsRead='0' and  CompanyId='{0}'  and SourceId='{1}'", CompanyId, SourceId);
            if (CarChangeType.HasValue)
            {
                query.AppendFormat(" and CarChangeType='{0}'", (int)CarChangeType.Value);
            }
            query.Append(" order by IsRead asc,IssueTime desc");
            DbCommand cmd = this._db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr != null)
                {
                    list = new List<MCarTypeSeatChange>();
                    while (dr.Read())
                    {
                        MCarTypeSeatChange model = new MCarTypeSeatChange()
                        {
                            Id = dr.GetString(dr.GetOrdinal("Id")),
                            CompanyId = dr.GetString(dr.GetOrdinal("CompanyId")),
                            TourId = dr.GetString(dr.GetOrdinal("TourId")),
                            OrderId = dr.GetString(dr.GetOrdinal("OrderId")),
                            IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime")),
                            CarChangeType = (CarChangeType)dr.GetByte(dr.GetOrdinal("CarChangeType")),
                            SourceId = dr.GetString(dr.GetOrdinal("SourceId")),
                            IsRead = dr.GetString(dr.GetOrdinal("IsRead")) == "1" ? true : false,
                            RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : null,
                            ChangeContent = !dr.IsDBNull(dr.GetOrdinal("ChangeContent")) ? dr.GetString(dr.GetOrdinal("ChangeContent")) : null
                        };
                        list.Add(model);
                    }
                }
            }
            return list;

        }

        /// <summary>
        /// 将车型、座次变更的信息变为已读状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsReadCarTypeSeatChange(string Id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Update_CarTypeSeatChange");
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, Id);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }


        #region  私有方法 xml到实体集合的相互转化
        /// <summary>
        /// 创建短线上车地点的XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourCarLocationXML(string TourId, IList<MTourCarLocation> list)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {

                xmlDoc.AppendFormat("<TourCarLocation TourLocationId=\"{0}\" TourId=\"{1}\" CarLocationId=\"{2}\" Location=\"{3}\" OffPrice=\"{4}\"  OnPrice=\"{5}\" Desc=\"{6}\" />", item.TourLocationId, TourId, item.CarLocationId, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(item.Location), item.OffPrice, item.OnPrice, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(item.Desc));

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建短线车型的XML
        /// </summary>
        /// <param name="TourId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourCarTypeXML(string TourId, IList<MTourCarType> list)
        {

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {

                xmlDoc.AppendFormat("<TourCarType TourCarTypeId=\"{0}\" TourId=\"{1}\" CarTypeId=\"{2}\"  CarTypeName=\"{3}\"  SeatNum=\"{4}\"  Desc=\"{5}\" />", item.TourCarTypeId, TourId, item.CarTypeId, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(item.CarTypeName), item.SeatNum, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(item.Desc));

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 得到计划的上车地点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourCarLocation> GetCarLocationByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MTourCarLocation> list = new List<MTourCarLocation>();
            MTourCarLocation item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MTourCarLocation()
                {
                    TourLocationId = Utils.GetXAttributeValue(xRow, "TourLocationId"),
                    CarLocationId = Utils.GetXAttributeValue(xRow, "CarLocationId"),
                    Location = Utils.GetXAttributeValue(xRow, "Location"),
                    OnPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "OnPrice")),
                    OffPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "OffPrice")),
                    Desc = Utils.GetXAttributeValue(xRow, "Desc")
                };
                list.Add(item);
            }

            return list;

        }

        /// <summary>
        /// 得到计划的预设车型
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourCarType> GetCarTypeByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MTourCarType> list = new List<MTourCarType>();
            MTourCarType item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MTourCarType()
                {
                    TourCarTypeId = Utils.GetXAttributeValue(xRow, "TourCarTypeId"),
                    CarTypeId = Utils.GetXAttributeValue(xRow, "CarTypeId"),
                    CarTypeName = Utils.GetXAttributeValue(xRow, "CarTypeName"),
                    SeatNum = Utils.GetInt(Utils.GetXAttributeValue(xRow, "SeatNum")),
                    Desc = Utils.GetXAttributeValue(xRow, "Desc")

                };
                list.Add(item);
            }

            return list;
        }
        #endregion

        #endregion

        /// <summary>
        /// 统计分析-状态查询表：自行设定计划状态，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SetTourStatus(MSetTourStatusInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_SetTourStatus");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, info.TourId);
            _db.AddInParameter(cmd, "Status", DbType.Byte, info.Status);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }


        #region private IList to xml

        /// <summary>
        /// 计划计调员信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPlaner(IList<EyouSoft.Model.TourStructure.MTourPlaner> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var planer in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" PlanerId=\"{1}\" Planer=\"{2}\" PlanerDeptId=\"{3}\" />",
                    TourId,
                    planer.PlanerId,
                    Utils.ReplaceXmlSpecialCharacter(planer.Planer),
                    planer.DeptId);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划用房数
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourRoom(IList<EyouSoft.Model.HTourStructure.MTourRoom> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<Root>");

            foreach (var room in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" RoomId=\"{1}\" Num=\"{2}\" />",
                    TourId,
                    room.RoomId,
                    room.Num);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划地接信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourDiJie(IList<EyouSoft.Model.HTourStructure.MTourDiJie> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var dijie in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" CityId=\"{1}\"  DiJieName=\"{2}\" DiJieId=\"{3}\" Contact=\"{4}\" Phone=\"{5}\" Remark=\"{6}\"  CityName=\"{7}\" />",
                                    TourId,
                                    dijie.CityId,
                                    Utils.ReplaceXmlSpecialCharacter(dijie.DiJieName),
                                    dijie.DiJieId,
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Contact),
                                    dijie.Phone,
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Remark),
                                    dijie.CityName);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        ///计划文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourFileXml(IList<EyouSoft.Model.HTourStructure.MTourFile> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" FileName=\"{1}\" FilePath=\"{2}\" />",
                    TourId,
                    Utils.ReplaceXmlSpecialCharacter(item.FileName),
                    item.FilePath
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 创建报价行程安排的xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPlanXml(IList<EyouSoft.Model.HTourStructure.MTourPlan> list, string TourId)
        {

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.PlanId = System.Guid.NewGuid().ToString();
                xmlDoc.Append("<Item ");
                xmlDoc.AppendFormat("PlanId=\"{0}\" ", item.PlanId);
                xmlDoc.AppendFormat("TourId=\"{0}\" ", TourId);
                xmlDoc.AppendFormat("Traffic=\"{0}\" ", (int)item.Traffic);
                xmlDoc.AppendFormat("TrafficPrice=\"{0}\" ", item.TrafficPrice);
                xmlDoc.AppendFormat("HotelId1=\"{0}\" ", item.HotelId1);
                xmlDoc.AppendFormat("HotelName1=\"{0}\" ", item.HotelName1);
                xmlDoc.AppendFormat("HotelPrice1=\"{0}\" ", item.HotelPrice1);
                xmlDoc.AppendFormat("IsBreakfast=\"{0}\" ", item.IsBreakfast ? 1 : 0);
                xmlDoc.AppendFormat("BreakfastPrice=\"{0}\" ", item.BreakfastPrice);
                xmlDoc.AppendFormat("BreakfastSettlementPrice=\"{0}\" ", item.BreakfastSettlementPrice);
                xmlDoc.AppendFormat("BreakfastRestaurantId=\"{0}\" ", item.BreakfastRestaurantId);
                xmlDoc.AppendFormat("BreakfastMenuId=\"{0}\" ", item.BreakfastMenuId);
                xmlDoc.AppendFormat("BreakfastMenu=\"{0}\" ", item.BreakfastMenu);
                xmlDoc.AppendFormat("IsLunch=\"{0}\" ", item.IsLunch ? 1 : 0);
                xmlDoc.AppendFormat("LunchPrice=\"{0}\" ", item.LunchPrice);
                xmlDoc.AppendFormat("LunchSettlementPrice=\"{0}\" ", item.LunchSettlementPrice);
                xmlDoc.AppendFormat("LunchRestaurantId=\"{0}\" ", item.LunchRestaurantId);
                xmlDoc.AppendFormat("LunchMenuId=\"{0}\" ", item.LunchMenuId);
                xmlDoc.AppendFormat("LunchMenu=\"{0}\" ", item.LunchMenu);
                xmlDoc.AppendFormat("IsSupper=\"{0}\" ", item.IsSupper ? 1 : 0);
                xmlDoc.AppendFormat("SupperPrice=\"{0}\" ", item.SupperPrice);
                xmlDoc.AppendFormat("SupperSettlementPrice=\"{0}\" ", item.SupperSettlementPrice);
                xmlDoc.AppendFormat("SupperRestaurantId=\"{0}\" ", item.SupperRestaurantId);
                xmlDoc.AppendFormat("SupperMenuId=\"{0}\" ", item.SupperMenuId);
                xmlDoc.AppendFormat("SupperMenu=\"{0}\" ", item.SupperMenu);
                xmlDoc.AppendFormat("Days=\"{0}\" ", item.Days);
                xmlDoc.AppendFormat("Content=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Content));
                xmlDoc.AppendFormat("FilePath=\"{0}\" ", item.FilePath);

                xmlDoc.AppendFormat("BreakfastId=\"{0}\" ", item.BreakfastId);
                xmlDoc.AppendFormat("LunchId=\"{0}\" ", item.LunchId);
                xmlDoc.AppendFormat("SupperId=\"{0}\" ", item.SupperId);

                xmlDoc.Append(" />");

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划行程城市xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourPlanCityXml(IList<EyouSoft.Model.HTourStructure.MTourPlan> list)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var citys = item.TourPlanCityList;
                if (citys != null)
                {
                    string PlanId = item.PlanId;

                    foreach (var city in citys)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" CityId=\"{1}\" CityName=\"{2}\" JiaoTong=\"{3}\" JiaoTongJiaGe=\"{4}\" />", PlanId, city.CityId, Utils.ReplaceXmlSpecialCharacter(city.CityName), (int)city.JiaoTong, city.JiaoTongJiaGe);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划行程购物点xml
        /// </summary>
        /// <returns></returns>
        private string CreateTourPlanShopXml(IList<EyouSoft.Model.HTourStructure.MTourPlan> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var item in list)
            {
                var shops = item.TourPlanShopList;
                if (shops != null)
                {
                    string PlanId = item.PlanId;

                    foreach (var shop in shops)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" ShopId=\"{1}\" ShopName=\"{2}\" LiuShui=\"{3}\" PeopleMoney=\"{4}\" ChildMoney=\"{5}\" />",
                            PlanId,
                            shop.ShopId,
                            Utils.ReplaceXmlSpecialCharacter(shop.ShopName),
                            shop.LiuShui,
                            shop.PeopleMoney,
                            shop.ChildMoney
                            );
                    }
                }

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划行程景点xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourPlanSpotXml(IList<EyouSoft.Model.HTourStructure.MTourPlan> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var spots = item.TourPlanSpotList;
                if (spots != null)
                {
                    string PlanId = item.PlanId;
                    foreach (var spot in spots)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" SpotId=\"{1}\" SpotName=\"{2}\" Price=\"{3}\" SettlementPrice=\"{4}\" PriceId=\"{5}\"  />",
                            PlanId,
                            spot.SpotId,
                            Utils.ReplaceXmlSpecialCharacter(spot.SpotName),
                            spot.Price,
                            spot.SettlementPrice,
                            spot.PriceId);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划购物点xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourShopXml(IList<EyouSoft.Model.HTourStructure.MTourShop> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" ShopId=\"{1}\" ShopName=\"{2}\" LiuShui=\"{3}\" PeopleMoney=\"{4}\" ChildMoney=\"{5}\" />",
                    TourId,
                    item.ShopId,
                    Utils.ReplaceXmlSpecialCharacter(item.ShopName),
                    item.LiuShui,
                    item.PeopleMoney,
                    item.ChildMoney
                );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 计划风味餐xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourFootXml(IList<EyouSoft.Model.HTourStructure.MTourFoot> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" RestaurantId=\"{1}\" MenuId=\"{2}\" Menu=\"{3}\" Price=\"{4}\" Remark=\"{5}\"  SettlementPrice=\"{6}\" FootId=\"{7}\" />",
                    TourId,
                    item.RestaurantId,
                    item.MenuId,
                    Utils.ReplaceXmlSpecialCharacter(item.Menu),
                    item.Price,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    item.SettlementPrice,
                    item.FootId
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划自费项目xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourSelfPayXml(IList<EyouSoft.Model.HTourStructure.MTourSelfPay> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" CityId=\"{1}\" CityName=\"{2}\" ScenicSpotId=\"{3}\" ScenicSpotName=\"{4}\" Price=\"{5}\" Cost=\"{6}\" Remark=\"{7}\" SettlementPrice=\"{8}\" PriceId=\"{9}\" />",
                    TourId,
                    item.CityId,
                    item.CityName,
                    item.ScenicSpotId,
                    item.ScenicSpotName,
                    item.Price,
                    item.Cost,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    item.SettlementPrice,
                    item.PriceId
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 计划赠送xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourGiveXml(IList<EyouSoft.Model.HTourStructure.MTourGive> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" ItemId=\"{1}\" Item=\"{2}\" Price=\"{3}\" Remark=\"{4}\" />",
                    TourId,
                    item.ItemId,
                    item.Item,
                    item.Price,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark)
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划小费xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourTipXml(IList<EyouSoft.Model.HTourStructure.MTourTip> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item Item=\"{0}\" Tip=\"{1}\" Price=\"{2}\" Days=\"{3}\" SumPrice=\"{4}\" Remark=\"{5}\" />",
                    TourId,
                    item.Tip,
                    item.Price,
                    item.Days,
                    item.SumPrice,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark)
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 团队计划报价单价明细
        /// </summary>
        private string CreateTourCostXml(IList<EyouSoft.Model.HTourStructure.MTourCost> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" Pricetype=\"{1}\" Price=\"{2}\" PriceUnit=\"{3}\" Remark=\"{4}\" CostMode=\"{5}\" />",
                    TourId,
                    (int)item.Pricetype,
                    item.Price,
                    (int)item.PriceUnit,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    (int)item.CostMode
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划价格
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPriceXml(IList<EyouSoft.Model.HTourStructure.MTourPrice> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" AdultPrice=\"{1}\" ChildPrice=\"{2}\" LeadPrice=\"{3}\" SingleRoomPrice=\"{4}\" OtherPrice=\"{5}\" CostMode=\"{6}\" TotalPrice=\"{7}\" />",
                    TourId,
                    item.AdultPrice,
                    item.ChildPrice,
                    item.LeadPrice,
                    item.SingleRoomPrice,
                    item.OtherPrice,
                    (int)item.CostMode,
                    item.TotalPrice
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划计调员的xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPlanerXml(IList<EyouSoft.Model.HTourStructure.MTourPlaner> list, string TourId)
        {

            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var planer in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" PlanerId=\"{1}\" Planer=\"{2}\" PlanerDeptId=\"{3}\" />", TourId, planer.PlanerId, Utils.ReplaceXmlSpecialCharacter(planer.Planer), planer.PlanerDeptId);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 计划安排的计调项目的xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPlanItemXml(IList<EyouSoft.Model.HTourStructure.MTourPlanItem> list, string TourId)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" PlanType=\"{1}\" />", TourId, (int)item.PlanType);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourJourneyXml(IList<EyouSoft.Model.HTourStructure.MTourJourney> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item SourceId=\"{0}\" JourneyType=\"{1}\" />",
                   item.SourceId,
                  (int)item.JourneyType);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        #endregion

        #region private xml to Ilist
        /// <summary>
        /// 计划计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlaner> GetTourPlanerList(string xml)
        {

            IList<EyouSoft.Model.TourStructure.MTourPlaner> list = new List<EyouSoft.Model.TourStructure.MTourPlaner>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.TourStructure.MTourPlaner model = new EyouSoft.Model.TourStructure.MTourPlaner();
                model.PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId");
                model.Planer = Utils.GetXAttributeValue(xRow, "Planer");
                model.DeptId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlanerDeptId"));
                list.Add(model);
            }

            return list;

        }

        /// <summary>
        /// 计划用房数
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourRoom> GetTourRoomList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourRoom> list = new List<EyouSoft.Model.HTourStructure.MTourRoom>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourRoom model = new EyouSoft.Model.HTourStructure.MTourRoom();
                model.RoomId = Utils.GetXAttributeValue(xRow, "RoomId");
                model.Num = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Num"));
                model.TypeName = Utils.GetXAttributeValue(xRow, "RoomName");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划用房房型列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourRoom> GetTypeRoomList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourRoom> list = new List<EyouSoft.Model.HTourStructure.MTourRoom>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourRoom model = new EyouSoft.Model.HTourStructure.MTourRoom();
                model.RoomId = Utils.GetXAttributeValue(xRow, "RoomId");
                model.TypeName = Utils.GetXAttributeValue(xRow, "TypeName");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划地接
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourDiJie> GetTourDiJieList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourDiJie> list = new List<EyouSoft.Model.HTourStructure.MTourDiJie>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourDiJie model = new EyouSoft.Model.HTourStructure.MTourDiJie();
                model.CityId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CityId"));
                model.CityName = Utils.GetXAttributeValue(xRow, "CityName");
                model.DiJieName = Utils.GetXAttributeValue(xRow, "DiJieName");
                model.DiJieId = Utils.GetXAttributeValue(xRow, "DiJieId");
                model.Contact = Utils.GetXAttributeValue(xRow, "Contact");
                model.Phone = Utils.GetXAttributeValue(xRow, "Phone");
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.Fax = Utils.GetXAttributeValue(xRow, "Fax");

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划文件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourFile> GetTourFileList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourFile> list = new List<EyouSoft.Model.HTourStructure.MTourFile>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourFile model = new EyouSoft.Model.HTourStructure.MTourFile();
                model.FileName = Utils.GetXAttributeValue(xRow, "FileName");
                model.FilePath = Utils.GetXAttributeValue(xRow, "FilePath");

                list.Add(model);
            }

            return list;
        }



        /// <summary>
        /// 计划购物点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourShop> GetTourShopList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourShop> list = new List<EyouSoft.Model.HTourStructure.MTourShop>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourShop model = new EyouSoft.Model.HTourStructure.MTourShop();
                model.ShopId = Utils.GetXAttributeValue(xRow, "ShopId");
                model.ShopName = Utils.GetXAttributeValue(xRow, "ShopName");
                model.LiuShui = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "LiuShui"));
                model.PeopleMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PeopleMoney"));
                model.ChildMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ChildMoney"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 报价风味餐
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourFoot> GetTourFootList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourFoot> list = new List<EyouSoft.Model.HTourStructure.MTourFoot>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourFoot model = new EyouSoft.Model.HTourStructure.MTourFoot();
                model.RestaurantId = Utils.GetXAttributeValue(xRow, "RestaurantId");
                model.MenuId = Utils.GetXAttributeValue(xRow, "MenuId");
                model.Menu = Utils.GetXAttributeValue(xRow, "Menu");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.FootId = Utils.GetXAttributeValue(xRow, "FootId");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划报价赠送
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourGive> GetTourGiveList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourGive> list = new List<EyouSoft.Model.HTourStructure.MTourGive>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourGive model = new EyouSoft.Model.HTourStructure.MTourGive();
                model.ItemId = Utils.GetXAttributeValue(xRow, "ItemId");
                model.Item = Utils.GetXAttributeValue(xRow, "Item");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划小费
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourTip> GetTourTipList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourTip> list = new List<EyouSoft.Model.HTourStructure.MTourTip>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourTip model = new EyouSoft.Model.HTourStructure.MTourTip();
                model.Tip = Utils.GetXAttributeValue(xRow, "Tip");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.Days = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Days"));
                model.SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SumPrice"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 自费项目
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourSelfPay> GetTourSelfPayList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourSelfPay> list = new List<EyouSoft.Model.HTourStructure.MTourSelfPay>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourSelfPay model = new EyouSoft.Model.HTourStructure.MTourSelfPay();
                model.CityId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CityId"));
                model.CityName = Utils.GetXAttributeValue(xRow, "CityName");
                model.ScenicSpotId = Utils.GetXAttributeValue(xRow, "ScenicSpotId");
                model.ScenicSpotName = Utils.GetXAttributeValue(xRow, "ScenicSpotName");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                model.Cost = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Cost"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.PriceId = Utils.GetXAttributeValue(xRow, "PriceId");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 团队报价单价明细
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourCost> GetTourCostList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourCost> list = new List<EyouSoft.Model.HTourStructure.MTourCost>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourCost model = new EyouSoft.Model.HTourStructure.MTourCost();
                model.Pricetype = (EyouSoft.Model.EnumType.TourStructure.Pricetype)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Pricetype"));
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetXAttributeValue(xRow, "PriceUnit"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.CostMode = (EyouSoft.Model.EnumType.TourStructure.CostMode)Utils.GetInt(Utils.GetXAttributeValue(xRow, "CostMode"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 价格
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourPrice> GetTourPriceList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourPrice> list = new List<EyouSoft.Model.HTourStructure.MTourPrice>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourPrice model = new EyouSoft.Model.HTourStructure.MTourPrice();
                model.AdultPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "AdultPrice"));
                model.ChildPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ChildPrice"));
                model.LeadPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "LeadPrice"));
                model.SingleRoomPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SingleRoomPrice"));
                model.OtherPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "OtherPrice"));
                model.CostMode = (EyouSoft.Model.EnumType.TourStructure.CostMode)Utils.GetInt(Utils.GetXAttributeValue(xRow, "CostMode"));
                model.TotalPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "TotalPrice"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 计划计调安排项目
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourPlanItem> GetTourPlanItemList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourPlanItem> list = new List<EyouSoft.Model.HTourStructure.MTourPlanItem>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourPlanItem model = new EyouSoft.Model.HTourStructure.MTourPlanItem();
                model.PlanType = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlanType"));
                list.Add(model);
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
        /// 获取联系人信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetCrmLinkman(string xml)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                EyouSoft.Model.CrmStructure.MCrmLinkman model = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                model.Id = Utils.GetXAttributeValue(xRow, "Id");
                model.CompanyId = Utils.GetXAttributeValue(xRow, "CompanyId");
                model.Name = Utils.GetXAttributeValue(xRow, "Name");
                model.Gender = (Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Gender"));
                model.Birthday = Utils.GetDateTime(Utils.GetXAttributeValue(xRow, "Birthday"));
                model.Department = Utils.GetXAttributeValue(xRow, "CompanyId");
                model.Post = Utils.GetXAttributeValue(xRow, "Post");
                model.Telephone = Utils.GetXAttributeValue(xRow, "Telephone");
                model.MobilePhone = Utils.GetXAttributeValue(xRow, "MobilePhone");
                model.QQ = Utils.GetXAttributeValue(xRow, "QQ");
                model.EMail = Utils.GetXAttributeValue(xRow, "EMail");
                model.MSN = Utils.GetXAttributeValue(xRow, "MSN");
                model.Fax = Utils.GetXAttributeValue(xRow, "Fax");
                list.Add(model);
            }

            return list;

        }



        /// <summary>
        /// 根据xml获取行程
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourPlan> GetTourPlanS(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourPlan> list = null;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);
            System.Xml.XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<EyouSoft.Model.HTourStructure.MTourPlan>();

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    System.Xml.XmlNode parent = root.ChildNodes[i];

                    EyouSoft.Model.HTourStructure.MTourPlan plan = new EyouSoft.Model.HTourStructure.MTourPlan();
                    plan.PlanId = parent["PlanId"].InnerText;
                    plan.Traffic = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(parent["Traffic"].InnerText);
                    plan.TrafficPrice = Utils.GetDecimal(parent["TrafficPrice"].InnerText);
                    plan.HotelId1 = parent["HotelId1"] != null ? parent["HotelId1"].InnerText : string.Empty;
                    plan.HotelName1 = parent["HotelName1"] != null ? parent["HotelName1"].InnerText : string.Empty;
                    plan.HotelPrice1 = parent["HotelPrice1"] != null ? Utils.GetDecimal(parent["HotelPrice1"].InnerText) : 0;
                    plan.IsBreakfast = parent["IsBreakfast"] != null ? parent["IsBreakfast"].InnerText == "1" : false;
                    plan.BreakfastPrice = parent["BreakfastPrice"] != null ? Utils.GetDecimal(parent["BreakfastPrice"].InnerText) : 0;
                    plan.BreakfastSettlementPrice = parent["BreakfastSettlementPrice"] != null ? Utils.GetDecimal(parent["BreakfastSettlementPrice"].InnerText) : 0;
                    plan.BreakfastRestaurantId = parent["BreakfastRestaurantId"] != null ? parent["BreakfastRestaurantId"].InnerText : string.Empty;
                    plan.BreakfastMenuId = parent["BreakfastMenuId"] != null ? parent["BreakfastMenuId"].InnerText : string.Empty;
                    plan.BreakfastMenu = parent["BreakfastMenu"] != null ? parent["BreakfastMenu"].InnerText : string.Empty;
                    plan.IsLunch = parent["IsLunch"] != null ? parent["IsLunch"].InnerText == "1" : false;
                    plan.LunchPrice = parent["LunchPrice"] != null ? Utils.GetDecimal(parent["LunchPrice"].InnerText) : 0;
                    plan.LunchSettlementPrice = parent["LunchSettlementPrice"] != null ? Utils.GetDecimal(parent["LunchSettlementPrice"].InnerText) : 0;
                    plan.LunchRestaurantId = parent["LunchRestaurantId"] != null ? parent["LunchRestaurantId"].InnerText : string.Empty;
                    plan.LunchMenuId = parent["LunchMenuId"] != null ? parent["LunchMenuId"].InnerText : string.Empty;
                    plan.LunchMenu = parent["LunchMenu"] != null ? parent["LunchMenu"].InnerText : string.Empty;
                    plan.IsSupper = parent["IsSupper"] != null ? parent["IsSupper"].InnerText == "1" : false;
                    plan.SupperPrice = parent["SupperPrice"] != null ? Utils.GetDecimal(parent["SupperPrice"].InnerText) : 0;
                    plan.SupperSettlementPrice = parent["SupperSettlementPrice"] != null ? Utils.GetDecimal(parent["SupperSettlementPrice"].InnerText) : 0;
                    plan.SupperRestaurantId = parent["SupperRestaurantId"] != null ? parent["SupperRestaurantId"].InnerText : string.Empty;
                    plan.SupperMenuId = parent["SupperMenuId"] != null ? parent["SupperMenuId"].InnerText : string.Empty;
                    plan.SupperMenu = parent["SupperMenu"] != null ? parent["SupperMenu"].InnerText : string.Empty;
                    plan.Days = parent["Days"] != null ? Utils.GetInt(parent["Days"].InnerText) : 0;
                    plan.Content = parent["Content"] != null ? parent["Content"].InnerText : string.Empty;
                    plan.FilePath = parent["FilePath"] != null ? parent["FilePath"].InnerText : string.Empty;

                    plan.BreakfastId = parent["BreakfastId"] != null ? parent["BreakfastId"].InnerText : string.Empty;
                    plan.LunchId = parent["LunchId"] != null ? parent["LunchId"].InnerText : string.Empty;
                    plan.SupperId = parent["SupperId"] != null ? parent["SupperId"].InnerText : string.Empty;

                    if (parent["TourPlanCity"] != null)
                    {
                        plan.TourPlanCityList = new List<EyouSoft.Model.HTourStructure.MTourPlanCity>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["TourPlanCity"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MTourPlanCity plancity = new EyouSoft.Model.HTourStructure.MTourPlanCity();
                            plancity.CityId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CityId"));
                            plancity.CityName = Utils.GetXAttributeValue(xRow, "CityName");
                            plancity.JiaoTong = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetXAttributeValue(xRow, "JiaoTong"));
                            plancity.JiaoTongJiaGe = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "JiaoTongJiaGe"));
                            plan.TourPlanCityList.Add(plancity);
                        }

                    }

                    if (parent["TourPlanShop"] != null)
                    {
                        plan.TourPlanShopList = new List<EyouSoft.Model.HTourStructure.MTourShop>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["TourPlanShop"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MTourShop planshop = new EyouSoft.Model.HTourStructure.MTourShop();
                            planshop.ShopId = Utils.GetXAttributeValue(xRow, "ShopId");
                            planshop.ShopName = Utils.GetXAttributeValue(xRow, "ShopName");
                            planshop.LiuShui = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "LiuShui"));
                            planshop.PeopleMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PeopleMoney"));
                            planshop.ChildMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ChildMoney"));
                            plan.TourPlanShopList.Add(planshop);
                        }


                    }

                    if (parent["TourPlanSpot"] != null)
                    {
                        plan.TourPlanSpotList = new List<EyouSoft.Model.HTourStructure.MTourPlanSpot>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["TourPlanSpot"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MTourPlanSpot planspot = new EyouSoft.Model.HTourStructure.MTourPlanSpot();
                            planspot.SpotId = Utils.GetXAttributeValue(xRow, "SpotId");
                            planspot.SpotName = Utils.GetXAttributeValue(xRow, "SpotName");
                            planspot.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                            planspot.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                            planspot.PriceId = Utils.GetXAttributeValue(xRow, "PriceId");
                            plan.TourPlanSpotList.Add(planspot);
                        }
                    }

                    list.Add(plan);
                }
            }



            return list;
        }

        /// <summary>
        /// 计调分车、分桌的备注
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private EyouSoft.Model.HTourStructure.TourPlanRemark GetTourPlanRemark(string xml)
        {
            EyouSoft.Model.HTourStructure.TourPlanRemark model = new EyouSoft.Model.HTourStructure.TourPlanRemark();

            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                model.IsCar = Utils.GetXAttributeValue(xRow, "IsCar") == "1";
                model.CarNum = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CarNum"));
                model.CarRemark = Utils.GetXAttributeValue(xRow, "CarRemark");
                model.IsCar = Utils.GetXAttributeValue(xRow, "IsDesk") == "1";
                model.CarNum = Utils.GetInt(Utils.GetXAttributeValue(xRow, "DeskNum"));
                model.CarRemark = Utils.GetXAttributeValue(xRow, "DeskRemark");
            }

            return model;
        }

        /// <summary>
        ///获取 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourJourney> GetTourJourney(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MTourJourney> list = new List<EyouSoft.Model.HTourStructure.MTourJourney>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourJourney model = new EyouSoft.Model.HTourStructure.MTourJourney();
                model.SourceId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "SourceId"));
                model.JourneyType = (EyouSoft.Model.EnumType.TourStructure.JourneyType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "JourneyType"));
                list.Add(model);
            }

            return list;

        }

        #endregion
    }
}
