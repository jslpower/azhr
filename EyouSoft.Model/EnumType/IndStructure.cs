//个人中心相关枚举 创建者：郑知远 创建时间：2001/09/02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.EnumType.IndStructure
{
    /// <summary>
    /// 紧急度
    /// </summary>
    public enum MemoUrgent
    {
        /// <summary>
        /// 一般
        /// </summary>
        一般 = 0,
        /// <summary>
        /// 紧急
        /// </summary>
        紧急
    }

    /// <summary>
    /// 备忘录状态
    /// </summary>
    public enum MemoState
    {
        /// <summary>
        /// 未完成
        /// </summary>
        未完成 = 0,
        /// <summary>
        /// 已完成
        /// </summary>
        已完成,
        /// <summary>
        /// 已取消
        /// </summary>
        已取消
    }

    /// <summary>
    /// 工作交流发布范围（0:全部 1:指定部门 2:指定人员）
    /// </summary>
    public enum PubScope
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = 0,
        /// <summary>
        /// 指定部门
        /// </summary>
        指定部门,
        /// <summary>
        /// 指定人员
        /// </summary>
        指定人员
    }

    /// <summary>
    /// 工作计划，汇报状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 审批中=0
        /// </summary>
        审批中 = 0,
        /// <summary>
        /// 已审批=1
        /// </summary>
        已审批 = 1,
        /// <summary>
        /// 已结束=2
        /// </summary>
        已结束 = 2
    }

    /// <summary>
    /// 客户单位欠款类型
    /// </summary>
    public enum ReceivableType
    {
        /// <summary>
        /// 超限=0
        /// </summary>
        超限 = 0,
        /// <summary>
        /// 超时=1
        /// </summary>
        超时 = 1,
        /// <summary>
        /// 超限且超期=2
        /// </summary>
        超限且超期 = 2
    }

    /// <summary>
    /// 事务提醒类型
    /// </summary>
    public enum RemindType
    {
        ///// <summary>
        ///// 订单提醒=0
        ///// </summary>
        //订单提醒 = 0,
        ///// <summary>
        ///// 计调提醒=1
        ///// </summary>
        计调提醒 = 1,
        /// <summary>
        /// 收款提醒=2
        /// </summary>
        收款提醒 = 2,
        /// <summary>
        /// 变更提醒=3
        /// </summary>
        变更提醒 = 3,
        ///// <summary>
        ///// 预控到期提醒=4
        ///// </summary>
        //预控到期提醒 = 4,
        ///// <summary>
        ///// 合同到期提醒=5
        ///// </summary>
        //合同到期提醒 = 5,
        ///// <summary>
        ///// 询价提醒=6
        ///// </summary>
        //询价提醒 = 6
    }

    /// <summary>
    /// 游客统计类型
    /// </summary>
    public enum TravellerFlowType
    {
        /// <summary>
        /// 组团游客=0
        /// </summary>
        组团游客 = 0,
        /// <summary>
        /// 地接游客=1
        /// </summary>
        地接游客 = 1,
        /// <summary>
        /// 出镜游客=2
        /// </summary>
        出镜游客 = 2
    }
}
