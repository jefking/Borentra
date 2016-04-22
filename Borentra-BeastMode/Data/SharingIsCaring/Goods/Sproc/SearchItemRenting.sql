CREATE PROCEDURE [Goods].[SearchItemRenting]
	@OwnerIdentifier [uniqueidentifier] = NULL
	, @RequesterIdentifier [uniqueidentifier] = NULL
	, @Identifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [rent].[Identifier]
		, [rent].[ItemIdentifier]
		, [rent].[On]
		, [rent].[Until]
		, [rent].[ReturnedOn]
		, [rent].[Status]
		, [rent].[Price]
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
	FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
		INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
			ON [rent].[ItemIdentifier] = [Item].[Identifier]
				AND [Item].[UserIdentifier] = COALESCE(@OwnerIdentifier, [Item].[UserIdentifier])
				AND [rent].[UserIdentifier] = COALESCE(@RequesterIdentifier, [rent].[UserIdentifier])
				AND [rent].[Identifier] = COALESCE(@Identifier, [rent].[Identifier])
		INNER JOIN [User].[vwProfile] [Owner] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Owner].[UserIdentifier]
		INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
			ON [rent].[UserIdentifier] = [Requester].[UserIdentifier]

END