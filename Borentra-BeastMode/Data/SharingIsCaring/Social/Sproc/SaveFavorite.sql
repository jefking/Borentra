CREATE PROCEDURE [Social].[SaveFavorite]
	@ReferenceIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Delete bit = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Delete = [dbo].[EnsureSetBool](@Delete);

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Social].[Favorite] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @ReferenceIdentifier AS [ReferenceIdentifier]
					, @Delete AS [Delete]
				) AS NewData
			ON [Data].[UserIdentifier] = [NewData].[UserIdentifier]
				AND [Data].[ReferenceIdentifier] = [NewData].[ReferenceIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Data].[ModifiedOn] = GETUTCDATE()
					, [Data].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
			WHEN NOT MATCHED
			THEN INSERT
				(
					[UserIdentifier]
					, [ReferenceIdentifier]
				)
				VALUES
				(
					[NewData].[UserIdentifier]
					, [NewData].[ReferenceIdentifier]
				);
	
		EXECUTE [Social].[SearchFavorite]
			@ReferenceIdentifier = @ReferenceIdentifier

	END
END