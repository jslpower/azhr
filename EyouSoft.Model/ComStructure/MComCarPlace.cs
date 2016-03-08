using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司上车地点加价实体
    /// </summary>
    [Serializable]
    public class MComCarPlace
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 上车地点
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// 加价金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
