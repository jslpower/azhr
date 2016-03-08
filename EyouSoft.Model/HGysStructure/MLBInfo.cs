using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    #region 供应商交易汇总信息业务实体
    /// <summary>
    /// 供应商交易汇总信息业务实体
    /// </summary>
    public class MJiaoYiXXInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MJiaoYiXXInfo() { }
        /// <summary>
        /// 交易次数
        /// </summary>
        public int JiaoYiCiShu { get; set; }
        /// <summary>
        /// 交易数量
        /// </summary>
        public int JiaoYiShuLiang { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal JieSuanJinE { get; set; }
        /// <summary>
        /// 已支付金额
        /// </summary>
        public decimal YiZhiFuJinE { get; set; }
        /// <summary>
        /// 未支付金额
        /// </summary>
        public decimal WeiZhiFuJinE { get { return JieSuanJinE - YiZhiFuJinE; } }
        /// <summary>
        /// 保底数
        /// </summary>
        public int BaoDiShu { get; set; }
    }
    #endregion

    #region 供应商列表信息业务实体
    /// <summary>
    /// 供应商列表信息业务实体
    /// </summary>
    public class MLBInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 国家省份城市县区
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 交易明细
        /// </summary>
        public MJiaoYiXXInfo JiaoYiXX { get; set; }        
        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsQianDan { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing LeiXing { get; set; } 
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<MLxrInfo> Lxrs { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LxrName
        {
            get
            {
                if (Lxrs != null && Lxrs.Count > 0)
                {
                    return Lxrs[0].Name;
                }

                return string.Empty;
            }
        }
        /// <summary>
        /// 发布人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 酒店前台电话
        /// </summary>
        public string JiuDianQianTaiTelephone { get; set; }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi JiuDianXingJi { get; set; }
        /// <summary>
        /// 餐馆菜系
        /// </summary>
        public string CanGuanCaiXi { get; set; }
        /// <summary>
        /// 餐馆餐标
        /// </summary>
        public string CanGuanCanBiao { get; set; }
        /// <summary>
        /// 餐馆-菜单集合
        /// </summary>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> CanGuanCaiDans { get; set; }
        /// <summary>
        /// 餐馆-菜单名称
        /// </summary>
        public string CanGuanCaiDanName
        {
            get
            {
                if (CanGuanCaiDans == null || CanGuanCaiDans.Count == 0) return "暂无菜单";

                return CanGuanCaiDans[0].Name;
            }
        }
        /// <summary>
        /// 车队-车型信息集合
        /// </summary>
        public IList<EyouSoft.Model.HGysStructure.MCheXingInfo> CheDuiCheXings { get; set; }
        /// <summary>
        /// 车队-车型名称
        /// </summary>
        public string CheDuiCheXingName
        {
            get
            {
                if (CheDuiCheXings == null || CheDuiCheXings.Count == 0) return "暂无车型";

                return CheDuiCheXings[0].Name;
            }
        }
        /// <summary>
        /// 购物-商品类别
        /// </summary>
        public string GouWuShangPinLeiBie { get; set; }
    }
    #endregion

    #region 供应商查询信息业务实体
    /// <summary>
    /// 供应商查询信息业务实体
    /// </summary>
    public class MLBChaXunInfo
    {
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing? LeiXing { get; set; }
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
        public int? DistrictId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 餐馆-菜系
        /// </summary>
        public string CanGuanCaiXi { get; set; }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi? JiuDianXingJi { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string JingDianName { get; set; }
    }
    #endregion    

    #region 供应商交易明细信息业务实体
    /// <summary>
    /// 供应商交易明细信息业务实体
    /// </summary>
    public class MJiaoYiMingXiInfo
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 销售员姓名
        /// </summary>
        public string XiaoShouYuanName { get; set; }
        /// <summary>
        /// 计调员姓名
        /// </summary>
        public string JiDiaoYuanName { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouname { get; set; }
        /// <summary>
        /// 交易数量
        /// </summary>
        public int ShuLiang { get; set; }
        /// <summary>
        /// 费用明细
        /// </summary>
        public string FeiYongMingXi { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal JinE { get; set; }
        /// <summary>
        /// 已支付金额
        /// </summary>
        public decimal YiZhiFuJinE { get; set; }
        /// <summary>
        /// 未支付金额
        /// </summary>
        public decimal WeiZhiFuJinE
        {
            get
            {
                return JinE - YiZhiFuJinE;
            }
        }
    }
    #endregion

    #region 供应商交易明细查询信息业务实体
    /// <summary>
    /// 供应商交易明细查询信息业务实体
    /// </summary>
    public class MJiaoYiMingXiChaXunInfo
    {
        /// <summary>
        /// 出团开始时间
        /// </summary>
        public DateTime? LSDate { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? LEDate { get; set; }
    }
    #endregion
}
