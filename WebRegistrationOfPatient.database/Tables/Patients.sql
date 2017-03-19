CREATE TABLE [dbo].[Patients](
	[Id] [INT] IDENTITY NOT NULL,
	[AddressId] [INT] NULL,
	[name] [VARCHAR](128) NULL,
	[surname] [VARCHAR](128) NULL,
	[personalIdentityNumber] [VARCHAR](128) NOT null, 
    CONSTRAINT [PK_Id_dboPatients] PRIMARY KEY ([Id]),
	CONSTRAINT [U_personalIdentityNumber_dboPatients] UNIQUE ([personalIdentityNumber]),
	CONSTRAINT [FK_AddressId_dboPatients] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses]
)
