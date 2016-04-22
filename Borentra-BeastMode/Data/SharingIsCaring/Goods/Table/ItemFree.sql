CREATE TABLE [Goods].[ItemFree]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ItemIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    [Status] TINYINT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ItemFree_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier]),
    CONSTRAINT [FK_ItemFree_Goods_Items] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item]([Identifier])
)

GO

CREATE INDEX [IX_ItemFree_ItemIdentifier] ON [Goods].[ItemFree] ([ItemIdentifier])

GO

CREATE INDEX [IX_ItemFree_UserIdentifier] ON [Goods].[ItemFree] ([UserIdentifier])
