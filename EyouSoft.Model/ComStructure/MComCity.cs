using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ComStructure
{
    #region 国家,省份,城市,县区名称
    /// <summary>
    /// 国家,省份,城市,县区名称
    /// </summary>
    [Serializable]
    public class MCPCC
    {
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 县区名称
        /// </summary>
        public string CountyName { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 县区编号
        /// </summary>
        public int DistrictId { get; set; }
    }
    #endregion
}
