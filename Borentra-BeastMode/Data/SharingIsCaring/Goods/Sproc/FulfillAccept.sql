CREATE PROCEDURE [Goods].[FulfillAccept]
	@Identifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Comment nvarchar(2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequestFulfill] [Fulfill] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItemRequest] [Request] WITH (NOLOCK)
								ON [Request].[UserIdentifier] = @UserIdentifier
									AND [Fulfill].[Identifier] = @Identifier
									AND [Request].[Identifier] = [Fulfill].[ItemRequestIdentifier])
	BEGIN

		RAISERROR(N'To accept, user must own item request.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		EXECUTE [Goods].[SaveItemRequestFulfill]
			@Identifier = @Identifier
			, @UserIdentifier = @UserIdentifier
			, @Comment = @Comment
			, @Status = 1

		DECLARE @ItemRequestIdentifier uniqueidentifier

		SELECT @ItemRequestIdentifier = ItemRequestIdentifier
		FROM [Goods].[vwItemRequestFulfill] WITH (NOLOCK)
		WHERE Identifier = @Identifier

		EXECUTE [Goods].[ItemRequestToItem]
			@ItemRequestIdentifier = @ItemRequestIdentifier
			, @UserIdentifier = @UserIdentifier

	END
END