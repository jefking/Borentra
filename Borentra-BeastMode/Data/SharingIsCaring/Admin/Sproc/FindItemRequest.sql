CREATE PROCEDURE [Admin].[FindItemRequest]
	@Top smallint = 100
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);
	
	SELECT TOP(@Top) [Request].[Identifier]
		, [Request].[UserIdentifier]
		, [Title]
		, [Description]
		, [Request].[CreatedOn]
		, [Request].[ModifiedOn]
		, [ForTrade]
		, [ForRent]
		, [ForFree]
		, [ForShare]
		, [Profile].[Location]
		, [Profile].[GeoLocation].Lat AS [Latitude]
		, [Profile].[GeoLocation].Long AS [Longitude]
		, [Profile].[Key] AS [OwnerKey]
		, [Profile].[DisplayName] AS [OwnerName]
		, [Profile].[FacebookId] AS [OwnerFacebookId]
		, [Request].[Key]
		, (CASE WHEN CAST([Request].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, (SELECT TOP 1 [Path]
			FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
			WHERE [ItemRequestIdentifier] = [Request].[Identifier]
			ORDER BY IsPrimary DESC) AS [PrimaryImagePathFormat]
	FROM [Goods].[vwItemRequest] [Request] WITH(NOLOCK)
		INNER JOIN [User].[vwProfile] [Profile] WITH (NOLOCK)
			ON [Request].[UserIdentifier] = [Profile].[UserIdentifier]
	ORDER BY [Request].[CreatedOn] DESC

END