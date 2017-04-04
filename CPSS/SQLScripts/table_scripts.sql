USE CPSS
GO
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
	[sort] INT NOT NULL,
	[comment] VARCHAR(50) NULL DEFAULT (''),
	[modifydDate] TIMESTAMP NOT NULL,
 CONSTRAINT [PK_department_depid] PRIMARY KEY CLUSTERED 
(
	[depid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
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
	[childcount] INT NOT NULL,
	[serialnumber] VARCHAR(26) NOT NULL,
	[name] VARCHAR(80) NOT NULL,
	[pinyin] VARCHAR(80) NULL DEFAULT(''),
	[alias] VARCHAR(30) NULL DEFAULT(''),
	[depid] INT NOT NULL,
	[lowestdiscount] SMALLINT NULL DEFAULT(100),
	[prepaidmenttotal] NUMERIC(18,5) NULL DEFAULT(0),
	[prepayfeetotal] NUMERIC(18,5) NULL DEFAULT(0),
	[status] SMALLINT NULL DEFAULT(0),
	[mobile] VARCHAR(60) NULL DEFAULT(''),
	[address] VARCHAR(66) NULL DEFAULT(''),
	[sort] INT NULL DEFAULT(0),
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[ModifyDate] TIMESTAMP NULL,
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[empid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
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
	[sort] INT NOT NULL,
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[ModifyDate] [timestamp] NULL,
 CONSTRAINT [PK_products_proid] PRIMARY KEY CLUSTERED 
(
	[proid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

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
	[sort] INT NOT NULL,
	[comment] VARCHAR(256) NULL DEFAULT(''),
	[modifydate] TIMESTAMP NOT NULL,
 CONSTRAINT [PK_storage_stoid] PRIMARY KEY CLUSTERED 
(
	[stoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

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
	[modifydate] TIMESTAMP NULL
) ON [PRIMARY]

GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[user]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[user]
END
GO
CREATE TABLE [dbo].[user](
	[userid] INT IDENTITY(1,1) NOT NULL,
	[comid] INT NOT NULL,--所属企业ID，即saas(companyinfo表)新增客户时分配的ID
	[subcomid] INT NOT NULL,--用户自己的公司ID
	[empid] INT NULL,--职员ID
	[username] VARCHAR(26) NOT NULL,
	[usepwd] VARCHAR(100) NULL,
	[prefix] VARCHAR(3) NULL,
	[manager] BIT NOT NULL,
	[status] SMALLINT NOT NULL,
	[loginstime] DATETIME NOT NULL,
	[loginetime] DATETIME NOT NULL,
	[sort] INT NULL DEFAULT(0),
	[modifydate] TIMESTAMP NOT NULL,
 CONSTRAINT [PK_sys_user_userid] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[unit]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[unit]
END
GO
CREATE TABLE [dbo].[unit](
	[unitid] INT IDENTITY(1,1) NOT NULL,
	[name] VARCHAR(10) NOT NULL,
	[status] SMALLINT NOT NULL,
	[sort] INT NULL DEFAULT(0),
	[modifydate] TIMESTAMP NOT NULL,
 CONSTRAINT [PK_unit_unitid] PRIMARY KEY CLUSTERED 
(
	[unitid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]

GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[userright]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[userright]
END
GO
CREATE TABLE [dbo].[userright](
	[menuid] INT NOT NULL,
	[userid] INT NOT NULL
    CONSTRAINT [pk_userright_menuid_userid] PRIMARY KEY CLUSTERED
	(
		[menuid] ASC,
		[userid] ASC
	)
) ON [PRIMARY]
