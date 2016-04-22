CREATE PROCEDURE [Goods].[SaveItemFree]
	@Identifier [uniqueidentifier] = NULL
	, @ItemIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @Status [tinyint] = 0
	, @Delete [bit] = 0
	, @Comment [nvarchar](2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Status = [dbo].[EnsureSetTinyInt](@Status, 0);
	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @Identifier = COALESCE(@Identifier, NEWID());
	
	IF @Status NOT BETWEEN 0 AND 3
	BEGIN

		RAISERROR(N'Status must be valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 0
		AND NOT EXISTS (SELECT 0
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
	ELSE IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 0
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE @ItemIdentifier = Identifier
							AND FreePrivacyLevel NOT IN (0, 4))
	BEGIN

		RAISERROR(N'Item doesn''t exist, or cannot be requested.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF EXISTS (SELECT 0
						FROM [Goods].[vwItemFree] WITH (NOLOCK)
						WHERE @ItemIdentifier = ItemIdentifier
							AND @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'Request has already been made.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Status IN (1,2)
		AND NOT EXISTS(SELECT 0
						FROM [Goods].[vwItem] [Item] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItemFree] [Free]
								ON [Item].[Identifier] = [Free].[ItemIdentifier]
									AND @Identifier = [Free].[Identifier]
									AND @UserIdentifier = [Item].[UserIdentifier])
	BEGIN

		RAISERROR(N'User doesn''t own item.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Goods].[ItemFree] AS [Free]
		USING (
			SELECT @Identifier AS [Identifier]
				, @ItemIdentifier AS [ItemIdentifier]
				, @UserIdentifier AS [UserIdentifier]
				, @Status AS [Status]
				, @Delete AS [Delete]
			) AS NewData
		ON [Free].[Identifier] = [NewData].[Identifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Free].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
				, [Free].[ModifiedOn] = GETUTCDATE()
				, [Free].[Status] = [NewData].[Status]
		WHEN NOT MATCHED
		THEN INSERT
			(
				[Identifier]
				, [ItemIdentifier]
				, [UserIdentifier]
				, [Status]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[ItemIdentifier]
				, [NewData].[UserIdentifier]
				, [NewData].[Status]
			);
		
		IF @Status = 1
		BEGIN
		
			DECLARE @ToUserIdentifier uniqueidentifier
			DECLARE @ItemToConvertIdentifier uniqueidentifier

			SELECT @ToUserIdentifier = UserIdentifier
				, @ItemToConvertIdentifier = ItemIdentifier
			FROM [Goods].[ItemFree]
			WHERE Identifier = @Identifier

			EXECUTE [Goods].[ItemOwnerChange]
				@ItemIdentifier = @ItemToConvertIdentifier
				, @UserIdentifier = @ToUserIdentifier

		END

		SET @Comment = [dbo].[TrimOrNull](@Comment);

		IF @Comment IS NOT NULL
			AND @Comment <> ''
		BEGIN
		
			EXECUTE [Goods].[SaveItemActionComment]
				@ItemActionIdentifier = @Identifier
				, @UserIdentifier = @UserIdentifier
				, @Comment = @Comment

		END

		EXECUTE [Goods].[SearchItemFree]
			@Identifier = @Identifier;

	END
END