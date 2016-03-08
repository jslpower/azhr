using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using EyouSoft.Toolkit.DAL;

namespace EyouSoft.DAL.CrmStructure
{
    /// <summary>
    /// 联系人Dal
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public class DCrmLinkman : EyouSoft.Toolkit.DAL.DALBase, IDAL.CrmStructure.ICrmLinkman
    {
        #region 私有变量
        private Database _db = null;

        /// <summary>
        /// 查询联系人的sql语句
        /// </summary>
        private string sql_Crm_SelectLinkman = "select * from tbl_CrmLinkman where CompanyId=@CompanyId and TypeId=@TypeId and Type=@Type order by SortId asc";


        #endregion

        #region 构造函数
        public DCrmLinkman()
        {
            _db = base.SystemStore;
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 获得相关联的联系人
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeId">类型编号</param>
        /// <param name="Type">类型</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmLinkman> GetCrmLinkManModel(string companyId, string TypeId, Model.EnumType.ComStructure.LxrType Type)
        {
            return GetLinkManModelList(companyId, TypeId, (int)Type);
        }

        /// <summary>
        /// 获得联系人Model
        /// </summary>
        /// <param name="linkManId">联系人编号</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmLinkman GetLinkManModel(string linkManId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from tbl_CrmLinkman where Id='{0}' order by SortId asc", linkManId);
            DbCommand dc = _db.GetSqlStringCommand(sb.ToString());
            Model.CrmStructure.MCrmLinkman model = new Model.CrmStructure.MCrmLinkman();
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    model.SortId = (int)reader["SortId"];
                    model.Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? null : (DateTime?)(reader["Birthday"]);
                    model.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    model.Department = !reader.IsDBNull(reader.GetOrdinal("Department")) ? (string)reader["Department"] : string.Empty;
                    model.EMail = !reader.IsDBNull(reader.GetOrdinal("EMail")) ? (string)reader["EMail"] : string.Empty;
                    model.Fax = !reader.IsDBNull(reader.GetOrdinal("Fax")) ? (string)reader["Fax"] : string.Empty;
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)(int.Parse(reader["Gender"].ToString()));
                    model.Id = !reader.IsDBNull(reader.GetOrdinal("Id")) ? (string)reader["Id"] : string.Empty;
                    model.MobilePhone = !reader.IsDBNull(reader.GetOrdinal("MobilePhone")) ? (string)reader["MobilePhone"] : string.Empty;
                    model.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    model.Post = !reader.IsDBNull(reader.GetOrdinal("Post")) ? (string)reader["Post"] : string.Empty;
                    model.QQ = !reader.IsDBNull(reader.GetOrdinal("QQ")) ? (string)reader["QQ"] : string.Empty;
                    model.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? (string)reader["Telephone"] : string.Empty;
                    model.Type = (EyouSoft.Model.EnumType.ComStructure.LxrType)(int.Parse(reader["Type"].ToString()));
                    model.TypeId = !reader.IsDBNull(reader.GetOrdinal("TypeId")) ? (string)reader["TypeId"] : string.Empty;
                    model.UserId = !reader.IsDBNull(reader.GetOrdinal("UserId")) ? (string)reader["UserId"] : string.Empty;
                   // model.Address = !reader.IsDBNull(reader.GetOrdinal("Address")) ? (string)reader["Address"] : string.Empty;
                    model.IsRemind = !reader.IsDBNull(reader.GetOrdinal("IsRemind")) ? reader["IsRemind"].ToString() == "0" ? false : true : false;
                    model.IssueTime = (DateTime)reader["IssueTime"];
                }
            }
            return model;
        }

        /// <summary>
        /// 获得联系人列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeId">类型编号</param>
        /// <param name="Type">类型</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmLinkman> GetLinkManModelList(string companyId, string TypeId, int Type)
        {
            IList<Model.CrmStructure.MCrmLinkman> list = new List<Model.CrmStructure.MCrmLinkman>();
            DbCommand dc = _db.GetSqlStringCommand(sql_Crm_SelectLinkman);
            _db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            _db.AddInParameter(dc, "TypeId", DbType.AnsiStringFixedLength, TypeId);
            _db.AddInParameter(dc, "Type", DbType.Byte, Type);
            using (IDataReader reader = DbHelper.ExecuteReader(dc, _db))
            {
                while (reader.Read())
                {
                    EyouSoft.Model.CrmStructure.MCrmLinkman linkmanModel = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                    linkmanModel.Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? null : (DateTime?)(reader["Birthday"]);
                    linkmanModel.CompanyId = !reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? (string)reader["CompanyId"] : string.Empty;
                    linkmanModel.Department = !reader.IsDBNull(reader.GetOrdinal("Department")) ? (string)reader["Department"] : string.Empty;
                    linkmanModel.EMail = !reader.IsDBNull(reader.GetOrdinal("EMail")) ? (string)reader["EMail"] : string.Empty;
                    linkmanModel.Fax = !reader.IsDBNull(reader.GetOrdinal("Fax")) ? (string)reader["Fax"] : string.Empty;
                    linkmanModel.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)(int.Parse(reader["Gender"].ToString()));
                    linkmanModel.Id = !reader.IsDBNull(reader.GetOrdinal("Id")) ? (string)reader["Id"] : string.Empty;
                    linkmanModel.MobilePhone = !reader.IsDBNull(reader.GetOrdinal("MobilePhone")) ? (string)reader["MobilePhone"] : string.Empty;
                    linkmanModel.Name = !reader.IsDBNull(reader.GetOrdinal("Name")) ? (string)reader["Name"] : string.Empty;
                    linkmanModel.Post = !reader.IsDBNull(reader.GetOrdinal("Post")) ? (string)reader["Post"] : string.Empty;
                    linkmanModel.QQ = !reader.IsDBNull(reader.GetOrdinal("QQ")) ? (string)reader["QQ"] : string.Empty;
                    linkmanModel.Telephone = !reader.IsDBNull(reader.GetOrdinal("Telephone")) ? (string)reader["Telephone"] : string.Empty;
                    linkmanModel.Type = (EyouSoft.Model.EnumType.ComStructure.LxrType)(int.Parse(reader["Type"].ToString()));
                    linkmanModel.TypeId = !reader.IsDBNull(reader.GetOrdinal("TypeId")) ? (string)reader["TypeId"] : string.Empty;
                    linkmanModel.UserId = !reader.IsDBNull(reader.GetOrdinal("UserId")) ? (string)reader["UserId"] : string.Empty;
                    linkmanModel.Address = !reader.IsDBNull(reader.GetOrdinal("Address")) ? (string)reader["Address"] : string.Empty;
                    linkmanModel.IsRemind = !reader.IsDBNull(reader.GetOrdinal("IsRemind")) ? reader["IsRemind"].ToString() == "0" ? false : true : false;
                    linkmanModel.SortId = (int)reader["SortId"];
                    linkmanModel.IssueTime = (DateTime)reader["IssueTime"];
                    list.Add(linkmanModel);
                }
            }
            return list;
        }
        #endregion
    }
}
