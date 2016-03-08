using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 线路区域
    /// 创建者：郑付杰
    /// 创建时间:2011/9/20
    /// </summary>
    public class DComArea : DALBase, EyouSoft.IDAL.ComStructure.IComArea
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComArea()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComArea 成员
        /// <summary>
        /// 添加线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>返回线路区域编号</returns>
        public bool Add(MComArea info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComArea_Add");


            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AreaName", DbType.String, info.AreaName);
            _db.AddInParameter(cmd, "ChildCompanyId", DbType.Int32, info.ChildCompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.String, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "Keyword", DbType.String, info.Keyword);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result")) == 1 ? true : false;
        }
        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="item">线路区域实体</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Update(MComArea info)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_ComArea_Update");


            _db.AddInParameter(cmd, "id", DbType.Int32, info.AreaId);
            _db.AddInParameter(cmd, "AreaName", DbType.String, info.AreaName);
            _db.AddInParameter(cmd, "ChildCompanyId", DbType.Int32, info.ChildCompanyId);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, (int)info.LngType);
            _db.AddInParameter(cmd, "MasterId", DbType.Int32, info.MasterId);
            _db.AddInParameter(cmd, "OperatorDeptId", DbType.String, info.OperatorDeptId);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.Operator);
            _db.AddInParameter(cmd, "Keyword", DbType.String, info.Keyword);
            _db.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);

            _db.AddOutParameter(cmd, "result", DbType.Int32, 4);
            DbHelper.RunProcedureWithResult(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "result")) == 1 ? true : false;
        }
        /// <summary>
        /// 批量删除线路区域
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaids">线路区域编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Delete(string companyId, params int[] areaids)
        {
            //string sql = "UPDATE tbl_ComArea SET IsDelete = 1 WHERE CHARINDEX(CAST(AreaId as varchar(15)),@ids,0) > 0  AND CompanyId = @CompanyId";
            string sql = string.Format("UPDATE tbl_ComArea SET IsDelete = '1' WHERE AreaId IN({0}) AND CompanyId = @CompanyId", Utils.GetSqlIdStrByArray(areaids));
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            //this._db.AddInParameter(comm, "@ids", DbType.String, areaIds);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域实体</returns>
        public MComArea GetModel(int areaId, string companyId)
        {
            string sql = "select [AreaId],[CompanyId],[AreaName],[ChildCompanyId],[LngType],[MasterId],[OperatorDeptId],[OperatorId],[Operator],[IssueTime],[IsDelete],Keyword from tbl_ComArea where AreaId=@areaId and CompanyId=@CompanyId";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@areaId", DbType.Int32, areaId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    MComArea item = new MComArea()
                    {
                        //AreaId = (int)reader["AreaId"],
                        //CompanyId = reader["CompanyId"].ToString(),
                        //AreaName = reader["AreaName"].ToString(),
                        //Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        //OperatorId = reader["OperatorId"].ToString(),
                        //Plan = GetPlanByXml(reader["PlanList"].ToString())

                        AreaId = (int)reader["AreaId"],
                        CompanyId = companyId,
                        AreaName = reader["AreaName"].ToString(),
                        ChildCompanyId = Utils.GetInt(reader["ChildCompanyId"].ToString()),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        Keyword = reader["Keyword"].ToString()
                    };
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据线路区域编号获取线路区域实体
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="typ">语种</param>
        /// <returns>线路区域实体</returns>
        public MComArea GetModel(int areaId, string companyId,EyouSoft.Model.EnumType.SysStructure.LngType typ)
        {
            string sql = "select [AreaId],[CompanyId],[AreaName],[ChildCompanyId],[LngType],[MasterId],[OperatorDeptId],[OperatorId],[Operator],[IssueTime],[IsDelete] from tbl_ComArea where MasterId=@areaId and CompanyId=@CompanyId and LngType=@LngType";

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@areaId", DbType.Int32, areaId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(comm, "@LngType", DbType.Byte, (int)typ);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    MComArea item = new MComArea()
                    {
                        //AreaId = (int)reader["AreaId"],
                        //CompanyId = reader["CompanyId"].ToString(),
                        //AreaName = reader["AreaName"].ToString(),
                        //Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        //OperatorId = reader["OperatorId"].ToString(),
                        //Plan = GetPlanByXml(reader["PlanList"].ToString())

                        AreaId = (int)reader["AreaId"],
                        CompanyId = companyId,
                        AreaName = reader["AreaName"].ToString(),
                        ChildCompanyId = Utils.GetInt(reader["ChildCompanyId"].ToString()),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString()
                    };
                    return item;
                }
            }

            return null;
        }
        /// <summary>
        /// 分页获取线路区域信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="model">搜索实体</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetList(int pageCurrent, int pageSize, ref int pageCount, string companyId, MComAreaSearch model)
        {
            string tableName = "tbl_ComArea";
            string primaryKey = "AreaId";
            string fields = " [AreaId],[CompanyId],[AreaName],[ChildCompanyId],[LngType],[MasterId],[OperatorDeptId],[OperatorId],[Operator],[IssueTime],[IsDelete] ";


            //AreaId,CompanyId,AreaName,[Type],OperatorId,(select OperatorId as PlanerId,(select ContactName from tbl_ComUser where UserId=tbl_ComAreaPlaner.OperatorId) as Planer from tbl_ComAreaPlaner where AreaId=tbl_ComArea.AreaId  for xml raw,root) PlanList ";

            string query = string.Format(" CompanyId = '{0}' and IsDelete='0'", companyId);
            string orderBy = " AreaId DESC";
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.AreaName))
                {
                    query += string.Format(" and AreaName like '%{0}%'", Utils.ToSqlLike(model.AreaName));
                }
            }
            IList<MComArea> list = new List<MComArea>();
            MComArea item = null;

            string xmlString = string.Empty;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                tableName, primaryKey, fields, query, orderBy))
            {
                while (reader.Read())
                {
                    item = new MComArea()
                    {
                        //AreaId = (int)reader["AreaId"],
                        //CompanyId = reader["CompanyId"].ToString(),
                        //AreaName = reader["AreaName"].ToString(),
                        //Type = (AreaType)Enum.Parse(typeof(AreaType), reader["Type"].ToString()),
                        //OperatorId = reader["OperatorId"].ToString(),
                        //Plan = GetPlanByXml(reader["PlanList"].ToString())

                        AreaId = (int)reader["AreaId"],
                        CompanyId = companyId,
                        AreaName = reader["AreaName"].ToString(),
                        ChildCompanyId = Utils.GetInt(reader["ChildCompanyId"].ToString()),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString()

                    };
                    //计调员

                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据公司编号获取线路区域信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>线路区域集合</returns>
        public IList<MComArea> GetAreaByCID(string companyId,string keyword)
        {
            string sql = "SELECT  [AreaId],[AreaName],[ChildCompanyId],[LngType],[MasterId],[OperatorDeptId],[OperatorId],[Operator],keyword FROM tbl_ComArea WHERE  IsDelete = '0' AND CompanyId = @companyId";
            if(!string.IsNullOrEmpty(keyword))sql+=" and keyword=@keyword";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(comm, "@keyword", DbType.String, keyword);

            IList<MComArea> list = new List<MComArea>();
            MComArea item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComArea()
                    {
                        AreaId = (int)reader["AreaId"],
                        CompanyId = companyId,
                        AreaName = reader["AreaName"].ToString(),
                        ChildCompanyId = Utils.GetInt(reader["ChildCompanyId"].ToString()),
                        LngType = (EyouSoft.Model.EnumType.SysStructure.LngType)Utils.GetInt(reader["LngType"].ToString()),
                        MasterId = Utils.GetInt(reader["MasterId"].ToString()),
                        OperatorDeptId = Utils.GetInt(reader["OperatorDeptId"].ToString()),
                        OperatorId = reader["OperatorId"].ToString(),
                        Operator = reader["Operator"].ToString(),
                        Keyword=reader["keyword"].ToString()

                    });
                }
            }

            return list;
        }

        private IList<MComAreaPlan> GetPlanByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MComAreaPlan> list = new List<MComAreaPlan>();
            MComAreaPlan item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MComAreaPlan()
                {
                    Planer = Utils.GetXAttributeValue(xRow, "Planer"),
                    OperatorId = Utils.GetXAttributeValue(xRow, "PlanerId")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 是否存在线路区域
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lngType"></param>
        /// <returns></returns>
        public bool isEsistsArea(int id, EyouSoft.Model.EnumType.SysStructure.LngType lngType)
        {
            string sql = "SELECT 1 FROM  tbl_ComArea  WHERE MasterId=@Id AND LngType=@LngType ";
            DbCommand cmd = _db.GetSqlStringCommand(sql);
            _db.AddInParameter(cmd, "Id", DbType.Int32, id);
            _db.AddInParameter(cmd, "LngType", DbType.Byte, lngType);
            return DbHelper.Exists(cmd, _db);
        }
        #endregion
    }
}
