USE CPSSMASTER
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