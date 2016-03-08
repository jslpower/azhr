using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 服务项目业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComProject
    {
        private readonly EyouSoft.IDAL.ComStructure.IComProject dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComProject>();

        public BComProject() { }

        #region
        /// <summary>
        /// 添加服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Add(MComProject item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加服务模版,类型为:{0}", item.Type));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改服务标准模版
        /// </summary>
        /// <param name="item">服务标准模版实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(MComProject item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改服务模版,编号为:{0}", item.Id));
                }
            }
            return result;
        }
        /// <summary>
        /// 删除服务标准模版
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Delete(int id,string companyId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(companyId))
            {
                result = dal.Delete(id, companyId);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除服务模版,编号为:{0}", id));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取服务标准模版实体
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版实体</returns>
        public MComProject GetModel(int id, string companyId)
        {
            MComProject item = null;
            if (id > 0 && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(id, companyId);
            }
            return item;
        }
        /// <summary>
        /// 获取指定模版中某项的信息
        /// </summary>
        /// <param name="projectType">模版类型</param>
        /// <param name="cpType">包含项目类型</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>服务标准模版集合</returns>
        public IList<MComProject> GetList(ProjectType projectType, ContainProjectType? cpType, string companyId)
        {
            IList<MComProject> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(projectType, cpType, companyId);
            }
            return list;
        }
        #endregion
    }
}
