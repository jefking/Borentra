CREATE PROCEDURE [Stats].[CommunityRent]
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
		, [rent].[Status]
		, [rent].[On]
		, [rent].[ReturnedOn]
		, [rent].[Price]
	FROM [Goods].[vwItemRenting] [rent] WITH(NOLOCK)
		INNER JOIN [User].[Profile] [Requester] WITH(NOLOCK)
			ON [rent].[UserIdentifier] = [Requester].[UserIdentifier]
		INNER JOIN [Goods].[Item] [Item] WITH(NOLOCK)
			ON [Item].[Identifier] = [rent].[ItemIdentifier]
		INNER JOIN [User].[Profile] [Owner] WITH(NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].UserIdentifier
	ORDER BY [rent].[CreatedOn] DESC

END