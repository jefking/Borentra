CREATE PROCEDURE [Social].[SaveTags]
	@UserIdentifier uniqueidentifier = NULL
	, @ReferenceIdentifier uniqueidentifier = NULL
	, @Tags nvarchar(128) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Tags = [dbo].[TrimOrNull](@Tags);
	
	IF [dbo].[UUIDIsInvalid](@UserIdentifier) = 1
	BEGIN

		RAISERROR(N'User identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@ReferenceIdentifier) = 1
	BEGIN

		RAISERROR(N'Reference identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Tags IS NULL
			OR @Tags = ''
	BEGIN

		RAISERROR(N'Tags must be specified and valid.'
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
						FROM [Goods].[vwItem] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
		AND NOT EXISTS (SELECT 0
						FROM [Goods].[vwItemRequest] WITH (NOLOCK)
						WHERE @ReferenceIdentifier = Identifier)
	BEGIN

		RAISERROR(N'Reference Item or Request doesn''t exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		MERGE [Social].[Tags] AS Data
			USING (
				SELECT @UserIdentifier AS [UserIdentifier]
					, @ReferenceIdentifier AS [ReferenceIdentifier]
					, @Tags AS [Tags]
				) AS NewData
			ON [Data].[ReferenceIdentifier] = [NewData].[ReferenceIdentifier]
				AND [Data].[UserIdentifier] = [NewData].[UserIdentifier]
		WHEN MATCHED
		THEN UPDATE
			SET [Data].[ModifiedOn] = GETUTCDATE()
				, [Data].[Tags] = COALESCE([NewData].[Tags], [Data].[Tags])
		WHEN NOT MATCHED
		THEN INSERT
			(
				[UserIdentifier]
				, [ReferenceIdentifier]
				, [Tags]
			)
			VALUES
			(
				[NewData].[UserIdentifier]
				, [NewData].[ReferenceIdentifier]
				, [NewData].[Tags]
			);

	END
END