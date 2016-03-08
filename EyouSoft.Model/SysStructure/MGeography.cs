using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SysStructure
{
    #region 国家、省份、城市、县区基类
    /// <summary>
    /// 国家、省份、城市、县区基类
    /// </summary>
    public abstract class MGeography
    {
        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拼音简拼
        /// </summary>
        public string JP { get; set; }
        /// <summary>
        /// 拼音全拼
        /// </summary>
        public string QP { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 泰文名称
        /// </summary>
        public string ThName { get; set; }
        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
    #endregion

    #region 国家信息业务实体
    /// <summary>
    /// 国家信息业务实体
    /// </summary>
    [Serializable]
    public class MSysCountry : MGeography
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份集合
        /// </summary>
        public IList<MSysProvince> Provinces { get; set; }
    }

    #endregion

    #region 省份信息业务实体
    /// <summary>
    /// 省份信息业务实体
    /// </summary>
    [Serializable]
    public class MSysProvince : MGeography
    {
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 城市集合
        /// </summary>
        public IList<MSysCity> Citys { get; set; }
    }
    #endregion

    #region 城市信息业务实体
    /// <summary>
    /// 城市信息业务实体
    /// </summary>
    [Serializable]
    public class MSysCity : MGeography
    {
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 县区集合
        /// </summary>
        public IList<MSysDistrict> Districts { get; set; }
    }
    #endregion

    #region 县区信息业务实体
    /// <summary>
    /// 县区信息业务实体
    /// </summary>
    [Serializable]
    public class MSysDistrict : MGeography
    {
        /// <summary>
        /// 县区编号
        /// </summary>
        public int DistrictId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
    }
    #endregion
}