CREATE TABLE [Social].[Conversation]
(
	[Identifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
	[ParentConversationIdentifier] UNIQUEIDENTIFIER NULL DEFAULT NULL, 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [ToUserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Read] BIT NOT NULL DEFAULT 0,
    [CreatedOn]            DATETIME2    DEFAULT (GETUTCDATE()) NOT NULL,
    [ModifiedOn]           DATETIME    DEFAULT (GETUTCDATE()) NOT NULL,
    [DeletedOn]            SMALLDATETIME    DEFAULT (NULL) NULL,
    [Comment] NVARCHAR(2048) NOT NULL,
    CONSTRAINT [FK_User_Profile_Social_Conversation_UserIdentifier] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier]),
    CONSTRAINT [FK_User_Profile_Social_Conversation_ToUserIdentifier] FOREIGN KEY ([ToUserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
)
