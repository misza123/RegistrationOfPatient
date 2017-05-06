CREATE TABLE [dbo].[EmployeeWorkHours]
(
	[Id] INT NOT NULL,
	[EmployeeId] INT NOT NULL, 
    [RommId] INT NOT NULL, 
    [StartWorkTime] DATETIME NOT NULL, 
    [EndWorkTime] DATETIME NOT NULL,
	CONSTRAINT [PK_Id_EmployeeWorkHours] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_EmployeeId_EmployeeWorkHours] FOREIGN KEY ([EmployeeId]) REFERENCES  [dbo].[Employees],
	CONSTRAINT [FK_RoomId_EmployeeWorkHours] FOREIGN KEY ([RommId]) REFERENCES [dbo].[Rooms]
)
