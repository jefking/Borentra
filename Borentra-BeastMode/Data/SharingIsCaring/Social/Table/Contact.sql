CREATE TABLE [Social].[Contact]
(
	[UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [FirstName] NVARCHAR(128) NULL, 
    [LastName] NVARCHAR(128) NULL, 
    [PhoneNumber] NVARCHAR(18) NULL, 
    [Invite] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    CONSTRAINT [PK_Contact] PRIMARY KEY ([UserIdentifier], [Email]) 
)
