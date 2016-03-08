using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.BLL.SysStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.GovStructure;
using EyouSoft.Model.HPlanStructure;

namespace EyouSoft.BLL.HPlanStructure
{
    using Exception = System.Exception;

    /// <summary>
    /// 描述:业务逻辑层计调安排
    /// </summary>
    public class BPlan : BLLBase
    {
        EyouSoft.IDAL.HPlanStructure.IPlan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HPlanStructure.IPlan>();

        #region 计调增删改查
        /// <summary>
        /// 计调添加
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>1:添加成功 0：添加失败 -1:领料不足 </returns>
        public int AddPlan(MPlanBaseInfo mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.CompanyId) || string.IsNullOrEmpty(mdl.TourId) || string.IsNullOrEmpty(mdl.SourceName) || string.IsNullOrEmpty(mdl.OperatorId) || string.IsNullOrEmpty(mdl.Operator))
            {
                return 0;
            }
            mdl.PlanId = Guid.NewGuid().ToString();
            var ok = this.dal.AddOrUpdPlan(mdl);
            if (ok > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("新增计调编号：{0}、计调类型：{1}的计调项目。", mdl.PlanId, mdl.Type));
            }
            return ok;
        }

        /// <summary>
        /// 计调预安排
        /// </summary>
        /// <param name="mdl">与安排实体</param>
        /// <returns>正数：成功 0或负数：失败</returns>
        public int PlanYuAnPai(EyouSoft.Model.HTourStructure.MTourStatusChange model)
        {
            if (string.IsNullOrEmpty(model.TourId)
                           || string.IsNullOrEmpty(model.CompanyId)
                           || string.IsNullOrEmpty(model.OperatorId)
                           || string.IsNullOrEmpty(model.Operator)
                           || model.OperatorDeptId == 0) return 0;

            return dal.PlanYuAnPai(model);
        }

        /// <summary>
        /// 计调修改
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>1:修改成功 0：修改失败 -1:领料不足 </returns>
        public int UpdPlan(MPlanBaseInfo mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.SourceName))
            {
                return 0;
            }

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

            switch (type)
            {
                case PlanProject.地接:
                    mdl.PlanHotelRoomList = this.dal.GetHotelRoom(planId);
                    break;
                case PlanProject.导游:
                    mdl.PlanGuide = this.dal.GetGuide(planId);
                    break;
                case PlanProject.酒店:
                    mdl.PlanHotel = this.dal.GetHotel(planId);
                    mdl.PlanHotelRoomList = this.dal.GetHotelRoom(planId);
                    break;
                case PlanProject.用车:
                    mdl.PlanCarList = this.dal.GetCar(planId);
                    break;
                case PlanProject.飞机:
                case PlanProject.火车:
                case PlanProject.汽车:
                case PlanProject.轮船:
                    mdl.PlanLargeFrequencyList = this.dal.GetLargeFrequency(planId);
                    break;
                case PlanProject.景点:
                    mdl.PlanAttractionsList = this.dal.GetAttractions(planId);
                    break;
                case PlanProject.用餐:
                    mdl.PlanDiningList = this.dal.GetDining(planId);
                    break;
                case PlanProject.购物:
                    break;
                //case PlanProject.领料:
                //    mdl.PlanGood = this.dal.GetGood(planId);
                //    break;
                case PlanProject.其它:
                    break;
            }

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

        /// <summary>
        /// 根据团队编号获取支付方式为导游现付的计调项结算金额
        /// </summary>
        /// <param name="tourId">计调编号</param>
        /// <returns>计调项结算金额</returns>
        public decimal GetGuidePayCost(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? 0 : this.dal.GetPlanCost(Payment.现付, tourId);
        }

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
            if(string.IsNullOrEmpty(tourId))
            {
                return null;
            }
            else
            {
                IList<Payment> paylist = new List<Payment>();
                if (payment.HasValue)
                {
                    paylist.Add(payment.Value);
                }
                return GetListP(planType, paylist, addStatus, isShowCostChange, changeType, tourId);
            }
        }

        //
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
        public IList<MPlan> GetListP(PlanProject planType, IList<Payment> paylist, PlanAddStatus? addStatus, bool isShowCostChange, PlanChangeChangeClass? changeType, string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this.dal.GetList(planType, paylist, addStatus, isShowCostChange, changeType, tourId);
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

        /// <summary>
        /// 获取团队或计调用房数
        /// </summary>
        /// <param name="type">1：计调编号，0或其他团队编号</param>
        /// <param name="Id">团队编号或计调编号</param>
        /// <returns></returns>
        public IList<MPlanHotelRoom> GetRoomList(string type, string Id)
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(type)) return null;
            return dal.GetRoomList(type, Id,this.LoginUserInfo.CompanyId);
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
        /// 根据团队编号获取报账汇总
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        public MBZHZ GetBZHZ(string tourid)
        {
            return string.IsNullOrEmpty(tourid) ? null : this.dal.GetBZHZ(tourid);
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

        /// <summary>
        /// 购物收入添加/修改
        /// </summary>
        /// <param name="m">购物收入实体</param>
        /// <returns>0成功 1失败 2已提交财务</returns>
        public int AddOrUpdGouWuShouRu(MGouWuShouRu m)
        {
            if (m == null || string.IsNullOrEmpty(m.PlanId))
            {
                throw new Exception("购物实体不能为空或者计调编号不能为空！");
            }
            var ok = this.dal.AddOrUpdGouWuShouRu(m);
            if (ok==0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("保存了计调编号：{0}的购物收入。", m.PlanId));
            }
            return ok;
        }

        /// <summary>
        /// 根据计调编号获取购物收入实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>购物收入实体</returns>
        public MGouWuShouRu GetGouWuShouRuMdl(string planId)
        {
            return string.IsNullOrEmpty(planId)?null:this.dal.GetGouWuShouRuMdl(planId);
        }

        /// <summary>
        /// 根据团队编号获取购物收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>购物收入列表</returns>
        public IList<MGouWuShouRuBase> GetGouWuShouRuLst(string tourId)
        {
            return string.IsNullOrEmpty(tourId)?null: this.dal.GetGouWuShouRuLst(tourId);
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
                throw new System.Exception("团队编号不能为空！");
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
                throw new System.Exception("团队编号不能为空！");
            }

            return this.dal.IsExist(tourId, true);
        }
        /// <summary>
        /// 根据计划编号和落实状态更新计调状态
        /// </summary>
        /// <param name="tourid">计划编号</param>
        /// <param name="status">落实状态：无计调任务=1 未安排 = 2 未落实=3 已落实=4 待确认=5</param>
        /// <param name="typ">计调类别：全部=0 酒店=1 用车=2 景点=3 导游=4 地接=5 用餐=6 购物=7 领料=8 飞机=9 火车=10 汽车=11 轮船=12 其它=13</param>
        /// <returns>返回执行结果：0 失败 1 成功</returns>
        public int DoGlobal(string tourid,PlanState status,PlanProject? typ)
        {
            if (string.IsNullOrEmpty(tourid))
            {
                throw new System.Exception("团队编号不能为空！");
            }

            return this.dal.DoGlobal(tourid, status,typ);
        }

        /// <summary>
        /// 根据计调编号和更新状态更新计调状态
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="status">更新的状态</param>
        /// <returns>1：成功 0：失败</returns>
        public int ChangePlanStatus(string planId,PlanState status)
        {
            if (string.IsNullOrEmpty(planId))
            {
                throw new System.Exception("计调编号不能为空！");
            }

            return this.dal.ChangePlanStatus(planId, status);
        }
        #endregion

        #region 获得计调中心列表
        /// <summary>
        /// 获得计调中心列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MPlanListSearch"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HPlanStructure.MPlanList> GetPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanListSearch MPlanListSearch)
        {
            bool isOnlySeft = false;
            int[] DeptIds = null;
            DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调列表, out isOnlySeft);
            return dal.GetPlanList(CompanyId, pageSize, pageIndex, ref recordCount, MPlanListSearch, DeptIds, isOnlySeft, LoginUserId);
        }
        #endregion

        #region 获得计调查询统计列表
        /// <summary>
        /// 获得计调查询统计列表计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HPlanStructure.MPlanTJInfo> GetPlanTJ(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanTJChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            bool isOnlySeft = false;
            int[] DeptIds = null;
            DeptIds = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_计调列表, out isOnlySeft);
            return dal.GetPlanTJ(companyId, pageSize, pageIndex, ref recordCount, chaXun, DeptIds, isOnlySeft, LoginUserId);
        }
        #endregion

        /// <summary>
        /// 是否允许操作计调项，返回1允许操作，其它不允许
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="planId">计调编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <returns></returns>
        public int IsYunXuCaoZuo(string tourId, string planId, string operatorId)
        {
            if (string.IsNullOrEmpty(tourId) 
                || string.IsNullOrEmpty(planId) 
                || string.IsNullOrEmpty(operatorId)) return 0;

            return dal.IsYunXuCaoZuo(tourId, planId, operatorId);
        }
    }
}
