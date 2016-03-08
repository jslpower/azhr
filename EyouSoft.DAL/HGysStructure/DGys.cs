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
    /// 供应商相关数据访问类
    /// </summary>
    public class DGys : DALBase, EyouSoft.IDAL.HGysStructure.IGys
    {
        #region static constants
        //static constants
        const string DEFAULT_XML_DOC = "<root></root>";
        const string SQL_SELECT_GetInfo = "SELECT A.*,(SELECT A1.ContactName FROM tbl_ComUser AS A1 WHERE A1.UserId=A.LastModifierId) AS LatestOperatorName FROM [tbl_Source] AS A WHERE A.[SourceId]=@GysId AND A.[LngType]=@LngType";
        const string SQL_SELECT_GetCheXings = "SELECT * FROM [tbl_SourceCar] WHERE [SourceId]=@GysId ORDER BY [IdentityId] ASC";
        const string SQL_SELECT_GetGouWu = "SELECT * FROM [tbl_SourceShop] WHERE [SourceId]=@GysId ";
        const string SQL_SELECT_GetGouWuChanPins = "SELECT * FROM [tbl_SourceShopChanPin] WHERE [SourceId]=@GysId ORDER BY [IdentityId] ASC";
        const string SQL_SELECT_GetGouWuHeTongs = "SELECT * FROM [tbl_SourceShopHeTong] WHERE [SourceId]=@GysId ORDER BY [IdentityId] ASC ";
        //const string SQL_SELECT_GetJingDians = "SELECT * FROM [tbl_SourceJingDian] WHERE [SourceId]=@GysId AND [LngType]=@LngType ORDER BY [IdentityId] ASC ";
        const string SQL_SELECT_GetJingDians = "SELECT * FROM (SELECT * FROM [tbl_SourceJingDian] WHERE [SourceId]=@GysId AND [LngType]=@LngType AND IsDelete='0' UNION ALL SELECT * FROM [tbl_SourceJingDian] WHERE [SourceId]=@GysId AND [LngType]<>@LngType AND SightId NOT IN(SELECT SightId FROM [tbl_SourceJingDian] WHERE [SourceId]=@GysId AND [LngType]=@LngType AND IsDelete='0') AND IsDelete='0') C ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetJiuDianBaoJias = "SELECT * FROM [tbl_SourceHotelBaoJia] WHERE [SourceId]=@GysId ORDER BY [IdentityId] ASC ";
        const string SQL_SELECT_GetFuJians = "SELECT * FROM [tbl_SourceFuJian] WHERE ItemId=@XID AND ItemType=@XTYPE  ORDER BY [IdentityId] ASC ";
        const string SQL_SELECT_GetQiTa = "SELECT * FROM [tbl_SourceOther] WHERE [SourceId]=@GysId ";
        const string SQL_SELECT_GetHeTongs = "SELECT * FROM [tbl_SourceHeTong] WHERE [SourceId]=@GysId ORDER BY IdentityId ASC";
        const string SQL_SELECT_GetLxrs = "SELECT * FROM [tbl_CrmLinkman] WHERE [TypeId]=@GysId AND [Type]=@LxrType AND [IsDeleted]='0' ORDER BY SortId ASC";
        //const string SQL_SELECT_IsExistsLng = "SELECT COUNT(*) FROM [tbl_Source] WHERE [SourceId]=@GysId AND LngType=@LngType";
        const string SQL_SELECT_GetGysLeiXing = "SELECT [Type] FROM [tbl_Source] WHERE [SourceId]=@GysId AND LngType=@LngType";
        const string SQL_SELECT_GetJingDianInfo = "SELECT * FROM [tbl_SourceJingDian] WHERE [SightId]=@JingDianId AND [LngType]=@LngType AND IsDelete=0";

        const string SQL_SELECT_GetJingDianInfo_bycity = "select * from view_Gys_JingDian as A  where A.JingDianId=@JingDianId and A.lngType=@LngType and A.CountyId=@CountyId";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DGys()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// create cpcd xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateCPCDXml(EyouSoft.Model.ComStructure.MCPCC info)
        {
            info = info ?? new EyouSoft.Model.ComStructure.MCPCC();

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            s.AppendFormat("<info CountryId=\"{0}\" ",info.CountryId);
            s.AppendFormat(" ProvinceId=\"{0}\" ", info.ProvinceId);
            s.AppendFormat(" CityId=\"{0}\" ", info.CityId);
            s.AppendFormat(" DistrictId=\"{0}\" ", info.DistrictId);
            s.Append(" /> ");
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create jiudian fujian xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateJiuDianFuJianXml(EyouSoft.Model.HGysStructure.MJiuDianInfo info)
        {
            if (info == null || info.FuJians == null || info.FuJians.Count == 0) return DEFAULT_XML_DOC;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in info.FuJians)
            {
                s.AppendFormat("<info FilePath=\"{0}\" FuJianType=\"{1}\" />", item.FilePath, (int)EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.酒店图片);
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create jiudian baojia xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateJiuDianBaoJiaXml(EyouSoft.Model.HGysStructure.MJiuDianInfo info)
        {
            if (info == null || info.BaoJias == null || info.BaoJias.Count == 0) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in info.BaoJias)
            {
                s.Append("<info");
                s.AppendFormat(" BinKeLeiXing=\"{0}\"", (int)item.BinKeLeiXing);
                s.AppendFormat(" TuanXing=\"{0}\"", (int)item.TuanXing);
                s.AppendFormat(" PJiaGe=\"{0}\"", item.PJiaGe);
                s.AppendFormat(" PBeiZhu=\"{0}\"", Utils.ReplaceXmlSpecialCharacter(item.PBeiZhu));
                s.AppendFormat(" DJiaGe=\"{0}\"", item.DJiaGe);
                s.AppendFormat(" DBeiZhu=\"{0}\"", Utils.ReplaceXmlSpecialCharacter(item.DBeiZhu));
                s.AppendFormat(" WJiaGe=\"{0}\"", item.WJiaGe);
                s.AppendFormat(" WBeiZhu=\"{0}\"", Utils.ReplaceXmlSpecialCharacter(item.WBeiZhu));
                s.Append("/>");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create hetong xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateHeTongXml(IList<EyouSoft.Model.HGysStructure.MHeTongInfo> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.Append("<info ");
                s.AppendFormat(" HeTongId=\"{0}\" ", item.HeTongId);
                if (item.STime.HasValue) s.AppendFormat(" STime=\"{0}\" ", item.STime.Value);
                if (item.ETime.HasValue) s.AppendFormat(" ETime=\"{0}\" ", item.ETime.Value);
                s.AppendFormat(" FilePath=\"{0}\" ", item.FilePath);
                s.AppendFormat(" BaoDi=\"{0}\" ", item.BaoDi);
                s.AppendFormat(" JiangLi=\"{0}\" ", item.JiangLi);
                s.AppendFormat(" BeiZhu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.BeiZhu));
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create lxr xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateLxrXml(IList<EyouSoft.Model.HGysStructure.MLxrInfo> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.Append("<info ");
                s.AppendFormat(" LxrId=\"{0}\" ", item.LxrId);
                s.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Name));
                s.AppendFormat(" Gender=\"{0}\" ", (int)EyouSoft.Model.EnumType.GovStructure.Gender.男);
                if (item.Birthday.HasValue) s.AppendFormat(" Birthday=\"{0}\" ", item.Birthday.Value);
                s.AppendFormat(" ZhiWu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.ZhiWu));
                s.AppendFormat(" Telephone=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Telephone));
                s.AppendFormat(" Mobile=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Mobile));
                s.AppendFormat(" QQ=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.QQ));
                s.AppendFormat(" Email=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Email));
                s.AppendFormat(" MSN=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.MSN));
                s.AppendFormat(" Fax=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Fax));
                s.AppendFormat(" LxrType=\"{0}\" ", (int)EyouSoft.Model.EnumType.ComStructure.LxrType.供应商);
                s.AppendFormat(" IsTiXing=\"{0}\" ", item.IsTiXing ? "1" : "0");
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create jingdian xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateJingDianXml(IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                s.Append("<info ");
                s.AppendFormat(" JingDianId=\"{0}\" ", item.JingDianId);
                s.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Name));
                s.AppendFormat(" XingJi=\"{0}\" ", (int)item.XingJi);
                s.AppendFormat(" WangZhi=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.WangZhi));
                s.AppendFormat(" YouLanShiJian=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.YouLanShiJian));
                s.AppendFormat(" Telephone=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Telephone));
                s.AppendFormat(" IsTuiJian=\"{0}\" ", item.IsTuiJian ? "1" : "0");
                s.AppendFormat(" JianJie=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.JianJie));
                s.AppendFormat(" IsXiu=\"{0}\" ", item.IsXiu ? "1" : "0");
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create jingdian fujian xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateJingDianFuJianXml(IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {
                if (item.FuJian == null || string.IsNullOrEmpty(item.FuJian.FilePath)) continue;
                s.Append("<info ");
                s.AppendFormat(" JingDianId=\"{0}\" ", item.JingDianId);
                s.AppendFormat(" FilePath=\"{0}\" ", item.FuJian.FilePath);
                s.AppendFormat(" FuJianType=\"{0}\" ", (int)EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.景点图片);
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create chexing xml
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        string CreateCheXingXml(IList<EyouSoft.Model.HGysStructure.MCheXingInfo> items)
        {
            if (items == null || items.Count == 0) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in items)
            {                
                s.Append("<info ");
                s.AppendFormat(" CheXingId=\"{0}\" ", item.CheXingId);
                s.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Name));
                if (item.FuJian != null && !string.IsNullOrEmpty(item.FuJian.FilePath))
                    s.AppendFormat(" FilePath=\"{0}\" ", item.FuJian.FilePath);
                s.AppendFormat(" ZuoWeiShu=\"{0}\" ", item.ZuoWeiShu);
                s.AppendFormat(" DanJiaJiShu=\"{0}\" ", item.DanJiaJiShu);
                s.AppendFormat(" BeiZhu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.BeiZhu));
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create gouwu xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateGouWuXml(EyouSoft.Model.HGysStructure.MGouWuInfo info)
        {
            if (info == null) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            s.Append("<info ");
            s.AppendFormat(" ShangPinLeiBie=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(info.ShangPinLeiBie));
            s.AppendFormat(" LiuShui=\"{0}\" ", info.LiuShui);
            s.AppendFormat(" RenTouCR=\"{0}\" ", info.RenTouCR);
            s.AppendFormat(" RenTouET=\"{0}\" ", info.RenTouET);
            s.AppendFormat(" BaoDiJinE=\"{0}\" ", info.BaoDiJinE);
            s.Append(" /> ");
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create gouwu chanpin xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateGouWuChanPinXml(EyouSoft.Model.HGysStructure.MGouWuInfo info)
        {
            if (info == null || info.ChanPins == null || info.ChanPins.Count == 0) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in info.ChanPins)
            {
                s.Append("<info ");
                s.AppendFormat(" ChanPinId=\"{0}\" ", item.ChanPinId);
                s.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Name));
                s.AppendFormat(" XiaoShouJinE=\"{0}\" ", item.XiaoShouJinE);
                s.AppendFormat(" FanDianJinE=\"{0}\" ", item.FanDianJinE);
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create gouwu hetong xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateGouWuHeTongXml(EyouSoft.Model.HGysStructure.MGouWuInfo info)
        {
            if (info == null || info.HeTongs == null || info.HeTongs.Count == 0) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            foreach (var item in info.HeTongs)
            {
                s.Append("<info ");
                s.AppendFormat(" HeTongId=\"{0}\" ", item.HeTongId);
                if (item.ETime.HasValue) s.AppendFormat(" ETime=\"{0}\" ", item.ETime.Value);
                s.AppendFormat(" GuoJi=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.GuoJi));
                s.AppendFormat(" LiuShui=\"{0}\" ", item.LiuShui);
                s.AppendFormat(" RenTouCR=\"{0}\" ", item.RenTouCR);
                s.AppendFormat(" RenTouET=\"{0}\" ", item.RenTouET);
                s.AppendFormat(" FilePath=\"{0}\" ", item.FilePath);
                s.AppendFormat(" IsQiYong=\"{0}\" ", item.IsQiYong ? "1" : "0");
                s.AppendFormat(" BaoDiJinE=\"{0}\" ", item.BaoDiJinE);
                s.Append(" /> ");
            }
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// create qita xml
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateQiTaXml(EyouSoft.Model.HGysStructure.MQiTaInfo info)
        {
            if (info == null) return DEFAULT_XML_DOC;
            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            s.Append("<info ");
            s.AppendFormat(" JiaGe=\"{0}\" ", info.JiaGe);
            s.AppendFormat(" JieSuanJiaGe=\"{0}\" ", info.JieSuanJiaGe);
            s.Append(" /> ");
            s.Append("</root>");
            return s.ToString();
        }

        /// <summary>
        /// get chexings
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MCheXingInfo> GetCheXings(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MCheXingInfo> items = new List<EyouSoft.Model.HGysStructure.MCheXingInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCheXings);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MCheXingInfo();
                    item.BeiZhu = rdr["Remark"].ToString();
                    item.CheXingId = rdr["CarId"].ToString();
                    item.DanJiaJiShu = rdr.GetDecimal(rdr.GetOrdinal("SinglePrice"));
                    item.FuJian = new EyouSoft.Model.HGysStructure.MFuJianInfo();
                    item.FuJian.FilePath = rdr["CarIMG"].ToString();
                    item.Name = rdr["TypeName"].ToString();
                    item.ZuoWeiShu = rdr.GetInt32(rdr.GetOrdinal("SeatNumber"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get gouwu chanpins
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> GetGouWuChanPins(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo> items = new List<EyouSoft.Model.HGysStructure.MGouWuChanPinInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGouWuChanPins);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MGouWuChanPinInfo();
                    item.ChanPinId = rdr["ProductId"].ToString();
                    item.FanDianJinE = rdr.GetDecimal(rdr.GetOrdinal("BackMoney"));
                    item.Name = rdr["ProductName"].ToString();
                    item.XiaoShouJinE = rdr.GetDecimal(rdr.GetOrdinal("ProductMoney"));
                    item.IdentityId = rdr.GetInt32(rdr.GetOrdinal("IdentityId"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get gouwu hetongs
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> GetGouWuHeTongs(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo> items = new List<EyouSoft.Model.HGysStructure.MGouWuHeTongInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGouWuHeTongs);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MGouWuHeTongInfo();
                    item.BaoDiJinE = rdr.GetDecimal(rdr.GetOrdinal("BaoDi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ContactTime"))) item.ETime = rdr.GetDateTime(rdr.GetOrdinal("ContactTime"));
                    item.FilePath = rdr["FilePath"].ToString();
                    item.GuoJi = rdr["Country"].ToString();
                    item.HeTongId = rdr["HeTongId"].ToString();
                    item.IsQiYong = rdr["IsDisable"].ToString() == "1";
                    item.LiuShui = rdr.GetDecimal(rdr.GetOrdinal("LiuShui"));
                    item.RenTouCR = rdr.GetDecimal(rdr.GetOrdinal("PeopleMoney"));
                    item.RenTouET = rdr.GetDecimal(rdr.GetOrdinal("ChildMoney"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get gouwu
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MGouWuInfo GetGouWu(string gysId)
        {
            var info = new EyouSoft.Model.HGysStructure.MGouWuInfo();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGouWu);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.BaoDiJinE = rdr.GetDecimal(rdr.GetOrdinal("MiniMoney"));
                    info.ChanPins = null;
                    info.HeTongs = null;
                    info.LiuShui = rdr.GetDecimal(rdr.GetOrdinal("GlideMoney"));
                    info.RenTouCR = rdr.GetDecimal(rdr.GetOrdinal("PeopleMoney"));
                    info.RenTouET = rdr.GetDecimal(rdr.GetOrdinal("ChildMoney"));
                    info.ShangPinLeiBie = rdr["SellType"].ToString();
                }
            }

            info.ChanPins = GetGouWuChanPins(gysId);
            info.HeTongs = GetGouWuHeTongs(gysId);

            return info;
        }

        /// <summary>
        /// get jingdians
        /// </summary>
        /// <param name="gysId"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> GetJingDians(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            IList<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo> items = new List<EyouSoft.Model.HGysStructure.MJingDianJingDianInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJingDians);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lng);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MJingDianJingDianInfo();
                    item.FuJian = null;
                    item.IsTuiJian = rdr["IsDefault"].ToString() == "1";
                    item.IsXiu = rdr["IsXiu"].ToString() == "1";
                    item.JianJie = rdr["GuideWord"].ToString();
                    item.JingDianId = rdr["SightId"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.Telephone = rdr["SpotTel"].ToString();
                    item.WangZhi = rdr["SpotURL"].ToString();
                    item.XingJi = (EyouSoft.Model.EnumType.GysStructure.JingDianXingJi)rdr.GetByte(rdr.GetOrdinal("Star"));
                    item.YouLanShiJian = rdr["SpotTime"].ToString();
                    items.Add(item);
                }
            }

            foreach (var item in items)
            {
                var fujians = GetFuJians(item.JingDianId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.景点图片);
                if (fujians != null && fujians.Count > 0) item.FuJian = fujians[0];
            }

            return items;
        }

        /// <summary>
        /// get jiudian baojias
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> GetJiuDianBaoJias(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo> items = new List<EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJiuDianBaoJias);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MJiuDianBaoJiaInfo();

                    item.BinKeLeiXing = (EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaRoomType)rdr.GetByte(rdr.GetOrdinal("CustomType"));
                    item.DBeiZhu = rdr["DBackMark"].ToString();
                    item.DJiaGe = rdr.GetDecimal(rdr.GetOrdinal("Dan"));
                    item.PBeiZhu = rdr["PBackMark"].ToString();
                    item.PJiaGe = rdr.GetDecimal(rdr.GetOrdinal("Ping"));
                    item.TuanXing = (EyouSoft.Model.EnumType.GysStructure.JiuDianBaoJiaPriceType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.WBeiZhu = rdr["WBackMark"].ToString();
                    item.WJiaGe = rdr.GetDecimal(rdr.GetOrdinal("Wang"));

                    items.Add(item);
                }
            }

            return items;

        }

        /// <summary>
        /// get fujians
        /// </summary>
        /// <param name="xId"></param>
        /// <param name="leiXing"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MFuJianInfo> GetFuJians(string xId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing leiXing)
        {
            IList<EyouSoft.Model.HGysStructure.MFuJianInfo> items = new List<EyouSoft.Model.HGysStructure.MFuJianInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetFuJians);
            _db.AddInParameter(cmd, "XID", DbType.AnsiStringFixedLength, xId);
            _db.AddInParameter(cmd, "XTYPE", DbType.Byte, leiXing);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MFuJianInfo();
                    item.FilePath = rdr["FilePath"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get qita
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MQiTaInfo GetQiTa(string gysId)
        {
            var info = new EyouSoft.Model.HGysStructure.MQiTaInfo();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetQiTa);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.JiaGe = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    info.JieSuanJiaGe = rdr.GetDecimal(rdr.GetOrdinal("ClosingCost"));
                }
            }

            return info;
        }

        /// <summary>
        /// get hetongs
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MHeTongInfo> GetHeTongs(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MHeTongInfo> items = new List<EyouSoft.Model.HGysStructure.MHeTongInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetHeTongs);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MHeTongInfo();
                    item.BaoDi = rdr.GetInt32(rdr.GetOrdinal("BaoDi"));
                    item.BeiZhu = rdr["BeiZhu"].ToString();
                    if(!rdr.IsDBNull(rdr.GetOrdinal("Etime")))item.ETime = rdr.GetDateTime(rdr.GetOrdinal("Etime"));
                    item.FilePath = rdr["FilePath"].ToString();
                    item.HeTongId = rdr["HeTongId"].ToString();
                    item.JiangLi = rdr.GetInt32(rdr.GetOrdinal("JiangLi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Stime"))) item.STime = rdr.GetDateTime(rdr.GetOrdinal("Stime"));
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// get lxrs
        /// </summary>
        /// <param name="gysId"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.HGysStructure.MLxrInfo> GetLxrs(string gysId)
        {
            IList<EyouSoft.Model.HGysStructure.MLxrInfo> items = new List<EyouSoft.Model.HGysStructure.MLxrInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetLxrs);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LxrType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.LxrType.供应商);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MLxrInfo();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("Birthday"))) item.Birthday = rdr.GetDateTime(rdr.GetOrdinal("Birthday"));
                    item.Email = rdr["EMail"].ToString();
                    item.Fax = rdr["Fax"].ToString();
                    item.IsTiXing = rdr["IsRemind"].ToString() == "1";
                    item.LxrId = rdr["Id"].ToString();
                    item.Mobile = rdr["MobilePhone"].ToString();
                    item.MSN = rdr["MSNSKYPE"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.QQ = rdr["QQ"].ToString();
                    item.Telephone = rdr["Telephone"].ToString();
                    item.ZhiWu = rdr["Post"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商交易汇总信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        EyouSoft.Model.HGysStructure.MJiaoYiXXInfo GetJiaoYiXX(string gysId)
        {
            var info = new EyouSoft.Model.HGysStructure.MJiaoYiXXInfo();
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_GetJiaoYiXX");
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);

            using (IDataReader rdr = DbHelper.RunReaderProcedure(cmd, _db))
            {
                if (rdr.Read())
                {
                    info.JiaoYiCiShu = rdr.GetInt32(rdr.GetOrdinal("JiaoYiCiShu"));
                    info.JiaoYiShuLiang = rdr.GetInt32(rdr.GetOrdinal("JiaoYiShuLiang"));
                    info.JieSuanJinE = rdr.GetDecimal(rdr.GetOrdinal("JieSuanJinE"));
                    info.YiZhiFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinE"));
                    info.BaoDiShu = rdr.GetInt32(rdr.GetOrdinal("BaoDiShu"));
                }
            }

            return info;
        }
        #endregion

        #region IGys 成员
        /*/// <summary>
        /// 是否存在指定语种的供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="lng">语种</param>
        /// <returns></returns>
        public bool IsExistsLng(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_IsExistsLng);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "@LngType", DbType.Byte, lng);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) == 1;
                }
            }

            return false;
        }*/

        /// <summary>
        /// 写入供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.HGysStructure.MGysInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_Insert");
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@LngType", DbType.Byte, info.LngType);
            _db.AddInParameter(cmd, "@GysName", DbType.String, info.GysName);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@XuKeZhengCode", DbType.String, info.XuKeZhengCode);
            _db.AddInParameter(cmd, "@FaRenName", DbType.String, info.FaRenName);
            _db.AddInParameter(cmd, "@FaRenTelephone", DbType.String, info.FaRenTelephone);
            _db.AddInParameter(cmd, "@Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "@JianJie", DbType.String, info.JianJie);
            _db.AddInParameter(cmd, "@BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "@IsQianDan", DbType.AnsiStringFixedLength, info.IsQianDan ? "1" : "0");
            _db.AddInParameter(cmd, "@IsTuiJian", DbType.AnsiStringFixedLength, info.IsTuiJian ? "1" : "0");
            _db.AddInParameter(cmd, "@IsFanDan", DbType.AnsiStringFixedLength, info.IsFanDan ? "1" : "0");
            _db.AddInParameter(cmd, "@IsMianYi", DbType.AnsiStringFixedLength, info.IsMianYi ? "1" : "0");
            _db.AddInParameter(cmd, "@IsHeTong", DbType.AnsiStringFixedLength, info.IsHeTong ? "1" : "0");
            _db.AddInParameter(cmd, "@JieSuanFangShi", DbType.Byte, info.JieSuanFangShi);
            _db.AddInParameter(cmd, "@GuaZhangQiXian", DbType.String, info.GuaZhangQiXian);
            _db.AddInParameter(cmd, "@WangZhi", DbType.String, info.WangZhi);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            if (info.JiuDian != null)
            {
                _db.AddInParameter(cmd, "@JiuDianQianTaiTelephone", DbType.String, info.JiuDian.QianTaiTelephone);
                _db.AddInParameter(cmd, "@JiuDianXingJi", DbType.Byte, info.JiuDian.XingJi);
            }
            if (info.CanGuan != null)
            {
                _db.AddInParameter(cmd, "@CanGuanCanBiao", DbType.String, info.CanGuan.CanBiao);
                _db.AddInParameter(cmd, "@CanGuanCaiXi", DbType.String, info.CanGuan.CaiXi);
            }
            _db.AddInParameter(cmd, "@CPCDXml", DbType.String, CreateCPCDXml(info.CPCD));
            _db.AddInParameter(cmd, "@JiuDianFuJianXml", DbType.String, CreateJiuDianFuJianXml(info.JiuDian));
            _db.AddInParameter(cmd, "@JiuDianBaoJiaXml", DbType.String, CreateJiuDianBaoJiaXml(info.JiuDian));
            _db.AddInParameter(cmd, "@HeTongXml", DbType.String, CreateHeTongXml(info.HeTongs));
            _db.AddInParameter(cmd, "@LxrXml", DbType.String, CreateLxrXml(info.Lxrs));
            _db.AddInParameter(cmd, "@JingDianXml", DbType.String, CreateJingDianXml(info.JingDians));
            _db.AddInParameter(cmd, "@JingDianFuJianXml", DbType.String, CreateJingDianFuJianXml(info.JingDians));
            _db.AddInParameter(cmd, "@CheXingXml", DbType.String, CreateCheXingXml(info.CheXings));
            _db.AddInParameter(cmd, "@GouWuXml", DbType.String, CreateGouWuXml(info.GouWu));
            _db.AddInParameter(cmd, "@GouWuChanPinXml", DbType.String, CreateGouWuChanPinXml(info.GouWu));
            _db.AddInParameter(cmd, "@GouWuHeTongXml", DbType.String, CreateGouWuHeTongXml(info.GouWu));
            _db.AddInParameter(cmd, "@QiTaXml", DbType.String, CreateQiTaXml(info.QiTa));
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
        /// 获取供应商信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <param name="lng">语言类型</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MGysInfo GetInfo(string gysId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            EyouSoft.Model.HGysStructure.MGysInfo info = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            _db.AddInParameter(cmd, "GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lng);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.HGysStructure.MGysInfo();

                    info.Address = rdr["Address"].ToString();
                    info.BeiZhu = rdr["Remark"].ToString();
                    info.CanGuan = null;
                    info.CheXings = null;
                    info.CompanyId = rdr["CompanyId"].ToString();
                    info.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    info.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    info.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    info.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.FaRenName = rdr["FaRenName"].ToString();
                    info.FaRenTelephone = rdr["FaRenTel"].ToString();
                    info.GouWu = null;
                    info.GuaZhangQiXian = rdr["GuaZhangQiXian"].ToString();
                    info.GysId = gysId;
                    info.GysName = rdr["Name"].ToString();
                    info.HeTongs = null;
                    info.IsFanDan = rdr["IsFanDan"].ToString() == "1";
                    info.IsHeTong = rdr["IsHeTong"].ToString() == "1";
                    info.IsMianYi = rdr["IsMianYi"].ToString() == "1";
                    info.IsQianDan = rdr["IsQianDan"].ToString() == "1";
                    info.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    info.IsTuiJian = rdr["IsTuiJian"].ToString() == "1";
                    info.JianJie = rdr["Desc"].ToString();
                    info.JieSuanFangShi = (EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi)rdr.GetByte(rdr.GetOrdinal("JieSuanFangShi"));
                    info.JingDians = null;
                    info.JiuDian = null;
                    info.LatestOperatorId = rdr["LastModifierId"].ToString();
                    info.LatestOperatorName = rdr["LatestOperatorName"].ToString();
                    info.LatestTime = rdr.GetDateTime(rdr.GetOrdinal("LastModifyTime"));
                    info.LeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)rdr.GetByte(rdr.GetOrdinal("Type"));
                    info.LngType = lng;
                    info.Lxrs = null;
                    info.OperatorId = rdr["OperatorId"].ToString();
                    info.QiTa = null;
                    info.XuKeZhengCode = rdr["LicenseKey"].ToString();
                    info.WangZhi = rdr["WangZhi"].ToString();
                    info.IdentityId = rdr.GetInt32(rdr.GetOrdinal("IdentityId"));

                    if (info.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店)
                    {
                        info.JiuDian = new EyouSoft.Model.HGysStructure.MJiuDianInfo();
                        info.JiuDian.QianTaiTelephone = rdr["QianTaiDianHua"].ToString();
                        info.JiuDian.XingJi = (EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi)rdr.GetByte(rdr.GetOrdinal("JiuDianXingJi"));
                    }

                    if (info.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆)
                    {
                        info.CanGuan = new EyouSoft.Model.HGysStructure.MCanGuanInfo();
                        info.CanGuan.CaiXi = rdr["CanGuanCaiXi"].ToString();
                        info.CanGuan.CanBiao = rdr["CanGuanCanBiao"].ToString();
                    }
                }
            }

            if (info == null) return null;

            info.Lxrs = GetLxrs(info.GysId);

            if (info.LeiXing != EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物) info.HeTongs = GetHeTongs(info.GysId);

            switch (info.LeiXing)
            {
                case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.车队: 
                    info.CheXings = GetCheXings(info.GysId); 
                    break;
                case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物: 
                    info.GouWu = GetGouWu(info.GysId); 
                    break;
                case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点:
                    info.JingDians = GetJingDians(info.GysId, lng);
                    break;
                case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店:
                    info.JiuDian.BaoJias = GetJiuDianBaoJias(info.GysId);
                    info.JiuDian.FuJians = GetFuJians(info.GysId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.酒店图片);
                    break;
                case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.其他:
                    info.QiTa = GetQiTa(info.GysId);
                    break;
            }

            return info;
        }

        /// <summary>
        /// 更新供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Update(EyouSoft.Model.HGysStructure.MGysInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_Update");
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, info.GysId);
            _db.AddInParameter(cmd, "@LngType", DbType.Byte, info.LngType);
            _db.AddInParameter(cmd, "@GysName", DbType.String, info.GysName);
            _db.AddInParameter(cmd, "@LeiXing", DbType.Byte, info.LeiXing);
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "@XuKeZhengCode", DbType.String, info.XuKeZhengCode);
            _db.AddInParameter(cmd, "@FaRenName", DbType.String, info.FaRenName);
            _db.AddInParameter(cmd, "@FaRenTelephone", DbType.String, info.FaRenTelephone);
            _db.AddInParameter(cmd, "@Address", DbType.String, info.Address);
            _db.AddInParameter(cmd, "@JianJie", DbType.String, info.JianJie);
            _db.AddInParameter(cmd, "@BeiZhu", DbType.String, info.BeiZhu);
            _db.AddInParameter(cmd, "@IsQianDan", DbType.AnsiStringFixedLength, info.IsQianDan ? "1" : "0");
            _db.AddInParameter(cmd, "@IsTuiJian", DbType.AnsiStringFixedLength, info.IsTuiJian ? "1" : "0");
            _db.AddInParameter(cmd, "@IsFanDan", DbType.AnsiStringFixedLength, info.IsFanDan ? "1" : "0");
            _db.AddInParameter(cmd, "@IsMianYi", DbType.AnsiStringFixedLength, info.IsMianYi ? "1" : "0");
            _db.AddInParameter(cmd, "@IsHeTong", DbType.AnsiStringFixedLength, info.IsHeTong ? "1" : "0");
            _db.AddInParameter(cmd, "@JieSuanFangShi", DbType.Byte, info.JieSuanFangShi);
            _db.AddInParameter(cmd, "@GuaZhangQiXian", DbType.String, info.GuaZhangQiXian);
            _db.AddInParameter(cmd, "@WangZhi", DbType.String, info.WangZhi);
            _db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, info.IssueTime);
            if (info.JiuDian != null)
            {
                _db.AddInParameter(cmd, "@JiuDianQianTaiTelephone", DbType.String, info.JiuDian.QianTaiTelephone);
                _db.AddInParameter(cmd, "@JiuDianXingJi", DbType.Byte, info.JiuDian.XingJi);
            }
            if (info.CanGuan != null)
            {
                _db.AddInParameter(cmd, "@CanGuanCanBiao", DbType.String, info.CanGuan.CanBiao);
                _db.AddInParameter(cmd, "@CanGuanCaiXi", DbType.String, info.CanGuan.CaiXi);
            }
            _db.AddInParameter(cmd, "@CPCDXml", DbType.String, CreateCPCDXml(info.CPCD));
            _db.AddInParameter(cmd, "@JiuDianFuJianXml", DbType.String, CreateJiuDianFuJianXml(info.JiuDian));
            _db.AddInParameter(cmd, "@JiuDianBaoJiaXml", DbType.String, CreateJiuDianBaoJiaXml(info.JiuDian));
            _db.AddInParameter(cmd, "@HeTongXml", DbType.String, CreateHeTongXml(info.HeTongs));
            _db.AddInParameter(cmd, "@LxrXml", DbType.String, CreateLxrXml(info.Lxrs));
            _db.AddInParameter(cmd, "@JingDianXml", DbType.String, CreateJingDianXml(info.JingDians));
            _db.AddInParameter(cmd, "@JingDianFuJianXml", DbType.String, CreateJingDianFuJianXml(info.JingDians));
            _db.AddInParameter(cmd, "@CheXingXml", DbType.String, CreateCheXingXml(info.CheXings));
            _db.AddInParameter(cmd, "@GouWuXml", DbType.String, CreateGouWuXml(info.GouWu));
            _db.AddInParameter(cmd, "@GouWuChanPinXml", DbType.String, CreateGouWuChanPinXml(info.GouWu));
            _db.AddInParameter(cmd, "@GouWuHeTongXml", DbType.String, CreateGouWuHeTongXml(info.GouWu));
            _db.AddInParameter(cmd, "@QiTaXml", DbType.String, CreateQiTaXml(info.QiTa));
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
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Gys_Delete");
            _db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, gysId);
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
        /// 获取供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MLBInfo> GetGyss(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MLBChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MLBInfo> items = new List<EyouSoft.Model.HGysStructure.MLBInfo>();

            string tableName = "tbl_Source";
            string fields = "SourceId,Name,CountryId,ProvinceId,CityId,CountyId,IsTuiJian,IsQianDan,Type,JiuDianXingJi,QianTaiDianHua,CanGuanCaiXi,CanGuanCanBiao";
            fields += ",(SELECT SellType FROM tbl_SourceShop AS A1 WHERE A1.SourceId=tbl_Source.SourceId) AS GouWuShangPinLeiBie";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);
            sql.AppendFormat(" AND LngType={0} ", (int)EyouSoft.Model.EnumType.SysStructure.LngType.中文);
            #region SQL
            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND [Type]={0} ", (int)chaXun.LeiXing.Value);
                }
                if (chaXun.CityId.HasValue)
                {
                    sql.AppendFormat(" AND CityId={0} ", chaXun.CityId.Value);
                }
                if (chaXun.CountryId.HasValue)
                {
                    sql.AppendFormat(" AND CountryId={0} ", chaXun.CountryId.Value);
                }
                if (chaXun.DistrictId.HasValue)
                {
                    sql.AppendFormat(" AND CountyId={0} ", chaXun.DistrictId.Value);
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    sql.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.CanGuanCaiXi))
                {
                    sql.AppendFormat(" AND CanGuanCaiXi LIKE '%{0}%' ", chaXun.CanGuanCaiXi);
                }
                if (chaXun.JiuDianXingJi.HasValue)
                {
                    sql.AppendFormat(" AND JiuDianXingJi={0} ", (int)chaXun.JiuDianXingJi.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.JingDianName))
                {
                    sql.AppendFormat(" AND (Name LIKE '%{0}%' OR EXISTS(SELECT 1 FROM tbl_SourceJingDian AS A1 WHERE A1.SourceId=tbl_Source.SourceId AND A1.Name LIKE '%{0}%')) ", chaXun.JingDianName);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MLBInfo();                    
                    item.CPCD = new EyouSoft.Model.ComStructure.MCPCC();
                    item.CPCD.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CPCD.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD.DistrictId = rdr.GetInt32(rdr.GetOrdinal("CountyId"));
                    item.CPCD.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.GysId = rdr.GetString(rdr.GetOrdinal("SourceId"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsQianDan = rdr.GetString(rdr.GetOrdinal("IsQianDan")) == "1";
                    item.IsTuiJian = rdr.GetString(rdr.GetOrdinal("IsTuiJian")) == "1";
                    item.JiaoYiXX = null;                    
                    item.LeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)rdr.GetByte(rdr.GetOrdinal("Type"));
                    item.Lxrs = null;                    

                    if (item.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店)
                    {
                        item.JiuDianQianTaiTelephone = rdr["QianTaiDianHua"].ToString();
                        item.JiuDianXingJi = (EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi)rdr.GetByte(rdr.GetOrdinal("JiuDianXingJi"));
                    }

                    if (item.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆)
                    {
                        item.CanGuanCaiXi = rdr["CanGuanCaiXi"].ToString();
                        item.CanGuanCanBiao = rdr["CanGuanCanBiao"].ToString();
                    }

                    item.GouWuShangPinLeiBie = rdr["GouWuShangPinLeiBie"].ToString();

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetLxrs(item.GysId);
                    item.JiaoYiXX = GetJiaoYiXX(item.GysId);

                    if (item.LeiXing == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.车队) item.CheDuiCheXings = GetCheXings(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取选用的供应商信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> items = new List<EyouSoft.Model.HGysStructure.MXuanYongInfo>();

            string tableName = "tbl_Source";
            string fields = "SourceId,Name,IsTuiJian,IsQianDan,Type,JieSuanFangShi";
            string orderByString = "IssueTime DESC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (chaXun != null && chaXun.LngType.HasValue)
            {
                sql.AppendFormat(" AND LngType={0} ", (int)chaXun.LngType);
            }
            else
            {
                sql.AppendFormat(" AND LngType={0} ", (int)EyouSoft.Model.EnumType.SysStructure.LngType.中文);
            }

            #region SQL
            if (chaXun != null)
            {
                if (chaXun.LeiXing.HasValue)
                {
                    sql.AppendFormat(" AND [Type]={0} ", (int)chaXun.LeiXing.Value);
                }
                if (chaXun.CityId.HasValue)
                {
                    sql.AppendFormat(" AND CityId={0} ", chaXun.CityId.Value);
                }
                if (chaXun.CountryId.HasValue)
                {
                    sql.AppendFormat(" AND CountryId={0} ", chaXun.CountryId.Value);
                }
                if (chaXun.DistrictId.HasValue)
                {
                    sql.AppendFormat(" AND CountyId={0} ", chaXun.DistrictId.Value);
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    sql.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.GysName);
                }
                if (chaXun.JiuDianXingJi.HasValue&&chaXun.JiuDianXingJi.Value>0)
                {
                    sql.AppendFormat(" AND jiudianxingji ={0} ", chaXun.JiuDianXingJi.Value);
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MXuanYongInfo();

                    item.GysId = rdr["SourceId"].ToString();
                    item.GysLeiXing = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing) rdr.GetByte(rdr.GetOrdinal("Type"));
                    item.GysName = rdr["Name"].ToString();
                    item.IsQianDan = rdr["IsQianDan"].ToString() == "1";
                    item.IsTuiJian = rdr["IsTuiJian"].ToString() == "1";
                    item.Lxrs = null;
                    item.JieSuanFangShi = (EyouSoft.Model.EnumType.GysStructure.JieSuanFangShi)rdr.GetByte(rdr.GetOrdinal("JieSuanFangShi"));

                    items.Add(item);
                }
            }

            if (chaXun != null && chaXun.IsLxr && items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.Lxrs = GetLxrs(item.GysId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取选用的景点信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MXuanYongChaXunInfo chaXun)
        {
            IList<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo> items = new List<EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo>();

            string tableName = "view_Gys_JingDian";
            string fields = "*";
            string orderByString = "IdentityId ASC";
            string sumString = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" CompanyId='{0}' ", companyId);

            if (chaXun != null && chaXun.LngType.HasValue)
            {
                sql.AppendFormat(" AND LngType={0} ", (int)chaXun.LngType);
            }
            else
            {
                sql.AppendFormat(" AND LngType={0} ", (int)EyouSoft.Model.EnumType.SysStructure.LngType.中文);
            }

            if (chaXun != null)
            {
                if (!string.IsNullOrEmpty(chaXun.GysName))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%' ", chaXun.GysName);
                }
                if (!string.IsNullOrEmpty(chaXun.JingDianName))
                {
                    sql.AppendFormat(" AND JingDianName LIKE '%{0}%' ", chaXun.JingDianName);
                }
                if (chaXun.ProvinceId.HasValue)
                {
                    sql.AppendFormat(" AND ProvinceId={0} ", chaXun.ProvinceId.Value);
                }
                if (chaXun.CityId.HasValue)
                {
                    sql.AppendFormat(" AND CityId={0} ", chaXun.CityId.Value);
                }
                if (chaXun.CountryId.HasValue)
                {
                    sql.AppendFormat(" AND CountryId={0} ", chaXun.CountryId.Value);
                }
                if (chaXun.DistrictId.HasValue)
                {
                    sql.AppendFormat(" AND CountyId={0} ", chaXun.DistrictId.Value);
                }
                if (chaXun.CityIds != null && chaXun.CityIds.Length > 0)
                {
                    sql.AppendFormat(" AND CityId IN ({0}) ", Utils.GetSqlIn<int>(chaXun.CityIds));
                }
                if (chaXun.AreaIds != null && chaXun.AreaIds.Length > 0)
                {
                    sql.AppendFormat(" AND CountyId IN ({0}) ", Utils.GetSqlIn<int>(chaXun.AreaIds));
                }

            }

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MXuanYongJingDianInfo();

                    item.JingDianId = rdr.GetString(rdr.GetOrdinal("JingDianId"));
                    item.MiaoShu = rdr["JingDianMiaoShu"].ToString();
                    item.Name = rdr["JingDianName"].ToString();
                    item.IsTuiJian = rdr["IsTuiJian"].ToString() == "1";

                    items.Add(item);
                }
            }

            if (chaXun != null && chaXun.IsJingDianFuJian && items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    var fujians = GetFuJians(item.JingDianId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.景点图片);
                    if (fujians != null && fujians.Count > 0) item.FuJian = fujians[0];
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商交易明细信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="heJi">合计信息[0:数量:int][1:结算金额:decimal][2:已支付金额:decimal]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HGysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi)
        {
            heJi = new object[] { 0, 0M, 0M };
            IList<EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo> items = new List<EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo>();
            string tableName = "view_Gys_JiaoYiMingXi";
            string fields = "*";
            string orderByString = "LDate DESC";
            string sumString = "SUM(ShuLiang) AS ShuLiangHeJi,SUM(JinE) AS JinEHeJi,SUM(YiZhiFuJinE) AS YiZhiFuJinEHeJi";
            StringBuilder sql = new StringBuilder();

            #region sql
            sql.AppendFormat(" CompanyId='{0}' ", companyId);
            sql.AppendFormat(" AND GysId='{0}' ", gysId);
            if (chaXun != null)
            {
                if (chaXun.LEDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate<'{0}' ", chaXun.LEDate.Value.AddDays(1));
                }
                if (chaXun.LSDate.HasValue)
                {
                    sql.AppendFormat(" AND LDate>'{0}' ", chaXun.LSDate.Value.AddDays(-1));
                }
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader1(_db, pageSize, pageIndex, ref recordCount, tableName, fields, sql.ToString(), orderByString, sumString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MJiaoYiMingXiInfo();
                    
                    item.DaoYouname = string.Empty;
                    item.FeiYongMingXi = rdr["FeiYongMingXi"].ToString();
                    item.JiDiaoYuanName = rdr["JiDiaoYuanName"].ToString();
                    item.JinE = rdr.GetDecimal(rdr.GetOrdinal("JinE"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.ShuLiang = rdr.GetInt32(rdr.GetOrdinal("ShuLiang"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.XiaoShouYuanName = rdr["XiaoShouYuanName"].ToString();
                    item.YiZhiFuJinE = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinE"));

                    string xml = rdr["DaoYouXml"].ToString();
                    if (!string.IsNullOrEmpty(xml))
                    {
                        var xroot = XElement.Parse(xml);
                        var xrows = Utils.GetXElements(xroot, "row");
                        foreach (var xrow in xrows)
                        {
                            string _name = Utils.GetXAttributeValue(xrow, "Name") + ",";
                            if (item.DaoYouname.IndexOf(_name) > -1) continue;

                            item.DaoYouname = item.DaoYouname + _name;
                        }
                        if (!string.IsNullOrEmpty(item.DaoYouname)) item.DaoYouname = item.DaoYouname.TrimEnd(',');
                    }

                    item.JiDiaoYuanName = rdr["JiDiaoYuanName"].ToString();

                    items.Add(item);
                }

                rdr.NextResult();
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ShuLiangHeJi"))) heJi[0] = rdr.GetInt32(rdr.GetOrdinal("ShuLiangHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("JinEHeJi"))) heJi[1] = rdr.GetDecimal(rdr.GetOrdinal("JinEHeJi"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("YiZhiFuJinEHeJi"))) heJi[2] = rdr.GetDecimal(rdr.GetOrdinal("YiZhiFuJinEHeJi"));
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.GysStructure.GysLeiXing? GetGysLeiXing(string gysId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetGysLeiXing);
            _db.AddInParameter(cmd, "@GysId", DbType.AnsiStringFixedLength, gysId);
            _db.AddInParameter(cmd, "@LngType", DbType.Byte, EyouSoft.Model.EnumType.SysStructure.LngType.中文);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)rdr.GetByte(0);
                }
            }

            return null;
        }

        /// <summary>
        /// 获取购物店信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaIds">县区编号集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> GetXuanYongGouWuDians(string companyId, int[] areaIds)
        {
            IList<EyouSoft.Model.HGysStructure.MXuanYongInfo> items = new List<EyouSoft.Model.HGysStructure.MXuanYongInfo>();
            string sql = "SELECT tbl_Source.SourceId,[Name],GlideMoney,PeopleMoney,ChildMoney FROM [tbl_Source] LEFT OUTER JOIN tbl_SourceShop ON tbl_Source.SourceId=tbl_SourceShop.SourceId WHERE [Type]=@Type AND [CompanyId]=@CompanyId AND [LngType]=@LngType AND [IsDelete]='0' ";
            if (areaIds != null && areaIds.Length > 0) sql += string.Format(" AND [CountyId] IN({0}) ", Utils.GetSqlIn<int>(areaIds));
            sql += " ORDER BY tbl_Source.IdentityId DESC ";

            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "Type", DbType.Byte, EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, EyouSoft.Model.EnumType.SysStructure.LngType.中文);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.HGysStructure.MXuanYongInfo();

                    item.GysId = rdr["SourceId"].ToString();
                    item.GysName = rdr["Name"].ToString();
                    item.LiuShui = rdr.GetDecimal(rdr.GetOrdinal("GlideMoney"));
                    item.PeopleMoney = rdr.GetDecimal(rdr.GetOrdinal("PeopleMoney"));
                    item.ChildMoney = rdr.GetDecimal(rdr.GetOrdinal("ChildMoney"));

                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取景点信息业务实体
        /// </summary>
        /// <param name="jingDingId">景点编号</param>
        /// <param name="lng">语言</param>
        /// <returns></returns>
        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDingId, EyouSoft.Model.EnumType.SysStructure.LngType lng)
        {
            EyouSoft.Model.HGysStructure.MJingDianJingDianInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJingDianInfo);
            _db.AddInParameter(cmd, "JingDianId", DbType.AnsiStringFixedLength, jingDingId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lng);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MJingDianJingDianInfo();
                    item.FuJian = null;
                    item.IsTuiJian = rdr["IsDefault"].ToString() == "1";
                    item.IsXiu = rdr["IsXiu"].ToString() == "1";
                    item.JianJie = rdr["GuideWord"].ToString();
                    item.JingDianId = rdr["SightId"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.Telephone = rdr["SpotTel"].ToString();
                    item.WangZhi = rdr["SpotURL"].ToString();
                    item.XingJi = (EyouSoft.Model.EnumType.GysStructure.JingDianXingJi)rdr.GetByte(rdr.GetOrdinal("Star"));
                    item.YouLanShiJian = rdr["SpotTime"].ToString();
                }
            }

            if (item != null)
            {
                var fujians = GetFuJians(item.JingDianId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.景点图片);
                if (fujians != null && fujians.Count > 0) item.FuJian = fujians[0];
            }

            return item;
        }

        public EyouSoft.Model.HGysStructure.MJingDianJingDianInfo GetJingDianInfo(string jingDianId, EyouSoft.Model.EnumType.SysStructure.LngType lng, string cityid)
        {
            EyouSoft.Model.HGysStructure.MJingDianJingDianInfo item = null;
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetJingDianInfo_bycity);
            _db.AddInParameter(cmd, "JingDianId", DbType.AnsiStringFixedLength, jingDianId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lng);
            _db.AddInParameter(cmd, "CountyId", DbType.AnsiStringFixedLength, cityid);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    item = new EyouSoft.Model.HGysStructure.MJingDianJingDianInfo();
                    item.FuJian = null;
                    item.IsTuiJian = rdr["IsTuiJian"].ToString() == "1";
                    item.JianJie = rdr["JingDianMiaoShu"].ToString();
                    item.JingDianId = rdr["JingDianId"].ToString();
                    item.Name = rdr["JingDianName"].ToString();
                    item.Telephone = rdr["SpotTel"].ToString();
                    item.WangZhi = rdr["SpotURL"].ToString();
                    item.XingJi = (EyouSoft.Model.EnumType.GysStructure.JingDianXingJi)rdr.GetByte(rdr.GetOrdinal("Star"));
                    item.YouLanShiJian = rdr["SpotTime"].ToString();
                }
            }

            if (item != null)
            {
                var fujians = GetFuJians(item.JingDianId, EyouSoft.Model.EnumType.GysStructure.GysFuJianLeiXing.景点图片);
                if (fujians != null && fujians.Count > 0) item.FuJian = fujians[0];
            }

            return item;
        }
        #endregion
    }
}
