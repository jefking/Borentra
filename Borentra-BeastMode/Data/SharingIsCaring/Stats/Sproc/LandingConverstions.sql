CREATE PROCEDURE [Stats].[LandingConverstions]
	@Days tinyint = 7
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Days = [dbo].[EnsureSet](@Days, 7);
	
	SELECT LandingTheme AS [Theme]
		, COUNT(0) AS ConverstionCount
		, CONVERT(varchar(4), DATEPART(year, [profile].[CreatedOn]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(month, [profile].[CreatedOn]))
			+ '-'
			+ CONVERT(varchar(2), DATEPART(day, [profile].[CreatedOn]))
			AS [Date]
	FROM [User].[vwProfile] [profile] with(nolock)
		INNER JOIN [User].[ProfileArchive] [stats] with(nolock)
			ON [profile].UserIdentifier = [stats].UserIdentifier
				AND [profile].[CreatedOn] >= DATEADD(DAY, @Days * -1, GETUTCDATE())
	GROUP BY [stats].[LandingTheme]
		, DATEPART(YEAR, [profile].[CreatedOn])
		, DATEPART(MONTH, [profile].[CreatedOn])
		, DATEPART(DAY, [profile].[CreatedOn])

END