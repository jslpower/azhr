using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GovStructure
{
    /// <summary>
    /// 公司合同BLL
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class BContract
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.GovStructure.IContract dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GovStructure.IContract>();
        /// <summary>
        /// 附件类型
        /// </summary>
        EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Enum.Parse(typeof(EyouSoft.Model.EnumType.ComStructure.AttachItemType), "公司合同");

        #region  成员方法
        /// <summary>
        /// 判断合同编号是否存在
        /// </summary>
        /// <param name="Number">合同编码</param>
        /// <param name="ID">合同ID,新增ID=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsNumber(string Number, string ID, string CompanyId)
        {
            if (!string.IsNullOrEmpty(Number) && ID != null && !string.IsNullOrEmpty(CompanyId))
            {
                return dal.ExistsNumber(Number, ID, CompanyId);
            }
            return false;
        }

        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <returns></returns>
        public bool AddGovContract(EyouSoft.Model.GovStructure.MGovContract model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Number))
            {
                model.ID = Guid.NewGuid().ToString();
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.ID;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                bool result = dal.AddGovContract(model);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("增加一条公司合同信息：编号为：" + model.ID);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <returns></returns>
        public bool UpdateGovContract(EyouSoft.Model.GovStructure.MGovContract model)
        {
            if (model != null && !string.IsNullOrEmpty(model.ID) && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.Number))
            {
                if (model.ComAttachList != null && model.ComAttachList.Count > 0)
                {
                    for (int i = 0; i < model.ComAttachList.Count; i++)
                    {
                        model.ComAttachList[i].ItemId = model.ID;
                        model.ComAttachList[i].ItemType = ItemType;
                    }
                }
                else
                {
                    model.ComAttachList = null;
                }
                bool result = dal.UpdateGovContract(model, ItemType);
                if (result)
                {
                    SysStructure.BSysLogHandle.Insert("更新一条公司合同信息：编号为：" + model.ID);
                }
                return result;
            }
            return false;
        }

        /// <summary>
        /// 获得合同实体
        /// </summary>
        /// <param name="ID">合同ID</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovContract GetGovContractModel(string ID)
        {
            EyouSoft.Model.GovStructure.MGovContract model = null;
            if (!string.IsNullOrEmpty(ID))
            {
                model = new EyouSoft.Model.GovStructure.MGovContract();
                model= dal.GetGovContractModel(ID, ItemType);
            }
            return model;
        }

        /// <summary>
        /// 根据条件获得合同编号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">合同类型</param>
        /// <param name="TimeBegin">签订时间</param>
        /// <param name="TimeEnd">到期时间</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovContract> GetGovContractList(string CompanyId, string Type, DateTime? TimeBegin, DateTime? TimeEnd, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.GovStructure.MGovContract> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.GovStructure.MGovContract>();
                list = dal.GetGovContractList(CompanyId, Type, TimeBegin, TimeEnd, ItemType, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据合同ID删除
        /// </summary>
        /// <param name="Ids">合同ID</param>
        /// <returns></returns>
        public bool DeleteGovContract(params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                bool result = dal.DeleteGovContract(ItemType, Ids);
                if (result)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("删除公司合同：编号为：" + sId.ToString());
                }
                return result;
            }
            return false;
        }
        #endregion
    }
}
