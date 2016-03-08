//金蝶相关业务逻辑类 汪奇志 2012-05-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.FinStructure
{
    /// <summary>
    /// 金蝶相关业务逻辑类
    /// </summary>
    public class BKis
    {
        readonly EyouSoft.IDAL.FinStructure.IKis dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.FinStructure.IKis>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BKis() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取KIS科目集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MKisAccountGroupInfo> GetAccountGroups(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetAccountGroups(companyId);
        }
        #endregion
    }
}
