#region  命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

#region 修改记录
//2011-09-01  王磊 创建
/*
 2012-12-31 陈志仁 修改 添加客源地城市
 */
#endregion

namespace EyouSoft.Model.TourStructure
{

    #region 单项业务,计划订单
    /// <summary>
    /// 单项业务,计划订单
    /// </summary>
    [Serializable]
    public class MTourOrder
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团队名称
        /// </summary>
        public string TourName { get; set; }
        /// <summary>
        /// 客源国家
        /// </summary>
        public int BuyCountryId { get; set; }
        /// <summary>
        /// 客源省份
        /// </summary>
        public int BuyProvincesId { get; set; }
        /// <summary>
        /// 客源城市
        /// </summary>
        public int BuyCityId { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 客源单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string ContactDepartId { get; set; }
        /// <summary>
        /// 客源单位(分销商平台报名录入)
        /// </summary>
        public string DCompanyName { get; set; }
        /// <summary>
        /// 联系人(分销商平台订单报名录入)
        /// </summary>
        public string DContactName { get; set; }
        /// <summary>
        /// 联系人电话(分销商平台订单报名录入)
        /// </summary>
        public string DContactTel { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 下单人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 领队数
        /// </summary>
        public int Others { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 领队价
        /// </summary>
        public decimal OtherPrice { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoomPrice { get; set; }
        /// <summary>
        /// 销售应收
        /// </summary>
        public decimal SalerIncome { get; set; }
        /// <summary>
        /// 导游现收
        /// </summary>
        public decimal GuideIncome { get; set; }
        /// <summary>
        /// 导游实收
        /// </summary>
        public decimal GuideRealIncome { get; set; }
        /// <summary>
        /// 导游收款备注
        /// </summary>
        public string GuideRemark { get; set; }
        /// <summary>
        /// 报价标准编号
        /// </summary>
        public int PriceStandId { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int LevId { get; set; }
        /// <summary>
        /// 结算价等级编号
        /// </summary>
        public int PeerLevId { get; set; }
        /// <summary>
        /// 结算成人价
        /// </summary>
        public decimal PeerAdultPrice { get; set; }
        /// <summary>
        /// 结算儿童价
        /// </summary>
        public decimal PeerChildPrice { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal SettlementMoney { get; set; }
        /// <summary>
        /// 结算费用增加
        /// </summary>
        public decimal PeerAddCost { get; set; }
        /// <summary>
        /// 结算费用减少
        /// </summary>
        public decimal PeerReduceCost { get; set; }
        /// <summary>
        /// 结算增加费用备注
        /// </summary>
        public string PeerAddCostRemark { get; set; }
        /// <summary>
        /// 结算减少费用备注
        /// </summary>
        public string PeerReduceCostRemark { get; set; }
        /// <summary>
        /// 确认结算金额
        /// </summary>
        public decimal ConfirmSettlementMoney { get; set; }
        /// <summary>
        /// 结算人
        /// </summary>
        public string SettlementPeople { get; set; }
        /// <summary>
        /// 结算人编号
        /// </summary>
        public string SettlementPeopleId { get; set; }
        /// <summary>
        /// 销售增加费用
        /// </summary>
        public decimal SaleAddCost { get; set; }
        /// <summary>
        /// 销售减少费用
        /// </summary>
        public decimal SaleReduceCost { get; set; }
        /// <summary>
        /// 销售增加费用备注
        /// </summary>
        public string SaleAddCostRemark { get; set; }
        /// <summary>
        /// 销售减少费用备注
        /// </summary>
        public string SaleReduceCostRemark { get; set; }
        /// <summary>
        /// 其它费用
        /// </summary>
        public decimal OtherCost { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 合计金额增加
        /// </summary>
        public decimal SumPriceAddCost { get; set; }
        /// <summary>
        /// 合计金额减少
        /// </summary>
        public decimal SumPriceReduceCost { get; set; }
        /// <summary>
        /// 合计金额增加的备注
        /// </summary>
        public string SumPriceAddCostRemark { get; set; }
        /// <summary>
        /// 合计金额减少的备注
        /// </summary>
        public string SumPriceReduceCostRemark { get; set; }
        /// <summary>
        /// 确认金额(到财务管理，应收管理里的确认金额，如果确认
        /// 金额状态为已确认，销售收款按这个金额操作，否则按合计费用
        /// 操作)
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 确认金额状态
        /// </summary>
        public bool ConfirmMoneyStatus { get; set; }
        /// <summary>
        /// 确认备注
        /// </summary>
        public string ConfirmRemark { get; set; }
        /// <summary>
        /// 订单金额确认人
        /// </summary>
        public string ConfirmPeople { get; set; }
        /// <summary>
        /// 订单金额确认人编号
        /// </summary>
        public string ConfirmPeopleId { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string OrderRemark { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 订单来源 (组团下单,代客预定)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderType OrderType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 留位时间
        /// </summary>
        public DateTime? SaveSeatDate { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 订单游客附件
        /// </summary>
        public string TravellerFile { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 还款月份
        /// </summary>
        public string PayMentMonth { get; set; }
        /// <summary>
        /// 还款的日期
        /// </summary>
        public string PayMentDay { get; set; }
        /// <summary>
        /// 还款的银行账号编号
        /// </summary>
        public int PayMentAccountId { get; set; }        
        /// <summary>
        /// 已收已审核（财务收款）
        /// </summary>
        public decimal CheckMoney { get; set; }
        /// <summary>
        /// 已退已审核（财务退款）
        /// </summary>
        public decimal ReturnMoney { get; set; }
        /// <summary>
        /// 订单收款（不管审核状态）
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        //--散拼短线订单添加的属性
        /// <summary>
        /// 未分配座位人数
        /// </summary>
        public int UnSeat { get; set; }
        /// <summary>
        /// 客户等级名称
        /// </summary>
        public string KeHuLevName { get; set; }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string BaoJiaBiaoZhunName { get; set; }
        /// <summary>
        /// 内部信息
        /// </summary>
        public string NeiBuXinXi { get; set; }
    }
    #endregion

    #region 同行分销-订单中心-订单汇总(点击人数)
    /// <summary>
    /// 同行分销-订单中心-订单汇总(点击人数)
    /// </summary>
    public class MTourOrderSummary : MTourOrder
    {
        /// <summary>
        /// 销售员联系电话
        /// </summary>
        public string SellerContactTel { get; set; }

        /// <summary>
        /// 销售员联系手机
        /// </summary>
        public string SellerContactMobile { get; set; }

        /// <summary>
        /// 操作员电话
        /// </summary>
        public string OperatorContactTel { get; set; }

        /// <summary>
        /// 操作员手机
        /// </summary>
        public string OperatorContactMobile { get; set; }
        /// <summary>
        /// 订单退款信息
        /// </summary>
        public IList<MTourOrderSales> TourOrderSalesList { get; set; }

        /// <summary>
        /// 订单游客信息
        /// </summary>
        public IList<MTourOrderTraveller> TourOrderTravellerList { get; set; }

    }
    #endregion

    #region 订单选座表
    /// <summary>
    /// 订单选座表
    /// </summary>
    [Serializable]
    public class MTourOrderCarSeat
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 编号订单
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNo { get; set; }
        /// <summary>
        /// 座位号
        /// </summary>
        public int SeatNo { get; set; }

    }
    #endregion

    #region 订单变更表
    /// <summary>
    /// 订单变更表
    /// </summary>
    [Serializable]
    public class MTourOrderChange
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 团队销售员编号
        /// </summary>
        public string TourSaleId { get; set; }
        /// <summary>
        /// 团队销售员
        /// </summary>
        public string TourSale { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 变更人数
        /// </summary>
        public int ChangePerson { get; set; }
        /// <summary>
        /// 变更金额
        /// </summary>
        public decimal ChangePrice { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime? IssueTime { get; set; }

        /// <summary>
        /// 订单销售员编号
        /// </summary>
        public string OrderSaleId { get; set; }
        /// <summary>
        /// 订单销售员
        /// </summary>
        public string OrderSale { get; set; }
        /// <summary>
        /// 变更内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 变更人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 变更人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 订单修改、变更操作时的状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.ChangeType ChangeType { get; set; }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool IsSure { get; set; }
        /// <summary>
        /// 确认人编号
        /// </summary>
        public string SurePersonId { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string SurePerson { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? SureTime { get; set; }


    }
    #endregion

    #region 团款结算单的基础信息
    /// <summary>
    /// 团款结算单的基础信息
    /// </summary>
    public class MOrderSale : MOrderSaleBase
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 余款
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime? LDate { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 联系人传真号-用于打印
        /// </summary>
        public string ContactFax { get; set; }
        /// <summary>
        /// 整团
        /// </summary>
        public string ServiceStandard { get; set; }
        /// <summary>
        /// 团队分项报价的集合
        /// </summary>
        public IList<MTourTeamPrice> TourTeamPriceList { get; set; }
        /// <summary>
        /// 散拼报价标准的集合
        /// </summary>
        public MTourPriceStandard TourPriceStandard { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 其它人数
        /// </summary>
        public int Others { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 增加的费用(销售)
        /// </summary>
        public decimal SaleAddCost { get; set; }
        /// <summary>
        /// 减少的费用(销售)
        /// </summary>
        public decimal SaleReduceCost { get; set; }
        /// <summary>
        /// 增加费用的备注(销售)
        /// </summary>
        public string SaleAddCostRemark { get; set; }
        /// <summary>
        /// 减少费用的备注(销售)
        /// </summary>
        public string SaleReduceCostRemark { get; set; }
        /// <summary>
        /// 退款信息
        /// </summary>
        public IList<MTourOrderSales> TourOrderSalesList { get; set; }
        /// <summary>
        /// 其它费用元/团
        /// </summary>
        public decimal OtherCost { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 服务标准
        /// </summary>
        public string FuWuBiaoZhun { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string DingDanBeiZhu { get; set; }
        /// <summary>
        /// 内部信息
        /// </summary>
        public string NeiBuXinXi { get; set; }
        /// <summary>
        /// 订单确认增减列表
        /// </summary>
        public IList<MOrderSalesConfirm> OrderSalesConfirm { get; set; }
    }
    #endregion

    #region 订单确认实体
    /// <summary>
    /// 订单确认实体
    /// </summary>
    public class MOrderConfirm
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 确认人部门编号
        /// </summary>
        public int ConfirmPeopleDeptId { get; set; }
        /// <summary>
        /// 金额确认人编号
        /// </summary>
        public string ConfirmPeopleId { get; set; }
        /// <summary>
        /// 金额确认人
        /// </summary>
        public string ConfirmPeople { get; set; }
        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 确认金额状态
        /// </summary>
        public bool ConfirmMoneyStatus { get; set; }
        /// <summary>
        /// 确认金额备注
        /// </summary>
        public string ConfirmRemark { get; set; }
        /// <summary>
        /// 增减列表
        /// </summary>
        public IList<MOrderSalesConfirm> ChangeInfo { get; set; }
    }
#endregion

    #region 订单确认增减实体
    /// <summary>
    /// 订单确认增减实体
    /// </summary>
    public class MOrderSalesConfirm
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 增加费用
        /// </summary>
        public decimal AddFee { get; set; }
        /// <summary>
        /// 增加备注
        /// </summary>
        public string AddRemark { get; set; }
        /// <summary>
        /// 减少费用
        /// </summary>
        public decimal RedFee { get; set; }
        /// <summary>
        /// 减少备注
        /// </summary>
        public string RedRemark { get; set; }
    }
    #endregion

    #region 团款确认单
    /// <summary>
    /// 团款确认单
    /// </summary>
    public class MOrderSaleBase
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单合同金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 变更金额增加的费用(合计金额增加)
        /// </summary>
        public decimal SumPriceAddCost { get; set; }
        /// <summary>
        /// 变更金额减少的费用(合计金额减少)
        /// </summary>
        public decimal SumPriceReduceCost { get; set; }
        /// <summary>
        /// 变更金额增加费用的备注
        /// </summary>
        public string SumPriceAddCostRemark { get; set; }
        /// <summary>
        /// 变更金额减少费用的备注
        /// </summary>
        public string SumPriceReduceCostRemark { get; set; }
        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 确认金额状态
        /// </summary>
        public bool ConfirmMoneyStatus { get; set; }
        /// <summary>
        /// 结算人
        /// </summary>
        public string ConfirmPeople { get; set; }
        /// <summary>
        /// 结算人编号
        /// </summary>
        public string ConfirmPeopleId { get; set; }
        /// <summary>
        /// 结算人部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 确认备注
        /// </summary>
        public string ConfirmRemark { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal CheckMoney { get; set; }
        /// <summary>
        /// 还款的月份
        /// </summary>
        public string PayMentMonth { get; set; }
        /// <summary>
        /// 还款的日期
        /// </summary>
        public string PayMentDay { get; set; }
        /// <summary>
        /// 还款的银行账户编号
        /// </summary>
        public int PayMentAccountId { get; set; }
        /// <summary>
        /// 订单销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int KeHuLevId { get; set; }
        /// <summary>
        /// 客户等级名称
        /// </summary>
        public string KeHuLevName { get; set; }
        /// <summary>
        /// 报价标准编号
        /// </summary>
        public int BaoJiaBiaoZhunId { get; set; }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string BaoJiaBiaoZhunName { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
    }
    #endregion

    #region 订单销售收款/退款
    /// <summary>
    /// 订单销售收款/退款
    /// </summary>
    [Serializable]
    public class MTourOrderSales
    {
        EyouSoft.Model.EnumType.FinStructure.ShouKuanType _ShouKuanType = EyouSoft.Model.EnumType.FinStructure.ShouKuanType.用户登记;
        /// <summary>
        /// 主键编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 收款/退款日期
        /// </summary>
        public DateTime? CollectionRefundDate { get; set; }
        /// <summary>
        /// 收款/退款人
        /// </summary>
        public string CollectionRefundOperator { get; set; }
        /// <summary>
        /// 收款/退款人编号
        /// </summary>
        public string CollectionRefundOperatorID { get; set; }
        /// <summary>
        /// 收款/退款金额
        /// </summary>
        public decimal CollectionRefundAmount { get; set; }
        /// <summary>
        /// 收款/退款方式编号
        /// </summary>
        public int CollectionRefundMode { get; set; }
        /// <summary>
        /// 收款/退款方式名称
        /// </summary>
        public string CollectionRefundModeName { get; set; }
        /// <summary>
        /// 收款/退款(0收,1退)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CollectionRefundState CollectionRefundState { get; set; }
        /// <summary>
        /// 审核人部门编号
        /// </summary>
        public int ApproverDeptId { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 登记人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 是否导游实收款
        /// </summary>
        public bool IsGuideRealIncome { get; set; }        
        /// <summary>
        /// 登记类型
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.ShouKuanType ShouKuanType
        {
            get { return _ShouKuanType; }
            set { _ShouKuanType = value; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 是否代收
        /// </summary>
        public bool IsDaiShou { get; set; }
        /// <summary>
        /// 代收人
        /// </summary>
        public string DaiShouRen { get; set; }
    }
    #endregion

    #region 销售批量收款的订单列表
    /// <summary>
    /// 批量收款的订单列表
    /// </summary>
    [Serializable]
    public class MTourOrderCollectionSales
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime? LDate { get; set; }

        /// <summary>
        /// 应收金额(确认金额)
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 未收金额
        /// </summary>
        public decimal NotReceivedMoeny { get; set; }

    }
    #endregion

    #region 订单游客信息
    /// <summary>
    /// 订单游客信息
    /// </summary>
    [Serializable]
    public class MTourOrderTraveller
    {
        /// <summary>
        /// 游客编号
        /// </summary>
        public string TravellerId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 游客中文姓名
        /// </summary>
        public string CnName { get; set; }
        /// <summary>
        /// 游客英文姓名
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 游客类型（成人,儿童，其它）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisitorType? VisitorType { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CardType? CardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 证件有效期
        /// </summary>
        public string CardValidDate { get; set; }
        /// <summary>
        /// 签证状态（办理中，通过，未通过）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.VisaStatus? VisaStatus { get; set; }
        /// <summary>
        /// 证件是否已办理（办理，未办理）
        /// </summary>
        public bool IsCardTransact { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender? Gender { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 出团通知
        /// </summary>
        public bool LNotice { get; set; }
        /// <summary>
        /// 回团通知
        /// </summary>
        public bool RNotice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 游客状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TravellerStatus TravellerStatus { get; set; }
        /// <summary>
        /// 退团金额
        /// </summary>
        public decimal RAmount { get; set; }
        /// <summary>
        /// 退团金额说明
        /// </summary>
        public string RAmountRemark { get; set; }
        /// <summary>
        /// 退团时间
        /// </summary>
        public DateTime? RTime { get; set; }
        /// <summary>
        /// 退团原因
        /// </summary>
        public string RRemark { get; set; }
        /// <summary>
        /// 是否存在保险
        /// </summary>
        public bool IsInsurance { get; set; }

        /// <summary>
        /// 游客保险集合
        /// </summary>
        public IList<MTourOrderTravellerInsurance> OrderTravellerInsuranceList { get; set; }
        /// <summary>
        /// 里程积分
        /// </summary>
        public string LiCheng { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? QianFaDate { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public string QianFaDi { get; set; }
    }
    #endregion

    #region 保险信息(基础表)
    /// <summary>
    /// 保险信息的基础列表
    /// </summary>
    public class MInsurance
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 保险编号
        /// </summary>
        public string InsuranceId { get; set; }

        /// <summary>
        /// 保险名称
        /// </summary>
        public string InsuranceName { get; set; }

        /// <summary>
        /// 保险单价
        /// </summary>
        public decimal UnitPrice { get; set; }

    }
    #endregion

    #region 游客保险信息
    /// <summary>
    /// 游客保险信息
    /// </summary>
    [Serializable]
    public class MTourOrderTravellerInsurance
    {

        /// <summary>
        /// 游客编号
        /// </summary>
        public string TravellerId { get; set; }
        /// <summary>
        /// 保险编号
        /// </summary>
        public string InsuranceId { get; set; }
        /// <summary>
        /// 购买份数
        /// </summary>
        public int BuyNum { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }

    }
    #endregion

    #region 订单详细信息的实体
    /// <summary>
    /// 订单详细信息的组合实体（用于添加订单）
    /// </summary>
    [Serializable]
    public class MTourOrderExpand : MTourOrder
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }

        /// <summary>
        /// 计划销售员
        /// </summary>
        public string TourSellerName { get; set; }

        /// <summary>
        /// 计划销售员编号
        /// </summary>
        public string TourSellerId { get; set; }

        /// <summary>
        /// 计划的计调员
        /// </summary>
        public IList<MTourPlaner> TourPlanerList { get; set; }


        /// <summary>
        /// 订单所有游客信息
        /// </summary>
        public IList<MTourOrderTraveller> MTourOrderTravellerList { get; set; }


        /// <summary>
        /// 垫付实体（用于添加）
        /// </summary>
        public EyouSoft.Model.TourStructure.MAdvanceApp AdvanceApp { get; set; }

        /// <summary>
        /// 订单变更或修改（用于修改）
        /// </summary>
        public MTourOrderChange TourOrderChange { get; set; }


        /// <summary>
        /// 订单上车地点列表
        /// </summary>
        public MTourOrderCarLocation TourOrderCarLocation { get; set; }

        /// <summary>
        /// 订单座次的选用集合
        /// </summary>
        public IList<MTourOrderCarTypeSeat> TourOrderCarTypeSeatList { get; set; }

        /// <summary>
        /// 计划预设车型
        /// </summary>
        public IList<MTourCarType> TourCarTypeList { get; set; }
        /// <summary>
        /// 订单结算时间
        /// </summary>
        public DateTime JieSuanTime { get; set; }
        /// <summary>
        /// 订单结算状态
        /// </summary>
        public bool JieSuanStatus { get; set; }
    }

    #endregion

    #region 同业分销订单中心查询类（查询实体）
    /// <summary>
    /// 同业分销订单中心查询类
    /// </summary>
    public class MSearchOrderCenter
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 下单开始时间
        /// </summary>
        public DateTime? OrderIssueBeginTime { get; set; }
        /// <summary>
        /// 下单结束时间
        /// </summary>
        public DateTime? OrderIssueEndTime { get; set; }
        /// <summary>
        /// 出团开始时间
        /// </summary>
        public DateTime? LeaveBeginTime { get; set; }
        /// <summary>
        /// 出团结束时间
        /// </summary>
        public DateTime? LeaveEndTime { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 查询的枚举
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderTypeBySearch OrderTypeBySearch { get; set; }
        /// <summary>
        /// 下单人编号
        /// </summary>
        public string XiaDanRenId { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string XiaDanRenName { get; set; }
        /// <summary>
        /// 订单状态数组
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus[] OrderStatus { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CrmName { get; set; }
    }

    #endregion

    #region 同业分销订单中心 (订单列表实体)
    /// <summary>
    /// 同业分销订单中心
    /// </summary>
    [Serializable]
    public class MTradeOrder
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 是否存在计划变更
        /// </summary>
        public bool IsTourChange { get; set; }
        /// <summary>
        /// 计划变更是否落实
        /// </summary>
        public bool ChangeState { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 订单的销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 下单人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        ///下单人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 销售价(销售成人价)
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 结算价(结算成人价)
        /// </summary>
        public decimal PeerAdultPrice { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 订单计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int KeHuLevId { get; set; }
        /// <summary>
        /// 报价标准编号
        /// </summary>
        public int BaoJiaBiaoZhunId { get; set; }
        /// <summary>
        /// 客户等级名称
        /// </summary>
        public string KeHuLevName { get; set; }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string BaoJiaBiaoZhunName { get; set; }
        /// <summary>
        /// 是否确认合同金额
        /// </summary>
        public bool IsQueRenHeTongJinE { get; set; }
    }

    #endregion

    #region 订单销售收款（查询实体）
    /// <summary>
    ///订单销售收款查询实体
    /// </summary>
    public class MSearchOrderPay
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 是否是当日收款对账
        /// </summary>
        public bool IsTodaySaleOrder { get; set; }

        ///// <summary>
        ///// 模块类型
        ///// </summary>
        //public EyouSoft.Model.EnumType.TourStructure.ModuleType ModuleType { get; set; }

    }



    #endregion

    #region 分销商、供应商订单
    /// <summary>
    /// 分销商、供应商订单基类
    /// </summary>
    public class PlatformBase
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }


        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }


        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }


        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime IssueTime { get; set; }


        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }


        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }


        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus GroupOrderStatus { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

    }


    #region 分销商平台

    /// <summary>
    /// 分销商订单搜索实体
    /// </summary>
    public class MSearchFinancialOrder
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 客源单位(分销商订单报名时手动录入)
        /// </summary>
        public string DCompanyName { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus? Status { get; set; }
        //  public EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus? Status { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }

    }

    /// <summary>
    /// 分销商平台_订单列表
    /// </summary>
    public class MFinancialOrder : PlatformBase
    {

        /// <summary>
        /// 线路区域
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 客源单位(分销商报名手动录入)
        /// </summary>
        public string DCompanyName { get; set; }
        /// <summary>
        /// 联系人（分销商报名手动录入）
        /// </summary>
        public string DContactName { get; set; }
        /// <summary>
        /// 联系人编号（分销商报名手动录入）
        /// </summary>
        public string DContactTel { get; set; }

        /// <summary>
        /// 销售员联系电话
        /// </summary>
        public string SellerContactTel { get; set; }
        /// <summary>
        /// 销售员联系手机
        /// </summary>
        public string SellerContactMobile { get; set; }
        /// <summary>
        /// 用于浮动的计调员信息
        /// </summary>
        public IList<Planer> PlanerList { get; set; }


    }

    /// <summary>
    /// 浮动的计调信息
    /// </summary>
    public class Planer
    {
        /// <summary>
        ///姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile { get; set; }

    }
    #endregion



    #region 分销商平台_订单中心
    /// <summary>
    /// 分销商平台_订单中心
    /// </summary>
    public class MSupplierOrder : PlatformBase
    {
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime RDate { get; set; }
        /// <summary>
        /// 订单合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
    }

    /// <summary>
    /// 分销商平台订单搜索查询类
    /// </summary>
    public class MSearchSupplierOrder
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 线路区域的编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 下单开始时间
        /// </summary>
        public DateTime? BeginIssueTime { get; set; }
        /// <summary>
        /// 下单结束时间
        /// </summary>
        public DateTime? EndIssueTime { get; set; }
        /// <summary>
        /// 出团开始时间
        /// </summary>
        public DateTime? BeginLDate { get; set; }
        /// <summary>
        /// 出团结束时间
        /// </summary>
        public DateTime? EndLDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.OrderStatus? Status { get; set; }

        //public EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus? Status { get; set; }
    }


    #endregion

    #endregion

    #region 报账_订单确认修改结算金额信息的实体
    /// <summary>
    /// 订单的结算费用信息
    /// </summary>
    public class MOrderSettlement
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单的确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }

        /// <summary>
        /// 订单确认金额状态
        /// </summary>
        public bool ConfirmMoneyStatus { get; set; }

        /// <summary>
        ///订单结算金额
        /// </summary>
        public decimal SettlementMoney { get; set; }

        /// <summary>
        /// 结算人
        /// </summary>
        public string SettlementPeople { get; set; }


        /// <summary>
        /// 结算人编号
        /// </summary>
        public string SettlementPeopleId { get; set; }


        /// <summary>
        /// 结算费用增加的金额
        /// </summary>
        public decimal PeerAddCost { get; set; }

        /// <summary>
        /// 结算费用减少的金额
        /// </summary>
        public decimal PeerReduceCost { get; set; }

        /// <summary>
        /// 结算费用增加的备注
        /// </summary>
        public string PeerAddCostRemark { get; set; }

        /// <summary>
        /// 结算费用减少的备注
        /// </summary>
        public string PeerReduceCostRemark { get; set; }


        /// <summary>
        /// 确认结算金额
        /// </summary>
        public decimal ConfirmSettlementMoney { get; set; }


        /// <summary>
        /// 订单利润(确认金额—合计金额)
        /// </summary>
        public decimal Profit { get; set; }

    }

    #endregion

    #region 订单统计相关
    /// <summary>
    /// 订单统计相关
    /// </summary>
    public class MOrderSum
    {
        /// <summary>
        ///散拼短线_未安排座位的人数
        /// </summary>
        public int NoSeat { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }

        /// <summary>
        /// 其它人数
        /// </summary>
        public int Others { get; set; }


        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 确认金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }

        /// <summary>
        /// 确认结算金额
        /// </summary>
        public decimal ConfirmSettlementMoney { get; set; }

        /// <summary>
        /// 导游现收
        /// </summary>
        public decimal GuideIncome { get; set; }

        /// <summary>
        /// 导游实收
        /// </summary>
        public decimal GuideRealIncome { get; set; }

        /// <summary>
        /// 销售应收
        /// </summary>
        public decimal SalerIncome { get; set; }

        /// <summary>
        /// 已收已审核(财务实收)
        /// </summary>
        public decimal CheckMoney { get; set; }

        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit { get; set; }


    }
    #endregion

    #region 打印单游客信息汇总(客源单位问题)
    /// <summary>
    /// 游客信息汇总(客源单位问题)
    /// </summary>
    [Serializable]
    public class BuyCompanyTraveller : MTourOrderTraveller
    {
        /// <summary>
        /// 游客所在客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }

    }
    #endregion


    #region 订单各种金额信息业务实体
    /// <summary>
    /// 订单各种金额信息业务实体
    /// </summary>
    public class OrderMoney
    {
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 已收已审核金额
        /// </summary>
        public decimal CheckMoney { get; set; }
        /// <summary>
        /// 已退已审核
        /// </summary>
        public decimal ReturnMoney { get; set; }
        /// <summary>
        /// 已收不管审核状态
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        /// <summary>
        /// 已退不管审核状态
        /// </summary>
        public decimal BackMoney { get; set; }
        /// <summary>
        /// 已审开票金额
        /// </summary>
        public decimal BillAmount { get; set; }
        /// <summary>
        /// 开票金额不管审核状态
        /// </summary>
        public decimal BillMoeny { get; set; }
        /// <summary>
        /// 退款未审核
        /// </summary>
        public decimal TuiKuanWeiShen { get { return BackMoney - ReturnMoney; } }
        /// <summary>
        /// 合同金额是否确认
        /// </summary>
        public bool IsConfirm { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region --短线添加的类--------------------

    #region 订单座次表
    /// <summary>
    /// 订单座次表
    /// </summary>
    public class MTourOrderCarTypeSeat
    {
        /// <summary>
        /// 计划车型编号
        /// </summary>
        public string TourCarTypeId { get; set; }

        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNumber { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }


    }
    #endregion


    #region 订单上车地点
    /// <summary>
    /// 订单上车地点
    /// </summary>
    public class MTourOrderCarLocation
    {
        /// <summary>
        /// 计划上车地点编号
        /// </summary>
        public string TourLocationId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 上车地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 送价
        /// </summary>
        public decimal OffPrice { get; set; }

        /// <summary>
        /// 接价
        /// </summary>
        public decimal OnPrice { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
    #endregion



    #region
    /// <summary>
    /// 订单选坐的初始化信息
    /// </summary>
    public class MTourOrderSeatInfo : MTourCarType
    {

        /// <summary>
        /// 初始化车型座次模板
        /// </summary>
        public IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> SysCarTypeSeatList { get; set; }

        /// <summary>
        /// 订单已选座次列表
        /// </summary>
        public IList<MTourOrderCarTypeSeat> TourOrderCarTypeSeatList { get; set; }

    }
    #endregion

    #endregion

}
