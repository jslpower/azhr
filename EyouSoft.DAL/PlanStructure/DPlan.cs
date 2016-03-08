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

namespace EyouSoft.DAL.PlanStructure
{
    using EyouSoft.Model.EnumType.FinStructure;
    using EyouSoft.Model.EnumType.GovStructure;
    using EyouSoft.Model.EnumType.PlanStructure;
    using EyouSoft.Model.EnumType.TourStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Model.GovStructure;
    using EyouSoft.Model.PlanStructure;

    /// <summary>
    /// 描述:数据操作计调安排类
    /// 创建人:马昌雄
    /// 创建时间:2011-09-23
    /// </summary>
    public class DPlan : EyouSoft.Toolkit.DAL.DALBase, EyouSoft.IDAL.PlanStructure.IPlan
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
            this.db.AddInParameter(dc, "@Num", DbType.Int32, mdl.Num);
            this.db.AddInParameter(dc, "@ReceiveJourney", DbType.String, mdl.ReceiveJourney);
            this.db.AddInParameter(dc, "@CostDetail", DbType.String, mdl.CostDetail);
            this.db.AddInParameter(dc, "@PlanCost", DbType.Decimal, mdl.PlanCost);
            this.db.AddInParameter(dc, "@PaymentType", DbType.Byte, (int)mdl.PaymentType);
            this.db.AddInParameter(dc, "@Status", DbType.Byte, (int)mdl.Status);
            this.db.AddInParameter(dc, "@GuideNotes", DbType.String, mdl.GuideNotes);
            this.db.AddInParameter(dc, "@Remarks", DbType.String, mdl.Remarks);
            this.db.AddInParameter(dc, "@SueId", DbType.AnsiStringFixedLength, mdl.SueId);
            //this.db.AddInParameter(dc, "@CostId", DbType.AnsiStringFixedLength, mdl.CostId);
            //this.db.AddInParameter(dc, "@CostName", DbType.String, mdl.CostName);
            //this.db.AddInParameter(dc, "@CostStatus", DbType.AnsiStringFixedLength, mdl.CostStatus?"1":"0");
            //this.db.AddInParameter(dc, "@CostTime", DbType.DateTime, mdl.CostTime.HasValue?mdl.CostTime.Value:(DateTime?)null);
            this.db.AddInParameter(dc, "@Confirmation", DbType.Decimal, mdl.Confirmation);
            //this.db.AddInParameter(dc, "@CostRemarks", DbType.String, mdl.CostRemarks);
            this.db.AddInParameter(dc, "@DeptId", DbType.Int32, mdl.DeptId);
            this.db.AddInParameter(dc, "@OperatorId", DbType.AnsiStringFixedLength, mdl.OperatorId);
            this.db.AddInParameter(dc, "@OperatorName", DbType.String, mdl.OperatorName);
            this.db.AddInParameter(dc, "@IssueTime", DbType.DateTime, mdl.IssueTime);
            this.db.AddInParameter(dc, "@Prepaid", DbType.Decimal, mdl.Prepaid);
            this.db.AddInParameter(dc, "@IsRebate", DbType.AnsiStringFixedLength, mdl.IsRebate ? "1" : "0");
            this.db.AddInParameter(dc, "@AddStatus", DbType.Byte, (int)mdl.AddStatus);
            this.db.AddInParameter(dc, "@ServiceStandard", DbType.String, mdl.ServiceStandard);
            this.db.AddInParameter(dc, "@CustomerInfo", DbType.String, mdl.CustomerInfo);
            this.db.AddInParameter(dc, "@StartDate", DbType.DateTime, mdl.StartDate);
            this.db.AddInParameter(dc, "@StartTime", DbType.String, mdl.StartTime);
            if (mdl.EndDate != DateTime.MinValue) this.db.AddInParameter(dc, "@EndDate", DbType.DateTime, mdl.EndDate);
            else this.db.AddInParameter(dc, "@EndDate", DbType.DateTime, DBNull.Value);
            this.db.AddInParameter(dc, "@EndTime", DbType.String, mdl.EndTime);

            if (mdl.PlanGuide != null)
            {
                this.db.AddInParameter(dc, "@XMLGuide", DbType.Xml, this.GetPlanGuideXml(mdl.PlanGuide));
            }
            if (mdl.PlanHotel != null)
            {
                this.db.AddInParameter(dc, "@XMLHotel", DbType.Xml, this.GetPlanHotelXml(mdl.PlanHotel));
                if (mdl.PlanHotel.PlanHotelRoomList != null)
                {
                    this.db.AddInParameter(dc, "@XMLHotelRoom", DbType.Xml, this.GetPlanHotelRoomXml(mdl.PlanHotel.PlanHotelRoomList));
                }
            }
            if (mdl.PlanCar != null)
            {
                this.db.AddInParameter(dc, "@XMLCar", DbType.Xml, this.GetPlanCarXml(mdl.PlanCar));
            }
            if (mdl.PlanLargeTime != null && mdl.PlanLargeTime.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLLargeFrequency", DbType.Xml, this.GetPlanLargeFrequencyXml(mdl.PlanLargeTime));
            }
            if (mdl.PlanAttractions != null)
            {
                this.db.AddInParameter(dc, "@XMLAttractions", DbType.Xml, this.GetPlanAttractionsXml(mdl.PlanAttractions));
            }
            if (mdl.PlanShip != null)
            {
                this.db.AddInParameter(dc, "@XMLShip", DbType.Xml, this.GetPlanShipXml(mdl.PlanShip));
                if (mdl.PlanShip.PlanShipOwnCostList != null && mdl.PlanShip.PlanShipOwnCostList.Count > 0)
                {
                    this.db.AddInParameter(dc, "@XMLShipOwnCost", DbType.Xml, this.GetPlanShipOwnCostXml(mdl.PlanShip.PlanShipOwnCostList));
                }
                if (mdl.PlanShip.PlanShipPriceList != null && mdl.PlanShip.PlanShipPriceList.Count > 0)
                {
                    this.db.AddInParameter(dc, "@XMLShipPrice", DbType.Xml, this.GetPlanShipPriceXml(mdl.PlanShip.PlanShipPriceList));
                }
            }
            if (mdl.PlanDiningPricelist != null && mdl.PlanDiningPricelist.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLDiningPrice", DbType.Xml, this.GetPlanDiningPriceXml(mdl.PlanDiningPricelist));
            }
            if (mdl.PlanGood != null)
            {
                this.db.AddInParameter(dc, "@XMLGood", DbType.Xml, this.GetPlanGoodXml(mdl.PlanGood));
            }

            if (mdl.PlanCostChange != null && mdl.PlanCostChange.Count > 0)
            {
                this.db.AddInParameter(dc, "@XMLCostChange", DbType.Xml, this.GetPlanCostChangeXml(mdl.PlanCostChange));
            }

            this.db.AddOutParameter(dc, "@IsResult", DbType.Int32, 1);
            this.db.AddInParameter(dc, "DNum", DbType.Decimal, mdl.DNum);

            DbHelper.RunProcedure(dc, this.db);

