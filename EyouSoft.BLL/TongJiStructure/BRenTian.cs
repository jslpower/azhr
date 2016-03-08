using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TongJiStructure
{
    /// <summary>
    /// 统计分析-人天数统计
    /// </summary>
    public class BRenTian : BLLBase
    {
        readonly EyouSoft.IDAL.TongJiStructure.IRenTian dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TongJiStructure.IRenTian>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BRenTian() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取人天数统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MRenTianInfo> GetRenTians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MRenTianChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetRenTians(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        #endregion
    }
}
