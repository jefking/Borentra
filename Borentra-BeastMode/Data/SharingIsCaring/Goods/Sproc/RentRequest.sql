CREATE PROCEDURE [Goods].[RentRequest]
	@ItemIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @On [SmallDateTime] = NULL
	, @Until [SmallDateTime] = NULL
	, @Comment [nvarchar](2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @On = COALESCE(@On, GETUTCDATE());

	IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 1
	BEGIN

		RAISERROR(N'Item identifier must be specified and valid.'
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
	ELSE IF EXISTS (SELECT 0
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE Identifier = @ItemIdentifier
							AND UserIdentifier = @UserIdentifier)
	BEGIN

		RAISERROR(N'User cannot borrow their own items.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF EXISTS (SELECT 0
						FROM Goods.[vwItemRenting] [renting] WITH (NOLOCK)
						INNER JOIN [Goods].[vwItem] [item] WITH(NOLOCK)
							ON [renting].[ItemIdentifier] = [item].[Identifier]
								AND [ItemIdentifier] = @ItemIdentifier
								AND [renting].[UserIdentifier] = @UserIdentifier
								AND [ReturnedOn] IS NULL
								AND [Status] BETWEEN 0 AND 1)
	BEGIN

		RAISERROR(N'User cannot queue rent more than once.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		EXECUTE [Goods].[SaveItemRenting]
			@ItemIdentifier = @ItemIdentifier
			, @UserIdentifier = @UserIdentifier
			, @On = @On
			, @Until = @Until
			, @Comment = @Comment

	END
END