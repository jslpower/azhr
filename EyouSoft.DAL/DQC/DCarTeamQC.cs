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
    public class DCarTeamQC : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.IQC.ICarTeamQC
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DCarTeamQC()
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

        #region ICarTeamQC 成员
        /// <summary>
        /// 添加车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_QC_CheDui_Add");
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, model.QCID);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "CarTeamName", DbType.String, model.CarTeamName);
            _db.AddInParameter(cmd, "CarCode", DbType.String, model.CarCode);
            _db.AddInParameter(cmd, "QCTime", DbType.DateTime, model.QCTime);
            _db.AddInParameter(cmd, "LeaderName", DbType.String, model.LeaderName);
            _db.AddInParameter(cmd, "LeaderTel", DbType.String, model.LeaderTel);
            _db.AddInParameter(cmd, "DriverService", DbType.Byte, (int)model.DriverService);
            _db.AddInParameter(cmd, "FindWay", DbType.Byte, (int)model.FindWay);
            _db.AddInParameter(cmd, "Advice", DbType.String, model.Advice);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "FileInfo", DbType.Xml, CreateComAttachXml(model.FileList));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) > 0 ? true : false;

        }

        /// <summary>
        /// 修改车队质检
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCarTeamQC(EyouSoft.Model.QC.MCarTeamQC model)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_QC_CheDui_Update");
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, model.QCID);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            _db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            _db.AddInParameter(cmd, "TourCode", DbType.String, model.TourCode);
            _db.AddInParameter(cmd, "CarTeamName", DbType.String, model.CarTeamName);
            _db.AddInParameter(cmd, "CarCode", DbType.String, model.CarCode);
            _db.AddInParameter(cmd, "QCTime", DbType.DateTime, model.QCTime);
            _db.AddInParameter(cmd, "LeaderName", DbType.String, model.LeaderName);
            _db.AddInParameter(cmd, "LeaderTel", DbType.String, model.LeaderTel);
            _db.AddInParameter(cmd, "DriverService", DbType.Byte, (int)model.DriverService);
            _db.AddInParameter(cmd, "FindWay", DbType.Byte, (int)model.FindWay);
            _db.AddInParameter(cmd, "Advice", DbType.String, model.Advice);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            _db.AddInParameter(cmd, "FileInfo", DbType.Xml, CreateComAttachXml(model.FileList));

            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) > 0 ? true : false;
        }

        /// <summary>
        /// 删除车队质检
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public bool DeleteCarTeamQC(string QCID)
        {
            string sql = "UPDATE tbl_CarTeamQC SET IsDelete='1' WHERE QCID=@ZhiJianId;";

            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "ZhiJianId", DbType.AnsiStringFixedLength, QCID);
            _db.AddInParameter(cmd, "FuJianType", DbType.Byte, EyouSoft.Model.EnumType.ComStructure.AttachItemType.车队质检);

            return DbHelper.ExecuteSql(cmd, _db) > 0;
        }

        /// <summary>
        /// 获取车队质检实体
        /// </summary>
        /// <param name="QCID"></param>
        /// <returns></returns>
        public EyouSoft.Model.QC.MCarTeamQC GetCarTeamQCModel(string QCID)
        {
            EyouSoft.Model.QC.MCarTeamQC model = null;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QCID, DriverService, FindWay, Advice, Operator, OperatorDeptId, OperatorId, IssueTime, CompanyId, TourId, TourCode, CarTeamName, CarCode, QCTime, LeaderName, LeaderTel,  ");
            strSql.AppendFormat("(select * from tbl_ComAttach where tbl_CarTeamQC.QCID=ItemId for xml raw,root('Root')) as FileInfo");
            strSql.Append("  from tbl_CarTeamQC ");
            strSql.Append(" where QCID=@QCID ");

            DbCommand cmd = this._db.GetSqlStringCommand(strSql.ToString());
            _db.AddInParameter(cmd, "QCID", DbType.AnsiStringFixedLength, QCID);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.QC.MCarTeamQC();
                    model.QCID = dr.GetString(dr.GetOrdinal("QCID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.CarTeamName = !dr.IsDBNull(dr.GetOrdinal("CarTeamName")) ? dr.GetString(dr.GetOrdinal("CarTeamName")) : string.Empty;
                    model.CarCode = !dr.IsDBNull(dr.GetOrdinal("CarCode")) ? dr.GetString(dr.GetOrdinal("CarCode")) : string.Empty;
                    model.QCTime = dr.GetDateTime(dr.GetOrdinal("QCTime"));
                    model.LeaderName = !dr.IsDBNull(dr.GetOrdinal("LeaderName")) ? dr.GetString(dr.GetOrdinal("LeaderName")) : string.Empty;
                    model.LeaderTel = !dr.IsDBNull(dr.GetOrdinal("LeaderTel")) ? dr.GetString(dr.GetOrdinal("LeaderTel")) : string.Empty;
                    model.DriverService = (EyouSoft.Model.EnumType.CrmStructure.DriverService)dr.GetByte(dr.GetOrdinal("DriverService"));
                    model.FindWay = (EyouSoft.Model.EnumType.CrmStructure.FindWay)dr.GetByte(dr.GetOrdinal("FindWay"));
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
        /// 分页列表信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.QC.MCarTeamQC> GetCarTeamQCList(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.QC.MCarTeamQCSearch search)
        {
            IList<EyouSoft.Model.QC.MCarTeamQC> list = new List<EyouSoft.Model.QC.MCarTeamQC>();

            string tableName = "tbl_CarTeamQC";

            string fields = " *,(select * from tbl_ComAttach where tbl_CarTeamQC.QCID=ItemId for xml raw,root('Root')) as FileInfo ";

            string orderByString = " IssueTime desc ";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId='{0}' AND IsDelete='0' ", companyId);

            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.TourCode))
                {
                    query.AppendFormat(" and TourCode like '%{0}%' ", search.TourCode);
                }

                if (!string.IsNullOrEmpty(search.CarTeamName))
                {
                    query.AppendFormat(" and CarTeamName like '%{0}%' ", search.CarTeamName);
                }
            }

            using (IDataReader dr = DbHelper.ExecuteReader1(this._db, pageSize, pageIndex, ref recordCount, tableName, fields, query.ToString(), orderByString, null))
            {

                while (dr.Read())
                {
                    EyouSoft.Model.QC.MCarTeamQC model = new EyouSoft.Model.QC.MCarTeamQC();

                    model.QCID = dr.GetString(dr.GetOrdinal("QCID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.TourId = dr.GetString(dr.GetOrdinal("TourId"));
                    model.TourCode = !dr.IsDBNull(dr.GetOrdinal("TourCode")) ? dr.GetString(dr.GetOrdinal("TourCode")) : string.Empty;
                    model.CarTeamName = !dr.IsDBNull(dr.GetOrdinal("CarTeamName")) ? dr.GetString(dr.GetOrdinal("CarTeamName")) : string.Empty;
                    model.CarCode = !dr.IsDBNull(dr.GetOrdinal("CarCode")) ? dr.GetString(dr.GetOrdinal("CarCode")) : string.Empty;
                    model.QCTime = dr.GetDateTime(dr.GetOrdinal("QCTime"));
                    model.LeaderName = !dr.IsDBNull(dr.GetOrdinal("LeaderName")) ? dr.GetString(dr.GetOrdinal("LeaderName")) : string.Empty;
                    model.LeaderTel = !dr.IsDBNull(dr.GetOrdinal("LeaderTel")) ? dr.GetString(dr.GetOrdinal("LeaderTel")) : string.Empty;
                    model.DriverService = (EyouSoft.Model.EnumType.CrmStructure.DriverService)dr.GetByte(dr.GetOrdinal("DriverService"));
                    model.FindWay = (EyouSoft.Model.EnumType.CrmStructure.FindWay)dr.GetByte(dr.GetOrdinal("FindWay"));
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
