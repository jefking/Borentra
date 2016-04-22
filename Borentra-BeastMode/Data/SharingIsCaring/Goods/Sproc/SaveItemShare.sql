CREATE PROCEDURE [Goods].[SaveItemShare]
	@Identifier [uniqueidentifier] = NULL
	, @ItemIdentifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @On [SmallDateTime] = NULL
	, @Until [SmallDateTime] = NULL
	, @ReturnedOn [SmallDateTime] = NULL
	, @Status [tinyint] = 0
	, @Delete [bit] = 0
	, @Comment [nvarchar](2048) = NULL
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
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE @ItemIdentifier = Identifier
							AND SharePrivacyLevel NOT IN (0, 4))
	BEGIN

		RAISERROR(N'Item doesn''t exist, or cannot be borrowed.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Goods].[ItemShare] AS Share
		USING (
			SELECT @Identifier AS [Identifier]
				, @ItemIdentifier AS [ItemIdentifier]
				, @UserIdentifier AS [UserIdentifier]
				, @On AS [On]
				, @Until AS [Until]
				, @ReturnedOn AS [ReturnedOn]
				, @Status AS [Status]
				, @Delete AS [Delete]
			) AS NewData
		ON [Share].[Identifier] = [NewData].[Identifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Share].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
				, [Share].[ModifiedOn] = GETUTCDATE()
				, [Share].[On] = COALESCE([NewData].[On], [Share].[On])
				, [Share].[Until] = COALESCE([NewData].[Until], [Share].[Until])
				, [Share].[ReturnedOn] = CASE [NewData].[Status] WHEN 2 THEN COALESCE([NewData].[ReturnedOn], GETUTCDATE()) ELSE NULL END
				, [Share].[Status] = [NewData].[Status]
		WHEN NOT MATCHED
		THEN INSERT
			(
				[Identifier]
				, [ItemIdentifier]
				, [UserIdentifier]
				, [On]
				, [Until]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[ItemIdentifier]
				, [NewData].[UserIdentifier]
				, [NewData].[On]
				, [NewData].[Until]
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

		EXECUTE [Goods].[SearchItemShare]
			@Identifier = @Identifier;

	END
END