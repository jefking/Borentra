CREATE PROCEDURE [Goods].[TradeRequest]
	@UserIdentifier [uniqueidentifier] = NULL,
	@ItemIdentifiers [nvarchar](max) = NULL 
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
BEGIN

	SET @ItemIdentifiers = [dbo].[TrimOrNull](@ItemIdentifiers);

	IF [dbo].UUIDIsInvalid(@UserIdentifier) = 1
	BEGIN

	RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @ItemIdentifiers IS NULL
	BEGIN

	RAISERROR(N'Item identifiers must be specified.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		CREATE TABLE #tmp
		(
			id uniqueidentifier
		);

		INSERT INTO #tmp
		(
			id
		)
		SELECT id
		FROM dbo.Split(@ItemIdentifiers);

		IF 2 > (SELECT count(0) FROM #tmp)
		BEGIN
		
			RAISERROR(N'A trade has to have atleast 2 items.'
							, 15
							, 1
						)
						WITH SETERROR;

		END
		ELSE IF 2 <> (SELECT count(distinct UserIdentifier)
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE Identifier in (SELECT id FROM #tmp))

		BEGIN

			RAISERROR(N'Exactly 2 users can take part in a trade, no more and no less'
							, 15
							, 1
						)
						WITH SETERROR;

		END
		ELSE
		-- TODO validate that the same trade doesn't already exist
		-- TODO validate that the user owns the items
		BEGIN

			DECLARE @outputTblTrade TABLE
			(
				TradeIdentifier uniqueidentifier
			);

			INSERT INTO Trade
			(
				UserIdentifier1
				, UserIdentifier2
			)
			OUTPUT INSERTED.Identifier
			INTO @outputTblTrade
			SELECT t1.u
				, t2.u
			FROM (SELECT UserIdentifier u
						FROM vwItem WITH (NOLOCK)
							INNER JOIN #tmp
								 ON #tmp.id = Identifier) t1,
				(SELECT UserIdentifier u
						FROM vwItem WITH (NOLOCK)
							INNER JOIN #tmp
							 ON #tmp.id = Identifier) t2
			WHERE t1.u <> t2.u 
				AND t1.u = @UserIdentifier;

		END

		DECLARE @TradeIdentifier uniqueidentifier

		SELECT @TradeIdentifier = TradeIdentifier
		FROM @outputTblTrade;

		IF @TradeIdentifier IS NOT NULL
		BEGIN

			INSERT INTO ItemTrade
			(
				TradeIdentifier
				, ItemIdentifier
			)
			SELECT
				@TradeIdentifier
				, Identifier
			FROM vwItem WITH (NOLOCK)
				INNER JOIN #tmp
					ON #tmp.id = Identifier

			EXECUTE [Goods].[SearchItemTrade]
				@TradeIdentifier = @TradeIdentifier;

		END
		
		CLEANUP:
			DROP TABLE #tmp;

	END
END