using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

//2011-09-01 AM 曹胡生 创建
namespace EyouSoft.Model.TourStructure
{
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.HTourStructure;

    #region 计划基础信息业务实体
    /// <summary>
    /// 计划基础信息业务实体
    /// </summary>
    public class MTourBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTourBaseInfo() { }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 计划所属公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 引用的线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 团队天数
        /// </summary>
        public int TourDays { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime? LDate { get; set; }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime? RDate
        {
            get
            {
                if (LDate.HasValue)
                {
                    if (TourDays > 0)
                    {
                        return LDate.Value.AddDays(TourDays - 1);
                    }
                    else
                    {
                        return LDate;
                    }

                }
                return null;
            }
            set { }
        }
        /// <summary>
        /// 出发交通
        /// </summary>
        public string LTraffic { get; set; }
        /// <summary>
        /// 返程交通
        /// </summary>
        public string RTraffic { get; set; }
        /// <summary>
        /// 集合方式
        /// </summary>
        public string Gather { get; set; }
        /// <summary>
        /// 销售员信息
        /// </summary>
        public MSaleInfo SaleInfo { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 成本核算
        /// </summary>
        public string CostCalculation { get; set; }
        /// <summary>
        /// 是否报销报账完成
        /// </summary>
        public bool IsSubmit { get; set; }
        /// <summary>
        /// 发布人信息
        /// </summary>
        public MOperatorInfo OperatorInfo { get; set; }
        /// <summary>
        /// 计划人数
        /// </summary>
        public int PlanPeopleNumber { get; set; }
        /// <summary>
        /// 计划所有订单留位人数
        /// </summary>
        public int LeavePeopleNumber { get; set; }
        /// <summary>
        /// 计划所有订单实收人数
        /// </summary>
        public int RealPeopleNumber { get; set; }
        /// <summary>
        /// 未处理人数
        /// </summary>
        public int WeiChuLiPersonNum { get; set; }
        /// <summary>
        /// 计划所有订单剩余人数
        /// </summary>
        public int PeopleNumberLast { get { return PlanPeopleNumber - RealPeopleNumber - LeavePeopleNumber; } }
        /// <summary>
        /// 导游
        /// </summary>
        public IList<MGuidInfo> GuideList { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 计划行程集合
        /// </summary>
        public IList<MPlanBaseInfo> TourPlan { get; set; }
        /// <summary>
        /// 计划计调集合
        /// </summary>
        public IList<MTourPlanItem> TourPlanItem { get; set; }
        /// <summary>
        /// 计划服务
        /// </summary>
        public MTourService TourService { get; set; }
        /// <summary>
        /// 计划计调人员列表
        /// </summary>
        public IList<MTourPlaner> TourPlaner { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public EyouSoft.Model.HTourStructure.MTourPlanStatus TourPlanStatus { get; set; }
        /// <summary>
        /// 签证资料文件
        /// </summary>
        public IList<EyouSoft.Model.ComStructure.MComAttach> VisaFileList { get; set; }
        /// <summary>
        /// 行程特色
        /// </summary>
        public string PlanFeature { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 是否已变更
        /// </summary>
        public bool IsChange { get; set; }
        /// <summary>
        /// 变更是否确认
        /// </summary>
        public bool IsSure { get; set; }
        /// <summary>
        /// 变更标题
        /// </summary>
        public string TourChangeTitle { get; set; }
        /// <summary>
        /// 变更内容
        /// </summary>
        public string TourChangeContent { get; set; }
        /// <summary>
        /// 是否有计调支出，控制删除与取消
        /// </summary>
        public bool IsPayMoney { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReson { get; set; }
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
        /// 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        public IList<MTourJourney> TourJourneyList { get; set; }
        /// <summary>
        /// 计划计调
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
        /// 线路编号集合(,分隔)
        /// </summary>
        public string RouteIds { get; set; }
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

    #region 团队计划业务实体
    /// <summary>
    /// 团队计划业务实体
    /// </summary>
    public class MTourTeamInfo : MTourBaseInfo
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
        /// 成人价(供应商计划时，为成人结算价)
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价(供应商计划时，为儿童结算价)
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 其它费用(增加费用)
        /// </summary>
        public decimal OtherCost { get; set; }
        /// <summary>
        /// 客户单位信息
        /// </summary>
        public MCompanyInfo CompanyInfo { get; set; }
        /// <summary>
        /// 客源省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 客源国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 对外报价类型（整团，分项）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourQuoteType OutQuoteType { get; set; }
        /// <summary>
        /// 报价备注
        /// </summary>
        public string QuoteRemark { get; set; }
        /// <summary>
        /// 游客列表
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> Traveller
        {
            set;
            get;
        }
        /// <summary>
        /// 计划分项报价集合,单项业务客人要求集合
        /// </summary>
        public IList<MTourTeamPrice> TourTeamPrice { get; set; }
        /// <summary>
        /// 销售增加费用
        /// </summary>
        public decimal SaleAddCost { get; set; }
        /// <summary>
        /// 销售减少费用
        /// </summary>
        public decimal SaleReduceCost { get; set; }
        /// <summary>
        /// 增加费用备注
        /// </summary>
        public string AddCostRemark { get; set; }
        /// <summary>
        /// 减少费用备注
        /// </summary>
        public string ReduceCostRemark { get; set; }
        /// <summary>
        /// 导游现收
        /// </summary>
        public decimal GuideIncome { get; set; }
        /// <summary>
        /// 销售应收
        /// </summary>
        public decimal SalerIncome { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string OrderRemark { set; get; }
        /// <summary>
        /// 超限审核实体
        /// </summary>
        public MAdvanceApp AdvanceApp { get; set; }
        /// <summary>
        /// 客源单位联系人编号
        /// </summary>
        public string ContactDepartId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string HeTongCode { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
    }
    #endregion

    #region 散拼计划业务实体
    /// <summary>
    /// 散拼计划业务实体
    /// </summary>
    public class MTourSanPinInfo : MTourBaseInfo
    {
        /// <summary>
        /// 是否显示在同行分销
        /// </summary>
        public bool IsShowDistribution { get; set; }
        /// <summary>
        /// 同行分销关键字编号
        /// </summary>
        public int KeyId { get; set; }
        /// <summary>
        /// 同行分销关键字
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 散拼计划是否审核
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 供应商公司名称
        /// </summary>
        public string SourceCompanyName { get; set; }
        /// <summary>
        /// 模板团编号
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 是否最近发团
        /// </summary>
        public bool IsRecentLeave { get; set; }
        /// <summary>
        ///成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        ///门市成人价（分销商平台列表）
        /// </summary>
        public decimal SaleAdultPrice { get; set; }
        /// <summary>
        /// 同行成人价（分销商平台列表）
        /// </summary>
        public decimal PeerAdultPrice { get; set; }
        /// <summary>
        /// 是否供应商发布的计划
        /// </summary>
        public bool IsSupplierPublis { get { return string.IsNullOrEmpty(SourceId) ? false : true; } }
        /// <summary>
        /// 散拼计划子团
        /// </summary>
        public IList<MTourChildrenInfo> TourChildrenInfo { get; set; }
        /// <summary>
        /// 散拼计划报价标准
        /// </summary>
        public IList<MTourPriceStandard> MTourPriceStandard { get; set; }
        /// <summary>
        /// 供应商发布的价格信息
        /// </summary>
        public MSupplierPublishPrice MSupplierPublishPrice { get; set; }
        /// <summary>
        /// 收客状态(正常收客，客满，停止收客)
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus TourShouKeStatus { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 供应商计划在分销商显示的发布人
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.ShowPublisher ShowPublisher { get; set; }
        /// <summary>
        /// 计划订单数
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 停收提前天数
        /// </summary>
        public int StopDays { get; set; }


        /// <summary>
        /// 短线团上车地点集合(用于散拼短线)
        /// </summary>
        public IList<MTourCarLocation> TourCarLocation { get; set; }

        /// <summary>
        /// 短线团预设车型集合（用于散拼短线）
        /// </summary>
        public IList<MTourCarType> TourCarType { get; set; }

        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourMode TourMode { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }

        /// <summary>
        /// 团队确认单
        /// </summary>
        public string TourFile { get; set; }

        /// <summary>
        /// 客户团号
        /// </summary>
        public string TourCustomerCode { get; set; }

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
        /// 领队
        /// </summary>
        public int Leaders { get; set; }
        /// <summary>
        /// 司陪数
        /// </summary>
        public int SiPei { get; set; }
        ///// <summary>
        ///// 行程亮点
        ///// </summary>
        //public string JourneySpot { get; set; }
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

    }
    #endregion

    #region 同行分销列表实体
    /// <summary>
    /// 同行分销列表实体
    /// </summary>
    public class MTourTongHanInfo : MTourSanPinInfo
    {
        /// <summary>
        /// 门市成人价
        /// </summary>
        public decimal MSAdultPrice { get; set; }
        /// <summary>
        /// 门市儿童价
        /// </summary>
        public decimal MSChildPrice { get; set; }
        /// <summary>
        /// 结算成人价
        /// </summary>
        public decimal JSAdultPrice { get; set; }
        /// <summary>
        /// 结算儿童价
        /// </summary>
        public decimal JSChildPrice { get; set; }
    }
    #endregion

    #region 供应商发布的报价
    /// <summary>
    /// 供应商发布的报价
    /// </summary>
    public class MSupplierPublishPrice
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 结算成人价
        /// </summary>
        public decimal SettleAdultPrice { get; set; }
        /// <summary>
        /// 结算儿童价
        /// </summary>
        public decimal SettleChildPrice { get; set; }
    }
    #endregion

    #region 散拼计划子团信息业务实体
    /// <summary>
    /// 散拼计划子团信息业务实体
    /// </summary>
    public class MTourChildrenInfo
    {
        /// <summary>
        /// 子团编号
        /// </summary>
        public string ChildrenId { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime RDate { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 是否最近出团
        /// </summary>
        public bool IsRecentLeave { get; set; }
    }
    #endregion

    #region 单项业务计划实体
    /// <summary>
    /// 单项业务计划实体
    /// </summary>
    public class MTourSingleInfo : MTourBaseInfo
    {
        /// <summary>
        /// 合计支出
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 单项业务供应商安排集合
        /// </summary>
        public IList<MSingleSupplier> SingleSupplier { get; set; }
    }
    #endregion

    #region 无计划散拼计划
    /// <summary>
    /// 无计划散拼计划
    /// </summary>
    //public class MWuTourInfo : MTourBaseInfo
    //{
    //    /// <summary>
    //    /// 订单编号
    //    /// </summary>
    //    public string OrderId { get; set; }
    //    /// <summary>
    //    /// 订单号
    //    /// </summary>
    //    public string OrderCode { get; set; }
    //    /// <summary>
    //    /// 成人数
    //    /// </summary>
    //    public int Adults { get; set; }
    //    /// <summary>
    //    /// 儿童数
    //    /// </summary>
    //    public int Childs { get; set; }
    //    /// <summary>
    //    /// 其它人群数
    //    /// </summary>
    //    public int Others { get; set; }
    //    /// <summary>
    //    /// 成人价(供应商计划时，为成人结算价)
    //    /// </summary>
    //    public decimal AdultPrice { get; set; }
    //    /// <summary>
    //    /// 儿童价(供应商计划时，为儿童结算价)
    //    /// </summary>
    //    public decimal ChildPrice { get; set; }
    //    /// <summary>
    //    /// 其它人群价格
    //    /// </summary>
    //    public decimal OtherPrice { get; set; }
    //    /// <summary>
    //    /// 游客列表
    //    /// </summary>
    //    public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> Traveller
    //    {
    //        set;
    //        get;
    //    }
    //    /// <summary>
    //    /// 供应商安排
    //    /// </summary>
    //    public IList<EyouSoft.Model.TourStructure.MSingleSupplier> SingleSupplier { get; set; }
    //}
    #endregion

    #region 组团,地接，出境计调列表实体类
    /// <summary>
    /// 组团,地接，出境计调列表实体类
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
        /// 销售员信息
        /// </summary>
        public MSaleInfo SaleInfo { get; set; }
        /// <summary>
        /// 计划计调人员列表
        /// </summary>
        public IList<MTourPlaner> TourPlaner { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 订单数
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 对外报价类型（整团，分项）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourQuoteType OutQuoteType { get; set; }
        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public MTourPlanStatus TourPlanStatus { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
    }
    #endregion

    #region 组团,地接，出境计调列表搜索实体
    /// <summary>
    /// 组团,地接，出境计调列表搜索实体
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
        /// 销售员信息
        /// </summary>
        public MSaleInfo SaleInfo { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 团队确认状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus? TourSureStatus { get; set; }
    }
    #endregion

    #region 组团计调列表实体ＸＸＸ
    public class MZTPlanList : MPlanList
    {
    }
    #endregion

    #region 地接计调列表实体ＸＸＸ
    public class MDJPlanList : MPlanList
    {
    }
    #endregion

    #region 出镜计调列表实体ＸＸＸ
    public class MCJPlanList : MPlanList
    {
    }
    #endregion

    #region 报账搜索实体
    /// <summary>
    /// 报账搜索实体
    /// </summary>
    [Serializable]
    public class MBZSearch
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 计调员名称
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 出团时间（开始）
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        ///出团时间(结束)
        /// </summary>
        public DateTime? LLDate { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string Guide { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId { get; set; }
        /// <summary>
        /// 是否报账：未报账/已报账
        /// </summary>
        public bool IsDealt { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string FaBuRenId { get; set; }
        /// <summary>
        /// 发布人姓名
        /// </summary>
        public string FaBuRenName { get; set; }
        /// <summary>
        /// 二级栏目枚举
        /// </summary>
        public Menu2 SL { get; set; }
        /// <summary>
        /// 报销报账枚举
        /// </summary>
        public BZList Type { get; set; }
        /// <summary>
        /// 是否显示购物收入、其他收入、其他支出
        /// </summary>
        public bool IsShowGouWu { get; set; }
    }
    #endregion

    #region 报账实体
    /// <summary>
    /// 报账实体
    /// </summary>
    [Serializable]
    public class MBZInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MBZInfo() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }
        /// <summary>
        /// 导游人员
        /// </summary>
        public IList<MGuidInfo> MGuidInfo { get; set; }
        /// <summary>
        /// 计调人员
        /// </summary>
        public IList<MTourPlaner> MPlanerInfo { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 收入（结算金额合计）
        /// </summary>
        public decimal TourSettlement { get; set; }
        /// <summary>
        /// 支出
        /// </summary>
        public decimal TourPay { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal Profit { get { return TourSettlement - TourPay; } }
        /// <summary>
        /// 利润分配
        /// </summary>
        public decimal DisProfit { get; set; }
        /// <summary>
        /// 其他收入（包括购物收入）
        /// </summary>
        public decimal OtherIncome { get; set; }
        /// <summary>
        /// 其他支出
        /// </summary>
        public decimal OtherOutpay { get; set; }
        /// <summary>
        /// 是否显示购物收入、其他收入、其他支出、利润分配
        /// </summary>
        public bool IsShowGouWu { get; set; }
        /// <summary>
        /// 净利润
        /// </summary>
        public decimal JProfit { get { return Profit + (IsShowGouWu ? (OtherIncome - OtherOutpay - DisProfit) : 0); } }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 订单结算金额合计，所有订单报账后用黑色，其它红色
        /// </summary>
        public bool IsBlack { get; set; }
        /// <summary>
        /// 发布人姓名
        /// </summary>
        public string FaBuRenName { get; set; }
        /// <summary>
        /// 计调员（张三,李四）
        /// </summary>
        public string Planers { get; set; }
        /// <summary>
        /// 导游（王二,马五）
        /// </summary>
        public string Guides { get; set; }
    }
    #endregion

    #region 计划行程
    /// <summary>
    /// 行程基础信息实体
    /// </summary>
    [Table(Name = "tbl_RoutePlan")]
    public class MPlanBaseInfo
    {
        /// <summary>
        /// 线路编号，报价编号，计划编号
        /// </summary>
        [Column(Name = "RouteId", DbType = "char(36)")]
        public string ItemId { get; set; }
        /// <summary>
        /// 行程主键编号
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "PlanId", DbType = "char(36)")]
        public string PlanId { get; set; }
        /// <summary>
        /// 行程的第几天
        /// </summary>
        [Column(Name = "Days", DbType = "int")]
        public int Days { get; set; }
        /// <summary>
        /// 行程区间
        /// </summary>
        [Column(Name = "Section", DbType = "nvarchar(255)")]
        public string Section { get; set; }
        /// <summary>
        /// 交通
        /// </summary>
        [Column(Name = "Traffic", DbType = "nvarchar(255)")]
        public string Traffic { get; set; }
        /// <summary>
        /// 行程酒店
        /// </summary>
        [Column(Name = "Hotel", DbType = "nvarchar(255)")]
        public string Hotel { get; set; }
        /// <summary>
        /// 行程酒店编号
        /// </summary>
        [Column(Name = "HotelId", DbType = "char(1)")]
        public string HotelId { get; set; }
        /// <summary>
        /// 行程是否包括早餐
        /// </summary>
        [Column(Name = "Breakfast", DbType = "char(1)")]
        public bool Breakfast { get; set; }
        /// <summary>
        /// 行程是否包括午餐
        /// </summary>
        [Column(Name = "Lunch", DbType = "char(1)")]
        public bool Lunch { get; set; }
        /// <summary>
        /// 行程是否包括晚餐
        /// </summary>
        [Column(Name = "Supper", DbType = "char(1)")]
        public bool Supper { get; set; }
        /// <summary>
        /// 行程内容
        /// </summary>
        [Column(Name = "Content", DbType = "nvarchar(Max)")]
        public string Content { get; set; }
        /// <summary>
        /// 行程上传文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 计划行程景点集合
        /// </summary>
        public IList<MTourPlanSpot> TourPlanSpot { get; set; }
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

        /// <summary>
        /// 行程编号
        /// </summary>
        public string PlanId { get; set; }
    }
    #endregion

    #region 计划安排计调项
    /// <summary>
    /// 计划安排计调项
    /// </summary>
    public class MTourPlanItem
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 计调项目
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject PlanType { get; set; }
    }
    #endregion

    #region 计划计调人员
    /// <summary>
    /// 计划计调人员
    /// </summary>
    public class MTourPlaner : MPersonInfo
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 计调员ID
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 计调员部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    #endregion

    #region 团队计划分项报价
    /// <summary>
    /// 团队计划分项报价
    /// </summary>
    public class MTourTeamPrice
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DanJia { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public float ShuLiang { get; set; }
        /// <summary>
        /// 单项报价
        /// </summary>
        public decimal Quote { get; set; }
        /// <summary>
        /// 服务名称（酒店名称...)
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceId { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit Unit { get; set; }
        /// <summary>
        /// 服务标准
        /// </summary>
        public string ServiceStandard { get; set; }
        /// <summary>
        /// 服务项目
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectType ServiceType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否含GST
        /// </summary>
        public bool IsTax { get; set; }
    }
    #endregion

    #region 散拼计划报价信息
    /// <summary>
    /// 散拼计划报价标准
    /// </summary>
    public class MTourPriceStandard
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 报价标准编号
        /// </summary>
        public int Standard { get; set; }
        /// <summary>
        /// 报价等级名称
        /// </summary>
        public string StandardName { get; set; }
        /// <summary>
        /// 客户等级列表
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPriceLevel> PriceLevel { get; set; }
    }

    /// <summary>
    /// 散拼计划客户等级
    /// </summary>
    public class MTourPriceLevel
    {
        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int LevelId { get; set; }
        /// <summary>
        /// 客户等级名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 客户等级类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.LevType LevType { get; set; }
        /// <summary>
        /// 价格类型(成本价格、销售价格)
        /// </summary>
        public CostMode CostMode { get; set; }
    }

    #endregion

    #region 计划服务
    /// <summary>
    /// 计划服务
    /// </summary>
    [Table(Name = "tbl_RouteServices")]
    public class MTourService
    {

        /// <summary>
        /// 线路库ID
        /// </summary>
        [Column(Name = "RouteId", DbType = "char(36)")]
        public string ItemId
        {
            set;
            get;
        }

        /// <summary>
        /// 服务标准
        /// </summary>
        public string ServiceStandard { get; set; }
        /// <summary>
        /// 不含项目
        /// </summary>
        [Column(Name = "Excluding", DbType = "nvarchar(Max)")]
        public string NoNeedItem { get; set; }
        /// <summary>
        /// 购物安排
        /// </summary>
        [Column(Name = "Shopping", DbType = "nvarchar(Max)")]
        public string ShoppingItem { get; set; }
        /// <summary>
        /// 儿童安排
        /// </summary>
        [Column(Name = "Children", DbType = "nvarchar(Max)")]
        public string ChildServiceItem { get; set; }
        /// <summary>
        /// 自费项目
        /// </summary>
        [Column(Name = "Chargeable", DbType = "nvarchar(Max)")]
        public string OwnExpense { get; set; }
        /// <summary>
        /// 注意事项
        /// </summary>
        [Column(Name = "Note", DbType = "nvarchar(Max)")]
        public string NeedAttention { get; set; }
        /// <summary>
        /// 温馨提醒
        /// </summary>
        [Column(Name = "WarmPrompt", DbType = "nvarchar(Max)")]
        public string WarmRemind { get; set; }
        /// <summary>
        /// 内部信息
        /// </summary>
        [Column(Name = "Internal", DbType = "nvarchar(Max)")]
        public string InsiderInfor { get; set; }
    }
    #endregion

    #region 计划变更实体

    /// <summary>
    /// 计划变更实体
    /// </summary>
    public class MTourPlanChangeBase
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
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路区域ID
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 计划销售员编号
        /// </summary>
        public string SaleId { get; set; }
        /// <summary>
        /// 计划销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计调员ID
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 变更导游编号
        /// </summary>
        public string GuideId { get; set; }
        /// <summary>
        /// 变更导游
        /// </summary>
        public string GuideNm { get; set; }
        /// <summary>
        /// 变更时间_开始
        /// </summary>
        public DateTime? IssueTimeS { get; set; }
        /// <summary>
        /// 变更时间_结束
        /// </summary>
        public DateTime? IssueTimeE { get; set; }
        /// <summary>
        /// 变更类型(0 导游变更1 销售变更)
        /// </summary>
        public ChangeType? ChangeType { get; set; }
        /// <summary>
        /// 变更状态(0：销售未确认 1：销售暂不处理 2：销售已确认/计调未确认 3：计调已确认)
        /// </summary>
        public ChangeStatus? State { get; set; }
        /// <summary>
        /// 二级栏目枚举（获取组织浏览权限用）
        /// </summary>
        public Menu2 SL { get; set; }
    }

    /// <summary>
    /// 计划变更实体
    /// </summary>
    public class MTourPlanChange : MTourPlanChangeBase
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 计划计调人员列表
        /// </summary>
        public IList<MTourPlaner> TourPlaner { get; set; }
        /// <summary>
        /// 计划导游
        /// </summary>
        public IList<MGuidInfo> TourGuide { get; set; }
        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public EyouSoft.Model.HTourStructure.MTourPlanStatus TourPlanStatus { get; set; }
        /// <summary>
        /// 变更标题
        /// </summary>
        public string Title { get; set; }
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
        /// 变更时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 变更人部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
    }

    /// <summary>
    /// 业务变更确认实体
    /// </summary>
    public class MTourPlanChangeConfirm
    {
        /// <summary>
        /// 变更编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 确认人类型[0:销售员 1:计调员]
        /// </summary>
        public ConfirmerType ConfirmerType { get; set; }
        /// <summary>
        /// 确认变更人
        /// </summary>
        public string Confirmer { get; set; }
        /// <summary>
        /// 确认变更人编号
        /// </summary>
        public string ConfirmerId { get; set; }
        /// <summary>
        /// 确认变更时间
        /// </summary>
        public DateTime ConfirmTime { get; set; }
        /// <summary>
        /// 要更新的变更状态（0：销售未确认 1：销售暂不处理 2：销售已确认/计调未确认 3：计调已确认）
        /// </summary>
        public ChangeStatus ChangeStatus { get; set; }
        /// <summary>
        /// 变更类型（0：导游变更 1：销售变更）
        /// </summary>
        public ChangeType ChangeType { get; set; }
    }
    #endregion

    #region 单项业务,无计划供应商安排
    /// <summary>
    /// 单项业务,无计划供应商安排
    /// </summary>
    public class MSingleSupplier
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject Type { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SourceName { get; set; }
        /// <summary>
        /// 具体安排
        /// </summary>
        public string GuideNotes { get; set; }
        /// <summary>
        /// 结算价
        /// </summary>
        public decimal PlanCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion

    #region 单位基本信息
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
    #endregion

    #region 人员相关信息
    /// <summary>
    /// 人员相关信息
    /// </summary>
    public class MPersonInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string Mobile { get; set; }
    }
    /// <summary>
    /// 销售员相关信息
    /// </summary>
    public class MSaleInfo : MPersonInfo
    {
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    /// <summary>
    /// 操作员相关信息
    /// </summary>
    public class MOperatorInfo : MPersonInfo
    {
        /// <summary>
        /// 操作员编号/报价员
        /// </summary>
        public string OperatorId { get; set; }
    }
    /// <summary>
    /// 计调员相关信息
    /// </summary>
    public class MPlanerInfo : MPersonInfo
    {
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }
    }
    /// <summary>
    /// 导游相关信息
    /// </summary>
    public class MGuidInfo : MPersonInfo
    {
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuidId { get; set; }
    }

    /// <summary>
    /// 发布人相关信息
    /// </summary>
    public class MPublisherInfo : MPersonInfo
    {
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
    #endregion

    #region 团队计划搜索实体
    /// <summary>
    /// 团队计划搜索实体
    /// </summary>
    public class MTourTeamSearch
    {
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 出团时间开始
        /// </summary>
        public DateTime? LDateStart { get; set; }
        /// <summary>
        /// 出团时间结束
        /// </summary>
        public DateTime? LDateEnd { get; set; }
        /// <summary>
        /// 回团日期开始
        /// </summary>
        public DateTime? RDateStart { get; set; }
        /// <summary>
        /// 回团日期结束
        /// </summary>
        public DateTime? RDateEnd { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 操作人/下单人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人/下单人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
    }
    #endregion

    #region 散拼计划搜索实体
    /// <summary>
    /// 散拼计划搜索实体
    /// </summary>
    public class MTourSanPinSearch
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 计划关键字编号
        /// </summary>
        public int KeyId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int TourDays { get; set; }
        /// <summary>
        /// 出团时间（开始）
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团时间(结束)
        /// </summary>
        public DateTime? LLDate { get; set; }
        /// <summary>
        /// 回团时间（开始）
        /// </summary>
        public DateTime? SRDate { get; set; }
        /// <summary>
        /// 回团时间(结束)
        /// </summary>
        public DateTime? LRDate { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus? TourStatus { get; set; }

        /// <summary>
        /// 团队确认状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourSureStatus? TourSureStatus { get; set; }
        /// <summary>
        /// 模版团编号
        /// </summary>
        public string ParentId { get; set; }
    }
    #endregion

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
        /// <summary>
        /// 是否出团
        /// </summary>
        public bool? IsChuTuan { get; set; }
        /// <summary>
        /// 线路区域关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 散拼模版团编号
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public TourType? TourType { get; set; }
    }
    #endregion

    #region 供应商平台计划搜索实体
    /// <summary>
    /// 供应商平台计划搜索实体
    /// </summary>
    public class MTourSupplierSearch
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 出团时间（开始）
        /// </summary>
        public DateTime? SLDate { get; set; }
        /// <summary>
        /// 出团时间(结束)
        /// </summary>
        public DateTime? LLDate { get; set; }
        /// <summary>
        /// 实收人数运算符
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.EqualSign? RealPeopleNumberManipulate { get; set; }
        /// <summary>
        /// 实收人数
        /// </summary>
        public int RealPeopleNumber { get; set; }
    }
    #endregion

    #region 线路区域团队
    /// <summary>
    /// 线路区域团队
    /// </summary>
    public class MAreaTour
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 团队数
        /// </summary>
        public int TourNum { get; set; }
    }
    #endregion

    #region 关键字团队
    /// <summary>
    /// 关键字团队
    /// </summary>
    public class MKeyTour
    {
        /// <summary>
        ///关键字编号
        /// </summary>
        public int KeyId { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 团队数
        /// </summary>
        public int TourNum { get; set; }
    }
    #endregion

    #region 计划项目安排落实实体
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
    #endregion

    #region 计划弹出信息
    /// <summary>
    /// 计划弹出信息
    /// </summary>
    public class MTourBaoInfo
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 发布人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 资源预控团号实体
    /// <summary>
    /// 资源预控团号实体
    /// </summary>
    public class MControlTour
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
    }
    #endregion

    #region 资源预控团号搜索实体
    /// <summary>
    /// 资源预控团号实体
    /// </summary>
    public class MControlTourSearch
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType? TourType { get; set; }
        /// <summary>
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }
        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }

    }
    #endregion

    #region 派团给计调时，显示订单列表
    /// <summary>
    /// 派团给计调时，显示订单列表
    /// </summary>
    public class MSendTourOrderList
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 计划的第几单
        /// </summary>
        public int TheNum { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 订单销售员
        /// </summary>
        public string SellerName { get; set; }
    }
    #endregion

    #region 团队状态变更实体
    /// <summary>
    /// 团队状态变更实体
    /// </summary>
    public class MTourStatusChange
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
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 团队订单合同确认金额合计
        /// </summary>
        public decimal TourIncome { get; set; }
        /// <summary>
        /// 团队总支出
        /// </summary>
        public decimal TourPay { get; set; }
        /// <summary>
        /// 团队订单结算金额合计
        /// </summary>
        public decimal TourSettlement { get; set; }
        /// <summary>
        ///  团队其它收入
        /// </summary>
        public decimal TourOtherIncome { get; set; }
        /// <summary>
        /// 团队利润
        /// </summary>
        public decimal TourProfit { get; set; }
        /// <summary>
        /// 团队订单分配利润
        /// </summary>
        public decimal DisOrderProfit { get; set; }
        /// <summary>
        /// 团队分配利润
        /// </summary>
        public decimal DisTourProfit { get; set; }
        /// <summary>
        /// 操作返回值
        /// </summary>
        public int OutputCode { get; set; }
    }
    #endregion

    #region 派团给计调实体
    /// <summary>
    /// 派团给计调实体
    /// </summary>
    public class MSendTour
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsiderInfor { get; set; }
        /// <summary>
        /// 计调员列表
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPlaner> Planer { get; set; }
        /// <summary>
        /// 计调项
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPlanItem> PlanItem { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    #endregion

    #region 计划原始信息实体
    /// <summary>
    /// 计划原始信息实体
    /// </summary>
    public class MTourOriginalInfo
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
        /// 计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 计划内容
        /// </summary>
        public string TourContent { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 订单利润分配订单实体
    /// <summary>
    /// 订单利润分配订单实体
    /// </summary>
    public class MTourOrderDisInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int PersonNum { get; set; }
        /// <summary>
        /// 确认合同金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 确认结算金额
        /// </summary>
        public decimal ConfirmSettlementMoney { get; set; }
        /// <summary>
        /// 订单销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal MaoProfit { get { return ConfirmMoney - ConfirmSettlementMoney; } }
    }
    #endregion


    #region  -----------------短线添加的类--------------------

    #region 计划上车地点
    /// <summary>
    /// 计划上车地点
    /// </summary>
    public class MTourCarLocation
    {
        /// <summary>
        /// 计划上车地点编号
        /// </summary>
        public string TourLocationId { get; set; }

        /// <summary>
        /// 上车地点编号
        /// </summary>
        public string CarLocationId { get; set; }

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

        /// <summary>
        /// 该上车地点是否被订单使用
        /// </summary>
        public bool isTourOrderExists { get; set; }

    }
    #endregion

    #region 计划预设车型
    /// <summary>
    /// 计划预设车型
    /// </summary>
    public class MTourCarType
    {
        /// <summary>
        /// 计划车型编号
        /// </summary>
        public string TourCarTypeId { get; set; }

        /// <summary>
        /// 车型编号
        /// </summary>
        public string CarTypeId { get; set; }

        /// <summary>
        /// 车型名称
        /// </summary>
        public string CarTypeName { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatNum { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
    #endregion

    #region  短线散拼计划业务实体
    /// <summary>
    /// 短线散拼计划业务实体
    /// </summary>
    //public class MTourShortSanPinInfo : MTourSanPinInfo
    //{

    //    /// <summary>
    //    /// 短线团上车地点集合
    //    /// </summary>
    //    public IList<MTourCarLocation> TourCarLocation { get; set; }

    //    /// <summary>
    //    /// 短线团预设车型集合
    //    /// </summary>
    //    public IList<MTourCarType> TourCarType { get; set; }

    //}
    #endregion

    #region 车型座次变更记录
    /// <summary>
    /// 车型座次变更记录
    /// </summary>
    public class MCarTypeSeatChange
    {

        /// <summary>
        ///自增编号
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
        /// 线路区域
        /// </summary>
        public string RouteName { get; set; }


        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 车型变更的类型，车型变更 = 0,座次变更 = 1
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.CarChangeType CarChangeType { get; set; }

        /// <summary>
        /// 车型变化的信息
        /// </summary>
        public string ChangeContent { get; set; }

        /// <summary>
        /// 分销商编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public bool IsRead { get; set; }
    }
    #endregion

    #endregion

    #region  自行设置计划状态信息业务实体
    /// <summary>
    /// 自行设置计划状态信息业务实体
    /// </summary>
    public class MSetTourStatusInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSetTourStatusInfo() { }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus Status { get; set; }
        /// <summary>
        /// 计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
    }
    #endregion
}
