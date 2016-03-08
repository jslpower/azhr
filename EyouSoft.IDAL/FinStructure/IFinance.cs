using System.Collections.Generic;

namespace EyouSoft.IDAL.FinStructure
{
    using System;

    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.KingDee;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.PlanStructure;
    using EyouSoft.Model.StatStructure;
    using EyouSoft.Model.TourStructure;

    /// <summary>
    /// 财务管理
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// </summary>
    public interface IFinance
    {
        #region 单团核算

        /// <summary>
        /// 添加利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddProfitDistribute(MProfitDistribute mdl);

        /// <summary>
        /// 修改利润分配
        /// </summary>
        /// <param name="mdl">利润分配实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdProfitDistribute(MProfitDistribute mdl);

        /// <summary>
        /// 删除利润分配
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        bool DelProfitDis(int id, string companyid);

        /// <summary>
        /// 根据分配编号获取利润分配实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companId"></param>
        /// <returns></returns>
        MProfitDistribute GetProfitDistribute(int id, string companId);

        /// <summary>
        /// 根据团队编号获取利润分配列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MProfitDistribute> GetProfitDistribute(string tourId);

            #endregion

        #region 应收管理

        /// <summary>
        /// 根据应收搜索实体获取应收帐款/已结清账款列表和金额汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumField">金额汇总信息</param>
        /// <param name="mSearch">应收搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应收帐款/已结清账款列表</returns>
        IList<MReceivableInfo> GetReceivableInfoLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref object[] sumField
                                                    , MReceivableBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);


        /// <summary>
        /// 根据系统公司编号获取当日收款对账列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="tourTypes">团队类型集合</param>
        /// <param name="isShowDistribution">是否同业分销</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="search">搜索实体</param>
        /// <returns>当日收款对账列表</returns>
        IList<MDayReceivablesChk> GetDayReceivablesChkLst(int pageSize
                                                        , int pageIndex
                                                        , ref int recordCount
                                                        , ref string xmlSum
                                                        , string companyId
                                                        , IList<TourType> tourTypes
                                                        , bool isShowDistribution
                                                        , MDayReceivablesChkBase search
                                                        , string operatorId
                                                        , params int[] deptIds);


        ///// <summary>
        ///// 根据订单编号获取金额确认实体
        ///// </summary>
        ///// <param name="type">0：团队 1：散客</param>
        ///// <param name="orderId">订单编号</param>
        ///// <returns>金额确认实体</returns>
        //MIncomeConfirm GetIncomeConfirmByOrderId(int type,string orderId);

        /// <summary>
        /// 根据订单销售收款/退款的实体设置审核状态
        /// </summary>
        /// <param name="mdl">订单销售收款/退款的实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetTourOrderSalesCheck(MTourOrderSales mdl);

        /// <summary>
        /// 根据团队编号获取导游实收列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MTourOrderSales> GetTourOrderSalesLstByTourId(string tourId);

        /// <summary>
        /// 根据订单编号集合获取未审核销售收款列表
        /// </summary>
        /// <param name="orderIds">订单编号集合</param>
        /// <returns>未审核销售收款列表</returns>
        IList<MTourOrderSales> GetBatchTourOrderSalesCheck(params string[] orderIds);

        #endregion

        #region 杂费收支

        /// <summary>
        /// 添加其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl);

        /// <summary>
        /// 修改其他（杂费）收入/支出费用
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mdl">其他费用收入/支出实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool UpdOtherFeeInOut(ItemType typ, MOtherFeeInOut mdl);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号集合可以批量删除
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用编号集合</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        int DelOtherFeeInOut(string companyId, ItemType typ,params int[] ids);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用编号获取其他（杂费）收入/支出费用实体
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="id">其他（杂费）收入/支出费用编号</param>
        /// <param name="companyId">系统公司编号</param>
        /// <returns>其他（杂费）收入/支出费用实体</returns>
        MOtherFeeInOut GetOtherFeeInOut(ItemType typ, int id,string companyId);

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
        int SetOtherFeeInOutAudit(ItemType typ,int auditDeptId, string auditId, string audit, string auditRemark, DateTime auditTime,FinStatus status, params int[] ids);

