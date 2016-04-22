CREATE TABLE [Goods].[Trade]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [UserIdentifier1] UNIQUEIDENTIFIER NOT NULL, 
    [UserIdentifier2] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT (GETUTCDATE()), 
    [AcceptedOn] SMALLDATETIME NULL, 
    [RejectedOn] SMALLDATETIME NULL, 
    [DeletedOn] SMALLDATETIME NULL,
	[ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [PK_Goods_Trade] PRIMARY KEY CLUSTERED ([Identifier]),
    CONSTRAINT [FK_Goods_Trade_User_Profile1] FOREIGN KEY ([UserIdentifier1]) REFERENCES [User].[Profile]([UserIdentifier]),
	CONSTRAINT [FK_Goods_Trade_User_Profile2] FOREIGN KEY ([UserIdentifier2]) REFERENCES [User].[Profile]([UserIdentifier])
)