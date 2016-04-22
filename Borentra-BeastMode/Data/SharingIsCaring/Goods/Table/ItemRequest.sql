CREATE TABLE [Goods].[ItemRequest] (
    [Identifier]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (256)   NOT NULL,
    [Description]    NVARCHAR (2048)  NULL,
    [CreatedOn]      SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]     SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]      SMALLDATETIME    DEFAULT (NULL) NULL,
	[ForTrade] BIT DEFAULT 0 NOT NULL,
	[ForRent] BIT DEFAULT 0 NOT NULL,
	[ForFree] BIT DEFAULT 0 NOT NULL,
	[ForShare] BIT DEFAULT 0 NOT NULL, 
    [Key] NVARCHAR(286) NULL, 
    CONSTRAINT [PK_Goods_itemRequest] PRIMARY KEY CLUSTERED ([Identifier] ASC),
    CONSTRAINT [FK_User_Profile_Goods_ItemRequest] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]),
    CONSTRAINT [AK_ItemRequest_Key] UNIQUE ([Key]) 
	);


GO

CREATE INDEX [IX_ItemRequest_UserIdentifier] ON [Goods].[ItemRequest] ([UserIdentifier])

GO
