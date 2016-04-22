CREATE PROCEDURE [Admin].[GenerateForProfile]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	EXECUTE [Admin].[AddToProfileStats];
	
	CREATE TABLE #Stats
	(
		UserIdentifier uniqueidentifier
		, BorrowCount int DEFAULT 0
		, LendCount int DEFAULT 0
		, RecieveCount int DEFAULT 0
		, GiveCount int DEFAULT 0
		, ItemCount smallint DEFAULT 0
		, ItemRequestCount smallint DEFAULT 0
		, TradeCount int DEFAULT 0
		, Points smallint DEFAULT 0
	)
	
	INSERT INTO #Stats
	(
		UserIdentifier
		, BorrowCount
		, LendCount
		, RecieveCount
		, GiveCount
		, ItemCount
		, ItemRequestCount
		, TradeCount
		, Points
	)
	SELECT [profile].[UserIdentifier]
		, (SELECT COUNT(0)
			FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
			WHERE [Share].[UserIdentifier] = [profileStats].[UserIdentifier]
				AND [Share].[Status] = 2)
		, (SELECT COUNT(0)
			FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Share].[ItemIdentifier] = [Item].[Identifier]
						AND [Item].[UserIdentifier] = [profileStats].[UserIdentifier]
						AND [Share].[Status] = 2)
		, (SELECT COUNT(0)
			FROM [Goods].[vwItemFree] [Free] WITH (NOLOCK)
			WHERE [Free].[UserIdentifier] = [profileStats].[UserIdentifier]
				AND [Free].[Status] = 1)
		, (SELECT COUNT(0)
			FROM [Goods].[vwItemFree] [Free] WITH (NOLOCK)
				INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
					ON [Free].[ItemIdentifier] = [Item].[Identifier]
						AND [Item].[UserIdentifier] = [profileStats].[UserIdentifier]
						AND [Free].[Status] = 1)
		, (SELECT COUNT(0)
			FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			WHERE [item].[UserIdentifier] = [profileStats].[UserIdentifier])
		, (SELECT COUNT(0)
			FROM [Goods].[vwItemRequest] [request] WITH(NOLOCK)
			WHERE [request].[UserIdentifier] = [profileStats].[UserIdentifier])
		, (SELECT COUNT(*)
			FROM [Goods].[vwTrade] [Trade] WITH (NOLOCK)
			WHERE [profileStats].[UserIdentifier] IN ([Trade].[UserIdentifier2], [Trade].[UserIdentifier1])
				AND [Trade].[AcceptedOn] IS NOT NULL)
		, (SELECT SUM([info].[Points]) AS [Points]
			FROM [Social].[vwBadgeInformation] [info] WITH(NOLOCK)
				INNER JOIN [Social].[vwBadge] [badge] WITH(NOLOCK)
					ON [info].[Identifier] = [badge].[BadgeId]
						AND [badge].[UserIdentifier] = [profileStats].[UserIdentifier])
	FROM [User].[ProfileStats] [profileStats] WITH (NOLOCK)
		INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
			ON [profile].[UserIdentifier] = [profileStats].[UserIdentifier]

	UPDATE [profileStats]
	SET ModifiedOn = GETUTCDATE()
		, BorrowCount = [stats].[BorrowCount]
		, LendCount = [stats].[LendCount]
		, RecieveCount = [stats].[RecieveCount]
		, GiveCount = [stats].[GiveCount]
		, ItemCount = [stats].[ItemCount]
		, ItemRequestCount = [stats].[ItemRequestCount]
		, TradeCount = [stats].[TradeCount]
		, Points = [stats].[Points]
	FROM [User].[ProfileStats] [profileStats]
		INNER JOIN #Stats [stats] WITH(NOLOCK)
			ON [stats].[UserIdentifier] = [profileStats].[UserIdentifier]
				AND
				(
					[profileStats].[BorrowCount] <> [stats].[BorrowCount]
					OR
					[profileStats].[LendCount] <> [stats].[LendCount]
					OR
					[profileStats].[RecieveCount] <> [stats].[RecieveCount]
					OR
					[profileStats].[GiveCount] <> [stats].[GiveCount]
					OR
					[profileStats].[ItemCount] <> [stats].[ItemCount]
					OR
					[profileStats].[ItemRequestCount] <> [stats].[ItemRequestCount]
					OR
					[profileStats].[TradeCount] <> [stats].[TradeCount]
					OR
					[profileStats].[Points] <> [stats].[Points]
				)

	CLEANUP:
		DROP TABLE #Stats;

END