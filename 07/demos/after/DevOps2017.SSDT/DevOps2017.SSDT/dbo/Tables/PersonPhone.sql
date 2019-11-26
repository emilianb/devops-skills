CREATE TABLE [dbo].[PersonPhone] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]    INT           NOT NULL,
    [PhoneType]   NVARCHAR (10) NOT NULL,
    [PhoneNumber] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonPhone_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

