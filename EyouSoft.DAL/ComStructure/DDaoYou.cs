using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit;
using System.Xml.Linq;

namespace EyouSoft.DAL.ComStructure
{
    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    /// <summary>
    /// 导游档案数据访问类
    /// </summary>
    public class DDaoYou : DALBase, EyouSoft.IDAL.ComStructure.IDaoYou
    {
        #region static constants
        //static constants
        const string DEFAULT_XML_DOC = "<root></root>";
        const string SQL_SELECT_GetInfo = "SELECT * FROM view_DaoYou WHERE UserId=@DaoYouId";
        const string SQL_SELECT_GetDaoYouLeiBies = "SELECT * FROM tbl_SourceGuideCategory WHERE UserId=@DaoYouId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DDaoYou()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create daoyou leibie xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateDaoYouLeiBieXml(IList<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.AppendFormat("<info LeiBie=\"{0}\" />", (int)item);
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// get daoyou leibies
        /// </summary>
        /// <param name="daoYouId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie> GetDaoYouLeiBies(string daoYouId)
        {
            IList<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie> items = new List<EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetDaoYouLeiBies);
            _db.AddInParameter(cmd, "DaoYouId", DbType.AnsiStringFixedLength, daoYouId);


            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    items.Add((EyouSoft.Model.EnumType.ComStructure.DaoYouLeiBie)rdr.GetByte(rdr.GetOrdinal("Category")));
                }
            }

            return items;
        }

        /// <summary>
        /// 生成导游安排类型集合List
        /// </summary>
        /// <param name="TypeXML">要分析的XML字符串</param>
        /// <returns></returns>
        private IList<MGuidePlanWorkType> GetTypeList(string TypeXML)
        {
            if (string.IsNullOrEmpty(TypeXML)) return null;
            IList<MGuidePlanWorkType> ResultList = null;
            ResultList = new List<MGuidePlanWorkType>();
            XElement root = XElement.Parse(TypeXML);
            var xRow = root.Elements("row");
            foreach (var tmp1 in xRow)
            {
                if (tmp1.Attribute("StartDate") != null && tmp1.Attribute("EndDate") != null && tmp1.Attribute("StartDate").Value.Trim() != "" && tmp1.Attribute("EndDate").Value.Trim() != "")
                {
                    var model = new MGuidePlanWorkType();
                    model.OnTime = Convert.ToDateTime(tmp1.Attribute("StartDate").Value);
                    model.NextTime = Convert.ToDateTime(tmp1.Attribute("EndDate").Value);
                    model.Type = tmp1.Attribute("Type").Value;
                    ResultList.Add(model);
                    model = null;
                }
            }
            return ResultList;
        }
        #endregion

        #region IDaoYou 成员
        /// <summary>
        /// 导游档案新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int CU(EyouSoft.Model.ComStructure.MDaoYouInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_DaoYou_CU");
            _db.AddInParameter(cmd, "DaoYouId", DbType.AnsiStringFixedLength, info.DaoYouId);
            _db.AddInParameter(cmd, "Username", DbType.String, info.Username);
            if (info.Pwd != null)
            {
                _db.AddInParameter(cmd, "Pwd", DbType.String, info.Pwd.NoEncryptPassword);
                _db.AddInParameter(cmd, "MD5Pwd", DbType.String, info.Pwd.MD5Password);
            }
            else
            {
                _db.AddInParameter(cmd, "Pwd", DbType.String, DBNull.Value);
                _db.AddInParameter(cmd, "MD5Pwd", DbType.String, DBNull.Value);
            }
            _db.AddInParameter(cmd, "XingMing", DbType.String, info.XingMing);
            _db.AddInParameter(cmd, "Gender", DbType.Byte, info.Gender);
            _db.AddInParameter(cmd, "JiBie", DbType.Byte, info.JiBie);
            _db.AddInParameter(cmd, "LeiBieXml", DbType.String, CreateDaoYouLeiBieXml(info.LeiBies));
            _db.AddInParameter(cmd, "YuZhong", DbType.String, info.YuZhong);
            _db.AddInParameter(cmd, "ShenFenZhengHao", DbType.String, info.ShenFenZhengHao);
            _db.AddInParameter(cmd, "DaoYouZhengHao", DbType.String, info.DaoYouZhengHao);
            _db.AddInParameter(cmd, "LingDuiZhengHao", DbType.String, info.LingDuiZhengHao);
            _db.AddInParameter(cmd, "GuaKaoDanWei", DbType.String, info.GuaKaoDanWei);
            _db.AddInParameter(cmd, "IsNianShen", DbType.AnsiStringFixedLength, info.IsNianShen ? "1" : "0");
            _db.AddInParameter(cmd, "Telephone", DbType.String, info.Telephone);
            _db.AddInParameter(cmd, "ShouJiHao", DbType.String, info.ShouJiHao);
            _db.AddInParameter(cmd, "QQ", DbType.String, info.QQ);
            _db.AddInParameter(cmd, "Email", DbType.String, info.Email);
            _db.AddInParameter(cmd, "MSN", DbType.String, info.MSN);
            _db.AddInParameter(cmd, "JiaTingTelephone", DbType.String, info.JiaTingTelephone);
            _db.AddInParameter(cmd, "JiaTingDiZhi", DbType.String, info.JiaTingDiZhi);
            _db.AddInParameter(cmd, "ZhaoPianFilePath", DbType.String, info.ZhaoPianFilePath);
            _db.AddInParameter(cmd, "XingGeTeDian", DbType.String, info.XingGeTeDian);
            _db.AddInParameter(cmd, "ShanChangXianLu", DbType.String, info.ShanChangXianLu);
            _db.AddInParameter(cmd, "KeHuPingJia", DbType.String, info.KeHuPingJia);
            _db.AddInParameter(cmd, "TeChang", DbType.String, info.TeChang);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "ZhiWeiLeiXing", DbType.Byte, info.ZhiWeiLeiXing);
            _db.AddOutParameter(cmd, "RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取导游档案实体
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MDaoYouInfo GetInfo(string daoYouId)
        {
            EyouSoft.Model.ComStructure.MDaoYouInfo info = new EyouSoft.Model.ComStructure.MDaoYouInfo();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "DaoYouId", DbType.AnsiStringFixedLength, daoYouId);


            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.ComStructure.MDaoYouInfo();

                    info.BeiZhu = rdr["Remark"].ToString();
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.DaoYouId = daoYouId;
                    info.DaoYouZhengHao = rdr["DaoYouZhengHao"].ToString();
                    info.Email = rdr["ContactEmail"].ToString();
                    info.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("ContactSex"));
                    info.GuaKaoDanWei = rdr["GuaKaoDanWei"].ToString();
                    info.IsNianShen = rdr["IsNianShen"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.JiaTingDiZhi = rdr["JiaTingDiZhi"].ToString();
                    info.JiaTingTelephone = rdr["JiaTingTelephone"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JiBie"))) info.JiBie = (EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie)rdr.GetByte(rdr.GetOrdinal("JiBie"));
                    info.KeHuPingJia = rdr["KeHuPingJia"].ToString();
                    info.LeiBies = null;
                    info.LingDuiZhengHao = rdr["LingDuiZhengHao"].ToString();
                    info.MSN = rdr["MSN"].ToString();
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.Pwd = null;
                    info.QQ = rdr["QQ"].ToString();
                    info.ShanChangXianLu = rdr["ShanChangXianLu"].ToString();
                    info.ShenFenZhengHao = rdr["IDcardId"].ToString();
                    info.ShouJiHao = rdr["ContactMobile"].ToString();
                    info.TeChang = rdr["TeChang"].ToString();
                    info.Telephone = rdr["ContactTel"].ToString();
                    info.Username = rdr["UserName"].ToString();
                    info.XingGeTeDian = rdr["XingGeTeDian"].ToString();
                    info.XingMing = rdr["ContactName"].ToString();
                    info.YuZhong = rdr["YuZhong"].ToString();
                    info.ZhaoPianFilePath = rdr["UserImg"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhiWeiLeiXing"))) info.ZhiWeiLeiXing = (EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing)rdr.GetByte(rdr.GetOrdinal("ZhiWeiLeiXing"));
                    info.IdentityId = Convert.ToInt32(rdr["IdentityId"]);

                }
            }

            if (info != null)
            {
                info.LeiBies = GetDaoYouLeiBies(daoYouId);
            }

            return info;
        }

        /// <summary>
        /// 获取导游导游信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouInfo> GetDaoYous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.ComStructure.MDaoYouInfo> items = new List<EyouSoft.Model.ComStructure.MDaoYouInfo>();

            string tableName = "view_DaoYou";
            string fields = "*";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            #region SQL
            if (chaXun != null)
            {
                if (chaXun.Gender.HasValue)
                {
                    sql.AppendFormat(" AND ContactSex={0} ", (int)chaXun.Gender.Value);
                }
                if (chaXun.JiBie.HasValue)
                {
                    sql.AppendFormat(" AND JiBie={0} ", (int)chaXun.JiBie.Value);
                }
                if (chaXun.LeiBie.HasValue)
                {
                    sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_SourceGuideCategory AS A1 WHEre A1.UserId=view_DaoYou.UserId AND A1.Category={0}) ", (int)chaXun.LeiBie.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.XingMing))
                {
                    sql.AppendFormat(" AND ContactName LIKE '%{0}%' ", chaXun.XingMing);
                }
                if (!string.IsNullOrEmpty(chaXun.YuZhong))
                {
                    sql.AppendFormat(" AND YuZhong LIKE '%{0}%' ", chaXun.YuZhong);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.ComStructure.MDaoYouInfo();

                    item.BeiZhu = rdr["Remark"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DaoYouId = rdr["UserId"].ToString();
                    item.DaoYouZhengHao = rdr["DaoYouZhengHao"].ToString();
                    item.Email = rdr["ContactEmail"].ToString();
                    item.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("ContactSex"));
                    item.GuaKaoDanWei = rdr["GuaKaoDanWei"].ToString();
                    item.IsNianShen = rdr["IsNianShen"].ToString() == "1";
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaTingDiZhi = rdr["JiaTingDiZhi"].ToString();
                    item.JiaTingTelephone = rdr["JiaTingTelephone"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JiBie"))) item.JiBie = (EyouSoft.Model.EnumType.ComStructure.DaoYouJiBie)rdr.GetByte(rdr.GetOrdinal("JiBie"));
                    item.KeHuPingJia = rdr["KeHuPingJia"].ToString();
                    item.LeiBies = null;
                    item.LingDuiZhengHao = rdr["LingDuiZhengHao"].ToString();
                    item.MSN = rdr["MSN"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Pwd = null;
                    item.QQ = rdr["QQ"].ToString();
                    item.ShanChangXianLu = rdr["ShanChangXianLu"].ToString();
                    item.ShenFenZhengHao = rdr["IDcardId"].ToString();
                    item.ShouJiHao = rdr["ContactMobile"].ToString();
                    item.TeChang = rdr["TeChang"].ToString();
                    item.Telephone = rdr["ContactTel"].ToString();
                    item.Username = rdr["UserName"].ToString();
                    item.XingGeTeDian = rdr["XingGeTeDian"].ToString();
                    item.XingMing = rdr["ContactName"].ToString();
                    item.YuZhong = rdr["YuZhong"].ToString();
                    item.ZhaoPianFilePath = rdr["UserImg"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ZhiWeiLeiXing"))) item.ZhiWeiLeiXing = (EyouSoft.Model.EnumType.ComStructure.ZhiWeiLeiXing)rdr.GetByte(rdr.GetOrdinal("ZhiWeiLeiXing"));
                    item.DaiTuanCiShu = rdr.GetInt32(rdr.GetOrdinal("DaiTuanCiShu"));
                    item.DaiTuanTianShu = rdr.GetInt32(rdr.GetOrdinal("DaiTuanTianShu"));
                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.LeiBies = GetDaoYouLeiBies(item.DaoYouId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<MGuidePlanWork> ResultList = null;
            string DateNow = "";
            string DateNext = "";
            if (NextTimeStart != null || NextTimeEnd != null)
            {
                DateNow = null;
                DateNext = null;
                if (NextTimeStart != null && NextTimeEnd != null && NextTimeEnd.Value.Subtract(NextTimeStart.Value).Days > 34)
                {
                    NextTimeEnd = NextTimeStart.Value.AddDays(34);
                }
                if (NextTimeStart != null && NextTimeEnd == null)
                {
                    NextTimeEnd = NextTimeStart.Value.AddDays(34);
                }
                if (NextTimeStart == null && NextTimeEnd != null)
                {
                    NextTimeStart = NextTimeEnd.Value.AddDays(-34);
                }
            }
            else
            {
                int monthNow = DateTime.Now.Month;
                int yearNow = DateTime.Now.Year;
                if (month > 0)
                {
                    monthNow = month;
                    if (year > 0)
                    {
                        yearNow = year;
                    }
                }
                if (monthNow == 1 || monthNow == 3 || monthNow == 5 || monthNow == 7 || monthNow == 8 || monthNow == 10 || monthNow == 12)
                {
                    DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                    DateNext = DateTime.Parse(DateNow).AddDays(30).ToString("yyyy-MM-dd");
                    DateNow = DateTime.Parse(DateNow).AddDays(-4).ToString("yyyy-MM-dd");
                }
                if (monthNow == 2)
                {
                    if ((yearNow % 100 == 0) && (yearNow % 400 == 0) || (yearNow % 100 != 0) && (yearNow % 4 == 0))
                    {
                        DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                        DateNext = DateTime.Parse(DateNow).AddDays(28).ToString("yyyy-MM-dd");
                        DateNow = DateTime.Parse(DateNow).AddDays(-6).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                        DateNext = DateTime.Parse(DateNow).AddDays(27).ToString("yyyy-MM-dd");
                        DateNow = DateTime.Parse(DateNow).AddDays(-7).ToString("yyyy-MM-dd");
                    }
                }
                if (monthNow == 4 || monthNow == 6 || monthNow == 9 || monthNow == 11)
                {
                    DateNow = yearNow.ToString() + "-" + monthNow.ToString() + "-1";
                    DateNext = DateTime.Parse(DateNow).AddDays(29).ToString("yyyy-MM-dd");
                    DateNow = DateTime.Parse(DateNow).AddDays(-5).ToString("yyyy-MM-dd");
                }
            }
            DbCommand dc = this._db.GetStoredProcCommand("proc_SelectGuideDYPB");
            this._db.AddInParameter(dc, "OnTimeStart", DbType.String, DateNow);
            this._db.AddInParameter(dc, "OnTimeEnd", DbType.String, DateNext);
            this._db.AddInParameter(dc, "NextTimeStart", DbType.String, NextTimeStart != null ? NextTimeStart.Value.ToString("yyyy-MM-dd") : "");
            this._db.AddInParameter(dc, "NextTimeEnd", DbType.String, NextTimeStart != null ? NextTimeEnd.Value.ToString("yyyy-MM-dd") : "");
            this._db.AddInParameter(dc, "NextLocation", DbType.String, Location);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null) && int.Parse(Result.ToString()) > 0)
            {
                string tableName = "view_GuideDYPB";
                string identityColumnName = "GuideId";
                string fields = " GuideId,CompanyId,Name,StaffStatus,IssueTime,TypeXML  ";
                string query = string.Format(" CompanyId='{0}' AND (StaffStatus<>2 OR TypeXML IS NOT NULL)", CompanyId);
                if (!string.IsNullOrEmpty(GuideName))
                {
                    query = query + string.Format(" AND Name like  '%{0}%' ", GuideName);
                }
                string orderByString = " IssueTime DESC";
                //using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, identityColumnName, fields, query, orderByString))
                //不分页
                var cmd = this._db.GetSqlStringCommand("select " + fields+" from " + tableName + " where " + query + " order by " + orderByString);
                using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(cmd,_db))
                {
                    ResultList = new List<MGuidePlanWork>();
                    MGuidePlanWork model = null;
                    while (dr.Read())
                    {
                        model = new MGuidePlanWork();
                        model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                        model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyID")) ? dr.GetString(dr.GetOrdinal("CompanyID")) : "";
                        model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                        //model.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus), dr.GetByte(dr.GetOrdinal("StaffStatus")).ToString());
                        model.TypeList = this.GetTypeList(dr["TypeXML"].ToString());
                        ResultList.Add(model);
                        model = null;
                    }
                };
            }
            return ResultList;
        }

        /// <summary>
        /// 获取排班统计列表
        /// </summary>
        /// <param name="CompanyId">系统公司编号</param>
        /// <param name="tourcode">团号</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="NextTimeStart">出团时间—开始</param>
        /// <param name="NextTimeEnd">出团时间—截至</param>
        /// <param name="tourmark">团态标识</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetPaiBanTongJi(string CompanyId, string tourcode, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string tourmark, int pageIndex, int pageSize, ref int recordCount)
        {
            var l = new List<MGuidePlanWork>();
            var q = new StringBuilder();

            //不分页
            q.Append("select TourId,LDate,RDate,TourCode,TourMark,SaleMark,ArriveCity,ArriveCityFlight,LeaveCity,LeaveCityFlight from tbl_tour where ");
            q.AppendFormat(" CompanyId = '{0}' AND TourType = {1}", CompanyId, (int)TourType.团队产品);
            q.Append(" AND IsDelete = 0");
            q.Append(" AND IsChuTuan = 0");
            q.AppendFormat(" AND ( LDate BETWEEN '{0}' AND '{1}' OR RDate BETWEEN '{0}' AND '{1}' OR ( LDate < '{0}' AND RDate > '{1}'))", Utils.GetFirstDayOfMonth(year, month), Utils.GetLastDayOfMonth(year, month));
            if (!string.IsNullOrEmpty(tourcode))
            {
                q.AppendFormat(" AND TourCode LIKE '%{0}%'", Utils.ToSqlLike(tourcode));
            }
            if (NextTimeStart.HasValue)
            {
                q.AppendFormat(" AND LDate >= '{0}'", NextTimeStart.Value);
            }
            if (NextTimeEnd.HasValue)
            {
                q.AppendFormat(" AND LDate < '{0}'", NextTimeEnd.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(tourmark))
            {
                q.AppendFormat(" AND TourMark LIKE '%{0}%'", Utils.ToSqlLike(tourmark));
            }
            q.Append(" order by ldate");

            var dc = this._db.GetSqlStringCommand(q.ToString());

            //using (var dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, "tbl_tour", "tourid", "TourId,LDate,RDate,TourCode,TourMark,SaleMark", q.ToString(), "ldate"))
            using (var dr = DbHelper.ExecuteReader(dc,_db))
            {
                while (dr.Read())
                {
                    l.Add(new MGuidePlanWork
                        {
                            TourId = dr["TourId"].ToString(),
                            TourCode = dr["TourCode"].ToString(),
                            LDate = dr.GetDateTime(dr.GetOrdinal("LDate")),
                            ArriveCity = dr["ArriveCity"].ToString(),
                            ArriveCityFlight = dr["ArriveCityFlight"].ToString(),
                            RDate = dr.GetDateTime(dr.GetOrdinal("RDate")),
                            LeaveCity = dr["LeaveCity"].ToString(),
                            LeaveCityFlight = dr["LeaveCityFlight"].ToString(),
                            TourMark = dr["TourMark"].ToString(),
                            SaleMark = dr["SaleMark"].ToString()
                        });
                }
            };
            return l;
        }

        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public IList<MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date)
        {
            IList<MGuidePlanWork> ResultList = null;
            DbCommand dc = _db.GetSqlStringCommand("select * from view_SourceGuidePlanWork where CompanyId=@CompanyId and GuideId=@GuideId and (datediff(dd,@date,StartDate)=0 or datediff(dd,@date,ENDdate)=0 or (@date>StartDate and @date<ENDdate))");
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            _db.AddInParameter(dc, "GuideId", DbType.AnsiStringFixedLength, GuideId);
            _db.AddInParameter(dc, "date", DbType.DateTime, date);
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, _db))
            {
                ResultList = new List<MGuidePlanWork>();
                MGuidePlanWork model = null;
                while (dr.Read())
                {
                    model = new MGuidePlanWork();
                    model.GuideId = !dr.IsDBNull(dr.GetOrdinal("GuideId")) ? dr.GetString(dr.GetOrdinal("GuideId")) : "";
                    model.CompanyId = !dr.IsDBNull(dr.GetOrdinal("CompanyId")) ? dr.GetString(dr.GetOrdinal("CompanyId")) : "";
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.PlanId = !dr.IsDBNull(dr.GetOrdinal("PlanId")) ? dr.GetString(dr.GetOrdinal("PlanId")) : "";
                    model.TourId = !dr.IsDBNull(dr.GetOrdinal("TourId")) ? dr.GetString(dr.GetOrdinal("TourId")) : "";
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : "";
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : "";
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                    {
                        model.LDate = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("RDate")))
                    {
                        model.RDate = dr.GetDateTime(dr.GetOrdinal("RDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("StartDate")))
                    {
                        model.OnTime = dr.GetDateTime(dr.GetOrdinal("StartDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDdate")))
                    {
                        model.NextTime = dr.GetDateTime(dr.GetOrdinal("ENDdate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }

                    model.TourDays = !dr.IsDBNull(dr.GetOrdinal("TourDays")) ? dr.GetInt32(dr.GetOrdinal("TourDays")) : 0;
                    model.OnLocation = !dr.IsDBNull(dr.GetOrdinal("OnLocation")) ? dr.GetString(dr.GetOrdinal("OnLocation")) : "";
                    model.NextLocation = !dr.IsDBNull(dr.GetOrdinal("NextLocation")) ? dr.GetString(dr.GetOrdinal("NextLocation")) : "";
                    model.GuideDays = !dr.IsDBNull(dr.GetOrdinal("GuideDays")) ? dr.GetInt32(dr.GetOrdinal("GuideDays")) : 0;
                    model.PlanCost = !dr.IsDBNull(dr.GetOrdinal("Confirmation")) ? dr.GetDecimal(dr.GetOrdinal("Confirmation")) : 0;
                    model.PlanState = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState), dr.GetByte(dr.GetOrdinal("Status")).ToString());
                    model.TourMark = dr["TourMark"].ToString();
                    model.SaleMark = dr["SaleMark"].ToString();
                    ResultList.Add(model);
                    model = null;
                }
            }
            return ResultList;
        }

        /// <summary>
        /// 删除导游信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="daoYouId">导游编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string daoYouId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_DaoYou_Delete");
            _db.AddInParameter(cmd, "@DaoYouId", DbType.AnsiStringFixedLength, daoYouId);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddOutParameter(cmd, "@RetCode", DbType.Int32, 4);

            int sqlExceptionCode = 0;
            try
            {
                DbHelper.RunProcedure(cmd, _db);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            if (sqlExceptionCode < 0)
            {
                return sqlExceptionCode;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode"));
            }
        }

        /// <summary>
        /// 获取导游带团明细信息集合
        /// </summary>
        /// <param name="daoYouId">导游编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo> GetDaoYouDaiTuanXXs(string daoYouId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo> items = new List<EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo>();
            string tableName = "view_DaoYou_DaiTuanXX";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" DaoYouId='{0}' ", daoYouId);
            #region SQL
            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.TourCode))
                {
                    sql.AppendFormat(" AND TourCode LIKE '%{0}%' ", Utils.ToSqlLike(chaXun.TourCode));
                }
                if (!string.IsNullOrEmpty(chaXun.RouteName))
                {
                    sql.AppendFormat(" AND RouteName LIKE '%{0}%' ", Utils.ToSqlLike(chaXun.RouteName));
                }
                if (chaXun.EDTTime.HasValue || chaXun.SDTTime.HasValue)
                {
                    sql.AppendFormat(" AND(1=0 ");
                    if (chaXun.SDTTime.HasValue && !chaXun.EDTTime.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN DTSTime AND DTETime) ", chaXun.SDTTime.Value);
                    }
                    if (chaXun.EDTTime.HasValue && !chaXun.SDTTime.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN DTSTime AND DTETime) ", chaXun.EDTTime.Value);
                    }
                    if (chaXun.EDTTime.HasValue && chaXun.SDTTime.HasValue)
                    {
                        sql.AppendFormat(" OR (DTSTime BETWEEN '{0}' AND '{1}') OR (DTETime BETWEEN '{0}' AND '{1}') OR (DTSTime < '{0}' AND DTETime > '{1}')", chaXun.SDTTime.Value, chaXun.EDTTime.Value);
                    }
                    sql.AppendFormat(" ) ");
                }

                if (chaXun.ELDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.ELDate.Value.AddDays(1));
                }
                if (chaXun.SLDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.SLDate.Value.AddDays(-1));
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.ComStructure.MDaoYouDaiTuanXXInfo();

                    item.AreaName = rdr["AreaName"].ToString();
                    item.DTETime = rdr.GetDateTime(rdr.GetOrdinal("DTETime"));
                    item.DTSTime = rdr.GetDateTime(rdr.GetOrdinal("DTSTime"));
                    item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    item.RJCR = rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.RJET = rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.RJLD = rdr.GetInt32(rdr.GetOrdinal("Leaders"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.TourCode = rdr["TourCode"].ToString();
                    item.TourId = rdr["TourId"].ToString();
                    item.XCTS = rdr.GetInt32(rdr.GetOrdinal("TourDays"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取导游上团统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo> GetDaoYouShangTuanTongJi(string companyId, EyouSoft.Model.ComStructure.MDaoYouShangTuanChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo> items = new List<EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo>();

            StringBuilder sql = new StringBuilder();

            #region sql
            sql.AppendFormat(" SELECT B.DaoYouId,COUNT(1) AS DaiTuanCiShu,SUM(B.DaiTuanTianShu) AS DaiTuanTianShu ");
            sql.AppendFormat(" ,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=B.DaoYouId) AS DaoYouName ");
            sql.AppendFormat("  FROM ( ");
            sql.AppendFormat(" SELECT * FROM view_DaoYou_DaiTuanXX AS A ");
            sql.AppendFormat(" WHERE A.CompanyId='{0}' ", companyId);
            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.DaoYouId))
                {
                    sql.AppendFormat(" AND A.DaoYouId='{0}' ", chaXun.DaoYouId);
                }
                else if (!string.IsNullOrEmpty(chaXun.DaoYouName))
                {
                    sql.AppendFormat(" AND A.DaoYouName LIKE '%{0}%' ", chaXun.DaoYouName);
                }
                if (!string.IsNullOrEmpty(chaXun.TourCode))
                {
                    sql.AppendFormat(" AND A.TourCode LIKE '%{0}%' ", Utils.ToSqlLike(chaXun.TourCode));
                }
                if (!string.IsNullOrEmpty(chaXun.RouteName))
                {
                    sql.AppendFormat(" AND A.RouteName LIKE '%{0}%' ", Utils.ToSqlLike(chaXun.RouteName));
                }

                if (chaXun.SDTTime.HasValue || chaXun.EDTTime.HasValue)
                {
                    sql.AppendFormat(" AND(1=0 ");
                    if (chaXun.SDTTime.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN DTSTime AND DTETime) ", chaXun.SDTTime.Value);
                    }
                    if (chaXun.EDTTime.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN DTSTime AND DTETime) ", chaXun.EDTTime.Value);
                    }
                    sql.AppendFormat(" ) ");
                }
            }

            sql.AppendFormat(" )B ");
            sql.AppendFormat(" GROUP BY B.DaoYouId ");
            #endregion

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.ComStructure.MDaoYouShangTuanInfo();

                    item.DaoYouId = rdr["DaoYouId"].ToString();
                    item.DaoYouName = rdr["DaoYouName"].ToString();
                    item.TianShu = rdr.GetInt32(rdr.GetOrdinal("DaiTuanTianShu"));
                    item.TuanDuiShu = rdr.GetInt32(rdr.GetOrdinal("DaiTuanCiShu"));

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
