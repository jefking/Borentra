CREATE TABLE [Social].[Favorite]
(
	[ReferenceIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    PRIMARY KEY ([ReferenceIdentifier], [UserIdentifier]),
    CONSTRAINT [FK_Social_Favorite_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
)
