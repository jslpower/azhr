using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SmsStructure
{
    /// <summary>
    /// 短信中心导入客户管理号码业务逻辑类
    /// </summary>
    public class BDaoRuLxr
    {
        readonly EyouSoft.IDAL.SmsStructure.IDaoRuLxr dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SmsStructure.IDaoRuLxr>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BDaoRuLxr() { }
        #endregion

        #region public members
        /// <summary>
        /// 获取导入客户管理号码集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo> GetLxrs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo searchInfo)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            var items = dal.GetLxrs(companyId, pageSize, pageIndex, ref recordCount, searchInfo);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.SysStructure.BGeography();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CountryId, item.ProvinceId, item.CityId, item.DistrictId);
                }
            }
            return items;
        }
        #endregion
    }
}
