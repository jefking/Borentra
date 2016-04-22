CREATE TABLE [Company].[CompanyArchive]
(
	[CompanyIdentifier] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Key] NVARCHAR(286) NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [ModifiedOn] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [DeletedOn] DATETIME NULL DEFAULT NULL, 
    CONSTRAINT [FK_ItemArchive_Compnany_Company] FOREIGN KEY ([CompanyIdentifier]) REFERENCES [Company].[Company]([Identifier]), 
)
