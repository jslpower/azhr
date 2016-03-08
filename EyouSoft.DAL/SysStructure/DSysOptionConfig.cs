using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EyouSoft.Model.SysStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.IDAL.SysStructure;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 导游须知、行程备注、报价备注、行程亮点、线路区域成员
    /// </summary>
    public class DSysOptionConfig : DALBase, ISysOptionConfig
    {
        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DSysOptionConfig()
        {
            _db = SystemStore;
        }
        #endregion


        #region 导游需知
        /// <summary>
        /// 添加导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddGuideKnow(MSysGuideKonw info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComGuideKown_Add");

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "DepartId", DbType.Int32, info.DepartId);
            _db.AddInParameter(cmd, "KnowMark", DbType.String, info.KnowMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 修改导游须知
        /// </summary>
        /// <param name="info">导游须知</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateGuideKnow(MSysGuideKonw info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComGuideKown_Update");


            _db.AddInParameter(cmd, "id", DbType.Int32, info.Id);
            _db.AddInParameter(cmd, "DepartId", DbType.Int32, info.DepartId);
            _db.AddInParameter(cmd, "KnowMark", DbType.String, info.KnowMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 删除导游须知
        /// </summary>
        /// <param name="id">导游须知编号</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteGuideKnow(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComGuideKown_Delete");
            _db.AddInParameter(cmd, "id", DbType.Int32, id);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 获取导游须知
        /// </summary>
        /// <param name="id">获取导游须知编号</param>
        /// <returns></returns>
        public MSysGuideKonw GetGuideKnow(int id)
        {
            string sql = "SELECT [Id],[CompanyId],[DepartId],[KnowMark],[OperatorDeptId],[OperatorId],[Operator],[IssueTime]FROM [tbl_ComGuideKown]   WHERE Id=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, id);
            MSysGuideKonw item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MSysGuideKonw()
                    {
                        Id = Utils.GetInt(reader["Id"].ToString()),
                        CompanyId = reader["CompanyId"].ToString(),
                        KnowMark = reader["KnowMark"].ToString(),
                        DepartId = Utils.GetInt(reader["DepartId"].ToString()),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        IssueTime = Utils.GetDateTime(reader["IssueTime"].ToString())
                    };
                }
            }
            return item;
        } /// <summary>
        /// 获取导游须知列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysGuideKonw> GetGuideKnowList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysGuideKonwSearch searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysGuideKonw> items = new List<EyouSoft.Model.SysStructure.MSysGuideKonw>();
            string TableName = "tbl_ComGuideKown";
            string OrderByString = "IssueTime DESC";
            string fields = "*";
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, string.Empty, OrderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysGuideKonw();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DepartId = Utils.GetInt(rdr["DepartId"].ToString());
                    item.KnowMark = rdr["KnowMark"].ToString();
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                    items.Add(item);
                }
            }
            return items;
        }
        #endregion

        #region 行程备注
        /// <summary>
        /// 判断是否存在形成备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsMSysJourneyMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {

            string sql = "SELECT 1 FROM  tbl_ComJourney WHERE MasterId=@Id AND LngType=@LngType ";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "Id", DbType.Int32, id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lngType);
            return DbHelper.Exists(cmd, _db);

        }
        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddMSysJourneyMark(MSysJourneyMark info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourney_Add");
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BackMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <param name="info">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateMSysJourneyMark(MSysJourneyMark info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourney_Update");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.Id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BackMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加行程备注
        /// </summary>
        /// <param name="id">行程备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteMSysJourneyMark(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourney_Delete");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, id);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 获取行程备注
        /// </summary>
        /// <param name="id">行程备注编号</param>
        /// <returns></returns>
        public MSysJourneyMark GetJourneyMark(int id)
        {
            string sql = "SELECT [Id],[CompanyId],[LngType],[MasterId],[BackMark],[OperatorDeptId],[OperatorId],[Operator],[IssueTime]  FROM [tbl_ComJourney]  WHERE Id=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, id);
            MSysJourneyMark item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MSysJourneyMark()
                    {
                        Id = Utils.GetInt(reader["Id"].ToString()),
                        CompanyId = reader["CompanyId"].ToString(),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        BackMark = reader["BackMark"].ToString(),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        IssueTime = Utils.GetDateTime(reader["IssueTime"].ToString())
                    };
                }
            }
            return item;
        }
        /// <summary>
        /// 获取行程备注列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysJourneyMark> GetJourneyMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyMarkSearch searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysJourneyMark> items = new List<EyouSoft.Model.SysStructure.MSysJourneyMark>();
            string TableName = "tbl_ComJourney";
            string OrderByString = "IssueTime DESC";
            string fields = "*";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId ='{0}' ", companyId);
            if (searchInfo != null)
            {
                if (searchInfo.LngType.HasValue)
                {
                    query.AppendFormat(" and  LngType ={0}", (int)searchInfo.LngType.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, query.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysJourneyMark();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                    item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());
                    item.BackMark = rdr["BackMark"].ToString();
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                    items.Add(item);
                }
            }
            return items;
        }

        #endregion

        #region 报价备注
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public bool isEsistsMBackPriceMark(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            string sql = "SELECT 1 FROM  tbl_ComBaojia WHERE MasterId=@Id AND LngType=@LngType ";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "Id", DbType.Int32, id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lngType);
            return DbHelper.Exists(cmd, _db);
        }
        /// <summary>
        /// 添加报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddMBackPriceMark(MBackPriceMark info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComBaojia_Add");

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BackMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加报价备注
        /// </summary>
        /// <param name="info">报价备注</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateMBackPriceMark(MBackPriceMark info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComBaojia_Update");

            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.Id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "BackMark", DbType.String, info.BackMark);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加报价备注
        /// </summary>
        /// <param name="id">报价备注编号</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteMBackPriceMark(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComBaojia_Delete");
            _db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, id);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 获取报价备注
        /// </summary>
        /// <param name="id">报价备注编号</param>
        /// <returns></returns>
        public MBackPriceMark GetBackPriceMark(int id)
        {
            string sql = "SELECT [Id],[CompanyId],[LngType],[MasterId],[BackMark],[OperatorDeptId],[OperatorId],[Operator],[IssueTime]  FROM [tbl_ComBaojia]  WHERE Id=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, id);
            MBackPriceMark item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MBackPriceMark()
                    {
                        Id = Utils.GetInt(reader["Id"].ToString()),
                        CompanyId = reader["CompanyId"].ToString(),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        BackMark = reader["BackMark"].ToString(),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        IssueTime = Utils.GetDateTime(reader["IssueTime"].ToString())
                    };
                }
            }
            return item;
        }
        /// <summary>
        /// 获取报价备注列表
        /// </summary>
        /// <returns></returns>
        public IList<MBackPriceMark> GetMBackPriceMarkList(string companyId, int pageSize, int pageIndex, ref int recordCount, MBackPriceMarkSearch searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MBackPriceMark> items = new List<EyouSoft.Model.SysStructure.MBackPriceMark>();
            string TableName = "tbl_ComBaojia";
            string OrderByString = "IssueTime DESC";
            string fields = "*";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId ='{0}'", companyId);
            if (searchInfo != null)
            {
                if (searchInfo.LngType.HasValue)
                {
                    query.AppendFormat(" and LngType ={0}", (int)searchInfo.LngType.Value);
                }
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, query.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MBackPriceMark();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                    item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());
                    item.BackMark = rdr["BackMark"].ToString();
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 获取报价备注列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<MBackPriceMark> GetMBackPriceMarkList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType LngType)
        {
            IList<EyouSoft.Model.SysStructure.MBackPriceMark> items = new List<EyouSoft.Model.SysStructure.MBackPriceMark>();

            string sql = string.Format(" select * from  tbl_ComBaojia where MasterId in ({0}) and LngType={1} ", GetIdsByArr(ids), (int)LngType);
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MBackPriceMark();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                    item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());
                    item.BackMark = rdr["BackMark"].ToString();
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());

                    items.Add(item);
                }
            }
            return items;
        }

        #endregion

        #region 行程亮点
        /// <summary>
        /// 是否存在行程亮点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsJourneyLight(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            string sql = "SELECT 1 FROM  tbl_ComJourneySpot WHERE MasterId=@Id AND LngType=@LngType ";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "Id", DbType.Int32, id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lngType);
            return DbHelper.Exists(cmd, _db);
        }
        /// <summary>
        /// 添加行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddJourneyLight(MSysJourneyLight info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourneySpot_Add");

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "DeptID", DbType.Int32, info.DeptID);
            _db.AddInParameter(cmd, "AreaID", DbType.Int32, info.AreaID);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, info.JourneySpot);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加行程亮点
        /// </summary>
        /// <param name="info">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateJourneyLight(MSysJourneyLight info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourneySpot_Update");

            _db.AddInParameter(cmd, "id", DbType.Int32, info.Id);
            _db.AddInParameter(cmd, "DeptID", DbType.Int32, info.DeptID);
            _db.AddInParameter(cmd, "AreaID", DbType.Int32, info.AreaID);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "JourneySpot", DbType.String, info.JourneySpot);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加行程亮点
        /// </summary>
        /// <param name="id">行程亮点</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteJourneyLight(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComJourneySpot_Delete");

            _db.AddInParameter(cmd, "id", DbType.Int32, id);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 获取行程亮点
        /// </summary>
        /// <param name="id">行程亮点编号</param>
        /// <returns></returns>
        public MSysJourneyLight GetJourneyLight(int id)
        {
            string sql = "SELECT [Id],[CompanyId],[DeptID],[AreaID],[JourneySpot],[LngType],[MasterId]      ,[OperatorDeptId],[OperatorId],[Operator],[IssueTime]  FROM  [tbl_ComJourneySpot] WHERE Id=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, id);
            MSysJourneyLight item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MSysJourneyLight()
                    {
                        Id = Utils.GetInt(reader["Id"].ToString()),
                        CompanyId = reader["CompanyId"].ToString(),
                        DeptID = Utils.GetInt(reader["DeptID"].ToString()),
                        AreaID = Utils.GetInt(reader["AreaID"].ToString()),
                        JourneySpot = reader["JourneySpot"].ToString(),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),

                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        IssueTime = Utils.GetDateTime(reader["IssueTime"].ToString())
                    };
                }
            }
            return item;
        }

        /// <summary>
        /// 获取行程亮点（多个）
        /// </summary>
        /// <param name="ids">行程亮点编号字符串(,分隔)</param>
        /// <returns></returns>
        public string GetJourneyLight(string ids)
        {
            string sql = "SELECT [Id],[JourneySpot] FROM  [tbl_ComJourneySpot] WHERE Id in(" + ids + ")";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            string JourneySpot = string.Empty;
            using (DataTable dt = DbHelper.DataTableQuery(comm, this._db))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        JourneySpot += dt.Rows[i]["JourneySpot"].ToString() + "<br />";
                    }
                }
            }
            return JourneySpot;

        }


        /// <summary>
        /// 获取行程亮点列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysJourneyLight> GetJourneyLightList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysJourneyLightSearch searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysJourneyLight> items = new List<EyouSoft.Model.SysStructure.MSysJourneyLight>();
            string TableName = "tbl_ComJourneySpot";
            string OrderByString = "IssueTime DESC";
            string fields = "*";

            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId='{0}' ", companyId);
            if (searchInfo != null)
            {
                if (searchInfo.DeptID.HasValue)
                {
                    query.AppendFormat(" and DeptID={0} ", searchInfo.DeptID.Value);
                }

                if (searchInfo.LngType.HasValue)
                {
                    query.AppendFormat(" and LngType={0} ", (int)searchInfo.LngType.Value);
                }
            }


            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, query.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysJourneyLight();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DeptID = Utils.GetInt(rdr["DeptID"].ToString());
                    item.AreaID = Utils.GetInt(rdr["AreaID"].ToString());
                    item.JourneySpot = rdr["JourneySpot"].ToString();
                    item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                    item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                    items.Add(item);
                }
            }
            return items;
        }


        /// <summary>
        /// 获取行程亮点列表(用于打印单)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public IList<MSysJourneyLight> GetJourneyLightList(int[] ids, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            IList<EyouSoft.Model.SysStructure.MSysJourneyLight> items = new List<EyouSoft.Model.SysStructure.MSysJourneyLight>();

            string SQL_QUERY_STRING = string.Format("SELECT [Id],[CompanyId],[DeptID],[AreaID],[JourneySpot],[LngType],[MasterId],[OperatorDeptId],[OperatorId],[Operator],[IssueTime]  FROM  [tbl_ComJourneySpot] WHERE MasterId in ({0}) and LngType={1}", GetIdsByArr(ids), (int)lngType);
            DbCommand cmd = _db.GetSqlStringCommand(SQL_QUERY_STRING);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysJourneyLight();
                    item.Id = Utils.GetInt(rdr["Id"].ToString());
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.DeptID = Utils.GetInt(rdr["DeptID"].ToString());
                    item.AreaID = Utils.GetInt(rdr["AreaID"].ToString());
                    item.JourneySpot = rdr["JourneySpot"].ToString();
                    item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                    item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());
                    item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                    item.OperatorId = rdr["OperatorId"].ToString();
                    item.Operator = rdr["Operator"].ToString();
                    item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                    items.Add(item);
                }
            }
            return items;
        }
        #endregion

        #region 线路区域
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int AddRoute(MSysRoute info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComArea_Add");

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AreaName", DbType.String, info.CompanyId);
            _db.AddInParameter(cmd, "ChildCompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.CompanyId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int UpdateRoute(MSysRoute info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComArea_Update");

            _db.AddInParameter(cmd, "id", DbType.AnsiStringFixedLength, info.AreaId);
            _db.AddInParameter(cmd, "AreaName", DbType.String, info.CompanyId);
            _db.AddInParameter(cmd, "ChildCompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.CompanyId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="info">线路区域</param>
        /// <returns>1:成功、0:失败</returns>
        public int DeleteRoute(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComArea_Delete");

            _db.AddInParameter(cmd, "id", DbType.AnsiStringFixedLength, id);
            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));
        }
        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="id">线路区域编号</param>
        /// <returns></returns>
        public MSysRoute GetRoute(int id)
        {
            string sql = "SELECT [AreaId],[CompanyId],[AreaName],[ChildCompanyId],[LngType],[MasterId]      ,[OperatorDeptId],[OperatorId],[Operator],[IssueTime],[IsDelete]  FROM  [tbl_ComArea] WHERE    AreaId=@Id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "Id", DbType.Int32, id);
            MSysRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new MSysRoute()
                    {
                        AreaId = Utils.GetInt(reader["AreaId"].ToString()),
                        CompanyId = reader["CompanyId"].ToString(),
                        AreaName = reader["AreaName"].ToString(),
                        ChildCompanyId = Utils.GetInt(reader["ChildCompanyId"].ToString()),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),

                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        IssueTime = Utils.GetDateTime(reader["IssueTime"].ToString()),
                        IsDelete = Utils.GetInt(reader["IsDelete"].ToString()) == 0 ? false : true

                    };
                }
            }
            return item;
        }
        /// <summary>
        /// 获取线路区域列表
        /// </summary>
        /// <returns></returns>
        public IList<MSysRoute> GetRouteList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysRouteSearch searchInfo)
        {
            {
                IList<EyouSoft.Model.SysStructure.MSysRoute> items = new List<EyouSoft.Model.SysStructure.MSysRoute>();
                string TableName = "tbl_ComArea";
                string OrderByString = "IssueTime DESC";
                string fields = "*";
                using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, string.Empty, OrderByString))
                {
                    while (rdr.Read())
                    {
                        var item = new EyouSoft.Model.SysStructure.MSysRoute();
                        item.AreaId = Utils.GetInt(rdr["AreaId"].ToString());
                        item.CompanyId = rdr["CompanyId"].ToString();

                        item.AreaName = rdr["AreaName"].ToString();
                        item.ChildCompanyId = Utils.GetInt(rdr["ChildCompanyId"].ToString());
                        item.LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(rdr["LngType"].ToString());
                        item.MasterId = Utils.GetInt(rdr["MasterId"].ToString());

                        item.OperatorDeptId = Utils.GetInt(rdr["OperatorDeptId"].ToString());
                        item.OperatorId = rdr["OperatorId"].ToString();
                        item.Operator = rdr["Operator"].ToString();
                        item.IssueTime = Utils.GetDateTime(rdr["IssueTime"].ToString());


                        items.Add(item);
                    }
                }
                return items;
            }
        }
        #endregion

    }
}
