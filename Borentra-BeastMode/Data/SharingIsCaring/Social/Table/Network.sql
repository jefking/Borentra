CREATE TABLE [Social].[Network] (
    [OwnerIdentifier]      UNIQUEIDENTIFIER NOT NULL,
    [ConnectionIdentifier] UNIQUEIDENTIFIER NOT NULL,
    [NetworkType]          TINYINT          DEFAULT ((0)) NOT NULL,
    [CreatedOn]            SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]           SMALLDATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]            SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_data_item] PRIMARY KEY CLUSTERED ([OwnerIdentifier] ASC, [ConnectionIdentifier] ASC),
    CONSTRAINT [FK_User_Profile_UserIdentifier_User_Network_Connection] FOREIGN KEY ([ConnectionIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]),
    CONSTRAINT [FK_User_Profile_UserIdentifier_User_Network_Owner] FOREIGN KEY ([OwnerIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
);


GO

CREATE INDEX [IX_Network_OwnerIdentifier] ON [Social].[Network] ([OwnerIdentifier])

GO
