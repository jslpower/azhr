using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TongJiStructure
{
    #region  统计分析-利润统计信息业务实体
    /// <summary>
    /// 统计分析-利润统计信息业务实体
    /// </summary>
    public class MLiRunInfo
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
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
        /// 购物成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 购物儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string KeHuName { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal YingShouJinE { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal YingFuJinE { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal MaoLi { get { return YingShouJinE - YingFuJinE; } }
        /// <summary>
        /// 人均毛利
        /// </summary>
        public decimal RenJunMaoLi
        {
            get
            {
                int renShu = RJCR + RJET + RJLD;
                if (renShu == 0) return 0;
                return MaoLi / renShu;
            }
        }
        /// <summary>
        /// 购物返利
        /// </summary>
        public decimal GWFanLi { get; set; }
        public bool IsTax { get; set; }
    }
    #endregion

    #region 统计分析-利润统计查询信息业务实体
    /// <summary>
    /// 统计分析-利润统计查询信息业务实体
    /// </summary>
    public class MLiRunChaXunInfo
    {
        /// <summary>
        /// 抵达开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 抵达截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 销售员部门编号
        /// </summary>
        public int? XiaoShouYuanDeptId { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        public string XiaoShouYuanId { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 客户单位编号
        /// </summary>
        public string KeHuId { get; set; }
        /// <summary>
        /// 客户单位名称
        /// </summary>
        public string KeHuName { get; set; }
        /// <summary>
        /// 毛利操作符
        /// </summary>
        public EyouSoft.Model.EnumType.FinStructure.EqualSign? MaoLi1 { get; set; }
        /// <summary>
        /// 毛利操作数
        /// </summary>
        public decimal? MaoLi2 { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int? AreaId { get; set; }
    }
    #endregion

    #region 统计分析-利润统计合计信息业务实体
    /// <summary>
    /// 统计分析-利润统计合计信息业务实体
    /// </summary>
    public class MLiRunHeJiInfo
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
        /// 签单成人数
        /// </summary>
        public int GWCR { get; set; }
        /// <summary>
        /// 签单儿童数
        /// </summary>
        public int GWET { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal YingShouJinE { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal YingFuJinE { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal MaoLi { get { return YingShouJinE - YingFuJinE; } }
        /// <summary>
        /// 人均毛利
        /// </summary>
        public decimal RenJunMaoLi
        {
            get
            {
                int renShu = RJCR + RJET + RJLD;
                if (renShu == 0) return 0;
                return MaoLi / renShu;
            }
        }
        /// <summary>
        /// 购物返利
        /// </summary>
        public decimal GWFanLi { get; set; }
    }
    #endregion 统计分析-利润统计合计信息业务实体
}
