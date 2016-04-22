CREATE PROCEDURE [Stats].[Dashboard]
	@UserIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		DECLARE @NewRequests INT
		SELECT @NewRequests = COUNT(0)
		FROM [Goods].[vwItemRequestFulfill] [fulfill] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemRequest] [request] WITH(NOLOCK)
				ON [request].[Identifier] = [fulfill].[ItemRequestIdentifier]
					AND @UserIdentifier IN ([request].[UserIdentifier], [fulfill].[UserIdentifier])
					AND [fulfill].[Status] = 0

		SELECT @NewRequests = @NewRequests + COUNT(0)
		FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemFree] [free] WITH(NOLOCK)
				ON [item].[Identifier] = [free].[ItemIdentifier]
					AND @UserIdentifier IN ([free].[UserIdentifier], [item].[UserIdentifier])
					AND [free].[Status] = 0

		SELECT @NewRequests = @NewRequests + COUNT(0)
		FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemShare] [share] WITH(NOLOCK)
				ON [item].[Identifier] = [share].[ItemIdentifier]
					AND @UserIdentifier IN ([share].[UserIdentifier], [item].[UserIdentifier])
					AND [share].[Status] IN (0, 1)

		DECLARE @History INT

		SELECT @History = COUNT(0)
		FROM [Goods].[vwItemRequestFulfill] [fulfill] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemRequest] [request] WITH(NOLOCK)
				ON [request].[Identifier] = [fulfill].[ItemRequestIdentifier]
					AND @UserIdentifier IN ([request].[UserIdentifier], [fulfill].[UserIdentifier])
					AND [fulfill].[Status] > 0

		SELECT @History = @History + COUNT(0)
		FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemShare] [share] WITH(NOLOCK)
				ON [item].[Identifier] = [share].[ItemIdentifier]
					AND [share].[Status] > 1
					AND @UserIdentifier IN ([share].[UserIdentifier], [item].[UserIdentifier])

		SELECT @History = @History + COUNT(0)
		FROM [Goods].[vwItem] [item] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemFree] [free] WITH(NOLOCK)
				ON [item].[Identifier] = [free].[ItemIdentifier]
					AND [free].[Status] > 0
					AND @UserIdentifier IN ([free].[UserIdentifier], [item].[UserIdentifier])

		DECLARE @Messages INT

		SELECT @Messages = COUNT(0)
		FROM [Social].[vwConversation] [msg] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [msg].[UserIdentifier] = [profile].[UserIdentifier]
					AND ToUserIdentifier = @UserIdentifier
					AND [Read] = 0

		DECLARE @NearBy INT
		DECLARE @Friends INT
		DECLARE @Items INT
		DECLARE @ItemRequests INT
		DECLARE @TradeCount INT
		DECLARE @FriendsOffersCount INT

		SELECT @Items = ItemCount
			, @ItemRequests = ItemRequestCount
			, @Friends = FriendCount
			, @NearBy = MembersNearby
			, @TradeCount = TradeCount
			, @FriendsOffersCount = FriendsOffersCount
		FROM [User].[vwProfileStats] WITH(NOLOCK)
		WHERE UserIdentifier = @UserIdentifier

		SELECT [dbo].[EnsureSet](@NearBy, 0) AS [PeopleNearByCount]
			, [dbo].[EnsureSet](@Items, 0) AS [ItemCount]
			, [dbo].[EnsureSet](@ItemRequests, 0) AS [ItemRequestCount]
			, [dbo].[EnsureSet](@Friends, 0) AS [FriendCount]
			, [dbo].[EnsureSet](@History, 0) AS [HistoryCount]
			, [dbo].[EnsureSet](@NewRequests, 0) AS [NewRequestCount]
			, [dbo].[EnsureSet](@Messages, 0) AS [MessageCount]
			, [dbo].[EnsureSet](@TradeCount, 0) AS [TradeCount]
			, [dbo].[EnsureSet](@FriendsOffersCount, 0) AS [FriendsOffersCount]

	END
END