CREATE VIEW [Goods].[vwItemActionComment]
AS
	SELECT Identifier
		, ItemActionIdentifier
		, UserIdentifier
		, Comment
		, CreatedOn
	FROM [Goods].[ItemActionComment]
	WHERE DeletedOn IS NULL