CREATE PROCEDURE [Goods].[TradeDelete]
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
						FROM [Goods].[vwTrade] WITH (NOLOCK)
						WHERE @UserIdentifier = UserIdentifier1
							AND @Identifier = Identifier)
	BEGIN

		RAISERROR(N'User did not create trade request.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
		
		UPDATE [Goods].[Trade]
		SET DeletedOn = GETUTCDATE()
			, [ModifiedOn] = GETUTCDATE()
		WHERE Identifier = @Identifier
			AND UserIdentifier1 = @UserIdentifier;

	END
END