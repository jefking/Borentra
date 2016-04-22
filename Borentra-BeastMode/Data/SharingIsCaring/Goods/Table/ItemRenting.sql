CREATE TABLE [Goods].[ItemRenting]
(
    [Identifier] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
	[ItemIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Price] MONEY NOT NULL DEFAULT 0,
    [On]             DATETIME2    DEFAULT (GETUTCDATE()) NOT NULL,
    [Until]          DATETIME2    DEFAULT GETUTCDATE() NOT NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] DATETIME2 NULL DEFAULT NULL,  
    [ReturnedOn] DATETIME2 NULL DEFAULT NULL, 
    [Status] TINYINT NOT NULL DEFAULT 0, 
    PRIMARY KEY ([Identifier]), 
    CONSTRAINT [FK_ItemRenting_Goods_ItemIdentifier] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item]([Identifier])
)