using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 规章制度BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BRegulation
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IRegulation dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IRegulation>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "规章制度");

        #region  成员方法
        /// <summary>
        /// 判断规章制度编号是否存在
        /// </summary>
        /// <param name="Code">规章制度编码</param>
        /// <param name="RegId">规章制度RegId,新增RegId=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsCode(string Code, string RegId, string CompanyId)
        {
            if (!string.IsNullOrEmpty(Code) && RegId != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsCode(Code, RegId, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 增加一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <returns></returns>
        public bool AddGovRegulation(EyouSoft.Model.GovStructure.MGovRegulation model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Code) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                model.RegId = Guid.NewGuid().ToString();
                if (model.ApplyDeptList != null && model.ApplyDeptList.Count > 0)
                {
                    for (int i = 0; i < model.ApplyDeptList.Count; i++)
                    {
                        model.ApplyDeptList[i].RegId = model.RegId;
                    }
                }
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.RegId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                bool result = dal.AddGovRegulation(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条规章制度信息：编号为：" + model.RegId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新一条规章制度
        /// </summary>
        /// <param name="model">规章制度model</param>
        /// <returns></returns>
        public bool UpdateRegulation(EyouSoft.Model.GovStructure.MGovRegulation model)
        {
            if (model != null && !string.IsNullOrEmpty(model.RegId) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Code) && !string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.OperatorId))
            {
                if (model.ApplyDeptList != null && model.ApplyDeptList.Count > 0)
                {
                    for (int i = 0; i < model.ApplyDeptList.Count; i++)
                    {
                        model.ApplyDeptList[i].RegId = model.RegId;
                    }
                }
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.RegId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                bool result = dal.UpdateRegulation(model, ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条规章制度信息：编号为：" + model.RegId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得规章制度实体
        /// </summary>
        /// <param name="ID">规章制度ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovRegulation GetGovRegulationModel(string RegId)
        {
            EyouSoft.Model.GovStructure.MGovRegulation model = null;
            if (!string.IsNullOrEmpty(RegId))
            {
                model = new EyouSoft.Model.GovStructure.MGovRegulation();
                model = dal.GetGovRegulationModel(RegId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 获得规章制度信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="SearchModel">查询实体</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovRegulation> GetGovRegulationList(string CompanyId, EyouSoft.Model.GovStructure.MGovRegSearch SearchModel, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovRegulation> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovRegulation>();
                list = dal.GetGovRegulationList(CompanyId, SearchModel,ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="Ids">ID</param>
        /// <returns></returns>
        public bool DeleteGovRegulation(params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                bool result = dal.DeleteGovRegulation(ItemType, Ids);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除规章制度：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }

        #endregion
    }
}
