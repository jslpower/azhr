//金蝶相关数据访问类接口 汪奇志 2012-05-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.FinStructure
{
    /// <summary>
    /// 金蝶相关数据访问类接口
    /// </summary>
    public interface IKis
    {
        /// <summary>
        /// 获取KIS科目集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.FinStructure.MKisAccountGroupInfo> GetAccountGroups(string companyId);
    }
}
