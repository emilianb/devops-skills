CREATE PROCEDURE [dbo].[Person_GetById]
	@personId int
AS
	SELECT [Id], [FirstName], [LastName], [EmailAddress], [Status] 
	from Person
	where Id = @personId

	
