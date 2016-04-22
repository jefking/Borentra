CREATE TABLE [Goods].[ItemTrade]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [TradeIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [ItemIdentifier] UNIQUEIDENTIFIER NOT NULL, 
	[CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    CONSTRAINT [PK_Goods_ItemTrade] PRIMARY KEY CLUSTERED ([Identifier]),
    CONSTRAINT [FK_Goods_ItemTrade_Goods_Trade] FOREIGN KEY ([TradeIdentifier]) REFERENCES [Goods].[Trade]([Identifier]), 
    CONSTRAINT [FK_Goods_ItemTrade_Goods_Item] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item]([Identifier])
)

GO

CREATE INDEX [IX_ItemTrade_ItemIdentifier] ON [Goods].[ItemTrade] ([ItemIdentifier])
