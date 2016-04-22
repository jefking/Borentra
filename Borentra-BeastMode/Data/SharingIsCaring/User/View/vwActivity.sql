CREATE VIEW [User].[vwActivity]
AS
	SELECT [Identifier]
		, [ReferenceIdentifier]
		, [UserIdentifier]
		, [Text]
		, [Type]
		, [ModifiedOn]
		, [CreatedOn]
	FROM [User].[Activity]
	WHERE DeletedOn IS NULL