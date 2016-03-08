using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//计调实体  HL 32011-9-2
namespace EyouSoft.Model.PlanStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.SourceStructure;
    using EyouSoft.Model.GovStructure;

    #region 计调中心业务实体
    /// <summary>
    /// 计调中心表
    /// </summary>    
    public class MPlanBaseInfo
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 业务员/计划销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 审核人/会计
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 财务/出纳
        /// </summary>
        public string Accountant { get; set; }
        /// <summary>
        /// 供应商编号、导游编号、领料内容编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 计调项目名称，例如酒店名称，地接社名称，导游姓名，车队，出票点，景点公司，游船名称，餐馆名称，购物店，领料内容，供应商
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 酒店/地接/用车/景点等计调项目
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject Type { get; set; }

        /// <summary>
        /// 联系人/领料人/导游
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话/导游电话
        /// </summary>
        public string ContactPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string ContactFax
        {
            get;
            set;
        }


        /// <summary>
        /// 安排数量（人数（成人+儿童+婴儿）/付费房数/用车数/大交通出票数/就餐人数/人头数/领料数/带团天数）
        /// </summary>
        public int Num
        {
            get;
            set;
        }

        /// <summary>
        /// 接待行程/单项业务计调项具体安排
        /// </summary>
        public string ReceiveJourney
        {
            get;
            set;
        }
        /// <summary>
        /// 费用明细/其它安排支出项目/单项业务计调项备注
        /// </summary>
        public string CostDetail
        {
            get;
            set;
        }

        /// <summary>
        /// 安排费用(小计费用)/预算费用
        /// </summary>
        public decimal PlanCost { get; set; }
        /// <summary>
        /// 财务现付/导游现付/导游签单/银行电汇/财务现收/导游现收/返利冲销
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.Payment PaymentType { get; set; }
        /// <summary>
        /// 未落实/已落实
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Status { get; set; }
        /// <summary>
        /// 导游须知/具体安排[单项服务]
        /// </summary>
        public string GuideNotes { get; set; }
        /// <summary>
        /// 其他备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 预控编号
        /// </summary>
        public string SueId { get; set; }
        /// <summary>
        /// 成本确认人ID
        /// </summary>
        public string CostId { get; set; }
        /// <summary>
        /// 成本确认人
        /// </summary>
        public string CostName { get; set; }
        /// <summary>
        /// 成本确认状态
        /// </summary>
        public bool CostStatus { get; set; }
        
        /// <summary>
        /// 成人确认时间
        /// </summary>
        public DateTime? CostTime { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 确认金额/结算金额
        /// </summary>
        public decimal Confirmation { get; set; }
        /// <summary>
        /// 确认备注
        /// </summary>
        public string CostRemarks { get; set; }
        /// <summary>
        /// 是否有返利(true 是,false 否)
        /// </summary>
        public bool IsRebate { get; set; }
        /// <summary>
        /// 添加状态(1,计调安排时添加 2,导游报账时添加3,销售报账时添加4,计调报账时添加)
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus AddStatus { get; set; }

        /// <summary>
        /// 服务标准/返利标准
        /// </summary>
        public string ServiceStandard { get; set; }

        /// <summary>
        /// 游客信息
        /// </summary>
        public string CustomerInfo { get; set; }
        /// <summary>
        /// 景点浏览开始日期/上团时间/入住时间/用车开始日期/登船日期/用餐时间
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 景点浏览开始时间/用车开始时间/登船时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 景点浏览结束日期/下团时间/离店时间/用车结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 景点浏览结束时间/用车开始时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 酒店业务实体
        /// </summary>
        public MPlanHotel PlanHotel { get; set; }

        /// <summary>
        /// 景点业务实体
        /// </summary>
        public MPlanAttractions PlanAttractions { get; set; }

        /// <summary>
        /// 用车业务实体
        /// </summary>
        public MPlanCar PlanCar { get; set; }

        /// <summary>
        /// 游轮业务实体
        /// </summary>
        public MPlanShip PlanShip { get; set; }

        /// <summary>
        /// 用餐价格实体
        /// </summary>
        public IList<EyouSoft.Model.PlanStructure.MPlanDiningPrice> PlanDiningPricelist { get; set; }

        /// <summary>
        /// 大交通时间业务实体
        /// </summary>
        public IList<MPlanLargeTime> PlanLargeTime { get; set; }

        /// <summary>
        /// 导游业务实体
        /// </summary>
        public MPlanGuide PlanGuide { get; set; }
        /// <summary>
        /// 计调增减变更
        /// </summary>
        public IList<MPlanCostChange> PlanCostChange { get; set; }
        /// <summary>
        /// 领料业务实体
        /// </summary>
        public MGovGoodUse PlanGood { get; set; }
        /// <summary>
        /// 数量（DECIMAL）
        /// </summary>
        public decimal DNum { get; set; }
    }
    #endregion

    #region 酒店业务实体
    /// <summary>
    /// 酒店业务实体
    /// </summary>
    public class MPlanHotel
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        //public HotelStar Star { get; set; }
        /// <summary>
        /// 免房数量
        /// </summary>
        public int FreeNumber { get; set; }
        /// <summary>
        /// 是否含早
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal IsMeal { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal MealPrice { get; set; }
        /// <summary>
        /// 用餐人数
        /// </summary>
        public int MealNumber { get; set; }
        /// <summary>
        /// 用餐次数
        /// </summary>
        public int MealFrequency { get; set; }
        /// <summary>
        /// 酒店房屋业务实体集合
        /// </summary>
        public IList<MPlanHotelRoom> PlanHotelRoomList { get; set; }
        /// <summary>
        /// 前台电话
        /// </summary>
        public string QianTaiTelephone { get; set; }
    }
    #endregion

    #region 酒店房屋业务实体
    /// <summary>
    /// 酒店房屋业务实体
    /// </summary>
    public class MPlanHotelRoom
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomId { get; set; }
        /// <summary>
        /// 房型
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 价格类型(1,间/2,床)
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType PriceType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 入住天数
        /// </summary>
        public double Days { get; set; }
        /// <summary>
        /// 入住日期
        /// </summary>
        public DateTime? CheckInDate { get; set; }
        /// <summary>
        /// 退房日期
        /// </summary>
        public DateTime? CheckOutDate { get; set; }
    }
    #endregion

    #region 景点业务实体
    /// <summary>
    /// 景点业务实体
    /// </summary>
    public class MPlanAttractions
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 景点名称编号
        /// </summary>
        public string AttractionsId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Attractions { get; set; }
        /// <summary>
        /// 成人数量
        /// </summary>
        public int AdultNumber { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber { get; set; }
    }
    #endregion

    #region 用车业务实体
    /// <summary>
    /// 用车业务实体
    /// </summary>
    public class MPlanCar
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 跟团/接送
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanCarType VehicleType { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string Models { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 司机
        /// </summary>
        public string Driver { get; set; }
        /// <summary>
        /// 司机电话
        /// </summary>
        public string DriverPhone { get; set; }
        /// <summary>
        /// 车型座位数
        /// </summary>
        public int SeatNumber { get; set; }
    }
    #endregion

    #region 游轮业务实体
    /// <summary>
    /// 游轮业务实体
    /// </summary>
    public class MPlanShip
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 船名编号
        /// </summary>
        public string SubId { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// 涉外游船(船载电话)
        /// </summary>
        public string ShipCalls { get; set; }
        /// <summary>
        /// 航线/线路
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// 登船码头
        /// </summary>
        public string LoadDock { get; set; }
        /// <summary>
        /// 登船号
        /// </summary>
        public string LoadCode { get; set; }
        /// <summary>
        /// 停靠景点/包含景点
        /// </summary>
        public string Sight { get; set; }
        /// <summary>
        /// 游轮自费项目业务实体集合
        /// </summary>
        public IList<MPlanShipOwnCost> PlanShipOwnCostList { get; set; }
        /// <summary>
        /// 游轮价格业务实体集合
        /// </summary>
        public IList<MPlanShipPrice> PlanShipPriceList { get; set; }

    }
    #endregion

    #region 游轮自费/楼层项目管理
    /// <summary>
    /// 游轮自费/楼层项目管理
    /// </summary>
    public class MPlanShipOwnCost
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 自费项目/楼层
        /// </summary>
        public string OwnItem { get; set; }
        /// <summary>
        /// 结算价/单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 是否楼层
        /// </summary>
        public bool IsFloor { get; set; }
    }
    #endregion

    #region 游轮价格业务实体
    /// <summary>
    /// 游轮价格业务实体
    /// </summary>
    public class MPlanShipPrice
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 人群类型(1,欧美/2,东南亚/3,加拿大/4,内宾)
        /// </summary>
        public PlanShipCrowdType CrowdType { get; set; }
        /// <summary>
        /// 涉外游船(房型)标间 = 0,套房=1,总套=2,行政间=3,包舱=4,阳台间=5,一等舱=6，二等舱=7，三等舱=8
        /// </summary>
        public PlanShipRoomType RoomType { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNumber { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 儿童数(不占床)
        /// </summary>
        public int ChildNoOccupancy { get; set; }
        /// <summary>
        /// 儿童价(不占床)
        /// </summary>
        public decimal ChildNoOccupancyPrice { get; set; }
        /// <summary>
        /// 婴儿数
        /// </summary>
        public int BabyNumber { get; set; }
        /// <summary>
        /// 婴儿价
        /// </summary>
        public decimal BabyNumberPrice { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public decimal DNum { get; set; }
    }
    #endregion

    #region 用餐价格实体
    /// <summary>
    /// 用餐价格实体
    /// </summary>
    public class MPlanDiningPrice
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 价格类型（成人/儿童）
        /// </summary>
        public PlanLargeAdultsType Pricetyp { get; set; }
        /// <summary>
        /// 是否含早餐
        /// </summary>
        public bool IsContainB { get; set; }
        /// <summary>
        /// 早餐次数
        /// </summary>
        public int TimeB { get; set; }
        /// <summary>
        /// 早餐人数
        /// </summary>
        public int PeopleB { get; set; }
        /// <summary>
        /// 早餐餐标
        /// </summary>
        public decimal PriceB { get; set; }
        /// <summary>
        /// 是否含中餐
        /// </summary>
        public bool IsContainL { get; set; }
        /// <summary>
        /// 中餐次数
        /// </summary>
        public int TimeL { get; set; }
        /// <summary>
        /// 中餐人数
        /// </summary>
        public int PeopleL { get; set; }
        /// <summary>
        /// 中餐餐标
        /// </summary>
        public decimal PriceL { get; set; }
        /// <summary>
        /// 是否含晚餐
        /// </summary>
        public bool IsContainS { get; set; }
        /// <summary>
        /// 晚餐次数
        /// </summary>
        public int TimeS { get; set; }
        /// <summary>
        /// 晚餐人数
        /// </summary>
        public int PeopleS { get; set; }
        /// <summary>
        /// 晚餐餐标
        /// </summary>
        public decimal PriceS { get; set; }
    }
    #endregion

    #region 大交通时间业务实体
    /// <summary>
    /// 大交通时间业务实体
    /// </summary>
    public class MPlanLargeTime
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime? DepartureTime { get; set; }
        /// <summary>
        /// 时间点
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 航班号/车次
        /// </summary>
        public string Numbers { get; set; }
        /// <summary>
        /// 出发地
        /// </summary>
        public string Departure { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// 坐位标准
        /// </summary>
        public string SeatStandard { get; set; }
        /// <summary>
        /// 付费数量
        /// </summary>
        public int PayNumber { get; set; }
        /// <summary>
        /// 免费数量
        /// </summary>
        public int FreeNumber { get; set;}
        /// <summary>
        /// 票单价
        /// </summary>
        public decimal FarePrice { get; set; }
        /// <summary>
        /// 头等/商务/经济
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType SeatType { get; set; }
        /// <summary>
        /// 成人/儿童/婴儿
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType AdultsType { get; set; }
        /// <summary>
        /// 保险
        /// </summary>
        public decimal Insurance { get; set; }
        /// <summary>
        /// 机建费
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// 附加费
        /// </summary>
        public decimal Surcharge { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public float Discount { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string BeiZhu { get; set; }
    }
    #endregion    

    #region 导游业务实体
    /// <summary>
    /// 导游业务实体
    /// </summary>
    public class MPlanGuide
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender Gender { get; set; }
        /// <summary>
        /// 上团地点
        /// </summary>
        public string OnLocation { get; set; }
        /// <summary>
        /// 下团地点
        /// </summary>
        public string NextLocation { get; set; }
        /// <summary>
        /// 任务类型（全陪，地陪，接团，送团）
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType TaskType { get; set; }
    }
    #endregion

    #region 计调增减变更实体
    /// <summary>
    /// 计调增减变更实体
    /// </summary>
    public class MPlanCostChange
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 变更类型
        /// </summary>
        public PlanChangeChangeClass ChangeType { get; set; }
        /// <summary>
        /// 增减类型(1增,0减)
        /// </summary>
        public bool Type { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNumber { get; set; }
        /// <summary>
        /// 增减费用
        /// </summary>
        public decimal ChangeCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 费用明细
        /// </summary>
        public string FeiYongMingXi { get; set; }
        /// <summary>
        /// 人数（decimal）
        /// </summary>
        public decimal DNum { get; set; }
    }
#endregion

    #region 已安排计调列表实体
    /// <summary>
    /// 已安排计调列表实体
    /// </summary>
    public class MPlan:MPlanBaseInfo
    {
        /// <summary>
        /// 免费房数量/免费数量
        /// </summary>
        public int FreeNumber { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string Models { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// 领料单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNumber { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber { get; set; }
    }
#endregion

    #region 报销报账（导游、销售、计调、财务）
    /// <summary>
    /// 导游现收实体
    /// </summary>
    public class MGuideIncome
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
        /// 客源单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 导游实收
        /// </summary>
        public decimal GuideRealIncome { get; set; }
        /// <summary>
        /// 导游应收/导游现收
        /// </summary>
        public decimal GuideIncome { get; set; }
        /// <summary>
        /// 导游现收备注
        /// </summary>
        public string GuideRemark { get; set; }
        /// <summary>
        /// 人数（成人+儿童+其他）
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 订单金额/合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
    }

    /// <summary>
    /// 报账汇总
    /// </summary>
    public class MBZHZ
    {
        /// <summary>
        /// 导游收入
        /// </summary>
        public decimal GuideIncome { get; set; }
        /// <summary>
        /// 导游借款
        /// </summary>
        public decimal GuideBorrow { get; set; }
        /// <summary>
        /// 导游支出
        /// </summary>
        public decimal GuideOutlay { get; set; }
        /// <summary>
        /// 导游归还/补领
        /// </summary>
        public decimal GuideMoneyRtn { get
        {
            return this.GuideIncome - this.GuideOutlay + this.GuideBorrow;
        } }
        /// <summary>
        /// 导游实领签单数
        /// </summary>
        public int GuideRelSign { get; set; }
        /// <summary>
        /// 导游已使用签单数
        /// </summary>
        public int GuideUsed { get; set; }
        /// <summary>
        /// 导游归还签单数
        /// </summary>
        public int GuideSignRtn { get
        {
            return this.GuideRelSign - this.GuideUsed;
        } }
    }

    /// <summary>
    /// 团队收支表
    /// </summary>
    public class MTourTotalInOut
    {
        /// <summary>
        /// 团队收入
        /// </summary>
        public decimal TourIncome { get; set; }

        /// <summary>
        /// 团队支出
        /// </summary>
        public decimal TourOutlay { get; set; }

        /// <summary>
        /// 团队利润
        /// </summary>
        public decimal TourProfit { get
        {
            return this.TourIncome - this.TourOutlay;
        } }

        /// <summary>
        /// 利润率
        /// </summary>
        public float TourProRate { get; set; }
    }
    #endregion

    #region 计调安排浮动内容信息业务实体
    /// <summary>
    /// 计调安排浮动内容信息业务实体
    /// </summary>
    public class MJiDiaoAnPaiFuDongInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiDiaoAnPaiFuDongInfo() { }
        /// <summary>
        /// 计调类型
        /// </summary>
        public PlanProject Type { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 对应HTML页面查看计调列浮动内容div.attr("data-type")
        /// </summary>
        public int TI
        {
            get
            {
                int _i = 0;

                switch (Type)
                {
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接: _i = 1; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店: _i = 2; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐: _i = 3; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点: _i = 4; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车: _i = 5; break;
                    //case EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮: _i = 6; break;
                    //case EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮: _i = 7; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游: _i = 7; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机: _i = 8; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车: _i = 8; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车: _i = 8; break;
                    case EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物: _i = 9; break;
                    default: break;
                }

                return _i;
            }
        }
    }
    #endregion
}
