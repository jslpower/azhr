using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.HPlanStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.GovStructure;
    using EyouSoft.Model.HPlanStructure;

    /// <summary>
    /// 描述:业务接口操作计调安排
    /// 创建时间:2013-06-26
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
        /// 计调预安排
        /// </summary>
        /// <param name="model">与安排实体</param>
        /// <returns>正数：成功 0或负数：失败</returns>
        int PlanYuAnPai(EyouSoft.Model.HTourStructure.MTourStatusChange model);

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
        /// 获取酒店安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanHotel GetHotel(string planId);

        /// <summary>
        /// 获取 地接/酒店 房屋类型安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanHotelRoom> GetHotelRoom(string planId);

        /// <summary>
        /// 获取景点安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanAttractions> GetAttractions(string planId);

        /// <summary>
        /// 获取用车安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanCar> GetCar(string planId);

        /// <summary>
        /// 获取用餐安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanDining> GetDining(string planId);

        /// <summary>
        /// 获取区间交通业安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        IList<MPlanLargeFrequency> GetLargeFrequency(string planId);

        /// <summary>
        /// 获取导游安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        MPlanGuide GetGuide(string planId);

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

        /// <summary>
        /// 根据团队编号和支付方式获取计调项结算金额
        /// </summary>
        /// <param name="payment">支付方式</param>
        /// <param name="tourId">计调编号</param>
        /// <returns>计调项结算金额</returns>
        decimal GetPlanCost(Payment payment, string tourId);

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
            IList<Payment> payment,
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

        /// <summary>
        /// 获取团队或计调用房数
        /// </summary>
        /// <param name="type">1：计调编号，0或其他团队编号</param>
        /// <param name="Id">团队编号或计调编号</param>
        /// <returns></returns>
        IList<MPlanHotelRoom> GetRoomList(string type, string Id,string companyId);
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
        /// 根据团队编号获取报账汇总
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        MBZHZ GetBZHZ(string tourid);

        /// <summary>
        /// 根据团队编号获取团队收支表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        MTourTotalInOut GetTourTotalInOut(string tourId);

        /// <summary>
        /// 购物收入添加/修改
        /// </summary>
        /// <param name="m">购物收入实体</param>
        /// <returns>0成功 1失败 2已提交财务</returns>
        int AddOrUpdGouWuShouRu(MGouWuShouRu m);

        /// <summary>
        /// 根据计调编号获取购物收入实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>购物收入实体</returns>
        MGouWuShouRu GetGouWuShouRuMdl(string planId);

        /// <summary>
        /// 根据团队编号获取购物收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>购物收入列表</returns>
        IList<MGouWuShouRuBase> GetGouWuShouRuLst(string tourId);
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

        /// <summary>
        /// 根据计划编号和落实状态更新计调状态
        /// </summary>
        /// <param name="tourid">计划编号</param>
        /// <param name="status">落实状态：无计调任务=1 未安排 = 2 未落实=3 已落实=4 待确认=5</param>
        /// <param name="typ">计调类别：全部=0 酒店=1 用车=2 景点=3 导游=4 地接=5 用餐=6 购物=7 领料=8 飞机=9 火车=10 汽车=11 轮船=12 其它=13</param>
        /// <returns>返回执行结果：0 失败 1 成功</returns>
        int DoGlobal(string tourid, PlanState status,PlanProject? typ);

        /// <summary>
        /// 根据计调编号和更新状态更新计调状态
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="status">更新的状态</param>
        /// <returns>1：成功 0：失败</returns>
        int ChangePlanStatus(string planId, PlanState status);
        #endregion

        /// <summary>
        /// 是否允许操作计调项，返回1允许操作，其它不允许
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="planId">计调编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <returns></returns>
        int IsYunXuCaoZuo(string tourId, string planId, string operatorId);

        /// <summary>
        /// 获得计调中心列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MPlanListSearch"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HPlanStructure.MPlanList> GetPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanListSearch MPlanListSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId);
        /// <summary>
        /// 获得计调查询统计列表计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HPlanStructure.MPlanTJInfo> GetPlanTJ(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanTJChaXunInfo chaXun, int[] DetpIds, bool isOnlySeft, string LoginUserId);
    }
}
