using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.SysStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.ComStructure
{
    ///// <summary>
    ///// 常用城市
    ///// 创建者：郑付杰
    ///// 创建时间：2011/9/29
    ///// </summary>
    //public class DComCity : DALBase, EyouSoft.IDAL.ComStructure.IComCity
    //{
    //    private readonly Database _db = null;
    //    /// <summary>
    //    /// 常用城市
    //    /// </summary>
    //    public DComCity()
    //    {
    //        this._db = base.SystemStore;
    //    }

    //    #region IComCity 成员
    //    ///// <summary>
    //    ///// 设置常用城市集合
    //    ///// </summary>
    //    ///// <param name="list">常用城市</param>
    //    ///// <param name="companyId">公司编号</param>
    //    ///// <returns>true：设置成功 false：设置失败</returns>
    //    //public bool SetCity(IList<MComCity> list, string companyId)
    //    //{
    //    //    StringBuilder xml = new StringBuilder();
    //    //    xml.Append("<item>");
    //    //    foreach (MComCity item in list)
    //    //    {
    //    //        xml.AppendFormat("<city id='{0}'>{1}</city>", companyId, item.CityId);
    //    //    }
    //    //    xml.Append("</item>");


    //    //    DbCommand comm = this._db.GetStoredProcCommand("proc_ComCity_Update");
    //    //    this._db.AddInParameter(comm, "@xml", DbType.Xml, xml.ToString());
    //    //    this._db.AddInParameter(comm, "@companyid", DbType.AnsiStringFixedLength, companyId);

    //    //    int result = DbHelper.ExecuteSql(comm, this._db);

    //    //    return result > 0 ? true : false;
    //    //}
    //    /// <summary>
    //    /// 设置常用城市
    //    /// </summary>
    //    /// <param name="cityId">城市编号</param>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>true：设置成功 false：设置失败</returns>
    //    public bool SetCity(int cityId, string companyId)
    //    {
    //        StringBuilder sql = new StringBuilder();
    //        sql.AppendFormat("if not exists(select id from tbl_ComCity where CityId = {0} and CompanyId = '{1}')", cityId, companyId);
    //        sql.Append(" begin");
    //        sql.AppendFormat(" insert into tbl_ComCity(CompanyId,CityId) values('{0}',{1})", companyId, cityId);
    //        sql.Append(" end else begin");
    //        sql.AppendFormat(" delete from tbl_ComCity where CityId = {0} and CompanyId = '{1}'", cityId, companyId);
    //        sql.Append(" end");

    //        DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

    //        int result = DbHelper.ExecuteSql(comm, this._db);

    //        return result > 0 ? true : false;
    //    }

    //    /// <summary>
    //    /// 获取公司常用国家省份城市县区
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns></returns>
    //    public IList<MSysCountry> GetAllCity(string companyId)
    //    {
    //        #region sql
    //        StringBuilder sql = new StringBuilder();
    //        sql.Append("with com_city(id,parentId,p1id,p2id,[name],jp,qp,enname,i,companyId)");
    //        sql.Append(" as");
    //        sql.Append(" (");
    //        sql.Append(" select a.CityId,b.ProvinceId,0,0,b.[Name],b.JP,b.QP,b.EnName,3,a.CompanyId from tbl_ComCity a");
    //        sql.AppendFormat(" inner join tbl_SysCity b on a.CityId = b.CityId where a.CompanyId = '{0}'", companyId);
    //        sql.Append(" union all");
    //        sql.Append(" select a.ProvinceId,a.CountryId,0,0,a.[Name],a.JP,a.QP,a.EnName,2,b.companyId FROM tbl_SysProvince a");
    //        sql.Append(" inner join com_city b on a.ProvinceId = b.parentId where b.i = 3");
    //        sql.Append(" union all	");
    //        sql.Append(" select a.CountryId,0,0,0,a.[Name],a.JP,a.QP,a.EnName,1,a.CompanyId from tbl_SysCountry a");
    //        sql.Append(" inner join com_city b on a.CountryId = b.parentId where b.i = 2");
    //        sql.Append(" union all");
    //        sql.Append(" select a.CountyId,a.CityId,c.ProvinceId,0,a.[Name],a.JP,a.QP,a.EnName,4,b.companyId FROM tbl_SysCounty a");
    //        sql.Append(" inner join com_city b on a.CityId = b.id");
    //        sql.Append(" inner join tbl_SysProvince c on b.parentId = c.ProvinceId");
    //        sql.Append(" where b.i = 3");
    //        sql.Append(" )");
    //        sql.Append(" select distinct* from com_city");
    //        #endregion
    //        DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

    //        IList<MSysCountry> list = new List<MSysCountry>();

    //        MSysCountry item = null;
    //        MSysProvince province = null;
    //        MSysCity city = null;
    //        MSysDistrict district = null;
    //        int i;
    //        //上级编号
    //        int parentId;
    //        //上级的上级编号
    //        int par1Id;
    //        string jp = string.Empty;
    //        string qp = string.Empty;
    //        string enName = string.Empty;
    //        using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
    //        {
    //            while (reader.Read())
    //            {
    //                i = (int)reader["i"];
    //                par1Id = (int)reader["p1id"];
    //                parentId = (int)reader["parentId"];
    //                jp = reader.IsDBNull(reader.GetOrdinal("jp")) ? string.Empty : reader["jp"].ToString();
    //                qp = reader.IsDBNull(reader.GetOrdinal("qp")) ? string.Empty : reader["qp"].ToString();
    //                enName = reader.IsDBNull(reader.GetOrdinal("enname")) ? string.Empty : reader["enname"].ToString();
    //                #region
    //                switch (i)
    //                {
    //                    case 1:
    //                        item = new MSysCountry()
    //                        {
    //                            CountryId = (int)reader["id"],
    //                            JP = jp,
    //                            QP = qp,
    //                            EnName = enName,
    //                            Name = reader["name"].ToString()
    //                        };
    //                        item.Provinces = new List<MSysProvince>();
    //                        list.Add(item);
    //                        break;
    //                    case 2:
    //                        province = new MSysProvince()
    //                        {
    //                            Name = reader["name"].ToString(),
    //                            JP = jp,
    //                            QP = qp,
    //                            EnName = enName,
    //                            ProvinceId = (int)reader["id"],
    //                            CountryId = (int)reader["parentId"]
    //                        };
    //                        province.Citys = new List<MSysCity>();
    //                        item.Provinces.Add(province);
    //                        break;
    //                    case 3:
    //                        city = new MSysCity()
    //                        {
    //                            JP = jp,
    //                            QP = qp,
    //                            EnName = enName,
    //                            ProvinceId = parentId,
    //                            CityId = (int)reader["id"],
    //                            Name = reader["name"].ToString()
    //                        };
    //                        city.Districts = new List<MSysDistrict>();
    //                        item.Provinces.Single(p => p.ProvinceId == parentId).Citys.Add(city);
    //                        break;
    //                    case 4:
    //                        district = new MSysDistrict()
    //                        {
    //                            JP = jp,
    //                            QP = qp,
    //                            EnName = enName,
    //                            DistrictId = (int)reader["id"],
    //                            CityId = parentId,
    //                            Name = reader["name"].ToString()
    //                        };
    //                        item.Provinces.Single(p => p.ProvinceId == par1Id).Citys.Single(c => c.CityId == parentId).Districts.Add(district);
    //                        break;
    //                }
    //                #endregion
    //            }
    //        }

    //        return list;
    //    }

    //    /// <summary>
    //    /// 获取公司常用城市编号
    //    /// </summary>
    //    /// <param name="companyId">公司编号</param>
    //    /// <returns>成员城市编号集合</returns>
    //    public IList<int> GetCityId(string companyId)
    //    {
    //        string sql = "SELECT CityId FROM tbl_ComCity WHERE CompanyId = @companyId";
    //        DbCommand comm = this._db.GetSqlStringCommand(sql);
    //        this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
    //        IList<int> list = new List<int>();
    //        using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
    //        {
    //            while (reader.Read())
    //            {
    //                list.Add((int)reader["CityId"]);
    //            }
    //        }
    //        return list;
    //    }

       
    //    #endregion

    //}
}
