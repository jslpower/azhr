using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 客户等级
    /// </summary>
    public interface IComLev
    {
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComLev item);
        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComLev item);
        /// <summary>
        /// 删除客户等级
        /// </summary>
        /// <param name="ids">客户等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(int[] ids, string companyId);
        /// <summary>
        /// 获取所有客户等级
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>客户等级集合</returns>
        IList<MComLev> GetList(string companyId);
    }
}
