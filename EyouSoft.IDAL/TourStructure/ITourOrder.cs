using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 订单相关操作、销售收款\退款
    /// 王磊
    /// 2011-9-5
    /// </summary>
    public interface ITourOrder
    {
        #region 订单
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="tourOrderExpand">订单详细信息组合实体</param>
        /// <returns></returns>
        int AddTourOrderExpand(MTourOrderExpand tourOrderExpand);

        /// <summary>
        /// 根据订单编号删除订单
        /// </summary> 
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        int DeleteTourOrderByOrderId(string orderId);


        /// <summary>
        /// 修改、变更订单信息（用于所有散拼订单）
        /// </summary>
        /// <param name="tourOrderExpand">订单详细信息组合实体</param>
        /// <returns></returns>
        int UpdateTourOrderExpand(MTourOrderExpand tourOrderExpand);

        /// <summary>
        /// 修改、变更订单信息(用于团队订单)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="list">游客集合</param>
        /// <returns></returns>
        int UpdateTourOrderExpand(string orderId, IList<MTourOrderTraveller> list);

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="change">订单变更</param>
        /// <returns>True or False</returns>
        int UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, MTourOrderChange change);



        /// <summary>
        /// 修改订单状态(分销商处理订单 已成交 不受理 写计调信息)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="saveSeatDate">继续留位时间</param>
        /// <param name="change">订单变更的实体</param>
        /// <param name="plan">计调信息</param>
        /// <returns>0：失败 1：成功</returns>
        int UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, DateTime? saveSeatDate, MTourOrderChange change, EyouSoft.Model.PlanStructure.MPlanBaseInfo plan);

        /// <summary>
        /// 修改团款结算单（确认团队金额）
        /// </summary>
        /// <param name="orderSale">确认单</param>
        /// <param name="orderChange">订单变更</param>
        /// <returns></returns>
        int UpdateOrderSettlement(MOrderConfirm orderChange);

        /// <summary>
        /// 修改订单的结算金额（确认结算金额）
        /// </summary>
        /// <param name="orderSettlement"></param>
        /// <param name="orderChange"></param>
        /// <returns>1:成功 0:失败</returns>
        int UpdateOrderSettlement(MOrderSettlement orderSettlement, MTourOrderChange orderChange);

        /// <summary>
        /// 根据订单编号获取订单的详细信息(订单、订单游客、游客保险)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        MTourOrderExpand GetTourOrderExpandByOrderId(string orderId);


        /// <summary>
        /// 根据团队编号获取订单列表（散拼订单查看多个订单、组团查询单个组团订单）
        /// </summary>
        /// <param name="tourId">团队编号</param>
        ///<param name="orderSum">订单统计实体</param>
        /// <returns></returns>
        IList<MTourOrder> GetTourOrderListById(string tourId, ref MOrderSum orderSum);

        /// <summary>
        /// 根据团队编号获取订单集合的分页列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="tourId">团队编号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="orderSum">统计的实体</param>
        /// <returns></returns>
        IList<MTourOrder> GetTourOrderListById(int pageSize, int pageIndex, string tourId, ref int recordCount, ref MOrderSum orderSum);


        /// <summary>
        /// 同业分销_订单中心列表
        /// </summary>
        /// <param name="searchOrderCenter">查询类</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <param name="loginId">当前登陆员</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="isOnlySeft">控制权限</param>
        /// <returns></returns>
        IList<MTradeOrder> GetTourOrderList(MSearchOrderCenter searchOrderCenter,
           int pageSize,
           int pageIndex,
           ref int recordCount, string loginId, int[] deptIds, bool isOnlySeft);

        /// <summary>
        /// 根据计划编号获取订单汇总表（同业分销—订单中心）
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<MTourOrderSummary> GetTourOrderSummaryByTourId(string tourId);

        /// <summary>
        /// 根据计划编号获取游客信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<MTourOrderTraveller> GetTourOrderTravellerByTourId(string tourId);


        /// <summary>
        /// 根据订单编号获取团款结算单（销售中心-销售收款-合同金额状态）
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="tourType">计划类型</param>
        /// <returns></returns>
        MOrderSale GetSettlementOrderByOrderId(string OrderId, TourType tourType);

        /// <summary>
        /// 根据订单编号获取订单游客信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        IList<MTourOrderTraveller> GetTourOrderTravellerByOrderId(string orderId);

        /// <summary>
        /// 分销商平台_我的订单
        /// </summary>
        /// <param name="search">查询实体</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <returns></returns>
        IList<MFinancialOrder> GetOrderList(MSearchFinancialOrder search,
           int pageSize,
           int pageIndex,
           ref int recordCount);


        /// <summary>
        /// 供应商平台_订单中心
        /// </summary>
        /// <param name="search">查询实体</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <returns></returns>
        IList<MSupplierOrder> GetOrderList(MSearchSupplierOrder search,
                    int pageSize,
                    int pageIndex,
                    ref int recordCount);

        #endregion

        #region 游客
        /// <summary>
        /// 根据游客编号获取游客的保险
        /// </summary>
        /// <param name="travellerId">游客编号</param>
        /// <returns></returns>
        IList<MTourOrderTravellerInsurance> GetTravellerInsuranceListByTravellerId(string travellerId);


        /// <summary>
        /// 根据游客编号获取游客信息
        /// </summary>
        /// <param name="travellerId"></param>
        /// <returns></returns>
        MTourOrderTraveller GetTourOrderTravellerById(string travellerId);

        /// <summary>
        /// 游客退团
        /// </summary>
        /// <param name="traveller">游客实体</param>
        /// <param name="change">订单修改、变更实体</param>
        /// <returns></returns>
        int UpdateTourOrderTraveller(MTourOrderTraveller traveller, MTourOrderChange change);

        #endregion


        #region 订单销售收款/退款


        /// <summary>
        /// 添加订单销售收款/退款
        /// </summary>
        /// <param name="tourOrderSales">收款/退款实体</param>
        /// <returns></returns>
        int AddTourOrderSales(MTourOrderSales tourOrderSales);

        /// <summary>
        /// 订单的批量收款
        /// </summary>
        /// <param name="tourOrderSalesList">收款实体集合</param>
        /// <returns></returns>
        int AddTourOrderSales(IList<MTourOrderSales> tourOrderSalesList);

        /// <summary>
        /// 删除订单销售收款/退款
        /// </summary>
        /// <param name="orderSalesId">订单销售收款/退款编号</param>
        /// <returns></returns>
        /// 
        int DeleteTourOrderSales(string orderSalesId);

        /// <summary>
        /// 修改订单销售收款/退款
        /// </summary>
        /// <param name="tourOrderSales"></param>
        /// <returns></returns>
        int UpdateTourOrderSales(MTourOrderSales tourOrderSales);

        /// <summary>
        /// 根据订单销售收款/退款的主键编号获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MTourOrderSales GetTourOrderSalesById(string id);

        /// <summary>
        /// 订单销售收款的信息展示列表(批量收款)
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        IList<MTourOrderCollectionSales> GetTourOrderCollectionSalesListByOrderId(params string[] orderIds);


        /// <summary>
        /// 根据订单编号获取订单销售收款/退款
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="collectionRefundState">收款/退款</param>
        /// <returns></returns>
        IList<MTourOrderSales> GetTourOrderSalesListByOrderId(string orderId, CollectionRefundState collectionRefundState);





        /// <summary>
        /// 导游实收相关操作
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="guideIncome">导游应收</param>
        /// <param name="guideRealIncome">导游实收</param>
        /// <param name="guideRemark">导游实收的备注</param>
        /// <param name="sales">导游收款记录</param>
        /// <returns>1：成功 0：失败</returns>
        int UpdateGuideRealIncome(string orderId, decimal guideIncome, decimal guideRealIncome, string guideRemark, MTourOrderSales sales);

        /// <summary>
        /// 根据获取导游收款的订单
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MTourOrder> GetGuideOrderListById(string tourId);

        #endregion


        #region 打印单

        /// <summary>
        /// 打印单游客信息
        /// </summary>
        /// <param name="tourId">团号</param>
        /// <param name="orderStatus">订单状态的数组 null 查询所有</param>
        /// <returns></returns>
        IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerById(string tourId, params int[] orderStatus);

        /// <summary>
        /// 根据订单号获取游客打印名单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerByOrderId(string orderId);
        #endregion


        #region 获取订单的金额

        /// <summary>
        /// 获取订单的金额
        /// </summary>
        /// <param name="orderId"></param>
        OrderMoney GetOrderMoney(string orderId);

        /// <summary>
        /// 杂费收入的金额、开票金额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrderMoney GetFinOtherInFeeMoney(string id); 


        #endregion


        #region  散拼短线的订单

        /// <summary>
        /// 获取订单选座的初始化信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        IList<MTourOrderSeatInfo> GetTourOrderSeatInfo(string TourId);

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
        int AddTourOrderExpand(MTourOrderExpand tourOrderExpand, ref IList<MTourOrderCarTypeSeat> list);

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
        int UpdateTourOrderExpand(MTourOrderExpand tourOrderExpand, ref IList<MTourOrderCarTypeSeat> list);

        #endregion

        /// <summary>
        /// 取消确认合同金额，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        int QuXiaoQueRenHeTongJinE(string companyId, string operatorId, string orderId);
    }
}
