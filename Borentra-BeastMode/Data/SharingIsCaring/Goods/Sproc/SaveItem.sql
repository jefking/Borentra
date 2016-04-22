CREATE PROCEDURE [Goods].[SaveItem]
	@Identifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @Title [nvarchar](256) = NULL
	, @Description [nvarchar](2048) = NULL
	, @Delete [bit] = NULL
	, @SharePrivacyLevel [tinyint] = 0
	, @TradePrivacyLevel [tinyint] = 0
	, @RentPrivacyLevel [tinyint] = 0
	, @FreePrivacyLevel [tinyint] = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @SharePrivacyLevel = [dbo].[EnsureSetTinyInt](@SharePrivacyLevel, 0);
	SET @TradePrivacyLevel = [dbo].[EnsureSetTinyInt](@TradePrivacyLevel, 0);
	SET @RentPrivacyLevel = [dbo].[EnsureSetTinyInt](@RentPrivacyLevel, 0);
	SET @FreePrivacyLevel = [dbo].[EnsureSetTinyInt](@FreePrivacyLevel, 0);

	SET @Title = [dbo].[TrimOrNull](@Title);
	SET @Description = [dbo].[TrimOrNull](@Description);
	
	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF (@Title IS NULL OR @Title = '')
		AND @Delete = 0
	BEGIN

		RAISERROR(N'Title must be specified.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @FreePrivacyLevel NOT BETWEEN 0 AND 4
	BEGIN

		RAISERROR(N'Free privacy level not within bounds 0-4.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;
	END
	ELSE IF @SharePrivacyLevel NOT BETWEEN 0 AND 4
	BEGIN

		RAISERROR(N'Share privacy level not within bounds 0-4.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;
	END
	ELSE IF @TradePrivacyLevel NOT BETWEEN 0 AND 4
	BEGIN

		RAISERROR(N'Trade privacy level not within bounds 0-4.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;
	END
	ELSE IF @RentPrivacyLevel NOT BETWEEN 0 AND 4
	BEGIN

		RAISERROR(N'Rent privacy level not within bounds 0-4.'
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
	ELSE
	BEGIN
	
		SET @Identifier = COALESCE(@Identifier, NEWID());

		DECLARE @Key [nvarchar](286);
		SET @Key = [dbo].[MakeKey](@Title);

		IF EXISTS (SELECT 0
					FROM [Goods].[Item] WITH (NOLOCK)
					WHERE [Key] = @Key
						AND Identifier <> @Identifier)
		BEGIN
		
			SET @Key = [dbo].[MakeKeyUnique](@Title, @Identifier);

		END

		MERGE [Goods].[Item] AS Items
		USING (
			SELECT @Identifier AS Identifier
				, @UserIdentifier AS UserIdentifier
				, @Title AS Title
				, @Description AS [Description]
				, @Delete AS [Delete]
				, @Key AS [Key]
				, @SharePrivacyLevel AS [SharePrivacyLevel]
				, @TradePrivacyLevel AS [TradePrivacyLevel]
				, @RentPrivacyLevel AS [RentPrivacyLevel]
				, @FreePrivacyLevel AS [FreePrivacyLevel]
			) AS NewData
		ON [Items].[Identifier] = [NewData].[Identifier]
			AND [Items].[UserIdentifier] = [NewData].[UserIdentifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Items].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
				, [Items].[ModifiedOn] = GETUTCDATE()
				, [Items].[Title] = CASE [NewData].[Delete] WHEN 1 THEN [Items].[Title] ELSE [NewData].[Title] END 
				, [Items].[Description] = CASE [NewData].[Delete] WHEN 1 THEN [Items].[Description] ELSE [NewData].[Description] END
				, [Items].[SharePrivacyLevel] = CASE [NewData].[SharePrivacyLevel] WHEN 0 THEN [Items].[SharePrivacyLevel] ELSE [NewData].[SharePrivacyLevel] END
				, [Items].[TradePrivacyLevel] = CASE [NewData].[TradePrivacyLevel] WHEN 0 THEN [Items].[TradePrivacyLevel] ELSE [NewData].[TradePrivacyLevel] END
				, [Items].[RentPrivacyLevel] = CASE [NewData].[RentPrivacyLevel] WHEN 0 THEN [Items].[RentPrivacyLevel] ELSE [NewData].[RentPrivacyLevel] END
				, [Items].[FreePrivacyLevel] = CASE [NewData].[FreePrivacyLevel] WHEN 0 THEN [Items].[FreePrivacyLevel] ELSE [NewData].[FreePrivacyLevel] END
		WHEN NOT MATCHED
		THEN INSERT
			(
				[Identifier]
				, [UserIdentifier]
				, [Title]
				, [Description]
				, [Key]
				, [SharePrivacyLevel]
				, [TradePrivacyLevel]
				, [RentPrivacyLevel]
				, [FreePrivacyLevel]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[UserIdentifier]
				, [NewData].[Title]
				, [NewData].[Description]
				, [NewData].[Key]
				, CASE [NewData].[SharePrivacyLevel] WHEN 0 THEN 4 ELSE [NewData].[SharePrivacyLevel] END
				, CASE [NewData].[TradePrivacyLevel] WHEN 0 THEN 4 ELSE [NewData].[TradePrivacyLevel] END
				, CASE [NewData].[RentPrivacyLevel] WHEN 0 THEN 4 ELSE [NewData].[RentPrivacyLevel] END
				, CASE [NewData].[FreePrivacyLevel] WHEN 0 THEN 4 ELSE [NewData].[FreePrivacyLevel] END
			);

		EXECUTE [Goods].[GetItem]
			@Identifier = @Identifier
			, @CallerIdentifier = @UserIdentifier;

	END
END