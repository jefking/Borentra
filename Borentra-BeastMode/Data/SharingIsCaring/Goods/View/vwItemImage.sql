CREATE VIEW [Goods].[vwItemImage]
AS
	SELECT [Identifier]
		, [ItemIdentifier]
		, [Path]
		, [FileName]
		, [ContentType]
		, [FileSize]
		, [IsPrimary]
		, [ModifiedOn]
	FROM [Goods].[ItemImage]
	WHERE [DeletedOn] IS NULL