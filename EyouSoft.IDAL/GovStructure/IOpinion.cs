using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 意见建议接口
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public interface IOpinion
    {
        #region  成员方法
        /// <summary>
        /// 增加一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        bool AddGovOpinion(Model.GovStructure.MGovOpinion model);

        /// <summary>
        /// 更新一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateGovOpinion(Model.GovStructure.MGovOpinion model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得意见建议实体
        /// </summary>
        /// <param name="OpinionId">意见建议ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovOpinion GetGovOpinionModel(string OpinionId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 根据条件意见建议信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="model">查询参数类</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovOpinion> GetGovOpinionList(string CompanyId, Model.GovStructure.MSearchOpinion MSearchOpinion, int PageSize, int PageIndex, ref int RecordCount);
        
        /// <summary>
        /// 根据意见建议编号删除
        /// </summary>
        /// <param name="OpinionIds">意见建议ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool DeleteGovOpinion(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] OpinionIds);

        #endregion  成员方法
    }
}
