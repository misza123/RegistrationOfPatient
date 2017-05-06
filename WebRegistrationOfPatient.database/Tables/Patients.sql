CREATE TABLE [dbo].[Patients](
	[Id] [INT] IDENTITY NOT NULL,
	[AddressId] [INT] NOT NULL,
	[Name] [NVARCHAR](128) NULL,
	[Surname] [NVARCHAR](128) NULL,
	[PersonalIdentityNumber] [NVARCHAR](32) NOT NULL, 
	[EmailAddress] [NVARCHAR](128) NULL,
	[PhoneNumber] [VARCHAR](16) NULL
    CONSTRAINT [PK_Id_dboPatients] PRIMARY KEY ([Id]),
	CONSTRAINT [U_PersonalIdentityNumber_Patients] UNIQUE ([PersonalIdentityNumber]),
	CONSTRAINT [FK_AddressId_Patients] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses]
)
