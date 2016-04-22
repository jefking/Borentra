CREATE PROCEDURE [User].[SaveActivity]
	@UserIdentifier [uniqueidentifier] = NULL
	, @ReferenceIdentifier [uniqueidentifier] = NULL
	, @Text [nvarchar](512) = NULL
	, @Type [tinyint] = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Type = [dbo].[EnsureSetTinyInt](@Type, 0);
	SET @Text = [dbo].[TrimOrNull](@Text);

	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type NOT BETWEEN 0 AND 6
	BEGIN

		RAISERROR(N'Type must be between 0 and 6.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Text IS NULL
		OR @Text = ''
	BEGIN

		RAISERROR(N'Text must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type = 1
		AND NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = UserIdentifier)
	BEGIN

		RAISERROR(N'User reference doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type = 2
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item reference doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type = 3
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequest] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item reference doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type = 5
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemImage] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item reference doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Type = 6
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequestImage] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Item reference doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
					FROM [User].[vwActivity] WITH(NOLOCK)
					WHERE ReferenceIdentifier = @ReferenceIdentifier
						AND UserIdentifier = @UserIdentifier
						AND [Text] = @Text
						AND [Type] = @Type
						AND CreatedOn > DATEADD(MINUTE, -30, GETUTCDATE()))
	BEGIN
	
		INSERT INTO [User].[Activity]
		(
			[ReferenceIdentifier]
			, [UserIdentifier]
			, [Text]
			, [Type]
		)
		VALUES
		(
			@ReferenceIdentifier
			, @UserIdentifier
			, @Text
			, @Type
		);

	END
END