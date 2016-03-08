using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 考勤管理
    /// <summary>
    /// 考勤管理信息实体
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovAttendance
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovAttendance() { }
        /// <summary>
        /// 考勤ID
        /// </summary>
        public string AttendanceId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string StaffId { get; set; }
        /// <summary>
        /// 考勤时间
        /// </summary>
        public DateTime? AttendanceTime { get; set; }
        /// <summary>
        /// 类型(全勤，迟到，早退，旷工，请假，加班，出差，休假，停职)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.AttendanceType AttendanceType { get; set; }
        /// <summary>
        /// 事由
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 天数或小时数
        /// </summary>
        public decimal TimeCount { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? IssueTime { get; set; }
    }
    #endregion

    #region 考勤列表实体（当月考勤概况）
    /// <summary>
    /// 考勤列表实体（当月考勤概况）
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MAttendanceAbout
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        public string StaffId { set; get; }
        /// <summary>
        /// 员工号(人事档案的档案编号)
        /// </summary>
        public string FileNumber { set; get; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string StaffName { set; get; }
        /// <summary>
        /// 员工部门id
        /// </summary>
        public int StaffDeptId { get; set; }
        /// <summary>
        /// 员工部门部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 全勤（单位：天）类型(全勤，迟到，早退，旷工，请假，加班，出差，休假，停职)
        /// </summary>
        public int Punctuality { set; get; }
        /// <summary>
        /// 迟到（单位：天）
        /// </summary>
        public int Late { set; get; }
        /// <summary>
        /// 早退（单位：天）
        /// </summary>
        public int LeaveEarly { set; get; }
        /// <summary>
        /// 旷工（单位：天）
        /// </summary>
        public int Absenteeism { set; get; }
        /// <summary>
        /// 请假（单位：天）
        /// </summary>
        public decimal Vacation { set; get; }
        /// <summary>
        /// 加班（单位：小时）
        /// </summary>
        public decimal OverTime { set; get; }
        /// <summary>
        /// 出差（单位：天）
        /// </summary>
        public decimal Travel { set; get; }
        /// <summary>
        /// 休假（单位：天）
        /// </summary>
        public decimal Rest { set; get; }
        /// <summary>
        /// 停职（单位：天）
        /// </summary>
        public decimal Suspension { set; get; }
    }
    #endregion

    #region 员工考勤列表
    /// <summary>
    /// 员工考勤列表
    /// 2011-09-19 邵权江 创建
    /// </summary>
    public class MGovAttendanceList : MGovFile
    {
        /// <summary>
        /// 考情信息
        /// </summary>
        public IList<EyouSoft.Model.GovStructure.MGovAttendance> AttendanceList { set; get; }
    }
    #endregion

    #region 考勤汇总信息实体
    /// <summary>
    /// 考勤汇总信息实体
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovAttendanceByDepartment
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartId { set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { set; get; }
        /// <summary>
        /// 人员信息
        /// </summary>
        public IList<EyouSoft.Model.GovStructure.MGovAttendanceList> PersonList { set; get; }
    }
    #endregion

    #region 考勤查询实体
    /// <summary>
    /// 考勤查询实体
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MSearchInfo
    {
        /// <summary>
        /// 年份(为null或小于等于0时，取当前年份值)
        /// </summary>
        public int? Year { set; get; }
        /// <summary>
        /// 月份(为null或小于等于0时，取当前月份值)
        /// </summary>
        public int? Month { set; get; }
        /// <summary>
        /// 部门Id(为null或小于等于0时，不作条件)
        /// </summary>
        public int? DepartId { set; get; }
        /// <summary>
        /// 部门Id集合(如：1,2,3)
        /// </summary>
        public string DepartIds { set; get; }
        /// <summary>
        /// 部门名称(为null或""时，不作条件)
        /// </summary>
        public string DepartName { set; get; }
        /// <summary>
        /// 员工编号(人事档案的ID)
        /// (为null或""时，不作条件)
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 员工号(人事档案的档案编号)
        /// (为null或""时，不作条件)
        /// </summary>
        public string FileNumber { set; get; }
        /// <summary>
        /// 员工姓名(为null或""时，不作条件)
        /// </summary>
        public string StaffName { set; get; }
    }
    #endregion
}
