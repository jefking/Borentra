CREATE PROCEDURE [Stats].[CommunityShare]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Owner].[Key] AS OwnerKey
		, [Owner].[DisplayName] AS OwnerDisplayName
		, [Requester].[Key] AS RequesterKey
		, [Requester].[DisplayName] AS RequesterDisplayName
		, [Item].[Title] AS ItemTitle
		, [Item].[Key] AS ItemKey
		, [Share].[Status]
		, [Share].[On]
		, [Share].[ReturnedOn]
	FROM [Goods].[ItemShare] [Share] WITH(NOLOCK)
		INNER JOIN [User].[Profile] [Requester] WITH(NOLOCK)
			ON [Share].[UserIdentifier] = [Requester].[UserIdentifier]
		INNER JOIN [Goods].[Item] [Item] WITH(NOLOCK)
			ON [Item].[Identifier] = [Share].[ItemIdentifier]
		INNER JOIN [User].[Profile] [Owner] WITH(NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].UserIdentifier
	ORDER BY [Share].[CreatedOn] DESC

END