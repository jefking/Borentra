CREATE PROCEDURE [Goods].[BorrowAccept]
	@Identifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @On [SmallDateTime] = NULL
	, @Until [SmallDateTime] = NULL
	, @Comment [nvarchar](2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
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
						WHERE @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User does not own item.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF EXISTS (SELECT 0
					FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
					INNER JOIN [Goods].[vwItemShare] [ExistingShares] WITH (NOLOCK)
						ON [Share].[ItemIdentifier] = [ExistingShares].[ItemIdentifier]
							AND [Share].[Identifier] = @Identifier
							AND [Share].[Identifier] <> @Identifier
							AND [ExistingShares].[Status] = 1)
	BEGIN

		RAISERROR(N'Item is already lent out.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SET @On = COALESCE(@On, GETUTCDATE());

		EXECUTE [Goods].[SaveItemShare]
			@Identifier = @Identifier
			, @UserIdentifier = @UserIdentifier
			, @On = @On
			, @Until = @Until
			, @Status = 1
			, @Comment = @Comment

	END
END