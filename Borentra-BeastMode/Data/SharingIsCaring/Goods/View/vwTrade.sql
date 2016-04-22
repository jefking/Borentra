CREATE VIEW [Goods].[vwTrade]
AS
	SELECT [Identifier]
      , [UserIdentifier1]
      , [UserIdentifier2]
      , [CreatedOn]
      , [AcceptedOn]
      , [RejectedOn]
      , [ModifiedOn]
	FROM [Goods].[Trade]
	WHERE DeletedOn IS NULL