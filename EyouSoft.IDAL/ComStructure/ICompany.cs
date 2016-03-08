using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 公司(系统)信息
    /// </summary>
    public interface ICompany
    {
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="info">公司信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(MCompany info);

        /// <summary>
        /// 根据公司编号获取公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns>公司信息实体</returns>
        MCompany GetModel(string companyId, string sysId);
        ///// <summary>
        ///// 获取公司所有账号
        ///// </summary>
        ///// <param name="companyId">公司编号</param>
        ///// <returns></returns>
        //IList<MComAccount> GetList(string companyId);
    }
}
