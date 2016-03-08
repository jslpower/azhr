using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.IDAL.ComStructure
{
    /// <summary>
    /// 公司服务标准模版
    /// </summary>
    public interface IComProject
    {
        /// <summary>
        /// 添加服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        bool Add(MComProject item);
        /// <summary>
        /// 修改服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        bool Update(MComProject item);
        /// <summary>
        /// 删除服务标准模版
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true:成功 false：失败</returns>
        bool Delete(int id, string companyId);
        /// <summary>
        /// 获取服务标准模版实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版实体</returns>
        MComProject GetModel(int id,string companyId);
        /// <summary>
        /// 获取指定模版中某项的信息
        /// </summary>
        /// <param name="projectType">模版类型</param>
        /// <param name="cpType">包含项目类型</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版集合</returns>
        IList<MComProject> GetList(ProjectType projectType, ContainProjectType? cpType, string companyId);
    }
}
