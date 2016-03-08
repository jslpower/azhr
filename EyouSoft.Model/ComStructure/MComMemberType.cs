using System;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 会员类型实体
    /// 修改记录:
    /// 1、2012-03-22 曹胡生 创建
    /// </summary>
    public class MComMemberType
    {
        /// <summary>
        /// 会员类型编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 会员类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
