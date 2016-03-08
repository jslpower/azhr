using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 会议管理
    /// <summary>
    /// 会议管理
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovMeeting
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovMeeting() { }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string MeetingId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 会议编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 会议类别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Category Category { get; set; }
        /// <summary>
        /// 会议主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 参会人员
        /// </summary>
        public string MeetingStaff { get; set; }
        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 会议地点
        /// </summary>
        public string Venue { get; set; }
        /// <summary>
        /// 会议纪要
        /// </summary>
        public string Minutes { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 参会人员实体集合
        /// </summary>
        public IList<Model.GovStructure.MGovMeetingStaff> MGovMeetingStaff { get; set; }
    }
    #endregion

    #region 参会人员
    /// <summary>
    /// 参会人员
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovMeetingStaff
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovMeetingStaff() { }
        /// <summary>
        /// 会议ID
        /// </summary>
        public string MeetingId { get; set; }
        /// <summary>
        /// 参会人员ID
        /// </summary>
        public string AcceptTypeID { get; set; }
        /// <summary>
        /// 参会人员
        /// </summary>
        public string AcceptType { get; set; }
    }
    #endregion

    #region 会议管理查询实体
    /// <summary>
    /// 会议管理查询实体
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MSearchMeeting
    {
        /// <summary>
        /// 会议编号
        /// </summary>
        public string Number { set; get; }
        /// <summary>
        /// 会议主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
    #endregion
}
