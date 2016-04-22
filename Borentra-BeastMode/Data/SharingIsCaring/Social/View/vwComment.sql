CREATE VIEW [Social].[vwComment]
AS
	SELECT ReferenceIdentifier
		, UserIdentifier
		, Comment
		, Identifier
		, CreatedOn
	FROM [Social].[Comment]
	WHERE DeletedOn IS NULL