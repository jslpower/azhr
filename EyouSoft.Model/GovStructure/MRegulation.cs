using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 规章制度
    /// <summary>
    /// 规章制度
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovRegulation
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovRegulation() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string RegId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 制度编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 制度内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string IssuedId { get; set; }
        /// <summary>
        /// 发布人姓名
        /// </summary>
        public string IssuedName { get; set; }
        /// <summary>
        /// 发布部门编号
        /// </summary>
        public int IssuedDeptId { get; set; }
        /// <summary>
        /// 发布部门
        /// </summary>
        public string IssuedDepartName { get; set; }
        /// <summary>
        /// 适用部门实体集合
        /// </summary>
        public IList<MGovRegApplyDept> ApplyDeptList { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 附件实体集合
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
    }
    #endregion

    #region 适用部门实体
    /// <summary>
    /// 适用部门实体
    /// </summary>
    public class MGovRegApplyDept
    {
        /// <summary>
        /// 规章制度ID
        /// </summary>
        public string RegId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
    }
    #endregion

    #region 规章制度查询实体
    /// <summary>
    /// 规章制度查询实体
    /// 2011-09-01 邵权江 创建
    /// </summary>
    public class MGovRegSearch
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovRegSearch() { }
        /// <summary>
        /// 制度编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 制度标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 适用部门DepIds
        /// </summary>
        public string[] DepIds { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepName { get; set; }
    }
    #endregion
}
