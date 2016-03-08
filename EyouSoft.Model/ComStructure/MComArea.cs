using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 线路区域
    /// 创建者：郑付杰
    /// 创建时间：2011/9/5
    /// </summary>
    public class MComArea
    {
        /// <summary>
        /// 自增编号
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
        /// 线路类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.AreaType Type { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 责任计调
        /// </summary>
        public IList<MComAreaPlan> Plan { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 分公司编号
        /// </summary>
        public int ChildCompanyId { get; set; }
        /// <summary>
        /// 语言类型
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 中文编号
        /// </summary>
        public int MasterId { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
    }

    /// <summary>
    /// 线路区域责任计调
    /// 创建者：郑付杰
    /// 创建时间：2011/9/7
    /// </summary>
    public class MComAreaPlan
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string Planer { get; set; }
        /// <summary>
        /// 责任计调人
        /// </summary>
        public string OperatorId { get; set; }
    }

    /// <summary>
    /// 线路区域搜索实体
    /// </summary>
    public class MComAreaSearch
    {
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 线路类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.AreaType? Type { get; set; }
    }
}
