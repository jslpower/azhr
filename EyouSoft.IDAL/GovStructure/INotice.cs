using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 通知公告接口
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public interface INotice
    {
        #region  成员方法
        /// <summary>
        /// 增加一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        bool AddGovNotice(Model.GovStructure.MGovNotice model);

        /// <summary>
        /// 更新一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateGovNotice(Model.GovStructure.MGovNotice model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得公告通知实体
        /// </summary>
        /// <param name="NoticeId">公告通知ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovNotice GetGovNoticeModel(string NoticeId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Title">标题</param>
        /// <param name="OperatorId">发布人ID</param>
        /// <param name="Operator">发布人</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId, string Title, string OperatorId, string Operator, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据接收类型获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">接收类型</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId, EyouSoft.Model.EnumType.GovStructure.ItemType Type, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 增加一条浏览人信息
        /// </summary>
        /// <param name="model">浏览人model</param>
        bool AddGovNoticeBrowse(Model.GovStructure.MGovNoticeBrowse model);

        /// <summary>
        /// 获得公告通知浏览人信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="NoticeId">公告通知编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovNoticeBrowse> GetGovNoticeBrowseList(string CompanyId, string NoticeId, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据公告通知编号删除
        /// </summary>
        /// <param name="NoticeIds">公告通知ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool DeleteGovNotice(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] NoticeIds);

        #endregion  成员方法
    }
}
