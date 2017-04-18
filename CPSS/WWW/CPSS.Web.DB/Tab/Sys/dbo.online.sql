USE CPSSMASTER
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