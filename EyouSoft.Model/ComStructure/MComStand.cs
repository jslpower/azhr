using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 报价标准
    /// </summary>
    [Serializable]
    public class MComStand
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
        /// 等级名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
