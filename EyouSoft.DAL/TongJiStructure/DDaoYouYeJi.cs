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
    /// 统计分析-导游业绩统计
    /// </summary>
    public class DDaoYouYeJi : DALBase, EyouSoft.IDAL.TongJiStructure.IDaoYouYeJi
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
        public DDaoYouYeJi()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IDaoYouYeJi 成员
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
            IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiInfo> items = new List<EyouSoft.Model.TongJiStructure.MDaoYouYeJiInfo>();
            heJi = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiHeJiInfo();

            string tableName = "view_TongJi_DaoYouYeJi";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = "SUM(RJCR) AS RJCRHJ,SUM(RJET) AS RJETHJ,SUM(RJLD) AS RJLDHJ,SUM(GWCR) AS GWCRHJ,SUM(GWET) AS GWETHJ,SUM(RJRS*JDS) AS QSRJRSHJ";
            StringBuilder sql = new StringBuilder();

            #region sql
            sql.AppendFormat(" CompanyId='{0}' ", companyId);
            sql.Append(" AND JDS>0 ");

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.DaoYouId))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_Plan AS A1 WHERE A1.TourId=view_TongJi_DaoYouYeJi.TourId AND A1.IsDelete='0' AND A1.Type=4 AND A1.Status=4 AND A1.SourceId='{0}') ", chaXun.DaoYouId);
                }
                else if(!string.IsNullOrEmpty(chaXun.DaoYouName))
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_Plan AS A1 WHERE A1.TourId=view_TongJi_DaoYouYeJi.TourId AND A1.IsDelete='0' AND A1.Type=4 AND A1.Status=4 AND A1.SourceName LIKE '%{0}%') ", chaXun.DaoYouName);
                }
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (chaXun.GysIds != null && chaXun.GysIds.Length > 0)
                {
                    foreach (var item in chaXun.GysIds)
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_Plan AS A1 WHERE A1.TourId=view_TongJi_DaoYouYeJi.TourId AND A1.IsDelete='0' AND A1.Type=7 AND A1.SourceId='{0}' ) ", item);
                    }
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
                    var item = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiInfo();
                    item.DaoYouName = string.Empty;
                    item.GWCR = rdr.GetInt32(rdr.GetOrdinal("GWCR"));
                    item.GWET = rdr.GetInt32(rdr.GetOrdinal("GWET"));
                    item.JDS = rdr.GetInt32(rdr.GetOrdinal("JDS"));
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("RJCR"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("RJET"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("RJLD"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.Gws = new List<EyouSoft.Model.TongJiStructure.MDaoYouYeJiGWInfo>();

                    string xml = rdr["DaoYouXml"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        var xroot = System.Xml.Linq.XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");

                        foreach (var xrow in xrows)
                        {
                            item.DaoYouName = item.DaoYouName + "," + Utils.GetXAttributeValue(xrow, "SourceName");
                        }

                        if (!string.IsNullOrEmpty(item.DaoYouName)) item.DaoYouName = item.DaoYouName.Trim(',');
                    }

                    xml = rdr["GouWuXml"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        var xroot = System.Xml.Linq.XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");

                        foreach (var xrow in xrows)
                        {
                            var item1 = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiGWInfo();
                            item1.GWCR = Utils.GetInt(Utils.GetXAttributeValue(xrow, "Adult"));
                            item1.GWET = Utils.GetInt(Utils.GetXAttributeValue(xrow, "Child"));
                            item1.GysName = Utils.GetXAttributeValue(xrow, "SourceName");
                            item1.YingYeE = Utils.GetDecimal(Utils.GetXAttributeValue(xrow, "YingYe"));

                            item.Gws.Add(item1);
                        }
                    }

                    items.Add(item);
                }

                rdr.NextResult();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RJCRHJ"))) heJi.RJCR = rdr.GetInt32(rdr.GetOrdinal("RJCRHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RJETHJ"))) heJi.RJET = rdr.GetInt32(rdr.GetOrdinal("RJETHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RJLDHJ"))) heJi.RJLD = rdr.GetInt32(rdr.GetOrdinal("RJLDHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GWCRHJ"))) heJi.GWCR = rdr.GetInt32(rdr.GetOrdinal("GWCRHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("GWETHJ"))) heJi.GWET = rdr.GetInt32(rdr.GetOrdinal("GWETHJ"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("QSRJRSHJ"))) heJi.QSRJRS = rdr.GetInt32(rdr.GetOrdinal("QSRJRSHJ"));
                }
            }

            return items;
        }

        /// <summary>
        /// 获取导游业绩排名统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo> GetDaoYouYeJiPaiMings(string companyId, EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo> items = new List<EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo>();

            StringBuilder sql = new StringBuilder();

            #region sql
            sql.Append(" SELECT B.OperatorId ");
            sql.Append(" ,SUM(B.Adult+B.Child) AS GWRS ");
            sql.Append(" ,SUM(B.YingYe) AS YingYeE ");
            sql.Append(" ,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=B.OperatorId) AS DaoYouName ");

            sql.Append(" FROM (SELECT * FROM view_TongJi_DaoYouYeJiPaiMing AS A ");
            sql.AppendFormat(" WHERE A.CompanyId='{0}' ", companyId);

            if (chaXun != null)
            {
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND A.LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (chaXun.GysId != null && chaXun.GysId.Length > 0)
                {
                    sql.AppendFormat(" AND A.SourceId IN({0}) ", Utils.GetSqlIn<string>(chaXun.GysId));
                }
                else if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND A.SourceName LIKE '%{0}%' ", chaXun.GysName);
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND A.LDate>'{0}' ", chaXun.STime.Value.AddDays(-1));
                }
            }
            sql.Append(" )B ");

            sql.AppendFormat(" GROUP BY B.OperatorId ");
            #endregion

            DbCommand cmd=_db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.TongJiStructure.MDaoYouYeJiPaiMingInfo();
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("YingYeE"));
                    item.RS = rdr.GetInt32(rdr.GetOrdinal("GWRS"));

                    items.Add(item);
                }
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
            IList<EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanInfo> items = new List<EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanInfo>();

            StringBuilder sql = new StringBuilder();

            #region sql
            sql.Append(" SELECT SUM(B.Adults+B.Childs+B.Leaders) AS RS ");
            sql.Append(" ,B.SourceId ");
            sql.Append(" ,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=B.SourceId) AS DaoYouName ");
            sql.Append(" FROM (SELECT * FROM view_TongJi_DaoYouDaiTuanRenShu AS A ");
            sql.AppendFormat(" WHERE A.CompanyId='{0}' ", companyId);
            if (chaXun != null)
            {
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND A.LDate<'{0}' ", chaXun.ETime.Value.AddDays(1));
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND A.LDate>'{0}' ", chaXun.STime.Value.AddDays(-1));
                }
            }
            sql.Append(" )B ");
            sql.Append(" GROUP BY B.SourceId ");
            #endregion

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.TongJiStructure.MDaoYouDaiTuanInfo();
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    item.RS = rdr.GetInt32(rdr.GetOrdinal("RS"));

                    items.Add(item);
                }
            }


            return items;
        }
        #endregion
    }
}
