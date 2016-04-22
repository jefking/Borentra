CREATE TABLE [Goods].[ItemRequestImage] (
    [Identifier]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ItemRequestIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [Path]           NVARCHAR (256)   NOT NULL,
    [FileName]       NVARCHAR (256)   NOT NULL,
    [ContentType]    NVARCHAR (64)    NOT NULL,
    [FileSize]       INT              NOT NULL,
    [IsPrimary]      BIT              DEFAULT ((0)) NULL,
    [CreatedOn]      SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]     SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]      SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_Goods_ItemRequestImage] PRIMARY KEY CLUSTERED ([Identifier] ASC)
);


GO


CREATE INDEX [IX_ItemRequestImage_ItemRequestIdentifier] ON [Goods].[ItemRequestImage] ([ItemRequestIdentifier])
