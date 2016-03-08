//金蝶相关数据访问类 汪奇志 2012-05-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using System.Data.Common;
using System.Data;

namespace EyouSoft.DAL.FinStructure
{
    /// <summary>
    /// 金蝶相关数据访问类
    /// </summary>
    public class DKis : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.FinStructure.IKis
    {
        #region static constants
        //static constants
        const string SQL_SELECT_GetAccountGroups = "SELECT SubjectId,SubjectCd,SubjectNm FROM tbl_FinKingDeeSubject WHERE CompanyId=@CompanyId AND IsDeleted='0' ";
        #endregion

        #region constructor
        /// <summary>
        /// db
        /// </summary>
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public DKis()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取KIS科目集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.FinStructure.MKisAccountGroupInfo> GetAccountGroups(string companyId)
        {
            IList<EyouSoft.Model.FinStructure.MKisAccountGroupInfo> items = new List<EyouSoft.Model.FinStructure.MKisAccountGroupInfo>();
            DbCommand cmd = _db.GetSqlStringCommand(SQL_SELECT_GetAccountGroups);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (rdr.Read())
                {
                    var item = new EyouSoft.Model.FinStructure.MKisAccountGroupInfo();

                    item.Code = rdr["SubjectCd"].ToString();
                    item.Name = rdr["SubjectNm"].ToString();

                    items.Add(item);
                }
            }

            return items;
        }
        #endregion

    }
}
