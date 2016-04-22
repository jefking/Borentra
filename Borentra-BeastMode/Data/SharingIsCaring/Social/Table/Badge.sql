CREATE TABLE [Social].[Badge]
(
	[UserIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [BadgeId] TINYINT NOT NULL, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    PRIMARY KEY ([UserIdentifier], [BadgeId]), 
    CONSTRAINT [FK_Badge_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier]), 
    CONSTRAINT [FK_Badge_Social_BadgeInformation] FOREIGN KEY ([BadgeId]) REFERENCES [Social].[BadgeInformation]([Identifier])
)

GO

CREATE INDEX [IX_Badge_UserIdentifier] ON [Social].[Badge] ([UserIdentifier])
