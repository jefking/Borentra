CREATE VIEW [Goods].[vwItemFree]
AS
	SELECT Identifier
		, ItemIdentifier
		, UserIdentifier
		, [Status]
		, [CreatedOn] AS [On]
	FROM [Goods].[ItemFree]
	WHERE DeletedOn IS NULL