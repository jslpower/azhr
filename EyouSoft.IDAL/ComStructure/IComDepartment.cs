using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public interface IComDepartment
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        bool Add(MComDepartment item);
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        bool Update(MComDepartment item);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1:部门已添加用户 2:该部门已添加下级部门 3:删除成功 4:删除失败		</returns>
        int Delete(int departId, string companyId);
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门实体</returns>
        MComDepartment GetModel(int departId,string companyId);
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门集合</returns>
        IList<MComDepartment> GetList(string companyId);
        /// <summary>
        /// 获取部门下的所有子部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门集合</returns>
        IList<MComDepartment> GetList(string departId, string companyId);

        /// <summary>
        /// 根据上级部门编号判断是否存在子部门
        /// </summary>
        /// <param name="prevDepartId">上级部门编号</param>
        /// <returns>True：存在 False：不存在</returns>
        bool IsExistSubDept(int prevDepartId);
    }
}
