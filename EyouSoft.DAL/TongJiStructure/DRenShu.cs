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
    /// 统计分析-人数统计
    /// </summary>
    public class DRenShu : DALBase, EyouSoft.IDAL.TongJiStructure.IRenShu
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
        public DRenShu()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IRenShu 成员
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
            IList<EyouSoft.Model.TongJiStructure.MRenShuInfo> items = new List<EyouSoft.Model.TongJiStructure.MRenShuInfo>();
            string tableName = "view_TongJi_RenShu";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = "";
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            #region sql
            if (chaXun != null)
            {
                if (chaXun.DeptId.HasValue)
                {
                    sql.AppendFormat(" AND SellerDeptId={0} ", chaXun.DeptId.Value);
                }
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.STime.Value.AddDays(-1));
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.TongJiStructure.MRenShuInfo();

                    item.DeptName = rdr["DeptName"].ToString();
                    item.GuoJi = rdr["CountryName"].ToString();
                    item.HZTS = rdr.GetInt32(rdr.GetOrdinal("HZTS"));
                    item.KeHuName = rdr["BuyCompanyName"].ToString();
                    item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    item.RDate = rdr.GetDateTime(rdr.GetOrdinal("RDate"));
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("Leaders"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.TS = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    item.ZJTS = rdr.GetInt32(rdr.GetOrdinal("ZJTS"));

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
