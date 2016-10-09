
USE [testdb]
GO

IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'sys_users') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE sys_users
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_users](
	[UserID] [int] NOT NULL,
	[CompanySerialNum] INT NOT NULL,
	[CompanyName] VARCHAR(128) NOT NULL,
	[UserName] VARCHAR(50) NOT NULL,
	[UserPwd] [varchar](100) NULL,
	[Manager] [bit] NOT NULL,
	[isSystem] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL
) ON [PRIMARY]

GO
INSERT INTO dbo.sys_users(UserID ,CompanySerialNum ,[CompanyName], UserName ,UserPwd ,Manager ,isSystem ,Deleted)
	VALUES(1, 12110, '系统测试公司', 'Admin', NULL, 1, 1,0)
GO

IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'[dbo].[Online]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[Online]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Online](
	[UserID] INT NOT NULL,
	[LoginName] VARCHAR(50) NOT NULL,
	[UserIP] VARCHAR(30) NOT NULL,
	[Browser] VARCHAR(50) NULL,
	[LoginTime] DATETIME NOT NULL,
	[OverTime] DATETIME NOT NULL,
	[ExpTime] DATETIME NOT NULL,
	[StateID] INT NOT NULL,
	[SGuid] UNIQUEIDENTIFIER NOT NULL
) ON [PRIMARY]

GO

IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'[dbo].[CompnayInfo]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[CompnayInfo]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CompnayInfo](
	[CompanyID] INT NOT NULL,
	[Server] VARCHAR(128) NOT NULL,
	[Database] VARCHAR(128) NOT NULL,
	[ConnectTimeout] int NULL DEFAULT(6000),
	[UserID] VARCHAR(128) NOT NULL,
	[Password] VARCHAR(128) NOT NULL
) ON [PRIMARY]

GO

INSERT INTO dbo.CompnayInfo (CompanyID ,Server ,[Database] ,ConnectTimeout ,UserID ,Password)
	VALUES(12110, '.\mssqlserver2012', 'testdb', 3000, 'sa', '000000')
GO
