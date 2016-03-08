using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace EyouSoft.Model.CrmStructure
{
    /// <summary>
    /// 质量回访挤掉安排项
    /// </summary>
    [Serializable]
    [Table(Name = "tbl_CrmVisitDetail")]
    public class MCrmVisitDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCrmVisitDetail() { }
        #region Model
        /// <summary>
        /// 计调项目类型
        /// </summary>
        [Column(Name = "PlanType",DbType="tinyint")]
        public Model.EnumType.PlanStructure.PlanProject PlanType
        {
            get;
            set;
        }

        /// <summary>
        /// 计调项目编号
        /// </summary>
        [Column(Name="PlanId",DbType="char(36)")]
        public string PlanId
        {
            get;
            set;
        }

        /// <summary>
        /// 回访编号
        /// </summary>
        [Column(Name = "VisitId", DbType = "char(36)")]
        public string VisitId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column(Name = "SourceId", DbType = "char(36)")]
        public string SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column(Name = "VisitId", DbType = "nvarchar(50)")]
        public string SourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 得分
        /// </summary>
        [Column(Name = "Score", DbType = "float(8,2)")]
        public float Score
        {
            get;
            set;
        }

        /// <summary>
        /// 总体评价
        /// </summary>
        [Column(Name = "TotalDesc", DbType = "nvarchar(255)")]
        public string TotalDesc
        {
            get;
            set;
        }
        #endregion Model

    }
}
