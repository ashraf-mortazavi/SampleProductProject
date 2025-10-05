CREATE TABLE [dbo].[Users]
(
	[Id]		[int]			IDENTITY(1,1)	NOT NULL,
	[UserName]	[varchar](50)					NOT NULL,
	[Password]	[varchar](50)					NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]

GO
