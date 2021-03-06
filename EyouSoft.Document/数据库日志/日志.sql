GO

/*
   2014年8月1日12:39:55
   用户: sa
   服务器: 192.168.1.254
   数据库: azhr
   应用程序: 
*/

/* 为了防止任何可能出现的数据丢失问题，您应该先仔细检查此脚本，然后再在数据库设计器的上下文之外运行此脚本。*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tbl_SourceJingDianJiaGe ADD
	ETprice money NOT NULL CONSTRAINT DF_tbl_SourceJingDianJiaGe_ETprice DEFAULT 0,
	JTprice money NOT NULL CONSTRAINT DF_tbl_SourceJingDianJiaGe_JTprice DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'儿童价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_SourceJingDianJiaGe', N'COLUMN', N'ETprice'
GO
DECLARE @v sql_variant 
SET @v = N'家庭价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_SourceJingDianJiaGe', N'COLUMN', N'JTprice'
GO
COMMIT
select Has_Perms_By_Name(N'dbo.tbl_SourceJingDianJiaGe', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.tbl_SourceJingDianJiaGe', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.tbl_SourceJingDianJiaGe', 'Object', 'CONTROL') as Contr_Per 

GO


/*
   2014年8月1日12:42:12
   用户: sa
   服务器: 192.168.1.254
   数据库: azhr
   应用程序: 
*/

/* 为了防止任何可能出现的数据丢失问题，您应该先仔细检查此脚本，然后再在数据库设计器的上下文之外运行此脚本。*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tbl_PlanAttractions ADD
	JTprice money NOT NULL CONSTRAINT DF_tbl_PlanAttractions_JTprice DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'家庭价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_PlanAttractions', N'COLUMN', N'JTprice'
GO
COMMIT
select Has_Perms_By_Name(N'dbo.tbl_PlanAttractions', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.tbl_PlanAttractions', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.tbl_PlanAttractions', 'Object', 'CONTROL') as Contr_Per 

GO


