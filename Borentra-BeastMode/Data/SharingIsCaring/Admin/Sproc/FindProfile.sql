CREATE PROCEDURE [Admin].[FindProfile]
	@Top smallint = 100
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 100);

	SELECT TOP (@Top)
		[Profile].[UserIdentifier] AS [Identifier]
		, [Profile].[DisplayName] AS [Name]
		, [Profile].[FacebookId]
		, [Profile].[Status]
		, [Profile].[Key]
		, [Profile].[Location]
		, [Profile].[CreatedOn]
		, [Profile].[PrivacyLevel]
		, [Profile].[Latitude]
		, [Profile].[Longitude]
		, [Profile].[SearchRadius]
		, (CASE WHEN CAST([Profile].CreatedOn AS DATE) = CAST(GETUTCDATE() AS DATE) THEN 1 ELSE 0 END) AS [IsNew]
		, [Profile].[BorrowCount]
		, [Profile].[LendCount]
		, [Profile].[RecieveCount]
		, [Profile].[GiveCount]
		, [Profile].[TradeCount]
		, [Profile].[ItemRequestCount]
		, [Profile].[TradeCount]
		, [Profile].[FriendCount]
		, [Profile].[MembersNearby] AS [MembersNearbyCount]
		, [archive].[LandingTheme]
	FROM [User].[vwProfileWithStats] [Profile] WITH (NOLOCK)
		LEFT OUTER JOIN [User].[ProfileArchive] [archive] WITH(NOLOCK)
			ON [Profile].[UserIdentifier] = [archive].[UserIdentifier]
	ORDER BY [Profile].[CreatedOn] DESC

END