CREATE TABLE [dbo].[Rooms]
(
	[Id] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [RoomNumber] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Id_Rooms] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Type_Rooms] FOREIGN KEY ([Type]) REFERENCES [dbo].[RoomTypes]
)
