CREATE PROCEDURE [User].[SaveProfile]
	@UserIdentifier [uniqueidentifier] = NULL
	, @DisplayName [nvarchar](256) = NULL
	, @Status [nvarchar](512) = NULL
	, @IdentityProvider [nvarchar](128) = NULL
	, @FacebookAccessToken [nvarchar](256) = NULL
	, @FacebookTokenExpiration [DateTime] = NULL
	, @Location [nvarchar](128) = NULL
	, @FacebookId bigint = 0
	, @Email nvarchar(512) = NULL
	, @PrivacyLevel [tinyint] = 0
	, @Latitude float = NULL
	, @Longitude float = NULL
	, @SearchRadius int = NULL
	, @Delete bit = 0
	, @IpAddress nvarchar(15) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @FacebookId = [dbo].[EnsureSetBigInt](@FacebookId, 0);
	SET @PrivacyLevel = [dbo].[EnsureSetTinyInt](@PrivacyLevel, 1);
	SET @Latitude = [dbo].[EnsureSetFloat](@Latitude, 0);
	SET @Longitude = [dbo].[EnsureSetFloat](@Longitude, 0);
	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	
	SET @DisplayName = [dbo].[TrimOrNull](@DisplayName);
	SET @Status = [dbo].[TrimOrNull](@Status);
	SET @IdentityProvider = [dbo].[TrimOrNull](@IdentityProvider);
	SET @FacebookAccessToken = [dbo].[TrimOrNull](@FacebookAccessToken);
	SET @Location = [dbo].[TrimOrNull](@Location);
	SET @Email = [dbo].[TrimOrNull](@Email);
	SET @IpAddress = [dbo].[TrimOrNull](@IpAddress);

	IF @SearchRadius < 100000
	BEGIN

		SET @SearchRadius = NULL;

	END

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @PrivacyLevel NOT BETWEEN 0 AND 4
	BEGIN

		RAISERROR(N'Privacy level not within bounds 0-4.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Delete = 1
		AND NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User must exist when deleting.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		DECLARE @Key [nvarchar](164);
		SET @Key = [dbo].[MakeKey](@DisplayName);

		IF EXISTS (SELECT 0
					FROM [User].[Profile] WITH (NOLOCK)
					WHERE [Key] = @Key
						AND UserIdentifier <> @UserIdentifier)
		BEGIN
		
			SET @Key = [dbo].[MakeKeyUnique](@DisplayName, @UserIdentifier);

		END
		
		DECLARE @GeoLocation sys.geography

		IF 0 <> @Latitude
			AND 0 <> @Longitude
		BEGIN

			SET @GeoLocation = geography::Point(@Latitude, @Longitude, 4326)

		END
		ELSE
		BEGIN

			SET @GeoLocation = NULL;

		END

		MERGE [User].[Profile] AS Profiles
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @DisplayName AS [DisplayName]
					, @Status AS [Status]
					, @IdentityProvider AS [IdentityProvider]
					, @FacebookAccessToken AS [FacebookAccessToken]
					, @FacebookTokenExpiration AS [FacebookTokenExpiration]
					, @Key AS [Key]
					, @Location AS [Location]
					, @FacebookId AS [FacebookId]
					, @Email AS [Email]
					, @PrivacyLevel AS [PrivacyLevel]
					, @GeoLocation AS [GeoLocation]
					, @SearchRadius AS [SearchRadius]
					, @Delete AS [Delete]
					, @IpAddress AS [IpAddress]
				) AS NewData
			ON [Profiles].[UserIdentifier] = [NewData].[UserIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Profiles].[ModifiedOn] = GETUTCDATE()
					, [Profiles].[DisplayName] = COALESCE([NewData].[DisplayName], [Profiles].[DisplayName])
					, [Profiles].[Status] = COALESCE([NewData].[Status], [Profiles].[Status])
					, [Profiles].[IdentityProvider] = COALESCE([NewData].[IdentityProvider], [Profiles].[IdentityProvider])
					, [Profiles].[FacebookAccessToken] = COALESCE([NewData].[FacebookAccessToken], [Profiles].[FacebookAccessToken])
					, [Profiles].[FacebookTokenExpiration] = COALESCE([NewData].[FacebookTokenExpiration], [Profiles].[FacebookTokenExpiration])
					, [Profiles].[Location] = COALESCE([NewData].[Location], [Profiles].[Location])
					, [Profiles].[FacebookId] = CASE [NewData].[FacebookId] WHEN 0 THEN [Profiles].[FacebookId] ELSE [NewData].[FacebookId] END
					, [Profiles].[Email] = COALESCE([NewData].[Email], [Profiles].[Email])
					, [Profiles].[PrivacyLevel] = CASE [NewData].[PrivacyLevel] WHEN 0 THEN [Profiles].[PrivacyLevel] ELSE [NewData].[PrivacyLevel] END
					, [Profiles].[GeoLocation] = COALESCE([NewData].[GeoLocation], [Profiles].[GeoLocation])
					, [Profiles].[SearchRadius] = COALESCE([NewData].[SearchRadius], [Profiles].[SearchRadius])
					, [Profiles].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
					, [Profiles].[IpAddress] = COALESCE([NewData].[IpAddress], [Profiles].[IpAddress])
			WHEN NOT MATCHED
			THEN INSERT
				(
					[UserIdentifier]
					, [DisplayName]
					, [Status]
					, [IdentityProvider]
					, [FacebookAccessToken]
					, [FacebookTokenExpiration]
					, [Key]
					, [Location]
					, [FacebookId]
					, [Email]
					, [PrivacyLevel]
					, [GeoLocation]
					, [SearchRadius]
					, [IpAddress]
				)
				VALUES
				(
					[NewData].[UserIdentifier]
					, COALESCE([NewData].[DisplayName], 'Unknown')
					, [NewData].[Status]
					, COALESCE([NewData].[IdentityProvider], 'Facebook')
					, COALESCE([NewData].[FacebookAccessToken], '')
					, COALESCE([NewData].[FacebookTokenExpiration], GETUTCDATE())
					, [NewData].[Key]
					, [NewData].[Location]
					, [NewData].[FacebookId]
					, [NewData].[Email]
					, CASE [NewData].[PrivacyLevel] WHEN 0 THEN 1 ELSE [NewData].[PrivacyLevel] END
					, [NewData].[GeoLocation]
					, COALESCE([NewData].[SearchRadius], 500000)
					, [NewData].[IpAddress]
				);

		EXECUTE [User].[GetProfile]
			@Identifier = @UserIdentifier,
			@CallerIdentifier = @UserIdentifier;

	END
END