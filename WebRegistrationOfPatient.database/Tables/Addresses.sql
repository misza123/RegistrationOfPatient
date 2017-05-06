CREATE TABLE [dbo].[Addresses]
    (
      [Id] INT IDENTITY
               NOT NULL ,
      [City] VARCHAR(128) NOT NULL ,
      [Street] VARCHAR(128) ,
      [HouseNumber] VARCHAR(8) ,
      [FlatNumber] VARCHAR(8) ,
      [PostalCode] VARCHAR(6) NOT NULL
      CONSTRAINT [PK_Id_Addresses] PRIMARY KEY ( [Id] )	 
    );
