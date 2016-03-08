using System;
using System.Collections.Generic;
using EyouSoft.Model.SmsStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit.DAL;
using System.Data.Common;
using System.Data;
using System.Text;

namespace EyouSoft.DAL.SmsStructure
{

    #region 短信常用短语数据访问

    /// <summary>
    /// 短信常用短语数据访问
    /// </summary>
    /// 周文超 2011-09-13
    public class DSmsPhrase : DALBase, IDAL.SmsStructure.ISmsPhrase
    {
        #region private member

        /// <summary>
        /// 数据库链接，构造函数初始化
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 添加短信常用短语Sql语句
        /// </summary>
        private const string SqlSmsPhraseAdd = @" INSERT INTO [tbl_SmsPhrase]
           ([CompanyId]
           ,[TypeId]
           ,[Content]
           ,[OperatorId]
           ,[IssueTime])
     VALUES
           (@CompanyId
           ,@TypeId
           ,@Content
           ,@OperatorId
           ,@IssueTime) ";

        /// <summary>
        /// 修改短信常用短语Sql语句
        /// </summary>
        private const string SqlSmsPhraseUpdate = @" UPDATE [tbl_SmsPhrase]
   SET [TypeId] = @TypeId
      ,[Content] = @Content
      ,[OperatorId] = @OperatorId
   WHERE Id = @Id ";

        /// <summary>
        /// 修改短信常用短语Sql语句(后面必须拼接条件)
        /// </summary>
        private const string SqlSmsPhraseDel = @" delete from [tbl_SmsPhrase] where  ";

