using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.ConStructure
{
    /// <summary>
    /// 合同号管理BLL
    /// 2011-09-07 邵权江 创建
    /// </summary>
    public class BContractNum
    {
        /// <summary>
        /// dal对象
        /// </summary>
        EyouSoft.IDAL.ConStructure.IContractNum dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ConStructure.IContractNum>();

        #region  成员方法
        /// <summary>
        /// 是否存在该合同号
        /// </summary>
        /// <param name="model">合同号段model</param>
        /// <returns></returns>
        public bool ExistsNum(Model.ConStructure.MContractNum model)
        {
            if (model != null && !string.IsNullOrEmpty(model.CompanyId) && !string.IsNullOrEmpty(model.ContractCode))
            {
                return dal.ExistsNum(model);
            }
            return false;

        }

        /// <summary>
        /// 登记合同号
        /// </summary>
        /// <param name="ListModel">合同号model集合</param>
        /// <returns>0：成功 负值小于0：失败 正值大于0：重复数</returns>
        public int AddContractNum(IList<Model.ConStructure.MContractNum> ListModel)
        {
            if (ListModel != null)
            {
                for (int i = 0; i < ListModel.Count; i++)
                {
                    if (ListModel[i] != null && !string.IsNullOrEmpty(ListModel[i].CompanyId) && !string.IsNullOrEmpty(ListModel[i].ContractCode) && ListModel[i].OperatorId != null)
                    {
                        ListModel[i].ContractId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return 0;
                    }
                }
                int result = dal.AddContractNum(ListModel);
                if (result == 1)
                {
                    foreach (Model.ConStructure.MContractNum model in ListModel)
                    {
                        SysStructure.BSysLogHandle.Insert("登记合同号信息：合同编号为：" + model.ContractId);
                    }
                }
                return result;
            }
            return -1;
        }

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ContractType">合同类型(0:国内，1：境外，2：所有)</param>
        /// <param name="SearchModel">查询参数实体</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId, int ContractType, Model.ConStructure.MContractNumSearch SearchModel, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<Model.ConStructure.MContractNumList> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<Model.ConStructure.MContractNumList>();
                list = dal.GetContractNumList(CompanyId, ContractType, SearchModel, PageSize, PageIndex, ref RecordCount);
            }
            return list;
        }

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">查询</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetAutocompleteHeTongs(string companyId, EyouSoft.Model.ConStructure.MAutocompleteChaXunInfo info)
        {
            if (string.IsNullOrEmpty(companyId) || info == null) return null;
            if (info.Length < 1) info.Length = 1;

            return dal.GetAutocompleteHeTongs(companyId, info);
        }


        /// <summary>
        /// 根据合同编号获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns></returns>
        public IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId, params string[] Ids)
        {
            IList<Model.ConStructure.MContractNumList> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && Ids.Length > 0)
            {
                list = new List<Model.ConStructure.MContractNumList>();
                list = dal.GetContractNumList(CompanyId, Ids);
            }
            return list;
        }

        /// <summary>
        /// 领用合同号
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ListModel">领用合同号model集合</param>
        /// <returns>操作结果 0：成功 负值：失败 正值大于0：重复数</returns>
        public int AddContractNumCollar(string CompanyId, IList<Model.ConStructure.MContractNumCollar> ListModel)
        {
            if (ListModel != null)
            {
                for (int i = 0; i < ListModel.Count; i++)
                {
                    if (ListModel[i] != null && !string.IsNullOrEmpty(ListModel[i].ContractId) && !string.IsNullOrEmpty(ListModel[i].ContractCode) && ListModel[i].UseId != null && ListModel[i].OperatorId != null)
                    {
                        ListModel[i].CollariId = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        return 0;
                    }
                }
                int result = dal.AddContractNumCollar(CompanyId, ListModel);
                if (result == 1)
                {
                    foreach (Model.ConStructure.MContractNumCollar model in ListModel)
                    {
                        SysStructure.BSysLogHandle.Insert("领用合同号信息：合同编号为：" + model.ContractId);
                    }
                }
                return result;
            }
            return -1;
        }

        /// <summary>
        /// 合同号（使用，销号，作废）
        /// </summary>
        /// <param name="ContractStatus">合同号状态</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns>0或负值：失败，1成功，2合同号不在状态</returns>
        public int ChangeContractStatus(EyouSoft.Model.EnumType.ConStructure.ContractStatus ContractStatus, params string[] Ids)
        {
            if (Ids.Length > 0)
            {
                int result = dal.ChangeContractStatus(ContractStatus, Ids);
                if (result == 1)
                {
                    StringBuilder sId = new StringBuilder();
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        sId.AppendFormat("'{0}',", Ids[i]);
                    }
                    sId.Remove(sId.Length - 1, 1);
                    SysStructure.BSysLogHandle.Insert("更新合同号信息：编号为：" + sId.ToString());
                }
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 批量合同号状态改变（使用，销号，作废）
        /// </summary>
        /// <param name="ListModel">合同号状态集合</param>
        /// <returns>1：成功 0：失败</returns>
        public int ChangeContractStatus(IList<EyouSoft.Model.ConStructure.MContractStatus> ListModel)
        {
            if (ListModel != null && ListModel.Count > 0)
            {
                int result = dal.ChangeContractStatus(ListModel);
                if (result == 1)
                {
                    StringBuilder sId = new StringBuilder();
                    foreach (Model.ConStructure.MContractStatus model in ListModel)
                    {
                        SysStructure.BSysLogHandle.Insert("合同号状态改变：合同编号为：" + model.ContractId);
                    }
                }
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 删除合同信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="heTongId">合同编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string heTongId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(heTongId)) return 0;

            int dalRetCode = dal.Delete(companyId, heTongId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除合同信息，合同编号：" + heTongId + "。");
            }

            return dalRetCode;
        } 
        #endregion  成员方法
    }
}
