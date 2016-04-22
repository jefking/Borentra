CREATE VIEW [Social].[vwNetwork]
AS
	SELECT [OwnerIdentifier]
		, [ConnectionIdentifier]
	FROM [Social].[Network] WITH(NOLOCK)
	WHERE [DeletedOn] IS NULL