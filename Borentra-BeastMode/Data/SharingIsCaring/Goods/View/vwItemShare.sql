CREATE VIEW [Goods].[vwItemShare]
AS
	SELECT [Identifier]
		, [ItemIdentifier]
		, [UserIdentifier]
		, [On]
		, [Until]
		, [ReturnedOn]
		, [Status]
		, [CreatedOn]
		, [ModifiedOn]
	FROM [Goods].[ItemShare]
	WHERE [DeletedOn] IS NULL