using System;
using System.Collections.Generic;

//2011-09-01 AM 曹胡生 创建
//2012-8-17 王磊 修改垫付申请的实体：MAdvanceApp
namespace EyouSoft.Model.TourStructure
{
    #region 团队计划报价实体
    /// <summary>
    /// 团队计划报价实体
    /// </summary>
    public class MTourQuoteInfo
    {
        /// <summary>
        /// 报价编号
        /// </summary>
        public string QuoteId { get; set; }
        /// <summary>
        /// 报价成功，生成的订单编号
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
        /// 父级编号(如果新增传0，保存为新报价时为当前ParentId,如果当前的ParentId为0，则传当前QuoteId)
        /// </summary>
        public string ParentId { get; set; }
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
        /// 行程天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 询价公司
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 询价公司编号
        /// </summary>
        public string BuyCompanyID { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 询价单位基本信息
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
        /// 客源单位联系人编号
        /// </summary>
        public string ContactDepartId { get; set; }
        /// <summary>
        /// 销售员信息
        /// </summary>
        public MSaleInfo SaleInfo { get; set; }
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
        /// 对外报价整团服务标准
        /// </summary>
        public string ServiceStandard { get; set; }
        /// <summary>
        /// 报价取消原因
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// 操作员编号/报价员
        /// </summary>
        public MOperatorInfo OperatorInfo { get; set; }
        /// <summary>
        /// 第几次报价
        /// </summary>
        public int TimeCount { get; set; }
        /// <summary>
        /// 是否列表显示（列表只显示已成功报价或最后一次报价）
        /// </summary>
        public bool IsLatest { get; set; }
        /// <summary>
        /// 咨询时间
        /// </summary>
        public DateTime InquiryTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 计划行程集合
        /// </summary>
        public IList<MPlanBaseInfo> QuotePlan { get; set; }
        /// <summary>
        /// 计划分项报价集合,单项业务客人要求集合
        /// </summary>
        public IList<MTourTeamPrice> TourTeamPrice { get; set; }
        /// <summary>
        /// 成本核算
        /// </summary>
        public string CostCalculation { get; set; }
        /// <summary>
        /// 向计调询价，计调员编号
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 向计调询价，计调员
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        ///计调是否报价（计调只修改成本核算，一旦保存，不能再改，除非销售再次询价）
        /// </summary>
        public bool IsPlanerQuote { get; set; }
        /// <summary>
        /// 计划服务
        /// </summary>
        public MTourService TourService { get; set; }
        /// <summary>
        /// 团队报价状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteState QuoteState { get; set; }
        /// <summary>
        /// 对外报价类型（整团，分项）
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourQuoteType OutQuoteType { get; set; }
        ///// <summary>
        ///// 模块类型
        ///// </summary>
        //public EyouSoft.Model.EnumType.TourStructure.ModuleType QuoteType { get; set; }
        /// <summary>
        /// 报价成功，团队信息
        /// </summary>
        public MTourQuoteTourInfo MTourQuoteTourInfo { get; set; }
        /// <summary>
        /// 报价成功，超限申请
        /// </summary>
        public MAdvanceApp AdvanceApp { get; set; }
        /// <summary>
        /// 团队报价次数编号列表
        /// </summary>
        public IList<MTourQuoteNo> TourQuoteNo { get; set; }
        /// <summary>
        /// 其它费用
        /// </summary>
        public decimal OtherCost { get; set; }
        /// <summary>
        /// 行程特色
        /// </summary>
        public string PlanFeature { get; set; }
        /// <summary>
        /// 报价备注
        /// </summary>
        public string QuoteRemark { get; set; }
        /// <summary>
        /// 签证资料文件
        /// </summary>
        public IList<EyouSoft.Model.ComStructure.MComAttach> VisaFileList { get; set; }
    }

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

    #region 团队报价团队信息
    /// <summary>
    /// 团队报价团队,订单信息
    /// </summary>
    public class MTourQuoteTourInfo
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
        /// 计划类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }

        #region 订单信息
        /// <summary>
        /// 游客列表
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> Traveller { get; set; }
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
        public string AddCostRemark { set; get; }
        /// <summary>
        /// 减少费用备注
        /// </summary>
        public string ReduceCostRemark { set; get; }
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
       
        #endregion
    }
    #endregion

    #region 垫付申请实体
    /// <summary>
    /// 垫付申请实体
    /// </summary>
    public class MAdvanceApp
    {
        //新增属性：DisburseId,CompanyId,ItemId,ItemType,OperatorId,Operator

        /// <summary>
        /// 垫付申请编号
        /// </summary>
        public string DisburseId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { set; get; }


        /// <summary>
        /// 报价编号/团队编号/订单编号
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 0：报价成功/1：成团/2：报名
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.TransfiniteType ItemType { get; set; }


        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        
        //---------------------------------------------------------------
        /// <summary>
        /// 申请人编号
        /// </summary>
        public string ApplierId { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applier { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 垫付金额
        /// </summary>
        public decimal DisburseAmount { get; set; }
        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }


    #endregion

    #region 团队报价搜索实体
    /// <summary>
    /// 团队报价搜索实体
    /// </summary>
    public class MTourQuoteSearch
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
        /// 询价单位编号
        /// </summary>
        public string BuyCompanyID { get; set; }
        /// <summary>
        /// 询价单位名称
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 报价员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 报价员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 团队报价状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.QuoteState? QuoteState { get; set; }
    }
    #endregion
}
