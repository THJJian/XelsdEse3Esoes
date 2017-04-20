USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[snstorage]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[snstorage]
END
GO
CREATE TABLE [dbo].[snstorage](
	[sn] VARCHAR(50) NOT NULL,
	[subcomid] INT NOT NULL,
	[stoid] INT NOT NULL,
	[clientid] INT NOT NULL,
	[empid] INT NOT NULL,
	[depid] INT NOT NULL,
	[proid] INT NOT NULL,
	[billtype] INT NOT NULL,
	[billid] INT NOT NULL,
	[costprice] NUMERIC(18,5) NULL,
	[ctime] DATETIME NOT NULL,
	[makedate] DATETIME NULL,
	[validdate] DATETIME NULL,
	[deleted] SMALLINT NULL DEFAULT(0)
) ON [PRIMARY]
GO