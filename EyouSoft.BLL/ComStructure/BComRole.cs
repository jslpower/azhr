using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 角色业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComRole
    {
        private readonly EyouSoft.IDAL.ComStructure.IComRole dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComRole>();

        public BComRole() { }

        #region
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        public int Add(MComRole item)
        {
            int result = 0;
            if (item != null)
            {
                result = dal.Add(item);
                if (result==1)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加角色,角色名称为:{0}", item.RoleName));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="item">角色实体</param>
        /// <returns>0:操作失败 1:操作成功 2:角色名重复</returns>
        public int Update(MComRole item)
        {
            int result = 0;
            if (item != null)
            {
                result = dal.Update(item);
                if (result == 1)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改角色,编号为:{0}", item.Id));
                }
            }
            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(params int[] id)
        {
            bool result = false;
            StringBuilder ids = new StringBuilder();
            for (int i = 0; i < id.Length; i++)
            {
                ids.Append(id[i]);
                if (i + 1 < id.Length)
                {
                    ids.Append(",");
                }
            }
            result = dal.Delete(ids.ToString());
            if (result)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除角色,编号为:{0}", ids.ToString()));
            }
            return result;
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>角色实体</returns>
        public MComRole GetModel(int id, string companyId)
        {
            MComRole item = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(id, companyId);
            }
            return item;
        }
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色集合</returns>
        public IList<MComRole> GetList(string companyId)
        {
            IList<MComRole> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId);
            }
            return list;
        }
        #endregion
    }
}
