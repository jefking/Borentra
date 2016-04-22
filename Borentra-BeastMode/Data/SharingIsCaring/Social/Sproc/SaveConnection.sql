CREATE PROCEDURE [Social].[SaveConnection]
	@OwnerIdentifier [uniqueidentifier] = NULL
	, @ConnectionIdentifier [uniqueidentifier] = NULL
	, @NetworkType [TinyInt] = 0
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON;
BEGIN

	SET @NetworkType = [dbo].[EnsureSetTinyInt](@NetworkType, 0);
	
	IF @NetworkType <= 0
		OR @NetworkType > 1
	BEGIN

		RAISERROR(N'Network must be specified and valid (1).'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	IF [dbo].[UUIDIsInvalid](@OwnerIdentifier) = 1
	BEGIN

		RAISERROR(N'Owner identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF [dbo].[UUIDIsInvalid](@ConnectionIdentifier) = 1
	BEGIN

		RAISERROR(N'Connection identifier must be specified and valid.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF @ConnectionIdentifier = @OwnerIdentifier
	BEGIN

		RAISERROR(N'Owner and Connection must be different.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE UserIdentifier = @OwnerIdentifier)
	BEGIN

		RAISERROR(N'Owner doesnt exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE IF NOT EXISTS (SELECT 0
						FROM [User].[vwProfile] WITH (NOLOCK)
						WHERE UserIdentifier = @ConnectionIdentifier)
	BEGIN

		RAISERROR(N'Connection doesnt exist.'
						, 15
						, 1
					)
					WITH SETERROR;
		RETURN;

	END
	ELSE
	BEGIN
	
		MERGE [Social].[Network] AS Network
			USING (
				SELECT @OwnerIdentifier AS [OwnerIdentifier]
					, @ConnectionIdentifier AS [ConnectionIdentifier]
					, @NetworkType AS [NetworkType]
				) AS NewData
			ON [Network].[OwnerIdentifier] = [NewData].[OwnerIdentifier]
				AND [Network].[ConnectionIdentifier] = [NewData].[ConnectionIdentifier]
			WHEN MATCHED
			THEN UPDATE
				SET [Network].[ModifiedOn] = GETUTCDATE()
			WHEN NOT MATCHED
			THEN INSERT
				(
					[OwnerIdentifier]
					, [ConnectionIdentifier]
					, [NetworkType]
				)
				VALUES
				(
					[NewData].[OwnerIdentifier]
					, [NewData].[ConnectionIdentifier]
					, [NewData].[NetworkType]
				);

	END
END