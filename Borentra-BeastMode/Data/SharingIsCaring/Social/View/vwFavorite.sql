CREATE VIEW [Social].[vwFavorite]
AS
	SELECT ReferenceIdentifier
		, UserIdentifier
	FROM [Social].[Favorite]
	WHERE DeletedOn IS NULL