CREATE TABLE [dbo].[Permissions]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	CONSTRAINT [PK_Id_Permissions] PRIMARY KEY ([Id]),
)
