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
    /// 公司支付方式
    /// 创建者：郑付杰
    /// 创建时间：2011/9/22
    /// </summary>
    public class DComPayment : DALBase, EyouSoft.IDAL.ComStructure.IComPayment
    {
        private readonly Database _db = null;

        #region 构造函数
        public DComPayment()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region IComPayment 成员
        /// <summary>
        /// 添加公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Add(MComPayment item)
        {
            string sql = "INSERT INTO tbl_ComPayment(CompanyId,[Name],SourceType,ItemType,AccountId,OperatorId,LeiXing) VALUES(@CompanyId,@Name,@SourceType,@ItemType,@AccountId,@OperatorId,@LeiXing)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@Name", DbType.AnsiString, item.Name);
            this._db.AddInParameter(comm, "@SourceType", DbType.Byte, (int)item.SourceType);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)item.ItemType);
            //款项来源为银行,则必须绑定公司银行帐号
            this._db.AddInParameter(comm, "@AccountId", DbType.Int32, item.SourceType == SourceType.银行 ? item.AccountId : 0);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            _db.AddInParameter(comm, "LeiXing", DbType.Byte, item.LeiXing);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;
        }

        /// 修改公司支付方式
        /// </summary>
        /// <param name="item">支付方式实体</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Update(MComPayment item)
        {
            string sql = "UPDATE tbl_Compayment SET [Name] = @Name,SourceType=@SourceType,ItemType=@ItemType,AccountId=@AccountId,OperatorId=@OperatorId WHERE PaymentId=@PaymentId AND CompanyId = @CompanyID and IsSystem='0'";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Name", DbType.AnsiString, item.Name);
            this._db.AddInParameter(comm, "@SourceType", DbType.Byte, (int)item.SourceType);
            this._db.AddInParameter(comm, "@ItemType", DbType.Byte, (int)item.ItemType);
            this._db.AddInParameter(comm, "@AccountId", DbType.Int32, item.SourceType == SourceType.银行 ? item.AccountId : 0);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@PaymentId", DbType.Int32, item.PaymentId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);

            int result = DbHelper.ExecuteSql(comm, this._db);
            return result > 0 ? true : false;

        }
        /// <summary>
        /// 删除公司非系统默认支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>true：成功 false：失败</returns>
        public bool Delete(int paymentId, string companyId)
        {
            string sql = "DELETE FROM tbl_ComPayment WHERE PaymentId = @PaymentId AND IsSystem = '0' AND LeiXing=0 AND CompanyId = @CompanyId  AND NOT EXISTS(SELECT 1 FROM   tbl_TourOrderSales WHERE  CollectionRefundMode = @PaymentId) AND NOT EXISTS(SELECT 1 FROM   tbl_FinRegister WHERE  PaymentType = @PaymentId AND CompanyId = @CompanyId) AND NOT EXISTS(SELECT 1 FROM   tbl_FinOtherInFee WHERE  PayType = @PaymentId AND CompanyId = @CompanyId) AND NOT EXISTS(SELECT 1 FROM   tbl_FinOtherOutFee WHERE  PayType = @PaymentId AND CompanyId = @CompanyId)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@PaymentId", DbType.Int32, paymentId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            int result = DbHelper.ExecuteSql(comm, this._db);

            return result > 0 ? true : false;
        }
        /// <summary>
        /// 获取单个支付方式
        /// </summary>
        /// <param name="paymentId">支付方式编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式实体</returns>
        public MComPayment GetModel(int paymentId, string companyId)
        {
            StringBuilder sql = new StringBuilder("SELECT tb1.PaymentId,tb1.CompanyId,tb1.[Name],tb1.SourceType,tb1.ItemType,");
            sql.Append("tb1.AccountId,tb2.BankName");
            sql.Append(" ,tb1.IsSystem,tb1.LeiXing ");
            sql.Append(" from tbl_ComPayment tb1 left join tbl_ComAccount tb2");
            sql.Append(" on tb1.AccountId = tb2.AccountId");
            sql.Append(" where tb1.PaymentId = @PaymentId AND tb1.CompanyId = @CompanyId");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@PaymentId", DbType.Int32, paymentId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    return new MComPayment()
                    {
                        AccountId = (int)reader["AccountId"],
                        ItemType = (ItemType)Enum.Parse(typeof(ItemType), reader["ItemType"].ToString()),
                        SourceType = (SourceType)Enum.Parse(typeof(SourceType), reader["SourceType"].ToString()),
                        PaymentId = (int)reader["PaymentId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        Name = reader["Name"].ToString(),
                        IsSystem = reader["IsSystem"].ToString() == "1",
                        LeiXing = (ZhiFuFangShiLeiXing)reader.GetByte(reader.GetOrdinal("LeiXing"))
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// 获取公司所有支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>支付方式集合</returns>
        public IList<MComPayment> GetList(string companyId)
        {
            StringBuilder sql = new StringBuilder("SELECT tb1.PaymentId,tb1.CompanyId,tb1.[Name],tb1.SourceType,tb1.ItemType,");
            sql.Append("tb1.AccountId,tb2.BankName,tb1.IsSystem,tb1.LeiXing");
            sql.Append(" from tbl_ComPayment tb1 left join tbl_ComAccount tb2");
            sql.Append(" on tb1.AccountId = tb2.AccountId");
            sql.Append(" where tb1.CompanyId = @CompanyId");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);
            IList<MComPayment> list = new List<MComPayment>();
            MComPayment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComPayment()
                    {
                        AccountId = (int)reader["AccountId"],
                        ItemType = (ItemType)Enum.Parse(typeof(ItemType), reader["ItemType"].ToString()),
                        SourceType = (SourceType)Enum.Parse(typeof(SourceType), reader["SourceType"].ToString()),
                        PaymentId = (int)reader["PaymentId"],
                        CompanyId = reader["CompanyId"].ToString(),
                        Name = reader["Name"].ToString(),
                        IsSystem = reader["IsSystem"].ToString() == "1",
                        LeiXing = (ZhiFuFangShiLeiXing)reader.GetByte(reader.GetOrdinal("LeiXing"))
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取公司支付方式
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceType">款项来源</param>
        /// <param name="itemType">支付方式类型</param>
        /// <returns>支付方式集合</returns>
        public IList<MComPayment> GetList(string companyId, SourceType? sourceType, ItemType itemType)
        {
            StringBuilder sql = new StringBuilder("SELECT PaymentId,[Name],[IsSystem],[SourceType],[LeiXing] FROM tbl_ComPayment WHERE ItemType = @iType");
            if (sourceType != null)
            {
                sql.Append(" AND SourceType = @sType");
            }
            sql.Append(" AND CompanyId = @companyId");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@iType", DbType.Byte, (int)itemType);
            if (sourceType != null)
            {
                this._db.AddInParameter(comm, "@sType", DbType.Byte, (int)sourceType);
            }
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

            IList<MComPayment> list = new List<MComPayment>();

            MComPayment item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MComPayment()
                    {
                        PaymentId = (int)reader["PaymentId"],
                        Name = reader["Name"].ToString(),
                        IsSystem = reader["IsSystem"].ToString() == "1",
                        SourceType = (SourceType)reader.GetByte(reader.GetOrdinal("SourceType")),
                        LeiXing = (ZhiFuFangShiLeiXing)reader.GetByte(reader.GetOrdinal("LeiXing"))
                    });
                }
            }
            return list;
        }

        #endregion
    }
}


