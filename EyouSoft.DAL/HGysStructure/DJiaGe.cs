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
    /// 供应商价格相关数据访问类接口
    /// </summary>
    public class DJiaGe : DALBase, EyouSoft.IDAL.HGysStructure.IJiaGe
    {
        #region static constants
        //static constants
        const string SQL_INSERT_InsertCheXingJiaGe = "INSERT INTO [tbl_SourceCarPrice]([JiaGeId],[CheXingId],[CustomType],[Stime],[Etime],[Price],[IsDelete],[OperatorId],[IssueTime]) VALUES (@JiaGeId,@CheXingId,@CustomType,@Stime,@Etime,@Price,@IsDelete,@OperatorId,@IssueTime)";
        const string SQL_UPDATE_UpdateCheXingJiaGe = "UPDATE [tbl_SourceCarPrice] SET [CustomType]=@CustomType,[Stime]=@Stime,[Etime]=@Etime,[Price]=@Price WHERE [JiaGeId]=@JiaGeId";
        const string SQL_UPDATE_DeleteCheXingJiaGe = "UPDATE [tbl_SourceCarPrice] SET [IsDelete]='1' WHERE [JiaGeId]=@JiaGeId";
        const string SQL_SELECT_GetCheXingJiaGes = "SELECT * FROM [tbl_SourceCarPrice] WHERE [CheXingId]=@CheXingId AND [IsDelete]='0' ORDER BY [IdentityId] DESC";

        const string SQL_INSERT_InsertJingDianJiaGe = "INSERT INTO [tbl_SourceJingDianJiaGe]([JiaGeId],[JingDianId],[CustomType],[Stime],[Etime],[MSprice],[THprice],[JSprice],[TourType],[IsDelete],[OperatorId],[IssueTime],ETprice,JTprice) VALUES (@JiaGeId,@JingDianId,@CustomType,@Stime,@Etime,@MSprice,@THprice,@JSprice,@TourType,@IsDelete,@OperatorId,@IssueTime,@ETprice,@JTprice)";
        const string SQL_UPDATE_UpdateJingDianJiaGe = "UPDATE [tbl_SourceJingDianJiaGe] SET [CustomType]=@CustomType,[Stime]=@Stime,[Etime]=@Etime,[MSprice]=@MSprice,[THprice]=@THprice,[JSprice]=@JSprice,[TourType]=@TourType,ETprice=@ETprice,JTprice=@JTprice WHERE [JiaGeId]=@JiaGeId";
        const string SQL_UPDATE_DeleteJingDianJiaGe = "UPDATE [tbl_SourceJingDianJiaGe] SET [IsDelete]='1' WHERE [JiaGeId]=@JiaGeId";
        //const string SQL_SELECT_GetJingDianJiaGes = "SELECT * FROM [tbl_SourceJingDianJiaGe] WHERE [JingDianId]=@JingDianId AND [IsDelete]='0' ORDER BY [IdentityId] DESC";

        const string SQL_INSERT_InsertCanGuanCaiDan = "INSERT INTO [tbl_SourceDiningMenu]([MenuId],[SourceId],[MenuName],[MenuContact],[ZRetailPrice],[ZTradePrice],[ZSettlementPrice],[RRetailPrice],[RTradePrice],[RSettlementPrice],[IsDelete],[OperatorId],[IssueTime],[LngType],TMianM,TMianN,IsDisplay) VALUES (@MenuId,@SourceId,@MenuName,@MenuContact,@ZRetailPrice,@ZTradePrice,@ZSettlementPrice,@RRetailPrice,@RTradePrice,@RSettlementPrice,@IsDelete,@OperatorId,@IssueTime,@LngType,@TMianM,@TMianN,@IsDisplay)";
        const string SQL_UPDATE_UpdateCanGuanCaiDan = "UPDATE [tbl_SourceDiningMenu] SET [MenuName]=@MenuName,[MenuContact]=@MenuContact,[ZRetailPrice]=@ZRetailPrice,[ZTradePrice]=@ZTradePrice,[ZSettlementPrice]=@ZSettlementPrice,[RRetailPrice]=@RRetailPrice,[RTradePrice]=@RTradePrice,[RSettlementPrice]=@RSettlementPrice,TMianM=@TMianM,TMianN=@TMianN,IsDisplay=@IsDisplay WHERE [MenuId]=@MenuId AND [LngType]=@LngType";
        const string SQL_UPDATE_DeleteCanGuanCaiDan = "UPDATE [tbl_SourceDiningMenu] SET [IsDelete]='1' WHERE [MenuId]=@MenuId";
        const string SQL_SELECT_GetCanGuanCaiDans = "SELECT * FROM [tbl_SourceDiningMenu] WHERE [SourceId]=@GysId AND [IsDelete]='0' AND [LngType]=@LngType ORDER BY [IdentityId] DESC";
        const string SQL_SELECT_GetCanGuanCaiDan = "SELECT * FROM [tbl_SourceDiningMenu] WHERE [SourceId]=@GysId AND [IsDelete]='0' AND IsDisplay=0 AND [LngType]=@LngType ORDER BY [IdentityId] DESC";
        const string SQL_SELECT_GetCanGuanCaiDanInfo = "SELECT * FROM [tbl_SourceDiningMenu] WHERE [MenuId]=@MenuId AND [LngType]=@LngType";

        const string SQL_UPDATE_DeleteJiuDianJiaGe = "UPDATE [tbl_SourceJiuDianJiaGe] SET [IsDelete]='1' WHERE [JiaGeId]=@JiaGeId";
        const string SQL_SELECT_GetJiuDianJiaGes = "SELECT *,(SELECT A1.TypeName FROM tbl_ComRoomTypeManage AS A1 WHERE A1.RoomId=A.FangXingId) AS FangXingName FROM [tbl_SourceJiuDianJiaGe] AS A WHERE A.[GysId]=@GysId AND A.[IsDelete]='0' ORDER BY A.[IdentityId] DESC";
        const string SQL_SELECT_GetJiuDianJiaGeInfo = "SELECT * FROM [tbl_SourceJiuDianJiaGe] WHERE [JiaGeId]=@JiaGeId";
        const string SQL_SELECT_GetJiuDianJiaGeXingQi = "SELECT [XingQi] FROM [tbl_SourceJiuDianJiaGeXingQi] WHERE [JiaGeId]=@JiaGeId ORDER BY [XingQi] ASC";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DJiaGe()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// get jiudian jiage xingqi
        /// </summary>
        /// <param name="jiaGeId"></param>
        /// <returns></returns>
        IList<DayOfWeek> GetJiuDianJiaGeXingQi(string jiaGeId)
        {
            IList<DayOfWeek> items = new List<DayOfWeek>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiuDianJiaGeXingQi);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, jiaGeId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    items.Add((DayOfWeek)rdr.GetByte(0));
                }
            }
            return items;
        }
        #endregion

        #region IJiaGe 成员
        /// <summary>
        /// 新增车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertCheXingJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "CheXingId", DbType.AnsiStringFixedLength, info.CheXingId);
            _db.AddInParameter(cmd, "CustomType", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "Stime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "Etime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "Price", DbType.Decimal, info.JiaGeJS);
            _db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCheXingJiaGe(EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateCheXingJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "CustomType", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "Stime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "Etime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "Price", DbType.Decimal, info.JiaGeJS);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除车型价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="cheXingId">车型编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteCheXingJiaGe(string companyId, string gysId, string cheXingId, string jiaGeId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_DeleteCheXingJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, jiaGeId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取车型价格信息集合
        /// </summary>
        /// <param name="cheXingId">车型编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo> GetCheXingJiaGes(string cheXingId)
        {
            IList<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo> items = new List<EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCheXingJiaGes);
            _db.AddInParameter(cmd, "CheXingId", DbType.AnsiStringFixedLength, cheXingId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MCheXingJiaGeInfo();
                    item.BinKeLeiXing = (EyouSoft.Model.EnumType.CrmStructure.CustomType)rdr.GetByte(rdr.GetOrdinal("CustomType"));
                    item.CheXingId = rdr["CheXingId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Etime"))) item.ETime = rdr.GetDateTime(rdr.GetOrdinal("Etime"));
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.JiaGeJS = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    item.OperatorId = rdr["OperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Stime"))) item.STime = rdr.GetDateTime(rdr.GetOrdinal("Stime"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertJingDianJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "JingDianId", DbType.AnsiStringFixedLength, info.JingDianId);
            _db.AddInParameter(cmd, "CustomType", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "Stime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "Etime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "MSprice", DbType.Decimal, info.JiaGeMS);
            _db.AddInParameter(cmd, "THprice", DbType.Decimal, info.JiaGeTH);
            _db.AddInParameter(cmd, "JSprice", DbType.Decimal, info.JiaGeJS);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, info.TuanXing);
            _db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "ETprice", DbType.Decimal, info.JiaGeET);
            _db.AddInParameter(cmd, "JTprice", DbType.Decimal, info.JiaGeJT);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateJingDianJiaGe(EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateJingDianJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "CustomType", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "Stime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "Etime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "MSprice", DbType.Decimal, info.JiaGeMS);
            _db.AddInParameter(cmd, "THprice", DbType.Decimal, info.JiaGeTH);
            _db.AddInParameter(cmd, "JSprice", DbType.Decimal, info.JiaGeJS);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, info.TuanXing);
            _db.AddInParameter(cmd, "ETprice", DbType.Decimal, info.JiaGeET);
            _db.AddInParameter(cmd, "JTprice", DbType.Decimal, info.JiaGeJT);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除景点价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteJingDianJiaGe(string companyId, string gysId, string jingDianId, string jiaGeId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_DeleteJingDianJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, jiaGeId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取景点价格信息集合
        /// </summary>
        /// <param name="jingDianId">景点编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> GetJingDianJiaGes(string jingDianId, EyouSoft.Model.HGysStructure.MJiaGeChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo> items = new List<EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM [tbl_SourceJingDianJiaGe] WHERE [JingDianId]=@JingDianId AND [IsDelete]='0' ");

            if (chaXun != null)
            {
                if (chaXun.BinKeLeiXing.HasValue)
                {
                    sql.AppendFormat(" AND CustomType={0} ", (int)chaXun.BinKeLeiXing.Value);
                }
                if (chaXun.TuanXing.HasValue)
                {
                    sql.AppendFormat(" AND TourType={0} ", (int)chaXun.TuanXing.Value);
                }
                if (chaXun.Time1.HasValue || chaXun.Time2.HasValue)
                {
                    sql.AppendFormat(" AND(1=0 ");
                    if (chaXun.Time1.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN STime AND ETime)  ", chaXun.Time1.Value);
                    }
                    if (chaXun.Time2.HasValue)
                    {
                        sql.AppendFormat(" OR ('{0}' BETWEEN STime AND ETime)  ", chaXun.Time2.Value);
                    }
                    sql.AppendFormat(" ) ");
                }
            }

            sql.Append(" ORDER BY [IdentityId] DESC ");
            //DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJingDianJiaGes);
            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "JingDianId", DbType.AnsiStringFixedLength, jingDianId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MJingDianJiaGeInfo();

                    item.BinKeLeiXing = (EyouSoft.Model.EnumType.CrmStructure.CustomType)rdr.GetByte(rdr.GetOrdinal("CustomType"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Etime"))) item.ETime = rdr.GetDateTime(rdr.GetOrdinal("Etime"));
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.JiaGeJS = rdr.GetDecimal(rdr.GetOrdinal("JSprice"));
                    item.JiaGeMS = rdr.GetDecimal(rdr.GetOrdinal("MSprice"));
                    item.JiaGeTH = rdr.GetDecimal(rdr.GetOrdinal("THprice"));
                    item.JiaGeET = rdr.GetDecimal(rdr.GetOrdinal("ETprice"));
                    item.JiaGeJT = rdr.GetDecimal(rdr.GetOrdinal("JTprice"));
                    item.JingDianId = rdr["JingDianId"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Stime"))) item.STime = rdr.GetDateTime(rdr.GetOrdinal("Stime"));
                    item.TuanXing = (EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaTuanXing)rdr.GetByte(rdr.GetOrdinal("TourType"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 新增餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertCanGuanCaiDan);
            _db.AddInParameter(cmd, "MenuId", DbType.AnsiStringFixedLength, info.CaiDanId);
            _db.AddInParameter(cmd, "SourceId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "MenuName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "MenuContact", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "ZRetailPrice", DbType.Decimal, info.JiaGeZMS);
            _db.AddInParameter(cmd, "ZTradePrice", DbType.Decimal, info.JiaGeZTH);
            _db.AddInParameter(cmd, "ZSettlementPrice", DbType.Decimal, info.JiaGeZJS);
            _db.AddInParameter(cmd, "RRetailPrice", DbType.Decimal, info.JiaGeRMS);
            _db.AddInParameter(cmd, "RTradePrice", DbType.Decimal, info.JiaGeRTH);
            _db.AddInParameter(cmd, "RSettlementPrice", DbType.Decimal, info.JiaGeRJS);
            _db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, info.LngType);
            _db.AddInParameter(cmd, "TMianM", DbType.Int32, info.TMianM);
            _db.AddInParameter(cmd, "TMianN", DbType.Int32, info.TMianN);
            _db.AddInParameter(cmd, "IsDisplay", DbType.AnsiStringFixedLength, info.IsDisplay?"1":"0");

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 修改餐馆菜单信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCanGuanCaiDan(EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateCanGuanCaiDan);
            _db.AddInParameter(cmd, "MenuId", DbType.AnsiStringFixedLength, info.CaiDanId);
            _db.AddInParameter(cmd, "MenuName", DbType.String, info.Name);
            _db.AddInParameter(cmd, "MenuContact", DbType.String, info.NeiRong);
            _db.AddInParameter(cmd, "ZRetailPrice", DbType.Decimal, info.JiaGeZMS);
            _db.AddInParameter(cmd, "ZTradePrice", DbType.Decimal, info.JiaGeZTH);
            _db.AddInParameter(cmd, "ZSettlementPrice", DbType.Decimal, info.JiaGeZJS);
            _db.AddInParameter(cmd, "RRetailPrice", DbType.Decimal, info.JiaGeRMS);
            _db.AddInParameter(cmd, "RTradePrice", DbType.Decimal, info.JiaGeRTH);
            _db.AddInParameter(cmd, "RSettlementPrice", DbType.Decimal, info.JiaGeRJS);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, info.LngType);
            _db.AddInParameter(cmd, "TMianM", DbType.Int32, info.TMianM);
            _db.AddInParameter(cmd, "TMianN", DbType.Int32, info.TMianN);
            _db.AddInParameter(cmd, "IsDisplay", DbType.AnsiStringFixedLength, info.IsDisplay ? "1" : "0");

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除餐馆菜单信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="caiDanId">菜单编号</param>
        /// <returns></returns>
        public int DeleteCanGuanCaiDan(string companyId, string gysId, string caiDanId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_DeleteCanGuanCaiDan);
            _db.AddInParameter(cmd, "MenuId", DbType.AnsiStringFixedLength, caiDanId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取餐馆菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDans(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> items = new List<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCanGuanCaiDans);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, LngType);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo();
                    item.CaiDanId = rdr["MenuId"].ToString();
                    item.GysId = rdr["SourceId"].ToString();
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeRJS = rdr.GetDecimal(rdr.GetOrdinal("RSettlementPrice"));
                    item.JiaGeRMS = rdr.GetDecimal(rdr.GetOrdinal("RRetailPrice"));
                    item.JiaGeRTH = rdr.GetDecimal(rdr.GetOrdinal("RTradePrice"));
                    item.JiaGeZJS = rdr.GetDecimal(rdr.GetOrdinal("ZSettlementPrice"));
                    item.JiaGeZMS = rdr.GetDecimal(rdr.GetOrdinal("ZRetailPrice"));
                    item.JiaGeZTH = rdr.GetDecimal(rdr.GetOrdinal("ZTradePrice"));
                    item.Name = rdr["MenuName"].ToString();
                    item.NeiRong = rdr["MenuContact"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.IsDisplay = rdr["IsDisplay"].ToString() == "1";
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取餐馆可用菜单信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="LngType">语种类型</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> GetCanGuanCaiDan(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            IList<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo> items = new List<EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCanGuanCaiDan);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, LngType);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo();
                    item.CaiDanId = rdr["MenuId"].ToString();
                    item.GysId = rdr["SourceId"].ToString();
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeRJS = rdr.GetDecimal(rdr.GetOrdinal("RSettlementPrice"));
                    item.JiaGeRMS = rdr.GetDecimal(rdr.GetOrdinal("RRetailPrice"));
                    item.JiaGeRTH = rdr.GetDecimal(rdr.GetOrdinal("RTradePrice"));
                    item.JiaGeZJS = rdr.GetDecimal(rdr.GetOrdinal("ZSettlementPrice"));
                    item.JiaGeZMS = rdr.GetDecimal(rdr.GetOrdinal("ZRetailPrice"));
                    item.JiaGeZTH = rdr.GetDecimal(rdr.GetOrdinal("ZTradePrice"));
                    item.Name = rdr["MenuName"].ToString();
                    item.NeiRong = rdr["MenuContact"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.IsDisplay = rdr["IsDisplay"].ToString() == "1";
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取餐馆菜单信息
        /// </summary>
        /// <param name="caiDanId">菜单编号</param>
        /// <param name="lngType">语言类型</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo GetCanGuanCaiDanInfo(string caiDanId, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCanGuanCaiDanInfo);
            _db.AddInParameter(cmd, "MenuId", DbType.AnsiStringFixedLength, caiDanId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lngType);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MCanGuanCaiDanInfo();

                    item.CaiDanId = rdr["MenuId"].ToString();
                    item.GysId = rdr["SourceId"].ToString();
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeRJS = rdr.GetDecimal(rdr.GetOrdinal("RSettlementPrice"));
                    item.JiaGeRMS = rdr.GetDecimal(rdr.GetOrdinal("RRetailPrice"));
                    item.JiaGeRTH = rdr.GetDecimal(rdr.GetOrdinal("RTradePrice"));
                    item.JiaGeZJS = rdr.GetDecimal(rdr.GetOrdinal("ZSettlementPrice"));
                    item.JiaGeZMS = rdr.GetDecimal(rdr.GetOrdinal("ZRetailPrice"));
                    item.JiaGeZTH = rdr.GetDecimal(rdr.GetOrdinal("ZTradePrice"));
                    item.Name = rdr["MenuName"].ToString();
                    item.NeiRong = rdr["MenuContact"].ToString();
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.TMianM = rdr.GetInt32(rdr.GetOrdinal("TMianM"));
                    item.TMianN = rdr.GetInt32(rdr.GetOrdinal("TMianN"));
                    item.IsDisplay = rdr["IsDisplay"].ToString() == "1";
                }
            }

            return item;
        }

        /// <summary>
        /// 新增酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [tbl_SourceJiuDianJiaGe]([JiaGeId],[GysId],[BinKeLeiXing],[FangXingId],[IsHanZao],[JiaGeZC],[JiaGeMS],[JiaGeTJS],[JiaGeSJS],[JiaGeTFW],[JiaGeSFW],[TMianM],[TMianN],[STime],[ETime],[BeiZhu],[IsDelete],[OperatorId],[IssueTime]) VALUES (@JiaGeId,@GysId,@BinKeLeiXing,@FangXingId,@IsHanZao,@JiaGeZC,@JiaGeMS,@JiaGeTJS,@JiaGeSJS,@JiaGeTFW,@JiaGeSFW,@TMianM,@TMianN,@STime,@ETime,@BeiZhu,@IsDelete,@OperatorId,@IssueTime);");

            if (info.XingQi != null && info.XingQi.Count > 0)
            {
                foreach (var w in info.XingQi)
                {
                    sql.AppendFormat(" INSERT INTO [tbl_SourceJiuDianJiaGeXingQi]([JiaGeId],[XingQi]) VALUES (@JiaGeId,{0}); ", (int)w);
                }
            }

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "BinKeLeiXing", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "FangXingId", DbType.AnsiStringFixedLength, info.FangXingId);
            _db.AddInParameter(cmd, "IsHanZao", DbType.AnsiStringFixedLength, info.IsHanZao ? "1" : "0");
            _db.AddInParameter(cmd, "JiaGeZC", DbType.Decimal, info.JiaGeZC);
            _db.AddInParameter(cmd, "JiaGeMS", DbType.Decimal, info.JiaGeMS);
            _db.AddInParameter(cmd, "JiaGeTJS", DbType.Decimal, info.JiaGeTJS);
            _db.AddInParameter(cmd, "JiaGeSJS", DbType.Decimal, info.JiaGeSJS);
            _db.AddInParameter(cmd, "JiaGeTFW", DbType.Decimal, info.JiaGeTFW);
            _db.AddInParameter(cmd, "JiaGeSFW", DbType.Decimal, info.JiaGeSFW);
            _db.AddInParameter(cmd, "TMianM", DbType.Int32, info.TMianM);
            _db.AddInParameter(cmd, "TMianN", DbType.Decimal, info.TMianN);
            _db.AddInParameter(cmd, "STime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "ETime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            return DbHelper.ExecuteSql(cmd, _db) > 0 ? 1 : -100;
        }

        /// <summary>
        /// 修改酒店价格信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateJiuDianJiaGe(EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo info)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM [tbl_SourceJiuDianJiaGeXingQi] WHERE [JiaGeId]=@JiaGeId;");
            sql.Append("UPDATE [tbl_SourceJiuDianJiaGe] SET [BinKeLeiXing]=@BinKeLeiXing,[FangXingId]=@FangXingId,[IsHanZao]=@IsHanZao,[JiaGeZC]=@JiaGeZC,[JiaGeMS]=@JiaGeMS,[JiaGeTJS]=@JiaGeTJS,[JiaGeSJS]=@JiaGeSJS,[JiaGeTFW]=@JiaGeTFW,[JiaGeSFW]=@JiaGeSFW,[TMianM]=@TMianM,[TMianN]=@TMianN,[STime]=@STime,[ETime]=@ETime,[BeiZhu]=@BeiZhu WHERE [JiaGeId]=@JiaGeId;");

            if (info.XingQi != null && info.XingQi.Count > 0)
            {
                foreach (var w in info.XingQi)
                {
                    sql.AppendFormat(" INSERT INTO [tbl_SourceJiuDianJiaGeXingQi]([JiaGeId],[XingQi]) VALUES (@JiaGeId,{0}); ", (int)w);
                }
            }

            DbCommand cmd = _db.GetSqlStringCommand(sql.ToString());
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, info.JiaGeId);
            _db.AddInParameter(cmd, "BinKeLeiXing", DbType.Byte, info.BinKeLeiXing);
            _db.AddInParameter(cmd, "FangXingId", DbType.AnsiStringFixedLength, info.FangXingId);
            _db.AddInParameter(cmd, "IsHanZao", DbType.AnsiStringFixedLength, info.IsHanZao ? "1" : "0");
            _db.AddInParameter(cmd, "JiaGeZC", DbType.Decimal, info.JiaGeZC);
            _db.AddInParameter(cmd, "JiaGeMS", DbType.Decimal, info.JiaGeMS);
            _db.AddInParameter(cmd, "JiaGeTJS", DbType.Decimal, info.JiaGeTJS);
            _db.AddInParameter(cmd, "JiaGeSJS", DbType.Decimal, info.JiaGeSJS);
            _db.AddInParameter(cmd, "JiaGeTFW", DbType.Decimal, info.JiaGeTFW);
            _db.AddInParameter(cmd, "JiaGeSFW", DbType.Decimal, info.JiaGeSFW);
            _db.AddInParameter(cmd, "TMianM", DbType.Int32, info.TMianM);
            _db.AddInParameter(cmd, "TMianN", DbType.Decimal, info.TMianN);
            _db.AddInParameter(cmd, "STime", DbType.DateTime, info.STime);
            _db.AddInParameter(cmd, "ETime", DbType.DateTime, info.ETime);
            _db.AddInParameter(cmd, "BeiZhu", DbType.String, info.BeiZhu);

            return DbHelper.ExecuteSql(cmd, _db) > 0 ? 1 : -100;
        }

        /// <summary>
        /// 删除酒店价格信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public int DeleteJiuDianJiaGe(string companyId, string gysId, string jiaGeId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_DeleteJiuDianJiaGe);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, jiaGeId);
            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 获取酒店价格信息集合
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> GetJiuDianJiaGes(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo> items = new List<EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiuDianJiaGes);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo();
                    item.BeiZhu = rdr["BeiZhu"].ToString();
                    item.BinKeLeiXing = (EyouSoft.Model.EnumType.CrmStructure.CustomType)rdr.GetByte(rdr.GetOrdinal("BinKeLeiXing"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ETime"))) item.ETime = rdr.GetDateTime(rdr.GetOrdinal("ETime"));
                    item.FangXingId = rdr["FangXingId"].ToString();
                    item.GysId = rdr["GysId"].ToString();
                    item.IsHanZao = rdr["IsHanZao"].ToString() == "1";
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.JiaGeMS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeMS"));
                    item.JiaGeSFW = rdr.GetDecimal(rdr.GetOrdinal("JiaGeSFW"));
                    item.JiaGeSJS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeSJS"));
                    item.JiaGeTFW = rdr.GetDecimal(rdr.GetOrdinal("JiaGeTFW"));
                    item.JiaGeTJS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeTJS"));
                    item.JiaGeZC = rdr.GetDecimal(rdr.GetOrdinal("JiaGeZC"));
                    item.OperatorId = rdr["OperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("STime"))) item.STime = rdr.GetDateTime(rdr.GetOrdinal("STime"));
                    item.TMianM = rdr.GetInt32(rdr.GetOrdinal("TMianM"));
                    item.TMianN = rdr.GetDecimal(rdr.GetOrdinal("TMianN"));
                    item.XingQi = null;
                    item.FangXingName = rdr["FangXingName"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取酒店价格信息业务实体
        /// </summary>
        /// <param name="jiaGeId">价格编号</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo GetJiuDianJiaGeInfo(string jiaGeId)
        {
            EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiuDianJiaGeInfo);
            _db.AddInParameter(cmd, "JiaGeId", DbType.AnsiStringFixedLength, jiaGeId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MJiuDianJiaGeInfo();
                    item.BeiZhu = rdr["BeiZhu"].ToString();
                    item.BinKeLeiXing = (EyouSoft.Model.EnumType.CrmStructure.CustomType)rdr.GetByte(rdr.GetOrdinal("BinKeLeiXing"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ETime"))) item.ETime = rdr.GetDateTime(rdr.GetOrdinal("ETime"));
                    item.FangXingId = rdr["FangXingId"].ToString();
                    item.GysId = rdr["GysId"].ToString();
                    item.IsHanZao = rdr["IsHanZao"].ToString() == "1";
                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.JiaGeId = rdr["JiaGeId"].ToString();
                    item.JiaGeMS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeMS"));
                    item.JiaGeSFW = rdr.GetDecimal(rdr.GetOrdinal("JiaGeSFW"));
                    item.JiaGeSJS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeSJS"));
                    item.JiaGeTFW = rdr.GetDecimal(rdr.GetOrdinal("JiaGeTFW"));
                    item.JiaGeTJS = rdr.GetDecimal(rdr.GetOrdinal("JiaGeTJS"));
                    item.JiaGeZC = rdr.GetDecimal(rdr.GetOrdinal("JiaGeZC"));
                    item.OperatorId = rdr["OperatorId"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("STime"))) item.STime = rdr.GetDateTime(rdr.GetOrdinal("STime"));
                    item.TMianM = rdr.GetInt32(rdr.GetOrdinal("TMianM"));
                    item.TMianN = rdr.GetDecimal(rdr.GetOrdinal("TMianN"));
                    item.XingQi = null;
                }
            }

            if (item == null) return item;

            item.XingQi = GetJiuDianJiaGeXingQi(item.JiaGeId);

            return item;
        }

        #endregion
    }
}
