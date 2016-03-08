//短信中心-短信任务相关数据访问类接口
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SmsStructure
{
    /// <summary>
    /// 短信中心-短信任务相关数据访问类接口
    /// </summary>
    public interface ISmsRenWu
    {
        /// <summary>
        /// 写入短信上行信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertShangXing(EyouSoft.Model.SmsStructure.MSmsShangXingInfo info);
        /// <summary>
        /// 写入短信任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.SmsStructure.MSmsRenWuInfo info);
        /// <summary>
        /// 接收任务，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int JieShouRenWu(EyouSoft.Model.SmsStructure.MSmsRenWuJieShouInfo info);
        /// <summary>
        /// 获取短信任务信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">总索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SmsStructure.MSmsRenWuInfo> GetRenWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MSmsRenWuChaXunInfo chaXun);

        /// <summary>
        /// 行程变化，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int XCBH(EyouSoft.Model.SmsStructure.MSmsXCBHInfo info);
        /// <summary>
        /// 进店报账，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int JDBZ(EyouSoft.Model.SmsStructure.MSmsJDBZInfo info);
    }
}
