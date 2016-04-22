CREATE TABLE [Social].[Comment]
(
	[ReferenceIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Comment] NVARCHAR(512) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    [Identifier] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([Identifier]), 
    CONSTRAINT [FK_Social_Comment_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
)

GO

CREATE INDEX [IX_Comment_ReferenceIdentifier] ON [Social].[Comment] ([ReferenceIdentifier])

GO

CREATE INDEX [IX_Comment_UserIdentifier] ON [Social].[Comment] ([UserIdentifier])
