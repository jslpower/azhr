using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 通知公告BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BNotice
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.INotice dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.INotice>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "通知公告");

        #region  成员方法
        /// <summary>
        /// 增加一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        public bool AddGovNotice(Model.GovStructure.MGovNotice model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                model.NoticeId = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.NoticeId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                if (model.MGovNoticeReceiverList != null && model.MGovNoticeReceiverList.Count > 0)
                {
                    for (int i = 0; i < model.MGovNoticeReceiverList.Count; i++)
                    {
                        model.MGovNoticeReceiverList[i].NoticeId = model.NoticeId;
                    }
                }
                bool result =dal.AddGovNotice(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条公告通知信息：编号为：" + model.NoticeId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新一条公告通知信息
        /// </summary>
        /// <param name="model">公告通知model</param>
        /// <returns></returns>
        public bool UpdateGovNotice(Model.GovStructure.MGovNotice model)
        {
            if (model != null && !string.IsNullOrEmpty(model.NoticeId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.NoticeId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                if (model.MGovNoticeReceiverList != null && model.MGovNoticeReceiverList.Count > 0)
                {
                    for (int i = 0; i < model.MGovNoticeReceiverList.Count; i++)
                    {
                        model.MGovNoticeReceiverList[i].NoticeId = model.NoticeId;
                    }
                }
                bool result = dal.UpdateGovNotice(model, ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条公告通知信息：编号为：" + model.NoticeId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得公告通知实体
        /// </summary>
        /// <param name="NoticeId">公告通知ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovNotice GetGovNoticeModel(string NoticeId)
        {
            EyouSoft.Model.GovStructure.MGovNotice model = null;
            if (!string.IsNullOrEmpty(NoticeId))
            {
                model = new EyouSoft.Model.GovStructure.MGovNotice();
                return dal.GetGovNoticeModel(NoticeId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Title">标题</param>
        /// <param name="OperatorId">发布人ID</param>
        /// <param name="Operator">发布人</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId, string Title, string OperatorId, string Operator, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovNotice> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovNotice>();
                return dal.GetGovNoticeList(CompanyId, Title,OperatorId, Operator, ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据接收类型获得公告通知信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">接收类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNotice> GetGovNoticeList(string CompanyId,EyouSoft.Model.EnumType.GovStructure.ItemType Type, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovNotice> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovNotice>();
                return dal.GetGovNoticeList(CompanyId, Type, ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 增加一条浏览人信息
        /// </summary>
        /// <param name="model">浏览人model</param>
        public bool AddGovNoticeBrowse(Model.GovStructure.MGovNoticeBrowse model)
        {
            if (model != null && !string.IsNullOrEmpty(model.NoticeId) && !string.IsNullOrEmpty(model.OperatorId))
            {
                bool result = dal.AddGovNoticeBrowse(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条浏览人信息：编号为：" + model.NoticeId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得公告通知浏览人信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="NoticeId">公告通知编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovNoticeBrowse> GetGovNoticeBrowseList(string CompanyId, string NoticeId, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovNoticeBrowse> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(NoticeId))
            {
                list = new List<Model.GovStructure.MGovNoticeBrowse>();
                return dal.GetGovNoticeBrowseList(CompanyId, NoticeId, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据公告通知编号删除
        /// </summary>
        /// <param name="NoticeIds">公告通知ID</param>
        /// <returns></returns>
        public bool DeleteGovNotice(params string[] NoticeIds)
        {
            if (NoticeIds.Length > 0)
            {
                bool result = dal.DeleteGovNotice(ItemType, NoticeIds);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < NoticeIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", NoticeIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除公告通知：编号为：" + sId);
                }
                return result;
            }
            return false;
        }

        #endregion  成员方法
    }
}
