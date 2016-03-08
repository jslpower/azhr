using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 系统配置业务层 
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComSetting
    {
        private readonly EyouSoft.IDAL.ComStructure.IComSetting dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComSetting>();

        /// <summary>
        /// default constructor
        /// </summary>
        public BComSetting() { }

        #region private members
        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <returns></returns>
        bool SetKeyValue(string companyId, string key, string value)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return false;

            if (dal.SetKeyValue(companyId, key, value))
            {
                string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);

                return true;
            }

            return false;
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="company">公司编号</param>
        /// <returns>配置实体</returns>
        public MComSetting GetModel(string company)
        {
            return EyouSoft.Security.Membership.UserProvider.GetComSetting(company);
        }

        /// <summary>
        /// 修改公司配置信息
        /// </summary>
        /// <param name="item">配置实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool UpdateComSetting(MComSetting item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.UpdateComSetting(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert("修改系统配置信息");
                }

                string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, item.CompanyId);

                if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName) != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
                }
            }
            return result;
        }

        /// <summary>
        /// 修改团号配置
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourCodeRule">团号规则</param>
        /// <returns></returns>
        public bool UpdateTourCodeSet(string CompanyId, string TourCodeRule)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(TourCodeRule))
            {
                result = dal.UpdateTourCodeSet(CompanyId, TourCodeRule);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改团号配置信息,团号规则:{0}", TourCodeRule));
                    string cacheName = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, CompanyId);
                    if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName) != null)
                    {
                        EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheName);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 设置公司短信配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">配置信息</param>
        /// <returns></returns>
        public bool SetComSmsConfig(string companyId, EyouSoft.Model.ComStructure.MSmsConfigInfo info)
        {
            if (string.IsNullOrEmpty(companyId)) return false;

            var setting = GetModel(companyId);

            if (setting != null && setting.SmsConfig != null && setting.SmsConfig.IsEnabled) return true;

            if (info == null || !info.IsEnabled)
            {
                info = new EyouSoft.BLL.SmsStructure.BSmsAccount().CreateSmsAccount();
            }

            if (info == null) return false;

            bool dalRetCode = dal.SetComSmsConfig(companyId, info);

            string cachekey = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, companyId);
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cachekey);

            return dalRetCode;
        }

        /// <summary>
        /// 设置金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">金蝶默认科目配置信息业务实体</param>
        /// <returns></returns>
        public bool SetKisConfigInfo(string companyId, EyouSoft.Model.ComStructure.MKisConfigInfo info)
        {
            return dal.SetKisConfigInfo(companyId, info);
        }

        /// <summary>
        /// 获取金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MKisConfigInfo GetKisConfigInfo(string companyId)
        {
            return dal.GetKisConfigInfo(companyId);
        }

        /// <summary>
        /// 设置子系统配置信息(webmaster)
        /// </summary>
        /// <param name="setting">配置信息业务实体</param>
        /// <returns></returns>
        public bool SetSysSetting(EyouSoft.Model.ComStructure.MComSetting setting)
        {
            if (setting == null) return false;
            if (string.IsNullOrEmpty(setting.CompanyId)) return false;

            if (dal.SetSysSetting(setting))
            {
                string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, setting.CompanyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取打印单据请求URI
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">打印单据类型</param>
        /// <returns></returns>
        public string GetPrintUri(string companyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType type)
        {
            string uri = "javascript:void(0)";

            if (string.IsNullOrEmpty(companyId) || type == PrintTemplateType.None) return uri;

            var setting = GetModel(companyId);

            if (setting != null && setting.PrintDocument != null && setting.PrintDocument.Count > 0)
            {
                foreach (var item in setting.PrintDocument)
                {
                    if (type == item.PrintTemplateType)
                    {
                        uri = item.PrintTemplate;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(uri)) uri = "javascript:void(0)";

            return uri;
        }

        /// <summary>
        /// 设置公司合同提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetGongSiHeTongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.公司合同到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置酒店预控提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetJiuDianYuKongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.洒店预控到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置游船预控提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetYouChuanYuKongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.游船预控到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置车辆预控提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetCheLiangYuKongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.车辆预控到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置景点预控提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetSightPreControlRemind(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.景点预控到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置其他预控提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetOtherPreControlRemind(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.其他预控到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置劳动合同提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetLaoDongHeTongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.劳动合同到期提醒).ToString(), v.ToString());
        }

        /// <summary>
        /// 设置供应商合同提醒配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="v">配置值</param>
        /// <returns></returns>
        public bool SetGongYingShangHeTongTiXing(string companyId, int v)
        {
            return SetKeyValue(companyId, ((int)SysConfiguration.供应商合同到期提醒).ToString(), v.ToString());
        }
        #endregion

    }
}
