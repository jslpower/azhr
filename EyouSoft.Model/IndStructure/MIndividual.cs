using System;
using System.Collections.Generic;

namespace EyouSoft.Model.IndStructure
{
    using EyouSoft.Model.EnumType.IndStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.TourStructure;

    using MTourPlanStatus = EyouSoft.Model.HTourStructure.MTourPlanStatus;

    #region 备忘录
    /// <summary>
    /// 备忘录
    /// 创建者：郑知远
    /// 创建时间：2011/09/01
    /// </summary>
    [Serializable]
    public class MMemo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MMemo() { }

        /// <summary>
        /// 备忘录编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string MemoTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MemoText { get; set; }

        /// <summary>
        /// 备忘时间
        /// </summary>
        public DateTime MemoTime { get; set; }

        /// <summary>
        /// 紧急度
        /// </summary>
        public MemoUrgent UrgentType { get; set; }

        /// <summary>
        /// 备忘录状态
        /// </summary>
        public MemoState MemoState { get; set; }

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 工作汇报搜索实体
    /// <summary>
    /// 工作汇报搜索实体
    /// </summary>
    [Serializable]
    public class MWorkReportSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MWorkReportSearch() { }

        /// <summary>
        /// 汇报标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 汇报人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 汇报时间(开始)
        /// </summary>
        public DateTime? IssueTimeS { get; set; }

