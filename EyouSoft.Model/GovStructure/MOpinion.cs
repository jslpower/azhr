using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 意见建议实体
    /// <summary>
    /// 意见建议实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovOpinion
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovOpinion() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string OpinionId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public string ProcessOpinion { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
        /// <summary>
        /// 是否公开(1是/0否)
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Submit { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
        /// <summary>
        /// 状态1：未处理,2：已处理
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 接收人员实体集合
        /// </summary>
        public IList<Model.GovStructure.MGovOpinionUser> MGovOpinionUserList { get; set; }
        /// <summary>
        /// 附件实体集合
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
    }
    #endregion

    #region 接收人员实体
    /// <summary>
    /// 接收人员实体
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovOpinionUser
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovOpinionUser() { }
        /// <summary>
        /// 意见编号
        /// </summary>
        public string OpinionId { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string User { get; set; }
    }
    #endregion

    #region 意见建议查询实体
    /// <summary>
    /// 意见建议查询实体
    /// 2011-09-25 邵权江 创建
    /// </summary>
    public class MSearchOpinion
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Submit { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 接受人
        /// </summary>
        public string OpinionUserId { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
        /// <summary>
        /// 状态1:未处理,2：已处理
        /// </summary>
        public string Status { get; set; }
    }
    #endregion
}
