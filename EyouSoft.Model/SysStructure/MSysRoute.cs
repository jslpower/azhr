using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    public class MSysRoute
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 分公司编号
        /// </summary>
        public int ChildCompanyId { get; set; }
        /// <summary>
        /// 语言类型
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 中文ID
        /// </summary>
        public int MasterId { get; set; }
        /// <summary>
        /// 操作者部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
    public class MSysRouteSearch
    {
    }
}
