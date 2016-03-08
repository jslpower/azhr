using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IDAL.TourStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType;
using EyouSoft.Model.EnumType.TourStructure;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using EyouSoft.Toolkit.DAL;
using System.Xml;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.TourStructure
{
    using System.Xml.Linq;

    /// <summary>
    /// 订单、游客、销售收款\退款
    /// 王磊
    /// 2011-9-5
    /// </summary>
    public class DTourOrder : EyouSoft.Toolkit.DAL.DALBase, ITourOrder
    {
        #region 初始化db
        private Database _db = null;

        /// <summary>
        /// 初始化_db
        /// </summary>
        public DTourOrder()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 添加订单
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="tourOrderExpand">订单、游客、游客保险组合的实体</param>
        /// <returns>
        ///1:修改后的订单人数超过计划剩余人数
        ///2:合同未领用
        ///3:添加成功
        ///4:添加失败
        ///-----------
        ///--5:自动客满
        ///--6:自动停收
        ///--7:手动客满
        ///--8:手动停收
        ///(5,6,7,8用于分销商订单报名时的计划状态判断)
        /// ----------
        ///王磊： 2012-08-20 
        /// --9:销售员超限
        ///--10:客户超限
        ///--11:销售员客户单位均超限
        /// </returns>
        public int AddTourOrderExpand(MTourOrderExpand tourOrderExpand)
        {

            string OrderId = !string.IsNullOrEmpty(tourOrderExpand.OrderId) ? tourOrderExpand.OrderId : Guid.NewGuid().ToString();
            //订单游客集合的xml
            string MTourOrderTravellerListXml = string.Empty;
            //订单保险集合的xml
            string MTourOrderTravellerInsuranceList = string.Empty;

            IList<MTourOrderTravellerInsurance> travellerInsuranceList = new List<MTourOrderTravellerInsurance>();
            IList<MTourOrderTraveller> travellerList = GetMTourOrderTravellerList(tourOrderExpand.MTourOrderTravellerList, OrderId, ref travellerInsuranceList);
            if (travellerList != null && travellerList.Count != 0)
            {
                MTourOrderTravellerListXml = GetXmlByMTourOrderTraveller(travellerList, OrderId);
            }
            if (travellerInsuranceList != null && travellerInsuranceList.Count != 0)
            {
                MTourOrderTravellerInsuranceList = GetXmlbyMTourOrderTravellerInsurance(travellerInsuranceList);
            }

            //垫付申请xml
            string AdvanceAppXml = string.Empty;
            if (tourOrderExpand.AdvanceApp != null)
            {
                AdvanceAppXml = CreateOverrunXml(tourOrderExpand.AdvanceApp);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Add");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, tourOrderExpand.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourOrderExpand.TourId);
            _db.AddInParameter(cmd, "BuyCountryId", DbType.Int32, tourOrderExpand.BuyCountryId);
            _db.AddInParameter(cmd, "BuyProvincesId", DbType.Int32, tourOrderExpand.BuyProvincesId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, tourOrderExpand.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, tourOrderExpand.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, tourOrderExpand.ContactTel);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, tourOrderExpand.ContactDepartId);
            _db.AddInParameter(cmd, "DCompanyName", DbType.String, tourOrderExpand.DCompanyName);
            _db.AddInParameter(cmd, "DContactName", DbType.String, tourOrderExpand.DContactName);
            _db.AddInParameter(cmd, "DContactTel", DbType.String, tourOrderExpand.DContactTel);
            _db.AddInParameter(cmd, "SellerName", DbType.String, tourOrderExpand.SellerName);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, tourOrderExpand.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.AnsiStringFixedLength, tourOrderExpand.DeptId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderExpand.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderExpand.OperatorId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, tourOrderExpand.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, tourOrderExpand.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, tourOrderExpand.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, tourOrderExpand.ChildPrice);
            _db.AddInParameter(cmd, "PriceStandId", DbType.Int32, tourOrderExpand.PriceStandId);
            _db.AddInParameter(cmd, "LevId", DbType.Int32, tourOrderExpand.LevId);
            _db.AddInParameter(cmd, "PeerLevId", DbType.Int32, tourOrderExpand.PeerLevId);
            _db.AddInParameter(cmd, "PeerAdultPrice", DbType.Currency, tourOrderExpand.PeerAdultPrice);
            _db.AddInParameter(cmd, "PeerChildPrice", DbType.Currency, tourOrderExpand.PeerChildPrice);
            _db.AddInParameter(cmd, "SettlementMoney", DbType.Currency, tourOrderExpand.SettlementMoney);
            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, tourOrderExpand.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, tourOrderExpand.GuideIncome);
            _db.AddInParameter(cmd, "SaleAddCost", DbType.Currency, tourOrderExpand.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Currency, tourOrderExpand.SaleReduceCost);
            _db.AddInParameter(cmd, "SaleAddCostRemark", DbType.String, tourOrderExpand.SaleAddCostRemark);
            _db.AddInParameter(cmd, "SaleReduceCostRemark", DbType.String, tourOrderExpand.SaleReduceCostRemark);
            _db.AddInParameter(cmd, "OtherCost", DbType.Currency, tourOrderExpand.OtherCost);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, tourOrderExpand.SumPrice);
            _db.AddInParameter(cmd, "OrderRemark", DbType.String, tourOrderExpand.OrderRemark);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (byte)tourOrderExpand.TourType);
            _db.AddInParameter(cmd, "OrderType", DbType.Byte, (byte)tourOrderExpand.OrderType);
            _db.AddInParameter(cmd, "Status", DbType.Byte, (byte)tourOrderExpand.OrderStatus);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, tourOrderExpand.SaveSeatDate);
            _db.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, tourOrderExpand.ContractId);//合同编号
            _db.AddInParameter(cmd, "ContractCode", DbType.String, tourOrderExpand.ContractCode);//合同号
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, tourOrderExpand.TravellerFile);
            _db.AddInParameter(cmd, "TourOrderTraveller", DbType.String, MTourOrderTravellerListXml);
            _db.AddInParameter(cmd, "TourOrderTravellerInsurance", DbType.String, MTourOrderTravellerInsuranceList);
            _db.AddInParameter(cmd, "AdvanceApp", DbType.String, AdvanceAppXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region 添加短线订单
        /// <summary>
        /// 添加短线订单
        /// </summary>
        /// <param name="tourOrderExpand">订单、游客、游客保险组合的实体</param>
        /// <param name="list">添加成功后 将其它订单所占当前订单所选车位返回</param>
        /// <returns>
        ///1:修改后的订单人数超过计划剩余人数
        ///2:合同未领用
        ///3:添加成功
        ///4:添加失败
        ///-----------
        ///--5:自动客满
        ///--6:自动停收
        ///--7:手动客满
        ///--8:手动停收
        ///(5,6,7,8用于分销商订单报名时的计划状态判断)
        /// ----------
        ///王磊： 2012-08-20 
        /// --9:销售员超限
        ///--10:客户超限
        ///--11:销售员客户单位均超限
        /// </returns>
        public int AddTourOrderExpand(MTourOrderExpand tourOrderExpand, ref IList<MTourOrderCarTypeSeat> list)
        {
            string OrderId = !string.IsNullOrEmpty(tourOrderExpand.OrderId) ? tourOrderExpand.OrderId : Guid.NewGuid().ToString();
            //订单游客集合的xml
            string MTourOrderTravellerListXml = string.Empty;
            //订单保险集合的xml
            string MTourOrderTravellerInsuranceList = string.Empty;

            IList<MTourOrderTravellerInsurance> travellerInsuranceList = new List<MTourOrderTravellerInsurance>();
            IList<MTourOrderTraveller> travellerList = GetMTourOrderTravellerList(tourOrderExpand.MTourOrderTravellerList, OrderId, ref travellerInsuranceList);
            if (travellerList != null && travellerList.Count != 0)
            {
                MTourOrderTravellerListXml = GetXmlByMTourOrderTraveller(travellerList, OrderId);
            }
            if (travellerInsuranceList != null && travellerInsuranceList.Count != 0)
            {
                MTourOrderTravellerInsuranceList = GetXmlbyMTourOrderTravellerInsurance(travellerInsuranceList);
            }
            //订单的上车地点
            string TourOrderCarLocationXml = string.Empty;
            if (tourOrderExpand.TourOrderCarLocation != null)
            {

                TourOrderCarLocationXml = CreateTourOrderCarLocationXml(OrderId, tourOrderExpand.TourOrderCarLocation);
            }

            //订单选座
            string TourOrderCarTypeSeatXml = string.Empty;
            if (tourOrderExpand.TourOrderCarTypeSeatList != null && tourOrderExpand.TourOrderCarTypeSeatList.Count != 0)
            {
                TourOrderCarTypeSeatXml = CreateTourOrderCarTypeSeat(OrderId, tourOrderExpand.TourOrderCarTypeSeatList);
            }

            //垫付申请xml
            string AdvanceAppXml = null;
            if (tourOrderExpand.AdvanceApp != null)
            {
                AdvanceAppXml = CreateOverrunXml(tourOrderExpand.AdvanceApp);
            }


            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Add");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, tourOrderExpand.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourOrderExpand.TourId);
            _db.AddInParameter(cmd, "BuyCountryId", DbType.Int32, tourOrderExpand.BuyCountryId);
            _db.AddInParameter(cmd, "BuyProvincesId", DbType.Int32, tourOrderExpand.BuyProvincesId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, tourOrderExpand.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, tourOrderExpand.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, tourOrderExpand.ContactTel);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, tourOrderExpand.ContactDepartId);
            _db.AddInParameter(cmd, "DCompanyName", DbType.String, tourOrderExpand.DCompanyName);
            _db.AddInParameter(cmd, "DContactName", DbType.String, tourOrderExpand.DContactName);
            _db.AddInParameter(cmd, "DContactTel", DbType.String, tourOrderExpand.DContactTel);
            _db.AddInParameter(cmd, "SellerName", DbType.String, tourOrderExpand.SellerName);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, tourOrderExpand.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.AnsiStringFixedLength, tourOrderExpand.DeptId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderExpand.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderExpand.OperatorId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, tourOrderExpand.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, tourOrderExpand.Childs);
            _db.AddInParameter(cmd, "Others", DbType.Int32, tourOrderExpand.Others);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, tourOrderExpand.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, tourOrderExpand.ChildPrice);
            _db.AddInParameter(cmd, "OtherPrice", DbType.Currency, tourOrderExpand.OtherPrice);
            _db.AddInParameter(cmd, "PriceStandId", DbType.Int32, tourOrderExpand.PriceStandId);
            _db.AddInParameter(cmd, "LevId", DbType.Int32, tourOrderExpand.LevId);
            _db.AddInParameter(cmd, "PeerLevId", DbType.Int32, tourOrderExpand.PeerLevId);
            _db.AddInParameter(cmd, "PeerAdultPrice", DbType.Currency, tourOrderExpand.PeerAdultPrice);
            _db.AddInParameter(cmd, "PeerChildPrice", DbType.Currency, tourOrderExpand.PeerChildPrice);
            _db.AddInParameter(cmd, "SettlementMoney", DbType.Currency, tourOrderExpand.SettlementMoney);
            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, tourOrderExpand.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, tourOrderExpand.GuideIncome);
            _db.AddInParameter(cmd, "SaleAddCost", DbType.Currency, tourOrderExpand.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Currency, tourOrderExpand.SaleReduceCost);
            _db.AddInParameter(cmd, "SaleAddCostRemark", DbType.String, tourOrderExpand.SaleAddCostRemark);
            _db.AddInParameter(cmd, "SaleReduceCostRemark", DbType.String, tourOrderExpand.SaleReduceCostRemark);
            _db.AddInParameter(cmd, "OtherCost", DbType.Currency, tourOrderExpand.OtherCost);
            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, tourOrderExpand.SumPrice);
            _db.AddInParameter(cmd, "OrderRemark", DbType.String, tourOrderExpand.OrderRemark);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (byte)tourOrderExpand.TourType);
            _db.AddInParameter(cmd, "OrderType", DbType.Byte, (byte)tourOrderExpand.OrderType);
            _db.AddInParameter(cmd, "Status", DbType.Byte, (byte)tourOrderExpand.OrderStatus);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, tourOrderExpand.SaveSeatDate);
            _db.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, tourOrderExpand.ContractId);//合同编号
            _db.AddInParameter(cmd, "ContractCode", DbType.String, tourOrderExpand.ContractCode);//合同号
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, tourOrderExpand.TravellerFile);
            _db.AddInParameter(cmd, "TourOrderTraveller", DbType.String, MTourOrderTravellerListXml);
            _db.AddInParameter(cmd, "TourOrderTravellerInsurance", DbType.String, MTourOrderTravellerInsuranceList);
            _db.AddInParameter(cmd, "AdvanceApp", DbType.String, AdvanceAppXml);
            _db.AddInParameter(cmd, "TourOrderCarLocation", DbType.String, TourOrderCarLocationXml);
            _db.AddInParameter(cmd, "TourOrderCarTypeSeat", DbType.String, TourOrderCarTypeSeatXml);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            //DbHelper.RunProcedureWithResult(cmd, _db);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (dr.Read())
                {
                    MTourOrderCarTypeSeat seat = new MTourOrderCarTypeSeat()
                    {

                        TourCarTypeId = dr["TourCarTypeId"].ToString(),
                        SeatNumber = dr["SeatNumber"].ToString(),
                        OrderId = OrderId
                    };
                    list.Add(seat);
                }
            }

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));


        }
        #endregion

        #region 删除订单
        /// <summary>
        /// 根据订单编号删除订单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>1:删除成功 0：删除失败</returns>
        public int DeleteTourOrderByOrderId(string orderId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Delete");
            _db.AddInParameter(cmd, "OrderId", DbType.String, orderId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

        }
        #endregion

        #region 修改_散拼订单
        /// <summary>
        /// 修改、变更订单信息(用于散拼订单)
        /// </summary>
        /// <param name="tourOrderExpand">订单、游客、游客保险组合的实体</param>
        /// <returns>
        ///1:修改后的订单人数超过计划剩余人数
        ///2:合同未领用
        ///3:修改成功
        ///4:修改失败
        ///--------------
        ///王磊：2012-08-20 
        ///--5:销售员超限
        ///--6:客户单位超限
        ///--7：销售员客户单位均超限
        /// -------------
        /// </returns>
        public int UpdateTourOrderExpand(MTourOrderExpand tourOrderExpand)
        {
            //订单修改或则变更的xml
            string TourOrderChangeXml = string.Empty;
            if (tourOrderExpand.TourOrderChange != null)
            {
                TourOrderChangeXml = this.GetTourOrderChangeXml(tourOrderExpand.TourOrderChange);
            }

            //订单游客的集合
            string MTourOrderTravellerListXml = string.Empty;
            //游客保险的集合
            string MTourOrderTravellerInsuranceList = string.Empty;

            //游客保险
            IList<MTourOrderTravellerInsurance> travellerInsuranceList = new List<MTourOrderTravellerInsurance>();

            //游客
            IList<MTourOrderTraveller> travellerList = GetMTourOrderTravellerList(tourOrderExpand.MTourOrderTravellerList, tourOrderExpand.OrderId, ref travellerInsuranceList);
            if (travellerList != null && travellerList.Count != 0)
            {
                MTourOrderTravellerListXml = GetXmlByMTourOrderTraveller(travellerList, tourOrderExpand.OrderId);
            }
            if (travellerInsuranceList != null && travellerInsuranceList.Count != 0)
            {
                MTourOrderTravellerInsuranceList = GetXmlbyMTourOrderTravellerInsurance(travellerInsuranceList);
            }

            //垫付申请xml
            string AdvanceAppXml = string.Empty;
            if (tourOrderExpand.AdvanceApp != null)
            {
                AdvanceAppXml = CreateOverrunXml(tourOrderExpand.AdvanceApp);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_Sanpin");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, tourOrderExpand.OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, tourOrderExpand.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourOrderExpand.TourId);
            _db.AddInParameter(cmd, "BuyCountryId", DbType.Int32, tourOrderExpand.BuyCountryId);
            _db.AddInParameter(cmd, "BuyProvincesId", DbType.Int32, tourOrderExpand.BuyProvincesId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, tourOrderExpand.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, tourOrderExpand.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, tourOrderExpand.ContactTel);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, tourOrderExpand.ContactDepartId);


            _db.AddInParameter(cmd, "SellerName", DbType.String, tourOrderExpand.SellerName);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, tourOrderExpand.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.AnsiStringFixedLength, tourOrderExpand.DeptId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderExpand.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderExpand.OperatorId);

            _db.AddInParameter(cmd, "Adults", DbType.Int32, tourOrderExpand.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, tourOrderExpand.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, tourOrderExpand.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, tourOrderExpand.ChildPrice);

            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, tourOrderExpand.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, tourOrderExpand.GuideIncome);


            _db.AddInParameter(cmd, "PriceStandId", DbType.Int32, tourOrderExpand.PriceStandId);
            _db.AddInParameter(cmd, "LevId", DbType.Int32, tourOrderExpand.LevId);
            _db.AddInParameter(cmd, "PeerLevId", DbType.Int32, tourOrderExpand.PeerLevId);
            _db.AddInParameter(cmd, "PeerAdultPrice", DbType.Currency, tourOrderExpand.PeerAdultPrice);
            _db.AddInParameter(cmd, "PeerChildPrice", DbType.Currency, tourOrderExpand.PeerChildPrice);

            _db.AddInParameter(cmd, "SettlementMoney", DbType.Currency, tourOrderExpand.SettlementMoney);



            _db.AddInParameter(cmd, "SaleAddCost", DbType.Currency, tourOrderExpand.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Currency, tourOrderExpand.SaleReduceCost);
            _db.AddInParameter(cmd, "SaleAddCostRemark", DbType.String, tourOrderExpand.SaleAddCostRemark);
            _db.AddInParameter(cmd, "SaleReduceCostRemark", DbType.String, tourOrderExpand.SaleReduceCostRemark);


            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, tourOrderExpand.SumPrice);


            _db.AddInParameter(cmd, "OrderRemark", DbType.String, tourOrderExpand.OrderRemark);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, tourOrderExpand.SaveSeatDate);
            //_db.AddInParameter(cmd, "TourType", DbType.Byte, (int)tourOrderExpand.TourType);
            //_db.AddInParameter(cmd, "OrderType", DbType.Byte, (int)tourOrderExpand.OrderType);
            _db.AddInParameter(cmd, "Status", DbType.Byte, (int)tourOrderExpand.OrderStatus);

            _db.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, tourOrderExpand.ContractId);//合同编号
            _db.AddInParameter(cmd, "ContractCode", DbType.String, tourOrderExpand.ContractCode);//合同号
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, tourOrderExpand.TravellerFile);//游客信息附件


            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, TourOrderChangeXml);//订单变更的xml
            _db.AddInParameter(cmd, "TourOrderTraveller", DbType.String, MTourOrderTravellerListXml);//订单游客的xml
            _db.AddInParameter(cmd, "TourOrderTravellerInsurance", DbType.String, MTourOrderTravellerInsuranceList);//订单游客保险的xml

            _db.AddInParameter(cmd, "AdvanceApp", DbType.String, AdvanceAppXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

        }
        #endregion

        #region 修改_散拼短线订单

        /// <summary>
        /// 修改、变更订单信息(用于散拼订单)
        /// </summary>
        /// <param name="tourOrderExpand">订单、游客、游客保险组合的实体</param>
        /// <param name="list">添加成功后 将其它订单所占当前订单所选车位返回</param>
        /// <returns>
        ///1:修改后的订单人数超过计划剩余人数
        ///2:合同未领用
        ///3:修改成功
        ///4:修改失败
        ///--------------
        ///王磊：2012-08-20 
        ///--5:销售员超限
        ///--6:客户单位超限
        ///--7：销售员客户单位均超限
        /// -------------
        /// </returns>
        public int UpdateTourOrderExpand(MTourOrderExpand tourOrderExpand, ref IList<MTourOrderCarTypeSeat> list)
        {

            //订单修改或则变更的xml
            string TourOrderChangeXml = string.Empty;
            if (tourOrderExpand.TourOrderChange != null)
            {
                TourOrderChangeXml = this.GetTourOrderChangeXml(tourOrderExpand.TourOrderChange);
            }

            //订单游客的集合
            string MTourOrderTravellerListXml = string.Empty;
            //游客保险的集合
            string MTourOrderTravellerInsuranceList = string.Empty;

            //游客保险
            IList<MTourOrderTravellerInsurance> travellerInsuranceList = new List<MTourOrderTravellerInsurance>();

            //游客
            IList<MTourOrderTraveller> travellerList = GetMTourOrderTravellerList(tourOrderExpand.MTourOrderTravellerList, tourOrderExpand.OrderId, ref travellerInsuranceList);
            if (travellerList != null && travellerList.Count != 0)
            {
                MTourOrderTravellerListXml = GetXmlByMTourOrderTraveller(travellerList, tourOrderExpand.OrderId);
            }
            if (travellerInsuranceList != null && travellerInsuranceList.Count != 0)
            {
                MTourOrderTravellerInsuranceList = GetXmlbyMTourOrderTravellerInsurance(travellerInsuranceList);
            }

            //垫付申请xml
            string AdvanceAppXml = string.Empty;
            if (tourOrderExpand.AdvanceApp != null)
            {
                AdvanceAppXml = CreateOverrunXml(tourOrderExpand.AdvanceApp);
            }

            //订单的上车地点
            string TourOrderCarLocationXml = string.Empty;
            if (tourOrderExpand.TourOrderCarLocation != null)
            {

                TourOrderCarLocationXml = CreateTourOrderCarLocationXml(tourOrderExpand.OrderId, tourOrderExpand.TourOrderCarLocation);
            }

            //订单选座
            string TourOrderCarTypeSeatXml = string.Empty;
            if (tourOrderExpand.TourOrderCarTypeSeatList != null && tourOrderExpand.TourOrderCarTypeSeatList.Count != 0)
            {
                TourOrderCarTypeSeatXml = CreateTourOrderCarTypeSeat(tourOrderExpand.OrderId, tourOrderExpand.TourOrderCarTypeSeatList);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_Sanpin");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, tourOrderExpand.OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, tourOrderExpand.OrderCode);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourOrderExpand.TourId);
            _db.AddInParameter(cmd, "BuyCountryId", DbType.Int32, tourOrderExpand.BuyCountryId);
            _db.AddInParameter(cmd, "BuyProvincesId", DbType.Int32, tourOrderExpand.BuyProvincesId);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, tourOrderExpand.BuyCompanyName);
            _db.AddInParameter(cmd, "BuyCompanyId", DbType.AnsiStringFixedLength, tourOrderExpand.BuyCompanyId);
            _db.AddInParameter(cmd, "ContactName", DbType.String, tourOrderExpand.ContactName);
            _db.AddInParameter(cmd, "ContactTel", DbType.String, tourOrderExpand.ContactTel);
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, tourOrderExpand.ContactDepartId);


            _db.AddInParameter(cmd, "SellerName", DbType.String, tourOrderExpand.SellerName);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, tourOrderExpand.SellerId);
            _db.AddInParameter(cmd, "DeptId", DbType.AnsiStringFixedLength, tourOrderExpand.DeptId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderExpand.Operator);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderExpand.OperatorId);

            _db.AddInParameter(cmd, "Adults", DbType.Int32, tourOrderExpand.Adults);
            _db.AddInParameter(cmd, "Childs", DbType.Int32, tourOrderExpand.Childs);
            _db.AddInParameter(cmd, "Others", DbType.Int32, tourOrderExpand.Others);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Currency, tourOrderExpand.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Currency, tourOrderExpand.ChildPrice);
            _db.AddInParameter(cmd, "OtherPrice", DbType.Currency, tourOrderExpand.OtherPrice);

            _db.AddInParameter(cmd, "SalerIncome", DbType.Currency, tourOrderExpand.SalerIncome);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, tourOrderExpand.GuideIncome);


            _db.AddInParameter(cmd, "PriceStandId", DbType.Int32, tourOrderExpand.PriceStandId);
            _db.AddInParameter(cmd, "LevId", DbType.Int32, tourOrderExpand.LevId);
            _db.AddInParameter(cmd, "PeerLevId", DbType.Int32, tourOrderExpand.PeerLevId);
            _db.AddInParameter(cmd, "PeerAdultPrice", DbType.Currency, tourOrderExpand.PeerAdultPrice);
            _db.AddInParameter(cmd, "PeerChildPrice", DbType.Currency, tourOrderExpand.PeerChildPrice);

            _db.AddInParameter(cmd, "SettlementMoney", DbType.Currency, tourOrderExpand.SettlementMoney);



            _db.AddInParameter(cmd, "SaleAddCost", DbType.Currency, tourOrderExpand.SaleAddCost);
            _db.AddInParameter(cmd, "SaleReduceCost", DbType.Currency, tourOrderExpand.SaleReduceCost);
            _db.AddInParameter(cmd, "SaleAddCostRemark", DbType.String, tourOrderExpand.SaleAddCostRemark);
            _db.AddInParameter(cmd, "SaleReduceCostRemark", DbType.String, tourOrderExpand.SaleReduceCostRemark);


            _db.AddInParameter(cmd, "SumPrice", DbType.Currency, tourOrderExpand.SumPrice);


            _db.AddInParameter(cmd, "OrderRemark", DbType.String, tourOrderExpand.OrderRemark);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, tourOrderExpand.SaveSeatDate);
            //_db.AddInParameter(cmd, "TourType", DbType.Byte, (int)tourOrderExpand.TourType);
            //_db.AddInParameter(cmd, "OrderType", DbType.Byte, (int)tourOrderExpand.OrderType);
            _db.AddInParameter(cmd, "Status", DbType.Byte, (int)tourOrderExpand.OrderStatus);

            _db.AddInParameter(cmd, "ContractId", DbType.AnsiStringFixedLength, tourOrderExpand.ContractId);//合同编号
            _db.AddInParameter(cmd, "ContractCode", DbType.String, tourOrderExpand.ContractCode);//合同号
            _db.AddInParameter(cmd, "TravellerFile", DbType.String, tourOrderExpand.TravellerFile);//游客信息附件


            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, TourOrderChangeXml);//订单变更的xml
            _db.AddInParameter(cmd, "TourOrderTraveller", DbType.String, MTourOrderTravellerListXml);//订单游客的xml
            _db.AddInParameter(cmd, "TourOrderTravellerInsurance", DbType.String, MTourOrderTravellerInsuranceList);//订单游客保险的xml

            _db.AddInParameter(cmd, "AdvanceApp", DbType.String, AdvanceAppXml);

            _db.AddInParameter(cmd, "TourOrderCarLocation", DbType.String, TourOrderCarLocationXml);
            _db.AddInParameter(cmd, "TourOrderCarTypeSeat", DbType.String, TourOrderCarTypeSeatXml);

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            // DbHelper.RunProcedureWithResult(cmd, _db);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (dr.Read())
                {
                    MTourOrderCarTypeSeat seat = new MTourOrderCarTypeSeat()
                    {

                        TourCarTypeId = dr["TourCarTypeId"].ToString(),
                        SeatNumber = dr["SeatNumber"].ToString(),
                        OrderId = tourOrderExpand.OrderId
                    };
                    list.Add(seat);
                }
            }

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        #endregion

        #region 修改_组团订单
        /// <summary>
        /// 修改、变更订单信息(用于计划订单)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="list">游客集合</param>
        /// <returns>
        /// 0:修改失败
        /// 1:修改成功
        /// </returns>
        public int UpdateTourOrderExpand(string orderId, IList<MTourOrderTraveller> list)
        {

            //获取游客信息的xml
            string MTourOrderTravellerListXml = string.Empty;

            //游客保险的xml
            string MTourOrderTravellerInsuranceList = string.Empty;
            //游客保险
            IList<MTourOrderTravellerInsurance> travellerInsuranceList = new List<MTourOrderTravellerInsurance>();

            IList<MTourOrderTraveller> travellerList = GetMTourOrderTravellerList(list, orderId, ref  travellerInsuranceList);


            if (travellerList != null && travellerList.Count != 0)
            {
                MTourOrderTravellerListXml = GetXmlByMTourOrderTraveller(travellerList, orderId);
            }
            if (travellerInsuranceList != null && travellerInsuranceList.Count != 0)
            {
                MTourOrderTravellerInsuranceList = GetXmlbyMTourOrderTravellerInsurance(travellerInsuranceList);
            }


            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            _db.AddInParameter(cmd, "TourOrderTraveller", DbType.String, MTourOrderTravellerListXml);
            _db.AddInParameter(cmd, "TourOrderTravellerInsurance", DbType.String, MTourOrderTravellerInsuranceList);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, _db);

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region 修改订单状态
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="change">订单变更的实体</param>
        /// <returns>0：失败 1：成功</returns>
        public int UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, MTourOrderChange change)
        {
            string changeXml = string.Empty;
            if (null != change)
            {
                changeXml = GetTourOrderChangeXml(change);
            }
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_OrderStatus");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            _db.AddInParameter(cmd, "OrderStatus", DbType.Byte, (int)orderStatus);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, null);
            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, changeXml);
            _db.AddInParameter(cmd, "MPlanBaseInfo", DbType.String, null);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 0);

        }
        #endregion

        #region 修改订单状态
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="saveSeatDate">继续留位时间</param>
        /// <param name="change">订单变更的实体</param>
        /// <param name="plan">计调信息</param>
        /// <returns>0：失败 1：成功</returns>
        public int UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, DateTime? saveSeatDate, MTourOrderChange change, EyouSoft.Model.PlanStructure.MPlanBaseInfo plan)
        {
            string changeXml = string.Empty;
            if (null != change)
            {
                changeXml = GetTourOrderChangeXml(change);
            }

            string planXml = string.Empty;
            if (null != plan)
            {
                planXml = CreatePlanBaseInfoXml(plan);
            }
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_OrderStatus");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            _db.AddInParameter(cmd, "OrderStatus", DbType.Byte, (int)orderStatus);
            _db.AddInParameter(cmd, "SaveSeatDate", DbType.DateTime, saveSeatDate);
            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, changeXml);
            _db.AddInParameter(cmd, "MPlanBaseInfo", DbType.String, planXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 0);

        }
        #endregion

        #region 修改订单确认金额
        /// <summary>
        /// 修改订单确认金额
        /// </summary>
        /// <param name="orderSale">团队确认单</param>
        /// <param name="orderChange">订单变更</param>
        /// <returns> 0:修改失败 1:修改成功</returns>
        public int UpdateOrderSettlement(MOrderConfirm orderChange)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_ConfirmMoney");
            _db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, orderChange.OrderId);
            _db.AddInParameter(cmd, "@ConfirmPeopleDeptId", DbType.Int32, orderChange.ConfirmPeopleDeptId);
            _db.AddInParameter(cmd, "@ConfirmPeopleId", DbType.AnsiStringFixedLength, orderChange.ConfirmPeopleId);
            _db.AddInParameter(cmd, "@ConfirmPeople", DbType.String, orderChange.ConfirmPeople);
            _db.AddInParameter(cmd, "@ConfirmMoney", DbType.Currency, orderChange.ConfirmMoney);
            _db.AddInParameter(cmd, "@ConfirmMoneyStatus", DbType.AnsiStringFixedLength, orderChange.ConfirmMoneyStatus ? "1" : "0");
            _db.AddInParameter(cmd, "@ConfirmRemark", DbType.String, orderChange.ConfirmRemark);
            _db.AddInParameter(cmd, "@ChangeInfo", DbType.String, GetOrderChangeXml(orderChange.ChangeInfo));
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, _db);

            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region 修改订单的结算金额
        /// <summary>
        /// 修改订单的结算单
        /// </summary>
        /// <param name="orderSettlement"></param>
        /// <param name="orderChange"></param>
        /// <returns>1:成功 0:失败</returns>
        public int UpdateOrderSettlement(MOrderSettlement orderSettlement, MTourOrderChange orderChange)
        {
            string changeXml = string.Empty;
            if (orderChange != null)
            {
                changeXml = GetTourOrderChangeXml(orderChange);
            }
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Update_SettlementMoney");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderSettlement.OrderId);
            _db.AddInParameter(cmd, "PeerAddCost", DbType.Currency, orderSettlement.PeerAddCost);
            _db.AddInParameter(cmd, "PeerAddCostRemark", DbType.String, orderSettlement.PeerAddCostRemark);
            _db.AddInParameter(cmd, "PeerReduceCost", DbType.Currency, orderSettlement.PeerReduceCost);
            _db.AddInParameter(cmd, "PeerReduceCostRemark", DbType.String, orderSettlement.PeerReduceCostRemark);
            _db.AddInParameter(cmd, "SettlementPeople", DbType.String, orderSettlement.SettlementPeople);
            _db.AddInParameter(cmd, "SettlementPeopleId", DbType.AnsiStringFixedLength, orderSettlement.SettlementPeopleId);
            _db.AddInParameter(cmd, "ConfirmSettlementMoney", DbType.Currency, orderSettlement.ConfirmSettlementMoney);
            _db.AddInParameter(cmd, "Profit", DbType.Currency, orderSettlement.Profit);
            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, changeXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString());

        }
        #endregion

        #region 获取订单信息游客信息、游客保险
        /// <summary>
        /// 根据订单编号获取订单、游客、游客保险信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单、游客集合、保险</returns>
        public MTourOrderExpand GetTourOrderExpandByOrderId(string orderId)
        {
            MTourOrderExpand order = null;
            string sql = string.Format("SELECT * From view_TourOrderExpand where OrderId='{0}' ", orderId);


            DbCommand cmd = _db.GetSqlStringCommand(sql);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr.Read())
                {
                    order = new MTourOrderExpand();
                    order.MTourOrderTravellerList = new List<MTourOrderTraveller>();
                    order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                    order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                    order.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    order.BuyCountryId = !dr.IsDBNull(dr.GetOrdinal("BuyCountryId")) ? dr.GetInt32(dr.GetOrdinal("BuyCountryId")) : 0;
                    order.BuyProvincesId = !dr.IsDBNull(dr.GetOrdinal("BuyProvincesId")) ? dr.GetInt32(dr.GetOrdinal("BuyProvincesId")) : 0;
                    order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    order.BuyCompanyId = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyId")) ? dr.GetString(dr.GetOrdinal("BuyCompanyId")) : string.Empty;
                    order.ContactName = !dr.IsDBNull(dr.GetOrdinal("ContactName")) ? dr.GetString(dr.GetOrdinal("ContactName")) : string.Empty;
                    order.ContactTel = !dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? dr.GetString(dr.GetOrdinal("ContactTel")) : string.Empty;

                    order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    order.SellerId = dr["SellerId"].ToString();
                    order.DeptId = !dr.IsDBNull(dr.GetOrdinal("SellerDeptId")) ? dr.GetInt32(dr.GetOrdinal("SellerDeptId")) : 0;

                    order.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                    order.OperatorId = dr["OperatorId"].ToString();

                    order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                    order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                    order.Others = dr.GetInt32(dr.GetOrdinal("Leaders"));
                    order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                    order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                    order.OtherPrice = dr.GetDecimal(dr.GetOrdinal("LeaderPrice"));

                    order.PriceStandId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                    order.LevId = dr.GetInt32(dr.GetOrdinal("LevId"));

                    order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                    order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                    order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;

                    order.SaleAddCost = dr.GetDecimal(dr.GetOrdinal("SaleAddCost"));
                    order.SaleAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleAddCostRemark")) : string.Empty;
                    order.SaleReduceCost = dr.GetDecimal(dr.GetOrdinal("SaleReduceCost"));
                    order.SaleReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleReduceCostRemark")) : string.Empty;

                    order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));

                    order.SaveSeatDate = dr.IsDBNull(dr.GetOrdinal("SaveSeatDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("SaveSeatDate"));
                    order.OrderRemark = !dr.IsDBNull(dr.GetOrdinal("OrderRemark")) ? dr.GetString(dr.GetOrdinal("OrderRemark")) : string.Empty;

                    order.GuideRealIncome = dr.GetDecimal(dr.GetOrdinal("GuideRealIncome"));
                    order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                    order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));

                    order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                    order.ConfirmMoneyStatus = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1" ? true : false;
                    order.ConfirmRemark = !dr.IsDBNull(dr.GetOrdinal("ConfirmRemark")) ? dr.GetString(dr.GetOrdinal("ConfirmRemark")) : string.Empty;

                    order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("OrderStatus"));

                    order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    order.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                    order.ReturnMoney = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));
                    order.ReceivedMoney = dr.GetDecimal(dr.GetOrdinal("ReceivedMoney"));

                    string TourOrderTraveller = dr.IsDBNull(dr.GetOrdinal("TourOrderTraveller")) == false ? dr.GetString(dr.GetOrdinal("TourOrderTraveller")) : string.Empty;
                    if (!string.IsNullOrEmpty(TourOrderTraveller))
                    {
                        order.MTourOrderTravellerList = GetTourOrderTravellerXml(TourOrderTraveller);
                    }

                    //计划信息
                    order.TourSellerId = dr.GetString(dr.GetOrdinal("TourSellerId"));
                    order.TourSellerName = dr.GetOrdinal("TourSellerName").ToString();

                }
            }

            return order;
        }
        #endregion

        #region 获取订单金额_杂费收入的相关金额
        /// <summary>
        /// 获取订单的金额
        /// </summary>
        /// <param name="orderId"></param>
        public OrderMoney GetOrderMoney(string orderId)
        {
            OrderMoney order = null;
            StringBuilder query = new StringBuilder();
            query.Append(" Select ConfirmMoney,CheckMoney,ReturnMoney,ReceivedMoney,");
            query.Append(" (Select isnull(sum(CollectionRefundAmount),0) from tbl_TourOrderSales ");
            query.Append(" where CollectionRefundState='1' and tbl_TourOrderSales.OrderId=tbl_TourOrder.OrderId) as BackMoney,");
            //query.Append(" (select isnull(sum(BillAmount),0) from tbl_FinBill ");
            //query.Append(" where IsApprove='1' and tbl_FinBill.OrderId=tbl_TourOrder.OrderId) as BillAmount,");
            //query.Append(" (select isnull(sum(BillAmount),0) from tbl_FinBill ");
            //query.Append(" where tbl_FinBill.OrderId=tbl_TourOrder.OrderId) as BillMoney");
            query.Append(" ConfirmMoneyStatus,BuyCompanyName,IssueTime,OrderCode ");
            query.Append(" from tbl_TourOrder");
            query.AppendFormat(" where OrderId='{0}' ", orderId);
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr.Read())
                {
                    order = new OrderMoney();
                    order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                    order.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                    order.ReturnMoney = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));
                    order.ReceivedMoney = dr.GetDecimal(dr.GetOrdinal("ReceivedMoney"));
                    order.BackMoney = dr.GetDecimal(dr.GetOrdinal("BackMoney"));
                    //order.BillAmount = dr.GetDecimal(dr.GetOrdinal("BillAmount"));
                    //order.BillMoeny = dr.GetDecimal(dr.GetOrdinal("BillMoney"));
                    order.IsConfirm = GetBoolean(dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")));
                    order.OrderCode = dr["OrderCode"].ToString();
                    order.BuyCompanyName = dr["BuyCompanyName"].ToString();
                    order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }
            return order;
        }


        /// <summary>
        /// 杂费收入的金额、开票金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderMoney GetFinOtherInFeeMoney(string id)
        {
            OrderMoney order = null;
            StringBuilder query = new StringBuilder();
            query.Append(" select FeeAmount,");
            query.Append(" (select isnull(sum(BillAmount),0) from tbl_FinBill ");
            query.Append(" where tbl_FinBill.OrderId=cast(tbl_FinOtherInFee.id as char(36)) and IsApprove='1') as BillAmount");
            query.AppendFormat(" from tbl_FinOtherInFee where id={0}", Utils.GetInt(id));

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr.Read())
                {
                    order = new OrderMoney();
                    order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("FeeAmount"));
                    order.BillAmount = dr.GetDecimal(dr.GetOrdinal("BillAmount"));
                }
            }
            return order;

        }

        #endregion

        #region 根据团队编号获取订单集合
        /// <summary>
        /// 根据团队编号获取订单列表（散拼订单查看多个订单、组团查询单个组团订单）
        /// </summary>
        /// <param name="tourId">团队编号</param>
        ///<param name="orderSum">统计的实体</param>
        /// <returns></returns>
        public IList<MTourOrder> GetTourOrderListById(string tourId, ref MOrderSum orderSum)
        {
            IList<MTourOrder> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT *");
            //query.Append(",(SELECT FinStatus FROM dbo.tbl_TourOrderSales WHERE OrderId=tbl_TourOrder.OrderId AND CollectionRefundState=0 AND IsGuideRealIncome=1) as FinStatus ");
            query.Append(" FROM ");
            query.Append(" tbl_TourOrder ");
            query.Append(" Where ");
            query.AppendFormat(" TourId='{0}'", tourId);

            //排序
            query.Append(" order By IssueTime desc ");

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrder>();

                    while (dr.Read())
                    {
                        MTourOrder order = new MTourOrder();
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.BuyCountryId = !dr.IsDBNull(dr.GetOrdinal("BuyCountryId")) ? dr.GetInt32(dr.GetOrdinal("BuyCountryId")) : 0;
                        order.BuyProvincesId = !dr.IsDBNull(dr.GetOrdinal("BuyProvincesId")) ? dr.GetInt32(dr.GetOrdinal("BuyProvincesId")) : 0;
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.BuyCompanyId = dr["BuyCompanyId"].ToString();
                        order.ContactDepartId = !dr.IsDBNull(dr.GetOrdinal("ContactId")) ? dr.GetString(dr.GetOrdinal("ContactId")) : string.Empty;
                        order.ContactName = !dr.IsDBNull(dr.GetOrdinal("ContactName")) ? dr.GetString(dr.GetOrdinal("ContactName")) : string.Empty;
                        order.ContactTel = !dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? dr.GetString(dr.GetOrdinal("ContactTel")) : string.Empty;
                        //order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                        //order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                        //order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;

                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                        order.SellerId = dr["SellerId"].ToString();
                        order.DeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));

                        order.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                        order.OperatorId = dr["OperatorId"].ToString();

                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.Others = dr.GetInt32(dr.GetOrdinal("Leaders"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                        order.OtherPrice = dr.GetDecimal(dr.GetOrdinal("LeaderPrice"));
                        order.SingleRoomPrice = dr.GetDecimal(dr.GetOrdinal("SingleRoomPrice"));

                        order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                        order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                        order.GuideRealIncome = dr.GetDecimal(dr.GetOrdinal("GuideRealIncome"));
                        order.GuideRemark = !dr.IsDBNull(dr.GetOrdinal("GuideRemark")) ? dr.GetString(dr.GetOrdinal("GuideRemark")) : string.Empty;

                        //order.PriceStandId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                        //order.LevId = dr.GetInt32(dr.GetOrdinal("LevId"));
                        //order.PeerLevId = dr.GetInt32(dr.GetOrdinal("PeerLevId"));

                        //order.PeerAdultPrice = dr.GetDecimal(dr.GetOrdinal("PeerAdultPrice"));
                        //order.PeerChildPrice = dr.GetDecimal(dr.GetOrdinal("PeerChildPrice"));
                        //order.PeerAddCost = dr.GetDecimal(dr.GetOrdinal("PeerAddCost"));
                        //order.PeerReduceCost = dr.GetDecimal(dr.GetOrdinal("PeerReduceCost"));
                        //order.PeerAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerAddCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerAddCostRemark")) : string.Empty;
                        //order.PeerReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerReduceCostRemark")) : string.Empty;
                        //order.SettlementMoney = dr.GetDecimal(dr.GetOrdinal("SettlementMoney"));
                        order.ConfirmSettlementMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        //order.SettlementPeople = !dr.IsDBNull(dr.GetOrdinal("SettlementPeople")) ? dr.GetString(dr.GetOrdinal("SettlementPeople")) : string.Empty;
                        //order.SettlementPeopleId = !dr.IsDBNull(dr.GetOrdinal("SettlementPeopleId")) ? dr.GetString(dr.GetOrdinal("SettlementPeopleId")) : string.Empty;

                        //order.SaleAddCost = dr.GetDecimal(dr.GetOrdinal("SaleAddCost"));
                        //order.SaleReduceCost = dr.GetDecimal(dr.GetOrdinal("SaleReduceCost"));
                        //order.SaleAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleAddCostRemark")) : string.Empty;
                        //order.SaleReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleReduceCostRemark")) : string.Empty;

                        //order.OtherCost = dr.GetDecimal(dr.GetOrdinal("OtherCost"));

                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        //order.SumPriceAddCost = dr.GetDecimal(dr.GetOrdinal("SumPriceAddCost"));
                        //order.SumPriceAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceAddCostRemark")) : string.Empty;
                        //order.SumPriceReduceCost = dr.GetDecimal(dr.GetOrdinal("SumPriceReduceCost"));
                        //order.SumPriceReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceReduceCostRemark")) : string.Empty;
                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.ConfirmMoneyStatus = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1" ? true : false;
                        order.ConfirmRemark = !dr.IsDBNull(dr.GetOrdinal("ConfirmRemark")) ? dr.GetString(dr.GetOrdinal("ConfirmRemark")) : string.Empty;
                        order.ConfirmPeople = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeople")) ? dr.GetString(dr.GetOrdinal("ConfirmPeople")) : string.Empty;
                        order.ConfirmPeopleId = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeopleId")) ? dr.GetString(dr.GetOrdinal("ConfirmPeopleId")) : string.Empty;

                        //order.Profit = order.ConfirmMoney - order.ConfirmSettlementMoney;

                        //order.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                        //order.OrderType = (OrderType)dr.GetByte(dr.GetOrdinal("OrderType"));
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("OrderStatus"));


                        //order.OrderRemark = !dr.IsDBNull(dr.GetOrdinal("OrderRemark")) ? dr.GetString(dr.GetOrdinal("OrderRemark")) : string.Empty;
                        //order.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : string.Empty;
                        //order.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : string.Empty;
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        //order.PayMentMonth = !dr.IsDBNull(dr.GetOrdinal("PayMentMonth")) ? dr.GetString(dr.GetOrdinal("PayMentMonth")) : string.Empty;
                        //order.PayMentDay = !dr.IsDBNull(dr.GetOrdinal("PayMentDay")) ? dr.GetString(dr.GetOrdinal("PayMentDay")) : string.Empty;
                        //order.PayMentAccountId = !dr.IsDBNull(dr.GetOrdinal("PayMentAccountId")) ? dr.GetInt32(dr.GetOrdinal("PayMentAccountId")) : 0;

                        order.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                        order.ReturnMoney = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));
                        order.ReceivedMoney = dr.GetDecimal(dr.GetOrdinal("ReceivedMoney"));
                        //order.UnSeat = dr.GetInt32(dr.GetOrdinal("UnSeat"));//散拼短线订单未安排座位的人数

                        //统计
                        orderSum.Adults += order.Adults;
                        orderSum.Childs += order.Childs;
                        orderSum.Others += order.Others;
                        orderSum.SalerIncome += order.SalerIncome;
                        orderSum.GuideIncome += order.GuideIncome;
                        orderSum.GuideRealIncome += order.GuideRealIncome;
                        orderSum.SumPrice += order.SumPrice;
                        orderSum.ConfirmMoney += order.ConfirmMoney;
                        orderSum.ConfirmSettlementMoney += order.ConfirmSettlementMoney;
                        orderSum.CheckMoney += order.CheckMoney;
                        orderSum.Profit += order.ConfirmMoney - order.ConfirmSettlementMoney;
                        list.Add(order);
                    }


                }
            }

            return list;

        }
        #endregion

        #region 根据团队编号获取订单集合的分页列表
        /// <summary>
        /// 根据团队编号获取订单集合的分页列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="tourId">团队编号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="orderSum">统计的实体</param>
        /// <returns></returns>
        public IList<MTourOrder> GetTourOrderListById(int pageSize, int pageIndex, string tourId, ref int recordCount, ref MOrderSum orderSum)
        {
            IList<MTourOrder> list = null;
            //查询的字段
            StringBuilder filed = new StringBuilder();
            filed.Append("OrderId,OrderCode,CompanyId,TourId,BuyCountryId,BuyProvincesId");
            filed.Append(",BuyCompanyName,BuyCompanyId,ContactDepartId,ContactName,ContactTel");
            filed.Append(",DCompanyName,DContactName,DContactTel,SellerName,SellerId,DeptId");
            filed.Append(",Operator,OperatorId,Adults,Childs,Others,AdultPrice,ChildPrice");
            filed.Append(",OtherPrice,PriceStandId,LevId,PeerAdultPrice,PeerChildPrice");
            filed.Append(",SettlementMoney,ConfirmSettlementMoney,SettlementPeople,SettlementPeopleId");
            filed.Append(",SaleAddCost,SaleReduceCost,SaleAddCostRemark,SaleReduceCostRemark");
            filed.Append(",PeerAddCost,PeerReduceCost,PeerAddCostRemark,PeerReduceCostRemark,OtherCost");
            filed.Append(",SumPrice,SumPriceAddCost,SumPriceReduceCost,SumPriceAddCostRemark,SumPriceReduceCostRemark");
            filed.Append(",ConfirmMoney,ConfirmMoneyStatus,ConfirmPeople,ConfirmPeopleId,ConfirmRemark");
            filed.Append(",Profit,SalerIncome,GuideIncome,GuideRealIncome,GuideRemark");
            filed.Append(",OrderRemark,SaveSeatDate");
            filed.Append(",TourType,OrderType,Status,IssueTime");
            filed.Append(",CheckMoney,ReturnMoney,ReceivedMoney");
            filed.Append(",TheNum,ContractId,ContractCode,TravellerFile");
            filed.Append(",PayMentMonth,PayMentDay,PayMentAccountId");
            //表名
            string table = "tbl_TourOrder";
            //查询条件
            //string query = string.Format("TourId='{0}' AND IsDelete='0'", tourId);
            StringBuilder query = new StringBuilder();
            query.AppendFormat("TourId='{0}' AND IsDelete='0' and guideincome=0 and OrderId not in (select OrderId from tbl_TourOrderSales where IsGuideRealIncome='1')", tourId);

            //query.Append(NotExitsOrderStatus());

            StringBuilder sumfield = new StringBuilder();
            sumfield.Append(" SELECT ");
            sumfield.Append("isnull(sum(Adults),0) as Adults,");
            sumfield.Append("isnull(sum(Childs),0) as Childs,");
            sumfield.Append("isnull(sum(Others),0) as Others,");
            sumfield.Append("isnull(sum(SumPrice),0) as SumPrice,");
            sumfield.Append("isnull(sum(ConfirmMoney),0) as ConfirmMoney,");
            sumfield.Append("isnull(sum(ConfirmSettlementMoney),0) as ConfirmSettlementMoney,");
            sumfield.Append("isnull(sum(GuideIncome),0) as GuideIncome,");
            sumfield.Append("isnull(sum(GuideRealIncome),0) as GuideRealIncome,");
            sumfield.Append("isnull(sum(SalerIncome),0) as SalerIncome,");
            sumfield.Append("isnull(sum(CheckMoney),0) as CheckMoney");
            sumfield.Append(" FROM ");
            sumfield.Append("tbl_TourOrder");
            //sumfield.Append(" AND ");
            //sumfield.AppendFormat("TourId='{0}' AND IsDelete='0'", tourId);

            string xmlSum = string.Empty;//ref 合计的xml字段
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, ref xmlSum
              , table, sumfield.ToString(), filed.ToString(), query.ToString(), "IssueTime DESC"))
            {
                if (dr != null)
                {
                    list = new List<MTourOrder>();
                    //订单统计
                    if (!string.IsNullOrEmpty(xmlSum))
                    {
                        orderSum = GetOrderSumByXml(xmlSum);
                    }
                    while (dr.Read())
                    {
                        MTourOrder order = new MTourOrder();
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.BuyCountryId = !dr.IsDBNull(dr.GetOrdinal("BuyCountryId")) ? dr.GetInt32(dr.GetOrdinal("BuyCountryId")) : 0;
                        order.BuyProvincesId = !dr.IsDBNull(dr.GetOrdinal("BuyProvincesId")) ? dr.GetInt32(dr.GetOrdinal("BuyProvincesId")) : 0;
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.BuyCompanyId = dr["BuyCompanyId"].ToString();
                        order.ContactDepartId = !dr.IsDBNull(dr.GetOrdinal("ContactDepartId")) ? dr.GetString(dr.GetOrdinal("ContactDepartId")) : string.Empty;
                        order.ContactName = !dr.IsDBNull(dr.GetOrdinal("ContactName")) ? dr.GetString(dr.GetOrdinal("ContactName")) : string.Empty;
                        order.ContactTel = !dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? dr.GetString(dr.GetOrdinal("ContactTel")) : string.Empty;
                        order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                        order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                        order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;

                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                        order.SellerId = dr["SellerId"].ToString();
                        order.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));

                        order.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                        order.OperatorId = dr["OperatorId"].ToString();

                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.Others = dr.GetInt32(dr.GetOrdinal("Others"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                        order.OtherPrice = dr.GetDecimal(dr.GetOrdinal("OtherPrice"));

                        order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                        order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                        order.GuideRealIncome = dr.GetDecimal(dr.GetOrdinal("GuideRealIncome"));
                        order.GuideRemark = !dr.IsDBNull(dr.GetOrdinal("GuideRemark")) ? dr.GetString(dr.GetOrdinal("GuideRemark")) : string.Empty;

                        order.PriceStandId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                        order.LevId = dr.GetInt32(dr.GetOrdinal("LevId"));

                        order.PeerAdultPrice = dr.GetDecimal(dr.GetOrdinal("PeerAdultPrice"));
                        order.PeerChildPrice = dr.GetDecimal(dr.GetOrdinal("PeerChildPrice"));
                        order.PeerAddCost = dr.GetDecimal(dr.GetOrdinal("PeerAddCost"));
                        order.PeerReduceCost = dr.GetDecimal(dr.GetOrdinal("PeerReduceCost"));
                        order.PeerAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerAddCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerAddCostRemark")) : string.Empty;
                        order.PeerReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerReduceCostRemark")) : string.Empty;
                        order.SettlementMoney = dr.GetDecimal(dr.GetOrdinal("SettlementMoney"));
                        order.ConfirmSettlementMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmSettlementMoney"));
                        order.SettlementPeople = !dr.IsDBNull(dr.GetOrdinal("SettlementPeople")) ? dr.GetString(dr.GetOrdinal("SettlementPeople")) : string.Empty;
                        order.SettlementPeopleId = !dr.IsDBNull(dr.GetOrdinal("SettlementPeopleId")) ? dr.GetString(dr.GetOrdinal("SettlementPeopleId")) : string.Empty;

                        order.SaleAddCost = dr.GetDecimal(dr.GetOrdinal("SaleAddCost"));
                        order.SaleReduceCost = dr.GetDecimal(dr.GetOrdinal("SaleReduceCost"));
                        order.SaleAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleAddCostRemark")) : string.Empty;
                        order.SaleReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleReduceCostRemark")) : string.Empty;

                        order.OtherCost = dr.GetDecimal(dr.GetOrdinal("OtherCost"));

                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        order.SumPriceAddCost = dr.GetDecimal(dr.GetOrdinal("SumPriceAddCost"));
                        order.SumPriceAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceAddCostRemark")) : string.Empty;
                        order.SumPriceReduceCost = dr.GetDecimal(dr.GetOrdinal("SumPriceReduceCost"));
                        order.SumPriceReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceReduceCostRemark")) : string.Empty;
                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.ConfirmMoneyStatus = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1" ? true : false;
                        order.ConfirmRemark = !dr.IsDBNull(dr.GetOrdinal("ConfirmRemark")) ? dr.GetString(dr.GetOrdinal("ConfirmRemark")) : string.Empty;
                        order.ConfirmPeople = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeople")) ? dr.GetString(dr.GetOrdinal("ConfirmPeople")) : string.Empty;
                        order.ConfirmPeopleId = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeopleId")) ? dr.GetString(dr.GetOrdinal("ConfirmPeopleId")) : string.Empty;


                        // order.Profit = dr.GetDecimal(dr.GetOrdinal("Profit"));
                        order.Profit = order.ConfirmMoney - order.ConfirmSettlementMoney;

                        order.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                        order.OrderType = (OrderType)dr.GetByte(dr.GetOrdinal("OrderType"));
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status"));


                        order.OrderRemark = !dr.IsDBNull(dr.GetOrdinal("OrderRemark")) ? dr.GetString(dr.GetOrdinal("OrderRemark")) : string.Empty;
                        order.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : string.Empty;
                        order.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : string.Empty;
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        order.PayMentMonth = !dr.IsDBNull(dr.GetOrdinal("PayMentMonth")) ? dr.GetString(dr.GetOrdinal("PayMentMonth")) : string.Empty;
                        order.PayMentDay = !dr.IsDBNull(dr.GetOrdinal("PayMentDay")) ? dr.GetString(dr.GetOrdinal("PayMentDay")) : string.Empty;
                        order.PayMentAccountId = !dr.IsDBNull(dr.GetOrdinal("PayMentAccountId")) ? dr.GetInt32(dr.GetOrdinal("PayMentAccountId")) : 0;

                        order.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                        order.ReturnMoney = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));
                        order.ReceivedMoney = dr.GetDecimal(dr.GetOrdinal("ReceivedMoney"));
                        list.Add(order);
                    }
                }
            }



            return list;
        }
        #endregion

        #region 同业分销_订单中心列表
        /// <summary>
        /// 同业分销_订单中心列表
        /// </summary>
        /// <param name="searchOrderCenter">查询类</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <param name="loginId">当前登陆员</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="isOnlySeft">权限</param>
        /// <returns></returns>
        public IList<MTradeOrder> GetTourOrderList(
            MSearchOrderCenter searchOrderCenter,
            int pageSize,
            int pageIndex,
            ref int recordCount,
            string loginId,
            int[] deptIds,
            bool isOnlySeft)
        {
            IList<EyouSoft.Model.TourStructure.MTradeOrder> list = null;

            StringBuilder fileds = new StringBuilder();
            fileds.Append("TourId,TourCode,RouteName,LDate,IsChange,ChangeState,CompanyId,OrderId,OrderCode,");
            fileds.Append("IssueTime,AdultPrice,Childs,Adults,SumPrice,ContactName,Status,OSellerId,OSellerName,");
            fileds.Append("ODeptId,OperatorId,Operator,IsShowDistribution,IsDelete,TourType,PeerAdultPrice,");
            fileds.Append("BuyCompanyName,TSellerName,TSellerId,TDeptId,ConfirmMoney");
            fileds.Append(" ,PriceStandId,LevId,ConfirmMoneyStatus ");

            StringBuilder query = new StringBuilder();

            #region SQL
            // query.Append("  IsDelete='0' AND TourType IN(0,1,2,3,4,5) ");
            //query.AppendFormat("  IsDelete='0' AND TourType<>{0} ", (int)TourType.单项服务);

            if (!string.IsNullOrEmpty(searchOrderCenter.CompanyId))
            {
                query.AppendFormat(" and CompanyId='{0}' ", searchOrderCenter.CompanyId);//公司编号
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.OrderCode))
            {
                query.AppendFormat(" and OrderCode like '%{0}%' ", searchOrderCenter.OrderCode);//订单号
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.TourCode))
            {
                query.AppendFormat(" and TourCode like '%{0}%' ", searchOrderCenter.TourCode);//团号
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.RouteName))
            {
                query.AppendFormat(" and RouteName like  '%{0}%' ", searchOrderCenter.RouteName);//线路名称
            }

            if (searchOrderCenter.OrderIssueBeginTime.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',IssueTime)>=0 ", searchOrderCenter.OrderIssueBeginTime.Value);//下单开始时间
            }

            if (searchOrderCenter.OrderIssueEndTime.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0 ", searchOrderCenter.OrderIssueEndTime.Value);//下单结束时间
            }

            if (searchOrderCenter.LeaveBeginTime.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',LDate)>=0 ", searchOrderCenter.LeaveBeginTime.Value);//出团开始时间
            }

            if (searchOrderCenter.LeaveEndTime.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',LDate)<=0 ", searchOrderCenter.LeaveEndTime.Value);//出团结束时间
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.SellerName))
            {
                query.AppendFormat(" and OSellerName='{0}'", searchOrderCenter.SellerName);//销售员
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.SellerId))
            {
                query.AppendFormat(" and OSellerId='{0}' ", searchOrderCenter.SellerId);//销售员
            }

            if (searchOrderCenter.OrderTypeBySearch == OrderTypeBySearch.全部订单)
            {
                //默认，单独控制一个权限不包括单项业务的订单
                //query.AppendFormat(" and TourType<>{0}", (int)TourType.单项服务);

                if (isOnlySeft)
                {
                    query.AppendFormat(" and ( TSellerId='{0}' or OSellerId='{0}' or OperatorId='{0}')", loginId);
                }
                else
                {
                    //权限
                    if (deptIds != null)
                    {
                        query.Append(" And ");
                        query.Append("(");
                        query.AppendFormat(" TDeptId in ({0}) ", GetIdsByArr(deptIds));
                        query.Append(" OR ");
                        query.AppendFormat(" ODeptId in ({0}) ", GetIdsByArr(deptIds));
                        query.Append(")");
                    }
                }
            }

            if (searchOrderCenter.OrderTypeBySearch == OrderTypeBySearch.我销售的订单)
            {
                //只控制订单的销售员
                query.AppendFormat(" and OSellerId='{0}' ", loginId);
            }

            if (searchOrderCenter.OrderTypeBySearch == OrderTypeBySearch.我操作的订单)
            {
                //只控制订单的操作员
                query.AppendFormat(" and OperatorId='{0}' ", loginId);
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.XiaDanRenId))
            {
                query.AppendFormat(" AND OperatorId='{0}' ", searchOrderCenter.XiaDanRenId);
            }
            else if (!string.IsNullOrEmpty(searchOrderCenter.XiaDanRenName))
            {
                query.AppendFormat(" AND Operator LIKE'%{0}%' ", searchOrderCenter.XiaDanRenName);
            }

            if (searchOrderCenter.OrderStatus != null && searchOrderCenter.OrderStatus.Length > 0)
            {
                query.AppendFormat(" AND Status IN({0}) ", Utils.GetSqlIn<OrderStatus>(searchOrderCenter.OrderStatus));
            }

            if (!string.IsNullOrEmpty(searchOrderCenter.CrmId))
            {
                query.AppendFormat(" AND BuyCompanyId='{0}' ", searchOrderCenter.CrmId);
            }
            else if (!string.IsNullOrEmpty(searchOrderCenter.CrmName))
            {
                query.AppendFormat(" AND BuyCompanyName LIKE '%{0}%' ", searchOrderCenter.CrmName);
            }
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(_db
                , pageSize
                , pageIndex
                , ref recordCount
                , "view_TourOrder"
                , "TourId"
                , fileds.ToString()
                , query.ToString()
                , "IssueTime desc"))
            {
                if (dr != null)
                {
                    list = new List<MTradeOrder>();

                    while (dr.Read())
                    {
                        MTradeOrder order = new MTradeOrder();
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                        order.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                        order.IsTourChange = dr.GetInt32(dr.GetOrdinal("IsChange")) == 1;
                        order.ChangeState = dr.GetInt32(dr.GetOrdinal("ChangeState")) == 1;
                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("OSellerName")) ? dr.GetString(dr.GetOrdinal("OSellerName")) : string.Empty;
                        order.SellerId = dr.GetString(dr.GetOrdinal("OSellerId"));
                        order.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                        order.Operator = dr.GetString(dr.GetOrdinal("Operator"));
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.PeerAdultPrice = dr.GetDecimal(dr.GetOrdinal("PeerAdultPrice"));
                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        order.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status"));
                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.KeHuLevId = dr.GetInt32(dr.GetOrdinal("LevId"));
                        order.BaoJiaBiaoZhunId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                        order.IsQueRenHeTongJinE = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1";

                        list.Add(order);
                    }
                }
            }
            return list;

        }
        #endregion

        #region 订单汇总信息（同业分销—订单中心）
        /// <summary>
        /// 根据计划编号获取订单汇总信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<MTourOrderSummary> GetTourOrderSummaryByTourId(string tourId)
        {
            IList<MTourOrderSummary> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("OrderId,OrderCode,CompanyId,TourId,BuyCountryId,BuyProvincesId");
            query.Append(",BuyCompanyName,BuyCompanyId,ContactName,ContactTel");
            query.Append(",DCompanyName,DContactName,DContactTel,SellerName,SellerId,Operator");
            query.Append(",OperatorId,SellerDeptId,Adults,Childs,Leaders,AdultPrice,ChildPrice");
            query.Append(",LeaderPrice,PriceStandId,LevId");
            query.Append(",SaleAddCost,SaleReduceCost");
            query.Append(",SaleAddCostRemark,SaleReduceCostRemark");
            query.Append(",SumPrice");
            query.Append(",ConfirmMoneyStatus,ConfirmMoney,ConfirmRemark,SalerIncome");
            query.Append(",GuideIncome,OrderRemark,SaveSeatDate,OrderType,OrderStatus");
            query.Append(",IsDelete,IssueTime,UpdateTime,CheckMoney,ReturnMoney,ReceivedMoney");
            query.Append(",IsClean,TheNum,ContractId,ContractCode,TravellerFile,");
            //退款信息
            query.Append("(");
            query.Append(" SELECT ");
            query.Append("Id,OrderId,CollectionRefundDate,CollectionRefundOperator,CollectionRefundOperatorID");
            query.Append(",CollectionRefundAmount,CollectionRefundMode,CollectionRefundState,ApproverDeptId");
            query.Append(",Approver,ApproverId,ApproveTime,Memo,OperatorId,Operator,IssueTime");
            query.Append(" FROM tbl_TourOrderSales as tsales");
            query.Append(" where torder.OrderId=tsales.OrderId");
            query.Append(" and CollectionRefundMode=1");
            query.Append(" for xml raw,root('Root')");
            query.Append(")");
            query.Append(" as TourOrderSales,");
            //游客信息
            query.Append("(");
            query.Append("SELECT TravellerId,OrderId,CnName,EnName,VisitorType,CardType,CardNumber");
            query.Append(",CardValidDate,VisaStatus,IsCardTransact,Gender,Contact,LNotice,RNotice");
            query.Append(",Remark,Status,RAmount,RAmountRemark,RTime,RRemark");
            query.Append(" FROM tbl_TourOrderTraveller as ttraveller");
            query.Append(" where torder.OrderId=ttraveller.OrderId");
            query.Append(" for xml path,elements,root('Root')	");
            query.Append(")");
            query.Append("as TourOrderTraveller,");

            //销售员联系方式
            query.Append(" (select ContactTel,ContactMobile from tbl_ComUser ");
            query.Append(" where tbl_ComUser.UserId=torder.SellerId for xml raw,root('Root')) as SellerContact,");

            //操作员联系方式
            query.Append(" (select ContactTel,ContactMobile from tbl_ComUser ");
            query.Append(" where tbl_ComUser.UserId=torder.OperatorId for xml raw,root('Root')) as OperatorContact");

            query.Append(" FROM ");
            query.Append("tbl_TourOrder as torder");
            query.Append(" Where  ");
            query.AppendFormat("TourId='{0}'", tourId);

            //刷选订单
            //2012-12-18 去掉   只有二个打印页面用到   而且打印页做过状态的筛选
            //query.Append(NotExitsOrderStatus());

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderSummary>();
                    while (dr.Read())
                    {
                        MTourOrderSummary order = new MTourOrderSummary();
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.BuyCountryId = !dr.IsDBNull(dr.GetOrdinal("BuyCountryId")) ? dr.GetInt32(dr.GetOrdinal("BuyCountryId")) : 0;
                        order.BuyProvincesId = !dr.IsDBNull(dr.GetOrdinal("BuyProvincesId")) ? dr.GetInt32(dr.GetOrdinal("BuyProvincesId")) : 0;
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.BuyCompanyId = dr["BuyCompanyId"].ToString();
                        //order.ContactDepartId = !dr.IsDBNull(dr.GetOrdinal("ContactDepartId")) ? dr.GetString(dr.GetOrdinal("ContactDepartId")) : string.Empty;
                        order.ContactName = !dr.IsDBNull(dr.GetOrdinal("ContactName")) ? dr.GetString(dr.GetOrdinal("ContactName")) : string.Empty;
                        order.ContactTel = !dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? dr.GetString(dr.GetOrdinal("ContactTel")) : string.Empty;
                        order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                        order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                        order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;

                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                        order.SellerId = dr["SellerId"].ToString();
                        order.DeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));

                        order.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                        order.OperatorId = dr["OperatorId"].ToString();

                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.Others = dr.GetInt32(dr.GetOrdinal("Leaders"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                        order.OtherPrice = dr.GetDecimal(dr.GetOrdinal("LeaderPrice"));

                        order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                        order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));

                        order.PriceStandId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                        order.LevId = dr.GetInt32(dr.GetOrdinal("LevId"));
                        //order.PeerAdultPrice = dr.GetDecimal(dr.GetOrdinal("PeerAdultPrice"));
                        //order.PeerChildPrice = dr.GetDecimal(dr.GetOrdinal("PeerChildPrice"));
                        //order.SettlementMoney = dr.GetDecimal(dr.GetOrdinal("SettlementMoney"));
                       // order.ConfirmSettlementMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmSettlementMoney"));



                        order.SaleAddCost = dr.GetDecimal(dr.GetOrdinal("SaleAddCost"));
                        order.SaleReduceCost = dr.GetDecimal(dr.GetOrdinal("SaleReduceCost"));
                        order.SaleAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleAddCostRemark")) : string.Empty;
                        order.SaleReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleReduceCostRemark")) : string.Empty;

                        //order.PeerAddCost = dr.GetDecimal(dr.GetOrdinal("PeerAddCost"));
                        //order.PeerReduceCost = dr.GetDecimal(dr.GetOrdinal("PeerReduceCost"));
                        //order.PeerAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerAddCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerAddCostRemark")) : string.Empty;
                        //order.PeerReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerReduceCostRemark")) : string.Empty;

                        //order.OtherCost = dr.GetDecimal(dr.GetOrdinal("OtherCost"));

                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        //order.SumPriceAddCost = dr.GetDecimal(dr.GetOrdinal("SumPriceAddCost"));
                        //order.SumPriceAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceAddCostRemark")) : string.Empty;
                        //order.SumPriceReduceCost = dr.GetDecimal(dr.GetOrdinal("SumPriceReduceCost"));
                        //order.SumPriceReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceReduceCostRemark")) : string.Empty;

                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.ConfirmMoneyStatus = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1" ? true : false;
                        order.ConfirmRemark = !dr.IsDBNull(dr.GetOrdinal("ConfirmRemark")) ? dr.GetString(dr.GetOrdinal("ConfirmRemark")) : string.Empty;

                        //order.Profit = dr.GetDecimal(dr.GetOrdinal("Profit"));
                        //order.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                        order.OrderType = (OrderType)dr.GetByte(dr.GetOrdinal("OrderType"));
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("OrderStatus"));


                        order.OrderRemark = !dr.IsDBNull(dr.GetOrdinal("OrderRemark")) ? dr.GetString(dr.GetOrdinal("OrderRemark")) : string.Empty;
                        order.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : string.Empty;
                        order.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : string.Empty;
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));


                        //order.PayMentMonth = !dr.IsDBNull(dr.GetOrdinal("PayMentMonth")) ? dr.GetString(dr.GetOrdinal("PayMentMonth")) : string.Empty;
                        //order.PayMentDay = !dr.IsDBNull(dr.GetOrdinal("PayMentDay")) ? dr.GetString(dr.GetOrdinal("PayMentDay")) : string.Empty;
                        //order.PayMentAccountId = !dr.IsDBNull(dr.GetOrdinal("PayMentAccountId")) ? dr.GetInt32(dr.GetOrdinal("PayMentAccountId")) : 0;


                        //订单销售退款的xml集合
                        string TourOrderSalesXml = !dr.IsDBNull(dr.GetOrdinal("TourOrderSales")) ? dr.GetString(dr.GetOrdinal("TourOrderSales")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourOrderSalesXml))
                        {
                            order.TourOrderSalesList = GetTourOrderSalesByXml(TourOrderSalesXml);
                        }

                        //订单游客的集合
                        string TourOrderTravellerXml = !dr.IsDBNull(dr.GetOrdinal("TourOrderTraveller")) ? dr.GetString(dr.GetOrdinal("TourOrderTraveller")) : string.Empty;
                        if (!string.IsNullOrEmpty(TourOrderTravellerXml))
                        {
                            order.TourOrderTravellerList = GetTourOrderTravellerXml(TourOrderTravellerXml);
                        }

                        string SellerContact = !dr.IsDBNull(dr.GetOrdinal("SellerContact")) ? dr.GetString(dr.GetOrdinal("SellerContact")) : string.Empty;
                        if (!string.IsNullOrEmpty(SellerContact))
                        {
                            string SellerContactTel = null;
                            string SellerContactMobile = null;

                            GetContract(SellerContact, ref SellerContactTel, ref SellerContactMobile);
                            order.SellerContactTel = SellerContactTel;
                            order.SellerContactMobile = SellerContactMobile;
                        }


                        string OperatorContact = !dr.IsDBNull(dr.GetOrdinal("OperatorContact")) ? dr.GetString(dr.GetOrdinal("OperatorContact")) : string.Empty;
                        if (!string.IsNullOrEmpty(OperatorContact))
                        {
                            string OperatorContactTel = null;

                            string OperatorContactMobile = null;

                            GetContract(OperatorContact, ref OperatorContactTel, ref OperatorContactMobile);
                            order.OperatorContactTel = OperatorContactTel;
                            order.OperatorContactMobile = OperatorContactMobile;
                        }

                        list.Add(order);
                    }
                }
            }

            return list;

        }
        #endregion

        #region 获取订单金额确认单(确认单)
        /// <summary>
        /// 根据订单编号获取订单金额确认单
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="tourType">计划类型</param>
        /// <returns></returns>
        public MOrderSale GetSettlementOrderByOrderId(string OrderId, TourType tourType)
        {
            MOrderSale order = null;
            var strSql = new StringBuilder();
            strSql.Append(" SELECT  T.TourCode ,");
            strSql.Append("         T.RouteName ,");
            strSql.Append("         O.SumPrice ,");
            strSql.Append("         O.ConfirmMoney ,");
            strSql.Append("         O.ConfirmMoneyStatus ,");
            strSql.Append("         O.SellerId ,");
            strSql.Append("         O.ConfirmRemark ,");
            strSql.Append("         ( SELECT    *");
            strSql.Append("           FROM      dbo.tbl_TourOrderChange");
            strSql.Append("           WHERE     OrderId = O.OrderId");
            strSql.Append("         FOR");
            strSql.Append("           XML RAW ,");
            strSql.Append("               ROOT");
            strSql.Append("         ) XMLConfirm");
            strSql.Append(" FROM    dbo.tbl_TourOrder O");
            strSql.Append("         INNER JOIN dbo.tbl_Tour T ON O.TourId = T.TourId");
            strSql.Append(" WHERE   O.OrderId = @OrderId");

            var cmd = _db.GetSqlStringCommand(strSql.ToString());
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, OrderId);
            using (var dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        order = new MOrderSale()
                            {
                                TourCode = dr["TourCode"].ToString(),
                                RouteName = dr["RouteName"].ToString(),
                                SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice")),
                                ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")),
                                SellerId = dr["SellerId"].ToString(),
                                ConfirmMoneyStatus = dr["ConfirmMoneyStatus"].ToString().Equals("1"),
                                ConfirmRemark = dr["ConfirmRemark"].ToString(),
                                OrderSalesConfirm = GetOrderSalesConfirmLst(dr["XMLConfirm"].ToString()),
                            };
                    }
                }
            }

            return order;

        }
        #endregion

        #region 获取订单游客信息
        /// <summary>
        /// 根据订单编号获取订单游客信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<MTourOrderTraveller> GetTourOrderTravellerByOrderId(string orderId)
        {
            IList<MTourOrderTraveller> list = null;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TravellerId,OrderId,CnName,EnName,CardId,VisitorType,CardType,CardNumber");
            query.Append(",CardValidDate,VisaStatus,IsCardTransact,Gender,Contact,LNotice,RNotice");
            query.Append(",Remark,Status,RAmount,RTime,RRemark,IsInsurance");
            query.Append(" FROM tbl_TourOrderTraveller ");
            query.Append(" WHERE ");
            query.AppendFormat("OrderId='{0}'", orderId);
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderTraveller>();
                    while (dr.Read())
                    {
                        MTourOrderTraveller traveller = new MTourOrderTraveller();
                        traveller.TravellerId = dr["TravellerId"].ToString();
                        traveller.OrderId = dr["OrderId"].ToString();
                        traveller.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                        traveller.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                        traveller.VisitorType = (VisitorType)dr.GetByte(dr.GetOrdinal("VisitorType"));
                        traveller.CardType = (CardType)dr.GetByte(dr.GetOrdinal("CardType"));
                        traveller.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                        traveller.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                        traveller.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                        traveller.VisaStatus = (VisaStatus)dr.GetByte(dr.GetOrdinal("VisaStatus"));
                        traveller.IsCardTransact = dr["IsCardTransact"].ToString().Equals("1") ? true : false;
                        traveller.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)dr.GetByte(dr.GetOrdinal("Gender"));
                        traveller.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                        traveller.LNotice = dr["LNotice"].ToString().Equals("1") ? true : false;
                        traveller.RNotice = dr["RNotice"].ToString().Equals("1") ? true : false;
                        traveller.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                        traveller.TravellerStatus = (TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                        traveller.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                        traveller.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                        traveller.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                        traveller.IsInsurance = dr["IsInsurance"].ToString().Equals("1") ? true : false;
                        list.Add(traveller);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 分销商平台(我的订单)
        /// <summary>
        /// 分销商平台_我的订单
        /// </summary>
        /// <param name="search">查询条件的实体</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param> 
        /// <returns></returns>
        public IList<MFinancialOrder> GetOrderList(
            MSearchFinancialOrder search,
            int pageSize,
            int pageIndex,
            ref int recordCount)
        {
            IList<MFinancialOrder> list = null;

            StringBuilder filed = new StringBuilder();
            filed.Append("TourId,OrderId,OrderCode,BuyCompanyId,DCompanyName,DContactName,DContactTel,Adults,");
            filed.Append("Childs,AdultPrice,ChildPrice,IssueTime,Status,LDate,Seller,Planer,AreaName,RouteName,SaveSeatDate");


            string tableName = "view_DistributionOrder";
            string paryKey = "OrderId";
            string orderByString = " IssueTime desc";

            StringBuilder query = new StringBuilder();
            //是否同业分销、未删除(登录编号，为BuyCountryId)
            query.AppendFormat(" IsShowDistribution ='{0}' and IsDelete='{1}' ", 1, 0);

            if (!string.IsNullOrEmpty(search.CompanyId))
            {
                query.AppendFormat(" and CompanyId='{0}' ", search.CompanyId);
            }
            if (!string.IsNullOrEmpty(search.CrmId))
            {
                query.AppendFormat(" and BuyCompanyId='{0}' ", search.CrmId);
            }
            if (!string.IsNullOrEmpty(search.DCompanyName))
            {
                query.AppendFormat(" and DCompanyName like '%{0}%' ", search.DCompanyName);
            }

            if (search.AreaId != 0)
            {
                query.AppendFormat(" and AreaId={0} ", search.AreaId);
            }

            if (search.Status.HasValue)
            {
                // query.Append(GetOrderStatusByGroupOrderStatus(search.Status.Value));
                query.AppendFormat(" and  Status={0} ", (int)search.Status.Value);
            }
            else
            {
                //筛选订单
                // 2012-12-18 去掉  只有分销商我的订单用到该状态 该页面只有取消操作 未处理的订单才能取消
                //query.Append(NotExitsOrderStatus());
            }


            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, paryKey, filed.ToString(), query.ToString(), orderByString))
            {
                if (dr != null)
                {
                    list = new List<MFinancialOrder>();
                    while (dr.Read())
                    {

                        MFinancialOrder order = new MFinancialOrder();
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                        order.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                        order.AreaName = !dr.IsDBNull(dr.GetOrdinal("AreaName")) ? dr.GetString(dr.GetOrdinal("AreaName")) : string.Empty;
                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                        DateTime? SaveSeatDate = !dr.IsDBNull(dr.GetOrdinal("SaveSeatDate")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("SaveSeatDate")) : null;
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status"));
                        order.GroupOrderStatus = GetGroupOrderStatus(order.OrderStatus, SaveSeatDate);
                        //客源单位联系人信息
                        order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                        order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                        order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;
                        //销售员信息
                        string sellerXml = !dr.IsDBNull(dr.GetOrdinal("Seller")) ? dr.GetString(dr.GetOrdinal("Seller")) : string.Empty;
                        if (!string.IsNullOrEmpty(sellerXml))
                        {
                            string SellerName = string.Empty;
                            string SellerContactTel = string.Empty;
                            string SellerContactMobile = string.Empty;
                            this.GetSellerContactByXml(sellerXml, ref SellerName, ref SellerContactTel, ref SellerContactMobile);
                            order.SellerName = SellerName;
                            order.SellerContactTel = SellerContactTel;
                            order.SellerContactMobile = SellerContactMobile;
                        }

                        string planerXml = !dr.IsDBNull(dr.GetOrdinal("Planer")) ? dr.GetString(dr.GetOrdinal("Planer")) : string.Empty;//计调员信息
                        if (!string.IsNullOrEmpty(planerXml))
                        {
                            order.PlanerList = GetPlanerByXml(planerXml);
                        }

                        list.Add(order);
                    }
                }
            }

            return list;

        }
        #endregion

        #region 供应商平台(订单中心)
        /// <summary>
        ///供应商平台订单中心 
        /// </summary>
        /// <param name="search">查询实体</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <returns></returns>
        public IList<MSupplierOrder> GetOrderList(
            MSearchSupplierOrder search,
            int pageSize,
            int pageIndex,
            ref int recordCount)
        {
            //string sql = "select TourCode,RouteId,RouteName,LDate,PlanPeopleNumber,AdultPrice,ChildPrice,b.Orders"
            //+ "from tbl_Tour a"
            //+ "left join"
            //+ "("
            //+ "select TourId,(select OrderId,OrderCode,IssueTime,SumPrice,ContactName,ContactTel,Adults,Childs,Others,Status"
            //+ "from tbl_TourOrder where TourId=a.TourId "
            //+ "	for xml raw,root('Root')) as Orders from tbl_TourOrder a"
            //+ "group by tourid"
            //+ ")b on a.TourId = b.TourId"
            //+ "where  b.orders is not null";

            IList<MSupplierOrder> list = null;
            string tableName = "view_SupplierOrder";
            string identity = "OrderId";
            string orderByString = " IssueTime desc";


            StringBuilder fileds = new StringBuilder();
            fileds.Append("TourId,OrderId,OrderCode,BuyCompanyName,BuyCompanyId,");
            fileds.Append("Adults,Childs,AdultPrice,ChildPrice,IssueTime,Status,SumPrice,CompanyId,");
            fileds.Append("LDate,RouteName,IsShowDistribution,IsDelete,SourceId,SourceCompanyName,AreaId,SellerName,SaveSeatDate,RDate");


            StringBuilder query = new StringBuilder();
            //供应商编号、供应商名称、是否同业分销、未删除
            query.AppendFormat("IsShowDistribution ='{0}' and IsDelete='{1}' ", 1, 0);

            if (!string.IsNullOrEmpty(search.CompanyId))
            {
                query.AppendFormat(" and CompanyId='{0}' ", search.CompanyId);
            }
            if (!string.IsNullOrEmpty(search.SourceId))
            {
                query.AppendFormat(" and SourceId= '{0}' ", search.SourceId);
            }

            if (!string.IsNullOrEmpty(search.TourId))
            {
                query.AppendFormat(" and TourId='{0}'", search.TourId);
            }

            if (!string.IsNullOrEmpty(search.OrderCode))
            {
                query.AppendFormat(" and OrderCode like '%{0}%' ", search.OrderCode);
            }

            if (!string.IsNullOrEmpty(search.RouteName))
            {
                query.AppendFormat(" and RouteName like '%{0}%' ", search.RouteName);
            }

            if (search.AreaId != 0)
            {
                query.AppendFormat(" and AreaId={0} ", search.AreaId);
            }

            if (search.BeginIssueTime.HasValue)
            {
                query.AppendFormat(" and  datediff(day,'{0}',IssueTime)>=0 ", search.BeginIssueTime.Value);
            }

            if (search.EndIssueTime.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',IssueTime)<=0 ", search.EndIssueTime.Value);
            }

            if (search.BeginLDate.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',LDate)>=0 ", search.BeginLDate.Value);
            }
            if (search.EndLDate.HasValue)
            {
                query.AppendFormat(" and datediff(day,'{0}',LDate)<=0 ", search.EndLDate.Value);
            }

            if (search.Status.HasValue)
            {
                //query.Append(GetOrderStatusByGroupOrderStatus(search.Status.Value));
                query.AppendFormat(" and   Status={0} ", (int)search.Status.Value);
            }
            else
            {
                //筛选订单
                //供应商 订单中心 的列表
                // query.Append(NotExitsOrderStatus());
            }

            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identity, fileds.ToString(), query.ToString(), orderByString))
            {
                if (dr != null)
                {
                    list = new List<MSupplierOrder>();
                    while (dr.Read())
                    {


                        MSupplierOrder order = new MSupplierOrder();

                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;

                        order.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));

                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));


                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;

                        DateTime? SaveSeatDate = !dr.IsDBNull(dr.GetOrdinal("SaveSeatDate")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("SaveSeatDate")) : null;
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status"));

                        //order.GroupOrderStatus = GetGroupOrderStatus(order.OrderStatus, SaveSeatDate);


                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;

                        order.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                        order.RDate = dr.GetDateTime(dr.GetOrdinal("RDate"));
                        list.Add(order);

                    }
                }
            }

            return list;

        }
        #endregion

        #region 获取游客保险信息
        /// <summary>
        /// 根据游客编号获取游客的保险
        /// </summary>
        /// <param name="travellerId">游客编号获取游客保险</param>
        /// <returns></returns>
        public IList<MTourOrderTravellerInsurance> GetTravellerInsuranceListByTravellerId(string travellerId)
        {
            IList<MTourOrderTravellerInsurance> list = null;
            string sql = "SELECT TravellerId,InsuranceId,BuyNum,UnitPrice,SumPrice FROM tbl_TourOrderTravellerInsurance where  travellerId='" + travellerId + "'";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderTravellerInsurance>();
                    while (dr.Read())
                    {
                        MTourOrderTravellerInsurance insurance = new MTourOrderTravellerInsurance();
                        insurance.TravellerId = dr.IsDBNull(dr.GetOrdinal("TravellerId")) == false ? dr.GetString(dr.GetOrdinal("TravellerId")) : string.Empty;
                        insurance.InsuranceId = dr.IsDBNull(dr.GetOrdinal("InsuranceId")) == false ? dr.GetString(dr.GetOrdinal("InsuranceId")) : string.Empty;
                        insurance.BuyNum = dr.IsDBNull(dr.GetOrdinal("BuyNum")) == false ? dr.GetInt32(dr.GetOrdinal("BuyNum")) : 0;
                        insurance.UnitPrice = dr.IsDBNull(dr.GetOrdinal("UnitPrice")) == false ? dr.GetDecimal(dr.GetOrdinal("UnitPrice")) : 0;
                        insurance.SumPrice = dr.IsDBNull(dr.GetOrdinal("SumPrice")) == false ? dr.GetDecimal(dr.GetOrdinal("SumPrice")) : 0;
                        list.Add(insurance);
                    }
                }
            }

            return list;

        }
        #endregion

        #region 获取游客信息根据游客编号
        /// <summary>
        /// 根据游客编号获取游客信息
        /// </summary>
        /// <param name="travellerId">游客编号</param>
        /// <returns></returns>
        public MTourOrderTraveller GetTourOrderTravellerById(string travellerId)
        {
            MTourOrderTraveller traveller = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("TravellerId,OrderId,CnName,EnName,CardId,VisitorType,CardType");
            query.Append(",CardNumber,CardValidDate,VisaStatus,IsCardTransact,Gender");
            query.Append(",Contact,LNotice,RNotice,Remark,Status,RAmount,RTime,RRemark,IsInsurance");
            query.Append(" FROM ");
            query.Append("tbl_TourOrderTraveller");
            query.Append(" Where ");
            query.AppendFormat("TravellerId='{0}'", travellerId);
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = _db.ExecuteReader(cmd))
            {
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        traveller = new MTourOrderTraveller();
                        traveller.TravellerId = dr["TravellerId"].ToString();
                        traveller.OrderId = dr["OrderId"].ToString();
                        traveller.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                        traveller.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                        traveller.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                        traveller.VisitorType = !dr.IsDBNull(dr.GetOrdinal("VisitorType")) ? (VisitorType?)dr.GetByte(dr.GetOrdinal("VisitorType")) : null;
                        traveller.CardType = !dr.IsDBNull(dr.GetOrdinal("CardType")) ? (CardType?)dr.GetByte(dr.GetOrdinal("CardType")) : null;
                        traveller.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                        traveller.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                        traveller.VisaStatus = !dr.IsDBNull(dr.GetOrdinal("VisaStatus")) ? (VisaStatus?)dr.GetByte(dr.GetOrdinal("VisaStatus")) : null;
                        traveller.IsCardTransact = dr["IsCardTransact"].ToString().Equals("1") ? true : false;
                        traveller.Gender = !dr.IsDBNull(dr.GetOrdinal("Gender")) ? (EyouSoft.Model.EnumType.GovStructure.Gender?)dr.GetByte(dr.GetOrdinal("Gender")) : null;
                        traveller.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                        traveller.LNotice = dr["LNotice"].ToString().Equals("1") ? true : false;
                        traveller.RNotice = dr["RNotice"].ToString().Equals("1") ? true : false;
                        traveller.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                        traveller.TravellerStatus = (TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                        traveller.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                        traveller.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                        traveller.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                        traveller.IsInsurance = dr["IsInsurance"].ToString().Equals("1") ? true : false;
                    }
                }

            }
            return traveller;

        }
        #endregion

        #region   获取游客信息根据计划编号
        /// <summary>
        /// 获取游客信息根据计划编号
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<MTourOrderTraveller> GetTourOrderTravellerByTourId(string tourId)
        {
            IList<MTourOrderTraveller> list = null;
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TravellerId,OrderId,CnName,EnName,CardId,VisitorType");
            query.Append(",CardType,CardNumber,CardValidDate,VisaStatus,IsCardTransact");
            query.Append(",Gender,Contact,LNotice,RNotice,Remark,Status,RAmount");
            query.Append(",RTime,RRemark,IsInsurance FROM tbl_TourOrderTraveller");
            query.Append(" where OrderId in (");
            query.Append(" select OrderId from tbl_TourOrder");
            query.Append(" where ");
            query.AppendFormat(" TourId='{0}' ", tourId);
            //筛选订单
            query.Append(NotExitsOrderStatus());
            query.Append(")");
            //排序
            query.Append(" Order by tbl_TourOrderTraveller.Id  asc ");

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderTraveller>();
                    while (dr.Read())
                    {
                        MTourOrderTraveller traveller = new MTourOrderTraveller();
                        traveller.TravellerId = dr["TravellerId"].ToString();
                        traveller.OrderId = dr["OrderId"].ToString();
                        traveller.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                        traveller.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                        traveller.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                        traveller.VisitorType = !dr.IsDBNull(dr.GetOrdinal("VisitorType")) ? (VisitorType?)dr.GetByte(dr.GetOrdinal("VisitorType")) : null;
                        traveller.CardType = !dr.IsDBNull(dr.GetOrdinal("CardType")) ? (CardType?)dr.GetByte(dr.GetOrdinal("CardType")) : null;
                        traveller.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                        traveller.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                        traveller.VisaStatus = !dr.IsDBNull(dr.GetOrdinal("VisaStatus")) ? (VisaStatus?)dr.GetByte(dr.GetOrdinal("VisaStatus")) : null;
                        traveller.IsCardTransact = dr["IsCardTransact"].ToString().Equals("1") ? true : false;
                        traveller.Gender = !dr.IsDBNull(dr.GetOrdinal("Gender")) ? (EyouSoft.Model.EnumType.GovStructure.Gender?)dr.GetByte(dr.GetOrdinal("Gender")) : null;
                        traveller.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                        traveller.LNotice = dr["LNotice"].ToString().Equals("1") ? true : false;
                        traveller.RNotice = dr["RNotice"].ToString().Equals("1") ? true : false;
                        traveller.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                        traveller.TravellerStatus = (TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                        traveller.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                        traveller.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                        traveller.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                        traveller.IsInsurance = dr["IsInsurance"].ToString().Equals("1") ? true : false;
                        list.Add(traveller);
                    }
                }
            }
            return list;

        }
        #endregion

        #region 游客退团
        /// <summary>
        /// 游客退团
        /// </summary>
        /// <param name="traveller">游客实体</param>
        /// <param name="change">订单修改、变更实体</param>
        /// <returns></returns>
        public int UpdateTourOrderTraveller(MTourOrderTraveller traveller, MTourOrderChange change)
        {
            string changeXml = string.Empty;
            if (change != null)
            {
                changeXml = GetTourOrderChangeXml(change);
            }

            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderTraveller_Update");
            _db.AddInParameter(cmd, "TravellerId", DbType.AnsiStringFixedLength, traveller.TravellerId);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, traveller.OrderId);
            _db.AddInParameter(cmd, "CnName", DbType.String, traveller.CnName);
            _db.AddInParameter(cmd, "EnName", DbType.String, traveller.EnName);
            _db.AddInParameter(cmd, "CardId", DbType.StringFixedLength, traveller.CardId);
            _db.AddInParameter(cmd, "VisitorType", DbType.Byte, (int?)traveller.VisitorType);
            _db.AddInParameter(cmd, "CardType", DbType.Byte, (int?)traveller.CardType);
            _db.AddInParameter(cmd, "CardNumber", DbType.String, traveller.CardNumber);
            _db.AddInParameter(cmd, "CardValidDate", DbType.String, traveller.CardValidDate);
            _db.AddInParameter(cmd, "VisaStatus", DbType.Byte, (int?)traveller.VisaStatus);
            _db.AddInParameter(cmd, "IsCardTransact", DbType.AnsiStringFixedLength, traveller.IsCardTransact == true ? 1 : 0);
            _db.AddInParameter(cmd, "Gender", DbType.Byte, (int?)traveller.Gender);
            _db.AddInParameter(cmd, "Contact", DbType.String, traveller.Contact);
            _db.AddInParameter(cmd, "LNotice", DbType.AnsiStringFixedLength, traveller.LNotice == true ? 1 : 0);
            _db.AddInParameter(cmd, "RNotice", DbType.AnsiStringFixedLength, traveller.RNotice == true ? 1 : 0);
            _db.AddInParameter(cmd, "Remark", DbType.String, traveller.Remark);
            _db.AddInParameter(cmd, "Status", DbType.Byte, (int)traveller.TravellerStatus);
            _db.AddInParameter(cmd, "RAmount", DbType.Currency, traveller.RAmount);
            _db.AddInParameter(cmd, "RAmountRemark", DbType.String, traveller.RAmountRemark);
            _db.AddInParameter(cmd, "RTime", DbType.String, traveller.RTime);
            _db.AddInParameter(cmd, "RRemark", DbType.String, traveller.RRemark);
            _db.AddInParameter(cmd, "IsInsurance", DbType.AnsiStringFixedLength, traveller.IsInsurance == true ? 1 : 0);
            _db.AddInParameter(cmd, "TourOrderChange", DbType.String, changeXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString());

        }
        #endregion

        #region 添加收款/退款
        /// <summary>
        /// 添加销售收款/退款
        /// </summary>
        /// <param name="tourOrderSales">订单收款退款的集合</param>
        /// <returns>0:添加失败 1:添加成功</returns>
        public int AddTourOrderSales(MTourOrderSales tourOrderSales)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderSales_Add");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, string.IsNullOrEmpty(tourOrderSales.Id) ? Guid.NewGuid().ToString() : tourOrderSales.Id);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, tourOrderSales.OrderId);
            _db.AddInParameter(cmd, "IsDaiShou", DbType.AnsiStringFixedLength, tourOrderSales.IsDaiShou ? "1" : "0");
            _db.AddInParameter(cmd, "DaiShouRen", DbType.String, tourOrderSales.DaiShouRen);
            _db.AddInParameter(cmd, "CollectionRefundDate", DbType.DateTime, tourOrderSales.CollectionRefundDate);
            _db.AddInParameter(cmd, "CollectionRefundOperator", DbType.String, tourOrderSales.CollectionRefundOperator);
            _db.AddInParameter(cmd, "CollectionRefundOperatorID", DbType.AnsiStringFixedLength, tourOrderSales.CollectionRefundOperatorID);
            _db.AddInParameter(cmd, "CollectionRefundAmount", DbType.Decimal, tourOrderSales.CollectionRefundAmount);
            _db.AddInParameter(cmd, "CollectionRefundMode", DbType.Byte, tourOrderSales.CollectionRefundMode);
            _db.AddInParameter(cmd, "CollectionRefundState", DbType.AnsiStringFixedLength, (int)tourOrderSales.CollectionRefundState);
            _db.AddInParameter(cmd, "ApproverDeptId", DbType.Int32, tourOrderSales.ApproverDeptId);
            _db.AddInParameter(cmd, "ApproverId", DbType.AnsiStringFixedLength, tourOrderSales.ApproverId);
            _db.AddInParameter(cmd, "Approver", DbType.String, tourOrderSales.Approver);
            _db.AddInParameter(cmd, "ApproveTime", DbType.DateTime, tourOrderSales.ApproveTime);
            _db.AddInParameter(cmd, "IsCheck", DbType.AnsiStringFixedLength, tourOrderSales.IsCheck == true ? '1' : '0');
            _db.AddInParameter(cmd, "Memo", DbType.String, tourOrderSales.Memo);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderSales.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderSales.Operator);
            _db.AddInParameter(cmd, "IsGuideRealIncome", DbType.AnsiStringFixedLength, tourOrderSales.IsGuideRealIncome == true ? '1' : '0');
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

        }
        #endregion

        #region 批量收款
        /// <summary>
        /// 订单批量销售收款
        /// </summary>
        /// <param name="tourOrderSalesList">销售收款/退款的集合</param>
        /// <returns>
        /// 1:成功 
        /// 0:失败
        /// </returns>
        public int AddTourOrderSales(IList<MTourOrderSales> tourOrderSalesList)
        {
            string xml = GetXmlByMTourOrderSales(tourOrderSalesList);//将集合转换为xml
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderSalesList_Add");
            _db.AddInParameter(cmd, "MTourOrderSales", DbType.Xml, xml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region 删除 收款/退款
        /// <summary>
        /// 删除订单销售的收款/退款
        /// </summary>
        /// <param name="orderSalesId">订单销售的收款/退款编号</param>
        /// <returns>
        /// 1:删除成功 
        /// 0：删除失败
        /// </returns>
        public int DeleteTourOrderSales(string orderSalesId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderSales_Delete");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, orderSalesId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region  修改 收款/退款
        /// <summary>
        /// 修改订单销售的收款/退款
        /// </summary>
        /// <param name="tourOrderSales">订单销售收款/退款的实体</param>
        /// <returns>
        /// 1:更新成功 
        /// 0:更新失败 
        /// </returns>
        public int UpdateTourOrderSales(MTourOrderSales tourOrderSales)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrderSales_Update");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, tourOrderSales.Id);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, tourOrderSales.OrderId);
            _db.AddInParameter(cmd, "IsDaiShou", DbType.AnsiStringFixedLength, tourOrderSales.IsDaiShou ? "1" : "0");
            _db.AddInParameter(cmd, "DaiShouRen", DbType.String, tourOrderSales.DaiShouRen);
            _db.AddInParameter(cmd, "CollectionRefundDate", DbType.DateTime, tourOrderSales.CollectionRefundDate);
            _db.AddInParameter(cmd, "CollectionRefundOperator", DbType.String, tourOrderSales.CollectionRefundOperator);
            _db.AddInParameter(cmd, "CollectionRefundOperatorID", DbType.AnsiStringFixedLength, tourOrderSales.CollectionRefundOperatorID);
            _db.AddInParameter(cmd, "CollectionRefundAmount", DbType.Decimal, tourOrderSales.CollectionRefundAmount);
            _db.AddInParameter(cmd, "CollectionRefundMode", DbType.Byte, tourOrderSales.CollectionRefundMode);
            _db.AddInParameter(cmd, "CollectionRefundState", DbType.AnsiStringFixedLength, (int)tourOrderSales.CollectionRefundState);
            _db.AddInParameter(cmd, "ApproverDeptId", DbType.Int32, tourOrderSales.ApproverDeptId);
            _db.AddInParameter(cmd, "ApproverId", DbType.AnsiStringFixedLength, tourOrderSales.ApproverId);
            _db.AddInParameter(cmd, "Approver", DbType.String, tourOrderSales.Approver);
            _db.AddInParameter(cmd, "ApproveTime", DbType.DateTime, tourOrderSales.ApproveTime);
            _db.AddInParameter(cmd, "IsCheck", DbType.AnsiStringFixedLength, tourOrderSales.IsCheck == true ? '1' : '0');
            _db.AddInParameter(cmd, "Memo", DbType.String, tourOrderSales.Memo);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, tourOrderSales.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, tourOrderSales.Operator);
            _db.AddInParameter(cmd, "IsGuideRealIncome", DbType.AnsiStringFixedLength, tourOrderSales.IsGuideRealIncome == true ? '1' : '0');
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 1);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }
        #endregion

        #region 根据订单销售收款/退款的主键编号获取实体
        /// <summary>
        /// 根据订单销售收款/退款的主键编号获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MTourOrderSales GetTourOrderSalesById(string id)
        {
            MTourOrderSales sales = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("Id,OrderId,IsDaiShou,DaiShouRen,CollectionRefundDate");
            query.Append(",CollectionRefundOperator,CollectionRefundOperatorID");
            query.Append(",CollectionRefundAmount,CollectionRefundMode,CollectionRefundState");
            query.Append(",ApproverDeptId,Approver,ApproverId,ApproveTime");
            query.Append(",FinStatus,Memo,OperatorId,Operator,IsGuideRealIncome");
            query.Append(",(Select Name from tbl_ComPayment where tbl_ComPayment.PayMentId=tbl_TourOrderSales.CollectionRefundMode) as Name");
            query.Append("  FROM ");
            query.Append("tbl_TourOrderSales");
            query.Append(" WHERE ");
            query.AppendFormat("Id='{0}'", id);
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        sales = new MTourOrderSales();
                        sales.Id = dr.GetString(dr.GetOrdinal("Id"));
                        sales.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        sales.IsDaiShou = dr["IsDaiShou"].ToString().Equals("1");
                        sales.DaiShouRen = dr["DaiShouRen"].ToString();
                        sales.CollectionRefundDate = dr.GetDateTime(dr.GetOrdinal("CollectionRefundDate"));
                        sales.CollectionRefundOperator = !dr.IsDBNull(dr.GetOrdinal("CollectionRefundOperator")) ? dr.GetString(dr.GetOrdinal("CollectionRefundOperator")) : string.Empty;
                        sales.CollectionRefundOperatorID = dr.GetString(dr.GetOrdinal("CollectionRefundOperatorID"));
                        sales.CollectionRefundAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount"));
                        sales.CollectionRefundMode = dr.GetInt32(dr.GetOrdinal("CollectionRefundMode"));
                        sales.CollectionRefundState = (CollectionRefundState)dr.GetByte(dr.GetOrdinal("CollectionRefundState"));
                        sales.ApproverDeptId = !dr.IsDBNull(dr.GetOrdinal("ApproverDeptId")) ? dr.GetInt32(dr.GetOrdinal("ApproverDeptId")) : 0;
                        sales.ApproverId = !dr.IsDBNull(dr.GetOrdinal("ApproverId")) ? dr.GetString(dr.GetOrdinal("ApproverId")) : string.Empty;
                        sales.Approver = !dr.IsDBNull(dr.GetOrdinal("Approver")) ? dr.GetString(dr.GetOrdinal("Approver")) : string.Empty;
                        sales.ApproveTime = !dr.IsDBNull(dr.GetOrdinal("ApproveTime")) ? (DateTime?)dr["ApproveTime"] : null;
                        sales.IsCheck = dr.GetByte(dr.GetOrdinal("FinStatus")) == 1 ? true : false;
                        sales.Memo = !dr.IsDBNull(dr.GetOrdinal("Memo")) ? dr.GetString(dr.GetOrdinal("Memo")) : string.Empty;
                        sales.Operator = dr["Operator"].ToString();
                        sales.OperatorId = dr["OperatorId"].ToString();
                        sales.IsGuideRealIncome = dr.GetString(dr.GetOrdinal("IsGuideRealIncome")) == "1" ? true : false;
                        sales.CollectionRefundModeName = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : string.Empty;
                    }
                }
            }
            return sales;

        }
        #endregion

        #region 获取订单批量收款的列表
        /// <summary>
        /// 根据ID获取订单批量收款的列表
        /// </summary>
        /// <param name="orderIds">订单编号的数组</param>
        /// <returns></returns>
        public IList<MTourOrderCollectionSales> GetTourOrderCollectionSalesListByOrderId(params string[] orderIds)
        {
            IList<MTourOrderCollectionSales> list = null;

            string ids = string.Empty;

            for (int i = 0; i < orderIds.Length; i++)
            {
                ids = ids + "'" + orderIds[i] + "'" + ",";
            }
            ids = ids.Substring(0, ids.Length - 1);

            StringBuilder query = new StringBuilder();
            query.Append(" Select ");
            query.Append("tbl_TourOrder.TourId,TourCode,OrderId,OrderCode,RouteName,LDate,tbl_TourOrder.BuyCompanyName,");
            query.Append("ConfirmMoney,CheckMoney");
            query.Append(" From ");
            query.Append(" tbl_TourOrder ");
            query.Append(" left join ");
            query.Append(" tbl_Tour ");
            query.Append(" on ");
            query.Append(" tbl_TourOrder.TourId=tbl_Tour.TourId ");
            query.Append(" Where ");
            query.Append(" OrderId ");
            query.Append(" In ");
            query.AppendFormat(" ({0}) ", ids);

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = _db.ExecuteReader(cmd))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderCollectionSales>();
                    while (dr.Read())
                    {
                        MTourOrderCollectionSales order = new MTourOrderCollectionSales();
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                        order.LDate = !dr.IsDBNull(dr.GetOrdinal("LDate")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("LDate")) : null;
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.NotReceivedMoeny = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney")) - dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                        list.Add(order);

                    }
                }
            }
            return list;
        }
        #endregion

        #region 获取销售收款、退款列表
        /// <summary>
        /// 根据订单的编号、收款类型获取销售收款、退款列表
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="collectionRefundState">收款类型</param>
        /// <returns></returns>
        public IList<MTourOrderSales> GetTourOrderSalesListByOrderId(string orderId,
            CollectionRefundState collectionRefundState)
        {
            IList<MTourOrderSales> list = null;

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("Id,OrderId,IsDaiShou,DaiShouRen,CollectionRefundDate,CollectionRefundOperator,CollectionRefundOperatorID");
            sql.Append(",CollectionRefundAmount,CollectionRefundMode,CollectionRefundState,ApproverDeptId");
            sql.Append(",Approver,ApproverId,ApproveTime,FinStatus,Memo,OperatorId,Operator,IsGuideRealIncome,IssueTime");
            sql.Append(",(Select Name from tbl_ComPayment where tbl_ComPayment.PayMentId=tbl_TourOrderSales.CollectionRefundMode) as Name");
            sql.Append(" ,[T] ");
            sql.Append(" FROM ");
            sql.Append("tbl_TourOrderSales");
            sql.AppendFormat(" Where OrderId='{0}'", orderId);
            sql.Append(" And ");
            sql.AppendFormat("CollectionRefundState='{0}'", (int)collectionRefundState);
            //导游现收未审核则不显现在列表
            sql.Append(" And ((IsGuideRealIncome=1 and FinStatus=1) or (IsGuideRealIncome=0 and FinStatus=0) or (IsGuideRealIncome=0 and FinStatus=1)) ");

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.TourStructure.MTourOrderSales>();
                    while (dr.Read())
                    {
                        MTourOrderSales sales = new MTourOrderSales();
                        sales.Id = dr.GetString(dr.GetOrdinal("Id"));
                        sales.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        sales.IsDaiShou = dr["IsDaiShou"].ToString().Equals("1");
                        sales.DaiShouRen = dr["DaiShouRen"].ToString();
                        sales.CollectionRefundDate = dr.GetDateTime(dr.GetOrdinal("CollectionRefundDate"));
                        sales.CollectionRefundOperator = !dr.IsDBNull(dr.GetOrdinal("CollectionRefundOperator")) ? dr.GetString(dr.GetOrdinal("CollectionRefundOperator")) : string.Empty;
                        sales.CollectionRefundOperatorID = dr.GetString(dr.GetOrdinal("CollectionRefundOperatorID"));
                        sales.CollectionRefundAmount = dr.GetDecimal(dr.GetOrdinal("CollectionRefundAmount"));
                        sales.CollectionRefundMode = dr.GetInt32(dr.GetOrdinal("CollectionRefundMode"));
                        sales.CollectionRefundState = (CollectionRefundState)dr.GetByte(dr.GetOrdinal("CollectionRefundState"));
                        sales.ApproverDeptId = !dr.IsDBNull(dr.GetOrdinal("ApproverDeptId")) ? dr.GetInt32(dr.GetOrdinal("ApproverDeptId")) : 0;
                        sales.ApproverId = !dr.IsDBNull(dr.GetOrdinal("ApproverId")) ? dr.GetString(dr.GetOrdinal("ApproverId")) : string.Empty;
                        sales.Approver = !dr.IsDBNull(dr.GetOrdinal("Approver")) ? dr.GetString(dr.GetOrdinal("Approver")) : string.Empty;
                        sales.ApproveTime = !dr.IsDBNull(dr.GetOrdinal("ApproveTime")) ? (DateTime?)dr["ApproveTime"] : null;
                        sales.IsCheck = dr.GetByte(dr.GetOrdinal("FinStatus")) == 1 ? true : false;
                        sales.Memo = !dr.IsDBNull(dr.GetOrdinal("Memo")) ? dr.GetString(dr.GetOrdinal("Memo")) : string.Empty;
                        sales.Operator = dr["Operator"].ToString();
                        sales.OperatorId = dr["OperatorId"].ToString();
                        sales.IsGuideRealIncome = dr.GetString(dr.GetOrdinal("IsGuideRealIncome")) == "1" ? true : false;
                        sales.CollectionRefundModeName = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : string.Empty;
                        sales.ShouKuanType = (EyouSoft.Model.EnumType.FinStructure.ShouKuanType)dr.GetByte(dr.GetOrdinal("T"));
                        list.Add(sales);
                    }
                }
            }

            return list;
        }
        #endregion

        #region 导游实收相关操作(导游收款)
        /// <summary>
        /// 导游实收相关操作
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="guideIncome">导游应收</param>
        /// <param name="guideRealIncome">导游实收</param>
        /// <param name="guideRemark">导游实收的备注</param>
        /// <param name="sales">导游收款的记录</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdateGuideRealIncome(
            string orderId,
            decimal guideIncome,
            decimal guideRealIncome,
            string guideRemark,
            MTourOrderSales sales)
        {
            string tourOrderSalesXml = string.Empty;
            if (null != sales)
            {
                sales.OrderId = orderId;
                sales.CollectionRefundDate = DateTime.Now;
                sales.CollectionRefundState = CollectionRefundState.收款;
                sales.CollectionRefundAmount = guideRealIncome;
                sales.IsGuideRealIncome = true;

                tourOrderSalesXml = this.GetXmlByMTourOrderSales(sales);
            }
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_GuideRealIncome");
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            _db.AddInParameter(cmd, "GuideIncome", DbType.Currency, guideIncome);
            _db.AddInParameter(cmd, "GuideRealIncome", DbType.Currency, guideRealIncome);
            _db.AddInParameter(cmd, "GuideRemark", DbType.String, guideRemark);
            _db.AddInParameter(cmd, "TourOrderSales", DbType.String, tourOrderSalesXml);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, _db);
            return Utils.GetInt(_db.GetParameterValue(cmd, "Result").ToString(), 0);
        }
        #endregion

        #region 根据获取导游收款的订单
        /// <summary>
        /// 根据获取导游收款的订单
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MTourOrder> GetGuideOrderListById(string tourId)
        {
            IList<MTourOrder> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT ");
            query.Append("OrderId,OrderCode,CompanyId,TourId,BuyCountryId,BuyProvincesId");
            query.Append(",BuyCompanyName,BuyCompanyId,ContactDepartId,ContactName,ContactTel");
            query.Append(",DCompanyName,DContactName,DContactTel,SellerName,SellerId,DeptId");
            query.Append(",Operator,OperatorId,Adults,Childs,Others,AdultPrice,ChildPrice");
            query.Append(",OtherPrice,PriceStandId,LevId,PeerLevId,PeerAdultPrice,PeerChildPrice");
            query.Append(",SettlementMoney,ConfirmSettlementMoney,SettlementPeople,SettlementPeopleId");
            query.Append(",SaleAddCost,SaleReduceCost,SaleAddCostRemark,SaleReduceCostRemark");
            query.Append(",PeerAddCost,PeerReduceCost,PeerAddCostRemark,PeerReduceCostRemark,OtherCost");
            query.Append(",SumPrice,SumPriceAddCost,SumPriceReduceCost,SumPriceAddCostRemark,SumPriceReduceCostRemark");
            query.Append(",ConfirmMoney,ConfirmMoneyStatus,ConfirmPeople,ConfirmPeopleId,ConfirmRemark");
            query.Append(",Profit,SalerIncome,GuideIncome,GuideRealIncome,GuideRemark");
            query.Append(",OrderRemark,SaveSeatDate");
            query.Append(",TourType,OrderType,Status,IssueTime");
            query.Append(",CheckMoney,ReturnMoney,ReceivedMoney");
            query.Append(",TheNum,ContractId,ContractCode,TravellerFile");
            query.Append(",PayMentMonth,PayMentDay,PayMentAccountId");
            query.Append(" FROM ");
            query.Append(" tbl_TourOrder ");
            query.Append(" Where ");
            query.AppendFormat(" TourId='{0}' AND IsDelete='0' ", tourId);
            query.Append(" AND (OrderId in (select OrderId from tbl_TourOrderSales where IsGuideRealIncome='1') or guideincome>0) ");

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrder>();

                    while (dr.Read())
                    {
                        MTourOrder order = new MTourOrder();
                        order.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                        order.OrderCode = !dr.IsDBNull(dr.GetOrdinal("OrderCode")) ? dr.GetString(dr.GetOrdinal("OrderCode")) : string.Empty;
                        order.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                        order.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                        order.BuyCountryId = !dr.IsDBNull(dr.GetOrdinal("BuyCountryId")) ? dr.GetInt32(dr.GetOrdinal("BuyCountryId")) : 0;
                        order.BuyProvincesId = !dr.IsDBNull(dr.GetOrdinal("BuyProvincesId")) ? dr.GetInt32(dr.GetOrdinal("BuyProvincesId")) : 0;
                        order.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                        order.BuyCompanyId = dr["BuyCompanyId"].ToString();
                        order.ContactDepartId = !dr.IsDBNull(dr.GetOrdinal("ContactDepartId")) ? dr.GetString(dr.GetOrdinal("ContactDepartId")) : string.Empty;
                        order.ContactName = !dr.IsDBNull(dr.GetOrdinal("ContactName")) ? dr.GetString(dr.GetOrdinal("ContactName")) : string.Empty;
                        order.ContactTel = !dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? dr.GetString(dr.GetOrdinal("ContactTel")) : string.Empty;
                        order.DCompanyName = !dr.IsDBNull(dr.GetOrdinal("DCompanyName")) ? dr.GetString(dr.GetOrdinal("DCompanyName")) : string.Empty;
                        order.DContactName = !dr.IsDBNull(dr.GetOrdinal("DContactName")) ? dr.GetString(dr.GetOrdinal("DContactName")) : string.Empty;
                        order.DContactTel = !dr.IsDBNull(dr.GetOrdinal("DContactTel")) ? dr.GetString(dr.GetOrdinal("DContactTel")) : string.Empty;

                        order.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                        order.SellerId = dr["SellerId"].ToString();
                        order.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));

                        order.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                        order.OperatorId = dr["OperatorId"].ToString();

                        order.Adults = dr.GetInt32(dr.GetOrdinal("Adults"));
                        order.Childs = dr.GetInt32(dr.GetOrdinal("Childs"));
                        order.Others = dr.GetInt32(dr.GetOrdinal("Others"));
                        order.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                        order.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                        order.OtherPrice = dr.GetDecimal(dr.GetOrdinal("OtherPrice"));

                        order.SalerIncome = dr.GetDecimal(dr.GetOrdinal("SalerIncome"));
                        order.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                        order.GuideRealIncome = dr.GetDecimal(dr.GetOrdinal("GuideRealIncome"));
                        order.GuideRemark = !dr.IsDBNull(dr.GetOrdinal("GuideRemark")) ? dr.GetString(dr.GetOrdinal("GuideRemark")) : string.Empty;

                        order.PriceStandId = dr.GetInt32(dr.GetOrdinal("PriceStandId"));
                        order.LevId = dr.GetInt32(dr.GetOrdinal("LevId"));
                        order.PeerLevId = dr.GetInt32(dr.GetOrdinal("PeerLevId"));

                        order.PeerAdultPrice = dr.GetDecimal(dr.GetOrdinal("PeerAdultPrice"));
                        order.PeerChildPrice = dr.GetDecimal(dr.GetOrdinal("PeerChildPrice"));
                        order.PeerAddCost = dr.GetDecimal(dr.GetOrdinal("PeerAddCost"));
                        order.PeerReduceCost = dr.GetDecimal(dr.GetOrdinal("PeerReduceCost"));
                        order.PeerAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerAddCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerAddCostRemark")) : string.Empty;
                        order.PeerReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("PeerReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("PeerReduceCostRemark")) : string.Empty;
                        order.SettlementMoney = dr.GetDecimal(dr.GetOrdinal("SettlementMoney"));
                        order.ConfirmSettlementMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmSettlementMoney"));
                        order.SettlementPeople = !dr.IsDBNull(dr.GetOrdinal("SettlementPeople")) ? dr.GetString(dr.GetOrdinal("SettlementPeople")) : string.Empty;
                        order.SettlementPeopleId = !dr.IsDBNull(dr.GetOrdinal("SettlementPeopleId")) ? dr.GetString(dr.GetOrdinal("SettlementPeopleId")) : string.Empty;

                        order.SaleAddCost = dr.GetDecimal(dr.GetOrdinal("SaleAddCost"));
                        order.SaleReduceCost = dr.GetDecimal(dr.GetOrdinal("SaleReduceCost"));
                        order.SaleAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleAddCostRemark")) : string.Empty;
                        order.SaleReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SaleReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SaleReduceCostRemark")) : string.Empty;

                        order.OtherCost = dr.GetDecimal(dr.GetOrdinal("OtherCost"));

                        order.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                        order.SumPriceAddCost = dr.GetDecimal(dr.GetOrdinal("SumPriceAddCost"));
                        order.SumPriceAddCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceAddCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceAddCostRemark")) : string.Empty;
                        order.SumPriceReduceCost = dr.GetDecimal(dr.GetOrdinal("SumPriceReduceCost"));
                        order.SumPriceReduceCostRemark = !dr.IsDBNull(dr.GetOrdinal("SumPriceReduceCostRemark")) ? dr.GetString(dr.GetOrdinal("SumPriceReduceCostRemark")) : string.Empty;
                        order.ConfirmMoney = dr.GetDecimal(dr.GetOrdinal("ConfirmMoney"));
                        order.ConfirmMoneyStatus = dr.GetString(dr.GetOrdinal("ConfirmMoneyStatus")) == "1" ? true : false;
                        order.ConfirmRemark = !dr.IsDBNull(dr.GetOrdinal("ConfirmRemark")) ? dr.GetString(dr.GetOrdinal("ConfirmRemark")) : string.Empty;
                        order.ConfirmPeople = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeople")) ? dr.GetString(dr.GetOrdinal("ConfirmPeople")) : string.Empty;
                        order.ConfirmPeopleId = !dr.IsDBNull(dr.GetOrdinal("ConfirmPeopleId")) ? dr.GetString(dr.GetOrdinal("ConfirmPeopleId")) : string.Empty;


                        order.Profit = dr.GetDecimal(dr.GetOrdinal("Profit"));

                        order.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                        order.OrderType = (OrderType)dr.GetByte(dr.GetOrdinal("OrderType"));
                        order.OrderStatus = (OrderStatus)dr.GetByte(dr.GetOrdinal("Status"));


                        order.OrderRemark = !dr.IsDBNull(dr.GetOrdinal("OrderRemark")) ? dr.GetString(dr.GetOrdinal("OrderRemark")) : string.Empty;
                        order.ContractId = !dr.IsDBNull(dr.GetOrdinal("ContractId")) ? dr.GetString(dr.GetOrdinal("ContractId")) : string.Empty;
                        order.ContractCode = !dr.IsDBNull(dr.GetOrdinal("ContractCode")) ? dr.GetString(dr.GetOrdinal("ContractCode")) : string.Empty;
                        order.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                        order.PayMentMonth = !dr.IsDBNull(dr.GetOrdinal("PayMentMonth")) ? dr.GetString(dr.GetOrdinal("PayMentMonth")) : string.Empty;
                        order.PayMentDay = !dr.IsDBNull(dr.GetOrdinal("PayMentDay")) ? dr.GetString(dr.GetOrdinal("PayMentDay")) : string.Empty;
                        order.PayMentAccountId = !dr.IsDBNull(dr.GetOrdinal("PayMentAccountId")) ? dr.GetInt32(dr.GetOrdinal("PayMentAccountId")) : 0;

                        order.CheckMoney = dr.GetDecimal(dr.GetOrdinal("CheckMoney"));
                        order.ReturnMoney = dr.GetDecimal(dr.GetOrdinal("ReturnMoney"));
                        order.ReceivedMoney = dr.GetDecimal(dr.GetOrdinal("ReceivedMoney"));


                        list.Add(order);
                    }


                }
            }

            return list;

        }
        #endregion

        #region 私有方法  实体、集合——xml的转换

        #region 订单游客的集合转换为xml
        /// <summary>
        /// 订单游客的集合转换为xml
        /// </summary>
        /// <param name="list">订单游客的集合</param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        private string GetXmlByMTourOrderTraveller(IList<MTourOrderTraveller> list, string orderId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            foreach (MTourOrderTraveller traveller in list)
            {
                query.Append("<Item ");
                query.AppendFormat("TravellerId=\"{0}\" ", string.IsNullOrEmpty(traveller.TravellerId) ? Guid.NewGuid().ToString() : traveller.TravellerId);
                query.AppendFormat("OrderId=\"{0}\" ", orderId);
                query.AppendFormat("CnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.CnName));
                query.AppendFormat("EnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.EnName));
                query.AppendFormat("CardId=\"{0}\" ", traveller.CardId);
                query.AppendFormat("VisitorType=\"{0}\" ", (int?)traveller.VisitorType);
                query.AppendFormat("CardType=\"{0}\" ", (int?)traveller.CardType);
                query.AppendFormat("CardNumber=\"{0}\" ", traveller.CardNumber);
                query.AppendFormat("CardValidDate=\"{0}\" ", traveller.CardValidDate);
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
                query.AppendFormat("RRemark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.RRemark));
                query.AppendFormat("IsInsurance=\"{0}\" ", traveller.IsInsurance == true ? 1 : 0);
                query.AppendFormat("LiCheng=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(traveller.LiCheng));
                query.Append("/>");
            }
            query.Append("</Root>");

            return query.ToString();
        }

        #endregion

        #region 游客的保险集合信息转换为xml
        /// <summary>
        /// 游客的保险集合信息转换为xml
        /// </summary>
        /// <param name="list">游客的保险集合</param>
        /// <returns></returns>
        private string GetXmlbyMTourOrderTravellerInsurance(IList<MTourOrderTravellerInsurance> list)
        {
            //xml:<Root><Item TravellerId="1" InsuranceType  BuyNum  UnitPrice SumPrice /></Root>
            StringBuilder sb = new StringBuilder();
            sb.Append("<Root>");
            foreach (MTourOrderTravellerInsurance travellerInsurance in list)
            {
                sb.AppendFormat("<Item TravellerId=\"{0}\" InsuranceId=\"{1}\"  BuyNum=\"{2}\"  UnitPrice=\"{3}\" SumPrice=\"{4}\" />", travellerInsurance.TravellerId, travellerInsurance.InsuranceId, travellerInsurance.BuyNum, travellerInsurance.UnitPrice, travellerInsurance.SumPrice);

            }
            sb.Append("</Root>");
            return sb.ToString();
        }
        #endregion

        #region 销售收款的集合装换为xml
        /// <summary>
        /// 销售收款的集合装换为xml
        /// </summary>
        /// <param name="list">订单批量收款的集合</param>
        /// <returns></returns>
        private string GetXmlByMTourOrderSales(IList<MTourOrderSales> list)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            foreach (MTourOrderSales sale in list)
            {
                query.Append("<Item ");
                query.AppendFormat("Id=\"{0}\" ", string.IsNullOrEmpty(sale.Id) ? Guid.NewGuid().ToString() : sale.Id);
                query.AppendFormat("OrderId=\"{0}\" ", sale.OrderId);
                query.AppendFormat("CollectionRefundDate=\"{0}\" ", sale.CollectionRefundDate);
                query.AppendFormat("CollectionRefundOperator=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(sale.CollectionRefundOperator));
                query.AppendFormat("CollectionRefundOperatorID=\"{0}\" ", sale.CollectionRefundOperatorID);
                query.AppendFormat("CollectionRefundAmount=\"{0}\" ", sale.CollectionRefundAmount);
                query.AppendFormat("CollectionRefundMode=\"{0}\" ", (int)sale.CollectionRefundMode);
                query.AppendFormat("CollectionRefundState=\"{0}\" ", (int)sale.CollectionRefundState);
                query.AppendFormat("ApproverDeptId=\"{0}\" ", sale.ApproverDeptId);
                query.AppendFormat("ApproverId=\"{0}\" ", sale.ApproverId);
                query.AppendFormat("Approver=\"{0}\" ", sale.Approver);
                query.AppendFormat("ApproveTime=\"{0}\" ", sale.ApproveTime);
                query.AppendFormat("IsCheck=\"{0}\" ", sale.IsCheck == true ? 1 : 0);
                query.AppendFormat("Memo=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(sale.Memo));
                query.AppendFormat("OperatorId=\"{0}\" ", sale.OperatorId);
                query.AppendFormat("Operator=\"{0}\" ", sale.Operator);
                query.AppendFormat("IsGuideRealIncome=\"{0}\" ", sale.IsGuideRealIncome == true ? 1 : 0);
                query.AppendFormat("IssueTime=\"{0}\" ", DateTime.Now);
                query.AppendFormat(" T=\"{0}\" ", (int)sale.ShouKuanType);
                query.Append(" />");

            }
            query.Append("</Root>");

            return query.ToString();
        }
        #endregion

        #region 销售收款的集合装换为xml
        /// <summary>
        /// 订单收款实体转换为xml
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        private string GetXmlByMTourOrderSales(MTourOrderSales sale)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            query.Append("<Item ");
            query.AppendFormat("Id=\"{0}\" ", string.IsNullOrEmpty(sale.Id) ? Guid.NewGuid().ToString() : sale.Id);
            query.AppendFormat("OrderId=\"{0}\" ", sale.OrderId);
            query.AppendFormat("CollectionRefundDate=\"{0}\" ", sale.CollectionRefundDate);
            query.AppendFormat("CollectionRefundOperator=\"{0}\" ", sale.CollectionRefundOperator);
            query.AppendFormat("CollectionRefundOperatorID=\"{0}\" ", sale.CollectionRefundOperatorID);
            query.AppendFormat("CollectionRefundAmount=\"{0}\" ", sale.CollectionRefundAmount);
            query.AppendFormat("CollectionRefundMode=\"{0}\" ", (int)sale.CollectionRefundMode);
            query.AppendFormat("CollectionRefundState=\"{0}\" ", (int)sale.CollectionRefundState);
            query.AppendFormat("ApproverDeptId=\"{0}\" ", sale.ApproverDeptId);
            query.AppendFormat("ApproverId=\"{0}\" ", sale.ApproverId);
            query.AppendFormat("Approver=\"{0}\" ", sale.Approver);
            query.AppendFormat("ApproveTime=\"{0}\" ", sale.ApproveTime);
            query.AppendFormat("IsCheck=\"{0}\" ", sale.IsCheck == true ? 1 : 0);
            query.AppendFormat("Memo=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(sale.Memo));
            query.AppendFormat("OperatorId=\"{0}\" ", sale.OperatorId);
            query.AppendFormat("Operator=\"{0}\" ", sale.Operator);
            query.AppendFormat("IsGuideRealIncome=\"{0}\" ", sale.IsGuideRealIncome == true ? 1 : 0);
            query.AppendFormat("IssueTime=\"{0}\" ", DateTime.Now);
            query.AppendFormat(" T=\"{0}\" ", (int)sale.ShouKuanType);
            query.Append(" />");
            query.Append("</Root>");

            return query.ToString();
        }
        #endregion

        #region 订单变更的实体转换为xml
        /// <summary>
        /// 订单变更的实体转换为xml
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        public string GetTourOrderChangeXml(MTourOrderChange change)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            query.Append("<Item ");
            query.AppendFormat("Id=\"{0}\" ", string.IsNullOrEmpty(change.Id) ? Guid.NewGuid().ToString() : change.Id);
            query.AppendFormat("CompanyId=\"{0}\" ", change.CompanyId);
            query.AppendFormat("TourId=\"{0}\" ", change.TourId);
            query.AppendFormat("TourCode=\"{0}\" ", change.TourCode);
            query.AppendFormat("RouteId=\"{0}\" ", change.RouteId);
            query.AppendFormat("RouteName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(change.RouteName));
            query.AppendFormat("TourSaleId=\"{0}\" ", change.TourSaleId);
            query.AppendFormat("TourSale=\"{0}\" ", change.TourSale);
            query.AppendFormat("OrderId=\"{0}\" ", change.OrderId);
            query.AppendFormat("OrderCode=\"{0}\" ", change.OrderCode);
            query.AppendFormat("ChangePerson=\"{0}\" ", change.ChangePerson);
            query.AppendFormat("ChangePrice=\"{0}\" ", change.ChangePrice);
            query.AppendFormat("OrderSaleId=\"{0}\" ", change.OrderSaleId);
            query.AppendFormat("OrderSale=\"{0}\" ", change.OrderSale);
            query.AppendFormat("Content=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(change.Content));
            query.AppendFormat("OperatorId=\"{0}\" ", change.OperatorId);
            query.AppendFormat("Operator=\"{0}\" ", change.Operator);
            query.AppendFormat("IssueTime=\"\" ", DateTime.Now);
            query.AppendFormat("ChangeType=\"{0}\" ", (int)change.ChangeType);
            query.AppendFormat("IsSure=\"{0}\" ", change.IsSure == true ? 1 : 0);
            query.AppendFormat("SurePersonId=\"{0}\" ", change.SurePersonId);
            query.AppendFormat("SurePerson=\"{0}\" ", change.SurePerson);
            query.AppendFormat("SureTime=\"{0}\" ", change.SureTime);
            query.AppendFormat("/>");
            query.Append("</Root>");
            return query.ToString();
        }
        #endregion

        #region 订单确认增减列表转xml
        /// <summary>
        /// 订单确认增减列表转xml
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public string GetOrderChangeXml(IList<MOrderSalesConfirm> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row OrderId='{0}'", i.OrderId);
                    sb.AppendFormat(" AddFee='{0}'", i.AddFee);
                    sb.AppendFormat(" AddRemark='{0}'", Utils.ReplaceXmlSpecialCharacter(i.AddRemark));
                    sb.AppendFormat(" RedFee='{0}'", i.RedFee);
                    sb.AppendFormat(" RedRemark='{0}'", Utils.ReplaceXmlSpecialCharacter(i.RedRemark));
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion


        #region
        /// <summary>
        ///  创建供应商安排的XML
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        private string CreatePlanBaseInfoXml(EyouSoft.Model.PlanStructure.MPlanBaseInfo plan)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            query.AppendFormat("<Item PlanId=\"{0}\" ", string.IsNullOrEmpty(plan.PlanId) ? Guid.NewGuid().ToString() : plan.PlanId);
            query.AppendFormat("CompanyId=\"{0}\" ", plan.CompanyId);
            query.AppendFormat("TourId=\"{0}\" ", plan.TourId);
            query.AppendFormat("Type=\"{0}\" ", (int)plan.Type);

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
            query.AppendFormat("Remarks=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.Remarks));
            query.AppendFormat("SueId=\"{0}\" ", plan.SueId);
            query.AppendFormat("CostId=\"{0}\" ", plan.CostId);
            query.AppendFormat("CostName=\"{0}\" ", plan.CostName);
            query.AppendFormat("CostStatus=\"{0}\" ", plan.CostStatus);
            query.AppendFormat("CostTime=\"{0}\" ", plan.CostTime);
            query.AppendFormat("Confirmation=\"{0}\" ", plan.Confirmation);
            query.AppendFormat("CostRemarks=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(plan.CostRemarks));
            query.AppendFormat("DeptId=\"{0}\" ", plan.DeptId);
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
            query.Append("</Root>");
            return query.ToString();
        }

        #endregion





        #region 创建垫付申请XML
        /// <summary>
        /// 创建垫付申请XML
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string CreateOverrunXml(EyouSoft.Model.TourStructure.MAdvanceApp info)
        {
            if (info == null) return null;
            //<Root><Item ApplierId="申请人编号" Applier="申请人" ApplyTime="申请时间" DisburseAmount="垫付金额" DeptId="操作人部门编号" Remark="备注"/></Root>
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<Item ApplierId=\"{0}\" Applier=\"{1}\" ApplyTime=\"{2}\" DisburseAmount=\"{3}\" DeptId=\"{4}\" Remark=\"{5}\" />", info.ApplierId, info.Applier, info.ApplyTime, info.DisburseAmount, info.DeptId, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(info.Remark));
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }
        #endregion

        #endregion

        #region 私有方法 xml——集合的转换

        #region 根据游客保险的xml——IList<MTourOrderTravellerInsurance>
        /// <summary>
        /// 根据游客保险的xml字段格式获取游客保险列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourOrderTravellerInsurance> GetTravellerInsuranceByXml(string xml)
        {
            IList<MTourOrderTravellerInsurance> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                if (root.HasChildNodes)
                {
                    list = new List<MTourOrderTravellerInsurance>();
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlNode child = root.ChildNodes[i];
                        MTourOrderTravellerInsurance insurance = new MTourOrderTravellerInsurance();
                        insurance.TravellerId = child.Attributes["TravellerId"].Value;
                        insurance.InsuranceId = child.Attributes["InsuranceId"].Value;
                        insurance.SumPrice = Utils.GetDecimal(child.Attributes["SumPrice"].Value);
                        insurance.BuyNum = Utils.GetInt(child.Attributes["BuyNum"].Value);
                        insurance.UnitPrice = Utils.GetDecimal(child.Attributes["UnitPrice"].Value);
                        list.Add(insurance);
                    }
                }
            }

            return list;
        }
        #endregion


        #region  团队计划分项报价的Xml—— IList<MTourTeamPrice>
        /// <summary>
        /// 根据团队计划分项报价Xml转换为集合
        /// </summary>
        /// <param name="tourTeamPriceXml"></param>
        /// <returns></returns>
        public IList<MTourTeamPrice> GetTourTeamPriceByXml(string tourTeamPriceXml)
        {
            IList<MTourTeamPrice> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(tourTeamPriceXml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<MTourTeamPrice>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode child = root.ChildNodes[i];
                    MTourTeamPrice tprice = new MTourTeamPrice();
                    tprice.TourId = child.Attributes["TourId"].Value;
                    tprice.Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)Utils.GetInt(child.Attributes["Unit"].Value);
                    tprice.Quote = Utils.GetDecimal(child.Attributes["Quote"].Value);
                    tprice.ServiceStandard = child.Attributes["ServiceStandard"] != null ? child.Attributes["ServiceStandard"].Value : string.Empty;
                    tprice.ServiceType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(child.Attributes["ServiceType"].Value);
                    tprice.ServiceName = child.Attributes["ServiceName"] != null ? child.Attributes["ServiceName"].Value : string.Empty;
                    tprice.ServiceId = child.Attributes["ServiceId"] != null ? child.Attributes["ServiceId"].Value : string.Empty;
                    tprice.Remark = child.Attributes["Remark"] != null ? child.Attributes["Remark"].Value : string.Empty;
                    list.Add(tprice);
                }
            }

            return list;
        }
        #endregion

        #region 销售退款的xml——IList<MTourOrderSales>
        /// <summary>
        /// 根据销售退款的xml转换为集合
        /// </summary>
        /// <param name="TourOrderSalesXml"></param>
        /// <returns></returns>
        public IList<MTourOrderSales> GetTourOrderSalesByXml(string TourOrderSalesXml)
        {
            IList<MTourOrderSales> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TourOrderSalesXml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<MTourOrderSales>();

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode child = root.ChildNodes[i];
                    MTourOrderSales sales = new MTourOrderSales();
                    sales.Id = child.Attributes["Id"] != null ? child.Attributes["Id"].Value : string.Empty;
                    sales.OrderId = child.Attributes["OrderId"] != null ? child.Attributes["OrderId"].Value : string.Empty;
                    sales.CollectionRefundDate = child.Attributes["CollectionRefundDate"] != null ? Utils.GetDateTime(child.Attributes["CollectionRefundDate"].Value) : (DateTime?)null;
                    sales.CollectionRefundOperator = child.Attributes["CollectionRefundOperator"] != null ? child.Attributes["CollectionRefundOperator"].Value : string.Empty;
                    sales.CollectionRefundOperatorID = child.Attributes["CollectionRefundOperatorID"] != null ? child.Attributes["CollectionRefundOperatorID"].Value : string.Empty;
                    sales.CollectionRefundAmount = child.Attributes["CollectionRefundAmount"] != null ? Utils.GetDecimal(child.Attributes["CollectionRefundAmount"].Value) : 0;
                    sales.CollectionRefundMode = child.Attributes["CollectionRefundMode"] != null ? Utils.GetInt(child.Attributes["CollectionRefundMode"].Value) : 0;
                    sales.CollectionRefundState = child.Attributes["CollectionRefundState"] != null ? (CollectionRefundState)Utils.GetInt(child.Attributes["CollectionRefundState"].Value) : 0;
                    sales.ApproverDeptId = child.Attributes["ApproverDeptId"] != null ? Utils.GetInt(child.Attributes["ApproverDeptId"].Value) : 0;
                    sales.ApproverId = child.Attributes["ApproverId"] != null ? child.Attributes["ApproverId"].Value : string.Empty;
                    sales.Approver = child.Attributes["Approver"] != null ? child.Attributes["Approver"].Value : string.Empty;
                    sales.ApproveTime = child.Attributes["ApproveTime"] != null ? Utils.GetDateTime(child.Attributes["ApproveTime"].Value) : (DateTime?)null;
                    sales.IsCheck = child.Attributes["IsCheck"] != null ? (child.Attributes["IsCheck"].Value == "1" ? true : false) : false;
                    sales.Memo = child.Attributes["Memo"] != null ? child.Attributes["Memo"].Value : string.Empty;
                    sales.OperatorId = child.Attributes["OperatorId"] != null ? child.Attributes["OperatorId"].Value : string.Empty;
                    sales.Operator = child.Attributes["Operator"] != null ? child.Attributes["Operator"].Value : string.Empty;
                    sales.IsGuideRealIncome = child.Attributes["IsGuideRealIncome"] != null ? (child.Attributes["IsGuideRealIncome"].Value == "1" ? true : false) : false;
                    sales.CollectionRefundModeName = child.Attributes["Name"] != null ? child.Attributes["Name"].Value : string.Empty;
                    list.Add(sales);
                }


            }

            return list;
        }
        #endregion

        #region 散拼价格组成的xml——MTourPriceStandard
        /// <summary>
        /// 根据散拼价格组成的xml获取实体
        /// </summary>
        /// <param name="TourPriceStandardXml"></param>
        /// <returns></returns>
        public MTourPriceStandard GetTourPriceStandardByXml(string TourPriceStandardXml)
        {

            MTourPriceStandard standard = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TourPriceStandardXml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                standard = new MTourPriceStandard();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode single = root.ChildNodes[i];
                    standard.Id = Utils.GetInt(single.Attributes["Id"].Value);
                    standard.TourId = single.Attributes["TourId"].Value;
                    standard.Standard = Utils.GetInt(single.Attributes["Standard"].Value);
                    standard.StandardName = single.Attributes["StandardName"].Value;
                    standard.PriceLevel = new List<MTourPriceLevel>();
                    for (int j = 0; j < root.ChildNodes.Count; j++)
                    {
                        MTourPriceLevel level = new MTourPriceLevel();
                        XmlNode child = root.ChildNodes[j];
                        if (child.Attributes["Standard"].Value == single.Attributes["Standard"].Value)
                        {
                            level.LevelId = Utils.GetInt(child.Attributes["LevelId"].Value);
                            level.LevelName = child.Attributes["LevelName"] != null ? child.Attributes["LevelName"].Value : string.Empty;
                            level.AdultPrice = Utils.GetDecimal(child.Attributes["AdultPrice"].Value);
                            level.ChildPrice = Utils.GetDecimal(child.Attributes["ChildPrice"].Value);
                        }

                    }
                }
            }
            return standard;
        }
        #endregion


        #region 订单游客的xml（游客保险的xml）——IList<MTourOrderTraveller>
        /// <summary>
        /// 订单游客的xml（游客保险的xml）获取订单游客、保险信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourOrderTraveller> GetTourOrderTravellerXml(string xml)
        {
            xml = xml.Replace("&lt;", "<").Replace("&gt;", ">");
            IList<MTourOrderTraveller> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<MTourOrderTraveller>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {

                    XmlNode child = root.ChildNodes[i];

                    MTourOrderTraveller traveller = new MTourOrderTraveller();
                    traveller.TravellerId = child["TravellerId"].InnerText;
                    traveller.OrderId = child["OrderId"].InnerText;
                    traveller.CnName = child["CnName"] != null ? child["CnName"].InnerText : string.Empty;
                    traveller.EnName = child["EnName"] != null ? child["EnName"].InnerText : string.Empty;
                    traveller.CardId = child["CardId"] != null ? child["CardId"].InnerText : string.Empty;
                    traveller.VisitorType = child["VisitorType"] != null ? (VisitorType?)Enum.Parse(typeof(VisitorType), child["VisitorType"].InnerText) : null;
                    traveller.CardType = child["CardType"] != null ? (CardType?)Enum.Parse(typeof(CardType), child["CardType"].InnerText) : null;
                    traveller.CardNumber = child["CardNumber"] != null ? child["CardNumber"].InnerText : string.Empty;
                    traveller.CardValidDate = child["CardValidDate"] != null ? child["CardValidDate"].InnerText : string.Empty;

                    traveller.VisaStatus = child["VisaStatus"] != null ? (VisaStatus?)Enum.Parse(typeof(VisaStatus), child["VisaStatus"].InnerText) : null;
                    traveller.IsCardTransact = child["IsCardTransact"].InnerText == "1" ? true : false;
                    traveller.Gender = child["Gender"] != null ? (EyouSoft.Model.EnumType.GovStructure.Gender?)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), child["Gender"].InnerText) : null;

                    traveller.Contact = child["Contact"] != null ? child["Contact"].InnerText : string.Empty;
                    traveller.LNotice = child["LNotice"].InnerText == "1" ? true : false;
                    traveller.RNotice = child["RNotice"].InnerText == "1" ? true : false;
                    traveller.Remark = child["Remark"] != null ? child["Remark"].InnerText : string.Empty;
                    traveller.TravellerStatus = (TravellerStatus)Utils.GetInt(child["Status"].InnerText);
                    traveller.RAmount = Utils.GetDecimal(child["RAmount"].InnerText);
                    traveller.RAmountRemark = child["RAmountRemark"] != null ? child["RAmountRemark"].InnerText : string.Empty;
                    traveller.RTime = child["RTime"] != null ? (DateTime?)Utils.GetDateTime(child["RTime"].InnerText) : null;
                    traveller.RRemark = child["RRemark"] != null ? child["RRemark"].InnerText : string.Empty;
                    //traveller.IsInsurance = child["IsInsurance"].InnerText == "1" ? true : false;
                    if (traveller.IsInsurance)
                    {
                        if (child["TourOrderTravellerInsurance"] != null && child["TourOrderTravellerInsurance"].HasChildNodes)
                        {
                            traveller.OrderTravellerInsuranceList = new List<MTourOrderTravellerInsurance>();
                            for (int j = 0; j < child["TourOrderTravellerInsurance"].ChildNodes.Count; j++)
                            {
                                XmlNode Insurance = child["TourOrderTravellerInsurance"].ChildNodes[j];
                                MTourOrderTravellerInsurance travellerInsurance = new MTourOrderTravellerInsurance();
                                travellerInsurance.TravellerId = Insurance["TravellerId"].InnerText;
                                travellerInsurance.InsuranceId = Insurance["InsuranceId"].InnerText;
                                travellerInsurance.BuyNum = Utils.GetInt(Insurance["BuyNum"].InnerText);
                                travellerInsurance.UnitPrice = Utils.GetDecimal(Insurance["UnitPrice"].InnerText);
                                travellerInsurance.SumPrice = Utils.GetDecimal(Insurance["SumPrice"].InnerText);
                                traveller.OrderTravellerInsuranceList.Add(travellerInsurance);
                            }
                        }
                    }

                    list.Add(traveller);
                }
            }

            return list;
        }
        #endregion

        #region 获取销售员信息（分销商平台区域浮动信息线路）
        /// <summary>
        ///  获取销售员信息（分销商平台区域浮动信息线路）
        /// </summary>
        /// <param name="xml">xml信息</param>
        /// <param name="ContactName">销售员姓名</param>
        /// <param name="ContactTel">销售员电话</param>
        /// <param name="ContactMobile">销售员手机号</param>
        private void GetSellerContactByXml(string xml, ref string ContactName, ref string ContactTel, ref string ContactMobile)
        {
            //ContactName,ContactTel,ContactMobile
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    ContactName = node.Attributes["ContactName"] != null ? node.Attributes["ContactName"].Value : string.Empty;
                    ContactTel = node.Attributes["ContactTel"] != null ? node.Attributes["ContactTel"].Value : string.Empty;
                    ContactMobile = node.Attributes["ContactMobile"] != null ? node.Attributes["ContactMobile"].Value : string.Empty;
                }
            }
        }
        #endregion

        #region  获取计调员信息（分销商平台区域浮动信息线路）
        /// <summary>
        ///  获取计调员信息（分销商平台区域浮动信息线路）
        /// </summary>
        /// <param name="xml">xml信息</param>
        /// <returns>计调员集合</returns>
        private IList<Planer> GetPlanerByXml(string xml)
        {
            IList<Planer> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<Planer>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    Planer planer = new Planer();
                    XmlNode node = root.ChildNodes[i];
                    planer.ContactName = node.Attributes["ContactName"] != null ? node.Attributes["ContactName"].Value : string.Empty;
                    planer.ContactTel = node.Attributes["ContactTel"] != null ? node.Attributes["ContactTel"].Value : string.Empty;
                    planer.ContactMobile = node.Attributes["ContactMobile"] != null ? node.Attributes["ContactMobile"].Value : string.Empty;
                    list.Add(planer);
                }
            }
            return list;
        }
        #endregion

        #region 获取订单的合计
        /// <summary>
        /// 获取订单的合计
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private MOrderSum GetOrderSumByXml(string xml)
        {
            MOrderSum sum = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("root");
            if (root != null)
            {
                XmlNode row = root.ChildNodes[0];
                sum = new MOrderSum();
                sum.Adults = Utils.GetInt(row.Attributes["Adults"].Value, 0);
                sum.Childs = Utils.GetInt(row.Attributes["Childs"].Value, 0);
                sum.Others = Utils.GetInt(row.Attributes["Others"].Value, 0);
                sum.SumPrice = Utils.GetDecimal(row.Attributes["SumPrice"].Value, 0);
                sum.ConfirmMoney = Utils.GetDecimal(row.Attributes["ConfirmMoney"].Value, 0);
                sum.ConfirmSettlementMoney = Utils.GetDecimal(row.Attributes["ConfirmSettlementMoney"].Value, 0);
                sum.GuideIncome = Utils.GetDecimal(row.Attributes["GuideIncome"].Value, 0);
                sum.GuideRealIncome = Utils.GetDecimal(row.Attributes["GuideRealIncome"].Value, 0);
                sum.SalerIncome = Utils.GetDecimal(row.Attributes["SalerIncome"].Value, 0);
                sum.CheckMoney = Utils.GetDecimal(row.Attributes["CheckMoney"].Value, 0);
                sum.Profit = sum.ConfirmMoney - sum.ConfirmSettlementMoney;
            }
            return sum;
        }
        #endregion

        #region 获取计划计调员信息IList<MTourPlaner>
        /// <summary>
        /// 获取计划计调员信息IList<MTourPlaner>
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourPlaner> GetTourPlanerListByXml(string xml)
        {
            IList<MTourPlaner> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<MTourPlaner>();
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    MTourPlaner planer = new MTourPlaner();
                    XmlNode node = root.ChildNodes[i];
                    planer.DeptId = node.Attributes["DeptId"] != null ? Utils.GetInt(node.Attributes["DeptId"].Value) : 0;
                    planer.Planer = node.Attributes["Planer"] != null ? node.Attributes["Planer"].Value : string.Empty;
                    planer.PlanerId = node.Attributes["PlanerId"] != null ? node.Attributes["PlanerId"].Value : string.Empty;
                    planer.TourId = node.Attributes["TourId"] != null ? node.Attributes["TourId"].Value : string.Empty;
                    list.Add(planer);
                }

            }
            return list;
        }
        #endregion

        #region 获取联系方式（打印单-订单信息汇总）
        /// <summary>
        /// 获取联系方式（打印单）
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tel"></param>
        /// <param name="mobile"></param>
        private void GetContract(string xml, ref string tel, ref string mobile)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    XmlNode node = root.ChildNodes[i];
                    tel = node.Attributes["ContactTel"] != null ? node.Attributes["ContactTel"].Value : string.Empty;
                    mobile = node.Attributes["ContactMobile"] != null ? node.Attributes["ContactMobile"].Value : string.Empty;
                }
            }
        }
        #endregion

        #region 获取订单确认列表
        /// <summary>
        /// 根据xml获取订单确认列表
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns>订单确认列表</returns>
        private static IList<MOrderSalesConfirm> GetOrderSalesConfirmLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MOrderSalesConfirm
            {
                OrderId = Utils.GetXAttributeValue(i, "OrderId"),
                AddFee = Utils.GetDecimal(Utils.GetXAttributeValue(i, "AddFee")),
                AddRemark = Utils.GetXAttributeValue(i, "AddRemark"),
                RedFee = Utils.GetDecimal(Utils.GetXAttributeValue(i, "RedFee")),
                RedRemark = Utils.GetXAttributeValue(i, "RedRemark"),
            }).ToList();
        }
        #endregion

        #endregion

        #region  分销商、供应商订单状态----------------------
        /// <summary>
        /// 根据供应商、分销商的查询订单状态；获取对应的订单状态
        /// 
        /// </summary>
        /// <param name="GroupOrderStatus"></param>
        /// <returns></returns>
        private string GetOrderStatusByGroupOrderStatus(GroupOrderStatus GroupOrderStatus)
        {
            string query = string.Empty;

            switch (GroupOrderStatus)
            {
                case GroupOrderStatus.报名不受理:
                    query = string.Format(" and  Status={0} ", (int)OrderStatus.不受理);
                    break;
                case GroupOrderStatus.报名未确认:
                    query = string.Format(" and  Status={0} ", (int)OrderStatus.未处理);
                    break;
                case GroupOrderStatus.已留位:
                    query = string.Format(" and  Status={0} ", (int)OrderStatus.已留位);
                    break;
                case GroupOrderStatus.已确认:
                    query = string.Format(" and  Status={0} ", (int)OrderStatus.已成交);
                    break;
                case GroupOrderStatus.已取消:
                    query = string.Format(" and  Status={0} ", (int)OrderStatus.已取消);
                    break;

                case GroupOrderStatus.预留不受理:
                    query = string.Format(" and  Status={0} and SaveSeatDate<>'' ", (int)OrderStatus.不受理);
                    break;
                case GroupOrderStatus.预留未确认:
                    query = string.Format(" and  (Status={0} or Status={1}) and SaveSeatDate<>'' ", (int)OrderStatus.未处理, (int)OrderStatus.已留位);
                    break;
                case GroupOrderStatus.预留过期:
                    query = string.Format(" and  Status={0}", (int)OrderStatus.留位过期);
                    break;
                default:
                    break;
            }

            return query;

        }

        /// <summary>
        /// 根据留位时间、订单状态 获取分销商、供应商的订单状态
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <param name="SaveSeatDate"></param>
        /// <returns></returns>
        public GroupOrderStatus GetGroupOrderStatus(OrderStatus OrderStatus, DateTime? SaveSeatDate)
        {
            GroupOrderStatus GroupOrderStatus;
            if (SaveSeatDate.HasValue)
            {
                switch (OrderStatus)
                {
                    case OrderStatus.留位过期:
                        GroupOrderStatus = GroupOrderStatus.预留过期;
                        break;
                    case OrderStatus.不受理:
                        GroupOrderStatus = GroupOrderStatus.预留不受理;
                        break;
                    case OrderStatus.已成交:
                        GroupOrderStatus = GroupOrderStatus.已确认;
                        break;
                    case OrderStatus.未处理:
                        GroupOrderStatus = GroupOrderStatus.预留未确认;
                        break;
                    case OrderStatus.已留位:
                        GroupOrderStatus = GroupOrderStatus.已留位;
                        break;
                    default:
                        GroupOrderStatus = GroupOrderStatus.已取消;
                        break;
                }
            }
            else
            {
                switch (OrderStatus)
                {
                    case OrderStatus.不受理:
                        GroupOrderStatus = GroupOrderStatus.报名不受理;
                        break;
                    case OrderStatus.已成交:
                        GroupOrderStatus = GroupOrderStatus.已确认;
                        break;
                    case OrderStatus.未处理:
                        GroupOrderStatus = GroupOrderStatus.报名未确认;
                        break;
                    default:
                        GroupOrderStatus = GroupOrderStatus.已取消;
                        break;
                }
            }

            return GroupOrderStatus;
        }
        #endregion

        #region 筛选订单状态
        /// <summary>
        /// 刷选订单状态
        /// </summary>
        /// <returns></returns>
        private string NotExitsOrderStatus()
        {
            return string.Format(" and  Status not in ({0},{1},{2},{3}) ",
                (int)OrderStatus.垫付申请审核, (int)OrderStatus.垫付申请审核成功, (int)OrderStatus.垫付申请审核失败, (int)OrderStatus.资金超限);
        }
        #endregion

        #region 处理游客、游客保险(GUID问题)
        /// <summary>
        /// 处理游客、游客保险
        /// </summary>
        /// <param name="list">游客集合</param>
        /// <param name="orderId">订单编号</param>
        /// <param name="travellerInsuranceList">有个保险的集合</param>
        /// <returns></returns>
        private IList<MTourOrderTraveller> GetMTourOrderTravellerList(IList<MTourOrderTraveller> list, string orderId, ref IList<MTourOrderTravellerInsurance> travellerInsuranceList)
        {
            if (list != null && list.Count != 0)
            {
                foreach (MTourOrderTraveller Traveller in list)
                {
                    string travellerId = Guid.NewGuid().ToString();
                    Traveller.TravellerId = string.IsNullOrEmpty(Traveller.TravellerId) ? travellerId : Traveller.TravellerId;
                    Traveller.OrderId = orderId;
                    if (Traveller.OrderTravellerInsuranceList != null && Traveller.OrderTravellerInsuranceList.Count != 0)
                    {
                        Traveller.IsInsurance = true;
                        foreach (MTourOrderTravellerInsurance Insurance in Traveller.OrderTravellerInsuranceList)
                        {
                            Insurance.TravellerId = string.IsNullOrEmpty(Insurance.TravellerId) ? Traveller.TravellerId : Insurance.TravellerId;
                            travellerInsuranceList.Add(Insurance);
                        }
                    }
                }
            }

            return list;
        }
        #endregion

        #region  打印单的方法

        /// <summary>
        /// 打印单游客信息
        /// </summary>
        /// <param name="tourId">团号</param>
        /// <param name="orderStatus">订单状态的数组 null 值查询所有</param>
        /// <returns></returns>
        public IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerById(string tourId, params int[] orderStatus)
        {
            IList<BuyCompanyTraveller> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" select A.*,B.BuyCompanyName,B.SellerName,B.TheNum from  tbl_TourOrderTraveller AS A ");
            query.Append(" inner join tbl_TourOrder AS B ");
            query.Append(" on A.OrderId=B.OrderId ");
            query.AppendFormat(" AND B.tourId='{0}' ", tourId);


            if (orderStatus != null && orderStatus.Length != 0)
            {
                query.AppendFormat(" and B.OrderStatus in ({0}) ", GetIdsByArr(orderStatus));
            }
            //排序
            query.Append(" Order By A.Id asc ");
            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                list = new List<BuyCompanyTraveller>();
                while (dr.Read())
                {
                    BuyCompanyTraveller traveller = new BuyCompanyTraveller();
                    traveller.TravellerId = dr["TravellerId"].ToString();
                    traveller.OrderId = dr["OrderId"].ToString();
                    traveller.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                    traveller.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                    traveller.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                    traveller.VisitorType = !dr.IsDBNull(dr.GetOrdinal("VisitorType")) ? (VisitorType?)dr.GetByte(dr.GetOrdinal("VisitorType")) : null;
                    traveller.CardType = !dr.IsDBNull(dr.GetOrdinal("CardType")) ? (CardType?)dr.GetByte(dr.GetOrdinal("CardType")) : null;
                    traveller.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                    traveller.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                    traveller.VisaStatus = !dr.IsDBNull(dr.GetOrdinal("VisaStatus")) ? (VisaStatus?)dr.GetByte(dr.GetOrdinal("VisaStatus")) : null;
                    traveller.IsCardTransact = dr["IsCardTransact"].ToString().Equals("1") ? true : false;
                    traveller.Gender = !dr.IsDBNull(dr.GetOrdinal("Gender")) ? (EyouSoft.Model.EnumType.GovStructure.Gender?)dr.GetByte(dr.GetOrdinal("Gender")) : null;
                    traveller.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                    traveller.LNotice = dr["LNotice"].ToString().Equals("1") ? true : false;
                    traveller.RNotice = dr["RNotice"].ToString().Equals("1") ? true : false;
                    traveller.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                    traveller.TravellerStatus = (TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    traveller.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                    traveller.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                    traveller.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                    //traveller.IsInsurance = dr["IsInsurance"].ToString().Equals("1") ? true : false;

                    traveller.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    traveller.OrderCode = dr["TheNum"].ToString();
                    traveller.SellerName = dr["SellerName"].ToString();
                    list.Add(traveller);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据订单号获取游客打印名单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerByOrderId(string orderId)
        {
            IList<BuyCompanyTraveller> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" select tbl_TourOrderTraveller.*,BuyCompanyName from  tbl_TourOrderTraveller ");
            query.Append(" inner join tbl_TourOrder ");
            query.Append(" on tbl_TourOrderTraveller.OrderId=tbl_TourOrder.OrderId ");
            query.AppendFormat(" where tbl_TourOrder.OrderId='{0}' ", orderId);
            query.Append(" Order By tbl_TourOrderTraveller.Id asc ");

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                list = new List<BuyCompanyTraveller>();
                while (dr.Read())
                {
                    BuyCompanyTraveller traveller = new BuyCompanyTraveller();
                    traveller.TravellerId = dr["TravellerId"].ToString();
                    traveller.OrderId = dr["OrderId"].ToString();
                    traveller.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                    traveller.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                    traveller.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                    traveller.VisitorType = !dr.IsDBNull(dr.GetOrdinal("VisitorType")) ? (VisitorType?)dr.GetByte(dr.GetOrdinal("VisitorType")) : null;
                    traveller.CardType = !dr.IsDBNull(dr.GetOrdinal("CardType")) ? (CardType?)dr.GetByte(dr.GetOrdinal("CardType")) : null;
                    traveller.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                    traveller.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                    traveller.VisaStatus = !dr.IsDBNull(dr.GetOrdinal("VisaStatus")) ? (VisaStatus?)dr.GetByte(dr.GetOrdinal("VisaStatus")) : null;
                    traveller.IsCardTransact = dr["IsCardTransact"].ToString().Equals("1") ? true : false;
                    traveller.Gender = !dr.IsDBNull(dr.GetOrdinal("Gender")) ? (EyouSoft.Model.EnumType.GovStructure.Gender?)dr.GetByte(dr.GetOrdinal("Gender")) : null;
                    traveller.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                    traveller.LNotice = dr["LNotice"].ToString().Equals("1") ? true : false;
                    traveller.RNotice = dr["RNotice"].ToString().Equals("1") ? true : false;
                    traveller.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                    traveller.TravellerStatus = (TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    traveller.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                    traveller.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                    traveller.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                    //traveller.IsInsurance = dr["IsInsurance"].ToString().Equals("1") ? true : false;

                    traveller.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    list.Add(traveller);
                }
            }
            return list;
        }

        #endregion

        #region --2012-08-20 短线添加的方法------------------


        /// <summary>
        /// 获取订单选座的初始化信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<MTourOrderSeatInfo> GetTourOrderSeatInfo(string TourId)
        {
            IList<MTourOrderSeatInfo> list = null;
            StringBuilder query = new StringBuilder();
            query.Append(" select *, ");
            query.Append(" (select * from tbl_SysCarTypeSeat  ");
            query.Append(" where TemplateId=(select TemplateId  ");
            query.Append(" from tbl_ComCarType where CarTypeId=tbl_TourCarType.CarTypeId)for xml raw,root('Root')) as SysCarTypeSeat, ");
            query.Append(" (select * from tbl_TourOrderCarTypeSeat  ");
            query.Append(" where TourCarTypeId=tbl_TourCarType.TourCarTypeId for xml raw,root('Root')) as TourOrderCarTypeSeat ");
            query.Append(" from tbl_TourCarType ");
            query.AppendFormat(" where TourId='{0}' ", TourId);

            DbCommand cmd = _db.GetSqlStringCommand(query.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null)
                {
                    list = new List<MTourOrderSeatInfo>();
                    while (dr.Read())
                    {
                        MTourOrderSeatInfo order = new MTourOrderSeatInfo();
                        order.TourCarTypeId = dr["TourCarTypeId"].ToString();
                        order.CarTypeId = dr["CarTypeId"].ToString();
                        order.CarTypeName = !dr.IsDBNull(dr.GetOrdinal("CarTypeName")) ? dr.GetString(dr.GetOrdinal("CarTypeName")) : null;
                        order.SeatNum = dr.GetInt32(dr.GetOrdinal("SeatNum"));
                        order.Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null;
                        string SysCarTypeSeat = !dr.IsDBNull(dr.GetOrdinal("SysCarTypeSeat")) ? dr.GetString(dr.GetOrdinal("SysCarTypeSeat")) : null;
                        if (!string.IsNullOrEmpty(SysCarTypeSeat))
                        {
                            order.SysCarTypeSeatList = GetSysCarTypeSeatListByXml(SysCarTypeSeat);
                        }

                        string TourOrderCarTypeSeat = !dr.IsDBNull(dr.GetOrdinal("TourOrderCarTypeSeat")) ? dr.GetString(dr.GetOrdinal("TourOrderCarTypeSeat")) : null;
                        if (!string.IsNullOrEmpty(TourOrderCarTypeSeat))
                        {
                            order.TourOrderCarTypeSeatList = GetTourOrderCarTypeSeatList(TourOrderCarTypeSeat);
                        }
                        list.Add(order);
                    }
                }
            }
            return list;
        }

        #region 私有方法  关于xml实体集合的相互转化
        /// <summary>
        /// 将订单的上车地点转换为xml
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string CreateTourOrderCarLocationXml(string orderId, MTourOrderCarLocation model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            query.AppendFormat("<Item TourLocationId=\"{0}\"   OffPrice=\"{1}\"  OnPrice=\"{2}\"  Desc=\"{3}\" OrderId=\"{4}\" Location='{5}' />", model.TourLocationId, model.OffPrice, model.OnPrice, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(model.Desc), orderId, model.Location);
            query.Append("</Root>");
            return query.ToString();
        }

        /// <summary>
        /// 将订单的选座信息改为xml
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourOrderCarTypeSeat(string orderId, IList<MTourOrderCarTypeSeat> list)
        {
            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            foreach (var model in list)
            {
                query.AppendFormat("<Item TourCarTypeId=\"{0}\"   SeatNumber=\"{1}\" OrderId=\"{2}\" />", model.TourCarTypeId, model.SeatNumber, orderId);
            }
            query.Append("</Root>");

            return query.ToString();
        }

        /// <summary>
        /// 根据xml获取车型座次的集合
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> GetSysCarTypeSeatListByXml(string xml)
        {
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list = null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null && root.HasChildNodes)
            {
                list = new List<EyouSoft.Model.SysStructure.MSysCarTypeSeat>();

                foreach (XmlNode node in root.ChildNodes)
                {
                    EyouSoft.Model.SysStructure.MSysCarTypeSeat model = new EyouSoft.Model.SysStructure.MSysCarTypeSeat();
                    model.SeatNumber = Utils.GetInt(node.Attributes["SeatNumber"].Value);
                    model.PointX = Utils.GetInt(node.Attributes["PointX"].Value);
                    model.PoinY = Utils.GetInt(node.Attributes["PoinY"].Value);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据xml获取订单选座的集合
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourOrderCarTypeSeat> GetTourOrderCarTypeSeatList(string xml)
        {
            IList<MTourOrderCarTypeSeat> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null && root.HasChildNodes)
            {
                list = new List<MTourOrderCarTypeSeat>();
                foreach (XmlNode node in root.ChildNodes)
                {
                    MTourOrderCarTypeSeat model = new MTourOrderCarTypeSeat();
                    model.TourCarTypeId = node.Attributes["TourCarTypeId"].Value;
                    model.OrderId = node.Attributes["OrderId"].Value;
                    model.SeatNumber = node.Attributes["SeatNumber"].Value;
                    list.Add(model);
                }


            }

            return list;
        }

        /// <summary>
        /// 根据订单上车地点的xml获取实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private MTourOrderCarLocation GetTourOrderCarLocationList(string xml)
        {
            MTourOrderCarLocation model = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null && root.HasChildNodes)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    model = new MTourOrderCarLocation()
                    {
                        TourLocationId = node.Attributes["TourLocationId"].Value,
                        OrderId = node.Attributes["OrderId"].Value,
                        OnPrice = Utils.GetDecimal(node.Attributes["OnPrice"].Value),
                        OffPrice = Utils.GetDecimal(node.Attributes["OffPrice"].Value),
                        Location = node.Attributes["Location"] != null ? node.Attributes["Location"].Value : null,
                        Desc = node.Attributes["Desc"] != null ? node.Attributes["Desc"].Value : null
                    };

                }
            }
            return model;
        }

        /// <summary>
        /// 根据订单预设车型的xml获取上车地点集合
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MTourCarType> GetTourCarTypeList(string xml)
        {
            IList<MTourCarType> list = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.SelectSingleNode("Root");
            if (root != null && root.HasChildNodes)
            {
                list = new List<MTourCarType>();
                foreach (XmlNode node in root.ChildNodes)
                {
                    MTourCarType model = new MTourCarType();
                    model.CarTypeId = node.Attributes["CarTypeId"].Value;
                    model.CarTypeName = node.Attributes["CarTypeName"] != null ? node.Attributes["CarTypeName"].Value : null;
                    model.Desc = node.Attributes["Desc"] != null ? node.Attributes["Desc"].Value : null;
                    model.SeatNum = Utils.GetInt(node.Attributes["SeatNum"].Value);
                    model.TourCarTypeId = node.Attributes["TourCarTypeId"].Value;
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion

        #endregion

        /// <summary>
        /// 取消确认合同金额，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public int QuXiaoQueRenHeTongJinE(string companyId, string operatorId, string orderId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_QuXiaoQueRenHeTongJinE");

            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, operatorId);
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

    }
}
