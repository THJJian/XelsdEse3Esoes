﻿USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[department]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[department]
END
GO
CREATE TABLE [dbo].[department](
	[depid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NULL DEFAULT (''),
	[childnumber] INT NOT NULL,
	[childcount] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT (''),
	[status] INT NOT NULL,
	[deleted] SMALLINT NULL DEFAULT(0),
	[sort] INT NOT NULL,
	[comment] VARCHAR(50) NULL DEFAULT (''),
	[modifydDate] TIMESTAMP NOT NULL,
 CONSTRAINT [PK_department_depid] PRIMARY KEY CLUSTERED 
(
	[depid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO