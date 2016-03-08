using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 描述：订单业务层
    /// 修改记录：
    /// 1、2011-09-05 PM 王磊 创建
    /// </summary>
    public class BTourOrder : EyouSoft.BLL.BLLBase
    {
        private readonly EyouSoft.IDAL.TourStructure.ITourOrder dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITourOrder>();


        #region 添加订单
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order">订单、游客、游客保险组合的实体</param>
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
        public int AddTourOrderExpand(MTourOrderExpand order)
        {
            //计划状态封团不允许添加订单 
            //收客客满 
            //计划停收 
            //订单人数超出了剩余人数 
            //合同号未领用
            //添加失败
            //添加失败
            //关于订单的金额

            //order.SumPrice可手动输入
            order.SettlementMoney = order.PeerAdultPrice * order.Adults + order.PeerChildPrice * order.Childs;
            order.ConfirmSettlementMoney = order.SettlementMoney + order.PeerAddCost - order.PeerReduceCost;
            order.ConfirmMoney = order.SumPrice + order.SumPriceAddCost - order.SumPriceReduceCost;
            order.SalerIncome = order.SumPrice - order.GuideIncome;

            int flg = dal.AddTourOrderExpand(order);
            if (flg == 3)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("新增订单,订单编号:{0}", order.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }
        #endregion

        #region 删除订单
        /// <summary>
        /// 根据订单编号删除订单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>1:删除成功 0：删除失败</returns>
        public bool DeleteTourOrderByOrderId(string orderId)
        {
            int flg = dal.DeleteTourOrderByOrderId(orderId);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("删除订单,订单编号:{0}", orderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;

        }
        #endregion

        #region 修改订单_散拼订单
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
        public int UpdateTourOrderExpand(MTourOrderExpand order)
        {
            //order.SumPrice可手动输入
            order.SettlementMoney = order.PeerAdultPrice * order.Adults + order.PeerChildPrice * order.Childs;
            order.ConfirmSettlementMoney = order.SettlementMoney + order.PeerAddCost - order.PeerReduceCost;
            order.ConfirmMoney = order.SumPrice + order.SumPriceAddCost - order.SumPriceReduceCost;
            order.SalerIncome = order.SumPrice - order.GuideIncome;


            int flg = dal.UpdateTourOrderExpand(order);
            if (flg == 3)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改订单,订单编号:{0}", order.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }
        #endregion

        #region 修改订单_团队订单
        /// <summary>
        /// 修改、变更订单信息(用于计划订单)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="list">游客集合</param>
        /// <returns>
        /// 0:修改失败
        /// 1:修改成功
        /// </returns>
        public bool UpdateTourOrderExpand(string orderId, IList<MTourOrderTraveller> list)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            int flg = dal.UpdateTourOrderExpand(orderId, list);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改订单,订单编号:{0}", orderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }
        #endregion

        #region 修改订单状态
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="change">变更修改的实体</param>
        /// <returns>失败 成功</returns>
        public bool UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, MTourOrderChange change)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            if (!CheckTourOrderChange(change))
            {

                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            int flg = dal.UpdateTourOrderExpand(orderId, orderStatus, change);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改订单的状态为：{0},订单编号:{1}", orderStatus, orderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }
        #endregion


        #region 修改订单状态（用于供应商处理订单）
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderStatus">订单状态</param>
        /// <param name="saveSeatDate">继续留位时间</param>
        /// <param name="change">订单变更的实体</param>
        /// <param name="plan">计调信息</param>
        /// <returns>0：失败 1：成功</returns>
        public bool UpdateTourOrderExpand(string orderId, OrderStatus orderStatus, DateTime? saveSeatDate, MTourOrderChange change, EyouSoft.Model.PlanStructure.MPlanBaseInfo plan)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            if (!CheckTourOrderChange(change))
            {

                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            if (orderStatus == OrderStatus.已留位)
            {
                if (!saveSeatDate.HasValue)
                {
                    throw new System.Exception("bll error:留位时间不能为空！");
                }
            }

            if (orderStatus == OrderStatus.已成交)
            {
                if (plan == null || string.IsNullOrEmpty(plan.CompanyId) || string.IsNullOrEmpty(plan.TourId) || string.IsNullOrEmpty(plan.SourceName))
                {
                    throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
                }
            }


            int flg = dal.UpdateTourOrderExpand(orderId, orderStatus, saveSeatDate, change, plan);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("供应商修改订单的状态为：{0},订单编号:{1}", orderStatus, orderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                if (plan != null)
                {
                    SysStructure.BSysLogHandle.Insert(string.Format("新增计调编号：{0}、计调类型：{1}的计调项目。", plan.PlanId, plan.Type));
                }
                return true;
            }
            return false;

        }
        #endregion

        #region 修改订单的确认金额
        /// <summary>
        /// 修改团款结算单（确认确认金额）
        /// </summary>
        /// <param name="orderChange">订单变更</param>
        /// <returns> 0:修改失败 1:修改成功</returns>
        public bool UpdateOrderSettlement(MOrderConfirm orderChange)
        {
            int flg = dal.UpdateOrderSettlement(orderChange);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat(
                    (orderChange.ConfirmMoneyStatus ? "确认" : "保存") + "订单的确认金额,订单编号：{0}，确认金额：{1}",
                    orderChange.OrderId,
                    orderChange.ConfirmMoney);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }

            return false;
        }
        #endregion

        #region 修改订单的结算金额
        /// <summary>
        /// 修改订单的结算单(确认结算金额)
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderChange"></param>
        /// <returns> 1:成功 0:失败</returns>
        public bool UpdateOrderSettlement(MOrderSettlement order, MTourOrderChange orderChange)
        {
            if (!CheckTourOrderChange(orderChange))
            {
                throw new System.Exception("bll error:id为null或string.IsNullOrEmpty(id)==true。");
            }
            /*else if (order.ConfirmSettlementMoney == 0 || order.ConfirmMoney == 0 || string.IsNullOrEmpty(order.SettlementPeopleId))
            {
                throw new System.Exception("bll error:查询Money为0或SettlementPeopleId为null或string.IsNullOrEmpty(SettlementPeopleId)==true。");
            }*/
            else if (string.IsNullOrEmpty(order.SettlementPeopleId))
            {
                throw new System.Exception("bll error:string.IsNullOrEmpty(SettlementPeopleId)==true。");
            }
            else
            {
                if (order.ConfirmMoneyStatus == false)
                {
                    return false;
                }
                else
                {
                    order.Profit = order.ConfirmMoney - order.ConfirmSettlementMoney;

                    int flg = dal.UpdateOrderSettlement(order, orderChange);
                    if (flg == 1)
                    {
                        //添加操作日志
                        StringBuilder str = new StringBuilder();
                        str.AppendFormat("修改订单的结算金额：{0},订单编号:{1}", order.ConfirmSettlementMoney, order.OrderId);
                        EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                        return true;
                    }

                }
            }
            return false;

        }
        #endregion

        #region 获取游客信息（保险）
        /// <summary>
        /// 根据订单编号获取订单、游客、游客保险信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单、游客集合、保险</returns>
        public MTourOrderExpand GetTourOrderExpandByOrderId(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            MTourOrderExpand order = dal.GetTourOrderExpandByOrderId(orderId);
            if (order != null)
            {
                //销售应收始终等于：合同金额-导游实收（添加时，默认等于导游现收）
                order.SalerIncome = order.SumPrice - order.GuideRealIncome;
            }
            return order;
        }
        #endregion


        #region 根据团队编号获取订单集合
        /// <summary>
        /// 根据团队编号获取订单列表（散拼订单查看多个订单、组团查询单个组团订单）
        /// </summary>
        /// <param name="tourId">团队编号</param>
        ///<param name="orderSum">订单统计实体</param>
        /// <returns></returns>
        public IList<MTourOrder> GetTourOrderListById(string tourId, ref MOrderSum orderSum)
        {
            if (string.IsNullOrEmpty(tourId)) return null;

            var items = dal.GetTourOrderListById(tourId, ref orderSum);

            if (items != null && items.Count > 0)
            {
                var bllDengJi = new EyouSoft.BLL.ComStructure.BComLev();
                var bllBiaoZhun = new EyouSoft.BLL.ComStructure.BComStand();

                foreach (var item in items)
                {
                    if (item.LevId > 0)
                        item.KeHuLevName = bllDengJi.GetName(item.LevId, item.CompanyId);
                    if (item.PriceStandId > 0)
                        item.BaoJiaBiaoZhunName = bllBiaoZhun.GetName(item.PriceStandId, item.CompanyId);
                }
            }

            return items;
        }
        #endregion


        #region  获取计划下的有效订单
        /// <summary>
        ///  根据团队编号获取计划下的有效订单 OrderStatus.已成交
        /// </summary>
        /// <param name="orderSum"></param>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MTourOrder> GetTourOrderListById(ref MOrderSum orderSum, string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            IList<MTourOrder> list = this.GetTourOrderListById(tourId, ref orderSum).Where(c => c.OrderStatus == OrderStatus.已成交).ToList();
            if (list != null && list.Count != 0)
            {
                orderSum = new MOrderSum();
                foreach (var order in list)
                {
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
                    orderSum.Profit += order.Profit;

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
            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderListById(pageSize, pageIndex, tourId, ref recordCount, ref orderSum);
        }
        #endregion

        #region 同业分销_订单集合
        /// <summary>
        /// 同业分销_订单中心列表
        /// </summary>
        /// <param name="searchOrderCenter">查询类</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总条数</param>
        /// <returns></returns>
        public IList<MTradeOrder> GetTourOrderList(MSearchOrderCenter searchOrderCenter,
            int pageSize,
            int pageIndex,
            ref int recordCount)
        {
            if (searchOrderCenter == null || string.IsNullOrEmpty(searchOrderCenter.CompanyId))
            {
                throw new System.Exception("bll error:查询实体为null或string.IsNullOrEmpty(查询实体.CompanyId)==true。");
            }
            string LoginUserId = this.LoginUserId;
            bool _isOnlySelf=false;
            int[] deptIds = null; //this.GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.同业分销_订单中心, out _isOnlySelf);

            var items = dal.GetTourOrderList(searchOrderCenter, pageSize, pageIndex, ref recordCount, LoginUserId, deptIds, _isOnlySelf);

            if (items != null && items.Count > 0)
            {
                var bllDengJi = new EyouSoft.BLL.ComStructure.BComLev();
                var bllBiaoZhun = new EyouSoft.BLL.ComStructure.BComStand();

                foreach (var item in items)
                {
                    if (item.KeHuLevId > 0)  item.KeHuLevName = bllDengJi.GetName(item.KeHuLevId, searchOrderCenter.CompanyId);
                    if (item.BaoJiaBiaoZhunId > 0) item.BaoJiaBiaoZhunName = bllBiaoZhun.GetName(item.BaoJiaBiaoZhunId, searchOrderCenter.CompanyId);
                }
            }

            return items;
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
            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderSummaryByTourId(tourId);
        }
        #endregion

        #region 获取计划游客信息 打印游客名单汇总 （同业分销—订单中心）
        /// <summary>
        /// 根据计划编号获取汇总名单打印（同业分销—订单中心）
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public IList<MTourOrderTraveller> GetTourOrderTravellerByTourId(string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderTravellerByTourId(tourId);
        }
        #endregion

        #region 获取团款结算单(销售中心)
        /// <summary>
        /// 根据订单编号获取团款结算单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="tourType">计划类型</param>
        /// <returns></returns>
        public MOrderSale GetSettlementOrderByOrderId(string orderId, TourType tourType)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            var info = dal.GetSettlementOrderByOrderId(orderId, tourType);

            if (info != null)
            {
                if (info.KeHuLevId > 0)
                    info.KeHuLevName = new EyouSoft.BLL.ComStructure.BComLev().GetName(info.KeHuLevId, info.CompanyId);
                if (info.BaoJiaBiaoZhunId > 0)
                    info.BaoJiaBiaoZhunName = new EyouSoft.BLL.ComStructure.BComStand().GetName(info.BaoJiaBiaoZhunId, info.CompanyId);
            }

            return info;
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
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderTravellerByOrderId(orderId);
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
        public IList<MFinancialOrder> GetOrderList(MSearchFinancialOrder search,
                   int pageSize,
                   int pageIndex,
                   ref int recordCount)
        {
            return dal.GetOrderList(search, pageSize, pageIndex, ref recordCount);

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
        public IList<MSupplierOrder> GetOrderList(MSearchSupplierOrder search,
            int pageSize,
            int pageIndex,
            ref int recordCount)
        {
            return dal.GetOrderList(search, pageSize, pageIndex, ref recordCount);
        }
        #endregion


        #region 根据游客编号获取游客保险信息
        /// <summary>
        /// 根据游客编号获取游客的保险
        /// </summary>
        /// <param name="travellerId">游客编号获取游客保险</param>
        /// <returns></returns>
        public IList<MTourOrderTravellerInsurance> GetTravellerInsuranceListByTravellerId(string travellerId)
        {
            if (string.IsNullOrEmpty(travellerId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTravellerInsuranceListByTravellerId(travellerId);
        }
        #endregion

        #region 根据游客编号获取游客信息
        /// <summary>
        /// 根据游客编号获取游客信息
        /// </summary>
        /// <param name="travellerId">游客编号</param>
        /// <returns></returns>
        public MTourOrderTraveller GetTourOrderTravellerById(string travellerId)
        {
            if (string.IsNullOrEmpty(travellerId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderTravellerById(travellerId);
        }


        /// <summary>
        /// 游客退团
        /// </summary>
        /// <param name="traveller">游客实体</param>
        /// <param name="change">订单修改、变更实体</param>
        /// <returns></returns>
        public bool UpdateTourOrderTraveller(MTourOrderTraveller traveller, MTourOrderChange change)
        {
            if (string.IsNullOrEmpty(traveller.TravellerId) || string.IsNullOrEmpty(traveller.OrderId))
            {
                throw new System.Exception("bll error:id为null或string.IsNullOrEmpty(id)==true。");
            }
            if (!CheckTourOrderChange(change))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            return dal.UpdateTourOrderTraveller(traveller, change) == 1 ? true : false;

        }
        #endregion

        #region 销售中心销售收款列表
        /// <summary>
        /// 销售中心销售收款列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">查询条件实体类</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MReceivableInfo> GetXiaoShouShouKuanList(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , EyouSoft.Model.FinStructure.MReceivableBase mSearch)
        {
            EyouSoft.IDAL.FinStructure.IFinance _dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.FinStructure.IFinance>();
            bool _isOnlySelf;

            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId))
            {
                throw new System.Exception("bll error:查询实体为null或string.IsNullOrEmpty(查询实体.CompanyId)==true。");
            }

            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId) || pageSize <= 0 || pageIndex <= 0)
            {
                return null;
            }
            var sum = new object[] { };
            return _dal.GetReceivableInfoLst(pageSize, pageIndex, ref recordCount, ref sum, mSearch, this.LoginUserId, this.GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.销售中心_销售收款, out _isOnlySelf));
        }
        #endregion


        #region 添加收款/退款
        /// <summary>
        /// 添加销售收款/退款
        /// </summary>
        /// <param name="tourOrderSales">订单收款退款的集合</param>
        /// <returns>0:添加失败 1:添加成功</returns>
        public bool AddTourOrderSales(MTourOrderSales tourOrderSales)
        {

            int flg = dal.AddTourOrderSales(tourOrderSales);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("添加订单的{0}。订单编号：{1}，{2}金额:{3}。", tourOrderSales.CollectionRefundState, tourOrderSales.OrderId, tourOrderSales.CollectionRefundState, tourOrderSales.CollectionRefundAmount);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
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
        public bool AddTourOrderSales(IList<MTourOrderSales> tourOrderSalesList)
        {
            int flg = dal.AddTourOrderSales(tourOrderSalesList);
            if (flg == 1)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.Append("批量添加订单的收款。");
                str.Append("订单编号：");
                foreach (MTourOrderSales sale in tourOrderSalesList)
                {
                    str.AppendFormat(" {0} ", sale.OrderId);

                }
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }

            return false;

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
        public bool DeleteTourOrderSales(string orderSalesId)
        {

            if (string.IsNullOrEmpty(orderSalesId))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }

            int flg = dal.DeleteTourOrderSales(orderSalesId);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("删除编号为：{0}的订单收款/退款记录。", orderSalesId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
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
        public bool UpdateTourOrderSales(MTourOrderSales tourOrderSales)
        {
            int flg = dal.UpdateTourOrderSales(tourOrderSales);
            if (flg == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改订单的{0}，订单号：{1},修改后的金额：{2}。", tourOrderSales.CollectionRefundState, tourOrderSales.OrderId, tourOrderSales.CollectionRefundAmount);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;

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
            if (string.IsNullOrEmpty(id))
            {
                throw new System.Exception("bll error:查询id为null或string.IsNullOrEmpty(id)==true。");
            }
            return dal.GetTourOrderSalesById(id);
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
            if (orderIds == null)
            {
                throw new System.Exception("bll error:查询orderIds为null。");
            }
            return dal.GetTourOrderCollectionSalesListByOrderId(orderIds);
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
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询orderIds为null。string.IsNullOrEmpty(id)==true.");
            }
            return dal.GetTourOrderSalesListByOrderId(orderId, collectionRefundState);
        }
        #endregion



        #region 修改导游实收
        /// <summary>
        /// 修改导游实收
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="guideIncome">导游应收</param>
        /// <param name="guideRealIncome">导游实收</param>
        /// <param name="guideRemark">导游实收的备注</param>
        /// <param name="sales">导游收款的记录</param>
        /// <returns>1：成功 0：失败</returns>
        public bool UpdateGuideRealIncome(string orderId, decimal guideIncome, decimal guideRealIncome, string guideRemark, MTourOrderSales sales)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }
            if (dal.UpdateGuideRealIncome(orderId, guideIncome, guideRealIncome, guideRemark, sales) == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("订单编号：{0},导游应收修改为:{1},导游实收:{2},备注：{3}", orderId, guideIncome, guideRealIncome, guideRemark);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }
        #endregion




        #region 根据计划编号获取存在导游收款的订单
        /// <summary>
        /// 根据计划编号获取存在导游收款的订单
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MTourOrder> GetGuideOrderListById(string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }
            return dal.GetGuideOrderListById(tourId);
        }
        #endregion

        #region 判断MTourOrderChange记录的实体 该输入的字段
        /// <summary>
        /// 判断MTourOrderChange记录的实体
        /// </summary>
        /// <param name="change"></param>
        /// <returns></returns>
        private bool CheckTourOrderChange(MTourOrderChange change)
        {
            if (change != null)
            {
                if (string.IsNullOrEmpty(change.CompanyId) || string.IsNullOrEmpty(change.TourId) || string.IsNullOrEmpty(change.OrderId))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion


        #region 打印单相关

        /// <summary>
        /// 打印单游客信息
        /// </summary>
        /// <param name="tourId">团号</param>
        /// <param name="orderStatus">订单状态的数组 null 查询所有</param>
        /// <returns></returns>
        public IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerById(string tourId, params int[] orderStatus)
        {

            if (string.IsNullOrEmpty(tourId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }

            return dal.GetTourOrderBuyCompanyTravellerById(tourId, orderStatus);
        }

        /// <summary>
        /// 根据订单号获取游客打印名单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public IList<BuyCompanyTraveller> GetTourOrderBuyCompanyTravellerByOrderId(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }
            return dal.GetTourOrderBuyCompanyTravellerByOrderId(orderId);
        }

        #endregion


        /// <summary>
        /// 获取订单的金额
        /// </summary>
        /// <param name="orderId"></param>
        public OrderMoney GetOrderMoney(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }
            OrderMoney orderMoney = dal.GetOrderMoney(orderId);
            if (orderMoney == null)
            {

                orderMoney = dal.GetFinOtherInFeeMoney(orderId);
            }

            return orderMoney;
        }




        #region  散拼短线的订单

        /// <summary>
        /// 获取订单选座的初始化信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<MTourOrderSeatInfo> GetTourOrderSeatInfo(string TourId)
        {
            if (string.IsNullOrEmpty(TourId)) return null;
            return dal.GetTourOrderSeatInfo(TourId);

        }

        /// <summary>
        /// 添加短线订单
        /// </summary>
        /// <param name="order">订单、游客、游客保险组合的实体</param>
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
        public int AddTourOrderExpand(MTourOrderExpand order, ref IList<MTourOrderCarTypeSeat> list)
        {

            //order.SumPrice可手动输入
            order.SettlementMoney = order.PeerAdultPrice * order.Adults + order.PeerChildPrice * order.Childs;
            order.ConfirmSettlementMoney = order.SettlementMoney + order.PeerAddCost - order.PeerReduceCost;
            order.ConfirmMoney = order.SumPrice + order.SumPriceAddCost - order.SumPriceReduceCost;
            order.SalerIncome = order.SumPrice - order.GuideIncome;

            int flg = dal.AddTourOrderExpand(order, ref list);
            if (flg == 3)
            {
                //添加操作日志
                StringBuilder str = new StringBuilder();
                str.AppendFormat("新增订单,订单编号:{0}", order.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

        /// <summary>
        /// 修改、变更订单信息(用于散拼订单)
        /// </summary>
        /// <param name="order">订单、游客、游客保险组合的实体</param>
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
        public int UpdateTourOrderExpand(MTourOrderExpand order, ref IList<MTourOrderCarTypeSeat> list)
        {
            order.SettlementMoney = order.PeerAdultPrice * order.Adults + order.PeerChildPrice * order.Childs;
            order.ConfirmSettlementMoney = order.SettlementMoney + order.PeerAddCost - order.PeerReduceCost;
            order.ConfirmMoney = order.SumPrice + order.SumPriceAddCost - order.SumPriceReduceCost;
            order.SalerIncome = order.SumPrice - order.GuideIncome;


            int flg = dal.UpdateTourOrderExpand(order, ref list);
            if (flg == 3)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("修改订单,订单编号:{0}", order.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
            }
            return flg;
        }

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
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(operatorId) 
                || string.IsNullOrEmpty(orderId)) return 0;

            int dalRetCode = dal.QuXiaoQueRenHeTongJinE(companyId, operatorId, orderId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("取消确认合同金额，订单编号：" + orderId + "。");
            }

            return dalRetCode;
        }
    }
}
