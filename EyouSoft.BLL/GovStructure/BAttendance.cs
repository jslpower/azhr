using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 考勤管理BLL
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class BAttendance
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IAttendance dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IAttendance>();

        #region  成员方法
        /// <summary>
        /// 是否已登记考勤
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">职员编号</param>
        /// <param name="AttendanceTime">考勤时间</param>
        /// <param name="AttendanceType">考情类型</param>
        /// <param name="AttendanceId">考勤id，新增时AttendanceId=""</param>
        /// <returns></returns>
        public bool ExistsNum(string CompanyId, string StaffId, DateTime AttendanceTime, EyouSoft.Model.EnumType.GovStructure.AttendanceType AttendanceType, string AttendanceId)
        {
            if (!string.IsNullOrEmpty(StaffId) && !string.IsNullOrEmpty(CompanyId) && AttendanceTime != null && AttendanceId != null)
            {
                return dal.ExistsNum(CompanyId, StaffId, AttendanceTime, AttendanceType, AttendanceId);
            }
            return false;
        }

        /// <summary>
        /// 批量登记考勤
        /// </summary>
        /// <param name="list">考勤model列表</param>
        /// <returns></returns>
        public bool AddAllGovAttendanceList(IList<EyouSoft.Model.GovStructure.MGovAttendance> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null && !string.IsNullOrEmpty(list[i].CompanyId) && !string.IsNullOrEmpty(list[i].StaffId) && list[i].AttendanceTime != null)
                    {
                        list[i].AttendanceId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                bool result = dal.AddAllGovAttendanceList(list);
                if (result)
                {
                    foreach (Model.GovStructure.MGovAttendance model in list)
                    {
                        SysStructure.BSysLogHandle.Insert("登记登记考勤信息：考勤编号为：" + model.AttendanceId);
                    }
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新考勤
        /// </summary>
        /// <param name="StaffId">员工编号</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="list">新考勤model列表</param>
        /// <returns></returns>
        public bool UpdateGovAttendance(string StaffId, string CompanyId, IList<EyouSoft.Model.GovStructure.MGovAttendance> list)
        {
            if (!string.IsNullOrEmpty(StaffId) && !string.IsNullOrEmpty(CompanyId) && list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null && !string.IsNullOrEmpty(list[i].CompanyId) && !string.IsNullOrEmpty(list[i].StaffId) && list[i].AttendanceTime != null)
                    {
                        list[i].AttendanceId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                bool result = dal.UpdateGovAttendance(StaffId, CompanyId, list);
                if (result)
                {
                    foreach (Model.GovStructure.MGovAttendance model in list)
                    {
                        SysStructure.BSysLogHandle.Insert("更新考勤信息：考勤编号为：" + model.AttendanceId);
                    }
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 根据考勤编号获取单个考勤实体
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovAttendance GetGovAttendanceModel(string AttendanceId, string CompanyId)
        {
            EyouSoft.Model.GovStructure.MGovAttendance model = null;
            if (!string.IsNullOrEmpty(AttendanceId) && !string.IsNullOrEmpty(CompanyId))
            {
                model = new EyouSoft.Model.GovStructure.MGovAttendance();
                model = dal.GetGovAttendanceModel(AttendanceId, CompanyId);
            }
            return model;
        }

        /// <summary>
        /// 获取某年月的考勤概况
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns>考勤概况实体</returns>
        public EyouSoft.Model.GovStructure.MAttendanceAbout GetAttendanceAbout(string CompanyId, string StaffId, int Year, int Month)
        {
            EyouSoft.Model.GovStructure.MAttendanceAbout model = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(StaffId) && Year > 0 && Month > 0)
            {
                model = new EyouSoft.Model.GovStructure.MAttendanceAbout();
                return dal.GetAttendanceAbout(CompanyId, StaffId, Year, Month);
            }
            return model;
        }

        /// <summary>
        /// 按考勤时间获取员工考勤信息集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="StaffId">员工编号</param>
        /// <param name="AttendanceTime">考勤的时间</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendance> GetList(string CompanyId, string StaffId, DateTime AttendanceTime)
        {
            IList<EyouSoft.Model.GovStructure.MGovAttendance> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(StaffId) && AttendanceTime != null)
            {
                list = new List<EyouSoft.Model.GovStructure.MGovAttendance>();
                return dal.GetList(CompanyId, StaffId, AttendanceTime);
            }
            return list;
        }

        /// <summary>
        /// 获取考勤管理集合信息
        /// Punctuality准点 = 0,Late迟到=1,LeaveEarly早退=2,Absenteeism旷工=3,Vacation请假=4,OverTime加班=5,Travel出差=6,Rest休假=7,Suspension停职=8
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FileNumber">员工号(为空时不作条件)</param>
        /// <param name="StaffName">员工名字(为空时不作条件)</param>
        /// <param name="DepartIds">部门ID集合(如：1,2,3)</param>
        /// <param name="Department">部门(为空时不作条件)</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MAttendanceAbout> GetList(string FileNumber, string StaffName, string DepartIds, string Department, string CompanyId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MAttendanceAbout> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MAttendanceAbout>();
                return dal.GetList(FileNumber, StaffName, DepartIds, Department, CompanyId, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 按年份，月份获取员工考勤列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendanceList> GetList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo)
        {
            if (!SearchInfo.Year.HasValue || SearchInfo.Year.Value == 0)
            {
                SearchInfo.Year = DateTime.Now.Year;
            }
            if (!SearchInfo.Month.HasValue || SearchInfo.Month.Value == 0)
            {
                SearchInfo.Month = DateTime.Now.Month;
            }
            IList<EyouSoft.Model.GovStructure.MGovAttendanceList> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MGovAttendanceList>();
                list = dal.GetList(CompanyId, SearchInfo);
            }
            return list;
        }

        /// <summary>
        /// 按部门获取考勤汇总信息集合
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">考勤查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment> GetAttendanceByDepartmentList(string CompanyId, EyouSoft.Model.GovStructure.MSearchInfo SearchInfo)
        {
            if (!SearchInfo.Year.HasValue || SearchInfo.Year.Value == 0)
            {
                SearchInfo.Year = DateTime.Now.Year;
            }
            if (!SearchInfo.Month.HasValue || SearchInfo.Month.Value == 0)
            {
                SearchInfo.Month = DateTime.Now.Month;
            }
            IList<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.GovStructure.MGovAttendanceByDepartment>();
                list = dal.GetAttendanceByDepartmentList(CompanyId, SearchInfo);
            }
            return list;
        }

        /// <summary>
        /// 根据考勤ID删除
        /// </summary>
        /// <param name="AttendanceId">考勤ID</param>
        /// <returns></returns>
        public bool DeleteGovAttendance(string AttendanceId)
        {
            if (!string.IsNullOrEmpty(AttendanceId))
            {
                bool result = dal.DeleteGovAttendance(AttendanceId);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("删除一条考勤信息：编号为：" + AttendanceId);
                }
                return result;
            }
            return false;
        }

        #endregion
    }
}
