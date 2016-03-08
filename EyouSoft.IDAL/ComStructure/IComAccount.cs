using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 公司银行帐号
    /// </summary>
    public interface IComAccount
    {
        /// <summary>
        /// 获取公司的所有银行帐号信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号集合</returns>
        IList<MComAccount> GetList(string companyId);

        /// <summary>
        /// 获取单个银行帐号
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号实体</returns>
        MComAccount GetModel(int id, string companyId);
    }
}
