CREATE TABLE [dbo].[Relationship]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RelationshipType] NVARCHAR(100) NOT NULL,
	[FromPersonId] INT NOT NULL, 
	[ToPersonId] INT NOT NULL
    CONSTRAINT [FK_Relationship_Person_FromPersonId] FOREIGN KEY ([FromPersonId]) REFERENCES [Person]([Id])
    CONSTRAINT [FK_Relationship_Person_ToPersonId] FOREIGN KEY ([ToPersonId]) REFERENCES [Person]([Id])
)
