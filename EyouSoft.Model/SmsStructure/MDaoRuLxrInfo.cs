using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SmsStructure
{
    #region 短信中心导入客户管理号码列表信息业务实体
    /// <summary>
    /// 短信中心导入客户管理号码列表信息业务实体
    /// </summary>
    public class MLBDaoRuLxrInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBDaoRuLxrInfo() { }

        /// <summary>
        /// 国家省份城市县区名称
        /// </summary>
        public EyouSoft.Model.ComStructure.MCPCC CPCD { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string DanWeiName { get; set; }
        /// <summary>
        /// 单位类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType DanWeiType { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LxrName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
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

    #region 短信中心导入客户管理号码列表查询信息业务实体
    /// <summary>
    /// 短信中心导入客户管理号码列表查询信息业务实体
    /// </summary>
    public class MLBDaoRuLxrSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLBDaoRuLxrSearchInfo() { }
        /// <summary>
        /// 单位类型
        /// </summary>
        public EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType? DanWeiType { get; set; }
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
    }
    #endregion
}