        /// <summary>
        /// 查询短信常用短语Sql语句(不含类别名称)
        /// </summary>
        private const string SqlSmsPhraseSelect = @" SELECT [Id]
      ,[CompanyId]
      ,[TypeId]
      ,[Content]
      ,[OperatorId]
      ,[IssueTime]
  FROM [tbl_SmsPhrase] ";

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsPhrase()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 添加短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsPhrase(MSmsPhrase model)
        {
            if (model == null || model.SmsPhraseType == null)
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseAdd);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, model.SmsPhraseType.TypeId);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改短信常用短语
        /// </summary>
        /// <param name="model">常用短语实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int UpdateSmsPhrase(MSmsPhrase model)
        {
            if (model == null || model.SmsPhraseType == null || model.PhraseId <= 0)
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseUpdate);
            _db.AddInParameter(dc, "TypeId", DbType.Int32, model.SmsPhraseType.TypeId);
            _db.AddInParameter(dc, "Content", DbType.String, model.Content);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "Id", DbType.Int32, model.PhraseId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 删除短信常用短语
        /// </summary>
        /// <param name="phraseIds">短信常用短语编号</param>
        /// <returns>返回1成功；其他失败</returns>
        public int DelSmsPhrase(params int[] phraseIds)
        {
            if (phraseIds == null || phraseIds.Length < 1)
                return 0;

            DbCommand dc;
            if (phraseIds.Length == 1)
                dc = _db.GetSqlStringCommand(SqlSmsPhraseDel + string.Format(" Id = {0} ", phraseIds[0]));
            else
            {
                string strIds = GetIdsByArr(phraseIds);
                if (string.IsNullOrEmpty(strIds))
                    return 0;

                dc = _db.GetSqlStringCommand(SqlSmsPhraseDel + string.Format(" Id in ({0}) ", strIds));
            }

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 获取常用短语信息
        /// </summary>
        /// <param name="phraseId">常用短语编号</param>
        /// <returns>返回常用短语信息</returns>
        public MSmsPhrase GetSmsPhrase(int phraseId)
        {
            MSmsPhrase model = null;
            if (phraseId <= 0)
                return model;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseSelect + " where Id = @Id ");
            _db.AddInParameter(dc, "Id", DbType.Int32, phraseId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new MSmsPhrase { SmsPhraseType = new MSmsPhraseTypeBase() };
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.PhraseId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        model.SmsPhraseType.TypeId = dr.GetInt32(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        model.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }

            return model;
        }

        /// <summary>
        /// 获取短信常用短语列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">查询实体</param>
        /// <returns>返回常用短语信息集合</returns>
        public IList<MSmsPhrase> GetSmsPhraseList(int pageSize, int pageIndex, ref int recordCount, string companyId
                                                    , MQuerySmsPhrase model)
        {
            IList<MSmsPhrase> list = null;
            if (string.IsNullOrEmpty(companyId))
                return list;

            MSmsPhrase tmpModel;

            #region Sql拼接

            string fields = " [Id],[CompanyId],[TypeId],[Content],(select ClassName from tbl_SmsPhraseType where tbl_SmsPhraseType.Id = tbl_SmsPhrase.TypeId) as TypeName ";
            string tableName = "tbl_SmsPhrase";
            string orderByString = " [IssueTime] asc ";
            string identityColumnName = " Id ";
            var strWhere = new StringBuilder();
            strWhere.AppendFormat(" CompanyId = '{0}' ", companyId);
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.KeyWord.Trim()))
                    strWhere.AppendFormat(" and Content like '%{0}%' ", model.KeyWord.Trim());
                if (model.SmsPhraseType != null && model.SmsPhraseType.TypeId > 0)
                    strWhere.AppendFormat(" and TypeId = {0} ", model.SmsPhraseType.TypeId);
            }

            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName
                , identityColumnName, fields, strWhere.ToString(), orderByString))
            {
                list = new List<MSmsPhrase>();
                while (dr.Read())
                {
                    tmpModel = new MSmsPhrase { SmsPhraseType = new MSmsPhraseTypeBase() };
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        tmpModel.PhraseId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        tmpModel.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeId")))
                        tmpModel.SmsPhraseType.TypeId = dr.GetInt32(dr.GetOrdinal("TypeId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Content")))
                        tmpModel.Content = dr.GetString(dr.GetOrdinal("Content"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeName")))
                        tmpModel.SmsPhraseType.TypeName = dr.GetString(dr.GetOrdinal("TypeName"));

                    list.Add(tmpModel);
                }
            }

            return list;
        }
    }

    #endregion

    #region 短信常用短语信息类别数据访问

    /// <summary>
    /// 短信常用短语信息类别数据访问
    /// </summary>
    /// 周文超 2011-09-13
    public class DSmsPhraseType : DALBase, IDAL.SmsStructure.ISmsPhraseType
    {

        #region private member

        /// <summary>
        /// 数据库链接对象（构造函数实例化）
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 常用短语信息类别添加Sql
        /// </summary>
        private const string SqlSmsPhraseTypeAdd = @" INSERT INTO [tbl_SmsPhraseType]
           ([CompanyId]
           ,[ClassName]
           ,[OperatorId]
           ,[IssueTime])
     VALUES
           (@CompanyId
           ,@ClassName
           ,@OperatorId
           ,@IssueTime) ";

        /// <summary>
        /// 常用短语信息类别修改Sql
        /// </summary>
        private const string SqlSmsPhraseTypeUpdate = @" UPDATE [tbl_SmsPhraseType]
   SET [ClassName] = @ClassName
      ,[OperatorId] = @OperatorId
 WHERE Id = @Id ";

        /// <summary>
        /// 常用短语信息类别删除Sql
        /// </summary>
        private const string SqlSmsPhraseTypeDel = @" DELETE FROM [tbl_SmsPhraseType] WHERE ";

        /// <summary>
        /// 常用短语信息类别查询Sql
        /// </summary>
        private const string SqlSmsPhraseTypeSelect = @" SELECT [Id]
      ,[CompanyId]
      ,[ClassName]
      ,[OperatorId]
      ,[IssueTime]
  FROM [tbl_SmsPhraseType] ";

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSmsPhraseType()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 添加常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int AddSmsPhraseType(MSmsPhraseType model)
        {
            if (model == null || string.IsNullOrEmpty(model.CompanyId))
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeAdd);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(dc, "ClassName", DbType.String, model.TypeName);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 修改常用短语信息类别
        /// </summary>
        /// <param name="model">常用短语信息类别实体</param>
        /// <returns>返回1成功；其他失败</returns>
        public int UpdateSmsPhraseType(MSmsPhraseType model)
        {
            if (model == null || model.TypeId <= 0)
                return 0;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeUpdate);
            _db.AddInParameter(dc, "ClassName", DbType.String, model.TypeName);
            _db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, model.TypeId);

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 删除常用短语信息类别
        /// </summary>
        /// <param name="typeIds">常用短语信息类别编号</param>
        /// <returns>返回1成功；其他失败</returns>
        public int DelSmsPhraseType(params int[] typeIds)
        {
            if (typeIds == null || typeIds.Length < 1)
                return 0;

            DbCommand dc;
            if (typeIds.Length == 1)
                dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeDel + string.Format(" Id = {0} ", typeIds[0]));
            else
            {
                string strIds = GetIdsByArr(typeIds);
                if (string.IsNullOrEmpty(strIds))
                    return 0;

                dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeDel + string.Format(" Id in ({0}) ", strIds));
            }

            return DbHelper.ExecuteSql(dc, _db) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 获取常用短语信息类别
        /// </summary>
        /// <param name="typeId">常用短语信息类别编号</param>
        /// <returns>返回常用短语信息类别实体</returns>
        public MSmsPhraseType GetSmsPhraseType(int typeId)
        {
            MSmsPhraseType model = null;
            if (typeId <= 0)
                return model;

            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeSelect + " where Id = @Id ");
            _db.AddInParameter(dc, "Id", DbType.Int32, typeId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new MSmsPhraseType();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.TypeId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClassName")))
                        model.TypeName = dr.GetString(dr.GetOrdinal("ClassName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OperatorId")))
                        model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                }
            }

            return model;
        }


        /// <summary>
        /// 获取常用短语信息类别列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>返回常用短语信息类别集合</returns>
        public IList<MSmsPhraseType> GetSmsPhraseTypeList(string companyId)
        {
            IList<MSmsPhraseType> list = null;
            if (string.IsNullOrEmpty(companyId))
                return list;

            MSmsPhraseType model;
            DbCommand dc = _db.GetSqlStringCommand(SqlSmsPhraseTypeSelect + " where CompanyId = @CompanyId ");
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<MSmsPhraseType>();
                while (dr.Read())
                {
                    model = new MSmsPhraseType();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.TypeId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CompanyId")))
                        model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ClassName")))
                        model.TypeName = dr.GetString(dr.GetOrdinal("ClassName"));
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

    #endregion
}