        /// <summary>
        /// 根据团队编号获取导游报账时添加的其他收入列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        IList<MOtherFeeInOut> GetOtherFeeInLst(string tourId);

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
        int SetOtherFeeOutPay(int accountantDeptId, string accountantId, string accountant, DateTime payTime, FinStatus status, IList<MBatchPay> lst);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用搜索实体获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="mSearch">其他（杂费）收入/支出费用搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        IList<MOtherFeeInOut> GetOtherFeeInOutLst(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , ItemType typ
                                                , MOtherFeeInOutBase mSearch
                                                , string operatorId
                                                , params int[] deptIds);

        /// <summary>
        /// 根据其他（杂费）收入/支出费用登记编号获取其他（杂费）收入/支出费用实体列表
        /// </summary>
        /// <param name="typ">收入/支出类型</param>
        /// <param name="ids">其他（杂费）收入/支出费用登记编号集合</param>
        /// <returns>其他（杂费）收入/支出费用实体列表</returns>
        IList<MOtherFeeInOut> GetOtherFeeInOutLst(ItemType typ, params int[] ids);


        #endregion

        #region 应付管理

        /// <summary>
        /// 根据应付帐款搜索实体获取应付帐款/已结清账款列表和汇总信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>应付帐款/已结清账款列表</returns>
        IList<MPayable> GetPayableLst(int pageSize
                                    , int pageIndex
                                    , ref int recordCount
                                    , ref string xmlSum
                                    , MPayableBase mSearch
                                    , string operatorId
                                    , params int[] deptIds);

        /// <summary>
        /// 根据搜索实体获取当天付款对账列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">应付帐款搜索实体</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>当天付款对账列表</returns>
        IList<MRegister> GetTodayPaidLst(int pageSize
                                        , int pageIndex
                                        , ref int recordCount
                                        , ref string xmlSum
                                        , MPayRegister mSearch
                                        , string operatorId
                                        , params int[] deptIds);

        /// <summary>
        /// 根据计调编号获取某一个支出项目登记信息
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>某一个支出项目登记信息</returns>
        MPayRegister GetPayRegisterBaseByPlanId(string planId);

        /// <summary>
        /// 根据计调编号获取某一个计调项目的出账登记列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="isPrepaid">是否预付申请</param>
        /// <returns>出账登记列表</returns>
        IList<MRegister> GetPayRegisterLstByPlanId(string planId,bool? isPrepaid);

        ///// <summary>
        ///// 根据团队编号获取计调登记列表实体
        ///// </summary>
        ///// <param name="tourId">团队编号</param>
        ///// <returns>应付账款登记实体</returns>
        //MPayablePayment GetPayablePaymentByTourId(string tourId);

        /// <summary>
        /// 添加一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款 2：预存款余额不足</returns>
        int AddRegister(MRegister mdl);

        /// <summary>
        /// 修改一个登记帐款
        /// </summary>
        /// <param name="mdl">登记实体</param>
        /// <returns>1：成功 0：失败 -1：超额付款</returns>
        int UpdRegister(MRegister mdl);

        /// <summary>
        /// 删除一个登记帐款
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>True：成功 Flase：失败</returns>
        bool DelRegister(string companyId, int registerId);

        /// <summary>
        /// 根据付款审批搜索实体获取付款审批列表和汇总
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="xmlSum">金额汇总信息</param>
        /// <param name="mSearch">付款审批搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>付款审批列表</returns>
        IList<MPayableApprove> GetMPayableApproveLst(int pageSize
                                                    , int pageIndex
                                                    , ref int recordCount
                                                    , ref string xmlSum
                                                    , MPayableApproveBase mSearch
                                                    , string operatorId
                                                    , params int[] deptIds);

        /// <summary>
        /// 根据登记编号获取登记实体
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="registerId">登记编号</param>
        /// <returns>登记实体</returns>
        MRegister GetRegisterById(string companyId, int registerId);


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
        int SetRegisterApprove(string approverId
                                , string approver
                                , DateTime approveTime
                                , string approveRemark
                                , FinStatus status
                                , string companyId
                                , params int[] registerIds);

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
        int SetRegisterPay(
            int accountantDeptId,
            string accountantId,
            string accountant,
            DateTime payTime,
            string companyId,
            IList<MBatchPay> lst);

