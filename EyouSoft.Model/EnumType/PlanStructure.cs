//计调中心枚举 HL 2011-9-2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.PlanStructure
{
    #region 计调项目
    /// <summary>
    /// 计调项目枚举
    /// </summary>
    public enum PlanProject
    {
        /// <summary>
        /// 酒店 = 1
        /// </summary>
        酒店 = 1,
        /// <summary>
        /// 用车 = 2
        /// </summary>
        用车 = 2,
        /// <summary>
        /// 景点 = 3
        /// </summary>
        景点 = 3,
        /// <summary>
        /// 导游 = 4
        /// </summary>
        导游 = 4,
        /// 地接 = 5
        /// </summary>
        地接 = 5,
        /// <summary>
        /// 用餐 = 6
        /// </summary>
        用餐 = 6,
        /// <summary>
        /// 购物 = 7
        /// </summary>
        购物 = 7,
        /// <summary>
        /// 领料 = 8
        /// </summary>
        领料 = 8,
        /// <summary>
        /// 飞机 = 9
        /// </summary>
        飞机 = 9,
        /// <summary>
        /// 火车=10
        /// </summary>
        火车 = 10,
        /// <summary>
        /// 汽车=11
        /// </summary>
        汽车 = 11,
        /// <summary>
        /// 轮船=12
        /// </summary>
        轮船 = 12,
        /// <summary>
        ///  其它 = 13
        /// </summary>
        其它 = 13,
        //涉外游轮 = 20,
        //国内游轮 = 21
    }

    #endregion

    #region 计调状态
    /// <summary>
    /// 计调状态
    /// </summary>
    public enum PlanState
    {
        /// <summary>
        /// 无计调任务=1
        /// </summary>
        无计调任务 = 1,
        /// <summary>
        /// 未安排=2
        /// </summary>
        未安排 = 2,
        /// <summary>
        /// 未落实=3
        /// </summary>
        未落实 = 3,
        /// <summary>
        /// 已落实=4
        /// </summary>
        已落实 = 4,
        /// <summary>
        /// 待确认=5
        /// </summary>
        待确认=5
        ///// <summary>
        ///// 已预控=5
        ///// </summary>
        //已预控=5,
        ///// <summary>
        ///// 已付定金=6
        ///// </summary>
        //已付定金=6
    }
    #endregion

    #region 预定方式
    /// <summary>
    /// 预定方式
    /// </summary>
    public enum DueToway
    {
        /// <summary>
        /// 自订=1
        /// </summary>
        自订 = 1,
        /// <summary>
        /// 代订=2
        /// </summary>
        代订 = 2
    }
    #endregion

    #region 计调用车价格类型
    /// <summary>
    /// 计调用车价格类型
    /// </summary>
    public enum PlanCarPriceType
    {
        /// <summary>
        /// 常规线路=1
        /// </summary>
        常规线路 = 1,
        /// <summary>
        /// 特殊线路=2
        /// </summary>
        特殊线路 = 2
    }
    #endregion

    #region 计调用餐价格类型
    /// <summary>
    /// 计调用餐价格类型
    /// </summary>
    public enum PlanDiningPriceType
    {
        /// <summary>
        /// 人=1
        /// </summary>
        人 = 1,
        /// <summary>
        /// 桌=2
        /// </summary>
        桌 = 2
    }
    #endregion

    #region 计调用餐类型
    /// <summary>
    /// 计调用餐类型
    /// </summary>
    public enum PlanDiningType
    {
        /// <summary>
        /// 早=1
        /// </summary>
        早 = 1,
        /// <summary>
        /// 中=2
        /// </summary>
        中 = 2,
        /// <summary>
        /// 晚=3
        /// </summary>
        晚 = 3,
        /// <summary>
        /// 风味餐=4
        /// </summary>
        风味餐=4
    }
    #endregion

    #region 添加状态
    /// <summary>
    /// 添加状态
    /// </summary>
    public enum PlanAddStatus
    {
        /// <summary>
        /// 其他=0
        /// </summary>
        其他 = 0,
        /// <summary>
        ///  计调安排时添加 = 1
        /// </summary>
        计调安排时添加 = 1,
        ///// <summary>
        ///// 导游报账时添加 = 2
        ///// </summary>
        //导游报账时添加 = 2,
        ///// <summary>
        ///// 销售报账时添加 = 3
        ///// </summary>
        //销售报账时添加 = 3,
        /// <summary>
        /// 计调报账时添加 = 4
        /// </summary>
        计调报账时添加 = 4,
        /// <summary>
        /// 预安排添加=5
        /// </summary>
        预安排添加 = 5
    }
    #endregion

    #region 支付方式
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum Payment
    {
        /// <summary>
        /// 财务现付 = 1
        /// </summary>
        信用卡支付 = 1,
        /// <summary>
        /// 导游现付 = 2
        /// </summary>
        现付 = 2,
        /// <summary>
        /// 导游签单=3
        /// </summary>
        签单 = 3,
        /// <summary>
        /// 预付款支付=4
        /// </summary>
        //预付款支付 = 4,
        /// <summary>
        /// 财务对冲
        /// </summary>
        财务对冲 = 5,
        转账=6
        //财务支付 = 20
    }
    #endregion

    #region 计调用车类型
    /// <summary>
    /// 计调用车类型
    /// </summary>
    public enum PlanCarType
    {
        /// <summary>
        /// 跟团=1
        /// </summary>
        跟团 = 1,
        /// <summary>
        /// 接送=2
        /// </summary>
        接送 = 2
    }
    #endregion

    #region 计调酒店房型价格类型
    /// <summary>
    /// 计调酒店房型价格类型
    /// </summary>
    public enum PlanHotelPriceType
    {
        /// <summary>
        /// 间=1
        /// </summary>
        间 = 1,
        /// <summary>
        /// 床=2
        /// </summary>
        床 = 2
    }
    #endregion

    #region 计调酒店是否含早
    /// <summary>
    /// 计调酒店是否含早
    /// </summary>
    public enum PlanHotelIsMeal
    {
        /// <summary>
        /// 净房
        /// </summary>
        净房 = 1,
        /// <summary>
        /// 含双早
        /// </summary>
        含双早,
        /// <summary>
        /// 含单早
        /// </summary>
        含单早,
        /// <summary>
        /// 含三早
        /// </summary>
        含三早
    }
    #endregion

    #region 游轮旅游人群类型
    /// <summary>
    /// 游轮旅游人群类型
    /// </summary>
    public enum PlanShipCrowdType
    {
        /// <summary>
        /// 欧美=1
        /// </summary>
        欧美 = 1,
        /// <summary>
        /// 东南亚=2
        /// </summary>
        东南亚 = 2,
        /// <summary>
        /// 加拿大=3
        /// </summary>
        加拿大 = 3,
        /// <summary>
        /// 内宾=4
        /// </summary>
        内宾 = 4
    }
    #endregion

    #region 游轮房型/舱位
    /// <summary>
    /// 游轮房型/舱位
    /// </summary>
    public enum PlanShipRoomType
    {
        /// <summary>
        /// 标间
        /// </summary>
        标间 = 0,
        /// <summary>
        /// 套房
        /// </summary>
        套房 = 1,
        /// <summary>
        /// 总套
        /// </summary>
        总套 = 2,
        /// <summary>
        /// 行政间
        /// </summary>
        行政间 = 3,
        /// <summary>
        /// 包舱
        /// </summary>
        包舱 = 4,
        /// <summary>
        /// 阳台间
        /// </summary>
        阳台间 = 5,
        /// <summary>
        /// 一等舱
        /// </summary>
        一等舱 = 6,
        /// <summary>
        /// 二等舱
        /// </summary>
        二等舱 = 7,
        /// <summary>
        /// 三等舱
        /// </summary>
        三等舱 = 8,

    }
    #endregion

    #region 计调变更类别
    /// <summary>
    /// 计调变更类别
    /// </summary>
    public enum PlanChangeChangeClass
    {
        ///// <summary>
        ///// 导游报账
        ///// </summary>
        //导游报账 = 1,
        /// <summary>
        /// 计调报账
        /// </summary>
        计调报账,
        ///// <summary>
        ///// 销售报账
        ///// </summary>
        //销售报账
    }
    #endregion

    #region 计调大交通舱位类别
    /// <summary>
    /// 计调大交通舱位类别
    /// </summary>
    public enum PlanLargeSeatType
    {
        /// <summary>
        /// 头等
        /// </summary>
        头等 = 1,
        /// <summary>
        /// 商务
        /// </summary>
        商务,
        /// <summary>
        /// 经济
        /// </summary>
        经济
    }
    #endregion

    #region 计调大交通人员类型
    /// <summary>
    /// 计调大交通人员类型
    /// </summary>
    public enum PlanLargeAdultsType
    {
        /// <summary>
        /// 成人
        /// </summary>
        成人 = 1,
        /// <summary>
        /// 儿童
        /// </summary>
        儿童,
        /// <summary>
        /// 婴儿
        /// </summary>
        婴儿
    }
    #endregion

    #region 计调导游任务类型
    /// <summary>
    /// 计调导游任务类型
    /// </summary>
    public enum PlanGuideTaskType
    {
        /// <summary>
        /// 全配
        /// </summary>
        全陪 = 1,
        /// <summary>
        /// 地陪
        /// </summary>
        地陪,
        /// <summary>
        /// 接团
        /// </summary>
        接团,
        /// <summary>
        /// 送团
        /// </summary>
        送团
    }
    #endregion

    #region 变更类型
    /// <summary>
    /// 计调中心打印单变更类型
    /// </summary>
    public enum PlanBGType
    {
        /// <summary>
        /// 新增
        /// </summary>
        新增 = 1,
        /// <summary>
        /// 变更
        /// </summary>
        变更,
        /// <summary>
        /// 确认
        /// </summary>
        确认
    }
    #endregion
}
