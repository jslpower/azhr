using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using EyouSoft.IDAL.SysStructure;
using System.Xml.Linq;
using System.Data.Common;
using System.Data;
using EyouSoft.Model.SysStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SysStructure
{
    public class DSysRoom : DALBase, ISysRoom
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetRooms = "SELECT * FROM [tbl_ComRoomTypeManage] WHERE [CompanyId]=@CompanyId AND [IsDelete]='0' ORDER BY [IssueTime]";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DSysRoom()
        {
            _db = SystemStore;
        }
        #endregion

        #region ISysRoute 成员
        /// <summary>
        /// 添加房型
        /// </summary>
        /// <param name="info">房型实体</param>
        /// <returns>1:成功、0:失败、2:该房型已存在</returns>

        public int AddRoom(MSysRoom info)
        {

            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysRoomType_Add");

            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "TypeName", DbType.String, info.TypeName);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.Int32, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.String, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            this._db.AddOutParameter(cmd, "result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));

        }
        /// <summary>
        /// 修改房型
        /// </summary>
        /// <param name="info">房型实体</param>
        /// <returns>1:成功、0:失败、2:该房型已存在</returns>
        public int UpdateRoom(MSysRoom info)
        {

            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysRoomType_Update");
            _db.AddInParameter(cmd, "RoomId", DbType.AnsiStringFixedLength, info.RoomId);
            _db.AddInParameter(cmd, "TypeName", DbType.String, info.TypeName);

            this._db.AddOutParameter(cmd, "result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));

        }
        /// <summary>
        /// 删除房型
        /// </summary>
        /// <param name="info">房型实体</param>
        /// <returns>1:成功、0:失败、2:该房型被使用</returns>
        public int DeleteRoom(string id, string comPanyID)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysRoomType_Delete");
            this._db.AddInParameter(cmd, "RoomId", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, comPanyID);

            this._db.AddOutParameter(cmd, "result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result"));

        }

        /// <summary>
        /// 获取房型集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<MSysRoom> GetRoomList(string companyId, int pageSize, int pageIndex, ref int recordCount, MSysRoomSearchModel searchInfo)
        {
            IList<EyouSoft.Model.SysStructure.MSysRoom> items = new List<EyouSoft.Model.SysStructure.MSysRoom>();
            string TableName = "tbl_ComRoomTypeManage";
            string OrderByString = "IssueTime DESC";
            string fields = "*";
            //StringBuilder cmdQuery = new StringBuilder("SELECT [RoomId],[CompanyId],[TypeName],[OperatorDeptId],[OperatorId],[Operator],[IssueTime],[IsDelete]  FROM [tbl_ComRoomTypeManage]");
            //DbCommand comm = this._db.GetSqlStringCommand(cmdQuery.ToString());

            string sql = string.Format("CompanyId='{0}'", companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, string.Empty, fields, sql, OrderByString))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.SysStructure.MSysRoom();
                    item.RoomId = rdr["RoomId"].ToString();
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.TypeName = rdr["TypeName"].ToString();
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
        /// 获取房型信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MSysRoom> GetRooms(string companyId)
        {
            IList<MSysRoom> items = new List<MSysRoom>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetRooms);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysRoom();
                    item.RoomId = rdr["RoomId"].ToString();
                    item.TypeName = rdr["TypeName"].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        public MSysRoom getModel(string id, string comID)
        {
            MSysRoom model = null;
            string sql = "SELECT  RoomId,CompanyId,TypeName,OperatorDeptId,OperatorId,Operator    FROM [tbl_ComRoomTypeManage] Where  IsDelete='0' and RoomId=@RoomId and CompanyId=@CompanyId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "RoomId", DbType.AnsiStringFixedLength, id);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, comID);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (dr != null && dr.Read())
                {
                    model = new MSysRoom()
                    {
                        RoomId = id,
                        CompanyId = dr["CompanyId"].ToString(),
                        TypeName = dr["TypeName"].ToString(),
                        OperatorDeptId = Utils.GetInt(dr["OperatorDeptId"].ToString()),
                        OperatorId = dr["OperatorId"].ToString(),
                        Operator = dr["Operator"].ToString()

                    };
                }
            }

            return model;
        }
        #endregion
    }
}
