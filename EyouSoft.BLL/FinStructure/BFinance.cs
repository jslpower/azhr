using System;
using System.Collections.Generic;

namespace EyouSoft.BLL.FinStructure
{
    using System.Linq;

    using EyouSoft.Cache.Facade;
    using EyouSoft.Component.Factory;
    using EyouSoft.IDAL.FinStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.PlanStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;
    using EyouSoft.Toolkit;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// 0.自己添加的数据（默认）
    /// 1.本部浏览：可以查看自己所在部门内用户的
    /// 2.部门浏览：可以查看自己所在部门及下级部门内用户的数据
    /// 3.内部浏览：可以查看自己所在部门的第一层级部门及下级部门内用户的数据
    /// 4.查看全部：可以查看所有用户的数据
    /// </summary>
    public class BFinance:BLLBase
    {
        private readonly IFinance _dal = ComponentFactory.CreateDAL<IFinance>();

        private bool _isOnlySelf;

        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddProfitDistribute(MProfitDistribute mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.TourId) ||string.IsNullOrEmpty(mdl.Staff))
            {
                return false;
            }
            var isOk = this._dal.AddProfitDistribute(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了利润分配数据");
            }
            return isOk;
        }

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdProfitDistribute(MProfitDistribute mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.TourId) || string.IsNullOrEmpty(mdl.Staff))
            {
                return false;
            }
            var isOk = this._dal.UpdProfitDistribute(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了编号：{0}的利润分配数据",mdl.Id));
            }
            return isOk;
        }

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public bool DelProfitDis(int id, string companyid)
        {
            if (id <= 0 || string.IsNullOrEmpty(companyid))
            {
                return false;
            }
            var isOk = this._dal.DelProfitDis(id,companyid);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了编号：{0}的利润分配数据", id));
            }
            return isOk;
        }

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        public MProfitDistribute GetProfitDistribute(int id, string companId)
        {
            return id <= 0 || string.IsNullOrEmpty(companId) ? null : this._dal.GetProfitDistribute(id, companId);
        }

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MProfitDistribute> GetProfitDistribute(string tourId)
        {
            return string.IsNullOrEmpty(tourId) ? null : this._dal.GetProfitDistribute(tourId);
        }

        #endregion

        #region 应收管理

        /// <summary>
        /// 根据应收搜索实体获取应收帐款/已结清账款列表和金额汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumField">金额汇总信息(SumPrice:"合同金额" CheckMoney:"已收" Unchecked:"已收待审" Unreceived:"欠款" ReturnMoney:"已退" UnChkRtn:"已退待审")</param>
        /// <param name="mSearch">应收搜索实体</param>
        /// <returns>应收帐款/已结清账款列表</returns>
        public IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , ref object[] sumField
                                                , MReceivableBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId))
            {
                throw new System.Exception("bll error:查询实体为null或string.IsNullOrEmpty(查询实体.CompanyId)==true。");
            }

            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId) || pageSize <= 0 || pageIndex <= 0)
            {
                return null;
            }
            var lst = this._dal.GetReceivableInfoLst(pageSize, pageIndex, ref recordCount, ref sumField, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应收管理, out _isOnlySelf));
            return lst;
        }

        /// <summary>
        /// 根据系统公司编号获取当日收款对账列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sum">金额汇总信息<!--<root><row CollectionRefundAmount="收款金额"/></root>--></param>
        /// <param name="tourTypes">团队类型集合</param>
        /// <param name="isShowDistribution">是否同业分销</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>当日收款对账列表</returns>
        public IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                                , int pageIndex
                                                                , ref int recordCount
                                                                , ref decimal sum
                                                                , IList<TourType> tourTypes
                                                                , bool isShowDistribution
                                                                , string companyId
                                                                , MDayReceivablesChkBase search)
        {
            var xmlSum = string.Empty;
            if (string.IsNullOrEmpty(companyId) || pageSize <= 0 || pageIndex <= 0)
            {
                return null;
            }
            var lst= this._dal.GetDayReceivablesChkLst(
                pageSize, pageIndex, ref recordCount, ref xmlSum, companyId, tourTypes, isShowDistribution, search, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应收管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                sum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "CollectionRefundAmount"));
            }
            return lst;
        }

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="mdl">订单销售收款/退款的实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetTourOrderSalesCheck(MTourOrderSales mdl)
        {
            var isOk = mdl != null && !string.IsNullOrEmpty(mdl.Id) && !string.IsNullOrEmpty(mdl.OrderId) && mdl.ApproverDeptId > 0 && this._dal.SetTourOrderSalesCheck(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("更新了收款/退款编号：{0}的财务的审核状态", mdl.Id));
            }
            return isOk;
        }

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="lst">订单销售收款/退款的实体</param>
        /// <returns>审核失败的收款/退款编号</returns>
        public IList<string> SetTourOrderSalesCheck(IList<MTourOrderSales> lst)
        {
            if (lst==null||lst.Count==0)
            {
                return null;
            }
            return (from m in lst where !this.SetTourOrderSalesCheck(m) select m.Id).ToList();
        }

        /// <summary>
        /// 报销完成根据团队编号对导游实收进行自动审核
        /// </summary>
        /// <param name="approverDeptId"></param>
        /// <param name="approverId"></param>
        /// <param name="approver"></param>
        /// <param name="approveTime"></param>
        /// <param name="tourId"></param>
        /// <returns>审核失败的收款/退款编号</returns>
        public IList<string> SetTourOrderSalesCheck(int approverDeptId, string approverId, string approver, DateTime approveTime,string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                return null;
            }
            var lst = this._dal.GetTourOrderSalesLstByTourId(tourId);
            if (lst != null && lst.Count > 0)
            {
                foreach (var m in lst)
                {
                    m.ApproverDeptId = approverDeptId;
                    m.ApproverId = approverId;
                    m.Approver = approver;
                    m.ApproveTime = approveTime;
                }
            }
            return this.SetTourOrderSalesCheck(lst);
        }

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        public IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds)
        {
            return orderIds == null || orderIds.Length == 0 ? null : this._dal.GetBatchTourOrderSalesCheck(orderIds);
        }

        #endregion

        #region 杂费收支

        /// <summary>
        /// 添加其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.OperatorId))
            {
                return false;
            }
            var isOk= this._dal.AddOtherFeeInOut(typ, mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("新增了杂费收支");
            }

            return isOk;
        }

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl)
        {
            if (mdl == null || mdl.Id<=0 || string.IsNullOrEmpty(mdl.OperatorId))
            {
                return false;
            }
            var isOk= this._dal.UpdOtherFeeInOut(typ, mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费收支编号：{0}的数据",mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int DelOtherFeeInOut(string companyId,ItemType typ, params int[] ids)
        {
            if (ids==null||ids.Length==0||string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            var isOk= this._dal.DelOtherFeeInOut(companyId,typ, ids);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了杂费收支编号：{0}的数据",Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        public MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id,string companyId)
        {
            return id<=0 ? null : this._dal.GetOtherFeeInOut(typ, id,companyId);
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量审核
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="auditDeptId">审核人部门编号</param>
        /// <param name="auditId">审核人编号</param>
        /// <param name="audit">审核人</param>
        /// <param name="auditRemark">审核意见</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="status">状态</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeInOutAudit(ItemType typ, int auditDeptId, string auditId, string audit, string auditRemark, DateTime auditTime, FinStatus status, params int[] ids)
        {
            if (ids == null || ids.Length == 0||string.IsNullOrEmpty(auditId)||string.IsNullOrEmpty(audit))
            {
                return 0;
            }
            var isOk = this._dal.SetOtherFeeInOutAudit(typ, auditDeptId, auditId, audit, auditRemark, auditTime,status, ids);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费收支编号：{0}的审核状态",Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 报销完成时根据团队编号自动审核导游报账时添加的其他收入
        /// </summary>
        /// <param name="auditDeptId">审核人部门编号</param>
        /// <param name="auditId">审核人编号</param>
        /// <param name="audit">审核人</param>
        /// <param name="auditTime">审核时间</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeInAudit(int auditDeptId, string auditId, string audit, DateTime auditTime, string tourId)
        {
            if (string.IsNullOrEmpty(tourId))
            {
                return 0;
            }
            var ids = this._dal.GetOtherFeeInLst(tourId).Select(m=>m.Id).ToArray();
            var isOk = this.SetOtherFeeInOutAudit(ItemType.收入, auditDeptId, auditId, audit, "报销完成自动审核", auditTime, FinStatus.账务待支付, ids);

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）支出费用编号集合可以批量支付
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="status">状态</param>
        /// <param name="lst">其他（杂费）支出费用编号和支付方式集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int SetOtherFeeOutPay(int accountantDeptId, string accountantId, string accountant, DateTime payTime, FinStatus status, IList<MBatchPay> lst)
        {
            if (lst == null || lst.Count == 0 || string.IsNullOrEmpty(accountantId) || string.IsNullOrEmpty(accountant))
            {
                return 0;
            }
            var isOk = this._dal.SetOtherFeeOutPay(accountantDeptId, accountantId, accountant, payTime,status, lst);
            if (isOk > 0)
            {
                foreach (var m in lst)
                {
                    //添加操作日志
                    SysStructure.BSysLogHandle.Insert(string.Format("修改了杂费支出编号：{0}的支付状态", m.RegisterId));
                }
            }

            return isOk;
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用搜索实体获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mSearch">其他（杂费）收入/支出费用搜索实体</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ItemType typ
                                                        , MOtherFeeInOutBase mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId))
            {
                return null;
            }
            return this._dal.GetOtherFeeInOutLst(pageSize, pageIndex, ref recordCount, typ, mSearch, this.LoginUserId, this.GetDataPrivs(typ== ItemType.收入? Menu2.财务管理_其他收入:Menu2.财务管理_其他支出, out _isOnlySelf));
        }

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        public IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids)
        {
            if (ids==null || ids.Length==0)
            {
                return null;
            }
            return this._dal.GetOtherFeeInOutLst(typ,ids);
        }

        #endregion

        #region 应付管理

        /// <summary>
        /// 根据应付帐款搜索实体获取应付帐款/已结清账款列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSum">金额汇总信息<!--<root><row Confirmation="应付金额" Prepaid="已付金额" UnChecked="已登待付"/></root>--></param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <returns>应付帐款/已结清账款列表</returns>
        public IList<MPayable> GetPayableLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref MPayableSum mSum
                                        , MPayableBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch==null||string.IsNullOrEmpty(mSearch.CompanyId)||pageIndex<=0||pageSize<=0)
            {
                return null;
            }
            var lst= this._dal.GetPayableLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应付管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                mSum.TotalPayable = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Confirmation"));
                mSum.TotalPaid = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "Prepaid"));
                mSum.TotalUnchecked = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "UnChecked"));
            }
            return lst;
        }

        /// <summary>
        /// 根据搜索实体获取当天付款对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sum">金额汇总信息<!--<root><row PaymentAmount="请款金额"/></root>--></param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <returns>当天付款对账列表</returns>
        public IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref decimal sum
                                        , MPayRegister mSearch)
        {
            var xmlSum = string.Empty;
            var lst= this._dal.GetTodayPaidLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_应付管理, out _isOnlySelf));
            if (!string.IsNullOrEmpty(xmlSum))
            {
                sum = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "PaymentAmount"));
            }
            return lst;
        }

        /// <summary>
        /// 根据计调编号集合获取批量出账登记列表
        /// </summary>
        /// <param name="planIds">计调编号集合</param>
        /// <returns>登记列表</returns>
        public IList<MPayRegister> GetPayRegisterBaseByPlanId(params string[] planIds)
        {
            if (planIds == null || planIds.Length==0)
            {
                return null;
            }
            return planIds.Select(planId => this.GetPayRegisterBaseByPlanId(planId)).Where(m=>m.Register<m.Payable).ToList();
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目出账登记基本信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>登记基本信息</returns>
        public MPayRegister GetPayRegisterBaseByPlanId(string planId)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetPayRegisterBaseByPlanId( planId);
        }

        /// <summary>
        /// 根据计调编号获取某一个支出项目出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(string planId,bool? isPrepaid)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetPayRegisterLstByPlanId(planId,isPrepaid);
        }

        /// <summary>
        /// 根据计调编号集合获取支出项目出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>出账登记列表</returns>
        public IList<MRegister> GetPayRegisterLstByPlanId(params string[] planId)
        {
            var lst = new List<MRegister>();
            if (planId!=null && planId.Length>0)
            {
                foreach (var i in planId)
                {
                    lst.AddRange(this.GetPayRegisterLstByPlanId(i,(bool?)null));
                }
            }
            return lst;
        }

        ///// <summary>
        ///// 根据团队编号获取计调登记列表实体
        ///// </summary>
        ///// <param name="tourId">团队编号</param>
        ///// <returns>应付账款登记实体</returns>
        //public MPayablePayment GetPayablePaymentByTourId(string tourId)
        //{
        //    return string.IsNullOrEmpty(tourId) ? null : this._dal.GetPayablePaymentByTourId(tourId);
        //}

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款 2：预存款余额不足</returns>
        public int AddRegister(MRegister mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.PlanId)||string.IsNullOrEmpty(mdl.Dealer)||string.IsNullOrEmpty(mdl.OperatorId))
            {
                return 0;
            }
            var isOk= this._dal.AddRegister(mdl);
            if (isOk>0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(mdl.IsPrepaid?"新增预付申请":"新增出账登记");
            }

            return isOk;
        }

        /// <summary>
        /// 批量添加多个登记帐款
        /// </summary>
        /// <param name="lst">登记列表</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int AddRegister(IList<MRegister> lst)
        {
            var isOk = 0;
            foreach (var m in lst)
            {
                isOk = this.AddRegister(m);
                if (isOk<=0)
                {
                    break;
                }
            }
             return isOk;
        }

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        public int UpdRegister(MRegister mdl)
        {
            if (mdl == null || string.IsNullOrEmpty(mdl.Dealer) || string.IsNullOrEmpty(mdl.OperatorId)||mdl.RegisterId<=0)
            {
                return 0;
            }
            var isOk = this._dal.UpdRegister(mdl);
            if (isOk==1)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了登记编号：{0}的出账登记",mdl.RegisterId));
            }

            return isOk;
        }

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        public bool DelRegister(string companyId, int registerId)
        {
            if (registerId<=0 || string.IsNullOrEmpty(companyId))
            {
                return false;
            }
            var isOk= this._dal.DelRegister(companyId, registerId);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了登记编号：{0}出账登记",registerId));
            }

            return isOk;
        }

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="paymentAmount">金额汇总信息<!--<root><row PaymentAmount="请款金额"/></root>--></param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <returns>付款审批列表</returns>
        public IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref decimal paymentAmount
                                                        , MPayableApproveBase mSearch)
        {
            var xmlSum = string.Empty;
            if (mSearch==null||string.IsNullOrEmpty(mSearch.CompanyId))
            {
                return null;
            }
            var lst= this._dal.GetMPayableApproveLst(pageSize, pageIndex, ref recordCount, ref xmlSum, mSearch, this.LoginUserId, this.GetDataPrivs(mSearch.SL, out _isOnlySelf));

            if (!string.IsNullOrEmpty(xmlSum))
            {
                paymentAmount = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(xmlSum, "PaymentAmount"));
            }

            return lst;
        }

        /// <summary>
        /// 根据登记编号获取登记实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>登记实体</returns>
        public MRegister GetRegisterById(string companyId, int registerId)
        {
            return registerId==0 || string.IsNullOrEmpty(companyId) ? null : this._dal.GetRegisterById(companyId, registerId);
        }

        /// <summary>
        /// 根据登记编号集合获取登记列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerIds">登记编号集合</param>
        /// <returns>登记列表</returns>
        public IList<MRegister> GetRegisterById(string companyId, params int[] registerIds)
        {
            if (registerIds == null || registerIds.Length==0)
            {
                return null;
            }
            return registerIds.Select(registerId => this.GetRegisterById(companyId, registerId)).ToList();
        }

        /// <summary>
        /// 根据登记编号集合设置登记审核状态
        /// </summary>
        /// <param name="approverId">审核人编号</param>
        /// <param name="approver">审核人</param>
        /// <param name="approveTime">审核时间</param>
        /// <param name="approveRemark">审核意见</param>
        /// <param name="status">审核</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerIds">登记编号集合</param>
        /// <returns>正数：成功 负数或0：失败</returns>
        public int SetRegisterApprove(string approverId
                                    , string approver
                                    , DateTime approveTime
                                    , string approveRemark
                                    , FinStatus status
                                    , string companyId
                                    , params int[] registerIds)
        {
            var isOk =0;
            if (registerIds==null||registerIds.Length<=0||string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            isOk = this._dal.SetRegisterApprove(approverId, approver, approveTime, approveRemark, status,companyId, registerIds);
            if (isOk > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("修改了出账登记审核状态");
            }
            return isOk;
        }

        /// <summary>
        /// 根据登记编号集合设置支付状态
        /// </summary>
        /// <param name="accountantDeptId">出纳部门编号</param>
        /// <param name="accountantId">出纳编号</param>
        /// <param name="accountant">出纳</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="lst">批量支付列表</param>
        /// <returns>1：成功 0：失败 2：预存款余额不足</returns>
        public int SetRegisterPay(int accountantDeptId
                                , string accountantId
                                , string accountant
                                , DateTime payTime
                                , string companyId
                                , IList<MBatchPay> lst)
        {
            var isOk = 0;
            if (lst == null || lst.Count <= 0 || string.IsNullOrEmpty(accountantId) || string.IsNullOrEmpty(accountant) || accountantDeptId <= 0 || string.IsNullOrEmpty(companyId))
            {
                return 0;
            }
            isOk = this._dal.SetRegisterPay(accountantDeptId, accountantId, accountant, payTime, companyId, lst);
            if (isOk > 0)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("修改了出账登记支付状态");
            }

            return isOk;
        }

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        public MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ)
        {
            return this._dal.GetPlanCostPayMdl(tourId, typ);
        }

        /// <summary>
        /// 根据团队编号、计调类型、是否确认获取计调成本确认列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <param name="isConfirmed">是否确认</param>
        /// <returns>计调成本确认列表</returns>
        public IList<MPlanCostConfirm> GetPlanCostConfirmLst(string tourId, PlanProject typ, bool isConfirmed)
        {
            return this._dal.GetPlanCostConfirmLst(tourId, typ, isConfirmed);
        }

        /// <summary>
        /// 根据计调编号设置计调成本确认
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="costId">成本确认人ID</param>
        /// <param name="costName">成本确认人</param>
        /// <param name="costRemark">成本确认备注</param>
        /// <param name="confirmation">确认金额/结算金额</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetPlanCostConfirmed(string planId, string costId, string costName, string costRemark, decimal confirmation)
        {

            var isOk = this._dal.SetPlanCostConfirmed(planId, costId, costName,costRemark,confirmation);
            if (isOk )
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了计调编号：{0} 的成本确认状态", planId));
            }
            return isOk;
        }

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体（Id=0：添加 Id>0：修改）</param>
        /// <returns>1：成功 0：失败 -1：已审</returns>
        public int AddOrUpdDebit(MDebit mdl)
        {
            if (mdl==null||string.IsNullOrEmpty(mdl.CompanyId)||string.IsNullOrEmpty(mdl.TourId)||string.IsNullOrEmpty(mdl.Borrower))
            {
                return 0;
            }
            if (mdl.Id>0)
            {
                var m = this._dal.GetDebit(mdl.Id);
                if (m!=null&&m.Status!=FinStatus.财务待审批)
                {
                    return -1;
                }
            }
            var isOk = this._dal.AddOrUpdDebit(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(
                    "操作员编号：" + this.LoginUserId + (mdl.Id > 0 ? "修改了财务借款编号：" + mdl.Id : "新增了财务借款"));
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetDebitApprove(MDebit mdl)
        {
            if (mdl == null || mdl.Id<=0||string.IsNullOrEmpty(mdl.CompanyId))
            {
                return false;
            }
            var isOk= this._dal.SetDebitApprove(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了财务借款编号：{0}的审批状态",mdl.Id));
            }

            return isOk;
        }

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        public MDebit GetDebit(int id)
        {
            return id<=0 ? null : this._dal.GetDebit(id);
        }

        /// <summary>
        /// 根据借款搜索实体获取借款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">借款搜索实体</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , MDebitBase mSearch)
        {
            if (mSearch == null || string.IsNullOrEmpty(mSearch.CompanyId)||pageSize<=0||pageIndex<=0)
            {
                return null;
            }
            return this._dal.GetDebitLst(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_借款管理, out _isOnlySelf));
        }

        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByTourId(string tourId,bool isBz)
        {
            return string.IsNullOrEmpty(tourId) ? null: this._dal.GetDebitLstByTourId(tourId,isBz);
        }

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        public IList<MDebit> GetDebitLstByPlanId(string planId)
        {
            return string.IsNullOrEmpty(planId) ? null : this._dal.GetDebitLstByPlanId(planId);
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool PayDebit(MDebit mdl)
        {
            if (mdl == null || mdl.Id <= 0 || string.IsNullOrEmpty(mdl.CompanyId))
            {
                return false;
            }

            var isOk=this._dal.Pay(mdl);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("修改了财务借款编号：{0}，实借金额：{1}的支付状态",mdl.Id,mdl.RealAmount));
            }

            return isOk;
        }

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>1：成功 0：失败 -1：已审核</returns>
        public int DeleteDebit(string companyId, int id)
        {
            if (string.IsNullOrEmpty(companyId)||id<=0)
            {
                return 0;
            }
            var m = this._dal.GetDebit(id);
            if (m!=null)
            {
                if (m.Status != FinStatus.财务待审批)
                {
                    return -1;
                }
            }
            var isOk = this._dal.DeleteDebit(companyId, id);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了财务借款编号：{0}的数据", id));
                return 1;
            }

            return 0;
        }
        #endregion

        #region 财务情况登记
        /// <summary>
        /// 财务情况登记新增/修改
        /// </summary>
        /// <param name="m">财务情况登记实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdDengJi(MCaiWuDengJi m)
        {
            if (m == null || string.IsNullOrEmpty(m.CompanyId))
            {
                return false;
            }
            var isOk = this._dal.AddOrUpdDengJi(m);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("操作员编号：" + this.LoginUserId + (m.Id > 0 ? "修改了财务情况登记编号：" + m.Id : "新增了财务情况登记"));
            }

            return isOk;
        }

        /// <summary>
        /// 财务情况登记删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelDengJi(string companyId,int[] ids)
        {
            if (ids == null || ids.Length == 0 || string.IsNullOrEmpty(companyId))
            {
                return false;
            }
            var isOk = this._dal.DelDengJi(companyId, ids);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了财务情况登记编号：{0}", Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据主键ID获取财务登记实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>财务登记实体</returns>
        public MCaiWuDengJi GetDengJiMdl(string companyId, int id)
        {
            return string.IsNullOrEmpty(companyId) || id == 0 ? null : this._dal.GetDengJiMdl(companyId, id);
        }

        /// <summary>
        /// 获取财务登记列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>财务登记列表</returns>
        public IList<MCaiWuDengJi> GetDengJiList(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MCaiWuDengJiBase mSearch)
        {
            return this._dal.GetDengJiList(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_财务情况登记, out _isOnlySelf));
        }
        #endregion

        #region 购物统计表
        /// <summary>
        /// 获取购物统计列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns></returns>
        public IList<MGouWuTongJi> GetGouWuTongJi(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MGouWuTongJiBase mSearch)
        {
            return this._dal.GetGouWuTongJi(pageSize, pageIndex, ref  recordCount, mSearch);
        }

        /// <summary>
        /// 获取购物统计明细列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sourceId">供应商ID</param>
        /// <returns></returns>
        public IList<MGouWuTongJiDetail> GetGouWuTongJi(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , string sourceId)
        {
            return this._dal.GetGouWuTongJi(pageSize, pageIndex, ref  recordCount, sourceId);
        }
        #endregion

        #region 签单挂失
        /// <summary>
        /// 签单挂失新增/修改
        /// </summary>
        /// <param name="m">签单挂失实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdGuaShi(MQianDanGuaShi m)
        {
            if (m == null || string.IsNullOrEmpty(m.CompanyId))
            {
                return false;
            }
            var isOk = this._dal.AddOrUpdGuaShi(m);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert("操作员编号：" + this.LoginUserId + (m.Id > 0 ? "修改了签单挂失编号：" + m.Id : "新增了签单挂失"));
            }

            return isOk;
        }

        /// <summary>
        /// 签单挂失删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        public bool DelGuaShi(string companyId, int[] ids)
        {
            if (ids == null || ids.Length == 0 || string.IsNullOrEmpty(companyId))
            {
                return false;
            }
            var isOk = this._dal.DelGuaShi(companyId,ids);
            if (isOk)
            {
                //添加操作日志
                SysStructure.BSysLogHandle.Insert(string.Format("删除了签单挂失编号：{0}", Utils.GetSqlIdStrByArray(ids)));
            }

            return isOk;
        }

        /// <summary>
        /// 根据主键ID获取签单挂失实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>签单挂失实体</returns>
        public MQianDanGuaShi GetGuaShiMdl(string companyId,int id)
        {
            return string.IsNullOrEmpty(companyId) || id == 0 ? null : this._dal.GetGuaShiMdl(companyId, id);
        }

        /// <summary>
        /// 获取签单挂失列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns>签单挂失列表</returns>
        public IList<MQianDanGuaShi> GetGuaShiList(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , MQianDanGuaShiBase mSearch)
        {
            return this._dal.GetGuaShiList(pageSize, pageIndex, ref recordCount, mSearch, this.LoginUserId, this.GetDataPrivs(Menu2.财务管理_签单遗失, out _isOnlySelf));
        }
        #endregion
    }
}
