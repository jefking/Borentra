CREATE PROCEDURE [Goods].[TradeAccept]
	@UserIdentifier [uniqueidentifier] = NULL
	, @TradeIdentifier [uniqueidentifier] = NULL 
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
				WHERE [Identifier] = @TradeIdentifier)
	BEGIN
		
		RAISERROR(N'Trade is not the recipient of this trade.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;
	END
	ELSE IF NOT EXISTS (SELECT 0
				FROM [Goods].[vwTrade] WITH (NOLOCK)
				WHERE [Identifier] = @TradeIdentifier
					AND [UserIdentifier2] = @UserIdentifier)
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

		DECLARE @TradeUsers TABLE
		(
			UserIdentifier1 [uniqueidentifier]
			, UserIdentifier2 [uniqueidentifier]
		);

		-- first get the 2 user ids
		INSERT INTO @TradeUsers
		(
			UserIdentifier1
			, UserIdentifier2
		)
		SELECT UserIdentifier1
			, UserIdentifier2
		FROM [Goods].[vwTrade] WITH (NOLOCK)
		WHERE Identifier = @TradeIdentifier;

		-- get the items of user 1 in the trade
		DECLARE @User1ItemIds TABLE
		(
			ItemIdentifier [UniqueIdentifier]
		);
		
		INSERT INTO @User1ItemIds
		(
			ItemIdentifier
		)	
		SELECT it.ItemIdentifier
		FROM [Goods].[vwTrade] t WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemTrade] it WITH (NOLOCK)
				ON it.TradeIdentifier = t.Identifier
					AND t.Identifier = @TradeIdentifier
			INNER JOIN [Goods].[vwItem] i WITH (NOLOCK)
				ON i.Identifier = it.ItemIdentifier
		WHERE i.UserIdentifier = (SELECT UserIdentifier1
									FROM @TradeUsers);

		-- and the items of user 2 in the trade
		DECLARE @User2ItemIds TABLE
		(
			ItemIdentifier [UniqueIdentifier]
		);

		INSERT INTO @User2ItemIds
		(
			ItemIdentifier
		)
		SELECT it.ItemIdentifier
		FROM [Goods].[vwTrade] t WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemTrade] it WITH (NOLOCK)
				ON it.TradeIdentifier = t.Identifier
					AND t.Identifier = @TradeIdentifier
			INNER JOIN [Goods].[vwItem] i WITH (NOLOCK)
				ON i.Identifier = it.ItemIdentifier
		WHERE i.UserIdentifier = (SELECT UserIdentifier2
									FROM @TradeUsers);

		UPDATE [Item]
		SET UserIdentifier = (SELECT UserIdentifier2
								FROM @TradeUsers)
			, ModifiedOn = GETUTCDATE()
		WHERE Identifier IN (SELECT ItemIdentifier
							FROM @User1ItemIds);

		UPDATE [Item]
		SET UserIdentifier = (SELECT UserIdentifier1
								FROM @TradeUsers)
			, ModifiedOn = GETUTCDATE()
		WHERE Identifier IN (SELECT ItemIdentifier
							FROM @User2ItemIds);

		UPDATE [Trade]
		SET AcceptedOn = GETUTCDATE()
			, ModifiedOn = GETUTCDATE()
		WHERE Identifier = @TradeIdentifier;
		
		EXECUTE [Goods].[SearchItemTrade]
			@TradeIdentifier = @TradeIdentifier;

	END
END