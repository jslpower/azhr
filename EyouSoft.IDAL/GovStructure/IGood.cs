using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 物品管理接口
    /// 2011-09-06 邵权江 创建
    /// </summary>
    public interface IGood
    {
        #region  成员方法
        /// <summary>
        /// 判断物品是否存在
        /// </summary>
        /// <param name="Name">物品名称</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品Id,新增Id=""</param>
        /// <returns></returns>
        bool ExistsNum(string Name, string GoodId, string CompanyId);

        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="model">物品model</param>
        bool AddGovGood(Model.GovStructure.MGovGood model);

        /// <summary>
        /// 更新物品
        /// </summary>
        /// <param name="model">物品model</param>
        /// <returns></returns>
        bool UpdateGovGood(Model.GovStructure.MGovGood model);

        /// <summary>
        /// 获得物品实体
        /// </summary>
        /// <param name="GoodId">物品编号ID</param>
        /// <param name="CompanyId">公司编号ID</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovGood GetGovGoodModel(string GoodId, string CompanyId);

        /// <summary>
        /// 根据条件获得物品信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="Name">物品名称</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovGoodList> GetGovGoodList(string CompanyId, string Name, DateTime? TimeBegin, DateTime? TimeEnd, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据物品编号删除
        /// </summary>
        /// <param name="GoodIds">物品编号ID</param>
        /// <returns>0或负值：失败，1成功，2正在使用</returns>
        int DeleteGovGood(params string[] GoodIds);

        /// <summary>
        /// 根据物品编号获取库存
        /// </summary>
        /// <param name="GoodIds">物品编号ID</param>
        /// <returns></returns>
        int GetGovGoodNum(string GoodId);

        /// <summary>
        /// 增加物品领用/发放/借阅
        /// </summary>
        /// <param name="model">物品领用/发放/借阅model</param>
        /// <returns>正值1:成功； 负值或0:失败；2:超过库存</returns>
        int AddGovGoodUse(Model.GovStructure.MGovGoodUse model);

        /// <summary>
        /// 批量增加物品领用/发放/借阅
        /// </summary>
        /// <param name="list">物品领用/发放/借阅model列表</param>
        /// <returns>0：成功； 正值：失败数量； -1：失败</returns>
        int AddGovGoodUseList(IList<EyouSoft.Model.GovStructure.MGovGoodUse> list);

        /// <summary>
        /// 物品领用/发放/借阅信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GoodId">物品编号</param>
        /// <param name="GoodUseType">使用类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovGoodUseList> GetGovUseGoodList(string CompanyId, string GoodId, int GoodUseType, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据借阅编号归还
        /// </summary>
        /// <param name="IDs">借阅编号ID</param>
        /// <returns>0或负值：失败，1成功</returns>
        int ReturnGovGoodBorrow(params string[] IDs);

        #endregion  成员方法
    }
}
