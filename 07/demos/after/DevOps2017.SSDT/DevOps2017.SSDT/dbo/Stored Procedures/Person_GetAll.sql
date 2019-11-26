
CREATE PROCEDURE [dbo].[Person_GetAll]
AS
	SELECT [Id], [FirstName], [LastName], [EmailAddress] from Person
	ORDER BY LastName, FirstName

