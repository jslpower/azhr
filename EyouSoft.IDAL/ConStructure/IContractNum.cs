using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ConStructure
{
    /// <summary>
    /// 合同号管理接口
    /// 2011-09-02 邵权江 创建
    /// </summary>
    public interface IContractNum
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该合同号段
        /// </summary>
        /// <param name="model">合同号段model</param>
        /// <returns></returns>
        bool ExistsNum(Model.ConStructure.MContractNum model);

        /// <summary>
        /// 登记合同号
        /// </summary>
        /// <param name="ListModel">合同号model集合</param>
        /// <returns> 0：成功 负值小于0：失败 正值大于0：重复数</returns>
        int AddContractNum(IList<Model.ConStructure.MContractNum> ListModel);

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ContractType">合同类型</param>
        /// <param name="SearchModel">查询参数实体</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId, int ContractType, Model.ConStructure.MContractNumSearch SearchModel, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据条件获得合同号列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">查询</param>
        /// <returns></returns>
        IList<Model.ConStructure.MContractNumList> GetAutocompleteHeTongs(string companyId, EyouSoft.Model.ConStructure.MAutocompleteChaXunInfo info);

        /// <summary>
        /// 根据合同编号获得合同号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns></returns>
        IList<Model.ConStructure.MContractNumList> GetContractNumList(string CompanyId, params string[] Ids);

        /// <summary>
        /// 领用合同号
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="ListModel">领用合同号model集合</param>
        /// <returns>操作结果 0：成功 负值：失败 正值大于0：重复数</returns>
        int AddContractNumCollar(string CompanyId, IList<Model.ConStructure.MContractNumCollar> ListModel);

        /// <summary>
        /// 合同号（使用，销号，作废）
        /// </summary>
        /// <param name="ContractStatus">合同号状态</param>
        /// <param name="Ids">合同号ids</param>
        /// <returns>0或负值：失败，1成功，2合同号不在状态</returns>
        int ChangeContractStatus(EyouSoft.Model.EnumType.ConStructure.ContractStatus ContractStatus, params string[] Ids);

        /// <summary>
        /// 批量合同号状态改变（使用，销号，作废）
        /// </summary>
        /// <param name="ListModel">合同号状态集合</param>
        /// <returns></returns>
        int ChangeContractStatus(IList<EyouSoft.Model.ConStructure.MContractStatus> ListModel);

        /// <summary>
        /// 删除合同信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="heTongId">合同编号</param>
        /// <returns></returns>
        int Delete(string companyId, string heTongId);
        #endregion  成员方法
    } 
}
