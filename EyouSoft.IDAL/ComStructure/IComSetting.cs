using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 系统配置接口
    /// 创建者：郑付杰
    /// 创建时间：2011/9/8
    /// </summary>
    public interface IComSetting
    {
        /*/// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="company">公司编号</param>
        /// <returns>配置实体</returns>
        MComSetting GetModel(string company);*/

        /// <summary>
        /// 修改公司配置信息
        /// </summary>
        /// <param name="item">配置实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool UpdateComSetting(MComSetting item);

        /// <summary>
        /// 修改团号配置
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourCodeRule">团号规则</param>
        /// <returns></returns>
        bool UpdateTourCodeSet(string CompanyId, string TourCodeRule);
        
        ///// <summary>
        ///// 根据键名获取值
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="SettingType"></param>
        ///// <returns></returns>
        //string GetSettingByType(string CompanyId, EyouSoft.Model.EnumType.ComStructure.SysConfiguration SettingType);

        /// <summary>
        /// 设置公司短信配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">配置信息</param>
        /// <returns></returns>
        bool SetComSmsConfig(string companyId, EyouSoft.Model.ComStructure.MSmsConfigInfo info);

        /// <summary>
        /// 设置金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">金蝶默认科目配置信息业务实体</param>
        /// <returns></returns>
        bool SetKisConfigInfo(string companyId, EyouSoft.Model.ComStructure.MKisConfigInfo info);
        /// <summary>
        /// 获取金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.ComStructure.MKisConfigInfo GetKisConfigInfo(string companyId);

        /// <summary>
        /// 设置子系统配置信息(webmaster)
        /// </summary>
        /// <param name="setting">配置信息业务实体</param>
        /// <returns></returns>
        bool SetSysSetting(EyouSoft.Model.ComStructure.MComSetting setting);
        /// <summary>
        /// 设置单个配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <returns></returns>
        bool SetKeyValue(string companyId, string key, string value);
    }
}
