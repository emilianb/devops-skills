MERGE INTO Lookup AS Target 
USING (VALUES 
  (0, 'phone-type', 'home', 'home'), 
  (1, 'phone-type', 'work', 'work'), 
  (2, 'phone-type', 'cell', 'cell'), 
  (3, 'employee-title', 'ceo', 'ceo'), 
  (4, 'employee-title', 'cto', 'cto'), 
  (5, 'employee-title', 'cio', 'cio'), 
  (6, 'employee-title', 'vp', 'vice president'),
  (7, 'employee-title', 'dir', 'director'),
  (9, 'employee-title', 'janitor', 'janitor'),
  (10, 'employee-title', 'dev-lead', 'development lead')
) 
AS Source (Id, LookupType, LookupKey, LookupValue) 
ON Target.Id = Source.Id
WHEN MATCHED THEN 
	UPDATE SET 
		LookupType = Source.LookupType,
		LookupKey = Source.LookupKey,
		LookupValue = Source.LookupValue
WHEN NOT MATCHED BY TARGET THEN 
	INSERT (Id, LookupType, LookupKey, LookupValue) 
	VALUES (Id, LookupType, LookupKey, LookupValue) 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;
