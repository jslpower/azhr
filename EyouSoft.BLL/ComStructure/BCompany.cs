using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 系统公司信息业务层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class BCompany
    {
        private readonly EyouSoft.IDAL.ComStructure.ICompany dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.ICompany>();

        /// <summary>
        /// default constructor
        /// </summary>
        public BCompany() { }

        #region public members
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="item">公司信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MCompany info)
        {
            bool result = false;

            if (info != null && !string.IsNullOrEmpty(info.Id))
            {
                result = dal.Update(info);
                if (result)
                {
                    EyouSoft.BLL.SysStructure.BSysLogHandle.Insert("修改系统信息");
                }

                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(EyouSoft.Cache.Tag.TagName.ComSetting, info.Id));
            }

            return result;
        }

        /// <summary>
        /// 根据公司编号获取公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sysId">系统编号</param>
        /// <returns>公司信息实体</returns>
        public MCompany GetModel(string companyId, string sysId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(sysId)) return null;

            MCompany info = dal.GetModel(companyId, sysId);

            if (info != null) info.ComAccount = new EyouSoft.BLL.ComStructure.BComAccount().GetList(companyId);

            return info;
        }

        //  /// <summary>
        ///// 获取公司所有账号
        ///// </summary>
        ///// <param name="companyId">公司编号</param>
        ///// <returns></returns>
        //public IList<MComAccount> GetList(string companyId)
        //{
        //    return dal.GetList(companyId);
        //}
        #endregion
    }
}
