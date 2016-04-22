CREATE PROCEDURE [Goods].[RecentItemShare]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT TOP (1) [Share].[Identifier]
		, [Share].[ItemIdentifier]
		, [Share].[UserIdentifier]
		, [Share].[On]
		, [Share].[Until]
		, [Share].[ReturnedOn]
		, [Share].[Status]
		, [Item].[UserIdentifier] AS [OwnerIdentifier]
		, [Item].[Key] AS [ItemKey]
		, [Owner].[FacebookId] AS [OwnerFacebookId]
		, [Owner].[Key] AS [OwnerKey]
		, [Owner].[Location] AS [OwnerLocation]
		, [Requester].[FacebookId] AS [RequesterFacebookId]
		, [Requester].[Key] AS [RequesterKey]
		, [Requester].[Location] AS [RequesterLocation]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH(NOLOCK)
			WHERE [ItemIdentifier] = [Item].Identifier
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
		INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
			ON [Share].[ItemIdentifier] = [Item].[Identifier]
				AND [Share].[Status] IN (1, 2)
		INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
		INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
			ON [Share].[UserIdentifier] = [Requester].[UserIdentifier]
	ORDER BY NEWID()

END