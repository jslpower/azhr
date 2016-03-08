using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司支付方式
    /// </summary>
    [Serializable]
    public class MComPayment
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int PaymentId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 款项来源
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.SourceType SourceType { get; set; }
        /// <summary>
        /// 收入支出类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ItemType ItemType { get; set; }
        /// <summary>
        /// 是否系统定义
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 银行账户编号
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 支付方式类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.ZhiFuFangShiLeiXing LeiXing { get; set; }
    }
}
