using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyouSoft.Toolkit;
using EyouSoft.Toolkit.DAL;
using System.Xml.Linq;

namespace EyouSoft.DAL.HPlanStructure
{
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.GovStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.GysStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.GovStructure;
    using EyouSoft.Model.HPlanStructure;

    /// <summary>
    /// 描述:数据操作计调安排类
    /// 创建时间:2013-05-31
    /// </summary>
    public class DPlan : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.HPlanStructure.IPlan
    {
        #region 构造
        /// <summary>
        /// 数据库对象
        /// </summary>
        private Database db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DPlan()
        {
            this.db = base.SystemStore;
        }
        #endregion

        #region 计调增删改查
        /// <summary>
        /// 计调添加/修改
        /// </summary>
        /// <param name="mdl">计调实体</param>
        /// <returns>正数：成功 0或负数：失败</returns>
        public int AddOrUpdPlan(MPlanBaseInfo mdl)
        {
            var dc = this.db.GetStoredProcCommand("proc_Plan_AddOrUpd");

            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this.db.AddInParameter(dc, "@CompanyId", DbType.AnsiStringFixedLength, mdl.CompanyId);
            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, mdl.TourId);
            this.db.AddInParameter(dc, "@Type", DbType.Byte, (int)mdl.Type);
            this.db.AddInParameter(dc, "@SourceId", DbType.AnsiStringFixedLength, mdl.SourceId);
            this.db.AddInParameter(dc, "@SourceName", DbType.String, mdl.SourceName);
            this.db.AddInParameter(dc, "@ContactName", DbType.String, mdl.ContactName);
            this.db.AddInParameter(dc, "@ContactPhone", DbType.String, mdl.ContactPhone);
            this.db.AddInParameter(dc, "@ContactFax", DbType.String, mdl.ContactFax);
            this.db.AddInParameter(dc, "@ContactMobile", DbType.String, mdl.ContactMobile);
            this.db.AddInParameter(dc, "@Num", DbType.Int32, mdl.Num);
            this.db.AddInParameter(dc, "@ReceiveJourney", DbType.String, mdl.ReceiveJourney);
            this.db.AddInParameter(dc, "@PlanCost", DbType.Decimal, mdl.PlanCost);
            this.db.AddInParameter(dc, "@PaymentType", DbType.Byte, (int)mdl.PaymentType);
            this.db.AddInParameter(dc, "@SigningCount", DbType.Int32, mdl.SigningCount);
            this.db.AddInParameter(dc, "@AdultNumber", DbType.Int32, mdl.AdultNumber);
            this.db.AddInParameter(dc, "@ChildNumber", DbType.Int32, mdl.ChildNumber);
            this.db.AddInParameter(dc, "@LeaderNumber", DbType.Int32, mdl.LeaderNumber);
            this.db.AddInParameter(dc, "@DueToway", DbType.Byte, (int)mdl.DueToway);
            this.db.AddInParameter(dc, "@IsDuePlan", DbType.AnsiStringFixedLength, mdl.IsDuePlan ? "1" : "0");
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this.db.AddInParameter(dc, "@GuideNotes", DbType.String, mdl.GuideNotes);
            this.db.AddInParameter(dc, "@Remarks", DbType.String, mdl.Remarks);
            this.db.AddInParameter(dc, "@Confirmation", DbType.Decimal, mdl.Confirmation);
            this.db.AddInParameter(dc, "@OperatorDeptId", DbType.Int32, mdl.OperatorDeptId);
            this.db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this.db.AddInParameter(dc, "@Operator", DbType.String, mdl.Operator);
            this.db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this.db.AddInParameter(dc, "@Prepaid", DbType.Decimal, mdl.Prepaid);
            this.db.AddInParameter(dc, "@AddStatus", DbType.Byte, (int)mdl.AddStatus);
            this.db.AddInParameter(dc, "@ServiceStandard", DbType.String, mdl.ServiceStandard);
            this.db.AddInParameter(dc, "@StartDate", DbType.DateTime, mdl.StartDate);
            this.db.AddInParameter(dc, "@StartTime", DbType.String, mdl.StartTime);
            if (mdl.EndDate != DateTime.MinValue) this.db.AddInParameter(dc, "@EndDate", DbType.DateTime, mdl.EndDate);
            else this.db.AddInParameter(dc, "@EndDate", DbType.DateTime, DBNull.Value);
            this.db.AddInParameter(dc, "@EndTime", DbType.String, mdl.EndTime);


            if (mdl.PlanHotel != null)
            {
                this.db.AddInParameter(dc, "@XMLHotel", DbType.Xml, this.GetPlanHotelXml(mdl.PlanHotel));
            }
            if (mdl.PlanHotelRoomList != null && mdl.PlanHotelRoomList.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLPlanHotelRoom", DbType.Xml, this.GetPlanHotelRoomXml(mdl.PlanHotelRoomList));
            }
            if (mdl.PlanAttractionsList != null && mdl.PlanAttractionsList.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLPlanAttractions", DbType.Xml, this.GetPlanAttractionsXml(mdl.PlanAttractionsList));
            }
            if (mdl.PlanCarList != null && mdl.PlanCarList.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLPlanCar", DbType.Xml, this.GetPlanCarXml(mdl.PlanCarList));
            }
            if (mdl.PlanDiningList != null && mdl.PlanDiningList.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLPlanDining", DbType.Xml, this.GetPlanDiningXml(mdl.PlanDiningList));
            }
            if (mdl.PlanLargeFrequencyList != null && mdl.PlanLargeFrequencyList.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLLargeFrequency", DbType.Xml, this.GetPlanLargeFrequencyXml(mdl.PlanLargeFrequencyList));
            }
            if (mdl.PlanGuide != null)
            {
                this.db.AddInParameter(dc, "@XMLGuide", DbType.Xml, this.GetPlanGuideXml(mdl.PlanGuide));
            }
            if (mdl.PlanGood != null)
            {
                this.db.AddInParameter(dc, "@XMLGood", DbType.Xml, this.GetPlanGoodXml(mdl.PlanGood));
            }
            if (mdl.PlanShop != null)
            {
                this.db.AddInParameter(dc, "@XMLShop", DbType.Xml, this.GetPlanShopXml(mdl.PlanShop));
            }
            if (mdl.PlanCostChange != null && mdl.PlanCostChange.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLCostChange", DbType.Xml, this.GetPlanCostChangeXml(mdl.PlanCostChange));
            }

            this.db.AddOutParameter(dc, "@IsResult", DbType.Int32, 1);

            DbHelper.RunProcedure(dc, this.db);

            return Convert.ToInt32(db.GetParameterValue(dc, "IsResult"));
        }

        /// <summary>
        /// 计调预安排
        /// </summary>
        /// <param name="mdl">与安排实体</param>
        /// <returns>正数：成功 0或负数：失败</returns>
        public int PlanYuAnPai(EyouSoft.Model.HTourStructure.MTourStatusChange model)
        {
            DbCommand cmd = db.GetStoredProcCommand("proc_Plan_YuAnPai");
            db.AddInParameter(cmd, "tourID", DbType.AnsiStringFixedLength, model.TourId);
            db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            db.AddInParameter(cmd, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorId);
            db.AddInParameter(cmd, "Operator", DbType.String, model.Operator);
            db.AddInParameter(cmd, "OperatDepID", DbType.Int32, model.OperatorDeptId);
            db.AddOutParameter(cmd, "IsResult", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, db);
            return Convert.ToInt32(db.GetParameterValue(cmd, "IsResult"));
        }

