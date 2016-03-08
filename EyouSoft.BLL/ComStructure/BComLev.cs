using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Cache.Tag;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 客户等级业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComLev
    {
        private readonly EyouSoft.IDAL.ComStructure.IComLev dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComLev>();

        public BComLev() { }

        #region
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComLev item)
        {
            if (item == null) return false;

            bool result = dal.Add(item);
            if (result)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加客户等级,名称为:{0}", item.Name));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.KeHuDengJi, item.CompanyId));
            }

            return result;
        }

        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="item">客户等级实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComLev item)
        {
            if (item == null || item.Id < 1) return false;

            bool result = dal.Update(item);
            if (result)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改客户等级,编号为:{0}", item.Id));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.KeHuDengJi, item.CompanyId));
            }

            return result;
        }

        /// <summary>
        /// 删除客户等级
        /// </summary>
        /// <param name="id">客户等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string companyId, params int[] id)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(companyId))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < id.Length; i++)
                {
                    sb.Append(id[i]);
                    if (i + 1 < id.Length)
                    {
                        sb.Append(",");
                    }
                }
                result = dal.Delete(id, companyId);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除客户等级,编号为:{0}", sb.ToString()));
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.KeHuDengJi, companyId));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有客户等级
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>客户等级集合</returns>
        public IList<MComLev> GetList(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            string cacheName = string.Format(TagName.KeHuDengJi, companyId);

            IList<MComLev> list = (IList<MComLev>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, list);
            }

            return list;
        }

        /// <summary>
        /// 获取客户等级信息业务实体
        /// </summary>
        /// <param name="dengJiId">客户等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public MComLev GetInfo(int dengJiId, string companyId)
        {
            var items = GetList(companyId);
            if (items == null || items.Count == 0) return null;

            MComLev info = null;
            foreach (var item in items)
            {
                if (item.Id == dengJiId) { info = item; break; }
            }

            return info;
        }

        /// <summary>
        /// 获取客户等级名称
        /// </summary>
        /// <param name="dengJiId">客户等级编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public string GetName(int dengJiId, string companyId)
        {
            var info = GetInfo(dengJiId, companyId);
            if (info != null) return info.Name;

            return string.Empty;
        }
        #endregion
    }
}
