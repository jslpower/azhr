using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;

namespace EyouSoft.BLL.PlanStructure
{
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.GovStructure;
    using EyouSoft.Model.PlanStructure;

    using Exception = System.Exception;

    /// <summary>
    /// 描述:业务逻辑层计调安排
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class BPlan
    {
        EyouSoft.IDAL.PlanStructure.IPlan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PlanStructure.IPlan>();

        #region 计调增删改查
        /// <summary>
        /// 计调添加
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>1:添加成功 0：添加失败 -1:领料不足 -2:预控数量不足</returns>
        public int AddPlan(MPlanBaseInfo mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.TourId) || string.IsNullOrEmpty(mdl.SourceName) || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.OperatorName))
            {
                return 0;
            }
            mdl.PlanId = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(mdl.SueId) && (mdl.Type == PlanProject.用车 || mdl.Type == PlanProject.酒店 || mdl.Type == PlanProject.景点 || mdl.Type == PlanProject.其它))
            {
                int UsedNum = 0;
                int SUNum = dal.GetControlSYNumById(mdl.SueId, mdl.Type, "", ref UsedNum);
                if (SUNum < mdl.Num)
                {
                    return -2;
                }
            }

            //if (mdl.Type != PlanProject.国内游轮 && mdl.Type != PlanProject.涉外游轮)
            //{
            //    mdl.DNum = mdl.Num;
            //}

            var ok = this.dal.AddOrUpdPlan(mdl);
            if (ok > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增计调编号：{0}、计调类型：{1}的计调项目。", mdl.PlanId, mdl.Type));
            }
            return ok;
        }

        /// <summary>
        /// 计调修改
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>1:修改成功 0：修改失败 -1:领料不足 -2:预控数量不足</returns>
        public int UpdPlan(MPlanBaseInfo mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.SourceName))
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(mdl.SueId) && mdl.SueId.Trim() != "" && (mdl.Type == PlanProject.用车 || mdl.Type == PlanProject.酒店 || mdl.Type == PlanProject.景点 || mdl.Type == PlanProject.其它))
            {
                int UsedNum = 0;
                int SUNum = dal.GetControlSYNumById(mdl.SueId, mdl.Type, mdl.PlanId, ref UsedNum);
                if (SUNum + UsedNum < mdl.Num)
                {
                    return -2;
                }
            }

            //if (mdl.Type != PlanProject.国内游轮 && mdl.Type != PlanProject.涉外游轮)
            //{
            //    mdl.DNum = mdl.Num;
            //}

            var ok = this.dal.AddOrUpdPlan(mdl);
            if (ok > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改计调编号：{0}、计调类型：{1}的计调项目。", mdl.PlanId, mdl.Type));
            }
            return ok;
        }

        /// <summary>
        /// 计调安排项目金额增/减
        /// </summary>
        /// <param name="mdl">金额增减实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdPlanCostChange(MPlanCostChange mdl)
        {
            if (mdl == null)
            {
                return false;
            }
            var ok = this.dal.AddOrUpdPlanCostChange(mdl);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改计调编号：{0}、变更类型：{1}、增减类型：{2}的计调项目。", mdl.PlanId, mdl.ChangeType, mdl.Type));
            }
            return ok;
        }

        /// <summary>
        /// 根据计调编号删除安排的计调项目
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelPlan(string planId)
        {
            if (string.IsNullOrEmpty(planId))
            {
                return false;
            }
            var ok = this.dal.DelPlan(planId);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除计调编号：{0}的计调项目。", planId));
            }
            return ok;
        }

        /// <summary>
        /// 根据计调编号获取某个计调安排项
        /// </summary>
        /// <param name="type">计调类型</param>
        /// <param name="planId">计调编号</param>
        /// <returns>计调实体</returns>
        public MPlanBaseInfo GetModel(PlanProject type, string planId)
        {

            if (string.IsNullOrEmpty(planId))
            {
                return null;
            }

            var mdl = this.dal.GetModel(planId);

            //switch (type)
            //{
            //    case PlanProject.地接:
            //        break;
            //    case PlanProject.导游:
            //        mdl.PlanGuide = this.dal.GetGuide(planId);
            //        break;
            //    case PlanProject.酒店:
            //        //mdl.PlanHotel = this.dal.GetHotel(planId);
            //        break;
            //    case PlanProject.用车:
            //        //mdl.PlanCar = this.dal.GetCar(planId);
            //        break;
            //    case PlanProject.飞机:
            //    case PlanProject.火车:
            //    case PlanProject.汽车:
            //        //mdl.PlanLargeTime = this.dal.GetLargeTime(planId);
            //        break;
            //    case PlanProject.景点:
            //        //mdl.PlanAttractions = this.dal.GetAttractions(planId);
            //        break;
            //    //case PlanProject.涉外游轮:
            //    //case PlanProject.国内游轮:
            //    //    mdl.PlanShip = this.dal.GetShip(planId);
            //    //    break;
            //    case PlanProject.用餐:
            //        //mdl.PlanDiningPricelist = this.dal.GetDining(planId);
            //        break;
            //    case PlanProject.购物:
            //        break;
            //    case PlanProject.领料:
            //        //mdl.PlanGood = this.dal.GetGood(planId);
            //        break;
            //    case PlanProject.其它:
            //        break;
            //}

            return mdl;
        }

        /// <summary>
        /// 根据团队编号获取支付方式为导游签单的统计数
        /// </summary>
        /// <param name="tourId">计调编号</param>
        /// <returns>导游签单的统计数</returns>
        public int GetGuideSignNum(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? 0 : this.dal.GetPlanCount(Payment.签单, tourId);
        }

        ///// <summary>
        ///// 根据团队编号获取支付方式为导游现付的计调项结算金额
        ///// </summary>
        ///// <param name="tourId">计调编号</param>
        ///// <returns>计调项结算金额</returns>
        //public decimal GetGuidePayCost(string tourId)
        //{
        //    return string.IsNullOrEmpty(tourId) ? 0 : this.dal.GetPlanCost(Payment.导游现付, tourId);
        //}

        /// <summary>
        /// 获取某个计调类型的安排项目的列表
        /// </summary>
        /// <param name="planType">计调类型</param>
        /// <param name="payment">支付方式</param>
        /// <param name="addStatus">添加状态</param>
        /// <param name="isShowCostChange">是否显示计调变更</param>
        /// <param name="changeType">计调变更类别</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>安排项目的列表</returns>
        public IList<MPlan> GetList(PlanProject planType, Payment? payment, PlanAddStatus? addStatus, bool isShowCostChange, PlanChangeChangeClass? changeType, string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetList(planType, payment, addStatus, isShowCostChange, changeType, tourId);
        }

        /// <summary>
        /// 根据团队编号获取团队支出列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MPlanBaseInfo> GetList(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetList(tourId);
        }

        /// <summary>
        /// 根据团队编号获取导游任务打印单的接待行程、服务标准、导游须知
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public MPlanBaseInfo GetGuidePrint(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetGuidePrint(tourId);
        }

        /// <summary>
        /// 根据计调编号、增减类型、计调变更类别获取计调变更实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="typ">增减类型(1增,0减)</param>
        /// <param name="chg">计调变更类别</param>
        /// <returns>计调变更实体</returns>
        public MPlanCostChange GetPlanCostChanges(string planId, bool typ, PlanChangeChangeClass chg)
        {
            return string.IsNullOrEmpty(planId) ? null : this.dal.GetPlanCostChanges(planId, typ, chg);
        }
        #endregion

        #region 报销报账（导游、销售、计调、财务）
        /// <summary>
        /// 报销报账时获取有导游现收的列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>有导游现收的列表</returns>
        public IList<MGuideIncome> GetGuideIncome(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetGuideIncome(tourId);
        }

        /// <summary>
        /// 报销报账-导游收入选用订单时获取没有导游现收的订单列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>没有导游现收的订单列表</returns>
        public IList<MGuideIncome> GetGuideIncome(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , string tourId)
        {
            return string.IsNullOrEmpty(tourId)
                       ? null
                       : this.dal.GetGuideIncome(pageSize, pageIndex, ref recordCount, tourId);
        }

        /// <summary>
        /// 添加/修改导游实收
        /// </summary>
        /// <param name="mdl">导游收入实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdGuideReal(MGuideIncome mdl)
        {
            if (mdl == null || mdl.GuideRealIncome <= 0)
            {
                return false;
            }
            var ok = this.dal.AddOrUpdGuideReal(mdl);
            if (ok)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了订单编号：{0}、导游实收：{1}的导游实收。", mdl.OrderId, mdl.GuideRealIncome));
            }
            return ok;
        }

        /// <summary>
        /// 报销报账时根据团队编号获取该团其他收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他收入列表</returns>
        public IList<MOtherFeeInOut> GetOtherIncome(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetOtherIncome(tourId);
        }

        /// <summary>
        /// 报销报账时根据团队编号获取该团其他支出列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他支出列表</returns>
        public IList<MOtherFeeInOut> GetOtherOutpay(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetOtherOutpay(tourId);
        }

        /// <summary>
        /// 根据团队编号获取报账汇总
        /// </summary>
        /// <param name="tourid">团队编号</param>
        /// <param name="isShowQiTaShouZhi">是否显示其他收支</param>
        /// <returns></returns>
        public MBZHZ GetBZHZ(string tourid, bool isShowQiTaShouZhi)
        {
            return string.IsNullOrEmpty(tourid) ? null : this.dal.GetBZHZ(tourid, isShowQiTaShouZhi);
        }

        /// <summary>
        /// 根据团队编号获取团队收支表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public MTourTotalInOut GetTourTotalInOut(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetTourTotalInOut(tourId);
        }
        #endregion

        #region 处理计调落实状态
        /// <summary>
        /// 根据团队编号和计调类型获取计调安排浮动信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="types">计调类型</param>
        /// <returns>供应商</returns>
        public IList<MJiDiaoAnPaiFuDongInfo> GetJiDiaoAnPaiFuDongs(string tourId, params PlanProject[] types)
        {
            if (string.IsNullOrEmpty(tourId)) return null;

            return dal.GetJiDiaoAnPaiFuDongs(tourId, types);
        }

        /// <summary>
        /// 根据团队编号判断是否存在未落实的计调项目
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExist(string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                throw new Exception("团队编号不能为空！");
            }

            return this.dal.IsExist(tourId, false);
        }

        /// <summary>
        /// 删除的时候根据团队编号判断是否存在已安排的计调项目
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExistPay(string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                throw new Exception("团队编号不能为空！");
            }

            return this.dal.IsExist(tourId, true);
        }
        #endregion
    }
}
