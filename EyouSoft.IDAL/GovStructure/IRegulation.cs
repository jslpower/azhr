using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 规章制度接口
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public interface IRegulation
    {
        #region  成员方法
        /// <summary>
        /// 判断规章制度编号是否存在
        /// </summary>
        /// <param name="Code">规章制度编码</param>
        /// <param name="RegId">规章制度RegId,新增RegId=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsCode(string Code, string RegId, string CompanyId);

        /// <summary>
        /// 增加一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <returns></returns>
        bool AddGovRegulation(EyouSoft.Model.GovStructure.MGovRegulation model);

        /// <summary>
        /// 更新一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateRegulation(EyouSoft.Model.GovStructure.MGovRegulation model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得规章制度实体
        /// </summary>
        /// <param name="ID">规章制度ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovRegulation GetGovRegulationModel(string RegId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得规章制度信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchModel">查询实体</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovRegulation> GetGovRegulationList(string CompanyId, EyouSoft.Model.GovStructure.MGovRegSearch SearchModel, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="Ids">ID</param>
        /// 附件类型
        /// <returns></returns>
        bool DeleteGovRegulation(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids);

        #endregion
    }
}
