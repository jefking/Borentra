CREATE PROCEDURE [Social].[CreateBadge]
	@Identifier tinyint = NULL
	, @Title nvarchar(30) = NULL
	, @Description nvarchar(65) = NULL
	, @Points tinyint = 0
	, @IconName nvarchar(30) = NULL
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @Title = [dbo].[TrimOrNull](@Title);
	SET @Description = [dbo].[TrimOrNull](@Description);
	SET @IconName = [dbo].[TrimOrNull](@IconName);
	SET @Identifier = [dbo].[EnsureSetTinyInt](@Identifier, 0);
	SET @Points = [dbo].[EnsureSetTinyInt](@Points, 0);

	IF @Identifier = 0
	BEGIN

		RAISERROR(N'Identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Points NOT BETWEEN 1 AND 10
	BEGIN

		RAISERROR(N'Points must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Description IS NULL
		OR @Description = ''
	BEGIN

		RAISERROR(N'Description must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @Title IS NULL
		OR @Title = ''
	BEGIN

		RAISERROR(N'Title must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @IconName IS NULL
		OR @IconName = ''
	BEGIN

		RAISERROR(N'Icon name must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN

		MERGE [Social].[BadgeInformation] AS [Badge]
			USING (
				SELECT @Identifier AS [Identifier]
					, @Title AS [Title]
					, @Description AS [Description]
					, @Points AS [Points]
					, @IconName AS [IconName]
				) AS NewData
			ON [Badge].[Identifier] = [NewData].[Identifier]
			WHEN MATCHED
				AND
				(
					[Badge].[Title] <> [NewData].[Title]
					OR
					[Badge].[Description] <> [NewData].[Description]
					OR
					[Badge].[Points] <> [NewData].[Points]
					OR
					[Badge].[IconName] <> [NewData].[IconName]
				)
			THEN UPDATE
				SET 
					[Title] = [NewData].[Title]
					, [Description] = [NewData].[Description]
					, [Points] = [NewData].[Points]
					, [IconName] = [NewData].[IconName]
					, [ModifiedOn] = GETUTCDATE()
			WHEN NOT MATCHED
			THEN INSERT
				(
					[Identifier]
					, [Title]
					, [Description]
					, [Points]
					, [IconName]
				)
				VALUES
				(
					[NewData].[Identifier]
					, [NewData].[Title]
					, [NewData].[Description]
					, [NewData].[Points]
					, [NewData].[IconName]
				);
	END
END