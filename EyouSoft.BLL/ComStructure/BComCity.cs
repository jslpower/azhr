using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.BLL.ComStructure
{
    ///// <summary>
    ///// 公司常用城市业务层
    ///// 创建者：郑付杰
    ///// 创建时间：2011/9/30
    ///// </summary>
    //public class BComCity
    //{
    //    private readonly EyouSoft.IDAL.ComStructure.IComCity dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComCity>();
    //    private readonly EyouSoft.BLL.SysStructure.BGeography bll = new EyouSoft.BLL.SysStructure.BGeography();
         
    //    public BComCity()
    //    {

    //    }

    //    #region IComCity 成员
    //    /*/// <summary>
    //    /// 设置常用城市集合(暂时不提供使用)
    //    /// </summary>
    //    /// <param name="list">常用城市</param>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>true：设置成功 false：设置失败</returns>
    //    public bool SetCity(IList<MComCity> list, string companyId)
    //    {
    //        bool result = false;
    //        if (!string.IsNullOrEmpty(companyId) && list != null && list.Count > 0)
    //        {
    //            result = dal.SetCity(list,companyId);
    //            //删除缓存
    //            string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComCity, companyId);
    //            if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName) != null)
    //            {
    //                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
    //            }
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 设置常用城市
    //    /// </summary>
    //    /// <param name="cityId">城市编号</param>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>true：设置成功 false：设置失败</returns>
    //    public bool SetCity(int cityId, string companyId)
    //    {
    //        bool result = false;
    //        if (cityId > 0 && !string.IsNullOrEmpty(companyId))
    //        {
    //            result = dal.SetCity(cityId, companyId);
    //            //删除缓存
    //            string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComCity, companyId);
    //            if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName) != null)
    //            {
    //                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
    //            }
    //        }
    //        return result;
    //    }

    //    /// <summary>
    //    /// 获取公司常用国家省份城市县区
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns></returns>
    //    private IList<MSysCountry> GetAllCity(string companyId)
    //    {
    //        if (string.IsNullOrEmpty(companyId)) return null;

    //        //缓存中存在数据，则缓存中获取，否则表里获取，再添加到缓存
    //        string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComCity, companyId);
    //        IList<MSysCountry> list = (IList<MSysCountry>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);

    //        if (list == null)
    //        {
    //            list = dal.GetAllCity(companyId);
    //            if (list != null && list.Count > 0)
    //            {
    //                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, list);
    //            }
    //            else
    //            {
    //                list = new EyouSoft.BLL.SysStructure.BGeography().GetAllList();
    //            }
    //        }

    //        return list;
    //    }
    //    /// <summary>
    //    /// 获取公司常用城市编号
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>成员城市编号集合</returns>
    //    public IList<int> GetCityId(string companyId)
    //    {
    //        IList<int> list = null;
    //        if (!string.IsNullOrEmpty(companyId))
    //        {
    //            list = dal.GetCityId(companyId);
    //        }
    //        return list;
    //    }
    //    /// <summary>
    //    /// 获取公司常用国家
    //    /// </summary>
    //    /// <param name="companyId"></param>
    //    /// <returns></returns>
    //    public IList<MSysCountry> GetCountry(string companyId)
    //    {
    //        IList<MSysCountry> list_country = null;
    //        if (!string.IsNullOrEmpty(companyId))
    //        {
    //            IList<MSysCountry> list = GetAllCity(companyId);
    //            if (list.Count > 0)
    //            {
    //                list_country = new List<MSysCountry>();
    //                MSysCountry item = null;
    //                foreach (MSysCountry sc in list)
    //                {
    //                    list_country.Add(item = new MSysCountry()
    //                    {
    //                        JP = sc.JP,
    //                        QP = sc.QP,
    //                        CompanyId = companyId,
    //                        CountryId = sc.CountryId,
    //                        EnName = sc.EnName,
    //                        Name = sc.Name
    //                    });
    //                }
    //            }
    //        }
    //        return list_country;
    //    }
    //    /// <summary>
    //    /// 获取公司常用省份
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <param name="countryId">国家编号</param>
    //    /// <returns>省份集合</returns>
    //    public IList<MSysProvince> GetProvince(int countryId,string companyId)
    //    {
    //        IList<MSysProvince> list_province = null;
    //        if (!string.IsNullOrEmpty(companyId))
    //        {
    //            IList<MSysCountry> list = GetAllCity(companyId);
    //            if (list.Count > 0)
    //            {
    //                list_province = new List<MSysProvince>();
    //                MSysProvince item = null;
    //                foreach (MSysProvince sp in list.SingleOrDefault(c => c.CountryId == countryId).Provinces)
    //                {
    //                    list_province.Add(item = new MSysProvince()
    //                    {
    //                        CountryId = countryId,
    //                        EnName = sp.EnName,
    //                        JP = sp.JP,
    //                        QP = sp.QP,
    //                        Name = sp.Name,
    //                        ProvinceId = sp.ProvinceId
    //                    });
    //                }
    //            }
    //        }
    //        return list_province;
    //    }
    //    /// <summary>
    //    /// 获取公司常用的省份城市
    //    /// </summary>
    //    /// <param name="provinceId"></param>
    //    /// <param name="companyId"></param>
    //    /// <returns>城市集合</returns>
    //    public IList<MSysCity> GetCity(int provinceId, string companyId)
    //    {
    //        IList<MSysCity> list_city = null;
    //        if (!string.IsNullOrEmpty(companyId))
    //        {
    //            IList<MSysCountry> list = GetAllCity(companyId);
    //            if (list.Count > 0)
    //            {
    //                list_city = new List<MSysCity>();
    //                MSysCity item = null;
    //                int countryId ;

    //                foreach (MSysCountry country in list)
    //                {
    //                    if (country.Provinces.SingleOrDefault(p => p.ProvinceId == provinceId) != null)
    //                    {
    //                        //找出国家编号
    //                        countryId = country.Provinces.Single(p => p.ProvinceId == provinceId).CountryId;
    //                        foreach (MSysCity sc in list.Single(c => c.CountryId == countryId)
    //                           .Provinces.Single(p => p.ProvinceId == provinceId).Citys)
    //                        {
    //                            list_city.Add(item = new MSysCity()
    //                            {
    //                                CityId = sc.CityId,
    //                                EnName = sc.EnName,
    //                                JP = sc.JP,
    //                                QP = sc.QP,
    //                                Name = sc.Name,
    //                                ProvinceId = provinceId
    //                            });
    //                        }
    //                        break;
    //                    }
    //                }
                   
    //            }
    //        }
    //        return list_city;
    //    }
    //    /// <summary>
    //    /// 获取公司常用的县区
    //    /// </summary>
    //    /// <param name="cityId">城市编号</param>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>县区集合</returns>
    //    public IList<MSysDistrict> GetDistrict(int cityId, string companyId)
    //    {
    //        IList<MSysDistrict> list_district = null;
    //        if (!string.IsNullOrEmpty(companyId))
    //        {
    //            IList<MSysCountry> list = GetAllCity(companyId);
    //            if (list.Count > 0)
    //            {
    //                list_district = new List<MSysDistrict>();
    //                int countryId;
    //                int provinceId;
    //                bool isGn = false;
    //                foreach (MSysCountry country in list)
    //                {
    //                    foreach (MSysProvince province in country.Provinces)
    //                    {
    //                        if (province.Citys.SingleOrDefault(c => c.CityId == cityId) != null)
    //                        {
    //                            provinceId = province.Citys.Single(c => c.CityId == cityId).ProvinceId;
    //                            countryId = country.Provinces.Single(p => p.ProvinceId == provinceId).CountryId;
    //                            list_district = list.Single(c => c.CountryId == countryId)
    //                                .Provinces.Single(p => p.ProvinceId == provinceId)
    //                                .Citys.Single(c => c.CityId == cityId).Districts;
    //                            isGn = true;
    //                            break;
    //                        }
    //                    }
    //                    if (!isGn)
    //                    {
    //                        continue;
    //                    }
    //                    else
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //        return list_district;
    //    }

    //    ///// <summary>
    //    ///// 获得国家,省份,城市,县区名称
    //    ///// </summary>
    //    ///// <param name="id">编号</param>
    //    ///// <param name="companyId">公司编号</param>
    //    ///// <param name="type">类型</param>
    //    ///// <returns>国家,省份,城市,县区名称Model</returns>
    //    //public Model.ComStructure.MCPCC GetName(int id, string companyId, Model.EnumType.ComStructure.SelectNameType type)
    //    //{
    //    //    Model.ComStructure.MCPCC model = new Model.ComStructure.MCPCC();
    //    //    IList<Model.SysStructure.MSysCountry> list=GetAllCity(companyId);
    //    //    if (list == null||id==0||string.IsNullOrEmpty(companyId))
    //    //        return model;
    //    //    int countryId = 0;
    //    //    int provinceId = 0;
    //    //    int cityId = 0;
    //    //    switch (type)
    //    //    {
    //    //        case Model.EnumType.ComStructure.SelectNameType.国家:
    //    //            model.CountryName=list.SingleOrDefault(item1 => item1.CountryId == id).Name;
    //    //            break;
    //    //        case Model.EnumType.ComStructure.SelectNameType.省份:
    //    //            countryId = list.SingleOrDefault(item1 => item1.Provinces.SingleOrDefault(item2 => item2.ProvinceId == id) != null).CountryId;
    //    //            model.CountryName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Name;
    //    //            model.ProvinceName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.ProvinceId == id).Name;
    //    //            break;
    //    //        case Model.EnumType.ComStructure.SelectNameType.城市:
    //    //            countryId = list.SingleOrDefault(item1 => item1.Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.CityId == id) != null) != null).CountryId;
    //    //            model.CountryName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Name;
    //    //            provinceId = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.CityId == id) != null).ProvinceId;
    //    //            model.ProvinceName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.CityId == id) != null).Name;
    //    //            model.CityName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.CityId == id) != null).Name;
    //    //            break;
    //    //        case Model.EnumType.ComStructure.SelectNameType.县区:
    //    //            countryId = list.SingleOrDefault(item1 => item1.Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.Districts.SingleOrDefault(item4 => item4.DistrictId == id) != null) != null) != null).CountryId;
    //    //            model.CountryName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Name;
    //    //            provinceId = list.SingleOrDefault(item1=>item1.CountryId==countryId).Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.Districts.SingleOrDefault(item4 => item4.DistrictId == id) != null) != null).ProvinceId;
    //    //            model.ProvinceName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.Citys.SingleOrDefault(item3 => item3.Districts.SingleOrDefault(item4 => item4.DistrictId == id) != null) != null).Name;
    //    //            cityId = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.ProvinceId == provinceId).Citys.SingleOrDefault(item3 => item3.Districts.SingleOrDefault(item4 => item4.DistrictId == id) != null).CityId;
    //    //            model.CityName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.ProvinceId == provinceId).Citys.SingleOrDefault(item3 => item3.Districts.SingleOrDefault(item4 => item4.DistrictId == id) != null).Name;
    //    //            model.CountyName = list.SingleOrDefault(item1 => item1.CountryId == countryId).Provinces.SingleOrDefault(item2 => item2.ProvinceId == provinceId).Citys.SingleOrDefault(item3=>item3.CityId==cityId).Districts.SingleOrDefault(item4=>item4.DistrictId==id).Name;
    //    //            break;
    //    //    }
    //    //    return model;
    //    //}

    //    /// <summary>
    //    /// 获取国家省份城市县区名称
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <param name="countryId">国家编号</param>
    //    /// <param name="provinceId">省份编号</param>
    //    /// <param name="cityId">城市编号</param>
    //    /// <param name="districtId">县区编号</param>
    //    /// <returns></returns>
    //    public EyouSoft.Model.ComStructure.MCPCC GetCPCD(string companyId, int countryId, int provinceId, int cityId, int districtId)
    //    {
    //        var info = new Model.ComStructure.MCPCC();
    //        if (string.IsNullOrEmpty(companyId)) return info;
    //        if (countryId <= 0 && provinceId <= 0 && cityId <= 0 && districtId <= 0) return info;

    //        IList < Model.SysStructure.MSysCountry > items = GetAllCity(companyId);
    //        if (items == null || items.Count == 0) return info;

    //        MSysCountry countryinfo = null;
    //        MSysProvince provinceinfo = null;
    //        MSysCity cityinfo = null;
    //        MSysDistrict districtinfo = null;

    //        if (countryId > 0)
    //        {
    //            foreach (var item in items)
    //            {
    //                if (item.CountryId == countryId)
    //                {
    //                    countryinfo = item;
    //                    break;
    //                }
    //            }

    //            if (countryinfo == null) return info;

    //            info.CountryName = countryinfo.Name;
    //        }

    //        if (provinceId > 0)
    //        {
    //            if (countryinfo != null && countryinfo.Provinces != null && countryinfo.Provinces.Count > 0)
    //            {
    //                foreach (var item in countryinfo.Provinces)
    //                {
    //                    if (item.ProvinceId == provinceId)
    //                    {
    //                        provinceinfo = item;
    //                        break;
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                foreach (var item1 in items)
    //                {
    //                    foreach (var item2 in item1.Provinces)
    //                    {
    //                        if (item2.ProvinceId == provinceId) { provinceinfo = item2;break;}

    //                        if (provinceinfo != null) { countryinfo = item1; break; }
    //                    }
    //                }
    //            }

    //            if (provinceinfo == null) return info;

    //            info.CountryName = countryinfo.Name;
    //            info.ProvinceName = provinceinfo.Name;
    //        }

    //        if (cityId > 0)
    //        {
    //            if (provinceinfo != null && provinceinfo.Citys != null && provinceinfo.Citys.Count > 0)
    //            {
    //                foreach (var item in provinceinfo.Citys)
    //                {
    //                    if (item.CityId == cityId) { cityinfo = item; break; }
    //                }
    //            }
    //            else
    //            {
    //                foreach (var item1 in items)
    //                {
    //                    foreach (var item2 in item1.Provinces)
    //                    {
    //                        foreach (var item3 in item2.Citys)
    //                        {
    //                            if (item3.CityId == cityId) { cityinfo = item3; break; }
    //                        }
    //                        if (cityinfo != null) { provinceinfo = item2; break; }
    //                    }
    //                    if (provinceinfo != null) { countryinfo = item1; break; }
    //                }
    //            }

    //            if (cityinfo == null) return info;

    //            info.CountryName = countryinfo.Name;
    //            info.ProvinceName = provinceinfo.Name;
    //            info.CityName = cityinfo.Name;
    //        }

    //        if (districtId > 0)
    //        {
    //            if (cityinfo != null && cityinfo.Districts != null && cityinfo.Districts.Count > 0)
    //            {
    //                foreach (var item in cityinfo.Districts)
    //                {
    //                    if (item.DistrictId == districtId) { districtinfo = item; break; }
    //                }
    //            }
    //            else
    //            {
    //                foreach (var item1 in items)
    //                {
    //                    foreach (var item2 in item1.Provinces)
    //                    {
    //                        foreach (var item3 in item2.Citys)
    //                        {
    //                            foreach (var item4 in item3.Districts)
    //                            {
    //                                if (item4.DistrictId == districtId) { districtinfo = item4; break; }
    //                            }
    //                            if (districtinfo != null) { cityinfo = item3; break; }
    //                        }
    //                        if (cityinfo != null) { provinceinfo = item2; break; }
    //                    }
    //                    if (provinceinfo != null) { countryinfo = item1; break; }
    //                }
    //            }

    //            if (districtinfo == null) return info;

    //            info.CountryName = countryinfo.Name;
    //            info.ProvinceName = provinceinfo.Name;
    //            info.CityName = cityinfo.Name;
    //            info.CountyName = districtinfo.Name;
    //        }

    //        return info;
    //    }*/

    //    #endregion
    //}
}
