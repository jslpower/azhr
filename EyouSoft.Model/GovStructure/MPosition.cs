using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 职务管理
    /// <summary>
    /// 职务管理
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovPosition
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovPosition() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public int PositionId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 职位说明书
        /// </summary>
        public string Description { get; set; }
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
