using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.SmsStructure;
using EyouSoft.Model.SmsStructure;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace EyouSoft.DAL.SmsStructure
{
    /// <summary>
    /// 短信配置数据接口
    /// </summary>
    /// 周文超 2011-09-14
    public class DSmsSetting : DALBase, IDAL.SmsStructure.ISmsSetting
    {
        #region private member

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 设置是否启用的Sql
        /// </summary>
        private const string SqlIsEnabledSet = @" update tbl_SmsSetting set [IsEnable] = @IsEnable where CompanyId = @CompanyId and [Type] = @Type ";

        /// <summary>
        /// 设置当天是否已发送的Sql
        /// </summary>
        private const string SqlIsSendSet = @" update tbl_SmsSetting set [IsSend] = @IsSend where CompanyId = @CompanyId and [Type] = @Type ";

        /// <summary>
        /// 设置短信配置信息的Sql
        /// </summary>
        private const string SqlSmsSettingUpdate = @" if exists (select 1 from tbl_SmsSetting where CompanyId = @CompanyId and [Type] = @Type)
	update tbl_SmsSetting set 
	[Message] = @Message
	,[BeforeDay] = @BeforeDay
	,[Hour] = @Hour
	,[OperatorId] = @OperatorId
    ,IsEnable=@IsEnable 
	where CompanyId = @CompanyId and [Type] = @Type
else
	insert into tbl_SmsSetting (
	CompanyId
	,[Type]
	,[Message]
	,[BeforeDay]
	,[Hour]
	,[IsEnable]
	,[IsSend]
	,[OperatorId]
	,[IssueTime]
	)
	values (
	@CompanyId
	,@Type
	,@Message
	,@BeforeDay
	,@Hour
	,@IsEnable
	,@IsSend
	,@OperatorId
	,getdate()
	) ";

        #endregion

        public DSmsSetting()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 设置公司短信配置信息
        /// </summary>
        /// <param name="model">短信配置实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int SetSmsSetting(MSmsSetting model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsSettingUpdate);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)model.Type);
            _db.AddInParameter(dc, "Message", DbType.String, model.Message);
            _db.AddInParameter(dc, "BeforeDay", DbType.Int32, model.BeforeDay);
            _db.AddInParameter(dc, "Hour", DbType.Int32, model.Hour);
            _db.AddInParameter(dc, "IsEnable", DbType.AnsiStringFixedLength, model.IsEnabled ? "1" : "0");
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, "0");//当天是否已发送直接写0值，没有发送
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isEnabled">是否启用</param>
        /// <returns></returns>
        public int SetIsEnabled(string companyId, SettingType type, bool isEnabled)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlIsEnabledSet);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)type);
            _db.AddInParameter(dc, "IsEnable", DbType.AnsiStringFixedLength, isEnabled ? "1" : "0");

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 设置当天是否已发送
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">配置类型</param>
        /// <param name="isSend">当天是否已发送</param>
        /// <returns></returns>
        public int SetIsSend(string companyId, SettingType type, bool isSend)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlIsSendSet);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(dc, "Type", DbType.Byte, (int)type);
            _db.AddInParameter(dc, "IsSend", DbType.AnsiStringFixedLength, isSend ? "1" : "0");

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 获取公司短信配置信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="types">配置类型集合</param>
        /// <returns>公司短信配置信息集合</returns>
        public IList<MSmsSetting> GetSmsSetting(string companyId, params SettingType[] types)
        {
            IList<MSmsSetting> list = null;
            if (string.IsNullOrEmpty(companyId))
                return list;

            MSmsSetting model;
            var strSql = new StringBuilder(" select CompanyId,[Type],[Message],[BeforeDay],[Hour],[IsEnable],[IsSend],[OperatorId],[IssueTime] from tbl_SmsSetting ");
            strSql.AppendFormat(" where CompanyId = '{0}' ", companyId);
            if (types != null)
            {
                if (types.Length == 1)
                    strSql.AppendFormat(" and [Type] = {0} ", (int)types[0]);
                else
                {
                    string strIds = types.Aggregate(string.Empty, (current, t) => current + ((int) t + ","));
                    strSql.AppendFormat(" and [Type] in ({0}) ", strIds.Trim(','));
                }
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc,_db))
            {
                list = new List<MSmsSetting>();
                while (dr.Read())
                {
                    model = new MSmsSetting();
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Type")))
                        model.Type = (SettingType)dr.GetByte(dr.GetOrdinal("Type"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Message")))
                        model.Message = dr.GetString(dr.GetOrdinal("Message"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BeforeDay")))
                        model.BeforeDay = dr.GetInt32(dr.GetOrdinal("BeforeDay"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Hour")))
                        model.Hour = dr.GetInt32(dr.GetOrdinal("Hour"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsEnable")) && dr.GetString(dr.GetOrdinal("IsEnable")) == "1")
                        model.IsEnabled = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("IsSend")) && dr.GetString(dr.GetOrdinal("IsSend")) == "1")
                        model.IsSend = true;
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    list.Add(model);
                }
            }

            return list;
        }
    }
}
