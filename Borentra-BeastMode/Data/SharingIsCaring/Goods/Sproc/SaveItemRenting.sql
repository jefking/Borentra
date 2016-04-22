CREATE PROCEDURE [Goods].[SaveItemRenting]
	@Identifier UNIQUEIDENTIFIER = NULL
	, @ItemIdentifier UNIQUEIDENTIFIER = NULL
    , @UserIdentifier UNIQUEIDENTIFIER = NULL
    , @On SMALLDATETIME = NULL
    , @Until SMALLDATETIME = NULL
	, @ReturnedOn SMALLDATETIME = NULL
	, @Delete bit = 0
	, @Comment nvarchar(2048) = NULL
	, @Status tinyint = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Status = [dbo].[EnsureSetTinyInt](@Status, 0);
	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @Identifier = COALESCE(@Identifier, NEWID());
	
	IF @Status NOT BETWEEN 0 AND 3
	BEGIN

		RAISERROR(N'Status must be valid.'
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
	ELSE IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 0
		AND NOT EXISTS (SELECT 0
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
	ELSE IF [dbo].[UUIDIsInvalid](@ItemIdentifier) = 0
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItem] [item] WITH (NOLOCK)
						WHERE @ItemIdentifier = [item].[Identifier]
							AND RentPrivacyLevel NOT IN (0, 4))
	BEGIN

		RAISERROR(N'Item doesn''t exist, or cannot be rented.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @On >= @Until
	BEGIN
	
		RAISERROR(N'On must be greater than until.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		DECLARE @Price money;
		DECLARE @Unit tinyint;

		SELECT @Price = [Price]
			, @Unit = [PerUnit]
		FROM [Goods].[vwItemRent] WITH(NOLOCK)
		WHERE [ItemIdentifier] = @ItemIdentifier

		SET @Price = [dbo].[ComputePrice](@Price, @Unit, @On, @Until);
		
		MERGE [Goods].[ItemRenting] AS [Data]
		USING (
			SELECT @Identifier AS [Identifier]
				, @ItemIdentifier AS [ItemIdentifier]
				, @UserIdentifier AS [UserIdentifier]
				, @On AS [On]
				, @Until AS [Until]
				, @ReturnedOn AS [ReturnedOn]
				, @Status AS [Status]
				, @Delete AS [Delete]
				, @Price AS [Price]
			) AS NewData
		ON [Data].[Identifier] = [NewData].[Identifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Data].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
				, [Data].[ModifiedOn] = GETUTCDATE()
				, [Data].[On] = COALESCE([NewData].[On], [Data].[On])
				, [Data].[Until] = COALESCE([NewData].[Until], [Data].[Until])
				, [Data].[ReturnedOn] = CASE [NewData].[Status] WHEN 2 THEN COALESCE([NewData].[ReturnedOn], GETUTCDATE()) ELSE NULL END
				, [Data].[Status] = COALESCE([NewData].[Status], [Data].[Status])
				, [Data].[Price] = COALESCE([NewData].[Price], [Data].[Price])
		WHEN NOT MATCHED
		THEN INSERT
			(
				[Identifier]
				, [ItemIdentifier]
				, [UserIdentifier]
				, [On]
				, [Until]
				, [Price]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[ItemIdentifier]
				, [NewData].[UserIdentifier]
				, [NewData].[On]
				, [NewData].[Until]
				, [NewData].[Price]
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
		
		EXECUTE [Goods].[SearchItemRenting]
			@Identifier = @Identifier;

	END
END