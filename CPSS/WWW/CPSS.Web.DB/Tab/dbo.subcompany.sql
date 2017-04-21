USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[subcompany]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[subcompany]
END
GO
CREATE TABLE [dbo].[subcompany](
	[subcomid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NULL DEFAULT(''),
	[childnumber] INT NOT NULL,--子级节点数量(只有子节点)
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT(''),
	[pricemode] SMALLINT NOT NULL,
	[email] VARCHAR(30) NULL DEFAULT(''),
	[linkman] VARCHAR(20) NULL DEFAULT(''),
	[linktel] VARCHAR(60) NULL DEFAULT(''),
	[status] SMALLINT NOT NULL,
	[deleted] SMALLINT NOT NULL,
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[sort] INT NOT NULL,
 CONSTRAINT [PK_company_subcomid] PRIMARY KEY CLUSTERED 
(
	[subcomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO dbo.subcompany(classid,parentid,childnumber,serialnumber,name,pinyin,pricemode,email,linkman,linktel,[status],comment,sort,deleted)
	VALUES ('000001','',0,'root','root','root',0,'','','',1,'',0,1)
GO