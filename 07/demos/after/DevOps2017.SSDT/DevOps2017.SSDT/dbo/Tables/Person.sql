CREATE TABLE [dbo].[Person] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (MAX) NOT NULL,
    [LastName]     NVARCHAR (MAX) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NOT NULL,
    [Status]       NVARCHAR (50)  DEFAULT ('ACTIVE') NOT NULL,
    CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);



