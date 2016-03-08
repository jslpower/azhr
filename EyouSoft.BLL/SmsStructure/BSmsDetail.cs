using System.Collections.Generic;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 短信发送明细业务逻辑
    /// </summary>
    /// 周文超 2011-09-14
    public class BSmsDetail : BLLBase
    {
        private readonly IDAL.SmsStructure.ISmsDetail _dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.SmsStructure.ISmsDetail>();

        /// <summary>
        /// 添加短信发送明细
        /// </summary>
        /// <param name="model">短信发送明细实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsDetail(Model.SmsStructure.MSmsDetail model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId) || model.Amount <= 0
                || model.Number == null || string.IsNullOrEmpty(model.Content))
                return 0;

            return _dal.AddSmsDetail(model);
        }

        /// <summary>
        /// 获取短信发送明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        public IList<Model.SmsStructure.MSmsDetail> GetSmsDetailList(int pageSize, int pageIndex, ref int recordCount
            , string companyId, Model.SmsStructure.MQuerySmsDetail model)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;

            return _dal.GetSmsDetailList(pageSize, pageIndex, ref recordCount, companyId, model);
        }

        /// <summary>
        /// 获取短信发送实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="PlanId">发送任务编号</param>
        /// <returns></returns>
        public Model.SmsStructure.MSmsDetail GetSmsDetaiInfo(string CompanyId, string PlanId)
        {
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(PlanId))
            {
                return _dal.GetSmsDetaiInfo(CompanyId, PlanId);
            }
            return null;
        }

        #region 定时短信方法

        /// <summary>
        /// 添加定时发送的短信
        /// </summary>
        /// <param name="model">定时发送的短信实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsTimerTask(Model.SmsStructure.MSmsTimerTask model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId)
                || model.Number == null || string.IsNullOrEmpty(model.Content))
                return 0;

            return _dal.AddSmsTimerTask(model);
        }

        #endregion
    }
}
