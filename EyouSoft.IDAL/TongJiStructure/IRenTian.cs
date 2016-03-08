using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TongJiStructure
{
    /// <summary>
    /// 统计分析-人天数统计
    /// </summary>
    public interface IRenTian
    {
        /// <summary>
        /// 获取人天数统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MRenTianInfo> GetRenTians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MRenTianChaXunInfo chaXun);
    }
}
