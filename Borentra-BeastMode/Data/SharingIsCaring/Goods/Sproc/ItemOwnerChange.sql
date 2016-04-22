CREATE PROCEDURE [Goods].[ItemOwnerChange]
	@ItemIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 1
	BEGIN

		RAISERROR(N'Item identifier must be specified and valid.'
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
	ELSE IF NOT EXISTS (SELECT 0
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE @ItemIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		DECLARE @Identifier uniqueidentifier
		SET @Identifier = NEWID();

		DECLARE @Key nvarchar(286)
		
		SELECT @Key = [Key]
		FROM [Goods].[Item]
		WHERE Identifier = @ItemIdentifier;

		UPDATE [Goods].[Item]
		SET [Key] = NEWID()
			, ModifiedOn = GETUTCDATE()
			, FreePrivacyLevel = 4
			, RentPrivacyLevel = 4
			, SharePrivacyLevel = 4
			, TradePrivacyLevel = 4
		WHERE Identifier = @ItemIdentifier;

		INSERT INTO [Goods].[Item]
		(
			[Identifier]
			, [UserIdentifier]
			, [Title]
			, [Description]
			, [Key]
			, [TradePrivacyLevel]
			, [FreePrivacyLevel]
			, [SharePrivacyLevel]
			, [RentPrivacyLevel]
			, [FromItemIdentifier]
		)
		SELECT @Identifier
			, @UserIdentifier
			, [item].[Title]
			, [item].[Description]
			, @Key
			, [item].[TradePrivacyLevel]
			, [item].[FreePrivacyLevel]
			, [item].[SharePrivacyLevel]
			, [item].[RentPrivacyLevel]
			, [item].[Identifier]
		FROM [Goods].[Item] [item]
		WHERE [item].[Identifier] = @ItemIdentifier

		INSERT INTO [Goods].[ItemImage]
		(
			[ItemIdentifier]
			, [Path]
			, [FileName]
			, [ContentType]
			, [FileSize]
			, [IsPrimary]
		)
		SELECT
			@Identifier
			, [Path]
			, [FileName]
			, [ContentType]
			, [FileSize]
			, [IsPrimary]
		FROM [Goods].[ItemImage] WITH(NOLOCK)
		WHERE ItemIdentifier = @ItemIdentifier

	END
END