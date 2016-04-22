CREATE TABLE [Company].[CompanyAdministrator]
(
	[CompanyIdentifier] UNIQUEIDENTIFIER NOT NULL , 
    [UserIdentifier] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] SMALLDATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [DeletedOn] SMALLDATETIME NULL, 
    PRIMARY KEY ([UserIdentifier], [CompanyIdentifier]), 
    CONSTRAINT [FK_CompanyAdministrators_Company_Company] FOREIGN KEY ([CompanyIdentifier]) REFERENCES [Company].[Company]([Identifier]), 
    CONSTRAINT [FK_CompanyAdministrators_User_Profile] FOREIGN KEY ([UserIdentifier]) REFERENCES [User].[Profile]([UserIdentifier])
)
