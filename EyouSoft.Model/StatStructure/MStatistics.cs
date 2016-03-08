using System;
using System.Collections.Generic;

//统计分析
//创建者：郑知远
//创建时间：2011-09-01
namespace EyouSoft.Model.StatStructure
{
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    #region 统计基础信息实体
    /// <summary>
    /// 统计基础信息实体
    /// </summary>
    [Serializable]
    public class MStaBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MStaBase() { }

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// 总支出
        /// </summary>
        public decimal TotalOutlay { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfit { get { return this.TotalIncome - this.TotalOutlay; } }

        /// <summary>
        /// 毛利率
        /// </summary>
        public decimal GrossProfitRate { get { return this.TotalIncome == 0 ? 0 : this.GrossProfit / this.TotalIncome; } }
    }
    #endregion

    #region 线路流量统计
    /// <summary>
    /// 线路流量统计
    /// </summary>
    [Serializable]
    public class MRouteFlow : MStaBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRouteFlow() { }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }

        /// <summary>
        /// 团数
        /// </summary>
        public int TourCount { get; set; }

        /// <summary>
        /// 人均毛利率
        /// </summary>
        public decimal PerGrossProfitRate
        {
            get
            {
                if ((Adults + Childs / 2) == 0) return 1;
                return this.GrossProfit / (Adults + Childs / 2);
            }
        }
    }

    /// <summary>
    /// 团队数量列表
    /// </summary>
    public class MRouteFlowTourList
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
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }
    }

    /// <summary>
    /// 总收入列表
    /// </summary>
    public class MRouteFlowOrderList
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 订单销售员
        /// </summary>
        public string SellerName { get; set; }
    }

    /// <summary>
    /// 总支出列表
    /// </summary>
    public class MRouteFlowPayList
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
        /// 实收人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal PayMoney { get; set; }
    }

    /// <summary>
    /// 线路流量搜索实体
    /// </summary>
    public class MRouteFlowSearch
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
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }

        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }

        /// <summary>
        /// 核算日期开始
        /// </summary>
        public DateTime? SReviewTime { get; set; }

        /// <summary>
        /// 核算日期结束
        /// </summary>
        public DateTime? EReviewTime { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    #endregion

    #region 部门业绩统计
    /// <summary>
    /// 按月统计每个部门的业绩，根据销售员属于那个部门
    /// </summary>
    [Serializable]
    public class MDepartment : MStaBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDepartment() { }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 员工人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 订单人数
        /// </summary>
        public int OrderPersonNum { get; set; }
    }

    /// <summary>
    /// 部门业绩统计汇总实体
    /// </summary>
    public class MDepartmentTongJi
    {
        /// <summary>
        /// 订单数量合计
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 订单人数合计
        /// </summary>
        public string PeopleNum { get; set; }
        /// <summary>
        /// 收入合计
        /// </summary>
        public string InCome { get; set; }
        /// <summary>
        /// 支出合计
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 毛利合计
        /// </summary>
        public string GrossProfit { get; set; }
    }

    /// <summary>
    /// 员工人数统计汇总实体
    /// </summary>
    public class MDepartmentPeopleListTongJi : MDepartmentTongJi
    {

    }

    /// <summary>
    /// 员工人数
    /// </summary>
    public class MDepartmentPeopleList : MStaBase
    {
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 部门业绩搜索实体
    /// </summary>
    public class MDepartmentSearch
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }

        /// <summary>
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }

        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }

        /// <summary>
        /// 核算日期开始
        /// </summary>
        public DateTime? SReviewTime { get; set; }

        /// <summary>
        /// 核算日期结束
        /// </summary>
        public DateTime? EReviewTime { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    #endregion

    #region 个人业绩统计
    /// <summary>
    /// 个人业绩统计
    /// </summary>
    [Serializable]
    public class MPersonal : MStaBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPersonal() { }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 个人业绩统计订单列表
    /// </summary>
    public class MPersonalOrderList : MStaBase
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
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public string LDate { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 订单人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 下单人
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 个人业绩统计订单列表合计信息业务实体
    /// </summary>
    public class MPersonalOrderListTongJi
    {
        /// <summary>
        /// 订单人数合计
        /// </summary>
        public string PeopleNum { get; set; }
        /// <summary>
        /// 收入合计
        /// </summary>
        public string InCome { get; set; }
        /// <summary>
        /// 支出合计
        /// </summary>
        public string Pay { get; set; }
        /// <summary>
        /// 毛利合计
        /// </summary>
        public string GrossProfit { get; set; }
    }

    /// <summary>
    /// 个人业绩搜索实体
    /// </summary>
    public class MPersonalSearch
    {
        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }

        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }

        /// <summary>
        /// 核算日期开始
        /// </summary>
        public DateTime? SReviewTime { get; set; }

        /// <summary>
        /// 核算日期结束
        /// </summary>
        public DateTime? EReviewTime { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
    }
    #endregion

    #region 收入对账单
    /// <summary>
    /// 收入对账单
    /// </summary>
    [Serializable]
    public class MReconciliation
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 应收款
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 已收款
        /// </summary>
        public decimal InAmount { get; set; }

        /// <summary>
        /// 未收款
        /// </summary>
        public decimal RestAmount { get; set; }
    }

    /// <summary>
    /// 收入对账单搜索实体
    /// </summary>
    public class MReconciliationSearch
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 销售员名称
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }

        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.EqualSign EqualSign { get; set; }

        /// <summary>
        /// 未收款
        /// </summary>
        public decimal RestAmount { get; set; }
    }

    /// <summary>
    /// 收入对账单统计实体
    /// </summary>
    public class MReconciliationTongJi
    {
        /// <summary>
        /// 应收款合计
        /// </summary>
        public string TotalAmount { get; set; }

        /// <summary>
        /// 已收款合计
        /// </summary>
        public string InAmount { get; set; }

        /// <summary>
        /// 未收款合计
        /// </summary>
        public string RestAmount { get; set; }
    }

    /// <summary>
    /// 收入对账单未收款实体
    /// </summary>
    public class MReconciliationRestAmount
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 客源单位名称
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 订单人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 应收款
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 已收款
        /// </summary>
        public decimal InAmount { get; set; }

        /// <summary>
        /// 未收款
        /// </summary>
        public decimal RestAmount { get; set; }

    }
    #endregion

    #region 状态查询表
    /// <summary>
    /// 状态查询表
    /// </summary>
    [Serializable]
    public class MTourStatus
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
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime RDate { get; set; }

        /// <summary>
        /// 计划人数
        /// </summary>
        public int PersonNum { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MTourPlaner> Planer { get; set; }

        /// <summary>
        /// 导游
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.MGuidInfo> Guide { get; set; }

        /// <summary>
        /// 团队状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus TourStatus { get; set; }
        /// <summary>
        /// 计划类型
        /// </summary>
        public EnumType.TourStructure.TourType TourType { get; set; }
    }

    /// <summary>
    /// 状态查询表搜索实体
    /// </summary>
    public class MTourStatusSearch
    {
        /// <summary>
        /// 出团日期开始
        /// </summary>
        public DateTime? LDateS { get; set; }
        /// <summary>
        /// 出团日期结束
        /// </summary>
        public DateTime? LDateE { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员名字
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计划状态
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourStatus[] TourStatus { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuiderId { get; set; }
        /// <summary>
        /// 导游
        /// </summary>
        public string Guide { get; set; }
        /// <summary>
        /// 回团日期开始
        /// </summary>
        public DateTime? RDateS { get; set; }
        /// <summary>
        /// 回团日期结束
        /// </summary>
        public DateTime? RDateE { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 计划销售员部门
        /// </summary>
        public int[] TourSellerDeptIds { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string JiDiaoYuanId { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }
    }
    #endregion

    #region 游客统计表
    /// <summary>
    /// 游客统计表
    /// </summary>
    public class MTravellerFlow
    {
        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 人天数
        /// </summary>
        public int PeopleDayNum { get; set; }
        /// <summary>
        /// 来源地
        /// </summary>
        public string Place { get; set; }
    }

    /// <summary>
    /// 游客统计表搜索实体
    /// </summary>
    public class MTravellerFlowSearch
    {
        /// <summary>
        /// 出团时间开始
        /// </summary>
        public DateTime? LDateS { get; set; }
        /// <summary>
        /// 出团时间结束
        /// </summary>
        public DateTime? LDateE { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
    }
    #endregion

    #region 预算/结算对比表

    /// <summary>
    /// 预算/结算对比表
    /// </summary>
    public class MBudgetContrastBase
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
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Crm { get; set; }

        /// <summary>
        /// 出团时间-开始
        /// </summary>
        public string LDateS { get; set; }

        /// <summary>
        /// 出团时间-结束
        /// </summary>
        public string LDateE { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }

        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }

        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }

    }

    /// <summary>
    /// 预算/结算对比表
    /// </summary>
    public class MBudgetContrast : MBudgetContrastBase
    {
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 预算收入
        /// </summary>
        public decimal BudgetIncome { get; set; }

        /// <summary>
        /// 预算支出
        /// </summary>
        public decimal BudgetOutgo { get; set; }

        /// <summary>
        /// 结算收入
        /// </summary>
        public decimal ClearingIncome { get; set; }

        /// <summary>
        /// 结算支出
        /// </summary>
        public decimal ClearingOutgo { get; set; }

        /// <summary>
        /// 预算毛利
        /// </summary>
        public decimal BudgetGProfit { get { return this.BudgetIncome - this.BudgetOutgo; } }

        /// <summary>
        /// 结算毛利
        /// </summary>
        public decimal ClearingGProfit { get { return this.ClearingIncome - this.ClearingOutgo; } }
    }
    #endregion

    #region 日记账_基础实体
    /// <summary>
    /// 日记账_基础实体
    /// </summary>
    public class MDayRegisterBase
    {
        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 查询条件：出团时间_开始
        /// </summary>
        public string LDateS { get; set; }

        /// <summary>
        /// 查询条件：出团时间_结束
        /// </summary>
        public string LDateE { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        /// 金额（大于等于、等于、小于等于）
        /// </summary>
        public EqualSign SignAmount { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? Amount { get; set; }
    }
    #endregion

    #region 日记账
    /// <summary>
    /// 日记账
    /// </summary>
    public class MDayRegister : MDayRegisterBase
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }

        /// <summary>
        /// 借方现金
        /// </summary>
        public decimal DebitCash { get; set; }

        /// <summary>
        /// 借方银行存款
        /// </summary>
        public decimal DebitBank { get; set; }

        /// <summary>
        /// 贷方现金
        /// </summary>
        public decimal LenderCash { get; set; }

        /// <summary>
        /// 贷方银行存款
        /// </summary>
        public decimal LenderBank { get; set; }
    }
    #endregion

    #region 利润统计
    /// <summary>
    /// 利润统计-基础
    /// </summary>
    public class MProfitStatisticsBase
    {
        /// <summary>
        /// 团号/订单号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string Crm { get; set; }
        /// <summary>
        /// 出团时间-开始
        /// </summary>
        public string LDateS { get; set; }
        /// <summary>
        /// 出团时间-结束
        /// </summary>
        public string LDateE { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 计调员编号
        /// </summary>
        public string PlanerId { get; set; }
        /// <summary>
        /// 计调员
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId { get; set; }
        /// <summary>
        /// 导游
        /// </summary>
        public string Guide { get; set; }
        /// <summary>
        /// 核算时间-开始
        /// </summary>
        public string IssueTimeS { get; set; }
        /// <summary>
        /// 核算时间-结束
        /// </summary>
        public string IssueTimeE { get; set; }
    }
    /// <summary>
    /// 利润统计
    /// </summary>
    public class MProfitStatistics : MProfitStatisticsBase
    {
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 收入
        /// </summary>
        public decimal Income { get; set; }
        /// <summary>
        /// 支出
        /// </summary>
        public decimal Outlay { get; set; }
        /// <summary>
        /// 核算时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        public decimal Profit
        {
            get
            {
                return this.Income - this.Outlay;
            }
        }
    }
    #endregion
}