CREATE PROCEDURE [Goods].[BorrowDelete]
	@Identifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
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
						FROM [Goods].[vwItemShare] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier
							AND @Identifier = Identifier)
	BEGIN

		RAISERROR(N'User does not own item.'
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
			, @UserIdentifier = @UserIdentifier
			, @Delete = 1;

	END
END