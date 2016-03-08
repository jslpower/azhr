using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 培训管理BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BTrain
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.ITrain dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.ITrain>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "培训管理");

        #region  成员方法
        /// <summary>
        /// 增加一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <returns></returns>
        public bool AddGovTrain(EyouSoft.Model.GovStructure.MGovTrain model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && model.StateTime != null && !string.IsNullOrEmpty(model.OperatorId))
            {
                model.TrainId = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.TrainId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                bool result = dal.AddGovTrain(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条培训信息：编号为：" + model.TrainId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <returns></returns>
        public bool UpdateGovTrain(EyouSoft.Model.GovStructure.MGovTrain model)
        {
            if (model != null && !string.IsNullOrEmpty(model.TrainId) && !string.IsNullOrEmpty(model.CompanyId) && model.StateTime != null && !string.IsNullOrEmpty(model.OperatorId))
            {
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.TrainId;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                bool result = dal.UpdateGovTrain(model, ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条培训信息：编号为：" + model.TrainId);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得培训实体
        /// </summary>
        /// <param name="TrainId">培训ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovTrain GetGovTrainModel(string TrainId)
        {
            EyouSoft.Model.GovStructure.MGovTrain model = null;
            if (!string.IsNullOrEmpty(TrainId))
            {
                model = new EyouSoft.Model.GovStructure.MGovTrain();
                model = dal.GetGovTrainModel(TrainId, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 根据培训时间条件获得培训信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="Title">主题</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovTrain> GetGovTrainList(string CompanyId, DateTime? TimeBegin, DateTime? TimeEnd, string Title, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovTrain> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovTrain>();
                list = dal.GetGovTrainList(CompanyId, TimeBegin, TimeEnd, Title, ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据培训ID删除
        /// </summary>
        /// <param name="TrainIds">培训ID</param>
        /// <returns></returns>
        public bool DeleteGovTrain(params string[] TrainIds)
        {
            if (TrainIds.Length > 0)
            {
                bool result = dal.DeleteGovTrain(ItemType, TrainIds);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < TrainIds.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", TrainIds[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除培训：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }

        #endregion
    }
}
