using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司银行帐号
    /// </summary>
    [Serializable]
    public class MComAccount
    {
        /// <summary>
        /// 银行账户编号
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 开户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行帐号
        /// </summary>
        public string BankNo { get; set; }        
        /// <summary>
        /// 初始金额
        /// </summary>
        public decimal InitialMoney { get; set; }
        /// <summary>
        /// 是否设置过账号余额
        /// </summary>
        public bool IsSet { get; set; }
    }
}
