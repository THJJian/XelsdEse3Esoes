﻿USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[storage]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[storage]
END
GO
CREATE TABLE [dbo].[storage](
	[stoid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NULL,
	[childnumber] INT NOT NULL,
	[childcount] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[PinYin] VARCHAR(80) NULL DEFAULT(''),
	[alias] VARCHAR(30) NULL DEFAULT(''),
	[status] SMALLINT NOT NULL,
	[deleted] SMALLINT NULL DEFAULT(0),
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[sort] INT NOT NULL,
 CONSTRAINT [PK_storage_stoid] PRIMARY KEY CLUSTERED 
(
	[stoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO dbo.storage(classid,parentid,childnumber,childcount,serialnumber,name,PinYin,alias,status,deleted,comment,sort)
	VALUES( '000001','',0,0,'root','root','root','root',1,1,'',0)
GO