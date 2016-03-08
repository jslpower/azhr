using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 报价等级
    /// </summary>
    public interface IComStand
    {
        /// <summary>
        /// 添加报价等级
        /// </summary>
        /// <param name="item">报价等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Add(MComStand item);
        /// <summary>
        /// 修改报价等级
        /// </summary>
        /// <param name="item">报价等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        bool Update(MComStand item);
        /// <summary>
        /// 删除报价等级
        /// </summary>
        /// <param name="ids">报价等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(string ids,string companyId);
        /// <summary>
        /// 获取所有报价等级
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>报价等级集合</returns>
        IList<MComStand> GetList(string companyId);
    }
}
