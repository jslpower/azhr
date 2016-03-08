using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 车型管理
    /// </summary>
    public interface IComCarType
    {
        /// <summary>
        /// 查询该车型是否有计划预设
        /// </summary>
        /// <param name="CarLocationId"></param>
        /// <returns></returns>
        bool IsExistsTourCarType(string CarLocationId);


        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool AddComCarType(MComCarType model);


        /// <summary>
        /// 修改车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        int UpdateComCarType(MComCarType model);


        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="CarTypeId">车型编号</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        int DelComCarType(string CarTypeId);

        /// <summary>
        /// 获取系统车型
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ComStructure.MComCarType> GetList(string companyId);

        /// <summary>
        /// 分页获取车型
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车型集合</returns>
        IList<EyouSoft.Model.ComStructure.MComCarType> GetList(int pageIndex, int pageSize, ref int recordCount, string companyId);
        /// <summary>
        /// 获取车型实体
        /// </summary>
        /// <param name="CarLocationId">车型编号</param>
        /// <returns>车型实体</returns>
        EyouSoft.Model.ComStructure.MComCarType GetModel(string CarTypeId);
    }

}
