CREATE TABLE [Social].[Tags]
(
	[UserIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [ReferenceIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Tags] NVARCHAR(128) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] DATETIME NULL, 
    PRIMARY KEY ([ReferenceIdentifier], [UserIdentifier])
)
