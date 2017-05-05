USE CPSS
GO
IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id=OBJECT_ID(N'[dbo].[user]') AND OBJECTPROPERTY(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [dbo].[user]
END
GO
CREATE TABLE [dbo].[user](
	[userid] INT IDENTITY(1,1) NOT NULL,
	[comid] INT NOT NULL,--所属企业ID，即saas(companyinfo表)新增客户时分配的ID
	[empid] INT NULL,--职员ID
	[username] VARCHAR(26) NOT NULL,
	[usepwd] VARCHAR(100) NULL,
	[prefix] VARCHAR(4) NULL,
	[manager] SMALLINT NOT NULL,
	[status] SMALLINT NOT NULL,
	[deleted] SMALLINT  NULL DEFAULT(0),
	[synchron] SMALLINT NOT NULL DEFAULT(0),--标志是否已经同步到中心库，由windows服务更新
	[comment] VARCHAR(256) NULL DEFAULT('')
 CONSTRAINT [PK_sys_user_userid] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 99) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO dbo.[user](comid,empid,username,usepwd,prefix,manager,status,deleted,synchron,comment)
	VALUES  (12110,1,'Admin','670b14728ad9902aecba32e22fa4f6bd','',1,1,1,1,'')
GO