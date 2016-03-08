using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 供应商-景点价格信息业务实体
    /// <summary>
    /// 供应商-景点价格信息业务实体
    /// </summary>
    public class MJingDianJiaGeInfo
    {
        /// <summary>
        /// 价格编号
        /// </summary>
        public string JiaGeId { get; set; }
        /// <summary>
        /// 景点编号
        /// </summary>
        public string JingDianId { get; set; }
        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing TuanXing { get; set; }
        /// <summary>
        /// 宾客类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CustomType BinKeLeiXing { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 门市价
        /// </summary>
        public decimal JiaGeMS { get; set; }
        /// <summary>
        /// 同行价
        /// </summary>
        public decimal JiaGeTH { get; set; }
        /// <summary>
        /// 结算价
        /// </summary>
        public decimal JiaGeJS { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal JiaGeET { get; set; }
        /// <summary>
        /// 家庭价
        /// </summary>
        public decimal JiaGeJT { get; set; }
    }
    #endregion

    #region  供应商-车型价格信息业务实体
    /// <summary>
    /// 供应商-车型价格信息业务实体
    /// </summary>
    public class MCheXingJiaGeInfo
    {
        /// <summary>
        /// 价格编号
        /// </summary>
        public string JiaGeId { get; set; }
        /// <summary>
        /// 宾客类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CustomType BinKeLeiXing { get; set; }
        /// <summary>
        /// 车型编号
        /// </summary>
        public string CheXingId { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 结算价
        /// </summary>
        public decimal JiaGeJS { get; set; }
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

    #region 供应商-餐馆菜单信息业务实体
    /// <summary>
    /// 供应商-餐馆菜单信息业务实体
    /// </summary>
    public class MCanGuanCaiDanInfo
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string CaiDanId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单内容
        /// </summary>
        public string NeiRong { get; set; }
        /// <summary>
        /// 价格-桌-门市
        /// </summary>
        public decimal JiaGeZMS { get; set; }
        /// <summary>
        /// 价格-桌-同行
        /// </summary>
        public decimal JiaGeZTH { get; set; }
        /// <summary>
        /// 价格-桌-结算
        /// </summary>
        public decimal JiaGeZJS { get; set; }
        /// <summary>
        /// 价格-人-门市
        /// </summary>
        public decimal JiaGeRMS { get; set; }
        /// <summary>
        /// 价格-人-同行
        /// </summary>
        public decimal JiaGeRTH { get; set; }
        /// <summary>
        /// 价格-人-结算
        /// </summary>
        public decimal JiaGeRJS { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public EyouSoft.Model.EnumType.SysStructure.LngType LngType { get; set; }
        /// <summary>
        /// 人-免餐 M免N M
        /// </summary>
        public int TMianM { get; set; }
        /// <summary>
        /// 人-免餐 M免N N
        /// </summary>
        public int TMianN { get; set; }
        /// <summary>
        /// 是否显示【0：不显示 1：显示】
        /// </summary>
        public bool IsDisplay { get; set; }
    }
    #endregion

    #region 供应商-酒店价格信息业务实体
    /// <summary>
    /// 供应商-酒店价格信息业务实体
    /// </summary>
    public class MJiuDianJiaGeInfo
    {
        /// <summary>
        /// 价格编号
        /// </summary>
        public string JiaGeId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 宾客类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CustomType BinKeLeiXing { get; set; }
        /// <summary>
        /// 房型编号
        /// </summary>
        public string FangXingId { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string FangXingName { get; set; }
        /// <summary>
        /// 是否含早
        /// </summary>
        public bool IsHanZao { get; set; }
        /// <summary>
        /// 早餐价格
        /// </summary>
        public decimal JiaGeZC { get; set; }
        /// <summary>
        /// 门市价格
        /// </summary>
        public decimal JiaGeMS { get; set; }
        /// <summary>
        /// 结算价格-团队
        /// </summary>
        public decimal JiaGeTJS { get; set; }
        /// <summary>
        /// 结算价格-散客
        /// </summary>
        public decimal JiaGeSJS { get; set; }
        /// <summary>
        /// 服务费-团队
        /// </summary>
        public decimal JiaGeTFW { get; set; }
        /// <summary>
        /// 服务费-散客
        /// </summary>
        public decimal JiaGeSFW { get; set; }
        /// <summary>
        /// 团队-免房 M免N M
        /// </summary>
        public int TMianM { get; set; }
        /// <summary>
        /// 团队-免房 M免N N
        /// </summary>
        public decimal TMianN { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ETime { get; set; }
        /// <summary>
        /// 星期
        /// </summary>
        public IList<DayOfWeek> XingQi { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BeiZhu { get; set; }
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

    #region 供应商价格查询实体
    /// <summary>
    /// 供应商价格查询实体
    /// </summary>
    public class MJiaGeChaXunInfo
    {
        /// <summary>
        /// 宾客类型
        /// </summary>
        public EyouSoft.Model.EnumType.CrmStructure.CustomType? BinKeLeiXing { get; set; }
        /// <summary>
        /// 团型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing? TuanXing { get; set; }
        /// <summary>
        /// 时间1
        /// </summary>
        public DateTime? Time1 { get; set; }
        /// <summary>
        /// 时间2
        /// </summary>
        public DateTime? Time2 { get; set; }
    }
    #endregion
}
