using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 部门附件
    /// </summary>
    [Serializable]
    public class MComDepartmentFiles
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 打印名称
        /// </summary>
        public string PrintName { get; set; }
        /// <summary>
        /// 打印页眉
        /// </summary>
        public string PrintHeader { get; set; }
        /// <summary>
        /// 打印页脚
        /// </summary>
        public string PrintFooter { get; set; }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string PrintTemplates { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
