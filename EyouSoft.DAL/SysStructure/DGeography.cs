using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.SysStructure;
using EyouSoft.Toolkit.DAL;
using System.Xml.Linq;
using EyouSoft.Toolkit;

namespace EyouSoft.DAL.SysStructure
{
    /// <summary>
    /// 国家省份城市县区相关数据访问类
    /// </summary>
    public class DGeography : DALBase, EyouSoft.IDAL.SysStructure.IGeography
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetCountrys = "SELECT *,(SELECT *,(SELECT *,(SELECT * FROM tbl_SysCounty AS D WHERE D.CityId=C.CityId FOR XML RAW,ROOT('root')) AS DistrictXml FROM tbl_SysCity AS C WHERE C.ProvinceId=B.ProvinceId FOR XML RAW,ROOT('root')) AS CityXml FROM tbl_SysProvince AS B WHERE B.CountryId=A.CountryId FOR XML RAW,ROOT('root')) AS ProvinceXml FROM tbl_SysCountry AS A WHERE A.CompanyId=@CompanyId";
        const string SQL_INSERT_InsertCountry = "INSERT INTO [tbl_SysCountry]([CompanyId],[Name],[JP],[QP],[EnName],[IsDefault],[ThName]) VALUES (@CompanyId,@Name,@JP,@QP,@EnName,@IsDefault,@ThName)";
        const string SQL_UPDATE_UpdateCountry = "UPDATE [tbl_SysCountry] SET [Name]=@Name,[JP]=@JP,[QP]=@QP,[EnName]=@EnName,[ThName]=@ThName WHERE [CountryId]=@CountryId";
        const string SQL_DELETE_DeleteCountry = "DELETE FROM [tbl_SysCountry] WHERE [CountryId]=@CountryId";
        const string SQL_INSERT_InsertProvince = "INSERT INTO [tbl_SysProvince]([CountryId],[Name],[JP],[QP],[EnName],[ThName]) VALUES (@CountryId,@Name,@JP,@QP,@EnName,@ThName)";
        const string SQL_UPDATE_UpdateProvince = "UPDATE [tbl_SysProvince] SET [Name]=@Name,[JP]=@JP,[QP]=@QP,[EnName]=@EnName,[ThName]=@ThName WHERE [ProvinceId]=@ProvinceId";
        const string SQL_DELETE_DeleteProvince = "DELETE FROM [tbl_SysProvince] WHERE [ProvinceId]=@ProvinceId";
        const string SQL_INSERT_InsertCity = "INSERT INTO [tbl_SysCity]([ProvinceId],[Name],[JP],[QP],[EnName],[ThName]) VALUES (@ProvinceId,@Name,@JP,@QP,@EnName,@ThName)";
        const string SQL_INSERT_InsertDistry = "INSERT INTO [tbl_SysCounty]([CityId],[Name],[JP],[QP],[EnName],[ThName]) VALUES (@CityId,@Name,@JP,@QP,@EnName,@ThName)";
        const string SQL_UPDATE_UpdateCity = "UPDATE [tbl_SysCity] SET [Name]=@Name,[JP]=@JP,[QP]=@QP,[EnName]=@EnName,[ThName]=@ThName WHERE [CityId]=@CityId";
        const string SQL_DELETE_DeleteCity = "DELETE FROM [tbl_SysCity] WHERE [CityId]=@CityId";
        #endregion

