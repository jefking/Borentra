CREATE PROCEDURE [Goods].[SaveItemRequestFulfill]
	@Identifier uniqueidentifier = NULL
	, @ItemRequestIdentifier uniqueidentifier = NULL
	, @UserIdentifier uniqueidentifier = NULL
	, @Status tinyint = NULL
	, @Delete bit = NULL
	, @WillTrade bit = NULL
	, @WillGive bit = NULL
	, @WillShare bit = NULL
	, @WillRent bit = NULL
	, @Comment nvarchar(2048) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Status = [dbo].[EnsureSetTinyInt](@Status, 0);
	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @WillTrade = [dbo].[EnsureSetBool](@WillTrade);
	SET @WillGive = [dbo].[EnsureSetBool](@WillGive);
	SET @WillShare = [dbo].[EnsureSetBool](@WillShare);
	SET @WillRent = [dbo].[EnsureSetBool](@WillRent);
	
	IF @Delete = 1
		AND [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'When Deleting Identifier must be specified.'
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
	ELSE IF [dbo].[UUIDIsInvalid](@ItemRequestIdentifier) = 1
		AND @Delete = 0
		AND @Status = 0
	BEGIN

		RAISERROR(N'Item Request identifier must be specified and valid.'
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
	ELSE IF @Delete = 1
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequestFulfill] WITH (NOLOCK)
						WHERE @Identifier = Identifier
							AND @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'Item Request Fulfill doesn''t exist, or user doesn''t own request fulfill.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@ItemRequestIdentifier) = 0
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequest] WITH (NOLOCK)
						WHERE @ItemRequestIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item Request doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Status = 0
		AND EXISTS (SELECT 0
						FROM [Goods].[vwItemRequest] WITH (NOLOCK)
						WHERE @ItemRequestIdentifier = Identifier
							AND @UserIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'Cannot fulfill your own request.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		IF [dbo].[UUIDIsInvalid](@Identifier) = 1
		BEGIN

			SELECT @Identifier = Identifier
			FROM [Goods].[vwItemRequestFulfill] WITH (NOLOCK)
			WHERE [ItemRequestIdentifier] = @itemRequestIdentifier
					AND [UserIdentifier] = @UserIdentifier

		END

		SET @Identifier = COALESCE(@Identifier, NEWID());

		MERGE [Goods].[ItemRequestFulfill] AS Data
			USING (
				SELECT @Identifier AS [Identifier]
					, @ItemRequestIdentifier AS [ItemRequestIdentifier]
					, @UserIdentifier AS [UserIdentifier]
					, @Status AS [Status]
					, @Delete AS [Delete]
					, @WillTrade AS [WillTrade]
					, @WillGive AS [WillGive]
					, @WillShare AS [WillShare]
					, @WillRent AS [WillRent]
				) AS NewData
			ON [Data].[Identifier] = [NewData].[Identifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Data].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
					, [Data].[ModifiedOn] = GETUTCDATE()
					, [Data].[Status] = [NewData].[Status]
					, [Data].[WillRent] = [NewData].[WillRent]
					, [Data].[WillTrade] = [NewData].[WillTrade]
					, [Data].[WillGive] = [NewData].[WillGive]
					, [Data].[WillShare] = [NewData].[WillShare]
			WHEN NOT MATCHED
			THEN INSERT
				(
					[Identifier]
					, [UserIdentifier]
					, [ItemRequestIdentifier]
					, [Status]
					, [WillRent]
					, [WillTrade]
					, [WillGive]
					, [WillShare]
				)
				VALUES
				(
					[NewData].[Identifier]
					, [NewData].[UserIdentifier]
					, [NewData].[ItemRequestIdentifier]
					, [NewData].[Status]
					, [NewData].[WillRent]
					, [NewData].[WillTrade]
					, [NewData].[WillGive]
					, [NewData].[WillShare]
				);

		SET @Comment = [dbo].[TrimOrNull](@Comment);

		IF @Comment IS NOT NULL
			AND @Comment <> ''
		BEGIN
		
			EXECUTE [Goods].[SaveItemActionComment]
				@ItemActionIdentifier = @Identifier
				, @UserIdentifier = @UserIdentifier
				, @Comment = @Comment

		END

		EXECUTE [Goods].[SearchItemRequestFulfill]
			@Identifier = @Identifier;

	END
END