using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TongJiStructure
{
    /// <summary>
    /// 统计分析-利润统计接口
    /// </summary>
    public interface ILiRun
    {
        /// <summary>
        /// 获取利润统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MLiRunInfo> GetLiRuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MLiRunChaXunInfo chaXun, out EyouSoft.Model.TongJiStructure.MLiRunHeJiInfo heJi);
    }
}
