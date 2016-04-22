CREATE VIEW [User].[vwProfileArchive]
AS
	SELECT UserIdentifier
		, LandingTheme
	FROM [User].[ProfileArchive]
	WHERE DeletedOn IS NULL