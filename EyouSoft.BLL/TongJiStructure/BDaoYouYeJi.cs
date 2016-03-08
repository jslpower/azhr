using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TongJiStructure
{
    /// <summary>
    /// 统计分析-导游业绩统计
    /// </summary>
    public class BDaoYouYeJi : BLLBase
    {
        readonly EyouSoft.IDAL.TongJiStructure.IDaoYouYeJi dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TongJiStructure.IDaoYouYeJi>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BDaoYouYeJi() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取导游业绩统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiInfo> GetDaoYouYeJis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MDaoYouYeJiChaXunInfo chaXun, out EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo heJi)
        {
            heJi = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo();
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetDaoYouYeJis(companyId, pageSize, pageIndex, ref recordCount, chaXun, out heJi);
        }

        /// <summary>
        /// 获取导游业绩排名统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo> GetDaoYouYeJiPaiMings(string companyId, EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            var items = dal.GetDaoYouYeJiPaiMings(companyId, chaXun);

            if (items != null && items.Count > 0)
            {
                if (chaXun != null)
                {
                    if (chaXun.PaiXu == 1)
                        return items.OrderBy(p => p.RJJinE).ToList();
                    else if (chaXun.PaiXu == 3)
                        return items.OrderBy(p => p.JinE).ToList();
                    else if (chaXun.PaiXu == 2)
                        return items.OrderByDescending(p => p.JinE).ToList();
                    else
                        return items.OrderByDescending(p => p.RJJinE).ToList();
                }

                return items.OrderByDescending(p => p.RJJinE).ToList();
            }

            return items;
        }

        /// <summary>
        /// 获取导游带团人数排名统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanInfo> GetDaoYouDaiTuans(string companyId, EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetDaoYouDaiTuans(companyId, chaXun);

            if (items != null && items.Count > 0)
            {
                if (chaXun != null)
                {
                    if (chaXun.PaiXu == 1)
                        return items.OrderBy(p => p.RS).ToList();
                }

                return items.OrderByDescending(p => p.RS).ToList();
            }

            return items;
        }
        #endregion
    }
}
