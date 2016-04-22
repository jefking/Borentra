CREATE PROCEDURE [Goods].[RentAccept]
	@Identifier UNIQUEIDENTIFIER = NULL
    , @CallerIdentifier UNIQUEIDENTIFIER = NULL
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
	ELSE IF [dbo].[UUIDIsInvalid](@CallerIdentifier) = 1
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
						WHERE @CallerIdentifier = UserIdentifier)
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
						WHERE @CallerIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User does not own item.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	--ELSE IF EXISTS (SELECT 0
	--				FROM [Goods].[vwItemRenting] [Share]
	--				INNER JOIN [Goods].[vwItemShare] [ExistingShares]
	--					ON [Share].[ItemIdentifier] = [ExistingShares].[ItemIdentifier]
	--						AND [Share].[Identifier] = @Identifier
	--						AND [Share].[Identifier] <> @Identifier
	--						AND [ExistingShares].[Status] = 1)
	--BEGIN

	--	RAISERROR(N'Item is already lent out.'
	--					, 15
	--					, 1
	--				)
	--				WITH SETERROR;
	--	RETURN;

	--END
	ELSE
	BEGIN

		EXECUTE [Goods].[SaveItemRenting]
			@Identifier = @Identifier
			, @UserIdentifier = @CallerIdentifier
			, @Status = 1
			, @Comment = @Comment

	END
END