using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 公司角色
    /// </summary>
    public interface IComRole
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        int Add(MComRole item);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        int Update(MComRole item);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色编号</param>
        /// <returns>true：成功 false：失败</returns>
        bool Delete(string ids);
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>角色实体</returns>
        MComRole GetModel(int id,string companyId);
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色集合</returns>
        IList<MComRole> GetList(string companyId);
    }
}
