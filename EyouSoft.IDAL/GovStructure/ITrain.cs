using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 培训管理接口
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public interface ITrain
    {
        #region  成员方法
        /// <summary>
        /// 增加一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <returns></returns>
        bool AddGovTrain(EyouSoft.Model.GovStructure.MGovTrain model);

        /// <summary>
        /// 更新一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateGovTrain(EyouSoft.Model.GovStructure.MGovTrain model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得培训实体
        /// </summary>
        /// <param name="TrainId">培训ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovTrain GetGovTrainModel(string TrainId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 根据培训时间条件获得培训信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="Title">主题</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovTrain> GetGovTrainList(string CompanyId, DateTime? TimeBegin, DateTime? TimeEnd, string Title, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据培训ID删除
        /// </summary>
        /// <param name="ItemType">附件类型</param>
        /// <param name="TrainIds">培训ID</param>
        /// <returns></returns>
        bool DeleteGovTrain(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] TrainIds);

        #endregion
    }
}
