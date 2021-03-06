﻿CREATE VIEW [User].[vwProfileStats]
AS
	SELECT [UserIdentifier]
		, [BorrowCount]
		, [LendCount]
		, [GiveCount]
		, [RecieveCount]
		, [TradeCount]
		, [ItemRequestCount]
		, [ItemCount]
		, [FriendCount]
		, [MembersNearby]
		, [Points]
		, [FriendsOffersCount]
	FROM [User].[ProfileStats]
	WHERE DeletedOn IS NULL