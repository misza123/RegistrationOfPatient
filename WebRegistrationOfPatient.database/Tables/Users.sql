CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY,
	[PatientId] INT NULL,
	[EmployeeId] INT NULL,
	[Login] VARCHAR(255) NOT NULL,
	[Password] VARCHAR(255) NOT NULL,
	[UserTypeId] INT NOT NULL,
	
	CONSTRAINT [PK_Id_Users] PRIMARY KEY ([Id]),
	--CONSTRAINT [FK_UserTypeId_Users] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserTypes],
	CONSTRAINT [FK_PatientId_Users] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients]  ON DELETE SET NULL,
	CONSTRAINT [FK_EmployeeId_Users]  FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ON DELETE SET NULL,
)
