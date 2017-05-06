CREATE TABLE [dbo].[PatientsVisits]
(
	[Id] INT NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    [PatientId] INT NOT NULL, 
    [VisitTypeId] INT NOT NULL, 
    [Comment] NVARCHAR(max) NULL
	CONSTRAINT [PK_Id_PatientsVisits] PRIMARY KEY ([Id])
	CONSTRAINT [FK_EmployeeId_PatientsVisits] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees],
	CONSTRAINT [FK_PatientId_PatientsVisits] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients],
	CONSTRAINT [FK_VisitTypeId_PatientsVisits]FOREIGN KEY ([VisitTypeId]) REFERENCES [dbo].[VisitTypes]
	)
