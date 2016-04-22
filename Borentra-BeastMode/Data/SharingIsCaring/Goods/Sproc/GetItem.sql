CREATE PROCEDURE [Goods].[GetItem]
	@Identifier [uniqueidentifier] = NULL
	, @Key nvarchar(286) = NULL
	, @CallerIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	DECLARE @IsAdmin BIT;
	SET @IsAdmin = 0;

	SET @Key = [dbo].[TrimOrNull](LOWER(@Key));
	
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

		DECLARE @Tags VARCHAR(MAX) 

		SELECT @Tags = COALESCE(@Tags + ', ', '') + [tags].[Tags]
		FROM [Social].[vwTags] [tags] WITH(NOLOCK)
			INNER JOIN [Goods].[vwItem] [item] WITH(NOLOCK)
				ON [tags].[ReferenceIdentifier] = [item].[Identifier]
					AND [item].[Identifier] = COALESCE(@Identifier, [item].[Identifier])
					AND [item].[Key] = COALESCE(@Key, [item].[Key])

		SELECT TOP (1)
			[Item].[Identifier]
			, [Item].[UserIdentifier]
			, [Item].[Title]
			, [Item].[Description]
			, [Item].[ModifiedOn]
			, [Item].[CreatedOn]
			, [Item].[Key]
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
			, (CASE [Item].[UserIdentifier] WHEN @CallerIdentifier THEN 1 ELSE 0 END) AS [IsMine]
			, (CASE WHEN CAST([Item].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
			, (SELECT TOP 1 [Share].[Status]
				FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
				WHERE [Share].[Status] = 1
					AND [Share].[ItemIdentifier] = [Item].[Identifier]) AS [Status]
			, (SELECT TOP 1 [Path]
				FROM [Goods].[vwItemImage] WITH (NOLOCK)
				WHERE [ItemIdentifier] = [Item].[Identifier]
				ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
			, [rent].[Price]
			, [rent].[PerUnit]
			, (CASE (SELECT TOP 1 1
				FROM [Social].[vwNetwork] [network] WITH (NOLOCK)
				WHERE [network].[OwnerIdentifier] = @CallerIdentifier
					AND [network].[ConnectionIdentifier] = [Item].[UserIdentifier]) WHEN 1 THEN 1 ELSE 0 END) AS [IsFriend]
			, @Tags AS [Tags]
		FROM [Goods].[vwItem] [Item] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
				ON [Item].[UserIdentifier] = [Profile].[UserIdentifier]
					AND [Item].[Identifier] = COALESCE(@Identifier, [Item].[Identifier])
					AND [Item].[Key] = COALESCE(@Key, [Item].[Key])
					AND
					(
						@IsAdmin = 1 -- Administrator can see all items.
						OR
						[Profile].[UserIdentifier] = @CallerIdentifier -- Owner can see all items.
						OR
						(
							( -- Profile Privacy Check
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
									AND EXISTS (SELECT [Network].[ConnectionIdentifier]
												FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
												WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
													AND [Profile].[UserIdentifier] = [Network].[OwnerIdentifier])
								)
							)
							AND --Item Security
							(
								1  = [Item].[MinimumPrivacyLevel]
								OR
								(
									2  = [Item].[MinimumPrivacyLevel]
									AND
									@CallerIdentifier IS NOT NULL
								)
								OR
								(
									3  = [Item].[MinimumPrivacyLevel]
									AND EXISTS (SELECT [Network].[ConnectionIdentifier]
												FROM [Social].[vwNetwork] [Network] WITH (NOLOCK)
												WHERE [Network].[ConnectionIdentifier] = @CallerIdentifier
													AND [Item].[UserIdentifier] = [Network].[OwnerIdentifier])
								)
							)
						)
					)
			LEFT OUTER JOIN [Goods].[vwItemRent] [rent] WITH(NOLOCK)
				ON [rent].[ItemIdentifier] = [Item].[Identifier]

	END
END