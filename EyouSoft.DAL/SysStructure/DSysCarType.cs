using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 描述：系统车型模板接口类
    /// 修改记录：
    /// 1、2012-08-14 PM 王磊 创建
    /// </summary>
    public class DSysCarType : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.SysStructure.ISysCarType
    {
        #region 初始化db
        private Database _db = null;

        /// <summary>
        /// 初始化_db
        /// </summary>
        public DSysCarType()
        {
            _db = base.SystemStore;
        }
        #endregion

        private string SQL_SELECT_GETCarTemplateList = "select tbl_SysCarType.Id,SeatNum,TempLateId,FilePath,IsDefault,(SELECT * FROM  tbl_SysCarTypeSeat where TemplateId=tbl_SysCarTypeTemplate.TemplateId FOR XML RAW ,ROOT)  AS SeatList from  tbl_SysCarType left join tbl_SysCarTypeTemplate on tbl_SysCarType.Id=tbl_SysCarTypeTemplate.Id where tbl_SysCarType.Id=@Id";

        private const string SQL_SELECT_GetCarTypeTemplaterInfo = "select tbl_SysCarType.Id,SeatNum,TempLateId,FilePath,IsDefault,(SELECT * FROM  tbl_SysCarTypeSeat where TemplateId=tbl_SysCarTypeTemplate.TemplateId FOR XML RAW ,ROOT)  AS SeatList from  tbl_SysCarType left join tbl_SysCarTypeTemplate on tbl_SysCarType.Id=tbl_SysCarTypeTemplate.Id where TempLateId=@TempLateId";

        private string _SQL_SELECT_GETCarTemplateList = "select tbl_SysCarType.Id,SeatNum,TempLateId,FilePath,IsDefault,(SELECT * FROM  tbl_SysCarTypeSeat where TemplateId=tbl_SysCarTypeTemplate.TemplateId FOR XML RAW ,ROOT)  AS SeatList from tbl_SysCarTypeTemplate  inner join tbl_SysCarType on tbl_SysCarType.Id=tbl_SysCarTypeTemplate.Id ";

        private const string SQL_SELECT_GetSysCarType = "SELECT * from tbl_SysCarType";

        #region ISysCarType 成员

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="SeatNum"></param>
        /// <returns>1:成功，0：失败 2:该座位数的车型在系统中已存在</returns>
        public int AddSysCarType(int SeatNum)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Add");
            this._db.AddInParameter(cmd, "SeatNum", DbType.Int32, SeatNum);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 添加车型模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns>1:成功，0：失败 </returns>
        public int AddSysCarType(EyouSoft.Model.SysStructure.MSysCarType model)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Add_Template");
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, model.TemplateId);
            this._db.AddInParameter(cmd, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(cmd, "IsDefault", DbType.Boolean, model.IsDefault == true ? 1 : 0);
            this._db.AddInParameter(cmd, "FilePath", DbType.String, model.FilePath);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));

        }

        /// <summary>
        /// 添加车型模板坐标
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <param name="list"></param>
        /// <returns>1:成功，0：失败</returns>
        public int AddSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Add_Seat");
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, TemplateId);
            this._db.AddInParameter(cmd, "CarTypeSeat", DbType.Xml, CreateCarTypeSeatXML(list));
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }



        /// <summary>
        /// 修改系统车位座位
        /// </summary>
        /// <returns></returns>
        public bool UpdateSysCarType(string TemplateId, IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {

            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Update");
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, TemplateId);
            this._db.AddInParameter(cmd, "CarTypeSeat", DbType.Xml, CreateCarTypeSeatXML(list));
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 设置系统默认车型
        /// </summary>
        /// <param name="Id">车型编号</param>
        /// <param name="TemplateId">模板编号</param>
        /// <returns></returns>
        public bool UpdateSysCarType(int Id,string TemplateId)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Update_IsDefault");
            this._db.AddInParameter(cmd, "Id", DbType.Int32, Id);
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, TemplateId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }


        /// <summary>
        /// 删除座位数
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1:成功，0：失败 2:该车型下的模板已有公司使用</returns>
        public int DeleteSysCarTypeSeatNum(int id)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Delete");
            this._db.AddInParameter(cmd, "Id", DbType.Int32, id);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }


        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns>1:成功，0：失败 2:该模板已有公司使用</returns>
        public int DeleteSysCarTypeTempLate(string TemplateId)
        {

            DbCommand cmd = this._db.GetStoredProcCommand("proc_SysCarType_Delete_Template");
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, TemplateId);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);
            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));
        }


        /// <summary>
        /// 根据座位的编号获取车型模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList(int id)
        {
            IList<EyouSoft.Model.SysStructure.MSysCarType> list = null;

            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GETCarTemplateList);
            this._db.AddInParameter(cmd, "id", DbType.String, id);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.SysStructure.MSysCarType>();
                    while (dr.Read())
                    {
                        EyouSoft.Model.SysStructure.MSysCarType CarType = new EyouSoft.Model.SysStructure.MSysCarType();
                        CarType.TemplateId = dr["TemplateId"].ToString();

                        CarType.SeatNum = dr.IsDBNull(dr.GetOrdinal("SeatNum")) ? 0 : dr.GetInt32(dr.GetOrdinal("SeatNum"));
                        CarType.IsDefault = dr.IsDBNull(dr.GetOrdinal("IsDefault")) ? false : dr.GetString(dr.GetOrdinal("IsDefault")) == "1" ? true : false;
                        CarType.FilePath = dr.IsDBNull(dr.GetOrdinal("FilePath")) ? string.Empty : dr.GetString(dr.GetOrdinal("FilePath"));
                        CarType.list = GetCarTypeSeatByXML(dr["SeatList"].ToString());
                        list.Add(CarType);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取车型模板
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarType> GetCarTypeList()
        {
            IList<EyouSoft.Model.SysStructure.MSysCarType> list = null;

            DbCommand cmd = this._db.GetSqlStringCommand(_SQL_SELECT_GETCarTemplateList);

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.SysStructure.MSysCarType>();
                    while (dr.Read())
                    {
                        EyouSoft.Model.SysStructure.MSysCarType CarType = new EyouSoft.Model.SysStructure.MSysCarType();
                        CarType.TemplateId = dr["TemplateId"].ToString();
                        CarType.Id = Utils.GetInt(dr["Id"].ToString());
                        CarType.SeatNum = dr.IsDBNull(dr.GetOrdinal("SeatNum")) ? 0 : dr.GetInt32(dr.GetOrdinal("SeatNum"));
                        CarType.IsDefault = dr.IsDBNull(dr.GetOrdinal("IsDefault")) ? false : dr.GetString(dr.GetOrdinal("IsDefault")) == "1" ? true : false;
                        CarType.FilePath = dr.IsDBNull(dr.GetOrdinal("FilePath")) ? string.Empty : dr.GetString(dr.GetOrdinal("FilePath"));
                        CarType.list = GetCarTypeSeatByXML(dr["SeatList"].ToString());
                        list.Add(CarType);
                    }
                }
            }
            return list;

        }




        /// <summary>
        /// 根据模板编号获取车型模板
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public EyouSoft.Model.SysStructure.MSysCarType GetCarTypeById(string TemplateId)
        {
            EyouSoft.Model.SysStructure.MSysCarType CarType = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetCarTypeTemplaterInfo);
            this._db.AddInParameter(cmd, "TemplateId", DbType.AnsiStringFixedLength, TemplateId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr != null)
                {

                    while (dr.Read())
                    {
                        CarType = new EyouSoft.Model.SysStructure.MSysCarType();
                        CarType.TemplateId = dr["TemplateId"].ToString();

                        CarType.SeatNum = dr.IsDBNull(dr.GetOrdinal("SeatNum")) ? 0 : dr.GetInt32(dr.GetOrdinal("SeatNum"));
                        CarType.IsDefault = dr.IsDBNull(dr.GetOrdinal("IsDefault")) ? false : dr.GetString(dr.GetOrdinal("IsDefault")) == "1" ? true : false;
                        CarType.FilePath = dr.IsDBNull(dr.GetOrdinal("FilePath")) ? string.Empty : dr.GetString(dr.GetOrdinal("FilePath"));
                        CarType.list = GetCarTypeSeatByXML(dr["SeatList"].ToString());

                    }
                }
            }
            return CarType;
        }

        /// <summary>
        /// 获取车型座位数
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> GetCarTypeSeatNumList()
        {
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> list = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSysCarType);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (dr != null)
                {
                    list = new List<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum>();
                    while (dr.Read())
                    {
                        EyouSoft.Model.SysStructure.MSysCarTypeSeatNum model = new EyouSoft.Model.SysStructure.MSysCarTypeSeatNum()
                        {
                            Id = Utils.GetInt(dr["Id"].ToString()),
                            SeatNum = Utils.GetInt(dr["SeatNum"].ToString())
                        };
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 根据xml获取模板座位
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateCarTypeSeatXML(IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list)
        {
            if (list != null && list.Count != 0)
            {
                StringBuilder xmlDoc = new StringBuilder();
                xmlDoc.Append("<Root>");
                foreach (var item in list)
                {
                    xmlDoc.AppendFormat("<CarTypeSeat SeatNumber=\"{0}\"  PointX=\"{1}\"  PoinY=\"{2}\" />", item.SeatNumber, item.PointX, item.PoinY);
                }
                xmlDoc.Append("</Root>");
                return xmlDoc.ToString();
            }
            return null;
        }


        /// <summary>
        /// 根据xml获取模板座位
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> GetCarTypeSeatByXML(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;

            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeat> list = new List<EyouSoft.Model.SysStructure.MSysCarTypeSeat>();
            EyouSoft.Model.SysStructure.MSysCarTypeSeat carTypeSeat = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                carTypeSeat = new EyouSoft.Model.SysStructure.MSysCarTypeSeat()
                {
                    SeatNumber = Utils.GetInt(Utils.GetXAttributeValue(xRow, "SeatNumber")),
                    PointX = Utils.GetInt(Utils.GetXAttributeValue(xRow, "PointX")),
                    PoinY = Utils.GetInt(Utils.GetXAttributeValue(xRow, "PoinY"))
                };
                list.Add(carTypeSeat);
            }
            return list;
        }


        #endregion
    }
}
