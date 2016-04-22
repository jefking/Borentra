CREATE PROCEDURE [Goods].[RentReturn]
	@Identifier UNIQUEIDENTIFIER = NULL
    , @CallerIdentifier UNIQUEIDENTIFIER = NULL
	, @Comment [nvarchar](2048) = NULL
	, @ReturnedOn [SmallDateTime] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @ReturnedOn = COALESCE(@ReturnedOn, GETUTCDATE());
	
	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'Identifier must be specified and valid.'
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
	--ELSE IF NOT EXISTS (SELECT 0
	--					FROM [Goods].[vwItemRenting] [Share] WITH (NOLOCK)
	--						INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
	--							ON [Share].[ItemIdentifier] = [Item].[Identifier]
	--								AND @CallerIdentifier = [Item].[UserIdentifier]
	--								AND @Identifier = [Share].[Identifier])
	--BEGIN

	--	RAISERROR(N'User does not own item which is being shared.'
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
			, @ReturnedOn = @ReturnedOn
			, @Status = 2
			, @Comment = @Comment
	END
END