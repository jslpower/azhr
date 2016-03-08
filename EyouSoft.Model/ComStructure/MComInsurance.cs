using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 保险实体
    /// 修改记录:
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public class MComInsurance
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 保险编号
        /// </summary>
        public string InsuranceId { get; set; }
        /// <summary>
        /// 保险名称
        /// </summary>
        public string InsuranceName { get; set; }
        /// <summary>
        /// 保险单价
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
