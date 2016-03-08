using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 文件管理业务接口
    /// 邵权江 2012-03-07
    /// </summary>
    public interface IDocuments
    {
        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model);

        /// <summary>
        /// 修改文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 根据 文件ID 获取文件实体信息
        /// </summary>
        /// <param name="ID">文件ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovDocuments GetGovDocumentsModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 修改审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddGovDocumentsApprove(EyouSoft.Model.GovStructure.MGovDocumentsApprove model);

        /// <summary>
        /// 删除文件信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool DeleteGovDocuments(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids);

        /// <summary>
        /// 获取文件信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FontSize">文件字号</param>
        /// <param name="Company">发布单位</param>
        /// <param name="Title">标题</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.GovStructure.MGovDocuments> GetGovDocumentsList(string CompanyId, string FontSize, string Company, string Title, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);
    }
}
