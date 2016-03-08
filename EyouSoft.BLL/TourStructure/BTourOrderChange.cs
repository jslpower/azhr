using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 描述：订单变更业务层
    /// 修改记录：
    /// 1、2011-09-05 PM 王磊 创建
    /// </summary>
    public class BTourOrderChange : EyouSoft.BLL.BLLBase
    {

        private readonly EyouSoft.IDAL.TourStructure.ITourOrderChange dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ITourOrderChange>();



        /// <summary>
        /// 确认订单变更
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="surePerson">确认编号</param>
        /// <param name="surePersonId">确认人</param>
        /// <returns>1:成功 0:失败</returns>
        public bool UpdateTourOrderChange(string id, string surePersonId, string surePerson)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(surePersonId) || string.IsNullOrEmpty(surePerson))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(id)==true.");
            }
            if (dal.UpdateTourOrderChange(id, surePersonId, surePerson) == 1)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("订单变更已确定，变更编号：{0}", id);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;


        }

        /// <summary>
        /// 根据订单的编号、（修改或变更）获取订单变更列表
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="changeType">变更或修改</param>
        /// <returns></returns>
        public IList<MTourOrderChange> GetTourOrderChangeList(string OrderId, EyouSoft.Model.EnumType.TourStructure.ChangeType? changeType)
        {
            if (string.IsNullOrEmpty(OrderId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(companyId)==true.");
            }
            return dal.GetTourOrderChangeList(OrderId, changeType);


        }



        /// <summary>
        /// 根据编号获取订单变更的详细信息
        /// </summary>
        /// <param name="id">变更Id 主键编号</param>
        /// <returns></returns>
        public MTourOrderChange GetTourOrderChangById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(companyId)==true.");
            }
            return dal.GetTourOrderChangById(id);
        }



        /// <summary>
        /// 查询获取订单变更列表 分页
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MTourOrderChange> GetTourOrderChangeList(string companyId
            , EyouSoft.Model.EnumType.TourStructure.ChangeType? changeType
            , int pageSize
            , int pageindex
            , ref int recordCount)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new System.Exception("bll error:查询id为null。string.IsNullOrEmpty(companyId)==true.");
            }

            //权限控制
            bool _isOnlySelf;
            int[] deptIds = this.GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.计调中心_业务变更, out _isOnlySelf);
            return dal.GetTourOrderChangeList(companyId, changeType, pageSize, pageindex, ref recordCount, deptIds, _isOnlySelf, this.LoginUserId);

        }



    }
}
