using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TongJiStructure
{
    /// <summary>
    /// 统计分析-人数统计
    /// </summary>
    public class BRenShu
    {
        readonly EyouSoft.IDAL.TongJiStructure.IRenShu dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TongJiStructure.IRenShu>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BRenShu() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取人数统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MRenShuInfo> GetRenShus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MRenShuChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetRenShus(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        #endregion
    }
}
