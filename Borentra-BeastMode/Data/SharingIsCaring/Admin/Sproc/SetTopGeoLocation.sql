CREATE PROCEDURE [Admin].[SetTopGeoLocation]
	@UserIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	DECLARE @Ip BIGINT

	SELECT TOP (1) @UserIdentifier = UserIdentifier
		, @Ip = [dbo].[ConvertIpAddress]([profile].[IpAddress])
	FROM [User].[vwProfile] [profile] WITH(NOLOCK)
	WHERE GeoLocation IS NULL
		AND IpAddress IS NOT NULL
		AND UserIdentifier = COALESCE(@UserIdentifier, UserIdentifier)
	ORDER BY [profile].[ModifiedOn] DESC
	
	IF @UserIdentifier IS NOT NULL
		AND @Ip IS NOT NULL
	BEGIN

		DECLARE @Geo sys.geography

		SELECT TOP (1) @Geo = GeoSpatial
		FROM [Geo].[LocationIpBlocks] [block] WITH(NOLOCK)
			INNER JOIN [Geo].[Location] [location] WITH(NOLOCK)
				ON [block].[LocationId] = [location].[LocationId]
				AND @Ip BETWEEN StartIp AND EndIp
		
		IF @Geo IS NOT NULL
		BEGIN

			UPDATE [User].[Profile]
			SET GeoLocation = @Geo
				, ModifiedOn = GETUTCDATE()
			WHERE UserIdentifier = @UserIdentifier

		END
	END
END