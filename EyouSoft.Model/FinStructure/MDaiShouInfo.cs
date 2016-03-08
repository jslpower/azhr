//代收相关信息业务实体 汪奇志 2013-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.FinStructure
{
    #region 代收信息业务实体
    /// <summary>
    /// 代收信息业务实体
    /// </summary>
    public class MDaiShouInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDaiShouInfo() { }

        /// <summary>
        /// 代收编号
        /// </summary>
        public string DaiShouId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 计划编号(OUTPUT)
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号(OUTPUT)
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 代收金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 客户单位编号(OUTPUT)
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位名称(OUTPUT)
        /// </summary>
        public string CrmName { get; set; }
        /// <summary>
        /// 计调安排编号
        /// </summary>
        public string AnPaiId { get; set; }
        /// <summary>
        /// 供应商编号(OUTPUT)
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称(OUTPUT)
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 代收备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名(OUTPUT)
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 代收状态
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.DaiShouStatus Status { get; set; }
        /// <summary>
        /// 代收时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 订单销售员姓名
        /// </summary>
        public string OrderSellerName { get; set; }
        /// <summary>
        /// 订单下单人姓名
        /// </summary>
        public string OrderXiaDanRenName { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ShenPiRenName { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ShenPiTime { get; set; }
    }
    #endregion

    #region 代收订单信息业务实体
    /// <summary>
    /// 代收订单信息业务实体
    /// </summary>
    public class MDaiShouOrderInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDaiShouOrderInfo() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string XiaDanRenName { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CrmName { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 合同金额确认状态
        /// </summary>
        public bool IsQueRen { get; set; }
    }
    #endregion

    #region 代收计调安排信息业务实体
    /// <summary>
    /// 代收计调安排信息业务实体
    /// </summary>
    public class MDaiShouJiDiaoAnPaiInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDaiShouJiDiaoAnPaiInfo() { }

        /// <summary>
        /// 计调安排编号
        /// </summary>
        public string AnPaiId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 安排类型
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject AnPaiLeiXing { get; set; } 
    }
    #endregion

    #region 代收信息查询业务实体
    /// <summary>
    /// 代收信息查询业务实体
    /// </summary>
    public class MDaiShouChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDaiShouChaXunInfo() { }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string CrmName { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 代收状态
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.DaiShouStatus? Status { get; set; }

    }
    #endregion

    #region 代收审批信息业务实体
    /// <summary>
    /// 代收审批信息业务实体
    /// </summary>
    public class MDaiShouShenPiInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MDaiShouShenPiInfo() { }

        /// <summary>
        /// 代收编号
        /// </summary>
        public string DaiShouId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 代收状态
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.DaiShouStatus Status { get; set; }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime Time { get; set; }
    }
    #endregion
}
