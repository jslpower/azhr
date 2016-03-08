using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HTourStructure
{
    using EyouSoft.Model.EnumType.GysStructure;
using EyouSoft.Model.EnumType.TourStructure;

    /// <summary>
    /// 报价基础信息
    /// </summary>
    public class MQuote
    {
        /// <summary>
        /// 报价编号
        /// </summary>
        public string QuoteId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourMode TourMode { get; set; }

        /// <summary>
        /// 报价类型
        /// </summary>
        public TourType TourType { get; set; }

        /// <summary>
        /// 询价单位编号
        /// </summary>
        public string BuyCompanyID { get; set; }

        /// <summary>
        /// 询价单位
        /// </summary>
        public string BuyCompanyName { get; set; }

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
        /// 询价日期
        /// </summary>
        public DateTime BuyTime { get; set; }

        /// <summary>
        /// 询价编号
        /// </summary>
        public string BuyId { get; set; }

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
        /// 天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 开始有效期
        /// </summary>
        public DateTime StartEffectTime { get; set; }

        /// <summary>
        /// 结束有效期
        /// </summary>
        public DateTime EndEffectTime { get; set; }

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
        /// 最大成人数
        /// </summary>
        public int MaxAdults { get; set; }

        /// <summary>
        /// 最小成人数
        /// </summary>
        public int MinAdults { get; set; }

        /// <summary>
        /// 酒店星级要求
        /// </summary>
        public JiuDianXingJi JiuDianXingJi { get; set; }

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
        /// 第几次报价
        /// </summary>
        public int TimeCount { get; set; }

        /// <summary>
        /// 团队报价状态（未成功，已成功，已取消）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteState QuoteStatus { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }

        /// <summary>
        /// 操作员编号/报价员
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作员部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }

        /// <summary>
        /// 是否列表显示（列表只显示已成功报价或最后一次报价）
        /// </summary>
        public bool IsLatest { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 报价文件信息
        /// </summary>
        public IList<MQuoteFile> QuoteFileList { get; set; }

        /// <summary>
        /// 报价行程安排表
        /// </summary>
        public IList<MQuotePlan> QuotePlanList { get; set; }

        /// <summary>
        /// 报价城市地图坐标表
        /// </summary>
        public IList<MQuotePoint> QuotePointList { get; set; }

        /// <summary>
        /// 购物
        /// </summary>
        public IList<MQuoteShop> QuoteShopList { get; set; }

        /// <summary>
        /// 报价风味餐
        /// </summary>
        public IList<MQuoteFoot> QuoteFootList { get; set; }

        /// <summary>
        /// 报价自费项目
        /// </summary>
        public IList<MQuoteSelfPay> QuoteSelfPayList { get; set; }

        /// <summary>
        /// 报价赠送
        /// </summary>
        public IList<MQuoteGive> QuoteGiveList { get; set; }

        /// <summary>
        /// 报价小费
        /// </summary>
        public IList<MQuoteTip> QuoteTipList { get; set; }

        /// <summary>
        /// 价格项目信息
        /// </summary>
        public IList<MQuoteCost> QuoteCostList { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public IList<MQuotePrice> QuotePriceList { get; set; }

        /// <summary>
        /// 报价成功的团队信息
        /// </summary>
        public MQuoteTour QuoteTour { get; set; }

        /// <summary>
        /// 行程亮点、行程备注、报价备注编号(用于语言选择)集合
        /// </summary>
        public IList<MQuoteJourney> QuoteJourneyList { get; set; }


    }

    #region 报价行程安排表
    /// <summary>
    /// 团队报价行程安排表
    /// </summary>
    public class MQuotePlan
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
        /// 酒店编号2
        /// </summary>
        public string HotelId2 { get; set; }

        /// <summary>
        /// 酒店名称2
        /// </summary>
        public string HotelName2 { get; set; }

        /// <summary>
        /// 酒店价格2
        /// </summary>
        public decimal HotelPrice2 { get; set; }

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
        /// 用餐中价格(结算价)
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
        /// 用餐晚价格(销售价)
        /// </summary>
        public decimal SupperPrice { get; set; }

        /// <summary>
        /// 用餐晚价格(销售价)
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
        public IList<MQuotePlanCity> QuotePlanCityList { get; set; }

        /// <summary>
        /// 报价行程购物点
        /// </summary>
        public IList<MQuoteShop> QuotePlanShopList { get; set; }

        /// <summary>
        /// 报价行程景点
        /// </summary>
        public IList<MQuotePlanSpot> QuotePlanSpotList { get; set; }

        /// <summary>
        /// 早餐的风味餐编号
        /// </summary>
        public string BreakfastId { get; set; }

        /// <summary>
        /// 午餐的风味餐编号
        /// </summary>
        public string LunchId { get; set; }

        /// <summary>
        /// 晚餐的风味餐编号
        /// </summary>
        public string SupperId { get; set; }

    }
    #endregion


    #region 报价行程城市
    /// <summary>
    /// 团队报价行程城市
    /// </summary>
    public class MQuotePlanCity
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
        ///// <summary>
        ///// //低于类别(用于区分城市：1还是县区：0)
        ///// </summary>
        //public int AreaType { get; set; }
    }
    #endregion

    #region 行程购物点
    /// <summary>
    /// 行程购物点
    /// </summary>
    public class MQuoteShop
    {

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
    #endregion

    #region 报价行程景点
    /// <summary>
    /// 报价行程景点
    /// </summary>
    public class MQuotePlanSpot
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
        /// 价格编号
        /// </summary>
        public string PriceId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 结算价格
        /// </summary>
        public decimal SettlementPrice { get; set; }
    }
    #endregion

    #region 报价城市地图坐标
    /// <summary>
    /// 报价城市地图坐标
    /// </summary>
    public class MQuotePoint
    {
        /// <summary>
        /// 坐标X
        /// </summary>
        public decimal PointX { get; set; }

        /// <summary>
        /// 坐标Y
        /// </summary>
        public decimal PointY { get; set; }
    }
    #endregion

    #region 报价文件信息
    /// <summary>
    /// 报价文件信息
    /// </summary>
    public class MQuoteFile
    {

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 报价单文件类别
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteFileModel FileModel { get; set; }
    }
    #endregion

    #region 报价风味餐
    /// <summary>
    /// 报价风味餐
    /// </summary>
    public class MQuoteFoot
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
        /// 价格
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
        /// 对应的早、中、晚风味餐编号
        /// </summary>
        public string FootId { get; set; }
    }
    #endregion

    #region 报价自费项目
    /// <summary>
    /// 报价自费项目
    /// </summary>
    public class MQuoteSelfPay
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
        /// 景点名称
        /// </summary>
        public string ScenicSpotName { get; set; }

        /// <summary>
        /// 景点价格编号
        /// </summary>
        public string PriceId { get; set; }

        /// <summary>
        /// 对外收费金额(销售价格)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 对外收费金额(结算价格)
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

    #region 报价赠送
    /// <summary>
    /// 报价赠送
    /// </summary>
    public class MQuoteGive
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

    #region 报价小费
    /// <summary>
    /// 报价小费
    /// </summary>
    public class MQuoteTip
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

    #region 报价单价明细
    /// <summary>
    /// 报价价格信息
    /// </summary>
    public class MQuoteCost
    {
        /// <summary>
        /// 报价编号
        /// </summary>
        public string QuoteId { get; set; }

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

    #region 报价价格
    /// <summary>
    /// 报价价格
    /// </summary>
    public class MQuotePrice
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
        /// 合计金额
        /// </summary>
        public decimal HeJiPrice { get; set; }

        /// <summary>
        /// 价格类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CostMode CostMode { get; set; }
    }
    #endregion

    #region  报价成功的团队信息
    /// <summary>
    /// 报价团队信息
    /// </summary>
    public class MQuoteTour
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
        /// 计调员
        /// </summary>
        public IList<MTourPlaner> PlanerList { get; set; }

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
        public int Leads { get; set; }
        /// <summary>
        /// 司陪数
        /// </summary>
        public int SiPei { get; set; }

        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }


        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }

        /// <summary>
        /// 领队价格
        /// </summary>
        public decimal LeadPrice { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoomPrice { get; set; }

        /// <summary>
        /// 用房数
        /// </summary>
        public IList<MTourRoom> TourRoomList { get; set; }

        /// <summary>
        /// 地接社信息
        /// </summary>
        public IList<MTourDiJie> TourDiJieList { get; set; }

        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsideInformation { get; set; }

        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }





    }
    #endregion


    #region 报价列表查询实体
    /// <summary>
    /// 报价查询实体
    /// </summary>
    public class MQuoteSearch
    {
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int? AreaId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 询价单位编号
        /// </summary>
        public string BuyCompanyID { get; set; }

        /// <summary>
        /// 询价单位
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 询价编号
        /// </summary>
        public string BuyId { get; set; }

        /// <summary>
        /// 询价开始时间
        /// </summary>
        public DateTime? BeginBuyTime { get; set; }

        /// <summary>
        /// 询价结束时间
        /// </summary>
        public DateTime? EndBuyTime { get; set; }

        /// <summary>
        /// 成团开始时间
        /// </summary>
        public DateTime? BeginTourTime { get; set; }

        /// <summary>
        /// 成团结束时间
        /// </summary>
        public DateTime? EndTourTime { get; set; }


        /// <summary>
        /// 最大成人数
        /// </summary>
        public int? MaxAdults { get; set; }

        /// <summary>
        /// 最小成人数
        /// </summary>
        public int? MinAdults { get; set; }


        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 团队报价状态（未成功，已成功，已取消）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteState? QuoteStatus { get; set; }
    }
    #endregion

    #region 报价分页列表信息
    /// <summary>
    /// 报价分页显示的信息
    /// </summary>
    public class MQuoteInfo : MQuote
    {

        /// <summary>
        /// 客户单位联系人信息
        /// </summary>
        public EyouSoft.Model.CrmStructure.MCrmLinkman CrmLinkman { get; set; }
    }
    #endregion

    #region 报价成本比较
    /// <summary>
    /// 报价对比
    /// </summary>
    public class MQuoteCompare
    {
        /// <summary>
        /// 报价编号
        /// </summary>
        public string QuoteId { get; set; }

        /// <summary>
        /// 报价次数
        /// </summary>
        public int TimeCount { get; set; }
    }

    #endregion

    #region 团队报价次数编号
    /// <summary>
    /// 团队报价次数编号
    /// </summary>
    public class MTourQuoteNo
    {
        /// <summary>
        /// 报价编号
        /// </summary>
        public string QuoteId { get; set; }
        /// <summary>
        /// 第几次报价
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 团队报价状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteState QuoteState { get; set; }
    }
    #endregion

    #region  行程亮点、行程备注、报价备注编号(用于语言选择)
    /// <summary>
    /// 行程亮点、行程备注、报价备注编号(用于语言选择)
    /// </summary>
    public class MQuoteJourney
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
