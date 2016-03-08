using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ComStructure
{
    using EyouSoft.Model.ComStructure;

    /// <summary>
    /// 导游档案数据访问类接口
    /// </summary>
    public interface IDaoYou
    {
        /// <summary>
        /// 导游档案新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int CU(EyouSoft.Model.ComStructure.MDaoYouInfo info);
        /// <summary>
        /// 获取导游档案实体
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        EyouSoft.Model.ComStructure.MDaoYouInfo GetInfo(string daoYouId);
        /// <summary>
        /// 获取导游导游信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MDaoYouInfo> GetDaoYous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouChaXunInfo chaXun);
        /// <summary>
        /// 删除导游信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        int Delete(string companyId, string daoYouId);
        /// <summary>
        /// 获取导游带团明细信息集合
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo> GetDaoYouDaiTuanXXs(string daoYouId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXChaXunInfo chaXun);
        /// <summary>
        /// 获取导游上团统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo> GetDaoYouShangTuanTongJi(string companyId, EyouSoft.Model.ComStructure.MDaoYouShangTuanChaXunInfo chaXun);

        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获取排班统计列表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="tourcode">团号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="NextTimeStart">出团时间—开始</param>
        /// <param name="NextTimeEnd">出团时间—截至</param>
        /// <param name="tourmark">团态标识</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<MGuidePlanWork> GetPaiBanTongJi(
            string CompanyId,
            string tourcode,
            int year,
            int month,
            DateTime? NextTimeStart,
            DateTime? NextTimeEnd,
            string tourmark,
            int pageIndex,
            int pageSize,
            ref int recordCount);

        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        IList<MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date);

    }
}
