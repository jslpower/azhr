using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 车型表实体
    /// </summary>
    [Serializable]
    public class MComCarType
    {
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CarTypeId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string CarTypeName { get; set; }
        /// <summary>
        /// 座位数量
        /// </summary>
        public int SeatNum { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 座位编号
        /// </summary>
        public IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> CarTypeSeatList { get; set; }
    }

}
