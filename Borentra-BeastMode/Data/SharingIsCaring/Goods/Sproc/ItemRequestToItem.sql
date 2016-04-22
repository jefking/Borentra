CREATE PROCEDURE [Goods].[ItemRequestToItem]
	@ItemRequestIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ItemRequestIdentifier) = 1
	BEGIN

		RAISERROR(N'Item request identifier must be specified and valid.'
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
						FROM [Goods].[vwItemRequest] WITH (NOLOCK)
						WHERE @ItemRequestIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item Request doesn''t exist.'
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
		
		DECLARE @Title [nvarchar](256) = NULL
		DECLARE @Description [nvarchar](2048) = NULL

		SELECT @Title = Title
			, @Description = [Description]
		FROM [Goods].[vwItemRequest]
		WHERE Identifier = @ItemRequestIdentifier

		UPDATE [Goods].[ItemRequest]
		SET ModifiedOn = GETUTCDATE()
			, ForFree = 0
			, ForRent = 0
			, ForShare = 0
			, ForTrade = 0
		WHERE Identifier = @ItemRequestIdentifier;

		EXECUTE [Goods].[SaveItem]
			@Identifier = @Identifier
			, @UserIdentifier = @UserIdentifier
			, @Title = @Title
			, @Description = @Description
			, @SharePrivacyLevel = 4
			, @TradePrivacyLevel = 4
			, @RentPrivacyLevel = 4
			, @FreePrivacyLevel = 4

		UPDATE [Goods].[Item]
		SET FromItemRequestIdentifier = @ItemRequestIdentifier
			, ModifiedOn = GETUTCDATE()
		WHERE Identifier = @Identifier;

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
		FROM [Goods].[ItemRequestImage] WITH(NOLOCK)
		WHERE ItemRequestIdentifier = @ItemRequestIdentifier

	END
END