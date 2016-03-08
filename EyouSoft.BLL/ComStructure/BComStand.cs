using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Cache.Tag;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 报价标准业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BComStand
    {
        private readonly EyouSoft.IDAL.ComStructure.IComStand dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComStand>();

        public BComStand() { }

        #region
        /// <summary>
        /// 添加报价标准
        /// </summary>
        /// <param name="item">报价标准实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComStand item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("添加报价标准,名称为:{0}", item.Name));
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.BaoJiaBiaoZhun, item.CompanyId));
                }
            }
            return result;
        }
        /// <summary>
        /// 修改报价标准
        /// </summary>
        /// <param name="item">报价标准实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComStand item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("修改报价标准,编号为:{0}", item.Id));
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.BaoJiaBiaoZhun, item.CompanyId));
                }
            }
            return result;
        }
        /// <summary>
        /// 删除报价标准
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="id">报价标准编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(string companyId,params int[] id)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(companyId))
            {
                StringBuilder ids = new StringBuilder();
                for (int i = 0; i < id.Length; i++)
                {
                    ids.Append(id[i]);
                    if (i + 1 < id.Length)
                    {
                        ids.Append(",");
                    }
                }
                result = dal.Delete(ids.ToString(), companyId);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(string.Format("删除报价标准,编号为:{0}", ids.ToString()));
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(TagName.BaoJiaBiaoZhun, companyId));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有报价标准
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>报价标准集合</returns>
        public IList<MComStand> GetList(string companyId)
        {
            string cacheName = string.Format(TagName.BaoJiaBiaoZhun, companyId);
            IList<MComStand> list = (IList<MComStand>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);

            if (list == null || list.Count == 0)
            {
                list = dal.GetList(companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, list);
            }

            return list;
        }

        /// <summary>
        /// 获取报价标准信息业务实体
        /// </summary>
        /// <param name="biaoZhunId">报价标准编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public MComStand GetInfo(int biaoZhunId,string companyId)
        {
            var items = GetList(companyId);
            if(items==null||items.Count==0) return null;

            MComStand info = null;
            foreach (var item in items)
            {
                if (item.Id == biaoZhunId) { info = item; break; }
            }

            return info;
        }

        /// <summary>
        /// 获取报价标准名称
        /// </summary>
        /// <param name="biaoZhunId">报价标准编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public string GetName(int biaoZhunId, string companyId)
        {
            var info = GetInfo(biaoZhunId, companyId);
            if (info != null) return info.Name;

            return string.Empty;
        }
        #endregion
    }
}
