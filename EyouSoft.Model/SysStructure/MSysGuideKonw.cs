using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    public class MSysGuideKonw
    {

        /// <summary>
        /// 信息编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 导游须知
        /// </summary>
        public string KnowMark { get; set; }
        /// <summary>
        /// 操作人部门编号
        /// </summary>
        public int OperatorDeptId { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    public class MSysGuideKonwSearch
    {
    }
}
