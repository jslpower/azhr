using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TongJiStructure
{
    #region 统计分析-入境目录表信息业务实体
    /// <summary>
    /// 统计分析-入境目录表信息业务实体
    /// </summary>
    public class MRuJingInfo
    {
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
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
        /// 行程天数
        /// </summary>
        public int TianShu { get; set; }
        /// <summary>
        /// 浏览城市
        /// </summary>
        public string YouLanChengShi { get; set; }
        /// <summary>
        /// 全陪
        /// </summary>
        public string QuanPeiName { get; set; }
        /// <summary>
        /// 页码号
        /// </summary>
        public string YeMaHao { get; set; }
        /// <summary>
        /// 装订序号
        /// </summary>
        public string ZhuangDingXuHao { get; set; }
        /// <summary>
        /// 入境人数
        /// </summary>
        public int RJRS { get { return RJCR + RJET + RJLD; } }
        /// <summary>
        /// 行程安排
        /// </summary>
        public string XingChengAnPai
        {
            get { return string.Format("{0}N{1}D", TianShu - 1, TianShu); }
        }
    }
    #endregion

    #region 统计分析-入境目录表查询信息业务实体
    /// <summary>
    /// 统计分析-入境目录表查询信息业务实体
    /// </summary>
    public class MRuJingChaXunInfo
    {
        /// <summary>
        /// 出团开始时间
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
