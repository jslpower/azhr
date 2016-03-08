using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 会议管理BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BMeeting
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IMeeting dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IMeeting>();

        #region  成员方法
        /// <summary>
        /// 判断会议管理编号是否存在
        /// </summary>
        /// <param name="Number">会议管理编号</param>
        /// <param name="MeetingId">会议管理Id,新增Id=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsNumber(string Number, string MeetingId, string CompanyId)
        {
            if (!string.IsNullOrEmpty(Number) && MeetingId != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsNumber(Number, MeetingId, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 增加一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        public bool AddGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyID) && !string.IsNullOrEmpty(model.Number)
                && !string.IsNullOrEmpty(model.Theme) && model.StartTime != null && model.EndTime != null && !string.IsNullOrEmpty(model.Venue) && !string.IsNullOrEmpty(model.OperatorId))
            {
                model.MeetingId = Guid.NewGuid().ToString();
                if (model.MGovMeetingStaff != null && model.MGovMeetingStaff.Count > 0)
                {
                    for (int i = 0; i < model.MGovMeetingStaff.Count; i++)
                    {
                        model.MGovMeetingStaff[i].MeetingId = model.MeetingId;
                    }
                }
                bool result = dal.AddGovMeeting(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条会议管理：编号为：" + model.MeetingId);
                }
                return result;
            }
            return false;
        } 

        /// <summary>
        /// 更新一条会议管理
        /// </summary>
        /// <param name="model">会议管理model</param>
        /// <returns></returns>
        public bool UpdateGovMeeting(EyouSoft.Model.GovStructure.MGovMeeting model)
        {
            if (model != null && !string.IsNullOrEmpty(model.MeetingId) && !string.IsNullOrEmpty(model.CompanyID) && !string.IsNullOrEmpty(model.Number) && model.Category != null
                && !string.IsNullOrEmpty(model.Theme) && model.StartTime != null && model.EndTime != null && !string.IsNullOrEmpty(model.Venue) && !string.IsNullOrEmpty(model.OperatorId))
            {
                if (model.MGovMeetingStaff != null && model.MGovMeetingStaff.Count > 0)
                {
                    for (int i = 0; i < model.MGovMeetingStaff.Count; i++)
                    {
                        model.MGovMeetingStaff[i].MeetingId = model.MeetingId;
                    }
                }
                bool result = dal.UpdateGovMeeting(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条会议管理：编号为：" + model.MeetingId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得会议管理实体
        /// </summary>
        /// <param name="MeetingId">会议管理ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovMeeting GetGovMeetingModel(string MeetingId)
        {
            EyouSoft.Model.GovStructure.MGovMeeting model=null;
            if (!string.IsNullOrEmpty(MeetingId))
            {
                model = new EyouSoft.Model.GovStructure.MGovMeeting();
                model = dal.GetGovMeetingModel(MeetingId);
            }
            return model;
        }

        /// <summary>
        /// 获得会议管理信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchInfo">会议查询实体</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovMeeting> GetGovMeetingList(string CompanyId, EyouSoft.Model.GovStructure.MSearchMeeting MSearchMeeting, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovMeeting> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovMeeting>();
                return dal.GetGovMeetingList(CompanyId, MSearchMeeting, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据会议管理ID删除
        /// </summary>
        /// <param name="MeetingIds">会议管理ID</param>
        /// <returns></returns>
        public bool DeleteGovMeeting(params string[] MeetingIds)
        {
            if (MeetingIds.Length > 0)
            {
                bool result = dal.DeleteGovMeeting(MeetingIds);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < MeetingIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", MeetingIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除会议管理：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }

        #endregion
    }
}
