using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 变更
    /// </summary>
    public class MBianGeng
    {
        /// <summary>
        /// 变更编号
        /// </summary>
        public string BianId { get; set; }

        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public EyouSoft.Model.EnumType.TourStructure.BianType? BianType { get; set; }

        /// <summary>
        /// 静态页面地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