        /// <summary>
        /// 根据团队编号、计调类型获取计调支付实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <returns>计调支付实体</returns>
        MPlanCostPay GetPlanCostPayMdl(string tourId, PlanProject typ);

        /// <summary>
        /// 根据团队编号、计调类型、是否确认获取计调成本确认列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="typ">计调类型</param>
        /// <param name="isConfirmed">是否确认</param>
        /// <returns>计调成本确认列表</returns>
        IList<MPlanCostConfirm> GetPlanCostConfirmLst(string tourId, PlanProject typ,bool isConfirmed);

        /// <summary>
        /// 根据计调编号设置计调成本确认
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="costId">成本确认人ID</param>
        /// <param name="costName">成本确认人</param>
        /// <param name="costRemark">成本确认备注</param>
        /// <param name="confirmation">确认金额/结算金额</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetPlanCostConfirmed(
            string planId, string costId, string costName, string costRemark, decimal confirmation);

        #endregion

        #region 借款管理

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdDebit(MDebit mdl);

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool SetDebitApprove(MDebit mdl);

        /// <summary>
        /// 根据借款编号获取借款实体
        /// </summary>
        /// <param name="id">借款编号</param>
        /// <returns>借款实体</returns>
        MDebit GetDebit(int id);

        /// <summary>
        /// 根据借款搜索实体获取借款列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">借款搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLst(int pageSize
                                , int pageIndex
                                , ref int recordCount
                                , MDebitBase mSearch
                                , string operatorId
                                , params int[] deptIds);
        /// <summary>
        /// 根据团队编号获取借款列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isBz">是否报账</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLstByTourId(string tourId,bool isBz);

        /// <summary>
        /// 根据计调编号获取借款列表
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>借款列表</returns>
        IList<MDebit> GetDebitLstByPlanId(string planId);

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="mdl">借款实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool Pay(MDebit mdl);

        /// <summary>
        /// 删除借款
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">借款编号</param>
        /// <returns>True：成功 False：失败</returns>
        bool DeleteDebit(string companyId, int id);

        #endregion

        #region 财务情况登记

        /// <summary>
        /// 财务情况登记新增/修改
        /// </summary>
        /// <param name="m">财务情况登记实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdDengJi(MCaiWuDengJi m);

        /// <summary>
        /// 财务情况登记删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelDengJi(string companyId, int[] ids);

        /// <summary>
        /// 根据主键ID获取财务登记实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>财务登记实体</returns>
        MCaiWuDengJi GetDengJiMdl(string companyId, int id);

        /// <summary>
        /// 获取财务登记列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>财务登记列表</returns>
        IList<MCaiWuDengJi> GetDengJiList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            MCaiWuDengJiBase mSearch,
            string operatorId,
            params int[] deptIds);
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
        IList<MGouWuTongJi> GetGouWuTongJi(int pageSize, int pageIndex, ref int recordCount, MGouWuTongJiBase mSearch);

        /// <summary>
        /// 获取购物统计明细列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sourceId">供应商ID</param>
        /// <returns></returns>
        IList<MGouWuTongJiDetail> GetGouWuTongJi(int pageSize, int pageIndex, ref int recordCount, string sourceId);
        #endregion

        #region 签单挂失

        /// <summary>
        /// 签单挂失新增/修改
        /// </summary>
        /// <param name="m">签单挂失实体</param>
        /// <returns>True：成功 False：失败</returns>
        bool AddOrUpdGuaShi(MQianDanGuaShi m);

        /// <summary>
        /// 签单挂失删除
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="ids">主键ID</param>
        /// <returns>True：成功 False：失败</returns>
        bool DelGuaShi(string companyId, int[] ids);

        /// <summary>
        /// 根据主键ID获取签单挂失实体
        /// </summary>
        /// <param name="companyId">系统公司ID</param>
        /// <param name="id">主键ID</param>
        /// <returns>签单挂失实体</returns>
        MQianDanGuaShi GetGuaShiMdl(string companyId, int id);

        /// <summary>
        /// 获取签单挂失列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="operatorId">当前操作者编号</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <returns>签单挂失列表</returns>
        IList<MQianDanGuaShi> GetGuaShiList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            MQianDanGuaShiBase mSearch,
            string operatorId,
            params int[] deptIds);

        #endregion
    }
}
