CREATE PROCEDURE [Social].[SaveRating]
	@ReferenceIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Rating tinyint = 0
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
	ELSE IF @Rating NOT BETWEEN 0 AND 5
	BEGIN

		RAISERROR(N'Rating not within bounds 0-5.'
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

		MERGE [Social].[Rating] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @ReferenceIdentifier AS [ReferenceIdentifier]
					, @Rating AS [Rating]
					, @Delete AS [Delete]
				) AS NewData
			ON [Data].[UserIdentifier] = [NewData].[UserIdentifier]
				AND [Data].[ReferenceIdentifier] = [NewData].[ReferenceIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Data].[ModifiedOn] = GETUTCDATE()
					, [Data].[Rating] = COALESCE([NewData].[Rating], [Data].[Rating])
					, [Data].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
			WHEN NOT MATCHED
			THEN INSERT
				(
					[UserIdentifier]
					, [ReferenceIdentifier]
					, [Rating]
				)
				VALUES
				(
					[NewData].[UserIdentifier]
					, [NewData].[ReferenceIdentifier]
					, [NewData].[Rating]
				);

		EXECUTE [Social].[SearchRating]
			@ReferenceIdentifier = @ReferenceIdentifier
			, @UserIdentifier = @UserIdentifier

	END
END