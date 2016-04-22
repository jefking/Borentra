CREATE PROCEDURE [Goods].[SearchItemRequestImage]
	@Identifier [uniqueidentifier] = NULL
	, @ItemRequestIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ItemRequestIdentifier) = 1
	BEGIN

		RAISERROR(N'Item request identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SELECT [image].[Identifier]
			, [ItemRequestIdentifier]
			, [Path] AS [VirtualPathFormat]
			, [FileName]
			, [ContentType]
			, [FileSize]
			, [IsPrimary]
			, [image].[ModifiedOn]
			, [request].[UserIdentifier]
		FROM [Goods].[vwItemRequestImage] [image] WITH (NOLOCK)
			INNER JOIN [Goods].[vwItemRequest] [request] WITH (NOLOCK)
				ON [image].[ItemRequestIdentifier] = [request].[Identifier]
					AND @ItemRequestIdentifier = [ItemRequestIdentifier]
					AND [image].[Identifier] = COALESCE(@Identifier, [image].[Identifier])

	END
END