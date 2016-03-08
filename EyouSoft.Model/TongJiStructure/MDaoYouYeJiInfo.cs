using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TongJiStructure
{
    #region 统计分析-导游业绩统计实体
    /// <summary>
    /// 统计分析-导游业绩统计实体
    /// </summary>
    public class MDaoYouYeJiInfo
    {
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
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
        /// 购物成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 购物儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 购物人数
        /// </summary>
        public int GWRS { get { return GWCR + GWET; } }
        /// <summary>
        /// 进店数量
        /// </summary>
        public int JDS { get; set; }
        /// <summary>
        /// 签单比例
        /// </summary>
        public string QDBL
        {
            get
            {
                decimal _QDBL = 0;

                if (RJRS != 0 && JDS != 0)
                {
                    _QDBL = (decimal)GWRS / (decimal)(RJRS * JDS);
                }

                return (_QDBL * 100).ToString("F2") + "%";
            }
        }
        /// <summary>
        /// 购物明细信息集合
        /// </summary>
        public IList<MDaoYouYeJiGWInfo> Gws { get; set; }
    }
    #endregion

    #region 统计分析-导游业绩统计购物明细业务实体
    /// <summary>
    /// 统计分析-导游业绩统计购物明细业务实体
    /// </summary>
    public class MDaoYouYeJiGWInfo
    {
        /// <summary>
        /// 购物店名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 购物成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 购物儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 购物人数
        /// </summary>
        public int GWRS { get { return GWCR + GWET; } }
        /// <summary>
        /// 营业额
        /// </summary>
        public decimal YingYeE { get; set; }
    }
    #endregion

    #region 统计分析-导游业绩统计查询实体
    /// <summary>
    /// 统计分析-导游业绩统计查询实体
    /// </summary>
    public class MDaoYouYeJiChaXunInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string DaoYouId { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 购物店编号集合
        /// </summary>
        public string[] GysIds { get; set; }
    }
    #endregion

    #region 统计分析-导游业绩统计合计实体
    /// <summary>
    /// 统计分析-导游业绩统计合计实体
    /// </summary>
    public class MDaoYouYeJiHeJiInfo
    {
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
        /// 购物成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 购物儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 购物人数
        /// </summary>
        public int GWRS { get { return GWCR + GWET; } }
        /// <summary>
        /// 全社入境人数（单团入境人数*进店次数）
        /// </summary>
        public int QSRJRS { get; set; }
        /// <summary>
        /// 全社签单比例
        /// </summary>
        public string QSQDBL
        {
            get
            {
                decimal _QSQDBL = 0;
                if (QSRJRS != 0)
                {
                    _QSQDBL = (decimal)GWRS / (decimal)QSRJRS;
                }
                return (_QSQDBL * 100).ToString("F2") + "%";
            }
        }
    }
    #endregion

    #region 统计分析-导游业绩排名统计实体
    /// <summary>
    /// 统计分析-导游业绩排名统计实体
    /// </summary>
    public class MDaoYouYeJiPaiMingInfo
    {
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int RS { get; set; }
        /// <summary>
        /// 营业额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 人均
        /// </summary>
        public decimal RJJinE
        {
            get
            {
                if (RS == 0) return 0;
                return JinE / RS;
            }
        }
    }
    #endregion

    #region 统计分析-导游业绩排名统计查询实体
    /// <summary>
    /// 统计分析-导游业绩排名统计查询实体
    /// </summary>
    public class MDaoYouYeJiPaiMingChaXunInfo
    {
        /// <summary>
        /// 抵达起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 抵达截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 购物店编号
        /// </summary>
        public string[] GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 排序方式 0:人均DESC 1:人均ASC 2:营业额DESC 3:营业额ASC
        /// </summary>
        public int PaiXu { get; set; }
    }
    #endregion

    #region 统计分析-导游带团人数排名统计实体
    /// <summary>
    /// 统计分析-导游带团人数排名统计实体
    /// </summary>
    public class MDaoYouDaiTuanInfo
    {
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 带团人数
        /// </summary>
        public int RS { get; set; }
    }
    #endregion

    #region 统计分析-导游带团人数排名统计查询实体
    /// <summary>
    /// 统计分析-导游带团人数排名统计查询实体
    /// </summary>
    public class MDaoYouDaiTuanChaXunInfo
    {
        /// <summary>
        /// 抵达起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 抵达截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 排序方式 0:人数DESC 1:人数ASC
        /// </summary>
        public int PaiXu { get; set; }
    }
    #endregion
}
