using System.Collections.Generic;

namespace EyouSoft.BLL.StatStructure
{
    using EyouSoft.Component.Factory;
    using EyouSoft.IDAL.StatStructure;
    using EyouSoft.Model.EnumType.ComStructure;
    using EyouSoft.Model.EnumType.StatStructure;
    using EyouSoft.Model.StatStructure;

    /// <summary>
    /// 统计分析
    /// 创建者：郑知远
    /// 创建时间：2011-09-06
    /// </summary>
    public class BStatistics : BLLBase
    {
        private readonly IStatistics _idal = ComponentFactory.CreateDAL<IStatistics>();

        #region 线路流量统计

        /// <summary>
        /// 获取线路流量统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DeptId">分公司部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MRouteFlow> GetRouteFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int DeptId, MRouteFlowSearch search)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, DeptId));
            return _idal.GetRouteFlowLst(companyId, pageSize, pageIndex, ref recordCount, deptids, search);
        }

        /// <summary>
        /// 获取线路流量统计团队数量列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptId">分公司部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowTourList> GetRouteFlowTourListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount, int deptId, MRouteFlowSearch search)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, deptId));
            return _idal.GetRouteFlowTourListByAreaId(companyId, pageSize, pageIndex, ref recordCount, deptids, search);
        }

        /// <summary>
        /// 获取线路流量统计总收入列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param>   
        /// <param name="deptId">分公司部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowOrderList> GetRouteFlowtOrderListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , ref string sumMoney, int deptId, MRouteFlowSearch search)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, deptId));
            return _idal.GetRouteFlowtOrderListByAreaId(
                companyId, pageSize, pageIndex, ref recordCount, ref sumMoney, deptids, search);
        }

        /// <summary>
        /// 获取线路流量统计总支出列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumMoney">返回总金额</param>  
        ///  <param name="deptId">部门编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRouteFlowPayList> GetRouteFlowPayListByAreaId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , ref string sumMoney, int deptId, MRouteFlowSearch search)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, deptId));
            return _idal.GetRouteFlowPayListByAreaId(
                companyId, pageSize, pageIndex, ref recordCount, ref sumMoney, deptids, search);
        }
        #endregion

        #region 部门业绩统计

        /// <summary>
        /// 获取部门业绩统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <param name="DeptId">分公司部门编号</param>
        /// <param name="TongJi">统计实体</param>
        /// <returns>部门业绩统计列表</returns>
        public IList<MDepartment> GetDepartmentLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int DeptId, MDepartmentSearch search, ref MDepartmentTongJi TongJi)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, DeptId));
            return _idal.GetDepartmentLst(companyId, pageSize, pageIndex, ref recordCount, deptids, search, ref TongJi);
        }

        /// <summary>
        /// 获取部门业绩统计员工人数列表
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sumCompanyId">分公司部门编号</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">统计实体</param> 
        /// <returns>员工人数</returns>
        public IList<MDepartmentPeopleList> GetDepartmentPeopleListByDeptId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , int sumCompanyId, MDepartmentSearch search, ref MDepartmentPeopleListTongJi tongJi)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, sumCompanyId));
            return _idal.GetDepartmentPeopleListByDeptId(
                companyId, pageSize, pageIndex, ref recordCount, deptids, search, ref tongJi);
        }

        #endregion

        #region 个人业绩统计

        /// <summary>
        /// 个人业绩统计
        /// </summary>
        /// <param name="companyId">系统公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DeptId">分公司部门编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>个人业绩统计列表</returns>
        public IList<MPersonal> GetPersonalLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int DeptId, MPersonalSearch search)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, DeptId));
            return _idal.GetPersonalLst(companyId, pageSize, pageIndex, ref recordCount, deptids, search);
        }

        /// <summary>
        /// 个人业绩统计订单列表
        /// </summary>
        ///<param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="sunCompanyId">分公司部门编号</param>
        /// <param name="search">查询实体</param>
        /// <param name="tongJi">统计实体</param>  
        /// <returns></returns>
        public IList<MPersonalOrderList> GetPersonalOrderListBySellerId(string companyId, int pageSize, int pageIndex, ref int recordCount
            , int sunCompanyId, MPersonalSearch search, ref MPersonalOrderListTongJi tongJi)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, sunCompanyId));
            return _idal.GetPersonalOrderListBySellerId(
                companyId, pageSize, pageIndex, ref recordCount, deptids, search, ref tongJi);
        }

        #endregion

        #region 收入对帐单

        /// <summary>
        /// 获取收入对帐单列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DeptId">分公司部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TongJi">统计实体</param>  
        /// <returns></returns>
        public IList<MReconciliation> GetReconciliationLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int DeptId, MReconciliationSearch mSearch, ref MReconciliationTongJi TongJi)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, DeptId));
            return _idal.GetReconciliationLst(companyId, pageSize, pageIndex, ref recordCount, deptids, mSearch, ref TongJi);
        }

        /// <summary>
        /// 获取收入对帐单未收款列表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="deptId">分公司部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="tongJi">统计实体</param>
        /// <returns></returns>
        public IList<MReconciliationRestAmount> GetReconciliationRestAmountLst(string companyId, int pageSize, int pageIndex
            , ref int recordCount, int deptId, MReconciliationSearch mSearch, ref MReconciliationTongJi tongJi)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, deptId));
            return _idal.GetReconciliationRestAmountLst(
                companyId, pageSize, pageIndex, ref recordCount, deptids, mSearch, ref tongJi);
        }

        #endregion

        #region 状态查询表

        /*/// <summary>
        /// 已封团状态查询表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns></returns>
        public IList<MTourStatus> GetTureTourStatusLst(string companyId, int pageSize, int pageIndex, ref int recordCount, MTourStatusSearch mSearch)
        {
            bool isOnlySelf;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] deptIds = GetDataPrivs(Model.EnumType.PrivsStructure.Menu2.统计分析_状态查询表, out  isOnlySelf);
            return _idal.GetTourStatusLst(companyId, pageSize, pageIndex, ref recordCount, true, deptIds, mSearch);

        }

        /// <summary>
        /// 未封团状态查询表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <returns></returns>
        public IList<MTourStatus> GetFalseTourStatusLst(string companyId, int pageSize, int pageIndex, ref int recordCount, MTourStatusSearch mSearch)
        {
            bool isOnlySelf;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] deptIds = GetDataPrivs(Model.EnumType.PrivsStructure.Menu2.统计分析_状态查询表, out  isOnlySelf);
            return _idal.GetTourStatusLst(companyId, pageSize, pageIndex, ref recordCount, false, deptIds, mSearch);
        }*/

        /// <summary>
        /// 状态查询表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="heJi">合计信息[0:int:人数合计]</param>
        /// <returns></returns>
        public IList<MTourStatus> GetTourStatusLst(string companyId, int pageSize, int pageIndex, ref int recordCount, MTourStatusSearch mSearch, out object[] heJi)
        {
            bool isOnlySelf=false;
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] deptIds = null; //GetDataPrivs(Model.EnumType.PrivsStructure.Menu2.统计分析_状态查询表, out  isOnlySelf);
            return _idal.GetTourStatusLst(companyId, pageSize, pageIndex, ref recordCount, deptIds, mSearch, out heJi);
        }
        #endregion

        #region 游客统计表

        /// <summary>
        /// 游客统计表
        /// </summary>
        /// <param name="companyId">公司编号</param> 
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DeptId">分公司部门编号</param>
        /// <param name="mSearch">搜索实体</param>
        /// <param name="TravellerFlowType">游客类型</param>
        /// <returns></returns>
        public IList<MTravellerFlow> GetTravellerFlowLst(string companyId, int pageSize, int pageIndex, ref int recordCount, int DeptId, MTravellerFlowSearch mSearch, EyouSoft.Model.EnumType.IndStructure.TravellerFlowType TravellerFlowType)
        {
            string deptids = GetIdsByArr(GetDepts(companyId, DeptId));
            return _idal.GetTravellerFlowLst(companyId, pageSize, pageIndex, ref recordCount, deptids, mSearch, TravellerFlowType);
        }



        #endregion

        #region 私有方法
        /// <summary>
        /// 将整形Id数组转换为半角逗号分割的字符串
        /// </summary>
        /// <param name="ids">整形Id数组</param>
        /// <returns>半角逗号分割的字符串</returns>
        protected string GetIdsByArr(params int[] ids)
        {
            /*string strIds = string.Empty;
            if (ids == null || ids.Length < 1)
                return strIds;

            strIds = ids.Where(strId => strId > 0).Aggregate(strIds, (current, strId) => current + (strId + ","));

            return strIds.Trim(',');*/

            if (ids == null || ids.Length < 1) return string.Empty;

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (var item in ids)
            {
                s.Append(",");
                s.Append(item);
            }

            return s.ToString().Substring(1);
        }
        #endregion
    }
}
