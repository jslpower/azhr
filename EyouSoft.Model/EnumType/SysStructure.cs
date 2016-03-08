//系统相关枚举 汪奇志 2012-03-05
using System;

namespace EyouSoft.Model.EnumType.SysStructure
{
    #region 权限类别
    /// <summary>
    /// 权限类别
    /// </summary>
    public enum PrivsType
    {
        /// <summary>
        /// 其它
        /// </summary>
        其它 = 0,
        /// <summary>
        /// 栏目
        /// </summary>
        栏目,
        /// <summary>
        /// 本部浏览：可以查看自己所在部门内用户的数据（不含下级部门）
        /// </summary>
        本部浏览,
        /// <summary>
        /// 部门浏览：可以查看自己所在部门及下级部门内用户的数据（含下级部门）
        /// </summary>
        部门浏览,
        /// <summary>
        /// 内部浏览：可以查看自己所在部门的第一层级部门及下级部门内用户的数据（包含第一级部门及下级部门）
        /// </summary>
        内部浏览,
        /// <summary>
        /// 查看全部：可以查看所有用户的数据（包含所有第一级部门及下级部门）
        /// </summary>
        查看全部
    }
    #endregion

    #region 语言类别
    /// <summary>
    /// 语言类别
    /// </summary>
    public enum LngType
    {
        /// <summary>
        /// 中文
        /// </summary>
        中文 = 1,
        /// <summary>
        /// 英文
        /// </summary>
        英文 = 2,
        ///// <summary>
        ///// 泰文
        ///// </summary>
        //泰文 = 3,
    }
    #endregion
}
