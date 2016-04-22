CREATE TABLE [User].[ProfileArchive]
(
	[UserIdentifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Key] NVARCHAR(286) NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [LandingTheme] NVARCHAR(128) NULL DEFAULT NULL, 
    [DeletedOn] DATETIME NULL DEFAULT NULL, 
    CONSTRAINT [FK_ItemArchive_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier]), 
)
