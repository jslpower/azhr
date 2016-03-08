using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BackgroundServices.IDAL
{
    #region 出团提醒服务接口

    /// <summary>
    /// 出团提醒服务接口
    /// </summary>
    /// 周文超 2011-09-23
    public interface ISysLeaveRemindService
    {
        /// <summary>
        /// 获取出团提醒配置，返回结果均是当前点时间内需要发送的配置信息
        /// </summary>
        /// <returns></returns>
        Queue<Model.SmsStructure.MSmsSetting> GetSmsSetting();

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        IList<Model.SmsStructure.MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay);

    }

    #endregion

    #region 回团提醒服务接口

    /// <summary>
    /// 回团提醒服务接口
    /// </summary>
    /// 周文超 2011-09-23
    public interface ISysBackRemindService
    {
        /// <summary>
        /// 获取回团提醒配置，返回结果均是当前点时间内需要发送的配置信息
        /// </summary>
        /// <returns></returns>
        Queue<Model.SmsStructure.MSmsSetting> GetSmsSetting();

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        IList<Model.SmsStructure.MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay);
    }

    #endregion

    #region 生日提醒服务接口

    /// <summary>
    /// 生日提醒服务接口
    /// </summary>
    /// 周文超 2011-09-23
    public interface ISysBirthdayRemindService
    {
        /// <summary>
        /// 获取生日提醒配置，返回结果均是当前点时间内需要发送的配置信息
        /// </summary>
        /// <returns></returns>
        Queue<Model.SmsStructure.MSmsSetting> GetSmsSetting();

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        IList<Model.SmsStructure.MSmsBirthdayRemindPlan> GetSmsBirthdayRemindPlan(string companyId, int beforeDay);
    }

    #endregion

    #region 进店提醒服务接口
    /// <summary>
    /// 进店提醒服务接口
    /// </summary>
    public interface ISysJinDianTiXingService
    {
        /// <summary>
        /// 获取进店提醒配置，返回结果均是当前点时间内需要发送的配置信息
        /// </summary>
        /// <returns></returns>
        Queue<Model.SmsStructure.MSmsSetting> GetSmsSetting();

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        IList<Model.SmsStructure.MSmsJinDianTiXingPlanInfo> GetSmsJinDianTiXings(string companyId, int beforeDay);
    }
    #endregion
}
