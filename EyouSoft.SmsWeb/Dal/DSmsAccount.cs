using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EyouSoft.SmsWeb.Model;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace EyouSoft.SmsWeb.Dal
{
    /// <summary>
    /// 短信账户信息数据访问
    /// </summary>
    /// 周文超 2011-09-14
    public class DSmsAccount : DALBase
    {
        #region static constants
        //static constants
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 账户信息添加Sql
        /// </summary>
        private const string SqlSmsAccountAdd = @" INSERT INTO [tbl_SmsAccount]
           ([AccountId]
           ,[AppKey]
           ,[AppSecret]
           ,[Pwd]
           ,[Amount]
           ,[SysType]
           ,[IssueTime])
     VALUES
           (@AccountId
           ,@AppKey
           ,@AppSecret
           ,@Pwd
           ,@Amount
           ,@SysType
           ,@IssueTime); ";

        /// <summary>
        /// 短信发送价格添加Sql
        /// </summary>
        private const string SqlSmsUnitPriceAdd = @" INSERT INTO [tbl_SmsUnitPrice]
           ([AccountId]
           ,[Channel]
           ,[UnitPrice])
     VALUES
           ('{0}'
           ,{1}
           ,{2}); ";

        /// <summary>
        /// 设置短信账户的发送价格Sql
        /// </summary>
        private const string SqlSmsUnitPriceSet = @" if exists (select 1 from [tbl_SmsUnitPrice] where [AccountId] = '{0}' and [Channel] = {1})
	begin
		update [tbl_SmsUnitPrice] 
		set [UnitPrice] = {2} 
		where [AccountId] = '{0}' and [Channel] = {1}
	end
else
	begin
		INSERT INTO [tbl_SmsUnitPrice]
			   ([AccountId]
			   ,[Channel]
			   ,[UnitPrice])
		 VALUES
			   ('{0}'
			   ,{1}
			   ,{2})
	end ; ";

        const string SQL_SELECT_IsExists = "SELECT COUNT(*) FROM [tbl_SmsAccount] WHERE [AccountId]=@AccountId AND [AppKey]=@AppKey";
        #endregion

        #region private members

        /// <summary>
        /// 生成发送价格表
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="priceList">发送价格集合</param>
        /// <returns></returns>
        private string GetInsertPriceSql(string accountId, List<MSmsChannelInfo> priceList)
        {
            if (string.IsNullOrEmpty(accountId) || priceList == null || priceList.Count < 1) return string.Empty;

            var strSql = new StringBuilder();
            foreach (var t in priceList)
            {
                if (t.Price <= 0) continue;

                strSql.AppendFormat(SqlSmsUnitPriceAdd, accountId, (int)t.Cnannel, t.Price);
            }

            return strSql.ToString();
        }

        /// <summary>
        /// 根据SqlXml构造发送短信价格信息集合
        /// </summary>
        /// <param name="strSqlXml">SqlXml</param>
        /// <returns>发送短信价格信息集合</returns>
        private List<MSmsChannelInfo> GetSmsUnitPrice(string strSqlXml)
        {
            if (string.IsNullOrEmpty(strSqlXml)) return null;

            List<MSmsChannelInfo> items = new List<MSmsChannelInfo>();

            XElement xRoot = XElement.Parse(strSqlXml);
            var xRows = Utils.GetXElements(xRoot, "row");
            if (xRows == null || xRows.Count() <= 0) return null;

            foreach (var xrow in xRows)
            {
                var item = new MSmsChannelInfo();
                item.Cnannel = Utils.GetEnumValue<Channel>(Utils.GetXAttributeValue(xrow, "Channel"), Channel.通用通道);
                item.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xrow, "UnitPrice"));
                item.Name = item.Cnannel.ToString();

                items.Add(item);
            }

            return items;
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsAccount()
        {
            _db = SmsStore;
        }

        /// <summary>
        /// 短信中心开户
        /// </summary>
        /// <param name="model">短信账户信息实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public MSmsAccountBase AddSmsAccount(MSmsAccount model)
        {
            MSmsAccountBase rModel = null;            
            DbCommand dc = _db.GetSqlStringCommand(SqlSmsAccountAdd + GetInsertPriceSql(model.AccountId, model.SmsUnitPrice));

            _db.AddInParameter(dc, "AccountId", DbType.AnsiStringFixedLength, model.AccountId);
            _db.AddInParameter(dc, "AppKey", DbType.AnsiStringFixedLength, model.AppKey);
            _db.AddInParameter(dc, "AppSecret", DbType.AnsiStringFixedLength, model.AppSecret);
            _db.AddInParameter(dc, "Pwd", DbType.String, model.Pwd);
            _db.AddInParameter(dc, "Amount", DbType.Decimal, model.Amount);
            _db.AddInParameter(dc, "SysType", DbType.Byte, (int)model.SysType);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            if (DbHelper.ExecuteSql(dc, _db) > 0)
            {
                rModel = new MSmsAccountBase
                             {
                                 AccountId = model.AccountId,
                                 AppKey = model.AppKey,
                                 AppSecret = model.AppSecret,
                                 Amount = 0
                             };
            }

            return rModel;
        }

        /// <summary>
        /// 设置账户通道及单价信息
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="smsUnitPrice">单价信息集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public int SetSmsUnitPrice(string accountId, List<MSmsChannelInfo> smsUnitPrice)
        {
            if (string.IsNullOrEmpty(accountId) || smsUnitPrice == null || smsUnitPrice.Count < 1) return 0;

            var strSql = new StringBuilder();
            foreach (var t in smsUnitPrice)
            {
                if (t.Price <= 0) continue;
                strSql.AppendFormat(SqlSmsUnitPriceSet, accountId, (int)t.Cnannel, t.Price);
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 获取公司可用余额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <returns></returns>
        public MSmsAccountBase GetSmsAccount(string accountId, string appKey)
        {
            MSmsAccountBase model = null;
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey))
                return model;

            DbCommand dc = _db.GetSqlStringCommand(string.Format(" SELECT [AccountId],[AppKey],[AppSecret],[Amount] FROM [tbl_SmsAccount] where AccountId = '{0}' and AppKey = '{1}' ", accountId, appKey));

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new MSmsAccountBase();
                    if (!dr.IsDBNull(dr.GetOrdinal("AccountId")))
                        model.AccountId = dr.GetString(dr.GetOrdinal("AccountId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppKey")))
                        model.AppKey = dr.GetString(dr.GetOrdinal("AppKey"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppSecret")))
                        model.AppSecret = dr.GetString(dr.GetOrdinal("AppSecret"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        model.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                }
            }

            return model;
        }

        /// <summary>
        /// 获取短信账户全部信息
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <returns></returns>
        public MSmsAccount GetFullSmsAccount(string accountId, string appKey)
        {
            MSmsAccount model = null;
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey))
                return model;

            string strSql = " select [AccountId],[AppKey],[AppSecret],[Pwd],[Amount],[SysType],[IssueTime],(select Channel,UnitPrice from tbl_SmsUnitPrice where tbl_SmsUnitPrice.AccountId = tbl_SmsAccount.[AccountId] for xml raw, root('root')) as SmsUnitPrice from tbl_SmsAccount where [AccountId] = @AccountId and [AppKey] = @AppKey ";
            DbCommand dc = _db.GetSqlStringCommand(strSql);
            _db.AddInParameter(dc, "AccountId", DbType.AnsiStringFixedLength, accountId);
            _db.AddInParameter(dc, "AppKey", DbType.AnsiStringFixedLength, appKey);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new MSmsAccount();
                    if (!dr.IsDBNull(dr.GetOrdinal("AccountId")))
                        model.AccountId = dr.GetString(dr.GetOrdinal("AccountId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppKey")))
                        model.AppKey = dr.GetString(dr.GetOrdinal("AppKey"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AppSecret")))
                        model.AppSecret = dr.GetString(dr.GetOrdinal("AppSecret"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Pwd")))
                        model.Pwd = dr.GetString(dr.GetOrdinal("Pwd"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Amount")))
                        model.Amount = dr.GetDecimal(dr.GetOrdinal("Amount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SysType")))
                        model.SysType = (SystemType)dr.GetByte(dr.GetOrdinal("SysType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SmsUnitPrice")))
                        model.SmsUnitPrice = GetSmsUnitPrice(dr.GetString(dr.GetOrdinal("SmsUnitPrice")));
                }
            }

            return model;
        }

        /// <summary>
        /// 更新账户可以余额
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="appKey">appKey</param>
        /// <param name="isAdd">是否增加（T为增加，F为减少）</param>
        /// <param name="addMoney">变动金额</param>
        /// <returns>返回1成功，其他失败</returns>
        public int UpdateSmsAccountAmount(string accountId, string appKey, bool isAdd, decimal addMoney)
        {
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(appKey) || addMoney <= 0)
                return 0;

            var strSql = new StringBuilder(" update tbl_SmsAccount set [Amount] = [Amount] ");
            strSql.Append(isAdd ? " + " : " - ");
            strSql.Append(" @Amount where [AccountId] = @AccountId and [AppKey] = @AppKey ");

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(dc, "Amount", DbType.Decimal, addMoney);
            _db.AddInParameter(dc, "AccountId", DbType.AnsiStringFixedLength, accountId);
            _db.AddInParameter(dc, "AppKey", DbType.AnsiStringFixedLength, appKey);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 账户验证
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <returns></returns>
        public bool IsExists(EyouSoft.SmsWeb.Model.MSmsAccountBase account)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExists);
            _db.AddInParameter(cmd, "AccountId", DbType.AnsiStringFixedLength, account.AccountId);
            _db.AddInParameter(cmd, "AppKey", DbType.AnsiStringFixedLength, account.AppKey);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,_db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) == 1;
                }
            }

            return false;
        }
        
    }
}
