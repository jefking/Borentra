CREATE VIEW [Company].[vwCompanyAdministrator]
AS
	SELECT CompanyIdentifier
		, UserIdentifier
	FROM [Company].[CompanyAdministrator]
	WHERE DeletedOn IS NULL