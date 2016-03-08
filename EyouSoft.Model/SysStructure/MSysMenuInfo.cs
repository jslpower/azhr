//汪奇志 2011-09-19
using System;
using System.Collections.Generic;

namespace EyouSoft.Model.SysStructure
{
    #region 系统默认一级栏目信息业务实体
    /// <summary>
    /// 系统默认一级栏目信息业务实体
    /// </summary>
    public class MSysMenu1Info
    {
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 二级栏目信息集合
        /// </summary>
        public IList<MSysMenu2Info> Menu2s { get; set; }
        /// <summary>
        /// 样式名称
        /// </summary>
        public string ClassName { get; set; }
    }
    #endregion

    #region 系统默认二级栏目信息业务实体
    /// <summary>
    /// 系统默认二级栏目信息业务实体
    /// </summary>
    [Serializable]
    public class MSysMenu2Info
    {
        /// <summary>
        /// 一级栏目编号
        /// </summary>
        public int FirstId { get; set; }
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 链接URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 权限信息集合
        /// </summary>
        public IList<MSysPrivsInfo> Privs { get; set; }
    }
    #endregion


    #region 子系统一级栏目信息业务实体
    /// <summary>
    /// 子系统一级栏目信息业务实体
    /// </summary>
    [Serializable]
    public class MComMenu1Info
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public string SysId { get; set; }
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int MenuId { get; set; }        
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 二级栏目信息集合
        /// </summary>
        public IList<MComMenu2Info> Menu2s { get; set; }
        /// <summary>
        /// 样式名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 是否在菜单中显示
        /// </summary>
        public bool IsDisplay { get; set; }
    }
    #endregion

    #region 子系统二级栏目信息业务实体
    /// <summary>
    /// 子系统二级栏目信息业务实体
    /// </summary>
    [Serializable]
    public class MComMenu2Info
    {
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统默认二级栏目编号
        /// </summary>
        public int DefaultMenu2Id { get; set; }
        /// <summary>
        /// 链接URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 权限信息集合
        /// </summary>
        public IList<MSysPrivsInfo> Privs { get; set; }
        /// <summary>
        /// 栏目权限编号(二级栏目下相应“栏目”权限编号)
        /// </summary>
        public int Menu2PrivId { get; set; }
    }
    #endregion


    #region 权限信息业务实体
    /// <summary>
    /// 权限信息业务实体
    /// </summary>
    [Serializable]
    public class MSysPrivsInfo
    {
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        public int SecondId { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary>
        public int PrivsId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限类别
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.PrivsType PrivsType { get; set; }
        /// <summary>
        /// 权限说明
        /// </summary>
        public string Remark { get; set; }
    }
    #endregion


    #region 当前位置信息业务实体
    /// <summary>
    /// 当前位置信息业务实体
    /// </summary>
    [Serializable]
    public class MComLocationInfo
    {
        /// <summary>
        /// 一级栏目编号
        /// </summary>
        public int Menu1Id { get; set; }
        /// <summary>
        /// 一级栏目名称
        /// </summary>
        public string Menu1Name { get; set; }
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        public int Menu2Id { get; set; }
        /// <summary>
        /// 二级栏目名称
        /// </summary>
        public string Menu2Name { get; set; }
        /// <summary>
        /// 系统默认二级栏目编号
        /// </summary>
        public int DefaultMenu2Id { get; set; }
    }
    #endregion
}
