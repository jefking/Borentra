CREATE VIEW [Goods].[vwItemRent]
AS
	SELECT [ItemIdentifier]
		, [Identifier]
		, [Price]
		, [PerUnit]
	FROM [Goods].[ItemRent]
	WHERE DeletedOn IS NULL