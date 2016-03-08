using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//海峡计调实体 2013-5-30
namespace EyouSoft.Model.HPlanStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;

    #region 计调业务实体
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
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
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
        /// 计调类型:酒店/地接/用车/景点等计调项目
        /// </summary>
        public PlanProject Type { get; set; }

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
        /// 联系手机
        /// </summary>
        public string ContactMobile
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
        /// 成人数
        /// </summary>
        public int AdultNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 领队数
        /// </summary>
        public int LeaderNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 接待行程
        /// </summary>
        public string ReceiveJourney
        {
            get;
            set;
        }

        /// <summary>
        /// 安排费用(小计费用)/预算费用
        /// </summary>
        public decimal PlanCost { get; set; }
        /// <summary>
        /// 支付方式:财务现付/导游现付/签单挂账/预付款支付/财务对冲
        /// </summary>
        public Payment PaymentType { get; set; }
        /// <summary>
        /// 签单数
        /// </summary>
        public int SigningCount { get; set; }
        /// <summary>
        /// 未落实/已落实
        /// </summary>
        public PlanState Status { get; set; }
        /// <summary>
        /// 预定方式（代订/自订）
        /// </summary>
        public DueToway DueToway { get; set; }
        /// <summary>
        /// 导游须知/具体安排
        /// </summary>
        public string GuideNotes { get; set; }
        /// <summary>
        /// 其他备注
        /// </summary>
        public string Remarks { get; set; }
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
        public int OperatorDeptId
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
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal Prepaid { get; set; }
        /// <summary>
        /// 未付金额
        /// </summary>
        public decimal Unpaid { get{return this.Confirmation - this.Prepaid;} }
        /// <summary>
        /// 已登代付
        /// </summary>
        public decimal YiDengDaiFu { get; set; }
        /// <summary>
        /// 确认金额/结算金额
        /// </summary>
        public decimal Confirmation { get; set; }
        /// <summary>
        /// 确认备注
        /// </summary>
        public string CostRemarks { get; set; }
        /// <summary>
        /// 添加状态(1,计调安排时添加 2,导游报账时添加3,销售报账时添加4,计调报账时添加,5,预安排添加)
        /// </summary>
        public PlanAddStatus AddStatus { get; set; }

        /// <summary>
        /// 服务标准
        /// </summary>
        public string ServiceStandard { get; set; }

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
        /// 是否预安排(true 是,false 否)
        /// </summary>
        public bool IsDuePlan { get; set; }

        /// <summary>
        /// 酒店业务实体
        /// </summary>
        public MPlanHotel PlanHotel { get; set; }

        /// <summary>
        /// 景点业务实体
        /// </summary>
        public IList<MPlanAttractions> PlanAttractionsList { get; set; }

        /// <summary>
        /// 用车业务实体
        /// </summary>
        public IList<MPlanCar> PlanCarList { get; set; }

        /// <summary>
        /// 用餐价格实体
        /// </summary>
        public IList<MPlanDining> PlanDiningList { get; set; }

        /// <summary>
        /// 区间交通业务实体
        /// </summary>
        public IList<MPlanLargeFrequency> PlanLargeFrequencyList { get; set; }

        /// <summary>
        /// 导游业务实体
        /// </summary>
        public MPlanGuide PlanGuide { get; set; }

        /// <summary>
        /// 领料业务实体
        /// </summary>
        public EyouSoft.Model.GovStructure.MGovGoodUse PlanGood { get; set; }

        /// <summary>
        /// 地接/酒店房屋类型业务实体
        /// </summary>
        public IList<MPlanHotelRoom> PlanHotelRoomList { get; set; }

        /// <summary>
        /// 购物业务实体
        /// </summary>
        public MPlanShop PlanShop { get; set; }

        /// <summary>
        /// 计调增减变更
        /// </summary>
        public IList<MPlanCostChange> PlanCostChange { get; set; }
        /// <summary>
        /// 团态标识：例一女、一两、一九
        /// </summary>
        public string TourMark { get; set; }
        /// <summary>
        /// 销售标识：黄橙绿青蓝紫
        /// </summary>
        public string SaleMark { get; set; }
        /// <summary>
        /// 短信编号
        /// </summary>
        public int IdentityId { get; set; }
        /// <summary>
        /// 计划成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 计划儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 计划领队数
        /// </summary>
        public int Leaders { get; set; }
        /// <summary>
        /// 计划司陪数
        /// </summary>
        public int SiPei { get; set; }
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
        public EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi Star { get; set; }
        /// <summary>
        /// 免房数量
        /// </summary>
        public decimal FreeNumber { get; set; }
        /// <summary>
        /// 是否含早
        /// </summary>
        public PlanHotelIsMeal IsMeal { get; set; }
        /// <summary>
        /// 免房金额
        /// </summary>
        public decimal FreePrice { get; set; }
        /// <summary>
        /// 酒店房屋类型业务实体
        /// </summary>
        public IList<MPlanHotelRoom> PlanHotelRoomList { get; set; }
    }
    #endregion

    #region 地接/酒店房屋类型业务实体
    /// <summary>
    /// 地接/酒店房屋类型业务实体
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
        /// 成人数
        /// </summary>
        public int AdultNumber { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 家庭价
        /// </summary>
        public decimal JTprice { get; set; }
        /// <summary>
        /// 席位
        /// </summary>
        public string Seats { get; set; }
        /// <summary>
        /// 浏览日期
        /// </summary>
        public DateTime? VisitTime { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
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
        /// 用车价格组成（常规线路/特殊线路）
        /// </summary>
        public PlanCarPriceType PriceType { get; set; }
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CarId { get; set; }
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
        /// 车价
        /// </summary>
        public decimal CarPrice { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 过路过桥费
        /// </summary>
        public decimal BridgePrice { get; set; }
        /// <summary>
        /// 司机小费
        /// </summary>
        public decimal DriverPrice { get; set; }
        /// <summary>
        /// 司机房费
        /// </summary>
        public decimal DriverRoomPrice { get; set; }
        /// <summary>
        /// 司机餐费
        /// </summary>
        public decimal DriverDiningPrice { get; set; }
        /// <summary>
        /// 空驶费
        /// </summary>
        public decimal EmptyDrivingPrice { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public decimal OtherPrice { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 用餐价格实体
    /// <summary>
    /// 用餐价格实体
    /// </summary>
    public class MPlanDining
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 用餐价格类型（人/桌）
        /// </summary>
        public PlanDiningPriceType PriceType { get; set; }
        /// <summary>
        /// 用餐类型(早/中/晚)
        /// </summary>
        public PlanDiningType DiningType { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNumber { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildNumber { get; set; }
        /// <summary>
        /// 领队数
        /// </summary>
        public int LeaderNumber { get; set; }
        /// <summary>
        /// 导游数
        /// </summary>
        public int GuideNumber { get; set; }
        /// <summary>
        /// 司机数
        /// </summary>
        public int DriverNumber { get; set; }
        /// <summary>
        /// 成人价/单价
        /// </summary>
        public decimal AdultUnitPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 菜单名称编号
        /// </summary>
        public string MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 桌数
        /// </summary>
        public int TableNumber { get; set; }
        /// <summary>
        /// 减免人数
        /// </summary>
        public int FreeNumber { get; set; }
        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal FreePrice { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal SumPrice { get; set; }
    }
    #endregion

    #region 区间交通业务实体
    /// <summary>
    /// 区间交通业务实体
    /// </summary>
    public class MPlanLargeFrequency
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
        /// 航班号/车次/车牌号
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
        /// 头等/商务/经济
        /// </summary>
        public PlanLargeSeatType SeatType { get; set; }
        /// <summary>
        /// 成人/儿童/婴儿
        /// </summary>
        public PlanLargeAdultsType AdultsType { get; set; }
        /// <summary>
        /// 人数/张数/付费数量
        /// </summary>
        public int PepolePayNum { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal FarePrice { get; set; }
        /// <summary>
        /// 免费数量
        /// </summary>
        public int FreeNumber { get; set; }
        /// <summary>
        /// 保险/手续费
        /// </summary>
        public decimal InsuranceHandlFee { get; set; }
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
        /// 导游性别
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
        public PlanGuideTaskType TaskType { get; set; }
    }
    #endregion

    #region 购物业务实体
    /// <summary>
    /// 购物业务实体
    /// </summary>
    public class MPlanShop
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 流水百分比
        /// </summary>
        public float LiuShui { get; set; }
        /// <summary>
        /// 保底金额
        /// </summary>
        public decimal BaoDi { get; set; }
        /// <summary>
        /// 营业额
        /// </summary>
        public decimal YingYe { get; set; }
        /// <summary>
        /// 成人人头费
        /// </summary>
        public decimal PeopleMoney { get; set; }
        /// <summary>
        /// 儿童人头费
        /// </summary>
        public decimal ChildMoney { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adult { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Child { get; set; }
        /// <summary>
        /// 交给公司：人头费总计
        /// 成人人头费*成人数+儿童人头费*儿童数
        /// </summary>
        public decimal ToCompanyRenTou { get; set; }
        /// <summary>
        /// 交给公司：保底金额
        /// 默认等于BaoDi
        /// </summary>
        public decimal ToCompanyBaoDi { get; set; }
        /// <summary>
        /// 交给公司：人数
        /// 默认等于成人数+儿童数
        /// </summary>
        public int ToCompanyRenShu { get; set; }
        /// <summary>
        /// 交给公司：保底2 手填
        /// </summary>
        public decimal ToCompanyBaoDi2 { get; set; }
        /// <summary>
        /// 交给公司：人数2
        /// 默认等于成人数+儿童数
        /// </summary>
        public int ToCompanyRenShu2 { get; set; }
        /// <summary>
        /// 交给公司：购物返点总额
        /// 默认=购物产品表产品数量*购物产品表返点金额
        /// </summary>
        public decimal ToCompanyFanDian { get; set; }
        /// <summary>
        /// 交给公司：额外流水营业额
        /// 默认=YingYe
        /// </summary>
        public decimal ToCompanyYingYe { get; set; }
        /// <summary>
        /// 交给公司：额外流水提取比例 手填
        /// </summary>
        public float ToCompanyTiQu { get; set; }
        /// <summary>
        /// 交给公司：合计金额
        /// 交给公司的合计金额写入跟团相关的杂费收入表
        /// =ToCompanyRenTou+ToCompanyBaoDi*ToCompanyRenShu+ToCompanyBaoDi2*ToCompanyRenShu2+ToCompanyFanDian+ToCompanyYingYe*ToCompanyTiQu/100
        /// </summary>
        public decimal ToCompanyTotal { get; set; }
        /// <summary>
        /// 交给导游：营业额 =YingYe
        /// </summary>
        public decimal ToGuideYingYe { get; set; }
        /// <summary>
        /// 交给导游：提取比例 手填
        /// </summary>
        public float ToGuideTiQu { get; set; }
        /// <summary>
        /// 交给导游：路桥费 手填
        /// </summary>
        public decimal ToGuideLu { get; set; }
        /// <summary>
        /// 交给导游：水费 手填
        /// </summary>
        public decimal ToGuideShui { get; set; }
        /// <summary>
        /// 交给导游：陪同床费 手填
        /// </summary>
        public decimal ToGuidePei { get; set; }
        /// <summary>
        /// 交给导游：交通 手填
        /// </summary>
        public decimal ToGuideJiao { get; set; }
        /// <summary>
        /// 交给导游：其他 手填
        /// </summary>
        public decimal ToGuideOther { get; set; }
        /// <summary>
        /// 交给导游：额外流水 手填
        /// </summary>
        public decimal ToGuideLiuShui { get; set; }
        /// <summary>
        /// 交给导游：合计金额
        /// =ToGuideYingYe*ToGuideTiQu/100-ToGuideLu-ToGuideShui-ToGuidePei-ToGuideJiao-ToGuideOther-ToGuideLiuShui
        /// </summary>
        public decimal ToGuideTotal { get; set; }
        /// <summary>
        /// 交给领队：营业额=YingYe
        /// </summary>
        public decimal ToLeaderYingYe { get; set; }
        /// <summary>
        /// 交给领队：提取比例 手填
        /// </summary>
        public float ToLeaderTiQu { get; set; }
        /// <summary>
        /// 交给领队：合计金额
        /// =ToLeaderYingYe*ToLeaderTiQu/100
        /// </summary>
        public decimal ToLeaderTotal { get; set; }
    }
    #endregion

    #region 购物产品业务实体
    /// <summary>
    /// 购物产品业务实体
    /// </summary>
    public class MPlanShopProduct
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyAmount { get; set; }
        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal BackMoney { get; set; }
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
        /// 人数（decimal）
        /// </summary>
        public decimal DNum { get; set; }
    }
    #endregion

    #region 已安排计调列表实体
    /// <summary>
    /// 已安排计调列表实体
    /// </summary>
    public class MPlan : MPlanBaseInfo
    {
        /// <summary>
        /// 免费房数量/免费数量
        /// </summary>
        public decimal FreeNumber { get; set; }
        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal FreePrice { get; set; }
        /// <summary>
        /// 人数/张数/付费数量
        /// </summary>
        public int PepolePayNum { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string Models { get; set; }
        /// <summary>
        /// 领料单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 价格组成
        /// </summary>
        public int PriceType { get; set; }
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
        public decimal GuideMoneyRtn
        {
            get
            {
                return this.GuideIncome - this.GuideOutlay + this.GuideBorrow;
            }
        }
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
        public int GuideSignRtn
        {
            get
            {
                return this.GuideRelSign - this.GuideUsed;
            }
        }
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
        public decimal TourProfit
        {
            get
            {
                return this.TourIncome - this.TourOutlay;
            }
        }

        /// <summary>
        /// 利润率
        /// </summary>
        public float TourProRate { get; set; }
    }

    /// <summary>
    /// 导游报账购物收入列表实体
    /// </summary>
    public class MGouWuShouRuBase
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 购物店编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 购物店
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string ContactFax { get; set; }
        /// <summary>
        /// 进店日期
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 进店成人数
        /// </summary>
        public int Adult { get; set; }
        /// <summary>
        /// 进店儿童数
        /// </summary>
        public int Child { get; set; }
        /// <summary>
        /// 营业额
        /// </summary>
        public decimal YingYe { get; set; }
        /// <summary>
        /// 交给公司：合计金额
        /// </summary>
        public decimal ToCompanyTotal { get; set; }
        /// <summary>
        /// 交给导游：合计金额
        /// </summary>
        public decimal ToGuideTotal { get; set; }
        /// <summary>
        /// 交给领队：合计金额
        /// </summary>
        public decimal ToLeaderTotal { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 购物收入实体
    /// </summary>
    public class MGouWuShouRu:MGouWuShouRuBase
    {
        /// <summary>
        /// 导游须知
        /// </summary>
        public string GuideNotes { get; set; }
        /// <summary>
        /// 国籍/地区
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 流水
        /// </summary>
        public decimal LiuShui { get; set; }
        /// <summary>
        /// 保底金额
        /// </summary>
        public decimal BaoDi { get; set; }
        /// <summary>
        /// 成人人头费
        /// </summary>
        public decimal PeopleMoney { get; set; }
        /// <summary>
        /// 儿童人头费
        /// </summary>
        public decimal ChildMoney { get; set; }
        /// <summary>
        /// 购买产品列表
        /// </summary>
        public IList<MGouMaiChanPin> GouMaiChanPin { get; set; }
        /// <summary>
        /// 交给公司：人头费总计
        /// </summary>
        public decimal ToCompanyRenTou { get; set; }
        /// <summary>
        /// 交给公司：保底金额
        /// </summary>
        public decimal ToCompanyBaoDi { get; set; }
        /// <summary>
        /// 交给公司：人数
        /// </summary>
        public int ToCompanyRenShu { get; set; }
        /// <summary>
        /// 交给公司：保底2
        /// </summary>
        public decimal ToCompanyBaoDi2 { get; set; }
        /// <summary>
        /// 交给公司：人数2
        /// </summary>
        public int ToCompanyRenShu2 { get; set; }
        /// <summary>
        /// 交给公司：购物返点总额
        /// </summary>
        public decimal ToCompanyFanDian { get; set; }
        /// <summary>
        /// 交给公司：额外流水营业额
        /// </summary>
        public decimal ToCompanyYingYe { get; set; }
        /// <summary>
        /// 交给公司：额外流水提取比例
        /// </summary>
        public double ToCompanyTiQu { get; set; }
        /// <summary>
        /// 交给导游：营业额
        /// </summary>
        public decimal ToGuideYingYe { get; set; }
        /// <summary>
        /// 交给导游：提取比例
        /// </summary>
        public double ToGuideTiQu { get; set; }
        /// <summary>
        /// 交给导游：路桥费
        /// </summary>
        public decimal ToGuideLu { get; set; }
        /// <summary>
        /// 交给导游：水费
        /// </summary>
        public decimal ToGuideShui { get; set; }
        /// <summary>
        /// 交给导游：陪同床费
        /// </summary>
        public decimal ToGuidePei { get; set; }
        /// <summary>
        /// 交给导游：交通
        /// </summary>
        public decimal ToGuideJiao { get; set; }
        /// <summary>
        /// 交给导游：其他
        /// </summary>
        public decimal ToGuideOther { get; set; }
        /// <summary>
        /// 交给导游：额外流水
        /// </summary>
        public decimal ToGuideLiuShui { get; set; }
        /// <summary>
        /// 交给领队：营业额
        /// </summary>
        public decimal ToLeaderYingYe { get; set; }
        /// <summary>
        /// 交给领队：提取比例
        /// </summary>
        public double ToLeaderTiQu { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 购买产品实体
    /// </summary>
    public class MGouMaiChanPin
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyAmount { get; set; }
        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal BackMoney { get; set; }
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

    #region 计调中心列表实体类
    /// <summary>
    /// 计调中心列表实体类
    /// </summary>
    public class MPlanList
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 是否已变更
        /// </summary>
        public bool IsChange { get; set; }
        /// <summary>
        /// 变更是否确认
        /// </summary>
        public bool IsSure { get; set; }
        /// <summary>
        /// 计划所属公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 团队天数
        /// </summary>
        public int TourDays { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客户单位信息
        /// </summary>
        public IList<MCompanyInfo> CompanyInfo { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计划计调人员列表
        /// </summary>
        public IList<EyouSoft.Model.HTourStructure.MTourPlaner> TourPlaner { get; set; }
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
        public int Leaders { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 对外报价类型（整团，分项）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourQuoteType OutQuoteType { get; set; }
        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourMode TourMode { get; set; }
        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public EyouSoft.Model.HTourStructure.MTourPlanStatus TourPlanStatus { get; set; }
        /// <summary>
        /// 操作状态(团队状态)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 团队状态(团队确认状态)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus TourSureStatus { get; set; }
    }
    #region 计调中心列表搜索实体
    /// <summary>
    /// 计调中心列表搜索实体
    /// </summary>
    public class MPlanListSearch
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
        /// 出团时间（开始）
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团时间(结束)
        /// </summary>
        public DateTime? LLDate { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 计调员名称
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 客户单位信息
        /// </summary>
        public MCompanyInfo CompanyInfo { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 操作状态(团队状态)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 团队状态(团队确认状态)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus? TourSureStatus { get; set; }
    }
    #endregion
    /// <summary>
    /// 客户单位基本信息
    /// </summary>
    public class MCompanyInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
    }
    /// <summary>
    /// 销售员相关信息
    /// </summary>
    public class MSaleInfo
    {
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员部门编号
        /// </summary>
        public int SellerDeptId { get; set; }
    }
    #endregion

    #region 计调查询统计集合实体
    /// <summary>
    /// 计调查询统计集合实体
    /// </summary>
    public class MPlanTJInfo
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 预定方式（代订/自订）
        /// </summary>
        public DueToway DueToway { get; set; }
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 抵达时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 入住/用餐时间
        /// </summary>
        public DateTime STime { get; set; }
        /// <summary>
        /// 入住/用餐截止时间
        /// </summary>
        public DateTime ETime { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public Payment PaymentType { get; set; }
        /// <summary>
        /// 酒店房屋类型业务实体
        /// </summary>
        public IList<MPlanHotelRoom> PlanHotelRoomList { get; set; }
        /// <summary>
        /// 用餐价格实体
        /// </summary>
        public IList<MPlanDining> PlanDiningList { get; set; }
        /// <summary>
        /// 用车业务实体
        /// </summary>
        public IList<MPlanCar> PlanCarList { get; set; }
        /// <summary>
        /// 景点业务实体
        /// </summary>
        public IList<MPlanAttractions> PlanAttractionsList { get; set; }
        /// <summary>
        /// 导游集合
        /// </summary>
        public IList<MGuidInfo> GuidList {get;set;}
        /// <summary>
        /// 导游须知（备注）
        /// </summary>
        public string GuideNotes { get; set; }
        /// <summary>
        /// 未落实/已落实
        /// </summary>
        public PlanState Status { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 国家省份城市县区
        /// </summary>
        public string CityName { get; set; }
    }

    #region  导游相关信息
    /// <summary>
    /// 导游相关信息
    /// </summary>
    public class MGuidInfo
    {
        /// 导游姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 导游电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 导游手机
        /// </summary>
        public string Mobile { get; set; }
    }
    #endregion

    #region 计调查询统计-查询实体
    /// <summary>
    /// 计调查询统计-查询实体
    /// </summary>
    public class MPlanTJChaXunInfo
    {
        /// <summary>
        /// 预定方式（代订/自订）
        /// </summary>
        public DueToway? DueToway { get; set; }
        /// <summary>
        /// 客户单位信息
        /// </summary>
        public MCompanyInfo CompanyInfo { get; set; }
        /// <summary>
        /// 入住/用餐开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 入住/用餐截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 抵达开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 抵达截止时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int? AreaId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Attractions { get; set; }
        /// <summary>
        /// 计调类型:1酒店/2用车/3景点/6用餐
        /// </summary>
        public PlanProject? Type { get; set; }
        /// <summary>
        /// 用餐时间类型
        /// </summary>
        public PlanDiningType? DiningType { get; set; }
    }
    #endregion

    #endregion

    
}
