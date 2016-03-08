using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.PlanStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.GovStructure;
    using EyouSoft.Model.PlanStructure;

    /// <summary>
    /// 描述:业务接口操作计调安排
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public interface IPlan
    {
        #region 计调增删改查

        /// <summary>
        /// 计调添加/修改
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>正数：成功 0或负数：失败</returns>
        int AddOrUpdPlan(MPlanBaseInfo mdl);

        /// <summary>
        /// 计调安排项目金额增/减
        /// </summary>
        /// <param name="mdl">金额增减实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdPlanCostChange(MPlanCostChange mdl);

        /// <summary>
        /// 根据计调编号删除安排的计调项目
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        bool DelPlan(string planId);

        /// <summary>
        /// 根据计调编号获取某个计调安排项
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanBaseInfo GetModel(string planId);

        /// <summary>
        /// 根据团队编号获取团队支出列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MPlanBaseInfo> GetList(string tourId);

        /// <summary>
        /// 获取导游安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanGuide GetGuide(string planId);

        /// <summary>
        /// 获取酒店安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanHotel GetHotel(string planId);

        /// <summary>
        /// 获取用车安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanCar GetCar(string planId);

        /// <summary>
        /// 获取大交通班次安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanLargeTime> GetLargeTime(string planId);

        /// <summary>
        /// 获取景点安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanAttractions GetAttractions(string planId);

        /// <summary>
        /// 获取游轮安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanShip GetShip(string planId);

        /// <summary>
        /// 获取用餐价格安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanDiningPrice> GetDining(string planId);

        /// <summary>
        /// 获取领料安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MGovGoodUse GetGood(string planId);

        /// <summary>
        /// 根据团队编号和支付方式获取统计数
        /// </summary>
        /// <param name="payment">支付方式</param>
        /// <param name="tourId">计调编号</param>
        /// <returns></returns>
        int GetPlanCount(Payment payment, string tourId);

        ///// <summary>
        ///// 根据团队编号和支付方式获取计调项结算金额
        ///// </summary>
        ///// <param name="payment">支付方式</param>
        ///// <param name="tourId">计调编号</param>
        ///// <returns>计调项结算金额</returns>
        //decimal GetPlanCost(Payment payment, string tourId);

        /// <summary>
        /// 获取某个计调安排项目的列表
        /// </summary>
        /// <param name="planType">计调类型</param>
        /// <param name="payment">支付方式</param>
        /// <param name="addStatus">添加状态</param>
        /// <param name="isShowCostChange">是否显示计调变更</param>
        /// <param name="changeType">计调变更类别</param>
        /// <param name="tourId">计调编号</param>
        /// <returns></returns>
        IList<MPlan> GetList(
            PlanProject planType,
            Payment? payment,
            PlanAddStatus? addStatus,
            bool isShowCostChange,
            PlanChangeChangeClass? changeType,
            string tourId);

        /// <summary>
        /// 根据团队编号获取导游任务打印单的接待行程、服务标准、导游须知
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        MPlanBaseInfo GetGuidePrint(string tourId);

        /// <summary>
        /// 根据计调编号、增减类型、计调变更类别获取计调变更实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="typ">增减类型(1增,0减)</param>
        /// <param name="chg">计调变更类别</param>
        /// <returns>计调变更实体</returns>
        MPlanCostChange GetPlanCostChanges(string planId, bool typ, PlanChangeChangeClass chg);

        #endregion

        #region 报销报账（导游、销售、计调、财务）

        /// <summary>
        /// 报销报账时获取有导游现收的列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>有导游现收的列表</returns>
        IList<MGuideIncome> GetGuideIncome(string tourId);

        /// <summary>
        /// 报销报账-导游收入选用订单时获取没有导游现收的订单列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>没有导游现收的订单列表</returns>
        IList<MGuideIncome> GetGuideIncome(int pageSize, int pageIndex, ref int recordCount, string tourId);

        /// <summary>
        /// 添加/修改导游实收
        /// </summary>
        /// <param name="mdl">导游收入实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdGuideReal(MGuideIncome mdl);

        /// <summary>
        /// 报销报账时根据团队编号获取该团其他收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他收入列表</returns>
        IList<MOtherFeeInOut> GetOtherIncome(string tourId);

        /// <summary>
        /// 报销报账时根据团队编号获取该团其他支出列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他支出列表</returns>
        IList<MOtherFeeInOut> GetOtherOutpay(string tourId);

        /// <summary>
        /// 根据团队编号获取报账汇总
        /// </summary>
        /// <param name="tourid">团队编号</param>
        /// <param name="isShowQiTaShouZhi">是否显示其他收支</param>
        /// <returns></returns>
        MBZHZ GetBZHZ(string tourid, bool isShowQiTaShouZhi);

        /// <summary>
        /// 根据团队编号获取团队收支表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        MTourTotalInOut GetTourTotalInOut(string tourId);
        #endregion

        #region 处理计调落实状态

        /// <summary>
        /// 根据团队编号和计调类型获取计调安排浮动信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="types">计调类型</param>
        /// <returns>供应商</returns>
        IList<MJiDiaoAnPaiFuDongInfo> GetJiDiaoAnPaiFuDongs(string tourId, params PlanProject[] types);

        /// <summary>
        /// 根据团队编号判断是否存在未落实的计调项目
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isDel">是否删除</param>
        /// <returns>True：存在 False：不存在</returns>
        bool IsExist(string tourId, bool isDel);

        #endregion

        #region 判断预控总使用数量是否大于预控数量
        /// <summary>
        /// 判断总使用数量是否大于预控数量(修改时要根据计调编号得到其使用数)
        /// </summary>
        /// <param name="SureId">预控编号</param>
        /// <param name="Type">计调项目类型</param>
        /// <param name="PlanId">计调编号</param>
        /// <param name="UsedNum">当前计调项使用的预控数量</param>
        /// <returns>剩余数</returns>
        int GetControlSYNumById(string SureId, PlanProject Type, string PlanId, ref int UsedNum);
        #endregion
    }
}
