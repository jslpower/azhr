using System;
using System.Collections.Generic;
using EyouSoft.Model.EnumType.SmsStructure;

namespace EyouSoft.Model.SmsStructure
{
    #region 出回团提醒短信任务实体

    /// <summary>
    /// 出回团提醒短信任务实体
    /// </summary>
    /// 周文超2011-09-23
    public class MSmsTourTimeTask
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 出团/回团时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 任务类型（出团、回团）
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 游客编号
        /// </summary>
        public string TravellerId { get; set; }

        /// <summary>
        /// 游客姓名
        /// </summary>
        public string Traveller { get; set; }

        /// <summary>
        /// 接收号码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否发送(T为已发送，F为未发送)
        /// </summary>
        public bool IsSend { get; set; }
    }

    #endregion

    #region 出回团提醒服务发送短信实体

    /// <summary>
    /// 出回团提醒短信任务实体
    /// </summary>
    /// 周文超2011-09-23
    public class MSmsTourTimePlan
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveTime { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime BackTime { get; set; }
        /// <summary>
        /// 集合方式
        /// </summary>
        public string Gather { get; set; }
        /// <summary>
        /// 团队销售员编号
        /// </summary>
        public string SellerId { get; set; }        
        /// <summary>
        /// 游客信息
        /// </summary>
        public IList<MSmsTourTimeTraveller> Traveller { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string DaoYouName { get; set; }
        /// <summary>
        /// 导游电话
        /// </summary>
        public string DaoYouTelephone { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
    }

    #endregion

    #region 出回团提醒服务发送短信游客实体

    /// <summary>
    /// 出回团提醒服务发送短信游客实体
    /// </summary>
    /// 周文超2011-09-23
    public class MSmsTourTimeTraveller
    {
        /// <summary>
        /// 游客编号
        /// </summary>
        public string TravellerId { get; set; }

        /// <summary>
        /// 游客姓名
        /// </summary>
        public string Traveller { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Code { get; set; }
    }

    #endregion

    #region 生日提醒短信任务实体

    /// <summary>
    /// 生日提醒短信任务实体
    /// </summary>
    public class MSmsBirthdayRemindPlan
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
    }

    #endregion

    #region 进店提醒短信任务实体
    /// <summary>
    /// 进店提醒短信任务实体
    /// </summary>
    public class MSmsJinDianTiXingPlanInfo
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LDate { get; set; }
        /// <summary>
        /// 导游安排信息集合
        /// </summary>
        public IList<MSmsJinDianDaoYouInfo> DaoYous { get; set; }
        /// <summary>
        /// 购物安排信息集合
        /// </summary>
        public IList<MSmsJinDianGouWuInfo> GouWus { get; set; }
    }

    /// <summary>
    /// 进店提醒短信任务导游实体
    /// </summary>
    public class MSmsJinDianDaoYouInfo
    {
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 进店提醒短信任务购物实体
    /// </summary>
    public class MSmsJinDianGouWuInfo
    {
        /// <summary>
        /// 购物店名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
    #endregion
}