        /// <summary>
        /// 计调安排项目金额增/减
        /// </summary>
        /// <param name="mdl">金额增减实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdPlanCostChange(MPlanCostChange mdl)
        {
            var sql = new StringBuilder();

            sql.Append(" IF EXISTS ( SELECT  1");
            sql.Append("             FROM    dbo.tbl_PlanCostChange");
            sql.Append("             WHERE   PlanId = @PlanId");
            sql.Append("                     AND ChangeType = @ChangeType");
            sql.Append("                     AND [Type] = @Type ) ");
            sql.Append("     BEGIN");
            sql.Append("         DECLARE @TmpNum INT ,");
            sql.Append("             @TmpChangeCost MONEY,");
            sql.Append("             @TmpDNum DECIMAL(10,4)");
            sql.Append("         SELECT  @TmpNum = PeopleNumber ,");
            sql.Append("                 @TmpChangeCost = ChangeCost,@TmpDNum=DNum");
            sql.Append("         FROM    dbo.tbl_PlanCostChange");
            sql.Append("         WHERE   PlanId = @PlanId");
            sql.Append("                 AND ChangeType = @ChangeType");
            sql.Append("                 AND [Type] = @Type");
            sql.Append("         IF @Type = '1' ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num - @TmpNum + @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation - @TmpChangeCost");
            sql.Append("                         + @ChangeCost");
            //sql.Append(" ,DNum=DNum-@TmpDNum+@DNum ");
            sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         ELSE ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num + @TmpNum - @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation + @TmpChangeCost");
            sql.Append("                         - @ChangeCost");
            //sql.Append(" ,DNum=DNum+@TmpDNum-@DNum ");
            sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         UPDATE  [dbo].[tbl_PlanCostChange]");
            sql.Append("         SET     [PeopleNumber] = @PeopleNumber ,");
            sql.Append("                 [ChangeCost] = @ChangeCost ,");
            sql.Append("                 [Remark] = @Remark ,");
            sql.Append("                 [IssueTime] = @IssueTime");
            sql.Append("                 ,[DNum] = @DNum");
            sql.Append("         WHERE   [PlanId] = @PlanId");
            sql.Append("                 AND [ChangeType] = @ChangeType");
            sql.Append("                 AND [Type] = @Type");
            sql.Append("     END");
            sql.Append(" ELSE ");
            sql.Append("     BEGIN");
            sql.Append("         IF @Type = '1' ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num + @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation + @ChangeCost");
            //sql.Append(" ,DNum=DNum+@DNum ");
            sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         ELSE ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num - @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation - @ChangeCost");
            //sql.Append(" ,DNum=DNum-@DNum ");
            sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         INSERT  INTO [dbo].[tbl_PlanCostChange]");
            sql.Append("                 ( [PlanId] ,");
            sql.Append("                   [ChangeType] ,");
            sql.Append("                   [Type] ,");
            sql.Append("                   [PeopleNumber] ,");
            sql.Append("                   [ChangeCost] ,");
            sql.Append("                   [Remark] ,");
            sql.Append("                   [IssueTime],[DNum]");
            sql.Append("                 )");
            sql.Append("         VALUES  ( @PlanId ,");
            sql.Append("                   @ChangeType ,");
            sql.Append("                   @Type ,");
            sql.Append("                   @PeopleNumber ,");
            sql.Append("                   @ChangeCost ,");
            sql.Append("                   @Remark ,");
            sql.Append("                   @IssueTime,@DNum");
            sql.Append("                 )");
            sql.Append("     END");

            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, mdl.PlanId);
            this.db.AddInParameter(dc, "@ChangeType", DbType.Byte, (int)mdl.ChangeType);
            this.db.AddInParameter(dc, "@Type", DbType.AnsiStringFixedLength, mdl.Type ? "1" : "0");
            this.db.AddInParameter(dc, "@PeopleNumber", DbType.Int32, mdl.PeopleNumber);
            this.db.AddInParameter(dc, "@ChangeCost", DbType.Decimal, mdl.ChangeCost);
            this.db.AddInParameter(dc, "@Remark", DbType.String, mdl.Remark);
            this.db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this.db.AddInParameter(dc, "DNum", DbType.Decimal, mdl.DNum);
            return DbHelper.ExecuteSqlTrans(dc, this.db) > 0;
        }


        /// <summary>
        /// 根据计调编号删除安排的计调项目
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public bool DelPlan(string planId)
        {
            //删除计调项的时候、根据计调编号判断该计调项是否存在已登记的支出帐款或者已审核的杂费收入项
            if (IsExist(planId))
            {
                return false;
            }

            var sql = new StringBuilder();

            //删除某个计调项目同步维护计调状态表
            sql.Append(" DECLARE @PlanType TINYINT ,");
            sql.Append("     @PlanStatus TINYINT ,");
            sql.Append("     @TourId CHAR(36) ,");
            sql.Append("     @Sql NVARCHAR(MAX)");
            sql.Append(" SELECT  @PlanType = Type ,");
            sql.Append("         @PlanStatus = Status ,");
            sql.Append("         @TourId = TourId");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append(" WHERE   PlanId = @PlanId");
            sql.Append(" UPDATE  tbl_plan");
            sql.Append(" SET     IsDelete = '1'");
            sql.Append(" WHERE   planId = @planId");
            sql.Append(" SET @Sql = dbo.fn_UpdPlanStatus(@PlanType, @PlanStatus, @TourId,'1')");
            sql.Append(" EXECUTE(@Sql)");
            //删除领料同步维护物品管理表
            sql.Append(" DELETE  FROM dbo.tbl_GovGoodUse  WHERE   PlanId = @PlanId ");
            //删除跟团相关的其他收入登记
            sql.AppendFormat(" UPDATE tbl_FinOtherInFee SET IsDeleted=1 WHERE PlanId = @PlanId AND Status={0}", (int)FinStatus.财务待审批);

            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@planId", DbType.AnsiStringFixedLength, planId);
            return DbHelper.ExecuteSqlTrans(dc, this.db) > 0;
        }
        /// <summary>
        /// 删除计调项的时候、根据计调编号判断该计调项是否存在已登记的支出帐款或者已审核的杂费收入项
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExist(string planId)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT 1 FROM dbo.tbl_FinRegister WHERE PlanId=@PlanId AND IsDeleted=0 UNION SELECT 1 FROM dbo.tbl_FinOtherInFee WHERE PlanId=@PlanId AND IsDeleted=0 AND Status=1");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@planId", DbType.AnsiStringFixedLength, planId);

            return DbHelper.Exists(dc, this.db);
        }

        /// <summary>
        /// 根据计调编号获取某个计调安排项
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanBaseInfo GetModel(string planId)
        {
            var mdl = new MPlanBaseInfo();
            var sql = new StringBuilder();

            sql.Append(" SELECT  * ");
            sql.Append(",XMLTour=(SELECT RouteName,TourCode,SellerName,TourMark,SaleMark,Adults,Childs,Leaders,SiPei,GuoJi=(SELECT [Name] FROM tbl_SysCountry WHERE CountryId=tbl_Tour.CountryId) FROM tbl_Tour WHERE TourId=tbl_Plan.TourId FOR XML RAW,ROOT('root'))");
            sql.Append(",Approver=STUFF((SELECT ',' + Approver FROM (SELECT Approver FROM tbl_FinRegister WHERE PlanId=tbl_Plan.PlanId AND IsDeleted=0 AND ISNULL(Approver,'')<>'' GROUP BY Approver) AS A FOR XML PATH('')),1, 1,'') ");
            sql.Append(", Accountant=STUFF((SELECT ',' + Accountant FROM (SELECT Accountant FROM tbl_FinRegister WHERE PlanId=tbl_Plan.PlanId AND IsDeleted=0 AND ISNULL(Accountant,'')<>'' GROUP BY Accountant) AS A FOR XML PATH('')),1, 1,'') ");
            sql.Append(" FROM    [dbo].[tbl_Plan] WHERE   PlanId = @PlanId");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.TourId = dr["TourId"].ToString();
                    mdl.RouteName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "RouteName");
                    mdl.TourCode = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourCode");
                    mdl.SellerName = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SellerName");
                    mdl.TourMark = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "TourMark");
                    mdl.SaleMark = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SaleMark");
                    mdl.GuoJi = Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "GuoJi");
                    mdl.Adults = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "Adults"));
                    mdl.Childs = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "Childs"));
                    mdl.Leaders = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "Leaders"));
                    mdl.SiPei = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTour"].ToString(), "SiPei"));
                    mdl.Approver = dr["Approver"].ToString();
                    mdl.Accountant = dr["Accountant"].ToString();
                    mdl.Type = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.SourceId = dr["SourceId"].ToString();
                    mdl.SourceName = dr["SourceName"].ToString();
                    mdl.ContactName = dr["ContactName"].ToString();
                    mdl.ContactPhone = dr["ContactPhone"].ToString();
                    mdl.ContactFax = dr["ContactFax"].ToString();
                    mdl.ContactMobile = dr["ContactMobile"].ToString();
                    mdl.Num = dr.GetInt32(dr.GetOrdinal("Num"));
                    mdl.ReceiveJourney = dr["ReceiveJourney"].ToString();
                    mdl.PlanCost = dr.GetDecimal(dr.GetOrdinal("PlanCost"));
                    mdl.PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType"));
                    mdl.SigningCount = dr.GetInt32(dr.GetOrdinal("SigningCount"));
                    mdl.AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber"));
                    mdl.ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber"));
                    mdl.LeaderNumber = dr.GetInt32(dr.GetOrdinal("LeaderNumber"));
                    mdl.DueToway = (DueToway)dr.GetByte(dr.GetOrdinal("DueToway"));
                    mdl.IsDuePlan = dr.GetString(dr.GetOrdinal("IsDuePlan")) == "1";
                    mdl.Status = (PlanState)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.GuideNotes = dr["GuideNotes"].ToString();
                    mdl.Remarks = dr["Remarks"].ToString();
                    mdl.CostId = dr["CostId"].ToString();
                    mdl.CostName = dr["CostName"].ToString();
                    mdl.CostStatus = dr.GetString(dr.GetOrdinal("CostStatus")) == "1";
                    if (!dr.IsDBNull(dr.GetOrdinal("CostTime")))
                    {
                        mdl.CostTime = dr.GetDateTime(dr.GetOrdinal("CostTime"));
                    }
                    mdl.Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation"));
                    mdl.CostRemarks = dr["CostRemarks"].ToString();
                    mdl.OperatorDeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.Operator = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.Prepaid = dr.GetDecimal(dr.GetOrdinal("Prepaid"));
                    mdl.AddStatus = (PlanAddStatus)dr.GetByte(dr.GetOrdinal("AddStatus"));
                    mdl.ServiceStandard = dr["ServiceStandard"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("StartDate")))
                    {
                        mdl.StartDate = dr.GetDateTime(dr.GetOrdinal("StartDate"));
                    }
                    mdl.StartTime = dr["StartTime"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("EndDate")))
                    {
                        mdl.EndDate = dr.GetDateTime(dr.GetOrdinal("EndDate"));
                    }
                    mdl.EndTime = dr["EndTime"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号获取团队支出列表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public IList<MPlanBaseInfo> GetList(string tourId)
        {
            var lst = new List<MPlanBaseInfo>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [Type] ,");
            sql.Append("         [SourceName] ,");
            sql.Append("         [Num] ,");
            sql.Append("         [PaymentType] ,");
            sql.AppendFormat("         [Confirmation],PlanId,CostStatus,Prepaid,YiDengDaiFu=(select isnull(sum(PaymentAmount),0) from tbl_FinRegister where planid=tbl_plan.planid and status<>{0}) ", (int)FinStatus.账务已支付);
            sql.Append(" FROM    [dbo].[tbl_Plan]");
            sql.Append(" WHERE   TourId = @TourId AND Status=@Status AND IsDelete='0'");
            sql.Append(" ORDER BY [Type]");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)PlanState.已落实);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanBaseInfo
                    {
                        Type = (PlanProject)dr.GetByte(dr.GetOrdinal("Type")),
                        SourceName = dr["SourceName"].ToString(),
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType")),
                        Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                        PlanId = dr["PlanId"].ToString(),
                        CostStatus = dr["CostStatus"].ToString() == "1",
                        Prepaid = dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        YiDengDaiFu = dr.GetDecimal(dr.GetOrdinal("YiDengDaiFu"))
                    };
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取酒店安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanHotel GetHotel(string planId)
        {
            var mdl = new MPlanHotel();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [Star] ,");
            sql.Append("         [Days] ,");
            sql.Append("         [FreeNumber] ,");
            sql.Append("         [IsMeal] ,");
            sql.Append("         [FreePrice] ,");
            sql.Append("         XMLHotelRoom=(");
            sql.Append("                        SELECT  [PlanId] ,");
            sql.Append("                                [RoomId] ,");
            sql.Append("                                [RoomType] ,");
            sql.Append("                                [UnitPrice] ,");
            sql.Append("                                [PriceType] ,");
            sql.Append("                                [Quantity] ,");
            sql.Append("                                [Total]");
            sql.Append("                        FROM    [dbo].[tbl_PlanHotelRoom]");
            sql.Append("                        WHERE   PlanId = tbl_PlanHotel.PlanId for xml raw,root");
            sql.Append("                        )");
            sql.Append(" FROM    [dbo].[tbl_PlanHotel]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.Star = (JiuDianXingJi)dr.GetByte(dr.GetOrdinal("Star"));
                    mdl.Days = dr.GetInt32(dr.GetOrdinal("Days"));
                    mdl.FreeNumber = dr.GetDecimal(dr.GetOrdinal("FreeNumber"));
                    mdl.IsMeal = (PlanHotelIsMeal)dr.GetByte(dr.GetOrdinal("IsMeal"));
                    mdl.FreePrice = dr.GetDecimal(dr.GetOrdinal("FreePrice"));
                    mdl.PlanHotelRoomList = this.GetPlanHotelRoomLst(dr["XMLHotelRoom"].ToString());
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取 地接/酒店 房屋类型安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanHotelRoom> GetHotelRoom(string planId)
        {
            var lst = new List<MPlanHotelRoom>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [RoomId] ,");
            sql.Append("         [RoomType] ,");
            sql.Append("         [UnitPrice] ,");
            sql.Append("         [PriceType] ,");
            sql.Append("         [Quantity] ,");
            sql.Append("         [Total] ");
            sql.Append(" FROM    [dbo].[tbl_PlanHotelRoom]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanHotelRoom();
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.RoomId = dr["RoomId"].ToString();
                    mdl.RoomType = dr["RoomType"].ToString();
                    mdl.UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"));
                    mdl.PriceType = (PlanHotelPriceType)dr.GetByte(dr.GetOrdinal("PriceType"));
                    mdl.Quantity = dr.GetInt32(dr.GetOrdinal("Quantity"));
                    mdl.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    lst.Add(mdl);
                    mdl = null;
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取景点安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanAttractions> GetAttractions(string planId)
        {
            var lst = new List<MPlanAttractions>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [AttractionsId] ,");
            sql.Append("         [Attractions] ,");
            sql.Append("         [AdultNumber] ,");
            sql.Append("         [ChildNumber] ,");
            sql.Append("         [AdultPrice] ,");
            sql.Append("         [ChildPrice] ,");
            sql.Append("         [JTprice] ,");
            sql.Append("         [Seats] ,");
            sql.Append("         [VisitTime] ,");
            sql.Append("         [SumPrice] ,");
            sql.Append("         [BeiZhu] ");
            sql.Append(" FROM    [dbo].[tbl_PlanAttractions]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanAttractions();
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.AttractionsId = dr["AttractionsId"].ToString();
                    mdl.Attractions = dr["Attractions"].ToString();
                    mdl.AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber"));
                    mdl.ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber"));
                    mdl.AdultPrice = dr.GetDecimal(dr.GetOrdinal("AdultPrice"));
                    mdl.ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice"));
                    mdl.JTprice = dr.GetDecimal(dr.GetOrdinal("JTprice"));
                    mdl.Seats = dr["Seats"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("VisitTime")))
                        mdl.VisitTime = dr.GetDateTime(dr.GetOrdinal("VisitTime"));
                    mdl.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    mdl.BeiZhu = dr["BeiZhu"].ToString();
                    lst.Add(mdl);
                    mdl = null;
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取用车安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanCar> GetCar(string planId)
        {
            var lst = new List<MPlanCar>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [PriceType] ,");
            sql.Append("         [CarId] ,");
            sql.Append("         [Models] ,");
            sql.Append("         [CarNumber] ,");
            sql.Append("         [Driver] ,");
            sql.Append("         [DriverPhone] ,");
            sql.Append("         [CarPrice] ,");
            sql.Append("         [Days] ,");
            sql.Append("         [BridgePrice] ,");
            sql.Append("         [DriverPrice] ,");
            sql.Append("         [DriverRoomPrice] ,");
            sql.Append("         [DriverDiningPrice] ,");
            sql.Append("         [EmptyDrivingPrice] ,");
            sql.Append("         [OtherPrice] ,");
            sql.Append("         [SumPrice] ,");
            sql.Append("         [Remark] ");
            sql.Append(" FROM    [dbo].[tbl_PlanCar]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanCar();
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.PriceType = (PlanCarPriceType)dr.GetByte(dr.GetOrdinal("PriceType"));
                    mdl.CarId = dr["CarId"].ToString();
                    mdl.Models = dr["Models"].ToString();
                    mdl.DriverPhone = dr["DriverPhone"].ToString();
                    mdl.CarNumber = dr["CarNumber"].ToString();
                    mdl.Driver = dr["Driver"].ToString();
                    mdl.Days = dr.GetInt32(dr.GetOrdinal("Days"));
                    mdl.CarPrice = dr.GetDecimal(dr.GetOrdinal("CarPrice"));
                    mdl.BridgePrice = dr.GetDecimal(dr.GetOrdinal("BridgePrice"));
                    mdl.DriverPrice = dr.GetDecimal(dr.GetOrdinal("DriverPrice"));
                    mdl.DriverRoomPrice = dr.GetDecimal(dr.GetOrdinal("DriverRoomPrice"));
                    mdl.DriverDiningPrice = dr.GetDecimal(dr.GetOrdinal("DriverDiningPrice"));
                    mdl.EmptyDrivingPrice = dr.GetDecimal(dr.GetOrdinal("EmptyDrivingPrice"));
                    mdl.OtherPrice = dr.GetDecimal(dr.GetOrdinal("OtherPrice"));
                    mdl.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    mdl.Remark = dr["Remark"].ToString();
                    lst.Add(mdl);
                    mdl = null;
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取用餐安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanDining> GetDining(string planId)
        {
            var lst = new List<MPlanDining>();
            var sql = new StringBuilder();

            sql.Append(" SELECT   [PlanId] ,");
            sql.Append("          [PriceType] ,");
            sql.Append("          [DiningType] ,");
            sql.Append("          [AdultNumber] ,");
            sql.Append("          [ChildNumber] ,");
            sql.Append("          [LeaderNumber] ,");
            sql.Append("          [GuideNumber] ,");
            sql.Append("          [DriverNumber] ,");
            sql.Append("          [AdultUnitPrice] ,");
            sql.Append("          [ChildPrice] ,");
            sql.Append("          [MenuId] ,");
            sql.Append("          [MenuName] ,");
            sql.Append("          [TableNumber] ,");
            sql.Append("          [FreeNumber] ,");
            sql.Append("          [FreePrice] ,");
            sql.Append("          [SumPrice]");
            sql.Append(" FROM     dbo.tbl_PlanDining");
            sql.Append(" WHERE   PlanId = @PlanId");

            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    lst.Add(new MPlanDining
                    {
                        PlanId = dr["PlanId"].ToString(),
                        PriceType = (PlanDiningPriceType)dr.GetByte(dr.GetOrdinal("PriceType")),
                        DiningType = (PlanDiningType)dr.GetByte(dr.GetOrdinal("DiningType")),
                        AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber")),
                        ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber")),
                        LeaderNumber = dr.GetInt32(dr.GetOrdinal("LeaderNumber")),
                        GuideNumber = dr.GetInt32(dr.GetOrdinal("GuideNumber")),
                        DriverNumber = dr.GetInt32(dr.GetOrdinal("DriverNumber")),
                        AdultUnitPrice = dr.GetDecimal(dr.GetOrdinal("AdultUnitPrice")),
                        ChildPrice = dr.GetDecimal(dr.GetOrdinal("ChildPrice")),
                        MenuId = dr["MenuId"].ToString(),
                        MenuName = dr["MenuName"].ToString(),
                        TableNumber = dr.GetInt32(dr.GetOrdinal("TableNumber")),
                        FreeNumber = dr.GetInt32(dr.GetOrdinal("FreeNumber")),
                        FreePrice = dr.GetDecimal(dr.GetOrdinal("FreePrice")),
                        SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"))
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取区间交通业安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanLargeFrequency> GetLargeFrequency(string planId)
        {
            var lst = new List<MPlanLargeFrequency>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [DepartureTime] ,");
            sql.Append("         [Time] ,");
            sql.Append("         [Departure] ,");
            sql.Append("         [Destination] ,");
            sql.Append("         [Numbers] ,");
            sql.Append("         [SeatStandard] ,");
            sql.Append("         [SeatType] ,");
            sql.Append("         [AdultsType] ,");
            sql.Append("         [PepolePayNum], ");
            sql.Append("         [FarePrice] ,");
            sql.Append("         [FreeNumber] ,");
            sql.Append("         [InsuranceHandlFee] ,");
            sql.Append("         [Fee] ,");
            sql.Append("         [Surcharge] ,");
            sql.Append("         [Discount] ,");
            sql.Append("         [SumPrice] ");
            sql.Append(" FROM    [dbo].[tbl_PlanLargeFrequency]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlanLargeFrequency();
                    mdl.PlanId = dr["PlanId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("DepartureTime")))
                        mdl.DepartureTime = dr.GetDateTime(dr.GetOrdinal("DepartureTime"));
                    mdl.Time = dr["Time"].ToString();
                    mdl.Departure = dr["Departure"].ToString();
                    mdl.Destination = dr["Destination"].ToString();
                    mdl.Numbers = dr["Numbers"].ToString();
                    mdl.SeatStandard = dr["SeatStandard"].ToString();
                    mdl.SeatType = (PlanLargeSeatType)dr.GetByte(dr.GetOrdinal("SeatType"));
                    mdl.AdultsType = (PlanLargeAdultsType)dr.GetByte(dr.GetOrdinal("AdultsType"));
                    mdl.PepolePayNum = dr.GetInt32(dr.GetOrdinal("PepolePayNum"));
                    mdl.FarePrice = dr.GetDecimal(dr.GetOrdinal("FarePrice"));
                    mdl.FreeNumber = dr.GetInt32(dr.GetOrdinal("FreeNumber"));
                    mdl.InsuranceHandlFee = dr.GetDecimal(dr.GetOrdinal("InsuranceHandlFee"));
                    mdl.Fee = dr.GetDecimal(dr.GetOrdinal("Fee"));
                    mdl.Surcharge = dr.GetDecimal(dr.GetOrdinal("Surcharge"));
                    mdl.Discount = float.Parse(dr["Discount"].ToString());
                    mdl.SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice"));
                    lst.Add(mdl);
                    mdl = null;
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取导游安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanGuide GetGuide(string planId)
        {
            var mdl = new MPlanGuide();
            var sql = new StringBuilder();

            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [Gender] ,");
            sql.Append("         [OnLocation] ,");
            sql.Append("         [NextLocation] ,");
            sql.Append("         [TaskType]");
            sql.Append(" FROM    [dbo].[tbl_PlanGuide]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.Gender = (Gender)dr.GetByte(dr.GetOrdinal("Gender"));
                    mdl.OnLocation = dr["OnLocation"].ToString();
                    mdl.NextLocation = dr["NextLocation"].ToString();
                    mdl.TaskType = (PlanGuideTaskType)dr.GetByte(dr.GetOrdinal("TaskType"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取领料安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MGovGoodUse GetGood(string planId)
        {
            var mdl = new MGovGoodUse();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [UseId] ,");
            sql.Append("         [GoodId] ,");
            sql.Append("         [CompanyId] ,");
            sql.Append("         [UseType] ,");
            sql.Append("         [UseTime] ,");
            sql.Append("         [DeptId] ,");
            sql.Append("         [Number] ,");
            sql.Append("         [UserId] ,");
            sql.Append("         [Price] ,");
            sql.Append("         [GoodUse] ,");
            sql.Append("         [OperatorId] ,");
            sql.Append("         [IssueTime] ,");
            sql.Append("         [ReturnTime] ,");
            sql.Append("         [PlanId]");
            sql.Append(" FROM    [dbo].[tbl_GovGoodUse]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.UseId = dr["UseId"].ToString();
                    mdl.GoodId = dr["GoodId"].ToString();
                    mdl.CompanyId = dr["CompanyId"].ToString();
                    mdl.Type = (GoodUseType)dr.GetByte(dr.GetOrdinal("UseType"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UseTime")))
                    {
                        mdl.Time = dr.GetDateTime(dr.GetOrdinal("UseTime"));
                    }
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("DeptId"));
                    mdl.Number = dr.GetInt32(dr.GetOrdinal("Number"));
                    mdl.UserId = dr["UserId"].ToString();
                    mdl.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                    mdl.Use = dr["GoodUse"].ToString();
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ReturnTime")))
                    {
                        mdl.ReturnTime = dr.GetDateTime(dr.GetOrdinal("ReturnTime"));
                    }
                    mdl.PlanId = dr["PlanId"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号和支付方式获取统计数
        /// </summary>
        /// <param name="payment">支付方式</param>
        /// <param name="tourId">计调编号</param>
        /// <returns></returns>
        public int GetPlanCount(Payment payment, string tourId)
        {
            var sql = new StringBuilder("select count(PlanId) from tbl_plan where tourid=@tourid and PaymentType=@payment and IsDelete='0'");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@tourid", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@payment", DbType.Byte, (int)payment);
            return (int)DbHelper.GetSingle(dc, this.db);
        }

        /// <summary>
        /// 根据团队编号和支付方式获取计调项结算金额
        /// </summary>
        /// <param name="payment">支付方式</param>
        /// <param name="tourId">计调编号</param>
        /// <returns>计调项结算金额</returns>
        public decimal GetPlanCost(Payment payment, string tourId)
        {
            var sql = new StringBuilder("select isnull(sum(Confirmation),0) from tbl_plan where tourid=@tourid and PaymentType=@payment and IsDelete='0'");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@tourid", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@payment", DbType.Byte, (int)payment);
            return (decimal)DbHelper.GetSingle(dc, this.db);
        }

        /// <summary>
        /// 获取某个计调安排项目的列表
        /// </summary>
        /// <param name="planType">计调类型</param>
        /// <param name="payment">支付方式</param>
        /// <param name="addStatus">添加状态</param>
        /// <param name="isShowCostChange">是否显示计调变更</param>
        /// <param name="changeType">计调变更类别</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public IList<MPlan> GetList(PlanProject planType, IList<Payment> payment, PlanAddStatus? addStatus, bool isShowCostChange, PlanChangeChangeClass? changeType, string tourId)
        {
            var lst = new List<MPlan>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  P.PlanId ,");
            sql.Append("         p.GuideNotes,p.Remarks, ");
            sql.Append("        SourceId ,");
            sql.Append("         SourceName ,");
            sql.Append("         P.ContactName Contact,");
            sql.Append("         ContactPhone ,ContactMobile ,ContactFax,");
            sql.Append("         StartDate ,");
            sql.Append("         EndDate ,");
            sql.Append("         StartTime ,");
            sql.Append("         EndTime ,");
            if (planType == PlanProject.地接)
            {
                //地接房型
                sql.Append("         XMLHotelRoom = ( SELECT    F.RoomType,ISNULL(F.Quantity,0) Quantity ");
                sql.Append("                   FROM      dbo.tbl_PlanHotelRoom F");
                sql.Append("                   WHERE     F.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.酒店)
            {
                sql.Append("         ISNULL(H.FreeNumber,0) FreeNumber ,");
                //酒店房型
                sql.Append("         XMLHotelRoom = ( SELECT    F.RoomType,ISNULL(F.UnitPrice,0) UnitPrice,F.PriceType,ISNULL(F.Quantity,0) Quantity,ISNULL(F.Total,0) Total ");
                sql.Append("                   FROM      dbo.tbl_PlanHotelRoom F");
                sql.Append("                   WHERE     F.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.用车)
            {
                //sql.Append("         ISNULL(YC.PriceType,0) PriceType ,");
                sql.Append("         ISNULL((SELECT top 1 PriceType from dbo.tbl_PlanCar WHERE P.PlanId = tbl_PlanCar.PlanId),0) PriceType ,");

                //用车
                sql.Append("         XMLCar = ( SELECT    C.Models,C.CarNumber,C.Driver,C.DriverPhone,ISNULL(C.CarPrice,0) CarPrice,ISNULL(C.Days,0) Days,ISNULL(C.SumPrice,0) SumPrice ");
                sql.Append("                   FROM      dbo.tbl_PlanCar C");
                sql.Append("                   WHERE     C.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.火车 || planType == PlanProject.汽车 || planType == PlanProject.飞机 || planType == PlanProject.轮船)
            {
                sql.Append("         XMLLargeFrequency = ( SELECT L.Numbers,ISNULL(L.PepolePayNum,0) PepolePayNum,ISNULL(L.SumPrice,0) SumPrice ");
                sql.Append("                   FROM      dbo.tbl_PlanLargeFrequency L");
                sql.Append("                   WHERE     L.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
                if (planType == PlanProject.火车)
                {
                    sql.Append("         XMLTrain = ( SELECT    ISNULL(SUM(L.FreeNumber),0)FreeNumber,ISNULL(SUM(L.PepolePayNum),0)PepolePayNum ");
                    sql.Append("                   FROM      dbo.tbl_PlanLargeFrequency L");
                    sql.Append("                   WHERE     L.PlanId = P.PlanId");
                    sql.Append("                   FOR");
                    sql.Append("                     XML RAW ,");
                    sql.Append("                         ROOT");
                    sql.Append("                 ) ,");
                }
            }
            if (planType == PlanProject.景点)
            {
                sql.Append("         XMLAttractions = ( SELECT    A.Attractions,ISNULL(A.AdultNumber,0) AdultNumber,ISNULL(A.ChildNumber,0) ChildNumber,ISNULL(A.AdultPrice,0) AdultPrice ");
                sql.Append("                   ,ISNULL(A.ChildPrice,0) ChildPrice,A.Seats,A.VisitTime,ISNULL(A.SumPrice,0) SumPrice  ");
                sql.Append("                   FROM      dbo.tbl_PlanAttractions A");
                sql.Append("                   WHERE     A.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.用餐)
            {
                sql.Append("         ISNULL((SELECT top 1 PriceType from dbo.tbl_PlanDining WHERE P.PlanId = tbl_PlanDining.PlanId),0) PriceType ,");
                //用餐
                sql.Append("         XMLDining = ( SELECT   ISNULL(SUM(D.FreeNumber),0) FreeNumber,ISNULL(SUM(D.FreePrice),0) FreePrice ");
                sql.Append("                   FROM      dbo.tbl_PlanDining D");
                sql.Append("                   WHERE     D.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
                //用餐明细
                sql.Append("         XMLDiningList = ( SELECT   D.MenuName,ISNULL(D.AdultNumber,0) AdultNumber,ISNULL(D.ChildNumber,0) ChildNumber,ISNULL(D.TableNumber,0) TableNumber,ISNULL(D.SumPrice,0) SumPrice ");
                sql.Append("                   FROM      dbo.tbl_PlanDining D");
                sql.Append("                   WHERE     D.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.导游)
            {
                sql.Append("         D.OnLocation ,D.NextLocation,");
            }
            if (isShowCostChange)
            {
                sql.Append(" XMLCostChange = ( SELECT    O.PlanId ,");
                sql.Append("                             O.ChangeType ,");
                sql.Append("                             O.Type ,");
                sql.Append("                             O.PeopleNumber ,");
                sql.Append("                             O.ChangeCost ,");
                sql.Append("                             O.Remark ,");
                sql.Append("                             CONVERT(VARCHAR(19), O.IssueTime, 120) IssueTime");
                sql.Append("                   FROM      dbo.tbl_PlanCostChange O");
                sql.Append("                   WHERE     O.PlanId = P.PlanId");
                if (changeType.HasValue)
                {
                    sql.Append("                             AND O.ChangeType = @ChangeType");
                }
                sql.Append("                 FOR");
                sql.Append("                   XML RAW ,");
                sql.Append("                       ROOT");
                sql.Append("                 ),");
            }
            sql.Append("         Num ,Prepaid,");
            sql.Append("         AdultNumber ,ChildNumber ,");
            sql.Append("         PaymentType ,SigningCount,");
            sql.Append("         Status ,");
            sql.Append("         PlanCost ,");
            sql.Append("         Confirmation ,");
            sql.Append("         AddStatus ,");
            sql.Append("         ServiceStandard,");
            sql.Append("         P.OperatorId,");
            sql.Append("         P.TourId,P.IdentityId");
            sql.Append(" FROM    dbo.tbl_Plan P");
            if (planType == PlanProject.酒店)
            {
                sql.Append("         LEFT JOIN dbo.tbl_PlanHotel H ON P.PlanId = H.PlanId");
            }
            if (planType == PlanProject.导游)
            {
                sql.Append("         LEFT JOIN dbo.tbl_PlanGuide D ON P.PlanId = D.PlanId");
            }
            sql.Append(" WHERE     P.TourId = @TourId");
            sql.Append("         AND Type = @Type");
            //if (payment.HasValue)
            //{
            //    sql.Append("         AND PaymentType = @Payment");
            //}
            if (payment != null && payment.Count > 0)
            {
                if (payment.Count == 1 && ((int)payment[0]) > 0)
                {
                    sql.Append(" AND PaymentType =" + (int)payment[0] + " ");
                }
                else
                {
                    StringBuilder s = new StringBuilder();
                    for (int i = 0; i < payment.Count; i++)
                    {
                        if (((int)payment[i]) > 0)
                        {
                            s.Append("  PaymentType =" + (int)payment[i] + " or");
                        }
                    }
                    if (!string.IsNullOrEmpty(s.ToString()))
                    {
                        sql.Append(" AND (" + s.ToString().Substring(0, s.ToString().Length - 2) + ") ");
                    }
                }
            }
            if (addStatus.HasValue)
            {
                sql.Append("         AND AddStatus = @AddStatus");
            }
            sql.Append("         AND IsDelete = '0'");
            sql.Append(" ORDER BY StartDate");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@Type", DbType.Byte, (int)planType);
            //if (payment.HasValue)
            //{
            //    this.db.AddInParameter(dc, "@Payment", DbType.Byte, (int)payment.Value);
            //}
            if (addStatus.HasValue)
            {
                this.db.AddInParameter(dc, "@AddStatus", DbType.Byte, (int)addStatus.Value);
            }
            if (isShowCostChange && changeType.HasValue)
            {
                this.db.AddInParameter(dc, "@ChangeType", DbType.Byte, (int)changeType.Value);
            }

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var mdl = new MPlan
                    {
                        TourId = dr["TourId"].ToString(),
                        PlanId = dr["PlanId"].ToString(),
                        GuideNotes = dr["GuideNotes"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        SourceId = dr["SourceId"].ToString(),
                        SourceName = dr["SourceName"].ToString(),
                        PlanCost = dr.GetDecimal(dr.GetOrdinal("PlanCost")),
                        Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                        PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType")),
                        Status = (PlanState)dr.GetByte(dr.GetOrdinal("Status")),
                        StartTime = dr["StartTime"].ToString(),
                        EndTime = dr["EndTime"].ToString(),
                        ContactName = dr["Contact"].ToString(),
                        ContactPhone = dr["ContactPhone"].ToString(),
                        ContactMobile = dr["ContactMobile"].ToString(),
                        ContactFax = dr["ContactFax"].ToString(),
                        ServiceStandard = dr["ServiceStandard"].ToString(),
                        Num = dr.GetInt32(dr.GetOrdinal("Num")),
                        Prepaid = dr.GetDecimal(dr.GetOrdinal("Prepaid")),
                        AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber")),
                        ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber")),
                        SigningCount = dr.GetInt32(dr.GetOrdinal("SigningCount")),
                        AddStatus = (PlanAddStatus)dr.GetByte(dr.GetOrdinal("AddStatus")),
                        OperatorId = dr["OperatorId"].ToString(),
                        IdentityId = dr.GetInt32(dr.GetOrdinal("IdentityId"))
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("StartDate")))
                    {
                        mdl.StartDate = dr.GetDateTime(dr.GetOrdinal("StartDate"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EndDate")))
                    {
                        mdl.EndDate = dr.GetDateTime(dr.GetOrdinal("EndDate"));
                    }
                    if (isShowCostChange)
                    {
                        mdl.PlanCostChange = this.GetPlanCostChangeLst(dr["XMLCostChange"].ToString());
                    }

                    switch (planType)
                    {
                        case PlanProject.地接:
                            mdl.PlanHotelRoomList = this.GetPlanHotelRoomLst(dr["XMLHotelRoom"].ToString());
                            break;
                        case PlanProject.酒店:
                            mdl.FreeNumber = dr.GetDecimal(dr.GetOrdinal("FreeNumber"));
                            mdl.PlanHotelRoomList = this.GetPlanHotelRoomLst(dr["XMLHotelRoom"].ToString());
                            break;
                        case PlanProject.用车:
                            mdl.PriceType = dr.GetByte(dr.GetOrdinal("PriceType"));
                            mdl.PlanCarList = this.GetPlanCarMdl(dr["XMLCar"].ToString());
                            break;
                        case PlanProject.飞机:
                        case PlanProject.火车:
                        case PlanProject.汽车:
                        case PlanProject.轮船:
                            if (planType == PlanProject.火车)
                            {
                                mdl.FreeNumber = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLTrain"].ToString(), "FreeNumber"));
                                mdl.PepolePayNum = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLTrain"].ToString(), "PepolePayNum"));
                            }
                            mdl.PlanLargeFrequencyList = this.GetLargeFrequencyLst(dr["XMLLargeFrequency"].ToString());
                            break;
                        case PlanProject.景点:
                            mdl.PlanAttractionsList = this.GetPlanAttractionsLst(dr["XMLAttractions"].ToString());
                            break;
                        case PlanProject.用餐:
                            mdl.PriceType = dr.GetByte(dr.GetOrdinal("PriceType"));
                            mdl.FreeNumber = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLDining"].ToString(), "FreeNumber"));
                            mdl.FreePrice = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLDining"].ToString(), "FreePrice"));
                            mdl.PlanDiningList = this.GetPlanDiningLst(dr["XMLDiningList"].ToString());
                            break;
                        //case PlanProject.领料:
                        //    if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                        //    {
                        //        mdl.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                        //    }
                        //    mdl.ContactName = dr["ContactName"].ToString();
                        //    mdl.PlanGood = new MGovGoodUse { UserId = dr["UserId"].ToString(), GoodId = dr["GoodId"].ToString() };
                        //    break;
                        case PlanProject.导游:
                            mdl.PlanGuide = new MPlanGuide { OnLocation = dr["OnLocation"].ToString(), NextLocation = dr["NextLocation"].ToString() };
                            break;
                    }
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取导游任务打印单的接待行程、服务标准、导游须知
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public MPlanBaseInfo GetGuidePrint(string tourId)
        {
            var mdl = new MPlanBaseInfo();
            var sql = new StringBuilder();

            sql.Append(" SELECT ");
            sql.Append(" (SELECT ReceiveJourney AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status AND Type=@Type FOR XML RAW,ROOT('root') ) N'接待行程' ");
            sql.Append(" ,(SELECT ServiceStandard AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status AND Type=@Type FOR XML RAW,ROOT('root') ) N'服务标准' ");
            sql.Append(" ,(SELECT GuideNotes AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status FOR XML RAW,ROOT('root') ) N'导游须知' ");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@Type", DbType.Byte, (int)PlanProject.导游);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)PlanState.已落实);
            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.ReceiveJourney = dr["接待行程"].ToString();
                    mdl.ServiceStandard = dr["服务标准"].ToString();
                    mdl.GuideNotes = dr["导游须知"].ToString();
                }
            }

            if (mdl != null)
            {
                string xml = mdl.GuideNotes;
                if (!string.IsNullOrEmpty(xml))
                {
                    mdl.GuideNotes = string.Empty;
                    var xroot = XElement.Parse(xml);
                    var xrows = Utils.GetXElements(xroot, "row");
                    foreach (var xrow in xrows)
                    {
                        if (!string.IsNullOrEmpty(mdl.GuideNotes)) mdl.GuideNotes = mdl.GuideNotes + "\n";
                        mdl.GuideNotes += Utils.GetXAttributeValue(xrow, "C");
                    }
                }

                xml = mdl.ReceiveJourney;
                if (!string.IsNullOrEmpty(xml))
                {
                    mdl.ReceiveJourney = string.Empty;
                    var xroot = XElement.Parse(xml);
                    var xrows = Utils.GetXElements(xroot, "row");
                    foreach (var xrow in xrows)
                    {
                        if (!string.IsNullOrEmpty(mdl.ReceiveJourney)) mdl.ReceiveJourney = mdl.ReceiveJourney + "\n";
                        mdl.ReceiveJourney += Utils.GetXAttributeValue(xrow, "C");
                    }
                }

                xml = mdl.ServiceStandard;
                if (!string.IsNullOrEmpty(xml))
                {
                    mdl.ServiceStandard = string.Empty;
                    var xroot = XElement.Parse(xml);
                    var xrows = Utils.GetXElements(xroot, "row");
                    foreach (var xrow in xrows)
                    {
                        if (!string.IsNullOrEmpty(mdl.ServiceStandard)) mdl.ServiceStandard = mdl.ServiceStandard + "\n";
                        mdl.ServiceStandard += Utils.GetXAttributeValue(xrow, "C");
                    }
                }

            }

            return mdl;
        }

        /// <summary>
        /// 根据计调编号、增减类型、计调变更类别获取计调变更实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="typ">增减类型(1增,0减)</param>
        /// <param name="chg">计调变更类别</param>
        /// <returns>计调变更实体</returns>
        public MPlanCostChange GetPlanCostChanges(string planId, bool typ, PlanChangeChangeClass chg)
        {
            var sql = new StringBuilder();
            var mdl = new MPlanCostChange();

            sql.Append(" SELECT    PlanId ,");
            sql.Append("           PeopleNumber ,");
            sql.Append("           ChangeCost ,");
            sql.Append("           Remark ");
            sql.Append(" FROM      dbo.tbl_PlanCostChange");
            sql.Append(" WHERE     PlanId = @PlanId");
            sql.Append("          AND ChangeType = @ChangeType");
            sql.Append("          AND Type = @Type");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            this.db.AddInParameter(dc, "@ChangeType", DbType.Byte, (int)chg);
            this.db.AddInParameter(dc, "@Type", DbType.AnsiStringFixedLength, typ ? "1" : "0");

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.PeopleNumber = dr.GetInt32(dr.GetOrdinal("PeopleNumber"));
                    mdl.ChangeCost = dr.GetDecimal(dr.GetOrdinal("ChangeCost"));
                    mdl.Remark = dr["Remark"].ToString();
                }
            }

            return mdl;
        }

        /// <summary>
        /// 获取团队或计调用房数
        /// </summary>
        /// <param name="type">1：计调编号，0或其他团队编号</param>
        /// <param name="Id">团队编号或计调编号</param>
        /// <returns></returns>
        public IList<MPlanHotelRoom> GetRoomList(string type, string Id, string companyId)
        {
            var sql = new StringBuilder();
            IList<MPlanHotelRoom> list = new List<MPlanHotelRoom>();
            sql.Append(" select rt.RoomId,rt.TypeName,isnull(tp.Quantity,0) Quantity ");
            if (type.Trim() == "1")
            {
                sql.Append(" ,isnull(tp.UnitPrice,0) UnitPrice,isnull(tp.PriceType,1) PriceType,isnull(tp.Total,0) Total ");
            }
            sql.Append(" from tbl_ComRoomTypeManage rt LEFT JOIN ");
            if (type.Trim() == "1")
            {
                sql.Append(" (select RoomId,Quantity,UnitPrice,PriceType,Total from  tbl_PlanHotelRoom where PlanId=@Id) tp ");
            }
            else
            {
                sql.Append(" (select RoomId,Num as Quantity from  tbl_TourRoom where TourId=@Id) tp ");
            }
            sql.Append(" ON rt.RoomId=tp.RoomId where companyid=@companyid ORDER BY tp.Quantity DESC");
            DbCommand cmd = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, Id);
            this.db.AddInParameter(cmd, "companyid", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this.db))
            {
                while (dr.Read())
                {
                    MPlanHotelRoom model = new MPlanHotelRoom();
                    model.RoomId = dr.GetString(dr.GetOrdinal("RoomId"));
                    model.Quantity = dr.GetInt32(dr.GetOrdinal("Quantity"));
                    model.RoomType = dr.GetString(dr.GetOrdinal("TypeName"));
                    if (type.Trim() == "1")
                    {
                        model.UnitPrice = dr.GetDecimal(dr.GetOrdinal("UnitPrice"));
                        model.PriceType = (PlanHotelPriceType)dr.GetByte(dr.GetOrdinal("PriceType"));
                        model.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    }
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion

        #region 报销报账（导游、销售、计调、财务）
        /// <summary>
        /// 报销报账时获取有导游现收的列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>有导游现收的列表</returns>
        public IList<MGuideIncome> GetGuideIncome(string tourId)
        {
            var lst = new List<MGuideIncome>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  OrderId ,");
            sql.Append("         OrderCode ,");
            sql.Append("         BuyCompanyName ,");
            sql.Append("         GuideIncome ,");
            sql.Append("         GuideRealIncome ,");
            sql.Append("         GuideRemark");
            sql.Append(" FROM    dbo.tbl_TourOrder");
            sql.Append(" WHERE   TourId = @TourId AND (GuideIncome>0 OR GuideRealIncome>0) AND IsDelete='0'");
            sql.Append(" ORDER BY    GuideIncome DESC,GuideRealIncome DESC");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    lst.Add(new MGuideIncome
                    {
                        OrderId = dr["OrderId"].ToString(),
                        OrderCode = dr["OrderCode"].ToString(),
                        BuyCompanyName = dr["BuyCompanyName"].ToString(),
                        GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome")),
                        GuideRealIncome = dr.GetDecimal(dr.GetOrdinal("GuideRealIncome")),
                        GuideRemark = dr["GuideRemark"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 报销报账-导游收入选用订单时获取没有导游现收的订单列表
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>没有导游现收的订单列表</returns>
        public IList<MGuideIncome> GetGuideIncome(int pageSize
                                                , int pageIndex
                                                , ref int recordCount
                                                , string tourId)
        {
            var lst = new List<MGuideIncome>();
            using (var dr = DbHelper.ExecuteReader(this.db, pageSize, pageIndex, ref recordCount
                , "tbl_TourOrder", "OrderId", "OrderId,OrderCode,BuyCompanyId,BuyCompanyName,Adults+Childs+Others PeopleNum,SumPrice"
                , string.Format("TourId = '{0}' AND GuideIncome<=0 AND IsDelete='0'", tourId), "IssueTime DESC"))
            {
                while (dr.Read())
                {
                    lst.Add(new MGuideIncome
                    {
                        OrderId = dr["OrderId"].ToString(),
                        OrderCode = dr["OrderCode"].ToString(),
                        BuyCompanyId = dr["BuyCompanyId"].ToString(),
                        BuyCompanyName = dr["BuyCompanyName"].ToString(),
                        PeopleNum = dr.GetInt32(dr.GetOrdinal("PeopleNum")),
                        SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice")),
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 添加/修改导游实收
        /// </summary>
        /// <param name="mdl">导游收入实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdGuideReal(MGuideIncome mdl)
        {
            var sql = new StringBuilder();
            sql.Append(" UPDATE  dbo.tbl_TourOrder");
            sql.Append(" SET     GuideRealIncome = @GuideRealIncome ,");
            sql.Append("         GuideRemark = @GuideRemark");
            sql.Append(" WHERE   OrderId = @OrderId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@OrderId", DbType.AnsiStringFixedLength, mdl.OrderId);
            this.db.AddInParameter(dc, "@GuideRealIncome", DbType.Decimal, mdl.GuideRealIncome);
            this.db.AddInParameter(dc, "@GuideRemark", DbType.String, mdl.GuideRemark);
            return DbHelper.ExecuteSql(dc, this.db) > 0;
        }

        /// <summary>
        /// 报销报账时根据团队编号获取该团其他收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他收入列表</returns>
        public IList<MOtherFeeInOut> GetOtherIncome(string tourId)
        {
            var lst = new List<MOtherFeeInOut>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  Id ,");
            sql.Append("         FeeItem ,");
            sql.Append("         CrmId ,");
            sql.Append("         Crm ,");
            sql.Append("         FeeAmount ,");
            sql.Append("         Remark ,");
            sql.Append("         Status ,");
            sql.Append("         PayType ,");
            sql.Append("         PayTypeName ,");
            sql.Append("         OperatorId,IsGuide");
            sql.Append(" FROM    dbo.tbl_FinOtherInFee");
            sql.Append(" WHERE   TourId = @TourId");
            sql.Append("         AND IsDeleted = '0'");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    lst.Add(new MOtherFeeInOut
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("Id")),
                        FeeItem = dr["FeeItem"].ToString(),
                        CrmId = dr["CrmId"].ToString(),
                        Crm = dr["Crm"].ToString(),
                        FeeAmount = dr.GetDecimal(dr.GetOrdinal("FeeAmount")),
                        Status = (FinStatus)dr.GetByte(dr.GetOrdinal("Status")),
                        Remark = dr["Remark"].ToString(),
                        PayType = dr.GetInt32(dr.GetOrdinal("PayType")),
                        PayTypeName = dr["PayTypeName"].ToString(),
                        OperatorId = dr["OperatorId"].ToString(),
                        IsGuide = dr["IsGuide"].ToString() == "1",
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取报账汇总
        /// </summary>
        /// <param name="tourid"></param>
        /// <returns></returns>
        public MBZHZ GetBZHZ(string tourid)
        {
            var mdl = new MBZHZ();
            var sql = new StringBuilder();
            sql.AppendFormat(" select GuideIncome=(select isnull(sum(GuideRealIncome),0) from tbl_TourOrder where tourid=@tourid and isdelete='0' and status={0})", (int)OrderStatus.已成交);
            sql.AppendFormat(" +(select isnull(sum(FeeAmount),0) from tbl_FinOtherInFee where tourid=@tourid and IsDeleted='0' and ISNULL(PayTypeName,(SELECT Name FROM tbl_ComPayment WHERE PaymentId=PayType))='导游现收')", (int)FinStatus.账务待支付);
            sql.AppendFormat(" ,XMLGuide=(select isnull(sum(RealAmount),0) RealAmount,isnull(sum(RelSignNum),0) RelSignNum from tbl_FinDebit where tourid=@tourid and IsDeleted='0' and status={0} for xml raw,root)", (int)FinStatus.账务已支付);
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@tourid", DbType.AnsiStringFixedLength, tourid);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                    mdl.GuideBorrow = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLGuide"].ToString(), "RealAmount"));
                    mdl.GuideOutlay = this.GetPlanCost(Payment.现付, tourid);
                    mdl.GuideRelSign = Utils.GetInt(Utils.GetValueFromXmlByAttribute(dr["XMLGuide"].ToString(), "RelSignNum"));
                    mdl.GuideUsed = this.GetPlanCount(Payment.签单, tourid);
                }
            }
            return mdl;
        }

        /// <summary>
        /// 根据团队编号获取团队收支表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public MTourTotalInOut GetTourTotalInOut(string tourId)
        {
            var mdl = new MTourTotalInOut();
            var sql = new StringBuilder();

            sql.Append(" SELECT  TourIncome = ( SELECT   ISNULL(SUM(ConfirmSettlementMoney), 0)");
            sql.Append("                        FROM     dbo.tbl_TourOrder");
            sql.Append("                        WHERE    TourId = dbo.tbl_Tour.TourId");
            sql.Append("                                 AND Status = @OrderStatus");
            sql.Append("                                 AND IsDelete = 0");
            sql.Append("                      ) ,");
            sql.Append("         TourOutlay = ( SELECT   ISNULL(SUM(Confirmation), 0)");
            sql.Append("                        FROM     dbo.tbl_Plan");
            sql.Append("                        WHERE    TourId = dbo.tbl_Tour.TourId");
            sql.Append("                                 AND Status = @PlanStatus");
            sql.Append("                                 AND IsDelete = 0");
            sql.Append("                      )");
            sql.Append(" FROM    dbo.tbl_Tour");
            sql.Append(" WHERE   TourId = @TourId");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@OrderStatus", DbType.Byte, (int)OrderStatus.已成交);
            this.db.AddInParameter(dc, "@PlanStatus", DbType.Byte, (int)PlanState.已落实);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.TourIncome = dr.GetDecimal(dr.GetOrdinal("TourIncome"));
                    mdl.TourOutlay = dr.GetDecimal(dr.GetOrdinal("TourOutlay"));
                    if (mdl.TourIncome != 0)
                    {
                        mdl.TourProRate = (float)(mdl.TourProfit / mdl.TourIncome);
                    }
                }
            }

            return mdl;
        }

        /// <summary>
        /// 购物收入添加/修改
        /// </summary>
        /// <param name="m">购物收入实体</param>
        /// <returns>0成功 1失败 2已提交财务</returns>
        public int AddOrUpdGouWuShouRu(MGouWuShouRu m)
        {
            var dc = this.db.GetStoredProcCommand("proc_AddOrUpdGouWuShouRu");

            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, m.PlanId);
            this.db.AddInParameter(dc, "@LiuShui", DbType.Double, m.LiuShui);
            this.db.AddInParameter(dc, "@BaoDi", DbType.Currency, m.BaoDi);
            this.db.AddInParameter(dc, "@YingYe", DbType.Currency, m.YingYe);
            this.db.AddInParameter(dc, "@PeopleMoney", DbType.Currency, m.PeopleMoney);
            this.db.AddInParameter(dc, "@ChildMoney", DbType.Currency, m.ChildMoney);
            this.db.AddInParameter(dc, "@Adult", DbType.Int32, m.Adult);
            this.db.AddInParameter(dc, "@Child", DbType.Int32, m.Child);
            this.db.AddInParameter(dc, "@ToCompanyRenTou", DbType.Currency, m.ToCompanyRenTou);
            this.db.AddInParameter(dc, "@ToCompanyBaoDi", DbType.Currency, m.ToCompanyBaoDi);
            this.db.AddInParameter(dc, "@ToCompanyRenShu", DbType.Int32, m.ToCompanyRenShu);
            this.db.AddInParameter(dc, "@ToCompanyBaoDi2", DbType.Currency, m.ToCompanyBaoDi2);
            this.db.AddInParameter(dc, "@ToCompanyRenShu2", DbType.Int32, m.ToCompanyRenShu2);
            this.db.AddInParameter(dc, "@ToCompanyFanDian", DbType.Currency, m.ToCompanyFanDian);
            this.db.AddInParameter(dc, "@ToCompanyYingYe", DbType.Currency, m.ToCompanyYingYe);
            this.db.AddInParameter(dc, "@ToCompanyTiQu", DbType.Double, m.ToCompanyTiQu);
            this.db.AddInParameter(dc, "@ToCompanyTotal", DbType.Currency, m.ToCompanyTotal);
            this.db.AddInParameter(dc, "@ToGuideYingYe", DbType.Currency, m.ToGuideYingYe);
            this.db.AddInParameter(dc, "@ToGuideTiQu", DbType.Double, m.ToGuideTiQu);
            this.db.AddInParameter(dc, "@ToGuideLu", DbType.Currency, m.ToGuideLu);
            this.db.AddInParameter(dc, "@ToGuideShui", DbType.Currency, m.ToGuideShui);
            this.db.AddInParameter(dc, "@ToGuidePei", DbType.Currency, m.ToGuidePei);
            this.db.AddInParameter(dc, "@ToGuideJiao", DbType.Currency, m.ToGuideJiao);
            this.db.AddInParameter(dc, "@ToGuideOther", DbType.Currency, m.ToGuideOther);
            this.db.AddInParameter(dc, "@ToGuideLiuShui", DbType.Currency, m.ToGuideLiuShui);
            this.db.AddInParameter(dc, "@ToGuideTotal", DbType.Currency, m.ToGuideTotal);
            this.db.AddInParameter(dc, "@ToLeaderYingYe", DbType.Currency, m.ToLeaderYingYe);
            this.db.AddInParameter(dc, "@ToLeaderTiQu", DbType.Double, m.ToLeaderTiQu);
            this.db.AddInParameter(dc, "@OperatorDeptId", DbType.Int32, m.OperatorDeptId);
            this.db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, m.OperatorId);
            this.db.AddInParameter(dc, "@Operator", DbType.String, m.Operator);
            this.db.AddInParameter(dc, "@ToLeaderTotal", DbType.Currency, m.ToLeaderTotal);

            if (m.GouMaiChanPin != null && m.GouMaiChanPin.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLGouMaiChanPin", DbType.Xml, this.GetGouMaiChanPinXml(m.GouMaiChanPin));
            }

            this.db.AddOutParameter(dc, "@Result", DbType.Int32, 1);

            DbHelper.RunProcedure(dc, this.db);

            return Convert.ToInt32(db.GetParameterValue(dc, "Result"));
        }

        /// <summary>
        /// 根据计调编号获取购物收入实体
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <returns>购物收入实体</returns>
        public MGouWuShouRu GetGouWuShouRuMdl(string planId)
        {
            var m = new MGouWuShouRu();
            var sql = new StringBuilder();
            sql.Append(" SELECT  SourceName ,");
            sql.Append("         ContactName ,");
            sql.Append("         ContactPhone ,");
            sql.Append("         ContactFax ,");
            sql.Append("         StartDate ,");
            sql.Append("         GuideNotes ,");
            sql.Append("         Remarks ,");
            sql.Append("         ( SELECT TOP 1 *");
            sql.Append("           FROM      dbo.tbl_SourceShopHeTong");
            sql.Append("           WHERE     SourceId = dbo.tbl_Plan.SourceId");
            sql.Append("                     AND IsDisable = 1");
            sql.Append("           ORDER BY  ContactTime DESC");
            sql.Append("         FOR");
            sql.Append("           XML RAW ,");
            sql.Append("               ROOT");
            sql.Append("         ) XMLSource ,");
            sql.Append("         LiuShui ,");
            sql.Append("         BaoDi ,");
            sql.Append("         PeopleMoney ,");
            sql.Append("         ChildMoney ,");
            sql.Append("         YingYe ,");
            sql.Append("         Adult ,");
            sql.Append("         Child ,");
            sql.Append("         ToCompanyRenTou ,");
            sql.Append("         ToCompanyBaoDi ,");
            sql.Append("         ToCompanyRenShu ,");
            sql.Append("         ToCompanyBaoDi2 ,");
            sql.Append("         ToCompanyRenShu2 ,");
            sql.Append("         ToCompanyFanDian ,");
            sql.Append("         ToCompanyYingYe ,");
            sql.Append("         ToCompanyTiQu ,");
            sql.Append("         ToCompanyTotal ,");
            sql.Append("         ToGuideYingYe ,");
            sql.Append("         ToGuideTiQu ,");
            sql.Append("         ToGuideLu ,");
            sql.Append("         ToGuideShui ,");
            sql.Append("         ToGuidePei ,");
            sql.Append("         ToGuideJiao ,");
            sql.Append("         ToGuideOther ,");
            sql.Append("         ToGuideLiuShui ,");
            sql.Append("         ToGuideTotal ,");
            sql.Append("         ToLeaderYingYe ,");
            sql.Append("         ToLeaderTiQu ,");
            sql.Append("         ToLeaderTotal,");
            sql.Append("         (SELECT * FROM dbo.tbl_PlanShopProduct WHERE PlanId=dbo.tbl_Plan.PlanId FOR XML RAW,ROOT) XMLChanPin");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append("         LEFT OUTER JOIN dbo.tbl_PlanShop ON dbo.tbl_Plan.PlanId = dbo.tbl_PlanShop.PlanId");
            sql.Append(" WHERE   dbo.tbl_Plan.PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    m.SourceName = dr["SourceName"].ToString();
                    m.ContactName = dr["ContactName"].ToString();
                    m.ContactPhone = dr["ContactPhone"].ToString();
                    m.ContactFax = dr["ContactFax"].ToString();
                    m.StartDate = Utils.GetDateTimeNullable(dr["StartDate"].ToString());
                    m.GuideNotes = dr["GuideNotes"].ToString();
                    m.Remarks = dr["Remarks"].ToString();
                    m.Country = Utils.GetValueFromXmlByAttribute(dr["XMLSource"].ToString(), "Country");
                    m.LiuShui = dr.IsDBNull(dr.GetOrdinal("LiuShui")) ? Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLSource"].ToString(), "LiuShui")) : Utils.GetDecimal(dr["LiuShui"].ToString());
                    m.BaoDi = dr.IsDBNull(dr.GetOrdinal("BaoDi")) ? Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLSource"].ToString(), "BaoDi")) : Utils.GetDecimal(dr["BaoDi"].ToString());
                    m.YingYe = Utils.GetDecimal(dr["YingYe"].ToString());
                    m.PeopleMoney = dr.IsDBNull(dr.GetOrdinal("PeopleMoney")) ? Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLSource"].ToString(), "PeopleMoney")) : Utils.GetDecimal(dr["PeopleMoney"].ToString());
                    m.ChildMoney = dr.IsDBNull(dr.GetOrdinal("ChildMoney")) ? Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLSource"].ToString(), "ChildMoney")) : Utils.GetDecimal(dr["ChildMoney"].ToString());
                    m.Adult = Utils.GetInt(dr["Adult"].ToString());
                    m.Child = Utils.GetInt(dr["Child"].ToString());
                    m.GouMaiChanPin = GetGouMaiChanPinLst(dr["XMLChanPin"].ToString());
                    m.ToCompanyRenTou = Utils.GetDecimal(dr["ToCompanyRenTou"].ToString());
                    m.ToCompanyBaoDi = Utils.GetDecimal(dr["ToCompanyBaoDi"].ToString());
                    m.ToCompanyRenShu = Utils.GetInt(dr["ToCompanyRenShu"].ToString());
                    m.ToCompanyBaoDi2 = Utils.GetDecimal(dr["ToCompanyBaoDi2"].ToString());
                    m.ToCompanyRenShu2 = Utils.GetInt(dr["ToCompanyRenShu2"].ToString());
                    m.ToCompanyFanDian = Utils.GetDecimal(dr["ToCompanyFanDian"].ToString());
                    m.ToCompanyYingYe = Utils.GetDecimal(dr["ToCompanyYingYe"].ToString());
                    m.ToCompanyTiQu = Utils.GetDouble(dr["ToCompanyTiQu"].ToString());
                    m.ToCompanyTotal = Utils.GetDecimal(dr["ToCompanyTotal"].ToString());
                    m.ToGuideYingYe = Utils.GetDecimal(dr["ToGuideYingYe"].ToString());
                    m.ToGuideTiQu = Utils.GetDouble(dr["ToGuideTiQu"].ToString());
                    m.ToGuideLu = Utils.GetDecimal(dr["ToGuideLu"].ToString());
                    m.ToGuideShui = Utils.GetDecimal(dr["ToGuideShui"].ToString());
                    m.ToGuidePei = Utils.GetDecimal(dr["ToGuidePei"].ToString());
                    m.ToGuideJiao = Utils.GetDecimal(dr["ToGuideJiao"].ToString());
                    m.ToGuideOther = Utils.GetDecimal(dr["ToGuideOther"].ToString());
                    m.ToGuideLiuShui = Utils.GetDecimal(dr["ToGuideLiuShui"].ToString());
                    m.ToGuideTotal = Utils.GetDecimal(dr["ToGuideTotal"].ToString());
                    m.ToLeaderYingYe = Utils.GetDecimal(dr["ToLeaderYingYe"].ToString());
                    m.ToLeaderTiQu = Utils.GetDouble(dr["ToLeaderTiQu"].ToString());
                    m.ToLeaderTotal = Utils.GetDecimal(dr["ToLeaderTotal"].ToString());
                }
            }
            return m;
        }

        /// <summary>
        /// 根据团队编号获取购物收入列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>购物收入列表</returns>
        public IList<MGouWuShouRuBase> GetGouWuShouRuLst(string tourId)
        {
            var l = new List<MGouWuShouRuBase>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  B.PlanId ,");
            sql.Append("         B.SourceId ,");
            sql.Append("         B.SourceName ,");
            sql.Append("         B.ContactName ,");
            sql.Append("         B.ContactPhone ,");
            sql.Append("         B.ContactFax ,");
            sql.Append("         B.StartDate ,");
            sql.Append("         A.Adult ,");
            sql.Append("         A.Child ,");
            sql.Append("         A.BaoDi ,");
            sql.Append("         A.YingYe ,");
            sql.Append("         A.ToCompanyTotal ,");
            sql.Append("         A.ToGuideTotal ,");
            sql.Append("         A.ToLeaderTotal ,");
            sql.Append("         B.Remarks");
            sql.Append(" FROM    dbo.tbl_Plan B");
            sql.Append("         LEFT OUTER JOIN dbo.tbl_PlanShop A ON B.PlanId = A.PlanId");
            sql.Append(" WHERE");
            sql.Append("        B.Status = @Status");
            sql.Append("        AND B.Type = @Type");
            sql.Append("        AND B.IsDelete = 0");
            sql.Append("        AND B.TourId = @TourId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)PlanState.已落实);
            this.db.AddInParameter(dc, "@Type", DbType.Byte, (int)PlanProject.购物);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    l.Add(new MGouWuShouRuBase
                    {
                        PlanId = dr["PlanId"].ToString(),
                        SourceId = dr["SourceId"].ToString(),
                        SourceName = dr["SourceName"].ToString(),
                        ContactName = dr["ContactName"].ToString(),
                        ContactPhone = dr["ContactPhone"].ToString(),
                        ContactFax = dr["ContactFax"].ToString(),
                        StartDate = Utils.GetDateTimeNullable(dr["StartDate"].ToString()),
                        Adult = Utils.GetInt(dr["Adult"].ToString()),
                        Child = Utils.GetInt(dr["Child"].ToString()),
                        YingYe = Utils.GetDecimal(dr["YingYe"].ToString()),
                        ToCompanyTotal = Utils.GetDecimal(dr["ToCompanyTotal"].ToString()),
                        ToGuideTotal = Utils.GetDecimal(dr["ToGuideTotal"].ToString()),
                        ToLeaderTotal = Utils.GetDecimal(dr["ToLeaderTotal"].ToString()),
                        Remarks = dr["Remarks"].ToString()
                    });
                }
            }
            return l;
        }
        #endregion

        #region 处理计调落实状态

        /// <summary>
        /// 根据团队编号和计调类型获取计调安排浮动信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="types">计调类型</param>
        /// <returns>供应商</returns>
        public IList<MJiDiaoAnPaiFuDongInfo> GetJiDiaoAnPaiFuDongs(string tourId, params PlanProject[] types)
        {
            var items = new List<MJiDiaoAnPaiFuDongInfo>();

            string s = string.Empty;
            s += " SELECT [SourceName],[ContactPhone],[Type] FROM [tbl_Plan] WHERE [TourId]=@TourId AND [IsDelete]='0' ";
            if (types != null && types.Length > 0)
            {
                s += string.Format(" AND [Type] IN({0}) ", Utils.GetSqlIn<PlanProject>(types));
            }
            s += " ORDER BY [IssueTime] ASC ";

            var dc = this.db.GetSqlStringCommand(s.ToString());
            this.db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    var item = new MJiDiaoAnPaiFuDongInfo();
                    item.GysName = dr["SourceName"].ToString();
                    item.Telephone = dr["ContactPhone"].ToString();
                    item.Type = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 根据团队编号判断是否存在未落实的计调项目
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isDel">是否删除</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExist(string tourId, bool isDel)
        {
            var sql = new StringBuilder();

            sql.Append(" SELECT 1");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.AppendFormat(" WHERE   TourId = '{0}'", tourId);
            sql.Append("         AND IsDelete = '0'");
            if (!isDel)
            {
                sql.AppendFormat("         AND Status <> {0}", (int)PlanState.已落实);
            }

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            return DbHelper.Exists(dc, this.db);
        }

        /// <summary>
        /// 根据计划编号和落实状态更新计调状态
        /// </summary>
        /// <param name="tourid">计划编号</param>
        /// <param name="status">落实状态：无计调任务=1 未安排 = 2 未落实=3 已落实=4 待确认=5</param>
        /// <param name="typ">计调类别：全部=0 酒店=1 用车=2 景点=3 导游=4 地接=5 用餐=6 购物=7 领料=8 飞机=9 火车=10 汽车=11 轮船=12 其它=13</param>
        /// <returns>返回执行结果：0 失败 1 成功</returns>
        public int DoGlobal(string tourid, PlanState status, PlanProject? typ)
        {
            var dc = this.db.GetStoredProcCommand("proc_Plan_DoGlobal");

            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourid);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)status);
            this.db.AddInParameter(dc, "@PlanType", DbType.Byte, typ.HasValue ? (int)typ.Value : 0);

            this.db.AddOutParameter(dc, "@Result", DbType.Int32, 1);

            DbHelper.RunProcedure(dc, this.db);

            return Convert.ToInt32(db.GetParameterValue(dc, "Result"));
        }

        /// <summary>
        /// 根据计调编号和更新状态更新计调状态
        /// </summary>
        /// <param name="planId">计调编号</param>
        /// <param name="status">更新的状态</param>
        /// <returns>1：成功 0：失败</returns>
        public int ChangePlanStatus(string planId, PlanState status)
        {
            var sql = new StringBuilder();

            sql.Append(" DECLARE @PlanType TINYINT ,");
            sql.Append("     @TourId CHAR(36) ,");
            sql.Append("     @Sql NVARCHAR(MAX)");
            sql.Append(" SELECT  @PlanType = Type ,");
            sql.Append("         @TourId = TourId");
            sql.Append(" FROM    dbo.tbl_Plan");
            sql.Append(" WHERE   PlanId = @PlanId");
            sql.Append(" UPDATE  tbl_plan");
            sql.Append(" SET     Status = @Status");
            sql.Append(" WHERE   planId = @planId");
            sql.Append(" SET @Sql = dbo.fn_UpdPlanStatus(@PlanType, @Status, @TourId,'0')");
            sql.Append(" EXECUTE(@Sql)");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@planId", DbType.AnsiStringFixedLength, planId);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)status);

            return DbHelper.ExecuteSqlTrans(dc, this.db) > 0 ? 1 : 0;
        }
        #endregion

        #region 获得计调中心列表
        /// <summary>
        /// 获得计调中心列表
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="MPlanListSearch"></param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HPlanStructure.MPlanList> GetPlanList(string CompanyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanListSearch MPlanListSearch, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.HPlanStructure.MPlanList> list = new List<EyouSoft.Model.HPlanStructure.MPlanList>();
            EyouSoft.Model.HPlanStructure.MPlanList item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Tour";
            string PrimaryKey = "TourId";
            string OrderByString = "LDate DESC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            //fields.Append(" CompanyId,TourId,TourCode,RouteName,LDate,TourStatus,TourSureStatus,TourType,OutQuoteType,SellerName,(select top 1 BuyCompanyId,BuyCompanyName,ContactName,ContactTel from tbl_TourOrder where TourId=tbl_Tour.TourId for xml raw,root) as CompanyList,TourDays,(select * from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as PlanerList,Adults,Childs,(select count(OrderId) from tbl_TourOrder where TourId=tbl_Tour.TourId and IsDelete=0 and Status=4) as OrderNum,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus,IsChange,IsSure");
            fields.Append(" CompanyId,TourId,TourCode,RouteName,LDate,TourStatus,TourSureStatus,TourType,OutQuoteType,SellerName,TourDays,Adults,Childs,Leaders,Operator,UpdateTime ");
            fields.Append(" ,(select top 1 BuyCompanyId,BuyCompanyName,ContactName,ContactTel from tbl_TourOrder where TourId=tbl_Tour.TourId for xml raw,root) as CompanyList ");
            fields.Append(" ,(select PlanerId,Planer,IsJieShou from tbl_TourPlaner where TourId=tbl_Tour.TourId for xml raw,root) as PlanerList ");
            //fields.Append(" ,exists(select 1 from tbl_TourPlanChange where TourId=tbl_Tour.TourId) as ChangeStatus ");
            fields.Append(" ,(CASE WHEN exists(select 1 from tbl_TourPlanChange where TourId=tbl_Tour.TourId) then '1' ELSE '0' END) as IsChange ");
            //fields.AppendFormat(" ,not exists(select 1 from tbl_TourPlanChange where TourId=tbl_Tour.TourId and Status={0}) as IsSure ", (int)ChangeStatus.计调已确认);
            fields.AppendFormat(" ,(CASE WHEN exists(select 1 from tbl_TourPlanChange where TourId=tbl_Tour.TourId and Status<{0}) then '0' ELSE '1' END) as IsSure ", (int)ChangeStatus.计调已确认);
            fields.Append(" ,(select * from tbl_TourPlanStatus where TourId=tbl_Tour.TourId for xml raw,root('TourPlanStatus')) as TourPlanStatus ");
            #endregion
            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and CompanyId='{0}' and TourType IN ({1},{4},{5}) and tourstatus > {2} and tourstatus <{3}", CompanyId, (int)EyouSoft.Model.EnumType.TourStructure.TourType.团队产品, (int)TourStatus.销售未派计划, (int)TourStatus.导游报销, (int)TourType.自由行, (int)TourType.散拼产品);
            //cmdQuery.AppendFormat(" IsDelete=0 and CompanyId='{0}' and TourType={1} and tourstatus <{2}", CompanyId, (int)EyouSoft.Model.EnumType.TourStructure.TourType.团队产品, (int)TourStatus.导游报销);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(" and (exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Tour.TourId)", LoginUserId);
                    cmdQuery.AppendFormat(" or exists(select 1 from tbl_TourPlaner where PlanerDeptId in ({0}) and TourId=tbl_Tour.TourId))", GetIdsByArr(DetpIds));
                }
            }
            if (MPlanListSearch != null)
            {
                if (!string.IsNullOrEmpty(MPlanListSearch.TourCode))
                {
                    cmdQuery.AppendFormat(" and TourCode like '%{0}%'", Utils.ToSqlLike(MPlanListSearch.TourCode));
                }
                if (MPlanListSearch.SLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)>=0", MPlanListSearch.SLDate);
                }
                if (MPlanListSearch.LLDate.HasValue)
                {
                    cmdQuery.AppendFormat(" and datediff(day,'{0}',LDate)<=0", MPlanListSearch.LLDate);
                }
                if (!string.IsNullOrEmpty(MPlanListSearch.PlanerId))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_Tour.TourId  and PlanerId='{0}')", MPlanListSearch.PlanerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.Planer))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where TourId=tbl_Tour.TourId  and Planer like '%{0}%')", Utils.ToSqlLike(MPlanListSearch.Planer));
                    }
                }
                if (MPlanListSearch.TourStatus != null)
                {
                    cmdQuery.AppendFormat(" and TourStatus={0}", (int)MPlanListSearch.TourStatus);
                }
                if (MPlanListSearch.TourSureStatus != null)
                {
                    cmdQuery.AppendFormat(" and TourSureStatus={0}", (int)MPlanListSearch.TourSureStatus);
                }
                if (!string.IsNullOrEmpty(MPlanListSearch.SellerId))
                {
                    cmdQuery.AppendFormat(" and SellerId='{0}'", MPlanListSearch.SellerId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.SellerName))
                    {
                        cmdQuery.AppendFormat(" and SellerName like '%{0}%'", Utils.ToSqlLike(MPlanListSearch.SellerName));
                    }
                }
                if (MPlanListSearch.CompanyInfo != null)
                {
                    if (!string.IsNullOrEmpty(MPlanListSearch.CompanyInfo.CompanyId))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Tour.TourId and BuyCompanyId='{0}')", MPlanListSearch.CompanyInfo.CompanyId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(MPlanListSearch.CompanyInfo.CompanyName))
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Tour.TourId and BuyCompanyName like '%{0}%')", Utils.ToSqlLike(MPlanListSearch.CompanyInfo.CompanyName));
                        }
                    }
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this.db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.HPlanStructure.MPlanList();
                    item.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    item.TourCode = rdr["TourCode"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LDate")))
                    {
                        item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    }
                    item.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    item.RouteName = rdr["RouteName"].ToString();
                    item.Adults = rdr.IsDBNull(rdr.GetOrdinal("Adults")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Adults"));
                    item.Childs = rdr.IsDBNull(rdr.GetOrdinal("Childs")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Childs"));
                    item.Leaders = rdr.IsDBNull(rdr.GetOrdinal("Leaders")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("Leaders"));
                    //if (item.TourType == EyouSoft.Model.EnumType.TourStructure.TourType.团队产品)
                    //{
                    //    item.CompanyInfo = GetCompanyInfoByXml(rdr["CompanyList"].ToString());
                    //}
                    item.Operator = rdr["Operator"].ToString();
                    item.UpdateTime = rdr.GetDateTime(rdr.GetOrdinal("UpdateTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsChange")))
                    {
                        item.IsChange = rdr.GetString(rdr.GetOrdinal("IsChange")) == "1";
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsSure")))
                    {
                        item.IsSure = rdr.GetString(rdr.GetOrdinal("IsSure")) == "1";
                    }
                    item.CompanyInfo = GetCompanyInfoByXml(rdr["CompanyList"].ToString());
                    item.SellerName = rdr.GetString(rdr.GetOrdinal("SellerName"));
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    item.TourPlaner = GetTourPlanerByXml(rdr["PlanerList"].ToString());
                    item.TourType = (EyouSoft.Model.EnumType.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourType"));
                    item.OutQuoteType = (EyouSoft.Model.EnumType.TourStructure.TourQuoteType)rdr.GetByte(rdr.GetOrdinal("OutQuoteType"));
                    item.TourPlanStatus = GetTourPlanStatus(rdr["TourPlanStatus"].ToString());
                    item.TourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    item.TourSureStatus = (EyouSoft.Model.EnumType.TourStructure.TourSureStatus)rdr.GetByte(rdr.GetOrdinal("TourSureStatus"));
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 获得计调查询统计列表计
        /// <summary>
        /// 获得计调查询统计列表计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <param name="DetpIds"></param>
        /// <param name="isOnlySeft"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HPlanStructure.MPlanTJInfo> GetPlanTJ(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.HPlanStructure.MPlanTJChaXunInfo chaXun, int[] DetpIds, bool isOnlySeft, string LoginUserId)
        {
            IList<EyouSoft.Model.HPlanStructure.MPlanTJInfo> list = new List<EyouSoft.Model.HPlanStructure.MPlanTJInfo>();
            EyouSoft.Model.HPlanStructure.MPlanTJInfo item = null;
            StringBuilder cmdQuery = new StringBuilder();
            string TableName = "tbl_Plan";
            string PrimaryKey = "PlanId";
            string OrderByString = "StartDate ASC";
            StringBuilder fields = new StringBuilder();
            #region 要查询的字段
            fields.Append(" PlanId,SourceId,SourceName,CompanyId,StartDate,EndDate,DueToway,TourId,GuideNotes,Status,PaymentType,[Type] ");
            fields.Append(" ,(select top 1 TourCode from tbl_Tour where TourId=tbl_Plan.TourId) as TourCode ");
            //fields.Append(" ,(select top 1 RouteName from tbl_Tour where TourId=tbl_Plan.TourId) as RouteName ");
            fields.Append(" ,(select top 1 LDate from tbl_Tour where TourId=tbl_Plan.TourId) as LDate ");
            //fields.Append(" ,(SELECT ContactName,ContactTel,ContactMobile FROM tbl_ComUser WHERE IsDelete='0' AND UserId=tbl_Plan.SourceId for xml raw,root) as GuidList ");
            fields.Append(" ,(SELECT ContactName,ContactTel,ContactMobile FROM tbl_ComUser WHERE IsDelete='0' ");
            fields.Append("      AND UserId IN (select p.SourceId from tbl_Plan p where p.Type=4 AND p.TourId=tbl_Plan.TourId) for xml raw,root) as GuidList ");
            fields.Append(" ,(select top 1 [Name] from tbl_SysCity where CityId in (select top 1 CityId from tbl_Source where SourceId=tbl_Plan.SourceId)) as CityName ");
            if (chaXun != null)
            {
                if (chaXun.Type.HasValue)
                {
                    if (chaXun.Type == PlanProject.酒店)
                    {
                        fields.Append(" ,XMLHotelRoom=(SELECT RoomType,ISNULL(UnitPrice,0) UnitPrice,PriceType,ISNULL(Quantity,0) Quantity,ISNULL(Total,0) Total ");
                        fields.Append("                FROM tbl_PlanHotelRoom WHERE PlanId = tbl_Plan.PlanId ");
                        fields.Append("                FOR XML RAW ,ROOT) ");
                    }
                    if (chaXun.Type == PlanProject.用车)
                    {
                        fields.Append(" ,XMLCar=(SELECT Models,CarNumber,Driver,DriverPhone,ISNULL(CarPrice,0) CarPrice,ISNULL(Days,0) Days,ISNULL(SumPrice,0) SumPrice ");
                        fields.Append("          FROM tbl_PlanCar WHERE PlanId = tbl_Plan.PlanId");
                        fields.Append("          FOR XML RAW ,ROOT) ");
                    }
                    if (chaXun.Type == PlanProject.景点)
                    {
                        fields.Append(" ,XMLAttractions=(SELECT Attractions,ISNULL(AdultNumber,0) AdultNumber,ISNULL(ChildNumber,0) ChildNumber,ISNULL(AdultPrice,0) AdultPrice ");
                        fields.Append("                  ,ISNULL(ChildPrice,0) ChildPrice,Seats,VisitTime,ISNULL(SumPrice,0) SumPrice  ");
                        fields.Append("                  FROM tbl_PlanAttractions WHERE PlanId = tbl_Plan.PlanId");
                        fields.Append("                  FOR XML RAW ,ROOT) ");
                    }
                    if (chaXun.Type == PlanProject.用餐)
                    {
                        fields.Append(" ,XMLDiningList=(SELECT DiningType,MenuName,ISNULL(AdultNumber,0) AdultNumber,ISNULL(ChildNumber,0) ChildNumber,ISNULL(TableNumber,0) TableNumber,ISNULL(SumPrice,0) SumPrice ");
                        fields.Append("                 FROM tbl_PlanDining WHERE PlanId = tbl_Plan.PlanId");
                        fields.Append("                 FOR XML RAW ,ROOT) ");
                    }
                }
            }
            #endregion

            #region 拼接查询条件
            cmdQuery.AppendFormat(" IsDelete=0 and CompanyId='{0}' ", companyId);
            if (isOnlySeft)
            {
                cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Plan.TourId)", LoginUserId);
            }
            else
            {
                if (DetpIds != null)
                {
                    cmdQuery.AppendFormat(" and (exists(select 1 from tbl_TourPlaner where PlanerId='{0}' and TourId=tbl_Plan.TourId)", LoginUserId);
                    cmdQuery.AppendFormat(" or exists(select 1 from tbl_TourPlaner where PlanerDeptId in ({0}) and TourId=tbl_Plan.TourId))", GetIdsByArr(DetpIds));
                }
            }
            if (chaXun != null)
            {
                //安排类型
                if (chaXun.Type.HasValue)
                {
                    cmdQuery.AppendFormat(" and [Type]={0} ", (int)chaXun.Type);
                }
                //预定方式
                if (chaXun.DueToway.HasValue)
                {
                    cmdQuery.AppendFormat(" and DueToway={0} ", (int)chaXun.DueToway);
                }
                //供应商
                if (!string.IsNullOrEmpty(chaXun.SourceId))
                {
                    cmdQuery.AppendFormat(" and SourceId='{0}' ", chaXun.SourceId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(chaXun.SourceName))
                    {
                        cmdQuery.AppendFormat(" and SourceName like '%{0}%' ", chaXun.SourceName);
                    }
                }
                //入住时间
                if (chaXun.Type.HasValue && (int)chaXun.Type == 1)
                {
                    if (chaXun.STime.HasValue && chaXun.ETime.HasValue)
                    {
                        cmdQuery.AppendFormat(" and ((datediff(day,'{0}',StartDate)>=0 and datediff(day,'{1}',StartDate)<=0) ", chaXun.STime, chaXun.ETime);
                        cmdQuery.AppendFormat("       or (datediff(day,'{0}',EndDate)>=0 and datediff(day,'{1}',EndDate)<=0) ) ", chaXun.STime, chaXun.ETime);
                    }
                    if (chaXun.STime.HasValue && !chaXun.ETime.HasValue)
                    {
                        cmdQuery.AppendFormat(" and (datediff(day,'{0}',StartDate)>=0 or datediff(day,'{0}',EndDate)>=0) ", chaXun.STime);
                    }
                    if (!chaXun.STime.HasValue && chaXun.ETime.HasValue)
                    {
                        cmdQuery.AppendFormat(" and datediff(day,'{0}',StartDate)<=0 and datediff(day,'{0}',EndDate)<=0 ", chaXun.ETime);
                    }
                }
                //用餐时间
                if (chaXun.Type.HasValue && (int)chaXun.Type == 6)
                {
                    if (chaXun.STime.HasValue)
                    {
                        cmdQuery.AppendFormat(" and datediff(day,'{0}',StartDate)>=0 ", chaXun.STime);
                    }
                    if (chaXun.ETime.HasValue)
                    {
                        cmdQuery.AppendFormat(" and datediff(day,'{0}',StartDate)<=0 ", chaXun.ETime);
                    }
                }
                //抵达时间
                if (chaXun.StartTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Tour where TourId=tbl_Plan.TourId and datediff(day,'{0}',LDate)>=0) ", chaXun.StartTime);
                }
                if (chaXun.EndTime.HasValue)
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Tour where TourId=tbl_Plan.TourId and datediff(day,'{0}',LDate)<=0) ", chaXun.EndTime);
                }
                //团号
                if (!string.IsNullOrEmpty(chaXun.TourCode))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Tour where TourId=tbl_Plan.TourId and TourCode like '%{0}%') ", Utils.ToSqlLike(chaXun.TourCode));
                }
                //客户单位
                if (chaXun.CompanyInfo != null)
                {
                    if (!string.IsNullOrEmpty(chaXun.CompanyInfo.CompanyId))
                    {
                        cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Plan.TourId and BuyCompanyId='{0}') ", chaXun.CompanyInfo.CompanyId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(chaXun.CompanyInfo.CompanyName))
                        {
                            cmdQuery.AppendFormat(" and exists(select 1 from tbl_TourOrder where TourId=tbl_Plan.TourId and BuyCompanyName like '%{0}%') ", Utils.ToSqlLike(chaXun.CompanyInfo.CompanyName));
                        }
                    }
                }
                //线路区域
                if (chaXun.AreaId.HasValue && chaXun.AreaId > 0)
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_Tour where TourId=tbl_Plan.TourId and AreaId={0}) ", chaXun.AreaId);
                }
                //用餐时间类型
                if (chaXun.DiningType.HasValue)
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_PlanDining p where p.PlanId=tbl_Plan.PlanId and p.DiningType={0}) ", (int)chaXun.DiningType);
                }
                //景点名称
                if (!string.IsNullOrEmpty(chaXun.Attractions))
                {
                    cmdQuery.AppendFormat(" and exists(select 1 from tbl_PlanAttractions p where p.PlanId=tbl_Plan.PlanId and p.Attractions like '%{0}%') ", chaXun.Attractions);
                }
            }
            #endregion
            using (IDataReader rdr = DbHelper.ExecuteReader(this.db, pageSize, pageIndex, ref recordCount, TableName, PrimaryKey, fields.ToString(), cmdQuery.ToString(), OrderByString))
            {
                while (rdr.Read())
                {
                    item = new EyouSoft.Model.HPlanStructure.MPlanTJInfo();
                    item.TourId = rdr.GetString(rdr.GetOrdinal("TourId"));
                    item.TourCode = rdr["TourCode"].ToString();
                    item.SourceId = rdr["SourceId"].ToString();
                    item.SourceName = rdr["SourceName"].ToString();
                    item.PlanId = rdr.GetString(rdr.GetOrdinal("PlanId"));
                    item.PaymentType = (Payment)rdr.GetByte(rdr.GetOrdinal("PaymentType"));
                    item.Status = (PlanState)rdr.GetByte(rdr.GetOrdinal("Status"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("StartDate")))
                    {
                        item.STime = rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EndDate")))
                    {
                        item.ETime = rdr.GetDateTime(rdr.GetOrdinal("EndDate"));
                    }
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LDate")))
                    {
                        item.StartTime = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    }
                    item.DueToway = (DueToway)rdr.GetByte(rdr.GetOrdinal("DueToway"));
                    item.GuideNotes = !rdr.IsDBNull(rdr.GetOrdinal("GuideNotes")) ? rdr["GuideNotes"].ToString() : "";
                    item.CityName = !rdr.IsDBNull(rdr.GetOrdinal("CityName")) ? rdr["CityName"].ToString() : "";
                    //item.RouteName = !rdr.IsDBNull(rdr.GetOrdinal("RouteName")) ? rdr["RouteName"].ToString() : "";
                    if (chaXun != null)
                    {
                        if (chaXun.Type.HasValue)
                        {
                            switch (chaXun.Type)
                            {
                                case PlanProject.酒店:
                                    item.PlanHotelRoomList = this.GetPlanHotelRoomLst(rdr["XMLHotelRoom"].ToString());
                                    break;
                                case PlanProject.用车:
                                    item.PlanCarList = this.GetPlanCarMdl(rdr["XMLCar"].ToString());
                                    break;
                                case PlanProject.景点:
                                    item.PlanAttractionsList = this.GetPlanAttractionsLst(rdr["XMLAttractions"].ToString());
                                    break;
                                case PlanProject.用餐:
                                    item.PlanDiningList = this.GetPlanDiningLst(rdr["XMLDiningList"].ToString());
                                    break;
                            }
                        }
                    }
                    item.GuidList = GetGuidInfoByXml(rdr["GuidList"].ToString());
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 私有方法

        #region 获得计划项目安排落实实体
        /// <summary>
        /// 获得计划项目安排落实实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private EyouSoft.Model.HTourStructure.MTourPlanStatus GetTourPlanStatus(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            EyouSoft.Model.HTourStructure.MTourPlanStatus item = new EyouSoft.Model.HTourStructure.MTourPlanStatus();
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item.TourId = Utils.GetXAttributeValue(xRow, "TourId");
                item.Car = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Car")));
                item.Dining = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Dining")));
                item.DJ = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "DJ")));
                item.Guide = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Guide")));
                item.Hotel = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Hotel")));
                item.LL = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "LL")));
                item.CarTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CarTicket")));
                item.TrainTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "TrainTicket")));
                item.PlaneTicket = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "PlaneTicket")));
                item.Other = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Other")));
                item.CShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "CShip")));
                //item.FShip = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "FShip")));
                item.Shopping = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Shopping")));
                item.Spot = (EyouSoft.Model.EnumType.PlanStructure.PlanState)(Utils.GetInt(Utils.GetXAttributeValue(xRow, "Spot")));
            }
            return item;
        }
        #endregion

        #region 根据XML转成实体

        #region 根据XML获取地接/酒店房屋类型列表
        /// <summary>
        /// 获取地接/酒店房屋类型列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>地接/酒店房屋类型列表</returns>
        private IList<MPlanHotelRoom> GetPlanHotelRoomLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanHotelRoom
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                RoomId = Utils.GetXAttributeValue(i, "RoomId"),
                RoomType = Utils.GetXAttributeValue(i, "RoomType"),
                UnitPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "UnitPrice")),
                PriceType = (PlanHotelPriceType)Utils.GetInt(Utils.GetXAttributeValue(i, "PriceType")),
                Total = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Total")),
                Quantity = Utils.GetInt(Utils.GetXAttributeValue(i, "Quantity"))
            }).ToList();
        }
        #endregion

        #region 根据XML获取计调增减列表
        /// <summary>
        /// 获取计调增减列表
        /// </summary>
        /// <param name="xml">计调增减XML</param>
        /// <returns>计调增减列表</returns>
        private IList<MPlanCostChange> GetPlanCostChangeLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanCostChange
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                ChangeType = (PlanChangeChangeClass)Utils.GetInt(Utils.GetXAttributeValue(i, "ChangeType")),
                Type = Utils.GetXAttributeValue(i, "Type") == "1",
                PeopleNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "PeopleNumber")),
                ChangeCost = Utils.GetDecimal(Utils.GetXAttributeValue(i, "ChangeCost")),
                Remark = Utils.GetXAttributeValue(i, "Remark"),
                IssueTime = Utils.GetDateTime(Utils.GetXAttributeValue(i, "IssueTime"))
            }).ToList();
        }
        #endregion

        #region 根据XML获取用车实体
        /// <summary>
        /// 获取用车实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>用车实体</returns>
        private IList<MPlanCar> GetPlanCarMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanCar
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                PriceType = (PlanCarPriceType)Utils.GetInt(Utils.GetXAttributeValue(i, "PriceType")),
                CarId = Utils.GetXAttributeValue(i, "CarId"),
                Models = Utils.GetXAttributeValue(i, "Models"),
                CarNumber = Utils.GetXAttributeValue(i, "CarNumber"),
                Driver = Utils.GetXAttributeValue(i, "Driver"),
                DriverPhone = Utils.GetXAttributeValue(i, "DriverPhone"),
                CarPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "CarPrice")),
                Days = Utils.GetInt(Utils.GetXAttributeValue(i, "Days")),
                BridgePrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "BridgePrice")),
                DriverPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "DriverPrice")),
                DriverRoomPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "DriverRoomPrice")),
                DriverDiningPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "DriverDiningPrice")),
                EmptyDrivingPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "EmptyDrivingPrice")),
                OtherPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "OtherPrice")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice")),
                Remark = Utils.GetXAttributeValue(i, "Remark")
            }).ToList();
        }
        #endregion

        #region 根据XML获取区间交通实体
        /// <summary>
        /// 获取区间交通实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>区间交通实体</returns>
        private IList<MPlanLargeFrequency> GetLargeFrequencyLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanLargeFrequency
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                DepartureTime = Utils.GetDateTime(Utils.GetXAttributeValue(i, "DepartureTime")),
                SeatType = (PlanLargeSeatType)Utils.GetInt(Utils.GetXAttributeValue(i, "SeatType")),
                AdultsType = (PlanLargeAdultsType)Utils.GetInt(Utils.GetXAttributeValue(i, "AdultsType")),
                Numbers = Utils.GetXAttributeValue(i, "Numbers"),
                Time = Utils.GetXAttributeValue(i, "Time"),
                Departure = Utils.GetXAttributeValue(i, "Departure"),
                Destination = Utils.GetXAttributeValue(i, "Destination"),
                SeatStandard = Utils.GetXAttributeValue(i, "SeatStandard"),
                PepolePayNum = Utils.GetInt(Utils.GetXAttributeValue(i, "PepolePayNum")),
                FreeNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "FreeNumber")),
                FarePrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FarePrice")),
                InsuranceHandlFee = Utils.GetDecimal(Utils.GetXAttributeValue(i, "InsuranceHandlFee")),
                Fee = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Fee")),
                Surcharge = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Surcharge")),
                Discount = float.Parse(string.IsNullOrEmpty(Utils.GetXAttributeValue(i, "Discount")) ? "0" : Utils.GetXAttributeValue(i, "Discount")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice"))
            }).ToList();
        }
        #endregion

        #region 根据XML获取用餐实体
        /// <summary>
        /// 获取用餐实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>用餐实体</returns>
        private IList<MPlanDining> GetPlanDiningLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanDining
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                DiningType = (PlanDiningType)Utils.GetInt(Utils.GetXAttributeValue(i, "DiningType")),
                PriceType = (PlanDiningPriceType)Utils.GetInt(Utils.GetXAttributeValue(i, "PriceType")),
                AdultNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "AdultNumber")),
                ChildNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "ChildNumber")),
                LeaderNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "LeaderNumber")),
                GuideNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "GuideNumber")),
                DriverNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "DriverNumber")),
                AdultUnitPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "AdultUnitPrice")),
                ChildPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "ChildPrice")),
                MenuId = Utils.GetXAttributeValue(i, "MenuId"),
                MenuName = Utils.GetXAttributeValue(i, "MenuName"),
                TableNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "TableNumber")),
                FreeNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "FreeNumber")),
                FreePrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FreePrice")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice"))
            }).ToList();
        }
        #endregion


        #region 根据XML获取景点实体
        /// <summary>
        /// 获取景点实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>景点实体</returns>
        private IList<MPlanAttractions> GetPlanAttractionsLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanAttractions
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                AttractionsId = Utils.GetXAttributeValue(i, "AttractionsId"),
                Attractions = Utils.GetXAttributeValue(i, "Attractions"),
                AdultNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "AdultNumber")),
                ChildNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "ChildNumber")),
                AdultPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "AdultPrice")),
                ChildPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "ChildPrice")),
                Seats = Utils.GetXAttributeValue(i, "Seats"),
                VisitTime = Utils.GetDateTime(Utils.GetXAttributeValue(i, "VisitTime")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice")),
                BeiZhu = Utils.GetXAttributeValue(i, "BeiZhu")
            }).ToList();
        }
        #endregion

        #region 根据XML获取购物产品实体
        /// <summary>
        /// 获取购物产品实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>购物产品实体</returns>
        private IList<MGouMaiChanPin> GetGouMaiChanPinLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MGouMaiChanPin
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                ProductName = Utils.GetXAttributeValue(i, "ProductName"),
                ProductId = Utils.GetXAttributeValue(i, "ProductId"),
                BuyAmount = Utils.GetInt(Utils.GetXAttributeValue(i, "BuyAmount")),
                BackMoney = Utils.GetDecimal(Utils.GetXAttributeValue(i, "BackMoney")),
            }).ToList();
        }
        #endregion

        #region 根据ＸＭＬ获到客户单位
        /// <summary>
        /// 根据ＸＭＬ获到客户单位
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MCompanyInfo> GetCompanyInfoByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MCompanyInfo> list = new List<MCompanyInfo>();
            MCompanyInfo item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MCompanyInfo()
                {
                    CompanyId = Utils.GetXAttributeValue(xRow, "BuyCompanyId"),
                    CompanyName = Utils.GetXAttributeValue(xRow, "BuyCompanyName"),
                    Contact = Utils.GetXAttributeValue(xRow, "ContactName"),
                    Phone = Utils.GetXAttributeValue(xRow, "ContactTel")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region 根据ＸＭＬ获到导游信息
        /// <summary>
        /// 根据ＸＭＬ获到导游信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<MGuidInfo> GetGuidInfoByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<MGuidInfo> list = new List<MGuidInfo>();
            MGuidInfo item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new MGuidInfo()
                {
                    Name = Utils.GetXAttributeValue(xRow, "ContactName"),
                    Tel = Utils.GetXAttributeValue(xRow, "ContactTel"),
                    Mobile = Utils.GetXAttributeValue(xRow, "ContactMobile")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region 根据ＸＭＬ获到计划计调员
        /// <summary>
        /// 根据ＸＭＬ获到计划计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HTourStructure.MTourPlaner> GetTourPlanerByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.HTourStructure.MTourPlaner> list = new List<EyouSoft.Model.HTourStructure.MTourPlaner>();
            EyouSoft.Model.HTourStructure.MTourPlaner item = null;
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.HTourStructure.MTourPlaner()
                {
                    Planer = Utils.GetXAttributeValue(xRow, "Planer"),
                    PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId"),
                    IsJieShou = (!string.IsNullOrEmpty(Utils.GetXAttributeValue(xRow, "IsJieShou")) && Utils.GetXAttributeValue(xRow, "IsJieShou") != "0")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion

        #endregion

        #region 将实体转成XML数据

        #region 将酒店实体转成XML数据
        /// <summary>
        /// 将酒店转成XML数据
        /// </summary>
        /// <param name="mdl">酒店实体</param>
        /// <returns></returns>
        private string GetPlanHotelXml(MPlanHotel mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" Star='{0}'", (int)mdl.Star);
                sb.AppendFormat(" Days='{0}'", mdl.Days);
                sb.AppendFormat(" FreeNumber='{0}'", mdl.FreeNumber);
                sb.AppendFormat(" IsMeal='{0}'", (int)(mdl.IsMeal));
                sb.AppendFormat(" FreePrice='{0}'", mdl.FreePrice);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 地接/酒店房屋类型转成XML数据
        /// <summary>
        /// 地接/酒店房屋类型转成XML数据
        /// </summary>
        /// <param name="lst">地接/酒店房屋类型列表</param>
        /// <returns></returns>
        private string GetPlanHotelRoomXml(IList<MPlanHotelRoom> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" RoomId='{0}'", i.RoomId);
                    sb.AppendFormat(" RoomType='{0}'", Utils.ReplaceXmlSpecialCharacter(i.RoomType));
                    sb.AppendFormat(" UnitPrice='{0}'", i.UnitPrice);
                    sb.AppendFormat(" PriceType='{0}'", (int)i.PriceType);
                    sb.AppendFormat(" Quantity='{0}'", i.Quantity);
                    sb.AppendFormat(" Total='{0}'", i.Total);
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将景点实体转成XML数据
        /// <summary>
        /// 将景点转成XML数据
        /// </summary>
        /// <param name="mdl">景点实体</param>
        /// <returns></returns>
        private string GetPlanAttractionsXml(IList<MPlanAttractions> lst)
        {
            var strXml = string.Empty;
            if (lst != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" AttractionsId='{0}'", i.AttractionsId);
                    sb.AppendFormat(" Attractions='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Attractions));
                    sb.AppendFormat(" AdultNumber='{0}'", i.AdultNumber);
                    sb.AppendFormat(" ChildNumber='{0}'", i.ChildNumber);
                    sb.AppendFormat(" AdultPrice='{0}'", i.AdultPrice);
                    sb.AppendFormat(" ChildPrice='{0}'", i.ChildPrice);
                    sb.AppendFormat(" Seats='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Seats));
                    if (i.VisitTime.HasValue) sb.AppendFormat(" VisitTime='{0}'", i.VisitTime.Value);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat(" BeiZhu='{0}'", Utils.ReplaceXmlSpecialCharacter(i.BeiZhu));
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将用车实体转成XML数据
        /// <summary>
        /// 将用车转成XML数据
        /// </summary>
        /// <param name="mdl">用车实体</param>
        /// <returns></returns>
        private string GetPlanCarXml(IList<MPlanCar> lst)
        {
            var strXml = string.Empty;
            if (lst != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" PriceType='{0}'", (int)i.PriceType);
                    sb.AppendFormat(" CarId='{0}'", i.CarId);
                    sb.AppendFormat(" Models='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Models));
                    sb.AppendFormat(" CarNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(i.CarNumber));
                    sb.AppendFormat(" Driver='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Driver));
                    sb.AppendFormat(" DriverPhone='{0}'", Utils.ReplaceXmlSpecialCharacter(i.DriverPhone));
                    sb.AppendFormat(" CarPrice='{0}'", i.CarPrice);
                    sb.AppendFormat(" Days='{0}'", i.Days);
                    sb.AppendFormat(" BridgePrice='{0}'", i.BridgePrice);
                    sb.AppendFormat(" DriverPrice='{0}'", i.DriverPrice);
                    sb.AppendFormat(" DriverRoomPrice='{0}'", i.DriverRoomPrice);
                    sb.AppendFormat(" DriverDiningPrice='{0}'", i.DriverDiningPrice);
                    sb.AppendFormat(" EmptyDrivingPrice='{0}'", i.EmptyDrivingPrice);
                    sb.AppendFormat(" OtherPrice='{0}'", i.OtherPrice);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat(" Remark='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Remark));
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将用餐价格转成XML数据
        /// <summary>
        /// 将用餐价格转成XML数据
        /// </summary>
        /// <param name="lst">用餐价格列表</param>
        /// <returns></returns>
        private string GetPlanDiningXml(IList<MPlanDining> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" PriceType='{0}'", (int)i.PriceType);
                    sb.AppendFormat(" DiningType='{0}'", (int)i.DiningType);
                    sb.AppendFormat(" AdultNumber='{0}'", i.AdultNumber);
                    sb.AppendFormat(" ChildNumber='{0}'", i.ChildNumber);
                    sb.AppendFormat(" LeaderNumber='{0}'", i.LeaderNumber);
                    sb.AppendFormat(" GuideNumber='{0}'", i.GuideNumber);
                    sb.AppendFormat(" DriverNumber='{0}'", i.DriverNumber);
                    sb.AppendFormat(" AdultUnitPrice='{0}'", i.AdultUnitPrice);
                    sb.AppendFormat(" ChildPrice='{0}'", i.ChildPrice);
                    sb.AppendFormat(" MenuId='{0}'", i.MenuId);
                    sb.AppendFormat(" MenuName='{0}'", Utils.ReplaceXmlSpecialCharacter(i.MenuName));
                    sb.AppendFormat(" TableNumber='{0}'", i.TableNumber);
                    sb.AppendFormat(" FreeNumber='{0}'", i.FreeNumber);
                    sb.AppendFormat(" FreePrice='{0}'", i.FreePrice);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;
        }
        #endregion

        #region 将区间交通转成XML数据
        /// <summary>
        /// 将区间交通转成XML数据
        /// </summary>
        /// <param name="lst">区间交通列表</param>
        /// <returns></returns>
        private string GetPlanLargeFrequencyXml(IList<MPlanLargeFrequency> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    if (i.DepartureTime.HasValue) sb.AppendFormat(" DepartureTime='{0}'", i.DepartureTime);
                    sb.AppendFormat(" Time='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Time));
                    sb.AppendFormat(" Numbers='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Numbers));
                    sb.AppendFormat(" Departure='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Departure));
                    sb.AppendFormat(" Destination='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Destination));
                    sb.AppendFormat(" SeatStandard='{0}'", Utils.ReplaceXmlSpecialCharacter(i.SeatStandard));
                    sb.AppendFormat(" SeatType='{0}'", (int)i.SeatType);
                    sb.AppendFormat(" AdultsType='{0}'", (int)i.AdultsType);
                    sb.AppendFormat(" PepolePayNum='{0}'", i.PepolePayNum);
                    sb.AppendFormat(" FarePrice='{0}'", i.FarePrice);
                    sb.AppendFormat(" FreeNumber='{0}'", i.FreeNumber);
                    sb.AppendFormat(" InsuranceHandlFee='{0}'", i.InsuranceHandlFee);
                    sb.AppendFormat(" Fee='{0}'", i.Fee);
                    sb.AppendFormat(" Surcharge='{0}'", i.Surcharge);
                    sb.AppendFormat(" Discount='{0}'", i.Discount);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将导游实体转成XML数据
        /// <summary>
        /// 将导游转成XML数据
        /// </summary>
        /// <param name="mdl">导游实体</param>
        /// <returns></returns>
        private string GetPlanGuideXml(MPlanGuide mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" Gender='{0}'", (int)mdl.Gender);
                sb.AppendFormat(" OnLocation='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.OnLocation));
                sb.AppendFormat(" NextLocation='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.NextLocation));
                sb.AppendFormat(" TaskType='{0}'", (int)mdl.TaskType);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将领料实体转成XML数据
        /// <summary>
        /// 将领料实体转成XML数据
        /// </summary>
        /// <param name="mdl">领料实体</param>
        /// <returns></returns>
        private string GetPlanGoodXml(MGovGoodUse mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" CompanyId='{0}'", mdl.CompanyId);
                sb.AppendFormat(" GoodId='{0}'", mdl.GoodId);
                sb.AppendFormat(" Type='{0}'", (int)mdl.Type);
                sb.AppendFormat(" DeptId='{0}'", mdl.DeptId);
                sb.AppendFormat(" Number='{0}'", mdl.Number);
                sb.AppendFormat(" UserId='{0}'", mdl.UserId);
                sb.AppendFormat(" Price='{0}'", mdl.Price);
                sb.AppendFormat(" OperatorId='{0}'", mdl.OperatorId);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将购物实体转成XML数据
        /// <summary>
        /// 将购物实体转成XML数据
        /// </summary>
        /// <param name="mdl">购物实体</param>
        /// <returns></returns>
        private string GetPlanShopXml(MPlanShop mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" LiuShui='{0}'", mdl.LiuShui);
                sb.AppendFormat(" BaoDi='{0}'", mdl.BaoDi);
                sb.AppendFormat(" YingYe='{0}'", mdl.YingYe);
                sb.AppendFormat(" PeopleMoney='{0}'", mdl.PeopleMoney);
                sb.AppendFormat(" ChildMoney='{0}'", mdl.ChildMoney);
                sb.AppendFormat(" Adult='{0}'", mdl.Adult);
                sb.AppendFormat(" Child='{0}'", mdl.Child);
                sb.AppendFormat(" ToCompanyRenTou='{0}'", mdl.ToCompanyRenTou);
                sb.AppendFormat(" ToCompanyBaoDi='{0}'", mdl.ToCompanyBaoDi);
                sb.AppendFormat(" ToCompanyRenShu='{0}'", mdl.ToCompanyRenShu);
                sb.AppendFormat(" ToCompanyBaoDi2='{0}'", mdl.ToCompanyBaoDi2);
                sb.AppendFormat(" ToCompanyRenShu2='{0}'", mdl.ToCompanyRenShu2);
                sb.AppendFormat(" ToCompanyFanDian='{0}'", mdl.ToCompanyFanDian);
                sb.AppendFormat(" ToCompanyYingYe='{0}'", mdl.ToCompanyYingYe);
                sb.AppendFormat(" ToCompanyTiQu='{0}'", mdl.ToCompanyTiQu);
                sb.AppendFormat(" ToCompanyTotal='{0}'", mdl.ToCompanyTotal);
                sb.AppendFormat(" ToGuideYingYe='{0}'", mdl.ToGuideYingYe);
                sb.AppendFormat(" ToGuideTiQu='{0}'", mdl.ToGuideTiQu);
                sb.AppendFormat(" ToGuideLu='{0}'", mdl.ToGuideLu);
                sb.AppendFormat(" ToGuideShui='{0}'", mdl.ToGuideShui);
                sb.AppendFormat(" ToGuidePei='{0}'", mdl.ToGuidePei);
                sb.AppendFormat(" ToGuideJiao='{0}'", mdl.ToGuideJiao);
                sb.AppendFormat(" ToGuideOther='{0}'", mdl.ToGuideOther);
                sb.AppendFormat(" ToGuideLiuShui='{0}'", mdl.ToGuideLiuShui);
                sb.AppendFormat(" ToGuideTotal='{0}'", mdl.ToGuideTotal);
                sb.AppendFormat(" ToLeaderYingYe='{0}'", mdl.ToLeaderYingYe);
                sb.AppendFormat(" ToLeaderTiQu='{0}'", mdl.ToLeaderTiQu);
                sb.AppendFormat(" ToLeaderTotal='{0}'", mdl.ToLeaderTotal);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将计调增减变更转成XML数据
        /// <summary>
        /// 将计调增减变更转成XML数据
        /// </summary>
        /// <param name="lst">计调增减变更列表</param>
        /// <returns></returns>
        private string GetPlanCostChangeXml(IList<MPlanCostChange> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" ChangeType='{0}'", (int)i.ChangeType);
                    sb.AppendFormat(" Type='{0}'", i.Type ? "1" : "0");
                    sb.AppendFormat(" PeopleNumber='{0}'", i.PeopleNumber);
                    sb.AppendFormat(" ChangeCost='{0}'", i.ChangeCost);
                    sb.AppendFormat(" Remark='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Remark));
                    sb.AppendFormat(" IssueTime='{0}'", i.IssueTime);
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将购买产品转成XML数据
        /// <summary>
        /// 将计调增减变更转成XML数据
        /// </summary>
        /// <param name="lst">计调增减变更列表</param>
        /// <returns></returns>
        private string GetGouMaiChanPinXml(IList<MGouMaiChanPin> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" ProductId='{0}'", i.ProductId);
                    sb.AppendFormat(" ProductName='{0}'", Utils.ReplaceXmlSpecialCharacter(i.ProductName));
                    sb.AppendFormat(" BuyAmount='{0}'", i.BuyAmount);
                    sb.AppendFormat(" BackMoney='{0}'", i.BackMoney);
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #endregion

        #endregion

        /// <summary>
        /// 是否允许操作计调项，返回1允许操作，其它不允许
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="planId">计调编号</param>
        /// <param name="operatorId">操作人编号</param>
        /// <returns></returns>
        public int IsYunXuCaoZuo(string tourId, string planId, string operatorId)
        {
            string sql = "SELECT COUNT(*) FROM tbl_Tour AS A WHERE A.TourId=@TourId AND EXISTS(SELECT 1 FROM tbl_TourPlaner AS A1 WHERE A1.TourId=@TourId AND A1.PlanerId=@OperatorId)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, operatorId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }
    }
}
