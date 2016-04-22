﻿CREATE TABLE [User].[ProfileStats]
(
	[UserIdentifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BorrowCount] SMALLINT NOT NULL DEFAULT 0, 
    [LendCount] SMALLINT NOT NULL DEFAULT 0, 
    [RecieveCount] SMALLINT NOT NULL DEFAULT 0, 
    [GiveCount] SMALLINT NOT NULL DEFAULT 0, 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] DATETIME NULL DEFAULT NULL, 
    [TradeCount] SMALLINT NOT NULL DEFAULT 0, 
    [ItemCount] SMALLINT NOT NULL DEFAULT 0, 
    [ItemRequestCount] SMALLINT NOT NULL DEFAULT 0, 
    [FriendCount] SMALLINT NOT NULL DEFAULT 0, 
    [MembersNearby] INT NOT NULL DEFAULT 0, 
    [Points] INT NOT NULL DEFAULT 0, 
    [FriendsOffersCount] SMALLINT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_User_ProfileStats_ToTable] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier])
)
