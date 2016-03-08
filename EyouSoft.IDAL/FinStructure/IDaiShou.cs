//代收相关信息数据访问类接口 汪奇志 2013-04-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.FinStructure
{
    /// <summary>
    /// 代收相关信息数据访问类接口
    /// </summary>
    public interface IDaiShou
    {
        /// <summary>
        /// 写入代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Insert(EyouSoft.Model.FinStructure.MDaiShouInfo info);
        /// <summary>
        /// 更新代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int Update(EyouSoft.Model.FinStructure.MDaiShouInfo info);
        /// <summary>
        /// 删除代收信息，返回1成功，其它失败
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int Delete(string daiShouId, string companyId);
        /// <summary>
        /// 获取代收信息业务实体
        /// </summary>
        /// <param name="daiShouId">代收编号</param>
        /// <returns></returns>
        EyouSoft.Model.FinStructure.MDaiShouInfo GetInfo(string daiShouId);
        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string tourId);
        /// <summary>
        /// 获取代收订单信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.FinStructure.MDaiShouOrderInfo> GetOrders(string tourId);
        /// <summary>
        /// 获取代收计调安排信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.FinStructure.MDaiShouJiDiaoAnPaiInfo> GetAnPais(string tourId);
        /// <summary>
        /// 获取代收信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">数据级浏览权限控制-用户编号</param>
        /// <param name="depts">数据级浏览权限控制-部门编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        IList<EyouSoft.Model.FinStructure.MDaiShouInfo> GetDaiShous(string companyId, string userId, int[] depts, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.FinStructure.MDaiShouChaXunInfo chaXun);
        /// <summary>
        /// 代收审批，返回1成功，其它失败
        /// </summary>
        /// <param name="info">审批实体</param>
        /// <returns></returns>
        int ShenPi(EyouSoft.Model.FinStructure.MDaiShouShenPiInfo info);
    }
}
