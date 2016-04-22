CREATE VIEW [Goods].[vwItemRequestImage]
AS
	SELECT [Identifier]
		, [ItemRequestIdentifier]
		, [Path]
		, [FileName]
		, [ContentType]
		, [FileSize]
		, [IsPrimary]
		, [ModifiedOn]
	FROM [Goods].[ItemRequestImage]
	WHERE [DeletedOn] IS NULL