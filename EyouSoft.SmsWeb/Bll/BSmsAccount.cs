using System.Collections.Generic;
using EyouSoft.SmsWeb.Dal;
using System;

namespace EyouSoft.SmsWeb.Bll
{
    /// <summary>
    /// 短信账户信息业务逻辑
    /// </summary>
    /// 周文超 2011-09-14
    public class BSmsAccount
    {
        private readonly DSmsAccount _dal = new DSmsAccount();

        /// <summary>
        /// 短信中心开户
        /// </summary>
        /// <param name="model">短信账户信息实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public Model.MSmsAccountBase AddSmsAccount(Model.MSmsAccount model)
        {
            if (model == null ) return null;

            model.AccountId = Guid.NewGuid().ToString();
            model.AppKey = Guid.NewGuid().ToString();
            model.AppSecret = Guid.NewGuid().ToString();
            model.Pwd = Guid.NewGuid().ToString();
            model.IssueTime = DateTime.Now;

            return _dal.AddSmsAccount(model);
        }

        /// <summary>
        /// 设置账户通道及单价信息
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="smsUnitPrice">单价信息集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public int SetSmsUnitPrice(string accountId, List<EyouSoft.SmsWeb.Model.MSmsChannelInfo> smsUnitPrice)
        {
            if (string.IsNullOrEmpty(accountId) || smsUnitPrice == null || smsUnitPrice.Count < 1) return 0;

            foreach (var item in smsUnitPrice)
            {
                if (item.Price <= 0) item.Price = 0.1M;
            }

            return _dal.SetSmsUnitPrice(accountId, smsUnitPrice);
        }

        /// <summary>
        /// 获取短信账户全部信息
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <returns></returns>
        public Model.MSmsAccount GetFullSmsAccount(string accountId, string appKey)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey))
                return null;

            return _dal.GetFullSmsAccount(accountId, appKey);
        }

        /// <summary>
        /// 获取公司可用余额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <returns></returns>
        public Model.MSmsAccountBase GetSmsAccount(string accountId, string appKey)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey))
                return null;

            return _dal.GetSmsAccount(accountId, appKey);
        }

        /// <summary>
        /// 增加账户可用金额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <param name="addMoney">增加金额</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddSmsAccountAmount(string accountId, string appKey, decimal addMoney)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey) || addMoney <= 0)
                return 0;

            return _dal.UpdateSmsAccountAmount(accountId, appKey, true, addMoney);
        }

        /// <summary>
        /// 减少账户可用金额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <param name="reduceMoney">减少金额</param>
        /// <returns>返回1成功，其他失败</returns>
        public int ReduceSmsAccountAmount(string accountId, string appKey, decimal reduceMoney)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey) || reduceMoney <= 0)
                return 0;

            return _dal.UpdateSmsAccountAmount(accountId, appKey, false, reduceMoney);
        }

        /// <summary>
        /// 账户验证
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        public bool IsExists(EyouSoft.SmsWeb.Model.MSmsAccountBase account)
        {
            if (account == null || string.IsNullOrEmpty(account.AccountId) || string.IsNullOrEmpty(account.AppKey)) return false;

            return _dal.IsExists(account);
        }
    }
}
