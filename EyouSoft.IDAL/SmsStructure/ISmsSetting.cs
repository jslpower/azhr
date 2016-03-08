using System.Collections.Generic;

namespace EyouSoft.IDAL.SmsStructure
{
    /// <summary>
    /// 短信配置数据接口
    /// </summary>
    /// 周文超 2011-09-14
    public interface ISmsSetting
    {
        /// <summary>
        /// 设置公司短信配置信息
        /// </summary>
        /// <param name="model">短信配置实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int SetSmsSetting(Model.SmsStructure.MSmsSetting model);

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isEnabled">是否启用</param>
        /// <returns></returns>
        int SetIsEnabled(string companyId, Model.EnumType.SmsStructure.SettingType type, bool isEnabled);

        /// <summary>
        /// 设置当天是否已发送
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isSend">当天是否已发送</param>
        /// <returns></returns>
        int SetIsSend(string companyId, Model.EnumType.SmsStructure.SettingType type, bool isSend);

        /// <summary>
        /// 获取公司短信配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="types">配置类型集合</param>
        /// <returns>公司短信配置信息集合</returns>
        IList<Model.SmsStructure.MSmsSetting> GetSmsSetting(string companyId,
                                                            params Model.EnumType.SmsStructure.SettingType[] types);
    }
}
