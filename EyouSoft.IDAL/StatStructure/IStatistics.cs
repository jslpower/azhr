using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.StatStructure
{
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.StatStructure;
    using EyouSoft.Model.StatStructure;

    /// <summary>
    /// 统计分析(供应商，线路，部门，个人)
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// </summary>
    public interface IStatistics
    {
        #region 线路流量统计

        /// <summary>
        /// 获取线路流量统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param> 
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MRouteFlow> GetRouteFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MRouteFlowSearch search);

        /// <summary>
        /// 获取线路流量统计团队数量列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MRouteFlowTourList> GetRouteFlowTourListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MRouteFlowSearch search);

        /// <summary>
        /// 获取线路流量统计总收入列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param>  
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MRouteFlowOrderList> GetRouteFlowtOrderListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount, ref string sumMoney, string deptIds, MRouteFlowSearch search);

        /// <summary>
        /// 获取线路流量统计总支出列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param>  
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MRouteFlowPayList> GetRouteFlowPayListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount, ref string sumMoney, string deptIds, MRouteFlowSearch search);
        #endregion

        #region 部门业绩统计

        /// <summary>
        /// 获取部门业绩统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <param name="TongJi">统计实体</param>
        /// <returns>部门业绩统计列表</returns>
        IList<MDepartment> GetDepartmentLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MDepartmentSearch search, ref MDepartmentTongJi TongJi);

        /// <summary>
        /// 获取部门业绩统计员工人数列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">统计实体</param>
        /// <returns>员工人数</returns>
        IList<MDepartmentPeopleList> GetDepartmentPeopleListByDeptId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MDepartmentSearch search, ref MDepartmentPeopleListTongJi tongJi);

        #endregion

        #region 个人业绩统计

        /// <summary>
        /// 个人业绩统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>个人业绩统计列表</returns>
        IList<MPersonal> GetPersonalLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MPersonalSearch search);

        /// <summary>
        /// 个人业绩统计订单列表
        /// </summary>
        ///<param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">统计实体</param>
        /// <returns></returns>
        IList<MPersonalOrderList> GetPersonalOrderListBySellerId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MPersonalSearch search, ref MPersonalOrderListTongJi tongJi);

        #endregion

        #region 收入对帐单

        /// <summary>
        /// 获取收入对帐单列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TongJi">统计实体</param>
        /// <returns></returns>
        IList<MReconciliation> GetReconciliationLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MReconciliationSearch mSearch, ref MReconciliationTongJi TongJi);

        /// <summary>
        /// 获取收入对帐单未收款列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号集合</param>
        /// <param name="mSearch">查询实体</param>
        /// <param name="tongJi">统计实体</param>
        /// <returns></returns>
        IList<MReconciliationRestAmount> GetReconciliationRestAmountLst(string companyId, int pageSize, int pageIndex, ref int recordCount
            , string deptIds, MReconciliationSearch mSearch, ref MReconciliationTongJi tongJi);

        #endregion

        #region 状态查询表

        /// <summary>
        /// 状态查询表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param> 
        /// <param name="mSearch">搜索实体</param>
        /// <param name="heJi">合计信息[0:int:人数合计]</param>
        /// <returns></returns>
        IList<MTourStatus> GetTourStatusLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int[] deptIds, MTourStatusSearch mSearch, out object[] heJi);

        #endregion

        #region 游客统计表

        /// <summary>
        /// 游客统计表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptIds">部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TravellerFlowType">游客类型</param>
        /// <returns></returns>
        IList<MTravellerFlow> GetTravellerFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, string deptIds, MTravellerFlowSearch mSearch, EyouSoft.Model.EnumType.IndStructure.TravellerFlowType TravellerFlowType);

        #endregion
    }
}
