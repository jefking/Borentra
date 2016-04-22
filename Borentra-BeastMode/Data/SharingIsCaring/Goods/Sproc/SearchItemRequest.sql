CREATE PROCEDURE [Goods].[SearchItemRequest]
	@Identifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Key nvarchar(286) = NULL
	, @CallerIdentifier [uniqueidentifier] = NULL
	, @Keyword [nvarchar](64) = NULL
	, @Top smallint = 100
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);

	IF @Top > 1000
	BEGIN

		SET @Top = 1000;

	END
	
	DECLARE @CallerLocation [sys].[geography];
	DECLARE @CallerDistance [int];

	IF [dbo].[UUIDIsInvalid](@CallerIdentifier) = 1
	BEGIN
	
		SET @CallerIdentifier = NULL;

	END
	ELSE -- Valid Caller Identifier
	BEGIN
	
		SELECT @CallerLocation = GeoLocation
			, @CallerDistance = [SearchRadius]
		FROM [User].[vwProfile] WITH(NOLOCK)
		WHERE [UserIdentifier] = @CallerIdentifier

	END
	
	SET @CallerDistance = [dbo].[EnsureSetFloat](@CallerDistance, 500000); -- Default 500KM

	SET @Key = [dbo].[TrimOrNull](@Key);
	SET @Key = LOWER(@Key);
	SET @Keyword = [dbo].[TrimOrNull](@Keyword);
	
	CREATE TABLE #Results
	(
		[Identifier] uniqueidentifier PRIMARY KEY
		, [IsMine] bit DEFAULT(0)
		, [MatchWeight] smallint DEFAULT(0)
		, [PrimaryImagePathFormat] nvarchar(256) DEFAULT(NULL)
	)

	INSERT INTO #Results(
		[Identifier]
		, [IsMine]
		, [MatchWeight]
		, [PrimaryImagePathFormat]
	)
	SELECT [Identifier]
		, (CASE [Profile].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
		, [dbo].[MatchWeight]([Title], @Keyword, 40) -- Title
			+ [dbo].[MatchWeight]([Description], @Keyword, 20)
			+ [dbo].[MatchWeight]([Profile].[DisplayName], @Keyword, 30) -- Display Name
			+ [dbo].[MatchWeight]([Profile].[Location], @Keyword, 30) -- Location
			+ [dbo].[MatchWeight]([Description], '#' + @Keyword, 40) -- Double down on hash hits
			+ (SELECT SUM([dbo].[MatchWeight]([tags].[Tags], @Keyword, 25)) -- Tags
				FROM [Social].[vwTags] [tags] WITH(NOLOCK)
				WHERE [tags].[ReferenceIdentifier] = [Request].[Identifier])
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
			WHERE [ItemRequestIdentifier] = [Request].[Identifier]
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItemRequest] [Request] WITH(NOLOCK)
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Request].[UserIdentifier] = [Profile].[UserIdentifier]
				AND [Request].[Identifier] = COALESCE(@Identifier, [Request].[Identifier])
				AND [Request].[UserIdentifier] = COALESCE(@UserIdentifier, [Request].[UserIdentifier])
				AND [Request].[Key] = COALESCE(@Key, [Request].[Key])
				AND
				(
					@Key IS NOT NULL
					OR
					@Identifier IS NOT NULL
					OR
					@UserIdentifier IS NOT NULL
					OR
					[Profile].[GeoLocation] IS NULL
					OR
					@CallerLocation IS NULL
					OR
					@CallerDistance >= @CallerLocation.STDistance([Profile].[GeoLocation])
				)
				AND -- Profile Privacy Check
				(
					[Profile].[PrivacyLevel] = 1
					OR
					[Profile].[UserIdentifier] = @CallerIdentifier
					OR
					(
						[Profile].[PrivacyLevel] = 2
						AND
						@CallerIdentifier IS NOT NULL
					)
					OR
					(
						[Profile].[PrivacyLevel] = 3
						AND
						(
							EXISTS (SELECT [Network].[ConnectionIdentifier]
									FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
									WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
										AND [Profile].[UserIdentifier] = [Network].[OwnerIdentifier])
						)
					)
				)

	SELECT TOP(@Top)
		[Request].[Identifier]
		, [Request].[UserIdentifier]
		, [Title]
		, [Description]
		, [Request].[CreatedOn]
		, [Request].[ForFree]
		, [Request].[ForRent]
		, [Request].[ForTrade]
		, [Request].[ForShare]
		, [Request].[Key]
		, [Profile].[FacebookId] AS [OwnerFacebookId]
		, [Profile].[DisplayName] AS [OwnerName]
		, [Profile].[Key] AS [OwnerKey]
		, [Profile].[Location]
		, [Profile].[GeoLocation].Lat AS [Latitude]
		, [Profile].[GeoLocation].Long AS [Longitude]
		, [Results].[IsMine]
		, (CASE [Profile].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
		, (CASE WHEN CAST([Request].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, [PrimaryImagePathFormat]
	FROM [Goods].[vwItemRequest] [Request] WITH(NOLOCK)
		INNER JOIN #Results [Results]
			ON [Request].[Identifier] = [Results].[Identifier]
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Request].[UserIdentifier] = [Profile].[UserIdentifier]
				AND
				(
					@Keyword IS NULL
					OR
					[MatchWeight] > 20
				)
	ORDER BY COALESCE([MatchWeight], 0)
		+ (CASE WHEN [PrimaryImagePathFormat] IS NULL THEN 0 ELSE 1 END) -- NASTY
		DESC
		, CASE [Profile].[GeoLocation] WHEN NULL THEN 100000000 ELSE @CallerLocation.STDistance([Profile].[GeoLocation]) END

	CLEANUP:
		DROP TABLE #Results;

END