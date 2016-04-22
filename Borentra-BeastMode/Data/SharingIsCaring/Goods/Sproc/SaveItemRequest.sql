CREATE PROCEDURE [Goods].[SaveItemRequest]
	@Identifier [uniqueidentifier] = NULL
	, @UserIdentifier [uniqueidentifier] = NULL
	, @Title [nvarchar](256) = NULL
	, @Description [nvarchar](2048) = NULL
	, @Delete [bit] = NULL
	, @ForTrade [bit] = NULL
	, @ForRent [bit] = NULL
	, @ForFree [bit] = NULL
	, @ForShare [bit] = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Delete = [dbo].[EnsureSetBool](@Delete);
	SET @ForFree = [dbo].[EnsureSetBool](@ForFree);
	SET @ForRent = [dbo].[EnsureSetBool](@ForRent);
	SET @ForTrade = [dbo].[EnsureSetBool](@ForTrade);
	SET @ForShare = [dbo].[EnsureSetBool](@ForShare);

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
					FROM [Goods].[ItemRequest] WITH (NOLOCK)
					WHERE [Key] = @Key
						AND Identifier <> @Identifier)
		BEGIN
		
			SET @Key = [dbo].[MakeKeyUnique](@Title, @Identifier);

		END

		MERGE [Goods].[ItemRequest] AS Request
			USING (
				SELECT @Identifier AS Identifier
					, @UserIdentifier AS UserIdentifier
					, @Title AS Title
					, @Description AS [Description]
					, @Delete AS [Delete]
					, @ForTrade AS [ForTrade]
					, @ForRent AS [ForRent]
					, @ForFree AS [ForFree]
					, @ForShare AS [ForShare]
					, @Key AS [Key]
				) AS NewData
			ON [Request].[Identifier] = [NewData].[Identifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Request].[DeletedOn] = CASE [NewData].[Delete] WHEN 1 THEN GETUTCDATE() ELSE NULL END
					, [Request].[ModifiedOn] = GETUTCDATE()
					, [Request].[Title] = CASE [NewData].[Delete] WHEN 1 THEN [Request].[Title] ELSE [NewData].[Title] END 
					, [Request].[Description] = CASE [NewData].[Delete] WHEN 1 THEN [Request].[Description] ELSE [NewData].[Description] END
					, [Request].[ForTrade] = [NewData].[ForTrade]
					, [Request].[ForRent] = [NewData].[ForRent]
					, [Request].[ForFree] = [NewData].[ForFree]
					, [Request].[ForShare] = [NewData].[ForShare]
			WHEN NOT MATCHED
			THEN INSERT
				(
					[Identifier]
					, [UserIdentifier]
					, [Title]
					, [Description]
					, [ForTrade]
					, [ForRent]
					, [ForFree]
					, [ForShare]
					, [Key]
				)
				VALUES
				(
					[NewData].[Identifier]
					, [NewData].[UserIdentifier]
					, [NewData].[Title]
					, [NewData].[Description]
					, [NewData].[ForTrade]
					, [NewData].[ForRent]
					, [NewData].[ForFree]
					, [NewData].[ForShare]
					, [NewData].[Key]
				);

			EXECUTE [Goods].[GetItemRequest]
				@Identifier = @Identifier
				, @CallerIdentifier = @UserIdentifier

	END
END