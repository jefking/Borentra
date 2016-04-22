CREATE PROCEDURE [Goods].[SearchForRss]
	@Top smallint = 100
	, @ResultType tinyint = 0 --0=both, 1=Item, 2=Request
	, @WithImages bit = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN
	
	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);
	SET @ResultType = [dbo].[EnsureSetTinyInt](@ResultType, 0);
	SET @WithImages = [dbo].[EnsureSetBool](@WithImages);

	IF @Top > 100
	BEGIN

		SET @Top = 100;

	END
	
	CREATE TABLE #Results
	(
		[Title] nvarchar(256)
		, [Description] nvarchar(2048)
		, [ModifiedOn] datetime
		, [Key] nvarchar(268)
		, [PrimaryImagePathFormat] nvarchar(256)
		, [ReferenceType] tinyint
	)

	IF @ResultType IN (0, 1)
	BEGIN

		INSERT INTO #Results
		(
			[Title]
			, [Description]
			, [ModifiedOn]
			, [Key]
			, [PrimaryImagePathFormat]
			, [ReferenceType]
		)
		SELECT TOP(@Top)
			[Item].[Title]
			, [Item].[Description]
			, [Item].[CreatedOn]
			, [Item].[Key]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemImage] WITH (NOLOCK)
				WHERE [ItemIdentifier] = [Item].[Identifier]
				ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
			, 2 AS ReferenceType
		FROM [Goods].[vwItem] [Item] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
				ON [Item].[UserIdentifier] = [Profile].[UserIdentifier]
					AND [Profile].[PrivacyLevel] = 1
					AND [Item].[MinimumPrivacyLevel] = 1
		ORDER BY [Item].[CreatedOn] DESC

	END
	
	IF @ResultType IN (0, 2)
	BEGIN

		INSERT INTO #Results
		(
			[Title]
			, [Description]
			, [ModifiedOn]
			, [Key]
			, [PrimaryImagePathFormat]
			, [ReferenceType]
		)
		SELECT TOP(@Top)
			[request].[Title]
			, [request].[Description]
			, [request].[CreatedOn]
			, [request].[Key]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
				WHERE [ItemRequestIdentifier] = [request].[Identifier]
				ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
			, 3
		FROM [Goods].[vwItemRequest] [request] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
				ON [request].[UserIdentifier] = [Profile].[UserIdentifier]
					AND [Profile].[PrivacyLevel] = 1
		ORDER BY [request].[CreatedOn] DESC

	END

	SELECT  TOP(@Top)
		[Title]
		, [Description]
		, [ModifiedOn]
		, [Key]
		, [PrimaryImagePathFormat]
		, [ReferenceType]
	FROM #Results
	WHERE @WithImages = 0
		OR [PrimaryImagePathFormat] IS NOT NULL
	ORDER BY [ModifiedOn] DESC

END