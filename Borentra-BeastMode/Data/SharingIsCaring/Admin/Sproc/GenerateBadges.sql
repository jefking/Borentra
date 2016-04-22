CREATE PROCEDURE [Admin].[GenerateBadges]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	EXECUTE [Social].[CreateBadges]

	-- 1 = PREMIUM
	-- We need to migrate this to a PAID badge
	--INSERT INTO [Social].[Badge]
	--(
	--	UserIdentifier
	--	, BadgeId
	--	, CreatedOn
	--	, ModifiedOn
	--)
	--SELECT DISTINCT [profile].[UserIdentifier]
	--	, 1
	--	, GETUTCDATE()
	--	, GETUTCDATE()
	--FROM [User].[vwProfile] [profile] WITH (NOLOCK)
	--WHERE [profile].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
	--										FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
	--										WHERE [BadgeId] = 1);

	-- 2 = Lender
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [share].[UserIdentifier]
		, 2
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [Goods].[vwItemShare] [share] WITH (NOLOCK)
	WHERE [share].[Status] IN (1, 2)
		AND [share].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 2)
											
	-- 3 = Borrower
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [share].[UserIdentifier]
		, 3
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [Goods].[vwItemShare] [share] WITH (NOLOCK)
	WHERE [Status] IN (1, 2)
		AND [share].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 3);
												
	-- 5 = Conversation Starter
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [conversation].[UserIdentifier]
		, 5
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [Social].[vwConversation] [conversation] WITH (NOLOCK)
	WHERE [ParentConversationIdentifier] IS NULL
		AND [conversation].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 5);

	-- 6 = Ambassador
	--MERGE [Social].[Badge] AS [Badge]
	--USING (
	--	SELECT 6 AS [BadgeId]
	--		, '' AS [UserIdentifier]
	--	) AS NewData
	--ON [Badge].[UserIdentifier] = [NewData].[UserIdentifier]
	--	AND [Badge].[BadgeId] = [NewData].[BadgeId]
	--WHEN NOT MATCHED
	--THEN INSERT
	--	(
	--		[BadgeId]
	--		, [UserIdentifier]
	--	)
	--	VALUES
	--	(
	--		[NewData].[BadgeId]
	--		, [NewData].[UserIdentifier]
	--	);
							
	-- 7 = Friendly
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
	)
	SELECT DISTINCT [stats].[UserIdentifier]
		, 7
	FROM [User].[vwProfileStats] [stats] WITH (NOLOCK)
	WHERE [stats].[FriendCount] > 0
		AND [stats].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 7);
	-- 8 = Trader
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
	)
	SELECT DISTINCT [stats].[UserIdentifier]
		, 8
	FROM [User].[vwProfileStats] [stats] WITH (NOLOCK)
	WHERE [stats].[TradeCount] > 0
		AND [stats].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 8);
											
	-- 9 = Giver
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
	)
	SELECT DISTINCT [stats].[UserIdentifier]
		, 9
	FROM [User].[vwProfileStats] [stats] WITH (NOLOCK)
	WHERE [stats].[GiveCount] > 0
		AND [stats].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 9);
											
	-- 10 = Reciever
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
	)
	SELECT DISTINCT [stats].[UserIdentifier]
		, 10
	FROM [User].[vwProfileStats] [stats] WITH (NOLOCK)
	WHERE [stats].[RecieveCount] > 0
		AND [stats].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 10);

	-- Rent Owner
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [rent].[UserIdentifier]
		, 13
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
	WHERE [Status] IN (1, 2)
		AND [rent].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 13);
											
	-- Renter
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [rent].[UserIdentifier]
		, 12
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
	WHERE [Status] IN (1, 2)
		AND [rent].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 12);
											
	-- Many Offers
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [profile].[UserIdentifier]
		, 14
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [User].[vwProfileWithStats] [profile] WITH (NOLOCK)
	WHERE [profile].[ItemCount] >= 10
		AND [profile].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 14);
											
	-- Many Wants
	INSERT INTO [Social].[Badge]
	(
		UserIdentifier
		, BadgeId
		, CreatedOn
		, ModifiedOn
	)
	SELECT DISTINCT [profile].[UserIdentifier]
		, 15
		, GETUTCDATE()
		, GETUTCDATE()
	FROM [User].[vwProfileWithStats] [profile] WITH (NOLOCK)
	WHERE [profile].[ItemRequestCount] >= 10
		AND [profile].[UserIdentifier] NOT IN (SELECT [badge].[UserIdentifier]
											FROM [Social].[vwBadge] [badge] WITH (NOLOCK)
											WHERE [BadgeId] = 15);

END