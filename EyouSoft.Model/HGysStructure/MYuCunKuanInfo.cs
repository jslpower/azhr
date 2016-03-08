using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 供应商预存款信息业务实体
    /// <summary>
    /// 供应商预存款信息业务实体
    /// </summary>
    public class MYuCunKuanInfo
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 预存款编号
        /// </summary>
        public string YuCunId { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion
}