        #region 构造函数
        private readonly Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DGeography()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region private members
        /// <summary>
        /// parse province xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IList<MSysProvince> ParseProvinceXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MSysProvince> items = new List<MSysProvince>();

            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            foreach (var xrow in xrows)
            {
                var item = new MSysProvince();

                item.Citys = ParseCityXml(Utils.GetXAttributeValue(xrow, "CityXml"));
                item.CountryId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "CountryId"));
                item.EnName = Utils.GetXAttributeValue(xrow, "EnName");
                item.JP = Utils.GetXAttributeValue(xrow, "JP");
                item.Name = Utils.GetXAttributeValue(xrow, "Name");
                item.ProvinceId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "ProvinceId"));
                item.QP = Utils.GetXAttributeValue(xrow, "QP");
                item.ThName = Utils.GetXAttributeValue(xrow, "ThName");

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// parse city xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IList<MSysCity> ParseCityXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MSysCity> items = new List<MSysCity>();

            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            foreach (var xrow in xrows)
            {
                var item = new MSysCity();

                item.CityId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "CityId"));
                item.Districts = ParseDistrictXml(Utils.GetXAttributeValue(xrow, "DistrictXml"));
                item.EnName = Utils.GetXAttributeValue(xrow, "EnName");
                item.JP = Utils.GetXAttributeValue(xrow, "JP");
                item.Name = Utils.GetXAttributeValue(xrow, "Name");
                item.QP = Utils.GetXAttributeValue(xrow, "QP");
                item.ProvinceId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "ProvinceId"));
                item.ThName = Utils.GetXAttributeValue(xrow, "ThName");

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// parse district xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IList<MSysDistrict> ParseDistrictXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MSysDistrict> items = new List<MSysDistrict>();

            var xroot = XElement.Parse(xml);
            var xrows = Utils.GetXElements(xroot, "row");

            foreach (var xrow in xrows)
            {
                var item = new MSysDistrict();

                item.CityId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "CityId"));
                item.DistrictId = Utils.GetInt(Utils.GetXAttributeValue(xrow, "CountyId"));
                item.EnName = Utils.GetXAttributeValue(xrow, "EnName");
                item.JP = Utils.GetXAttributeValue(xrow, "JP");
                item.Name = Utils.GetXAttributeValue(xrow, "Name");
                item.QP = Utils.GetXAttributeValue(xrow, "QP");
                item.ThName = Utils.GetXAttributeValue(xrow, "ThName");

                items.Add(item);
            }

            return items;
        }
        #endregion

        #region IGeography 成员
        /// <summary>
        /// 获取国家信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MSysCountry> GetCountrys(string companyId)
        {
            IList<MSysCountry> items = new List<MSysCountry>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetCountrys);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new MSysCountry();

                    item.CompanyId = companyId;
                    item.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.EnName = rdr["EnName"].ToString();
                    item.JP = rdr["JP"].ToString();
                    item.Name = rdr["Name"].ToString();
                    item.Provinces = ParseProvinceXml(rdr["ProvinceXml"].ToString());
                    item.QP = rdr["QP"].ToString();
                    item.ThName = rdr["ThName"].ToString();
                    item.IsDefault = rdr["IsDefault"].ToString() == "1";

                    items.Add(item);
                }
            }

            return items;
        }
        /// <summary>
        /// 分页获取县区信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="namejp">县区名称简拼</param>
        /// <returns>县区信息集合</returns>
        public IList<MSysDistrict> GetAreas(int pageCurrent, int pageSize, ref int pageCount, string companyId, string namejp)
        {
            IList<MSysDistrict> list = new List<MSysDistrict>();
            StringBuilder fields = new StringBuilder();
            fields.Append("CountyId,Name,EnName,ThName,JP,QP");
            string tableName = "tbl_SysCounty";
            StringBuilder query = new StringBuilder("cityid in (select cityid from tbl_SysCity where ProvinceId in (select ProvinceId from tbl_SysProvince where CountryId in (select CountryId from tbl_SysCountry where companyid='" + companyId + "')))");
            if (!string.IsNullOrEmpty(namejp))
            {
                query.AppendFormat(" and JP like '%{0}%' ", namejp);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref pageCount,
                 tableName, "CountyId", fields.ToString(), query.ToString(), " CountyId desc "))
            {
                while (dr.Read())
                {
                    MSysDistrict model = new MSysDistrict();
                    model.DistrictId = dr.GetInt32(dr.GetOrdinal("CountyId"));
                    model.Name = !dr.IsDBNull(dr.GetOrdinal("Name")) ? dr.GetString(dr.GetOrdinal("Name")) : "";
                    model.EnName = !dr.IsDBNull(dr.GetOrdinal("EnName")) ? dr.GetString(dr.GetOrdinal("EnName")) : "";
                    model.ThName = !dr.IsDBNull(dr.GetOrdinal("ThName")) ? dr.GetString(dr.GetOrdinal("ThName")) : "";
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 新增国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCountry(EyouSoft.Model.SysStructure.MSysCountry info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertCountry);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "IsDefault", DbType.AnsiStringFixedLength, "0");
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 新增省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertProvince(EyouSoft.Model.SysStructure.MSysProvince info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertProvince);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 新增城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertCity(EyouSoft.Model.SysStructure.MSysCity info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertCity);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 新增县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDistrict(EyouSoft.Model.SysStructure.MSysDistrict info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_INSERT_InsertDistry);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCountry(EyouSoft.Model.SysStructure.MSysCountry info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateCountry);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateProvince(EyouSoft.Model.SysStructure.MSysProvince info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateProvince);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateCity(EyouSoft.Model.SysStructure.MSysCity info)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_UPDATE_UpdateCity);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            _db.AddInParameter(cmd, "Name", DbType.String, info.Name);
            _db.AddInParameter(cmd, "JP", DbType.String, info.JP);
            _db.AddInParameter(cmd, "QP", DbType.String, info.QP);
            _db.AddInParameter(cmd, "EnName", DbType.String, info.EnName);
            _db.AddInParameter(cmd, "ThName", DbType.String, info.ThName);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 更新县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateDistrict(EyouSoft.Model.SysStructure.MSysDistrict info)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除国家信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public int DeleteCountry(string companyId, int countryId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteCountry);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, countryId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除省份信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="provinceId">省份编号</param>
        /// <returns></returns>
        public int DeleteProvince(string companyId, int provinceId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteProvince);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, provinceId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除城市信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public int DeleteCity(string companyId, int cityId)
        {
            DbCommand cmd = _db.GetSqlStringCommand(SQL_DELETE_DeleteCity);
            _db.AddInParameter(cmd, "CityId", DbType.Int32, cityId);

            return DbHelper.ExecuteSql(cmd, _db) == 1 ? 1 : -100;
        }

        /// <summary>
        /// 删除县区信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="districtId">县区编号</param>
        /// <returns></returns>
        public int DeleteDistrict(string companyId, int[] districtId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("DELETE FROM [tbl_SysCounty] WHERE [CountyId] in ({0})", Utils.GetSqlIn<int>(districtId));
            DbCommand cmd = _db.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(cmd, _db) >= 1 ? 1 : -100;
        }
        #endregion
    }
}