using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//人事变动/离职申请 2011-09-26 邵权江
namespace EyouSoft.Model.GovStructure
{
    #region 人事变动/离职申请业务实体
    /// <summary>
    /// 人事变动/离职申请业务实体
    /// </summary>
    public class MGovFilePersonnel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 档案ID
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 希望离职时间
        /// </summary>
        public DateTime? ApplicationTime { get; set; }
        /// <summary>
        /// 交接内容
        /// </summary>
        public string TransitionContent { get; set; }
        /// <summary>
        /// 交接状态(未完成/已完成)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.TransitionState TransitionState { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 审批人业务实体集合
        /// </summary>
        public IList<MGovPersonnelApprove> GovPersonnelApproveList { get; set; }
        /// <summary>
        /// 离职状态(在职/离职)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffStatus StaffStatus { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.ApprovalStatus ApproveState { get; set; }
        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime? DepartureTime { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string FileName { get; set; }
    }
    #endregion

    #region 离职审批人表业务实体
    /// <summary>
    /// 离职审批人表业务实体
    /// </summary>
    public class MGovPersonnelApprove
    {
        /// <summary>
        /// 人事变动id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public string ApproveID { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApproveName { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApproveTime { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApprovalViews { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.ApprovalStatus ApproveState { get; set; }
        
    }
    #endregion
}
