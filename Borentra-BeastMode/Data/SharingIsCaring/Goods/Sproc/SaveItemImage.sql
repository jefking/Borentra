CREATE PROCEDURE [Goods].[SaveItemImage]
	@Identifier [uniqueidentifier] = NULL
	, @ItemIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @Path [nvarchar](256) = NULL
	, @FileName [nvarchar](256) = NULL
	, @ContentType [nvarchar](64) = NULL
	, @FileSize [int] = NULL
	, @IsPrimary [bit] = 0
	, @Delete [bit] = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @IsPrimary = [dbo].[EnsureSetBool](@IsPrimary);
	SET @FileSize = [dbo].[EnsureSet](@FileSize, 0);
	SET @Path = [dbo].[TrimOrNull](@Path);
	SET @FileName = [dbo].[TrimOrNull](@FileName);
	SET @ContentType = [dbo].[TrimOrNull](@ContentType);
	
	IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 1
		AND [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'Identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Path IS NULL
		AND @Delete = 0
		AND @IsPrimary = 0
	BEGIN

		RAISERROR(N'Path must be specified.'
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
	
		IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 1
			AND [dbo].[UUIDIsInvalid](@Identifier) = 0
		BEGIN

			SELECT @ItemIdentifier = ItemIdentifier
			FROM [Goods].[vwItemImage] WITH (NOLOCK)
			WHERE Identifier = @Identifier;

		END

		IF @IsPrimary = 1
		BEGIN

			UPDATE [Goods].[ItemImage]
			SET IsPrimary = 0
				, ModifiedOn = GETUTCDATE()
			WHERE ItemIdentifier = @ItemIdentifier;

		END

		SET @Identifier = COALESCE(@Identifier, NEWID());

		MERGE [Goods].[ItemImage] AS Images
			USING (
				SELECT @Identifier AS Identifier
					, @ItemIdentifier AS ItemIdentifier
					, @UserIdentifier AS UserIdentifier
					, @Path AS [Path]
					, @FileName AS [FileName]
					, @ContentType AS ContentType
					, @FileSize AS FileSize
					, @IsPrimary AS IsPrimary
					, @Delete AS [Delete]
				) AS NewData
			ON [Images].[Identifier] = [NewData].[Identifier]
				AND [Images].[ItemIdentifier] = [NewData].[ItemIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Images].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
					, [Images].[ModifiedOn] = GETUTCDATE()
					, [Images].[IsPrimary] = [NewData].[IsPrimary]
			WHEN NOT MATCHED
			THEN INSERT
				(
					[Identifier]
					, [ItemIdentifier]
					, [Path]
					, [FileName]
					, [ContentType]
					, [FileSize]
					, [IsPrimary]
				)
				VALUES
				(
					[NewData].[Identifier]
					, [NewData].[ItemIdentifier]
					, [NewData].[Path]
					, [NewData].[FileName]
					, [NewData].[ContentType]
					, [NewData].[FileSize]
					, [NewData].[IsPrimary]
				);

			EXECUTE [Goods].[SearchItemImage]
				@Identifier = @Identifier
				, @ItemIdentifier = @ItemIdentifier;
	END
END