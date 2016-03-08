using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    /// <summary>
    /// 公司角色
    /// </summary>
    [Serializable]
    public class MComRole
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色权限集合
        /// </summary>
        public string RoleChilds { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否系统角色
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
