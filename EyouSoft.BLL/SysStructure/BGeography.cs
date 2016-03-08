using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;
using EyouSoft.Cache.Tag;

namespace EyouSoft.BLL.SysStructure
{
    /// <summary>
    /// 国家省份城市县区相关业务逻辑类
    /// </summary>
    public class BGeography
    {
        private readonly EyouSoft.IDAL.SysStructure.IGeography dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SysStructure.IGeography>();

        #region public members
        /// <summary>
        /// 获取国家信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MSysCountry> GetCountrys(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var cacheName = string.Format(TagName.ComCountry, companyId);
            IList<MSysCountry> items = (IList<MSysCountry>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);

            if (items == null)
            {
                items = dal.GetCountrys(companyId);

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, items);
            }

            return items;
        }

        /// <summary>
        /// 获取国家信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public MSysCountry GetCountry(string companyId, int countryId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.CountryId == countryId)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取省份信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public IList<MSysProvince> GetProvinces(string companyId, int countryId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.CountryId == countryId) return item.Provinces;
            }

            return null;
        }

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <returns></returns>
        public MSysProvince GetProvince(string companyId, int provinceId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.Provinces == null) continue;
                foreach (var item1 in item.Provinces)
                {
                    if (item1.ProvinceId == provinceId) return item1;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取城市信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <returns></returns>
        public IList<MSysCity> GetCitys(string companyId, int provinceId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.Provinces == null) continue;
                foreach (var item1 in item.Provinces)
                {
                    if (item1.ProvinceId == provinceId) return item1.Citys;
                }
            }

            return null;
        }
        /// <summary>
        /// 分页获取县区信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="namejp">县区名称简拼</param>
        /// <returns>县区信息集合</returns>
        public IList<MSysDistrict> GetAreas(int pageCurrent, int pageSize, ref int pageCount, string companyId, string namejp)
        {
            var items = GetCountrys(companyId);
            IList<MSysDistrict> list = new List<MSysDistrict>();
            if (!string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(namejp))
            {
                pageCurrent = pageCurrent < 1 ? 1 : pageCurrent;
                list = dal.GetAreas(pageCurrent, pageSize, ref pageCount, companyId, namejp);
            }
            return list;
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public MSysCity GetCity(string companyId, int cityId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.Provinces == null) continue;
                foreach (var item1 in item.Provinces)
                {
                    if (item1.Citys == null) continue;
                    foreach (var item2 in item1.Citys)
                    {
                        if (item2.CityId == cityId) return item2;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取县区信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public IList<MSysDistrict> GetDistricts(string companyId, int cityId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.Provinces == null) continue;
                foreach (var item1 in item.Provinces)
                {
                    if (item1.Citys == null) continue;
                    foreach (var item2 in item1.Citys)
                    {
                        if (item2.CityId == cityId) return item2.Districts;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取县区信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="districtId">县区编号</param>
        /// <returns></returns>
        public MSysDistrict GetDistrict(string companyId, int districtId)
        {
            var items = GetCountrys(companyId);
            if (items == null) return null;

            foreach (var item in items)
            {
                if (item.Provinces == null) continue;
                foreach (var item1 in item.Provinces)
                {
                    if (item1.Citys == null) continue;
                    foreach (var item2 in item1.Citys)
                    {
                        if (item2.Districts == null) continue;
                        foreach (var item3 in item2.Districts)
                        {
                            if (item3.DistrictId == districtId) return item3;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取国家省份城市县区名称
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="districtId">县区编号</param>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MCPCC GetCPCD(string companyId, int countryId, int provinceId, int cityId, int districtId)
        {
            var info = new Model.ComStructure.MCPCC();
            if (string.IsNullOrEmpty(companyId)) return info;
            if (countryId <= 0 && provinceId <= 0 && cityId <= 0 && districtId <= 0) return info;

            IList<Model.SysStructure.MSysCountry> items = GetCountrys(companyId);
            if (items == null || items.Count == 0) return info;

            MSysCountry countryinfo = null;
            MSysProvince provinceinfo = null;
            MSysCity cityinfo = null;
            MSysDistrict districtinfo = null;

            if (countryId > 0)
            {
                foreach (var item in items)
                {
                    if (item.CountryId == countryId)
                    {
                        countryinfo = item;
                        break;
                    }
                }

                if (countryinfo == null) return info;

                info.CountryName = countryinfo.Name;
            }

            if (provinceId > 0)
            {
                if (countryinfo != null && countryinfo.Provinces != null && countryinfo.Provinces.Count > 0)
                {
                    foreach (var item in countryinfo.Provinces)
                    {
                        if (item.ProvinceId == provinceId)
                        {
                            provinceinfo = item;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var item1 in items)
                    {
                        foreach (var item2 in item1.Provinces)
                        {
                            if (item2.ProvinceId == provinceId) { provinceinfo = item2; break; }

                            if (provinceinfo != null) { countryinfo = item1; break; }
                        }
                    }
                }

                if (provinceinfo == null) return info;

                info.CountryName = countryinfo.Name;
                info.ProvinceName = provinceinfo.Name;
            }

            if (cityId > 0)
            {
                if (provinceinfo != null && provinceinfo.Citys != null && provinceinfo.Citys.Count > 0)
                {
                    foreach (var item in provinceinfo.Citys)
                    {
                        if (item.CityId == cityId) { cityinfo = item; break; }
                    }
                }
                else
                {
                    foreach (var item1 in items)
                    {
                        foreach (var item2 in item1.Provinces)
                        {
                            foreach (var item3 in item2.Citys)
                            {
                                if (item3.CityId == cityId) { cityinfo = item3; break; }
                            }
                            if (cityinfo != null) { provinceinfo = item2; break; }
                        }
                        if (provinceinfo != null) { countryinfo = item1; break; }
                    }
                }

                if (cityinfo == null) return info;

                info.CountryName = countryinfo.Name;
                info.ProvinceName = provinceinfo.Name;
                info.CityName = cityinfo.Name;
            }

            if (districtId > 0)
            {
                if (cityinfo != null && cityinfo.Districts != null && cityinfo.Districts.Count > 0)
                {
                    foreach (var item in cityinfo.Districts)
                    {
                        if (item.DistrictId == districtId) { districtinfo = item; break; }
                    }
                }
                else
                {
                    foreach (var item1 in items)
                    {
                        foreach (var item2 in item1.Provinces)
                        {
                            foreach (var item3 in item2.Citys)
                            {
                                foreach (var item4 in item3.Districts)
                                {
                                    if (item4.DistrictId == districtId) { districtinfo = item4; break; }
                                }
                                if (districtinfo != null) { cityinfo = item3; break; }
                            }
                            if (cityinfo != null) { provinceinfo = item2; break; }
                        }
                        if (provinceinfo != null) { countryinfo = item1; break; }
                    }
                }

                if (districtinfo == null) return info;

                info.CountryName = countryinfo.Name;
                info.ProvinceName = provinceinfo.Name;
                info.CityName = cityinfo.Name;
                info.CountyName = districtinfo.Name;
            }

            return info;
        }

        /// <summary>
        /// 新增国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCountry(EyouSoft.Model.SysStructure.MSysCountry info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)) return 0;

            int dalRetCode = dal.InsertCountry(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, info.CompanyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 新增省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertProvince(string companyId, EyouSoft.Model.SysStructure.MSysProvince info)
        {
            if (info == null
                || info.CountryId < 1
                || string.IsNullOrEmpty(companyId)) return 0;

            int dalRetCode = dal.InsertProvince(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 新增城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCity(string companyId, EyouSoft.Model.SysStructure.MSysCity info)
        {
            if (info == null
               || info.ProvinceId < 1) return 0;

            int dalRetCode = dal.InsertCity(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }
        /// <summary>
        /// 新增县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDistrict(string companyId, EyouSoft.Model.SysStructure.MSysDistrict info)
        {
            if (info == null
               || info.CityId < 1) return 0;

            int dalRetCode = dal.InsertDistrict(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }
        /// <summary>
        /// 更新国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCountry(EyouSoft.Model.SysStructure.MSysCountry info)
        {
            if (info == null
                || info.CountryId < 1
                || string.IsNullOrEmpty(info.CompanyId)) return 0;

            int dalRetCode = dal.UpdateCountry(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, info.CompanyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 更新省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateProvince(string companyId, EyouSoft.Model.SysStructure.MSysProvince info)
        {
            if (info == null
               || info.ProvinceId < 1) return 0;

            int dalRetCode = dal.UpdateProvince(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 更新城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCity(string companyId, EyouSoft.Model.SysStructure.MSysCity info)
        {
            if (info == null
              || info.CityId < 1) return 0;

            int dalRetCode = dal.UpdateCity(info);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public int DeleteCountry(string companyId, int countryId)
        {
            if (string.IsNullOrEmpty(companyId)
                || countryId < 1) return 0;

            var info = GetCountry(companyId, countryId);
            if (info != null && info.Provinces != null && info.Provinces.Count > 0) return -1;

            int dalRetCode = dal.DeleteCountry(companyId, countryId);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <returns></returns>
        public int DeleteProvince(string companyId, int provinceId)
        {
            if (string.IsNullOrEmpty(companyId)
                || provinceId < 1) return 0;

            var info = GetProvince(companyId, provinceId);
            if (info != null && info.Citys != null && info.Citys.Count > 0) return -1;

            int dalRetCode = dal.DeleteProvince(companyId, provinceId);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public int DeleteCity(string companyId, int cityId)
        {
            if (string.IsNullOrEmpty(companyId)
                || cityId < 1) return 0;

            var info = GetCity(companyId, cityId);
            if (info != null && info.Districts != null && info.Districts.Count > 0) return -1;

            int dalRetCode = dal.DeleteCity(companyId, cityId);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 删除县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">县区编号</param>
        /// <returns></returns>
        public int DeleteDistrict(string companyId, int[] cityId)
        {
            if (string.IsNullOrEmpty(companyId)
                || cityId.Length < 1) return 0;

            int dalRetCode = dal.DeleteDistrict(companyId, cityId);

            if (dalRetCode == 1)
            {
                string cacheName = string.Format(TagName.ComCountry, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
            }

            return dalRetCode;
        }

        #endregion
    }
}
