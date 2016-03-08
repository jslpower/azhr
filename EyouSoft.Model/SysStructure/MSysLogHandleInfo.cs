using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    #region 操作日志信息业务实体
    /// <summary>
    /// 操作日志信息业务实体
    /// </summary>
    [Serializable]
    public class MSysLogHandleInfo
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 一级栏目编号
        /// </summary>
        public int Menu1Id { get; set; }
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        public int Menu2Id { get; set; }
        /// <summary>
        /// 一级栏目名称
        /// </summary>
        public string Menu1Name { get; set; }
        /// <summary>
        /// 二级栏目名称
        /// </summary>
        public string Menu2Name { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        public string RemoteIp { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 操作日志查询信息业务实体
    /// <summary>
    /// 操作日志查询信息业务实体
    /// </summary>
    public class MSysLogHandleSearch
    {        
        /// <summary>
        /// 部门编号
        /// </summary>
        public int? DeptId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 开始操作时间
        /// </summary>
        public DateTime? SDate { get; set; }
        /// <summary>
        /// 结束操作时间
        /// </summary>
        public DateTime? EDate { get; set; }
    }
    #endregion
}
