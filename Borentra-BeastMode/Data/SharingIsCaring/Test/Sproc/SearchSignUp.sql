CREATE PROCEDURE [Test].[SearchSignUp]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SELECT COALESCE([profile].[Email], [sign].[Email]) AS [Email]
      , [sign].[Name]
      , [sign].[Type]
      , [sign].[CreatedOn]
	  , [profile].[DisplayName]
	  , [profile].[Key]
	  , [profile].[FacebookId]
	  , [profile].[UserIdentifier]
	FROM [Test].[vwSignUp] [sign] WITH(NOLOCK)
		LEFT OUTER JOIN [User].[vwProfile] [profile] WITH (NOLOCK)
			ON [sign].[UserIdentifier] = [profile].[UserIdentifier]
	ORDER BY [sign].[CreatedOn] DESC

END