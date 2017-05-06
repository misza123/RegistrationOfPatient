CREATE TABLE [dbo].[GroupPermissions]
(
	[Id] INT NOT NULL,
	[GroupId] INT NOT NULL,
	[PermissionId] INT NOT NULL,
	CONSTRAINT [PK_Id_GroupPermissions]  PRIMARY KEY ([Id]),
	CONSTRAINT [FK_GroupId_GroupPermissions] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ON DELETE CASCADE,
	CONSTRAINT [FK_PermissionId_GroupPermissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ON DELETE CASCADE
)
