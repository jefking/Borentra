CREATE PROCEDURE [User].[SearchActivity]
	@UserIdentifier [uniqueidentifier] = NULL
	, @Maximum [tinyint] = 10
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Maximum = [dbo].[EnsureSetTinyInt](@Maximum, 10);

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
	
		DECLARE @CallerLocation [sys].[geography];
		DECLARE @CallerDistance [int];
		
		SELECT @CallerLocation = [GeoLocation]
			, @CallerDistance = [SearchRadius]
		FROM [User].[vwProfile] WITH(NOLOCK)
		WHERE [User].[vwProfile].UserIdentifier = @UserIdentifier

		CREATE TABLE #Friends
		(
			UserIdentifier uniqueidentifier
			, UserContext tinyint DEFAULT(0)
		)

		INSERT INTO #Friends -- User
		(
			UserIdentifier
			, UserContext
		)
		SELECT
			@UserIdentifier
			, 1 -- User

		INSERT INTO #Friends -- Friends
		(
			UserIdentifier
			, UserContext
		)
		SELECT
			ConnectionIdentifier
			, 2 -- Friend
		FROM [Social].[vwNetwork] [network] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [network].[ConnectionIdentifier] = [profile].[UserIdentifier]
					AND OwnerIdentifier = @UserIdentifier
					AND [profile].[PrivacyLevel] < 4

		INSERT INTO #Friends -- Nearby
		(
			UserIdentifier
			, UserContext
		)
		SELECT UserIdentifier
			, 3 -- Nearby
		FROM [User].[vwProfile] WITH(NOLOCK)
		WHERE UserIdentifier NOT IN (SELECT UserIdentifier
										FROM #Friends WITH(NOLOCK))
			AND @CallerDistance >= @CallerLocation.STDistance([GeoLocation])
			AND PrivacyLevel <= 2

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
			, [UserContext] tinyint DEFAULT(0)
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
			, [UserContext]
		)
		SELECT TOP(@Maximum) [Identifier]
			, [activity].[UserIdentifier]
			, [Text]
			, [Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [friends].[UserContext]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
					AND [activity].[Type] IN (0, 4)
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
			, [UserContext]
		)
		SELECT TOP(@Maximum) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [profile].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [user].[FacebookId]
			, [user].[Key]
			, [user].[DisplayName]
			, [friends].[UserContext]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
					AND [activity].[Type] = 1
			INNER JOIN [User].[vwProfile] [profile] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [profile].[UserIdentifier]
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
			, [UserContext]
			, [ImagePathFormat]
		)
		SELECT TOP(@Maximum) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [item].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [friends].[UserContext]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemImage] WITH (NOLOCK)
				WHERE [ItemIdentifier] = [item].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
					AND [activity].[Type] = 2
			INNER JOIN [Goods].[vwItem] [item] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [item].[Identifier]
				AND --Item Security
				(
					(
						[item].[UserIdentifier] = @UserIdentifier
						OR
						[item].[MinimumPrivacyLevel] IN (1, 2)
						OR
						(
							3 = [item].[MinimumPrivacyLevel]
							AND
							EXISTS (SELECT [Network].[ConnectionIdentifier]
									FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
									WHERE [Network].[ConnectionIdentifier] = @UserIdentifier
										AND [item].[UserIdentifier] = [Network].[OwnerIdentifier])
						)
					)
				)
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
			, [UserContext]
			, [ImagePathFormat]
		)
		SELECT TOP(@Maximum) [activity].[Identifier]
			, [activity].[ReferenceIdentifier]
			, [request].[Key] AS [ReferenceKey]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [friends].[UserContext]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
				WHERE [ItemRequestIdentifier] = [request].[Identifier]
				ORDER BY IsPrimary DESC) AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
			INNER JOIN [Goods].[vwItemRequest] [request] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [request].[Identifier]
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
			, [UserContext]
			, [ImagePathFormat]
		)
		SELECT TOP(@Maximum) [activity].[Identifier]
			, [item].[Identifier]
			, [item].[Key]
			, [activity].[UserIdentifier]
			, [activity].[Text]
			, [activity].[Type]
			, [activity].[ModifiedOn]
			, [profile].[FacebookId]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [friends].[UserContext]
			, [image].[Path] AS [ImagePathFormat]
		FROM [User].[vwActivity] [activity] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemImage] [image] WITH (NOLOCK)
				ON [activity].[ReferenceIdentifier] = [image].[Identifier]
					AND [Type] = 5
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
			INNER JOIN [Goods].[vwItem] [item] WITH(NOLOCK)
				ON [image].[ItemIdentifier] = [item].[Identifier]
				AND --Item Security
				(
					(
						[item].[MinimumPrivacyLevel] IN (1, 2)
						OR
						(
							3 = [item].[MinimumPrivacyLevel]
							AND
							EXISTS (SELECT [Network].[ConnectionIdentifier]
									FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
									WHERE [Network].[ConnectionIdentifier] = @UserIdentifier
										AND [item].[UserIdentifier] = [Network].[OwnerIdentifier])
						)
						OR
						(
							4 = [item].[MinimumPrivacyLevel]
							AND
							[item].[UserIdentifier] = @UserIdentifier
						)
					)
				)
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
		SELECT TOP(@Maximum) [activity].[Identifier]
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
					AND [Type] = 6
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [activity].[UserIdentifier] = [profile].[UserIdentifier]
			INNER JOIN #Friends [friends] WITH (NOLOCK)
				ON [friends].[UserIdentifier] = [activity].[UserIdentifier]
			INNER JOIN [Goods].[vwItemRequest] [request] WITH(NOLOCK)
				ON [image].[ItemRequestIdentifier] = [request].[Identifier]
		ORDER BY [activity].[ModifiedOn] DESC

		SELECT TOP(@Maximum)
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
			, [UserContext]
			, (SELECT COUNT(0)
				FROM [Social].[vwFavorite] [favorite] WITH(NOLOCK)
				WHERE [favorite].[ReferenceIdentifier] = [results].[Identifier]
				) AS [FavoriteCount]
			, (SELECT COUNT(0)
				FROM [Social].[vwComment] [comment] WITH(NOLOCK)
				WHERE [comment].[ReferenceIdentifier] = [results].[Identifier]
				) AS [CommentCount]
			, (CASE WHEN [fav].[UserIdentifier] IS NULL THEN 0 ELSE 1 END) AS [CallerFavorited]
			, [ImagePathFormat]
		FROM #Results [results] WITH(NOLOCK)
			LEFT OUTER JOIN [Social].[vwFavorite] [fav] WITH(NOLOCK)
				ON [results].[Identifier] = [fav].[ReferenceIdentifier]
					AND [fav].[UserIdentifier] = @UserIdentifier
		ORDER BY [ModifiedOn] DESC

		CLEANUP:
			DROP TABLE #Friends;
			DROP TABLE #Results;

	END
END