CREATE VIEW [Goods].[vwItemRenting]
AS
	SELECT [Identifier]
		, [ItemIdentifier]
		, [UserIdentifier]
		, [Price]
		, [On]
		, [Until]
		, [ReturnedOn]
		, [Status]
		, [CreatedOn]
	FROM [Goods].[ItemRenting]
	WHERE DeletedOn IS NULL