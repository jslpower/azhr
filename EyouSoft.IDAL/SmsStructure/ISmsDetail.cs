using System.Collections.Generic;

namespace EyouSoft.IDAL.SmsStructure
{
    /// <summary>
    /// 短信发送明细数据接口
    /// </summary>
    /// 周文超 2011-09-14
    public interface ISmsDetail
    {
        /// <summary>
        /// 添加短信发送明细
        /// </summary>
        /// <param name="model">短信发送明细实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int AddSmsDetail(Model.SmsStructure.MSmsDetail model);

        /// <summary>
        /// 获取短信发送明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        IList<Model.SmsStructure.MSmsDetail> GetSmsDetailList(int pageSize, int pageIndex, ref int recordCount
                                                              , string companyId, Model.SmsStructure.MQuerySmsDetail model);

        /// <summary>
        /// 添加定时发送的短信
        /// </summary>
        /// <param name="model">定时发送的短信实体</param>
        /// <returns>返回1成功；其他失败</returns>
        int AddSmsTimerTask(Model.SmsStructure.MSmsTimerTask model);

        /// <summary>
        /// 获取短信发送实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="PlanId">发送任务编号</param>
        /// <returns></returns>
        Model.SmsStructure.MSmsDetail GetSmsDetaiInfo(string CompanyId, string PlanId);
    }
}
