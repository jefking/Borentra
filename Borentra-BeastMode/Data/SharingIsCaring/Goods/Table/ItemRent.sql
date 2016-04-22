CREATE TABLE [Goods].[ItemRent]
(
	[ItemIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [Identifier] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [Price] MONEY NOT NULL DEFAULT 0, 
    [PerUnit] TINYINT NOT NULL DEFAULT 1, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    CONSTRAINT [FK_ItemRent_Goods_Item] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item]([Identifier]), 
    PRIMARY KEY ([Identifier])
)

GO

CREATE UNIQUE INDEX [UX_ItemRent_ItemIdentifier] ON [Goods].[ItemRent] ([ItemIdentifier])