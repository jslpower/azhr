using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.DQC
{
    public class DTourServiceQC : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.IQC.ITourServiceQC
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DTourServiceQC()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region private to xml

        /// <summary>
        /// 创建附件信息的xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateComAttachXml(IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {

            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.Append("<Item ");
                xmlDoc.AppendFormat("ItemType=\"{0}\" ", (int)item.ItemType);
                xmlDoc.AppendFormat("ItemId=\"{0}\" ", item.ItemId);
                xmlDoc.AppendFormat("Name=\"{0}\" ", item.Name);
                xmlDoc.AppendFormat("FilePath=\"{0}\" ", item.FilePath);
                xmlDoc.AppendFormat("Size=\"{0}\" ", item.Size);
                xmlDoc.AppendFormat("Downloads=\"{0}\" ", item.Downloads);
                xmlDoc.Append(" />");
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 获取附件的集合
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetComAttachList(string xml)
        {
            IList<EyouSoft.Model.ComStructure.MComAttach> list = new List<EyouSoft.Model.ComStructure.MComAttach>();

            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");

            foreach (var xRow in xRows)
            {
                EyouSoft.Model.ComStructure.MComAttach model = new EyouSoft.Model.ComStructure.MComAttach();
                model.ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "ItemType"));
                model.ItemId = Utils.GetXAttributeValue(xRow, "ItemId");
                model.Name = Utils.GetXAttributeValue(xRow, "Name");
                model.FilePath = Utils.GetXAttributeValue(xRow, "FilePath");
                model.Size = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Size"));
                model.Downloads = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Downloads"));
                list.Add(model);
            }

            return list;
        }

        #endregion

        #region ITourServiceQC 成员
        /// <summary>
        /// 添加团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model)
        {

            DbCommand cmd = _db.GetStoredProcCommand("proc_QC_TuanDui_Add");
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, model.QCID);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "GuideOneName", DbType.String, model.GuideOneName);
            _db.AddInParameter(cmd, "GuideTwoName", DbType.String, model.GuideTwoName);
            _db.AddInParameter(cmd, "QCTime", DbType.DateTime, model.QCTime);
            _db.AddInParameter(cmd, "LeaderName", DbType.String, model.LeaderName);
            _db.AddInParameter(cmd, "LeaderTel", DbType.String, model.LeaderTel);
            _db.AddInParameter(cmd, "Trip", DbType.Byte, (int)model.Trip);
            _db.AddInParameter(cmd, "Scenic", DbType.Byte, (int)model.Scenic);
            _db.AddInParameter(cmd, "Hotel", DbType.Byte, (int)model.Hotel);
            _db.AddInParameter(cmd, "Food", DbType.Byte, (int)model.Food);
            _db.AddInParameter(cmd, "GuideOne", DbType.Byte, (int)model.GuideOne);
            _db.AddInParameter(cmd, "GuideTwo", DbType.Byte, (int)model.GuideTwo);
            _db.AddInParameter(cmd, "TripRemark", DbType.String, model.TripRemark);
            _db.AddInParameter(cmd, "ScenicRemark", DbType.String, model.ScenicRemark);
            _db.AddInParameter(cmd, "HotelRemark", DbType.String, model.HotelRemark);
            _db.AddInParameter(cmd, "FoodRemark", DbType.String, model.FoodRemark);
            _db.AddInParameter(cmd, "GuideOneRemark", DbType.String, model.GuideOneRemark);
            _db.AddInParameter(cmd, "GuideTwoRemark", DbType.String, model.GuideTwoRemark);
            _db.AddInParameter(cmd, "Advice", DbType.String, model.Advice);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "FileInfo", DbType.Xml, CreateComAttachXml(model.FileList));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) > 0 ? true : false;



        }
        /// <summary>
        /// 修改团队服务质检信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTourServiceQC(EyouSoft.Model.QC.MTourServiceQC model)
        {

            DbCommand cmd = _db.GetStoredProcCommand("proc_QC_TuanDui_Update");
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, model.QCID);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "GuideOneName", DbType.String, model.GuideOneName);
            _db.AddInParameter(cmd, "GuideTwoName", DbType.String, model.GuideTwoName);
            _db.AddInParameter(cmd, "QCTime", DbType.DateTime, model.QCTime);
            _db.AddInParameter(cmd, "LeaderName", DbType.String, model.LeaderName);
            _db.AddInParameter(cmd, "LeaderTel", DbType.String, model.LeaderTel);
            _db.AddInParameter(cmd, "Trip", DbType.Byte, (int)model.Trip);
            _db.AddInParameter(cmd, "Scenic", DbType.Byte, (int)model.Scenic);
            _db.AddInParameter(cmd, "Hotel", DbType.Byte, (int)model.Hotel);
            _db.AddInParameter(cmd, "Food", DbType.Byte, (int)model.Food);
            _db.AddInParameter(cmd, "GuideOne", DbType.Byte, (int)model.GuideOne);
            _db.AddInParameter(cmd, "GuideTwo", DbType.Byte, (int)model.GuideTwo);
            _db.AddInParameter(cmd, "TripRemark", DbType.String, model.TripRemark);
            _db.AddInParameter(cmd, "ScenicRemark", DbType.String, model.ScenicRemark);
            _db.AddInParameter(cmd, "HotelRemark", DbType.String, model.HotelRemark);
            _db.AddInParameter(cmd, "FoodRemark", DbType.String, model.FoodRemark);
            _db.AddInParameter(cmd, "GuideOneRemark", DbType.String, model.GuideOneRemark);
            _db.AddInParameter(cmd, "GuideTwoRemark", DbType.String, model.GuideTwoRemark);
            _db.AddInParameter(cmd, "Advice", DbType.String, model.Advice);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "FileInfo", DbType.Xml, CreateComAttachXml(model.FileList));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) > 0 ? true : false;
        }

        /// <summary>
        /// 删除团队服务质检信息
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public bool DeleteTourServiceQC(string QCID)
        {
            string sql = "UPDATE tbl_TourServiceQC SET IsDelete='1' WHERE QCID=@ZhiJianId;";

            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "ZhiJianId", DbType.AnsiStringFixedLength, QCID);
            _db.AddInParameter(cmd, "FuJianType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.AttachItemType.车队质检);

            return DbHelper.ExecuteSql(cmd, _db) > 0;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public EyouSoft.Model.QC.MTourServiceQC GetTourServiceQCModel(string QCID)
        {
            EyouSoft.Model.QC.MTourServiceQC model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QCID, Trip, Scenic, Hotel, Food, GuideOne, GuideTwo, TripRemark, ScenicRemark, HotelRemark, FoodRemark, CompanyId, GuideOneRemark, GuideTwoRemark, Advice, Operator, OperatorDeptId, OperatorId, IssueTime, TourId, TourCode, GuideOneName, GuideTwoName, QCTime, LeaderName, LeaderTel,  ");
            strSql.AppendFormat("(select * from tbl_ComAttach where tbl_TourServiceQC.QCID=ItemId for xml raw,root('Root')) as FileInfo ");
            strSql.Append("  from tbl_TourServiceQC ");
            strSql.Append(" where QCID=@QCID ");

            DbCommand cmd = _db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, QCID);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.QC.MTourServiceQC();
                    model.QCID = dr.GetString(dr.GetOrdinal("QCID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.GuideOneName = !dr.IsDBNull(dr.GetOrdinal("GuideOneName")) ? dr.GetString(dr.GetOrdinal("GuideOneName")) : string.Empty;
                    model.GuideTwoName = !dr.IsDBNull(dr.GetOrdinal("GuideTwoName")) ? dr.GetString(dr.GetOrdinal("GuideTwoName")) : string.Empty;

                    model.QCTime = dr.GetDateTime(dr.GetOrdinal("QCTime"));
                    model.LeaderName = !dr.IsDBNull(dr.GetOrdinal("LeaderName")) ? dr.GetString(dr.GetOrdinal("LeaderName")) : string.Empty;
                    model.LeaderTel = !dr.IsDBNull(dr.GetOrdinal("LeaderTel")) ? dr.GetString(dr.GetOrdinal("LeaderTel")) : string.Empty;
                    model.Trip = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Trip"));
                    model.Scenic = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Scenic"));
                    model.Hotel = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Hotel"));
                    model.Food = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Food"));

                    model.GuideOne = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("GuideOne"));
                    model.GuideTwo = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("GuideTwo"));
                    model.TripRemark = !dr.IsDBNull(dr.GetOrdinal("TripRemark")) ? dr.GetString(dr.GetOrdinal("TripRemark")) : string.Empty;
                    model.ScenicRemark = !dr.IsDBNull(dr.GetOrdinal("ScenicRemark")) ? dr.GetString(dr.GetOrdinal("ScenicRemark")) : string.Empty;
                    model.HotelRemark = !dr.IsDBNull(dr.GetOrdinal("HotelRemark")) ? dr.GetString(dr.GetOrdinal("HotelRemark")) : string.Empty;
                    model.FoodRemark = !dr.IsDBNull(dr.GetOrdinal("FoodRemark")) ? dr.GetString(dr.GetOrdinal("FoodRemark")) : string.Empty;
                    model.GuideOneRemark = !dr.IsDBNull(dr.GetOrdinal("GuideOneRemark")) ? dr.GetString(dr.GetOrdinal("GuideOneRemark")) : string.Empty;
                    model.GuideTwoRemark = !dr.IsDBNull(dr.GetOrdinal("GuideTwoRemark")) ? dr.GetString(dr.GetOrdinal("GuideTwoRemark")) : string.Empty;

                    model.Advice = !dr.IsDBNull(dr.GetOrdinal("Advice")) ? dr.GetString(dr.GetOrdinal("Advice")) : string.Empty;

                    model.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));

                    model.FileList = !dr.IsDBNull(dr.GetOrdinal("FileInfo")) ? GetComAttachList(dr.GetString(dr.GetOrdinal("FileInfo"))) : null;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.QC.MTourServiceQC> GetTourServiceQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MTourServiceQCSearch search)
        {
            IList<EyouSoft.Model.QC.MTourServiceQC> list = new List<EyouSoft.Model.QC.MTourServiceQC>();

            string tableName = "tbl_TourServiceQC";

            string fields = " *,(select * from tbl_ComAttach where tbl_TourServiceQC.QCID=ItemId for xml raw,root('Root')) as FileInfo ";

            string orderByString = " IssueTime desc ";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.TourCode))
                {
                    query.AppendFormat(" and TourCode like '%{0}%' ", search.TourCode);
                }

                if (!string.IsNullOrEmpty(search.GuideName))
                {
                    query.AppendFormat(" and ( GuideOneName like '%{0}%' OR GuideTwoName like '%{0}%' ) ", search.GuideName);
                }
            }

            using (IDataReader dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, tableName, fields, query.ToString(), orderByString, null))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.QC.MTourServiceQC model = new EyouSoft.Model.QC.MTourServiceQC();
                    model.QCID = dr.GetString(dr.GetOrdinal("QCID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.GuideOneName = !dr.IsDBNull(dr.GetOrdinal("GuideOneName")) ? dr.GetString(dr.GetOrdinal("GuideOneName")) : string.Empty;
                    model.GuideTwoName = !dr.IsDBNull(dr.GetOrdinal("GuideTwoName")) ? dr.GetString(dr.GetOrdinal("GuideTwoName")) : string.Empty;

                    model.QCTime = dr.GetDateTime(dr.GetOrdinal("QCTime"));
                    model.LeaderName = !dr.IsDBNull(dr.GetOrdinal("LeaderName")) ? dr.GetString(dr.GetOrdinal("LeaderName")) : string.Empty;
                    model.LeaderTel = !dr.IsDBNull(dr.GetOrdinal("LeaderTel")) ? dr.GetString(dr.GetOrdinal("LeaderTel")) : string.Empty;
                    model.Trip = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Trip"));
                    model.Scenic = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Scenic"));
                    model.Hotel = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Hotel"));
                    model.Food = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("Food"));

                    model.GuideOne = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("GuideOne"));
                    model.GuideTwo = (EyouSoft.Model.EnumType.CrmStructure.SatisfactionType)dr.GetByte(dr.GetOrdinal("GuideTwo"));
                    model.TripRemark = !dr.IsDBNull(dr.GetOrdinal("TripRemark")) ? dr.GetString(dr.GetOrdinal("TripRemark")) : string.Empty;
                    model.ScenicRemark = !dr.IsDBNull(dr.GetOrdinal("ScenicRemark")) ? dr.GetString(dr.GetOrdinal("ScenicRemark")) : string.Empty;
                    model.HotelRemark = !dr.IsDBNull(dr.GetOrdinal("HotelRemark")) ? dr.GetString(dr.GetOrdinal("HotelRemark")) : string.Empty;
                    model.FoodRemark = !dr.IsDBNull(dr.GetOrdinal("FoodRemark")) ? dr.GetString(dr.GetOrdinal("FoodRemark")) : string.Empty;
                    model.GuideOneRemark = !dr.IsDBNull(dr.GetOrdinal("GuideOneRemark")) ? dr.GetString(dr.GetOrdinal("GuideOneRemark")) : string.Empty;
                    model.GuideTwoRemark = !dr.IsDBNull(dr.GetOrdinal("GuideTwoRemark")) ? dr.GetString(dr.GetOrdinal("GuideTwoRemark")) : string.Empty;

                    model.Advice = !dr.IsDBNull(dr.GetOrdinal("Advice")) ? dr.GetString(dr.GetOrdinal("Advice")) : string.Empty;

                    model.Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : string.Empty;
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));

                    model.FileList = !dr.IsDBNull(dr.GetOrdinal("FileInfo")) ? GetComAttachList(dr.GetString(dr.GetOrdinal("FileInfo"))) : null;

                    list.Add(model);
                }
            }

            return list;
        }
        #endregion
    }
}
