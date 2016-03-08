using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TongJiStructure
{
    #region 统计分析-人天数统计实体
    /// <summary>
    /// 统计分析-人天数统计实体
    /// </summary>
    public class MRenTianInfo
    {
        /// <summary>
        /// 国籍
        /// </summary>
        public string GuoJi { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
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
        /// 到时间
        /// </summary>
        public DateTime XCSTime { get; set; }
        /// <summary>
        /// 离时间
        /// </summary>
        public DateTime XCETime { get { return XCSTime.AddDays(1); } }
        /// <summary>
        /// 住宿晚数
        /// </summary>
        public int ZhuSuWanShu { get; set; }
        /// <summary>
        /// 人天数
        /// </summary>
        public int RTS { get { return RJRS * 1; } }
        /// <summary>
        /// 入境航班
        /// </summary>
        public string RHangBan { get; set; }
        /// <summary>
        /// 出境航班
        /// </summary>
        public string CHangBan { get; set; }
        /// <summary>
        /// 入住酒店名称
        /// </summary>
        public string JiuDianName { get; set; }
        /// <summary>
        /// 领队姓名
        /// </summary>
        public string LingDuiName { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 县区名称
        /// </summary>
        public string CountyName { get; set; }
    }
    #endregion

    #region 统计分析-人天数统计查询实体
    /// <summary>
    /// 统计分析-人天数统计查询实体
    /// </summary>
    public class MRenTianChaXunInfo
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int? DeptId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// 住宿晚数操作符
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.EqualSign? WanShu1 { get; set; }
        /// <summary>
        /// 住宿晚数操作数
        /// </summary>
        public int? WanShu2 { get; set; }
    }
    #endregion
}
