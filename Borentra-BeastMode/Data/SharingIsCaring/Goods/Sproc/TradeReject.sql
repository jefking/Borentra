CREATE PROCEDURE [Goods].[TradeReject]
	@UserIdentifier [uniqueidentifier] = NULL,
	@TradeIdentifier [uniqueidentifier] = NULL 
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].UUIDIsInvalid(@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].UUIDIsInvalid(@TradeIdentifier) = 1
	BEGIN

		RAISERROR(N'Trade identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
				FROM [Goods].[vwTrade] WITH (NOLOCK)
				WHERE [Identifier] = @TradeIdentifier
					AND UserIdentifier2 = @UserIdentifier)
	BEGIN
		
		RAISERROR(N'User is not the recipient of this trade.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;
	END
	ELSE
	BEGIN

		UPDATE [Trade]
		SET RejectedOn = GETUTCDATE()
			, ModifiedOn = GETUTCDATE()
		WHERE Identifier = @TradeIdentifier;
		
		EXECUTE [Goods].[SearchItemTrade]
			@TradeIdentifier = @TradeIdentifier;

	END
END