            return Convert.ToInt32(db.GetParameterValue(dc, "IsResult"));
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
            sql.Append(" ,DNum=DNum-@TmpDNum+@DNum ");
            if (!string.IsNullOrEmpty(mdl.FeiYongMingXi))
            {
                sql.Append("                 ,CostDetail=@FeiYongMingXi ");
            }
            sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新景点成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanAttractions");
            //sql.Append("                 SET     AdultNumber = AdultNumber - @TmpNum + @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新游轮成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanShipPrice");
            //sql.Append("                 SET     AdultNumber = AdultNumber - @TmpNum + @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         ELSE ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num + @TmpNum - @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation + @TmpChangeCost");
            sql.Append("                         - @ChangeCost");
            sql.Append(" ,DNum=DNum+@TmpDNum-@DNum ");
            if (!string.IsNullOrEmpty(mdl.FeiYongMingXi))
            {
                sql.Append("                 ,CostDetail=@FeiYongMingXi ");
            }
            sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新景点成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanAttractions");
            //sql.Append("                 SET     AdultNumber = AdultNumber + @TmpNum - @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新游轮成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanShipPrice");
            //sql.Append("                 SET     AdultNumber = AdultNumber + @TmpNum - @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
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
            sql.Append(" ,DNum=DNum+@DNum ");
            if (!string.IsNullOrEmpty(mdl.FeiYongMingXi))
            {
                sql.Append("                 ,CostDetail=@FeiYongMingXi ");
            }
            sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新景点成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanAttractions");
            //sql.Append("                 SET     AdultNumber = AdultNumber + @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新游轮成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanShipPrice");
            //sql.Append("                 SET     AdultNumber = AdultNumber + @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            sql.Append("             END");
            sql.Append("         ELSE ");
            sql.Append("             BEGIN");
            sql.Append("                 UPDATE  dbo.tbl_Plan");
            sql.Append("                 SET     Num = Num - @PeopleNumber ,");
            sql.Append("                         Confirmation = Confirmation - @ChangeCost");
            sql.Append(" ,DNum=DNum-@DNum ");
            if (!string.IsNullOrEmpty(mdl.FeiYongMingXi))
            {
                sql.Append("                 ,CostDetail=@FeiYongMingXi ");
            }
            sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新景点成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanAttractions");
            //sql.Append("                 SET     AdultNumber = AdultNumber - @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
            ////更新游轮成人数
            //sql.Append("                 UPDATE  dbo.tbl_PlanShipPrice");
            //sql.Append("                 SET     AdultNumber = AdultNumber - @PeopleNumber");
            //sql.Append("                 WHERE   PlanId = @PlanId");
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
            db.AddInParameter(dc, "FeiYongMingXi", DbType.String, mdl.FeiYongMingXi);
            db.AddInParameter(dc, "DNum", DbType.Decimal, mdl.DNum);
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
            sql.Append(" DECLARE @DelGoodId CHAR(36) ,");
            sql.Append("     @DelNumber INT");
            sql.Append(" SELECT  @DelGoodId = GoodId ,");
            sql.Append("         @DelNumber = Number");
            sql.Append(" FROM    dbo.tbl_GovGoodUse");
            sql.Append(" WHERE   PlanId = @PlanId");
            sql.Append(" UPDATE  tbl_GovGood");
            sql.Append(" SET     Stock = Stock + @DelNumber");
            sql.Append(" WHERE   GoodId = @DelGoodId");
            sql.Append(" DELETE  FROM dbo.tbl_GovGoodUse");
            sql.Append(" WHERE   PlanId = @PlanId");
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

            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [CompanyId] ,");
            sql.Append("         [TourId] ,");
            sql.Append("         XMLTour=(SELECT RouteName,TourCode,SellerName FROM tbl_Tour WHERE TourId=tbl_Plan.TourId FOR XML RAW,ROOT) ,");
            sql.Append("         Approver=STUFF((SELECT ',' + Approver FROM (SELECT Approver FROM tbl_FinRegister WHERE PlanId=tbl_Plan.PlanId AND IsDeleted=0 AND ISNULL(Approver,'')<>'' GROUP BY Approver) AS A FOR XML PATH('')),1, 1,'') ,");
            sql.Append("         Accountant=STUFF((SELECT ',' + Accountant FROM (SELECT Accountant FROM tbl_FinRegister WHERE PlanId=tbl_Plan.PlanId AND IsDeleted=0 AND ISNULL(Accountant,'')<>'' GROUP BY Accountant) AS A FOR XML PATH('')),1, 1,'') ,");
            sql.Append("         [Type] ,");
            sql.Append("         [SourceId] ,");
            sql.Append("         [SourceName] ,");
            sql.Append("         [ContactName] ,");
            sql.Append("         [ContactPhone] ,");
            sql.Append("         [ContactFax] ,");
            sql.Append("         [Num] ,");
            sql.Append("         [ReceiveJourney] ,");
            //sql.Append("         [CostDetail] ,");
            sql.Append("         [PlanCost] ,");
            sql.Append("         [PaymentType] ,");
            sql.Append("         [Status] ,");
            sql.Append("         [GuideNotes] ,");
            sql.Append("         [Remarks] ,");
            //sql.Append("         [SueId] ,");
            sql.Append("         [CostId] ,");
            sql.Append("         [CostName] ,");
            sql.Append("         [CostStatus] ,");
            sql.Append("         [CostTime] ,");
            sql.Append("         [Confirmation] ,");
            sql.Append("         [CostRemarks] ,");
            sql.Append("         [OperatorDeptId] ,");
            sql.Append("         [OperatorId] ,");
            sql.Append("         [Operator] ,");
            sql.Append("         [IssueTime] ,");
            sql.Append("         [Prepaid] ,");
            //sql.Append("         [IsRebate] ,");
            sql.Append("         [AddStatus] ,");
            sql.Append("         [ServiceStandard] ,");
            //sql.Append("         [CustomerInfo] ,");
            sql.Append("         [StartDate] ,");
            sql.Append("         [StartTime] ,");
            sql.Append("         [EndDate] ,");
            sql.Append("         [EndTime]");
            sql.Append(" FROM    [dbo].[tbl_Plan]");
            sql.Append(" WHERE   PlanId = @PlanId");

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
                    mdl.Approver = dr["Approver"].ToString();
                    mdl.Accountant = dr["Accountant"].ToString();
                    mdl.Type = (PlanProject)dr.GetByte(dr.GetOrdinal("Type"));
                    mdl.SourceId = dr["SourceId"].ToString();
                    mdl.SourceName = dr["SourceName"].ToString();
                    mdl.ContactName = dr["ContactName"].ToString();
                    mdl.ContactPhone = dr["ContactPhone"].ToString();
                    mdl.ContactFax = dr["ContactFax"].ToString();
                    mdl.Num = dr.GetInt32(dr.GetOrdinal("Num"));
                    mdl.ReceiveJourney = dr["ReceiveJourney"].ToString();
                    //mdl.CostDetail = dr["CostDetail"].ToString();
                    mdl.PlanCost = dr.GetDecimal(dr.GetOrdinal("PlanCost"));
                    mdl.PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType"));
                    mdl.Status = (PlanState)dr.GetByte(dr.GetOrdinal("Status"));
                    mdl.GuideNotes = dr["GuideNotes"].ToString();
                    mdl.Remarks = dr["Remarks"].ToString();
                    //mdl.SueId = dr["SueId"].ToString();
                    mdl.CostId = dr["CostId"].ToString();
                    mdl.CostName = dr["CostName"].ToString();
                    mdl.CostStatus = dr.GetString(dr.GetOrdinal("CostStatus")) == "1";
                    if (!dr.IsDBNull(dr.GetOrdinal("CostTime")))
                    {
                        mdl.CostTime = dr.GetDateTime(dr.GetOrdinal("CostTime"));
                    }
                    mdl.Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation"));
                    mdl.CostRemarks = dr["CostRemarks"].ToString();
                    mdl.DeptId = dr.GetInt32(dr.GetOrdinal("OperatorDeptId"));
                    mdl.OperatorId = dr["OperatorId"].ToString();
                    mdl.OperatorName = dr["Operator"].ToString();
                    mdl.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    mdl.Prepaid = dr.GetDecimal(dr.GetOrdinal("Prepaid"));
                    //mdl.IsRebate = dr.GetString(dr.GetOrdinal("IsRebate")) == "1";
                    mdl.AddStatus = (PlanAddStatus)dr.GetByte(dr.GetOrdinal("AddStatus"));
                    mdl.ServiceStandard = dr["ServiceStandard"].ToString();
                    //mdl.CustomerInfo = dr["CustomerInfo"].ToString();
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
                    //mdl.DNum = dr.GetDecimal(dr.GetOrdinal("DNum"));
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
            sql.Append("         [Confirmation],PlanId,ServiceStandard ");
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
                            //CostDetail = dr["CostDetail"].ToString(),
                            PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType")),
                            Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                            PlanId = dr["PlanId"].ToString(),
                            //DNum = dr.GetDecimal(dr.GetOrdinal("DNum"))
                            ServiceStandard = dr["ServiceStandard"].ToString()
                        };
                    lst.Add(mdl);
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
                    mdl.OnLocation = dr["OnLocation"].ToString();
                    mdl.NextLocation = dr["NextLocation"].ToString();
                    mdl.TaskType = (PlanGuideTaskType)dr.GetByte(dr.GetOrdinal("TaskType"));
                }
            }
            return mdl;
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
            //sql.Append("         [MealPrice] ,");
            //sql.Append("         [MealNumber] ,");
            //sql.Append("         [MealFrequency],");
            sql.Append("         XMLHotelRoom=(");
            sql.Append("                        SELECT  [PlanId] ,");
            sql.Append("                                [RoomId] ,");
            sql.Append("                                [RoomType] ,");
            sql.Append("                                [UnitPrice] ,");
            sql.Append("                                [PriceType] ,");
            sql.Append("                                [Quantity] ,");
            sql.Append("                                [Total],Days ");
            sql.Append("                        FROM    [dbo].[tbl_PlanHotelRoom]");
            sql.Append("                        WHERE   PlanId = tbl_PlanHotel.PlanId for xml raw,root");
            sql.Append("                        )");
            sql.Append("  ");
            sql.Append(" FROM    [dbo].[tbl_PlanHotel]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    //mdl.Star = (HotelStar)dr.GetByte(dr.GetOrdinal("Star"));
                    mdl.Days = dr.GetInt32(dr.GetOrdinal("Days"));
                    mdl.FreeNumber = dr.GetInt32(dr.GetOrdinal("FreeNumber"));
                    mdl.IsMeal = (PlanHotelIsMeal)dr.GetByte(dr.GetOrdinal("IsMeal"));
                    mdl.MealPrice = dr.GetDecimal(dr.GetOrdinal("MealPrice"));
                    mdl.MealNumber = dr.GetInt32(dr.GetOrdinal("MealNumber"));
                    mdl.MealFrequency = dr.GetInt32(dr.GetOrdinal("MealFrequency"));
                    mdl.PlanHotelRoomList = this.GetPlanHotelRoomLst(dr["XMLHotelRoom"].ToString());
                    mdl.QianTaiTelephone = dr["QianTaiTelephone"].ToString();
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取用车安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanCar GetCar(string planId)
        {
            var mdl = new MPlanCar();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [CarId] ,");
            sql.Append("         [Models] ,");
            //sql.Append("         [VehicleType] ,");
            sql.Append("         [DriverPhone] ,");
            sql.Append("         [CarNumber] ,");
            sql.Append("         [Driver] ,");
            sql.Append("         SeatNumber=(SELECT SeatNumber FROM tbl_SourceCar WHERE CarId=tbl_PlanCar.CarId)");
            sql.Append(" FROM    [dbo].[tbl_PlanCar]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.CarId = dr["CarId"].ToString();
                    mdl.Models = dr["Models"].ToString();
                    //mdl.VehicleType = (PlanCarType)dr.GetByte(dr.GetOrdinal("VehicleType"));
                    mdl.DriverPhone = dr["DriverPhone"].ToString();
                    mdl.CarNumber = dr["CarNumber"].ToString();
                    mdl.Driver = dr["Driver"].ToString();
                    mdl.SeatNumber = dr.IsDBNull(dr.GetOrdinal("SeatNumber"))
                                         ? 0
                                         : dr.GetInt32(dr.GetOrdinal("SeatNumber"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取大交通班次安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanLargeTime> GetLargeTime(string planId)
        {
            var lst = new List<MPlanLargeTime>();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [DepartureTime] ,");
            sql.Append("         [Time] ,");
            sql.Append("         [Departure] ,");
            sql.Append("         [Destination] ,");
            sql.Append("         [Numbers] ,");
            sql.Append("         [SeatStandard] ,");
            sql.Append("         [PayNumber] ,");
            sql.Append("         [FreeNumber] ,");
            sql.Append("         [FarePrice], ");
            sql.Append("         [SeatType] ,");
            sql.Append("         [AdultsType] ,");
            sql.Append("         [Insurance] ,");
            sql.Append("         [Fee] ,");
            sql.Append("         [Surcharge] ,");
            sql.Append("         [Discount] ,");
            sql.Append("         [SumPrice],[BeiZhu] ");
            sql.Append(" FROM    [dbo].[tbl_PlanLargeFrequency]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    lst.Add(new MPlanLargeTime
                    {
                        PlanId = dr["PlanId"].ToString(),
                        DepartureTime = dr.GetDateTime(dr.GetOrdinal("DepartureTime")),
                        Time = dr["Time"].ToString(),
                        Departure = dr["Departure"].ToString(),
                        Destination = dr["Destination"].ToString(),
                        Numbers = dr["Numbers"].ToString(),
                        SeatStandard = dr["SeatStandard"].ToString(),
                        PayNumber = dr.GetInt32(dr.GetOrdinal("PayNumber")),
                        FreeNumber = dr.GetInt32(dr.GetOrdinal("FreeNumber")),
                        FarePrice = dr.GetDecimal(dr.GetOrdinal("FarePrice")),
                        SeatType = (PlanLargeSeatType)dr.GetByte(dr.GetOrdinal("SeatType")),
                        AdultsType = (PlanLargeAdultsType)dr.GetByte(dr.GetOrdinal("AdultsType")),
                        Insurance = dr.GetDecimal(dr.GetOrdinal("Insurance")),
                        Fee = dr.GetDecimal(dr.GetOrdinal("Fee")),
                        Surcharge = dr.GetDecimal(dr.GetOrdinal("Surcharge")),
                        Discount = float.Parse(dr["Discount"].ToString()),
                        SumPrice = dr.GetDecimal(dr.GetOrdinal("SumPrice")),
                        BeiZhu = dr["BeiZhu"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取景点安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanAttractions GetAttractions(string planId)
        {
            var mdl = new MPlanAttractions();
            var sql = new StringBuilder();
            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [AttractionsId] ,");
            sql.Append("         [Attractions] ,");
            sql.Append("         [AdultNumber] ,");
            sql.Append("         [ChildNumber]");
            sql.Append(" FROM    [dbo].[tbl_PlanAttractions]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.AttractionsId = dr["AttractionsId"].ToString();
                    mdl.Attractions = dr["Attractions"].ToString();
                    mdl.AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber"));
                    mdl.ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber"));
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取游轮安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public MPlanShip GetShip(string planId)
        {
            var mdl = new MPlanShip();
            var sql = new StringBuilder();

            mdl.PlanShipOwnCostList = new List<MPlanShipOwnCost>();
            mdl.PlanShipPriceList = new List<MPlanShipPrice>();

            sql.Append(" SELECT  [PlanId] ,");
            sql.Append("         [SubId] ,");
            sql.Append("         [ShipName] ,");
            sql.Append("         [ShipCalls] ,");
            sql.Append("         [LoadDock] ,");
            sql.Append("         [LoadCode] ,");
            sql.Append("         [Line] ,");
            sql.Append("         [Sight] ,");
            sql.Append("         XMLShipOwnCost = ( SELECT   PlanId ,");
            sql.Append("                                     OwnItem ,");
            sql.Append("                                     Price ,");
            sql.Append("                                     PeopleNum,");
            sql.Append("                                     IsFloor");
            sql.Append("                            FROM     dbo.tbl_PlanShipOwnCost");
            sql.Append("                            WHERE    PlanId = dbo.tbl_PlanShip.PlanId");
            sql.Append("                          FOR");
            sql.Append("                            XML RAW ,");
            sql.Append("                                ROOT");
            sql.Append("                          ) ,");
            sql.Append("         XMLShipPrice = ( SELECT PlanId ,");
            sql.Append("                                 RoomType ,");
            sql.Append("                                 CrowdType ,");
            sql.Append("                                 AdultNumber ,");
            sql.Append("                                 AdultPrice ,");
            sql.Append("                                 ChildNumber ,");
            sql.Append("                                 ChildPrice ,");
            sql.Append("                                 ChildNoOccupancy ,");
            sql.Append("                                 ChildNoOccupancyPrice ,");
            sql.Append("                                 BabyNumber ,");
            sql.Append("                                 BabyNumberPrice,");
            sql.Append("                                 SumPrice,BeiZhu,DNum");
            sql.Append("                          FROM   dbo.tbl_PlanShipPrice");
            sql.Append("                          WHERE  PlanId = dbo.tbl_PlanShip.PlanId");
            sql.Append("                        FOR");
            sql.Append("                          XML RAW ,");
            sql.Append("                              ROOT");
            sql.Append("                        )");
            sql.Append(" FROM    [dbo].[tbl_PlanShip]");
            sql.Append(" WHERE   PlanId = @PlanId");
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.PlanId = dr["PlanId"].ToString();
                    mdl.SubId = dr["SubId"].ToString();
                    mdl.ShipName = dr["ShipName"].ToString();
                    mdl.ShipCalls = dr["ShipCalls"].ToString();
                    mdl.LoadDock = dr["LoadDock"].ToString();
                    mdl.LoadCode = dr["LoadCode"].ToString();
                    mdl.Line = dr["Line"].ToString();
                    mdl.Sight = dr["Sight"].ToString();
                    mdl.PlanShipOwnCostList = this.GetPlanShipOwnCostLst(dr["XMLShipOwnCost"].ToString());
                    mdl.PlanShipPriceList = this.GetPlanShipPriceLst(dr["XMLShipPrice"].ToString());
                }
            }
            return mdl;
        }

        /// <summary>
        /// 获取用餐价格安排
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public IList<MPlanDiningPrice> GetDining(string planId)
        {
            var lst = new List<MPlanDiningPrice>();
            var sql = new StringBuilder();

            sql.Append(" SELECT   [PlanId] ,");
            sql.Append("          [Pricetyp] ,");
            sql.Append("          [IsContainB] ,");
            sql.Append("          [TimeB] ,");
            sql.Append("          [PeopleB] ,");
            sql.Append("          [PriceB] ,");
            sql.Append("          [IsContainL] ,");
            sql.Append("          [TimeL] ,");
            sql.Append("          [PeopleL] ,");
            sql.Append("          [PriceL] ,");
            sql.Append("          [IsContainS] ,");
            sql.Append("          [TimeS] ,");
            sql.Append("          [PeopleS] ,");
            sql.Append("          [PriceS]");
            sql.Append(" FROM     dbo.tbl_PlanDiningPrice");
            sql.Append(" WHERE   PlanId = @PlanId");

            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@PlanId", DbType.AnsiStringFixedLength, planId);

            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    lst.Add(new MPlanDiningPrice
                        {
                            PlanId = dr["PlanId"].ToString(),
                            Pricetyp = (PlanLargeAdultsType)dr.GetByte(dr.GetOrdinal("Pricetyp")),
                            IsContainB = dr.GetString(dr.GetOrdinal("IsContainB")) == "1",
                            TimeB = dr.GetInt32(dr.GetOrdinal("TimeB")),
                            PeopleB = dr.GetInt32(dr.GetOrdinal("PeopleB")),
                            PriceB = dr.GetDecimal(dr.GetOrdinal("PriceB")),
                            IsContainL = dr.GetString(dr.GetOrdinal("IsContainL")) == "1",
                            TimeL = dr.GetInt32(dr.GetOrdinal("TimeL")),
                            PeopleL = dr.GetInt32(dr.GetOrdinal("PeopleL")),
                            PriceL = dr.GetDecimal(dr.GetOrdinal("PriceL")),
                            IsContainS = dr.GetString(dr.GetOrdinal("IsContainS")) == "1",
                            TimeS = dr.GetInt32(dr.GetOrdinal("TimeS")),
                            PeopleS = dr.GetInt32(dr.GetOrdinal("PeopleS")),
                            PriceS = dr.GetDecimal(dr.GetOrdinal("PriceS")),
                        });
                }
            }
            return lst;
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
        /// <param name="isShowQiTaShouZhi">是否显示其他收支</param>
        /// <returns>计调项结算金额</returns>
        public decimal GetPlanCost(Payment payment, string tourId, bool isShowQiTaShouZhi)
        {
            var sql = new StringBuilder("SELECT GuideOutPay=(select isnull(sum(Confirmation),0) from tbl_plan where tourid=@tourid and PaymentType=@payment and IsDelete='0')");
            if (isShowQiTaShouZhi)
            {
                sql.AppendFormat(" +(select isnull(sum(FeeAmount),0) from tbl_FinOtherOutFee where tourid=@tourid and IsDeleted='0' and ISNULL(PayTypeName,(SELECT Name FROM tbl_ComPayment WHERE PaymentId=PayType))='{0}')", payment);
            }
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
        /// <param name="tourId">计调编号</param>
        /// <returns></returns>
        public IList<MPlan> GetList(PlanProject planType, Payment? payment, PlanAddStatus? addStatus, bool isShowCostChange, PlanChangeChangeClass? changeType, string tourId)
        {
            var lst = new List<MPlan>();
            var sql = new StringBuilder();

            sql.Append(" SELECT  P.PlanId ,");
            sql.Append("         SourceId ,");
            sql.Append("         SourceName ,");
            sql.Append("         P.ContactName Contact,");
            sql.Append("         ContactPhone ,");
            sql.Append("         StartDate ,");
            sql.Append("         EndDate ,");
            sql.Append("         StartTime ,");
            sql.Append("         EndTime ,");
            if (planType == PlanProject.酒店)
            {
                sql.Append("         ISNULL(H.FreeNumber,0) FreeNumber ,");
            }
            if (planType == PlanProject.用车)
            {
                sql.Append("         C.Models ,");
            }
            if (planType == PlanProject.火车 || planType == PlanProject.汽车 || planType == PlanProject.飞机)
            {
                sql.Append("         XMLTrainBus = ( SELECT    ISNULL(SUM(T.FreeNumber),0)FreeNumber,CONVERT(VARCHAR(19),MIN(T.DepartureTime),120) DepartureTime");
                sql.Append("                   FROM      dbo.tbl_PlanLargeFrequency T");
                sql.Append("                   WHERE     T.PlanId = P.PlanId");
                sql.Append("                   FOR");
                sql.Append("                     XML RAW ,");
                sql.Append("                         ROOT");
                sql.Append("                 ) ,");
            }
            if (planType == PlanProject.景点)
            {
                sql.Append("         ISNULL(S.AdultNumber,0) AdultNumber ,");
                sql.Append("         ISNULL(S.ChildNumber,0) ChildNumber ,");
            }
            //if (planType == PlanProject.国内游轮 || planType == PlanProject.涉外游轮)
            //{
            //    sql.Append("         I.ShipName ,");
            //    sql.Append("         XMLShip = ( SELECT  ISNULL(SUM(R.AdultNumber),0) AdultNumber ,");
            //    sql.Append("                             ISNULL(SUM(R.ChildNumber),0) ChildNumber");
            //    sql.Append("                     FROM    dbo.tbl_PlanShipPrice R");
            //    sql.Append("                     WHERE   R.PlanId = I.PlanId");
            //    sql.Append("                   FOR");
            //    sql.Append("                     XML RAW ,");
            //    sql.Append("                         ROOT");
            //    sql.Append("                   ) ,");
            //}
            if (planType == PlanProject.用餐)
            {
                sql.Append("         Dining = ( SELECT   ISNULL(SUM(E.PeopleB + E.PeopleL + E.PeopleS),0)");
                sql.Append("                    FROM     dbo.tbl_PlanDiningPrice E");
                sql.Append("                    WHERE    E.PlanId = P.PlanId");
                sql.Append("                  ) ,");
            }
            if (planType == PlanProject.领料)
            {
                sql.Append("         ISNULL(G.Price,0) Price,G.UserId ,ContactName,G.GoodId,");
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
            sql.Append("         Num ,");
            //sql.Append("         CostDetail ,");
            sql.Append("         PaymentType ,");
            sql.Append("         Status ,");
            sql.Append("         PlanCost ,");
            sql.Append("         Confirmation ,");
            sql.Append("         AddStatus ,");
            sql.Append("         ServiceStandard,");
            sql.Append("         P.OperatorId,");
            //sql.Append("         P.DNum,");
            sql.Append("         TourId");
            sql.Append(" FROM    dbo.tbl_Plan P");
            if (planType == PlanProject.酒店)
            {
                sql.Append("         LEFT JOIN dbo.tbl_PlanHotel H ON P.PlanId = H.PlanId");
            }
            if (planType == PlanProject.用车)
            {
                sql.Append("         LEFT JOIN dbo.tbl_PlanCar C ON C.PlanId = P.PlanId");
            }
            if (planType == PlanProject.景点)
            {
                sql.Append("         LEFT JOIN dbo.tbl_PlanAttractions S ON P.PlanId = S.PlanId");
            }
            //if (planType == PlanProject.国内游轮 || planType == PlanProject.涉外游轮)
            //{
            //    sql.Append("         LEFT JOIN dbo.tbl_PlanShip I ON P.PlanId = I.PlanId");
            //}
            if (planType == PlanProject.领料)
            {
                sql.Append("         LEFT JOIN dbo.tbl_GovGoodUse G ON P.PlanId = G.PlanId");
            }
            sql.Append(" WHERE   TourId = @TourId");
            sql.Append("         AND Type = @Type");
            if (payment.HasValue)
            {
                sql.Append("         AND PaymentType = @Payment");
            }
            if (addStatus.HasValue)
            {
                sql.Append("         AND AddStatus = @AddStatus");
            }
            sql.Append("         AND IsDelete = '0'");

            var dc = this.db.GetSqlStringCommand(sql.ToString());

            this.db.AddInParameter(dc, "@TourId", DbType.AnsiStringFixedLength, tourId);
            this.db.AddInParameter(dc, "@Type", DbType.Byte, (int)planType);
            if (payment.HasValue)
            {
                this.db.AddInParameter(dc, "@Payment", DbType.Byte, (int)payment.Value);
            }
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
                            SourceId = dr["SourceId"].ToString(),
                            SourceName = dr["SourceName"].ToString(),
                            //CostDetail = dr["CostDetail"].ToString(),
                            PlanCost = dr.GetDecimal(dr.GetOrdinal("PlanCost")),
                            Confirmation = dr.GetDecimal(dr.GetOrdinal("Confirmation")),
                            PaymentType = (Payment)dr.GetByte(dr.GetOrdinal("PaymentType")),
                            Status = (PlanState)dr.GetByte(dr.GetOrdinal("Status")),
                            StartTime = dr["StartTime"].ToString(),
                            EndTime = dr["EndTime"].ToString(),
                            ContactName = dr["Contact"].ToString(),
                            ContactPhone = dr["ContactPhone"].ToString(),
                            ServiceStandard = dr["ServiceStandard"].ToString(),
                            Num = dr.GetInt32(dr.GetOrdinal("Num")),
                            AddStatus = (PlanAddStatus)dr.GetByte(dr.GetOrdinal("AddStatus")),
                            OperatorId = dr["OperatorId"].ToString()
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
                        case PlanProject.酒店:
                            mdl.FreeNumber = dr.GetInt32(dr.GetOrdinal("FreeNumber"));
                            break;
                        case PlanProject.用车:
                            mdl.Models = dr["Models"].ToString();
                            break;
                        case PlanProject.飞机:
                        case PlanProject.火车:
                        case PlanProject.汽车:
                            if (mdl.Num == 0)
                            {
                                //mdl.Num =
                                //    Utils.GetInt(
                                //        Utils.GetValueFromXmlByAttribute(dr["XMLTrainBus"].ToString(), "PayNumber"));
                            }
                            mdl.FreeNumber =
                                Utils.GetInt(
                                    Utils.GetValueFromXmlByAttribute(dr["XMLTrainBus"].ToString(), "FreeNumber"));
                            mdl.PlanLargeTime = new List<MPlanLargeTime>
                                {
                                    new MPlanLargeTime
                                        {
                                            DepartureTime =
                                                Utils.GetDateTime(
                                                    Utils.GetValueFromXmlByAttribute(
                                                        dr["XMLTrainBus"].ToString(), "DepartureTime"))
                                        }
                                };
                            break;
                        case PlanProject.景点:
                            mdl.AdultNumber = dr.GetInt32(dr.GetOrdinal("AdultNumber"));
                            mdl.ChildNumber = dr.GetInt32(dr.GetOrdinal("ChildNumber"));
                            break;
                        //case PlanProject.涉外游轮:
                        //case PlanProject.国内游轮:
                        //    mdl.ShipName = dr["ShipName"].ToString();
                        //    mdl.AdultNumber = Utils.GetInt(
                        //            Utils.GetValueFromXmlByAttribute(dr["XMLShip"].ToString(), "AdultNumber"));
                        //    mdl.ChildNumber = Utils.GetInt(
                        //            Utils.GetValueFromXmlByAttribute(dr["XMLShip"].ToString(), "ChildNumber"));
                        //    break;
                        case PlanProject.用餐:
                            if (mdl.Num == 0)
                            {
                                //mdl.Num = dr.GetInt32(dr.GetOrdinal("Dining"));
                            }
                            break;
                        case PlanProject.领料:
                            if (!dr.IsDBNull(dr.GetOrdinal("Price")))
                            {
                                mdl.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                            }
                            mdl.ContactName = dr["ContactName"].ToString();
                            mdl.PlanGood = new MGovGoodUse { UserId = dr["UserId"].ToString(), GoodId = dr["GoodId"].ToString() };
                            break;
                    }
                    //mdl.DNum = dr.GetDecimal(dr.GetOrdinal("DNum"));
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

            /*sql.Append(" SELECT  STUFF(( SELECT  '\n' + ReceiveJourney");
            sql.Append("                 FROM    ( SELECT    ReceiveJourney");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type = @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                           UNION ALL");
            sql.Append("                           SELECT    ''");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type <> @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                         ) AS A");
            sql.Append("                 WHERE   ISNULL(A.ReceiveJourney, '') <> ''");
            sql.Append("               FOR");
            sql.Append("                 XML PATH('')");
            sql.Append("               ), 1, 1, '') N'接待行程' ,");
            sql.Append("         STUFF(( SELECT  '\n' + ServiceStandard");
            sql.Append("                 FROM    ( SELECT    ServiceStandard");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type = @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                           UNION ALL");
            sql.Append("                           SELECT    ''");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type <> @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                         ) AS A");
            sql.Append("                 WHERE   ISNULL(A.ServiceStandard, '') <> ''");
            sql.Append("               FOR");
            sql.Append("                 XML PATH('')");
            sql.Append("               ), 1, 1, '') N'服务标准' ,");
            sql.Append("         STUFF(( SELECT  '\n' + GuideNotes");
            sql.Append("                 FROM    ( SELECT    GuideNotes");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type = @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                           UNION ALL");
            sql.Append("                           SELECT    GuideNotes");
            sql.Append("                           FROM      dbo.tbl_Plan");
            sql.Append("                           WHERE     TourId = @TourId");
            sql.Append("                                     AND Type <> @Type");
            sql.Append("                                     AND IsDelete = 0");
            sql.Append("                                     AND Status = @Status");
            sql.Append("                         ) AS A");
            sql.Append("                 WHERE   ISNULL(A.GuideNotes, '') <> ''");
            sql.Append("               FOR");
            sql.Append("                 XML PATH('')");
            sql.Append("               ), 1, 1, '') N'导游须知'");*/

            sql.Append(" SELECT ");
            sql.Append(" (SELECT ReceiveJourney AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status AND Type=@Type FOR XML RAW,ROOT('root') ) N'接待行程' ");
            sql.Append(" ,(SELECT ServiceStandard AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status AND Type=@Type FOR XML RAW,ROOT('root') ) N'服务标准' ");
            sql.Append(" ,(SELECT GuideNotes AS C FROM tbl_Plan WHERE TourId=@TourId AND IsDelete='0' AND Status=@Status AND Type<>@Type FOR XML RAW,ROOT('root') ) N'导游须知' ");

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
            sql.Append("         AND IsDeleted = '0' and isnull(planid,'')=''");
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
        /// 报销报账时根据团队编号获取该团其他支出列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>其他支出列表</returns>
        public IList<MOtherFeeInOut> GetOtherOutpay(string tourId)
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
            sql.Append(" FROM    dbo.tbl_FinOtherOutFee");
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
        /// <param name="tourid">团队编号</param>
        /// <param name="isShowQiTaShouZhi">是否显示其他收支</param>
        /// <returns></returns>
        public MBZHZ GetBZHZ(string tourid, bool isShowQiTaShouZhi)
        {
            var mdl = new MBZHZ();
            var sql = new StringBuilder();
            sql.AppendFormat(" select GuideIncome=(select isnull(sum(GuideRealIncome),0) from tbl_TourOrder where tourid=@tourid and OrderStatus={0} AND IsDelete = '0')", (int)OrderStatus.已成交);
            if (isShowQiTaShouZhi)
            {
                sql.AppendFormat(" +(select isnull(sum(FeeAmount),0) from tbl_FinOtherInFee where tourid=@tourid and IsDeleted='0' and ISNULL(PayTypeName,(SELECT Name FROM tbl_ComPayment WHERE PaymentId=PayType))='导游现收')", (int)FinStatus.账务待支付);
            }
            sql.AppendFormat(" ,XMLGuide=(select isnull(sum(RealAmount),0) RealAmount,isnull(sum(RelSignNum),0) RelSignNum from tbl_FinDebit where tourid=@tourid and IsDeleted='0' and status={0} for xml raw,root)", (int)FinStatus.账务已支付);
            var dc = this.db.GetSqlStringCommand(sql.ToString());
            this.db.AddInParameter(dc, "@tourid", DbType.AnsiStringFixedLength, tourid);
            using (var dr = DbHelper.ExecuteReader(dc, this.db))
            {
                while (dr.Read())
                {
                    mdl.GuideIncome = dr.GetDecimal(dr.GetOrdinal("GuideIncome"));
                    mdl.GuideBorrow = Utils.GetDecimal(Utils.GetValueFromXmlByAttribute(dr["XMLGuide"].ToString(), "RealAmount"));
                    mdl.GuideOutlay = this.GetPlanCost(Payment.现付, tourid, isShowQiTaShouZhi);
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

            sql.Append(" SELECT  TourIncome = ( SELECT   ISNULL(SUM(ConfirmMoney), 0)");
            sql.Append("                        FROM     dbo.tbl_TourOrder");
            sql.Append("                        WHERE    TourId = dbo.tbl_Tour.TourId");
            sql.Append("                                 AND OrderStatus = @OrderStatus");
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
        #endregion

        #region 判断预控总使用数量是否大于预控数量
        /// <summary>
        /// 判断总使用数量是否大于预控数量(修改时要根据计调编号得到其使用数)
        /// </summary>
        /// <param name="SureId">预控编号</param>
        /// <param name="Type">计调项目类型</param>
        /// <param name="PlanId">计调编号</param>
        /// <param name="UsedNum">当前计调项使用的预控数量</param>
        /// <returns>剩余数</returns>
        public int GetControlSYNumById(string SureId, PlanProject Type, string PlanId, ref int UsedNum)
        {
            string sql = string.Empty;
            int SYNum = 0;
            switch (Type)
            {
                case PlanProject.酒店:
                    {
                        sql = "select isnull(ControlNum-AlreadyNum,0) as SYNum,0 as UsedNum from view_SourceSueHotel where Id=@SureId";
                        if (!string.IsNullOrEmpty(PlanId))
                        {
                            sql = string.Format("select isnull(ControlNum-AlreadyNum,0) as SYNum,(select Num from tbl_Plan where PlanId='{0}' and SueId='{1}' and IsDelete='0') as UsedNum from view_SourceSueHotel where Id=@SureId", PlanId, SureId);
                        }
                        break;
                    }
                case PlanProject.用车:
                    {
                        sql = "select isnull(ControlNum-AlreadyNum,0) as SYNum,0 as UsedNum  from view_SourceSueCar where Id=@SureId";
                        if (!string.IsNullOrEmpty(PlanId))
                        {
                            sql = string.Format("select isnull(ControlNum-AlreadyNum,0) as SYNum,(select Num from tbl_Plan where PlanId='{0}' and SueId='{1}' and IsDelete='0') as UsedNum from view_SourceSueCar where Id=@SureId", PlanId, SureId);
                        }
                        break;
                    }
                case PlanProject.景点:
                    sql = "select isnull(ControlNum-AlreadyNum,0) as SYNum,0 as UsedNum  from view_SourceSueSight where Id=@SureId";
                    if (!string.IsNullOrEmpty(PlanId))
                    {
                        sql = string.Format("select isnull(ControlNum-AlreadyNum,0) as SYNum,(select Num from tbl_Plan where PlanId='{0}' and SueId='{1}' and IsDelete='0') as UsedNum from view_SourceSueSight where Id=@SureId", PlanId, SureId);
                    }
                    break;
                case PlanProject.其它:
                    sql = "select isnull(ControlNum-AlreadyNum,0) as SYNum,0 as UsedNum  from view_SourceSueOther where Id=@SureId";
                    if (!string.IsNullOrEmpty(PlanId))
                    {
                        sql = string.Format("select isnull(ControlNum-AlreadyNum,0) as SYNum,(select Num from tbl_Plan where PlanId='{0}' and SueId='{1}' and IsDelete='0') as UsedNum from view_SourceSueOther where Id=@SureId", PlanId, SureId);
                    }
                    break;
            }
            DbCommand cmd = this.db.GetSqlStringCommand(sql);
            this.db.AddInParameter(cmd, "SureId", DbType.AnsiStringFixedLength, SureId);
            using (var dr = DbHelper.ExecuteReader(cmd, this.db))
            {
                if (dr.Read())
                {
                    UsedNum = dr.IsDBNull(dr.GetOrdinal("UsedNum")) ? 0 : dr.GetInt32(dr.GetOrdinal("UsedNum"));
                    SYNum = dr.IsDBNull(dr.GetOrdinal("SYNum")) ? 0 : dr.GetInt32(dr.GetOrdinal("SYNum"));
                }
            }
            return SYNum;
        }
        #endregion

        #region 私有方法

        #region 根据XML获到计划计调员
        /// <summary>
        /// 根据XML获到计划计调员
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.MTourPlaner> GetTourPlanerByXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return null;
            IList<EyouSoft.Model.TourStructure.MTourPlaner> list = new List<EyouSoft.Model.TourStructure.MTourPlaner>();
            EyouSoft.Model.TourStructure.MTourPlaner item = null;
            XElement xRoot = XElement.Parse(xml);
            var xRows = Utils.GetXElements(xRoot, "root");
            foreach (var xRow in xRows)
            {
                item = new EyouSoft.Model.TourStructure.MTourPlaner()
                {
                    Planer = Utils.GetXAttributeValue(xRow, "Planer"),
                    PlanerId = Utils.GetXAttributeValue(xRow, "PlanerId")
                };
                list.Add(item);
            }
            return list;
        }
        #endregion

        #region 根据XML获取计调供应商列表
        /// <summary>
        /// 获取计调供应商列表
        /// </summary>
        /// <param name="xml">计调供应商XML</param>
        /// <returns>计调供应商列表</returns>
        private IList<string> GetPlanSourceLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => Utils.GetXAttributeValue(i, "SourceName")).ToList();
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

        #region 根据XML获取导游实体
        /// <summary>
        /// 获取导游实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>导游实体</returns>
        private MPlanGuide GetPlanGuideMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MPlanGuide
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                OnLocation = Utils.GetXAttributeValue(x, "OnLocation"),
                NextLocation = Utils.GetXAttributeValue(x, "NextLocation"),
                TaskType = (PlanGuideTaskType)Utils.GetInt(Utils.GetXAttributeValue(x, "TaskType"))
            };
        }
        #endregion

        #region 根据XML获取酒店实体
        /// <summary>
        /// 获取酒店实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>酒店实体</returns>
        private MPlanHotel GetPlanHotelMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MPlanHotel
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                //Star = (HotelStar)Utils.GetInt(Utils.GetXAttributeValue(x, "Star")),
                Days = Utils.GetInt(Utils.GetXAttributeValue(x, "Days")),
                FreeNumber = Utils.GetInt(Utils.GetXAttributeValue(x, "FreeNumber")),
                IsMeal = (PlanHotelIsMeal)Utils.GetInt(Utils.GetXAttributeValue(x, "IsMeal")),
                MealPrice = Utils.GetDecimal(Utils.GetXAttributeValue(x, "MealPrice")),
                MealNumber = Utils.GetInt(Utils.GetXAttributeValue(x, "MealNumber")),
                MealFrequency = Utils.GetInt(Utils.GetXAttributeValue(x, "MealFrequency"))
            };
        }
        #endregion

        #region 根据XML获取酒店房型列表
        /// <summary>
        /// 获取酒店房型列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>酒店房型列表</returns>
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
                Quantity = Utils.GetInt(Utils.GetXAttributeValue(i, "Quantity")),
                Days = Utils.GetDouble(Utils.GetXAttributeValue(i, "Days")),
                CheckInDate = Utils.GetDateTimeNullable(Utils.GetXAttributeValue(i, "CheckInDate")),
                CheckOutDate = Utils.GetDateTimeNullable(Utils.GetXAttributeValue(i, "CheckOutDate"))
            }).ToList();
        }
        #endregion

        #region 根据XML获取用车实体
        /// <summary>
        /// 获取用车实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>用车实体</returns>
        private MPlanCar GetPlanCarMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MPlanCar
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                CarId = Utils.GetXAttributeValue(x, "CarId"),
                Models = Utils.GetXAttributeValue(x, "Models"),
                VehicleType = (PlanCarType)Utils.GetInt(Utils.GetXAttributeValue(x, "VehicleType")),
                DriverPhone = Utils.GetXAttributeValue(x, "DriverPhone"),
                CarNumber = Utils.GetXAttributeValue(x, "CarNumber"),
                Driver = Utils.GetXAttributeValue(x, "Driver"),
            };
        }
        #endregion

        #region 根据XML获取大交通班次列表
        /// <summary>
        /// 获取大交通班次列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>大交通班次列表</returns>
        private IList<MPlanLargeTime> GetPlanLargeFrequencyLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanLargeTime
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                DepartureTime = Utils.GetDateTime(Utils.GetXAttributeValue(i, "DepartureTime")),
                Time = Utils.GetXAttributeValue(i, "Time"),
                Departure = Utils.GetXAttributeValue(i, "Departure"),
                Destination = Utils.GetXAttributeValue(i, "Destination"),
                Numbers = Utils.GetXAttributeValue(i, "Numbers"),
                SeatStandard = Utils.GetXAttributeValue(i, "SeatStandard"),
                PayNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "PayNumber")),
                FreeNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "FreeNumber")),
                FarePrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "FarePrice")),
                SeatType = (PlanLargeSeatType)Utils.GetInt(Utils.GetXAttributeValue(i, "SeatType")),
                AdultsType = (PlanLargeAdultsType)Utils.GetInt(Utils.GetXAttributeValue(i, "AdultsType")),
                Insurance = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Insurance")),
                Fee = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Fee")),
                Surcharge = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Surcharge")),
                Discount = float.Parse(Utils.GetXAttributeValue(i, "Discount")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice")),
            }).ToList();
        }
        #endregion

        #region 根据XML获取景点实体
        /// <summary>
        /// 获取景点实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>景点实体</returns>
        private MPlanAttractions GetPlanAttractionsMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MPlanAttractions
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                Attractions = Utils.GetXAttributeValue(x, "Attractions"),
                AdultNumber = Utils.GetInt(Utils.GetXAttributeValue(x, "AdultNumber")),
                ChildNumber = Utils.GetInt(Utils.GetXAttributeValue(x, "ChildNumber")),
            };
        }
        #endregion

        #region 根据XML获取游轮实体
        /// <summary>
        /// 获取游轮实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>游轮实体</returns>
        private MPlanShip GetPlanShipMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MPlanShip
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                SubId = Utils.GetXAttributeValue(x, "SubId"),
                ShipName = Utils.GetXAttributeValue(x, "ShipName"),
                ShipCalls = Utils.GetXAttributeValue(x, "ShipCalls"),
                LoadDock = Utils.GetXAttributeValue(x, "LoadDock"),
                LoadCode = Utils.GetXAttributeValue(x, "LoadCode"),
                Line = Utils.GetXAttributeValue(x, "Line"),
                Sight = Utils.GetXAttributeValue(x, "Sight"),
            };
        }
        #endregion

        #region 根据XML获取游轮自费/楼层项目列表
        /// <summary>
        /// 获取游轮自费/楼层项目列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>游轮自费/楼层项目列表</returns>
        private IList<MPlanShipOwnCost> GetPlanShipOwnCostLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanShipOwnCost
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                OwnItem = Utils.GetXAttributeValue(i, "OwnItem"),
                Price = Utils.GetDecimal(Utils.GetXAttributeValue(i, "Price")),
                PeopleNum = Utils.GetInt(Utils.GetXAttributeValue(i, "PeopleNum")),
                IsFloor = Utils.GetXAttributeValue(i, "IsFloor") == "1",
            }).ToList();
        }
        #endregion

        #region 根据XML获取游轮价格列表
        /// <summary>
        /// 获取游轮价格列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>游轮价格列表</returns>
        private IList<MPlanShipPrice> GetPlanShipPriceLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanShipPrice
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                RoomType = (PlanShipRoomType)Utils.GetInt(Utils.GetXAttributeValue(i, "RoomType")),
                CrowdType = (PlanShipCrowdType)Utils.GetInt(Utils.GetXAttributeValue(i, "CrowdType")),
                AdultNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "AdultNumber")),
                AdultPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "AdultPrice")),
                ChildNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "ChildNumber")),
                ChildPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "ChildPrice")),
                ChildNoOccupancy = Utils.GetInt(Utils.GetXAttributeValue(i, "ChildNoOccupancy")),
                ChildNoOccupancyPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "ChildNoOccupancyPrice")),
                BabyNumber = Utils.GetInt(Utils.GetXAttributeValue(i, "BabyNumber")),
                BabyNumberPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "BabyNumberPrice")),
                SumPrice = Utils.GetDecimal(Utils.GetXAttributeValue(i, "SumPrice")),
                BeiZhu = Utils.GetXAttributeValue(i, "BeiZhu"),
                DNum = Utils.GetDecimal(Utils.GetXAttributeValue(i, "DNum"))
            }).ToList();
        }
        #endregion

        #region 根据XML获取用餐价格列表
        /// <summary>
        /// 获取用餐价格列表
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>用餐价格列表</returns>
        private IList<MPlanDiningPrice> GetPlanDiningPriceLst(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MPlanDiningPrice
            {
                PlanId = Utils.GetXAttributeValue(i, "PlanId"),
                Pricetyp = (PlanLargeAdultsType)Utils.GetInt(Utils.GetXAttributeValue(i, "Pricetyp")),
                IsContainB = bool.Parse(Utils.GetXAttributeValue(i, "IsContainB")),
                TimeB = Utils.GetInt(Utils.GetXAttributeValue(i, "TimeB")),
                PeopleB = Utils.GetInt(Utils.GetXAttributeValue(i, "PeopleB")),
                PriceB = Utils.GetDecimal(Utils.GetXAttributeValue(i, "PriceB")),
                IsContainL = bool.Parse(Utils.GetXAttributeValue(i, "IsContainL")),
                TimeL = Utils.GetInt(Utils.GetXAttributeValue(i, "TimeL")),
                PeopleL = Utils.GetInt(Utils.GetXAttributeValue(i, "PeopleL")),
                PriceL = Utils.GetDecimal(Utils.GetXAttributeValue(i, "PriceL")),
                IsContainS = bool.Parse(Utils.GetXAttributeValue(i, "IsContainS")),
                TimeS = Utils.GetInt(Utils.GetXAttributeValue(i, "TimeS")),
                PeopleS = Utils.GetInt(Utils.GetXAttributeValue(i, "PeopleS")),
                PriceS = Utils.GetDecimal(Utils.GetXAttributeValue(i, "PriceS")),
            }).ToList();
        }
        #endregion

        #region 根据XML获取领料实体
        /// <summary>
        /// 获取领料实体
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>领料实体</returns>
        private MGovGoodUse GetPlanGoodMdl(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            return new MGovGoodUse
            {
                PlanId = Utils.GetXAttributeValue(x, "PlanId"),
                CompanyId = Utils.GetXAttributeValue(x, "CompanyId"),
                GoodId = Utils.GetXAttributeValue(x, "GoodId"),
                Type = (GoodUseType)Utils.GetInt(Utils.GetXAttributeValue(x, "Type")),
                DeptId = Utils.GetInt(Utils.GetXAttributeValue(x, "DeptId")),
                Number = Utils.GetInt(Utils.GetXAttributeValue(x, "Number")),
                UserId = Utils.GetXAttributeValue(x, "UserId"),
                Price = Utils.GetDecimal(Utils.GetXAttributeValue(x, "Price")),
                OperatorId = Utils.GetXAttributeValue(x, "OperatorId"),
            };
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
                //sb.AppendFormat(" Star='{0}'", (int)mdl.Star);
                sb.AppendFormat(" Days='{0}'", mdl.Days);
                sb.AppendFormat(" FreeNumber='{0}'", mdl.FreeNumber);
                sb.AppendFormat(" IsMeal='{0}'", (int)(mdl.IsMeal));
                sb.AppendFormat(" MealPrice='{0}'", mdl.MealPrice);
                sb.AppendFormat(" MealNumber='{0}'", mdl.MealNumber);
                sb.AppendFormat(" MealFrequency='{0}'", mdl.MealFrequency);
                sb.AppendFormat(" QianTaiTelephone='{0}' ", mdl.QianTaiTelephone);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将酒店房屋类型转成XML数据
        /// <summary>
        /// 将酒店房屋类型转成XML数据
        /// </summary>
        /// <param name="lst">酒店房屋类型列表</param>
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
                    sb.AppendFormat(" Days='{0}'", i.Days);
                    if (i.CheckInDate.HasValue)
                    {
                        sb.AppendFormat(" CheckInDate='{0}' ", i.CheckInDate.Value);
                    }
                    if (i.CheckOutDate.HasValue)
                    {
                        sb.AppendFormat(" CheckOutDate='{0}' ", i.CheckOutDate.Value);
                    }
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
        private string GetPlanCarXml(MPlanCar mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" CarId='{0}'", mdl.CarId);
                sb.AppendFormat(" Models='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.Models));
                sb.AppendFormat(" VehicleType='{0}'", (int)mdl.VehicleType);
                sb.AppendFormat(" DriverPhone='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.DriverPhone));
                sb.AppendFormat(" CarNumber='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.CarNumber));
                sb.AppendFormat(" Driver='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.Driver));
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将大交通班次转成XML数据
        /// <summary>
        /// 将大交通班次转成XML数据
        /// </summary>
        /// <param name="lst">大交通班次列表</param>
        /// <returns></returns>
        private string GetPlanLargeFrequencyXml(IList<MPlanLargeTime> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" DepartureTime='{0}'", i.DepartureTime);
                    sb.AppendFormat(" Time='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Time));
                    sb.AppendFormat(" Departure='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Departure));
                    sb.AppendFormat(" Destination='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Destination));
                    sb.AppendFormat(" Numbers='{0}'", Utils.ReplaceXmlSpecialCharacter(i.Numbers));
                    sb.AppendFormat(" SeatStandard='{0}'", Utils.ReplaceXmlSpecialCharacter(i.SeatStandard));
                    sb.AppendFormat(" PayNumber='{0}'", i.PayNumber);
                    sb.AppendFormat(" FreeNumber='{0}'", i.FreeNumber);
                    sb.AppendFormat(" FarePrice='{0}'", i.FarePrice);
                    sb.AppendFormat(" SeatType='{0}'", (int)i.SeatType);
                    sb.AppendFormat(" AdultsType='{0}'", (int)i.AdultsType);
                    sb.AppendFormat(" Insurance='{0}'", i.Insurance);
                    sb.AppendFormat(" Fee='{0}'", i.Fee);
                    sb.AppendFormat(" Surcharge='{0}'", i.Surcharge);
                    sb.AppendFormat(" Discount='{0}'", i.Discount);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat(" BeiZhu='{0}' ", Utils.ReplaceXmlSpecialCharacter(i.BeiZhu));
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
        private string GetPlanAttractionsXml(MPlanAttractions mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" AttractionsId='{0}'", mdl.AttractionsId);
                sb.AppendFormat(" Attractions='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.Attractions));
                sb.AppendFormat(" AdultNumber='{0}'", mdl.AdultNumber);
                sb.AppendFormat(" ChildNumber='{0}'", mdl.ChildNumber);
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将游轮实体转成XML数据
        /// <summary>
        /// 将游轮转成XML数据
        /// </summary>
        /// <param name="mdl">游轮实体</param>
        /// <returns></returns>
        private string GetPlanShipXml(MPlanShip mdl)
        {
            var strXml = string.Empty;
            if (mdl != null)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                sb.AppendFormat("<row PlanId='{0}'", mdl.PlanId);
                sb.AppendFormat(" SubId='{0}'", mdl.SubId);
                sb.AppendFormat(" ShipName='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.ShipName));
                sb.AppendFormat(" ShipCalls='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.ShipCalls));
                sb.AppendFormat(" LoadDock='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.LoadDock));
                sb.AppendFormat(" LoadCode='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.LoadCode));
                sb.AppendFormat(" Line='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.Line));
                sb.AppendFormat(" Sight='{0}'", Utils.ReplaceXmlSpecialCharacter(mdl.Sight));
                sb.AppendFormat("/>");
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将游轮自费/楼层项目转成XML数据
        /// <summary>
        /// 将游轮自费/楼层项目转成XML数据
        /// </summary>
        /// <param name="lst">游轮自费/楼层项目列表</param>
        /// <returns></returns>
        private string GetPlanShipOwnCostXml(IList<MPlanShipOwnCost> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" OwnItem='{0}'", Utils.ReplaceXmlSpecialCharacter(i.OwnItem));
                    sb.AppendFormat(" Price='{0}'", i.Price);
                    sb.AppendFormat(" PeopleNum='{0}'", i.PeopleNum);
                    sb.AppendFormat(" IsFloor='{0}'", i.IsFloor ? "1" : "0");
                    sb.AppendFormat("/>");
                }
                sb.AppendFormat("</root>");
                strXml = sb.ToString();
            }
            return strXml;

        }
        #endregion

        #region 将游轮价格转成XML数据
        /// <summary>
        /// 将游轮价格转成XML数据
        /// </summary>
        /// <param name="lst">游轮价格列表</param>
        /// <returns></returns>
        private string GetPlanShipPriceXml(IList<MPlanShipPrice> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" RoomType='{0}'", (int)i.RoomType);
                    sb.AppendFormat(" CrowdType='{0}'", (int)i.CrowdType);
                    sb.AppendFormat(" AdultNumber='{0}'", i.AdultNumber);
                    sb.AppendFormat(" AdultPrice='{0}'", i.AdultPrice);
                    sb.AppendFormat(" ChildNumber='{0}'", i.ChildNumber);
                    sb.AppendFormat(" ChildPrice='{0}'", i.ChildPrice);
                    sb.AppendFormat(" ChildNoOccupancy='{0}'", i.ChildNoOccupancy);
                    sb.AppendFormat(" ChildNoOccupancyPrice='{0}'", i.ChildNoOccupancyPrice);
                    sb.AppendFormat(" BabyNumber='{0}'", i.BabyNumber);
                    sb.AppendFormat(" BabyNumberPrice='{0}'", i.BabyNumberPrice);
                    sb.AppendFormat(" SumPrice='{0}'", i.SumPrice);
                    sb.AppendFormat(" BeiZhu='{0}' ", Utils.ReplaceXmlSpecialCharacter(i.BeiZhu));
                    sb.AppendFormat(" DNum='{0}' ", i.DNum);
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
        private string GetPlanDiningPriceXml(IList<MPlanDiningPrice> lst)
        {
            var strXml = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("<root>");
                foreach (var i in lst)
                {
                    sb.AppendFormat("<row PlanId='{0}'", i.PlanId);
                    sb.AppendFormat(" Pricetyp='{0}'", (int)i.Pricetyp);
                    sb.AppendFormat(" IsContainB='{0}'", i.IsContainB ? "1" : "0");
                    sb.AppendFormat(" TimeB='{0}'", i.TimeB);
                    sb.AppendFormat(" PeopleB='{0}'", i.PeopleB);
                    sb.AppendFormat(" PriceB='{0}'", i.PriceB);
                    sb.AppendFormat(" IsContainL='{0}'", i.IsContainL ? "1" : "0");
                    sb.AppendFormat(" TimeL='{0}'", i.TimeL);
                    sb.AppendFormat(" PeopleL='{0}'", i.PeopleL);
                    sb.AppendFormat(" PriceL='{0}'", i.PriceL);
                    sb.AppendFormat(" IsContainS='{0}'", i.IsContainS ? "1" : "0");
                    sb.AppendFormat(" TimeS='{0}'", i.TimeS);
                    sb.AppendFormat(" PeopleS='{0}'", i.PeopleS);
                    sb.AppendFormat(" PriceS='{0}'", i.PriceS);
                    sb.AppendFormat("/>");
                }
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

        #endregion
    }
}
