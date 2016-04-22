CREATE VIEW [Company].[vwCompany]
AS
	SELECT [UserIdentifier] AS Identifier
		, [LogoPath]
		, [Description]
		, [WebsiteUrl]
		, [PhoneNumber]
		, [company].[Key]
		, [company].[CreatedOn]
		, [profile].[Email]
		, [profile].[Location]
		, [profile].[GeoLocation]
		, [profile].[DisplayName] AS [Name]
		, [company].[TwitterHandle] AS [Twitter]
		, [company].[FacebookHandle] AS [Facebook]
	FROM [Company].[Company] [company]
		INNER JOIN [User].[vwProfile] [profile]
			ON [company].[Identifier] = [profile].[UserIdentifier]
	WHERE DeletedOn IS NULL