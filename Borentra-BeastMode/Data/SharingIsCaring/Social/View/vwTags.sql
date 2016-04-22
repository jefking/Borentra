CREATE VIEW [Social].[vwTags]
AS
	SELECT [Tags]
		, [UserIdentifier]
		, [ReferenceIdentifier]
	FROM [Social].[Tags]
	WHERE DeletedOn IS NULL