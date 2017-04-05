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