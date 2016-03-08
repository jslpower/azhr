using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Model.EnumType.ComStructure;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 团号生成规则数据层
    /// 创建者：郑付杰
    /// 创建时间：2011/9/23
    /// </summary>
    public class DTourNoOptionCode : DALBase, EyouSoft.IDAL.ComStructure.ITourNoOptionCode
    {
        private readonly Database _db = null;
        #region 构造函数
        public DTourNoOptionCode()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region ITourNoOptionCode 成员

        /// <summary>
        /// 添加团号生成规则内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(IList<MTourNoOptionCode> list)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourNoOptionCode_AddOrUpdate");
            this._db.AddInParameter(cmd, "IsAdd", DbType.AnsiStringFixedLength, "1");
            this._db.AddInParameter(cmd, "TourNoOptionCodeList", DbType.Xml, CreateTourNoOptionCodeListXml(list));
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 修改团号生成规则内容
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(IList<MTourNoOptionCode> list)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_TourNoOptionCode_AddOrUpdate");
            this._db.AddInParameter(cmd, "IsAdd", DbType.AnsiStringFixedLength, "0");
            this._db.AddInParameter(cmd, "TourNoOptionCodeList", DbType.Xml, CreateTourNoOptionCodeListXml(list));
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获取团号生成规则信息
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns>团号生成规则实体</returns>
        public IList<MTourNoOptionCode> GetModel(string CompanyId)
        {
            IList<MTourNoOptionCode> list = new List<MTourNoOptionCode>();
            string sql = "SELECT CompanyId,ItemType,ItemId,Code FROM tbl_TourNoOptionCode WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(new MTourNoOptionCode()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        Code = reader.IsDBNull(reader.GetOrdinal("Code")) ? string.Empty : reader["Code"].ToString(),
                        ItemId = reader["ItemId"].ToString().Trim(),
                        ItemType = (OptionItemType)Enum.Parse(typeof(OptionItemType), reader["ItemType"].ToString())
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 创建团号生成规则项目内容列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTourNoOptionCodeListXml(IList<MTourNoOptionCode> list)
        {
            //-团号生成规则项目内容列表XML:<Root><TourNoOptionCodeList CompanyId="公司名称" ItemType="规则项目类型" ItemId="规则项目编号" Code="规则项目编码"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<TourNoOptionCodeList CompanyId=\"{0}\" ItemType=\"{1}\" ItemId=\"{2}\" Code=\"{3}\"/>", item.CompanyId, (int)item.ItemType, item.ItemId, EyouSoft.Toolkit.Utils.ReplaceXmlSpecialCharacter(item.Code));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }
        #endregion
    }
}
