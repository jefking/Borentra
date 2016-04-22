CREATE PROCEDURE [User].[SearchUserActivity]
	@UserIdentifier [uniqueidentifier] = NULL
	, @Top [tinyint] = 10
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetTinyInt](@Top, 10);

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		CREATE TABLE #Results
		(
			[Identifier] uniqueidentifier
			, [ReferenceIdentifier] uniqueidentifier
			, [ReferenceKey] nvarchar(286)
			, [UserIdentifier] uniqueidentifier
			, [Text] nvarchar(512)
			, [Type] tinyint
			, [ModifiedOn] smalldatetime
			, [UserFacebookId] bigint
			, [UserKey] nvarchar(286)
			, [UserDisplayName] nvarchar(256)
			, [ImagePathFormat] nvarchar(256)
		)
		
		-- Blank reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
		)
		SELECT TOP(@Top) [Identifier]
			, [activity].[UserIdentifier]
			, [Text]
			, [Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
					AND [Type] IN (0, 4)
					AND @UserIdentifier = [activity].[UserIdentifier]
		ORDER BY [activity].[ModifiedOn] DESC
		
		-- User reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [ReferenceIdentifier]
			, [ReferenceKey]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
		)
		SELECT TOP(@Top) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [profile].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [user].[FacebookId]
			, [user].[Key]
			, [user].[DisplayName]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [profile].[UserIdentifier]
					AND @UserIdentifier = [activity].[UserIdentifier]
					AND [Type] = 1
			INNER JOIN [User].[vwProfile] [user] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [user].[UserIdentifier]
		ORDER BY [activity].[ModifiedOn] DESC
		
		-- Item reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [ReferenceIdentifier]
			, [ReferenceKey]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
			, [ImagePathFormat]
		)
		SELECT TOP(@Top) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [item].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemImage] WITH (NOLOCK)
				WHERE [ItemIdentifier] = [item].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItem] [item] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [item].[Identifier]
					AND @UserIdentifier = [activity].[UserIdentifier]
					AND [Type] = 2
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
		ORDER BY [activity].[ModifiedOn] DESC
		
		-- Item Request reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [ReferenceIdentifier]
			, [ReferenceKey]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
			, [ImagePathFormat]
		)
		SELECT TOP(@Top) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [request].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
				WHERE [ItemRequestIdentifier] = [request].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemRequest] [request] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [request].[Identifier]
					AND @UserIdentifier = [activity].[UserIdentifier]
					AND [Type] = 3
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
		ORDER BY [activity].[ModifiedOn] DESC
		
		-- Item Image reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [ReferenceIdentifier]
			, [ReferenceKey]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
			, [ImagePathFormat]
		)
		SELECT TOP(@Top) [activity].[Identifier]
			, [item].[Identifier]
			, [item].[Key]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [image].[Path] AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemImage] [image] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [image].[Identifier]
					AND @UserIdentifier = [activity].[UserIdentifier]
					AND [Type] = 5
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
			INNER JOIN [Goods].[vwItem] [item] WITH(NOLOCK)
				ON [image].[ItemIdentifier] = [item].[Identifier]
		ORDER BY [activity].[ModifiedOn] DESC
	
		-- Item Request Image reference types
		INSERT INTO #Results
		(
			[Identifier]
			, [ReferenceIdentifier]
			, [ReferenceKey]
			, [UserIdentifier]
			, [Text]
			, [Type]
			, [ModifiedOn]
			, [UserFacebookId]
			, [UserKey]
			, [UserDisplayName]
			, [ImagePathFormat]
		)
		SELECT TOP(@Top) [activity].[Identifier]
			, [request].[Identifier]
			, [request].[Key]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [image].[Path] AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemRequestImage] [image] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [image].[Identifier]
					AND @UserIdentifier = [activity].[UserIdentifier]
					AND [Type] = 6
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
			INNER JOIN [Goods].[vwItemRequest] [request] WITH(NOLOCK)
				ON [image].[ItemRequestIdentifier] = [request].[Identifier]
		ORDER BY [activity].[ModifiedOn] DESC

		SELECT TOP(@Top)
			[Identifier]
			, [results].[ReferenceIdentifier]
			, [ReferenceKey]
			, [ModifiedOn]
			, [results].[UserIdentifier]
			, [UserFacebookId]
			, [UserDisplayName]
			, [UserKey]
			, [Text]
			, [Type]
			, 1 AS [UserContext]
			, (SELECT COUNT(0)
				FROM [Social].[vwFavorite] [favorite] WITH(NOLOCK)
				WHERE [favorite].[ReferenceIdentifier] = [results].[Identifier]
				) AS [FavoriteCount]
			, (SELECT COUNT(0)
				FROM [Social].[vwComment] [comment] WITH(NOLOCK)
				WHERE [comment].[ReferenceIdentifier] = [results].[Identifier]
				) AS [CommentCount]
			, [ImagePathFormat]
		FROM #Results [results] WITH(NOLOCK)
		ORDER BY [ModifiedOn] DESC

		CLEANUP:
			DROP TABLE #Results;

	END
END