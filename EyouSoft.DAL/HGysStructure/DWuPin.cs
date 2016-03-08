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
    /// 物品管理相关数据访问类
    /// </summary>
    public class DWuPin : DALBase, EyouSoft.IDAL.HGysStructure.IWuPin
    {
        #region static constants
        //static constants
        const string SQL_INSERT_Insert = "INSERT INTO [tbl_GovGood]([GoodId],[CompanyId],[GoodName],[Number],[Price],[GoodUse],[InTime],[Remark],[OperatorId],[IssueTime]) VALUES (@GoodId,@CompanyId,@GoodName,@Number,@Price,@GoodUse,@InTime,@Remark,@OperatorId,@IssueTime)";
        const string SQL_UPDATE_Update = "UPDATE [tbl_GovGood] SET [GoodName]=@GoodName,[Number]=@Number,[Price]=@Price,[GoodUse]=@GoodUse,[InTime]=@InTime,[Remark]=@Remark WHERE [GoodId]=@GoodId";
        const string SQL_UPDATE_Delete = "UPDATE [tbl_GovGood] SET [IsDelete]='1' WHERE [GoodId]=@GoodId";
        const string SQL_SELECT_GetInfo = "SELECT A.*,(SELECT A1.ContactName FROM [tbl_ComUser] AS A1 WHERE A1.UserId=A.OperatorId) AS OperatorName FROM [tbl_GovGood] AS A WHERE A.[GoodId]=@GoodId";
        const string SQL_INSERT_InsertLingYong = "INSERT INTO [tbl_GovGoodUse]([UseId],[GoodId],[UseType],[UseTime],[Number],[UserId],[GoodUse],[OperatorId],[IssueTime],[JieYueStatus],[ReturnTime],[PlanId],[GuiHuanOperatorId],[TourId]) VALUES (@UseId,@GoodId,@UseType,@UseTime,@Number,@UserId,@GoodUse,@OperatorId,@IssueTime,@JieYueStatus,@ReturnTime,@PlanId,@GuiHuanOperatorId,@TourId)";
        const string SQL_SELECT_GetLingYongs = "SELECT A.*,B.Price,(SELECT A1.TourCode FROM [tbl_Tour] AS A1 WHERE A1.TourId=A.TourId) AS TourCode,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=A.UserId) AS LingYongRenName FROM [tbl_GovGoodUse] AS A INNER JOIN [tbl_GovGood] AS B ON A.[GoodId]=B.[GoodId] WHERE A.[GoodId]=@GoodId AND A.[UseType]=@UseType ORDER BY A.[IssueTime] DESC";
        const string SQL_UPDATE_GuiHuan = "UPDATE [tbl_GovGoodUse] SET [JieYueStatus]=@JieYueStatus,[ReturnTime]=@GuiHuanTime,[GuiHuanOperatorId]=@GuiHuanOperatorId WHERE [UseId]=@UseId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DWuPin()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region IWuPin 成员
        /// <summary>
        /// 新增物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MWuPinInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_Insert);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, info.WuPinId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "GoodName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Number", DbType.Int32, info.ShuLiangRK);
            _db.AddInParameter(cmd, "Price", DbType.Decimal, info.DanJia);
            _db.AddInParameter(cmd, "GoodUse", DbType.String, info.YongTu);
            _db.AddInParameter(cmd, "InTime", DbType.DateTime, info.RuKuTime);
            _db.AddInParameter(cmd, "Remark", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MWuPinInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Update);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, info.WuPinId);
            _db.AddInParameter(cmd, "GoodName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "Number", DbType.Int32, info.ShuLiangRK);
            _db.AddInParameter(cmd, "Price", DbType.Decimal, info.DanJia);
            _db.AddInParameter(cmd, "GoodUse", DbType.String, info.YongTu);
            _db.AddInParameter(cmd, "InTime", DbType.DateTime, info.RuKuTime);
            _db.AddInParameter(cmd, "Remark", DbType.String, info.BeiZhu);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除物品信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string wuPinId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_Delete);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, wuPinId);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取物品信息实体
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MWuPinInfo GetInfo(string wuPinId)
        {
            EyouSoft.Model.HGysStructure.MWuPinInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, wuPinId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MWuPinInfo();

                    item.BeiZhu = rdr["Remark"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DanJia = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.Name = rdr["GoodName"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.OperatorName = rdr["OperatorName"].ToString();
                    item.RuKuTime = rdr.GetDateTime(rdr.GetOrdinal("InTime"));
                    item.ShuLiang = null;
                    item.ShuLiangRK = rdr.GetInt32(rdr.GetOrdinal("Number"));
                    item.WuPinId = wuPinId;
                    item.YongTu = rdr["GoodUse"].ToString();
                }
            }

            return item;
        }

        /// <summary>
        /// 获取物品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MWuPinInfo> GetWuPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MWuPinChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MWuPinInfo> items = new List<EyouSoft.Model.HGysStructure.MWuPinInfo>();

            string tableName = "tbl_GovGood";
            string fields = "*,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=tbl_GovGood.OperatorId) AS OperatorName";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            #region sql
            if (chaXun != null)
            {
                if (chaXun.ETime.HasValue)
                {
                    sql.AppendFormat(" AND InTime<='{0}' ", chaXun.ETime.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.Name))
                {
                    sql.AppendFormat(" AND GoodName LIKE '%{0}%' ", chaXun.Name);
                }
                if (chaXun.STime.HasValue)
                {
                    sql.AppendFormat(" AND InTime>='{0}' ", chaXun.STime.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MWuPinInfo();

                    item.BeiZhu = rdr["Remark"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DanJia = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.Name = rdr["GoodName"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.OperatorName = rdr["OperatorName"].ToString();
                    item.RuKuTime = rdr.GetDateTime(rdr.GetOrdinal("InTime"));
                    item.ShuLiang = null;
                    item.ShuLiangRK = rdr.GetInt32(rdr.GetOrdinal("Number"));
                    item.WuPinId = rdr["GoodId"].ToString();
                    item.YongTu = rdr["GoodUse"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取物品数量信息
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">不含的领用编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo GetShuLiangInfo(string wuPinId, string lingYongId)
        {
            var info = new EyouSoft.Model.HGysStructure.MWuPinShuLiangInfo();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT A.Number AS RuKu ");
            sql.Append(" ,(SELECT SUM(Number) FROM [tbl_GovGoodUse] AS A1 WHERE A1.GoodId=@GoodId AND A1.UseType=0 {0}) AS LingYong ");
            sql.Append(" ,(SELECT SUM(Number) FROM [tbl_GovGoodUse] AS A1 WHERE A1.GoodId=@GoodId AND A1.UseType=1 {0}) AS FaFang  ");
            sql.Append(" ,(SELECT SUM(Number) FROM [tbl_GovGoodUse] AS A1 WHERE A1.GoodId=@GoodId AND A1.UseType=2 AND JieYueStatus=0 {0}) AS JieYue1  ");
            sql.Append(" ,(SELECT SUM(Number) FROM [tbl_GovGoodUse] AS A1 WHERE A1.GoodId=@GoodId AND A1.UseType=2 AND JieYueStatus=1 {0}) AS JieYue2 ");
            sql.Append(" FROM [tbl_GovGood] AS A WHERE A.GoodId=@GoodId ");

            string s = string.Empty;

            if (!string.IsNullOrEmpty(lingYongId))
            {
                s = " AND A1.UseId<>@LingYongId ";
            }

            DbCommand cmd = _db.GetSqlStringCommand(string.Format(sql.ToString(), s));
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, wuPinId);
            _db.AddInParameter(cmd, "LingYongId", DbType.AnsiStringFixedLength, lingYongId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RuKu"))) info.RuKu = rdr.GetInt32(rdr.GetOrdinal("RuKu"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LingYong"))) info.LingYong = rdr.GetInt32(rdr.GetOrdinal("LingYong"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("FaFang"))) info.FaFang = rdr.GetInt32(rdr.GetOrdinal("FaFang"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JieYue1"))) info.JieYue1 = rdr.GetInt32(rdr.GetOrdinal("JieYue1"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JieYue2"))) info.JieYue2 = rdr.GetInt32(rdr.GetOrdinal("JieYue2"));
                }
            }

            return info;
        }

        /// <summary>
        /// 新增领用信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertLingYong(EyouSoft.Model.HGysStructure.MWuPinLingYongInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertLingYong);
            _db.AddInParameter(cmd, "UseId", DbType.AnsiStringFixedLength, info.LingYongId);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, info.WuPinId);
            _db.AddInParameter(cmd, "UseType", DbType.Byte, info.LingYongLeiXing);
            _db.AddInParameter(cmd, "UseTime", DbType.DateTime, info.ShiJian);
            _db.AddInParameter(cmd, "Number", DbType.Int32, info.ShuLiang);
            _db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, info.LingYongRenId);
            _db.AddInParameter(cmd, "GoodUse", DbType.String, info.YongTu);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "JieYueStatus", DbType.Byte, EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus.借阅中);
            _db.AddInParameter(cmd, "ReturnTime", DbType.DateTime, DBNull.Value);
            _db.AddInParameter(cmd, "PlanId", DbType.AnsiStringFixedLength, DBNull.Value);
            _db.AddInParameter(cmd, "GuiHuanOperatorId", DbType.AnsiStringFixedLength, DBNull.Value);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, DBNull.Value);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取领用信息集合
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="leiXing">领用类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo> GetLingYongs(string wuPinId, EyouSoft.Model.EnumType.GysStructure.WuPinLingYongLeiXing leiXing)
        {
            IList<EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo> items = new List<EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetLingYongs);
            _db.AddInParameter(cmd, "GoodId", DbType.AnsiStringFixedLength, wuPinId);
            _db.AddInParameter(cmd, "UseType", DbType.Byte, leiXing);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MWuPinLingYongLBInfo();
                    item.DanJia = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    item.JieYueStatus = (EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus)rdr.GetByte(rdr.GetOrdinal("JieYueStatus"));
                    item.LingYongId = rdr["UseId"].ToString();
                    item.LingYongRenId = rdr["UserId"].ToString();
                    item.LingYongRenName = rdr["LingYongRenName"].ToString();
                    item.ShiJian = rdr.GetDateTime(rdr.GetOrdinal("UseTime"));
                    item.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("Number"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.YongTu = rdr["GoodUse"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 物品归还，返回1成功，其它失败
        /// </summary>
        /// <param name="wuPinId">物品编号</param>
        /// <param name="lingYongId">领用编号</param>
        /// <param name="guiHuanOperatorId">归还操作人编号</param>
        /// <param name="guiHuanTime">归还时间</param>
        /// <returns></returns>
        public int GuiHuan(string wuPinId, string lingYongId, string guiHuanOperatorId, DateTime guiHuanTime)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_GuiHuan);
            _db.AddInParameter(cmd, "JieYueStatus", DbType.Byte, EyouSoft.Model.EnumType.GysStructure.WuPinJieYueStatus.已归还);
            _db.AddInParameter(cmd, "GuiHuanTime", DbType.DateTime, guiHuanTime);
            _db.AddInParameter(cmd, "GuiHuanOperatorId", DbType.AnsiStringFixedLength, guiHuanOperatorId);
            _db.AddInParameter(cmd, "UseId", DbType.AnsiStringFixedLength, lingYongId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        #endregion
    }
}
