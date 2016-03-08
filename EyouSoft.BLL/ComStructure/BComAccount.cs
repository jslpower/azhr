using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.ComStructure
{
    /// <summary>
    /// 公司银行帐号业务层
    /// </summary>
    public class BComAccount
    {
        private readonly EyouSoft.IDAL.ComStructure.IComAccount dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ComStructure.IComAccount>();

        /// <summary>
        /// default constructor
        /// </summary>
        public BComAccount() { }

        #region IComAccount 成员

        /// <summary>
        /// 获取公司的所有银行帐号信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号集合</returns>
        public IList<EyouSoft.Model.ComStructure.MComAccount> GetList(string companyId)
        {
            return dal.GetList(companyId);
        }

        /*/// <summary>
        /// 获取单个银行帐号
        /// </summary>
        /// <param name="id">银行账户编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>银行帐号实体</returns>
        public EyouSoft.Model.ComStructure.MComAccount GetModel(int id, string companyId)
        {
            return dal.GetModel(id, companyId);
        }*/

        #endregion
    }
}
