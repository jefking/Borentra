CREATE TABLE [Goods].[ItemImage] (
    [Identifier]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ItemIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [Path]           NVARCHAR (256)   NOT NULL,
    [FileName]       NVARCHAR (256)   NOT NULL,
    [ContentType]    NVARCHAR (64)    NOT NULL,
    [FileSize]       INT              NOT NULL DEFAULT 0,
    [IsPrimary]      BIT              DEFAULT ((0)) NULL,
    [CreatedOn]      SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]     SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]      SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Goods_ItemImage] PRIMARY KEY CLUSTERED ([Identifier] ASC),

);


GO


CREATE INDEX [IX_ItemImage_ItemIdentifier] ON [Goods].[ItemImage] ([ItemIdentifier])
