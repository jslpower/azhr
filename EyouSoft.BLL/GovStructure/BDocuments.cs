using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 文件管理管理BLL
    /// 2012-03-07 邵权江 创建
    /// </summary>
    public class BDocuments
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IDocuments dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IDocuments>();
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "文件管理");

        #region  成员方法
        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.FontSize) && !string.IsNullOrEmpty(model.Company)
                && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.AttnId) && !string.IsNullOrEmpty(model.OperatorID))
            {
                model.DocumentsId = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.DocumentsId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                if (model.GovDocumentsApproveList != null && model.GovDocumentsApproveList.Count > 0)
                {
                    for (int i = 0; i < model.GovDocumentsApproveList.Count; i++)
                    {
                        model.GovDocumentsApproveList[i].DocumentsId = model.DocumentsId;
                    }
                }
                bool result = dal.AddGovDocuments(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条文件管理：编号为：" + model.DocumentsId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 修改文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateGovDocuments(EyouSoft.Model.GovStructure.MGovDocuments model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            if (model != null && !string.IsNullOrEmpty(model.DocumentsId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.FontSize) && !string.IsNullOrEmpty(model.Company)
                && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.AttnId) && !string.IsNullOrEmpty(model.OperatorID))
            {
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.DocumentsId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                if (model.GovDocumentsApproveList != null && model.GovDocumentsApproveList.Count > 0)
                {
                    for (int i = 0; i < model.GovDocumentsApproveList.Count; i++)
                    {
                        model.GovDocumentsApproveList[i].DocumentsId = model.DocumentsId;
                    }
                }
                bool result = dal.UpdateGovDocuments(model,ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("修改一条文件管理：编号为：" + model.DocumentsId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 根据 文件ID 获取文件实体信息
        /// </summary>
        /// <param name="DocumentsId">文件ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovDocuments GetGovFilePersonnelModel(string DocumentsId)
        {
            EyouSoft.Model.GovStructure.MGovDocuments model = null;
            if (!string.IsNullOrEmpty(DocumentsId))
            {
                model = new EyouSoft.Model.GovStructure.MGovDocuments();
                model = dal.GetGovDocumentsModel(DocumentsId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 修改审批信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGovDocumentsApprove(EyouSoft.Model.GovStructure.MGovDocumentsApprove model)
        {
            if (model != null && !string.IsNullOrEmpty(model.DocumentsId) && !string.IsNullOrEmpty(model.ApproveID))
            {
                bool result = dal.AddGovDocumentsApprove(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条文件审批信息：编号为：" + model.DocumentsId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 删除文件信息
        /// </summary>
        /// <param name="Ids">编号</param>
        /// <returns></returns>
        public bool DeleteGovDocuments(params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                bool result = dal.DeleteGovDocuments(ItemType,Ids);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除文件管理：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }

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
        public IList<EyouSoft.Model.GovStructure.MGovDocuments> GetGovDocumentsList(string CompanyId, string FontSize, string Company, string Title, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovDocuments> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovDocuments>();
                return dal.GetGovDocumentsList(CompanyId, FontSize,Company,Title,ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        #endregion
    }
}
