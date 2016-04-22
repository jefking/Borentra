CREATE VIEW [Social].[vwRating]
AS
	SELECT ReferenceIdentifier
		, UserIdentifier
		, Rating
	FROM [Social].[Rating]
	WHERE DeletedOn IS NULL