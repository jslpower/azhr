using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TourStructure
{
    using EyouSoft.Model.TourStructure;

    /// <summary>
    /// 描述：团队报价数据访问层
    /// 修改记录：
    /// 1、2011-09-05 PM 曹胡生 创建
    /// </summary>
    public class DQuote : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.TourStructure.IQuote
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DQuote()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region SQL语名
        //获取报价信息
        private const string SQL_SELECT_GetQuoteInfo = "SELECT tbl_Quote.*,(SELECT * FROM tbl_QuotePrice WHERE QuoteId=@QuoteId for xml raw,root('QuotePrice')) as QuotePrice,(select tbl_QuotePlan.*,(SELECT * FROM tbl_QuotePlanSpot WHERE PlanId=tbl_QuotePlan.PlanId for xml raw,root('QuotePlanSpot')) as QuotePlanSpot from tbl_QuotePlan where QuoteId=@QuoteId order by Days for xml raw,root('QuotePlan')) as QuotePlan,(select * from tbl_ComAttach where ItemType=17 and ItemId=@QuoteId for xml raw,root) as VisaFile FROM tbl_Quote WHERE QuoteId=@QuoteId";
        //取消报价记录
        private const string SQL_UPDATE_CancelQuote = "DECLARE @ParentId CHAR(36) SELECT @ParentId = ParentId FROM tbl_Quote WHERE  QuoteId = @QuoteId AND ParentId <> '0' UPDATE tbl_Quote SET IsLatest = 0,QuoteStatus = @QuoteState WHERE(ParentId = @ParentId OR QuoteId = @ParentId OR QuoteId = @QuoteId OR ParentId = @QuoteId) AND QuoteStatus <> 3;UPDATE tbl_Quote SET QuoteStatus=@QuoteState,CancelReason=@CancelReason,IsLatest =1 WHERE  QuoteId=@QuoteId AND QuoteStatus<>3 ";
        //删除报价记录
        private const string SQL_UPDATE_DelQuote = "update tbl_Quote set IsDelete=1 where (ParentId in(select DISTINCT ParentId from tbl_Quote where CHARINDEX(QuoteId,@QuoteIds,0)>0 and ParentId<>'0') or QuoteId in(select DISTINCT ParentId from tbl_Quote where CHARINDEX(QuoteId,@QuoteIds,0)>0  and ParentId<>'0') or CHARINDEX(ParentId,@QuoteIds,0)>0 OR CHARINDEX(QuoteId,@QuoteIds,0)>0) and QuoteStatus<>3";
        //获取销售员超限详细
        private const string SQL_SELECT_GetSaleOverrunDetail = "select * from view_SalesmanWarningDetail where SellerId=@SellerId and Transfinite+@Money>0";
        //获取客户超限详细(2012-08-20 单团账龄超限不计入客户单位欠款)
        //private const string SQL_SELECT_GetCustomerOverrunDetail = "select * from view_CustomerWarningDetail where CrmId=@CrmId and (Transfinite+@Money>0 or DeadDay>Deadline)";
        private const string SQL_SELECT_GetCustomerOverrunDetail = "select * from view_CustomerWarningDetail where CrmId=@CrmId and Transfinite+@Money>0 ";

        //修改报价记录(计调报价)
        private const string SQL_UPDATE_PlanerQuote = "update tbl_Quote set CostMoney=@CostMoney,IsPlanerQuote='1' where QuoteId=@QuoteId and IsPlanerQuote='0'";

        //得到报价次数
        private const string SQL_SELECT_GetQuoteNo = "declare @ParentId char(36) select @ParentId=ParentId from tbl_Quote where QuoteId=@QuoteId if(@ParentId='0') select QuoteId,TimeCount,QuoteStatus FROM tbl_Quote WHERE ParentId=@QuoteId or QuoteId=@QuoteId order by TimeCount else select QuoteId,TimeCount,QuoteStatus FROM tbl_Quote WHERE ParentId=@ParentId or QuoteId=@ParentId order by TimeCount  ";
        #endregion

        #region 成员方法
        /// <summary>
        /// 获得团队报价列表
        /// </summary>
        /// <param name="CompanyId">报价所属公司编号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="info">搜索实体</param> 
        /// <param name="ModuleType">模块</param>
        /// <param name="DeptId">部门集合</param>  
        /// <param name="isOnlySelf">是否仅查看自己的数据</param> 
        /// <param name="LoginUserId">当前登录的用户编号</param>  
        /// <param name="ShowBeforeMonth">显示当前时间前几个月的数据</param> 
        /// <param name="ShowAfterMonth">显示当前时间前后个月的数据</param>  
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourQuoteInfo> GetTourQuoteList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TourStructure.MTourQuoteSearch info, int[] DeptId, bool isOnlySelf, string LoginUserId, int ShowBeforeMonth, int ShowAfterMonth)
        {
            IList<EyouSoft.Model.TourStructure.MTourQuoteInfo> list = new List<EyouSoft.Model.TourStructure.MTourQuoteInfo>();
            EyouSoft.Model.TourStructure.MTourQuoteInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Quote";
            string PrimaryKey = "QuoteId";
            string OrderByString = "InquiryTime DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" QuoteId,RouteName,AdultPrice,BuyCompanyName,(select AreaName from tbl_ComArea where AreaId=tbl_Quote.AreaId) as AreaName,Days,Adults,Childs,SellerId,SellerName,Operator,OperatorId,InquiryTime,TimeCount,QuoteStatus,ParentId,Contact,Phone,PlanerId,IsPlanerQuote,Planer,CancelReason,TourId ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsLatest='1' and IsDelete='0' and CompanyId='{0}'", CompanyId);
            if (isOnlySelf)
            {
                cmdQuery.AppendFormat(" and SellerId='{0}'", LoginUserId);
            }
            else
            {
                if (DeptId != null)
                {
                    cmdQuery.AppendFormat(GetOrgCondition(LoginUserId, DeptId, "SellerId", "DeptId"));
                }
            }
            if (ShowBeforeMonth != 0)
            {
                cmdQuery.AppendFormat(" and datediff(month,InquiryTime,getdate())<={0}", ShowBeforeMonth);
            }
            if (ShowAfterMonth != 0)
            {
                cmdQuery.AppendFormat(" and datediff(month,getdate(),InquiryTime)<={0}", ShowAfterMonth);
            }
            if (info != null)
            {
                if (info.AreaId != 0)
                {
                    cmdQuery.AppendFormat(" and AreaId={0}", info.AreaId);
                }
                if (!string.IsNullOrEmpty(info.BuyCompanyID))
                {
                    cmdQuery.AppendFormat(" and BuyCompanyID='{0}'", info.BuyCompanyID);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.BuyCompanyName))
                    {
                        cmdQuery.AppendFormat(" and BuyCompanyName like '%{0}%'", Utils.ToSqlLike(info.BuyCompanyName));
                    }
                }
                if (!string.IsNullOrEmpty(info.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", info.SellerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.SellerName))
                    {
                        cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(info.SellerName));
                    }
                }
                if (!string.IsNullOrEmpty(info.OperatorId))
                {
                    cmdQuery.AppendFormat(" and OperatorId='{0}'", info.OperatorId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.Operator))
                    {
                        cmdQuery.AppendFormat(" and Operator like '%{0}%'", Utils.ToSqlLike(info.Operator));
                    }
                }
                if (!string.IsNullOrEmpty(info.RouteName))
                {
                    cmdQuery.AppendFormat(" and RouteName like '%{0}%'", Utils.ToSqlLike(info.RouteName));
                }
                if (info.QuoteState != null)
                {
                    cmdQuery.AppendFormat(" and QuoteStatus={0} ", (int)info.QuoteState);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.TourStructure.MTourQuoteInfo()
                    {
                        QuoteId = rdr["QuoteId"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        AreaName = rdr["AreaName"].ToString(),
                        AdultPrice = rdr.IsDBNull(rdr.GetOrdinal("AdultPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("AdultPrice")),
                        BuyCompanyName = rdr["BuyCompanyName"].ToString(),
                        Days = rdr.IsDBNull(rdr.GetOrdinal("Days")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Days")),
                        Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults")),
                        Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs")),
                        SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo() { SellerId = rdr["SellerId"].ToString(), Name = rdr["SellerName"].ToString() },
                        OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo()
                        {
                            OperatorId = rdr["OperatorId"].ToString(),
                            Name = rdr["Operator"].ToString()
                        },
                        TimeCount = rdr.IsDBNull(rdr.GetOrdinal("TimeCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TimeCount")),
                        InquiryTime = rdr.GetDateTime(rdr.GetOrdinal("InquiryTime")),
                        QuoteState = (EyouSoft.Model.EnumType.TourStructure.QuoteState)rdr.GetByte(rdr.GetOrdinal("QuoteStatus")),
                        Phone = rdr["Phone"].ToString(),
                        Contact = rdr["Contact"].ToString(),
                        PlanerId = rdr["PlanerId"].ToString().Trim(),
                        IsPlanerQuote = rdr["IsPlanerQuote"].ToString().Trim() == "1",
                        Planer = rdr["Planer"].ToString().Trim(),
                        CancelReason = rdr["CancelReason"].ToString(),
                        MTourQuoteTourInfo = new MTourQuoteTourInfo{TourId = rdr["TourId"].ToString()}
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得团队报价信息
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.MTourQuoteInfo GetQuoteInfo(string QuoteId)
        {
            EyouSoft.Model.TourStructure.MTourQuoteInfo info = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetQuoteInfo);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                if (sdr.Read())
                {
                    info = new EyouSoft.Model.TourStructure.MTourQuoteInfo();

                    info.QuoteId = QuoteId;
                    info.CompanyId = sdr["CompanyId"].ToString();
                    info.QuoteState = (EyouSoft.Model.EnumType.TourStructure.QuoteState)sdr.GetByte(sdr.GetOrdinal("QuoteStatus"));
                    info.CancelReason = sdr["CancelReason"].ToString();
                    info.InquiryTime = sdr.GetDateTime(sdr.GetOrdinal("InquiryTime"));
                    info.PlanerId = sdr["PlanerId"].ToString();
                    info.IsLatest = sdr.GetString(sdr.GetOrdinal("IsLatest")) == "1";
                    info.Planer = sdr["Planer"].ToString();
                    info.UpdateTime = sdr.GetDateTime(sdr.GetOrdinal("UpdateTime"));
                    //info.QuoteType = (EyouSoft.Model.EnumType.TourStructure.ModuleType)sdr.GetByte(sdr.GetOrdinal("QuoteType"));
                    info.AreaId = sdr.IsDBNull(sdr.GetOrdinal("AreaId")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("AreaId"));
                    info.RouteId = sdr["RouteId"].ToString();
                    info.RouteName = sdr["RouteName"].ToString();
                    info.Days = sdr.IsDBNull(sdr.GetOrdinal("Days")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Days"));
                    info.BuyCompanyID = sdr["BuyCompanyID"].ToString();
                    info.BuyCompanyName = sdr["BuyCompanyName"].ToString();
                    info.Contact = sdr["Contact"].ToString();
                    info.Phone = sdr["Phone"].ToString();
                    info.SaleInfo = new EyouSoft.Model.TourStructure.MSaleInfo()
                    {
                        SellerId = sdr["SellerId"].ToString(),
                        Name = sdr["SellerName"].ToString()
                    };
                    info.OperatorInfo = new EyouSoft.Model.TourStructure.MOperatorInfo()
                    {
                        OperatorId = sdr["OperatorId"].ToString(),
                        Name = sdr["Operator"].ToString()
                    };
                    info.Adults = sdr.IsDBNull(sdr.GetOrdinal("Adults")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Adults"));
                    info.Childs = sdr.IsDBNull(sdr.GetOrdinal("Childs")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Childs"));
                    info.AdultPrice = sdr.IsDBNull(sdr.GetOrdinal("AdultPrice")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("AdultPrice"));
                    info.ChildPrice = sdr.IsDBNull(sdr.GetOrdinal("ChildPrice")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("ChildPrice"));
                    info.TotalPrice = sdr.IsDBNull(sdr.GetOrdinal("TotalPrice")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("TotalPrice"));
                    info.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)sdr.GetByte(sdr.GetOrdinal("OutQuoteType"));
                    info.ParentId = sdr["ParentId"].ToString();
                    info.TimeCount = sdr.IsDBNull(sdr.GetOrdinal("TimeCount")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("TimeCount"));
                    info.ServiceStandard = sdr["ServiceStandard"].ToString();
                    info.QuotePlan = GetPlanByXml(sdr["QuotePlan"].ToString());
                    info.TourTeamPrice = GetTourTeamPriceByXml(sdr["QuotePrice"].ToString());
                    info.CostCalculation = sdr["CostMoney"].ToString();
                    info.TourService = new EyouSoft.Model.TourStructure.MTourService()
                    {
                        ChildServiceItem = sdr["ChildServiceItem"].ToString(),
                        InsiderInfor = sdr["InsiderInfor"].ToString(),
                        NeedAttention = sdr["NeedAttention"].ToString(),
                        NoNeedItem = sdr["NotNeedItem"].ToString(),
                        OwnExpense = sdr["OwnExpense"].ToString(),
                        ShoppingItem = sdr["ShoppingItem"].ToString(),
                        WarmRemind = sdr["WarmRemind"].ToString()
                    };
                    info.TourQuoteNo = GetTourQuoteNo(QuoteId);
                    info.IsPlanerQuote = sdr["IsPlanerQuote"].ToString() == "1";
                    info.OtherCost = sdr.IsDBNull(sdr.GetOrdinal("OtherCost")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("OtherCost"));
                    info.PlanFeature = sdr["PlanFeature"].ToString();
                    info.VisaFileList = GetVisaFileByXml(sdr["VisaFile"].ToString());
                    info.CountryId = sdr.GetInt32(sdr.GetOrdinal("CountryId"));
                    info.ProvinceId = sdr.GetInt32(sdr.GetOrdinal("ProvinceId"));
                    info.QuoteRemark = sdr["QuoteRemark"].ToString();
                    info.ContactDepartId = sdr["ContactDepartId"].ToString();
                }
            }
            return info;
        }

        /// <summary>
        /// 新增团队报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Quote_Add");
            _db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, info.QuoteId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "ParentId", DbType.AnsiStringFixedLength, info.ParentId);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, info.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, info.RouteName);
            _db.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, info.RouteId);
            _db.AddInParameter(cmd, "Days", DbType.Int32, info.Days);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, info.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, info.BuyCompanyName);
            _db.AddInParameter(cmd, "Contact", DbType.String, info.Contact);
            _db.AddInParameter(cmd, "Phone", DbType.String, info.Phone);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SaleInfo.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, info.SaleInfo.Name);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, info.SaleInfo.DeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, info.Adults);
            _db.AddInParameter(cmd, "Childs ", DbType.Int32, info.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Decimal, info.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Decimal, info.ChildPrice);
            _db.AddInParameter(cmd, "TotalPrice", DbType.Decimal, info.TotalPrice);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)info.OutQuoteType);
            _db.AddInParameter(cmd, "ServiceStandard", DbType.String, info.ServiceStandard);
            _db.AddInParameter(cmd, "CostCalculation", DbType.String, info.CostCalculation);
            _db.AddInParameter(cmd, "NotNeedItem", DbType.String, info.TourService.NoNeedItem);
            _db.AddInParameter(cmd, "ShoppingItem", DbType.String, info.TourService.ShoppingItem);
            _db.AddInParameter(cmd, "ChildServiceItem", DbType.String, info.TourService.ChildServiceItem);
            _db.AddInParameter(cmd, "OwnExpense", DbType.String, info.TourService.OwnExpense);
            _db.AddInParameter(cmd, "NeedAttention", DbType.String, info.TourService.NeedAttention);
            _db.AddInParameter(cmd, "WarmRemind ", DbType.String, info.TourService.WarmRemind);
            _db.AddInParameter(cmd, "InsiderInfor ", DbType.String, info.TourService.InsiderInfor);
            //_db.AddInParameter(cmd, "QuoteType", DbType.Byte, (int)info.QuoteType);
            _db.AddInParameter(cmd, "QuoteStatus", DbType.Byte, (int)info.QuoteState);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorInfo.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.OperatorInfo.Name);
            _db.AddInParameter(cmd, "InquiryTime", DbType.DateTime, info.InquiryTime);
            _db.AddInParameter(cmd, "QuotePlan", DbType.String, CreatePlanXml(info.QuotePlan, info.QuoteId));
            _db.AddInParameter(cmd, "QuotePrice", DbType.String, CreateTourTeamPriceXml(info.TourTeamPrice, info.QuoteId));
            _db.AddInParameter(cmd, "QuotePlanSpot", DbType.String, CreatePlanSpotXml(info.QuotePlan));
            _db.AddInParameter(cmd, "OtherCost", DbType.Decimal, info.OtherCost);
            _db.AddInParameter(cmd, "PlanFeature", DbType.String, info.PlanFeature);
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, info.QuoteRemark);
            _db.AddInParameter(cmd, "PlanerId", DbType.AnsiStringFixedLength, info.PlanerId);
            _db.AddInParameter(cmd, "Planer", DbType.String, info.Planer);
            _db.AddInParameter(cmd, "VisaFile", DbType.String, CreateVisaFileXml(info.QuoteId, info.VisaFileList));
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, info.ContactDepartId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 修改团队报价(2报价成功 3 修改成功 4操作失败 5销售员超限 6客户超限 7 销售员客户均超限)
        /// 2012-8-17 王磊 返回的结果 去掉【1超限，垫付申请中】
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTourQuote(EyouSoft.Model.TourStructure.MTourQuoteInfo info)
        {
            DbCommand cmd = _db.GetStoredProcCommand("proc_Quote_Update");
            _db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, info.QuoteId);
            _db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            _db.AddInParameter(cmd, "AreaId", DbType.Int32, info.AreaId);
            _db.AddInParameter(cmd, "RouteName", DbType.String, info.RouteName);
            _db.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, info.RouteId);
            _db.AddInParameter(cmd, "Days", DbType.Int32, info.Days);
            _db.AddInParameter(cmd, "BuyCompanyID", DbType.AnsiStringFixedLength, info.BuyCompanyID);
            _db.AddInParameter(cmd, "BuyCompanyName", DbType.String, info.BuyCompanyName);
            _db.AddInParameter(cmd, "Contact", DbType.String, info.Contact);
            _db.AddInParameter(cmd, "Phone", DbType.String, info.Phone);
            _db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, info.SaleInfo.SellerId);
            _db.AddInParameter(cmd, "SellerName", DbType.String, info.SaleInfo.Name);
            _db.AddInParameter(cmd, "DeptId", DbType.Int32, info.SaleInfo.DeptId);
            _db.AddInParameter(cmd, "Adults", DbType.Int32, info.Adults);
            _db.AddInParameter(cmd, "Childs ", DbType.Int32, info.Childs);
            _db.AddInParameter(cmd, "AdultPrice", DbType.Decimal, info.AdultPrice);
            _db.AddInParameter(cmd, "ChildPrice", DbType.Decimal, info.ChildPrice);
            _db.AddInParameter(cmd, "TotalPrice", DbType.Decimal, info.TotalPrice);
            _db.AddInParameter(cmd, "OutQuoteType", DbType.Byte, (int)info.OutQuoteType);
            _db.AddInParameter(cmd, "ServiceStandard", DbType.String, info.ServiceStandard);
            _db.AddInParameter(cmd, "CostCalculation", DbType.String, info.CostCalculation);
            _db.AddInParameter(cmd, "NotNeedItem", DbType.String, info.TourService.NoNeedItem);
            _db.AddInParameter(cmd, "ShoppingItem", DbType.String, info.TourService.ShoppingItem);
            _db.AddInParameter(cmd, "ChildServiceItem", DbType.String, info.TourService.ChildServiceItem);
            _db.AddInParameter(cmd, "OwnExpense", DbType.String, info.TourService.OwnExpense);
            _db.AddInParameter(cmd, "NeedAttention", DbType.String, info.TourService.NeedAttention);
            _db.AddInParameter(cmd, "WarmRemind ", DbType.String, info.TourService.WarmRemind);
            _db.AddInParameter(cmd, "InsiderInfor ", DbType.String, info.TourService.InsiderInfor);
            _db.AddInParameter(cmd, "QuoteStatus", DbType.Byte, (int)info.QuoteState);
            _db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, info.OperatorInfo.OperatorId);
            _db.AddInParameter(cmd, "Operator", DbType.String, info.OperatorInfo.Name);
            _db.AddInParameter(cmd, "UpdateTime", DbType.DateTime, System.DateTime.Now);
            _db.AddInParameter(cmd, "QuotePlan", DbType.String, CreatePlanXml(info.QuotePlan, info.QuoteId));
            _db.AddInParameter(cmd, "QuotePrice", DbType.String, CreateTourTeamPriceXml(info.TourTeamPrice, info.QuoteId));
            _db.AddInParameter(cmd, "OtherCost", DbType.Decimal, info.OtherCost);
            _db.AddInParameter(cmd, "PlanFeature", DbType.String, info.PlanFeature);
            _db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, info.OrderId);
            _db.AddInParameter(cmd, "OrderCode", DbType.String, info.OrderCode);
            _db.AddInParameter(cmd, "QuotePlanSpot", DbType.String, CreatePlanSpotXml(info.QuotePlan));
            _db.AddInParameter(cmd, "TourInfo", DbType.String, CreateTourInfoXml(info.MTourQuoteTourInfo, info.Days));
            _db.AddInParameter(cmd, "AdvanceApp", DbType.String, CreateOverrunXml(info.AdvanceApp));
            _db.AddInParameter(cmd, "Traveller", DbType.String, CreateTravellerXml(info.OrderId, info.MTourQuoteTourInfo == null ? null : info.MTourQuoteTourInfo.Traveller));
            _db.AddInParameter(cmd, "TravellerInsur", DbType.Xml, CreateInsurXml(info.MTourQuoteTourInfo == null ? null : info.MTourQuoteTourInfo.Traveller));
            _db.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            _db.AddInParameter(cmd, "CountryId", DbType.Int32, info.CountryId);
            _db.AddInParameter(cmd, "QuoteRemark", DbType.String, info.QuoteRemark);
            _db.AddInParameter(cmd, "PlanerId", DbType.AnsiStringFixedLength, info.PlanerId);
            _db.AddInParameter(cmd, "Planer", DbType.String, info.Planer);
            _db.AddInParameter(cmd, "VisaFile", DbType.String, CreateVisaFileXml(info.QuoteId, info.VisaFileList));
            _db.AddInParameter(cmd, "ContactDepartId", DbType.AnsiStringFixedLength, info.ContactDepartId);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 取消团队报价
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="Reason">取消原因</param> 
        /// <returns></returns>
        public bool CalcelTourQuote(string QuoteId, string Reason)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_CancelQuote);
            this._db.AddInParameter(cmd, "QuoteState", DbType.Byte, (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价);
            this._db.AddInParameter(cmd, "CancelReason", DbType.String, Reason);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 删除报价
        /// </summary>
        /// <param name="QuoteIds">报价编号</param>
        /// <returns></returns>
        public bool DeleteQuote(string QuoteIds)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_DelQuote);
            this._db.AddInParameter(cmd, "QuoteIds", DbType.String, QuoteIds);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        ///// <summary>
        ///// 获取销售员超限详细
        ///// </summary>
        ///// <param name="SaleId">销售员编号</param>
        ///// <param name="Money">当前金额</param>
        ///// <returns></returns>
        //public EyouSoft.Model.FinStructure.MSalesmanWarning GetSaleOverrunDetail(string SaleId, decimal Money)
        //{
        //    EyouSoft.Model.FinStructure.MSalesmanWarning model = null;
        //    DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSaleOverrunDetail);
        //    this._db.AddInParameter(cmd, "SellerId", DbType.AnsiStringFixedLength, SaleId);
        //    this._db.AddInParameter(cmd, "Money", DbType.Decimal, Money);
        //    using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
        //    {
        //        if (sdr.Read())
        //        {
        //            model = new EyouSoft.Model.FinStructure.MSalesmanWarning();

        //            model.SellerId = sdr.GetString(sdr.GetOrdinal("SellerId"));
        //            model.SellerName = sdr.IsDBNull(sdr.GetOrdinal("SellerName")) ? "" : sdr.GetString(sdr.GetOrdinal("SellerName"));
        //            model.AmountOwed = sdr.IsDBNull(sdr.GetOrdinal("AmountOwed")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("AmountOwed"));
        //            model.ConfirmAdvances = sdr.IsDBNull(sdr.GetOrdinal("ConfirmAdvances")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("ConfirmAdvances"));
        //            model.PreIncome = sdr.IsDBNull(sdr.GetOrdinal("PreIncome")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("PreIncome"));
        //            model.SumPay = sdr.IsDBNull(sdr.GetOrdinal("SumPay")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("SumPay"));
        //            model.Arrear = sdr.IsDBNull(sdr.GetOrdinal("Arrear")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("Arrear"));
        //            model.Transfinite = sdr.IsDBNull(sdr.GetOrdinal("Transfinite")) ? Money : sdr.GetDecimal(sdr.GetOrdinal("Transfinite")) + Money;
        //            model.TransfiniteTime = sdr.IsDBNull(sdr.GetOrdinal("TransfiniteTime")) ? System.DateTime.Now : sdr.GetDateTime(sdr.GetOrdinal("TransfiniteTime"));
        //        }
        //        return model;
        //    }
        //}

        ///// <summary>
        ///// 获取客户超限详细
        ///// </summary>
        ///// <param name="CrmId">客户编号</param>
        ///// <param name="Money">当前金额</param>
        ///// <returns></returns>
        //public EyouSoft.Model.FinStructure.MCustomerWarning GetCustomerOverrunDetail(string CrmId, decimal Money)
        //{
        //    EyouSoft.Model.FinStructure.MCustomerWarning model = null;
        //    DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetCustomerOverrunDetail);
        //    this._db.AddInParameter(cmd, "CrmId", DbType.AnsiStringFixedLength, CrmId);
        //    this._db.AddInParameter(cmd, "Money", DbType.Decimal, Money);
        //    using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
        //    {
        //        if (sdr.Read())
        //        {
        //            model = new EyouSoft.Model.FinStructure.MCustomerWarning();

        //            model.SellerId = sdr.GetString(sdr.GetOrdinal("SellerId"));
        //            model.SellerName = sdr.IsDBNull(sdr.GetOrdinal("SellerName")) ? "" : sdr.GetString(sdr.GetOrdinal("SellerName"));
        //            model.AmountOwed = sdr.IsDBNull(sdr.GetOrdinal("AmountOwed")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("AmountOwed"));
        //            model.Arrear = sdr.IsDBNull(sdr.GetOrdinal("Arrear")) ? 0 : sdr.GetDecimal(sdr.GetOrdinal("Arrear"));
        //            model.Transfinite = sdr.IsDBNull(sdr.GetOrdinal("Transfinite")) ? Money : sdr.GetDecimal(sdr.GetOrdinal("Transfinite")) + Money;
        //            model.TransfiniteTime = sdr.IsDBNull(sdr.GetOrdinal("TransfiniteTime")) ? ((sdr.IsDBNull(sdr.GetOrdinal("DeadDay")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("DeadDay"))) > 0 ? null : (DateTime?)System.DateTime.Now) : (DateTime?)sdr.GetDateTime(sdr.GetOrdinal("TransfiniteTime"));
        //            model.Customer = sdr.IsDBNull(sdr.GetOrdinal("Customer")) ? "" : sdr.GetString(sdr.GetOrdinal("Customer"));
        //            model.DeadDay = sdr.IsDBNull(sdr.GetOrdinal("DeadDay")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("DeadDay"));
        //            model.Deadline = sdr.IsDBNull(sdr.GetOrdinal("Deadline")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Deadline"));
        //            model.TourCount = sdr.IsDBNull(sdr.GetOrdinal("TourCount")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("TourCount"));
        //        }
        //        return model;
        //    }
        //}

        /// <summary>
        ///计调报价 
        /// </summary>
        /// <param name="QuoteId">报价编号</param>
        /// <param name="CostCalculation">成本核算</param>
        /// <returns></returns>
        public bool PlanerQuote(string QuoteId, string CostCalculation)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_PlanerQuote);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            this._db.AddInParameter(cmd, "CostMoney", DbType.String, CostCalculation);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据报价编号得到相关其它报价
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.MTourQuoteNo> GetTourQuoteNo(string QuoteId)
        {
            IList<EyouSoft.Model.TourStructure.MTourQuoteNo> list = new List<EyouSoft.Model.TourStructure.MTourQuoteNo>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetQuoteNo);
            this._db.AddInParameter(cmd, "QuoteId", DbType.AnsiStringFixedLength, QuoteId);
            using (IDataReader sdr = DbHelper.ExecuteReader(cmd, _db))
            {
                while (sdr.Read())
                {
                    list.Add(new EyouSoft.Model.TourStructure.MTourQuoteNo()
                    {
                        QuoteId = sdr.GetString(sdr.GetOrdinal("QuoteId")),
                        Times = sdr.GetInt32(sdr.GetOrdinal("TimeCount")),
                        QuoteState = (EyouSoft.Model.EnumType.TourStructure.QuoteState)sdr.GetByte(sdr.GetOrdinal("QuoteStatus"))
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获得报价签证文件
        /// </summary>
        /// <param name="QuoteId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileList(string QuoteId, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.ComStructure.MComAttach> list = new List<EyouSoft.Model.ComStructure.MComAttach>();
            EyouSoft.Model.ComStructure.MComAttach item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_ComAttach";
            string PrimaryKey = "";
            string OrderByString = "Downloads";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append("[Name],FilePath");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" ItemId='{0}' and ItemType=17", QuoteId);
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.ComStructure.MComAttach()
                    {
                        Name = rdr["Name"].ToString(),
                        FilePath = rdr["FilePath"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 得到报价行程
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> GetPlanByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list = new List<EyouSoft.Model.TourStructure.MPlanBaseInfo>();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                list.Add(new EyouSoft.Model.TourStructure.MPlanBaseInfo()
                {
                    PlanId = Utils.GetXAttributeValue(xRow, "PlanId"),
                    ItemId = Utils.GetXAttributeValue(xRow, "QuoteId"),
                    Section = Utils.GetXAttributeValue(xRow, "Section"),
                    Hotel = Utils.GetXAttributeValue(xRow, "Hotel"),
                    Breakfast = Utils.GetXAttributeValue(xRow, "Breakfast") == "1" ? true : false,
                    Lunch = Utils.GetXAttributeValue(xRow, "Lunch") == "1" ? true : false,
                    Supper = Utils.GetXAttributeValue(xRow, "Supper") == "1" ? true : false,
                    Content = Utils.GetXAttributeValue(xRow, "Content"),
                    Days = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Days")),
                    FilePath = Utils.GetXAttributeValue(xRow, "FilePath"),
                    TourPlanSpot = this.GetPlanSpotByXml(Utils.GetXAttributeValue(xRow, "QuotePlanSpot")),
                    HotelId = Utils.GetXAttributeValue(xRow, "HotelId"),
                    Traffic = Utils.GetXAttributeValue(xRow, "Traffic")
                });
            }
            return list;
        }

        /// <summary>
        /// 得到行程景点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlanSpot> GetPlanSpotByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourPlanSpot> list = new List<EyouSoft.Model.TourStructure.MTourPlanSpot>();
            EyouSoft.Model.TourStructure.MTourPlanSpot item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourPlanSpot()
                {
                    PlanId = Utils.GetXAttributeValue(xRow, "PlanId"),
                    SpotId = Utils.GetXAttributeValue(xRow, "SpotId"),
                    SpotName = Utils.GetXAttributeValue(xRow, "SpotName")
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到团队计划分项报价
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourTeamPrice> GetTourTeamPriceByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourTeamPrice> list = new List<EyouSoft.Model.TourStructure.MTourTeamPrice>();
            EyouSoft.Model.TourStructure.MTourTeamPrice item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourTeamPrice()
                {
                    TourId = Utils.GetXAttributeValue(xRow, "TourId"),
                    ServiceType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "ServiceType")),
                    ServiceStandard = Utils.GetXAttributeValue(xRow, "ServiceStandard"),
                    Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)Utils.GetInt(Utils.GetXAttributeValue(xRow, "Unit")),
                    Quote = Utils.GetDecimal(Utils.GetXAttributeValue(xRow, "Quote"))
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 获得团队报价次数编号列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourQuoteNo> GetTourQuoteNoByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourQuoteNo> list = new List<EyouSoft.Model.TourStructure.MTourQuoteNo>();
            EyouSoft.Model.TourStructure.MTourQuoteNo item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourQuoteNo()
                {
                    QuoteId = Utils.GetXAttributeValue(xRow, "QuoteId"),
                    Times = Utils.GetInt(Utils.GetXAttributeValue(xRow, "TimeCount"))
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 得到签证文件实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.ComStructure.MComAttach> GetVisaFileByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.ComStructure.MComAttach> list = new List<EyouSoft.Model.ComStructure.MComAttach>();
            EyouSoft.Model.ComStructure.MComAttach item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.ComStructure.MComAttach()
                {
                    Downloads = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Downloads")),
                    FilePath = Utils.GetXAttributeValue(xRow, "FilePath"),
                    ItemId = Utils.GetXAttributeValue(xRow, "ItemId"),
                    ItemType = (EyouSoft.Model.EnumType.ComStructure.AttachItemType)Utils.GetInt(Utils.GetXAttributeValue(xRow, "ItemType")),
                    Name = Utils.GetXAttributeValue(xRow, "Name"),
                    Size = Utils.GetInt(Utils.GetXAttributeValue(xRow, "Size"))
                };
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 创建行程XML
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreatePlanXml(IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list, string QuoteId)
        {
            //<Root><QuotePlan PlanId="行程编号" QuoteId="报价编号" Days="第几天" Section="区间" Hotel="住宿" HotelId="住宿编号" Breakfast="用餐早" Lunch="用餐中" Supper="用餐晚" Content="行程内容" FilePath="行程图片" Traffic="交通" /></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                item.PlanId = System.Guid.NewGuid().ToString();
                xmlDoc.AppendFormat("<QuotePlan PlanId=\"{0}\" QuoteId=\"{1}\" Days=\"{2}\" Section=\"{3}\" Hotel=\"{4}\" HotelId=\"{5}\" Breakfast=\"{6}\" Lunch=\"{7}\" Supper=\"{8}\" Content=\"{9}\" FilePath=\"{10}\" Traffic=\"{11}\"/>",
                    item.PlanId,
                    QuoteId,
                    item.Days,
                    Utils.ReplaceXmlSpecialCharacter(item.Section),
                    Utils.ReplaceXmlSpecialCharacter(item.Hotel),
                    item.HotelId,
                    item.Breakfast ? "1" : "0",
                    item.Lunch ? "1" : "0",
                    item.Supper ? "1" : "0",
                    Utils.ReplaceXmlSpecialCharacter(item.Content),
                    item.FilePath,
                    Utils.ReplaceXmlSpecialCharacter(item.Traffic));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建行程景点XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreatePlanSpotXml(IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list)
        {
            //<Root><QuotePlanSpot PlanId="行程编号" SpotID="景点编号" SpotName="景点名称"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                var spots = item.TourPlanSpot;
                if (spots != null)
                {
                    string PlanId = item.PlanId;
                    foreach (var spot in spots)
                    {
                        xmlDoc.AppendFormat("<QuotePlanSpot PlanId=\"{0}\" SpotID=\"{1}\" SpotName=\"{2}\"/>", PlanId, spot.SpotId, Utils.ReplaceXmlSpecialCharacter(spot.SpotName));
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建团队分项报价XML
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteId"></param>
        /// <returns></returns>
        private string CreateTourTeamPriceXml(IList<EyouSoft.Model.TourStructure.MTourTeamPrice> list, string QuoteId)
        {
            //<Root><QuotePrice QuoteId="报价编号" Unit="单位" Quote="单项报价" ServiceStandard="服务标准" ServiceType="服务类型" ServiceId="服务编号" ServiceName="服务项目名称"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<QuotePrice QuoteId=\"{0}\" Unit=\"{1}\" Quote=\"{2}\" ServiceStandard=\"{3}\" ServiceType=\"{4}\" ServiceId=\"{5}\" ServiceName=\"{6}\"/>", QuoteId, (int)item.Unit, item.Quote, Utils.ReplaceXmlSpecialCharacter(item.ServiceStandard), (int)item.ServiceType, item.ServiceId, item.ServiceName);
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建报价成功后游客XML
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateTravellerXml(string OrderId, IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list)
        {
            //<Root><Traveller TravellerId="游客编号" OrderId="订单编号" CnName="游客中文姓名" EnName="游客英文姓名" VisitorType="游客类型" CardType="证件类型" CardNumber="证件号码" CardValidDate="证件有效期" VisaStatus="签证状态" IsCardTransact="证件是否已办理" Gender="性别" Contact="联系方式" LNotice="出团通知" RNotice="回团通知" Remark="备注" Status="游客状态" CareId="身份证号" IsInsurance="是否有保险" LiCheng="里程积分"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                if (item.OrderTravellerInsuranceList != null && item.OrderTravellerInsuranceList.Count > 0)
                {
                    item.IsInsurance = true;
                }
                item.TravellerId = System.Guid.NewGuid().ToString();
                xmlDoc.AppendFormat("<Traveller TravellerId=\"{0}\" OrderId=\"{1}\" CnName=\"{2}\" EnName=\"{3}\" VisitorType=\"{4}\" CardType=\"{5}\" CardNumber=\"{6}\" CardValidDate=\"{7}\" VisaStatus=\"{8}\" IsCardTransact=\"{9}\" Gender=\"{10}\" Contact=\"{11}\" LNotice=\"{12}\" RNotice=\"{13}\" Remark=\"{14}\" Status=\"{15}\" CardId=\"{16}\" IsInsurance=\"{17}\" LiCheng=\"{18}\"/>", item.TravellerId, OrderId, Utils.ReplaceXmlSpecialCharacter(item.CnName), Utils.ReplaceXmlSpecialCharacter(item.EnName), (int)item.VisitorType, (int)item.CardType, item.CardNumber, item.CardValidDate, (int?)(item.VisaStatus.HasValue ? item.VisaStatus : null), item.IsCardTransact ? "1" : "0", (int)item.Gender, Utils.ReplaceXmlSpecialCharacter(item.Contact), item.LNotice ? "1" : "0", item.RNotice ? "1" : "0", Utils.ReplaceXmlSpecialCharacter(item.Remark), (int)EyouSoft.Model.EnumType.TourStructure.TravellerStatus.在团, item.CardId, item.IsInsurance ? "1" : "0",Utils.ReplaceXmlSpecialCharacter(item.LiCheng));
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建团队，订单信息XML
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string CreateTourInfoXml(EyouSoft.Model.TourStructure.MTourQuoteTourInfo info, int Days)
        {
            if (info == null) return null;
            //<Root><TourInfo TourId="团队编号" TourCode="团号" LDate="出团日期" RDate="回团日期" LTraffic="出发交通" RTraffic="返程交通" Gather="集合方式" TourType="计划类型" TourStatus="计划状态" SaleAddCost="销售增加费用" SaleReduceCost=\"销售减少费用\" AddCostRemark=\"增加备注\" ReduceCostRemark=\"减少备注\" OrderRemark=\"订单备注\" GuideIncome="导游现收" SalerIncome="销售应收" /></Root>
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<TourInfo TourId=\"{0}\" TourCode=\"{1}\" LDate=\"{2}\" RDate=\"{3}\" LTraffic=\"{4}\" RTraffic=\"{5}\" Gather=\"{6}\" TourType=\"{7}\" TourStatus=\"{8}\" SaleAddCost=\"{9}\" SaleReduceCost=\"{10}\" AddCostRemark=\"{11}\" ReduceCostRemark=\"{12}\" OrderRemark=\"{13}\" GuideIncome=\"{14}\" SalerIncome=\"{15}\" />",
                System.Guid.NewGuid().ToString(),
                string.IsNullOrEmpty(info.TourCode) ? "" : info.TourCode,
                info.LDate,
                info.LDate.AddDays(Days - 1),
                string.IsNullOrEmpty(info.LTraffic) ? "" : Utils.ReplaceXmlSpecialCharacter(info.LTraffic), string.IsNullOrEmpty(info.RTraffic) ? "" : Utils.ReplaceXmlSpecialCharacter(info.RTraffic), string.IsNullOrEmpty(info.Gather) ? "" : Utils.ReplaceXmlSpecialCharacter(info.Gather),
                (int)info.TourType,
                (int)info.TourStatus,
                info.SaleAddCost,
                info.SaleReduceCost,
                string.IsNullOrEmpty(info.AddCostRemark) ? "" : Utils.ReplaceXmlSpecialCharacter(info.AddCostRemark),
                string.IsNullOrEmpty(info.ReduceCostRemark) ? "" : Utils.ReplaceXmlSpecialCharacter(info.ReduceCostRemark),
                string.IsNullOrEmpty(info.OrderRemark) ? "" : Utils.ReplaceXmlSpecialCharacter(info.OrderRemark), info.GuideIncome,
                info.SalerIncome
                );
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建垫付申请XML
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string CreateOverrunXml(EyouSoft.Model.TourStructure.MAdvanceApp info)
        {
            if (info == null) return null;
            //<Root><AdvanceApp ApplierId="申请人编号" Applier="申请人" ApplyTime="申请时间" DisburseAmount="垫付金额" DeptId="操作人部门编号" Remark="备注"/></Root>
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            xmlDoc.AppendFormat("<AdvanceApp ApplierId=\"{0}\" Applier=\"{1}\" ApplyTime=\"{2}\" DisburseAmount=\"{3}\" DeptId=\"{4}\" Remark=\"{5}\" />", info.ApplierId, info.Applier, info.ApplyTime, info.DisburseAmount, info.DeptId, info.Remark);
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建签证文件XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateVisaFileXml(string QuoteId, IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {
            //<Root><VisaFile ItemType="关联类型" ItemId="关联编号" Name="附件名称" FilePath="附件路径" Size="附件大小" Downloads="下载次数"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                xmlDoc.AppendFormat("<VisaFile ItemType=\"{0}\" ItemId=\"{1}\" Name=\"{2}\" FilePath=\"{3}\" Size=\"{4}\" Downloads=\"{5}\"/>", (int)EyouSoft.Model.EnumType.ComStructure.AttachItemType.报价签证资料, QuoteId, item.Name, item.FilePath, item.Size, item.Downloads);

            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }

        /// <summary>
        /// 创建游客保险XML
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string CreateInsurXml(IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list)
        {
            //<Root><TravellerInsur TravellerId="游客编号" InsuranceId="保险编号" BuyNum="购买份数" UnitPrice="单价" SumPrice="合计金额"/></Root>
            if (list == null || list.Count == 0) return null;
            StringBuilder xmlDoc = new StringBuilder();
            xmlDoc.Append("<Root>");
            foreach (var item in list)
            {
                if (item.OrderTravellerInsuranceList != null)
                {
                    foreach (var items in item.OrderTravellerInsuranceList)
                    {
                        xmlDoc.AppendFormat("<TravellerInsur TravellerId=\"{0}\" InsuranceId=\"{1}\" BuyNum=\"{2}\" UnitPrice=\"{3}\" SumPrice=\"{4}\"/>", item.TravellerId, items.InsuranceId, items.BuyNum, items.UnitPrice, items.SumPrice);
                    }
                }
            }
            xmlDoc.Append("</Root>");
            return xmlDoc.ToString();

        }
        #endregion
    }
}
