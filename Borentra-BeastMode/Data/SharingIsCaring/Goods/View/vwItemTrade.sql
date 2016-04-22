CREATE VIEW [Goods].[vwItemTrade]
AS
	SELECT [Identifier]
      , [TradeIdentifier]
      , [ItemIdentifier]
      , [CreatedOn]
      , [ModifiedOn]
	FROM [Goods].[ItemTrade]
	WHERE DeletedOn IS NULL
