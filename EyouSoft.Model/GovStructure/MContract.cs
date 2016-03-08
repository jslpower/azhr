using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 公司合同
    /// <summary>
    /// 公司合同
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public class MGovContract
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovContract() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 合同类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 签订时间
        /// </summary>
        public DateTime? SignedTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? MaturityTime { get; set; }
        /// <summary>
        /// 合同单位
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 是否提醒(1是/0否)
        /// </summary>
        public bool IsRemind { get; set; }
        /// <summary>
        /// 合同描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 签订人ID
        /// </summary>
        public string signierId { get; set; }
        /// <summary>
        /// 签订部门ID
        /// </summary>
        public int SignedDepId { get; set; }
        /// <summary>
        /// 签订人
        /// </summary>
        public string signier { get; set; }
        /// <summary>
        /// 签订部门
        /// </summary>
        public string SignedDep { get; set; }
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
        public IList<EyouSoft.Model.ComStructure.MComAttach> ComAttachList { get; set; }
    }
    #endregion
}
