CREATE TABLE [dbo].[UsersGroups]
(
	[Id] INT NOT NULL ,
	[UserId] INT NOT NULL,
	[GroupId] INT NOT NULL,
	CONSTRAINT [PK_Id_UsersGroups] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_UserId_UsersGroups] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ON DELETE CASCADE,
	CONSTRAINT [FK_GroupId_UsersGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups]
)
