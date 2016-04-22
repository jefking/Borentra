CREATE PROCEDURE [Goods].[SearchItemActionComment]
	@ItemActionIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	IF [dbo].[UUIDIsInvalid](@ItemActionIdentifier) = 1
	BEGIN

		RAISERROR(N'Item Action identifier must be specified and valid.'
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
						FROM [Goods].[vwItemShare] [Share] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
								ON [Item].[Identifier] = [Share].[ItemIdentifier]
									AND @UserIdentifier IN ([Item].[UserIdentifier], [Share].[UserIdentifier]))
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemFree] [Free] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
								ON [Item].[Identifier] = [Free].[ItemIdentifier]
									AND @UserIdentifier IN ([Item].[UserIdentifier], [Free].[UserIdentifier]))
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRenting] [rent] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItem] [Item] WITH (NOLOCK)
								ON [Item].[Identifier] = [rent].[ItemIdentifier]
									AND @UserIdentifier IN ([Item].[UserIdentifier], [rent].[UserIdentifier]))
			AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequestFulfill] [Fulfill] WITH (NOLOCK)
							INNER JOIN [Goods].[vwItemRequest] [Request] WITH (NOLOCK)
								ON @UserIdentifier IN ([Request].[UserIdentifier], [Fulfill].[UserIdentifier])
									AND [Request].[Identifier] = [Fulfill].[ItemRequestIdentifier])
	BEGIN
	
		RAISERROR(N'User isn''t part of action or item.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SELECT [Comment].Identifier
			, [Comment].ItemActionIdentifier
			, [Comment].UserIdentifier
			, [Comment].Comment
			, [Comment].CreatedOn
			, [Commenter].[DisplayName] AS [UserName]
			, [Commenter].[FacebookId] AS [UserFacebookId]
		FROM [Goods].[vwItemActionComment] [Comment] WITH (NOLOCK)
			INNER JOIN [User].[vwProfile] [Commenter] WITH (NOLOCK)
				ON [Comment].[UserIdentifier] = [Commenter].[UserIdentifier]
					AND [Comment].[ItemActionIdentifier] = @ItemActionIdentifier

	END
END