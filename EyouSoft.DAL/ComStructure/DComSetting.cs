using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 系统配置
    /// 创建者：郑付杰
    /// 创建时间:2011/9/20
    /// </summary>
    public class DComSetting : DALBase, EyouSoft.IDAL.ComStructure.IComSetting
    {
        #region static constants
        //static constants
        const string SQL_PROC_SetSettings = "proc_ComSetting_Update";
        const string SQL_SELECT_GetSettings = "SELECT [Key],[Value] FROM tbl_ComSetting WHERE [CompanyId]=@CompanyId";//SELECT [WLogo],[NLogo],[MLogo],[Name] FROM [tbl_Company] WHERE [Id]=@CompanyId
        #endregion

        #region constructor
        private readonly Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DComSetting()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region public members
        /*/// <summary>
        /// 获取公司配置信息
        /// </summary>
        /// <param name="company">公司编号</param>
        /// <returns>配置实体</returns>
        public MComSetting GetModel(string company)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSettings);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, company);

            MComSetting item = new MComSetting();
            item.PrintDocument = new List<PrintDocument>();
            item.SmsConfig = new MSmsConfigInfo();

            using (IDataReader reader = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (reader.Read())
                {
                    string key = reader["Key"].ToString();
                    string value = reader["value"].ToString();

                    if (string.IsNullOrEmpty(key)) continue;

                    #region 单据配置键处理
                    //单据配置的key为枚举+'_'
                    if (key.IndexOf(string.Format("{0}_", (int)SysConfiguration.单据配置)) == 0)
                    {
                        string printTemplateType = key.Substring(string.Format("{0}_", (int)SysConfiguration.单据配置).Length);
                        item.PrintDocument.Add(new PrintDocument()
                        {
                            PrintTemplate = value,
                            PrintTemplateType = (PrintTemplateType)Enum.Parse(typeof(PrintTemplateType), printTemplateType)
                        });
                        continue;
                    }
                    #endregion

                    #region 配置键为数字处理
                    int? _key = EyouSoft.Toolkit.Utils.GetIntNullable(key, null);
                    if (_key.HasValue)
                    {
                        EyouSoft.Model.EnumType.ComStructure.SysConfiguration sysConfiguration = (SysConfiguration)_key.Value;
                        switch (sysConfiguration)
                        {
                            case SysConfiguration.Word模版: item.WordTemplate = value; break;
                            //case SysConfiguration.变更提醒: item.ChangeRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.出团提醒: item.GroupRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.弹窗提醒: item.PopRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.付款提醒: item.PaymnetRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.公告提醒: item.AnnouncementRemind = Utils.GetInt(value); break;
                            case SysConfiguration.公司章: item.CompanyChapter = value; break;
                            //case SysConfiguration.回团提醒: item.BackRemind = Utils.GetInt(value); break;
                            case SysConfiguration.积分比例: item.IntegralProportion = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收出境线: item.ExitArea = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收国内线: item.CountryArea = Utils.GetInt(value); break;
                            case SysConfiguration.计划停收省内线: item.ProvinceArea = Utils.GetInt(value); break;
                            case SysConfiguration.劳动合同到期提醒: item.ContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.供应商合同到期提醒: item.SContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.公司合同到期提醒: item.ComPanyContractRemind = Utils.GetInt(value); break;
                            case SysConfiguration.洒店预控到期提醒: item.HotelControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.车辆预控到期提醒: item.CarControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.游船预控到期提醒: item.ShipControlRemind = Utils.GetInt(value); break;
                            case SysConfiguration.列表显示控制_前几个月: item.ShowBeforeMonth = Utils.GetInt(value); break;
                            case SysConfiguration.列表显示控制_后几个月: item.ShowAfterMonth = Utils.GetInt(value); break;
                            case SysConfiguration.留位时间控制: item.SaveTime = Utils.GetInt(value); break;
                            case SysConfiguration.欠款额度控制: item.ArrearsRangeControl = GetBoolean(value); break;
                            //case SysConfiguration.生日提醒: item.BirthdayRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.收款提醒: item.CollectionRemind = Utils.GetInt(value); break;
                            //case SysConfiguration.提醒时间配置: item.RemindTime = Utils.GetInt(value); break;
                            case SysConfiguration.跳过报账终审: item.SkipFinalJudgment = GetBoolean(value); break;
                            case SysConfiguration.跳过导游报账: item.SkipGuide = GetBoolean(value); break;
                            case SysConfiguration.跳过销售报账: item.SkipSale = GetBoolean(value); break;
                            case SysConfiguration.团号配置: item.TourNoSetting = value; break;
                            //case SysConfiguration.物品借阅到期提醒: item.BorrowingRemind = Utils.GetInt(value); break;
                            case SysConfiguration.页脚: item.FooterPath = value; break;
                            case SysConfiguration.页眉: item.PagePath = value; break;
                            //case SysConfiguration.询价提醒: item.InquiryRemind = Utils.GetInt(value); break;
                            case SysConfiguration.财务收入审核: item.FinancialIncomeReview = GetBoolean(value); break;
                            case SysConfiguration.财务支出审核: item.FinancialExpensesReview = GetBoolean(value); break;
                            //case SysConfiguration.订单毛利配置: item.OrderCost = GetBoolean(value); break;
                            //case SysConfiguration.下计调任务跳过: item.PlanTaskJump = GetBoolean(value); break;
                            //case SysConfiguration.报账配置: item.PlanGuide = Utils.GetEnumValue<PlanGuideSetting>(value, PlanGuideSetting.显示所有计调); break;
                            //case SysConfiguration.封团配置: item.TourClose = Utils.GetEnumValue<TourCloseSetting>(value, TourCloseSetting.单团核算结束后封团); break;
                            case SysConfiguration.是否开启KIS整合: item.IsEnableKis = GetBoolean(value); break;
                            default: break;
                        }

                        continue;
                    }
                    #endregion

                    #region 配置键为字符串处理
                    switch (key)
                    {
                        case "SmsAccount": item.SmsConfig.Account = value; break;
                        case "SmsAppKey": item.SmsConfig.AppKey = value; break;
                        case "SmsAppSecret": item.SmsConfig.AppSecret = value; break;
                    }
                    #endregion
                }

                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        item.WLogo = reader["WLogo"].ToString();
                        item.NLogo = reader["NLogo"].ToString();
                        item.MLogo = reader["MLogo"].ToString();
                        item.CompanyName = reader["Name"].ToString();
                    }
                }

                item.CompanyId = company;
            }

            return item;
        }*/

        /// <summary>
        /// 修改公司配置信息
        /// </summary>
        /// <param name="item">配置实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool UpdateComSetting(MComSetting item)
        {
            #region create xml
            //xml格式<item><val id="key">value</val></item>
            StringBuilder xml = new StringBuilder("<item>");

            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.页眉, item.PagePath);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.页脚, item.FooterPath);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.Word模版, item.WordTemplate);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.公司章, item.CompanyChapter);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.积分比例, item.IntegralProportion);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收出境线, item.ExitArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收国内线, item.CountryArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收省内线, item.ProvinceArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.列表显示控制_后几个月, item.ShowAfterMonth);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.列表显示控制_前几个月, item.ShowBeforeMonth);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.留位时间控制, item.SaveTime);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过报账终审, GetBooleanToStr(item.SkipFinalJudgment));
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过导游报账, GetBooleanToStr(item.SkipGuide));
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过销售报账, GetBooleanToStr(item.SkipSale));
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.财务收入审核, GetBooleanToStr(item.FinancialIncomeReview));
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.财务支出审核, GetBooleanToStr(item.FinancialExpensesReview));
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.欠款额度控制, GetBooleanToStr(item.ArrearsRangeControl));

            xml.Append("</item>");
            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }

        /// <summary>
        /// 修改团号配置
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TourCodeRule">团号规则</param>
        /// <returns></returns>
        public bool UpdateTourCodeSet(string CompanyId, string TourCodeRule)
        {
            #region create xml
            //xml格式<item><val id="key">value</val></item>
            StringBuilder xml = new StringBuilder();
            xml.Append("<item>");
            xml.AppendFormat("<val id='{0}'>{1}</val>",  (int)SysConfiguration.团号配置, TourCodeRule);
            xml.Append("</item>");
            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }

        ///// <summary>
        ///// 根据键名获取值
        ///// </summary>
        ///// <param name="CompanyId"></param>
        ///// <param name="SettingType"></param>
        ///// <returns></returns>
        //public string GetSettingByType(string CompanyId, SysConfiguration SettingType)
        //{
        //    string sql = "SELECT CompanyId,[Key],[Value] FROM tbl_ComSetting WHERE CompanyId = @CompanyId and [Key]=@Key";
        //    DbCommand comm = this._db.GetSqlStringCommand(sql);
        //    this._db.AddInParameter(comm, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
        //    this._db.AddInParameter(comm, "Key", DbType.String, (int)SettingType);
        //    string value = string.Empty;
        //    using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
        //    {
        //        if (reader.Read())
        //        {
        //            value = reader.IsDBNull(reader.GetOrdinal("Value")) ? "0" : reader.GetString(reader.GetOrdinal("Value"));
        //        }
        //    }
        //    return value;
        //}

        /// <summary>
        /// 设置公司短信配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">配置信息</param>
        /// <returns></returns>
        public bool SetComSmsConfig(string companyId, EyouSoft.Model.ComStructure.MSmsConfigInfo info)
        {
            #region create xml
            //xml格式<item><val id="key">value</val></item>
            StringBuilder xml = new StringBuilder();
            xml.Append("<item>");
            xml.AppendFormat("<val id='{0}'>{1}</val>", "SmsAccount", info.Account);
            xml.AppendFormat("<val id='{0}'>{1}</val>", "SmsAppKey", info.AppKey);
            xml.AppendFormat("<val id='{0}'>{1}</val>", "SmsAppSecret", info.AppSecret);
            xml.Append("</item>");
            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }

        /// <summary>
        /// 设置金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="info">金蝶默认科目配置信息业务实体</param>
        /// <returns></returns>
        public bool SetKisConfigInfo(string companyId, EyouSoft.Model.ComStructure.MKisConfigInfo info)
        {
            #region create xml
            //xml格式<item><val id="key">value</val></item>
            StringBuilder xml = new StringBuilder();
            xml.Append("<item>");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷, info.Kis1000);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借, info.Kis1999);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷, info.Kis2000);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借, info.Kis2999);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷, info.Kis3000);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借, info.Kis3999);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借, info.Kis4000);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷, info.Kis4500);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷, info.Kis4501);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷, info.Kis4999);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷, info.Kis5000);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷, info.Kis5001);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷, info.Kis5002);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷, info.Kis5003);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷, info.Kis5004);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷, info.Kis5005);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借, info.Kis5006);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借, info.Kis5007);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借, info.Kis5008);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借, info.Kis5009);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借, info.Kis5999);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务收入_贷, info.Kis6000);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队预支_贷, info.Kis6001);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应付账款_贷, info.Kis6002);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队支出_贷, info.Kis6003);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_预收账款_借, info.Kis6500);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应收账款_借, info.Kis6501);
            //xml.AppendFormat("<val id='{0}'>{1}</val>", (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务成本_借, info.Kis6999);
            xml.Append("</item>");
            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }

        /// <summary>
        /// 获取金蝶默认科目配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.ComStructure.MKisConfigInfo GetKisConfigInfo(string companyId)
        {
            var info = new EyouSoft.Model.ComStructure.MKisConfigInfo();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetSettings);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    int? _key = EyouSoft.Toolkit.Utils.GetIntNullable(rdr["Key"].ToString(), null);
                    if (!_key.HasValue) continue;

                    string _value = rdr["Value"].ToString();

                    switch (_key)
                    {
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_贷: info.Kis1000 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.订单收款_借: info.Kis1999 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_贷: info.Kis2000 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.计调预付款_借: info.Kis2999 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_贷: info.Kis3000 = _value; break;
                        case (int) EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.导游备用金_借: info.Kis3999 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预付账款_借: info.Kis4000 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_预收账款_贷: info.Kis4500 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_团队借款_贷: info.Kis4501 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.团未完导游先报账_现金_贷: info.Kis4999 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务收入_贷: info.Kis5000 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队借款_贷: info.Kis5001 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队预支_贷: info.Kis5002 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_团队支出_贷: info.Kis5003 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应付账款_贷: info.Kis5004 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_贷: info.Kis5005 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_主营业务成本_借: info.Kis5006 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_预收账款_借: info.Kis5007 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_现金_借: info.Kis5008 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_银行存款_借: info.Kis5009 = _value; break;
                        case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.单团核算_应收帐款_借: info.Kis5999 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务收入_贷: info.Kis6000 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队预支_贷: info.Kis6001 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应付账款_贷: info.Kis6002 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_团队支出_贷: info.Kis6003 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_预收账款_借: info.Kis6500 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_应收账款_借: info.Kis6501 = _value; break;
                        //case (int)EyouSoft.Model.EnumType.KingDee.SysConfigDefaultSubject.后期收款_主营业务成本_借: info.Kis6999 = _value; break;
                        default: break;
                    }

                }
            } 

            return info;
        }

        /// <summary>
        /// 设置子系统配置信息(webmaster)
        /// </summary>
        /// <param name="setting">配置信息业务实体</param>
        /// <returns></returns>
        public bool SetSysSetting(EyouSoft.Model.ComStructure.MComSetting setting)
        {
            #region create xml
            var xml = new StringBuilder();
            xml.Append("<item>");

            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.团号配置, Utils.ReplaceXmlSpecialCharacter(setting.TourNoSetting));
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.列表显示控制_前几个月, setting.ShowBeforeMonth);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.列表显示控制_后几个月, setting.ShowAfterMonth);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.留位时间控制, setting.SaveTime);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收国内线, setting.CountryArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收省内线, setting.ProvinceArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.计划停收出境线, setting.ExitArea);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.积分比例, setting.IntegralProportion);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过导游报账, setting.SkipGuide ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过销售报账, setting.SkipSale ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.跳过报账终审, setting.SkipFinalJudgment ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.劳动合同到期提醒, setting.ContractRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.供应商合同到期提醒, setting.SContractRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.公司合同到期提醒, setting.ComPanyContractRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.财务支出审核, setting.FinancialExpensesReview ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.财务收入审核, setting.FinancialIncomeReview ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.欠款额度控制, setting.ArrearsRangeControl ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.洒店预控到期提醒, setting.HotelControlRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.车辆预控到期提醒, setting.CarControlRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.游船预控到期提醒, setting.ShipControlRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.是否开启KIS整合, setting.IsEnableKis ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.景点预控到期提醒, setting.SightControlRemind);
            xml.AppendFormat("<val id='{0}'>{1}</val>", (int)SysConfiguration.其他预控到期提醒, setting.OtherControlRemind);            

            //打印单据配置
            if (setting.PrintDocument != null && setting.PrintDocument.Count > 0)
            {
                foreach (var t in setting.PrintDocument)
                {
                    if (t == null) continue;

                    xml.AppendFormat("<val id='{0}_{1}'>{2}</val>", (int)SysConfiguration.单据配置, (int)t.PrintTemplateType, t.PrintTemplate);
                }
            }

            xml.AppendFormat("<val id='{0}'>{1}</val>", "MaxUserNumber", setting.MaxUserNumber);
            xml.AppendFormat("<val id='{0}'>{1}</val>", "UserLoginLimitType", (int)setting.UserLoginLimitType);
            xml.AppendFormat("<val id='{0}'>{1}</val>", "IsEnableDuanXian", setting.IsEnableDuanXian ? "1" : "0");
            xml.AppendFormat("<val id='{0}'>{1}</val>", "ZiDongShanChuSanPinJiHua", setting.IsZiDongShanChuSanPinJiHua ? "1" : "0");

            xml.Append("</item>");

            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, setting.CompanyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }

        /// <summary>
        /// 设置单个配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <returns></returns>
        public bool SetKeyValue(string companyId, string key, string value)
        {
            #region create xml
            //xml格式<item><val id="key">value</val></item>
            StringBuilder xml = new StringBuilder();
            xml.Append("<item>");
            xml.AppendFormat("<val id='{0}'>{1}</val>", key, value);
            xml.Append("</item>");
            #endregion

            DbCommand cmd = this._db.GetStoredProcCommand(SQL_PROC_SetSettings);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "@xml", DbType.Xml, xml.ToString());
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
                return false;
            }
            else
            {
                return Convert.ToInt32(_db.GetParameterValue(cmd, "RetCode")) == 1;
            }
        }
        #endregion
    }
}
