//财务管理相关枚举 创建者：郑知远 创建时间：2011/09/02

namespace EyouSoft.Model.EnumType.FinStructure
{
    #region 查询条件
    /// <summary>
    /// 查询条件（大于等于、等于、小于等于）
    /// </summary>
    public enum EqualSign
    {
        /// <summary>
        /// 大于等于
        /// </summary>
        大于等于 = 0,
        /// <summary>
        /// 等于
        /// </summary>
        等于 = 1,
        /// <summary>
        /// 小于等于
        /// </summary>
        小于等于 = 2
    }
    #endregion

    #region 往来对账类型
    /// <summary>
    /// 往来对账
    /// </summary>
    public enum ReconciliationType
    {
        /// <summary>
        /// 今日收款
        /// </summary>
        今日收款 = 0,
        /// <summary>
        /// 今日付款
        /// </summary>
        今日付款,
        /// <summary>
        /// 今日应收
        /// </summary>
        今日应收,
        /// <summary>
        /// 今日应付
        /// </summary>
        今日应付
    }

    #endregion

    #region 财务状态
    /// <summary>
    /// 财务状态
    /// </summary>
    public enum FinStatus
    {
        /// <summary>
        /// 财务待审批
        /// </summary>
        财务待审批 = 0,
        /// <summary>
        /// 账务待支付
        /// </summary>
        账务待支付 = 1,
        /// <summary>
        /// 账务已支付
        /// </summary>
        账务已支付 = 2,
        /// <summary>
        /// 销售待确认
        /// </summary>
        销售待确认 = 3,
    }
    #endregion

    #region 垫付类型
    /// <summary>
    /// 垫付类型
    /// </summary>
    public enum TransfiniteType
    {
        /// <summary>
        /// 报价成功=0
        /// </summary>
        报价成功 = 0,
        /// <summary>
        /// 成团=1
        /// </summary>
        成团 = 1,
        /// <summary>
        /// 报名=2
        /// </summary>
        报名 = 2
    }
    #endregion

    #region 报名，报价成功，成团超限类型
    /// <summary>
    /// 报名，报价成功，成团超限类型
    /// </summary>
    public enum OverrunType
    {
        /// <summary>
        /// 客户单位超限=0
        /// </summary>
        客户单位超限 = 0,
        /// <summary>
        /// 客户单位超时=1
        /// </summary>
        客户单位超时 = 1,
        /// <summary>
        /// 销售员超限=2
        /// </summary>
        销售员超限 = 2
    }
    #endregion

    #region 超限审批状态
    /// <summary>
    /// 超限审批状态
    /// </summary>
    public enum TransfiniteStatus
    {
        /// <summary>
        /// 未审批=0
        /// </summary>
        未审批=0,
        /// <summary>
        /// 通过=1
        /// </summary>
        通过=1,
        /// <summary>
        /// 未通过=2
        /// </summary>
        未通过=2
    }
    #endregion

    #region 代收状态
    /// <summary>
    /// 代收状态
    /// </summary>
    public enum DaiShouStatus
    {
        /// <summary>
        /// 未审批
        /// </summary>
        未审批=0,
        /// <summary>
        /// 已通过
        /// </summary>
        已通过,
        /// <summary>
        /// 未通过
        /// </summary>
        未通过
    }
    #endregion

    #region 收款登记类型
    /// <summary>
    /// 收款登记类型
    /// </summary>
    public enum ShouKuanType
    {
        /// <summary>
        /// 用户登记
        /// </summary>
        用户登记 = 0,
        /// <summary>
        /// 导游实收
        /// </summary>
        导游实收 = 1,
        /// <summary>
        /// 供应商代收
        /// </summary>
        供应商代收 = 2
    }
    #endregion

    #region 财务登记类型
    /// <summary>
    /// 财务登记类型
    /// </summary>
    public enum CaiWuDengJi
    {
        /// <summary>
        /// 未选择=0
        /// </summary>
        请选择=0,
        /// <summary>
        /// 收=0
        /// </summary>
        收=1,
        /// <summary>
        /// 付=1
        /// </summary>
        付=2
    }
    #endregion
}
