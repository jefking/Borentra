CREATE PROCEDURE [Goods].[SaveItemActionComment]
	@ItemActionIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @Comment [nvarchar](2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Comment = [dbo].[TrimOrNull](@Comment);

	IF @Comment IS NULL
		OR @Comment = ''
	BEGIN

		RAISERROR(N'Comment must be specified.'
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
	ELSE IF [dbo].[UUIDIsInvalid](@ItemActionIdentifier) = 1
	BEGIN

		RAISERROR(N'Item action identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE UserIdentifier = @UserIdentifier)
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemShare] WITH (NOLOCK)
						WHERE Identifier = @ItemActionIdentifier)
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemFree] WITH (NOLOCK)
						WHERE Identifier = @ItemActionIdentifier)
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequestFulfill] WITH (NOLOCK)
						WHERE Identifier = @ItemActionIdentifier)
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRenting] WITH (NOLOCK)
						WHERE Identifier = @ItemActionIdentifier)
	BEGIN

		RAISERROR(N'Item action identifier must relate to data.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		INSERT INTO [Goods].[ItemActionComment]
		(
			[ItemActionIdentifier]
			, [UserIdentifier]
			, [Comment]
		)
		VALUES
		(
			@ItemActionIdentifier
			, @UserIdentifier
			, @Comment
		);

	END
END