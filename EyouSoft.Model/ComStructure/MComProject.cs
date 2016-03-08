using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司服务标准模版
    /// </summary>
    [Serializable]
    public class MComProject
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
        /// 类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ProjectType Type { get; set; }
        /// <summary>
        /// 包含项目类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectType ItemType { get; set; }
        /// <summary>
        /// 包含项目单位
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit Unit { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
