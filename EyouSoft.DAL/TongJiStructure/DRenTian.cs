using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.TongJiStructure
{
    /// <summary>
    /// 统计分析-人天数统计
    /// </summary>
    public class DRenTian : DALBase, EyouSoft.IDAL.TongJiStructure.IRenTian
    {
        #region static constants
        //static constants
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DRenTian()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IRenTian 成员
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
            IList<EyouSoft.Model.TongJiStructure.MRenTianInfo> items = new List<EyouSoft.Model.TongJiStructure.MRenTianInfo>();

            string tableName = "view_TongJi_RenTian";
            string fields = "*";
            string orderByString = "LDate DESC,TianX ASC";
            string sumString = "";
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            #region sql
            if (chaXun != null)
            {
                if (chaXun.CityId.HasValue)
                {
                    sql.AppendFormat(" AND CityId={0} ",chaXun.CityId.Value);
                }
                if (chaXun.CountryId.HasValue)
                {
                    sql.AppendFormat(" AND CountryId={0} ", chaXun.CountryId.Value);
                }
                if (chaXun.DeptId.HasValue)
                {
                    sql.AppendFormat(" AND SellerDeptId={0} ", chaXun.DeptId.Value);
                }
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    sql.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND LDate>='{0}' ", chaXun.STime.Value);
                }
                if (chaXun.CountyId.HasValue)
                {
                    sql.AppendFormat(" AND CountyId={0} ", chaXun.CountyId.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.TongJiStructure.MRenTianInfo();
                    item.CHangBan = rdr["LeaveCityFlight"].ToString();
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    item.DeptName = rdr["DeptName"].ToString();
                    item.GuoJi = rdr["GuoJi"].ToString();                    
                    item.LingDuiName = rdr["LingDuiName"].ToString();
                    item.RHangBan = rdr["ArriveCityFlight"].ToString();
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("Leaders"));                    
                    item.TourCode = rdr["TourCode"].ToString();
                    int cityId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    int jiuDianCityId = rdr.GetInt32(rdr.GetOrdinal("JiuDianCityId"));
                    if (cityId == jiuDianCityId) item.ZhuSuWanShu = 1;
                    item.XCSTime = rdr.GetDateTime(rdr.GetOrdinal("XingChengRiQi"));
                    item.CityName = rdr["CityName"].ToString();
                    if (item.ZhuSuWanShu == 1) item.JiuDianName = rdr["HotelName1"].ToString();
                    item.CountyName = rdr["CountyName"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
