using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;


namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 车型数据层
    /// 创建者：李晓欢
    /// 创建时间：2012/8/14
    /// </summary>
    public class DComCarType : DALBase, EyouSoft.IDAL.ComStructure.IComCarType
    {
        private readonly Microsoft.Practices.EnterpriseLibrary.Data.Database _db = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DComCarType()
        {
            _db = this.SystemStore;
        }
        #endregion


        #region IComCarType成员

        /// <summary>
        /// 查询该车型是否有计划预设
        /// </summary>
        /// <param name="CarTypeId"></param>
        /// <returns></returns>
        public bool IsExistsTourCarType(string CarTypeId)
        {
            string sql = "select 1 from tbl_TourCarType where CarTypeId=@CarTypeId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CarTypeId", DbType.AnsiStringFixedLength, CarTypeId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null && dr.Read())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddComCarType(MComCarType model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarType_Add");
            this._db.AddInParameter(cmd, "CarTypeId", DbType.AnsiStringFixedLength, model.CarTypeId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(cmd, "CarTypeName", DbType.String, model.CarTypeName);
            this._db.AddInParameter(cmd, "SeatNum", DbType.Int32, model.SeatNum);
            this._db.AddInParameter(cmd, "Desc", DbType.String, model.Desc);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, model.TemplateId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获取车型实体
        /// </summary>
        /// <param name="CarTypeId">车型编号</param>
        /// <returns>返回实体</returns>
        public MComCarType GetModel(string CarTypeId)
        {
            MComCarType model = null;
            string sql = "SELECT [CarTypeId],[CompanyId],[CarTypeName],[SeatNum],[Desc],[OperatorId],[Operator],[TemplateId] FROM [tbl_ComCarType] Where [CarTypeId]=@CarTypeId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CarTypeId", DbType.AnsiStringFixedLength, CarTypeId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null && dr.Read())
                {
                    model = new MComCarType()
                    {
                        CarTypeId = dr["CarTypeId"].ToString(),
                        CarTypeName = !dr.IsDBNull(dr.GetOrdinal("CarTypeName")) ? dr.GetString(dr.GetOrdinal("CarTypeName")) : null,
                        CompanyId = dr["CompanyId"].ToString(),
                        Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                        Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : null,
                        OperatorId = dr["OperatorId"].ToString(),
                        SeatNum = dr.GetInt32(dr.GetOrdinal("SeatNum")),
                        TemplateId = dr["TemplateId"].ToString()
                    };
                }
            }

            return model;
        }

        /// <summary>
        /// 修改车型
        /// </summary>
        /// <param name="model">车型实体</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        public int UpdateComCarType(MComCarType model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarType_Update");
            this._db.AddInParameter(cmd, "CarTypeId", DbType.AnsiStringFixedLength, model.CarTypeId);
            this._db.AddInParameter(cmd, "CarTypeName", DbType.String, model.CarTypeName);
            this._db.AddInParameter(cmd, "SeatNum", DbType.Int32, model.SeatNum);
            this._db.AddInParameter(cmd, "Desc", DbType.String, model.Desc);
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, model.TemplateId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="CarTypeId">车型编号</param>
        /// <returns>0:失败 1:成功 2:计划有使用</returns>
        public int DelComCarType(string CarTypeId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarType_Delete");
            this._db.AddInParameter(cmd, "CarTypeId", DbType.AnsiStringFixedLength, CarTypeId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));


        }

        /// <summary>
        /// 获取系统车型
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComCarType> GetList(string companyId)
        {
            IList<EyouSoft.Model.ComStructure.MComCarType> list = null;

            string query = "Select * from tbl_ComCarType where CompanyId=@CompanyId and IsDelete='0' ";
            DbCommand cmd = _db.GetSqlStringCommand(query);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                list = new List<EyouSoft.Model.ComStructure.MComCarType>();
                while (dr.Read())
                {
                    MComCarType item = new MComCarType()
                    {
                        CarTypeId = dr["CarTypeId"].ToString(),
                        CarTypeName = !dr.IsDBNull(dr.GetOrdinal("CarTypeName")) ? dr.GetString(dr.GetOrdinal("CarTypeName")) : null,
                        CompanyId = dr["CompanyId"].ToString(),
                        Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                        Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : null,
                        OperatorId = dr["OperatorId"].ToString(),
                        SeatNum = dr.GetInt32(dr.GetOrdinal("SeatNum")),
                        TemplateId = dr["TemplateId"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取车型
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>车型集合</returns>
        public IList<EyouSoft.Model.ComStructure.MComCarType> GetList(
            int pageIndex, 
            int pageSize, 
            ref int recordCount, 
            string companyId)
        {
            IList<EyouSoft.Model.ComStructure.MComCarType> list = null;
            string fileds = "CarTypeId,CompanyId,CarTypeName,SeatNum,[Desc],OperatorId,Operator,TemplateId";
            string tableName = "tbl_ComCarType";
            string sqlWhere = string.Format(" CompanyId='{0}' and IsDelete='0' ", companyId);

            using (IDataReader dr = DbHelper.ExecuteReader(this._db,
                pageSize,
                pageIndex,
                ref recordCount,
                tableName,
                "CarTypeId",
                fileds,
                sqlWhere,
                " IssueTime desc "))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.ComStructure.MComCarType>();
                    while (dr.Read())
                    {
                        MComCarType item = new MComCarType()
                        {
                            CarTypeId = dr["CarTypeId"].ToString(),
                            CarTypeName = !dr.IsDBNull(dr.GetOrdinal("CarTypeName")) ? dr.GetString(dr.GetOrdinal("CarTypeName")) : null,
                            CompanyId = dr["CompanyId"].ToString(),
                            Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                            Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : null,
                            OperatorId = dr["OperatorId"].ToString(),
                            SeatNum = dr.GetInt32(dr.GetOrdinal("SeatNum")),
                            TemplateId = dr["TemplateId"].ToString()
                        };
                        list.Add(item);
                    }
                }
            }


            return list;
        }




        /// <summary>
        /// 创建座位编号xml
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string CreateCarTypeSeatXMl(IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {
            if (list == null && list.Count == 0) return null;
            System.Text.StringBuilder SeatXmldoc = new StringBuilder();
            SeatXmldoc.Append("<Root>");
            foreach (var item in list)
            {
                SeatXmldoc.Append("<CarTypeSeat  SeatNumber='" + item.SeatNumber + "' PointX=" + item.PointX + " PoinY=" + item.PoinY + " />");
            }
            SeatXmldoc.Append("</Root>");
            return SeatXmldoc.ToString();
        }

        #endregion


    }

}
