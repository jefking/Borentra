CREATE PROCEDURE [Goods].[BorrowReturn]
	@Identifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @ReturnedOn [SmallDateTime] = NULL
	, @Comment [nvarchar](2048) = NULL
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
						FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
								ON [Share].[ItemIdentifier] = [Item].[Identifier]
									AND @UserIdentifier = [Item].[UserIdentifier]
									AND @Identifier = [Share].[Identifier])
	BEGIN

		RAISERROR(N'User does not own item which is being shared.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		EXECUTE [Goods].[SaveItemShare]
			@Identifier = @Identifier
			, @ReturnedOn = @ReturnedOn
			, @Status = 2
			, @Comment = @Comment
			, @UserIdentifier = @UserIdentifier;

	END
END