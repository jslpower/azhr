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

namespace EyouSoft.DAL.HGysStructure
{
    /// <summary>
    /// 领队、司机相关数据访问类
    /// </summary>
    public class DSiJi : DALBase, EyouSoft.IDAL.HGysStructure.ISiJi
    {
        #region static constants
        //static constants
        const string SQL_INSERT_Insert = "INSERT INTO [tbl_SourceLeadTeam]([Id],[CompanyId],[SouceType],[Name],[Sex],[Tel],[Mobile],[Address],[BackMark],[AssessType],[AssessContact],[IsDelete],[OperatorId],[IssueTime]) VALUES (@Id,@CompanyId,@SouceType,@Name,@Sex,@Tel,@Mobile,@Address,@BackMark,@AssessType,@AssessContact,@IsDelete,@OperatorId,@IssueTime)";
        const string SQL_UPDATE_Update = "UPDATE [tbl_SourceLeadTeam] SET [Name]=@Name,[Sex]=@Sex,[Tel]=@Tel,[Mobile]=@Mobile,[Address]=@Address,[BackMark]=@BackMark,[AssessType]=@AssessType,[AssessContact]=@AssessContact WHERE [Id]=@Id";
        const string SQL_UPDATE_Delete = "UPDATE [tbl_SourceLeadTeam] SET [IsDelete]='1' WHERE [Id]=@Id";
        const string SQL_SELECT_GetInfo = "SELECT * FROM [tbl_SourceLeadTeam] WHERE [Id]=@Id";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DSiJi()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region ISiJi 成员
        /// <summary>
        /// 新增领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MSiJiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_Insert);
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "SouceType", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Sex", DbType.Byte, info.Gender);
            _db.AddInParameter(cmd, "Tel", DbType.String, info.Telephone);
            _db.AddInParameter(cmd, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "AssessType", DbType.Byte, info.PingJiaLeiXing);
            _db.AddInParameter(cmd, "AssessContact", DbType.String, info.PingJiaNeiRong);
            _db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MSiJiInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Update);
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Sex", DbType.Byte, info.Gender);
            _db.AddInParameter(cmd, "Tel", DbType.String, info.Telephone);
            _db.AddInParameter(cmd, "Mobile", DbType.String, info.Mobile);
            _db.AddInParameter(cmd, "Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "AssessType", DbType.Byte, info.PingJiaLeiXing);
            _db.AddInParameter(cmd, "AssessContact", DbType.String, info.PingJiaNeiRong);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除领队、司机信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Delete);
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, gysId);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取领队、司机信息业务实体
        /// </summary>
        /// <param name="gysId">领队、司机编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MSiJiInfo GetInfo(string gysId)
        {
            EyouSoft.Model.HGysStructure.MSiJiInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MSiJiInfo();

                    item.Address = rdr["Address"].ToString();
                    item.BeiZhu = rdr["BackMark"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("Sex"));
                    item.GysId = gysId;
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.LeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)rdr.GetByte(rdr.GetOrdinal("SouceType"));
                    item.Mobile = rdr["Mobile"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.PingJiaLeiXing = (EyouSoft.Model.EnumType.GysStructure.SiJiPingJiaLeiXing)rdr.GetByte(rdr.GetOrdinal("AssessType"));
                    item.PingJiaNeiRong = rdr["AssessContact"].ToString();
                    item.Telephone = rdr["Tel"].ToString();

                }
            }

            return item;
        }

        /// <summary>
        /// 获取领队、司机信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MSiJiInfo> GetSiJis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MSiJiChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MSiJiInfo> items = new List<EyouSoft.Model.HGysStructure.MSiJiInfo>();

            string tableName = "tbl_SourceLeadTeam";
            string fields = "*";
            string orderByString = "IdentityId ASC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            #region sql
            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND SouceType={0} ", (int)chaXun.LeiXing.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.Name);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MSiJiInfo();

                    item.Address = rdr["Address"].ToString();
                    item.BeiZhu = rdr["BackMark"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)rdr.GetByte(rdr.GetOrdinal("Sex"));
                    item.GysId = rdr["Id"].ToString();
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.LeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)rdr.GetByte(rdr.GetOrdinal("SouceType"));
                    item.Mobile = rdr["Mobile"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.PingJiaLeiXing = (EyouSoft.Model.EnumType.GysStructure.SiJiPingJiaLeiXing)rdr.GetByte(rdr.GetOrdinal("AssessType"));
                    item.PingJiaNeiRong = rdr["AssessContact"].ToString();
                    item.Telephone = rdr["Tel"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        #endregion
    }
}
