CREATE TABLE [dbo].[Employees]
(
	[Id] INT NOT NULL Identity, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Surname] NVARCHAR(255) NOT NULL, 
    [EmployeeTypeId] INT NOT NULL
	CONSTRAINT [PK_Id_Employees] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_EmployeeTypeId] FOREIGN KEY ([EmployeeTypeId])REFERENCES [dbo].[EmployeeTypes]
)
