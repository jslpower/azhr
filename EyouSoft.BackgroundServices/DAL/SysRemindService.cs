using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.SmsStructure;
using EyouSoft.Model.SmsStructure;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Xml.Linq;

namespace EyouSoft.BackgroundServices.DAL
{
    #region 出团提醒服务数据访问

    /// <summary>
    /// 出团提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysLeaveRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysLeaveRemindService
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 解析游客信息XML
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        private IList<MSmsTourTimeTraveller> ParseYouKeXml(string xml)
        {
            IList<MSmsTourTimeTraveller> list = null;
            if (string.IsNullOrEmpty(xml)) return list;

            XElement xRoot = XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0)  return list;

            list = new List<MSmsTourTimeTraveller>();
            foreach (var t in xRows)
            {
                var model = new MSmsTourTimeTraveller
                {
                    TravellerId = Utils.GetXAttributeValue(t, "TravellerId"),
                    Traveller = Utils.GetXAttributeValue(t, "CnName"),
                    Code = Utils.GetXAttributeValue(t, "Contact")
                };

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// parse daoyou xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        string[] ParseDaoYouXml(string xml)
        {
            string[] daoyou = new string[] { string.Empty, string.Empty };

            if (string.IsNullOrEmpty(xml)) return daoyou;

            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            if (xrows == null || xrows.Count() == 0) return daoyou;

            foreach (var xrow in xrows)
            {
                daoyou[0] = Utils.GetXAttributeValue(xrow, "SourceName");
                daoyou[1] = Utils.GetXAttributeValue(xrow, "ContactPhone");
                break;
            }

            return daoyou;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysLeaveRemindService()
        {
            _db = SystemStore;
        }

        #region 非ISysLeaveRemindService成员

        /// <summary>
        /// 更新短信当天已发送
        /// </summary>
        /// <param name="type">短信配置类型</param>
        /// <param name="companyId">公司编号集合</param>
        /// <returns></returns>
        int SetTodaySend(SettingType type, params string[] companyId)
        {
            if (companyId == null || companyId.Length < 1)
                return 0;

            var strSql = new StringBuilder(" update [tbl_SmsSetting] set [IsSend] = @IsSend ");
            strSql.Append(" where [Type] = @Type ");
            if (companyId.Length == 1)
                strSql.AppendFormat(" and [CompanyId] = '{0}' ", companyId[0]);
            else
            {
                string strIds = companyId.Where(t => !string.IsNullOrEmpty(t)).Aggregate(string.Empty, (current, t) => current + "'" + t + "',");
                strSql.AppendFormat(" and [CompanyId] in ({0}) ", strIds);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)type);

            return DbHelper.ExecuteSql(dc, _db);
        }

        /// <summary>
        /// 根据类型获取短信提醒配置，指定点时间等于当前点时间时，更新当天已发送
        /// </summary>
        /// <param name="settingType">提醒配置类型</param>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSettingByType(SettingType settingType)
        {
            var strSql = new StringBuilder(" SELECT [CompanyId],[Type],[Message],[BeforeDay],[Hour],[IsEnable],[IsSend],[OperatorId],[IssueTime] FROM [tbl_SmsSetting] ");
            strSql.Append(" where [IsEnable] = @IsEnable ");
            strSql.Append(" and [IsSend] = @IsSend ");
            strSql.Append(" and [Type] = @Type ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "IsEnable", DbType.AnsiStringFixedLength, "1");
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)settingType);

            var list = new Queue<MSmsSetting>();
            MSmsSetting model;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                while (dr.Read())
                {
                    int hour = -1;
                    if (!dr.IsDBNull(dr.GetOrdinal("Hour")))
                    {
                        hour = dr.GetInt32(dr.GetOrdinal("Hour"));
                    }

                    if (hour != DateTime.Now.Hour) continue;

                    model = new MSmsSetting();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Type")))
                        model.Type = (SettingType)dr.GetByte(dr.GetOrdinal("Type"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Message")))
                        model.Message = dr.GetString(dr.GetOrdinal("Message"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BeforeDay")))
                        model.BeforeDay = dr.GetInt32(dr.GetOrdinal("BeforeDay"));
                    model.Hour = hour;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")))
                        model.IsEnabled = GetBoolean(dr.GetString(dr.GetOrdinal("IsEnable")));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSend")))
                        model.IsSend = GetBoolean(dr.GetString(dr.GetOrdinal("IsSend")));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    model.SendSmsUserId=GetSendSmsUserId(model.CompanyId);

                    SetTodaySend(settingType, model.CompanyId);

                    list.Enqueue(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <param name="settingType">类型</param>
        /// <returns></returns>
        public IList<MSmsTourTimePlan> GetSmsTourTimePlanByType(string companyId, int beforeDay, SettingType settingType)
        {

            IList<MSmsTourTimePlan> list = new List<MSmsTourTimePlan>();
            if (string.IsNullOrEmpty(companyId))
                return list;

            if (beforeDay < 0) beforeDay = 0;
            DateTime cTime = DateTime.Now.AddDays(beforeDay);
            #region Sql语句构造

            var strSql = new StringBuilder();
            /*//团队信息
            strSql.Append(" select CompanyId,TourId,LDate,RDate,Gather,SellerId ");
            //游客信息
            strSql.Append(" ,(select TravellerId,CnName,Contact from tbl_TourOrderTraveller where OrderId in ");
            //订单编号条件
            strSql.AppendFormat(
                " (select OrderId from tbl_TourOrder where tbl_TourOrder.TourId = tbl_Tour.TourId and tbl_TourOrder.[Status] = {0} and tbl_TourOrder.IsDelete = '0') ",
                (int)Model.EnumType.TourStructure.OrderStatus.已成交);
            strSql.AppendFormat(" and [Status] = {0}  ", (int)Model.EnumType.TourStructure.TravellerStatus.在团);
            string strTmp = string.Empty;
            string strTmpTour = string.Empty;
            switch (settingType)
            {
                case SettingType.出团通知:
                    strTmp = " and [LNotice] = '1' ";
                    strTmpTour = string.Format(" and year(LDate) = {0} and month(LDate) = {1} and day(LDate) = {2} ",
                                               cTime.Year, cTime.Month, cTime.Day);
                    break;
                case SettingType.回团通知:
                    strTmp = " and [RNotice] = '1' ";
                    strTmpTour = string.Format(" and year(RDate) = {0} and month(RDate) = {1} and day(RDate) = {2} ",
                                               cTime.Year, cTime.Month, cTime.Day);
                    break;
            }
            strSql.Append(strTmp);
            strSql.Append(" for xml raw,root('root')) as TravellerInfo ");

            strSql.Append(" ,RouteName ");
            strSql.Append(" ,(SELECT SourceName,ContactPhone FROM tbl_Plan AS B WHERE B.TourId=tbl_Tour.TourId AND B.IsDelete='0' AND [Status]=4 FOR XML RAW,ROOT('root')) AS DaoYouXml ");

            strSql.Append(" from tbl_Tour where IsDelete = '0' ");
            strSql.Append(strTmpTour);
            strSql.AppendFormat(" and tbl_Tour.CompanyId = '{0}' ", companyId);
            strSql.Append(" and exists (");
            strSql.AppendFormat(" select 1 from tbl_TourOrder where tbl_TourOrder.TourId = tbl_Tour.TourId AND tbl_TourOrder.IsDelete='0' AND tbl_TourOrder.Status={0} ", (int)Model.EnumType.TourStructure.OrderStatus.已成交);
            strSql.Append(" and exists (select 1 from tbl_TourOrderTraveller where tbl_TourOrderTraveller.OrderId = tbl_TourOrder.OrderId and Contact is not null and Contact <> '' ");
            strSql.Append(strTmp);
            strSql.Append(" ) ");
            strSql.Append(" ) ");*/

            string s1 = string.Empty;//游客查询条件
            string s2 = string.Empty;//团队查询条件
            switch (settingType)
            {
                case SettingType.出团通知:
                    s1 = " AND A1.[LNotice] = '1' ";
                    s2 = string.Format(" AND year(A.LDate) = {0} AND month(A.LDate) = {1} AND day(A.LDate) = {2} ", cTime.Year, cTime.Month, cTime.Day);
                    break;
                case SettingType.回团通知:
                    s1 = " AND A1.[RNotice] = '1' ";
                    s2 = string.Format(" AND year(A.RDate) = {0} AND month(A.RDate) = {1} AND day(A.RDate) = {2} ", cTime.Year, cTime.Month, cTime.Day);
                    break;
            }

            strSql.AppendFormat(" SELECT A.CompanyId,A.TourId,A.TourCode,A.RouteName,A.LDate,A.RDate ");
            strSql.AppendFormat(" ,(SELECT A1.TravellerId,A1.CnName,A1.Contact FROM tbl_TourOrderTraveller AS A1 WHERE A1.TourId=A.TourId AND A1.Contact IS NOT NULL AND LEN(A1.Contact)>0 AND A1.Status=0 {0}  FOR XML RAW,ROOT('root')) AS YouKeXml) ", s1);
            //strSql.AppendFormat(" ,(SELECT A1.SourceName,A1.ContactPhone FROM tbl_Plan AS A1 WHERE A1.TourId=A.TourId AND A1.Status=4 AND A1.Type=4 AND A.IsDelete='0'  FOR XML RAW,ROOT('root')) AS DaoYouXml ");
            strSql.AppendFormat(" FROM [tbl_Tour] AS A WHERE A.CompanyId='{0}' AND IsDelete='0' ", companyId);
            strSql.Append(s2);
            strSql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourOrderTraveller AS A1 WHERE A1.TourId=A.TourId AND A1.Contact IS NOT NULL AND LEN(A1.Contact)>0 AND A1.Status=0 {0} ) ", s1);

            #endregion

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                MSmsTourTimePlan model;
                while (dr.Read())
                {
                    model = new MSmsTourTimePlan();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TourId")))
                        model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LDate")))
                        model.LeaveTime = dr.GetDateTime(dr.GetOrdinal("LDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RDate")))
                        model.BackTime = dr.GetDateTime(dr.GetOrdinal("RDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("YouKeXml")))
                        model.Traveller = ParseYouKeXml(dr.GetString(dr.GetOrdinal("YouKeXml")));
                    model.RouteName = dr["RouteName"].ToString();
                    model.TourCode = dr["TourCode"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }


        /// <summary>
        /// 获取发送短信用户编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        string GetSendSmsUserId(string companyId)
        {
            string s = "SELECT UserId FROM tbl_ComUser WHERE CompanyId=@CompanyId AND IsDelete='0' AND IsAdmin='1' ";
            DbCommand cmd = _db.GetSqlStringCommand(s);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetString(0);
                }
            }

            return string.Empty;
        }
        #endregion

        #region ISysLeaveRemindService成员

        /// <summary>
        /// 获取出团提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return GetSmsSettingByType(SettingType.出团通知);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<Model.SmsStructure.MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay)
        {
            return GetSmsTourTimePlanByType(companyId, beforeDay, SettingType.出团通知);
        }

        #endregion
    }

    #endregion

    #region 回团提醒服务数据访问

    /// <summary>
    /// 回团提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysBackRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysBackRemindService
    {
        #region private member

        /// <summary>
        /// 出团提醒服务数据访问类的实例对象（回团提醒的数据访问通过此对象）
        /// </summary>
        private readonly SysLeaveRemindService _dalLeaveRemind = new SysLeaveRemindService();

        #endregion

        /// <summary>
        /// 获取回团提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return _dalLeaveRemind.GetSmsSettingByType(SettingType.回团通知);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<MSmsTourTimePlan> GetSmsTourTimePlan(string companyId, int beforeDay)
        {
            return _dalLeaveRemind.GetSmsTourTimePlanByType(companyId, beforeDay, SettingType.回团通知);
        }
    }

    #endregion

    #region 生日提醒服务数据访问

    /// <summary>
    /// 生日提醒服务数据访问
    /// </summary>
    /// 周文超 2011-09-23
    public class SysBirthdayRemindService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysBirthdayRemindService
    {
        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 出团提醒服务数据访问类的实例对象
        /// </summary>
        private readonly SysLeaveRemindService _dalLeaveRemind = new SysLeaveRemindService();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysBirthdayRemindService()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 获取生日提醒配置
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return _dalLeaveRemind.GetSmsSettingByType(SettingType.生日提醒);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<MSmsBirthdayRemindPlan> GetSmsBirthdayRemindPlan(string companyId, int beforeDay)
        {
            IList<MSmsBirthdayRemindPlan> list = new List<MSmsBirthdayRemindPlan>();
            if (string.IsNullOrEmpty(companyId))
                return list;

            if (beforeDay < 0) beforeDay = 0;
            DateTime cTime = DateTime.Now.AddDays(beforeDay);

            var strSql = new StringBuilder();
            //客户联系人（含组团社、供应商）
            strSql.Append(" SELECT CompanyId,[Name],MobilePhone,Birthday FROM tbl_CrmLinkman ");
            strSql.Append(" WHERE MobilePhone IS NOT NULL AND MobilePhone <> '' AND LEN(MobilePhone)>0 AND Birthday IS NOT NULL AND Birthday>'1900-01-01' ");
            strSql.AppendFormat(" AND MONTH(Birthday) = {0} AND DAY(Birthday) = {1} ", cTime.Month, cTime.Day);
            strSql.AppendFormat(" AND IsDelete='0' AND IsRemind='1' AND CompanyId='{0}' ", companyId);

            strSql.Append(" UNION ALL  ");

            strSql.AppendFormat(" SELECT [CompanyId],[ContactName],[ContactMobile],[BirthDate] FROM [tbl_ComUser] ");
            strSql.AppendFormat(" WHERE ContactMobile IS NOT NULL AND LEN(ContactMobile)>0 AND BirthDate IS NOT NULL AND BirthDate>'1900-01-01' ");
            strSql.AppendFormat(" AND MONTH(BirthDate) = {0} AND DAY(BirthDate) = {1} ", cTime.Month, cTime.Day);
            strSql.AppendFormat(" AND IsDelete='0' AND CompanyId='{0}' ", companyId);

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                MSmsBirthdayRemindPlan model;
                while (dr.Read())
                {
                    model = new MSmsBirthdayRemindPlan();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Name")))
                        model.Name = dr.GetString(dr.GetOrdinal("Name"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MobilePhone")))
                        model.MobilePhone = dr.GetString(dr.GetOrdinal("MobilePhone"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Birthday"))) model.Birthday = dr.GetDateTime(dr.GetOrdinal("Birthday"));

                    list.Add(model);
                }
            }

            return list;
        }
    }

    #endregion

    #region 进店提醒服务数据访问类
    /// <summary>
    /// 进店提醒服务数据访问类
    /// </summary>
    public class DSysJinDianTiXingService : DALBase, EyouSoft.BackgroundServices.IDAL.ISysJinDianTiXingService
    {
        #region constructor
        /// <summary>
        /// 出团提醒服务数据访问类的实例对象
        /// </summary>
        private readonly SysLeaveRemindService _dalLeaveRemind = new SysLeaveRemindService();

        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DSysJinDianTiXingService()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// parse daoyou xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IList<MSmsJinDianDaoYouInfo> ParseDaoYouXml(string xml)
        {
            IList<MSmsJinDianDaoYouInfo> items = new List<MSmsJinDianDaoYouInfo>();
            if (string.IsNullOrEmpty(xml)) return items;
            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            if (xrows == null || xrows.Count() == 0) return items;
            foreach (var xrow in xrows)
            {
                var item = new MSmsJinDianDaoYouInfo();
                item.Mobile = Utils.GetXAttributeValue(xrow, "ContactPhone");
                item.Name = Utils.GetXAttributeValue(xrow, "SourceName");
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// parse gouwudian xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IList<MSmsJinDianGouWuInfo> ParseGouWuXml(string xml)
        {
            IList<MSmsJinDianGouWuInfo> items = new List<MSmsJinDianGouWuInfo>();
            if (string.IsNullOrEmpty(xml)) return items;
            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            if (xrows == null || xrows.Count() == 0) return items;
            foreach (var xrow in xrows)
            {
                var item = new MSmsJinDianGouWuInfo();
                item.Address = Utils.GetXAttributeValue(xrow, "Address"); ;
                item.Name = Utils.GetXAttributeValue(xrow, "SourceName");
                items.Add(item);
            }
            return items;
        }
        #endregion

        #region EyouSoft.BackgroundServices.IDAL.ISysJinDianTiXingService 成员
        /// <summary>
        /// 获取进店提醒配置，返回结果均是当前点时间内需要发送的配置信息
        /// </summary>
        /// <returns></returns>
        public Queue<MSmsSetting> GetSmsSetting()
        {
            return _dalLeaveRemind.GetSmsSettingByType(SettingType.进店提醒);
        }

        /// <summary>
        /// 根据公司编号获取待发送的短信实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="beforeDay">提前X天</param>
        /// <returns></returns>
        public IList<MSmsJinDianTiXingPlanInfo> GetSmsJinDianTiXings(string companyId, int beforeDay)
        {
            if (beforeDay < 0) beforeDay = 0;
            DateTime cTime = DateTime.Today.AddDays(beforeDay);

            IList<MSmsJinDianTiXingPlanInfo> items = new List<MSmsJinDianTiXingPlanInfo>();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" SELECT A.CompanyId,A.TourId,A.TourCode,A.RouteName ");
            sql.AppendFormat(" ,(SELECT A1.PlanId,A1.SourceName,A1.ContactPhone FROM tbl_Plan AS A1 WHERE A1.TourId=A.TourId AND A1.Status={0} AND A1.Type={1} AND A1.IsDelete='0' FOR XML RAW,ROOT('root')) AS DaoYouXml ",(int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实,(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游);
            sql.AppendFormat(" ,(SELECT A1.PlanId,A1.SourceId,A1.SourceName,B.Address FROM tbl_Plan AS A1 LEFT OUTER JOIN dbo.tbl_Source B ON A1.SourceId=B.SourceId WHERE A1.TourId=A.TourId AND A1.Status={0} AND A1.Type={1} AND A1.IsDelete='0' FOR XML RAW,ROOT('root')) AS GWXml ", (int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实, (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物);
            sql.AppendFormat(" FROM [tbl_Tour] AS A ");
            sql.AppendFormat(" WHERE A.IsDelete='0' AND A.CompanyId='{0}' ", companyId);
            sql.AppendFormat(" AND A.LDate='{0}' ", cTime);
            sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_Plan AS A1 WHERE A1.TourId=A.TourId AND A1.Status={0} AND A1.Type={1} AND A1.IsDelete='0') ", (int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实, (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游);
            sql.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_Plan AS A1 WHERE A1.TourId=A.TourId AND A1.Status={0} AND A1.Type={1} AND A1.IsDelete='0') ", (int)EyouSoft.Model.EnumType.PlanStructure.PlanState.已落实, (int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物);

            DbCommand cmd= _db.GetSqlStringCommand(sql.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSmsJinDianTiXingPlanInfo();
                    item.CompanyId = companyId;
                    item.DaoYous = ParseDaoYouXml(rdr["DaoYouXml"].ToString());
                    item.GouWus = ParseGouWuXml(rdr["GWXml"].ToString());
                    item.RouteName = rdr["RotueName"].ToString();
                    item.TourCode = rdr["TourCode"].ToString();
                    item.TourId = rdr["TourId"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
    #endregion
}
