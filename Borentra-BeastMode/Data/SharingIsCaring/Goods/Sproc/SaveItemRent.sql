CREATE PROCEDURE [Goods].[SaveItemRent]
	@ItemIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
    , @Price MONEY = 0
    , @PerUnit TINYINT = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Price = [dbo].[EnsureSetMoney](@Price, 0);
	SET @PerUnit = [dbo].[EnsureSetTinyInt](@PerUnit, 0);
	
	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @PerUnit <> 1
	BEGIN
	
		RAISERROR(N'Per Unit must be equal to 1.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Price = 0
	BEGIN

		RAISERROR(N'Price must be greater than 0.'
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
						WHERE @ItemIdentifier = Identifier
							AND @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'Item doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Goods].[ItemRent] AS [Data]
		USING (
			SELECT 
				@ItemIdentifier AS [ItemIdentifier]
				, @Price AS [Price]
				, @PerUnit AS [PerUnit]
			) AS NewData
		ON [Data].[ItemIdentifier] = [NewData].[ItemIdentifier]
		WHEN MATCHED THEN UPDATE
		SET ModifiedOn = GETUTCDATE()
			, [Price] = [NewData].[Price]
			, [PerUnit] = [NewData].[PerUnit]
		WHEN NOT MATCHED THEN INSERT
			(
				[ItemIdentifier]
				, [Price]
				, [PerUnit]
			)
			VALUES
			(
				[NewData].[ItemIdentifier]
				, [NewData].[Price]
				, [NewData].[PerUnit]
			);

		EXECUTE [Goods].[SearchItemRent]
			@ItemIdentifier = @ItemIdentifier;

	END
END