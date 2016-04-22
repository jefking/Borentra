CREATE PROCEDURE [Social].[SearchRating]
	@ReferenceIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
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

		SELECT ReferenceIdentifier
			, UserIdentifier
			, Rating
		FROM [Social].[vwRating] WITH(NOLOCK)
		WHERE @ReferenceIdentifier = ReferenceIdentifier
			AND UserIdentifier = COALESCE(@UserIdentifier, UserIdentifier);

	END
END