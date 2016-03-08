using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.Model.TourStructure;


namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 单项业务
    /// 王磊
    /// 2011-9-5
    /// </summary>
    public interface ISingleService
    {
        /// <summary>
        /// 添加单项业务
        /// </summary>
        /// <param name="singleServiceExtend"></param>
        /// <returns></returns>
        int AddSingleService(MSingleServiceExtend singleServiceExtend);


        /// <summary>
        /// 根据团队编号删除单项业务
        /// </summary>
        /// <param name="tourId">团队编号数组</param>
        /// <returns></returns>
        int DeleteSingleServiceByTourId(params string[] tourId); 

        /// <summary>
        /// 更新单项业务的基础信息
        /// </summary>
        /// <param name="singleServiceExtend"></param>
        /// <returns></returns>
        int UpdateSingleService(MSingleServiceExtend singleServiceExtend);

        /// <summary>
        /// 根据团队编号获取单项业务拓展实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        MSingleServiceExtend GetSingleServiceExtendByTourId(string tourId); 


        /// <summary>
        /// 查询获取单项业务的集合
        /// </summary>
        /// <param name="search">查询实体类</param>
        /// <param name="pagesize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="loginId">当前登录人编号</param>
        /// <param name="deptIds">部门编号</param>
        /// <returns></returns>
        IList<MSingleService> GetSingleServiceList(MSeachSingleService search, int pagesize, int pageindex, ref int recordCount, string loginId, int[] deptIds, bool isOnlySeft);


        ///// <summary>
        ///// 单项业务提交财务
        ///// </summary>
        ///// <param name="OrderCode">订单编号数组</param>
        ///// <returns></returns>
        //int CommitFinancial(params string[] OrderCode);


    }
}
