CREATE PROCEDURE [Social].[SearchComment]
	@Identifier uniqueidentifier = NULL
	, @CallerIdentifier uniqueidentifier = NULL
	, @ReferenceIdentifier uniqueidentifier = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ReferenceIdentifier) = 1
		AND [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'Reference Identifier or Identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		SELECT ReferenceIdentifier
			, [comment].UserIdentifier
			, Comment
			, Identifier
			, [comment].CreatedOn
			, [profile].[FacebookId] AS [OwnerFacebookId]
			, [profile].[DisplayName] AS [OwnerName]
			, [profile].[Key] AS [OwnerKey]
			, (CASE WHEN @CallerIdentifier = [comment].UserIdentifier THEN 1 ELSE 0 END) AS [IsMine]
		FROM [Social].[vwComment] [comment] WITH(NOLOCK)
			INNER JOIN [User].[vwProfile] [profile] WITH(NOLOCK)
				ON [comment].[UserIdentifier] = [profile].[UserIdentifier]
					AND ReferenceIdentifier = COALESCE(@ReferenceIdentifier, [comment].ReferenceIdentifier)
					AND Identifier = COALESCE(@Identifier, [comment].Identifier)
		ORDER BY [comment].[CreatedOn] ASC

	END
END