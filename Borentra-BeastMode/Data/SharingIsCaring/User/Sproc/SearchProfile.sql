CREATE PROCEDURE [User].[SearchProfile]
	@Identifier [uniqueidentifier] = NULL
	, @Key [nvarchar](164) = NULL
	, @ConnectionIdentifier [uniqueidentifier] = NULL
	, @Top smallint = 100
	, @Keyword [nvarchar](128) = NULL
	, @CallerIdentifier [uniqueidentifier] = NULL
	, @Radius int = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);

	IF @Top > 2000
	BEGIN

		SET @Top = 2000;

	END
	
	SET @Key = [dbo].[TrimOrNull](@Key);
	SET @Key = LOWER(@Key);

	SET @Keyword = [dbo].[TrimOrNull](@Keyword);
	
	DECLARE @IsAdmin BIT;
	SET @IsAdmin = 0;

	DECLARE @Any tinyint
	SET @Any = 0;

	IF @Identifier IS NULL
		AND @Key IS NULL
		AND @ConnectionIdentifier IS NULL
		AND @Keyword IS NULL
	BEGIN

		SET @Any = 1;

	END
	
	DECLARE @CallerLocation [sys].[geography];
	DECLARE @CallerDistance [int];

	-- Caller Identifier
	-- When NULL, only public (1) Profiles exposed
	-- When Set, Community (2)
	-- When Set, Friends (3)
	-- Never 3, Unless Caller = Profile Owner
	IF [dbo].[UUIDIsInvalid](@CallerIdentifier) = 1
	BEGIN
	
		SET @CallerIdentifier = NULL;

	END
	ELSE IF [dbo].[IsAdministrator](@CallerIdentifier) = 1
	BEGIN

		SET @IsAdmin = 1;

	END
	ELSE -- Valid Caller Identifier
	BEGIN

		SELECT @CallerLocation = GeoLocation
			, @CallerDistance = [SearchRadius]
		FROM [User].[vwProfile] WITH(NOLOCK)
		WHERE [UserIdentifier] = @CallerIdentifier

	END

	IF @Radius IS NOT NULL
	BEGIN

		SET @CallerDistance = @Radius;

	END
	ELSE
	BEGIN

		SET @CallerDistance = [dbo].[EnsureSetFloat](@CallerDistance, 100000); -- Default 100KM
	
	END

	CREATE TABLE #Results
	(
		[Identifier] uniqueidentifier PRIMARY KEY
		, [IsMine] bit DEFAULT(0)
		, [MatchWeight] smallint DEFAULT(0)
	)

	IF [dbo].[UUIDIsInvalid](@ConnectionIdentifier) = 0
	BEGIN

		INSERT INTO #Results
		(
			[Identifier]
			, [IsMine]
			, [MatchWeight]
		)
		SELECT [Profiles].[UserIdentifier] AS [Identifier]
			, (CASE [Profiles].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, CASE [Profiles].[Status] WHEN NULL THEN 0 ELSE 1 END
				+ CASE [Profiles].[Location] WHEN NULL THEN 0 ELSE 1 END
				+ 5 -- To ensure they are in the results
				AS [MatchWeight]
		FROM [User].[vwProfile] [Profiles] WITH (NOLOCK)
			INNER JOIN [Social].[vwNetwork] [Network] WITH (NOLOCK)
				ON [Network].[ConnectionIdentifier] = [Profiles].[UserIdentifier]
					AND [Network].[OwnerIdentifier] = @ConnectionIdentifier
					AND [Profiles].[PrivacyLevel] <= 3;

	END
	ELSE
	BEGIN

		INSERT INTO #Results
		(
			[Identifier]
			, [IsMine]
			, [MatchWeight]
		)
		SELECT [Profiles].[UserIdentifier] AS [Identifier]
			, (CASE [Profiles].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, CASE [Profiles].[UserIdentifier] WHEN @Identifier THEN 90 ELSE 0 END -- User Identifier
				+ [dbo].[MatchWeight]([Profiles].[Key], @Key, 90) -- Key
				+ [dbo].[MatchWeight]([Profiles].[Location], @Keyword, 10) -- Location
				+ [dbo].[MatchWeight]([Profiles].[DisplayName], @Keyword, 30) -- Display Name
				+ [dbo].[MatchWeight]([Profiles].[Status], @Keyword, 5) -- Status
				+ @Any -- For Wildcard Searches
				+ CASE [Profiles].[Status] WHEN NULL THEN 0 ELSE 1 END
				+ CASE [Profiles].[Location] WHEN NULL THEN 0 ELSE 1 END
				AS [MatchWeight]
		FROM [User].[vwProfile] [Profiles] WITH (NOLOCK)
		WHERE [Profiles].[UserIdentifier] = COALESCE(@Identifier, [Profiles].[UserIdentifier])
			AND [Profiles].[Key] = COALESCE(@Key, [Profiles].[Key])
			AND
			(
				@Key IS NOT NULL
				OR
				@Identifier IS NOT NULL
				OR
				[Profiles].[GeoLocation] IS NULL
				OR
				@CallerLocation IS NULL
				OR
				@CallerDistance >= @CallerLocation.STDistance([Profiles].[GeoLocation])
			)
			AND -- Profile Privacy Check
			(
				@IsAdmin = 1
				OR
				[Profiles].[UserIdentifier] = @CallerIdentifier
				OR
				[Profiles].[PrivacyLevel] = 1
				OR
				(
					(
						[Profiles].[PrivacyLevel] = 2
						AND
						@CallerIdentifier IS NOT NULL
					)
					OR
					(
						[Profiles].[PrivacyLevel] = 3
						AND
						(
							EXISTS (SELECT [Network].[ConnectionIdentifier]
									FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
									WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
										AND [Profiles].[UserIdentifier] = [Network].[OwnerIdentifier])
						)
					)
				)
			)
			AND -- Don't show items unless the user has requested theirs
			(
				@Identifier = @CallerIdentifier
				OR
				[Profiles].[UserIdentifier] <> @CallerIdentifier
				OR
				@Key IS NOT NULL
			)

	END

	SELECT TOP (@Top)
		[Profile].[UserIdentifier] AS [Identifier]
		, [Profile].[DisplayName] AS [Name]
		, [Profile].[FacebookId]
		, [Profile].[Key]
		, [Profile].[Location]
		, [Profile].[Latitude]
		, [Profile].[Longitude]
		, (CASE WHEN CAST(CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, (CASE (SELECT TOP 1 1
			FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
			WHERE [network].[OwnerIdentifier] = @CallerIdentifier
				AND [network].[ConnectionIdentifier] = [Profile].[UserIdentifier]) WHEN 1 THEN 1 ELSE 0 END) AS [IsFriend]
	FROM #Results [Results]
		INNER JOIN [User].[vwProfileWithStats] [Profile] WITH (NOLOCK)
			ON [Results].[Identifier] = [Profile].[UserIdentifier]
				AND
				(
					(
						@Any = 1
						AND
						[MatchWeight] > 0
					)
					OR
					(
						@Any = 0
						AND
						[MatchWeight] > 2
					)
				)
		LEFT OUTER JOIN [User].[vwProfileArchive] [archive] WITH(NOLOCK)
			ON [Profile].[UserIdentifier] = [archive].[UserIdentifier]
	ORDER BY [MatchWeight] DESC
		, CASE [Profile].[GeoLocation] WHEN NULL THEN 100000000 ELSE @CallerLocation.STDistance([Profile].[GeoLocation]) END

	CLEANUP:
		DROP TABLE #Results;

END