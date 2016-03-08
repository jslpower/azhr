//合同号管理枚举
//2011-09-05 邵权江 创建
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.ConStructure
{
    /// <summary>
    /// 合同类型
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// 国内合同
        /// </summary>
        国内合同 = 0,
        /// <summary>
        /// 境外合同
        /// </summary>
        境外合同 = 1,
        /// <summary>
        /// 单项合同
        /// </summary>
        单项合同 = 2
    }

    /// <summary>
    /// 合同号状态
    /// </summary>
    public enum ContractStatus
    {
        /// <summary>
        /// 未领用
        /// </summary>
        未领用 = 0,
        /// <summary>
        /// 领用
        /// </summary>
        领用,
        /// <summary>
        /// 使用
        /// </summary>
        使用,
        /// <summary>
        /// 销号
        /// </summary>
        销号,
        /// <summary>
        /// 作废
        /// </summary>
        作废
    }
}
