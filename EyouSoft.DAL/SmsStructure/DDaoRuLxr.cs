using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.SmsStructure
{
    /// <summary>
    /// 短信中心导入客户管理号码数据访问类
    /// </summary>
    public class DDaoRuLxr : EyouSoft.Toolkit.DAL.DALBase, IDAL.SmsStructure.IDaoRuLxr
    {
        #region constructor
        private readonly Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DDaoRuLxr() { _db = SystemStore; }
        #endregion

        #region public members
        /// <summary>
        /// 获取导入客户管理号码集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo> GetLxrs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SmsStructure.MLBDaoRuLxrSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo> items = new List<EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo>();

            string tableName = "view_Sms_Lxr";
            string fields = "CountryId,ProvinceId,CityId,DistrictId,DanWeiName,LxrName,DanWeiType,MingXiType,Mobile";
            string orderByString = "IssueTime DESC";
            StringBuilder query = new StringBuilder();

            #region SQL
            query.AppendFormat(" CompanyId='{0}' ", companyId);

            if (searchInfo != null)
            {
                if (searchInfo.CityId.HasValue)
                {
                    query.AppendFormat(" AND CityId={0} ", searchInfo.CityId.Value);
                }
                if (searchInfo.CountryId.HasValue)
                {
                    query.AppendFormat(" AND CountryId={0} ", searchInfo.CountryId.Value);
                }
                if (searchInfo.DanWeiType.HasValue)
                {
                    int _danWeiType = -1;
                    int _mingXiType = -1;
                    switch (searchInfo.DanWeiType.Value)
                    {
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.餐馆:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.车队:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.车队;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.分销商:
                            _danWeiType = 0;
                            _mingXiType = (int)EyouSoft.Model.EnumType.CrmStructure.CrmType.同行客户;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.地接社:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.地接社;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.购物:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.景点:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.酒店:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.区间交通:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.区间交通;
                            break;
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.其他:
                            _danWeiType = 1;
                            _mingXiType = (int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.其他;
                            break;                        
                        case EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.系统用户:
                            _danWeiType = 2;
                            break;
                        default: break;
                    }

                    if (_danWeiType > -1)
                    {
                        query.AppendFormat(" AND DanWeiType={0} ", _danWeiType);
                    }

                    if (_mingXiType > -1)
                    {
                        query.AppendFormat(" AND MingXiType={0} ", _mingXiType);
                    }
                }
                if (searchInfo.DistrictId.HasValue)
                {
                    query.AppendFormat(" AND DistrictId={0} ", searchInfo.DistrictId.Value);
                }
                if (searchInfo.ProvinceId.HasValue)
                {
                    query.AppendFormat(" AND ProvinceId={0} ", searchInfo.ProvinceId.Value);
                }
            }
           
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(_db, pageSize, pageIndex, ref recordCount, tableName, string.Empty, fields, query.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo item = new EyouSoft.Model.SmsStructure.MLBDaoRuLxrInfo();

                    item.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.CountryId = rdr.GetInt32(rdr.GetOrdinal("CountryId"));
                    item.CPCD = null;
                    item.DanWeiName = rdr["DanWeiName"].ToString();
                    item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.系统用户;
                    item.DistrictId = rdr.GetInt32(rdr.GetOrdinal("DistrictId"));                    
                    item.LxrName = rdr["LxrName"].ToString();
                    item.Mobile = rdr["Mobile"].ToString().Trim();
                    item.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));

                    //类型转换
                    int _danWeiType=rdr.GetInt32(rdr.GetOrdinal("DanWeiType")) ;
                    int _mingXiType = rdr.GetByte(rdr.GetOrdinal("MingXiType"));

                    if (_danWeiType == 0)
                    {
                        EyouSoft.Model.EnumType.CrmStructure.CrmType crmType = (EyouSoft.Model.EnumType.CrmStructure.CrmType)_mingXiType;

                        item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.分销商;
                    }
                    else if (_danWeiType == 1)
                    {
                        EyouSoft.Model.EnumType.GysStructure.GysLeiXing sourceType = (EyouSoft.Model.EnumType.GysStructure.GysLeiXing)_mingXiType;

                        switch (sourceType)
                        {
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.餐馆; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.车队: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.车队; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.地接社: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.地接社; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.购物; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.景点; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.酒店; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.区间交通: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.区间交通; break;
                            case EyouSoft.Model.EnumType.GysStructure.GysLeiXing.其他: item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.其他; break;
                        }
                    }
                    else if (_danWeiType == 2)
                    {
                        item.DanWeiType = EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType.系统用户;
                    }

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion
    }
}
