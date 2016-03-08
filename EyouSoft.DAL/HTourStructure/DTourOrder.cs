using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.HTourStructure
{

    public class DTourOrder : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.HTourStructure.ITourOrder
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DTourOrder()
        {
            this._db = base.SystemStore;
        }
        #endregion


        /// <summary>
        /// 添加团队游客
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TourId"></param>
        /// <returns>0:失败 1：成功 -1:不能删除退团的游客</returns>
        public int AddOrUpdateTourTraveller(IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list, string TourId)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourOrder_Traveller");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            this._db.AddInParameter(cmd, "TourOrderTraveller", DbType.Xml, CreateTourTravellerXml(list));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));

        }


        /// <summary>
        /// 获取游客信息
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> GetTourOrderTraveller(string TourId)
        {
            IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.HTourStructure.MTourOrderTraveller>();

            string sql = "select *,BuyCompanyName from tbl_TourOrderTraveller inner join tbl_TourOrder on tbl_TourOrderTraveller.OrderId=tbl_TourOrder.OrderId where tbl_TourOrderTraveller.TourId=@TourId order by tbl_TourOrderTraveller.Id  ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HTourStructure.MTourOrderTraveller model = new EyouSoft.Model.HTourStructure.MTourOrderTraveller();
                    model.TravellerId = dr.GetString(dr.GetOrdinal("TravellerId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                    model.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                    model.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                    model.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                    model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)dr.GetByte(dr.GetOrdinal("VisitorType"));
                    model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)dr.GetByte(dr.GetOrdinal("CardType"));
                    model.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                    model.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                    model.Birthday = !dr.IsDBNull(dr.GetOrdinal("Birthday")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("Birthday")) : null;
                    model.VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)dr.GetByte(dr.GetOrdinal("VisaStatus"));
                    model.IsCardTransact = dr.GetString(dr.GetOrdinal("IsCardTransact")) == "1";
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)dr.GetByte(dr.GetOrdinal("Gender"));
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                    model.LNotice = dr.GetString(dr.GetOrdinal("LNotice")) == "1";
                    model.RNotice = dr.GetString(dr.GetOrdinal("RNotice")) == "1";
                    model.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                    model.TravellerStatus = (EyouSoft.Model.EnumType.TourStructure.TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    model.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                    model.RAmountRemark = !dr.IsDBNull(dr.GetOrdinal("RAmountRemark")) ? dr.GetString(dr.GetOrdinal("RAmountRemark")) : string.Empty;
                    model.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                    model.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                    model.BuyCompanyName = dr["BuyCompanyName"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取游客信息的实体
        /// </summary>
        /// <param name="travellerId"></param>
        /// <returns></returns>
        public EyouSoft.Model.HTourStructure.MTourOrderTraveller GetTravellerModel(string travellerId)
        {

            EyouSoft.Model.HTourStructure.MTourOrderTraveller model = null;

            string sql = "select * from tbl_TourOrderTraveller where TravellerId=@TravellerId ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TravellerId", DbType.AnsiStringFixedLength, travellerId);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HTourStructure.MTourOrderTraveller();
                    model.TravellerId = dr.GetString(dr.GetOrdinal("TravellerId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.OrderId = dr.GetString(dr.GetOrdinal("OrderId"));
                    model.CnName = !dr.IsDBNull(dr.GetOrdinal("CnName")) ? dr.GetString(dr.GetOrdinal("CnName")) : string.Empty;
                    model.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : string.Empty;
                    model.CardId = !dr.IsDBNull(dr.GetOrdinal("CardId")) ? dr.GetString(dr.GetOrdinal("CardId")) : string.Empty;
                    model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)dr.GetByte(dr.GetOrdinal("VisitorType"));
                    model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)dr.GetByte(dr.GetOrdinal("CardType"));
                    model.CardNumber = !dr.IsDBNull(dr.GetOrdinal("CardNumber")) ? dr.GetString(dr.GetOrdinal("CardNumber")) : string.Empty;
                    model.CardValidDate = !dr.IsDBNull(dr.GetOrdinal("CardValidDate")) ? dr.GetString(dr.GetOrdinal("CardValidDate")) : string.Empty;
                    model.Birthday = !dr.IsDBNull(dr.GetOrdinal("Birthday")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("Birthday")) : null;
                    model.VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)dr.GetByte(dr.GetOrdinal("VisaStatus"));
                    model.IsCardTransact = dr.GetString(dr.GetOrdinal("IsCardTransact")) == "1";
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)dr.GetByte(dr.GetOrdinal("Gender"));
                    model.Contact = !dr.IsDBNull(dr.GetOrdinal("Contact")) ? dr.GetString(dr.GetOrdinal("Contact")) : string.Empty;
                    model.LNotice = dr.GetString(dr.GetOrdinal("LNotice")) == "1";
                    model.RNotice = dr.GetString(dr.GetOrdinal("RNotice")) == "1";
                    model.Remark = !dr.IsDBNull(dr.GetOrdinal("Remark")) ? dr.GetString(dr.GetOrdinal("Remark")) : string.Empty;
                    model.TravellerStatus = (EyouSoft.Model.EnumType.TourStructure.TravellerStatus)dr.GetByte(dr.GetOrdinal("Status"));
                    model.RAmount = dr.GetDecimal(dr.GetOrdinal("RAmount"));
                    model.RAmountRemark = !dr.IsDBNull(dr.GetOrdinal("RAmountRemark")) ? dr.GetString(dr.GetOrdinal("RAmountRemark")) : string.Empty;
                    model.RTime = !dr.IsDBNull(dr.GetOrdinal("RTime")) ? (DateTime?)dr.GetDateTime(dr.GetOrdinal("RTime")) : null;
                    model.RRemark = !dr.IsDBNull(dr.GetOrdinal("RRemark")) ? dr.GetString(dr.GetOrdinal("RRemark")) : string.Empty;
                }
            }

            return model;
        }


        



        #region private list to xml

        /// <summary>
        /// 创建团队游客的xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourTravellerXml(IList<EyouSoft.Model.HTourStructure.MTourOrderTraveller> list)
        {

            StringBuilder query = new StringBuilder();
            query.Append("<Root>");
            foreach (EyouSoft.Model.HTourStructure.MTourOrderTraveller traveller in list)
            {
                query.Append("<Item ");
                query.AppendFormat("TravellerId=\"{0}\" ", string.IsNullOrEmpty(traveller.TravellerId) ? Guid.NewGuid().ToString() : traveller.TravellerId);
                query.AppendFormat("CnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.CnName));
                query.AppendFormat("EnName=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.EnName));
                query.AppendFormat("CardId=\"{0}\" ", traveller.CardId);
                if (traveller.Birthday.HasValue)
                {
                    query.AppendFormat("Birthday=\"{0}\" ", traveller.Birthday.Value);
                }
                query.AppendFormat("VisitorType=\"{0}\" ", (int?)traveller.VisitorType);
                query.AppendFormat("CardType=\"{0}\" ", (int?)traveller.CardType);
                query.AppendFormat("CardNumber=\"{0}\" ", traveller.CardNumber);
                query.AppendFormat("CardValidDate=\"{0}\" ", traveller.CardValidDate);
                query.AppendFormat("VisaStatus=\"{0}\" ", (int?)traveller.VisaStatus);
                query.AppendFormat("IsCardTransact=\"{0}\" ", traveller.IsCardTransact == true ? 1 : 0);
                query.AppendFormat("Gender=\"{0}\" ", (int?)traveller.Gender);
                query.AppendFormat("Contact=\"{0}\" ", traveller.Contact);
                query.AppendFormat("LNotice=\"{0}\" ", traveller.LNotice == true ? 1 : 0);
                query.AppendFormat("RNotice=\"{0}\" ", traveller.RNotice == true ? 1 : 0);
                query.AppendFormat("Remark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.Remark));
                query.AppendFormat("Status=\"{0}\" ", (int)traveller.TravellerStatus);
                query.AppendFormat("RAmount=\"{0}\" ", traveller.RAmount);
                query.AppendFormat("RAmountRemark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.RAmountRemark));
                query.AppendFormat("RTime=\"{0}\" ", traveller.RTime);
                query.AppendFormat("RRemark=\"{0}\" ", EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(traveller.RRemark));

                query.Append("/>");
            }
            query.Append("</Root>");

            return query.ToString();
        }

        #endregion

    }
}
