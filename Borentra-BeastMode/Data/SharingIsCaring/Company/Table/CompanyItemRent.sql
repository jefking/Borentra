CREATE TABLE [Company].[CompanyItemRent]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [ItemRentIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Quantity] TINYINT NOT NULL DEFAULT 1, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    CONSTRAINT [FK_CompanyItemRent_Goods_ItemRent] FOREIGN KEY ([ItemRentIdentifier]) REFERENCES [Goods].[ItemRent]([Identifier])
)
