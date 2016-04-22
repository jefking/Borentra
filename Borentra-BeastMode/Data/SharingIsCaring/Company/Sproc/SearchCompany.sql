CREATE PROCEDURE [Company].[SearchCompany]
	@Identifier UNIQUEIDENTIFIER = NULL
	, @Key NVARCHAR(286) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Key = [dbo].[TrimOrNull](@Key);

	SELECT [Identifier] 
		, [Name]
		, [LogoPath]
		, [Description]
		, [WebsiteUrl]
		, [PhoneNumber]
		, [Email]
		, [Key]
		, [Location]
		, [GeoLocation].Lat AS [Latitude]
		, [GeoLocation].Long AS [Longitude]
		, [CreatedOn]
	FROM [Company].[vwCompany] WITH(NOLOCK)
	WHERE [Identifier] = COALESCE(@Identifier, [Identifier])
		AND [Key] = COALESCE(@Key, [Key])

END