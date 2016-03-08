using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 物品信息业务实体
    /// <summary>
    /// 物品信息业务实体
    /// </summary>
    public class MWuPinInfo
    {
        /// <summary>
        /// 物品编号
        /// </summary>
        public string WuPinId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量-入库
        /// </summary>
        public int ShuLiangRK { get; set; }
        /// <summary>
        /// 物品单价
        /// </summary>
        public decimal DanJia { get; set; }        
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime RuKuTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string YongTu { get; set; }
        /// <summary>
        /// 入库人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 入库人姓名
        /// </summary>
        public string OperatorName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 物品数量信息(OUTPUT)
        /// </summary>
        public MWuPinShuLiangInfo ShuLiang { get; set; }
    }
    #endregion

    #region 物品查询信息业务实体
    /// <summary>
    /// 物品查询信息业务实体
    /// </summary>
    public class MWuPinChaXunInfo
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 入库开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 入库截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
    }
    #endregion

    #region 物品领用、发放、借阅信息业务实体
    /// <summary>
    /// 物品领用、发放、借阅信息业务实体
    /// </summary>
    public class MWuPinLingYongInfo
    {
        /// <summary>
        /// 领用、发放、借阅编号
        /// </summary>
        public string LingYongId { get; set; }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string WuPinId { get; set; }
        /// <summary>
        /// 领用、发放、借阅类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing LingYongLeiXing { get; set; }
        /// <summary>
        /// 领用、发放、借阅时间
        /// </summary>
        public DateTime ShiJian { get; set; }
        /// <summary>
        /// 领用、发放、借阅数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 领用、发放、借阅人编号
        /// </summary>
        public string LingYongRenId { get; set; }        
        /// <summary>
        /// 用途
        /// </summary>
        public string YongTu { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion

    #region 物品领用、发放、借阅列表信息业务实体
    /// <summary>
    /// 物品领用、发放、借阅列表信息业务实体
    /// </summary>
    public class MWuPinLingYongLBInfo
    {
        /// <summary>
        /// 领用、发放、借阅编号
        /// </summary>
        public string LingYongId { get; set; }
        /// <summary>
        /// 领用、发放、借阅时间
        /// </summary>
        public DateTime ShiJian { get; set; }
        /// <summary>
        /// 领用、发放、借阅数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 领用、发放、借阅人编号
        /// </summary>
        public string LingYongRenId { get; set; }
        /// <summary>
        /// 领用、发放、借阅人姓名
        /// </summary>
        public string LingYongRenName { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string YongTu { get; set; }
        /// <summary>
        /// 物品单价
        /// </summary>
        public decimal DanJia { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal ZongJia { get { return DanJia * ShuLiang; } }
        /// <summary>
        /// 借阅状态
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus JieYueStatus { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
    }
    #endregion

    #region 物品数量信息业务实体
    /// <summary>
    /// 物品数量信息业务实体
    /// </summary>
    public class MWuPinShuLiangInfo
    {
        /// <summary>
        /// 入库数量
        /// </summary>
        public int RuKu { get; set; }
        /// <summary>
        /// 领用数量
        /// </summary>
        public int LingYong { get; set; }
        /// <summary>
        /// 发放数量
        /// </summary>
        public int FaFang { get; set; }
        /// <summary>
        /// 借阅数量-未归还
        /// </summary>
        public int JieYue1 { get; set; }
        /// <summary>
        /// 借阅数量-已归还
        /// </summary>
        public int JieYue2 { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int KuCun { get { return RuKu - LingYong - FaFang - JieYue1; } }
    }
    #endregion
}
