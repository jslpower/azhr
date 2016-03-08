using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TongJiStructure
{
    #region 统计分析-人数统计实体
    /// <summary>
    /// 统计分析-人数统计实体
    /// </summary>
    public class MRenShuInfo
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string KeHuName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime RDate { get; set; }
        /// <summary>
        /// 入境成人数
        /// </summary>
        public int RJCR { get; set; }
        /// <summary>
        /// 入境儿童数
        /// </summary>
        public int RJET { get; set; }
        /// <summary>
        /// 入境领队数
        /// </summary>
        public int RJLD { get; set; }
        /// <summary>
        /// 入境人数
        /// </summary>
        public int RJRS { get { return RJCR + RJET + RJLD; } }
        /// <summary>
        /// 来杭人数
        /// </summary>
        public int HZRS { get { return RJRS; } }
        /// <summary>
        /// 来杭天数
        /// </summary>
        public int HZTS { get; set; }
        /// <summary>
        /// 来杭人天数
        /// </summary>
        public int HZRTS { get { return HZRS * HZTS; } }
        /// <summary>
        /// 来浙人数
        /// </summary>
        public int ZJRS { get { return RJRS; } }
        /// <summary>
        /// 来浙天数
        /// </summary>
        public int ZJTS { get; set; }
        /// <summary>
        /// 来浙人天数
        /// </summary>
        public int ZJRTS { get { return ZJRS * ZJTS; } }
        /// <summary>
        /// 行程天数
        /// </summary>
        public int TS { get; set; }
        /// <summary>
        /// 来华人天数
        /// </summary>
        public int RTS { get { return RJRS * TS; } }
    }
    #endregion

    #region 统计分析-人数统计查询实体
    /// <summary>
    /// 统计分析-人数统计查询实体
    /// </summary>
    public class MRenShuChaXunInfo
    {
        /// <summary>
        /// 出团起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int? DeptId { get; set; }
    }
    #endregion
}
