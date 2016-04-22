CREATE PROCEDURE [Admin].[Archive]
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	-- ITEM
	INSERT INTO [Goods].[ItemArchive]
	(
		[ItemIdentifier]
		, [Key]
	)
	SELECT [item].[Identifier]
		, [item].[Key]
	FROM [Goods].[Item] [item] WITH (NOLOCK)
	WHERE [item].DeletedOn IS NOT NULL
		AND [item].[Identifier] NOT IN (SELECT [Identifier]
								FROM [Goods].[ItemArchive] [archive])
								
	UPDATE [item]
	SET ModifiedOn = GETUTCDATE()
		, [Key] = NEWID()
	FROM [Goods].[Item] [item]
		INNER JOIN [Goods].[ItemArchive] [archive]
			ON [item].[Identifier] = [archive].[ItemIdentifier]
				AND [item].[DeletedOn] IS NOT NULL
				AND [item].[Key] IS NOT NULL
				AND [item].[Key] = [archive].[Key]
	-- ITEM
				
	-- ITEM REQUEST
	INSERT INTO [Goods].[ItemRequestArchive]
	(
		[ItemRequestIdentifier]
		, [Key]
	)
	SELECT [item].[Identifier]
		, [item].[Key]
	FROM [Goods].[ItemRequest] [item] WITH (NOLOCK)
	WHERE [item].DeletedOn IS NOT NULL
		AND [item].[Identifier] NOT IN (SELECT [Identifier]
								FROM [Goods].[ItemRequestArchive] [archive])
								
	UPDATE [item]
	SET ModifiedOn = GETUTCDATE()
		, [Key] = NEWID()
	FROM [Goods].[ItemRequest] [item]
		INNER JOIN [Goods].[ItemRequestArchive] [archive]
			ON [item].[Identifier] = [archive].[ItemRequestIdentifier]
				AND [item].[DeletedOn] IS NOT NULL
				AND [item].[Key] IS NOT NULL
				AND [item].[Key] = [archive].[Key]
	-- ITEM REQUEST

	-- PROFILE
	INSERT INTO [User].[ProfileArchive]
	(
		[UserIdentifier]
		, [Key]
	)
	SELECT [profile].[UserIdentifier]
		, [profile].[Key]
	FROM [User].[Profile] [profile] WITH (NOLOCK)
	WHERE [profile].DeletedOn IS NOT NULL
		AND [profile].[UserIdentifier] NOT IN (SELECT [UserIdentifier]
								FROM [User].[ProfileArchive] [archive])
								
	UPDATE [profile]
	SET ModifiedOn = GETUTCDATE()
		, [Key] = NEWID()
	FROM [User].[Profile] [profile]
		INNER JOIN [User].[ProfileArchive] [archive]
			ON [profile].[UserIdentifier] = [archive].[UserIdentifier]
				AND [profile].[DeletedOn] IS NOT NULL
				AND [profile].[Key] IS NOT NULL
				AND [profile].[Key] = [archive].[Key]
	-- PROFILE

	-- COMPANY
	INSERT INTO [Company].[CompanyArchive]
	(
		[CompanyIdentifier]
		, [Key]
	)
	SELECT [company].[Identifier]
		, [company].[Key]
	FROM [Company].[Company] [company] WITH (NOLOCK)
	WHERE [company].DeletedOn IS NOT NULL
		AND [company].[Identifier] NOT IN (SELECT [CompanyIdentifier]
								FROM [Company].[CompanyArchive] [archive])
								
	UPDATE [company]
	SET ModifiedOn = GETUTCDATE()
		, [Key] = NEWID()
	FROM [Company].[Company] [company]
		INNER JOIN [Company].[CompanyArchive] [archive]
			ON [Company].[Identifier] = [archive].[CompanyIdentifier]
				AND [Company].[DeletedOn] IS NOT NULL
				AND [Company].[Key] IS NOT NULL
				AND [Company].[Key] = [archive].[Key]
	-- COMPANY
		
END