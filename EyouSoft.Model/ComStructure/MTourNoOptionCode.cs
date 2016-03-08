using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 团号生成规则项目内容
    /// </summary>
    [Serializable]
    public class MTourNoOptionCode
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int CodeId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 规则项目类型
        /// </summary>
        public EyouSoft.Model.EnumType.ComStructure.OptionItemType ItemType { get; set; }
        /// <summary>
        /// 规则项目编号
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 规则项目编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
