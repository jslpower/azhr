using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 公司合同接口
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public interface IContract
    {
        #region  成员方法
        /// <summary>
        /// 判断合同编号是否存在
        /// </summary>
        /// <param name="Number">合同编码</param>
        /// <param name="ID">合同ID,新增ID=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsNumber(string Number, string ID, string CompanyId);

        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <returns></returns>
        bool AddGovContract(EyouSoft.Model.GovStructure.MGovContract model);

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="model">合同model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool UpdateGovContract(EyouSoft.Model.GovStructure.MGovContract model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 获得合同实体
        /// </summary>
        /// <param name="ID">合同ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovContract GetGovContractModel(string ID, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType);

        /// <summary>
        /// 根据条件合同编号列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Type">合同类型</param>
        /// <param name="TimeBegin">签订时间</param>
        /// <param name="TimeEnd">到期时间</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovContract> GetGovContractList(string CompanyId, string Type, DateTime? TimeBegin, DateTime? TimeEnd, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount);
        
        /// <summary>
        /// 根据合同ID删除
        /// </summary>
        /// <param name="IDs">合同ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        bool DeleteGovContract(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] Ids);

        #endregion
    }
}
