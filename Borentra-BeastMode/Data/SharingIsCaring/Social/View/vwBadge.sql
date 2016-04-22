CREATE VIEW [Social].[vwBadge]
AS
	SELECT UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	FROM [Social].[Badge]
	WHERE DeletedOn IS NULL
