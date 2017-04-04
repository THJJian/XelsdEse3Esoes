USE master
GO

IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[syscompany]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[syscompany]
END
GO
CREATE TABLE [dbo].[syscompany](	
	[comid] INT IDENTITY(1,1) NOT NULL,
	[comname] VARCHAR(128) NOT NULL,
	[linkemail] VARCHAR(30) NOT NULL,
	[linkman] VARCHAR(20) NOT NULL,
	[linktel] VARCHAR(60) NOT NULL,
	[status] SMALLINT NOT NULL,
	[sortindex] INT NOT NULL,
	[comment] VARCHAR(100) NULL DEFAULT(''),
	[modifydate] TIMESTAMP NULL,
	CONSTRAINT [PK_syscompany_comid] PRIMARY KEY CLUSTERED 
	(
		[comid] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

GO
IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'[dbo].[sysuser]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[sysuser]
END
GO

CREATE TABLE [dbo].[sysuser](
	[id] INT IDENTITY(1,1) NOT NULL,
	[comid] INT NOT NULL,
	[comname] VARCHAR(128) NOT NULL,
	[userid] INT NOT NULL,
	[username] VARCHAR(50) NOT NULL,
	[userpwd] VARCHAR(100) NULL,
	[issystem] BIT NOT NULL,
	[ismanager] BIT NOT NULL,
	[modifydate] TIMESTAMP NULL,
	CONSTRAINT [PK_sysuser_id] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT INTO dbo.[sysuser]([comid] ,[comname], [userid] ,[username] ,[userpwd],[issystem],[ismanager])
	VALUES(12110, '系统测试公司', 1, 'Admin', '670b14728ad9902aecba32e22fa4f6bd',0,1)
GO

IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[online]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[online]
END
GO
CREATE TABLE [dbo].[online](
	[userid] INT NOT NULL,
	[username] VARCHAR(50) NOT NULL,
	[userip] VARCHAR(30) NOT NULL,
	[browser] VARCHAR(50) NULL,
	[logintime] DATETIME NOT NULL,
	[overtime] DATETIME NOT NULL,
	[exptime] DATETIME NOT NULL,
	[status] INT NOT NULL,
	[sguid] UNIQUEIDENTIFIER NOT NULL,
	[modifydate] TIMESTAMP NULL
) ON [PRIMARY]

GO

IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[compnaydbinfo]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[compnaydbinfo]
END
GO
CREATE TABLE [dbo].[compnaydbinfo](
	[id] INT IDENTITY(1,1) NOT NULL,
	[comid] INT NOT NULL,
	[dbserver] VARCHAR(128) NOT NULL,
	[dbase] VARCHAR(128) NOT NULL,
	[timeout] int NULL DEFAULT(6000),
	[uid] VARCHAR(128) NOT NULL,
	[pwd] VARCHAR(128) NOT NULL,
	[modifydate] TIMESTAMP NULL,
	CONSTRAINT [PK_compnaydbinfo_id] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO dbo.[compnaydbinfo]([comid] ,[dbserver] ,[dbase] ,[timeout] ,[uid] ,[pwd])
	VALUES(12110, '.', 'cpss', 3000, 'sa', '000000')
GO
