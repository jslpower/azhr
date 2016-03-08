using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 会议管理接口
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public interface IMeeting
    {
        #region  成员方法
        /// <summary>
        /// 判断会议管理编号是否存在
        /// </summary>
        /// <param name="Number">会议管理编号</param>
        /// <param name="MeetingId">会议管理Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsNumber(string Number, string MeetingId, string CompanyId);

        /// <summary>
        /// 增加一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        bool AddGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model);

        /// <summary>
        /// 更新一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        bool UpdateGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model);

        /// <summary>
        /// 获得会议管理实体
        /// </summary>
        /// <param name="MeetingId">会议管理ID</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovMeeting GetGovMeetingModel(string MeetingId);

        /// <summary>
        /// 获得会议管理信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">会议查询实体</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovMeeting> GetGovMeetingList(string CompanyId, EyouSoft.Model.GovStructure.MSearchMeeting MSearchMeeting, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据会议管理ID删除
        /// </summary>
        /// <param name="MeetingIds">会议管理ID</param>
        /// <returns></returns>
        bool DeleteGovMeeting(params string[] MeetingIds);

        #endregion
    }
}
