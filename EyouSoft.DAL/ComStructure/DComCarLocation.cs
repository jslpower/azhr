using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using System.Data;
using EyouSoft.Model.ComStructure;
using System.Data.Common;

namespace EyouSoft.DAL.ComStructure
{

    /// <summary>
    /// 上车地点数据层
    /// </summary>
    public class DComCarLocation : DALBase, EyouSoft.IDAL.ComStructure.IComCarLocation
    {
        private readonly Microsoft.Practices.EnterpriseLibrary.Data.Database _db = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DComCarLocation()
        {
            this._db = this.SystemStore;
        }
        #endregion

        #region 成员

        /// <summary>
        /// 查询该上车地点是否有计划预设
        /// </summary>
        /// <param name="CarLocationId"></param>
        /// <returns></returns>
        public bool IsExistsTourCarLocation(string CarLocationId)
        {
            string sql = "select 1 from tbl_TourCarLocation where CarLocationId=@CarLocationId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, CarLocationId);
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
        /// 添加上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns></returns>
        public bool AddCarLocation(MComCarLocation model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarLocation_Add");
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, model.CarLocationId);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(cmd, "Location", DbType.String, model.Location);
            this._db.AddInParameter(cmd, "OffPrice", DbType.Decimal, model.OffPrice);
            this._db.AddInParameter(cmd, "OnPrice", DbType.Decimal, model.OnPrice);
            this._db.AddInParameter(cmd, "Desc", DbType.String, model.Desc);
            this._db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, model.Status == true ? 1 : 0);
            this._db.AddInParameter(cmd, "OperatorId", DbType.StringFixedLength, model.OperatorId);
            this._db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获取上车地点实体
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>上车地点实体</returns>
        public MComCarLocation GetModel(string CarLocationId)
        {
            MComCarLocation model = null;
            string sql = "SELECT [CarLocationId],[CompanyId],[Location],[OffPrice],[OnPrice],[Desc],[Status],[OperatorId],[Operator],[IsDelete] FROM [tbl_ComCarLocation]  Where CarLocationId=@CarLocationId";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, CarLocationId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr.Read())
                {
                    model = new MComCarLocation()
                    {
                        CarLocationId = dr["CarLocationId"].ToString(),
                        CompanyId = dr["CompanyId"].ToString(),
                        Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                        Location = !dr.IsDBNull(dr.GetOrdinal("Location")) ? dr.GetString(dr.GetOrdinal("Location")) : null,
                        OffPrice = dr.GetDecimal(dr.GetOrdinal("OffPrice")),
                        OnPrice = dr.GetDecimal(dr.GetOrdinal("OnPrice")),
                        Operator = dr.GetString(dr.GetOrdinal("Operator")),
                        OperatorId = dr.GetString(dr.GetOrdinal("OperatorId")),
                        Status = dr.GetString(dr.GetOrdinal("Status")) == "1" ? true : false

                    };
                }

            }

            return model;
        }

        /// <summary>
        /// 删除上车地点
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        public int DelCarLocation(string CarLocationId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarLocation_Delete");
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, CarLocationId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));


        }

        /// <summary>
        /// 修改上车地点
        /// </summary>
        /// <param name="model">上车地点实体</param>
        /// <returns>1:成功，0：失败,2:该上车地点已被计划使用</returns>
        public int UpdateCarLocation(MComCarLocation model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComCarLocation_Update");
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, model.CarLocationId);
            this._db.AddInParameter(cmd, "Location", DbType.String, model.Location);
            this._db.AddInParameter(cmd, "OffPrice", DbType.Currency, model.OffPrice);
            this._db.AddInParameter(cmd, "OnPrice", DbType.Currency, model.OnPrice);
            this._db.AddInParameter(cmd, "Desc", DbType.String, model.Desc);
            this._db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, model.Status == true ? 1 : 0);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 设置上车地点的启用禁用
        /// </summary>
        /// <param name="CarLocationId">上车地点编号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateCarLocation(string CarLocationId, bool Status)
        {
            string sql = "UPDATE [tbl_ComCarLocation] SET [Status] = @Status WHERE CarLocationId=@CarLocationId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "CarLocationId", DbType.AnsiStringFixedLength, CarLocationId);
            this._db.AddInParameter(cmd, "Status", DbType.AnsiStringFixedLength, Status == true ? 1 : 0);
            return DbHelper.ExecuteSql(cmd, this._db);
        }

        /// <summary>
        /// 获取上车地点列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        ///<param name="status">启用or 禁用</param>
        ///<param name="location">上车地点</param>
        /// <returns>结果集</returns>
        public IList<EyouSoft.Model.ComStructure.MComCarLocation> GetList(int pageIndex, int pageSize, ref int recordCount, string companyId, bool? status, string location)
        {
            IList<EyouSoft.Model.ComStructure.MComCarLocation> list = null;

            string fileds = "[CarLocationId],[CompanyId],[Location],[OffPrice],[OnPrice],[Desc],[Status],[OperatorId],[Operator]";

            StringBuilder query = new StringBuilder();
            query.AppendFormat("CompanyId='{0}' and IsDelete='0' ", companyId);

            if (status.HasValue)
            {
                query.AppendFormat(" and  Status='{0}' ", status.Value == true ? 1 : 0);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query.AppendFormat(" and  Location like '%{0}%' ", location);
            }


            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize,
                pageIndex,
                ref recordCount,
                "tbl_ComCarLocation",
                "CarLocationId",
                fileds,
                query.ToString(),
                "Status Desc"))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.ComStructure.MComCarLocation>();
                    while (dr.Read())
                    {
                        MComCarLocation item = new MComCarLocation()
                        {
                            CarLocationId = dr["CarLocationId"].ToString(),
                            CompanyId = dr["CompanyId"].ToString(),
                            Desc = !dr.IsDBNull(dr.GetOrdinal("Desc")) ? dr.GetString(dr.GetOrdinal("Desc")) : null,
                            Location = !dr.IsDBNull(dr.GetOrdinal("Location")) ? dr.GetString(dr.GetOrdinal("Location")) : null,
                            OffPrice = dr.GetDecimal(dr.GetOrdinal("OffPrice")),
                            OnPrice = dr.GetDecimal(dr.GetOrdinal("OnPrice")),
                            Status = dr.GetString(dr.GetOrdinal("Status")) == "1" ? true : false,
                            Operator = !dr.IsDBNull(dr.GetOrdinal("Operator")) ? dr.GetString(dr.GetOrdinal("Operator")) : null,
                            OperatorId = dr["OperatorId"].ToString()
                        };
                        list.Add(item);
                    }

                }
            }

            return list;
        }

        #endregion
    }
}
