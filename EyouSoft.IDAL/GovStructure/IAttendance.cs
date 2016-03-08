using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 考勤接口
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public interface IAttendance
    {
        #region  成员方法
        /// <summary>
        /// 是否已登记考勤(准点 迟到 早退 旷工)
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">职员编号</param>
        /// <param name="AttendanceTime">考勤时间</param>
        /// <param name="AttendanceType">考情类型</param>
        /// <param name="AttendanceId">考勤id，新增时AttendanceId=""</param>
        /// <returns></returns>
        bool ExistsNum(string CompanyId, string StaffId, DateTime AttendanceTime, EyouSoft.Model.EnumType.GovStructure.AttendanceType AttendanceType, string AttendanceId);

        /// <summary>
        /// 批量登记考勤
        /// </summary>
        /// <param name="list">考勤model列表</param>
        /// <returns></returns>
        bool AddAllGovAttendanceList(IList<EyouSoft.Model.GovStructure.MGovAttendance> list);

        /// <summary>
        /// 更新考勤
        /// </summary>
        /// <param name="StaffId">员工编号</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="list">考勤model列表</param>
        /// <returns></returns>
        bool UpdateGovAttendance(string StaffId, string CompanyId, IList<EyouSoft.Model.GovStructure.MGovAttendance> list);

        /// <summary>
        /// 根据考勤编号获取单个考勤实体
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovAttendance GetGovAttendanceModel(string AttendanceId, string CompanyId);

        /// <summary>
        /// 获取某年月的考勤概况
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns>考勤概况实体</returns>
        EyouSoft.Model.GovStructure.MAttendanceAbout GetAttendanceAbout(string CompanyId, string StaffId, int Year, int Month);

        /// <summary>
        /// 按考勤时间获取单个员工考勤信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="AttendanceTime">考勤的时间</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovAttendance> GetList(string CompanyId, string StaffId, DateTime AttendanceTime);

        /// <summary>
        /// 获取考勤管理集合信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordCount"></param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FileNumber">员工号(为空时不作条件)</param>
        /// <param name="StaffName">员工名字(为空时不作条件)</param>
        /// <param name="DepartIds">部门ID集合(如：1,2,3)</param>
        /// <param name="Department">部门ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MAttendanceAbout> GetList(string FileNumber, string StaffName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 按条件获取员工考勤列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovAttendanceList> GetList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo);

        /// <summary>
        /// 按部门获取考勤汇总信息集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment> GetAttendanceByDepartmentList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo);

        /// <summary>
        /// 根据考勤ID删除
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <returns></returns>
        bool DeleteGovAttendance(string AttendanceId);

        #endregion
    }
}
