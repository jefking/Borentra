CREATE TABLE [Goods].[ItemRequestFulfill]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ItemRequestIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Status] TINYINT NOT NULL DEFAULT 0, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [WillRent] BIT NOT NULL DEFAULT 0, 
    [WillTrade] BIT NOT NULL DEFAULT 0, 
    [WillGive] BIT NOT NULL DEFAULT 0, 
    [WillShare] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_User_Profile_Goods_ItemRequestFulfill] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]),
    CONSTRAINT [FK_Goods_Item_Goods_ItemRequestFulfill] FOREIGN KEY ([ItemRequestIdentifier]) REFERENCES [Goods].[ItemRequest] ([Identifier])
)

GO

CREATE INDEX [IX_ItemRequestFulfill_ItemRequestIdentifier] ON [Goods].[ItemRequestFulfill] ([ItemRequestIdentifier])

GO

CREATE INDEX [IX_ItemRequestFulfill_UserIdentifier] ON [Goods].[ItemRequestFulfill] ([UserIdentifier])
