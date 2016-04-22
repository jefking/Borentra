CREATE PROCEDURE [Admin].[SetTopLocation]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	DECLARE @UserIdentifier uniqueidentifier
	DECLARE @Geo sys.geography

	SELECT TOP (1) @UserIdentifier = UserIdentifier
		, @Geo = [profile].[GeoLocation]
	FROM [User].[vwProfile] [profile] WITH(NOLOCK)
	WHERE [profile].[Location] IS NULL
		AND GeoLocation IS NOT NULL
	ORDER BY [profile].[ModifiedOn] DESC

	IF @UserIdentifier IS NOT NULL
		AND @Geo IS NOT NULL
	BEGIN

		DECLARE @Location nvarchar(100)

		SELECT TOP (1) @Location = City  + ', ' + Region + ', ' + Country
		FROM [Geo].[Location] WITH(NOLOCK)
		ORDER BY @Geo.STDistance([location].[GeoSpatial])
		
		UPDATE [User].[Profile]
		SET Location = COALESCE(@Location, '')
			, ModifiedOn = GETUTCDATE()
		WHERE UserIdentifier = @UserIdentifier
	END
END