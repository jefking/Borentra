CREATE TABLE [Goods].[Item] (
    [Identifier]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (256)   NOT NULL,
    [Description]    NVARCHAR (2048)  NULL,
    [CreatedOn]      SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]     SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]      SMALLDATETIME    DEFAULT (NULL) NULL,
    [Key] NVARCHAR(286) NULL, 
    [TradePrivacyLevel] TINYINT NOT NULL DEFAULT 4, 
    [FreePrivacyLevel] TINYINT NOT NULL DEFAULT 4, 
    [SharePrivacyLevel] TINYINT NOT NULL DEFAULT 4, 
    [RentPrivacyLevel] TINYINT NOT NULL DEFAULT 4, 
    [FromItemIdentifier] UNIQUEIDENTIFIER NULL, 
    [FromItemRequestIdentifier] UNIQUEIDENTIFIER NULL,
	[MinimumPrivacyLevel] AS [dbo].[Minimum]([TradePrivacyLevel], [FreePrivacyLevel], [SharePrivacyLevel], [RentPrivacyLevel])
    CONSTRAINT [PK_data_item] PRIMARY KEY CLUSTERED ([Identifier] ASC),
    CONSTRAINT [FK_User_Profile_Goods_Item] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]), 
    CONSTRAINT [AK_Item_Key] UNIQUE ([Key]), 
    CONSTRAINT [FK_Goods_Item_Goods_Item] FOREIGN KEY ([FromItemIdentifier]) REFERENCES [Goods].[Item]([Identifier]), 
    CONSTRAINT [FK_Goods_Item_Goods_ItemRequest] FOREIGN KEY ([FromItemRequestIdentifier]) REFERENCES [Goods].[ItemRequest]([Identifier]) 
);


GO

CREATE INDEX [IX_Item_UserIdentifier] ON [Goods].[Item] ([UserIdentifier])

GO
