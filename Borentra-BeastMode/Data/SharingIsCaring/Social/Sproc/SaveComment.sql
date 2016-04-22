CREATE PROCEDURE [Social].[SaveComment]
	@Identifier uniqueidentifier = NULL
	, @ReferenceIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Comment nvarchar(2048) = NULL
	, @Delete bit = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @Comment = [dbo].[TrimOrNull](@Comment);

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		SET @Identifier = NULL;

	END

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Delete = 0
		AND (@Comment IS NULL
			OR @Comment = '')
	BEGIN

		RAISERROR(N'Comment must be specified and valid.'
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
	ELSE IF EXISTS (SELECT 0
					FROM [Social].[vwComment] WITH(NOLOCK)
					WHERE [ReferenceIdentifier] = @ReferenceIdentifier
						AND [UserIdentifier] = @UserIdentifier
						AND [Comment] = @Comment)
	BEGIN

		SELECT @Identifier = [Identifier]
		FROM [Social].[vwComment] WITH(NOLOCK)
		WHERE [ReferenceIdentifier] = @ReferenceIdentifier
			AND [UserIdentifier] = @UserIdentifier
			AND [Comment] = @Comment

	END
	ELSE
	BEGIN

		SET @Identifier = COALESCE(@Identifier, NEWID());

		MERGE [Social].[Comment] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @ReferenceIdentifier AS [ReferenceIdentifier]
					, @Comment AS [Comment]
					, @Identifier AS [Identifier]
					, @Delete AS [Delete]
				) AS NewData
			ON [Data].[Identifier] = [NewData].[Identifier]
				AND [Data].[UserIdentifier] = [NewData].[UserIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Data].[ModifiedOn] = GETUTCDATE()
					, [Data].[Comment] = COALESCE([NewData].[Comment], [Data].[Comment])
					, [Data].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
			WHEN NOT MATCHED
			THEN INSERT
				(
					[UserIdentifier]
					, [Identifier]
					, [ReferenceIdentifier]
					, [Comment]
				)
				VALUES
				(
					[NewData].[UserIdentifier]
					, [newData].[Identifier]
					, [NewData].[ReferenceIdentifier]
					, [NewData].[Comment]
				);

	END

	EXECUTE [Social].[SearchComment]
		@Identifier = @Identifier;

END