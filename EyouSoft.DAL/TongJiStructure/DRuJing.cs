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
    /// 统计分析-入境目录表
    /// </summary>
    public class DRuJing : DALBase, EyouSoft.IDAL.TongJiStructure.IRuJing
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
        public DRuJing()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IRuJing 成员
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
            IList<EyouSoft.Model.TongJiStructure.MRuJingInfo> items = new List<EyouSoft.Model.TongJiStructure.MRuJingInfo>();

            string tableName = "view_TongJi_RuJing";
            string fields = "*";
            string orderByString = "LDate";
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
                    var item = new EyouSoft.Model.TongJiStructure.MRuJingInfo();

                    item.GuoJi = rdr["CountryName"].ToString();
                    item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    item.QuanPeiName = rdr["QuanPeiName"].ToString();
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("Leaders"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.TianShu = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    item.YeMaHao = rdr["YeMaHao"].ToString();
                    item.YouLanChengShi = rdr["YouLanChengShi"].ToString();
                    item.ZhuangDingXuHao = rdr["ZhuangDingXuHao"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}
