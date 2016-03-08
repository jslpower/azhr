using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 培训管理
    /// <summary>
    /// 培训管理
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovTrain
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovTrain() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string TrainId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime? StateTime { get; set; }
        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 培训主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 培训人
        /// </summary>
        public string TrainPeople { get; set; }
        /// <summary>
        /// 培训参与人员
        /// </summary>
        public string JoinPeople { get; set; }
        /// <summary>
        /// 培训地点
        /// </summary>
        public string TrainingLocations { get; set; }
        /// <summary>
        /// 培训内容
        /// </summary>
        public string Training { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 附件实体集合
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
    }
    #endregion

    #region 参与人员
    /// <summary>
    /// 参与人员
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovTrainStaff
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovTrainStaff() { }
        /// <summary>
        /// 培训ID
        /// </summary>
        public string TrainId { get; set; }
        /// <summary>
        /// 培训人员ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 培训人员
        /// </summary>
        public string User { get; set; }
    }
    #endregion
}
