CREATE TABLE [Goods].[ItemActionComment] (
    [Identifier]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ItemActionIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [UserIdentifier]      UNIQUEIDENTIFIER NOT NULL,
    [Comment]             NVARCHAR (1024)  NOT NULL,
    [CreatedOn]           SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]          SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]           SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Goods_ItemAction_Comment] PRIMARY KEY CLUSTERED ([Identifier] ASC),
    CONSTRAINT [FK_User_Profile_Goods_ItemActionComment] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
);


GO

CREATE INDEX [IX_ItemActionComment_ItemActionIdentifier] ON [Goods].[ItemActionComment] ([ItemActionIdentifier])

GO

CREATE INDEX [IX_ItemActionComment_UserIdentifier] ON [Goods].[ItemActionComment] ([UserIdentifier])
