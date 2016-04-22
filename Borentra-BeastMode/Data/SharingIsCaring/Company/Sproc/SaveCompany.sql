CREATE PROCEDURE [Company].[SaveCompany]
	@Identifier uniqueidentifier = NULL
    , @LogoPath NVARCHAR(256) = NULL
    , @Description NVARCHAR(1024) = NULL
    , @WebsiteUrl NVARCHAR(255) = NULL
    , @PhoneNumber NVARCHAR(25) = NULL
	, @BannerPath nvarchar(256) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @LogoPath = [dbo].[TrimOrNull](@LogoPath);
	SET @BannerPath = [dbo].[TrimOrNull](@BannerPath);
	SET @Description = [dbo].[TrimOrNull](@Description);
	SET @WebsiteUrl = [dbo].[TrimOrNull](@WebsiteUrl);
	SET @PhoneNumber = [dbo].[TrimOrNull](@PhoneNumber);

	IF [dbo].[UUIDIsInvalid](@Identifier) = 1
	BEGIN

		RAISERROR(N'New Companies must have Name specified.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		SET @Identifier = COALESCE(@Identifier, NEWID());
		
		DECLARE @Name nvarchar(256);

		SELECT @Name = [DisplayName]
		FROM [User].[vwProfile] WITH (NOLOCK)
		WHERE [UserIdentifier] = @Identifier
		
		DECLARE @Key nvarchar(286);
		SET @Key = [dbo].[MakeKey](@Name);

		IF EXISTS (SELECT 0
					FROM [Company].[Company] WITH (NOLOCK)
					WHERE [Key] = @Key
						AND Identifier <> @Identifier)
		BEGIN
		
			SET @Key = [dbo].[MakeKeyUnique](@Name, @Identifier);

		END

		MERGE [Company].[Company] AS [Data]
		USING (
			SELECT 
				@Identifier AS [Identifier]
				, @BannerPath AS [BannerPath]
				, @LogoPath AS [LogoPath]
				, @Description AS [Description]
				, @WebsiteUrl AS [WebsiteUrl]
				, @PhoneNumber AS [PhoneNumber]
				, @Key AS [Key]
			) AS NewData
		ON [Data].[Identifier] = [NewData].[Identifier]
		WHEN MATCHED THEN UPDATE
			SET [BannerPath] = COALESCE([NewData].[BannerPath], [Data].[BannerPath])
				, [LogoPath] = COALESCE([NewData].[LogoPath], [Data].[LogoPath])
				, [Description] = COALESCE([NewData].[Description], [Data].[Description])
				, [WebsiteUrl] = COALESCE([NewData].[WebsiteUrl], [Data].[WebsiteUrl])
				, [PhoneNumber] = COALESCE([NewData].[PhoneNumber], [Data].[PhoneNumber])
				, [ModifiedOn] = GETUTCDATE()
		WHEN NOT MATCHED THEN INSERT
			(
				[Identifier]
				, [BannerPath]
				, [LogoPath]
				, [Description]
				, [WebsiteUrl]
				, [PhoneNumber]
				, [Key]
			)
			VALUES
			(
				[NewData].[Identifier]
				, [NewData].[BannerPath]
				, [NewData].[LogoPath]
				, [NewData].[Description]
				, [NewData].[WebsiteUrl]
				, [NewData].[PhoneNumber]
				, [NewData].[Key]
			);

			EXECUTE [Company].[SearchCompany]
				@Identifier = @Identifier;

	END
END