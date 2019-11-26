CREATE TABLE [dbo].[Lookup] (
    [Id]          INT           NOT NULL,
    [LookupType]  NVARCHAR (50) NOT NULL,
    [LookupKey]   NVARCHAR (50) NOT NULL,
    [LookupValue] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

