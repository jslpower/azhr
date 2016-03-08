using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    public class MSysJourneyLight
    {
        #region 行程亮点

        /// <summary>
        /// 行程亮点编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DeptID { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 行程亮点
        /// </summary>
        public string JourneySpot { get; set; }
        /// <summary>
        /// 语言类型
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 中文ID
        /// </summary>
        public int MasterId { get; set; }
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
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        #endregion


    }
    public class MSysJourneyLightSearch
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int? DeptID { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType? LngType { get; set; }
    }
}
