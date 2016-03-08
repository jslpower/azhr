using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TongJiStructure
{
    /// <summary>
    /// 统计分析-人数统计
    /// </summary>
    public interface IRenShu
    {
        /// <summary>
        /// 获取人数统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MRenShuInfo> GetRenShus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MRenShuChaXunInfo chaXun);
    }
}
