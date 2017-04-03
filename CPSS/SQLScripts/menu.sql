IF EXISTS(SELECT 1 FROM sysobjects WHERE id=object_id(N'[menu]') AND objectproperty(id,N'IsUserTable')=1)
BEGIN
	DROP TABLE [menu]
END
GO
CREATE TABLE [dbo].[menu](
	[menuid] [int] NOT NULL identity(1,1),
	[classid] [varchar](60) NOT NULL,
	[parentid] [varchar](54) NOT NULL,
	[buttonid] [varchar](50) NOT NULL,
	[title] [varchar](50) NOT NULL,
	[iconcls] [varchar](60) NULL,
	[url] [varchar](256) NOT NULL,
	[isbutton] [bit] NULL DEFAULT(0),
	[isroot] [bit] NULL DEFAULT(0),
	[isenabled] [bit] NULL DEFAULT(0),
	[isbeginsplit] [BIT] NULL DEFAULT(0),
	[isendsplit] [BIT] NULL DEFAULT(0),
	[sort] [INT] NULL DEFAULT 0
 CONSTRAINT [PK_sys_menus_menuid] PRIMARY KEY CLUSTERED 
(
	[menuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_sys_menus_classid] UNIQUE NONCLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO