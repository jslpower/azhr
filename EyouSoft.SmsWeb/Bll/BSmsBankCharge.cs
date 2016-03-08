using System;
using System.Collections.Generic;
using EyouSoft.SmsWeb.Dal;
using EyouSoft.SmsWeb.Model;

namespace EyouSoft.SmsWeb.Bll
{
    /// <summary>
    /// 短信中心充值明细业务逻辑
    /// </summary>
    /// 周文超 2011-09-15
    public class BSmsBankCharge
    {
        private readonly DSmsBankCharge _dal = new DSmsBankCharge();

        /// <summary>
        /// 客户充值（未审核状态）
        /// </summary>
        /// <param name="model">充值明细实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int RechargeByCostomer(MSmsBankCharge model)
        {
            if (model == null || string.IsNullOrEmpty(model.AccountId) || string.IsNullOrEmpty(model.AppKey) 
                || model.ChargeAmount <= 0)
                return 0;

            model.Status = ChargeStatus.未审核;
            model.RealAmount = 0;
            model.IssueTime = DateTime.Now;

            return _dal.Recharge(model);
        }

        /// <summary>
        /// 审核充值明细
        /// </summary>
        /// <param name="model">短信充值明细审核业务实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int CheckRechargeState(MCheckSmsBankCharge model)
        {
            if (model == null || string.IsNullOrEmpty(model.ChargeId)||model.Status== ChargeStatus.未审核)
                return 0;

            return CheckRechargeState(new List<MCheckSmsBankCharge> { model });
        }

        /// <summary>
        /// 审核充值明细
        /// </summary>
        /// <param name="list">短信充值明细审核业务实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        private int CheckRechargeState(IList<MCheckSmsBankCharge> list)
        {
            if (list == null || list.Count < 1)
                return 0;

            return _dal.CheckRechargeState(list);
        }

        /// <summary>
        /// 查询充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="queryModel">查询实体</param>
        /// <returns>返回充值明细集合</returns>
        public List<MSmsBankCharge> GetSmsBankCharge(int pageSize, int pageIndex, ref int recordCount
            , MQuerySmsBankCharge queryModel)
        {
            return _dal.GetSmsBankCharge(pageSize, pageIndex, ref recordCount, queryModel);
        }
    }
}
