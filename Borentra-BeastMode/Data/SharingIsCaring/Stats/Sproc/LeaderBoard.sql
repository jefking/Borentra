CREATE PROCEDURE [Stats].[LeaderBoard]
	@CallerIdentifier uniqueidentifier = NULL
	, @Top smallint = 10
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Top = [dbo].[EnsureSetSmallInt](@Top, 10);

	IF [dbo].[UUIDIsInvalid](@CallerIdentifier) = 1
	BEGIN
	
		SELECT TOP(@Top) [profile].[UserIdentifier]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [profile].[FacebookId]
			, [profile].[Points]
		FROM [User].[vwProfileWithStats] [profile] WITH (NOLOCK)
		ORDER BY [Points] DESC

	END
	ELSE
	BEGIN
	
		DECLARE @CallerLocation [sys].[geography];
		DECLARE @CallerDistance [int];

		SELECT @CallerLocation = GeoLocation
			, @CallerDistance = [SearchRadius]
		FROM [User].[vwProfile] WITH(NOLOCK)
		WHERE [UserIdentifier] = @CallerIdentifier

		SET @CallerDistance = [dbo].[EnsureSetFloat](@CallerDistance, 500000); -- Default 500KM
		
		SELECT TOP(@Top) [profile].[UserIdentifier]
			, [profile].[Key]
			, [profile].[DisplayName]
			, [profile].[FacebookId]
			, [profile].[Points]
		FROM [User].[vwProfileWithStats] [profile] WITH (NOLOCK)
		WHERE @CallerDistance >= @CallerLocation.STDistance([profile].[GeoLocation])
		ORDER BY [Points] DESC

	END
END