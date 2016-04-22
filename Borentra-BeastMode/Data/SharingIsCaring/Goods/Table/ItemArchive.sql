CREATE TABLE [Goods].[ItemArchive]
(
	[ItemIdentifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Key] NVARCHAR(286) NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_ItemArchive_Goods_Item] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item]([Identifier]), 
)
