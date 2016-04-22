CREATE PROCEDURE [Company].[SaveCompanyItemRent]
	@Identifier UNIQUEIDENTIFIER = NULL
    , @ItemRentIdentifier UNIQUEIDENTIFIER = NULL
    , @Quantity TINYINT = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Quantity = [dbo].[EnsureSetTinyInt](@Quantity, 0);

	IF [dbo].[UUIDIsInvalid](@ItemRentIdentifier) = 1
	BEGIN

		RAISERROR(N'Item Rent Identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Quantity = 1
	BEGIN

		RAISERROR(N'Quantity must be greater than 0.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SET @Identifier = COALESCE(@Identifier, NEWID());

		MERGE [Company].[CompanyItemRent] AS [Data]
		USING (
			SELECT 
				@Identifier AS [Identifier]
				, @ItemRentIdentifier AS [ItemRentIdentifier]
				, @Quantity AS [Quantity]
			) AS NewData
		ON [Data].[Identifier] = [NewData].[Identifier]
		WHEN MATCHED THEN UPDATE
		SET ModifiedOn = GETUTCDATE()
			, Quantity = @Quantity
		WHEN NOT MATCHED THEN INSERT
			(
				[Identifier]
				, [ItemRentIdentifier]
				, [Quantity]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[ItemRentIdentifier]
				, [NewData].[Quantity]
			);

	END
END