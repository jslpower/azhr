using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HTourStructure
{
    using EyouSoft.Model.EnumType.SysStructure;

    public class MTour
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourMode TourMode { get; set; }
        /// <summary>
        /// 团队确认单
        /// </summary>
        public string TourFile { get; set; }
        /// <summary>
        /// 客户团号
        /// </summary>
        public string TourCustomerCode { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 团队天数
        /// </summary>
        public int TourDays { get; set; }
        /// <summary>
        /// 询价单位编号
        /// </summary>
        public string BuyCompanyID { get; set; }
        /// <summary>
        /// 询价单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime RDate { get; set; }
        /// <summary>
        /// 抵达城市
        /// </summary>
        public string ArriveCity { get; set; }
        /// <summary>
        /// 抵达航班/时间
        /// </summary>
        public string ArriveCityFlight { get; set; }
        /// <summary>
        /// 离开城市
        /// </summary>
        public string LeaveCity { get; set; }
        /// <summary>
        /// 离开航班/时间
        /// </summary>
        public string LeaveCityFlight { get; set; }
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
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 领队
        /// </summary>
        public int Leaders { get; set; }
        /// <summary>
        /// 司陪数
        /// </summary>
        public int SiPei { get; set; }
        /// <summary>
        /// 行程亮点
        /// </summary>
        public string JourneySpot { get; set; }
        /// <summary>
        /// 对外报价类型（整团，分项）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourQuoteType OutQuoteType { get; set; }
        /// <summary>
        /// 报价备注
        /// </summary>
        public string QuoteRemark { get; set; }
        /// <summary>
        /// 个性服务要求
        /// </summary>
        public string SpecificRequire { get; set; }
        /// <summary>
        /// 行程备注
        /// </summary>
        public string TravelNote { get; set; }
        /// <summary>
        /// 计划类型（组团团队，组团散拼，地接团队，地接散拼，出境团队，出境散拼，单项服务，供应商）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 计划状态(操作状态)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 团队状态(未确认、已确认、已取消)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus TourSureStatus { get; set; }

        /// <summary>
        /// 销售应收
        /// </summary>
        public decimal SalerIncome { get; set; }

        /// <summary>
        /// 导游应收
        /// </summary>
        public decimal GuideIncome { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }

        /// <summary>
        /// 已收金额【已审核】
        /// </summary>
        public decimal CheckMoney { get; set; }

        /// <summary>
        /// 未收金额=应收金额-已收金额
        /// </summary>
        public decimal WeiShouJinE { get
        {
            return this.ConfirmMoney - this.CheckMoney;
        } }

        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsideInformation { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 计调员（张三，李四）
        /// </summary>
        public string Planers { get; set; }
        /// <summary>
        /// 导游（王二，马五）
        /// </summary>
        public string Guides { get; set; }
        /// <summary>
        /// 计划计调员
        /// </summary>
        public IList<MTourPlaner> TourPlanerList { get; set; }

        /// <summary>
        /// 团队计划用房数
        /// </summary>
        public IList<MTourRoom> TourRoomList { get; set; }

        /// <summary>
        /// 计划地接
        /// </summary>
        public IList<MTourDiJie> TourDiJieList { get; set; }

        /// <summary>
        /// 计划文件
        /// </summary>
        public IList<MTourFile> TourFileList { get; set; }

        /// <summary>
        /// 团队计划行程安排
        /// </summary>
        public IList<MTourPlan> TourPlanList { get; set; }

        /// <summary>
        /// 团队计划风味餐
        /// </summary>
        public IList<MTourFoot> TourFootList { get; set; }

        /// <summary>
        /// 团队计划购物点
        /// </summary>
        public IList<MTourShop> TourShopList { get; set; }

        /// <summary>
        /// 计划自费项目
        /// </summary>
        public IList<MTourSelfPay> TourSelfPayList { get; set; }

        /// <summary>
        /// 计划赠送
        /// </summary>
        public IList<MTourGive> TourGiveList { get; set; }

        /// <summary>
        /// 计划小费
        /// </summary>
        public IList<MTourTip> TourTipList { get; set; }

        /// <summary>
        /// 计划单价明细
        /// </summary>
        public IList<MTourCost> TourCostList { get; set; }

        /// <summary>
        /// 计划价格
        /// </summary>
        public IList<MTourPrice> TourPriceList { get; set; }

        /// <summary>
        /// 安排的计调项
        /// </summary>
        public IList<MTourPlanItem> TourPlanItemList { get; set; }

        /// <summary>
        /// 计调分车、分桌的备注
        /// </summary>
        public TourPlanRemark TourPlanRemark { get; set; }

        /// <summary>
        /// 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        public IList<MTourJourney> TourJourneyList { get; set; }


        /// <summary>
        /// 变更标题
        /// </summary>
        public string TourChangeTitle { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string TourChangeContent { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReson { get; set; }
        /// <summary>
        /// 线路编号集合(,分隔)
        /// </summary>
        public string RouteIds { get; set; }
        /// <summary>
        /// 预控人数
        /// </summary>
        public int PlanPeopleNumber { get; set; }
        /// <summary>
        /// 预留人数
        /// </summary>
        public int LeavePeopleNumber { get; set; }
        /// <summary>
        /// 散拼计划报价标准
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPriceStandard> MTourPriceStandard { get; set; }
        /// <summary>
        /// 收客状态(正常收客，客满，停止收客)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus TourShouKeStatus { get; set; }
        /// <summary>
        /// 是否显示在同行分销
        /// </summary>
        public bool IsShowDistribution { get; set; }
        /// <summary>
        /// 散拼模版团编号
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 子团出团日期（半角逗号隔开）
        /// </summary>
        public string ZiTuanLDates { get; set; }
    }

    #region 计划行程安排表
    /// <summary>
    /// 团队计划行程安排表
    /// </summary>
    public class MTourPlan
    {
        /// <summary>
        /// 行程编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 交通
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject Traffic { get; set; }

        /// <summary>
        /// 交通价格
        /// </summary>
        public decimal TrafficPrice { get; set; }

        /// <summary>
        /// 酒店编号1
        /// </summary>
        public string HotelId1 { get; set; }

        /// <summary>
        /// 酒店名称1
        /// </summary>
        public string HotelName1 { get; set; }

        /// <summary>
        /// 酒店价格1
        /// </summary>
        public decimal HotelPrice1 { get; set; }

        /// <summary>
        /// 是否用餐早
        /// </summary>
        public bool IsBreakfast { get; set; }

        /// <summary>
        /// 用餐早价格(销售价)
        /// </summary>
        public decimal BreakfastPrice { get; set; }


        /// <summary>
        /// 用餐早价格(结算价)
        /// </summary>
        public decimal BreakfastSettlementPrice { get; set; } 

        /// <summary>
        /// 用餐早餐厅编号
        /// </summary>
        public string BreakfastRestaurantId { get; set; }

        /// <summary>
        /// 用餐早菜单编号
        /// </summary>
        public string BreakfastMenuId { get; set; }

        /// <summary>
        /// 用餐早菜单
        /// </summary>
        public string BreakfastMenu { get; set; }


        /// <summary>
        /// 是否用餐中
        /// </summary>
        public bool IsLunch { get; set; }

        /// <summary>
        /// 用餐中价格(销售价)
        /// </summary>
        public decimal LunchPrice { get; set; }

        /// <summary>
        /// 用餐中价格(销售价)
        /// </summary>
        public decimal LunchSettlementPrice { get; set; } 

        /// <summary>
        /// 用餐中餐厅编号
        /// </summary>
        public string LunchRestaurantId { get; set; }

        /// <summary>
        /// 用餐中菜单编号
        /// </summary>
        public string LunchMenuId { get; set; }

        /// <summary>
        /// 用餐中菜单
        /// </summary>
        public string LunchMenu { get; set; }

        /// <summary>
        /// 用餐晚
        /// </summary>
        public bool IsSupper { get; set; }

        /// <summary>
        /// 用餐晚价格(销售价格)
        /// </summary>
        public decimal SupperPrice { get; set; }

        /// <summary>
        /// 用餐晚价格(结算价格)
        /// </summary>
        public decimal SupperSettlementPrice { get; set; } 

        /// <summary>
        /// 用餐晚餐厅编号
        /// </summary>
        public string SupperRestaurantId { get; set; }

        /// <summary>
        /// 用餐晚菜单编号
        /// </summary>
        public string SupperMenuId { get; set; }

        /// <summary>
        /// 用餐晚菜单
        /// </summary>
        public string SupperMenu { get; set; }

        /// <summary>
        /// 第几天
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 行程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 行程图片
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 团队报价行程城市
        /// </summary>
        public IList<MTourPlanCity> TourPlanCityList { get; set; }

        /// <summary>
        /// 报价行程购物点
        /// </summary>
        public IList<MTourShop> TourPlanShopList { get; set; }

        /// <summary>
        /// 报价行程景点
        /// </summary>
        public IList<MTourPlanSpot> TourPlanSpotList { get; set; }

        /// <summary>
        /// 早餐风味餐编号
        /// </summary>
        public string BreakfastId { get; set; }

        /// <summary>
        /// 中餐风味餐编号
        /// </summary>
        public string LunchId { get; set; }

        /// <summary>
        /// 晚餐风味餐编号
        /// </summary>
        public string SupperId { get; set; }

    }
    #endregion

    #region 计划行程城市
    /// <summary>
    /// 计划行程城市
    /// </summary>
    public class MTourPlanCity
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 交通
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject JiaoTong { get; set; }
        /// <summary>
        /// 交通价格
        /// </summary>
        public decimal JiaoTongJiaGe { get; set; }
    }
    #endregion


    #region 计划行程景点
    /// <summary>
    /// 计划行程景点
    /// </summary>
    public class MTourPlanSpot
    {
        /// <summary>
        /// 景点编号
        /// </summary>
        public string SpotId { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string SpotName { get; set; }

        /// <summary>
        /// 景点价格编号
        /// </summary>
        public string PriceId { get; set; }

        /// <summary>
        /// 景点销售价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 景点结算价格
        /// </summary>
        public decimal SettlementPrice { get; set; }
    }
    #endregion

    #region 团队计划用房数
    /// <summary>
    /// 团队计划用房数
    /// </summary>
    public class MTourRoom
    {
        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 房型名称
        /// </summary>
        public string TypeName { get; set; }
    }
    /// <summary>
    /// 团队计划用房数
    /// </summary>
    public class MTourRoomList : MTourRoom
    {
        /// <summary>
        /// 房型列表
        /// </summary>
        public IList<MTourRoom> TypeRoomList { get; set; }
    }
    #endregion

    #region 计划地接
    /// <summary>
    /// 计划地接
    /// </summary>
    public class MTourDiJie
    {

        /// <summary>
        /// 城市
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 地接社名称
        /// </summary>
        public string DiJieName { get; set; }
        /// <summary>
        /// 地接社编号
        /// </summary>
        public string DiJieId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region  计划文件
    /// <summary>
    /// 计划文件
    /// </summary>
    public class MTourFile
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
    #endregion

    /// <summary>
    /// 计划购物点
    /// </summary>
    public class MTourShop {
        /// <summary>
        /// 购物点编号
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 购物点名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 流水
        /// </summary>
        public decimal LiuShui { get; set; }

        /// <summary>
        /// 成人人头费
        /// </summary>
        public decimal PeopleMoney { get; set; }

        /// <summary>
        /// 儿童人头费
        /// </summary>
        public decimal ChildMoney { get; set; }

    }

    #region 计划风味餐
    /// <summary>
    /// 计划风味餐
    /// </summary>
    public class MTourFoot
    {
        /// <summary>
        /// 餐厅编号
        /// </summary>
        public string RestaurantId { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Menu { get; set; }

        /// <summary>
        /// 价格(销售价格)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 结算价格
        /// </summary>
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 对应的早中晚风味餐编号
        /// </summary>
        public string FootId { get; set; }
    }
    #endregion

    #region 计划自费项目
    /// <summary>
    /// 报价自费项目
    /// </summary>
    public class MTourSelfPay
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 景点编号
        /// </summary>
        public string ScenicSpotId { get; set; }

        /// <summary>
        /// 景点价格编号
        /// </summary>
        public string PriceId { get; set; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string ScenicSpotName { get; set; }



        /// <summary>
        /// 对外收费金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 结算价格
        /// </summary>
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// 减少成本金额
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 计划赠送
    /// <summary>
    /// 计划赠送
    /// </summary>
    public class MTourGive
    {
        /// <summary>
        /// 赠送项目编号
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
    #endregion

    #region 计划小费
    /// <summary>
    /// 计划小费
    /// </summary>
    public class MTourTip
    {
        /// <summary>
        /// 小费名称
        /// </summary>
        public string Tip { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion

    #region 计划单价明细
    /// <summary>
    /// 计划价格信息
    /// </summary>
    public class MTourCost
    {

        /// <summary>
        /// 项目类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.Pricetype Pricetype { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 价格单位
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.PriceUnit PriceUnit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 价格类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CostMode CostMode { get; set; }

    }
    #endregion

    #region 计划价格
    /// <summary>
    /// 计划价格
    /// </summary>
    public class MTourPrice
    {

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
        public decimal LeadPrice { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoomPrice { get; set; }

        /// <summary>
        /// 其它价
        /// </summary>
        public decimal OtherPrice { get; set; }

        /// <summary>
        /// 价格类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CostMode CostMode { get; set; }
        /// <summary>
        /// 合计价格
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public int LevelId { get; set; }
    }
    #endregion

    /// <summary>
    /// 计划计调员
    /// </summary>
    public class MTourPlaner
    {
        /// <summary>
        /// 团队计划ID
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

        /// <summary>
        /// 计调部门编号
        /// </summary>
        public int PlanerDeptId { get; set; }
        /// <summary>
        /// 计调是否接收任务
        /// </summary>
        public bool IsJieShou { get; set; }

    }


    /// <summary>
    /// 计划安排的计调项目
    /// </summary>
    public class MTourPlanItem
    {

        /// <summary>
        /// 计调项目类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject PlanType { get; set; }
    }

    /// <summary>
    /// 计调分车、分桌的备注
    /// </summary>
    public class TourPlanRemark
    {
        /// <summary>
        /// 是否分车
        /// </summary>
        public bool IsCar { get; set; }

        /// <summary>
        /// 几车
        /// </summary>
        public int CarNum { get; set; }

        /// <summary>
        /// 车备注
        /// </summary>
        public string CarRemark { get; set; }

        /// <summary>
        /// 是否分桌
        /// </summary>
        public bool IsDesk { get; set; }

        /// <summary>
        /// 几桌
        /// </summary>
        public int DeskNum { get; set; }

        /// <summary>
        /// 桌备注
        /// </summary>
        public string DeskRemark { get; set; }

    }


    /// <summary>
    /// 安排计调
    /// </summary>
    public class MTourToPlaner : TourPlanRemark
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary> 
        public string CompanyId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsideInformation { get; set; }

        /// <summary>
        /// 计划计调
        /// </summary>
        public IList<MTourPlaner> TourPlanerList { get; set; }

        /// <summary>
        /// 计划安排的计调项目
        /// </summary>
        public IList<MTourPlanItem> TourPlanItemList { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }


    }


    /// <summary>
    /// 团队确认
    /// </summary>
    public class MTourSure
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 领队
        /// </summary>
        public int Leaders { get; set; }

        /// <summary>
        /// 销售应收
        /// </summary>
        public decimal SalerIncome { get; set; }

        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsideInformation { get; set; }

        /// <summary>
        /// 团队确认
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus TourSureStatus { get; set; }

    }


    /// <summary>
    /// 计划项目安排落实实体
    /// </summary>
    public class MTourPlanStatus
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 导游
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Guide { get; set; }
        /// <summary>
        /// 酒店
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Hotel { get; set; }
        /// <summary>
        /// 用餐
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Dining { get; set; }
        /// <summary>
        /// 用车
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Car { get; set; }
        /// <summary>
        /// 地接
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState DJ { get; set; }
        /// <summary>
        /// 国内游轮
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState CShip { get; set; }
        /// <summary>
        /// 国外游轮
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState FShip { get; set; }
        /// <summary>
        /// 景点
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Spot { get; set; }
        /// <summary>
        /// 飞机票
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState PlaneTicket { get; set; }
        /// <summary>
        /// 火车票
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState TrainTicket { get; set; }
        /// <summary>
        /// 汽车票
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState CarTicket { get; set; }
        /// <summary>
        /// 购物
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Shopping { get; set; }
        /// <summary>
        /// 领料
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState LL { get; set; }
        /// <summary>
        /// 其它
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState Other { get; set; }
    }


    /// <summary>
    /// 计划列表的实体
    /// </summary>
    public class MTourInfo : MTour
    {
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// 儿童价格
        /// </summary>
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// 领队价格
        /// </summary>
        public decimal LeaderPrice { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoomPrice { get; set; }

        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public MTourPlanStatus TourPlanStatus { get; set; }

        /// <summary>
        /// 客户单位联系人信息
        /// </summary>
        public EyouSoft.Model.CrmStructure.MCrmLinkman CrmLinkman { get; set; }
        /// <summary>
        /// 计调员
        /// </summary>
        public string Planers { get; set; }

        /// <summary>
        /// 是否存在变更
        /// </summary>
        public bool IsChange { get; set; }

        /// <summary>
        /// 变更是否确认
        /// </summary>
        public bool IsSure { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }
    }

    /// <summary>
    /// 计划查询实体
    /// </summary>
    public class MTourSearch
    {
        /// <summary>
        /// 语种
        /// </summary>
        public LngType? LngType { get; set; }
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 询价单位编号
        /// </summary>
        public string BuyCompanyID { get; set; }

        /// <summary>
        /// 询价单位
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 对方业务员
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int? AreaId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 抵达开始时间
        /// </summary>
        public DateTime? BeginLDate { get; set; }

        /// <summary>
        /// 抵达结束时间
        /// </summary>
        public DateTime? EndLDate { get; set; }

        /// <summary>
        /// 离境开始时间
        /// </summary>
        public DateTime? BeginRDate { get; set; }

        /// <summary>
        /// 离境结束时间
        /// </summary>
        public DateTime? EndRDate { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }

        /// <summary>
        /// 团队确认状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus? TourSureStatus { get; set; }

        /// <summary>
        /// 开始有效期
        /// </summary>
        public DateTime? StartEffectTime { get; set; }

        /// <summary>
        /// 结束有效期
        /// </summary>
        public DateTime? EndEffectTime { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int? Days { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int? PeopleNum { get; set; }

        /// <summary>
        /// 计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType? TourType { get; set; }

        /// <summary>
        /// 计调员ID
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 发布日期开始
        /// </summary>
        public DateTime? IssueSTime { get; set; }
        /// <summary>
        /// 发布日期结束
        /// </summary>
        public DateTime? IssueETime { get; set; }
    }

    #region 分销商平台计划搜索实体
    /// <summary>
    /// 分销商平台计划搜索实体
    /// </summary>
    public class MTourSaleSearch
    {
       
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团时间（开始）
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团时间(结束)
        /// </summary>
        public DateTime? LLDate { get; set; }
    }
    #endregion

    /// <summary>
    /// 计划状态变更表
    /// </summary>
    public class MTourStatusChange {

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 计调是否已接收
        /// </summary>
        public bool? IsJieShou { get; set; }
        /// <summary>
        /// 变更备注【计调退回说明等】
        /// </summary>
        public string Remark { get; set; }
    
    }


    #region  行程亮点、行程备注、报价备注编号(用于语言选择)
    /// <summary>
    /// 行程亮点、行程备注、报价备注编号(用于语言选择)
    /// </summary>
    public class MTourJourney 
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// 编号类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.JourneyType JourneyType { get; set; }
    }
    #endregion
}
