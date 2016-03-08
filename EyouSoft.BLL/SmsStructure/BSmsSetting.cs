using System.Collections.Generic;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 短信配置业务逻辑
    /// </summary>
    /// 周文超 2011-09-14
    public class BSmsSetting : BLLBase
    {
        private readonly IDAL.SmsStructure.ISmsSetting _dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.SmsStructure.ISmsSetting>();

        /// <summary>
        /// 设置公司短信配置信息（全部信息）
        /// </summary>
        /// <param name="model">短信配置实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int SetSmsSetting(Model.SmsStructure.MSmsSetting model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return 0;

            return _dal.SetSmsSetting(model);
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isEnabled">是否启用</param>
        /// <returns></returns>
        public int SetIsEnabled(string companyId, Model.EnumType.SmsStructure.SettingType type, bool isEnabled)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;

            return _dal.SetIsEnabled(companyId, type, isEnabled);
        }

        /// <summary>
        /// 设置当天是否已发送
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isSend">当天是否已发送</param>
        /// <returns></returns>
        public int SetIsSend(string companyId, Model.EnumType.SmsStructure.SettingType type, bool isSend)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;

            return _dal.SetIsEnabled(companyId, type, isSend);
        }

        /// <summary>
        /// 获取公司短信配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <returns></returns>
        public Model.SmsStructure.MSmsSetting GetSmsSetting(string companyId, Model.EnumType.SmsStructure.SettingType type)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;

            IList<Model.SmsStructure.MSmsSetting> list = _dal.GetSmsSetting(companyId, type);

            if (list == null || list.Count < 1)
                return null;

            return list[0];
        }

        /// <summary>
        /// 获取公司短信配置
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<Model.SmsStructure.MSmsSetting> GetSmsSetting(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return null;

            return _dal.GetSmsSetting(companyId, null);
        }
    }
}
