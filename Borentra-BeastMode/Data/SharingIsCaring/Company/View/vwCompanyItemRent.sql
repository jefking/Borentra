CREATE VIEW [Company].[vwCompanyItemRent]
AS
	SELECT [Identifier]
		, [ItemRentIdentifier]
		, [Quantity]
	FROM [Company].[CompanyItemRent]
	WHERE DeletedOn IS NULL