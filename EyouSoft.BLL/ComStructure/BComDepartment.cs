using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 部门业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComDepartment
    {
        private readonly EyouSoft.IDAL.ComStructure.IComDepartment dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComDepartment>();
        /// <summary>
        /// 部门业务类
        /// </summary>
        public BComDepartment() { }

        #region
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Add(MComDepartment item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComDept, item.CompanyId);
                    if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey) != null)
                    {
                        EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
                    }
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加部门,名称为:{0}", item.DepartName));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(MComDepartment item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComDept, item.CompanyId);
                    if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey) != null)
                    {
                        EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
                    }
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改部门,编号为:{0}", item.DepartId));
                }
            }
            return result;

        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1:部门已添加用户 2:该部门已添加下级部门 3:删除成功 4:删除失败	</returns>
        public int Delete(int departId, string companyId)
        {
            int result = 4;
            if (!string.IsNullOrEmpty(companyId))
            {
                result = dal.Delete(departId, companyId);
                if (result == 3)
                {
                    string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.ComDept, companyId);
                    if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey) != null)
                    {
                        EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
                    }
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除部门,编号为:{0}", departId));
                }
            }
            return result;
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门实体</returns>
        public MComDepartment GetModel(int departId, string companyId)
        {
            MComDepartment item = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(departId, companyId);
            }
            return item;
        }
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        ///  <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MComDepartment> GetList(string companyId)
        {
            IList<MComDepartment> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId);
            }
            return list;
        }

        /// <summary>
        /// 获取部门下的所有子部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门集合</returns>
        public IList<MComDepartment> GetList(string departId, string companyId)
        {
            IList<MComDepartment> list = null;
            if (!string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(departId))
            {
                list = dal.GetList(departId, companyId);
            }
            return list;
        }
        /// <summary>
        /// 根据上级部门编号判断是否存在子部门
        /// </summary>
        /// <param name="prevDepartId">上级部门编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExistSubDept(int prevDepartId)
        {
            return prevDepartId > 0 && this.dal.IsExistSubDept(prevDepartId);
        }

        #endregion
    }
}
