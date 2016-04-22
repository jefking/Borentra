CREATE PROCEDURE [Goods].[SearchItemRent]
	@Identifier [uniqueidentifier] = NULL
	, @ItemIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 1
	BEGIN

		RAISERROR(N'Item identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SELECT [ItemIdentifier]
			, [Identifier]
			, [Price]
			, [PerUnit]
		FROM [Goods].[vwItemRent] [rent] WITH (NOLOCK)
		WHERE [rent].[ItemIdentifier] = @ItemIdentifier
			AND [rent].[Identifier] = COALESCE(@Identifier, [rent].[Identifier])

	END
END