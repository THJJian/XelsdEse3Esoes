IF EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID(N'[dbo].[client]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[client]
END
GO
CREATE TABLE [dbo].[client](
	[clientid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NULL DEFAULT(''),
	[childnumber] INT NOT NULL,--子级节点数量(只有子节点)
	[childcount] INT NOT NULL,--子级节点数量(包含子节点下的子节点)
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT(''),
	[alias] VARCHAR(30) NULL DEFAULT(''),
	[address] VARCHAR(80) NULL DEFAULT(''),
	[zipcode] VARCHAR(10) NULL DEFAULT(''),
	[linkman] VARCHAR(20) NULL DEFAULT(''),
	[linktel] VARCHAR(60) NULL DEFAULT(''),
	[linkaddress] VARCHAR(120) NULL DEFAULT(''),
	[credits] NUMERIC(18,5) NULL DEFAULT(''),
	[pricemode] SMALLINT NULL DEFAULT(0),
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[status] INT NULL DEFAULT(0),
	[sort] INT NULL DEFAULT(0),
	[modifydate] TIMESTAMP NULL
 CONSTRAINT [PK_client_clientid] PRIMARY KEY CLUSTERED 
(
	[clientid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO