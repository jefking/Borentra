CREATE TABLE [Social].[Rating]
(
	[ReferenceIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Rating] TINYINT NOT NULL DEFAULT 0, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    PRIMARY KEY ([UserIdentifier], [ReferenceIdentifier]),
    CONSTRAINT [FK_Social_Rating_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile] ([UserIdentifier])
)
