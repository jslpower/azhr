using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 投诉管理
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ICrmComplaint
    {
        /// <summary>
        /// 添加投诉管理Model
        /// </summary>
        /// <param name="model">投诉管理Model</param>
        /// <returns></returns>
        int AddCrmComplaintModel(Model.CrmStructure.MCrmComplaint model);


        /// <summary>
        /// 获得投诉管理Model
        /// </summary>
        /// <param name="ComplaintId">投诉管理编号</param>
        /// <returns></returns>
        Model.CrmStructure.MCrmComplaint GetCrmComplaintModel(string ComplaintId);



        /// <summary>
        /// 获得投诉管理显示列表页面上的数据
        /// </summary>
        /// <param name="model">投诉管理列表页面搜索Model</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MComplaintsListModel> GetVisitShowModel(Model.CrmStructure.MComplaintsSearchModel model, int pageIndex, int pageSize, ref int recordCount);


        /// <summary>
        /// 获得投诉管理每日汇总表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MCrmDayTotalModel> GetComplaintDayTotalList(string CompanyId);

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <param name="complaintsId">投诉编号</param>
        /// <param name="handleName">处理人</param>
        /// <param name="handleTime">处理时间</param>
        /// <param name="handleOpinion">处理意见</param>
        /// <param name="isHandle">是否处理</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetComplaintDeal(string complaintsId, string handleName, DateTime handleTime, string handleOpinion,bool isHandle);
    }
}
