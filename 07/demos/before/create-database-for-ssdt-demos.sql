-- This creates the empty sample database schema that gets imported into a 
-- SQL Server Database Project.

/*
DROP DATABASE DevOps2017_SSDT
GO
*/

CREATE DATABASE DevOps2017_SSDT
GO

USE DevOps2017_SSDT
GO

CREATE TABLE [dbo].[Person] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (MAX) NOT NULL,
    [LastName]     NVARCHAR (MAX) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NOT NULL,
    [Status] NVARCHAR(50) NOT NULL DEFAULT 'ACTIVE', 
    CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [dbo].[PersonPhone]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[PersonId] INT NOT NULL, 
	[PhoneType] NVARCHAR(10) NOT NULL,
	[PhoneNumber] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_PersonPhone_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])
)
GO

CREATE TABLE [dbo].[Lookup]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [LookupType] NVARCHAR(50) NOT NULL, 
    [LookupKey] NVARCHAR(50) NOT NULL, 
    [LookupValue] NVARCHAR(50) NOT NULL
)
GO

CREATE PROCEDURE [dbo].[Person_GetAll]
AS
	SELECT [Id], [FirstName], [LastName], [EmailAddress] from Person
	ORDER BY LastName, FirstName

GO