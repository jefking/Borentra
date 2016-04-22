CREATE TABLE [Goods].[ItemRequestArchive]
(
	[ItemRequestIdentifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Key] NVARCHAR(286) NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_ItemArchive_Goods_ItemRequest] FOREIGN KEY ([ItemRequestIdentifier]) REFERENCES [Goods].[ItemRequest]([Identifier]), 
)
