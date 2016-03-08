using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.GovStructure
{
    /// <summary>
    /// 职务管理接口
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public interface IPosition
    {
        #region  成员方法
        /// <summary>
        /// 判断职务信息是否存在
        /// </summary>
        /// <param name="PositionName">职务名称</param>
        /// <param name="Id">职务Id,新增Id=0</param>
        /// <returns></returns>
        bool ExistsNum(string PositionName, int Id, string CompanyId);

        /// <summary>
        /// 增加一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        int AddGovPosition(EyouSoft.Model.GovStructure.MGovPosition model);

        /// <summary>
        /// 更新一条职务信息
        /// </summary>
        /// <param name="model">职务model</param>
        /// <returns></returns>
        bool UpdateGovPosition(EyouSoft.Model.GovStructure.MGovPosition model);

        /// <summary>
        /// 获得职务实体
        /// </summary>
        /// <param name="PositionId">职务ID</param>
        /// <returns></returns>
        EyouSoft.Model.GovStructure.MGovPosition GetGovPositionModel(int PositionId, string CompanyId);

        /// <summary>
        /// 获得职务信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        IList<Model.GovStructure.MGovPosition> GetGovPositionList(string CompanyId, int PageSize, int PageIndex, ref int RecordCount);

        /// <summary>
        /// 根据职务ID删除(需判断职务有无人员担任)
        /// </summary>
        /// <param name="PositionIds">职务ID</param>
        /// <returns>0或负值：失败，1成功，2职务正在使用</returns>
        int DeleteGovPosition(params string[] PositionIds);

        #endregion
    }
}
