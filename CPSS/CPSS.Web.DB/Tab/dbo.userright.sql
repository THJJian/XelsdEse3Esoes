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