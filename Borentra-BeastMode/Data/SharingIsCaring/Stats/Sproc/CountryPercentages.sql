CREATE PROCEDURE [Stats].[CountryPercentages]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	CREATE TABLE #UsersByCode
	(
		IsoCode char(2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
	);

	INSERT INTO #UsersByCode
	(
		IsoCode
	)
	SELECT 
		(SELECT TOP (1) [IsoCode]
			FROM [Geo].[Country] [country] WITH(NOLOCK)
			WHERE [country].[IsInHeatMap] = 1
			ORDER BY [country].[Position].STDistance([profile].[GeoLocation]))
	FROM [User].[vwProfile] [profile] WITH(NOLOCK)
	WHERE [profile].[GeoLocation] IS NOT NULL

	DECLARE @UserCount int

	SELECT @UserCount = COUNT(0)
	FROM [User].[vwProfile] WITH(NOLOCK)
	WHERE [GeoLocation] IS NOT NULL

	SELECT IsoCode
		, ROUND((CAST(UserSum AS FLOAT) / CAST(@UserCount AS FLOAT)) * 100, 0) AS [Percentage]
	FROM 
	(
		SELECT [country].[IsoCode]
			, (
					SELECT COUNT(0)
					FROM #UsersByCode [users] WITH(NOLOCK)
					WHERE [country].[IsoCode] = [users].[IsoCode]
				) AS UserSum
		FROM [Geo].[Country] [country] WITH(NOLOCK)
		WHERE [country].[IsInHeatMap] = 1
	) [Data]

	CLEANUP:
		DROP TABLE #UsersByCode

END