using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TongJiStructure
{
    /// <summary>
    /// 统计分析-入境目录表
    /// </summary>
    public class BRuJing : BLLBase
    {
        readonly EyouSoft.IDAL.TongJiStructure.IRuJing dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TongJiStructure.IRuJing>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BRuJing() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取统计分析-入境目录表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MRuJingInfo> GetRuJings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MRuJingChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetRuJings(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        #endregion
    }
}
