CREATE PROCEDURE [Admin].[FindItem]
	@Top smallint = 100
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);

	SELECT TOP(@Top)
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
		, (CASE WHEN CAST([Item].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemImage] WITH (NOLOCK)
			WHERE [ItemIdentifier] = [Item].[Identifier]
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
		, (SELECT TOP 1 [Share].[Status]
			FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
			WHERE [Share].[Status] = 1
				AND [Share].[ItemIdentifier] = [Item].[Identifier]) AS [Status]
		,  (SELECT TOP (1) [tags].[Tags] -- Tags
				FROM [Social].[vwTags] [tags] WITH(NOLOCK)
				WHERE [tags].[ReferenceIdentifier] = [Item].[Identifier]) AS [Tags]
	FROM [Goods].[vwItem] [Item] WITH (NOLOCK)
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Item].[UserIdentifier] = [Profile].[UserIdentifier]
	ORDER BY [Item].[CreatedOn] DESC

END