using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HGysStructure
{
    using EyouSoft.Model.EnumType.GysStructure;

    #region 供应商选用信息业务实体
    /// <summary>
    /// 供应商选用信息业务实体
    /// </summary>
    public class MXuanYongInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongInfo() { }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing GysLeiXing { get; set; }
        /// <summary>
        /// 是否签单
        /// </summary>
        public bool IsQianDan { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 联系人信息集合
        /// </summary>
        public IList<MLxrInfo> Lxrs { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public JieSuanFangShi JieSuanFangShi { get; set; }
        /// <summary>
        /// 流水
        /// </summary>
        public decimal LiuShui { get; set; }
        /// <summary>
        /// 成人人头
        /// </summary>
        public decimal PeopleMoney { get; set; }
        /// <summary>
        /// 儿童人头
        /// </summary>
        public decimal ChildMoney { get; set; }
        /// <summary>
        /// 合同-流水
        /// </summary>
        public decimal HLiuShui { get; set; }
        /// <summary>
        /// 合同-成人人头
        /// </summary>
        public decimal HPeopleMoney { get; set; }
        /// <summary>
        /// 合同-儿童人头
        /// </summary>
        public decimal HChildMoney { get; set; }
    }
    #endregion

    #region 供应商选用查询信息业务实体
    /// <summary>
    /// 供应商选用查询信息业务实体
    /// </summary>
    public class MXuanYongChaXunInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongChaXunInfo() { }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GysName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing? LeiXing { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int? DistrictId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string JingDianName { get; set; }
        /// <summary>
        /// 是否包含联系人信息
        /// </summary>
        public bool IsLxr { get; set; }        
        /// <summary>
        /// 是否包含景点附件信息
        /// </summary>
        public bool IsJingDianFuJian { get; set; }
        /// <summary>
        /// 城市信息集合
        /// </summary>
        public int[] CityIds { get; set; }
        /// <summary>
        /// 县区信息集合
        /// </summary>
        public int[] AreaIds { get; set; }

        public EyouSoft.Model.EnumType.SysStructure.LngType? LngType { get; set; }
        /// <summary>
        /// 酒店星级要求
        /// </summary>
        public int? JiuDianXingJi { get; set; }
    }
    #endregion

    #region 景点选用信息业务实体
    /// <summary>
    /// 景点选用信息业务实体
    /// </summary>
    public class MXuanYongJingDianInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MXuanYongJingDianInfo() { }
        /// <summary>
        /// 景点编号
        /// </summary>
        public string JingDianId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 景点描述
        /// </summary>
        public string MiaoShu { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsTuiJian { get; set; }
        /// <summary>
        /// 景点附件
        /// </summary>
        public MFuJianInfo FuJian { get; set; }
    }
    #endregion
}
