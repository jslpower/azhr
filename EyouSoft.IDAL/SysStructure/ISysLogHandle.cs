using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;

namespace EyouSoft.IDAL.SysStructure
{
    /// <summary>
    /// 操作日志数据访问类接口
    /// </summary>
    public interface ISysLogHandle
    {
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="item">操作日志信息业务实体</param>
        /// <returns></returns>
        bool Insert(MSysLogHandleInfo info);
        /// <summary>
        /// 获取操作日志信息
        /// </summary>
        /// <param name="logId">日志编号</param>
        /// <returns></returns>
        MSysLogHandleInfo GetInfo(string logId);
        /// <summary>
        /// 获取操作日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门集合</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>        
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<MSysLogHandleInfo> GetLogs(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, MSysLogHandleSearch searchInfo);
        /// <summary>
        /// 获取登录日志信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<MSysLogLoginInfo> GetLoginLogs(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysLogLoginSearchInfo searchInfo);
    }
}
