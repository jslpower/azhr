using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.CrmStructure
{
    #region 客户开户行信息
    /// <summary>
    /// 客户开户行信息
    /// </summary>
    public class MCrmBank
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmBank() { }

        /// <summary>
        /// 开户行编号
        /// </summary>
        public string BankId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CrmId { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行帐号
        /// </summary>
        public string BankAccount { get; set; }
    }
    #endregion
}
