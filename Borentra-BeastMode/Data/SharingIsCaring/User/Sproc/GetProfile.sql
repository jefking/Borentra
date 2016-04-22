CREATE PROCEDURE [User].[GetProfile]
	@Identifier [uniqueidentifier] = NULL
	, @Key [nvarchar](164) = NULL
	, @CallerIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN
	
	SET @Key = [dbo].[TrimOrNull](@Key);
	SET @Key = LOWER(@Key);
	
	DECLARE @IsAdmin BIT;
	SET @IsAdmin = 0;

	-- Caller Identifier
	-- When NULL, only public (1) Profiles exposed
	-- Never 3, Unless Caller = Profile Owner
	IF [dbo].[IsAdministrator](@CallerIdentifier) = 1
	BEGIN

		SET @IsAdmin = 1;

	END

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
		AND @Key IS NULL
	BEGIN

		RAISERROR(N'Identifier or Key must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		SELECT TOP(1)
			[profile].[UserIdentifier] AS [Identifier]
			, (CASE [profile].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, (CASE WHEN CAST(CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
			, [profile].[DisplayName] AS [Name]
			, [profile].[FacebookId]
			, [profile].[Status]
			, [profile].[Key]
			, [profile].[Email]
			, [profile].[FacebookAccessToken]
			, [profile].[FacebookTokenExpiration]
			, [profile].[Location]
			, [profile].[CreatedOn]
			, [profile].[PrivacyLevel]
			, [profile].[Latitude]
			, [profile].[Longitude]
			, [profile].[SearchRadius]
			, [profile].[BorrowCount]
			, [profile].[LendCount]
			, [profile].[RecieveCount]
			, [profile].[GiveCount]
			, [profile].[TradeCount]
			, [profile].[MembersNearby] AS [PeopleNearByCount]
			, [profile].[Points]
			, [profile].[FriendsOffersCount]
			, (SELECT [archive].[LandingTheme]
				FROM [User].[vwProfileArchive] [archive] WITH(NOLOCK)
				WHERE [profile].[UserIdentifier] = [archive].[UserIdentifier]) AS [Theme]
			, (CASE (SELECT TOP 1 1
				FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
				WHERE [network].[OwnerIdentifier] = @CallerIdentifier
					AND [network].[ConnectionIdentifier] = [Profile].[UserIdentifier]) WHEN 1 THEN 1 ELSE 0 END) AS [IsFriend]
		FROM [User].[vwProfileWithStats] [profile] WITH (NOLOCK)
		WHERE [profile].[UserIdentifier] = COALESCE(@Identifier, [profile].[UserIdentifier])
			AND [profile].[Key] = COALESCE(@Key, [profile].[Key])
			AND -- Profile Privacy Check
			(
				@IsAdmin = 1
				OR
				[profile].[UserIdentifier] = @CallerIdentifier
				OR
				[profile].[PrivacyLevel] = 1
				OR
				(
					[profile].[PrivacyLevel] = 2
					AND
					@CallerIdentifier IS NOT NULL
				)
				OR
				(
					[profile].[PrivacyLevel] = 3
					AND
					(
						[profile].[UserIdentifier] = @CallerIdentifier
						OR
						EXISTS (SELECT [Network].[ConnectionIdentifier]
								FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
								WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
									AND [profile].[UserIdentifier] = [Network].[OwnerIdentifier])
					)
				)
			)

	END
END