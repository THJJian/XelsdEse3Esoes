IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[subcompany]') AND OBJECTPROPERTY(id, N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[subcompany]
END
GO
CREATE TABLE [dbo].[subcompany](
	[subcomid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NULL DEFAULT(''),
	[childnumber] INT NOT NULL,
	[childcount] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT(''),
	[pricemode] SMALLINT NOT NULL,
	[email] VARCHAR(30) NULL DEFAULT(''),
	[linkman] VARCHAR(20) NULL DEFAULT(''),
	[linktel] VARCHAR(60) NULL DEFAULT(''),
	[status] SMALLINT NOT NULL,
	[comment] VARCHAR(100) NULL DEFAULT(''),
	[sort] INT NOT NULL,
	[modifydate] TIMESTAMP NULL,
 CONSTRAINT [PK_company_subcomid] PRIMARY KEY CLUSTERED 
(
	[subcomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO