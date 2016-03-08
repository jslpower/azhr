using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 上车地点
    /// </summary>
    public interface IComCarLocation
    {
        /// <summary>
        /// 查询该上车地点是否有计划预设
        /// </summary>
        /// <param name="CarLocationId"></param>
        /// <returns></returns>
        bool IsExistsTourCarLocation(string CarLocationId);

        /// <summary>
        /// 添加上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns></returns>
        bool AddCarLocation(EyouSoft.Model.ComStructure.MComCarLocation model);


        /// <summary>
        /// 获取上车地点实体
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>上车地点实体</returns>
        EyouSoft.Model.ComStructure.MComCarLocation GetModel(string CarLocationId);


        /// <summary>
        /// 删除上车地点
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        int DelCarLocation(string CarLocationId);


        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        int UpdateCarLocation(EyouSoft.Model.ComStructure.MComCarLocation model);

        /// <summary>
        /// 设置上车地点的启用禁用
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        int UpdateCarLocation(string CarLocationId, bool Status);


        /// <summary>
        /// 获取上车地点列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        ///<param name="status">启用or 禁用</param>
        ///<param name="location">上车地点</param>
        /// <returns>结果集</returns>
        IList<EyouSoft.Model.ComStructure.MComCarLocation> GetList(int pageIndex, int pageSize, ref int recordCount, string companyId, bool? status, string location);
    }
}
