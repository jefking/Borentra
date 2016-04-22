CREATE TABLE [Goods].[ItemShare] (
    [Identifier]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ItemIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [On]             SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [Until]          SMALLDATETIME    DEFAULT (NULL) NULL,
    [ReturnedOn]     SMALLDATETIME    DEFAULT (NULL) NULL,
    [Status]         TINYINT          DEFAULT ((0)) NOT NULL,
    [CreatedOn]      SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]     SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]      SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Goods_ItemShare] PRIMARY KEY CLUSTERED ([Identifier] ASC),
    CONSTRAINT [FK_User_Profile_Goods_ItemShare] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]),
    CONSTRAINT [FK_Goods_Item_Goods_ItemShare] FOREIGN KEY ([ItemIdentifier]) REFERENCES [Goods].[Item] ([Identifier])
);


GO

CREATE INDEX [IX_ItemShare_ItemIdentifier] ON [Goods].[ItemShare] ([ItemIdentifier])

GO

CREATE INDEX [IX_ItemShare_UserIdentifier] ON [Goods].[ItemShare] ([UserIdentifier])
