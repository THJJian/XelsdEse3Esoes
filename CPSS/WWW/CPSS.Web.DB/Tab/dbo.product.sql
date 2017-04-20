USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[product]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[product]
END
GO
CREATE TABLE [dbo].[product](
	[proid] INT IDENTITY(1,1) NOT NULL,
	[classid] VARCHAR(60) NOT NULL,
	[parentid] VARCHAR(54) NOT NULL,
	[childcount] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT(''),
	[alias] VARCHAR(30) NULL DEFAULT(''),
	[standard] VARCHAR(80) NULL DEFAULT(''),--规格
	[modal] VARCHAR(20) NULL DEFAULT(''),--模式
	[permitcode] VARCHAR(50) NULL DEFAULT(''),--许可编码
	[brand] VARCHAR(50) NULL DEFAULT(''),--品牌
	[trademark] VARCHAR(50) NULL DEFAULT(''),--商标
	[makearea] VARCHAR(60) NULL DEFAULT(''),--产地
	[barcode] VARCHAR(30) NULL DEFAULT(''),--条形码
	[price] NUMERIC(18,5) NULL DEFAULT(0),--单价
	[taxrate] NUMERIC(5,2) NULL DEFAULT(0),
	[unitid] SMALLINT NOT NULL,--单位
	[validmonth] SMALLINT NULL DEFAULT(1),--有效期(月)
	[validday] SMALLINT NULL DEFAULT(15),--有效期(日)
	[status] SMALLINT NULL DEFAULT(0),
	[costmethod] SMALLINT NULL DEFAULT(0),
	[snmanage] INT NOT NULL,
	[snlength] SMALLINT NOT NULL,
	[deleted] SMALLINT NULL DEFAULT(0),
	[sort] INT NOT NULL,
	[comment] VARCHAR(256) NULL DEFAULT(''),
 CONSTRAINT [PK_products_proid] PRIMARY KEY CLUSTERED 
(
	[proid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO