CREATE TABLE [User].[Activity] (
    [Identifier]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [ReferenceIdentifier] UNIQUEIDENTIFIER NULL,
    [UserIdentifier]      UNIQUEIDENTIFIER NOT NULL,
    [Text]                NVARCHAR (512)   NOT NULL,
    [Type]                TINYINT          DEFAULT ((0)) NOT NULL,
    [CreatedOn]           DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]          DATETIME2    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]           SMALLDATETIME    DEFAULT (NULL) NULL,
    CONSTRAINT [PK_User_Activity] PRIMARY KEY NONCLUSTERED ([Identifier]),
    CONSTRAINT [FK_User_Profile_Users_User_Activity] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]), 
);


GO

CREATE INDEX [IX_Activity_UserIdentifier] ON [User].[Activity] ([UserIdentifier])

GO


CREATE INDEX [IX_Activity_ReferenceIdentifier] ON [User].[Activity] ([ReferenceIdentifier])

GO

CREATE CLUSTERED INDEX [IX_Activity_ModifiedOn] ON [User].[Activity] ([ModifiedOn] DESC)
