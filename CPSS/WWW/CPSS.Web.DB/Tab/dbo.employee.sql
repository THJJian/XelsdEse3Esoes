﻿USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[employee]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[employee]
END
GO
CREATE TABLE [dbo].[employee](
	[empid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NOT NULL,
	[childnumber] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NOT NULL DEFAULT(''),
	[depid] INT NOT NULL,
	[depname] VARCHAR(80) NOT NULL,
	[lowestdiscount] SMALLINT NULL DEFAULT(100),
	[preinadvancetotal] NUMERIC(18,5) NULL DEFAULT(0),
	[prepayfeetotal] NUMERIC(18,5) NULL DEFAULT(0),
	[mobile] VARCHAR(60) NULL DEFAULT(''),
	[address] VARCHAR(66) NULL DEFAULT(''),
	[status] SMALLINT NULL DEFAULT(0),
	[deleted] SMALLINT NULL DEFAULT(0),
	[sort] INT NULL DEFAULT(0),
	[comment] VARCHAR(256) NULL DEFAULT(''),
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[empid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO dbo.[employee](classid,parentid,childnumber,serialnumber,name,pinyin,[depid],[depname],[lowestdiscount],[preinadvancetotal],[prepayfeetotal],[mobile],[address],[status],comment,sort,deleted)
	VALUES ('000001','',0,'root','root','root',0,'',100,0,0,'','',1,'',0,1)
GO
