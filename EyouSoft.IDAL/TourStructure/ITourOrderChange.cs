using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 订单变更
    /// 王磊
    /// 2011-9-5
    /// </summary>
    public interface ITourOrderChange
    {
        #region

        /// <summary>
        /// 确认订单变更
        /// </summary>
        /// <param name="id">变更编号</param>
        /// <param name="surePersonId">确认人编号</param>
        /// <param name="surePerson">确认人</param>
        /// <returns></returns>
        int UpdateTourOrderChange(string id, string surePersonId, string surePerson);

        /// <summary>
        /// 根据订单的编号、（修改或变更）获取订单变更列表
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="changeType">变更或修改</param>
        /// <returns></returns>
        IList<MTourOrderChange> GetTourOrderChangeList(string OrderId, EyouSoft.Model.EnumType.TourStructure.ChangeType? changeType);


        /// <summary>
        /// 根据编号获取订单变更详细信息
        /// </summary>
        /// <param name="id">变更的主键编号</param>
        /// <returns></returns>
        MTourOrderChange GetTourOrderChangById(string id);

        /// <summary>
        /// 获取订单的变更列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="changeType">修改或变更的枚举</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        IList<MTourOrderChange> GetTourOrderChangeList(string CompanyId
             , EyouSoft.Model.EnumType.TourStructure.ChangeType? changeType
             , int pageSize
             , int pageindex
             , ref int recordCount
             , int[] DetpIds
             , bool isOnlySeft
             , string LoginUserId);

        #endregion

    }
}
