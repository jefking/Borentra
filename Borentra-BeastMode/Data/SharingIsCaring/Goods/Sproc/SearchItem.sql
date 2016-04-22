CREATE PROCEDURE [Goods].[SearchItem]
	@UserIdentifier [uniqueidentifier] = NULL
	, @Keyword [nvarchar](64) = NULL
	, @Top smallint = 100
	, @CallerIdentifier [uniqueidentifier] = NULL
	, @ShareType tinyint = NULL
	, @FilterFriendsOnly bit = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	DECLARE @IsAdmin BIT;
	SET @IsAdmin = 0;
	
	SET @Keyword = [dbo].[TrimOrNull](@Keyword);
	SET @FilterFriendsOnly = [dbo].[EnsureSetBool](@FilterFriendsOnly);
	
	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);

	IF @Top > 2000
	BEGIN

		SET @Top = 2000;

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
	
	SET @CallerDistance = [dbo].[EnsureSetFloat](@CallerDistance, 500000); -- Default 500KM
	
	CREATE TABLE #Results
	(
		[Identifier] uniqueidentifier PRIMARY KEY
		, [MatchWeight] smallint DEFAULT(0)
		, [IsMine] bit DEFAULT(0)
		, [PrimaryImagePathFormat] nvarchar(256) DEFAULT(NULL)
	)

	INSERT INTO #Results
	(
		[Identifier]
		, [MatchWeight]
		, [IsMine]
		, [PrimaryImagePathFormat]
	)
	SELECT [Item].[Identifier]
		, [dbo].[MatchWeight]([Title], @Keyword, 11) -- Title
			+ [dbo].[MatchWeight]([Description], @Keyword, 8)
			+ [dbo].[MatchWeight]([Profile].[DisplayName], @Keyword, 7) -- Display Name
			+ [dbo].[MatchWeight]([Profile].[Location], @Keyword, 9) -- Location
			+ [dbo].[MatchWeight]([Description], '#' + @Keyword, 10) -- Double down on hash hits
			+ (SELECT SUM([dbo].[MatchWeight]([tags].[Tags], @Keyword, 6)) -- Tags
				FROM [Social].[vwTags] [tags] WITH(NOLOCK)
				WHERE [tags].[ReferenceIdentifier] = [Item].[Identifier])
			AS [MatchWeight]
		, (CASE [Item].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH (NOLOCK)
			WHERE [ItemIdentifier] = [Item].[Identifier]
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItem] [Item] WITH (NOLOCK)
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Profile].[UserIdentifier]
				AND [Item].[UserIdentifier] = COALESCE(@UserIdentifier, [Item].[UserIdentifier])
				AND -- Show Self
				(
					@CallerIdentifier IS NULL
					OR
					@UserIdentifier IS NOT NULL
					OR
					[Profile].[UserIdentifier] <> @CallerIdentifier
				)
				AND --Radial Distance
				(
					@UserIdentifier IS NOT NULL
					OR
					[Profile].[GeoLocation] IS NULL
					OR
					@CallerLocation IS NULL
					OR
					@CallerDistance >= @CallerLocation.STDistance([Profile].[GeoLocation])
				)
				AND --Share Type Filter
				(
					@ShareType IS NULL
					OR
					(
						@ShareType = 1
						AND
						[Item].[FreePrivacyLevel] = 1
					)
					OR
					(
						@ShareType = 2
						AND
						[Item].[SharePrivacyLevel] = 1
					)
					OR
					(
						@ShareType = 3
						AND
						[Item].[TradePrivacyLevel] = 1
					)
					OR
					(
						@ShareType = 4
						AND
						[Item].[RentPrivacyLevel] = 1
					)
				)
				AND -- Friends Filter
				(
					@FilterFriendsOnly = 0
					OR
					(
						@CallerIdentifier IS NOT NULL
						AND
						EXISTS (SELECT 0
								FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
								WHERE [Network].[ConnectionIdentifier] = [Profile].[UserIdentifier]
									AND [Network].[OwnerIdentifier] = @CallerIdentifier)
					)
				)
				AND -- Profile Privacy Check
				(
					[Profile].[UserIdentifier] = @CallerIdentifier
					OR
					[Profile].[PrivacyLevel] = 1
					OR
					(
						[Profile].[PrivacyLevel] = 2
						AND
						@CallerIdentifier IS NOT NULL
					)
					OR
					(
						[Profile].[PrivacyLevel] = 3
						AND EXISTS (SELECT 0
									FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
									WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
										AND [Profile].[UserIdentifier] = [Network].[OwnerIdentifier])
					)
				)
				AND --Item Security
				(
					[Item].[UserIdentifier] = @CallerIdentifier
					OR
					1 = [Item].[MinimumPrivacyLevel]
					OR
					(
						2 = [Item].[MinimumPrivacyLevel]
						AND
						@CallerIdentifier IS NOT NULL
					)
					OR
					(
						3 = [Item].[MinimumPrivacyLevel]
						AND EXISTS (SELECT 0
								FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
								WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
									AND [Item].[UserIdentifier] = [Network].[OwnerIdentifier])
					)
				)

	SELECT TOP(@Top)
		[Item].[Identifier]
		, [Item].[UserIdentifier]
		, [Item].[Title]
		, [Item].[Description]
		, [Item].[Key]
		, [Item].[CreatedOn]
		, [Item].[SharePrivacyLevel]
		, [Item].[TradePrivacyLevel]
		, [Item].[FreePrivacyLevel]
		, [Item].[RentPrivacyLevel]
		, [Profile].[FacebookId] AS [OwnerFacebookId]
		, [Profile].[DisplayName] AS [OwnerName]
		, [Profile].[Key] AS [OwnerKey]
		, [Profile].[Location]
		, [Profile].[GeoLocation].Lat AS [Latitude]
		, [Profile].[GeoLocation].Long AS [Longitude]
		, [Results].[IsMine]
		, (CASE WHEN CAST([Item].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, [Results].[PrimaryImagePathFormat]
		, [rent].[Price]
		, [rent].[PerUnit]
	FROM #Results [Results]
		INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
			ON [Results].[Identifier] = [Item].[Identifier]
				AND
				(
					@Keyword IS NULL
					OR
					[MatchWeight] > 5 --This will need to be worked on
				)
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Profile].[UserIdentifier]
		LEFT OUTER JOIN [Goods].[vwItemRent] [rent] WITH(NOLOCK)
			ON [rent].[ItemIdentifier] = [Item].[Identifier]
	ORDER BY COALESCE([MatchWeight], 0)
		+ (CASE WHEN [PrimaryImagePathFormat] IS NULL THEN 0 ELSE 1 END) -- NASTY
		DESC
		, CASE [Profile].[GeoLocation] WHEN NULL THEN 100000000 ELSE @CallerLocation.STDistance([Profile].[GeoLocation]) END

	CLEANUP:
		DROP TABLE #Results;

END