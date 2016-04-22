CREATE PROCEDURE [Goods].[SearchItemImage]
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

		SELECT [image].[Identifier]
			, [ItemIdentifier]
			, [Path] AS [VirtualPathFormat]
			, [FileName]
			, [ContentType]
			, [FileSize]
			, [IsPrimary]
			, [image].[ModifiedOn]
			, [item].[UserIdentifier]
		FROM [Goods].[vwItemImage] [image] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItem] [item] WITH (NOLOCK)
				ON [image].[ItemIdentifier] = [item].[Identifier]
					AND @ItemIdentifier = [ItemIdentifier]
					AND [image].[Identifier] = COALESCE(@Identifier, [image].[Identifier])

	END
END