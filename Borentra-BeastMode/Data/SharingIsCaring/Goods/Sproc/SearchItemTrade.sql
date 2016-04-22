CREATE PROCEDURE [Goods].[SearchItemTrade]
	@TradeIdentifier uniqueidentifier = NULL,
	@ReceiverIdentifier uniqueIdentifier = NULL,
	@RequesterIdentifier uniqueIdentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT [Trade].[Identifier] AS [TradeIdentifier]
		, [Trade].[AcceptedOn] AS [AcceptedOn]
		, [Trade].[CreatedOn] AS [CreatedOn]
		, [Trade].[ModifiedOn] AS [ModifiedOn]
		, [Trade].[RejectedOn] AS [RejectedOn]
		, [Requester].[Key] AS [RequesterKey]
		, [Requester].[FacebookId] AS [RequesterFacebookId]
		, [Requester].[DisplayName] AS [RequesterName]
		, [Requester].[UserIdentifier] AS [RequesterIdentifier]
		, [Requester].[Email] AS [RequesterEmail]
		, [Receiver].[Key] AS [ReceiverKey]
		, [Receiver].[FacebookId] AS [ReceiverFacebookId]
		, [Receiver].[DisplayName] AS [ReceiverName]
		, [Receiver].[UserIdentifier] AS [ReceiverIdentifier]
		, [Receiver].[Email] AS [ReceiverEmail]
		, [Item].[Identifier] AS [ItemIdentifier]
		, [Item].UserIdentifier AS [ItemOwnerIdentifier]
		, [Item].[Title] AS [ItemTitle]
		, [Item].[Key] AS [TradeItemKey]
		, [Item].[Description] AS [ItemDescription]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH(NOLOCK)
			WHERE [ItemIdentifier] = [Item].Identifier
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
		FROM [Goods].[vwTrade] [Trade] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemTrade] [ItemTrade] WITH (NOLOCK)
				ON [Trade].[Identifier] = [ItemTrade].[TradeIdentifier]
					AND [Trade].[RejectedOn] IS NULL
					AND [Trade].[AcceptedOn] IS NULL
					AND [Trade].[Identifier] = COALESCE(@TradeIdentifier, [ItemTrade].[TradeIdentifier])
			INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
				ON  [Item].[Identifier] = [ItemTrade].[ItemIdentifier]
			INNER JOIN [User].[vwProfile] [Requester] WITH (NOLOCK)
				ON [Requester].[UserIdentifier] = [Trade].[UserIdentifier1]
					AND [Requester].[UserIdentifier] = COALESCE(@RequesterIdentifier, [Trade].[UserIdentifier1])
			INNER JOIN [User].[vwProfile] [Receiver] WITH (NOLOCK)
				ON [Receiver].[UserIdentifier] = [Trade].[UserIdentifier2]
					AND [Receiver].[UserIdentifier] = COALESCE(@ReceiverIdentifier, [Trade].[UserIdentifier2])

END