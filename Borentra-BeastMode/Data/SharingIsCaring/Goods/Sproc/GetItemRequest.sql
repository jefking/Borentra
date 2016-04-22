CREATE PROCEDURE [Goods].[GetItemRequest]
	@Identifier [uniqueidentifier] = NULL
	, @Key nvarchar(286) = NULL
	, @CallerIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Key = [dbo].[TrimOrNull](@Key);
	SET @Key = LOWER(@Key);
	
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

		DECLARE @Tags VARCHAR(MAX) 

		SELECT @Tags = COALESCE(@Tags + ', ', '') + [tags].[Tags]
		FROM [Social].[vwTags] [tags] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItemRequest] [request] WITH(NOLOCK)
				ON [tags].[ReferenceIdentifier] = [request].[Identifier]
					AND [request].[Identifier] = COALESCE(@Identifier, [request].[Identifier])
					AND [request].[Key] = COALESCE(@Key, [request].[Key])
		
		SELECT TOP(1)
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
			, (CASE [Profile].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, (CASE WHEN CAST([Request].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
				WHERE [ItemRequestIdentifier] = [Request].[Identifier]
				ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
			, (CASE [Profile].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, (CASE (SELECT TOP 1 1
				FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
				WHERE [network].[OwnerIdentifier] = @CallerIdentifier
					AND [network].[ConnectionIdentifier] = [Request].[UserIdentifier]) WHEN 1 THEN 1 ELSE 0 END) AS [IsFriend]
			, @Tags AS [Tags]
		FROM [Goods].[vwItemRequest] [Request] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
				ON [Request].[UserIdentifier] = [Profile].[UserIdentifier]
					AND [Request].[Identifier] = COALESCE(@Identifier, [Request].[Identifier])
					AND [Request].[Key] = COALESCE(@Key, [Request].[Key])
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
							AND EXISTS (SELECT [Network].[ConnectionIdentifier]
										FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
										WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
											AND [Profile].[UserIdentifier] = [Network].[OwnerIdentifier])
						)
					)

	END
END