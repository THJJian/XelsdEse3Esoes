
USE [testdb]
GO

IF exists(SELECT 1 FROM sysobjects WHERE id=object_id(N'[sys_menus]') AND objectproperty(id,N'IsUserTable')=1)
begin
	DROP TABLE [sys_menus]
end

/****** Object:  Table [dbo].[sys_menus]    Script Date: 2016/9/28 22:14:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sys_menus](
	[menuid] [int] NOT NULL identity(1,1),
	[ClassID] [varchar](60) NOT NULL,
	[ParentClassID] [varchar](54) NOT NULL,
	[ButtonID] [varchar](50) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[IconCls] [varchar](60) NULL,
	[Url] [varchar](256) NOT NULL,
	[IsButton] [bit] NULL,
	[IsRoot] [bit] NULL,
	[IsEnabled] [bit] NULL
 CONSTRAINT [PK_sys_menus] PRIMARY KEY CLUSTERED 
(
	[menuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

