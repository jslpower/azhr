using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.HTourStructure
{
    /// <summary>
    /// 团队信息
    /// </summary>
    public class DTour : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.HTourStructure.ITour
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

        /// <summary>
        /// 验证是否存在相同的团号(特价产品)
        /// </summary>
        /// <param name="TourCode">团号</param>
        /// <param name="QuoteId">计划编号</param>
        /// <returns></returns>
        public bool isExist(string TourCode, string TourId)
        {
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            string cmdText = "SELECT COUNT(*) FROM view_Tour_Model WHERE TourCode=@TourCode";
            _db.AddInParameter(cmd, "TourCode", DbType.String, TourCode);
            if (!string.IsNullOrEmpty(TourId))
            {
                cmdText += " AND TourId<>@TourId ";
                _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            }

            cmd.CommandText = cmdText;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }
            return false;
        }

        /// <summary> 
        /// 添加团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddTour(EyouSoft.Model.HTourStructure.MTour model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Add");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)model.LngType);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(cmd, "TourMode", DbType.Byte, (int)model.TourMode);
            _db.AddInParameter(cmd, "TourFile", DbType.String, model.TourFile);
            _db.AddInParameter(cmd, "TourCustomerCode", DbType.String, model.TourCustomerCode);
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
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
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

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }


        /// <summary> 
        /// 添加散拼计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddSanPin(EyouSoft.Model.HTourStructure.MTour model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_SanPin_Add");
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
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, model.LDate==DateTime.MinValue?new DateTime?():model.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, model.RDate == DateTime.MinValue ? new DateTime?() : model.RDate);
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
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
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
            _db.AddInParameter(cmd, "LeavePeopleNumber", DbType.Int32, 0);
            _db.AddInParameter(cmd, "PlanPeopleNumber", DbType.Int32, model.PlanPeopleNumber);
            _db.AddInParameter(cmd, "IsShowDistribution", DbType.AnsiStringFixedLength, model.IsShowDistribution ? "1" : "0");

            _db.AddInParameter(cmd, "TourJourney", DbType.Xml, CreateTourJourneyXml(model.TourJourneyList));
            _db.AddInParameter(cmd, "TourShouKeStatus", DbType.Int32, (int)model.TourShouKeStatus);
            //散拼报价
            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, CreateTourSanPinPriceXml(model.MTourPriceStandard));

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
            //散拼模版团编号
            _db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, model.ParentId);
            //线路编号集合(以,分隔)
            //if (model.RouteIds == null)
            //    model.RouteIds = "";
            //_db.AddInParameter(cmd, "RouteIds", DbType.String, model.RouteIds);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 修改团队计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateTour(EyouSoft.Model.HTourStructure.MTour model)
        {

            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Update");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)model.LngType);
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
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
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
        /// 修改散拼计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSanPin(EyouSoft.Model.HTourStructure.MTour model)
        {

            DbCommand cmd = _db.GetStoredProcCommand("proc_SanPin_Update");
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
            _db.AddInParameter(cmd, "LDate", DbType.DateTime, model.LDate==DateTime.MinValue?new DateTime?():model.LDate);
            _db.AddInParameter(cmd, "RDate", DbType.DateTime, model.RDate == DateTime.MinValue ? new DateTime?() : model.RDate);
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
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
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

            _db.AddInParameter(cmd, "PlanPeopleNumber", DbType.Int32, model.PlanPeopleNumber);
            _db.AddInParameter(cmd, "IsShowDistribution", DbType.AnsiStringFixedLength, model.IsShowDistribution ? "1" : "0");
            //散拼报价
            _db.AddInParameter(cmd, "TourPriceStandard", DbType.Xml, CreateTourSanPinPriceXml(model.MTourPriceStandard));

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
            _db.AddInParameter(cmd, "ZiTuanLDates", DbType.String, model.ZiTuanLDates);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }


        /// <summary>
        /// 修改团队状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateTourStatus(EyouSoft.Model.HTourStructure.MTourStatusChange model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Tour_Status");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)model.TourStatus);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);
            _db.AddInParameter(cmd, "IsJieShou", DbType.AnsiStringFixedLength, model.IsJieShou.HasValue ? (model.IsJieShou.Value ? "1" : "0") : "0");
            _db.AddInParameter(cmd, "Remark", DbType.String, model.Remark);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 修改团态标识、销售标识
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int UpdateTourMark(EyouSoft.Model.ComStructure.MGuidePlanWork m)
        {
            var sql = new StringBuilder("update tbl_tour set tourmark=@tourmark,salemark=@salemark where tourid=@tourid");
            var dc = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(dc, "@tourid", DbType.AnsiStringFixedLength, m.TourId);
            this._db.AddInParameter(dc, "@tourmark", DbType.String, m.TourMark);
            this._db.AddInParameter(dc, "@salemark", DbType.String, m.SaleMark);
            return DbHelper.ExecuteSql(dc, this._db);
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
        /// 获取实体
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetRouteInfoByTourId(string TourId)
        {
            EyouSoft.Model.HTourStructure.MTour model = null;
            string sql = "select TourId,RouteName,TourPlan from view_Tour_Model where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {

                    model = new EyouSoft.Model.HTourStructure.MTour();
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    //计划行程安排
                    model.TourPlanList = !dr.IsDBNull(dr.GetOrdinal("TourPlan")) ? GetTourPlan(dr.GetString(dr.GetOrdinal("TourPlan"))) : null;
                }
            }
            return model;

        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetTourModel(string TourId)
        {
            EyouSoft.Model.HTourStructure.MTour model = null;
            string sql = "select * from view_Tour_Model where TourId=@TourId";

            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {

                    model = new EyouSoft.Model.HTourStructure.MTour();

                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)dr.GetByte(dr.GetOrdinal("LngType"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourMode = (EyouSoft.Model.EnumType.TourStructure.TourMode)Utils.GetInt(dr["TourMode"].ToString());
                    model.TourFile = !dr.IsDBNull(dr.GetOrdinal("TourFile")) ? dr.GetString(dr.GetOrdinal("TourFile")) : string.Empty;

                    model.TourCustomerCode = !dr.IsDBNull(dr.GetOrdinal("TourCustomerCode")) ? dr.GetString(dr.GetOrdinal("TourCustomerCode")) : string.Empty;
                    model.AreaId = Utils.GetInt(dr["AreaId"].ToString());
                    model.AreaName = dr["AreaName"].ToString();
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    model.TourDays = dr.GetInt32(dr.GetOrdinal("TourDays"));
                    model.BuyCompanyID = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyID")) ? dr.GetString(dr.GetOrdinal("BuyCompanyID")) : string.Empty;
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    model.CountryId = dr.GetInt32(dr.GetOrdinal("CountryId"));
                    model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RDate")))
                    {
                        model.RDate = dr.GetDateTime(dr.GetOrdinal("RDate"));
                    }
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : string.Empty;
                    model.ArriveCityFlight = !dr.IsDBNull(dr.GetOrdinal("ArriveCityFlight")) ? dr.GetString(dr.GetOrdinal("ArriveCityFlight")) : string.Empty;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : string.Empty;
                    model.LeaveCityFlight = !dr.IsDBNull(dr.GetOrdinal("LeaveCityFlight")) ? dr.GetString(dr.GetOrdinal("LeaveCityFlight")) : string.Empty;
                    model.SellerId = !dr.IsDBNull(dr.GetOrdinal("SellerId")) ? dr.GetString(dr.GetOrdinal("SellerId")) : string.Empty;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    model.SellerDeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));
                    model.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                    model.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                    model.Leaders = dr.GetInt32(dr.GetOrdinal("Leaders"));
                    model.SiPei = dr.GetInt32(dr.GetOrdinal("sipei"));

                    model.JourneySpot = !dr.IsDBNull(dr.GetOrdinal("JourneySpot")) ? dr.GetString(dr.GetOrdinal("JourneySpot")) : string.Empty;
                    model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)dr.GetByte(dr.GetOrdinal("OutQuoteType"));
                    model.QuoteRemark = !dr.IsDBNull(dr.GetOrdinal("QuoteRemark")) ? dr.GetString(dr.GetOrdinal("QuoteRemark")) : string.Empty;
                    model.SpecificRequire = !dr.IsDBNull(dr.GetOrdinal("SpecificRequire")) ? dr.GetString(dr.GetOrdinal("SpecificRequire")) : string.Empty;
                    model.TravelNote = !dr.IsDBNull(dr.GetOrdinal("TravelNote")) ? dr.GetString(dr.GetOrdinal("TravelNote")) : string.Empty;
                    model.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                    model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                    model.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)dr.GetByte(dr.GetOrdinal("TourSureStatus"));
                    model.InsideInformation = !dr.IsDBNull(dr.GetOrdinal("InsideInformation")) ? dr.GetString(dr.GetOrdinal("InsideInformation")) : string.Empty;
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.Operator = dr.GetString(dr.GetOrdinal("Operator"));
                    model.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    model.UpdateTime = dr.GetDateTime(dr.GetOrdinal("UpdateTime"));
                    model.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                    model.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                    model.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));


                    model.TourJourneyList = !dr.IsDBNull(dr.GetOrdinal("TourJourney")) ? GetTourJourney(dr.GetString(dr.GetOrdinal("TourJourney"))) : null;

                    //计划计调员
                    model.TourPlanerList = !dr.IsDBNull(dr.GetOrdinal("TourPlaner")) ? GetTourPlanerList(dr.GetString(dr.GetOrdinal("TourPlaner"))) : null;
                    //计划计调项集合
                    model.TourPlanItemList = !dr.IsDBNull(dr.GetOrdinal("TourPlanItem")) ? GetTourPlanItemList(dr.GetString(dr.GetOrdinal("TourPlanItem"))) : null;
                    //计调分车、分桌的备注
                    model.TourPlanRemark = !dr.IsDBNull(dr.GetOrdinal("TourPlanRemark")) ? GetTourPlanRemark(dr.GetString(dr.GetOrdinal("TourPlanRemark"))) : null;

                    //计划房型
                    model.TourRoomList = !dr.IsDBNull(dr.GetOrdinal("TourRoom")) ? GetTourRoomList(dr.GetString(dr.GetOrdinal("TourRoom"))) : null;
                    //计划地接
                    model.TourDiJieList = !dr.IsDBNull(dr.GetOrdinal("TourDiJie")) ? GetTourDiJieList(dr.GetString(dr.GetOrdinal("TourDiJie"))) : null;
                    //计划文件
                    model.TourFileList = !dr.IsDBNull(dr.GetOrdinal("TourFiles")) ? GetTourFileList(dr.GetString(dr.GetOrdinal("TourFiles"))) : null;
                    //计划行程安排
                    model.TourPlanList = !dr.IsDBNull(dr.GetOrdinal("TourPlan")) ? GetTourPlan(dr.GetString(dr.GetOrdinal("TourPlan"))) : null;
                    //计划购物点
                    model.TourShopList = !dr.IsDBNull(dr.GetOrdinal("TourShop")) ? GetTourShopList(dr.GetString(dr.GetOrdinal("TourShop"))) : null;
                    //计划风味餐
                    model.TourFootList = !dr.IsDBNull(dr.GetOrdinal("TourFoot")) ? GetTourFootList(dr.GetString(dr.GetOrdinal("TourFoot"))) : null;
                    //计划自费项目
                    model.TourSelfPayList = !dr.IsDBNull(dr.GetOrdinal("TourSelfPay")) ? GetTourSelfPayList(dr.GetString(dr.GetOrdinal("TourSelfPay"))) : null;
                    //计划报价赠送 
                    model.TourGiveList = !dr.IsDBNull(dr.GetOrdinal("TourGive")) ? GetTourGiveList(dr.GetString(dr.GetOrdinal("TourGive"))) : null;
                    //计划小费
                    model.TourTipList = !dr.IsDBNull(dr.GetOrdinal("TourTip")) ? GetTourTipList(dr.GetString(dr.GetOrdinal("TourTip"))) : null;
                    //计划价格条目
                    model.TourCostList = !dr.IsDBNull(dr.GetOrdinal("TourCost")) ? GetTourCostList(dr.GetString(dr.GetOrdinal("TourCost"))) : null;
                    //计划价格
                    model.TourPriceList = !dr.IsDBNull(dr.GetOrdinal("TourPrice")) ? GetTourPriceList(dr.GetString(dr.GetOrdinal("TourPrice"))) : null;

                    model.Planers = dr["Planers"].ToString();
                    model.Guides = dr["Guides"].ToString();
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.RouteIds = !dr.IsDBNull(dr.GetOrdinal("RouteIds")) ? dr.GetString(dr.GetOrdinal("RouteIds")) : string.Empty;
                }
            }

            return model;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetPaituanModel(string TourId)
        {
            EyouSoft.Model.HTourStructure.MTour model = null;
            //string sql = "select * from view_Tour_Model where TourId=@TourId";
            string sql = "select *,(select * from tbl_TourPlanItem where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanItem,(select GuideUserId,SourceName from tbl_Plan where TourId=tbl_Tour.TourId and type=12 and IsDelete='0' for xml raw,root) as GuideList, (select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as TourPlanerList, (select * from tbl_ComAttach where ItemType=18 and ItemId=@TourId for xml raw,root) as VisaFile  from  tbl_Tour where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {

                    model = new EyouSoft.Model.HTourStructure.MTour();

                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                }
            }

            return model;
        }


        /// <summary>
        /// 获取散拼实体
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTour GetSanPinModel(string TourId,bool? isparent,DateTime? ldate)
        {
            EyouSoft.Model.HTourStructure.MTour model = null;

            string SQL_SELECT_GetTourInfo = isparent.HasValue && ldate.HasValue ? "select * from view_SanPin_Model where parentid=@TourId and ldate=@ldate" : "select * from view_SanPin_Model where TourId=@TourId";

            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetTourInfo);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "ldate", DbType.DateTime, ldate);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    #region  注释
                    model = new EyouSoft.Model.HTourStructure.MTour();

                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)dr.GetByte(dr.GetOrdinal("LngType"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourMode = (EyouSoft.Model.EnumType.TourStructure.TourMode)dr.GetByte(dr.GetOrdinal("TourMode"));
                    model.TourFile = !dr.IsDBNull(dr.GetOrdinal("TourFile")) ? dr.GetString(dr.GetOrdinal("TourFile")) : string.Empty;

                    model.TourCustomerCode = !dr.IsDBNull(dr.GetOrdinal("TourCustomerCode")) ? dr.GetString(dr.GetOrdinal("TourCustomerCode")) : string.Empty;
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    model.AreaName = dr["AreaName"].ToString();
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    model.TourDays = dr.GetInt32(dr.GetOrdinal("TourDays"));
                    model.BuyCompanyID = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyID")) ? dr.GetString(dr.GetOrdinal("BuyCompanyID")) : string.Empty;
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    model.CountryId = dr.GetInt32(dr.GetOrdinal("CountryId"));
                    model.LDate = dr.IsDBNull(dr.GetOrdinal("LDate"))?DateTime.MinValue: dr.GetDateTime(dr.GetOrdinal("LDate"));
                    //model.RDate =dr.GetDateTime(dr.GetOrdinal("RDate"));
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : string.Empty;
                    model.ArriveCityFlight = !dr.IsDBNull(dr.GetOrdinal("ArriveCityFlight")) ? dr.GetString(dr.GetOrdinal("ArriveCityFlight")) : string.Empty;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : string.Empty;
                    model.LeaveCityFlight = !dr.IsDBNull(dr.GetOrdinal("LeaveCityFlight")) ? dr.GetString(dr.GetOrdinal("LeaveCityFlight")) : string.Empty;
                    model.SellerId = !dr.IsDBNull(dr.GetOrdinal("SellerId")) ? dr.GetString(dr.GetOrdinal("SellerId")) : string.Empty;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    model.SellerDeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));
                    model.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                    model.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                    model.Leaders = dr.GetInt32(dr.GetOrdinal("Leaders"));
                    model.SiPei = dr.GetInt32(dr.GetOrdinal("sipei"));
                    model.IsShowDistribution = dr.IsDBNull(dr.GetOrdinal("IsShowDistribution")) ? false : dr.GetString(dr.GetOrdinal("IsShowDistribution")) == "1" ? true : false;

                    model.JourneySpot = !dr.IsDBNull(dr.GetOrdinal("JourneySpot")) ? dr.GetString(dr.GetOrdinal("JourneySpot")) : string.Empty;
                    model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)dr.GetByte(dr.GetOrdinal("OutQuoteType"));
                    model.QuoteRemark = !dr.IsDBNull(dr.GetOrdinal("QuoteRemark")) ? dr.GetString(dr.GetOrdinal("QuoteRemark")) : string.Empty;
                    model.SpecificRequire = !dr.IsDBNull(dr.GetOrdinal("SpecificRequire")) ? dr.GetString(dr.GetOrdinal("SpecificRequire")) : string.Empty;
                    model.TravelNote = !dr.IsDBNull(dr.GetOrdinal("TravelNote")) ? dr.GetString(dr.GetOrdinal("TravelNote")) : string.Empty;
                    model.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                    model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                    model.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)dr.GetByte(dr.GetOrdinal("TourSureStatus"));
                    model.InsideInformation = !dr.IsDBNull(dr.GetOrdinal("InsideInformation")) ? dr.GetString(dr.GetOrdinal("InsideInformation")) : string.Empty;
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.Operator = dr.GetString(dr.GetOrdinal("Operator"));
                    model.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    model.UpdateTime = dr.GetDateTime(dr.GetOrdinal("UpdateTime"));
                    //model.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                    //model.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                    //model.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));

                    //-----------
                    model.TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)dr.GetByte(dr.GetOrdinal("TourShouKeStatus"));
                    model.PlanPeopleNumber = dr.IsDBNull(dr.GetOrdinal("PlanPeopleNumber")) ? 0 : dr.GetInt32(dr.GetOrdinal("PlanPeopleNumber"));
                    model.LeavePeopleNumber = dr.IsDBNull(dr.GetOrdinal("LeavePeopleNumber")) ? 0 : dr.GetInt32(dr.GetOrdinal("LeavePeopleNumber"));
                    model.MTourPriceStandard = GetTourSanPinPrice(TourId);


                    model.TourJourneyList = !dr.IsDBNull(dr.GetOrdinal("TourJourney")) ? GetTourJourney(dr.GetString(dr.GetOrdinal("TourJourney"))) : null;

                    //计划计调员
                    model.TourPlanerList = !dr.IsDBNull(dr.GetOrdinal("TourPlaner")) ? GetTourPlanerList(dr.GetString(dr.GetOrdinal("TourPlaner"))) : null;
                    //计划计调项集合
                    model.TourPlanItemList = !dr.IsDBNull(dr.GetOrdinal("TourPlanItem")) ? GetTourPlanItemList(dr.GetString(dr.GetOrdinal("TourPlanItem"))) : null;
                    //计调分车、分桌的备注
                    model.TourPlanRemark = !dr.IsDBNull(dr.GetOrdinal("TourPlanRemark")) ? GetTourPlanRemark(dr.GetString(dr.GetOrdinal("TourPlanRemark"))) : null;

                    //计划房型
                    model.TourRoomList = !dr.IsDBNull(dr.GetOrdinal("TourRoom")) ? GetTourRoomList(dr.GetString(dr.GetOrdinal("TourRoom"))) : null;
                    //计划地接
                    model.TourDiJieList = !dr.IsDBNull(dr.GetOrdinal("TourDiJie")) ? GetTourDiJieList(dr.GetString(dr.GetOrdinal("TourDiJie"))) : null;
                    //计划文件
                    model.TourFileList = !dr.IsDBNull(dr.GetOrdinal("TourFiles")) ? GetTourFileList(dr.GetString(dr.GetOrdinal("TourFiles"))) : null;
                    //计划行程安排
                    model.TourPlanList = !dr.IsDBNull(dr.GetOrdinal("TourPlan")) ? GetTourPlan(dr.GetString(dr.GetOrdinal("TourPlan"))) : null;
                    //计划购物点
                    model.TourShopList = !dr.IsDBNull(dr.GetOrdinal("TourShop")) ? GetTourShopList(dr.GetString(dr.GetOrdinal("TourShop"))) : null;
                    //计划风味餐
                    model.TourFootList = !dr.IsDBNull(dr.GetOrdinal("TourFoot")) ? GetTourFootList(dr.GetString(dr.GetOrdinal("TourFoot"))) : null;
                    //计划自费项目
                    model.TourSelfPayList = !dr.IsDBNull(dr.GetOrdinal("TourSelfPay")) ? GetTourSelfPayList(dr.GetString(dr.GetOrdinal("TourSelfPay"))) : null;
                    //计划报价赠送 
                    model.TourGiveList = !dr.IsDBNull(dr.GetOrdinal("TourGive")) ? GetTourGiveList(dr.GetString(dr.GetOrdinal("TourGive"))) : null;
                    //计划小费
                    model.TourTipList = !dr.IsDBNull(dr.GetOrdinal("TourTip")) ? GetTourTipList(dr.GetString(dr.GetOrdinal("TourTip"))) : null;
                    //计划价格条目
                    model.TourCostList = !dr.IsDBNull(dr.GetOrdinal("TourCost")) ? GetTourCostList(dr.GetString(dr.GetOrdinal("TourCost"))) : null;
                    //计划价格
                    model.TourPriceList = !dr.IsDBNull(dr.GetOrdinal("TourPrice")) ? GetTourPriceList(dr.GetString(dr.GetOrdinal("TourPrice"))) : null;

                    model.Planers = dr["Planers"].ToString();
                    model.Guides = dr["Guides"].ToString();
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.RouteIds = !dr.IsDBNull(dr.GetOrdinal("RouteIds")) ? dr.GetString(dr.GetOrdinal("RouteIds")) : string.Empty;
                    model.ZiTuanLDates = dr["ZiTuanLDates"].ToString();
                    #endregion

                    #region Old
                    //model.TourId = dr["TourId"].ToString();
                    // model.TourCode = dr["TourCode"].ToString();
                    //model.AreaId = dr.IsDBNull(dr.GetOrdinal("AreaId")) ? 0 : dr.GetInt32(dr.GetOrdinal("AreaId"));
                    // model.RouteName = dr["RouteName"].ToString();
                    // model.CompanyId = dr["CompanyId"].ToString();

                    // model.LDate =dr.GetDateTime(dr.GetOrdinal("LDate"));
                    // //model.RDate = dr.IsDBNull(dr.GetOrdinal("RDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("RDate"));
                    // //model.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo() { OperatorId = dr["OperatorId"].ToString(); Name = rdr["Operator"].ToString() },
                    // //model.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = dr["SellerId"].ToString(), Name = dr["SellerName"].ToString() },

                    //  model.SellerId = !dr.IsDBNull(dr.GetOrdinal("SellerId")) ? dr.GetString(dr.GetOrdinal("SellerId")) : string.Empty;
                    //model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    // model.SellerDeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));

                    // model.IsShowDistribution = dr.IsDBNull(dr.GetOrdinal("IsShowDistribution")) ? false : dr.GetString(dr.GetOrdinal("IsShowDistribution")) == "1" ? true : false,

                    // model.PlanPeopleNumber = dr.IsDBNull(dr.GetOrdinal("PlanPeopleNumber")) ? 0 : dr.GetInt32(dr.GetOrdinal("PlanPeopleNumber"));
                    // model.LeavePeopleNumber = dr.IsDBNull(dr.GetOrdinal("LeavePeopleNumber")) ? 0 : dr.GetInt32(dr.GetOrdinal("LeavePeopleNumber"));

                    // model.TourShouKeStatus = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)dr.GetByte(dr.GetOrdinal("TourShouKeStatus"));

                    // model.TourDays = dr.IsDBNull(dr.GetOrdinal("TourDays")) ? 0 : dr.GetInt32(dr.GetOrdinal("TourDays"));

                    // model.TourPlan = this.GetTourPlan(TourId);
                    // model.MTourPriceStandard = this.GetTourSanPinPrice(TourId);

                    // model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                    // model.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                    // //model.GuideList = GetGuidByXml(dr["GuideList"].ToString());
                    // model.TourPlanItem = GetTourPlanItemByXml(dr["TourPlanItem"].ToString());
                    // model.TourPlaner = GetTourPlanerByXml(dr["TourPlanerList"].ToString());

                    // model.Adults = dr.IsDBNull(dr.GetOrdinal("Adults")) ? 0 : dr.GetInt32(dr.GetOrdinal("Adults"));
                    // model.Childs = dr.IsDBNull(dr.GetOrdinal("Childs")) ? 0 : dr.GetInt32(dr.GetOrdinal("Childs"));
                    // model.RouteIds = !dr.IsDBNull(dr.GetOrdinal("RouteIds")) ? dr.GetString(dr.GetOrdinal("RouteIds")) : string.Empty;
                    #endregion
                }
            }

            return model;
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
            string SQL_SELECT_GetSanPinPrice = "if exists(select 1 from tbl_TourPrice where TourId=@TourId) select A.*,(select Name from tbl_ComStand where Id=A.Standard) as StandardName,(select Name from tbl_ComLev where LevId=A.LevelId) as LevelName from tbl_TourPrice as A where TourId=@TourId else select A.*,(select Name from tbl_ComStand where Id=A.Standard) as StandardName,(select Name from tbl_ComLev where LevId=A.LevelId) as LevelName from tbl_TourPrice as A where TourId=(select TourId from tbl_Tour where TourId=@TourId)";

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
        /// 获取计划数量
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetTourNum(string CompanyId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("select dbo.fn_PadLeft(cast(count(TourId)+1 as nvarchar(50)),'0',0) from tbl_Tour where CompanyId=@CompanyId and TourCode<>''");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            return DbHelper.GetSingle(cmd, this._db).ToString();
        }

        /// <summary>
        /// 获取计划数量
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="crmId">客户ID</param>
        /// <param name="ldate">出团时间</param>
        /// <param name="format">序列号格式化类型</param>
        /// <returns></returns>
        public string GetTourNum(string companyId, string crmId, DateTime? ldate, EyouSoft.Model.EnumType.ComStructure.OptionItemTypeSeriesFormat format)
        {
            const string sNum = "dbo.fn_PadLeft(cast(count(TourId)+1 as nvarchar(50)),'0',0)";
            var s = new StringBuilder(string.Format("select {0} from tbl_Tour where CompanyId=@CompanyId and TourCode<>''", format == EyouSoft.Model.EnumType.ComStructure.OptionItemTypeSeriesFormat.流水号 ? sNum : "CASE WHEN COUNT(TourId) BETWEEN 0 AND 25 THEN CAST(CHAR(ASCII('A')+COUNT(TourId)) AS NVARCHAR(50)) ELSE " + sNum + " END"));
            if (!string.IsNullOrEmpty(crmId))
            {
                s.AppendFormat(" and BuyCompanyID='{0}'", crmId);
            }
            if (ldate.HasValue)
            {
                s.AppendFormat(" and LDate='{0}'", ldate.Value);
            }
            DbCommand cmd = this._db.GetSqlStringCommand(s.ToString());
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.GetSingle(cmd, this._db).ToString();
        }

        /// <summary>
        /// 获取派团给计调的信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTourToPlaner GetTourToPlaner(string TourId)
        {
            EyouSoft.Model.HTourStructure.MTourToPlaner model = null;

            StringBuilder query = new StringBuilder();
            query.Append(" select * ");
            query.Append(" ,(select * from tbl_TourPlaner where TourId=tbl_TourPlanRemark.TourId for xml raw,root('Root')) as TourPlaner  ");
            query.Append(" ,(select * from tbl_TourPlanItem where TourId=tbl_TourPlanRemark.TourId for xml raw,root('Root')) as TourPlanItem ");
            query.Append(",(select InsideInformation from tbl_Tour where TourId=tbl_TourPlanRemark.TourId) as InsideInformation ");
            query.Append(" from tbl_TourPlanRemark ");
            query.Append(" where TourId=@TourId ");

            DbCommand cmd = this._db.GetSqlStringCommand(query.ToString());
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HTourStructure.MTourToPlaner();
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.IsCar = dr.GetString(dr.GetOrdinal("IsCar")) == "1";
                    model.CarNum = dr.GetInt32(dr.GetOrdinal("CarNum"));
                    model.CarRemark = !dr.IsDBNull(dr.GetOrdinal("CarRemark")) ? dr.GetString(dr.GetOrdinal("CarRemark")) : string.Empty;
                    model.IsDesk = dr.GetString(dr.GetOrdinal("IsDesk")) == "1";
                    model.DeskNum = dr.GetInt32(dr.GetOrdinal("DeskNum"));
                    model.DeskRemark = !dr.IsDBNull(dr.GetOrdinal("DeskRemark")) ? dr.GetString(dr.GetOrdinal("DeskRemark")) : string.Empty;
                    model.TourPlanerList = !dr.IsDBNull(dr.GetOrdinal("TourPlaner")) ? GetTourPlanerList(dr.GetString(dr.GetOrdinal("TourPlaner"))) : null;
                    model.TourPlanItemList = !dr.IsDBNull(dr.GetOrdinal("TourPlanItem")) ? GetTourPlanItemList(dr.GetString(dr.GetOrdinal("TourPlanItem"))) : null;


                    model.InsideInformation = !dr.IsDBNull(dr.GetOrdinal("InsideInformation")) ? dr.GetString(dr.GetOrdinal("InsideInformation")) : string.Empty;
                }
            }

            return model;
        }


        /// <summary>
        /// 派团给计调
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1：计划已派团给计调  0:失败 1：成功</returns>
        public int SendTourToPlaner(EyouSoft.Model.HTourStructure.MTourToPlaner model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_to_Planer");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "IsCar", DbType.AnsiStringFixedLength, model.IsCar ? 1 : 0);
            _db.AddInParameter(cmd, "CarNum", DbType.Int32, model.CarNum);
            _db.AddInParameter(cmd, "CarRemark", DbType.String, model.CarRemark);
            _db.AddInParameter(cmd, "IsDesk", DbType.AnsiStringFixedLength, model.IsDesk ? 1 : 0);
            _db.AddInParameter(cmd, "DeskNum", DbType.Int32, model.DeskNum);
            _db.AddInParameter(cmd, "DeskRemark", DbType.String, model.DeskRemark);
            _db.AddInParameter(cmd, "InsideInformation", DbType.String, model.InsideInformation);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);
            _db.AddInParameter(cmd, "TourPlaner", DbType.Xml, CreateTourPlanerXml(model.TourPlanerList, model.TourId));
            _db.AddInParameter(cmd, "TourPlanItem", DbType.Xml, CreateTourPlanItemXml(model.TourPlanItemList, model.TourId));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

        }


        /// <summary>
        /// 团队确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int TourSure(EyouSoft.Model.HTourStructure.MTourSure model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_Sure");
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, model.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, model.Childs);
            _db.AddInParameter(cmd, "Leaders", DbType.Int32, model.Leaders);
            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, model.SalerIncome);
            _db.AddInParameter(cmd, "InsideInformation", DbType.String, model.InsideInformation);
            _db.AddInParameter(cmd, "TourSureStatus", DbType.Byte, (int)model.TourSureStatus);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }


        /// <summary>
        /// 获取计划列表(团队产品、特价产品)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourInfo> GetTourInfoList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            EyouSoft.Model.HTourStructure.MTourSearch search)
        {
            IList<EyouSoft.Model.HTourStructure.MTourInfo> list = new List<EyouSoft.Model.HTourStructure.MTourInfo>();

            string tableName = "view_Tour";

            string orderByString = " UpdateTime desc ";

            StringBuilder fields = new StringBuilder();
            fields.Append(" TourId,TourCode,AreaId,AreaName,RouteName,TourDays,LDate,RDate,AdultPrice,SellerId,LngType,  ");
            fields.Append(" ChildPrice,LeaderPrice,SingleRoomPrice,Adults,Childs,Leaders,BuyCompanyName, ");
            fields.Append(" CrmLinkman,SellerName,TourPlanStatus,TourStatus,TourSureStatus,Planers,ArriveCity,ArriveCityFlight,LeaveCity,LeaveCityFlight,IsChange,IsSure,OrderId,CancelReson,CheckMoney,ConfirmMoney,SumPrice,IssueTime,OutQuoteType,Operator ");


            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId='{1}' AND IsDelete='{0}' ", 0, search.CompanyId);

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.BuyCompanyID))
                {
                    query.AppendFormat(" and  BuyCompanyID='{0}' ", search.BuyCompanyID);
                }
                else if (!string.IsNullOrEmpty(search.BuyCompanyName))
                {
                    query.AppendFormat(" and BuyCompanyName like '%{0}%' ", Utils.ToSqlLike(search.BuyCompanyName));
                }

                if (!string.IsNullOrEmpty(search.ContactName))
                {
                    query.AppendFormat(" and exists(select 1 from tbl_CrmLinkman where TypeId=view_Tour.BuyCompanyID and Name like '%{0}%') ", Utils.ToSqlLike(search.ContactName));
                }

                if (!string.IsNullOrEmpty(search.TourCode))
                {
                    query.AppendFormat(" and TourCode like '%{0}%' ", Utils.ToSqlLike(search.TourCode));
                }

                if (search.AreaId.HasValue && search.AreaId.Value != 0)
                {
                    query.AppendFormat(" and AreaId={0} ", search.AreaId.Value);
                }

                if (!string.IsNullOrEmpty(search.RouteName))
                {
                    query.AppendFormat(" and RouteName like '%{0}%' ", Utils.ToSqlLike(search.RouteName));
                }

                if (search.BeginLDate.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',LDate)>=0 ", search.BeginLDate.Value);
                }

                if (search.EndLDate.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',LDate)<=0 ", search.EndLDate.Value);
                }

                if (search.BeginRDate.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',RDate)>=0 ", search.BeginRDate.Value);
                }

                if (search.EndRDate.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',RDate)<=0 ", search.EndRDate.Value);
                }

                if (search.IssueSTime.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0 ", search.IssueSTime.Value);
                }

                if (search.IssueETime.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0 ", search.IssueETime.Value);
                }

                if (!string.IsNullOrEmpty(search.SellerId))
                {
                    query.AppendFormat(" and  SellerId='{0}' ", search.SellerId);
                }
                else if (!string.IsNullOrEmpty(search.SellerName))
                {
                    query.AppendFormat(" and SellerName like '%{0}%' ", search.SellerName);
                }

                if (search.TourStatus.HasValue)
                {
                    query.AppendFormat(" and TourStatus={0} ", (int)search.TourStatus.Value);
                }

                if (search.TourSureStatus.HasValue)
                {
                    query.AppendFormat(" and TourSureStatus={0} ", (int)search.TourSureStatus.Value);
                }

                if (search.TourType.HasValue)
                {
                    query.AppendFormat(" and TourType={0} ", (int)search.TourType.Value);
                }

                if (search.StartEffectTime.HasValue)
                {
                    query.AppendFormat(" and (datediff(day,'{0}',LDate)>=0  or datediff(day,'{0}',RDate)>=0) ", search.StartEffectTime.Value);
                }

                if (search.EndEffectTime.HasValue)
                {
                    query.AppendFormat(" and (datediff(day,'{0}',LDate)<=0  or datediff(day,'{0}',RDate)<=0) ", search.EndEffectTime.Value);
                }

                if (search.Days.HasValue && search.Days != 0)
                {
                    query.AppendFormat(" and TourDays={0} ", search.Days.Value);
                }

                if (search.PeopleNum.HasValue && search.PeopleNum != 0)
                {
                    query.AppendFormat(" and Adults+Childs+Leaders={0} ", search.PeopleNum.Value);
                }

                if (!string.IsNullOrEmpty(search.PlanerId))
                {
                    query.AppendFormat(" and exists(select top 1 1 from tbl_tourplaner where tourid=view_tour.tourid and planerid='{0}') ", search.PlanerId);
                }
                else if (!string.IsNullOrEmpty(search.Planer))
                {
                    query.AppendFormat(" and exists(select top 1 1 from tbl_tourplaner where tourid=view_tour.tourid and planer like '%{0}%') ", Utils.ToSqlLike(search.Planer));
                }

                if (search.LngType.HasValue)
                {
                    query.AppendFormat(" and LngType={0}", (int)search.LngType.Value);
                }
            }

            using (IDataReader dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), query.ToString(), orderByString, null))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HTourStructure.MTourInfo model = new EyouSoft.Model.HTourStructure.MTourInfo();
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = dr["TourCode"].ToString();
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    model.AreaName = !dr.IsDBNull(dr.GetOrdinal("AreaName")) ? dr.GetString(dr.GetOrdinal("AreaName")) : string.Empty;
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    model.TourDays = dr.GetInt32(dr.GetOrdinal("TourDays"));
                    model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    model.RDate = Utils.GetDateTime(dr["RDate"].ToString());
                    model.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                    model.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                    model.LeaderPrice = dr.GetDecimal(dr.GetOrdinal("LeaderPrice"));
                    model.SingleRoomPrice = dr.GetDecimal(dr.GetOrdinal("SingleRoomPrice"));
                    model.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                    model.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                    model.Leaders = dr.GetInt32(dr.GetOrdinal("Leaders"));
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : string.Empty;
                    model.ArriveCityFlight = !dr.IsDBNull(dr.GetOrdinal("ArriveCityFlight")) ? dr.GetString(dr.GetOrdinal("ArriveCityFlight")) : string.Empty;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : string.Empty;
                    model.LeaveCityFlight = !dr.IsDBNull(dr.GetOrdinal("LeaveCityFlight")) ? dr.GetString(dr.GetOrdinal("LeaveCityFlight")) : string.Empty;
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                    model.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)dr.GetByte(dr.GetOrdinal("TourSureStatus"));

                    model.CrmLinkman = !dr.IsDBNull(dr.GetOrdinal("CrmLinkman")) ? GetCrmLinkman(dr.GetString(dr.GetOrdinal("CrmLinkman")))[0] : null;
                    model.TourPlanStatus = !dr.IsDBNull(dr.GetOrdinal("TourPlanStatus")) ? GetTourPlanStatus(dr.GetString(dr.GetOrdinal("TourPlanStatus"))) : null;

                    model.Planers = dr["Planers"].ToString();
                    model.OrderId = dr["OrderId"].ToString();

                    model.IsChange = dr.GetString(dr.GetOrdinal("IsChange")) == "1";
                    model.IsSure = dr.GetString(dr.GetOrdinal("IsSure")) == "1";
                    model.SellerId = dr["SellerId"].ToString();

                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)dr.GetByte(dr.GetOrdinal("LngType"));

                    model.CancelReson = dr["CancelReson"].ToString();
                    model.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                    model.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                    model.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)dr.GetByte(dr.GetOrdinal("OutQuoteType"));
                    model.Operator = dr["Operator"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取计划列表(团队产品、特价产品)
        /// </summary>
        /// <param name="TourId">团号</param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTourInfo GetTourInfoModel(string TourId)
        {

            EyouSoft.Model.HTourStructure.MTourInfo model = null;
            string sql = "SELECT TourId,TourCode,AreaId,AreaName,RouteName,TourDays,LDate,RDate,AdultPrice,SellerId,LngType, ChildPrice,LeaderPrice,SingleRoomPrice,Adults,Childs,Leaders,BuyCompanyName,  CrmLinkman,SellerName,TourPlanStatus,TourStatus,TourSureStatus,Planers,ArriveCity,ArriveCityFlight,LeaveCity,LeaveCityFlight,IsChange,IsSure,OrderId,CancelReson,CheckMoney,ConfirmMoney,SumPrice,IssueTime,OutQuoteType FROM view_Tour WHERE TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {

                while (dr.Read())
                {
                    model = new EyouSoft.Model.HTourStructure.MTourInfo();
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = dr["TourCode"].ToString();
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    model.AreaName = !dr.IsDBNull(dr.GetOrdinal("AreaName")) ? dr.GetString(dr.GetOrdinal("AreaName")) : string.Empty;
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    model.TourDays = dr.GetInt32(dr.GetOrdinal("TourDays"));
                    model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    model.RDate = Utils.GetDateTime(dr["RDate"].ToString());
                    model.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                    model.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                    model.LeaderPrice = dr.GetDecimal(dr.GetOrdinal("LeaderPrice"));
                    model.SingleRoomPrice = dr.GetDecimal(dr.GetOrdinal("SingleRoomPrice"));
                    model.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                    model.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                    model.Leaders = dr.GetInt32(dr.GetOrdinal("Leaders"));
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : string.Empty;
                    model.ArriveCityFlight = !dr.IsDBNull(dr.GetOrdinal("ArriveCityFlight")) ? dr.GetString(dr.GetOrdinal("ArriveCityFlight")) : string.Empty;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : string.Empty;
                    model.LeaveCityFlight = !dr.IsDBNull(dr.GetOrdinal("LeaveCityFlight")) ? dr.GetString(dr.GetOrdinal("LeaveCityFlight")) : string.Empty;
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    model.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)dr.GetByte(dr.GetOrdinal("TourStatus"));
                    model.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)dr.GetByte(dr.GetOrdinal("TourSureStatus"));

                    model.CrmLinkman = !dr.IsDBNull(dr.GetOrdinal("CrmLinkman")) ? GetCrmLinkman(dr.GetString(dr.GetOrdinal("CrmLinkman")))[0] : null;
                    model.TourPlanStatus = !dr.IsDBNull(dr.GetOrdinal("TourPlanStatus")) ? GetTourPlanStatus(dr.GetString(dr.GetOrdinal("TourPlanStatus"))) : null;

                    model.Planers = dr["Planers"].ToString();
                    model.OrderId = dr["OrderId"].ToString();

                    model.IsChange = dr.GetString(dr.GetOrdinal("IsChange")) == "1";
                    model.IsSure = dr.GetString(dr.GetOrdinal("IsSure")) == "1";
                    model.SellerId = dr["SellerId"].ToString();

                    model.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)dr.GetByte(dr.GetOrdinal("LngType"));

                    model.CancelReson = dr["CancelReson"].ToString();
                    model.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                    model.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                    model.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)dr.GetByte(dr.GetOrdinal("OutQuoteType"));
                }
            }

            return model;
        }

        #region private IList to xml

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


        /// <summary>
        /// 计划计调员信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        private string CreateTourPlaner(IList<EyouSoft.Model.HTourStructure.MTourPlaner> list, string TourId)
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
                    planer.PlanerDeptId);
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
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Phone),
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Remark),
                                    Utils.ReplaceXmlSpecialCharacter(dijie.CityName));
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
                    Utils.ReplaceXmlSpecialCharacter(item.FilePath)
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
                xmlDoc.AppendFormat("HotelName1=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.HotelName1));
                xmlDoc.AppendFormat("HotelPrice1=\"{0}\" ", item.HotelPrice1);
                xmlDoc.AppendFormat("IsBreakfast=\"{0}\" ", item.IsBreakfast ? 1 : 0);
                xmlDoc.AppendFormat("BreakfastPrice=\"{0}\" ", item.BreakfastPrice);
                xmlDoc.AppendFormat("BreakfastSettlementPrice=\"{0}\" ", item.BreakfastSettlementPrice);
                xmlDoc.AppendFormat("BreakfastRestaurantId=\"{0}\" ", item.BreakfastRestaurantId);
                xmlDoc.AppendFormat("BreakfastMenuId=\"{0}\" ", item.BreakfastMenuId);
                xmlDoc.AppendFormat("BreakfastMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.BreakfastMenu));
                xmlDoc.AppendFormat("IsLunch=\"{0}\" ", item.IsLunch ? 1 : 0);
                xmlDoc.AppendFormat("LunchPrice=\"{0}\" ", item.LunchPrice);
                xmlDoc.AppendFormat("LunchSettlementPrice=\"{0}\" ", item.LunchSettlementPrice);
                xmlDoc.AppendFormat("LunchRestaurantId=\"{0}\" ", item.LunchRestaurantId);
                xmlDoc.AppendFormat("LunchMenuId=\"{0}\" ", item.LunchMenuId);
                xmlDoc.AppendFormat("LunchMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.LunchMenu));
                xmlDoc.AppendFormat("IsSupper=\"{0}\" ", item.IsSupper ? 1 : 0);
                xmlDoc.AppendFormat("SupperPrice=\"{0}\" ", item.SupperPrice);
                xmlDoc.AppendFormat("SupperSettlementPrice=\"{0}\" ", item.SupperSettlementPrice);
                xmlDoc.AppendFormat("SupperRestaurantId=\"{0}\" ", item.SupperRestaurantId);
                xmlDoc.AppendFormat("SupperMenuId=\"{0}\" ", item.SupperMenuId);
                xmlDoc.AppendFormat("SupperMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.SupperMenu));
                xmlDoc.AppendFormat("Days=\"{0}\" ", item.Days);
                xmlDoc.AppendFormat("Content=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Content));
                xmlDoc.AppendFormat("FilePath=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.FilePath));

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
                    Utils.ReplaceXmlSpecialCharacter(item.CityName),
                    item.ScenicSpotId,
                    Utils.ReplaceXmlSpecialCharacter(item.ScenicSpotName),
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
                    Utils.ReplaceXmlSpecialCharacter(item.Item),
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
                    Utils.ReplaceXmlSpecialCharacter(item.Tip),
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

        /// <summary>
        /// 创建散拼计划价格标准XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourSanPinPriceXml(IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list)
        {
            //<Root><TourPriceStandard Standard="报价标准编号" LevelId="客户等级编号" AdultPrice="成人价" ChildPrice="儿童价" CostMode="价格类型" /></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                if (item != null)
                {
                    foreach (var items in item.PriceLevel)
                    {
                        xmlDoc.AppendFormat("<TourPriceStandard Standard=\"{0}\" LevelId=\"{1}\" AdultPrice=\"{2}\" ChildPrice=\"{3}\" CostMode=\"{4}\" />", item.Standard, items.LevelId, items.AdultPrice, items.ChildPrice, (int)items.CostMode);
                    }
                }
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
        private IList<EyouSoft.Model.HTourStructure.MTourPlaner> GetTourPlanerList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MTourPlaner> list = new List<EyouSoft.Model.HTourStructure.MTourPlaner>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MTourPlaner model = new EyouSoft.Model.HTourStructure.MTourPlaner();
                model.PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId");
                model.Planer = Utils.GetXAttributeValue(xRow, "Planer");
                model.PlanerDeptId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlanerDeptId"));
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
                model.LevelId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "LevelId"));
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
        private IList<EyouSoft.Model.HTourStructure.MTourPlan> GetTourPlan(string xml)
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
