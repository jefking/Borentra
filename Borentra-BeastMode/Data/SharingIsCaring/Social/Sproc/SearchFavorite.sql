CREATE PROCEDURE [Social].[SearchFavorite]
	@ReferenceIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ReferenceIdentifier) = 1
	BEGIN

		RAISERROR(N'Reference identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		SELECT [fav].ReferenceIdentifier
			, [fav].UserIdentifier
			, [profile].[DisplayName] AS [UserDisplayName]
			, [profile].[Key] AS [UserKey]
			, [profile].[FacebookId] AS [UserFacebookId]
		FROM [Social].[vwFavorite] [fav] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH (NOLOCK)
				ON [fav].[UserIdentifier] = [profile].[UserIdentifier]
					AND @ReferenceIdentifier = [fav].ReferenceIdentifier

	END
END