using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 国家省份城市县区相关数据访问类接口
    /// </summary>
    public interface IGeography
    { 
        /// <summary>
        /// 获取国家信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MSysCountry> GetCountrys(string companyId);
        /// <summary>
        /// 分页获取县区信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="namejp">县区名称简拼</param>
        /// <returns>县区信息集合</returns>
        IList<MSysDistrict> GetAreas(int pageCurrent, int pageSize, ref int pageCount, string companyId, string namejp);
        /// <summary>
        /// 新增国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertCountry(EyouSoft.Model.SysStructure.MSysCountry info);
        /// <summary>
        /// 新增省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertProvince(EyouSoft.Model.SysStructure.MSysProvince info);
        /// <summary>
        /// 新增城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertCity(EyouSoft.Model.SysStructure.MSysCity info);
        /// <summary>
        /// 新增县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertDistrict(EyouSoft.Model.SysStructure.MSysDistrict info);
        /// <summary>
        /// 更新国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateCountry(EyouSoft.Model.SysStructure.MSysCountry info);
        /// <summary>
        /// 更新省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateProvince(EyouSoft.Model.SysStructure.MSysProvince info);
        /// <summary>
        /// 更新城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateCity(EyouSoft.Model.SysStructure.MSysCity info);
        /// <summary>
        /// 更新县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateDistrict(EyouSoft.Model.SysStructure.MSysDistrict info);
        /// <summary>
        /// 删除国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        int DeleteCountry(string companyId, int countryId);
        /// <summary>
        /// 删除省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <returns></returns>
        int DeleteProvince(string companyId, int provinceId);
        /// <summary>
        /// 删除城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        int DeleteCity(string companyId, int cityId);
        /// <summary>
        /// 删除县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="districtId">县区编号</param>
        /// <returns></returns>
        int DeleteDistrict(string companyId, int[] districtId);
    }
}
