using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.HTourStructure
{
    using EyouSoft.Model.EnumType.GysStructure;
    using EyouSoft.Model.EnumType.TourStructure;

    /// <summary>
    /// 报价
    /// </summary>
    public class DQuote : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.HTourStructure.IQuote
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DQuote()
        {
            this._db = base.SystemStore;
        }
        #endregion

        /// <summary>
        /// 验证是否存在相同的询价编号
        /// </summary>
        /// <param name="BuyId">询价编号</param>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        public bool isExist(string BuyId, string QuoteId)
        {
            DbCommand cmd = _db.GetSqlStringCommand("SELECT 1");
            string cmdText = "SELECT COUNT(*) FROM view_Quote WHERE BuyId=@BuyId AND IsDelete=0";
            _db.AddInParameter(cmd, "BuyId", DbType.String, BuyId);
            if (!string.IsNullOrEmpty(QuoteId))
            {
                cmdText += " AND QuoteId<>@QuoteId AND QuoteId NOT IN (SELECT CASE WHEN ISNULL(ParentId,'0')<>'0' THEN ParentId ELSE QuoteId END FROM dbo.view_Quote WHERE QuoteId=@QuoteId) AND ParentId NOT IN (SELECT CASE WHEN ISNULL(ParentId,'0')<>'0' THEN ParentId ELSE QuoteId END FROM dbo.view_Quote WHERE QuoteId=@QuoteId)";
                _db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            }
            else
            {
                cmdText += " AND QuoteId NOT IN (";
                cmdText += " SELECT  CASE WHEN ISNULL(v.ParentId, '0') <> '0' THEN v.ParentId";
                cmdText += "         END";
                cmdText += " FROM    dbo.view_Quote v";
                cmdText += " WHERE   v.BuyId = dbo.view_Quote.BuyId AND v.QuoteId = dbo.view_Quote.QuoteId)";
                cmdText += " AND ParentId NOT IN (";
                cmdText += " SELECT  CASE WHEN ISNULL(v.ParentId, '0') <> '0' THEN v.ParentId";
                cmdText += "         END";
                cmdText += " FROM    dbo.view_Quote v";
                cmdText += " WHERE   v.BuyId = dbo.view_Quote.BuyId AND v.QuoteId = dbo.view_Quote.QuoteId)";
            }

            cmd.CommandText = cmdText;
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:添加成功 2：报价成功 0：操作失败</returns>
        public int AddQuote(EyouSoft.Model.HTourStructure.MQuote model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Quote_Add");
            _db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, model.QuoteId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, model.ParentId);
            _db.AddInParameter(cmd, "TourMode", DbType.Byte, (int)model.TourMode);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)model.TourType);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, model.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            _db.AddInParameter(cmd, "Contact", DbType.String, model.Contact);
            _db.AddInParameter(cmd, "Phone", DbType.String, model.Phone);
            _db.AddInParameter(cmd, "Fax", DbType.String, model.Fax);
            _db.AddInParameter(cmd, "BuyTime", DbType.DateTime, model.BuyTime);
            //_db.AddInParameter(cmd, "BuyId", DbType.String, model.BuyId);
            //_db.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);

            _db.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            _db.AddInParameter(cmd, "Days", DbType.Int32, model.Days);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, model.CountryId);
            _db.AddInParameter(cmd, "StartEffectTime", DbType.DateTime, model.StartEffectTime);
            _db.AddInParameter(cmd, "EndEffectTime", DbType.DateTime, model.EndEffectTime);
            _db.AddInParameter(cmd, "ArriveCity", DbType.String, model.ArriveCity);
            _db.AddInParameter(cmd, "ArriveCityFlight", DbType.String, model.ArriveCityFlight);
            _db.AddInParameter(cmd, "LeaveCity", DbType.String, model.LeaveCity);
            _db.AddInParameter(cmd, "LeaveCityFlight", DbType.String, model.LeaveCityFlight);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, model.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, model.SellerName);
            _db.AddInParameter(cmd, "SellerDeptId", DbType.Int32, model.SellerDeptId);
            _db.AddInParameter(cmd, "MaxAdults", DbType.Int32, model.MaxAdults);
            _db.AddInParameter(cmd, "MinAdults", DbType.Int32, model.MinAdults);
            _db.AddInParameter(cmd,"jiudianxingji",DbType.Byte,(int)model.JiuDianXingJi);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)model.OutQuoteType);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, model.QuoteRemark);
            _db.AddInParameter(cmd, "SpecificRequire", DbType.String, model.SpecificRequire);
            _db.AddInParameter(cmd, "TravelNote", DbType.String, model.TravelNote);
            _db.AddInParameter(cmd, "QuoteStatus", DbType.Byte, (int)model.QuoteStatus);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);
            //行程亮点、行程备注、报价备注编号(用于语言选择)
            _db.AddInParameter(cmd, "QuoteJourney", DbType.Xml, CreateQuoteJourneyXml(model.QuoteJourneyList));
            //文件
            _db.AddInParameter(cmd, "QuoteFile", DbType.Xml, CreateQuoteFileXml(model.QuoteFileList, model.QuoteId));
            //行程
            _db.AddInParameter(cmd, "QuotePlan", DbType.Xml, CreateQuotePlanXml(model.QuotePlanList, model.QuoteId));
            _db.AddInParameter(cmd, "QuotePlanCity", DbType.Xml, CreateQuotePlanCityXml(model.QuotePlanList));
            _db.AddInParameter(cmd, "QuotePlanShop", DbType.Xml, CreateQuotePlanShopXml(model.QuotePlanList));
            _db.AddInParameter(cmd, "QuotePlanSpot", DbType.Xml, CreateQuotePlanSpotXml(model.QuotePlanList));
            //购物
            _db.AddInParameter(cmd, "QuoteShop", DbType.Xml, CreateQuoteShopXml(model.QuoteShopList, model.QuoteId));
            //风味餐
            _db.AddInParameter(cmd, "QuoteFoot", DbType.Xml, CreateQuoteFootXml(model.QuoteFootList, model.QuoteId));
            //自费项目
            _db.AddInParameter(cmd, "QuoteSelfPay", DbType.Xml, CreateQuoteSelfPayXml(model.QuoteSelfPayList, model.QuoteId));
            //赠送
            _db.AddInParameter(cmd, "QuoteGive", DbType.Xml, CreateQuoteGiveXml(model.QuoteGiveList, model.QuoteId));
            //小费
            _db.AddInParameter(cmd, "QuoteTip", DbType.Xml, CreateQuoteTipXml(model.QuoteTipList, model.QuoteId));
            //成本价格项目信息
            _db.AddInParameter(cmd, "QuoteCost", DbType.Xml, CreateQuoteCostXml(model.QuoteCostList, model.QuoteId));
            //价格信息
            _db.AddInParameter(cmd, "QuotePrice", DbType.Xml, CreateQuotePriceXml(model.QuotePriceList, model.QuoteId));
            //报价成功团队信息
            _db.AddInParameter(cmd, "QuoteTour", DbType.Xml, CreateQuoteTourXml(model.QuoteTour));
            //报价成功——计调员信息
            _db.AddInParameter(cmd, "QuoteTourPlaner", DbType.Xml, CreateQuoteTourPlanerXml(model.QuoteTour));
            //报价成功——地接社信息
            _db.AddInParameter(cmd, "QuoteTourDiJie", DbType.Xml, CreateTourDiJie(model.QuoteTour));
            //报价成功——用房数
            _db.AddInParameter(cmd, "QuoteTourRoom", DbType.Xml, CreateQuoteTourRoomXml(model.QuoteTour));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 修改报价
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:修改成功 2：报价成功 0：操作失败</returns>
        public int UpdateQuote(EyouSoft.Model.HTourStructure.MQuote model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Quote_Update");
            _db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, model.QuoteId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, model.ParentId);
            _db.AddInParameter(cmd, "TourMode", DbType.Byte, (int)model.TourMode);
            _db.AddInParameter(cmd, "TourType", DbType.Byte, (int)model.TourType);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, model.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            _db.AddInParameter(cmd, "Contact", DbType.String, model.Contact);
            _db.AddInParameter(cmd, "Phone", DbType.String, model.Phone);
            _db.AddInParameter(cmd, "Fax", DbType.String, model.Fax);
            _db.AddInParameter(cmd, "BuyTime", DbType.DateTime, model.BuyTime);
            //_db.AddInParameter(cmd, "BuyId", DbType.String, model.BuyId);
            //_db.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            _db.AddInParameter(cmd, "Days", DbType.Int32, model.Days);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, model.CountryId);
            _db.AddInParameter(cmd, "StartEffectTime", DbType.DateTime, model.StartEffectTime);
            _db.AddInParameter(cmd, "EndEffectTime", DbType.DateTime, model.EndEffectTime);
            _db.AddInParameter(cmd, "ArriveCity", DbType.String, model.ArriveCity);
            _db.AddInParameter(cmd, "ArriveCityFlight", DbType.String, model.ArriveCityFlight);
            _db.AddInParameter(cmd, "LeaveCity", DbType.String, model.LeaveCity);
            _db.AddInParameter(cmd, "LeaveCityFlight", DbType.String, model.LeaveCityFlight);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, model.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, model.SellerName);
            _db.AddInParameter(cmd, "SellerDeptId", DbType.Int32, model.SellerDeptId);
            _db.AddInParameter(cmd, "MaxAdults", DbType.Int32, model.MaxAdults);
            _db.AddInParameter(cmd, "MinAdults", DbType.Int32, model.MinAdults);
            _db.AddInParameter(cmd, "jiudianxingji", DbType.Byte, (int)model.JiuDianXingJi);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, model.JourneySpot);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)model.OutQuoteType);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, model.QuoteRemark);
            _db.AddInParameter(cmd, "SpecificRequire", DbType.String, model.SpecificRequire);
            _db.AddInParameter(cmd, "TravelNote", DbType.String, model.TravelNote);
            _db.AddInParameter(cmd, "QuoteStatus", DbType.Byte, (int)model.QuoteStatus);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, model.OperatorDeptId);

            //取消报价的原因
            _db.AddInParameter(cmd, "CancelReason", DbType.String, model.CancelReason);

            //行程亮点、行程备注、报价备注编号(用于语言选择)
            _db.AddInParameter(cmd, "QuoteJourney", DbType.Xml, CreateQuoteJourneyXml(model.QuoteJourneyList));

            //文件
            _db.AddInParameter(cmd, "QuoteFile", DbType.Xml, CreateQuoteFileXml(model.QuoteFileList, model.QuoteId));
            //行程
            _db.AddInParameter(cmd, "QuotePlan", DbType.Xml, CreateQuotePlanXml(model.QuotePlanList, model.QuoteId));
            _db.AddInParameter(cmd, "QuotePlanCity", DbType.Xml, CreateQuotePlanCityXml(model.QuotePlanList));
            _db.AddInParameter(cmd, "QuotePlanShop", DbType.Xml, CreateQuotePlanShopXml(model.QuotePlanList));
            _db.AddInParameter(cmd, "QuotePlanSpot", DbType.Xml, CreateQuotePlanSpotXml(model.QuotePlanList));
            //购物
            _db.AddInParameter(cmd, "QuoteShop", DbType.Xml, CreateQuoteShopXml(model.QuoteShopList, model.QuoteId));
            //风味餐
            _db.AddInParameter(cmd, "QuoteFoot", DbType.Xml, CreateQuoteFootXml(model.QuoteFootList, model.QuoteId));
            //自费项目
            _db.AddInParameter(cmd, "QuoteSelfPay", DbType.Xml, CreateQuoteSelfPayXml(model.QuoteSelfPayList, model.QuoteId));
            //赠送
            _db.AddInParameter(cmd, "QuoteGive", DbType.Xml, CreateQuoteGiveXml(model.QuoteGiveList, model.QuoteId));
            //小费
            _db.AddInParameter(cmd, "QuoteTip", DbType.Xml, CreateQuoteTipXml(model.QuoteTipList, model.QuoteId));
            //成本价格项目信息
            _db.AddInParameter(cmd, "QuoteCost", DbType.Xml, CreateQuoteCostXml(model.QuoteCostList, model.QuoteId));
            //价格信息
            _db.AddInParameter(cmd, "QuotePrice", DbType.Xml, CreateQuotePriceXml(model.QuotePriceList, model.QuoteId));


            //报价成功团队信息
            _db.AddInParameter(cmd, "QuoteTour", DbType.Xml, CreateQuoteTourXml(model.QuoteTour));
            //报价成功——计调员信息
            _db.AddInParameter(cmd, "QuoteTourPlaner", DbType.Xml, CreateQuoteTourPlanerXml(model.QuoteTour));
            //报价成功——地接社信息
            _db.AddInParameter(cmd, "QuoteTourDiJie", DbType.Xml, CreateTourDiJie(model.QuoteTour));
            //报价成功——用房数
            _db.AddInParameter(cmd, "QuoteTourRoom", DbType.Xml, CreateQuoteTourRoomXml(model.QuoteTour));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteIds">报价编号</param>
        /// <returns></returns>
        public bool DeleteQuote(string QuoteIds)
        {
            string SQL_UPDATE_DelQuote = "update tbl_Quote set IsDelete=1 where (ParentId in(select DISTINCT ParentId from tbl_Quote where CHARINDEX(QuoteId,@QuoteIds,0)>0 and ParentId<>'0') or QuoteId in(select DISTINCT ParentId from tbl_Quote where CHARINDEX(QuoteId,@QuoteIds,0)>0  and ParentId<>'0') or CHARINDEX(ParentId,@QuoteIds,0)>0 OR CHARINDEX(QuoteId,@QuoteIds,0)>0) and QuoteStatus<>3";
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_DelQuote);
            this._db.AddInParameter(cmd, "QuoteIds", DbType.String, QuoteIds);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MQuote GetQuoteModel(string QuoteId)
        {

            EyouSoft.Model.HTourStructure.MQuote model = null;

            StringBuilder query = new StringBuilder();
            query.Append(" SELECT * FROM  view_Quote WHERE QuoteId=@QuoteId ");

            DbCommand cmd = this._db.GetSqlStringCommand(query.ToString());

            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HTourStructure.MQuote();
                    model.QuoteId = dr.GetString(dr.GetOrdinal("QuoteId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.ParentId = dr.GetString(dr.GetOrdinal("ParentId"));
                    model.TourMode = (EyouSoft.Model.EnumType.TourStructure.TourMode)dr.GetByte(dr.GetOrdinal("TourMode"));
                    model.TourType = (TourType)dr.GetByte(dr.GetOrdinal("TourType"));
                    model.BuyCompanyID = dr.GetString(dr.GetOrdinal("BuyCompanyID"));
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : string.Empty;
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                    model.Phone = !dr.IsDBNull(dr.GetOrdinal("Phone")) ? dr.GetString(dr.GetOrdinal("Phone")) : string.Empty;
                    model.Fax = !dr.IsDBNull(dr.GetOrdinal("Fax")) ? dr.GetString(dr.GetOrdinal("Fax")) : string.Empty;
                    model.BuyTime = dr.GetDateTime(dr.GetOrdinal("BuyTime"));
                    model.BuyId = !dr.IsDBNull(dr.GetOrdinal("BuyId")) ? dr.GetString(dr.GetOrdinal("BuyId")) : string.Empty;
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : string.Empty;
                    model.Days = dr.GetInt32(dr.GetOrdinal("Days"));
                    model.CountryId = dr.GetInt32(dr.GetOrdinal("CountryId"));
                    model.StartEffectTime = dr.GetDateTime(dr.GetOrdinal("StartEffectTime"));
                    model.EndEffectTime = dr.GetDateTime(dr.GetOrdinal("EndEffectTime"));
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : string.Empty;
                    model.ArriveCityFlight = !dr.IsDBNull(dr.GetOrdinal("ArriveCityFlight")) ? dr.GetString(dr.GetOrdinal("ArriveCityFlight")) : string.Empty;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : string.Empty;
                    model.LeaveCityFlight = !dr.IsDBNull(dr.GetOrdinal("LeaveCityFlight")) ? dr.GetString(dr.GetOrdinal("LeaveCityFlight")) : string.Empty;
                    model.SellerId = !dr.IsDBNull(dr.GetOrdinal("SellerId")) ? dr.GetString(dr.GetOrdinal("SellerId")) : string.Empty;
                    model.SellerName = !dr.IsDBNull(dr.GetOrdinal("SellerName")) ? dr.GetString(dr.GetOrdinal("SellerName")) : string.Empty;
                    model.SellerDeptId = dr.GetInt32(dr.GetOrdinal("SellerDeptId"));
                    model.MaxAdults = dr.GetInt32(dr.GetOrdinal("MaxAdults"));
                    model.MinAdults = dr.GetInt32(dr.GetOrdinal("MinAdults"));
                    model.JourneySpot = !dr.IsDBNull(dr.GetOrdinal("JourneySpot")) ? dr.GetString(dr.GetOrdinal("JourneySpot")) : string.Empty;
                    model.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)dr.GetByte(dr.GetOrdinal("OutQuoteType"));
                    model.QuoteRemark = !dr.IsDBNull(dr.GetOrdinal("QuoteRemark")) ? dr.GetString(dr.GetOrdinal("QuoteRemark")) : string.Empty;
                    model.SpecificRequire = !dr.IsDBNull(dr.GetOrdinal("SpecificRequire")) ? dr.GetString(dr.GetOrdinal("SpecificRequire")) : string.Empty;
                    model.TravelNote = !dr.IsDBNull(dr.GetOrdinal("TravelNote")) ? dr.GetString(dr.GetOrdinal("TravelNote")) : string.Empty;
                    model.TimeCount = dr.GetInt32(dr.GetOrdinal("TimeCount"));
                    model.QuoteStatus = (EyouSoft.Model.EnumType.TourStructure.QuoteState)dr.GetByte(dr.GetOrdinal("QuoteStatus"));
                    model.CancelReason = !dr.IsDBNull(dr.GetOrdinal("CancelReason")) ? dr.GetString(dr.GetOrdinal("CancelReason")) : string.Empty;
                    model.OperatorId = !dr.IsDBNull(dr.GetOrdinal("OperatorId")) ? dr.GetString(dr.GetOrdinal("OperatorId")) : string.Empty;
                    model.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                    model.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    model.IsLatest = dr.GetString(dr.GetOrdinal("IsLatest")) == "1";
                    model.UpdateTime = dr.GetDateTime(dr.GetOrdinal("UpdateTime"));
                    model.JiuDianXingJi = (JiuDianXingJi)dr.GetByte(dr.GetOrdinal("jiudianxingji"));

                    //获取 行程亮点、行程备注、报价备注编号(用于语言选择)
                    model.QuoteJourneyList = !dr.IsDBNull(dr.GetOrdinal("QuoteJourney")) ? GetQuoteJourney(dr.GetString(dr.GetOrdinal("QuoteJourney"))) : null;

                    //报价文件的信息
                    model.QuoteFileList = !dr.IsDBNull(dr.GetOrdinal("QuoteFile")) ? GetQuoteFileList(dr.GetString(dr.GetOrdinal("QuoteFile"))) : null;
                    //报价行程信息
                    model.QuotePlanList = !dr.IsDBNull(dr.GetOrdinal("QuotePlan")) ? GetQuotePlan(dr.GetString(dr.GetOrdinal("QuotePlan"))) : null;


                    //报价购物信息
                    model.QuoteShopList = !dr.IsDBNull(dr.GetOrdinal("QuoteShop")) ? GetQuoteShopList(dr.GetString(dr.GetOrdinal("QuoteShop"))) : null;
                    //报价风味餐
                    model.QuoteFootList = !dr.IsDBNull(dr.GetOrdinal("QuoteFoot")) ? GetQuoteFootList(dr.GetString(dr.GetOrdinal("QuoteFoot"))) : null;
                    //报价赠送
                    model.QuoteGiveList = !dr.IsDBNull(dr.GetOrdinal("QuoteGive")) ? GetQuoteGiveList(dr.GetString(dr.GetOrdinal("QuoteGive"))) : null;
                    //小费
                    model.QuoteTipList = !dr.IsDBNull(dr.GetOrdinal("QuoteTip")) ? GetQuoteTipList(dr.GetString(dr.GetOrdinal("QuoteTip"))) : null;
                    //自费项目
                    model.QuoteSelfPayList = !dr.IsDBNull(dr.GetOrdinal("QuoteSelfPay")) ? GetQuoteSelfPayList(dr.GetString(dr.GetOrdinal("QuoteSelfPay"))) : null;
                    //成本
                    model.QuoteCostList = !dr.IsDBNull(dr.GetOrdinal("QuoteCost")) ? GetQuoteCostList(dr.GetString(dr.GetOrdinal("QuoteCost"))) : null;
                    //价格
                    model.QuotePriceList = !dr.IsDBNull(dr.GetOrdinal("QuotePrice")) ? GetQuotePriceList(dr.GetString(dr.GetOrdinal("QuotePrice"))) : null;


                }

            }

            return model;
        }


        /// <summary>
        /// 获取报价的分页列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteInfo> GetQuoteList(
            int pageSize,
            int pageIndex,
            ref int recordCount,
            EyouSoft.Model.HTourStructure.MQuoteSearch search)
        {

            IList<EyouSoft.Model.HTourStructure.MQuoteInfo> list = new List<EyouSoft.Model.HTourStructure.MQuoteInfo>();

            StringBuilder fields = new StringBuilder();
            fields.Append("QuoteId,RouteName,StartEffectTime,EndEffectTime,ArriveCity,LeaveCity,BuyCompanyName,Contact,Phone,Fax,ParentId,AreaId, ");
            fields.Append(" (select top 1 * from tbl_CrmLinkman ");
            fields.Append(" where TypeId=tbl_Quote.BuyCompanyID ORDER BY SortId for xml raw,root('root')) as CrmLinkman,");
            fields.Append("(select * from tbl_QuotePrice where QuoteId=tbl_Quote.QuoteId for xml raw,root('root') )as QuotePrice,");
            fields.Append("Days,MaxAdults,MinAdults,BuyTime,TimeCount,QuoteStatus,CancelReason");

            string tableName = "tbl_Quote";

            string orderByString = " UpdateTime desc ";

            StringBuilder query = new StringBuilder();
            query.AppendFormat("  IsLatest='1' and IsDelete='0' and companyid='{0}' ", search.CompanyId);

            if (search != null)
            {

                if (search.AreaId.HasValue)
                {
                    query.AppendFormat(" and  AreaId={0} ", search.AreaId.Value);
                }

                if (!string.IsNullOrEmpty(search.RouteName))
                {

                    query.AppendFormat(" and RouteName like '%{0}%' ", search.RouteName);
                }

                if (!string.IsNullOrEmpty(search.BuyCompanyID))
                {
                    query.AppendFormat(" and BuyCompanyID  = '{0}' ", search.BuyCompanyID);
                }

                else if (!string.IsNullOrEmpty(search.BuyCompanyName))
                {
                    query.AppendFormat(" and BuyCompanyName   like '%{0}%' ", search.BuyCompanyName);
                }

                if (!string.IsNullOrEmpty(search.Contact))
                {
                    query.AppendFormat(" and Contact    like '%{0}%' ", search.Contact);
                }

                if (!string.IsNullOrEmpty(search.BuyId))
                {
                    query.AppendFormat(" and BuyId   like '%{0}%' ", search.BuyId);
                }

                if (search.BeginBuyTime.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',BuyTime)>=0 ", search.BeginBuyTime.Value);
                }

                if (search.EndBuyTime.HasValue)
                {
                    query.AppendFormat(" and datediff(day,'{0}',BuyTime)<=0", search.EndBuyTime.Value);
                }

                if (search.BeginTourTime.HasValue)
                {
                    query.AppendFormat(" and exists(select 1 from tbl_tour where tourid= tbl_Quote.tourid and datediff(day,'{0}',issuetime)>=0) ", search.BeginTourTime.Value);
                }

                if (search.EndTourTime.HasValue)
                {
                    query.AppendFormat(" and exists(select 1 from tbl_tour where tourid= tbl_Quote.tourid and datediff(day,'{0}',issuetime)<=0) ", search.EndTourTime.Value);
                }

                if (search.MinAdults.HasValue)
                {
                    query.AppendFormat(" and ( MinAdults<={0} and MaxAdults>={1} )", search.MinAdults.Value, search.MinAdults.Value);
                }

                if (search.MaxAdults.HasValue)
                {

                    query.AppendFormat(" and (MinAdults<={0} and MaxAdults>={1} )", search.MaxAdults.Value, search.MaxAdults.Value);
                }

                if (!string.IsNullOrEmpty(search.SellerId))
                {
                    query.AppendFormat(" and SellerId='{0}' ", search.SellerId);
                }

                else if (!string.IsNullOrEmpty(search.SellerName))
                {
                    query.AppendFormat(" and SellerName like '%{0}%' ", search.SellerName);
                }

                if (search.QuoteStatus.HasValue)
                {

                    query.AppendFormat(" and  QuoteStatus={0} ", (int)search.QuoteStatus.Value);
                }

            }

            using (IDataReader dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, tableName, fields.ToString(), query.ToString(), orderByString, null))
            {

                while (dr.Read())
                {
                    EyouSoft.Model.HTourStructure.MQuoteInfo model = new EyouSoft.Model.HTourStructure.MQuoteInfo();
                    model.QuoteId = dr.GetString(dr.GetOrdinal("QuoteId"));
                    model.RouteName = !dr.IsDBNull(dr.GetOrdinal("RouteName")) ? dr.GetString(dr.GetOrdinal("RouteName")) : null;
                    model.StartEffectTime = dr.GetDateTime(dr.GetOrdinal("StartEffectTime"));
                    model.EndEffectTime = dr.GetDateTime(dr.GetOrdinal("EndEffectTime"));
                    model.ArriveCity = !dr.IsDBNull(dr.GetOrdinal("ArriveCity")) ? dr.GetString(dr.GetOrdinal("ArriveCity")) : null;
                    model.LeaveCity = !dr.IsDBNull(dr.GetOrdinal("LeaveCity")) ? dr.GetString(dr.GetOrdinal("LeaveCity")) : null;
                    model.BuyCompanyName = !dr.IsDBNull(dr.GetOrdinal("BuyCompanyName")) ? dr.GetString(dr.GetOrdinal("BuyCompanyName")) : null;
                    model.Contact = dr["Contact"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.CrmLinkman = !dr.IsDBNull(dr.GetOrdinal("CrmLinkman")) ? GetCrmLinkman(dr.GetString(dr.GetOrdinal("CrmLinkman")))[0] : null;
                    model.QuotePriceList = !dr.IsDBNull(dr.GetOrdinal("QuotePrice")) ? GetQuotePriceList(dr.GetString(dr.GetOrdinal("QuotePrice"))) : null;
                    model.Days = dr.GetInt32(dr.GetOrdinal("Days"));
                    model.MaxAdults = dr.GetInt32(dr.GetOrdinal("MaxAdults"));
                    model.MinAdults = dr.GetInt32(dr.GetOrdinal("MinAdults"));
                    model.BuyTime = dr.GetDateTime(dr.GetOrdinal("BuyTime"));
                    model.TimeCount = dr.GetInt32(dr.GetOrdinal("TimeCount"));
                    model.QuoteStatus = (EyouSoft.Model.EnumType.TourStructure.QuoteState)dr.GetByte(dr.GetOrdinal("QuoteStatus"));

                    model.CancelReason = !dr.IsDBNull(dr.GetOrdinal("CancelReason")) ? dr.GetString(dr.GetOrdinal("CancelReason")) : string.Empty;
                    model.ParentId = dr.GetString(dr.GetOrdinal("ParentId"));
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaId"));

                    list.Add(model);
                }
            }

            return list;

        }


        /// <summary>
        /// 获取报价比较
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteCompare> GetQuoteCompareList(string ParentId)
        {

            IList<EyouSoft.Model.HTourStructure.MQuoteCompare> list = new List<EyouSoft.Model.HTourStructure.MQuoteCompare>();
            // "select QuoteId,TimeCount  from tbl_Quote where QuoteId=@ParentId or ParentId=@ParentId";
            string sql = "select QuoteId,TimeCount  from tbl_Quote where QuoteId=@QuoteId or ParentId=@QuoteId order by TimeCount asc ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, ParentId);
            this._db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, ParentId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HTourStructure.MQuoteCompare model = new EyouSoft.Model.HTourStructure.MQuoteCompare();
                    model.QuoteId = dr.GetString(dr.GetOrdinal("QuoteId"));
                    model.TimeCount = dr.GetInt32(dr.GetOrdinal("TimeCount"));
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取报价比较的价格条目信息
        /// </summary>
        /// <param name="QuoteIds"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MQuoteCost> GetQuoteCostList(string[] QuoteIds)
        {

            string ids = string.Empty;

            foreach (var id in QuoteIds)
            {
                ids += string.Format("'{0}'", id) + ",";
            }

            ids = ids.Substring(0, ids.Length - 1);

            IList<EyouSoft.Model.HTourStructure.MQuoteCost> list = new List<EyouSoft.Model.HTourStructure.MQuoteCost>();

            string sql = string.Format("select * from tbl_QuoteCost where QuoteId in ({0})", ids);

            DbCommand cmd = this._db.GetSqlStringCommand(sql);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HTourStructure.MQuoteCost model = new EyouSoft.Model.HTourStructure.MQuoteCost();

                    model.QuoteId = dr.GetString(dr.GetOrdinal("QuoteId"));
                    model.Pricetype = (EyouSoft.Model.EnumType.TourStructure.Pricetype)dr.GetByte(dr.GetOrdinal("Pricetype"));
                    model.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)dr.GetByte(dr.GetOrdinal("PriceUnit"));
                    model.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                    model.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                    model.CostMode = (EyouSoft.Model.EnumType.TourStructure.CostMode)dr.GetByte(dr.GetOrdinal("CostMode"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据报价编号、价格类型、项目类型获取报价差异信息
        /// </summary>
        /// <param name="quoteId">报价编号</param>
        /// <param name="costMode">价格类型</param>
        /// <param name="priceType">项目类型</param>
        /// <returns>报价差异信息</returns>
        public DataSet GetQuoteCompare(string quoteId,CostMode costMode,Pricetype priceType)
        {
            var dc = this._db.GetStoredProcCommand("proc_QuoteCompare");
            this._db.AddInParameter(dc, "QuoteId", DbType.AnsiStringFixedLength, quoteId);
            this._db.AddInParameter(dc, "CostMode", DbType.Byte, (int)costMode);
            this._db.AddInParameter(dc, "Pricetype", DbType.Byte, (int)priceType);

            return DbHelper.RunDataSetProcedure(dc, this._db);
        }

        /// <summary>
        /// 根据报价编号得到相关其它报价
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourQuoteNo> GetTourQuoteNo(string QuoteId)
        {
            IList<EyouSoft.Model.HTourStructure.MTourQuoteNo> list = new List<EyouSoft.Model.HTourStructure.MTourQuoteNo>();

            string sql = "declare @ParentId char(36) select @ParentId=ParentId from tbl_Quote where QuoteId=@QuoteId if(@ParentId='0') select QuoteId,TimeCount,QuoteStatus FROM tbl_Quote WHERE ParentId=@QuoteId or QuoteId=@QuoteId order by TimeCount else select QuoteId,TimeCount,QuoteStatus FROM tbl_Quote WHERE ParentId=@ParentId or QuoteId=@ParentId order by TimeCount  ";

            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.HTourStructure.MTourQuoteNo()
                    {
                        QuoteId = sdr.GetString(sdr.GetOrdinal("QuoteId")),
                        Times = sdr.GetInt32(sdr.GetOrdinal("TimeCount")),
                        QuoteState = (EyouSoft.Model.EnumType.TourStructure.QuoteState)sdr.GetByte(sdr.GetOrdinal("QuoteStatus"))
                    });
                }
            }
            return list;
        }




        #region private IList to xml

        /// <summary>
        /// 创建报价行程安排的xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuotePlanXml(IList<EyouSoft.Model.HTourStructure.MQuotePlan> list, string QuoteId)
        {

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.PlanId = System.Guid.NewGuid().ToString();
                xmlDoc.Append("<Item ");
                xmlDoc.AppendFormat("PlanId=\"{0}\" ", item.PlanId);
                xmlDoc.AppendFormat("QuoteId=\"{0}\" ", QuoteId);
                xmlDoc.AppendFormat("Traffic=\"{0}\" ", (int)item.Traffic);
                xmlDoc.AppendFormat("TrafficPrice=\"{0}\" ", item.TrafficPrice);
                xmlDoc.AppendFormat("HotelId1=\"{0}\" ", item.HotelId1);
                xmlDoc.AppendFormat("HotelName1=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.HotelName1));
                xmlDoc.AppendFormat("HotelPrice1=\"{0}\" ", item.HotelPrice1);
                xmlDoc.AppendFormat("HotelId2=\"{0}\" ", item.HotelId2);
                xmlDoc.AppendFormat("HotelName2=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.HotelName2));
                xmlDoc.AppendFormat("HotelPrice2=\"{0}\" ", item.HotelPrice2);
                xmlDoc.AppendFormat("IsBreakfast=\"{0}\" ", item.IsBreakfast ? 1 : 0);
                xmlDoc.AppendFormat("BreakfastPrice=\"{0}\" ", item.BreakfastPrice);
                xmlDoc.AppendFormat("BreakfastSettlementPrice=\"{0}\" ", item.BreakfastSettlementPrice);
                xmlDoc.AppendFormat("BreakfastRestaurantId=\"{0}\" ", item.BreakfastRestaurantId);
                xmlDoc.AppendFormat("BreakfastMenuId=\"{0}\" ", item.BreakfastMenuId);
                xmlDoc.AppendFormat("BreakfastMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.BreakfastMenu));
                xmlDoc.AppendFormat("IsLunch=\"{0}\" ", item.IsLunch ? 1 : 0);
                xmlDoc.AppendFormat("LunchPrice=\"{0}\" ", item.LunchPrice);
                xmlDoc.AppendFormat("LunchSettlementPrice=\"{0}\" ", item.LunchSettlementPrice);
                xmlDoc.AppendFormat("LunchRestaurantId=\"{0}\" ", item.LunchRestaurantId);
                xmlDoc.AppendFormat("LunchMenuId=\"{0}\" ", item.LunchMenuId);
                xmlDoc.AppendFormat("LunchMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.LunchMenu));
                xmlDoc.AppendFormat("IsSupper=\"{0}\" ", item.IsSupper ? 1 : 0);
                xmlDoc.AppendFormat("SupperPrice=\"{0}\" ", item.SupperPrice);
                xmlDoc.AppendFormat("SupperSettlementPrice=\"{0}\" ", item.SupperSettlementPrice);
                xmlDoc.AppendFormat("SupperRestaurantId=\"{0}\" ", item.SupperRestaurantId);
                xmlDoc.AppendFormat("SupperMenuId=\"{0}\" ", item.SupperMenuId);
                xmlDoc.AppendFormat("SupperMenu=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.SupperMenu));
                xmlDoc.AppendFormat("Days=\"{0}\" ", item.Days);
                xmlDoc.AppendFormat("Content=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.Content));
                xmlDoc.AppendFormat("FilePath=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(item.FilePath));

                xmlDoc.AppendFormat("BreakfastId=\"{0}\" ", item.BreakfastId);
                xmlDoc.AppendFormat("LunchId=\"{0}\" ", item.LunchId);
                xmlDoc.AppendFormat("SupperId=\"{0}\" ", item.SupperId);
                xmlDoc.Append(" />");

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价行程城市xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateQuotePlanCityXml(IList<EyouSoft.Model.HTourStructure.MQuotePlan> list)
        {
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var citys = item.QuotePlanCityList;
                if (citys != null)
                {
                    string PlanId = item.PlanId;

                    foreach (var city in citys)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" CityId=\"{1}\" CityName=\"{2}\" JiaoTong=\"{3}\" JiaoTongJiaGe=\"{4}\" />", PlanId, city.CityId, Utils.ReplaceXmlSpecialCharacter(city.CityName), (int)city.JiaoTong, city.JiaoTongJiaGe);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 行程购物点xml
        /// </summary>
        /// <returns></returns>
        private string CreateQuotePlanShopXml(IList<EyouSoft.Model.HTourStructure.MQuotePlan> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var item in list)
            {
                var shops = item.QuotePlanShopList;
                if (shops != null)
                {
                    string PlanId = item.PlanId;

                    foreach (var shop in shops)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" ShopId=\"{1}\" ShopName=\"{2}\" LiuShui=\"{3}\" PeopleMoney=\"{4}\" ChildMoney=\"{5}\" />",
                            PlanId,
                            shop.ShopId,
                            Utils.ReplaceXmlSpecialCharacter(shop.ShopName),
                            shop.LiuShui,
                            shop.PeopleMoney,
                            shop.ChildMoney
                            );
                    }
                }

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价行程景点xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateQuotePlanSpotXml(IList<EyouSoft.Model.HTourStructure.MQuotePlan> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var spots = item.QuotePlanSpotList;
                if (spots != null)
                {
                    string PlanId = item.PlanId;
                    foreach (var spot in spots)
                    {
                        xmlDoc.AppendFormat("<Item PlanId=\"{0}\" SpotId=\"{1}\" SpotName=\"{2}\" Price=\"{3}\" SettlementPrice=\"{4}\" PriceId=\"{5}\" />",
                            PlanId,
                            spot.SpotId,
                            Utils.ReplaceXmlSpecialCharacter(spot.SpotName),
                            spot.Price,
                            spot.SettlementPrice,
                            spot.PriceId);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价地图坐标xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuotePointXml(IList<EyouSoft.Model.HTourStructure.MQuotePoint> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" PointX=\"{1}\" PointY=\"{2}\" />", QuoteId, item.PointX, item.PointY);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价购物点xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteShopXml(IList<EyouSoft.Model.HTourStructure.MQuoteShop> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" ShopId=\"{1}\" ShopName=\"{2}\" LiuShui=\"{3}\" PeopleMoney=\"{4}\" ChildMoney=\"{5}\" />",
                    QuoteId,
                    item.ShopId,
                    Utils.ReplaceXmlSpecialCharacter(item.ShopName),
                    item.LiuShui,
                    item.PeopleMoney,
                    item.ChildMoney
                    );

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        /// <summary>
        /// 报价风味餐xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteFootXml(IList<EyouSoft.Model.HTourStructure.MQuoteFoot> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" RestaurantId=\"{1}\" MenuId=\"{2}\" Menu=\"{3}\" Price=\"{4}\" Remark=\"{5}\" SettlementPrice=\"{6}\" FootId=\"{7}\" />",
                    QuoteId,
                    item.RestaurantId,
                    item.MenuId,
                    Utils.ReplaceXmlSpecialCharacter(item.Menu),
                    item.Price,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    item.SettlementPrice,
                    item.FootId
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价赠送xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteGiveXml(IList<EyouSoft.Model.HTourStructure.MQuoteGive> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" ItemId=\"{1}\" Item=\"{2}\" Price=\"{3}\" Remark=\"{4}\" />",
                    QuoteId,
                    item.ItemId,
                    Utils.ReplaceXmlSpecialCharacter(item.Item),
                    item.Price,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark)
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价小费xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteTipXml(IList<EyouSoft.Model.HTourStructure.MQuoteTip> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" Tip=\"{1}\" Price=\"{2}\" Days=\"{3}\" SumPrice=\"{4}\" Remark=\"{5}\" />",
                    QuoteId,
                    Utils.ReplaceXmlSpecialCharacter(item.Tip),
                    item.Price,
                    item.Days,
                    item.SumPrice,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark)
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 自费项目xml
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteSelfPayXml(IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" CityId=\"{1}\" CityName=\"{2}\" ScenicSpotId=\"{3}\" ScenicSpotName=\"{4}\" Price=\"{5}\" Cost=\"{6}\" Remark=\"{7}\" SettlementPrice=\"{8}\" PriceId=\"{9}\" />",
                    QuoteId,
                    item.CityId,
                    Utils.ReplaceXmlSpecialCharacter(item.CityName),
                    item.ScenicSpotId,
                    Utils.ReplaceXmlSpecialCharacter(item.ScenicSpotName),
                    item.Price,
                    item.Cost,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    item.SettlementPrice,
                    item.PriceId
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 团队报价单价明细
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteCostXml(IList<EyouSoft.Model.HTourStructure.MQuoteCost> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" Pricetype=\"{1}\" Price=\"{2}\" PriceUnit=\"{3}\" Remark=\"{4}\" CostMode=\"{5}\" />",
                    QuoteId,
                    (int)item.Pricetype,
                    item.Price,
                    (int)item.PriceUnit,
                    Utils.ReplaceXmlSpecialCharacter(item.Remark),
                    (int)item.CostMode
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价价格
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuotePriceXml(IList<EyouSoft.Model.HTourStructure.MQuotePrice> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" AdultPrice=\"{1}\" ChildPrice=\"{2}\" LeadPrice=\"{3}\" SingleRoomPrice=\"{4}\" OtherPrice=\"{5}\" CostMode=\"{6}\" HeJiPrice=\"{7}\" />",
                    QuoteId,
                    item.AdultPrice,
                    item.ChildPrice,
                    item.LeadPrice,
                    item.SingleRoomPrice,
                    item.OtherPrice,
                    (int)item.CostMode,
                    item.HeJiPrice
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateQuoteFileXml(IList<EyouSoft.Model.HTourStructure.MQuoteFile> list, string QuoteId)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item QuoteId=\"{0}\" FileName=\"{1}\" FilePath=\"{2}\" FileModel=\"{3}\" />",
                    QuoteId,
                    Utils.ReplaceXmlSpecialCharacter(item.FileName),
                    Utils.ReplaceXmlSpecialCharacter(item.FilePath),
                    (int)item.FileModel
                   );
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价成功的团队信息
        /// </summary>
        /// <param name="QuoteTour"></param>
        /// <returns></returns>
        private string CreateQuoteTourXml(EyouSoft.Model.HTourStructure.MQuoteTour QuoteTour)
        {
            if (QuoteTour == null) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.Append("<Item ");
            xmlDoc.AppendFormat("TourId=\"{0}\" ", QuoteTour.TourId);
            xmlDoc.AppendFormat("TourCode=\"{0}\" ", QuoteTour.TourCode);
            xmlDoc.AppendFormat("LDate=\"{0}\" ", QuoteTour.LDate);
            xmlDoc.AppendFormat("RDate=\"{0}\" ", QuoteTour.RDate);
            xmlDoc.AppendFormat("ArriveCity=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(QuoteTour.ArriveCity));
            xmlDoc.AppendFormat("ArriveCityFlight=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(QuoteTour.ArriveCityFlight));
            xmlDoc.AppendFormat("LeaveCity=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(QuoteTour.LeaveCity));
            xmlDoc.AppendFormat("LeaveCityFlight=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(QuoteTour.LeaveCityFlight));
            xmlDoc.AppendFormat("Adults=\"{0}\" ", QuoteTour.Adults);
            xmlDoc.AppendFormat("Childs=\"{0}\" ", QuoteTour.Childs);
            xmlDoc.AppendFormat("Leads=\"{0}\" ", QuoteTour.Leads);
            xmlDoc.AppendFormat("SiPei=\"{0}\" ", QuoteTour.SiPei);
            xmlDoc.AppendFormat("AdultPrice=\"{0}\" ", QuoteTour.AdultPrice);
            xmlDoc.AppendFormat("ChildPrice=\"{0}\" ", QuoteTour.ChildPrice);
            xmlDoc.AppendFormat("LeadPrice=\"{0}\" ", QuoteTour.LeadPrice);
            xmlDoc.AppendFormat("SingleRoomPrice=\"{0}\" ", QuoteTour.SingleRoomPrice);
            xmlDoc.AppendFormat("InsideInformation=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(QuoteTour.InsideInformation));
            xmlDoc.AppendFormat("TourType=\"{0}\" ", (int)QuoteTour.TourType);
            xmlDoc.AppendFormat("TourStatus=\"{0}\" />", (int)QuoteTour.TourStatus);
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 报价成功的团队计调员信息
        /// </summary>
        /// <param name="QuoteTour"></param>
        /// <returns></returns>
        private string CreateQuoteTourPlanerXml(EyouSoft.Model.HTourStructure.MQuoteTour QuoteTour)
        {
            if (QuoteTour == null) return null;
            if (QuoteTour.PlanerList == null && QuoteTour.PlanerList.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var planer in QuoteTour.PlanerList)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" PlanerId=\"{1}\" Planer=\"{2}\" PlanerDeptId=\"{3}\" />", QuoteTour.TourId, planer.PlanerId, Utils.ReplaceXmlSpecialCharacter(planer.Planer), planer.PlanerDeptId);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价成功的团队地接社信息
        /// </summary>
        /// <param name="QuoteTour"></param>
        /// <returns></returns>
        private string CreateTourDiJie(EyouSoft.Model.HTourStructure.MQuoteTour QuoteTour)
        {
            if (QuoteTour == null) return null;
            if (QuoteTour.TourDiJieList == null || QuoteTour.TourDiJieList.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var dijie in QuoteTour.TourDiJieList)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" CityId=\"{1}\" DiJieName=\"{2}\" DiJieId=\"{3}\" Contact=\"{4}\" Phone=\"{5}\" Remark=\"{6}\" CityName=\"{7}\" />",
                                    QuoteTour.TourId,
                                    dijie.CityId,
                                    Utils.ReplaceXmlSpecialCharacter(dijie.DiJieName),
                                    dijie.DiJieId,
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Contact),
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Phone),
                                    Utils.ReplaceXmlSpecialCharacter(dijie.Remark),
                                    Utils.ReplaceXmlSpecialCharacter(dijie.CityName));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 报价成功的团队用房数
        /// </summary>
        /// <param name="QuoteTour"></param>
        /// <returns></returns>
        private string CreateQuoteTourRoomXml(EyouSoft.Model.HTourStructure.MQuoteTour QuoteTour)
        {
            if (QuoteTour == null) return null;
            if (QuoteTour.TourRoomList == null && QuoteTour.TourRoomList.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");

            foreach (var room in QuoteTour.TourRoomList)
            {
                xmlDoc.AppendFormat("<Item TourId=\"{0}\" RoomId=\"{1}\" Num=\"{2}\" />",
                    QuoteTour.TourId,
                    room.RoomId,
                    room.Num);
            }

            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateQuoteJourneyXml(IList<EyouSoft.Model.HTourStructure.MQuoteJourney> list)
        {
            if (list == null || list.Count == 0) return null;

            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<Item SourceId=\"{0}\" JourneyType=\"{1}\" />",
                   item.SourceId,
                  (int)item.JourneyType);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }


        #endregion


        #region private xml to Ilist

        /// <summary>
        /// 报价单文件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteFile> GetQuoteFileList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteFile> list = new List<EyouSoft.Model.HTourStructure.MQuoteFile>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteFile model = new EyouSoft.Model.HTourStructure.MQuoteFile();
                model.FileName = Utils.GetXAttributeValue(xRow, "FileName");
                model.FilePath = Utils.GetXAttributeValue(xRow, "FilePath");
                model.FileModel = (EyouSoft.Model.EnumType.TourStructure.QuoteFileModel)Utils.GetInt(Utils.GetXAttributeValue(xRow, "FileModel"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 报价地图坐标
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuotePoint> GetQuotePointList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuotePoint> list = new List<EyouSoft.Model.HTourStructure.MQuotePoint>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuotePoint model = new EyouSoft.Model.HTourStructure.MQuotePoint();
                model.PointX = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PointX"));
                model.PointY = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PointY"));

                list.Add(model);
            }

            return list;

        }

        /// <summary>
        /// 报价购物点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteShop> GetQuoteShopList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteShop> list = new List<EyouSoft.Model.HTourStructure.MQuoteShop>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteShop model = new EyouSoft.Model.HTourStructure.MQuoteShop();
                model.ShopId = Utils.GetXAttributeValue(xRow, "ShopId");
                model.ShopName = Utils.GetXAttributeValue(xRow, "ShopName");
                model.LiuShui = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "LiuShui"));
                model.PeopleMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "PeopleMoney"));
                model.ChildMoney = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ChildMoney"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 报价风味餐
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteFoot> GetQuoteFootList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MQuoteFoot> list = new List<EyouSoft.Model.HTourStructure.MQuoteFoot>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteFoot model = new EyouSoft.Model.HTourStructure.MQuoteFoot();
                model.RestaurantId = Utils.GetXAttributeValue(xRow, "RestaurantId");
                model.MenuId = Utils.GetXAttributeValue(xRow, "MenuId");
                model.Menu = Utils.GetXAttributeValue(xRow, "Menu");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");

                model.FootId = Utils.GetXAttributeValue(xRow, "FootId");

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 报价赠送
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteGive> GetQuoteGiveList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteGive> list = new List<EyouSoft.Model.HTourStructure.MQuoteGive>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteGive model = new EyouSoft.Model.HTourStructure.MQuoteGive();
                model.ItemId = Utils.GetXAttributeValue(xRow, "ItemId");
                model.Item = Utils.GetXAttributeValue(xRow, "Item");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 小费
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteTip> GetQuoteTipList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MQuoteTip> list = new List<EyouSoft.Model.HTourStructure.MQuoteTip>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteTip model = new EyouSoft.Model.HTourStructure.MQuoteTip();
                model.Tip = Utils.GetXAttributeValue(xRow, "Tip");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.Days = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Days"));
                model.SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SumPrice"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 自费项目
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> GetQuoteSelfPayList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteSelfPay> list = new List<EyouSoft.Model.HTourStructure.MQuoteSelfPay>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteSelfPay model = new EyouSoft.Model.HTourStructure.MQuoteSelfPay();
                model.CityId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CityId"));
                model.CityName = Utils.GetXAttributeValue(xRow, "CityName");
                model.ScenicSpotId = Utils.GetXAttributeValue(xRow, "ScenicSpotId");
                model.ScenicSpotName = Utils.GetXAttributeValue(xRow, "ScenicSpotName");
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.Cost = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Cost"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.PriceId = Utils.GetXAttributeValue(xRow, "PriceId");
                model.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 团队报价单价明细
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteCost> GetQuoteCostList(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteCost> list = new List<EyouSoft.Model.HTourStructure.MQuoteCost>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteCost model = new EyouSoft.Model.HTourStructure.MQuoteCost();
                model.Pricetype = (EyouSoft.Model.EnumType.TourStructure.Pricetype)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Pricetype"));
                model.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                model.PriceUnit = (EyouSoft.Model.EnumType.TourStructure.PriceUnit)Utils.GetInt(Utils.GetXAttributeValue(xRow, "PriceUnit"));
                model.Remark = Utils.GetXAttributeValue(xRow, "Remark");
                model.CostMode = (EyouSoft.Model.EnumType.TourStructure.CostMode)Utils.GetInt(Utils.GetXAttributeValue(xRow, "CostMode"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 价格
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuotePrice> GetQuotePriceList(string xml)
        {

            IList<EyouSoft.Model.HTourStructure.MQuotePrice> list = new List<EyouSoft.Model.HTourStructure.MQuotePrice>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuotePrice model = new EyouSoft.Model.HTourStructure.MQuotePrice();
                model.AdultPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "AdultPrice"));
                model.ChildPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "ChildPrice"));
                model.LeadPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "LeadPrice"));
                model.SingleRoomPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SingleRoomPrice"));
                model.OtherPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "OtherPrice"));
                model.CostMode = (EyouSoft.Model.EnumType.TourStructure.CostMode)Utils.GetInt(Utils.GetXAttributeValue(xRow, "CostMode"));
                model.HeJiPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow,"HeJiPrice"));
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetCrmLinkman(string xml)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                EyouSoft.Model.CrmStructure.MCrmLinkman model = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                model.Id = Utils.GetXAttributeValue(xRow, "Id");
                model.CompanyId = Utils.GetXAttributeValue(xRow, "CompanyId");
                model.Name = Utils.GetXAttributeValue(xRow, "Name");
                model.Gender = (Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Gender"));
                model.Birthday = Utils.GetDateTime(Utils.GetXAttributeValue(xRow, "Birthday"));
                model.Department = Utils.GetXAttributeValue(xRow, "CompanyId");
                model.Post = Utils.GetXAttributeValue(xRow, "Post");
                model.Telephone = Utils.GetXAttributeValue(xRow, "Telephone");
                model.MobilePhone = Utils.GetXAttributeValue(xRow, "MobilePhone");
                model.QQ = Utils.GetXAttributeValue(xRow, "QQ");
                model.EMail = Utils.GetXAttributeValue(xRow, "EMail");
                model.MSN = Utils.GetXAttributeValue(xRow, "MSN");
                model.Fax = Utils.GetXAttributeValue(xRow, "Fax");
                list.Add(model);
            }

            return list;

        }

        /// <summary>
        /// 根据xml获取行程
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuotePlan> GetQuotePlan(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuotePlan> list = null;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);
            System.Xml.XmlNode root = doc.SelectSingleNode("Root");
            if (root != null)
            {
                list = new List<EyouSoft.Model.HTourStructure.MQuotePlan>();

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    System.Xml.XmlNode parent = root.ChildNodes[i];

                    EyouSoft.Model.HTourStructure.MQuotePlan plan = new EyouSoft.Model.HTourStructure.MQuotePlan();
                    plan.PlanId = parent["PlanId"].InnerText;
                    plan.Traffic = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(parent["Traffic"].InnerText);
                    plan.TrafficPrice = Utils.GetDecimal(parent["TrafficPrice"].InnerText);
                    plan.HotelId1 = parent["HotelId1"] != null ? parent["HotelId1"].InnerText : string.Empty;
                    plan.HotelId2 = parent["HotelId2"] != null ? parent["HotelId2"].InnerText : string.Empty;
                    plan.HotelName1 = parent["HotelName1"] != null ? parent["HotelName1"].InnerText : string.Empty;
                    plan.HotelName2 = parent["HotelName2"] != null ? parent["HotelName2"].InnerText : string.Empty;
                    plan.HotelPrice1 = parent["HotelPrice1"] != null ? Utils.GetDecimal(parent["HotelPrice1"].InnerText) : 0;
                    plan.HotelPrice2 = parent["HotelPrice2"] != null ? Utils.GetDecimal(parent["HotelPrice2"].InnerText) : 0;
                    plan.IsBreakfast = parent["IsBreakfast"] != null ? parent["IsBreakfast"].InnerText == "1" : false;
                    plan.BreakfastPrice = parent["BreakfastPrice"] != null ? Utils.GetDecimal(parent["BreakfastPrice"].InnerText) : 0;
                    plan.BreakfastSettlementPrice = parent["BreakfastSettlementPrice"] != null ? Utils.GetDecimal(parent["BreakfastSettlementPrice"].InnerText) : 0;
                    plan.BreakfastRestaurantId = parent["BreakfastRestaurantId"] != null ? parent["BreakfastRestaurantId"].InnerText : string.Empty;
                    plan.BreakfastMenuId = parent["BreakfastMenuId"] != null ? parent["BreakfastMenuId"].InnerText : string.Empty;
                    plan.BreakfastMenu = parent["BreakfastMenu"] != null ? parent["BreakfastMenu"].InnerText : string.Empty;
                    plan.IsLunch = parent["IsLunch"] != null ? parent["IsLunch"].InnerText == "1" : false;
                    plan.LunchPrice = parent["LunchPrice"] != null ? Utils.GetDecimal(parent["LunchPrice"].InnerText) : 0;
                    plan.LunchSettlementPrice = parent["LunchSettlementPrice"] != null ? Utils.GetDecimal(parent["LunchSettlementPrice"].InnerText) : 0;
                    plan.LunchRestaurantId = parent["LunchRestaurantId"] != null ? parent["LunchRestaurantId"].InnerText : string.Empty;
                    plan.LunchMenuId = parent["LunchMenuId"] != null ? parent["LunchMenuId"].InnerText : string.Empty;
                    plan.LunchMenu = parent["LunchMenu"] != null ? parent["LunchMenu"].InnerText : string.Empty;
                    plan.IsSupper = parent["IsSupper"] != null ? parent["IsSupper"].InnerText == "1" : false;
                    plan.SupperPrice = parent["SupperPrice"] != null ? Utils.GetDecimal(parent["SupperPrice"].InnerText) : 0;
                    plan.SupperSettlementPrice = parent["SupperSettlementPrice"] != null ? Utils.GetDecimal(parent["SupperSettlementPrice"].InnerText) : 0;
                    plan.SupperRestaurantId = parent["SupperRestaurantId"] != null ? parent["SupperRestaurantId"].InnerText : string.Empty;
                    plan.SupperMenuId = parent["SupperMenuId"] != null ? parent["SupperMenuId"].InnerText : string.Empty;
                    plan.SupperMenu = parent["SupperMenu"] != null ? parent["SupperMenu"].InnerText : string.Empty;
                    plan.Days = parent["Days"] != null ? Utils.GetInt(parent["Days"].InnerText) : 0;
                    plan.Content = parent["Content"] != null ? parent["Content"].InnerText : string.Empty;
                    plan.FilePath = parent["FilePath"] != null ? parent["FilePath"].InnerText : string.Empty;

                    plan.BreakfastId = parent["BreakfastId"] != null ? parent["BreakfastId"].InnerText : string.Empty;
                    plan.LunchId = parent["LunchId"] != null ? parent["LunchId"].InnerText : string.Empty;
                    plan.SupperId = parent["SupperId"] != null ? parent["SupperId"].InnerText : string.Empty;

                    if (parent["QuotePlanCity"] != null)
                    {
                        plan.QuotePlanCityList = new List<EyouSoft.Model.HTourStructure.MQuotePlanCity>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["QuotePlanCity"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MQuotePlanCity plancity = new EyouSoft.Model.HTourStructure.MQuotePlanCity();
                            plancity.CityId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "CityId"));
                            plancity.CityName = Utils.GetXAttributeValue(xRow, "CityName");
                            plancity.JiaoTong = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetXAttributeValue(xRow, "JiaoTong"));
                            plancity.JiaoTongJiaGe = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "JiaoTongJiaGe"));
                            plan.QuotePlanCityList.Add(plancity);

                        }
                    }

                    if (parent["QuotePlanShop"] != null)
                    {
                        plan.QuotePlanShopList = new List<EyouSoft.Model.HTourStructure.MQuoteShop>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["QuotePlanShop"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MQuoteShop planshop = new EyouSoft.Model.HTourStructure.MQuoteShop();
                            planshop.ShopId = Utils.GetXAttributeValue(xRow, "ShopId");
                            planshop.ShopName = Utils.GetXAttributeValue(xRow, "ShopName");
                            plan.QuotePlanShopList.Add(planshop);
                        }
                    }

                    if (parent["QuotePlanSpot"] != null)
                    {
                        plan.QuotePlanSpotList = new List<EyouSoft.Model.HTourStructure.MQuotePlanSpot>();

                        System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(parent["QuotePlanSpot"].InnerText);
                        var xRows = Utils.GetXElements(xRoot, "row");
                        foreach (var xRow in xRows)
                        {
                            EyouSoft.Model.HTourStructure.MQuotePlanSpot planspot = new EyouSoft.Model.HTourStructure.MQuotePlanSpot();
                            planspot.SpotId = Utils.GetXAttributeValue(xRow, "SpotId");
                            planspot.SpotName = Utils.GetXAttributeValue(xRow, "SpotName");
                            planspot.Price = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Price"));
                            planspot.SettlementPrice = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "SettlementPrice"));
                            planspot.PriceId = Utils.GetXAttributeValue(xRow, "PriceId");
                            plan.QuotePlanSpotList.Add(planspot);
                        }
                    }

                    list.Add(plan);
                }
            }



            return list;
        }

        /// <summary>
        ///获取 行程亮点、行程备注、报价备注编号(用于语言选择)
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MQuoteJourney> GetQuoteJourney(string xml)
        {
            IList<EyouSoft.Model.HTourStructure.MQuoteJourney> list = new List<EyouSoft.Model.HTourStructure.MQuoteJourney>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                EyouSoft.Model.HTourStructure.MQuoteJourney model = new EyouSoft.Model.HTourStructure.MQuoteJourney();
                model.SourceId = Utils.GetInt(Utils.GetXAttributeValue(xRow, "SourceId"));
                model.JourneyType = (EyouSoft.Model.EnumType.TourStructure.JourneyType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "JourneyType"));
                list.Add(model);
            }

            return list;

        }

        #endregion

    }
}
