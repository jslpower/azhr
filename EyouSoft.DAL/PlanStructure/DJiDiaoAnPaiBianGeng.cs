//计调安排变更相关数据访问类 汪奇志 2013-04-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using EyouSoft.IDAL.FinStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Xml.Linq;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.PlanStructure
{
    /// <summary>
    /// 计调安排变更相关数据访问类
    /// </summary>
    public class DJiDiaoAnPaiBianGeng : DALBase, EyouSoft.IDAL.PlanStructure.IJiDiaoAnPaiBianGeng
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
        public DJiDiaoAnPaiBianGeng()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members

        #endregion

        #region IJiDiaoAnPaiBianGeng 成员
        /// <summary>
        /// 获取计调安排变更信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <param name="bianGengLeiXing">变更类型</param>
        /// <param name="jiaJianLeiXing">加减类型</param>
        /// <returns></returns>
        public EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengInfo GetInfo(string anPaiId, EyouSoft.Model.EnumType.PlanStructure.PlanChangeChangeClass bianGengLeiXing, string jiaJianLeiXing)
        {
            EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengInfo info = null;

            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT A.* ");
            sql.Append(" FROM [tbl_PlanCostChange] AS A ");
            sql.Append(" WHERE A.PlanId=@AnPaiId AND A.ChangeType=@BianBengLeiXing ");
            if (jiaJianLeiXing == "jia") sql.Append(" AND A.Type='1' ");
            else sql.Append(" AND A.Type='0' ");
            #endregion

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "AnPaiId", DbType.AnsiStringFixedLength, anPaiId);
            _db.AddInParameter(cmd, "BianBengLeiXing", DbType.Byte, bianGengLeiXing);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengInfo();

                    info.AnPaiId = anPaiId;
                    info.BeiZhu = rdr["Remark"].ToString();
                    info.BianGengLeiXing = bianGengLeiXing;
                    info.JiaJianLeiXing = jiaJianLeiXing;
                    info.JinE = rdr.GetDecimal(rdr.GetOrdinal("ChangeCost"));
                    info.RenShu = rdr.GetInt32(rdr.GetOrdinal("PeopleNumber"));
                    info.DRenShu = rdr.GetDecimal(rdr.GetOrdinal("DNum"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取计调安排变更相关信息业务实体
        /// </summary>
        /// <param name="anPaiId">安排编号</param>
        /// <returns></returns>
        public EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengXgInfo GetXgInfo(string anPaiId)
        {
            EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengXgInfo info = null;

            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            //sql.Append(" B.CostDetail AS FeiYongMingXi ");
            sql.Append(" '' AS FeiYongMingXi ");
            sql.Append(" ,C.SellerId AS TourXiaoShouYuanId ");
            sql.Append(" ,C.TourStatus ");
            sql.Append(" ,(SELECT PlanerId FROM tbl_TourPlaner AS A1 WHERE A1.TourId=C.TourId FOR XML RAW,ROOT('root')) AS TourJiDiaoXml ");
            sql.Append(" ,(SELECT GuideUserId FROM tbl_Plan AS A1 WHERE A1.TourId=C.TourId AND A1.Type=12 AND A1.IsDelete='0' FOR XML RAW,ROOT('root')) AS TourDaoYouXml  ");
            sql.Append(" ,C.TourId ");
            sql.Append(" ,B.PaymentType AS ZhiFuFangShi ");
            sql.Append(" ,B.SourceName AS GysName ");
            sql.Append(" ,B.Type AS AnPaiLeiXing ");
            sql.Append(" ,B.ContactName AS GysLxrName ");
            sql.Append(" ,B.ContactPhone AS GysLxrTelephone ");
            sql.Append(" FROM [tbl_Plan] AS B INNER JOIN [tbl_Tour] AS C ON B.[TourId]=C.[TourId] ");
            sql.Append(" WHERE B.PlanId=@AnPaiId ");
            #endregion

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "AnPaiId", DbType.AnsiStringFixedLength, anPaiId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.PlanStructure.MJiDiaoAnPaiBianGengXgInfo();

                    info.AnPaiId = anPaiId;
                    info.FeiYongMingXi = rdr["FeiYongMingXi"].ToString();
                    info.TourDaoYous = new List<string>();
                    info.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    info.TourJiDiaos = new List<string>();
                    info.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    info.TourXiaShouYuanId = rdr["TourXiaoShouYuanId"].ToString();
                    info.ZhiFuFangShi = (EyouSoft.Model.EnumType.PlanStructure.Payment)rdr.GetByte(rdr.GetOrdinal("ZhiFuFangShi"));
                    info.GysName = rdr["GysName"].ToString();
                    info.AnPaiLeiXing = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)rdr.GetByte(rdr.GetOrdinal("AnPaiLeiXing"));
                    info.GysLxrName = rdr["GysLxrName"].ToString();
                    info.GysLxrTelephone = rdr["GysLxrTelephone"].ToString();

                    string xml1 = rdr["TourJiDiaoXml"].ToString();
                    string xml2 = rdr["TourDaoYouXml"].ToString();

                    if (!string.IsNullOrEmpty(xml1))
                    {
                        var xroot = XElement.Parse(xml1);
                        var xrows = Utils.GetXElements(xroot, "row");
                        foreach (var xrow in xrows)
                        {
                            info.TourJiDiaos.Add(Utils.GetXAttributeValue(xrow, "PlanerId"));
                        }
                    }

                    if (!string.IsNullOrEmpty(xml2))
                    {
                        var xroot = XElement.Parse(xml2);
                        var xrows = Utils.GetXElements(xroot, "row");
                        foreach (var xrow in xrows)
                        {
                            info.TourDaoYous.Add(Utils.GetXAttributeValue(xrow, "GuideUserId"));
                        }
                    }

                }
            }

            return info;
        }

        #endregion
    }
}
