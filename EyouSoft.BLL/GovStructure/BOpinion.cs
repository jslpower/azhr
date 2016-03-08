using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 意见建议BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BOpinion
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IOpinion dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IOpinion>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "意见建议");

        #region  成员方法
        /// <summary>
        /// 增加一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        public bool AddGovOpinion(Model.GovStructure.MGovOpinion model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                model.OpinionId = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.OpinionId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                if (model.MGovOpinionUserList != null && model.MGovOpinionUserList.Count > 0)
                {
                    for (int i = 0; i < model.MGovOpinionUserList.Count; i++)
                    {
                        model.MGovOpinionUserList[i].OpinionId = model.OpinionId;
                    }
                }
                bool result = dal.AddGovOpinion(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条意见建议信息：编号为：" + model.OpinionId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新一条意见建议信息
        /// </summary>
        /// <param name="model">意见建议model</param>
        /// <returns></returns>
        public bool UpdateGovOpinion(Model.GovStructure.MGovOpinion model)
        {
            if (model != null && !string.IsNullOrEmpty(model.OpinionId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.OpinionId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                if (model.MGovOpinionUserList != null && model.MGovOpinionUserList.Count > 0)
                {
                    for (int i = 0; i < model.MGovOpinionUserList.Count; i++)
                    {
                        model.MGovOpinionUserList[i].OpinionId = model.OpinionId;
                    }
                }
                bool result = dal.UpdateGovOpinion(model, ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条意见建议信息：编号为：" + model.OpinionId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得意见建议实体
        /// </summary>
        /// <param name="OpinionId">意见建议ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovOpinion GetGovOpinionModel(string OpinionId)
        {
            EyouSoft.Model.GovStructure.MGovOpinion model = null;
            if (!string.IsNullOrEmpty(OpinionId))
            {
                model = new EyouSoft.Model.GovStructure.MGovOpinion();
                model = dal.GetGovOpinionModel(OpinionId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 根据条件意见建议信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="model">查询参数类</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovOpinion> GetGovOpinionList(string CompanyId, Model.GovStructure.MSearchOpinion MSearchOpinion, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovOpinion> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovOpinion>();
                return dal.GetGovOpinionList(CompanyId, MSearchOpinion, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据意见建议编号删除
        /// </summary>
        /// <param name="OpinionIds">意见建议ID</param>
        /// <returns></returns>
        public bool DeleteGovOpinion(params string[] OpinionIds)
        {
            if (OpinionIds.Length > 0)
            {
                bool result = dal.DeleteGovOpinion(ItemType, OpinionIds);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < OpinionIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", OpinionIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除意见建议：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
