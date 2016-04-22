CREATE PROCEDURE [Goods].[SearchItemShare]
	@Identifier [uniqueidentifier] = NULL
	, @ItemIdentifier [uniqueidentifier] = NULL
	, @OwnerIdentifier [uniqueidentifier] = NULL
	, @RequesterIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Share].[Identifier]
		, [Share].[ItemIdentifier]
		, [Share].[On]
		, [Share].[Until]
		, [Share].[ReturnedOn]
		, [Share].[Status]
		, [Item].[UserIdentifier] AS [OwnerIdentifier]
		, [Item].[Key] AS [ItemKey]
		, [Item].[Title] AS [ItemTitle]
		, [Owner].[FacebookId] AS [OwnerFacebookId]
		, [Owner].[Key] AS [OwnerKey]
		, [Owner].[Location] AS [OwnerLocation]
		, [Owner].[DisplayName] AS [OwnerDisplayName]
		, [Requester].[FacebookId] AS [RequesterFacebookId]
		, [Requester].[Key] AS [RequesterKey]
		, [Requester].[DisplayName] AS [RequesterDisplayName]
		, [Requester].[Location] AS [RequesterLocation]
		, [Requester].[UserIdentifier] AS [RequesterUserIdentifier]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH(NOLOCK)
			WHERE [ItemIdentifier] = [Item].Identifier
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
		INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
			ON [Share].[ItemIdentifier] = [Item].[Identifier]
				AND [Item].[UserIdentifier] = COALESCE(@OwnerIdentifier, [Item].[UserIdentifier])
				AND [Share].[Identifier] = COALESCE(@Identifier, [Share].[Identifier])
				AND [Share].[ItemIdentifier] = COALESCE(@ItemIdentifier, [Share].[ItemIdentifier])
				AND [Share].[UserIdentifier] = COALESCE(@RequesterIdentifier, [Share].[UserIdentifier])
		INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
		INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
			ON [Share].[UserIdentifier] = [Requester].[UserIdentifier]

END