        /// <summary>
        /// 汇报时间(结束)
        /// </summary>
        public DateTime? IssueTimeE { get; set; }
    }
    #endregion

    #region 工作汇报
    /// <summary>
    /// 工作汇报
    /// </summary>
    [Serializable]
    public class MWorkReport
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MWorkReport() { }

        /// <summary>
        /// 报告编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 汇报标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 汇报内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// 汇报部门编号
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 汇报部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 汇报人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 汇报人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.EnumType.IndStructure.Status Status { get; set; }

        /// <summary>
        /// 汇报时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 审核人列表
        /// </summary>
        public IList<MWorkReportCheck> list { get; set; }
    }
    #endregion

    #region 工作汇报审核人实体
    /// <summary>
    /// 工作汇报审核人实体
    /// </summary>
    public class MWorkReportCheck
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 汇报编号
        /// </summary>
        public int WorkId { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.EnumType.IndStructure.Status Status { get; set; }
    }
    #endregion

    #region 工作计划
    /// <summary>
    /// 工作计划
    /// </summary>
    [Serializable]
    public class MWorkPlan
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MWorkPlan() { }

        /// <summary>
        /// 工作计划编号
        /// </summary>
        public int WorkPlanId { get; set; }

        /// <summary>
        /// 系统公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 计划部门编号
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 计划部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 汇报人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 汇报人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 附件上传
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 计划说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime ActualTime { get; set; }


        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 填写时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 接受人明细
        /// </summary>
        public IList<MWorkPlanCheck> list { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.EnumType.IndStructure.Status Status { get; set; }

    }
    #endregion

    #region 工作计划审核人实体
    /// <summary>
    /// 工作计划审核人实体
    /// </summary>
    public class MWorkPlanCheck
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 工作计划编号
        /// </summary>
        public int WorkPlanId { get; set; }
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string ApproverId { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Approver { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApproveTime{ get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.EnumType.IndStructure.Status Status { get; set; }
    }
    #endregion

    #region 工作计划搜索实体
    /// <summary>
    /// 工作计划基础实体
    /// </summary>
    [Serializable]
    public class MWorkPlanSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MWorkPlanSearch() { }

        /// <summary>
        /// 计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 计划人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 计划时间(开始)
        /// </summary>
        public DateTime? IssueTimeS { get; set; }

        /// <summary>
        /// 计划时间(结束)
        /// </summary>
        public DateTime? IssueTimeE { get; set; }
    }
    #endregion

    #region 提醒中心共同信息
    /// <summary>
    /// 提醒中心共同信息
    /// </summary>
    [Serializable]
    public class MRemindBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRemindBase() { }

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
        /// 计划所有订单剩余人数
        /// </summary>
        public int PeopleNumberLast { get { return PlanPeopleNumber - RealPeopleNumber - LeavePeopleNumber; } }

        /// <summary>
        /// 客户单位
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 销售员编号
        /// </summary>
        public string SellerId { get; set; }

        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string SellerName { get; set; }

    }
    #endregion

    #region 订单提醒
    /// <summary>
    /// 订单提醒
    /// </summary>
    [Serializable]
    public class MOrderRemind : MRemindBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOrderRemind() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 成人数
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// 儿童数
        /// </summary>
        public int Childs { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 订单人数
        /// </summary>
        public int PeopleNum { get { return this.Adults + this.Childs; } }

        /// <summary>
        /// 团队类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.TourType TourType { get; set; }
    }
    #endregion

    #region 计调提醒
    /// <summary>
    /// 计调提醒
    /// </summary>
    [Serializable]
    public class MPlanRemind : MRemindBase
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MPlanRemind() { }

        /// <summary>
        /// 计划项目安排落实实体
        /// </summary>
        public MTourPlanStatus TourPlanStatus { get; set; }

        /// <summary>
        /// 计划类型
        /// </summary>
        public TourType TourType { get; set; }
    }
    #endregion

    #region 收款提醒
    /// <summary>
    /// 收款提醒
    /// </summary>
    [Serializable]
    public class MReceivablesRemind //: EyouSoft.Model.FinStructure.MCustomerWarning
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MReceivablesRemind() { }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ConfirmMoney { get; set; }
        /// <summary>
        /// 欠款金额
        /// </summary>
        public decimal Arrear { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 欠款类型（超限/超时）
        /// </summary>
        public ReceivableType ReceivableType
        {
            get { return ReceivableType.超限; }
            //get
            //{
            //    if (Transfinite > 0 && DeadDay > 0)
            //    {
            //        return ReceivableType.超限且超期;
            //    }
            //    if (Transfinite > 0)
            //    {
            //        return ReceivableType.超限;
            //    }
            //    return ReceivableType.超时;
            //}
        }
    }
    #endregion

    #region 询价提醒
    /// <summary>
    /// 询价提醒
    /// </summary>
    public class MInquiryRemind
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
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 询价单位
        /// </summary>
        public string BuyCompanyName { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int PersonNum { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string SellerName { get; set; }
        /// <summary>
        /// 报价员
        /// </summary>
        public string Operator { get; set; }

        ///// <summary>
        ///// 模块类型
        ///// </summary>
        //public EyouSoft.Model.EnumType.TourStructure.ModuleType QuoteType { get; set; }
    }
    #endregion

    #region 计划变更提醒
    /// <summary>
    /// 计划变更提醒
    /// </summary>
    [Serializable]
    public class MTourChangeRemind : MTourPlanChange
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MTourChangeRemind() { }
    }
    #endregion

    #region 订单变更提醒
    /// <summary>
    /// 订单变更提醒
    /// </summary>
    [Serializable]
    public class MOrderChangeRemind : MTourOrderChange
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MOrderChangeRemind() { }
    }
    #endregion

    //#region 酒店预控到期提醒
    ///// <summary>
    ///// 酒店预控到期提醒
    ///// </summary>
    //[Serializable]
    //public class MHotelcontrolRemindHotel : EyouSoft.Model.SourceStructure.MSourceSueHotel
    //{
    //    public MHotelcontrolRemindHotel() { }

    //}
    //#endregion

    //#region 车辆预控到期提醒
    ///// <summary>
    ///// 车辆预控到期提醒
    ///// </summary>
    //[Serializable]
    //public class MCarcontrolRemindVehicle : EyouSoft.Model.SourceStructure.MSourceSueCar
    //{
    //    public MCarcontrolRemindVehicle() { }
    //}
    //#endregion

    //#region 游船预控到期提醒
    ///// <summary>
    ///// 游船预控到期提醒
    ///// </summary>
    //[Serializable]
    //public class MShipcontrolRemindCruise : EyouSoft.Model.SourceStructure.MSourceSueShip
    //{
    //    public MShipcontrolRemindCruise() { }
    //}
    //#endregion

    #region 劳动合同到期提醒
    /// <summary>
    /// 劳动合同到期提醒
    /// </summary>
    [Serializable]
    public class MLaborContractExpireRemind
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLaborContractExpireRemind() { }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string FileNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime MaturityTime { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }
    }
    #endregion

    #region 供应商合同到期提醒
    /// <summary>
    /// 供应商合同到期提醒
    /// </summary>
    [Serializable]
    public class MSourceContractExpireRemind
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceContractExpireRemind() { }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime MaturityTime { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }
    }
    #endregion

    #region 公司合同到期提醒
    /// <summary>
    /// 公司合同到期提醒
    /// </summary>
    [Serializable]
    public class MCompanyContractExpireRemind
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCompanyContractExpireRemind() { }
        /// <summary>
        /// 合同单位
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 合同类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime MaturityTime { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }
    }
    #endregion

    #region 公告提醒
    /// <summary>
    /// 公告提醒
    /// </summary>
    [Serializable]
    public class MNoticeRemind : EyouSoft.Model.GovStructure.MGovNotice
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MNoticeRemind() { }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
    }
    #endregion
}
