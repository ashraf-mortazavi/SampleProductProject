CREATE TABLE [dbo].[UserProducts]
(
	[Id]		[int]	IDENTITY(1,1)   NOT NULL,
	[UserId]	[int]		            NOT NULL,
	[Title]     [nvarchar](80)          NOT NULL,
	[Code]		[varchar](20)	        NOT NULL,
	[Price]     [int]		            NULL,
	CONSTRAINT [PK_UserProducts] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserProducts]
    WITH CHECK ADD  CONSTRAINT [FK_UserProducts_Users_UserId]
    FOREIGN KEY([UserId])
    REFERENCES [dbo].[Users] ([Id])
    ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserProducts] CHECK CONSTRAINT [FK_UserProducts_Users_UserId]

GO

