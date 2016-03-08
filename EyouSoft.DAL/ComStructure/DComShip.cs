using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.DAL.ComStructure
{
    /*/// <summary>
    /// 游轮线路数据层
    /// </summary>
    public class DComShipRoute:DALBase,EyouSoft.IDAL.ComStructure.IComShip
    {
        private readonly Database _db = null;
        public DComShipRoute()
        {
            this._db = base.SystemStore;
        }

        #region IComShip 成员
        /// <summary>
        /// 添加游轮线路
        /// </summary>
        /// <param name="item">游轮线路实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComShip item)
        {
            string sql = "INSERT INTO tbl_ComShipRoute(CompanyId,ShipRouteName,ShipType) VALUES(@CompanyId,@Name,@ShipType)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@ShipType", DbType.Byte, item.PlanShipType);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改游轮线路
        /// </summary>
        /// <param name="item">游轮线路实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComShip item)
        {
            string sql = "UPDATE tbl_ComShipRoute SET ShipRouteName = @Name,ShipType=@ShipType WHERE ShipRouteId = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@ShipType", DbType.Byte, item.PlanShipType);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 游轮线路列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">游轮类型</param>
        /// <returns>游轮线路集合</returns>
        public IList<MComShip> GetList(string companyId,PlanShipType? type)
        {
            StringBuilder sql = new StringBuilder("SELECT ShipRouteId,CompanyId,ShipRouteName,ShipType FROM tbl_ComShipRoute WHERE CompanyId = @CompanyId");
            if (type != null)
            {
                sql.AppendFormat(" AND ShipType = {0}", (int)type);
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComShip> list = new List<MComShip>();
            MComShip item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComShip()
                    {
                        Id = (int)reader["ShipRouteId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        PlanShipType = (PlanShipType)Enum.Parse(typeof(PlanShipType), reader["ShipType"].ToString()),
                        Name = reader.IsDBNull(reader.GetOrdinal("ShipRouteName")) ? string.Empty : reader["ShipRouteName"].ToString()
                    });
                }
            }
            return list;
        }

        #endregion
    }
    /// <summary>
    /// 游轮自费项目数据层
    /// </summary>
    public class DComShipOwnCost : DALBase, EyouSoft.IDAL.ComStructure.IComShip
    {
        private readonly Database _db = null;
        public DComShipOwnCost()
        {
            this._db = base.SystemStore;
        }

        #region IComShip 成员
        /// <summary>
        /// 添加游轮自费项目
        /// </summary>
        /// <param name="item">游轮自费项目实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Add(MComShip item)
        {
            string sql = "INSERT INTO tbl_ComShipOwnCost(CompanyId,ShipCostName,ShipType) VALUES(@CompanyId,@Name,@ShipType)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@ShipType", DbType.Byte, item.PlanShipType);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 修改游轮自费项目
        /// </summary>
        /// <param name="item">游轮自费项目实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComShip item)
        {
            string sql = "UPDATE tbl_ComShipOwnCost SET ShipCostName = @Name,ShipType=@ShipType WHERE ShipCostId = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            this._db.AddInParameter(comm, "@Name", DbType.String, item.Name);
            this._db.AddInParameter(comm, "@ShipType", DbType.Byte, item.PlanShipType);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, item.Id);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 游轮自费项目列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">游轮类型</param>
        /// <returns>游轮自费项目集合</returns>
        public IList<MComShip> GetList(string companyId,PlanShipType? type)
        {
            StringBuilder sql = new StringBuilder("SELECT ShipCostId,CompanyId,ShipCostName,ShipType FROM tbl_ComShipOwnCost WHERE CompanyId = @CompanyId");
            if (type != null)
            {
                sql.AppendFormat(" AND ShipType = {0}", (int)type);
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComShip> list = new List<MComShip>();
            MComShip item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComShip()
                    {
                        Id = (int)reader["ShipCostId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        PlanShipType = (PlanShipType)Enum.Parse(typeof(PlanShipType), reader["ShipType"].ToString()),
                        Name = reader.IsDBNull(reader.GetOrdinal("ShipCostName")) ? string.Empty : reader["ShipCostName"].ToString()
                    });
                }
            }
            return list;
        }

        #endregion
    }*/
}
