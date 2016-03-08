using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//文件管理 2012-03-06 邵权江
namespace EyouSoft.Model.GovStructure
{
    #region 文件管理业务实体
    /// <summary>
    /// 文件管理业务实体
    /// </summary>
    public class MGovDocuments
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string DocumentsId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 文件字号
        /// </summary>
        public string FontSize { get; set; }
        /// <summary>
        /// 发布单位
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.FileType FileType { get; set; }
        /// <summary>
        /// 经办人ID
        /// </summary>
        public string AttnId { get; set; }
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
        public IList<MGovDocumentsApprove> GovDocumentsApproveList { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.ApprovalStatus ApproveState { get; set; }
        /// <summary>
        /// 附件实体
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
        /// <summary>
        /// 经办人人姓名
        /// </summary>
        public string AttnName { get; set; }
    }
    #endregion

    #region 审批人表业务实体
    /// <summary>
    /// 审批人表业务实体
    /// </summary>
    public class MGovDocumentsApprove
    {
        /// <summary>
        /// 文件id
        /// </summary>
        public string DocumentsId { get; set; }
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
