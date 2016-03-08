using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml.Linq;
using EyouSoft.Toolkit;
namespace EyouSoft.DAL.GovStructure
{
    /// <summary>
    /// 培训管理数据访问层
    /// 2011-09-26 邵权江 创建
    /// </summary>
    public class DTrain:EyouSoft.Toolkit.DAL.DALBase,EyouSoft.IDAL.GovStructure.ITrain
    {
        #region 私有变量
        private readonly Database _db = null;
        #endregion

        #region 构造函数
        public DTrain()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region SQL语名
 
        #endregion

        #region 成员方法
        /// <summary>
        /// 增加一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <returns></returns>
        public bool AddGovTrain(EyouSoft.Model.GovStructure.MGovTrain model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovTrain_Add");
            this._db.AddInParameter(dc, "TrainId", DbType.AnsiStringFixedLength, model.TrainId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "StateTime", DbType.DateTime, model.StateTime);
            this._db.AddInParameter(dc, "EndTime", DbType.DateTime, model.EndTime);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "TrainPeople", DbType.String, model.TrainPeople);
            this._db.AddInParameter(dc, "JoinPeople", DbType.String, model.JoinPeople);
            this._db.AddInParameter(dc, "TrainingLocations", DbType.String, model.TrainingLocations);
            this._db.AddInParameter(dc, "Training", DbType.String, model.Training);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComNoticeXML(model.ComAttachList));
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                IsTrue = int.Parse(Result.ToString()) > 0 ? true : false;
            }
            return IsTrue;
        }

        /// <summary>
        /// 更新一条培训
        /// </summary>
        /// <param name="model">培训model</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public bool UpdateGovTrain(EyouSoft.Model.GovStructure.MGovTrain model, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovTrain_Update");
            this._db.AddInParameter(dc, "TrainId", DbType.AnsiStringFixedLength, model.TrainId);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "StateTime", DbType.DateTime, model.StateTime);
            this._db.AddInParameter(dc, "EndTime", DbType.DateTime, model.EndTime);
            this._db.AddInParameter(dc, "Title", DbType.String, model.Title);
            this._db.AddInParameter(dc, "TrainPeople", DbType.String, model.TrainPeople);
            this._db.AddInParameter(dc, "JoinPeople", DbType.String, model.JoinPeople);
            this._db.AddInParameter(dc, "TrainingLocations", DbType.String, model.TrainingLocations);
            this._db.AddInParameter(dc, "Training", DbType.String, model.Training);
            this._db.AddInParameter(dc, "DepartId", DbType.Int32, model.DepartId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddInParameter(dc, "ComAttachXML", DbType.Xml, CreateComNoticeXML(model.ComAttachList));
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                IsTrue = int.Parse(Result.ToString()) > 0 ? true : false;
            }
            return IsTrue;
        }

        /// <summary>
        /// 获得培训实体
        /// </summary>
        /// <param name="TrainId">培训ID</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        public EyouSoft.Model.GovStructure.MGovTrain GetGovTrainModel(string TrainId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            EyouSoft.Model.GovStructure.MGovTrain model = null;
            StringBuilder StrSql = new StringBuilder();
            StrSql.Append("SELECT TrainId,CompanyId,StateTime,EndTime,Title,TrainPeople,JoinPeople,TrainingLocations,Training,DepartId,OperatorId,IssueTime, ");
            StrSql.AppendFormat(" (SELECT Name,FilePath,Size,Downloads FROM tbl_ComAttach WHERE ItemType={0} AND ItemId=a.TrainId FOR XML RAW,ROOT('ROOT'))AS ComAttachXML ", (int)ItemType);
            StrSql.AppendFormat(" FROM tbl_GovTrain a WHERE TrainId='{0}' ", TrainId);
            DbCommand dc = this._db.GetSqlStringCommand(StrSql.ToString());
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(dc, this._db))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovTrain();
                    model.TrainId = dr.GetString(dr.GetOrdinal("TrainId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("StateTime")))
                    {
                        model.StateTime = dr.GetDateTime(dr.GetOrdinal("StateTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        model.EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime"));
                    }
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    model.TrainPeople = dr.IsDBNull(dr.GetOrdinal("TrainPeople")) ? "" : dr.GetString(dr.GetOrdinal("TrainPeople"));
                    model.JoinPeople = dr.IsDBNull(dr.GetOrdinal("JoinPeople")) ? "" : dr.GetString(dr.GetOrdinal("JoinPeople"));
                    model.TrainingLocations = dr.IsDBNull(dr.GetOrdinal("TrainingLocations")) ? "" : dr.GetString(dr.GetOrdinal("TrainingLocations"));
                    model.Training = dr.IsDBNull(dr.GetOrdinal("Training")) ? "" : dr.GetString(dr.GetOrdinal("Training"));
                    model.DepartId = dr.GetInt32(dr.GetOrdinal("DepartId"));
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), TrainId, ItemType);
                }
            };
            return model;
        }

        /// <summary>
        /// 根据培训时间条件获得培训信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="TimeBegin">开始时间</param>
        /// <param name="TimeEnd">结束时间</param>
        /// <param name="Title">主题</param>
        /// <param name="ItemType">附件类型</param>
        /// <param name="PageSize">页面数据总记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">每页显示的记录数</param>
        /// <returns></returns>
        public IList<Model.GovStructure.MGovTrain> GetGovTrainList(string CompanyId, DateTime? TimeBegin, DateTime? TimeEnd, string Title, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, int PageSize, int PageIndex, ref int RecordCount)
        {
            IList<EyouSoft.Model.GovStructure.MGovTrain> ResultList = null;
            string tableName = "view_GovTrain";
            string identityColumnName = "OpinionId";
            string fields = "TrainId,CompanyId,StateTime,EndTime,Title,JoinPeople,TrainingLocations,DepartId,OperatorId,IssueTime,ComAttachXML  ";
            string query = string.Format(" CompanyId='{0}' ", CompanyId, (int)ItemType);
            if (!string.IsNullOrEmpty(Title))
            {
                query = query + string.Format(" AND Title like '%{0}%' ", Title);
            }
            if (!string.IsNullOrEmpty(TimeBegin.ToString()))
            {
                query = query + string.Format(" AND datediff(dd, '{0}', StateTime) >= 0", TimeBegin);
            }
            if (!string.IsNullOrEmpty(TimeEnd.ToString()))
            {
                query = query + string.Format(" AND datediff(dd, '{0}', EndTime) <= 0", TimeEnd);
            }
            string orderByString = " IssueTime DESC";
            using (IDataReader dr = EyouSoft.Toolkit.DAL.DbHelper.ExecuteReader(_db, PageSize, PageIndex, ref RecordCount, tableName, identityColumnName, fields, query, orderByString))
            {
                ResultList = new List<EyouSoft.Model.GovStructure.MGovTrain>();
                EyouSoft.Model.GovStructure.MGovTrain model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.GovStructure.MGovTrain();
                    model.TrainId = dr.GetString(dr.GetOrdinal("TrainId"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("StateTime")))
                    {
                        model.StateTime = dr.GetDateTime(dr.GetOrdinal("StateTime"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EndTime")))
                    {
                        model.EndTime = dr.GetDateTime(dr.GetOrdinal("EndTime"));
                    }
                    model.Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? "" : dr.GetString(dr.GetOrdinal("Title"));
                    model.JoinPeople = dr.IsDBNull(dr.GetOrdinal("JoinPeople")) ? "" : dr.GetString(dr.GetOrdinal("JoinPeople"));
                    model.TrainingLocations = dr.IsDBNull(dr.GetOrdinal("TrainingLocations")) ? "" : dr.GetString(dr.GetOrdinal("TrainingLocations"));
                    model.DepartId = dr.GetInt32(dr.GetOrdinal("DepartId"));
                    model.OperatorId = dr.GetString(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    model.ComAttachList = this.GetAttachList(dr["ComAttachXML"].ToString(), dr.GetString(dr.GetOrdinal("TrainId")), ItemType);

                    ResultList.Add(model);
                    model = null;
                }
            };
            return ResultList;
        }

        /// <summary>
        /// 根据培训ID删除
        /// </summary>
        /// <param name="ItemType">附件类型</param>
        /// <param name="TrainIds">培训ID</param>
        /// <returns></returns>
        public bool DeleteGovTrain(EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType, params string[] TrainIds)
        {
            StringBuilder sId = new StringBuilder();
            for (int i = 0; i < TrainIds.Length; i++)
            {
                sId.AppendFormat("{0},", TrainIds[i]);
            }
            sId.Remove(sId.Length - 1, 1);
            DbCommand dc = this._db.GetStoredProcCommand("proc_GovTrain_Delete");
            this._db.AddInParameter(dc, "TrainIds", DbType.AnsiString, sId.ToString());
            this._db.AddInParameter(dc, "ItemType", DbType.Byte, (int)ItemType);
            this._db.AddOutParameter(dc, "Result", DbType.Int32, 4);
            EyouSoft.Toolkit.DAL.DbHelper.RunProcedure(dc, this._db);
            object Result = this._db.GetParameterValue(dc, "Result");
            if (!Result.Equals(null))
            {
                return int.Parse(Result.ToString()) == 0 ? false : true;
            }
            return false;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 创建参与人员XML
        /// </summary>
        /// <param name="Lists">参与人员集合</param>
        /// <returns></returns>
        private string CreateTrainStaffListXML(IList<EyouSoft.Model.GovStructure.MGovTrainStaff> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.GovStructure.MGovTrainStaff model in list)
            {
                StrBuild.AppendFormat("<GovTrainStaff TrainId=\"{0}\"", model.TrainId);
                StrBuild.AppendFormat(" UserId=\"{0}\" />", model.UserId);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 创建规章制度附件XML
        /// </summary>
        /// <param name="Lists">附件集合</param>
        /// <returns></returns>
        private string CreateComNoticeXML(IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ComStructure.MComAttach model in list)
            {
                StrBuild.AppendFormat("<ComAttach ItemType=\"{0}\"", (int)model.ItemType);
                StrBuild.AppendFormat(" ItemId=\"{0}\" ", model.ItemId);
                StrBuild.AppendFormat(" Name=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.Name));
                StrBuild.AppendFormat(" FilePath=\"{0}\" ", Utils.ReplaceXmlSpecialCharacter(model.FilePath));
                StrBuild.AppendFormat(" Size=\"{0}\" ", (int)model.Size);
                StrBuild.AppendFormat(" Downloads=\"{0}\" />", model.Downloads);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 参与集合List
        /// </summary>
        /// <param name="TrainStaffXML">参与信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.GovStructure.MGovTrainStaff> GetTrainStaffList(string TrainStaffXML, string TrainId)
        {
            if (string.IsNullOrEmpty(TrainStaffXML)) return null;
            IList<EyouSoft.Model.GovStructure.MGovTrainStaff> ResultList = null;
            ResultList = new List<EyouSoft.Model.GovStructure.MGovTrainStaff>();
            XElement root = XElement.Parse(TrainStaffXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.GovStructure.MGovTrainStaff model = new EyouSoft.Model.GovStructure.MGovTrainStaff()
                {
                    TrainId = TrainId,
                    UserId = tmp1.Attribute("ID").Value,
                    User = tmp1.Attribute("Name").Value
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息XML</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetAttachList(string ComAttachXML, string NoticeId, EyouSoft.Model.EnumType.ComStructure.AttachItemType ItemType)
        {
            if (string.IsNullOrEmpty(ComAttachXML)) return null;
            IList<EyouSoft.Model.ComStructure.MComAttach> ResultList = null;
            ResultList = new List<EyouSoft.Model.ComStructure.MComAttach>();
            XElement root = XElement.Parse(ComAttachXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.ComStructure.MComAttach model = new EyouSoft.Model.ComStructure.MComAttach()
                {
                    Name = tmp1.Attribute("Name").Value,
                    FilePath = tmp1.Attribute("FilePath").Value,
                    Size = int.Parse(tmp1.Attribute("Size").Value),
                    Downloads = int.Parse(tmp1.Attribute("Downloads").Value),
                    ItemId = NoticeId,
                    ItemType = ItemType
                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }
        #endregion
    }
}
