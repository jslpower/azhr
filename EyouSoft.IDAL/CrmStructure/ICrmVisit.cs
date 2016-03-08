using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 团队回访
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ICrmVisit
    {
        /// <summary>
        /// 添加团队回访Model
        /// </summary>
        /// <param name="model">团队回访Model</param>
        /// <returns></returns>
        int AddCrmVisitModel(Model.CrmStructure.MCrmVisit model);


        /// <summary>
        /// 获得显示在团队回访页面上的列表数据
        /// </summary>
        /// <param name="model">团队回访列表显示Model</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MVisitListModel> GetVisitShowModel(Model.CrmStructure.MVisitListModel model,DateTime? startDate,DateTime? endDate, int pageIndex, int pageSize, ref int recordCount);


        /// <summary>
        /// 获得团队回访每日汇总表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MDayTotalModel> GetDayTotalModelList(string CompanyId, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, ref int recordCount);
        /// <summary>
        /// 获取回访明细
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="startTime">时间</param>
        /// <returns>回访明细集合</returns>
        IList<Model.CrmStructure.MCrmVisit> GetCrmVisit(int pageIndex, int pageSize, ref int recordCount,string companyId,string tourId, DateTime? startTime,DateTime? endTime);

        /// <summary>
        /// 获得团队回访Model
        /// </summary>
        /// <param name="visitId">团队回访编号</param>
        /// <returns></returns>
        Model.CrmStructure.MCrmVisit GetVisitModel(string tourId,string visitId);

        /// <summary>
        /// 修改团队回访Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateCrmVisitModel(Model.CrmStructure.MCrmVisit model);

    }
}
