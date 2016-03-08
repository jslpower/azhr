using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TongJiStructure
{
    /// <summary>
    /// 统计分析-导游业绩统计
    /// </summary>
    public interface IDaoYouYeJi
    {
        /// <summary>
        /// 获取导游业绩统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiInfo> GetDaoYouYeJis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MDaoYouYeJiChaXunInfo chaXun, out EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo heJi);
        /// <summary>
        /// 获取导游业绩排名统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo> GetDaoYouYeJiPaiMings(string companyId, EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingChaXunInfo chaXun);
        /// <summary>
        /// 获取导游带团人数排名统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanInfo> GetDaoYouDaiTuans(string companyId, EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanChaXunInfo chaXun);
    }
}
