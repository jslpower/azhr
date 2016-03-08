using System;

namespace EyouSoft.Model.ConStructure
{
    #region 合同号实体
    /// <summary>
    /// 合同号实体
    /// 2011-09-01 邵权江 创建
    /// </summary>
    public class MContractNum
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MContractNum() {}
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 合同类型
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractType ContractType { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 合同号状态
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractStatus ContractStatus { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 合同号领用实体
    /// <summary>
    /// 合同号领用
    /// 2011-09-01 邵权江 创建
    /// </summary>
    public class MContractNumCollar
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MContractNumCollar() { }
        /// <summary>
        /// 领用编号
        /// </summary>
        public string CollariId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 领用部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 领用人编号
        /// </summary>
        public string UseId { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 合同号列表实体
    /// <summary>
    /// 合同号列表实体
    /// </summary>
    public class MContractNumList
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MContractNumList() { }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime? CollarTime { get; set; }
        /// <summary>
        /// 领用人编号
        /// </summary>
        public string UseId { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string UseName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 客源单位编号
        /// </summary>
        public string BuyCompanyId { get; set; }
        /// <summary>
        /// 客源单位
        /// </summary>
        public string BuyCompanyName { get; set; }
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
        /// 金额
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 合同号状态
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractStatus ContractStatus { get; set; }
    }
    #endregion

    #region 合同号查询实体
    /// <summary>
    /// 合同号领用
    /// 2011-09-01 邵权江 创建
    /// </summary>
    public class MContractNumSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MContractNumSearch() { }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 领用人编号
        /// </summary>
        public string UseId { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string UseName { get; set; }
        /// <summary>
        /// 领用开始时间
        /// </summary>
        public DateTime? TimeStart { get; set; }
        /// <summary>
        /// 领用结束时间
        /// </summary>
        public DateTime? TimeEnd { get; set; }
        /// <summary>
        /// 合同号状态
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractStatus? ContractStatus { get; set; }
    }
    #endregion

    #region 合同号状态实体
    /// <summary>
    /// 合同号状态实体
    /// 2011-09-01 邵权江 创建
    /// </summary>
    public class MContractStatus
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MContractStatus() { }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 合同号状态
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractStatus? ContractStatus { get; set; }
    }
    #endregion

    #region 合同自动匹配查询实体
    /// <summary>
    /// 合同自动匹配查询实体
    /// </summary>
    public class MAutocompleteChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MAutocompleteChaXunInfo() { }
        /// <summary>
        /// 合同号
        /// </summary>
        public string HeTongCode { get; set; }
        /// <summary>
        /// 合同类型
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractType? LeiXing { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public EyouSoft.Model.EnumType.ConStructure.ContractStatus? Status { get; set; }
        /// <summary>
        /// 获取的记录数
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HeTongId { get; set; }
    }
    #endregion
}
