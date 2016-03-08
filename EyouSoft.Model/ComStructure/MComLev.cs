using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 客户等级
    /// </summary>
    [Serializable]
    public class MComLev
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
        /// 客户等级名称
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
        /// 是否系统默认
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 客户等级类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.LevType LevType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BackMark { get; set; }
        /// <summary>
        /// 浮动百分比
        /// </summary>
        public decimal FloatMoney { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        public string Operator { get; set; }



    }
}
