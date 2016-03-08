using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using EyouSoft.Model.ComStructure;
using EyouSoft.Toolkit.DAL;
using EyouSoft.Toolkit;
using System.Xml.Linq;

namespace EyouSoft.DAL.ComStructure
{
    /// <summary>
    /// 部门信息
    /// 创建者：郑付杰
    /// 创建时间：2011/9/19
    /// </summary>
    public class DComDepartment : DALBase, EyouSoft.IDAL.ComStructure.IComDepartment
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComDepartment()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComDepartment 成员
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Add(MComDepartment item)
        {
            //StringBuilder sql = new StringBuilder("INSERT INTO tbl_ComDepartment(CompanyId,DepartName,DepartHead,PrevDepartId,Contact,Fax,Remarks,OperatorId,Operator,IssueTime,IsDefaultConfig)  VALUES  (@CompanyId,@DepartName,@DepartHead,@PrevDepartId,@Contact,@Fax,@Remarks,@OperatorId,Operator,@IssueTime,@IsDefaultConfig)");

            //DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            DbCommand comm = this._db.GetStoredProcCommand("proc_Department_Add");
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@DepartName", DbType.String, item.DepartName);
            this._db.AddInParameter(comm, "@DepartHead", DbType.String, item.DepartHead);
            this._db.AddInParameter(comm, "@PrevDepartId", DbType.Int32, item.PrevDepartId);
            this._db.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._db.AddInParameter(comm, "@Fax", DbType.String, item.Fax);
            this._db.AddInParameter(comm, "@Remarks", DbType.String, item.Remarks);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@Operator", DbType.String, item.Operator);
            this._db.AddInParameter(comm, "@IssueTime", DbType.DateTime, item.IssueTime);
            this._db.AddInParameter(comm, "@IsDefaultConfig", DbType.Byte, item.IsDefaultConfig);
            this._db.AddInParameter(comm, "@DepartPlan", DbType.String, item.DepartPlan);
            this._db.AddInParameter(comm, "@DepartPlanId", DbType.String, item.DepartPlanId);

            this._db.AddInParameter(comm, "@ComAttachXML", DbType.Xml, CreateComNoticeXML(item.PrintFiles));


            this._db.AddOutParameter(comm, "@result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(comm, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(comm, "result")) == 1 ? true : false;

        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="item">部门实体</param>
        /// <returns>true:成功 false：失败</returns>
        public bool Update(MComDepartment item)
        {
            //StringBuilder sql = new StringBuilder("UPDATE tbl_ComDepartment SET DepartName = @DepartName,DepartHead=@DepartHead,Contact=@Contact,Fax=@Fax,Remarks=@Remarks,OperatorId=@OperatorId WHERE DepartId = @DepartId AND CompanyId = @CompanyId");
            //DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComDepartment_Update");
            this._db.AddInParameter(comm, "@DepartName", DbType.String, item.DepartName);
            this._db.AddInParameter(comm, "@DepartHead", DbType.String, item.DepartHead);
            this._db.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._db.AddInParameter(comm, "@Fax", DbType.String, item.Fax);
            this._db.AddInParameter(comm, "@Remarks", DbType.String, item.Remarks);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@DepartId", DbType.Int32, item.DepartId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@ComAttachXML", DbType.Xml, CreateComNoticeXML(item.PrintFiles));
            this._db.AddInParameter(comm, "@DepartPlan", DbType.String, item.DepartPlan);
            this._db.AddInParameter(comm, "@DepartPlanId", DbType.String, item.DepartPlanId);

            this._db.AddOutParameter(comm, "@result", DbType.Int32, 4);

            DbHelper.RunProcedureWithResult(comm, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(comm, "result")) == 1 ? true : false;

        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1:部门已添加用户 2:该部门已添加下级部门 3:删除成功 4:删除失败		</returns>
        public int Delete(int departId, string companyId)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ComDepartment_Delete");
            this._db.AddInParameter(comm, "@departId", DbType.Int32, departId);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);
            return DbHelper.RunProcedureWithResult(comm, this._db);
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门实体</returns>
        public MComDepartment GetModel(int departId, string companyId)
        {
            string sql = "SELECT DepartId,CompanyId,DepartName,DepartHead,PrevDepartId,Contact,Fax,Remarks,OperatorId,  IsDefaultConfig, DepartPlan,DepartPlanId,        (SELECT Id,DepartId,  PrintName,PrintHeader,PrintFooter,PrintTemplates,IsDefault FROM tbl_ComPrintConfig WHERE DepartId=@DepartId  FOR XML RAW,ROOT('ROOT'))AS ComAttachXML    FROM tbl_ComDepartment WHERE DepartId = @DepartId AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@DepartId", DbType.Int32, departId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return new MComDepartment()
                    {
                        DepartId = (int)reader["DepartId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        DepartName = reader.IsDBNull(reader.GetOrdinal("DepartName")) ? string.Empty : reader["DepartName"].ToString(),
                        DepartHead = reader.IsDBNull(reader.GetOrdinal("DepartHead")) ? string.Empty : reader["DepartHead"].ToString(),
                        PrevDepartId = (int)reader["PrevDepartId"],
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? string.Empty : reader["Contact"].ToString(),
                        Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? string.Empty : reader["Fax"].ToString(),
                        OperatorId = reader["OperatorId"].ToString(),
                        Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? string.Empty : reader["Remarks"].ToString(),
                        IsDefaultConfig = reader["IsDefaultConfig"].ToString() == "1" ? true : false,
                        DepartPlan = reader["DepartPlan"].ToString(),
                        DepartPlanId = reader["DepartPlanId"].ToString(),
                        PrintFiles = GetAttachList(reader["ComAttachXML"].ToString(), departId.ToString())
                    };
                }
            }

            return null;
        }
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        ///  <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MComDepartment> GetList(string companyId)
        {
            string sql = "SELECT DepartId,DepartName,PrevDepartId FROM tbl_ComDepartment WHERE CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComDepartment> list = new List<MComDepartment>();
            MComDepartment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComDepartment
                        {
                            DepartId = (int)reader["DepartId"],
                            DepartName =
                                reader.IsDBNull(reader.GetOrdinal("DepartName"))
                                    ? string.Empty
                                    : reader["DepartName"].ToString(),
                            PrevDepartId = (int)reader["PrevDepartId"]
                        });
                }
            }

            return list;
        }
        /// <summary>
        /// 获取部门下的所有子部门信息
        /// </summary>
        /// <param name="departId">部门编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>部门集合</returns>
        public IList<MComDepartment> GetList(string departId, string companyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("with depart( DepartId,DepartName,PrevDepartId)");
            sql.Append(" as");
            sql.Append(" (");
            sql.Append(" select DepartId,DepartName,PrevDepartId");
            sql.AppendFormat(" from tbl_ComDepartment where DepartId = {0} and CompanyId = '{1}'", departId, companyId);
            sql.Append(" union all");
            sql.Append(" select a.DepartId,a.DepartName,a.PrevDepartId ");
            sql.Append(" from tbl_ComDepartment a ");
            sql.Append(" inner join depart b on a.PrevDepartId = b.DepartId");
            sql.Append(" )");
            sql.AppendFormat(" select * from depart where DepartId <> {0}", departId);

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            IList<MComDepartment> list = new List<MComDepartment>();

            MComDepartment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComDepartment
                    {
                        DepartId = (int)reader["DepartId"],
                        DepartName =
                            reader.IsDBNull(reader.GetOrdinal("DepartName"))
                                ? string.Empty
                                : reader["DepartName"].ToString(),
                        PrevDepartId = (int)reader["PrevDepartId"]
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 根据上级部门编号判断是否存在子部门
        /// </summary>
        /// <param name="prevDepartId">上级部门编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExistSubDept(int prevDepartId)
        {
            var sql = new StringBuilder("select DepartId from tbl_ComDepartment where PrevDepartId=@PrevDepartId");
            var comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@PrevDepartId", DbType.Int32, prevDepartId);

            return DbHelper.Exists(comm, this._db);
        }

        /// <summary>
        /// 创建附件XML
        /// </summary>
        /// <param name="list">附件集合</param>
        /// <returns></returns>
        private string CreateComNoticeXML(IList<EyouSoft.Model.ComStructure.MComDepartmentFiles> list)
        {
            //if (list == null) return "";
            if (list == null) return null;
            StringBuilder StrBuild = new StringBuilder();
            StrBuild.Append("<ROOT>");
            foreach (EyouSoft.Model.ComStructure.MComDepartmentFiles model in list)
            {
                StrBuild.AppendFormat("<ComAttach PrintName=\"{0}\"", model.PrintName);
                StrBuild.AppendFormat(" PrintHeader=\"{0}\" ", model.PrintHeader);
                StrBuild.AppendFormat(" PrintFooter=\"{0}\" ", model.PrintFooter);
                StrBuild.AppendFormat(" PrintTemplates=\"{0}\" ", model.PrintTemplates);
                StrBuild.AppendFormat(" IsDefault=\"{0}\" />", model.IsDefault ? 1 : 0);
            }
            StrBuild.Append("</ROOT>");
            return StrBuild.ToString();
        }

        /// <summary>
        /// 生成附件集合List
        /// </summary>
        /// <param name="ComAttachXML">附件信息</param>
        /// <param name="userId">员工编号</param>
        /// <param name="ItemType">附件类型</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComDepartmentFiles> GetAttachList(string ComAttachXML, string userId)
        {
            if (string.IsNullOrEmpty(ComAttachXML)) return null;
            IList<EyouSoft.Model.ComStructure.MComDepartmentFiles> ResultList = null;
            ResultList = new List<EyouSoft.Model.ComStructure.MComDepartmentFiles>();
            XElement root = XElement.Parse(ComAttachXML);
            IEnumerable<XElement> xRow = root.Elements("row");
            foreach (XElement tmp1 in xRow)
            {
                EyouSoft.Model.ComStructure.MComDepartmentFiles model = new EyouSoft.Model.ComStructure.MComDepartmentFiles()
                {

                    DepartId = Utils.GetInt(tmp1.Attribute("DepartId").Value),
                    PrintName = tmp1.Attribute("PrintName").Value,
                    PrintHeader = tmp1.Attribute("PrintHeader").Value,
                    PrintFooter = tmp1.Attribute("PrintFooter").Value,
                    PrintTemplates = tmp1.Attribute("PrintTemplates").Value,
                    IsDefault = tmp1.Attribute("IsDefault").Value == "1" ? true : false

                };
                ResultList.Add(model);
                model = null;
            }
            return ResultList;
        }

        #endregion
    }
}
