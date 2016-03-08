using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 描述：计划上车地点
    /// 修改记录：
    /// 1、2012-08-14 PM 王磊 创建
    /// </summary>
    [Serializable]
    public class MComCarLocation
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public string CarLocationId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 上车地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 送价
        /// </summary>
        public decimal OffPrice { get; set; }
        /// <summary>
        /// 接价
        /// </summary>
        public decimal OnPrice { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 状态(启用or禁用)
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

    }
}
