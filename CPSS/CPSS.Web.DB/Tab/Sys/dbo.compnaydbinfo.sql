USE CPSSMASTER
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