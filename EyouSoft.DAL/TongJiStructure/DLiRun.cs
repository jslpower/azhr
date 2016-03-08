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
    /// 统计分析-利润统计
    /// </summary>
    public class DLiRun : DALBase, EyouSoft.IDAL.TongJiStructure.ILiRun
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
        public DLiRun()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region ILiRun 成员
        /// <summary>
        /// 获取利润统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="heJi">合计</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MLiRunInfo> GetLiRuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TongJiStructure.MLiRunChaXunInfo chaXun, out EyouSoft.Model.TongJiStructure.MLiRunHeJiInfo heJi)
        {
            heJi = new EyouSoft.Model.TongJiStructure.MLiRunHeJiInfo();
            IList<EyouSoft.Model.TongJiStructure.MLiRunInfo> items=new List<EyouSoft.Model.TongJiStructure.MLiRunInfo>();
            string tableName = "view_TongJi_LiRun";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = "SUM(Adults) AS CRHJ,SUM(Childs) AS ETHJ,SUM(Leaders) AS LDHJ,SUM(YingShouJinE) AS YSHJ,SUM(YingFuJinE) YFHJ,SUM(GWCR) AS GWCRHJ,SUM(GWET) AS GWETHJ,SUM(GWFanLi) AS GWFLHJ";
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            #region sql
            if (chaXun != null)
            {
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (!string.IsNullOrEmpty(chaXun.KeHuId))
                {
                    sql.AppendFormat(" AND BuyCompanyID='{0}' ", chaXun.KeHuId);
                }
                else if (!string.IsNullOrEmpty(chaXun.KeHuName))
                {
                    sql.AppendFormat(" AND BuyCompanyName LIKE '%{0}%' ", chaXun.KeHuName);
                }
                if (chaXun.MaoLi1.HasValue && chaXun.MaoLi2.HasValue)
                {
                    string _operator = string.Empty;

                    switch (chaXun.MaoLi1.Value)
                    {
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.大于等于: _operator = ">="; break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.等于: _operator = "="; break;
                        case EyouSoft.Model.EnumType.FinStructure.EqualSign.小于等于: _operator = "<="; break;
                    }

                    sql.AppendFormat(" AND YingShouJinE-YingFuJinE {0} {1} ", _operator, chaXun.MaoLi2.Value);
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.STime.Value.AddDays(-1));
                }
                if (chaXun.XiaoShouYuanDeptId.HasValue)
                {
                    sql.AppendFormat(" AND SellerDeptId ={0} ", chaXun.XiaoShouYuanDeptId.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.XiaoShouYuanId))
                {
                    sql.AppendFormat(" AND SellerId='{0}' ", chaXun.XiaoShouYuanId);
                }
                else if (!string.IsNullOrEmpty(chaXun.XiaoShouYuanName))
                {
                    sql.AppendFormat(" AND SellerName LIKE '%{0}%' ", chaXun.XiaoShouYuanName);
                }
                if (chaXun.AreaId.HasValue)
                {
                    sql.AppendFormat(" AND AreaId={0} ", chaXun.AreaId.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.TongJiStructure.MLiRunInfo();
                    item.AreaName = rdr["AreaName"].ToString();
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    item.GWCR = rdr.GetInt32(rdr.GetOrdinal("GWCR"));
                    item.GWET = rdr.GetInt32(rdr.GetOrdinal("GWET"));
                    item.GWFanLi = rdr.GetDecimal(rdr.GetOrdinal("GWFanLi"));
                    item.KeHuName = rdr["BuyCompanyName"].ToString();
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("Leaders"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.TourCode = rdr["TourCode"].ToString();
                    item.XiaoShouYuanName = rdr["SellerName"].ToString();
                    item.YingFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YingFuJinE"));
                    item.YingShouJinE = rdr.GetDecimal(rdr.GetOrdinal("YingShouJinE"));
                    item.IsTax = rdr["IsTax"].ToString()=="1";

                    items.Add(item);
                }

                rdr.NextResult();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GWCRHJ"))) heJi.GWCR = rdr.GetInt32(rdr.GetOrdinal("GWCRHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GWETHJ"))) heJi.GWET = rdr.GetInt32(rdr.GetOrdinal("GWETHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GWFLHJ"))) heJi.GWFanLi = rdr.GetDecimal(rdr.GetOrdinal("GWFLHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CRHJ"))) heJi.RJCR = rdr.GetInt32(rdr.GetOrdinal("CRHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ETHJ"))) heJi.RJET = rdr.GetInt32(rdr.GetOrdinal("ETHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LDHJ"))) heJi.RJLD = rdr.GetInt32(rdr.GetOrdinal("LDHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YFHJ"))) heJi.YingFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YFHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YSHJ"))) heJi.YingShouJinE = rdr.GetDecimal(rdr.GetOrdinal("YSHJ"));                    
                }
            }

            return items;
        }
        #endregion
    }
}
