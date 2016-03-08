using System;

namespace EyouSoft.Model.GovStructure
{
    #region 物品实体
    /// <summary>
    /// 物品实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovGood
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovGood() { }

        /// <summary>
        /// 物品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Use { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
    }
    #endregion

    #region 物品领用/发放/借阅 实体
    /// <summary>
    /// 物品领用/发放/借阅 实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovGoodUse
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovGoodUse() { }

        /// <summary>
        /// 领用编号
        /// </summary>
        public string UseId { get; set; }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string GoodId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 领用/发放/借阅
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.GoodUseType Type { get; set; }
        /// <summary>
        /// 领用/发放/借阅时间
        /// </summary>
        public DateTime? Time { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 使用人ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 使用人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Use { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 领用部门名称
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
    }
    #endregion

    #region 物品列表实体
    /// <summary>
    /// 物品列表实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovGoodList : MGovGood
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovGoodList() { }

        /// <summary>
        /// 领用数量
        /// </summary>
        public int CollarNumber { get; set; }
        /// <summary>
        /// 发放数量
        /// </summary>
        public int GrantNumber { get; set; }
        /// <summary>
        /// 借阅数量
        /// </summary>
        public int BorrowNumber { get; set; }
    }
    #endregion

    #region 物品使用列表实体
    /// <summary>
    /// 物品使用列表实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovGoodUseList : MGovGoodUse
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovGoodUseList() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal SumPrice { get { return Price * Number; } }
    }
    #endregion
}
