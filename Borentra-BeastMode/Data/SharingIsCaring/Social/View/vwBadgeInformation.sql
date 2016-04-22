CREATE VIEW [Social].[vwBadgeInformation]
AS
	SELECT Identifier
		, Title
		, [Description]
		, [Points]
		, [IconName]
	FROM [Social].[BadgeInformation]
	WHERE DeletedOn IS NULL
